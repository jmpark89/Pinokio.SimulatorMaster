


namespace Pinokio.Designer{ 
    partial class FormAssemblyEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAssemblyEdit));
            devDept.Eyeshot.CancelToolBarButton cancelToolBarButton2 = new devDept.Eyeshot.CancelToolBarButton("Cancel", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ProgressBar progressBar2 = new devDept.Eyeshot.ProgressBar(devDept.Eyeshot.ProgressBar.styleType.Circular, 0, "Idle", System.Drawing.Color.Black, System.Drawing.Color.Transparent, System.Drawing.Color.Green, 1D, true, cancelToolBarButton2, false, 0.1D, true);
            devDept.Eyeshot.ShortcutKeysSettings shortcutKeysSettings2 = new devDept.Eyeshot.ShortcutKeysSettings(((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A))), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I))), System.Windows.Forms.Keys.None, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F))), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Add))), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Subtract))), System.Windows.Forms.Keys.None, System.Windows.Forms.Keys.None, System.Windows.Forms.Keys.None, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G))), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.G))), System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.Down, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right))), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up))), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left))), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down))), System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.D, System.Windows.Forms.Keys.A, System.Windows.Forms.Keys.E, System.Windows.Forms.Keys.Q, System.Windows.Forms.Keys.W, System.Windows.Forms.Keys.S);
            devDept.Graphics.BackgroundSettings backgroundSettings2 = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.LinearGradient, System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231))))), System.Drawing.Color.DodgerBlue, System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231))))), 0.75D, null, devDept.Graphics.colorThemeType.Auto, 0.33D);
            devDept.Eyeshot.Camera camera2 = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(0D, 0D, 45D), 380D, new devDept.Geometry.Quaternion(0.018434349666532526D, 0.039532590434972079D, 0.42221602280006187D, 0.90544518284475428D), devDept.Graphics.projectionType.Perspective, 40D, 3.9494941278546096D, false, 0.001D);
            devDept.Eyeshot.HomeToolBarButton homeToolBarButton2 = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton2 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton2 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton2 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton2 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.RotateToolBarButton rotateToolBarButton2 = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton2 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.ToolBar toolBar2 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopCenter, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(homeToolBarButton2)),
            ((devDept.Eyeshot.ToolBarButton)(magnifyingGlassToolBarButton2)),
            ((devDept.Eyeshot.ToolBarButton)(zoomWindowToolBarButton2)),
            ((devDept.Eyeshot.ToolBarButton)(zoomToolBarButton2)),
            ((devDept.Eyeshot.ToolBarButton)(panToolBarButton2)),
            ((devDept.Eyeshot.ToolBarButton)(rotateToolBarButton2)),
            ((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton2))});
            devDept.Eyeshot.Grid grid2 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point3D(-100D, -100D, 0D), new devDept.Geometry.Point3D(100D, 100D, 0D), 10D, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Vector3D(0D, 0D, 1D)), System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), false, true, false, false, 10, 100, 10, System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90))))), System.Drawing.Color.Transparent, false, System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255))))));
            devDept.Eyeshot.RotateSettings rotateSettings2 = new devDept.Eyeshot.RotateSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.None), 10D, true, 1D, devDept.Eyeshot.rotationType.Trackball, devDept.Eyeshot.rotationCenterType.CursorLocation, new devDept.Geometry.Point3D(0D, 0D, 0D), false);
            devDept.Eyeshot.ZoomSettings zoomSettings2 = new devDept.Eyeshot.ZoomSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Shift), 25, true, devDept.Eyeshot.zoomStyleType.AtCursorLocation, false, 1D, System.Drawing.Color.Empty, devDept.Eyeshot.Camera.perspectiveFitType.Accurate, false, 10, true);
            devDept.Eyeshot.PanSettings panSettings2 = new devDept.Eyeshot.PanSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Ctrl), 25, true);
            devDept.Eyeshot.NavigationSettings navigationSettings2 = new devDept.Eyeshot.NavigationSettings(devDept.Eyeshot.Camera.navigationType.Examine, new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Left, devDept.Eyeshot.modifierKeys.None), new devDept.Geometry.Point3D(-1000D, -1000D, -1000D), new devDept.Geometry.Point3D(1000D, 1000D, 1000D), 8D, 50D, 50D);
            devDept.Eyeshot.Viewport.SavedViewsManager savedViewsManager2 = new devDept.Eyeshot.Viewport.SavedViewsManager(8);
            devDept.Eyeshot.Viewport viewport2 = new devDept.Eyeshot.Viewport(new System.Drawing.Point(0, 0), new System.Drawing.Size(916, 782), backgroundSettings2, camera2, new devDept.Eyeshot.ToolBar[] {
            toolBar2}, devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[] {
            grid2}, false, rotateSettings2, zoomSettings2, panSettings2, navigationSettings2, savedViewsManager2, devDept.Eyeshot.viewType.Trimetric);
            devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon2 = new devDept.Eyeshot.CoordinateSystemIcon(System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.OrangeRed, "Origin", "X", "Y", "Z", true, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, false);
            devDept.Eyeshot.OriginSymbol originSymbol2 = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", true, null, false);
            devDept.Eyeshot.ViewCubeIcon viewCubeIcon2 = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, true, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(147))))), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), 'S', 'N', 'W', 'E', true, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItemImport = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemExport = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckItemGeometryInAxis = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonItemObjectManipulator = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSetSelectionAsCurrent = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItemManipulatorType = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBoxManipulatorType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barEditItemManipulatorActionType = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemCheckedComboBoxEditManipulatorActionType = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.barEditItemManipulatorAction = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBoxManipulatorAction = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItemCreateNode = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDownLoadAsset = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAddModel = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemUpload3DSource = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemImportReferenceFile = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAddReferenceItem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemUploadSim = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckItem2 = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckItem3 = new DevExpress.XtraBars.BarCheckItem();
            this.barToggleSwitchItem1 = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.barEditItemUploadedObj = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemCheckEditUploadedObj = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.barEditItemUploadedRefClass = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemCheckEditUploadedRefClass = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.barEditItemUploadedSimClass = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemCheckEditUploadedSimClass = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.ribbonPageMain = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupMain = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupEdit = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupSelect = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.assemblyModel1 = new Pinokio.Designer.AssemblyModel();
            this.assemblyTreeView1 = new Pinokio.Designer.AssemblyTreeView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.xtraOpenFileDialog1 = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.xtraSaveFileDialog1 = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.propertyGridControlReferenceType = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxManipulatorType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEditManipulatorActionType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxManipulatorAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditUploadedObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditUploadedRefClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditUploadedSimClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.assemblyModel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlReferenceType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.propertyGridControlReferenceType);
            this.layoutControl1.Controls.Add(this.propertyGridControl1);
            this.layoutControl1.Controls.Add(this.assemblyModel1);
            this.layoutControl1.Controls.Add(this.assemblyTreeView1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 150);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1581, 806);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.DarkBlue;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.barButtonItemImport,
            this.barButtonItemExport,
            this.barCheckItemGeometryInAxis,
            this.barButtonItemObjectManipulator,
            this.barButtonItemSetSelectionAsCurrent,
            this.barEditItemManipulatorType,
            this.barEditItemManipulatorActionType,
            this.barEditItemManipulatorAction,
            this.barStaticItem1,
            this.barStaticItem2,
            this.barButtonItemCreateNode,
            this.barButtonItemDownLoadAsset,
            this.barButtonItemAddModel,
            this.barButtonItemUpload3DSource,
            this.barButtonItemImportReferenceFile,
            this.barButtonItemAddReferenceItem,
            this.barButtonItemUploadSim,
            this.barCheckItem1,
            this.barCheckItem2,
            this.barCheckItem3,
            this.barToggleSwitchItem1,
            this.barEditItemUploadedObj,
            this.barEditItemUploadedRefClass,
            this.barEditItemUploadedSimClass});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 28;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageMain});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBoxManipulatorType,
            this.repositoryItemCheckedComboBoxEditManipulatorActionType,
            this.repositoryItemComboBoxManipulatorAction,
            this.repositoryItemCheckEditUploadedObj,
            this.repositoryItemCheckEditUploadedRefClass,
            this.repositoryItemCheckEditUploadedSimClass});
            this.ribbonControl1.Size = new System.Drawing.Size(1581, 150);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // barButtonItemImport
            // 
            this.barButtonItemImport.Caption = "Import 3D File";
            this.barButtonItemImport.Id = 1;
            this.barButtonItemImport.Name = "barButtonItemImport";
            this.barButtonItemImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemImport_ItemClick);
            // 
            // barButtonItemExport
            // 
            this.barButtonItemExport.Caption = "Export (3D File)";
            this.barButtonItemExport.Id = 2;
            this.barButtonItemExport.Name = "barButtonItemExport";
            this.barButtonItemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemExport_ItemClick);
            // 
            // barCheckItemGeometryInAxis
            // 
            this.barCheckItemGeometryInAxis.Caption = "Geometry in \'Y Axis Up\'";
            this.barCheckItemGeometryInAxis.Id = 3;
            this.barCheckItemGeometryInAxis.Name = "barCheckItemGeometryInAxis";
            // 
            // barButtonItemObjectManipulator
            // 
            this.barButtonItemObjectManipulator.Caption = "Object Manipulator";
            this.barButtonItemObjectManipulator.Id = 4;
            this.barButtonItemObjectManipulator.Name = "barButtonItemObjectManipulator";
            this.barButtonItemObjectManipulator.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemObjectManipulator_ItemClick);
            // 
            // barButtonItemSetSelectionAsCurrent
            // 
            this.barButtonItemSetSelectionAsCurrent.Caption = "Set Selection As Current";
            this.barButtonItemSetSelectionAsCurrent.Id = 5;
            this.barButtonItemSetSelectionAsCurrent.Name = "barButtonItemSetSelectionAsCurrent";
            this.barButtonItemSetSelectionAsCurrent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSetSelectionAsCurrent_ItemClick);
            // 
            // barEditItemManipulatorType
            // 
            this.barEditItemManipulatorType.Caption = "Manipulator Type";
            this.barEditItemManipulatorType.Edit = this.repositoryItemComboBoxManipulatorType;
            this.barEditItemManipulatorType.Id = 7;
            this.barEditItemManipulatorType.Name = "barEditItemManipulatorType";
            this.barEditItemManipulatorType.EditValueChanged += new System.EventHandler(this.barEditItemManipulatorType_EditValueChanged);
            // 
            // repositoryItemComboBoxManipulatorType
            // 
            this.repositoryItemComboBoxManipulatorType.AutoHeight = false;
            this.repositoryItemComboBoxManipulatorType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxManipulatorType.DropDownRows = 3;
            this.repositoryItemComboBoxManipulatorType.Items.AddRange(new object[] {
            "Standard",
            "Rings",
            "Large"});
            this.repositoryItemComboBoxManipulatorType.Name = "repositoryItemComboBoxManipulatorType";
            this.repositoryItemComboBoxManipulatorType.NullText = "Standard";
            // 
            // barEditItemManipulatorActionType
            // 
            this.barEditItemManipulatorActionType.Caption = "Manipulator Action Type";
            this.barEditItemManipulatorActionType.Edit = this.repositoryItemCheckedComboBoxEditManipulatorActionType;
            this.barEditItemManipulatorActionType.Id = 8;
            this.barEditItemManipulatorActionType.Name = "barEditItemManipulatorActionType";
            // 
            // repositoryItemCheckedComboBoxEditManipulatorActionType
            // 
            this.repositoryItemCheckedComboBoxEditManipulatorActionType.AutoHeight = false;
            this.repositoryItemCheckedComboBoxEditManipulatorActionType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCheckedComboBoxEditManipulatorActionType.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Translation on axis", System.Windows.Forms.CheckState.Checked),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Rotation", System.Windows.Forms.CheckState.Checked),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Scaling")});
            this.repositoryItemCheckedComboBoxEditManipulatorActionType.Name = "repositoryItemCheckedComboBoxEditManipulatorActionType";
            this.repositoryItemCheckedComboBoxEditManipulatorActionType.EditValueChanged += new System.EventHandler(this.repositoryItemCheckedComboBoxEditManipulatorActionType_EditValueChanged);
            // 
            // barEditItemManipulatorAction
            // 
            this.barEditItemManipulatorAction.Caption = "Manipulator Action";
            this.barEditItemManipulatorAction.Edit = this.repositoryItemComboBoxManipulatorAction;
            this.barEditItemManipulatorAction.Id = 9;
            this.barEditItemManipulatorAction.Name = "barEditItemManipulatorAction";
            // 
            // repositoryItemComboBoxManipulatorAction
            // 
            this.repositoryItemComboBoxManipulatorAction.AutoHeight = false;
            this.repositoryItemComboBoxManipulatorAction.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxManipulatorAction.DropDownRows = 4;
            this.repositoryItemComboBoxManipulatorAction.Items.AddRange(new object[] {
            "Translate",
            "Rotate",
            "Scale"});
            this.repositoryItemComboBoxManipulatorAction.Name = "repositoryItemComboBoxManipulatorAction";
            this.repositoryItemComboBoxManipulatorAction.NullText = "Translate";
            this.repositoryItemComboBoxManipulatorAction.SelectedIndexChanged += new System.EventHandler(this.repositoryItemComboBoxManipulatorAction_SelectedIndexChanged);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Id = 10;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Id = 11;
            this.barStaticItem2.Name = "barStaticItem2";
            // 
            // barButtonItemCreateNode
            // 
            this.barButtonItemCreateNode.Caption = "Create Node (3D, Simulation)";
            this.barButtonItemCreateNode.Id = 12;
            this.barButtonItemCreateNode.Name = "barButtonItemCreateNode";
            this.barButtonItemCreateNode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCreateNode_ItemClick);
            // 
            // barButtonItemDownLoadAsset
            // 
            this.barButtonItemDownLoadAsset.Caption = "Download Asset";
            this.barButtonItemDownLoadAsset.Id = 13;
            this.barButtonItemDownLoadAsset.Name = "barButtonItemDownLoadAsset";
            this.barButtonItemDownLoadAsset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemDownLoadAsset_ItemClick);
            // 
            // barButtonItemAddModel
            // 
            this.barButtonItemAddModel.Caption = "Add (3D File)";
            this.barButtonItemAddModel.Id = 14;
            this.barButtonItemAddModel.Name = "barButtonItemAddModel";
            this.barButtonItemAddModel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAddModel_ItemClick);
            // 
            // barButtonItemUpload3DSource
            // 
            this.barButtonItemUpload3DSource.Caption = "Upload 3D Source";
            this.barButtonItemUpload3DSource.Id = 15;
            this.barButtonItemUpload3DSource.Name = "barButtonItemUpload3DSource";
            this.barButtonItemUpload3DSource.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemUpload3DItem_ItemClick);
            // 
            // barButtonItemImportReferenceFile
            // 
            this.barButtonItemImportReferenceFile.Caption = "Import (Reference File)";
            this.barButtonItemImportReferenceFile.Id = 16;
            this.barButtonItemImportReferenceFile.Name = "barButtonItemImportReferenceFile";
            this.barButtonItemImportReferenceFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemImportReferenceFile_ItemClick);
            // 
            // barButtonItemAddReferenceItem
            // 
            this.barButtonItemAddReferenceItem.Caption = "Add (Reference File)";
            this.barButtonItemAddReferenceItem.Id = 17;
            this.barButtonItemAddReferenceItem.Name = "barButtonItemAddReferenceItem";
            this.barButtonItemAddReferenceItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAddReferenceItem_ItemClick);
            // 
            // barButtonItemUploadSim
            // 
            this.barButtonItemUploadSim.Caption = "Upload Simulation Source";
            this.barButtonItemUploadSim.Id = 18;
            this.barButtonItemUploadSim.Name = "barButtonItemUploadSim";
            this.barButtonItemUploadSim.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemUploadSim_ItemClick);
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Caption = "barCheckItem1";
            this.barCheckItem1.Id = 19;
            this.barCheckItem1.Name = "barCheckItem1";
            // 
            // barCheckItem2
            // 
            this.barCheckItem2.Caption = "barCheckItem2";
            this.barCheckItem2.Id = 20;
            this.barCheckItem2.Name = "barCheckItem2";
            // 
            // barCheckItem3
            // 
            this.barCheckItem3.Caption = "barCheckItem3";
            this.barCheckItem3.Id = 21;
            this.barCheckItem3.Name = "barCheckItem3";
            // 
            // barToggleSwitchItem1
            // 
            this.barToggleSwitchItem1.Caption = "barToggleSwitchItem1";
            this.barToggleSwitchItem1.Id = 22;
            this.barToggleSwitchItem1.Name = "barToggleSwitchItem1";
            // 
            // barEditItemUploadedObj
            // 
            this.barEditItemUploadedObj.Caption = "Uploaded Obj";
            this.barEditItemUploadedObj.Edit = this.repositoryItemCheckEditUploadedObj;
            this.barEditItemUploadedObj.Id = 23;
            this.barEditItemUploadedObj.Name = "barEditItemUploadedObj";
            // 
            // repositoryItemCheckEditUploadedObj
            // 
            this.repositoryItemCheckEditUploadedObj.AutoHeight = false;
            this.repositoryItemCheckEditUploadedObj.Name = "repositoryItemCheckEditUploadedObj";
            // 
            // barEditItemUploadedRefClass
            // 
            this.barEditItemUploadedRefClass.Caption = "Uploaded Reference Class";
            this.barEditItemUploadedRefClass.Edit = this.repositoryItemCheckEditUploadedRefClass;
            this.barEditItemUploadedRefClass.Id = 24;
            this.barEditItemUploadedRefClass.Name = "barEditItemUploadedRefClass";
            // 
            // repositoryItemCheckEditUploadedRefClass
            // 
            this.repositoryItemCheckEditUploadedRefClass.AutoHeight = false;
            this.repositoryItemCheckEditUploadedRefClass.Name = "repositoryItemCheckEditUploadedRefClass";
            // 
            // barEditItemUploadedSimClass
            // 
            this.barEditItemUploadedSimClass.Caption = "Uploaded Simulation Class";
            this.barEditItemUploadedSimClass.Edit = this.repositoryItemCheckEditUploadedSimClass;
            this.barEditItemUploadedSimClass.Id = 25;
            this.barEditItemUploadedSimClass.Name = "barEditItemUploadedSimClass";
            // 
            // repositoryItemCheckEditUploadedSimClass
            // 
            this.repositoryItemCheckEditUploadedSimClass.AutoHeight = false;
            this.repositoryItemCheckEditUploadedSimClass.Name = "repositoryItemCheckEditUploadedSimClass";
            // 
            // ribbonPageMain
            // 
            this.ribbonPageMain.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupMain,
            this.ribbonPageGroupEdit,
            this.ribbonPageGroupSelect,
            this.ribbonPageGroup1});
            this.ribbonPageMain.Name = "ribbonPageMain";
            this.ribbonPageMain.Text = "Main";
            // 
            // ribbonPageGroupMain
            // 
            this.ribbonPageGroupMain.ItemLinks.Add(this.barButtonItemImport);
            this.ribbonPageGroupMain.ItemLinks.Add(this.barButtonItemAddModel);
            this.ribbonPageGroupMain.ItemLinks.Add(this.barButtonItemExport);
            this.ribbonPageGroupMain.ItemLinks.Add(this.barButtonItemImportReferenceFile);
            this.ribbonPageGroupMain.ItemLinks.Add(this.barButtonItemAddReferenceItem);
            this.ribbonPageGroupMain.ItemLinks.Add(this.barCheckItemGeometryInAxis);
            this.ribbonPageGroupMain.Name = "ribbonPageGroupMain";
            this.ribbonPageGroupMain.Text = "Main";
            // 
            // ribbonPageGroupEdit
            // 
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barStaticItem1);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemObjectManipulator);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barStaticItem2);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barEditItemManipulatorType);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barEditItemManipulatorAction);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barEditItemManipulatorActionType);
            this.ribbonPageGroupEdit.Name = "ribbonPageGroupEdit";
            this.ribbonPageGroupEdit.Text = "Object Manupulator";
            // 
            // ribbonPageGroupSelect
            // 
            this.ribbonPageGroupSelect.ItemLinks.Add(this.barButtonItemSetSelectionAsCurrent);
            this.ribbonPageGroupSelect.Name = "ribbonPageGroupSelect";
            this.ribbonPageGroupSelect.Text = "Select";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemCreateNode);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemUpload3DSource);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemUploadSim);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemDownLoadAsset);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Generate";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 956);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1581, 22);
            // 
            // propertyGridControl1
            // 
            this.propertyGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.propertyGridControl1.Location = new System.Drawing.Point(1243, 29);
            this.propertyGridControl1.MenuManager = this.ribbonControl1;
            this.propertyGridControl1.Name = "propertyGridControl1";
            this.propertyGridControl1.Size = new System.Drawing.Size(326, 372);
            this.propertyGridControl1.TabIndex = 12;
            // 
            // assemblyModel1
            // 
            this.assemblyModel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.assemblyModel1.CurrentRef = null;
            this.assemblyModel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.assemblyModel1.IDByNames = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<uint>>)(resources.GetObject("assemblyModel1.IDByNames")));
            this.assemblyModel1.Location = new System.Drawing.Point(313, 12);
            this.assemblyModel1.MouseLocation = new System.Drawing.Point(0, 0);
            this.assemblyModel1.MouseLocationSnapToCorrdinate = new devDept.Geometry.Point3D(0D, 0D, 0D);
            this.assemblyModel1.Name = "assemblyModel1";
            this.assemblyModel1.NodeReferenceByID = ((System.Collections.Generic.Dictionary<uint, Pinokio.Animation.NodeReference>)(resources.GetObject("assemblyModel1.NodeReferenceByID")));
            this.assemblyModel1.ProgressBar = progressBar2;
            this.assemblyModel1.SelectedFloorHeight = 0D;
            this.assemblyModel1.SelectedFloorID = ((uint)(0u));
            this.assemblyModel1.ShortcutKeys = shortcutKeysSettings2;
            this.assemblyModel1.Size = new System.Drawing.Size(916, 782);
            this.assemblyModel1.TabIndex = 11;
            this.assemblyModel1.TempMoveArrows = new devDept.Eyeshot.Entities.Mesh[] {
        ((devDept.Eyeshot.Entities.Mesh)(resources.GetObject("assemblyModel1.TempMoveArrows"))),
        ((devDept.Eyeshot.Entities.Mesh)(resources.GetObject("assemblyModel1.TempMoveArrows1"))),
        ((devDept.Eyeshot.Entities.Mesh)(resources.GetObject("assemblyModel1.TempMoveArrows2"))),
        ((devDept.Eyeshot.Entities.Mesh)(resources.GetObject("assemblyModel1.TempMoveArrows3")))};
            this.assemblyModel1.Text = "assemblyModel1";
            coordinateSystemIcon2.LabelFont = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            viewport2.CoordinateSystemIcon = coordinateSystemIcon2;
            viewport2.Legends = new devDept.Eyeshot.Legend[0];
            originSymbol2.LabelFont = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            viewport2.OriginSymbol = originSymbol2;
            viewCubeIcon2.Font = null;
            viewCubeIcon2.InitialRotation = new devDept.Geometry.Quaternion(0D, 0D, 0D, 1D);
            viewport2.ViewCubeIcon = viewCubeIcon2;
            this.assemblyModel1.Viewports.Add(viewport2);
            this.assemblyModel1.WorkCompleted += new devDept.Eyeshot.Environment.WorkCompletedEventHandler(this.assemblyModel1_WorkCompleted);
            // 
            // assemblyTreeView1
            // 
            this.assemblyTreeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.assemblyTreeView1.HideSelection = false;
            this.assemblyTreeView1.ImageIndex = 0;
            this.assemblyTreeView1.Location = new System.Drawing.Point(12, 29);
            this.assemblyTreeView1.Name = "assemblyTreeView1";
            this.assemblyTreeView1.SelectedImageIndex = 0;
            this.assemblyTreeView1.Size = new System.Drawing.Size(297, 765);
            this.assemblyTreeView1.TabIndex = 10;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem1,
            this.splitterItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1581, 806);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.assemblyTreeView1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(301, 786);
            this.layoutControlItem4.Text = "Assembly";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(88, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.assemblyModel1;
            this.layoutControlItem6.Location = new System.Drawing.Point(301, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(920, 786);
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.propertyGridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(1231, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(330, 393);
            this.layoutControlItem1.Text = "Property View";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(88, 14);
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(1221, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(10, 786);
            // 
            // xtraOpenFileDialog1
            // 
            this.xtraOpenFileDialog1.FileName = "s";
            this.xtraOpenFileDialog1.Filter = "3D Format (*.igs; *.iges;*.obj;*.stp; *.step;*.stl; *.3ds)|*.igs; *.iges;*.obj;*." +
    "stp; *.step;*.stl; *.3ds";
            this.xtraOpenFileDialog1.Multiselect = true;
            this.xtraOpenFileDialog1.RestoreDirectory = true;
            // 
            // xtraSaveFileDialog1
            // 
            this.xtraSaveFileDialog1.FileName = "xtraSaveFileDialog1";
            this.xtraSaveFileDialog1.Filter = "Obj (*.obj)|*.obj";
            this.xtraSaveFileDialog1.RestoreDirectory = true;
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // propertyGridControlReferenceType
            // 
            this.propertyGridControlReferenceType.Cursor = System.Windows.Forms.Cursors.Default;
            this.propertyGridControlReferenceType.Location = new System.Drawing.Point(1243, 422);
            this.propertyGridControlReferenceType.MenuManager = this.ribbonControl1;
            this.propertyGridControlReferenceType.Name = "propertyGridControlReferenceType";
            this.propertyGridControlReferenceType.Size = new System.Drawing.Size(326, 372);
            this.propertyGridControlReferenceType.TabIndex = 13;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.propertyGridControlReferenceType;
            this.layoutControlItem2.Location = new System.Drawing.Point(1231, 393);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(330, 393);
            this.layoutControlItem2.Text = "Reference Type";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(88, 14);
            // 
            // FormAssemblyEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1581, 978);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.IconOptions.Image = global::Pinokio.Designer.Properties.Resources.LOGO_ICON;
            this.Name = "FormAssemblyEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assembly Edit";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxManipulatorType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEditManipulatorActionType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxManipulatorAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditUploadedObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditUploadedRefClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditUploadedSimClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.assemblyModel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlReferenceType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.XtraOpenFileDialog xtraOpenFileDialog1;
        private Pinokio.Designer.AssemblyTreeView assemblyTreeView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Pinokio.Designer.AssemblyModel assemblyModel1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.XtraSaveFileDialog xtraSaveFileDialog1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageMain;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupMain;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupEdit;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.BarButtonItem barButtonItemImport;
        private DevExpress.XtraBars.BarButtonItem barButtonItemExport;
        private DevExpress.XtraBars.BarCheckItem barCheckItemGeometryInAxis;
        private DevExpress.XtraBars.BarButtonItem barButtonItemObjectManipulator;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSetSelectionAsCurrent;
        private DevExpress.XtraBars.BarEditItem barEditItemManipulatorType;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxManipulatorType;
        private DevExpress.XtraBars.BarEditItem barEditItemManipulatorActionType;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEditManipulatorActionType;
        private DevExpress.XtraBars.BarEditItem barEditItemManipulatorAction;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxManipulatorAction;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSelect;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCreateNode;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDownLoadAsset;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAddModel;
        private DevExpress.XtraBars.BarButtonItem barButtonItemUpload3DSource;
        private DevExpress.XtraBars.BarButtonItem barButtonItemImportReferenceFile;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAddReferenceItem;
        private DevExpress.XtraBars.BarButtonItem barButtonItemUploadSim;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.BarCheckItem barCheckItem2;
        private DevExpress.XtraBars.BarCheckItem barCheckItem3;
        private DevExpress.XtraBars.BarToggleSwitchItem barToggleSwitchItem1;
        private DevExpress.XtraBars.BarEditItem barEditItemUploadedObj;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditUploadedObj;
        private DevExpress.XtraBars.BarEditItem barEditItemUploadedRefClass;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditUploadedRefClass;
        private DevExpress.XtraBars.BarEditItem barEditItemUploadedSimClass;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditUploadedSimClass;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControlReferenceType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}