using CSScriptLib;
using DevExpress.CodeParser;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Services;
using DevExpress.XtraTreeList.Nodes;
using Pinokio.Animation;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinokio.Designer
{
    public partial class EditScriptCodeForm : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<uint, Dictionary<string, ScriptInfo>> scriptInfos = new Dictionary<uint, Dictionary<string, ScriptInfo>>();

        public EditScriptCodeForm()
        {
            InitializeComponent();
            richEditControlScriptCode.ActiveView.BackColor = Color.FromArgb(30,30,30);
            InitializeTreeNode();
        }

        /// <summary>
        /// Script가 존재하는 Node 종류들을 생성하고 하위에 Script 종류별로 TreeNode 생성
        /// </summary>
        private void InitializeTreeNode()
        {
            treeListNodeScript.BeginUnboundLoad();

            foreach (SimNode node in ModelManager.Instance.SimNodes.Values)
            {
                Type nodeType = node.GetType();

                List<FieldInfo> simObjFieldinfos = new List<FieldInfo>();
                SimNode.GetFieldInfo(nodeType, ref simObjFieldinfos);
                List<FieldInfo> scriptCodeFieldInfos = simObjFieldinfos.Where(x => x.Name.Contains(SimNode.ScriptCodeString) && typeof(SimNode).Name != x.DeclaringType.Name).ToList();
                if (scriptCodeFieldInfos.Count > 0)
                {
                    TreeListNode parentNode = treeListNodeScript.AppendNode(new object[] { node.Name, node.Name }, null);
                    scriptInfos.Add(node.ID, new Dictionary<string, ScriptInfo>());
                    foreach (FieldInfo field in scriptCodeFieldInfos)
                    {
                        string backName = field.Name.Split(new string[] { SimNode.ScriptCodeString }, System.StringSplitOptions.RemoveEmptyEntries)[1];
                        FieldInfo functionField = simObjFieldinfos.Find(x => x.Name.Contains(SimNode.ScriptFunctionString) && x.Name.Contains(backName));

                        if (functionField != null)
                        {
                            string displayName = field.Name.Split(new string[] { "<", SimNode.ScriptCodeString, ">" }, System.StringSplitOptions.RemoveEmptyEntries)[0];
                            TreeListNode treeNode = treeListNodeScript.AppendNode(new object[] { field.Name, displayName }, parentNode);
                            scriptInfos[node.ID].Add(field.Name, new ScriptInfo(node, field, functionField));
                        }
                    }
                }
            }

            if (treeListNodeScript.FocusedNode.HasChildren)
                treeListNodeScript.FocusedNode = treeListNodeScript.FocusedNode.Nodes[0];

            treeListNodeScript.ExpandAll();
            treeListNodeScript.EndUnboundLoad();
        }

        private void treeListNodeScript_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                UpdateScriptCode(e.Node);
            }
            else
            {
                richEditControlScriptCode.Text = string.Empty;
                richEditControlScriptCode.Enabled = false;
                simpleButtonSaveScriptCode.Enabled = false;
            }
        }

        private void UpdateScriptCode(TreeListNode treeNode)
        {
            string nodeName = treeNode.ParentNode[0] as string;
            SimNode node = ModelManager.Instance.SimNodesByName[nodeName];
            uint nodeID = node.ID;
            string fieldName = treeNode[0] as string;
            FieldInfo fieldInfo = scriptInfos[nodeID][fieldName].CodeInfo;
            string value = fieldInfo.GetValue(node) as string;
            richEditControlScriptCode.Enabled = true;
            MySyntaxHighlightService service = new MySyntaxHighlightService(richEditControlScriptCode);
            richEditControlScriptCode.ReplaceService<ISyntaxHighlightService>(service);
            richEditControlScriptCode.Text = value;
            simpleButtonSaveScriptCode.Enabled = true;
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeListNodeScript.FocusedNode.Level == 1)
                {
                    string nodeName = treeListNodeScript.FocusedNode.ParentNode[0] as string;
                    string fieldName = treeListNodeScript.FocusedNode[0] as string;
                    SimNode node = ModelManager.Instance.SimNodesByName[nodeName];
                    uint nodeID = node.ID;
                    FieldInfo codeInfo = scriptInfos[nodeID][fieldName].CodeInfo;
                    CSScript.Evaluator.Reset();
                    dynamic function = CSScript.Evaluator.CreateDelegate(richEditControlScriptCode.Text);
                    FieldInfo functionInfo = scriptInfos[nodeID][fieldName].FunctionInfo;
                    codeInfo.SetValue(node, richEditControlScriptCode.Text);
                    functionInfo.SetValue(node, function);
                }
            }
            catch(Exception ex)
            {
                DialogResult dialogResult = MessageBox.Show("함수에 오류가 있습니다. 이전 코드로 복구하시겠습니까?", "함수 생성 오류", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if(dialogResult != DialogResult.OK)
                {
                    string nodeName = treeListNodeScript.FocusedNode.ParentNode[0] as string;
                    string fieldName = treeListNodeScript.FocusedNode[0] as string;
                    SimNode node = ModelManager.Instance.SimNodesByName[nodeName];
                    uint nodeID = node.ID;
                    FieldInfo codeInfo = scriptInfos[nodeID][fieldName].CodeInfo;
                    FieldInfo functionInfo = scriptInfos[nodeID][fieldName].FunctionInfo;
                    codeInfo.SetValue(node, richEditControlScriptCode.Text);
                    functionInfo.SetValue(node, null);
                }
                UpdateScriptCode(treeListNodeScript.FocusedNode);
            }
        }

        /// <summary>
        ///  This class implements the Execute method of the ISyntaxHighlightService interface to parse and colorize text.
        /// </summary>
        public class MySyntaxHighlightService : ISyntaxHighlightService
        {
            readonly RichEditControl syntaxEditor;
            SyntaxColors syntaxColors;
            SyntaxHighlightProperties commentProperties;
            SyntaxHighlightProperties keywordProperties;
            SyntaxHighlightProperties stringProperties;
            SyntaxHighlightProperties xmlCommentProperties;
            SyntaxHighlightProperties textProperties;

            public MySyntaxHighlightService(RichEditControl syntaxEditor)
            {
                this.syntaxEditor = syntaxEditor;
                syntaxColors = new SyntaxColors(UserLookAndFeel.Default);
            }

            void HighlightSyntax(TokenCollection tokens)
            {
                commentProperties = new SyntaxHighlightProperties();
                commentProperties.ForeColor = syntaxColors.CommentColor;

                keywordProperties = new SyntaxHighlightProperties();
                keywordProperties.ForeColor = syntaxColors.KeywordColor;

                stringProperties = new SyntaxHighlightProperties();
                stringProperties.ForeColor = syntaxColors.StringColor;

                xmlCommentProperties = new SyntaxHighlightProperties();
                xmlCommentProperties.ForeColor = syntaxColors.XmlCommentColor;

                textProperties = new SyntaxHighlightProperties();
                textProperties.ForeColor = syntaxColors.TextColor;

                Document document = syntaxEditor.Document;
                List<SyntaxHighlightToken> syntaxTokens = new List<SyntaxHighlightToken>(tokens.Count);
                foreach (Token token in tokens)
                {
                    var categorizedToken = token as CategorizedToken;
                    if (categorizedToken != null)
                        HighlightCategorizedToken(categorizedToken, syntaxTokens);
                }
                if (syntaxTokens.Count > 0)
                {
                    document.ApplySyntaxHighlight(syntaxTokens);
                }
            }
            void HighlightCategorizedToken(CategorizedToken token, List<SyntaxHighlightToken> syntaxTokens)
            {
                Color backColor = syntaxEditor.ActiveView.BackColor;
                TokenCategory category = token.Category;
                switch (category)
                {
                    case TokenCategory.Comment:
                        syntaxTokens.Add(SetTokenColor(token, commentProperties, backColor));
                        break;
                    case TokenCategory.Keyword:
                        syntaxTokens.Add(SetTokenColor(token, keywordProperties, backColor));
                        break;
                    case TokenCategory.String:
                        syntaxTokens.Add(SetTokenColor(token, stringProperties, backColor));
                        break;
                    case TokenCategory.XmlComment:
                        syntaxTokens.Add(SetTokenColor(token, xmlCommentProperties, backColor));
                        break;
                    default:
                        syntaxTokens.Add(SetTokenColor(token, textProperties, backColor));
                        break;
                }
            }
            SyntaxHighlightToken SetTokenColor(Token token, SyntaxHighlightProperties foreColor, Color backColor)
            {
                if (syntaxEditor.Document.Paragraphs.Count < token.Range.Start.Line)
                    return null;
                int paragraphStart = syntaxEditor.Document.Paragraphs[token.Range.Start.Line - 1].Range.Start.ToInt();
                int tokenStart = paragraphStart + token.Range.Start.Offset - 1;
                if (token.Range.End.Line != token.Range.Start.Line)
                    paragraphStart = syntaxEditor.Document.Paragraphs[token.Range.End.Line - 1].Range.Start.ToInt();

                int tokenEnd = paragraphStart + token.Range.End.Offset - 1;
                Debug.Assert(tokenEnd > tokenStart);
                return new SyntaxHighlightToken(tokenStart, tokenEnd - tokenStart, foreColor);
            }

            #region #ISyntaxHighlightServiceMembers
            public void Execute()
            {
                string newText = syntaxEditor.Text;
                // Determine the language by file extension.
                string ext = System.IO.Path.GetExtension(syntaxEditor.Options.DocumentSaveOptions.CurrentFileName);
                ParserLanguageID lang_ID = ParserLanguage.FromFileExtension(ext);
                // Do not parse HTML or XML.
                if (lang_ID == ParserLanguageID.Html ||
                    lang_ID == ParserLanguageID.Xml) return;
                else if (lang_ID == ParserLanguageID.None)
                    lang_ID = ParserLanguageID.CSharp;
                // Use DevExpress.CodeParser to parse text into tokens.
                ITokenCategoryHelper tokenHelper = TokenCategoryHelperFactory.CreateHelper(lang_ID);
                if (tokenHelper != null)
                {
                    TokenCollection highlightTokens = tokenHelper.GetTokens(newText);
                    if (highlightTokens != null && highlightTokens.Count > 0)
                    {
                        HighlightSyntax(highlightTokens);
                    }

                }
            }

            public void ForceExecute()
            {
                Execute();
            }
            #endregion #ISyntaxHighlightServiceMembers
        }
        /// <summary>
        ///  This class defines colors to highlight tokens.
        /// </summary>
        public class SyntaxColors
        {
            static Color DefaultCommentColor { get { return Color.Green; } }
            static Color DefaultKeywordColor { get { return Color.Blue; } }
            static Color DefaultStringColor { get { return Color.Brown; } }
            static Color DefaultXmlCommentColor { get { return Color.Gray; } }
            static Color DefaultTextColor { get { return Color.Black; } }
            UserLookAndFeel lookAndFeel;

            public Color CommentColor { get { return GetCommonColorByName(CommonSkins.SkinInformationColor, DefaultCommentColor); } }
            public Color KeywordColor { get { return GetCommonColorByName(CommonSkins.SkinQuestionColor, DefaultKeywordColor); } }
            public Color TextColor { get { return GetCommonColorByName(CommonColors.WindowText, DefaultTextColor); } }
            public Color XmlCommentColor { get { return GetCommonColorByName(CommonColors.DisabledText, DefaultXmlCommentColor); } }
            public Color StringColor { get { return GetCommonColorByName(CommonSkins.SkinWarningColor, DefaultStringColor); } }

            public SyntaxColors(UserLookAndFeel lookAndFeel)
            {
                this.lookAndFeel = lookAndFeel;
            }

            Color GetCommonColorByName(string colorName, Color defaultColor)
            {
                Skin skin = CommonSkins.GetSkin(lookAndFeel);
                if (skin == null)
                    return defaultColor;
                return skin.Colors[colorName];
            }
        }
    }

    public struct ScriptInfo
    {
        public uint ID { get; set; }
        public SimNode Node { get; set; }
        public string CodeInfoName { get; set; }
        public FieldInfo CodeInfo { get; set; }
        public string FunctionInfoName { get; set; }
        public FieldInfo FunctionInfo { get; set; }

        public ScriptInfo(SimNode node, FieldInfo codeInfo, FieldInfo functionInfo)
        {
            ID = node.ID;
            Node = node;
            CodeInfoName = codeInfo.Name;
            CodeInfo = codeInfo;
            FunctionInfoName = functionInfo.Name;
            FunctionInfo = functionInfo;
        }
    }
}