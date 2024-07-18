
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace Pinokio.Designer
{
    partial class IntegrityTestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntegrityTestForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.gridControlSimNode = new DevExpress.XtraGrid.GridControl();
            this.gridViewSimNode = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.CancelBT = new DevExpress.XtraEditors.SimpleButton();
            this.IgnoreSaveBT = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.gridControlFactory = new DevExpress.XtraGrid.GridControl();
            this.gridViewFactory = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSimNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSimNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFactory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFactory)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.CancelBT);
            this.splitContainer1.Panel2.Controls.Add(this.IgnoreSaveBT);
            this.splitContainer1.Size = new System.Drawing.Size(879, 466);
            this.splitContainer1.SplitterDistance = 775;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(775, 466);
            this.splitContainer2.SplitterDistance = 28;
            this.splitContainer2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "SimNode Errors";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.gridControlSimNode);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(775, 434);
            this.splitContainer3.SplitterDistance = 214;
            this.splitContainer3.TabIndex = 0;
            // 
            // gridControlSimNode
            // 
            this.gridControlSimNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSimNode.EmbeddedNavigator.CausesValidation = false;
            this.gridControlSimNode.EmbeddedNavigator.ShowToolTips = false;
            this.gridControlSimNode.Location = new System.Drawing.Point(0, 0);
            this.gridControlSimNode.MainView = this.gridViewSimNode;
            this.gridControlSimNode.Name = "gridControlSimNode";
            this.gridControlSimNode.Size = new System.Drawing.Size(775, 214);
            this.gridControlSimNode.TabIndex = 1;
            this.gridControlSimNode.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSimNode});
            // 
            // gridViewSimNode
            // 
            this.gridViewSimNode.GridControl = this.gridControlSimNode;
            this.gridViewSimNode.Name = "gridViewSimNode";
            this.gridViewSimNode.OptionsBehavior.Editable = false;
            this.gridViewSimNode.OptionsBehavior.SmartVertScrollBar = false;
            this.gridViewSimNode.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewSimNode.OptionsCustomization.AllowFilter = false;
            this.gridViewSimNode.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewSimNode.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridViewSimNode.OptionsFilter.AllowFilterEditor = false;
            this.gridViewSimNode.OptionsFind.AllowFindPanel = false;
            this.gridViewSimNode.OptionsFind.AllowMruItems = false;
            this.gridViewSimNode.OptionsView.ColumnAutoWidth = false;
            // 
            // CancelBT
            // 
            this.CancelBT.Location = new System.Drawing.Point(13, 41);
            this.CancelBT.Name = "CancelBT";
            this.CancelBT.Size = new System.Drawing.Size(75, 23);
            this.CancelBT.TabIndex = 1;
            this.CancelBT.Text = "Cancel";
            this.CancelBT.Click += new System.EventHandler(this.CancelBT_Click);
            // 
            // IgnoreSaveBT
            // 
            this.IgnoreSaveBT.Location = new System.Drawing.Point(13, 12);
            this.IgnoreSaveBT.Name = "IgnoreSaveBT";
            this.IgnoreSaveBT.Size = new System.Drawing.Size(75, 23);
            this.IgnoreSaveBT.TabIndex = 0;
            this.IgnoreSaveBT.Text = "Ignore";
            this.IgnoreSaveBT.Click += new System.EventHandler(this.IgnoreSaveBT_Click);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.gridControlFactory);
            this.splitContainer4.Size = new System.Drawing.Size(775, 216);
            this.splitContainer4.SplitterDistance = 25;
            this.splitContainer4.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "Factory Errors";
            // 
            // gridControlFactory
            // 
            this.gridControlFactory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlFactory.EmbeddedNavigator.CausesValidation = false;
            this.gridControlFactory.EmbeddedNavigator.ShowToolTips = false;
            this.gridControlFactory.Location = new System.Drawing.Point(0, 0);
            this.gridControlFactory.MainView = this.gridViewFactory;
            this.gridControlFactory.Name = "gridControlFactory";
            this.gridControlFactory.Size = new System.Drawing.Size(775, 187);
            this.gridControlFactory.TabIndex = 2;
            this.gridControlFactory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFactory});
            // 
            // gridViewFactory
            // 
            this.gridViewFactory.GridControl = this.gridControlFactory;
            this.gridViewFactory.Name = "gridViewFactory";
            this.gridViewFactory.OptionsBehavior.Editable = false;
            this.gridViewFactory.OptionsBehavior.SmartVertScrollBar = false;
            this.gridViewFactory.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewFactory.OptionsCustomization.AllowFilter = false;
            this.gridViewFactory.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewFactory.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridViewFactory.OptionsFilter.AllowFilterEditor = false;
            this.gridViewFactory.OptionsFind.AllowFindPanel = false;
            this.gridViewFactory.OptionsFind.AllowMruItems = false;
            this.gridViewFactory.OptionsView.ColumnAutoWidth = false;
            // 
            // IntegrityTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 466);
            this.Controls.Add(this.splitContainer1);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("IntegrityTestForm.IconOptions.LargeImage")));
            this.Name = "IntegrityTestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Integrity Test";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSimNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSimNode)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFactory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFactory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton CancelBT;
        private DevExpress.XtraEditors.SimpleButton IgnoreSaveBT;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private GridControl gridControlSimNode;
        private GridView gridViewSimNode;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label label2;
        private GridControl gridControlFactory;
        private GridView gridViewFactory;
    }
}