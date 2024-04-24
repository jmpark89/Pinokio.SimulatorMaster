
namespace Pinokio.Designer
{
    partial class EditScriptCodeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditScriptCodeForm));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeListNodeScript = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumnFullName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.richEditControlScriptCode = new DevExpress.XtraRichEdit.RichEditControl();
            this.simpleButtonSaveScriptCode = new DevExpress.XtraEditors.SimpleButton();
            this.treeListColumnDisplayName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListNodeScript)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeListNodeScript);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(894, 577);
            this.splitContainerControl1.SplitterPosition = 185;
            this.splitContainerControl1.TabIndex = 1;
            // 
            // treeListNodeScript
            // 
            this.treeListNodeScript.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnFullName,
            this.treeListColumnDisplayName});
            this.treeListNodeScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListNodeScript.Location = new System.Drawing.Point(0, 0);
            this.treeListNodeScript.Name = "treeListNodeScript";
            this.treeListNodeScript.OptionsBehavior.Editable = false;
            this.treeListNodeScript.Size = new System.Drawing.Size(185, 577);
            this.treeListNodeScript.TabIndex = 0;
            this.treeListNodeScript.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListNodeScript_FocusedNodeChanged);
            // 
            // treeListColumnFullName
            // 
            this.treeListColumnFullName.Caption = "Nodes";
            this.treeListColumnFullName.FieldName = "treeListColumnFullName";
            this.treeListColumnFullName.Name = "treeListColumnFullName";
            this.treeListColumnFullName.OptionsColumn.ReadOnly = true;
            this.treeListColumnFullName.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.richEditControlScriptCode);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.simpleButtonSaveScriptCode);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(699, 577);
            this.splitContainerControl2.SplitterPosition = 55;
            this.splitContainerControl2.TabIndex = 0;
            // 
            // richEditControlScriptCode
            // 
            this.richEditControlScriptCode.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.richEditControlScriptCode.Appearance.Text.Options.UseBackColor = true;
            this.richEditControlScriptCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditControlScriptCode.LayoutUnit = DevExpress.XtraRichEdit.DocumentLayoutUnit.Pixel;
            this.richEditControlScriptCode.Location = new System.Drawing.Point(0, 0);
            this.richEditControlScriptCode.Name = "richEditControlScriptCode";
            this.richEditControlScriptCode.Size = new System.Drawing.Size(699, 512);
            this.richEditControlScriptCode.TabIndex = 0;
            // 
            // simpleButtonSaveScriptCode
            // 
            this.simpleButtonSaveScriptCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleButtonSaveScriptCode.Location = new System.Drawing.Point(0, 0);
            this.simpleButtonSaveScriptCode.Name = "simpleButtonSaveScriptCode";
            this.simpleButtonSaveScriptCode.Size = new System.Drawing.Size(699, 55);
            this.simpleButtonSaveScriptCode.TabIndex = 2;
            this.simpleButtonSaveScriptCode.Text = "Save";
            this.simpleButtonSaveScriptCode.Click += new System.EventHandler(this.simpleButtonSave_Click);
            // 
            // treeListColumnDisplayName
            // 
            this.treeListColumnDisplayName.Caption = "Nodes";
            this.treeListColumnDisplayName.FieldName = "Nodes";
            this.treeListColumnDisplayName.Name = "treeListColumnDisplayName";
            this.treeListColumnDisplayName.Visible = true;
            this.treeListColumnDisplayName.VisibleIndex = 0;
            // 
            // EditScriptCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 577);
            this.Controls.Add(this.splitContainerControl1);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("EditScriptCodeForm.IconOptions.LargeImage")));
            this.Name = "EditScriptCodeForm";
            this.Text = "Edit Script";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListNodeScript)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList treeListNodeScript;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnFullName;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSaveScriptCode;
        private DevExpress.XtraRichEdit.RichEditControl richEditControlScriptCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnDisplayName;
    }
}