
namespace Pinokio.Designer.UI.AssetEdit.AssemblyEdit
{
    partial class FormAssetStore
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAssetStore));
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlOnlineSimClass = new DevExpress.XtraGrid.GridControl();
            this.gridViewOnlineSimClass = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSimDownload = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControlOnlineRefClass = new DevExpress.XtraGrid.GridControl();
            this.gridViewOnlineRefClass = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnThumnail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnLatestUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDownload = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControlOnline3DObj = new DevExpress.XtraGrid.GridControl();
            this.gridViewOnline3DObj = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnObjName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnObjThumnail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnObjDownload = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.toolbarFormControl1 = new DevExpress.XtraBars.ToolbarForm.ToolbarFormControl();
            this.toolbarFormManager1 = new DevExpress.XtraBars.ToolbarForm.ToolbarFormManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOnlineSimClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOnlineSimClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOnlineRefClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOnlineRefClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOnline3DObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOnline3DObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 615);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Size = new System.Drawing.Size(1453, 20);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.simpleButtonRefresh);
            this.layoutControl1.Controls.Add(this.gridControlOnlineSimClass);
            this.layoutControl1.Controls.Add(this.gridControlOnlineRefClass);
            this.layoutControl1.Controls.Add(this.gridControlOnline3DObj);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1453, 584);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // simpleButtonRefresh
            // 
            this.simpleButtonRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonRefresh.ImageOptions.Image")));
            this.simpleButtonRefresh.Location = new System.Drawing.Point(12, 12);
            this.simpleButtonRefresh.Name = "simpleButtonRefresh";
            this.simpleButtonRefresh.Size = new System.Drawing.Size(137, 36);
            this.simpleButtonRefresh.StyleController = this.layoutControl1;
            this.simpleButtonRefresh.TabIndex = 8;
            this.simpleButtonRefresh.Text = "Refresh";
            this.simpleButtonRefresh.Click += new System.EventHandler(this.simpleButtonRefresh_Click);
            // 
            // gridControlOnlineSimClass
            // 
            this.gridControlOnlineSimClass.Location = new System.Drawing.Point(978, 102);
            this.gridControlOnlineSimClass.MainView = this.gridViewOnlineSimClass;
            this.gridControlOnlineSimClass.Name = "gridControlOnlineSimClass";
            this.gridControlOnlineSimClass.Size = new System.Drawing.Size(451, 458);
            this.gridControlOnlineSimClass.TabIndex = 7;
            this.gridControlOnlineSimClass.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOnlineSimClass});
            // 
            // gridViewOnlineSimClass
            // 
            this.gridViewOnlineSimClass.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName,
            this.gridColumnSimDownload});
            this.gridViewOnlineSimClass.GridControl = this.gridControlOnlineSimClass;
            this.gridViewOnlineSimClass.Name = "gridViewOnlineSimClass";
            this.gridViewOnlineSimClass.OptionsBehavior.Editable = false;
            this.gridViewOnlineSimClass.RowHeight = 32;
            this.gridViewOnlineSimClass.DoubleClick += new System.EventHandler(this.gridViewOnlineSimClass_DoubleClick);
            // 
            // gridColumnName
            // 
            this.gridColumnName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumnName.AppearanceHeader.Options.UseFont = true;
            this.gridColumnName.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnName.Caption = "Name";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 0;
            // 
            // gridColumnSimDownload
            // 
            this.gridColumnSimDownload.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumnSimDownload.AppearanceHeader.Options.UseFont = true;
            this.gridColumnSimDownload.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnSimDownload.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnSimDownload.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnSimDownload.Caption = "Download";
            this.gridColumnSimDownload.FieldName = "Download";
            this.gridColumnSimDownload.Name = "gridColumnSimDownload";
            this.gridColumnSimDownload.Visible = true;
            this.gridColumnSimDownload.VisibleIndex = 1;
            // 
            // gridControlOnlineRefClass
            // 
            this.gridControlOnlineRefClass.Location = new System.Drawing.Point(462, 102);
            this.gridControlOnlineRefClass.MainView = this.gridViewOnlineRefClass;
            this.gridControlOnlineRefClass.Name = "gridControlOnlineRefClass";
            this.gridControlOnlineRefClass.Size = new System.Drawing.Size(512, 458);
            this.gridControlOnlineRefClass.TabIndex = 6;
            this.gridControlOnlineRefClass.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOnlineRefClass});
            // 
            // gridViewOnlineRefClass
            // 
            this.gridViewOnlineRefClass.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName,
            this.gridColumnThumnail,
            this.gridColumnDescription,
            this.gridColumnLatestUserID,
            this.gridColumnDownload});
            this.gridViewOnlineRefClass.GridControl = this.gridControlOnlineRefClass;
            this.gridViewOnlineRefClass.Name = "gridViewOnlineRefClass";
            this.gridViewOnlineRefClass.OptionsBehavior.Editable = false;
            this.gridViewOnlineRefClass.RowHeight = 32;
            this.gridViewOnlineRefClass.DoubleClick += new System.EventHandler(this.gridViewOnlineRefClass_DoubleClick);
            // 
            // gridColumnThumnail
            // 
            this.gridColumnThumnail.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumnThumnail.AppearanceHeader.Options.UseFont = true;
            this.gridColumnThumnail.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnThumnail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnThumnail.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnThumnail.Caption = "Thumnail";
            this.gridColumnThumnail.FieldName = "Thumnail";
            this.gridColumnThumnail.Name = "gridColumnThumnail";
            this.gridColumnThumnail.Visible = true;
            this.gridColumnThumnail.VisibleIndex = 1;
            // 
            // gridColumnDescription
            // 
            this.gridColumnDescription.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumnDescription.AppearanceHeader.Options.UseFont = true;
            this.gridColumnDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnDescription.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnDescription.Caption = "Description";
            this.gridColumnDescription.FieldName = "Description";
            this.gridColumnDescription.Name = "gridColumnDescription";
            this.gridColumnDescription.Visible = true;
            this.gridColumnDescription.VisibleIndex = 2;
            // 
            // gridColumnLatestUserID
            // 
            this.gridColumnLatestUserID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumnLatestUserID.AppearanceHeader.Options.UseFont = true;
            this.gridColumnLatestUserID.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnLatestUserID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnLatestUserID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnLatestUserID.Caption = "Latest User ID";
            this.gridColumnLatestUserID.FieldName = "UserID";
            this.gridColumnLatestUserID.Name = "gridColumnLatestUserID";
            this.gridColumnLatestUserID.Visible = true;
            this.gridColumnLatestUserID.VisibleIndex = 3;
            // 
            // gridColumnDownload
            // 
            this.gridColumnDownload.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumnDownload.AppearanceHeader.Options.UseFont = true;
            this.gridColumnDownload.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnDownload.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnDownload.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnDownload.Caption = "Download";
            this.gridColumnDownload.FieldName = "Download";
            this.gridColumnDownload.Name = "gridColumnDownload";
            this.gridColumnDownload.Visible = true;
            this.gridColumnDownload.VisibleIndex = 4;
            // 
            // gridControlOnline3DObj
            // 
            this.gridControlOnline3DObj.Location = new System.Drawing.Point(24, 102);
            this.gridControlOnline3DObj.MainView = this.gridViewOnline3DObj;
            this.gridControlOnline3DObj.Name = "gridControlOnline3DObj";
            this.gridControlOnline3DObj.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControlOnline3DObj.Size = new System.Drawing.Size(434, 458);
            this.gridControlOnline3DObj.TabIndex = 5;
            this.gridControlOnline3DObj.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOnline3DObj});
            // 
            // gridViewOnline3DObj
            // 
            this.gridViewOnline3DObj.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnObjName,
            this.gridColumnObjThumnail,
            this.gridColumnObjDownload});
            this.gridViewOnline3DObj.GridControl = this.gridControlOnline3DObj;
            this.gridViewOnline3DObj.Name = "gridViewOnline3DObj";
            this.gridViewOnline3DObj.OptionsBehavior.Editable = false;
            this.gridViewOnline3DObj.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridViewOnline3DObj.RowHeight = 32;
            this.gridViewOnline3DObj.DoubleClick += new System.EventHandler(this.gridViewOnline3DObj_DoubleClick);
            // 
            // gridColumnObjName
            // 
            this.gridColumnObjName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumnObjName.AppearanceHeader.Options.UseFont = true;
            this.gridColumnObjName.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnObjName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnObjName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnObjName.Caption = "Name";
            this.gridColumnObjName.FieldName = "Name";
            this.gridColumnObjName.Name = "gridColumnObjName";
            this.gridColumnObjName.Visible = true;
            this.gridColumnObjName.VisibleIndex = 0;
            // 
            // gridColumnObjThumnail
            // 
            this.gridColumnObjThumnail.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumnObjThumnail.AppearanceHeader.Options.UseFont = true;
            this.gridColumnObjThumnail.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnObjThumnail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnObjThumnail.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnObjThumnail.Caption = "Thumnail";
            this.gridColumnObjThumnail.FieldName = "Thumnail";
            this.gridColumnObjThumnail.Name = "gridColumnObjThumnail";
            this.gridColumnObjThumnail.Visible = true;
            this.gridColumnObjThumnail.VisibleIndex = 1;
            // 
            // gridColumnObjDownload
            // 
            this.gridColumnObjDownload.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumnObjDownload.AppearanceHeader.Options.UseFont = true;
            this.gridColumnObjDownload.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnObjDownload.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnObjDownload.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnObjDownload.Caption = "Download";
            this.gridColumnObjDownload.FieldName = "Download";
            this.gridColumnObjDownload.Name = "gridColumnObjDownload";
            this.gridColumnObjDownload.Visible = true;
            this.gridColumnObjDownload.VisibleIndex = 2;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1453, 584);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 40);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1433, 524);
            this.layoutControlGroup1.Text = "Online";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControlOnline3DObj;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(438, 479);
            this.layoutControlItem2.Text = "3D OBJ File";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(85, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridControlOnlineRefClass;
            this.layoutControlItem3.Location = new System.Drawing.Point(438, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(516, 479);
            this.layoutControlItem3.Text = "Reference Class";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(85, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridControlOnlineSimClass;
            this.layoutControlItem4.Location = new System.Drawing.Point(954, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(455, 479);
            this.layoutControlItem4.Text = "Simulation Class";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(85, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButtonRefresh;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(141, 40);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(141, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1292, 40);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "next_32x32.png");
            this.imageCollection1.InsertGalleryImage("task_32x32.png", "images/tasks/task_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/tasks/task_32x32.png"), 1);
            this.imageCollection1.Images.SetKeyName(1, "task_32x32.png");
            // 
            // toolbarFormControl1
            // 
            this.toolbarFormControl1.Location = new System.Drawing.Point(0, 0);
            this.toolbarFormControl1.Manager = this.toolbarFormManager1;
            this.toolbarFormControl1.Name = "toolbarFormControl1";
            this.toolbarFormControl1.Size = new System.Drawing.Size(1453, 31);
            this.toolbarFormControl1.TabIndex = 5;
            this.toolbarFormControl1.TabStop = false;
            this.toolbarFormControl1.ToolbarForm = this;
            // 
            // toolbarFormManager1
            // 
            this.toolbarFormManager1.DockControls.Add(this.barDockControlTop);
            this.toolbarFormManager1.DockControls.Add(this.barDockControlBottom);
            this.toolbarFormManager1.DockControls.Add(this.barDockControlLeft);
            this.toolbarFormManager1.DockControls.Add(this.barDockControlRight);
            this.toolbarFormManager1.Form = this;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 31);
            this.barDockControlTop.Manager = this.toolbarFormManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1453, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 635);
            this.barDockControlBottom.Manager = this.toolbarFormManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1453, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.toolbarFormManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 604);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1453, 31);
            this.barDockControlRight.Manager = this.toolbarFormManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 604);
            // 
            // FormAssetStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1453, 635);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.toolbarFormControl1);
            this.IconOptions.Image = global::Pinokio.Designer.Properties.Resources.LOGO_ICON;
            this.Name = "FormAssetStore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asset Items";
            this.ToolbarFormControl = this.toolbarFormControl1;
            this.Load += new System.EventHandler(this.FormAssetStore_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOnlineSimClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOnlineSimClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOnlineRefClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOnlineRefClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOnline3DObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOnline3DObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gridControlOnline3DObj;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOnline3DObj;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.GridControl gridControlOnlineSimClass;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOnlineSimClass;
        private DevExpress.XtraGrid.GridControl gridControlOnlineRefClass;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOnlineRefClass;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDownload;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.ToolbarForm.ToolbarFormControl toolbarFormControl1;
        private DevExpress.XtraBars.ToolbarForm.ToolbarFormManager toolbarFormManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRefresh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnThumnail;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLatestUserID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDescription;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnObjName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnObjThumnail;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnObjDownload;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSimDownload;
    }
}