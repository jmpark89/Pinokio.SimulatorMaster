using DevExpress.XtraEditors;

namespace Pinokio.Designer
{
    partial class AMHSReportFormDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AMHSReportFormDetails));
            this.gridControlCommandDetails = new DevExpress.XtraGrid.GridControl();
            this.gridViewCommandDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlTotal = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlRouteRailEdge = new DevExpress.XtraGrid.GridControl();
            this.gridViewRouteRailEdge = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnRouteRailEdgeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnRouteRailEdgeNam = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnOrder = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemCommandList = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemRouteRailEdge = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCommandDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCommandDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTotal)).BeginInit();
            this.layoutControlTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRouteRailEdge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRouteRailEdge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCommandList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRouteRailEdge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlCommandDetails
            // 
            this.gridControlCommandDetails.Location = new System.Drawing.Point(12, 39);
            this.gridControlCommandDetails.MainView = this.gridViewCommandDetails;
            this.gridControlCommandDetails.Name = "gridControlCommandDetails";
            this.gridControlCommandDetails.Size = new System.Drawing.Size(1562, 482);
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
            this.layoutControlTotal.Controls.Add(this.gridControlRouteRailEdge);
            this.layoutControlTotal.Controls.Add(this.gridControlCommandDetails);
            this.layoutControlTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlTotal.Location = new System.Drawing.Point(0, 0);
            this.layoutControlTotal.Name = "layoutControlTotal";
            this.layoutControlTotal.Root = this.layoutControlGroup1;
            this.layoutControlTotal.Size = new System.Drawing.Size(1586, 788);
            this.layoutControlTotal.TabIndex = 2;
            this.layoutControlTotal.Text = "layoutControl2";
            // 
            // gridControlRouteRailEdge
            // 
            this.gridControlRouteRailEdge.Location = new System.Drawing.Point(12, 552);
            this.gridControlRouteRailEdge.MainView = this.gridViewRouteRailEdge;
            this.gridControlRouteRailEdge.Name = "gridControlRouteRailEdge";
            this.gridControlRouteRailEdge.Size = new System.Drawing.Size(1562, 224);
            this.gridControlRouteRailEdge.TabIndex = 8;
            this.gridControlRouteRailEdge.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRouteRailEdge});
            // 
            // gridViewRouteRailEdge
            // 
            this.gridViewRouteRailEdge.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnRouteRailEdgeName,
            this.gridColumnRouteRailEdgeNam,
            this.gridColumnOrder});
            this.gridViewRouteRailEdge.GridControl = this.gridControlRouteRailEdge;
            this.gridViewRouteRailEdge.Name = "gridViewRouteRailEdge";
            this.gridViewRouteRailEdge.OptionsBehavior.Editable = false;
            this.gridViewRouteRailEdge.OptionsBehavior.ReadOnly = true;
            this.gridViewRouteRailEdge.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewRouteRailEdge_CustomDrawCell);
            this.gridViewRouteRailEdge.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewRouteRailEdge_FocusedRowChanged);
            this.gridViewRouteRailEdge.Click += new System.EventHandler(this.gridViewRouteRailEdge_Click);
            // 
            // gridColumnRouteRailEdgeName
            // 
            this.gridColumnRouteRailEdgeName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumnRouteRailEdgeName.AppearanceHeader.Options.UseFont = true;
            this.gridColumnRouteRailEdgeName.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnRouteRailEdgeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnRouteRailEdgeName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnRouteRailEdgeName.Caption = "Edge Rail Name";
            this.gridColumnRouteRailEdgeName.FieldName = "Name";
            this.gridColumnRouteRailEdgeName.Name = "gridColumnRouteRailEdgeName";
            this.gridColumnRouteRailEdgeName.Visible = true;
            this.gridColumnRouteRailEdgeName.VisibleIndex = 0;
            // 
            // gridColumnRouteRailEdgeNam
            // 
            this.gridColumnRouteRailEdgeNam.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnRouteRailEdgeNam.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnRouteRailEdgeNam.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnRouteRailEdgeNam.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumnRouteRailEdgeNam.AppearanceHeader.Options.UseFont = true;
            this.gridColumnRouteRailEdgeNam.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnRouteRailEdgeNam.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnRouteRailEdgeNam.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnRouteRailEdgeNam.Caption = "Type";
            this.gridColumnRouteRailEdgeNam.FieldName = "Type";
            this.gridColumnRouteRailEdgeNam.Name = "gridColumnRouteRailEdgeNam";
            this.gridColumnRouteRailEdgeNam.Visible = true;
            this.gridColumnRouteRailEdgeNam.VisibleIndex = 1;
            // 
            // gridColumnOrder
            // 
            this.gridColumnOrder.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnOrder.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnOrder.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnOrder.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumnOrder.AppearanceHeader.Options.UseFont = true;
            this.gridColumnOrder.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnOrder.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnOrder.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnOrder.Caption = "Order";
            this.gridColumnOrder.FieldName = "Order";
            this.gridColumnOrder.Name = "gridColumnOrder";
            this.gridColumnOrder.Visible = true;
            this.gridColumnOrder.VisibleIndex = 2;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemCommandList,
            this.layoutControlItemRouteRailEdge,
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
            this.layoutControlItemCommandList.Size = new System.Drawing.Size(1566, 513);
            this.layoutControlItemCommandList.Text = "AMHS List";
            this.layoutControlItemCommandList.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItemCommandList.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemCommandList.TextSize = new System.Drawing.Size(76, 20);
            this.layoutControlItemCommandList.TextToControlDistance = 7;
            // 
            // layoutControlItemRouteRailEdge
            // 
            this.layoutControlItemRouteRailEdge.Control = this.gridControlRouteRailEdge;
            this.layoutControlItemRouteRailEdge.Location = new System.Drawing.Point(0, 523);
            this.layoutControlItemRouteRailEdge.Name = "layoutControlItemRouteRailEdge";
            this.layoutControlItemRouteRailEdge.Size = new System.Drawing.Size(1566, 245);
            this.layoutControlItemRouteRailEdge.Text = "Route";
            this.layoutControlItemRouteRailEdge.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemRouteRailEdge.TextSize = new System.Drawing.Size(33, 14);
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 513);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1566, 10);
            // 
            // AMHSReportFormDetails
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1586, 788);
            this.Controls.Add(this.layoutControlTotal);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("AMHSReportFormDetails.IconOptions.LargeImage")));
            this.Name = "AMHSReportFormDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AMHS Details";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AMHSReportFormDetails_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCommandDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCommandDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTotal)).EndInit();
            this.layoutControlTotal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRouteRailEdge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRouteRailEdge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCommandList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRouteRailEdge)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gridControlRouteRailEdge;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRouteRailEdge;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRouteRailEdge;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnRouteRailEdgeName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnRouteRailEdgeNam;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOrder;
    }
}