

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using Pinokio.Designer.DataClass;
using Pinokio.Animation;

namespace Pinokio.Designer
{
    partial class ModelDesigner
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelDesigner));
            this.document1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanelParts = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanelParts_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControlParts = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlPart = new DevExpress.XtraGrid.GridControl();
            this.gridViewPart = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroupParts = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemParts = new DevExpress.XtraLayout.LayoutControlItem();
            this.dockPanelLineStatus = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainerLineStatus = new DevExpress.XtraBars.Docking.ControlContainer();
            this.treeListLineStatus = new DevExpress.XtraTreeList.TreeList();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.RB_BTN_NEW = new DevExpress.XtraBars.BarButtonItem();
            this.RB_BTN_LOAD = new DevExpress.XtraBars.BarButtonItem();
            this.RB_BTN_SAVE = new DevExpress.XtraBars.BarButtonItem();
            this.RB_BTN_FLOORSETUP = new DevExpress.XtraBars.BarButtonItem();
            this.RB_BTN_LINK = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemImPortModel = new DevExpress.XtraBars.BarButtonItem();
            this.bbiModelingProductionData = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSettingSteps = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAMHSReport = new DevExpress.XtraBars.BarButtonItem();
            this.bbiProductionReport = new DevExpress.XtraBars.BarButtonItem();
            this.Animation_Tog_Switch = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.AnimationSpeedTrack = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemZoomTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.barButtonItemPlayback = new DevExpress.XtraBars.BarButtonItem();
            this.beiCurrentSimTimeView = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTimeEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.beiSimStartTimeSetting = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTimeEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.Background_Color_Tog_Switch = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.beSimulationAcceleration = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.beiSimEndTimeSetting = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTimeEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.beiAnimationSpeed = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemSpinEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.bbiSimResume = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSimPause = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSimStop = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSimReservePause = new DevExpress.XtraBars.BarButtonItem();
            this.Auto_Focusing_Tog_Switch = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.barButtonItemOpenFolder = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemViewSetting = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEditScript = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDockInsertNode = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDockInsertCoupledModel = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDockNodes = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDockObjectProperties = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDockPart = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDockLineStatus = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDockLineStatusDetail = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEditEquipment = new DevExpress.XtraBars.BarButtonItem();
            this.RB_BTN_SAVE_AS = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEditBreakDown = new DevExpress.XtraBars.BarButtonItem();
            this.bbiLoadProductionReport = new DevExpress.XtraBars.BarButtonItem();
            this.bbiLoadAMHSReport = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEditNodes = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEditVisible = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonGroup1 = new DevExpress.XtraBars.BarButtonGroup();
            this.WarmUpPeriod = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.BeiHours = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox5 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.BeiMinutes = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox6 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.BeiDays = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox4 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.BliWarmUpPeriod = new DevExpress.XtraBars.BarLinkContainerItem();
            this.bbiSaveSnapShot = new DevExpress.XtraBars.BarButtonItem();
            this.bbiLoadSnapShot = new DevExpress.XtraBars.BarButtonItem();
            this.State_Based_Vehicle_Tog_Switch = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.State_Based_Equipment_Tog_Switch = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.State_Based_TransportLine_Tog_Switch = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.bbiAlignLeft = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAlignCenter = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAlignRight = new DevExpress.XtraBars.BarButtonItem();
            this.bbiTopAlign = new DevExpress.XtraBars.BarButtonItem();
            this.bbiMiddleAlign = new DevExpress.XtraBars.BarButtonItem();
            this.bbiBottomAlign = new DevExpress.XtraBars.BarButtonItem();
            this.RB_FIRST = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.RB_PG_FILES = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.RB_PG_SETTINGS = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgProduction = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupEdit = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgConvert = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageSimulation = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgAnimation = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgSimulationExecution = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.bbiSimRun = new DevExpress.XtraBars.BarButtonItem();
            this.rpgTimeCondition = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgSnapShot = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgStateBasedColors = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpAnalysis = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgReport = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpWindow = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTimeEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.repositoryItemComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.dockPanelLineStatusDetail = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanelLineStatusDetail_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.lbCount = new System.Windows.Forms.Label();
            this.labelAutoRefresh = new System.Windows.Forms.Label();
            this.cbTree_Interval = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridControlLineStatusDetail = new DevExpress.XtraGrid.GridControl();
            this.gridViewLineStatusDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dockPanelSimNodeProperties = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainerSimNode = new DevExpress.XtraBars.Docking.ControlContainer();
            this.propertyGridControlSimObject = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanelInsertedSimNodes = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanelCoupledModels_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControlInsertedSimNodes = new DevExpress.XtraLayout.LayoutControl();
            this.simNodeTreeList = new Pinokio.Animation.SimNodeTreeList();
            this.simpleButtonRemoveTreeNode = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroupInsertedSimNodes = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemInsertedSimNodes = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemRemoveSimNode = new DevExpress.XtraLayout.LayoutControlItem();
            this.dockPanelInsertRefNode = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanelInsertRefNode_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControlInsertNode = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlInsertRefNode = new DevExpress.XtraGrid.GridControl();
            this.gridViewInsertRefNode = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroupInsertNode = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemInsertNode = new DevExpress.XtraLayout.LayoutControlItem();
            this.dockPanelInsertCoupledModel = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanelInsertCoupledModel_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControlInsertCoupledModel = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlInsertCoupledModel = new DevExpress.XtraGrid.GridControl();
            this.gridViewInsertCoupledModel = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButtonAddCoupledModel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroupInsertCoupledModel = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemInsertCoupledModel = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAddCoupledModel = new DevExpress.XtraLayout.LayoutControlItem();
            this.dockPanelMainScreen = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.repositoryItemTimeEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.dockPanelInsertNodeTree = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.treeListInsertNodeTree = new DevExpress.XtraTreeList.TreeList();
            this.treeListRefTypeCol = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListNodeTypeCol = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListHeightCol = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.layoutControlGroupInsertNodeTree = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cbTree_Interval2 = new System.Windows.Forms.ComboBox();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.contextMenuStripEditNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemAddNode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDeleteNode = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.documentGroup1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.xtraSaveFileDialog1 = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            this.xtraOpenFileDialog1 = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.accordionControlElement6 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
            this.xtraOpenFileDialog2 = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.SimEndCondiText = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.SimStateBarEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.textCurrentSimTimeView = new DevExpress.XtraBars.BarEditItem();
            this.textSimTimeSetting = new DevExpress.XtraBars.BarEditItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barHeaderItem1 = new DevExpress.XtraBars.BarHeaderItem();
            this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
            this.barEditItem3 = new DevExpress.XtraBars.BarEditItem();
            this.barEditItem4 = new DevExpress.XtraBars.BarEditItem();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanelParts.SuspendLayout();
            this.dockPanelParts_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlParts)).BeginInit();
            this.layoutControlParts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupParts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemParts)).BeginInit();
            this.dockPanelLineStatus.SuspendLayout();
            this.controlContainerLineStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLineStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            this.dockPanelLineStatusDetail.SuspendLayout();
            this.dockPanelLineStatusDetail_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbTree_Interval.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLineStatusDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLineStatusDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPart)).BeginInit();
            this.dockPanelSimNodeProperties.SuspendLayout();
            this.controlContainerSimNode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlSimObject)).BeginInit();
            this.panelContainer1.SuspendLayout();
            this.dockPanelInsertedSimNodes.SuspendLayout();
            this.dockPanelCoupledModels_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlInsertedSimNodes)).BeginInit();
            this.layoutControlInsertedSimNodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simNodeTreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupInsertedSimNodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInsertedSimNodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRemoveSimNode)).BeginInit();
            this.dockPanelInsertRefNode.SuspendLayout();
            this.dockPanelInsertRefNode_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlInsertNode)).BeginInit();
            this.layoutControlInsertNode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlInsertRefNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInsertRefNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupInsertNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInsertNode)).BeginInit();
            this.dockPanelInsertCoupledModel.SuspendLayout();
            this.dockPanelInsertCoupledModel_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlInsertCoupledModel)).BeginInit();
            this.layoutControlInsertCoupledModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlInsertCoupledModel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInsertCoupledModel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupInsertCoupledModel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInsertCoupledModel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAddCoupledModel)).BeginInit();
            this.dockPanelMainScreen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit5)).BeginInit();
            this.dockPanelInsertNodeTree.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListInsertNodeTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupInsertNodeTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.contextMenuStripEditNode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SimEndCondiText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SimStateBarEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // document1
            // 
            this.document1.Caption = "document1";
            this.document1.ControlName = "document1";
            this.document1.FloatLocation = new System.Drawing.Point(513, 160);
            this.document1.FloatSize = new System.Drawing.Size(863, 989);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelParts,
            this.dockPanelLineStatus,
            this.dockPanelLineStatusDetail});
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelSimNodeProperties,
            this.panelContainer1,
            this.dockPanelMainScreen});
            this.dockManager1.Style = DevExpress.XtraBars.Docking2010.Views.DockingViewStyle.Light;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // dockPanelParts
            // 
            this.dockPanelParts.Controls.Add(this.dockPanelParts_Container);
            this.dockPanelParts.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelParts.ID = new System.Guid("d1e012a0-1d47-49ab-a898-17c1e8bf03ce");
            this.dockPanelParts.Location = new System.Drawing.Point(617, 126);
            this.dockPanelParts.Name = "dockPanelParts";
            this.dockPanelParts.OriginalSize = new System.Drawing.Size(218, 200);
            this.dockPanelParts.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelParts.Size = new System.Drawing.Size(218, 616);
            this.dockPanelParts.Text = "Parts";
            this.dockPanelParts.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            this.dockPanelParts.ClosingPanel += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(this.dockPanelParts_ClosingPanel);
            // 
            // dockPanelParts_Container
            // 
            this.dockPanelParts_Container.Controls.Add(this.layoutControlParts);
            this.dockPanelParts_Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanelParts_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanelParts_Container.Name = "dockPanelParts_Container";
            this.dockPanelParts_Container.Size = new System.Drawing.Size(218, 616);
            this.dockPanelParts_Container.TabIndex = 0;
            // 
            // layoutControlParts
            // 
            this.layoutControlParts.Controls.Add(this.gridControlPart);
            this.layoutControlParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlParts.Location = new System.Drawing.Point(0, 0);
            this.layoutControlParts.Name = "layoutControlParts";
            this.layoutControlParts.Root = this.layoutControlGroupParts;
            this.layoutControlParts.Size = new System.Drawing.Size(218, 616);
            this.layoutControlParts.TabIndex = 1;
            this.layoutControlParts.Text = "Parts";
            // 
            // layoutControlGroupParts
            // 
            this.layoutControlGroupParts.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupParts.GroupBordersVisible = false;
            this.layoutControlGroupParts.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemParts});
            this.layoutControlGroupParts.Name = "layoutControlGroupParts";
            this.layoutControlGroupParts.Size = new System.Drawing.Size(218, 616);
            this.layoutControlGroupParts.TextVisible = false;
            // 
            // layoutControlItemParts
            // 
            this.layoutControlItemParts.Control = this.gridControlPart;
            this.layoutControlItemParts.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemParts.Name = "layoutControlItemParts";
            this.layoutControlItemParts.Size = new System.Drawing.Size(198, 596);
            this.layoutControlItemParts.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemParts.TextVisible = false;
            // 
            // dockPanelLineStatus
            // 
            this.dockPanelLineStatus.Controls.Add(this.controlContainerLineStatus);
            this.dockPanelLineStatus.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelLineStatus.FloatVertical = true;
            this.dockPanelLineStatus.ID = new System.Guid("0ae4fbb4-582e-40c8-a828-f2de23d6b276");
            this.dockPanelLineStatus.Location = new System.Drawing.Point(0, 331);
            this.dockPanelLineStatus.Name = "dockPanelLineStatus";
            this.dockPanelLineStatus.OriginalSize = new System.Drawing.Size(260, 455);
            this.dockPanelLineStatus.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelLineStatus.Size = new System.Drawing.Size(260, 454);
            this.dockPanelLineStatus.Text = "Line Status";
            this.dockPanelLineStatus.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            this.dockPanelLineStatus.ClosingPanel += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(this.dockPanelLineStatus_ClosingPanel);
            // 
            // controlContainerLineStatus
            // 
            this.controlContainerLineStatus.Controls.Add(this.treeListLineStatus);
            this.controlContainerLineStatus.Location = new System.Drawing.Point(3, 26);
            this.controlContainerLineStatus.Name = "controlContainerLineStatus";
            this.controlContainerLineStatus.Size = new System.Drawing.Size(139, 425);
            this.controlContainerLineStatus.TabIndex = 0;
            // 
            // treeListLineStatus
            // 
            this.treeListLineStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListLineStatus.Location = new System.Drawing.Point(0, 0);
            this.treeListLineStatus.MenuManager = this.ribbonControl1;
            this.treeListLineStatus.Name = "treeListLineStatus";
            this.treeListLineStatus.Size = new System.Drawing.Size(139, 425);
            this.treeListLineStatus.TabIndex = 0;
            this.treeListLineStatus.RowCellClick += new DevExpress.XtraTreeList.RowCellClickEventHandler(this.treeList_RowCellClick);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.AutoSizeItems = true;
            this.ribbonControl1.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.DarkBlue;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.RB_BTN_NEW,
            this.RB_BTN_LOAD,
            this.RB_BTN_SAVE,
            this.RB_BTN_FLOORSETUP,
            this.RB_BTN_LINK,
            this.barButtonItemImPortModel,
            this.bbiModelingProductionData,
            this.bbiSettingSteps,
            this.bbiAMHSReport,
            this.bbiProductionReport,
            this.Animation_Tog_Switch,
            this.AnimationSpeedTrack,
            this.barButtonItemPlayback,
            this.beiCurrentSimTimeView,
            this.beiSimStartTimeSetting,
            this.Background_Color_Tog_Switch,
            this.beSimulationAcceleration,
            this.beiSimEndTimeSetting,
            this.beiAnimationSpeed,
            this.bbiSimResume,
            this.bbiSimPause,
            this.bbiSimStop,
            this.bbiSimReservePause,
            this.Auto_Focusing_Tog_Switch,
            this.barButtonItemOpenFolder,
            this.barButtonItemViewSetting,
            this.bbiEditScript,
            this.bbiDockInsertNode,
            this.bbiDockInsertCoupledModel,
            this.bbiDockNodes,
            this.bbiDockObjectProperties,
            this.bbiDockPart,
            this.bbiDockLineStatus,
            this.bbiDockLineStatusDetail,
            this.bbiEditEquipment,
            this.RB_BTN_SAVE_AS,
            this.bbiEditBreakDown,
            this.bbiLoadProductionReport,
            this.bbiLoadAMHSReport,
            this.bbiEditNodes,
            this.bbiEditVisible,
            this.barButtonGroup1,
            this.WarmUpPeriod,
            this.BeiHours,
            this.BeiMinutes,
            this.BeiDays,
            this.BliWarmUpPeriod,
            this.bbiSaveSnapShot,
            this.bbiLoadSnapShot,
            this.State_Based_Vehicle_Tog_Switch,
            this.State_Based_Equipment_Tog_Switch,
            this.State_Based_TransportLine_Tog_Switch,
            this.barSubItem1,
            this.bbiAlignLeft,
            this.bbiAlignCenter,
            this.bbiTopAlign,
            this.bbiBottomAlign,
            this.bbiAlignRight,
            this.bbiMiddleAlign});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 130;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.RB_FIRST,
            this.ribbonPageSimulation,
            this.rpAnalysis,
            this.rpWindow});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemComboBox2,
            this.repositoryItemTimeEdit2,
            this.repositoryItemComboBox3,
            this.repositoryItemComboBox4,
            this.repositoryItemComboBox5,
            this.repositoryItemComboBox6,
            this.repositoryItemTextEdit3});
            this.ribbonControl1.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(1677, 126);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // RB_BTN_NEW
            // 
            this.RB_BTN_NEW.Caption = "New";
            this.RB_BTN_NEW.Id = 2;
            this.RB_BTN_NEW.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RB_BTN_NEW.ImageOptions.Image")));
            this.RB_BTN_NEW.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("RB_BTN_NEW.ImageOptions.LargeImage")));
            this.RB_BTN_NEW.Name = "RB_BTN_NEW";
            this.RB_BTN_NEW.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RB_BTN_NEW_ItemClick);
            // 
            // RB_BTN_LOAD
            // 
            this.RB_BTN_LOAD.Caption = "Load";
            this.RB_BTN_LOAD.Id = 3;
            this.RB_BTN_LOAD.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RB_BTN_LOAD.ImageOptions.Image")));
            this.RB_BTN_LOAD.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("RB_BTN_LOAD.ImageOptions.LargeImage")));
            this.RB_BTN_LOAD.Name = "RB_BTN_LOAD";
            this.RB_BTN_LOAD.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RB_BTN_LOAD_ItemClick);
            // 
            // RB_BTN_SAVE
            // 
            this.RB_BTN_SAVE.Caption = "Save";
            this.RB_BTN_SAVE.Id = 4;
            this.RB_BTN_SAVE.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RB_BTN_SAVE.ImageOptions.Image")));
            this.RB_BTN_SAVE.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("RB_BTN_SAVE.ImageOptions.LargeImage")));
            this.RB_BTN_SAVE.Name = "RB_BTN_SAVE";
            this.RB_BTN_SAVE.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RB_BTN_SAVE_ItemClick);
            // 
            // RB_BTN_FLOORSETUP
            // 
            this.RB_BTN_FLOORSETUP.Caption = "Edit Floor     ";
            this.RB_BTN_FLOORSETUP.Id = 7;
            this.RB_BTN_FLOORSETUP.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RB_BTN_FLOORSETUP.ImageOptions.Image")));
            this.RB_BTN_FLOORSETUP.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("RB_BTN_FLOORSETUP.ImageOptions.LargeImage")));
            this.RB_BTN_FLOORSETUP.Name = "RB_BTN_FLOORSETUP";
            this.RB_BTN_FLOORSETUP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RB_BTN_FLOORSETUP_ItemClick);
            // 
            // RB_BTN_LINK
            // 
            this.RB_BTN_LINK.Caption = "Link";
            this.RB_BTN_LINK.Enabled = false;
            this.RB_BTN_LINK.Id = 12;
            this.RB_BTN_LINK.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("RB_BTN_LINK.ImageOptions.SvgImage")));
            this.RB_BTN_LINK.Name = "RB_BTN_LINK";
            this.RB_BTN_LINK.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RB_BTN_LINK_ItemClick);
            // 
            // barButtonItemImPortModel
            // 
            this.barButtonItemImPortModel.Caption = "3D Model Manager";
            this.barButtonItemImPortModel.Enabled = false;
            this.barButtonItemImPortModel.Id = 26;
            this.barButtonItemImPortModel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemImPortModel.ImageOptions.Image")));
            this.barButtonItemImPortModel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemImPortModel.ImageOptions.LargeImage")));
            this.barButtonItemImPortModel.Name = "barButtonItemImPortModel";
            this.barButtonItemImPortModel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemImPortModel_ItemClick);
            // 
            // bbiModelingProductionData
            // 
            this.bbiModelingProductionData.Caption = "Edit Product";
            this.bbiModelingProductionData.Id = 175;
            this.bbiModelingProductionData.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiModelingProductionData.ImageOptions.Image")));
            this.bbiModelingProductionData.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiModelingProductionData.ImageOptions.LargeImage")));
            this.bbiModelingProductionData.Name = "bbiModelingProductionData";
            this.bbiModelingProductionData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiModelingProductionData_ItemClick);
            // 
            // bbiSettingSteps
            // 
            this.bbiSettingSteps.Caption = "Edit Step";
            this.bbiSettingSteps.Id = 198;
            this.bbiSettingSteps.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiSettingSteps.ImageOptions.Image")));
            this.bbiSettingSteps.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiSettingSteps.ImageOptions.LargeImage")));
            this.bbiSettingSteps.Name = "bbiSettingSteps";
            this.bbiSettingSteps.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSettingSteps_ItemClick);
            // 
            // bbiAMHSReport
            // 
            this.bbiAMHSReport.Caption = "AMHS Report";
            this.bbiAMHSReport.Enabled = false;
            this.bbiAMHSReport.Id = 55;
            this.bbiAMHSReport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiAMHSReport.ImageOptions.Image")));
            this.bbiAMHSReport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiAMHSReport.ImageOptions.LargeImage")));
            this.bbiAMHSReport.Name = "bbiAMHSReport";
            this.bbiAMHSReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAMHSReport_ItemClick);
            // 
            // bbiProductionReport
            // 
            this.bbiProductionReport.Caption = "Production Report";
            this.bbiProductionReport.Enabled = false;
            this.bbiProductionReport.Id = 198;
            this.bbiProductionReport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiProductionReport.ImageOptions.Image")));
            this.bbiProductionReport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiProductionReport.ImageOptions.LargeImage")));
            this.bbiProductionReport.Name = "bbiProductionReport";
            this.bbiProductionReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiProductionReport_ItemClick);
            // 
            // Animation_Tog_Switch
            // 
            this.Animation_Tog_Switch.BindableChecked = true;
            this.Animation_Tog_Switch.Caption = "Animation Off/On:";
            this.Animation_Tog_Switch.Checked = true;
            this.Animation_Tog_Switch.Id = 42;
            this.Animation_Tog_Switch.Name = "Animation_Tog_Switch";
            this.Animation_Tog_Switch.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.Animation_Tog_Switch_CheckedChanged);
            // 
            // AnimationSpeedTrack
            // 
            this.AnimationSpeedTrack.Edit = this.repositoryItemZoomTrackBar1;
            this.AnimationSpeedTrack.EditValue = 1;
            this.AnimationSpeedTrack.EditWidth = 180;
            this.AnimationSpeedTrack.Id = 45;
            this.AnimationSpeedTrack.Name = "AnimationSpeedTrack";
            this.AnimationSpeedTrack.EditValueChanged += new System.EventHandler(this.AnimationSpeedTrack_EditValueChanged);
            // 
            // repositoryItemZoomTrackBar1
            // 
            this.repositoryItemZoomTrackBar1.EditValueChangedDelay = 50;
            this.repositoryItemZoomTrackBar1.LargeChange = 10;
            this.repositoryItemZoomTrackBar1.Maximum = 200;
            this.repositoryItemZoomTrackBar1.Minimum = 1;
            this.repositoryItemZoomTrackBar1.Name = "repositoryItemZoomTrackBar1";
            // 
            // barButtonItemPlayback
            // 
            this.barButtonItemPlayback.Id = 29;
            this.barButtonItemPlayback.Name = "barButtonItemPlayback";
            // 
            // beiCurrentSimTimeView
            // 
            this.beiCurrentSimTimeView.Caption = "       Current Sim Time: ";
            this.beiCurrentSimTimeView.Edit = this.repositoryItemTimeEdit1;
            this.beiCurrentSimTimeView.EditWidth = 200;
            this.beiCurrentSimTimeView.Enabled = false;
            this.beiCurrentSimTimeView.Id = 104;
            this.beiCurrentSimTimeView.Name = "beiCurrentSimTimeView";
            // 
            // repositoryItemTimeEdit1
            // 
            this.repositoryItemTimeEdit1.AutoHeight = false;
            this.repositoryItemTimeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEdit1.Mask.EditMask = "HH:mm:ss";
            this.repositoryItemTimeEdit1.Name = "repositoryItemTimeEdit1";
            // 
            // beiSimStartTimeSetting
            // 
            this.beiSimStartTimeSetting.Caption = "Sim Start Time Setting: ";
            this.beiSimStartTimeSetting.Edit = this.repositoryItemTimeEdit3;
            this.beiSimStartTimeSetting.EditWidth = 200;
            this.beiSimStartTimeSetting.Id = 106;
            this.beiSimStartTimeSetting.Name = "beiSimStartTimeSetting";
            // 
            // repositoryItemTimeEdit3
            // 
            this.repositoryItemTimeEdit3.AutoHeight = false;
            this.repositoryItemTimeEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEdit3.Mask.EditMask = "yyyy/MM/dd HH:mm:ss";
            this.repositoryItemTimeEdit3.Name = "repositoryItemTimeEdit3";
            // 
            // Background_Color_Tog_Switch
            // 
            this.Background_Color_Tog_Switch.BindableChecked = true;
            this.Background_Color_Tog_Switch.Caption = "Background Dark/Light: ";
            this.Background_Color_Tog_Switch.Checked = true;
            this.Background_Color_Tog_Switch.Id = 109;
            this.Background_Color_Tog_Switch.Name = "Background_Color_Tog_Switch";
            this.Background_Color_Tog_Switch.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.Background_Color_Tog_Switch_CheckedChanged);
            // 
            // beSimulationAcceleration
            // 
            this.beSimulationAcceleration.Caption = "Acceleration Rate: ";
            this.beSimulationAcceleration.Edit = this.repositoryItemSpinEdit1;
            this.beSimulationAcceleration.EditValue = "1";
            this.beSimulationAcceleration.EditWidth = 150;
            this.beSimulationAcceleration.Id = 112;
            this.beSimulationAcceleration.Name = "beSimulationAcceleration";
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            this.repositoryItemSpinEdit1.ReadOnly = true;
            // 
            // beiSimEndTimeSetting
            // 
            this.beiSimEndTimeSetting.Caption = " Sim End Time Setting: ";
            this.beiSimEndTimeSetting.Edit = this.repositoryItemTimeEdit4;
            this.beiSimEndTimeSetting.EditWidth = 200;
            this.beiSimEndTimeSetting.Id = 113;
            this.beiSimEndTimeSetting.Name = "beiSimEndTimeSetting";
            // 
            // repositoryItemTimeEdit4
            // 
            this.repositoryItemTimeEdit4.AutoHeight = false;
            this.repositoryItemTimeEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEdit4.Mask.EditMask = "yyyy/MM/dd HH:mm:ss";
            this.repositoryItemTimeEdit4.Name = "repositoryItemTimeEdit4";
            // 
            // beiAnimationSpeed
            // 
            this.beiAnimationSpeed.Caption = "Animation Speed: ";
            this.beiAnimationSpeed.Edit = this.repositoryItemSpinEdit2;
            this.beiAnimationSpeed.EditValue = "1";
            this.beiAnimationSpeed.EditWidth = 70;
            this.beiAnimationSpeed.Id = 125;
            this.beiAnimationSpeed.Name = "beiAnimationSpeed";
            this.beiAnimationSpeed.EditValueChanged += new System.EventHandler(this.beiAnimationSpeed_EditValueChanged);
            // 
            // repositoryItemSpinEdit2
            // 
            this.repositoryItemSpinEdit2.AutoHeight = false;
            this.repositoryItemSpinEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit2.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.repositoryItemSpinEdit2.Name = "repositoryItemSpinEdit2";
            // 
            // bbiSimResume
            // 
            this.bbiSimResume.Caption = "Resume";
            this.bbiSimResume.Id = 51;
            this.bbiSimResume.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiSimResume.ImageOptions.Image")));
            this.bbiSimResume.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiSimResume.ImageOptions.LargeImage")));
            this.bbiSimResume.Name = "bbiSimResume";
            this.bbiSimResume.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // bbiSimPause
            // 
            this.bbiSimPause.Caption = "Pause";
            this.bbiSimPause.Id = 52;
            this.bbiSimPause.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiSimPause.ImageOptions.Image")));
            this.bbiSimPause.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiSimPause.ImageOptions.LargeImage")));
            this.bbiSimPause.Name = "bbiSimPause";
            this.bbiSimPause.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbiSimPause.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSimPause_ItemClick);
            // 
            // bbiSimStop
            // 
            this.bbiSimStop.Caption = "Stop";
            this.bbiSimStop.Id = 53;
            this.bbiSimStop.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiSimStop.ImageOptions.Image")));
            this.bbiSimStop.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiSimStop.ImageOptions.LargeImage")));
            this.bbiSimStop.Name = "bbiSimStop";
            this.bbiSimStop.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbiSimStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSimStop_ItemClick);
            // 
            // bbiSimReservePause
            // 
            this.bbiSimReservePause.Caption = "Reserve Pause";
            this.bbiSimReservePause.Id = 54;
            this.bbiSimReservePause.ImageOptions.Image = global::Pinokio.Designer.Properties.Resources.time2_16x16;
            this.bbiSimReservePause.ImageOptions.LargeImage = global::Pinokio.Designer.Properties.Resources.time2_32x32;
            this.bbiSimReservePause.Name = "bbiSimReservePause";
            this.bbiSimReservePause.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSimReservePause_ItemClick);
            // 
            // Auto_Focusing_Tog_Switch
            // 
            this.Auto_Focusing_Tog_Switch.Caption = "Auto Focusing Off/On: ";
            this.Auto_Focusing_Tog_Switch.Id = 144;
            this.Auto_Focusing_Tog_Switch.Name = "Auto_Focusing_Tog_Switch";
            // 
            // barButtonItemOpenFolder
            // 
            this.barButtonItemOpenFolder.Id = 30;
            this.barButtonItemOpenFolder.Name = "barButtonItemOpenFolder";
            // 
            // barButtonItemViewSetting
            // 
            this.barButtonItemViewSetting.Id = 31;
            this.barButtonItemViewSetting.Name = "barButtonItemViewSetting";
            // 
            // bbiEditScript
            // 
            this.bbiEditScript.Caption = "Edit Script";
            this.bbiEditScript.Id = 33;
            this.bbiEditScript.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiEditScript.ImageOptions.Image")));
            this.bbiEditScript.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiEditScript.ImageOptions.LargeImage")));
            this.bbiEditScript.Name = "bbiEditScript";
            this.bbiEditScript.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiEditScript_ItemClick);
            // 
            // bbiDockInsertNode
            // 
            this.bbiDockInsertNode.Caption = "Insert Node";
            this.bbiDockInsertNode.Id = 33;
            this.bbiDockInsertNode.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiDockInsertNode.ImageOptions.SvgImage")));
            this.bbiDockInsertNode.Name = "bbiDockInsertNode";
            this.bbiDockInsertNode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiInsertNode_ItemClick);
            // 
            // bbiDockInsertCoupledModel
            // 
            this.bbiDockInsertCoupledModel.Caption = "Insert Coupled Model";
            this.bbiDockInsertCoupledModel.Id = 34;
            this.bbiDockInsertCoupledModel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiDockInsertCoupledModel.ImageOptions.SvgImage")));
            this.bbiDockInsertCoupledModel.Name = "bbiDockInsertCoupledModel";
            this.bbiDockInsertCoupledModel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiInsertCoupledModel_ItemClick);
            // 
            // bbiDockNodes
            // 
            this.bbiDockNodes.Caption = "Nodes";
            this.bbiDockNodes.Id = 35;
            this.bbiDockNodes.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiDockNodes.ImageOptions.SvgImage")));
            this.bbiDockNodes.Name = "bbiDockNodes";
            this.bbiDockNodes.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiNodes_ItemClick);
            // 
            // bbiDockObjectProperties
            // 
            this.bbiDockObjectProperties.Caption = "Object Properties";
            this.bbiDockObjectProperties.Id = 36;
            this.bbiDockObjectProperties.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiDockObjectProperties.ImageOptions.SvgImage")));
            this.bbiDockObjectProperties.Name = "bbiDockObjectProperties";
            this.bbiDockObjectProperties.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiObjectProperties_ItemClick);
            // 
            // bbiDockPart
            // 
            this.bbiDockPart.Caption = "Part";
            this.bbiDockPart.Id = 38;
            this.bbiDockPart.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiDockPart.ImageOptions.SvgImage")));
            this.bbiDockPart.Name = "bbiDockPart";
            this.bbiDockPart.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbiDockPart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDockPart_ItemClick);
            // 
            // bbiDockLineStatus
            // 
            this.bbiDockLineStatus.Caption = "Line Status";
            this.bbiDockLineStatus.Id = 39;
            this.bbiDockLineStatus.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiDockLineStatus.ImageOptions.SvgImage")));
            this.bbiDockLineStatus.Name = "bbiDockLineStatus";
            this.bbiDockLineStatus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbiDockLineStatus.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDockLineStatus_ItemClick);
            // 
            // bbiDockLineStatusDetail
            // 
            this.bbiDockLineStatusDetail.Caption = "Line Status Detail";
            this.bbiDockLineStatusDetail.Id = 40;
            this.bbiDockLineStatusDetail.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiDockLineStatusDetail.ImageOptions.SvgImage")));
            this.bbiDockLineStatusDetail.Name = "bbiDockLineStatusDetail";
            this.bbiDockLineStatusDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbiDockLineStatusDetail.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDockLineStatusDetail_ItemClick);
            // 
            // bbiEditEquipment
            // 
            this.bbiEditEquipment.Caption = "Edit Equipment";
            this.bbiEditEquipment.Id = 41;
            this.bbiEditEquipment.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiEditEquipment.ImageOptions.Image")));
            this.bbiEditEquipment.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiEditEquipment.ImageOptions.LargeImage")));
            this.bbiEditEquipment.Name = "bbiEditEquipment";
            this.bbiEditEquipment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiEditEquipment_ItemClick);
            // 
            // RB_BTN_SAVE_AS
            // 
            this.RB_BTN_SAVE_AS.Caption = "Save AS";
            this.RB_BTN_SAVE_AS.Id = 42;
            this.RB_BTN_SAVE_AS.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RB_BTN_SAVE_AS.ImageOptions.Image")));
            this.RB_BTN_SAVE_AS.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("RB_BTN_SAVE_AS.ImageOptions.LargeImage")));
            this.RB_BTN_SAVE_AS.Name = "RB_BTN_SAVE_AS";
            this.RB_BTN_SAVE_AS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RB_BTN_SAVE_AS_ItemClick);
            // 
            // bbiEditBreakDown
            // 
            this.bbiEditBreakDown.Caption = "Edit BreakDown";
            this.bbiEditBreakDown.Enabled = false;
            this.bbiEditBreakDown.Id = 42;
            this.bbiEditBreakDown.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiEditBreakDown.ImageOptions.Image")));
            this.bbiEditBreakDown.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiEditBreakDown.ImageOptions.LargeImage")));
            this.bbiEditBreakDown.Name = "bbiEditBreakDown";
            this.bbiEditBreakDown.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiEditBreakDown_ItemClick);
            // 
            // bbiLoadProductionReport
            // 
            this.bbiLoadProductionReport.Caption = "Load Production Report";
            this.bbiLoadProductionReport.Enabled = false;
            this.bbiLoadProductionReport.Id = 43;
            this.bbiLoadProductionReport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiLoadProductionReport.ImageOptions.Image")));
            this.bbiLoadProductionReport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiLoadProductionReport.ImageOptions.LargeImage")));
            this.bbiLoadProductionReport.Name = "bbiLoadProductionReport";
            this.bbiLoadProductionReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiLoadProductionReport_ItemClick);
            // 
            // bbiLoadAMHSReport
            // 
            this.bbiLoadAMHSReport.Caption = "Load AMHS Report";
            this.bbiLoadAMHSReport.Enabled = false;
            this.bbiLoadAMHSReport.Id = 44;
            this.bbiLoadAMHSReport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiLoadAMHSReport.ImageOptions.Image")));
            this.bbiLoadAMHSReport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiLoadAMHSReport.ImageOptions.LargeImage")));
            this.bbiLoadAMHSReport.Name = "bbiLoadAMHSReport";
            this.bbiLoadAMHSReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiLoadAMHSReport_ItemClick);
            // 
            // bbiEditNodes
            // 
            this.bbiEditNodes.Caption = "Edit Nodes";
            this.bbiEditNodes.Id = 45;
            this.bbiEditNodes.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiEditNodes.ImageOptions.Image")));
            this.bbiEditNodes.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiEditNodes.ImageOptions.LargeImage")));
            this.bbiEditNodes.Name = "bbiEditNodes";
            this.bbiEditNodes.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiEditNodes_ItemClick);
            // 
            // bbiEditVisible
            // 
            this.bbiEditVisible.Caption = "Edit Visible";
            this.bbiEditVisible.Id = 46;
            this.bbiEditVisible.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiEditVisible.ImageOptions.Image")));
            this.bbiEditVisible.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiEditVisible.ImageOptions.LargeImage")));
            this.bbiEditVisible.Name = "bbiEditVisible";
            this.bbiEditVisible.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiEditVisible_ItemClick);
            // 
            // barButtonGroup1
            // 
            this.barButtonGroup1.Caption = "barButtonGroup1";
            this.barButtonGroup1.Id = 53;
            this.barButtonGroup1.Name = "barButtonGroup1";
            // 
            // WarmUpPeriod
            // 
            this.WarmUpPeriod.Caption = "Warm Up Period: ";
            this.WarmUpPeriod.Edit = this.repositoryItemComboBox2;
            this.WarmUpPeriod.Id = 55;
            this.WarmUpPeriod.Name = "WarmUpPeriod";
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            // 
            // BeiHours
            // 
            this.BeiHours.Caption = "hours: ";
            this.BeiHours.Edit = this.repositoryItemComboBox5;
            this.BeiHours.Id = 108;
            this.BeiHours.Name = "BeiHours";
            this.BeiHours.ShownEditor += new DevExpress.XtraBars.ItemClickEventHandler(this.BeiHours_ShownEditor);
            // 
            // repositoryItemComboBox5
            // 
            this.repositoryItemComboBox5.AutoHeight = false;
            this.repositoryItemComboBox5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox5.Name = "repositoryItemComboBox5";
            // 
            // BeiMinutes
            // 
            this.BeiMinutes.Caption = "Minutes: ";
            this.BeiMinutes.Edit = this.repositoryItemComboBox6;
            this.BeiMinutes.Id = 109;
            this.BeiMinutes.Name = "BeiMinutes";
            this.BeiMinutes.ShownEditor += new DevExpress.XtraBars.ItemClickEventHandler(this.BeiMinutes_ShownEditor);
            // 
            // repositoryItemComboBox6
            // 
            this.repositoryItemComboBox6.AutoHeight = false;
            this.repositoryItemComboBox6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox6.Name = "repositoryItemComboBox6";
            // 
            // BeiDays
            // 
            this.BeiDays.Caption = "Days: ";
            this.BeiDays.Edit = this.repositoryItemComboBox4;
            this.BeiDays.Id = 110;
            this.BeiDays.Name = "BeiDays";
            this.BeiDays.ShownEditor += new DevExpress.XtraBars.ItemClickEventHandler(this.BeiDays_ShownEditor);
            // 
            // repositoryItemComboBox4
            // 
            this.repositoryItemComboBox4.AutoHeight = false;
            this.repositoryItemComboBox4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox4.Name = "repositoryItemComboBox4";
            // 
            // BliWarmUpPeriod
            // 
            this.BliWarmUpPeriod.Caption = "Warm Up Period: 0(D) / 0(H) : 0(M)";
            this.BliWarmUpPeriod.Id = 111;
            this.BliWarmUpPeriod.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.BeiDays),
            new DevExpress.XtraBars.LinkPersistInfo(this.BeiHours),
            new DevExpress.XtraBars.LinkPersistInfo(this.BeiMinutes)});
            this.BliWarmUpPeriod.Name = "BliWarmUpPeriod";
            this.BliWarmUpPeriod.CloseUp += new System.EventHandler(this.BliWarmUpPeriod_CloseUp);
            // 
            // bbiSaveSnapShot
            // 
            this.bbiSaveSnapShot.Caption = "Save SnapShot";
            this.bbiSaveSnapShot.Id = 114;
            this.bbiSaveSnapShot.Name = "bbiSaveSnapShot";
            this.bbiSaveSnapShot.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSaveSnapShot_ItemClick);
            // 
            // bbiLoadSnapShot
            // 
            this.bbiLoadSnapShot.Caption = "Load SnapShot";
            this.bbiLoadSnapShot.Id = 115;
            this.bbiLoadSnapShot.Name = "bbiLoadSnapShot";
            this.bbiLoadSnapShot.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiLoadSnapShot_ItemClick);
            // 
            // State_Based_Vehicle_Tog_Switch
            // 
            this.State_Based_Vehicle_Tog_Switch.Caption = "Vehicle";
            this.State_Based_Vehicle_Tog_Switch.Id = 118;
            this.State_Based_Vehicle_Tog_Switch.Name = "State_Based_Vehicle_Tog_Switch";
            this.State_Based_Vehicle_Tog_Switch.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.State_Based_Vehicle_Tog_Switch_CheckedChanged);
            // 
            // State_Based_Equipment_Tog_Switch
            // 
            this.State_Based_Equipment_Tog_Switch.Caption = "Equipment";
            this.State_Based_Equipment_Tog_Switch.Id = 119;
            this.State_Based_Equipment_Tog_Switch.Name = "State_Based_Equipment_Tog_Switch";
            this.State_Based_Equipment_Tog_Switch.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.State_Based_Equipment_Tog_Switch_CheckedChanged);
            // 
            // State_Based_TransportLine_Tog_Switch
            // 
            this.State_Based_TransportLine_Tog_Switch.Caption = "TransportLine";
            this.State_Based_TransportLine_Tog_Switch.Id = 120;
            this.State_Based_TransportLine_Tog_Switch.Name = "State_Based_TransportLine_Tog_Switch";
            this.State_Based_TransportLine_Tog_Switch.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.State_Based_TransportLine_Tog_Switch_CheckedChanged);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "Alignment";
            this.barSubItem1.Id = 121;
            this.barSubItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barSubItem1.ImageOptions.Image")));
            this.barSubItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barSubItem1.ImageOptions.LargeImage")));
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAlignLeft),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAlignCenter),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAlignRight),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiTopAlign, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiMiddleAlign),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiBottomAlign)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // bbiAlignLeft
            // 
            this.bbiAlignLeft.Caption = "Align Left";
            this.bbiAlignLeft.Id = 122;
            this.bbiAlignLeft.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiAlignLeft.ImageOptions.Image")));
            this.bbiAlignLeft.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiAlignLeft.ImageOptions.LargeImage")));
            this.bbiAlignLeft.Name = "bbiAlignLeft";
            this.bbiAlignLeft.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAlignLeft_ItemClick);
            // 
            // bbiAlignCenter
            // 
            this.bbiAlignCenter.Caption = "Center";
            this.bbiAlignCenter.Id = 124;
            this.bbiAlignCenter.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiAlignCenter.ImageOptions.Image")));
            this.bbiAlignCenter.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiAlignCenter.ImageOptions.LargeImage")));
            this.bbiAlignCenter.Name = "bbiAlignCenter";
            this.bbiAlignCenter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAlignCenter_ItemClick);
            // 
            // bbiAlignRight
            // 
            this.bbiAlignRight.Caption = "Align Right";
            this.bbiAlignRight.Id = 129;
            this.bbiAlignRight.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiAlignRight.ImageOptions.Image")));
            this.bbiAlignRight.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiAlignRight.ImageOptions.LargeImage")));
            this.bbiAlignRight.Name = "bbiAlignRight";
            this.bbiAlignRight.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAlignRight_ItemClick);
            // 
            // bbiTopAlign
            // 
            this.bbiTopAlign.Caption = "Top Align";
            this.bbiTopAlign.Id = 126;
            this.bbiTopAlign.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiTopAlign.ImageOptions.Image")));
            this.bbiTopAlign.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiTopAlign.ImageOptions.LargeImage")));
            this.bbiTopAlign.Name = "bbiTopAlign";
            this.bbiTopAlign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiTopAlign_ItemClick);
            // 
            // bbiMiddleAlign
            // 
            this.bbiMiddleAlign.Caption = "Middle Align";
            this.bbiMiddleAlign.Id = 130;
            this.bbiMiddleAlign.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiMiddleAlign.ImageOptions.Image")));
            this.bbiMiddleAlign.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiMiddleAlign.ImageOptions.LargeImage")));
            this.bbiMiddleAlign.Name = "bbiMiddleAlign";
            this.bbiMiddleAlign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiMiddleAlign_ItemClick);
            // 
            // bbiBottomAlign
            // 
            this.bbiBottomAlign.Caption = "Bottom Align";
            this.bbiBottomAlign.Id = 128;
            this.bbiBottomAlign.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiBottomAlign.ImageOptions.Image")));
            this.bbiBottomAlign.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiBottomAlign.ImageOptions.LargeImage")));
            this.bbiBottomAlign.Name = "bbiBottomAlign";
            this.bbiBottomAlign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiBottomAlign_ItemClick);
            // 
            // RB_FIRST
            // 
            this.RB_FIRST.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.RB_PG_FILES,
            this.RB_PG_SETTINGS,
            this.rpgProduction,
            this.ribbonPageGroupEdit,
            this.rpgConvert});
            this.RB_FIRST.Name = "RB_FIRST";
            this.RB_FIRST.Text = "Modeling";
            // 
            // RB_PG_FILES
            // 
            this.RB_PG_FILES.ItemLinks.Add(this.RB_BTN_NEW);
            this.RB_PG_FILES.ItemLinks.Add(this.RB_BTN_LOAD);
            this.RB_PG_FILES.ItemLinks.Add(this.RB_BTN_SAVE);
            this.RB_PG_FILES.ItemLinks.Add(this.RB_BTN_SAVE_AS);
            this.RB_PG_FILES.Name = "RB_PG_FILES";
            this.RB_PG_FILES.Text = "Files";
            // 
            // RB_PG_SETTINGS
            // 
            this.RB_PG_SETTINGS.ItemLinks.Add(this.RB_BTN_FLOORSETUP);
            this.RB_PG_SETTINGS.Name = "RB_PG_SETTINGS";
            this.RB_PG_SETTINGS.Text = "Floor";
            // 
            // rpgProduction
            // 
            this.rpgProduction.ItemLinks.Add(this.bbiModelingProductionData);
            this.rpgProduction.ItemLinks.Add(this.bbiSettingSteps);
            this.rpgProduction.ItemLinks.Add(this.bbiEditEquipment);
            this.rpgProduction.ItemLinks.Add(this.bbiEditBreakDown);
            this.rpgProduction.Name = "rpgProduction";
            this.rpgProduction.Text = "Production";
            // 
            // ribbonPageGroupEdit
            // 
            this.ribbonPageGroupEdit.ItemLinks.Add(this.bbiEditScript);
            this.ribbonPageGroupEdit.Name = "ribbonPageGroupEdit";
            this.ribbonPageGroupEdit.Text = "Logic";
            // 
            // rpgConvert
            // 
            this.rpgConvert.ItemLinks.Add(this.barButtonItemImPortModel);
            this.rpgConvert.ItemLinks.Add(this.bbiEditNodes);
            this.rpgConvert.ItemLinks.Add(this.bbiEditVisible);
            this.rpgConvert.ItemLinks.Add(this.barSubItem1);
            this.rpgConvert.Name = "rpgConvert";
            this.rpgConvert.Text = "Model";
            // 
            // ribbonPageSimulation
            // 
            this.ribbonPageSimulation.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgAnimation,
            this.rpgSimulationExecution,
            this.rpgTimeCondition,
            this.rpgSnapShot,
            this.rpgStateBasedColors});
            this.ribbonPageSimulation.Name = "ribbonPageSimulation";
            this.ribbonPageSimulation.Text = "Simulation";
            // 
            // rpgAnimation
            // 
            this.rpgAnimation.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            this.rpgAnimation.ItemLinks.Add(this.Animation_Tog_Switch);
            this.rpgAnimation.ItemLinks.Add(this.beiAnimationSpeed);
            this.rpgAnimation.ItemLinks.Add(this.AnimationSpeedTrack);
            this.rpgAnimation.ItemLinks.Add(this.Background_Color_Tog_Switch);
            this.rpgAnimation.ItemLinks.Add(this.Auto_Focusing_Tog_Switch);
            this.rpgAnimation.Name = "rpgAnimation";
            this.rpgAnimation.Text = "Animation";
            // 
            // rpgSimulationExecution
            // 
            this.rpgSimulationExecution.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            this.rpgSimulationExecution.ItemLinks.Add(this.bbiSimRun, true);
            this.rpgSimulationExecution.ItemLinks.Add(this.bbiSimPause);
            this.rpgSimulationExecution.ItemLinks.Add(this.bbiSimStop);
            this.rpgSimulationExecution.ItemLinks.Add(this.bbiSimReservePause);
            this.rpgSimulationExecution.Name = "rpgSimulationExecution";
            this.rpgSimulationExecution.Text = "Simulation Execution";
            // 
            // bbiSimRun
            // 
            this.bbiSimRun.Caption = "Run";
            this.bbiSimRun.Id = 50;
            this.bbiSimRun.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiSimRun.ImageOptions.Image")));
            this.bbiSimRun.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiSimRun.ImageOptions.LargeImage")));
            this.bbiSimRun.Name = "bbiSimRun";
            this.bbiSimRun.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSimRun_ItemClick);
            // 
            // rpgTimeCondition
            // 
            this.rpgTimeCondition.ItemLinks.Add(this.beiSimStartTimeSetting);
            this.rpgTimeCondition.ItemLinks.Add(this.beiSimEndTimeSetting);
            this.rpgTimeCondition.ItemLinks.Add(this.beiCurrentSimTimeView);
            this.rpgTimeCondition.ItemLinks.Add(this.beSimulationAcceleration);
            this.rpgTimeCondition.ItemLinks.Add(this.BliWarmUpPeriod);
            this.rpgTimeCondition.Name = "rpgTimeCondition";
            this.rpgTimeCondition.Text = "Time Condition";
            // 
            // rpgSnapShot
            // 
            this.rpgSnapShot.ItemLinks.Add(this.bbiSaveSnapShot);
            this.rpgSnapShot.ItemLinks.Add(this.bbiLoadSnapShot);
            this.rpgSnapShot.Name = "rpgSnapShot";
            this.rpgSnapShot.Text = "SnapShot";
            // 
            // rpgStateBasedColors
            // 
            this.rpgStateBasedColors.ItemLinks.Add(this.State_Based_Vehicle_Tog_Switch);
            this.rpgStateBasedColors.ItemLinks.Add(this.State_Based_Equipment_Tog_Switch);
            this.rpgStateBasedColors.ItemLinks.Add(this.State_Based_TransportLine_Tog_Switch);
            this.rpgStateBasedColors.Name = "rpgStateBasedColors";
            this.rpgStateBasedColors.Text = "State-Based Colors";
            // 
            // rpAnalysis
            // 
            this.rpAnalysis.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgReport});
            this.rpAnalysis.Name = "rpAnalysis";
            this.rpAnalysis.Text = "Analysis";
            // 
            // rpgReport
            // 
            this.rpgReport.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            this.rpgReport.ItemLinks.Add(this.bbiProductionReport);
            this.rpgReport.ItemLinks.Add(this.bbiAMHSReport);
            this.rpgReport.ItemLinks.Add(this.bbiLoadProductionReport);
            this.rpgReport.ItemLinks.Add(this.bbiLoadAMHSReport);
            this.rpgReport.Name = "rpgReport";
            this.rpgReport.Text = "Report";
            // 
            // rpWindow
            // 
            this.rpWindow.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2});
            this.rpWindow.Name = "rpWindow";
            this.rpWindow.Text = "Window";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiDockInsertNode);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiDockInsertCoupledModel);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiDockNodes);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiDockPart);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiDockLineStatus);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiDockLineStatusDetail);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiDockObjectProperties);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Dockable Windows";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // repositoryItemTimeEdit2
            // 
            this.repositoryItemTimeEdit2.AutoHeight = false;
            this.repositoryItemTimeEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEdit2.Name = "repositoryItemTimeEdit2";
            // 
            // repositoryItemComboBox3
            // 
            this.repositoryItemComboBox3.AutoHeight = false;
            this.repositoryItemComboBox3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox3.Name = "repositoryItemComboBox3";
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // dockPanelLineStatusDetail
            // 
            this.dockPanelLineStatusDetail.Controls.Add(this.dockPanelLineStatusDetail_Container);
            this.dockPanelLineStatusDetail.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanelLineStatusDetail.FloatVertical = true;
            this.dockPanelLineStatusDetail.ID = new System.Guid("3cbb586e-c67a-4c99-b910-5ba53ec4aaea");
            this.dockPanelLineStatusDetail.Location = new System.Drawing.Point(399, 495);
            this.dockPanelLineStatusDetail.Name = "dockPanelLineStatusDetail";
            this.dockPanelLineStatusDetail.OriginalSize = new System.Drawing.Size(200, 247);
            this.dockPanelLineStatusDetail.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanelLineStatusDetail.SavedIndex = 0;
            this.dockPanelLineStatusDetail.SavedSizeFactor = 1.19197D;
            this.dockPanelLineStatusDetail.Size = new System.Drawing.Size(879, 247);
            this.dockPanelLineStatusDetail.Text = "Line Status Details";
            this.dockPanelLineStatusDetail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            this.dockPanelLineStatusDetail.ClosingPanel += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(this.dockPanelLineStatusDetail_ClosingPanel);
            // 
            // dockPanelLineStatusDetail_Container
            // 
            this.dockPanelLineStatusDetail_Container.Controls.Add(this.splitContainerControl1);
            this.dockPanelLineStatusDetail_Container.Location = new System.Drawing.Point(0, 44);
            this.dockPanelLineStatusDetail_Container.Name = "dockPanelLineStatusDetail_Container";
            this.dockPanelLineStatusDetail_Container.Size = new System.Drawing.Size(879, 203);
            this.dockPanelLineStatusDetail_Container.TabIndex = 0;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.lbCount);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelAutoRefresh);
            this.splitContainerControl1.Panel1.Controls.Add(this.cbTree_Interval);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControlLineStatusDetail);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(879, 203);
            this.splitContainerControl1.SplitterPosition = 22;
            this.splitContainerControl1.TabIndex = 5;
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbCount.Location = new System.Drawing.Point(635, 0);
            this.lbCount.Name = "lbCount";
            this.lbCount.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lbCount.Size = new System.Drawing.Size(56, 16);
            this.lbCount.TabIndex = 4;
            this.lbCount.Text = "0 Counts";
            // 
            // labelAutoRefresh
            // 
            this.labelAutoRefresh.AutoSize = true;
            this.labelAutoRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelAutoRefresh.Location = new System.Drawing.Point(691, 0);
            this.labelAutoRefresh.Name = "labelAutoRefresh";
            this.labelAutoRefresh.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.labelAutoRefresh.Size = new System.Drawing.Size(87, 16);
            this.labelAutoRefresh.TabIndex = 3;
            this.labelAutoRefresh.Text = "Auto Refresh :";
            // 
            // cbTree_Interval
            // 
            this.cbTree_Interval.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbTree_Interval.Location = new System.Drawing.Point(778, 0);
            this.cbTree_Interval.MenuManager = this.ribbonControl1;
            this.cbTree_Interval.Name = "cbTree_Interval";
            this.cbTree_Interval.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTree_Interval.Properties.Items.AddRange(new object[] {
            "OFF",
            "10",
            "20",
            "30",
            "40",
            "50",
            "60"});
            this.cbTree_Interval.Size = new System.Drawing.Size(101, 20);
            this.cbTree_Interval.TabIndex = 2;
            this.cbTree_Interval.SelectedIndexChanged += new System.EventHandler(this.cbTree_Interval_SelectedIndexChanged);

            // 
            // gridControlPart
            // 
            this.gridControlPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlPart.Location = new System.Drawing.Point(0, 0);
            this.gridControlPart.MainView = this.gridViewPart;
            this.gridControlPart.MenuManager = this.ribbonControl1;
            this.gridControlPart.Name = "gridControlPart";
            this.gridControlPart.Size = new System.Drawing.Size(879, 171);
            this.gridControlPart.TabIndex = 0;
            this.gridControlPart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPart});
            // 
            // gridViewPart
            // 
            this.gridViewPart.GridControl = this.gridControlPart;
            this.gridViewPart.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewPart.Name = "gridViewPart";
            this.gridViewPart.OptionsBehavior.Editable = false;
            this.gridViewPart.OptionsView.ColumnAutoWidth = false;
            this.gridViewPart.OptionsView.ShowGroupPanel = false;
            this.gridViewPart.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
            this.gridViewPart.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewPart.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GridViewPart_FocusedRowChanged);
            this.gridViewPart.MouseUp += gridViewPart_MouseClick;
            // 
            // gridControlLineStatusDetail
            // 
            this.gridControlLineStatusDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlLineStatusDetail.Location = new System.Drawing.Point(0, 0);
            this.gridControlLineStatusDetail.MainView = this.gridViewLineStatusDetail;
            this.gridControlLineStatusDetail.MenuManager = this.ribbonControl1;
            this.gridControlLineStatusDetail.Name = "gridControlLineStatusDetail";
            this.gridControlLineStatusDetail.Size = new System.Drawing.Size(879, 171);
            this.gridControlLineStatusDetail.TabIndex = 0;
            this.gridControlLineStatusDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLineStatusDetail});
            // 
            // gridViewLineStatusDetail
            // 
            this.gridViewLineStatusDetail.GridControl = this.gridControlLineStatusDetail;
            this.gridViewLineStatusDetail.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewLineStatusDetail.Name = "gridViewLineStatusDetail";
            this.gridViewLineStatusDetail.OptionsBehavior.Editable = false;
            this.gridViewLineStatusDetail.OptionsView.ColumnAutoWidth = false;
            this.gridViewLineStatusDetail.OptionsView.ShowGroupPanel = false;
            this.gridViewLineStatusDetail.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
            this.gridViewLineStatusDetail.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewLineStatusDetail.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.ListGridView_FocusedRowChanged);
            this.gridViewLineStatusDetail.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ListGridView_gridControl_MouseUp);
            // 
            // dockPanelSimNodeProperties
            // 
            this.dockPanelSimNodeProperties.Controls.Add(this.controlContainerSimNode);
            this.dockPanelSimNodeProperties.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanelSimNodeProperties.ID = new System.Guid("b33c68f8-9969-44c7-84bf-894023a61471");
            this.dockPanelSimNodeProperties.Location = new System.Drawing.Point(1278, 126);
            this.dockPanelSimNodeProperties.Name = "dockPanelSimNodeProperties";
            this.dockPanelSimNodeProperties.OriginalSize = new System.Drawing.Size(399, 542);
            this.dockPanelSimNodeProperties.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanelSimNodeProperties.SavedIndex = 0;
            this.dockPanelSimNodeProperties.Size = new System.Drawing.Size(399, 616);
            this.dockPanelSimNodeProperties.Text = "Object Properties";
            this.dockPanelSimNodeProperties.ClosingPanel += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(this.dockPanelSimNodeProperties_ClosingPanel);
            // 
            // controlContainerSimNode
            // 
            this.controlContainerSimNode.Controls.Add(this.propertyGridControlSimObject);
            this.controlContainerSimNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlContainerSimNode.Location = new System.Drawing.Point(1, 43);
            this.controlContainerSimNode.Name = "controlContainerSimNode";
            this.controlContainerSimNode.Size = new System.Drawing.Size(398, 573);
            this.controlContainerSimNode.TabIndex = 0;
            // 
            // propertyGridControlSimObject
            // 
            this.propertyGridControlSimObject.Cursor = System.Windows.Forms.Cursors.Default;
            this.propertyGridControlSimObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControlSimObject.Location = new System.Drawing.Point(0, 0);
            this.propertyGridControlSimObject.MenuManager = this.ribbonControl1;
            this.propertyGridControlSimObject.Name = "propertyGridControlSimObject";
            this.propertyGridControlSimObject.Size = new System.Drawing.Size(398, 573);
            this.propertyGridControlSimObject.TabIndex = 4;
            this.propertyGridControlSimObject.CellValueChanging += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(this.propertyGridControlSimNode_CellValueChanging);
            this.propertyGridControlSimObject.CellValueChanged += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(this.propertyGridControlSimNode_CellValueChanged);
            // 
            // panelContainer1
            // 
            this.panelContainer1.ActiveChild = this.dockPanelInsertedSimNodes;
            this.panelContainer1.Controls.Add(this.dockPanelInsertRefNode);
            this.panelContainer1.Controls.Add(this.dockPanelInsertCoupledModel);
            this.panelContainer1.Controls.Add(this.dockPanelInsertedSimNodes);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.panelContainer1.ID = new System.Guid("90187d60-b140-44dd-aaf1-3bb461d0f599");
            this.panelContainer1.Location = new System.Drawing.Point(0, 126);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(399, 606);
            this.panelContainer1.Size = new System.Drawing.Size(399, 616);
            this.panelContainer1.Tabbed = true;
            this.panelContainer1.Text = "panelContainer1";
            // 
            // dockPanelInsertedSimNodes
            // 
            this.dockPanelInsertedSimNodes.Controls.Add(this.dockPanelCoupledModels_Container);
            this.dockPanelInsertedSimNodes.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelInsertedSimNodes.ID = new System.Guid("d1e012a0-1d47-49ab-a898-17c1e8bf03ce");
            this.dockPanelInsertedSimNodes.Location = new System.Drawing.Point(0, 43);
            this.dockPanelInsertedSimNodes.Name = "dockPanelInsertedSimNodes";
            this.dockPanelInsertedSimNodes.OriginalSize = new System.Drawing.Size(398, 564);
            this.dockPanelInsertedSimNodes.Size = new System.Drawing.Size(398, 542);
            this.dockPanelInsertedSimNodes.Text = "Nodes";
            this.dockPanelInsertedSimNodes.ClosingPanel += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(this.dockPanelInsertedSimNodes_ClosingPanel);
            // 
            // dockPanelCoupledModels_Container
            // 
            this.dockPanelCoupledModels_Container.Controls.Add(this.layoutControlInsertedSimNodes);
            this.dockPanelCoupledModels_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanelCoupledModels_Container.Name = "dockPanelCoupledModels_Container";
            this.dockPanelCoupledModels_Container.Size = new System.Drawing.Size(398, 542);
            this.dockPanelCoupledModels_Container.TabIndex = 0;
            // 
            // layoutControlInsertedSimNodes
            // 
            this.layoutControlInsertedSimNodes.Controls.Add(this.simNodeTreeList);
            this.layoutControlInsertedSimNodes.Controls.Add(this.simpleButtonRemoveTreeNode);
            this.layoutControlInsertedSimNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlInsertedSimNodes.Location = new System.Drawing.Point(0, 0);
            this.layoutControlInsertedSimNodes.Name = "layoutControlInsertedSimNodes";
            this.layoutControlInsertedSimNodes.Root = this.layoutControlGroupInsertedSimNodes;
            this.layoutControlInsertedSimNodes.Size = new System.Drawing.Size(398, 542);
            this.layoutControlInsertedSimNodes.TabIndex = 1;
            this.layoutControlInsertedSimNodes.Text = "layoutControlInsertedCoupledModels";
            // 
            // simNodeTreeList
            // 
            this.simNodeTreeList.Location = new System.Drawing.Point(12, 12);
            this.simNodeTreeList.Model = null;
            this.simNodeTreeList.Name = "simNodeTreeList";
            this.simNodeTreeList.OptionsBehavior.Editable = false;
            this.simNodeTreeList.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Multiple;
            this.simNodeTreeList.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            this.simNodeTreeList.Size = new System.Drawing.Size(374, 491);
            this.simNodeTreeList.TabIndex = 2;
            // 
            // simpleButtonRemoveTreeNode
            // 
            this.simpleButtonRemoveTreeNode.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonRemoveTreeNode.Appearance.Options.UseFont = true;
            this.simpleButtonRemoveTreeNode.Location = new System.Drawing.Point(12, 507);
            this.simpleButtonRemoveTreeNode.Name = "simpleButtonRemoveTreeNode";
            this.simpleButtonRemoveTreeNode.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.simpleButtonRemoveTreeNode.Size = new System.Drawing.Size(374, 23);
            this.simpleButtonRemoveTreeNode.StyleController = this.layoutControlInsertedSimNodes;
            this.simpleButtonRemoveTreeNode.TabIndex = 9;
            this.simpleButtonRemoveTreeNode.Text = "Remove";
            this.simpleButtonRemoveTreeNode.Click += new System.EventHandler(this.simpleButtonRemoveTreeNode_Click);
            // 
            // layoutControlGroupInsertedSimNodes
            // 
            this.layoutControlGroupInsertedSimNodes.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupInsertedSimNodes.GroupBordersVisible = false;
            this.layoutControlGroupInsertedSimNodes.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemInsertedSimNodes,
            this.layoutControlItemRemoveSimNode});
            this.layoutControlGroupInsertedSimNodes.Name = "layoutControlGroupInsertedCoupledModels";
            this.layoutControlGroupInsertedSimNodes.Size = new System.Drawing.Size(398, 542);
            this.layoutControlGroupInsertedSimNodes.TextVisible = false;
            // 
            // layoutControlItemInsertedSimNodes
            // 
            this.layoutControlItemInsertedSimNodes.Control = this.simNodeTreeList;
            this.layoutControlItemInsertedSimNodes.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemInsertedSimNodes.Name = "layoutControlItemInsertedCoupledModels";
            this.layoutControlItemInsertedSimNodes.Size = new System.Drawing.Size(378, 495);
            this.layoutControlItemInsertedSimNodes.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemInsertedSimNodes.TextVisible = false;
            // 
            // layoutControlItemRemoveSimNode
            // 
            this.layoutControlItemRemoveSimNode.Control = this.simpleButtonRemoveTreeNode;
            this.layoutControlItemRemoveSimNode.ControlAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.layoutControlItemRemoveSimNode.Location = new System.Drawing.Point(0, 495);
            this.layoutControlItemRemoveSimNode.Name = "layoutControlItemRemoveCoupledModels";
            this.layoutControlItemRemoveSimNode.Size = new System.Drawing.Size(378, 27);
            this.layoutControlItemRemoveSimNode.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemRemoveSimNode.TextVisible = false;
            // 
            // dockPanelInsertRefNode
            // 
            this.dockPanelInsertRefNode.Controls.Add(this.dockPanelInsertRefNode_Container);
            this.dockPanelInsertRefNode.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelInsertRefNode.ID = new System.Guid("0c7163d4-c74f-4fe5-8565-dbca7688fdc7");
            this.dockPanelInsertRefNode.Location = new System.Drawing.Point(0, 43);
            this.dockPanelInsertRefNode.Name = "dockPanelInsertRefNode";
            this.dockPanelInsertRefNode.OriginalSize = new System.Drawing.Size(398, 564);
            this.dockPanelInsertRefNode.Size = new System.Drawing.Size(398, 542);
            this.dockPanelInsertRefNode.Text = "Insert Node";
            this.dockPanelInsertRefNode.ClosingPanel += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(this.dockPanelInsertRefNode_ClosingPanel);
            // 
            // dockPanelInsertRefNode_Container
            // 
            this.dockPanelInsertRefNode_Container.Controls.Add(this.layoutControlInsertNode);
            this.dockPanelInsertRefNode_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanelInsertRefNode_Container.Name = "dockPanelInsertRefNode_Container";
            this.dockPanelInsertRefNode_Container.Size = new System.Drawing.Size(398, 542);
            this.dockPanelInsertRefNode_Container.TabIndex = 0;
            // 
            // layoutControlInsertNode
            // 
            this.layoutControlInsertNode.Controls.Add(this.gridControlInsertRefNode);
            this.layoutControlInsertNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlInsertNode.Location = new System.Drawing.Point(0, 0);
            this.layoutControlInsertNode.Name = "layoutControlInsertNode";
            this.layoutControlInsertNode.Root = this.layoutControlGroupInsertNode;
            this.layoutControlInsertNode.Size = new System.Drawing.Size(398, 542);
            this.layoutControlInsertNode.TabIndex = 1;
            this.layoutControlInsertNode.Text = "layoutControlInsertNode";
            // 
            // gridControlInsertRefNode
            // 
            this.gridControlInsertRefNode.Location = new System.Drawing.Point(12, 12);
            this.gridControlInsertRefNode.MainView = this.gridViewInsertRefNode;
            this.gridControlInsertRefNode.MenuManager = this.ribbonControl1;
            this.gridControlInsertRefNode.Name = "gridControlInsertRefNode";
            this.gridControlInsertRefNode.Size = new System.Drawing.Size(374, 518);
            this.gridControlInsertRefNode.TabIndex = 0;
            this.gridControlInsertRefNode.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewInsertRefNode});
            // 
            // gridViewInsertRefNode
            // 
            this.gridViewInsertRefNode.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridViewInsertRefNode.GridControl = this.gridControlInsertRefNode;
            this.gridViewInsertRefNode.Name = "gridViewInsertRefNode";
            this.gridViewInsertRefNode.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewInsertNode_RowClick);
            this.gridViewInsertRefNode.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewInsertRefNode_FocusedRowChanged);
            this.gridViewInsertRefNode.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewInsertRefNode_CellValueChanged);
            this.gridViewInsertRefNode.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewInsertRefNode_CellValueChanging); 
            this.gridViewInsertRefNode.Click += new System.EventHandler(this.gridViewInsertRefNode_Click);
            // 
            // layoutControlGroupInsertNode
            // 
            this.layoutControlGroupInsertNode.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupInsertNode.GroupBordersVisible = false;
            this.layoutControlGroupInsertNode.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemInsertNode});
            this.layoutControlGroupInsertNode.Name = "layoutControlGroup1";
            this.layoutControlGroupInsertNode.Size = new System.Drawing.Size(398, 542);
            this.layoutControlGroupInsertNode.TextVisible = false;
            // 
            // layoutControlItemInsertNode
            // 
            this.layoutControlItemInsertNode.Control = this.gridControlInsertRefNode;
            this.layoutControlItemInsertNode.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemInsertNode.Name = "layoutControlItem3";
            this.layoutControlItemInsertNode.Size = new System.Drawing.Size(378, 522);
            this.layoutControlItemInsertNode.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemInsertNode.TextVisible = false;
            // 
            // dockPanelInsertCoupledModel
            // 
            this.dockPanelInsertCoupledModel.Controls.Add(this.dockPanelInsertCoupledModel_Container);
            this.dockPanelInsertCoupledModel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelInsertCoupledModel.FloatVertical = true;
            this.dockPanelInsertCoupledModel.ID = new System.Guid("0c7163d4-c74f-4fe5-8565-dbca7688fdc7");
            this.dockPanelInsertCoupledModel.Location = new System.Drawing.Point(0, 43);
            this.dockPanelInsertCoupledModel.Name = "dockPanelInsertCoupledModel";
            this.dockPanelInsertCoupledModel.OriginalSize = new System.Drawing.Size(398, 564);
            this.dockPanelInsertCoupledModel.Size = new System.Drawing.Size(398, 542);
            this.dockPanelInsertCoupledModel.Text = "Insert Coupled Model";
            this.dockPanelInsertCoupledModel.ClosingPanel += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(this.dockPanelInsertCoupledModel_ClosingPanel);
            // 
            // dockPanelInsertCoupledModel_Container
            // 
            this.dockPanelInsertCoupledModel_Container.Controls.Add(this.layoutControlInsertCoupledModel);
            this.dockPanelInsertCoupledModel_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanelInsertCoupledModel_Container.Name = "dockPanelInsertCoupledModel_Container";
            this.dockPanelInsertCoupledModel_Container.Size = new System.Drawing.Size(398, 542);
            this.dockPanelInsertCoupledModel_Container.TabIndex = 0;
            // 
            // layoutControlInsertCoupledModel
            // 
            this.layoutControlInsertCoupledModel.Controls.Add(this.gridControlInsertCoupledModel);
            this.layoutControlInsertCoupledModel.Controls.Add(this.simpleButtonAddCoupledModel);
            this.layoutControlInsertCoupledModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlInsertCoupledModel.Location = new System.Drawing.Point(0, 0);
            this.layoutControlInsertCoupledModel.Name = "layoutControlInsertCoupledModel";
            this.layoutControlInsertCoupledModel.Root = this.layoutControlGroupInsertCoupledModel;
            this.layoutControlInsertCoupledModel.Size = new System.Drawing.Size(398, 542);
            this.layoutControlInsertCoupledModel.TabIndex = 1;
            this.layoutControlInsertCoupledModel.Text = "layoutControlInsertCoupledModel";
            // 
            // gridControlInsertCoupledModel
            // 
            this.gridControlInsertCoupledModel.Location = new System.Drawing.Point(12, 12);
            this.gridControlInsertCoupledModel.MainView = this.gridViewInsertCoupledModel;
            this.gridControlInsertCoupledModel.MenuManager = this.ribbonControl1;
            this.gridControlInsertCoupledModel.Name = "gridControlInsertCoupledModel";
            this.gridControlInsertCoupledModel.Size = new System.Drawing.Size(374, 491);
            this.gridControlInsertCoupledModel.TabIndex = 0;
            this.gridControlInsertCoupledModel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewInsertCoupledModel});
            // 
            // gridViewInsertCoupledModel
            // 
            this.gridViewInsertCoupledModel.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridViewInsertCoupledModel.GridControl = this.gridControlInsertCoupledModel;
            this.gridViewInsertCoupledModel.Name = "gridViewInsertCoupledModel";
            this.gridViewInsertCoupledModel.OptionsBehavior.Editable = false;
            this.gridViewInsertCoupledModel.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewInsertCoupledModel_RowClick);
            // 
            // simpleButtonAddCoupledModel
            // 
            this.simpleButtonAddCoupledModel.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonAddCoupledModel.Appearance.Options.UseFont = true;
            this.simpleButtonAddCoupledModel.Location = new System.Drawing.Point(12, 507);
            this.simpleButtonAddCoupledModel.Name = "simpleButtonAddCoupledModel";
            this.simpleButtonAddCoupledModel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.simpleButtonAddCoupledModel.Size = new System.Drawing.Size(374, 23);
            this.simpleButtonAddCoupledModel.StyleController = this.layoutControlInsertCoupledModel;
            this.simpleButtonAddCoupledModel.TabIndex = 9;
            this.simpleButtonAddCoupledModel.Text = "Add";
            this.simpleButtonAddCoupledModel.Click += new System.EventHandler(this.SimpleButtonAddCoupledModel_Click);
            // 
            // layoutControlGroupInsertCoupledModel
            // 
            this.layoutControlGroupInsertCoupledModel.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupInsertCoupledModel.GroupBordersVisible = false;
            this.layoutControlGroupInsertCoupledModel.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemInsertCoupledModel,
            this.layoutControlItemAddCoupledModel});
            this.layoutControlGroupInsertCoupledModel.Name = "layoutControlGroupCoupledModel";
            this.layoutControlGroupInsertCoupledModel.Size = new System.Drawing.Size(398, 542);
            this.layoutControlGroupInsertCoupledModel.TextVisible = false;
            // 
            // layoutControlItemInsertCoupledModel
            // 
            this.layoutControlItemInsertCoupledModel.Control = this.gridControlInsertCoupledModel;
            this.layoutControlItemInsertCoupledModel.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemInsertCoupledModel.Name = "layoutControlItemCoupledModel";
            this.layoutControlItemInsertCoupledModel.Size = new System.Drawing.Size(378, 495);
            this.layoutControlItemInsertCoupledModel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemInsertCoupledModel.TextVisible = false;
            // 
            // layoutControlItemAddCoupledModel
            // 
            this.layoutControlItemAddCoupledModel.Control = this.simpleButtonAddCoupledModel;
            this.layoutControlItemAddCoupledModel.ControlAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.layoutControlItemAddCoupledModel.Location = new System.Drawing.Point(0, 495);
            this.layoutControlItemAddCoupledModel.Name = "layoutControlItemAddCoupledModel";
            this.layoutControlItemAddCoupledModel.Size = new System.Drawing.Size(378, 27);
            this.layoutControlItemAddCoupledModel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemAddCoupledModel.TextVisible = false;
            // 
            // dockPanelMainScreen
            // 
            this.dockPanelMainScreen.Controls.Add(this.dockPanel1_Container);
            this.dockPanelMainScreen.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelMainScreen.ID = new System.Guid("ce868525-6e4a-4442-9e98-beae7d7d5532");
            this.dockPanelMainScreen.Location = new System.Drawing.Point(399, 126);
            this.dockPanelMainScreen.Name = "dockPanelMainScreen";
            this.dockPanelMainScreen.Options.ShowCloseButton = false;
            this.dockPanelMainScreen.OriginalSize = new System.Drawing.Size(879, 200);
            this.dockPanelMainScreen.Size = new System.Drawing.Size(879, 616);
            this.dockPanelMainScreen.Text = "Main Screen";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Location = new System.Drawing.Point(0, 43);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(879, 573);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // repositoryItemTimeEdit5
            // 
            this.repositoryItemTimeEdit5.AutoHeight = false;
            this.repositoryItemTimeEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEdit5.Mask.EditMask = "dd 일 HH 시간 mm 분";
            this.repositoryItemTimeEdit5.Name = "repositoryItemTimeEdit5";
            // 
            // dockPanelInsertNodeTree
            // 
            this.dockPanelInsertNodeTree.Controls.Add(this.controlContainer1);
            this.dockPanelInsertNodeTree.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelInsertNodeTree.FloatVertical = true;
            this.dockPanelInsertNodeTree.ID = new System.Guid("df2cb870-7083-4f4e-a19c-2f6c24797fef");
            this.dockPanelInsertNodeTree.Location = new System.Drawing.Point(0, 43);
            this.dockPanelInsertNodeTree.Name = "dockPanelInsertNodeTree";
            this.dockPanelInsertNodeTree.OriginalSize = new System.Drawing.Size(398, 542);
            this.dockPanelInsertNodeTree.Size = new System.Drawing.Size(398, 542);
            this.dockPanelInsertNodeTree.Text = "Insert Node Tree";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.layoutControl1);
            this.controlContainer1.Location = new System.Drawing.Point(0, 0);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(398, 542);
            this.controlContainer1.TabIndex = 0;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.treeListInsertNodeTree);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2621, 546, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroupInsertNodeTree;
            this.layoutControl1.Size = new System.Drawing.Size(398, 542);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // treeListInsertNodeTree
            // 
            this.treeListInsertNodeTree.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListRefTypeCol,
            this.treeListNodeTypeCol,
            this.treeListHeightCol});
            this.treeListInsertNodeTree.Location = new System.Drawing.Point(12, 12);
            this.treeListInsertNodeTree.MenuManager = this.ribbonControl1;
            this.treeListInsertNodeTree.Name = "treeListInsertNodeTree";
            this.treeListInsertNodeTree.Size = new System.Drawing.Size(374, 518);
            this.treeListInsertNodeTree.StateImageList = this.imageCollection1;
            this.treeListInsertNodeTree.TabIndex = 4;
            this.treeListInsertNodeTree.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeListInsertNodeTree_GetStateImage);
            this.treeListInsertNodeTree.RowClick += new DevExpress.XtraTreeList.RowClickEventHandler(this.treeListInsertNodeTree_RowClick);
            this.treeListInsertNodeTree.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListInsertNodeTree_FocusedNodeChanged);
            this.treeListInsertNodeTree.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeListInsertNodeTree_CellValueChanged);
            // 
            // treeListRefTypeCol
            // 
            this.treeListRefTypeCol.Caption = "RefType";
            this.treeListRefTypeCol.FieldName = "RefType";
            this.treeListRefTypeCol.Name = "treeListRefTypeCol";
            this.treeListRefTypeCol.Visible = true;
            this.treeListRefTypeCol.VisibleIndex = 0;
            // 
            // treeListNodeTypeCol
            // 
            this.treeListNodeTypeCol.Caption = "NodeType";
            this.treeListNodeTypeCol.FieldName = "NodeType";
            this.treeListNodeTypeCol.Name = "treeListNodeTypeCol";
            this.treeListNodeTypeCol.Visible = true;
            this.treeListNodeTypeCol.VisibleIndex = 1;
            // 
            // treeListHeightCol
            // 
            this.treeListHeightCol.Caption = "Height";
            this.treeListHeightCol.FieldName = "Height";
            this.treeListHeightCol.Name = "treeListHeightCol";
            this.treeListHeightCol.Visible = true;
            this.treeListHeightCol.VisibleIndex = 2;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.InsertGalleryImage("open_32x32.png", "images/actions/open_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/open_32x32.png"), 0);
            this.imageCollection1.Images.SetKeyName(0, "open_32x32.png");
            this.imageCollection1.InsertGalleryImage("csharp_32x32.png", "images/programming/csharp_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/programming/csharp_32x32.png"), 1);
            this.imageCollection1.Images.SetKeyName(1, "csharp_32x32.png");
            this.imageCollection1.InsertGalleryImage("ide_32x32.png", "images/programming/ide_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/programming/ide_32x32.png"), 2);
            this.imageCollection1.Images.SetKeyName(2, "ide_32x32.png");
            this.imageCollection1.InsertGalleryImage("bofolder_32x32.png", "images/business%20objects/bofolder_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/business%20objects/bofolder_32x32.png"), 3);
            this.imageCollection1.Images.SetKeyName(3, "bofolder_32x32.png");
            this.imageCollection1.InsertGalleryImage("build_32x32.png", "images/programming/build_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/programming/build_32x32.png"), 4);
            this.imageCollection1.Images.SetKeyName(4, "build_32x32.png");
            // 
            // layoutControlGroupInsertNodeTree
            // 
            this.layoutControlGroupInsertNodeTree.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupInsertNodeTree.GroupBordersVisible = false;
            this.layoutControlGroupInsertNodeTree.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroupInsertNodeTree.Name = "layoutControlGroupInsertNodeTree";
            this.layoutControlGroupInsertNodeTree.Size = new System.Drawing.Size(398, 542);
            this.layoutControlGroupInsertNodeTree.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.treeListInsertNodeTree;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(378, 522);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // cbTree_Interval2
            // 
            this.cbTree_Interval2.Location = new System.Drawing.Point(0, 0);
            this.cbTree_Interval2.Name = "cbTree_Interval2";
            this.cbTree_Interval2.Size = new System.Drawing.Size(121, 20);
            this.cbTree_Interval2.TabIndex = 0;
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(200, 100);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // contextMenuStripEditNode
            // 
            this.contextMenuStripEditNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAddNode,
            this.toolStripMenuItemDeleteNode});
            this.contextMenuStripEditNode.Name = "contextMenuStripEditNode";
            this.contextMenuStripEditNode.Size = new System.Drawing.Size(109, 48);
            this.contextMenuStripEditNode.Text = "Edit Node";
            this.contextMenuStripEditNode.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStripEditNode_ItemClicked);
            // 
            // toolStripMenuItemAddNode
            // 
            this.toolStripMenuItemAddNode.Name = "toolStripMenuItemAddNode";
            this.toolStripMenuItemAddNode.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItemAddNode.Text = "Add";
            // 
            // toolStripMenuItemDeleteNode
            // 
            this.toolStripMenuItemDeleteNode.Name = "toolStripMenuItemDeleteNode";
            this.toolStripMenuItemDeleteNode.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItemDeleteNode.Text = "Delete";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(109, 515);
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(50, 20);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(399, 571);
            this.Root.TextVisible = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // xtraSaveFileDialog1
            // 
            this.xtraSaveFileDialog1.FileName = "xtraSaveFileDialog1";
            // 
            // xtraOpenFileDialog1
            // 
            this.xtraOpenFileDialog1.FileName = "xtraOpenFileDialog1";
            // 
            // accordionControlElement6
            // 
            this.accordionControlElement6.Name = "accordionControlElement6";
            this.accordionControlElement6.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement6.Text = "Conveyor";
            // 
            // barButtonItem11
            // 
            this.barButtonItem11.Caption = "Floor Setup";
            this.barButtonItem11.Id = 6;
            this.barButtonItem11.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem11.ImageOptions.Image")));
            this.barButtonItem11.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem11.ImageOptions.LargeImage")));
            this.barButtonItem11.Name = "barButtonItem11";
            // 
            // xtraOpenFileDialog2
            // 
            this.xtraOpenFileDialog2.FileName = "xtraOpenFileDialog2";
            // 
            // SimEndCondiText
            // 
            this.SimEndCondiText.Name = "SimEndCondiText";
            // 
            // SimStateBarEdit
            // 
            this.SimStateBarEdit.Name = "SimStateBarEdit";
            // 
            // textCurrentSimTimeView
            // 
            this.textCurrentSimTimeView.Caption = "Current Sim Time : ";
            this.textCurrentSimTimeView.Edit = this.repositoryItemTextEdit2;
            this.textCurrentSimTimeView.Id = 100;
            this.textCurrentSimTimeView.Name = "textCurrentSimTimeView";
            // 
            // textSimTimeSetting
            // 
            this.textSimTimeSetting.Caption = "Sim Time Setting : ";
            this.textSimTimeSetting.Edit = this.repositoryItemTextEdit1;
            this.textSimTimeSetting.Enabled = false;
            this.textSimTimeSetting.Id = 99;
            this.textSimTimeSetting.Name = "textSimTimeSetting";
            // 
            // barEditItem1
            // 
            this.barEditItem1.Edit = null;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // barHeaderItem1
            // 
            this.barHeaderItem1.Name = "barHeaderItem1";
            // 
            // barEditItem2
            // 
            this.barEditItem2.Caption = "Sim Start Time Setting: ";
            this.barEditItem2.Edit = this.repositoryItemTimeEdit3;
            this.barEditItem2.EditWidth = 200;
            this.barEditItem2.Id = 106;
            this.barEditItem2.Name = "barEditItem2";
            // 
            // barEditItem3
            // 
            this.barEditItem3.Caption = "Sim Start Time Setting: ";
            this.barEditItem3.Edit = this.repositoryItemTimeEdit3;
            this.barEditItem3.EditWidth = 200;
            this.barEditItem3.Id = 106;
            this.barEditItem3.Name = "barEditItem3";
            // 
            // barEditItem4
            // 
            this.barEditItem4.Caption = "Sim Start Time Setting: ";
            this.barEditItem4.Edit = this.repositoryItemTimeEdit3;
            this.barEditItem4.EditWidth = 200;
            this.barEditItem4.Id = 106;
            this.barEditItem4.Name = "barEditItem4";
            // 
            // ModelDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dockPanelMainScreen);
            this.Controls.Add(this.panelContainer1);
            this.Controls.Add(this.dockPanelSimNodeProperties);
            this.Controls.Add(this.dockPanelLineStatusDetail);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "ModelDesigner";
            this.Size = new System.Drawing.Size(1677, 742);
            this.Load += new System.EventHandler(this.ModelDesigner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.document1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanelParts.ResumeLayout(false);
            this.dockPanelParts_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlParts)).EndInit();
            this.layoutControlParts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupParts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemParts)).EndInit();
            this.dockPanelLineStatus.ResumeLayout(false);
            this.controlContainerLineStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListLineStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            this.dockPanelLineStatusDetail.ResumeLayout(false);
            this.dockPanelLineStatusDetail_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbTree_Interval.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLineStatusDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLineStatusDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPart)).EndInit();
            this.dockPanelSimNodeProperties.ResumeLayout(false);
            this.controlContainerSimNode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlSimObject)).EndInit();
            this.panelContainer1.ResumeLayout(false);
            this.dockPanelInsertedSimNodes.ResumeLayout(false);
            this.dockPanelCoupledModels_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlInsertedSimNodes)).EndInit();
            this.layoutControlInsertedSimNodes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.simNodeTreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupInsertedSimNodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInsertedSimNodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRemoveSimNode)).EndInit();
            this.dockPanelInsertRefNode.ResumeLayout(false);
            this.dockPanelInsertRefNode_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlInsertNode)).EndInit();
            this.layoutControlInsertNode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlInsertRefNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInsertRefNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupInsertNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInsertNode)).EndInit();
            this.dockPanelInsertCoupledModel.ResumeLayout(false);
            this.dockPanelInsertCoupledModel_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlInsertCoupledModel)).EndInit();
            this.layoutControlInsertCoupledModel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlInsertCoupledModel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInsertCoupledModel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupInsertCoupledModel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInsertCoupledModel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAddCoupledModel)).EndInit();
            this.dockPanelMainScreen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit5)).EndInit();
            this.dockPanelInsertNodeTree.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListInsertNodeTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupInsertNodeTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.contextMenuStripEditNode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SimEndCondiText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SimStateBarEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void PropertyGridControlSimObject_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            pinokio3DModel1_KeyDown(sender, e);
        }

        #endregion
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelInsertedSimNodes;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanelCoupledModels_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelParts;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanelParts_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelSimNodeProperties;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainerSimNode;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup documentGroup1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelMainScreen;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private PinokioEditorModel pinokio3DModel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private DevExpress.XtraEditors.XtraSaveFileDialog xtraSaveFileDialog1;
        private DevExpress.XtraEditors.XtraOpenFileDialog xtraOpenFileDialog1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement6;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage RB_FIRST;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup RB_PG_FILES;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgProduction;
        private DevExpress.XtraBars.BarButtonItem bbiModelingProductionData;
        private DevExpress.XtraBars.BarButtonItem bbiSettingSteps;
        private DevExpress.XtraBars.BarButtonItem RB_BTN_NEW;
        private DevExpress.XtraBars.BarButtonItem RB_BTN_LOAD;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup RB_PG_SETTINGS;
        private DevExpress.XtraBars.BarButtonItem RB_BTN_SAVE;
        private DevExpress.XtraBars.BarButtonItem RB_BTN_FLOORSETUP;
        private DevExpress.XtraBars.BarButtonItem barButtonItem11;
        private DevExpress.XtraBars.BarButtonItem RB_BTN_LINK;
        private SimNodeTreeList simNodeTreeList;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelInsertRefNode;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelInsertCoupledModel;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanelInsertRefNode_Container;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanelInsertCoupledModel_Container;
        private DevExpress.XtraGrid.GridControl gridControlInsertRefNode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewInsertRefNode;
        private DevExpress.XtraGrid.GridControl gridControlInsertCoupledModel;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewInsertCoupledModel;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgConvert;
        private DevExpress.XtraBars.BarButtonItem barButtonItemImPortModel;
        private DevExpress.XtraEditors.XtraOpenFileDialog xtraOpenFileDialog2;
        private DevExpress.XtraLayout.LayoutControl layoutControlInsertNode;
        private DevExpress.XtraLayout.LayoutControl layoutControlInsertCoupledModel;
        private DevExpress.XtraLayout.LayoutControl layoutControlInsertedSimNodes;
        private DevExpress.XtraLayout.LayoutControl layoutControlParts;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupInsertNode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemInsertNode;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupInsertCoupledModel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemInsertCoupledModel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAddCoupledModel;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupInsertedSimNodes;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupParts;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemInsertedSimNodes;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRemoveSimNode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemParts;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageSimulation;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControlSimObject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonAddCoupledModel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRemoveTreeNode;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpAnalysis;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgReport;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup16;
        private DevExpress.XtraBars.BarButtonItem bbiProductionReport;
        private DevExpress.XtraBars.BarButtonItem bbiAMHSReport;
        private DevExpress.XtraBars.BarEditItem AnimationSpeedTrack;
        private DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar repositoryItemZoomTrackBar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit SimEndCondiText;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit SimStateBarEdit;
        private DevExpress.XtraBars.BarButtonItem bbiSimRun;
        private DevExpress.XtraBars.BarButtonItem bbiSimResume;
        private DevExpress.XtraBars.BarButtonItem bbiSimPause;
        private DevExpress.XtraBars.BarButtonItem bbiSimStop;
        private DevExpress.XtraBars.BarButtonItem bbiSimReservePause;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgTimeCondition;
        private DevExpress.XtraBars.BarButtonItem PlayBack;
        public DevExpress.XtraBars.BarToggleSwitchItem Auto_Focusing_Tog_Switch;
        private DevExpress.XtraBars.BarToggleSwitchItem Background_Color_Tog_Switch;
        private DevExpress.XtraBars.BarEditItem beiAnimationSpeed;
        private DevExpress.XtraBars.BarEditItem beiSimStartTimeSetting;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit2;
        private DevExpress.XtraBars.BarEditItem beSimulationAcceleration;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraBars.BarEditItem beiSimEndTimeSetting;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit5;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgAnimation;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgSimulationExecution;
        private DevExpress.XtraBars.BarEditItem beiCurrentSimTimeView;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPlayback;
        private DevExpress.XtraBars.BarButtonItem barButtonItemOpenFolder;
        private DevExpress.XtraBars.BarButtonItem barButtonItemViewSetting;
        private DevExpress.XtraBars.BarEditItem textCurrentSimTimeView;
        private DevExpress.XtraBars.BarEditItem textSimTimeSetting;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEditNode;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddNode;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteNode;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelLineStatusDetail;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanelLineStatusDetail_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelLineStatus;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainerLineStatus;
        private GridControl gridControlLineStatusDetail;
        private GridView gridViewLineStatusDetail;
        private GridControl gridControlPart;
        private GridView gridViewPart;
        private TreeList treeListLineStatus;
        private System.Windows.Forms.ComboBox cbTree_Interval2;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label labelAutoRefresh;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage3DModel;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupEdit;
        public DevExpress.XtraBars.BarToggleSwitchItem Animation_Tog_Switch;
        private DevExpress.XtraBars.BarButtonItem bbiEditScript;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpWindow;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem bbiDockInsertNode;
        private DevExpress.XtraBars.BarButtonItem bbiDockInsertCoupledModel;
        private DevExpress.XtraBars.BarButtonItem bbiDockNodes;
        private DevExpress.XtraBars.BarButtonItem bbiDockObjectProperties;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
        private DevExpress.XtraBars.BarButtonItem bbiDockPart;
        private DevExpress.XtraBars.BarButtonItem bbiDockLineStatus;
        private DevExpress.XtraBars.BarButtonItem bbiDockLineStatusDetail;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbTree_Interval;
        private DevExpress.XtraBars.BarButtonItem bbiEditEquipment;
        private DevExpress.XtraBars.BarButtonItem RB_BTN_SAVE_AS;
        private DevExpress.XtraBars.BarButtonItem bbiEditBreakDown;
        private DevExpress.XtraBars.BarButtonItem bbiLoadProductionReport;
        private DevExpress.XtraBars.BarButtonItem bbiLoadAMHSReport;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelInsertNodeTree;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private TreeList treeListInsertNodeTree;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupInsertNodeTree;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListCategoryCol;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListNodeTypeCol;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListHeightCol;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListRefTypeCol;
        private DevExpress.XtraBars.BarButtonItem bbiEditNodes;
        private DevExpress.XtraBars.BarButtonItem bbiEditVisible;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup1;
        private DevExpress.XtraBars.BarEditItem WarmUpPeriod;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.BarHeaderItem barHeaderItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit2;
        private DevExpress.XtraBars.BarEditItem barEditItem2;
        private DevExpress.XtraBars.BarEditItem barEditItem3;
        private DevExpress.XtraBars.BarEditItem barEditItem4;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox3;
        private DevExpress.XtraBars.BarEditItem BeiHours;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox5;
        private DevExpress.XtraBars.BarEditItem BeiMinutes;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox6;
        private DevExpress.XtraBars.BarEditItem BeiDays;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox4;
        private DevExpress.XtraBars.BarLinkContainerItem BliWarmUpPeriod;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraBars.BarButtonItem bbiSaveSnapShot;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgSnapShot;
        private DevExpress.XtraBars.BarButtonItem bbiLoadSnapShot;
        private DevExpress.XtraBars.BarToggleSwitchItem State_Based_Vehicle_Tog_Switch;
        private DevExpress.XtraBars.BarToggleSwitchItem State_Based_Equipment_Tog_Switch;
        private DevExpress.XtraBars.BarToggleSwitchItem State_Based_TransportLine_Tog_Switch;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgStateBasedColors;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem bbiAlignLeft;
        private DevExpress.XtraBars.BarButtonItem bbiAlignCenter;
        private DevExpress.XtraBars.BarButtonItem bbiAlignRight;
        private DevExpress.XtraBars.BarButtonItem bbiTopAlign;
        private DevExpress.XtraBars.BarButtonItem bbiBottomAlign;
        private DevExpress.XtraBars.BarButtonItem bbiMiddleAlign;

    }
}