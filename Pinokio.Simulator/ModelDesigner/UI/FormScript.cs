using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ScintillaNET;
using CSScriptLib;

namespace Pinokio.Designer
{
    public partial class FormScript : Form
    {
        public dynamic ScriptDynamicFunction;
        public string DynamicBufferScript;

        public FormScript()
        {
            InitializeComponent();
            InitalizeScriptOptions();
        }

        public void InitalizeScriptOptions()
        {
            ScriptTextBox.Text = string.Empty;
            string[] keywordArray1 = new string[]
        {
            "abstract",
            "arguments",
            "as",
            "AS3",
            "author",
            "base",
            "bool",
            "break",
            "by",
            "byte",
            "case",
            "catch",
            "char",
            "checked",
            "class",
            "const",
            "continue",
            "copy",
            "decimal",
            "default",
            "delegate",
            "delete",
            "deprecated",
            "descending",
            "do",
            "double",
            "dynamic",
            "each",
            "else",
            "enum",
            "event",
            "eventType",
            "example",
            "exampleText",
            "exception",
            "explicit",
            "extends",
            "extern",
            "false",
            "final",
            "finally",
            "fixed",
            "float",
            "for",
            "foreach",
            "from",
            "function",
            "get",
            "goto",
            "group",
            "haxe",
            "if",
            "implements",
            "implicit",
            "import",
            "in",
            "include",
            "Infinity",
            "inheritDoc",
            "instanceof",
            "int",
            "interface",
            "internal",
            "into",
            "intrinsic",
            "is",
            "langversion",
            "link",
            "lock",
            "long",
            "mtasc",
            "mxmlc",
            "namespace",
            "NaN",
            "native",
            "new",
            "null",
            "object",
            "operator",
            "orderby",
            "out",
            "override",
            "package",
            "param",
            "params",
            "partial",
            "playerversion",
            "private",
            "productversion",
            "protected",
            "public",
            "readonly",
            "ref",
            "return",
            "sbyte",
            "sealed",
            "see",
            "select",
            "serial",
            "serialData",
            "serialField",
            "set",
            "short",
            "since",
            "sizeof",
            "stackalloc",
            "static",
            "string",
            "struct",
            "super",
            "switch",
            "this",
            "throw",
            "throws",
            "true",
            "try",
            "typeof",
            "uint",
            "ulong",
            "unchecked",
            "undefined",
            "unsafe",
            "usage",
            "use",
            "ushort",
            "using",
            "var",
            "version",
            "virtual",
            "void",
            "volatile",
            "where",
            "while",
            "with",
            "yield"
        };

            string[] keywordArray2 = new string[]
        {
            "ArgumentError",
            "Array",
            "Boolean",
            "Byte",
            "Char",
            "Class",
            "Date",
            "DateTime",
            "Decimal",
            "DefinitionError",
            "Double",
            "Error",
            "EvalError",
            "File",
            "Forms",
            "Function",
            "Int16",
            "Int32",
            "Int64",
            "IntPtr",
            "Math",
            "Namespace",
            "Null",
            "Number",
            "Object",
            "Path",
            "RangeError",
            "ReferenceError",
            "RegExp",
            "SByte",
            "ScintillaNET",
            "SecurityError",
            "Single",
            "String",
            "SyntaxError",
            "System",
            "TypeError",
            "UInt16",
            "UInt32",
            "UInt64",
            "UIntPtr",
            "Void",
            "Windows",
            "XML",
            "XMLList",
            "arguments",
            "int",
            "uint",
            "void"
        };

            int BACKGROUND_COLOR = 0x2A211C;
            int FOREGROUND_COLOR = 0xB7B7B7;
            bool CODEFOLDING_CIRCULAR = true;

            this.ScriptTextBox.Dock = DockStyle.Fill;
            this.ScriptTextBox.WrapMode = WrapMode.None;
            this.ScriptTextBox.IndentationGuides = IndentView.LookBoth;

            this.ScriptTextBox.SetSelectionBackColor(true, GetColor(0x114D9C));
            ScriptTextBox.CaretForeColor = Color.White;

            this.ScriptTextBox.StyleResetDefault();

            this.ScriptTextBox.Styles[Style.Default].BackColor = GetColor(0x212121);
            this.ScriptTextBox.Styles[Style.Default].ForeColor = GetColor(0xFFFFFF);
            this.ScriptTextBox.Styles[Style.Default].Size = 12;

            this.ScriptTextBox.StyleClearAll();

            this.ScriptTextBox.Styles[Style.Cpp.Identifier].ForeColor = GetColor(0xD0DAE2);
            this.ScriptTextBox.Styles[Style.Cpp.Comment].ForeColor = GetColor(0xBD758B);
            this.ScriptTextBox.Styles[Style.Cpp.CommentLine].ForeColor = GetColor(0x40BF57);
            this.ScriptTextBox.Styles[Style.Cpp.CommentDoc].ForeColor = GetColor(0x2FAE35);
            this.ScriptTextBox.Styles[Style.Cpp.Number].ForeColor = GetColor(0xFFFF00);
            this.ScriptTextBox.Styles[Style.Cpp.String].ForeColor = GetColor(0xFFFF00);
            this.ScriptTextBox.Styles[Style.Cpp.Character].ForeColor = GetColor(0xE95454);
            this.ScriptTextBox.Styles[Style.Cpp.Preprocessor].ForeColor = GetColor(0x8AAFEE);
            this.ScriptTextBox.Styles[Style.Cpp.Operator].ForeColor = GetColor(0xE0E0E0);
            this.ScriptTextBox.Styles[Style.Cpp.Regex].ForeColor = GetColor(0xff00ff);
            this.ScriptTextBox.Styles[Style.Cpp.CommentLineDoc].ForeColor = GetColor(0x77A7DB);
            this.ScriptTextBox.Styles[Style.Cpp.Word].ForeColor = GetColor(0x48A8EE);
            this.ScriptTextBox.Styles[Style.Cpp.Word2].ForeColor = GetColor(0xF98906);
            this.ScriptTextBox.Styles[Style.Cpp.CommentDocKeyword].ForeColor = GetColor(0xB3D991);
            this.ScriptTextBox.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = GetColor(0xFF0000);
            this.ScriptTextBox.Styles[Style.Cpp.GlobalClass].ForeColor = GetColor(0x48A8EE);

            this.ScriptTextBox.Lexer = Lexer.Cpp;

            this.ScriptTextBox.SetKeywords(0, ConnectString(keywordArray1, " "));
            this.ScriptTextBox.SetKeywords(1, ConnectString(keywordArray2, " "));

            this.ScriptTextBox.Styles[Style.LineNumber].BackColor = GetColor(BACKGROUND_COLOR);
            this.ScriptTextBox.Styles[Style.LineNumber].ForeColor = GetColor(FOREGROUND_COLOR);
            this.ScriptTextBox.Styles[Style.IndentGuide].ForeColor = GetColor(FOREGROUND_COLOR);
            this.ScriptTextBox.Styles[Style.IndentGuide].BackColor = GetColor(BACKGROUND_COLOR);

            this.ScriptTextBox.Margins[1].Width = 30;
            this.ScriptTextBox.Margins[1].Type = MarginType.Number;
            this.ScriptTextBox.Margins[1].Mask = 0;
            this.ScriptTextBox.Margins[1].Sensitive = true;

            this.ScriptTextBox.Margins[2].Width = 20;
            this.ScriptTextBox.Margins[2].Type = MarginType.Symbol;
            this.ScriptTextBox.Markers[2].Symbol = MarkerSymbol.Circle;
            this.ScriptTextBox.Margins[2].Mask = (1 << 2);
            this.ScriptTextBox.Margins[2].Sensitive = true;

            this.ScriptTextBox.Markers[2].SetBackColor(GetColor(0xFF003B));
            this.ScriptTextBox.Markers[2].SetForeColor(GetColor(0x000000));
            this.ScriptTextBox.Markers[2].SetAlpha(100);

            this.ScriptTextBox.SetFoldMarginColor(true, GetColor(BACKGROUND_COLOR));
            this.ScriptTextBox.SetFoldMarginHighlightColor(true, GetColor(BACKGROUND_COLOR));

            this.ScriptTextBox.SetProperty("fold", "1");
            this.ScriptTextBox.SetProperty("fold.compact", "1");

            this.ScriptTextBox.Margins[3].Width = 20;
            this.ScriptTextBox.Margins[3].Type = MarginType.Symbol;
            this.ScriptTextBox.Margins[3].Mask = Marker.MaskFolders;
            this.ScriptTextBox.Margins[3].Sensitive = true;

            for (int i = 25; i <= 31; i++)
            {
                this.ScriptTextBox.Markers[i].SetForeColor(GetColor(BACKGROUND_COLOR));
                this.ScriptTextBox.Markers[i].SetBackColor(GetColor(FOREGROUND_COLOR));
            }

            this.ScriptTextBox.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            this.ScriptTextBox.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            this.ScriptTextBox.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            this.ScriptTextBox.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            this.ScriptTextBox.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            this.ScriptTextBox.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            this.ScriptTextBox.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            this.ScriptTextBox.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);
        }

        private Color GetColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        private string ConnectString(string[] sourceArray, string link)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < sourceArray.Length; i++)
            {
                if (sourceArray[i].Length > 0)
                {
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Append(link);
                    }

                    stringBuilder.Append(sourceArray[i]);
                }
            }

            return stringBuilder.ToString();
        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {
            try
            {
                //CS SCript ㄱㄱ
                if (!string.IsNullOrEmpty(ScriptTextBox.Text))
                {
                    DynamicBufferScript = ScriptTextBox.Text;
                    CSScript.Evaluator.Reset();
//                    ScriptDynamicFunction = CSScript.Evaluator.CompileMethod(DynamicBufferScript).CreateObject("*");
                }

                if (ScriptDynamicFunction != null)
                {
                    //MessageBox.Show(PlayBackManager.Instance.ScriptDynamicFunction.UpdateBufferLine("Buffer"));
                }
                MessageBox.Show("Script를 정상적으로 로드하였습니다");
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ScriptFunction Error: " + ex.Message);
                MessageBox.Show("ScriptFunction Error: " + ex.Message);
                this.Close();
            }
        }

        private void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN_CLEAR_Click(object sender, EventArgs e)
        {
            ScriptTextBox.Text = string.Empty;
        }

    }
}
