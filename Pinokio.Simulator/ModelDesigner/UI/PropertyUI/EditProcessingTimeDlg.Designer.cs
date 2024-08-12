
namespace Pinokio.Designer
{
    partial class EditProcessingTimeDlg
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.gridControlProcessingTime = new DevExpress.XtraGrid.GridControl();
            this.gridViewProcessingTime = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sb_OK = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sb_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProcessingTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProcessingTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sb_Cancel);
            this.layoutControl1.Controls.Add(this.sb_OK);
            this.layoutControl1.Controls.Add(this.gridControlProcessingTime);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1052, 262);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1052, 262);
            this.Root.TextVisible = false;
            // 
            // gridControlProcessingTime
            // 
            this.gridControlProcessingTime.Location = new System.Drawing.Point(12, 12);
            this.gridControlProcessingTime.MainView = this.gridViewProcessingTime;
            this.gridControlProcessingTime.Name = "gridControlProcessingTime";
            this.gridControlProcessingTime.Size = new System.Drawing.Size(1028, 212);
            this.gridControlProcessingTime.TabIndex = 4;
            this.gridControlProcessingTime.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProcessingTime});
            // 
            // gridViewProcessingTime
            // 
            this.gridViewProcessingTime.GridControl = this.gridControlProcessingTime;
            this.gridViewProcessingTime.Name = "gridViewProcessingTime";
            this.gridViewProcessingTime.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewProcessingTime_CellValueChanged);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlProcessingTime;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1032, 216);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // sb_OK
            // 
            this.sb_OK.Location = new System.Drawing.Point(12, 228);
            this.sb_OK.Name = "sb_OK";
            this.sb_OK.Size = new System.Drawing.Size(512, 22);
            this.sb_OK.StyleController = this.layoutControl1;
            this.sb_OK.TabIndex = 5;
            this.sb_OK.Text = "OK";
            this.sb_OK.Click += new System.EventHandler(this.sb_OK_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sb_OK;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 216);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(516, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // sb_Cancel
            // 
            this.sb_Cancel.Location = new System.Drawing.Point(528, 228);
            this.sb_Cancel.Name = "sb_Cancel";
            this.sb_Cancel.Size = new System.Drawing.Size(512, 22);
            this.sb_Cancel.StyleController = this.layoutControl1;
            this.sb_Cancel.TabIndex = 6;
            this.sb_Cancel.Text = "Cancel";
            this.sb_Cancel.Click += new System.EventHandler(this.sb_Cancel_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sb_Cancel;
            this.layoutControlItem3.Location = new System.Drawing.Point(516, 216);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(516, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // EditProcessingTimeDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 262);
            this.Controls.Add(this.layoutControl1);
            this.Name = "EditProcessingTimeDlg";
            this.Text = "EditProcessingTimeDlg";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProcessingTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProcessingTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gridControlProcessingTime;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProcessingTime;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton sb_Cancel;
        private DevExpress.XtraEditors.SimpleButton sb_OK;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}