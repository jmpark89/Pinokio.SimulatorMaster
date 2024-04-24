using DevExpress.XtraEditors;

namespace Pinokio.Designer
{
    partial class ProductionReportFormDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductionReportFormDetails));
            this.gridControlCommandDetails = new DevExpress.XtraGrid.GridControl();
            this.gridViewCommandDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlTotal = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemCommandList = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCommandDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCommandDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTotal)).BeginInit();
            this.layoutControlTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCommandList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlCommandDetails
            // 
            this.gridControlCommandDetails.Location = new System.Drawing.Point(12, 39);
            this.gridControlCommandDetails.MainView = this.gridViewCommandDetails;
            this.gridControlCommandDetails.Name = "gridControlCommandDetails";
            this.gridControlCommandDetails.Size = new System.Drawing.Size(1562, 727);
            this.gridControlCommandDetails.TabIndex = 4;
            this.gridControlCommandDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCommandDetails});
            // 
            // gridViewCommandDetails
            // 
            this.gridViewCommandDetails.GridControl = this.gridControlCommandDetails;
            this.gridViewCommandDetails.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewCommandDetails.Name = "gridViewCommandDetails";
            this.gridViewCommandDetails.OptionsBehavior.Editable = false;
            this.gridViewCommandDetails.OptionsBehavior.ReadOnly = true;
            this.gridViewCommandDetails.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewCommandDetails.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gridViewCommandDetails.OptionsMenu.ShowSummaryItemMode = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewCommandDetails.OptionsView.ColumnAutoWidth = false;
            this.gridViewCommandDetails.OptionsView.ShowDetailButtons = false;
            this.gridViewCommandDetails.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewCommandDetails_FocusedRowChanged);
            this.gridViewCommandDetails.Click += new System.EventHandler(this.gridViewCommandDetails_Click);
            // 
            // layoutControlTotal
            // 
            this.layoutControlTotal.Controls.Add(this.gridControlCommandDetails);
            this.layoutControlTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlTotal.Location = new System.Drawing.Point(0, 0);
            this.layoutControlTotal.Name = "layoutControlTotal";
            this.layoutControlTotal.Root = this.layoutControlGroup1;
            this.layoutControlTotal.Size = new System.Drawing.Size(1586, 788);
            this.layoutControlTotal.TabIndex = 2;
            this.layoutControlTotal.Text = "layoutControl2";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemCommandList,
            this.splitterItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1586, 788);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemCommandList
            // 
            this.layoutControlItemCommandList.Control = this.gridControlCommandDetails;
            this.layoutControlItemCommandList.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemCommandList.Name = "layoutControlItemCommandList";
            this.layoutControlItemCommandList.Size = new System.Drawing.Size(1566, 758);
            this.layoutControlItemCommandList.Text = "Production List";
            this.layoutControlItemCommandList.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItemCommandList.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemCommandList.TextSize = new System.Drawing.Size(76, 20);
            this.layoutControlItemCommandList.TextToControlDistance = 7;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 758);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1566, 10);
            // 
            // ProductionReportFormDetails
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1586, 788);
            this.Controls.Add(this.layoutControlTotal);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("ProductionReportFormDetails.IconOptions.LargeImage")));
            this.Name = "ProductionReportFormDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Production Details";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProductionReportFormDetails_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCommandDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCommandDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTotal)).EndInit();
            this.layoutControlTotal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCommandList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gridControlCommandDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCommandDetails;
        private DevExpress.XtraLayout.LayoutControl layoutControlTotal;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCommandList;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
    }
}