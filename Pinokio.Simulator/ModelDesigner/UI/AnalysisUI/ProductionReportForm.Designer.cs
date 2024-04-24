using DevExpress.XtraEditors;

namespace Pinokio.Designer
{
    partial class ProductionReportForm
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY1 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY2 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel2 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.ChartCalculatedField chartCalculatedField1 = new DevExpress.XtraCharts.ChartCalculatedField();
            DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY3 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductionReportForm));
            this.ProductionReportPages = new DevExpress.XtraTab.XtraTabControl();
            this.StepTrendTab = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl4 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl5 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl7 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnInquiryTargetStepTrend = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl46 = new DevExpress.XtraEditors.LabelControl();
            this.cboxEditStepGroupBy = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnPrevTimeStepTrend = new DevExpress.XtraEditors.SimpleButton();
            this.btnNextTimeStepTrend = new DevExpress.XtraEditors.SimpleButton();
            this.btnMonthUnitStepTrend = new DevExpress.XtraEditors.SimpleButton();
            this.btnWeekUnitStepTrend = new DevExpress.XtraEditors.SimpleButton();
            this.btnDayUnitStepTrend = new DevExpress.XtraEditors.SimpleButton();
            this.btnHourUnitStepTrend = new DevExpress.XtraEditors.SimpleButton();
            this.timeEditToStepTrend = new DevExpress.XtraEditors.TimeEdit();
            this.timeEditFromStepTrend = new DevExpress.XtraEditors.TimeEdit();
            this.btnSearchStepTrend = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.accordionControl3 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionContentContainer2 = new DevExpress.XtraBars.Navigation.AccordionContentContainer();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDownTotalTimeYMinStepTrend = new System.Windows.Forms.NumericUpDown();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDownTotalTimeYMaxStepTrend = new System.Windows.Forms.NumericUpDown();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDownCommandCountYMinStepTrend = new System.Windows.Forms.NumericUpDown();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDownCommandCountYMaxStepTrend = new System.Windows.Forms.NumericUpDown();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl24 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl25 = new DevExpress.XtraEditors.LabelControl();
            this.elapsedMaxStepTrend = new System.Windows.Forms.NumericUpDown();
            this.elapsedMinStepTrend = new System.Windows.Forms.NumericUpDown();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl27 = new DevExpress.XtraEditors.LabelControl();
            this.cboxEditTimeIntervalStepTrend = new DevExpress.XtraEditors.ComboBoxEdit();
            this.accordionControlElement3 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement5 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.splitContainerControl8 = new DevExpress.XtraEditors.SplitContainerControl();
            this.label2 = new System.Windows.Forms.Label();
            this.toggleStepTrend = new DevExpress.XtraEditors.ToggleSwitch();
            this.btnGridSaveStepTrend = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl9 = new DevExpress.XtraEditors.SplitContainerControl();
            this.chartControlStepTrend = new DevExpress.XtraCharts.ChartControl();
            this.gridControlStepTrend = new DevExpress.XtraGrid.GridControl();
            this.gridViewStepTrend = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Time = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PartStepCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SimTotalTimeAvg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.StdevTotalTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AvgQueuedTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AvgImportingTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AvgStepWaitingTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AvgProcessingTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AvgExportingTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
            this.timeEdit1 = new DevExpress.XtraEditors.TimeEdit();
            this.timeEdit2 = new DevExpress.XtraEditors.TimeEdit();
            this.simpleButton8 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.accordionControl2 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.EqpPlanOperationRate = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl15 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnInquiryTargetEqpPlanOperationRate = new DevExpress.XtraEditors.SimpleButton();
            this.btnInquiryTargetEqpPlanOperationRateBy = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrevTimeEqpPlanOperationRate = new DevExpress.XtraEditors.SimpleButton();
            this.btnNextTimeEqpPlanOperationRate = new DevExpress.XtraEditors.SimpleButton();
            this.btnMonthUnitEqpPlanOperationRate = new DevExpress.XtraEditors.SimpleButton();
            this.btnWeekUnitEqpPlanOperationRate = new DevExpress.XtraEditors.SimpleButton();
            this.btnDayUnitEqpPlanOperationRate = new DevExpress.XtraEditors.SimpleButton();
            this.btnHourUnitEqpPlanOperationRate = new DevExpress.XtraEditors.SimpleButton();
            this.timeEditToEqpPlanOperationRate = new DevExpress.XtraEditors.TimeEdit();
            this.timeEditFromEqpPlanOperationRate = new DevExpress.XtraEditors.TimeEdit();
            this.btnSearchEqpPlanOperationRate = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cboxEditEqpPlanOperationRate = new DevExpress.XtraEditors.ComboBoxEdit();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionContentContainer1 = new DevExpress.XtraBars.Navigation.AccordionContentContainer();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDownTotalTimeYMinEqpPlanOperationRate = new System.Windows.Forms.NumericUpDown();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDownTotalTimeYMaxEqpPlanOperationRate = new System.Windows.Forms.NumericUpDown();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDownCommandCountYMinEqpPlanOperationRate = new System.Windows.Forms.NumericUpDown();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDownCommandCountYMaxEqpPlanOperationRate = new System.Windows.Forms.NumericUpDown();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.elapsedMaxEqpPlanOperationRate = new System.Windows.Forms.NumericUpDown();
            this.elapsedMinEqpPlanOperationRate = new System.Windows.Forms.NumericUpDown();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.cboxEditTimeIntervalEqpPlanOperationRate = new DevExpress.XtraEditors.ComboBoxEdit();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement2 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.splitContainerControl3 = new DevExpress.XtraEditors.SplitContainerControl();
            this.label1 = new System.Windows.Forms.Label();
            this.toggleEqpPlanOperationRate = new DevExpress.XtraEditors.ToggleSwitch();
            this.btnGridSaveEqpPlanOperationRate = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl6 = new DevExpress.XtraEditors.SplitContainerControl();
            this.chartControlEqpPlanOperationRate = new DevExpress.XtraCharts.ChartControl();
            this.gridControlEqpPlanOperationRate = new DevExpress.XtraGrid.GridControl();
            this.gridViewEqpPlanOperationRate = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton18 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton27 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton28 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton29 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton30 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton31 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton32 = new DevExpress.XtraEditors.SimpleButton();
            this.timeEdit7 = new DevExpress.XtraEditors.TimeEdit();
            this.timeEdit8 = new DevExpress.XtraEditors.TimeEdit();
            this.simpleButton33 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl43 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl44 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl45 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit6 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.accordionControl6 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.FactoryInoutTab = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl10 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl11 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl12 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnInquiryTargetInout = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrevTimeInout = new DevExpress.XtraEditors.SimpleButton();
            this.btnNextTimeInout = new DevExpress.XtraEditors.SimpleButton();
            this.btnMonthUnitInout = new DevExpress.XtraEditors.SimpleButton();
            this.btnWeekUnitInout = new DevExpress.XtraEditors.SimpleButton();
            this.btnDayUnitInout = new DevExpress.XtraEditors.SimpleButton();
            this.btnHourUnitInout = new DevExpress.XtraEditors.SimpleButton();
            this.timeEditToInout = new DevExpress.XtraEditors.TimeEdit();
            this.timeEditFromInout = new DevExpress.XtraEditors.TimeEdit();
            this.btnSearchInout = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl28 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl29 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl30 = new DevExpress.XtraEditors.LabelControl();
            this.cboxEditInoutBy = new DevExpress.XtraEditors.ComboBoxEdit();
            this.accordionControl4 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionContentContainer3 = new DevExpress.XtraBars.Navigation.AccordionContentContainer();
            this.labelControl31 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDownTotalTimeYMinInout = new System.Windows.Forms.NumericUpDown();
            this.labelControl32 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDownTotalTimeYMaxInout = new System.Windows.Forms.NumericUpDown();
            this.labelControl33 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDownCommandCountYMinInout = new System.Windows.Forms.NumericUpDown();
            this.labelControl34 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDownCommandCountYMaxInout = new System.Windows.Forms.NumericUpDown();
            this.labelControl35 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl36 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl37 = new DevExpress.XtraEditors.LabelControl();
            this.elapsedMaxInout = new System.Windows.Forms.NumericUpDown();
            this.elapsedMinInout = new System.Windows.Forms.NumericUpDown();
            this.labelControl38 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl39 = new DevExpress.XtraEditors.LabelControl();
            this.cboxEditTimeIntervalInout = new DevExpress.XtraEditors.ComboBoxEdit();
            this.accordionControlElement4 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement6 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.splitContainerControl13 = new DevExpress.XtraEditors.SplitContainerControl();
            this.label3 = new System.Windows.Forms.Label();
            this.toggleInout = new DevExpress.XtraEditors.ToggleSwitch();
            this.btnGridSaveInout = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl14 = new DevExpress.XtraEditors.SplitContainerControl();
            this.chartControlInout = new DevExpress.XtraCharts.ChartControl();
            this.gridControlInout = new DevExpress.XtraGrid.GridControl();
            this.gridViewInout = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.InoutTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.InputCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.OutputCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton19 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton20 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton21 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton22 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton23 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton24 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton25 = new DevExpress.XtraEditors.SimpleButton();
            this.timeEdit5 = new DevExpress.XtraEditors.TimeEdit();
            this.timeEdit6 = new DevExpress.XtraEditors.TimeEdit();
            this.simpleButton26 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl40 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl41 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl42 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit7 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.accordionControl5 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.cboxEditStepGroup = new DevExpress.XtraEditors.ComboBoxEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.xtraSaveFileDialog1 = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            this.accordionControlElement23 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionContentContainer16 = new DevExpress.XtraBars.Navigation.AccordionContentContainer();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl63 = new DevExpress.XtraEditors.LabelControl();
            this.accordionControlElement22 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionContentContainer15 = new DevExpress.XtraBars.Navigation.AccordionContentContainer();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.accordionControlElement21 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionContentContainer17 = new DevExpress.XtraBars.Navigation.AccordionContentContainer();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.labelControl64 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl65 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.labelControl66 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.labelControl67 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.labelControl68 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.labelControl69 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl70 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEdit3 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl71 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl72 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.labelControl73 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.labelControl74 = new DevExpress.XtraEditors.LabelControl();
            this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
            this.labelControl75 = new DevExpress.XtraEditors.LabelControl();
            this.accordionControlElement24 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.InOutTotalTimeAvg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.InOutStdevTotalTime = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionReportPages)).BeginInit();
            this.ProductionReportPages.SuspendLayout();
            this.StepTrendTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl4)).BeginInit();
            this.splitContainerControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl5)).BeginInit();
            this.splitContainerControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl7)).BeginInit();
            this.splitContainerControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxEditStepGroupBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditToStepTrend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditFromStepTrend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl3)).BeginInit();
            this.accordionControl3.SuspendLayout();
            this.accordionContentContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalTimeYMinStepTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalTimeYMaxStepTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCommandCountYMinStepTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCommandCountYMaxStepTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elapsedMaxStepTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elapsedMinStepTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxEditTimeIntervalStepTrend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl8)).BeginInit();
            this.splitContainerControl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleStepTrend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl9)).BeginInit();
            this.splitContainerControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlStepTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStepTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStepTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl2)).BeginInit();
            this.EqpPlanOperationRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl15)).BeginInit();
            this.splitContainerControl15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditToEqpPlanOperationRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditFromEqpPlanOperationRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxEditEqpPlanOperationRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            this.accordionControl1.SuspendLayout();
            this.accordionContentContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalTimeYMinEqpPlanOperationRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalTimeYMaxEqpPlanOperationRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCommandCountYMinEqpPlanOperationRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCommandCountYMaxEqpPlanOperationRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elapsedMaxEqpPlanOperationRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elapsedMinEqpPlanOperationRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxEditTimeIntervalEqpPlanOperationRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).BeginInit();
            this.splitContainerControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleEqpPlanOperationRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl6)).BeginInit();
            this.splitContainerControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlEqpPlanOperationRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEqpPlanOperationRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEqpPlanOperationRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit7.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit8.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl6)).BeginInit();
            this.FactoryInoutTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl10)).BeginInit();
            this.splitContainerControl10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl11)).BeginInit();
            this.splitContainerControl11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl12)).BeginInit();
            this.splitContainerControl12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditToInout.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditFromInout.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxEditInoutBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl4)).BeginInit();
            this.accordionControl4.SuspendLayout();
            this.accordionContentContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalTimeYMinInout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalTimeYMaxInout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCommandCountYMinInout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCommandCountYMaxInout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elapsedMaxInout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elapsedMinInout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxEditTimeIntervalInout.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl13)).BeginInit();
            this.splitContainerControl13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleInout.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl14)).BeginInit();
            this.splitContainerControl14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlInout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlInout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit7.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxEditStepGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            this.accordionContentContainer17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // ProductionReportPages
            // 
            this.ProductionReportPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProductionReportPages.Location = new System.Drawing.Point(0, 0);
            this.ProductionReportPages.Name = "ProductionReportPages";
            this.ProductionReportPages.SelectedTabPage = this.StepTrendTab;
            this.ProductionReportPages.Size = new System.Drawing.Size(1471, 935);
            this.ProductionReportPages.TabIndex = 1;
            this.ProductionReportPages.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.StepTrendTab,
            this.EqpPlanOperationRate,
            this.FactoryInoutTab});
            // 
            // StepTrendTab
            // 
            this.StepTrendTab.Controls.Add(this.splitContainerControl4);
            this.StepTrendTab.Name = "StepTrendTab";
            this.StepTrendTab.Size = new System.Drawing.Size(1469, 911);
            this.StepTrendTab.Text = "Step Trend";
            // 
            // splitContainerControl4
            // 
            this.splitContainerControl4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl4.Appearance.Options.UseBackColor = true;
            this.splitContainerControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl4.Horizontal = false;
            this.splitContainerControl4.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl4.Name = "splitContainerControl4";
            this.splitContainerControl4.Panel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl4.Panel1.Appearance.Options.UseBackColor = true;
            this.splitContainerControl4.Panel1.Controls.Add(this.splitContainerControl5);
            this.splitContainerControl4.Panel1.Controls.Add(this.simpleButton1);
            this.splitContainerControl4.Panel1.Controls.Add(this.simpleButton2);
            this.splitContainerControl4.Panel1.Controls.Add(this.simpleButton3);
            this.splitContainerControl4.Panel1.Controls.Add(this.simpleButton4);
            this.splitContainerControl4.Panel1.Controls.Add(this.simpleButton5);
            this.splitContainerControl4.Panel1.Controls.Add(this.simpleButton6);
            this.splitContainerControl4.Panel1.Controls.Add(this.simpleButton7);
            this.splitContainerControl4.Panel1.Controls.Add(this.timeEdit1);
            this.splitContainerControl4.Panel1.Controls.Add(this.timeEdit2);
            this.splitContainerControl4.Panel1.Controls.Add(this.simpleButton8);
            this.splitContainerControl4.Panel1.Controls.Add(this.labelControl4);
            this.splitContainerControl4.Panel1.Controls.Add(this.labelControl5);
            this.splitContainerControl4.Panel1.Controls.Add(this.labelControl6);
            this.splitContainerControl4.Panel1.Controls.Add(this.comboBoxEdit1);
            this.splitContainerControl4.Panel1.Text = "Panel1";
            this.splitContainerControl4.Panel2.Controls.Add(this.accordionControl2);
            this.splitContainerControl4.Panel2.Text = "Panel2";
            this.splitContainerControl4.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            this.splitContainerControl4.Size = new System.Drawing.Size(1469, 911);
            this.splitContainerControl4.SplitterPosition = 540;
            this.splitContainerControl4.TabIndex = 2;
            // 
            // splitContainerControl5
            // 
            this.splitContainerControl5.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl5.Appearance.Options.UseBackColor = true;
            this.splitContainerControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl5.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl5.Name = "splitContainerControl5";
            this.splitContainerControl5.Panel1.Controls.Add(this.splitContainerControl7);
            this.splitContainerControl5.Panel1.Text = "Panel1";
            this.splitContainerControl5.Panel2.Controls.Add(this.splitContainerControl8);
            this.splitContainerControl5.Panel2.Text = "Panel2";
            this.splitContainerControl5.Size = new System.Drawing.Size(1465, 907);
            this.splitContainerControl5.SplitterPosition = 278;
            this.splitContainerControl5.TabIndex = 12;
            // 
            // splitContainerControl7
            // 
            this.splitContainerControl7.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl7.Appearance.Options.UseBackColor = true;
            this.splitContainerControl7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl7.Horizontal = false;
            this.splitContainerControl7.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl7.Name = "splitContainerControl7";
            this.splitContainerControl7.Panel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl7.Panel1.Appearance.Options.UseBackColor = true;
            this.splitContainerControl7.Panel1.Controls.Add(this.btnInquiryTargetStepTrend);
            this.splitContainerControl7.Panel1.Controls.Add(this.labelControl46);
            this.splitContainerControl7.Panel1.Controls.Add(this.cboxEditStepGroupBy);
            this.splitContainerControl7.Panel1.Controls.Add(this.btnPrevTimeStepTrend);
            this.splitContainerControl7.Panel1.Controls.Add(this.btnNextTimeStepTrend);
            this.splitContainerControl7.Panel1.Controls.Add(this.btnMonthUnitStepTrend);
            this.splitContainerControl7.Panel1.Controls.Add(this.btnWeekUnitStepTrend);
            this.splitContainerControl7.Panel1.Controls.Add(this.btnDayUnitStepTrend);
            this.splitContainerControl7.Panel1.Controls.Add(this.btnHourUnitStepTrend);
            this.splitContainerControl7.Panel1.Controls.Add(this.timeEditToStepTrend);
            this.splitContainerControl7.Panel1.Controls.Add(this.timeEditFromStepTrend);
            this.splitContainerControl7.Panel1.Controls.Add(this.btnSearchStepTrend);
            this.splitContainerControl7.Panel1.Controls.Add(this.labelControl17);
            this.splitContainerControl7.Panel1.Controls.Add(this.labelControl18);
            this.splitContainerControl7.Panel1.Text = "Panel1";
            this.splitContainerControl7.Panel2.Controls.Add(this.accordionControl3);
            this.splitContainerControl7.Panel2.Text = "Panel2";
            this.splitContainerControl7.Size = new System.Drawing.Size(278, 907);
            this.splitContainerControl7.SplitterPosition = 197;
            this.splitContainerControl7.TabIndex = 1;
            // 
            // btnInquiryTargetStepTrend
            // 
            this.btnInquiryTargetStepTrend.Location = new System.Drawing.Point(110, 27);
            this.btnInquiryTargetStepTrend.Name = "btnInquiryTargetStepTrend";
            this.btnInquiryTargetStepTrend.Size = new System.Drawing.Size(158, 23);
            this.btnInquiryTargetStepTrend.TabIndex = 14;
            this.btnInquiryTargetStepTrend.Text = "Select Targets";
            this.btnInquiryTargetStepTrend.Click += new System.EventHandler(this.btnInquiryTarget_Click);
            // 
            // labelControl46
            // 
            this.labelControl46.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl46.Appearance.Options.UseFont = true;
            this.labelControl46.Location = new System.Drawing.Point(36, 5);
            this.labelControl46.Name = "labelControl46";
            this.labelControl46.Size = new System.Drawing.Size(60, 14);
            this.labelControl46.TabIndex = 13;
            this.labelControl46.Text = "Group By:";
            // 
            // cboxEditStepGroupBy
            // 
            this.cboxEditStepGroupBy.EditValue = "";
            this.cboxEditStepGroupBy.Location = new System.Drawing.Point(110, 2);
            this.cboxEditStepGroupBy.Name = "cboxEditStepGroupBy";
            this.cboxEditStepGroupBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxEditStepGroupBy.Properties.Items.AddRange(new object[] {
            "Equipment",
            "Step",
            "Product"});
            this.cboxEditStepGroupBy.Size = new System.Drawing.Size(158, 20);
            this.cboxEditStepGroupBy.TabIndex = 12;
            this.cboxEditStepGroupBy.SelectedIndexChanged += new System.EventHandler(this.cboxEditStepGroupBy_SelectedIndexChanged);
            // 
            // btnPrevTimeStepTrend
            // 
            this.btnPrevTimeStepTrend.Location = new System.Drawing.Point(3, 112);
            this.btnPrevTimeStepTrend.Name = "btnPrevTimeStepTrend";
            this.btnPrevTimeStepTrend.Size = new System.Drawing.Size(19, 23);
            this.btnPrevTimeStepTrend.TabIndex = 10;
            this.btnPrevTimeStepTrend.Text = "◁";
            this.btnPrevTimeStepTrend.Click += new System.EventHandler(this.btnPrevTime_Click);
            // 
            // btnNextTimeStepTrend
            // 
            this.btnNextTimeStepTrend.Location = new System.Drawing.Point(249, 112);
            this.btnNextTimeStepTrend.Name = "btnNextTimeStepTrend";
            this.btnNextTimeStepTrend.Size = new System.Drawing.Size(19, 23);
            this.btnNextTimeStepTrend.TabIndex = 9;
            this.btnNextTimeStepTrend.Text = "▷";
            this.btnNextTimeStepTrend.Click += new System.EventHandler(this.btnNextTime_Click);
            // 
            // btnMonthUnitStepTrend
            // 
            this.btnMonthUnitStepTrend.Location = new System.Drawing.Point(194, 112);
            this.btnMonthUnitStepTrend.Name = "btnMonthUnitStepTrend";
            this.btnMonthUnitStepTrend.Size = new System.Drawing.Size(49, 23);
            this.btnMonthUnitStepTrend.TabIndex = 8;
            this.btnMonthUnitStepTrend.Text = "Month";
            this.btnMonthUnitStepTrend.Click += new System.EventHandler(this.btnMonthUnit_Click);
            // 
            // btnWeekUnitStepTrend
            // 
            this.btnWeekUnitStepTrend.Location = new System.Drawing.Point(139, 112);
            this.btnWeekUnitStepTrend.Name = "btnWeekUnitStepTrend";
            this.btnWeekUnitStepTrend.Size = new System.Drawing.Size(49, 23);
            this.btnWeekUnitStepTrend.TabIndex = 7;
            this.btnWeekUnitStepTrend.Text = "Week";
            this.btnWeekUnitStepTrend.Click += new System.EventHandler(this.btnWeekUnit_Click);
            // 
            // btnDayUnitStepTrend
            // 
            this.btnDayUnitStepTrend.Location = new System.Drawing.Point(82, 112);
            this.btnDayUnitStepTrend.Name = "btnDayUnitStepTrend";
            this.btnDayUnitStepTrend.Size = new System.Drawing.Size(49, 23);
            this.btnDayUnitStepTrend.TabIndex = 6;
            this.btnDayUnitStepTrend.Text = "Day";
            this.btnDayUnitStepTrend.Click += new System.EventHandler(this.btnDayUnit_Click);
            // 
            // btnHourUnitStepTrend
            // 
            this.btnHourUnitStepTrend.Location = new System.Drawing.Point(27, 112);
            this.btnHourUnitStepTrend.Name = "btnHourUnitStepTrend";
            this.btnHourUnitStepTrend.Size = new System.Drawing.Size(49, 23);
            this.btnHourUnitStepTrend.TabIndex = 5;
            this.btnHourUnitStepTrend.Text = "Hour";
            this.btnHourUnitStepTrend.Click += new System.EventHandler(this.btnHourUnit_Click);
            // 
            // timeEditToStepTrend
            // 
            this.timeEditToStepTrend.EditValue = new System.DateTime(2020, 9, 7, 0, 0, 0, 0);
            this.timeEditToStepTrend.Location = new System.Drawing.Point(91, 167);
            this.timeEditToStepTrend.Name = "timeEditToStepTrend";
            this.timeEditToStepTrend.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEditToStepTrend.Properties.Mask.EditMask = "G";
            this.timeEditToStepTrend.Size = new System.Drawing.Size(180, 20);
            this.timeEditToStepTrend.TabIndex = 4;
            // 
            // timeEditFromStepTrend
            // 
            this.timeEditFromStepTrend.AllowDrop = true;
            this.timeEditFromStepTrend.EditValue = new System.DateTime(2020, 9, 7, 0, 0, 0, 0);
            this.timeEditFromStepTrend.Location = new System.Drawing.Point(91, 141);
            this.timeEditFromStepTrend.Name = "timeEditFromStepTrend";
            this.timeEditFromStepTrend.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEditFromStepTrend.Properties.Mask.EditMask = "G";
            this.timeEditFromStepTrend.Size = new System.Drawing.Size(180, 20);
            this.timeEditFromStepTrend.TabIndex = 3;
            // 
            // btnSearchStepTrend
            // 
            this.btnSearchStepTrend.Location = new System.Drawing.Point(189, 54);
            this.btnSearchStepTrend.Name = "btnSearchStepTrend";
            this.btnSearchStepTrend.Size = new System.Drawing.Size(79, 23);
            this.btnSearchStepTrend.TabIndex = 2;
            this.btnSearchStepTrend.Text = "Search";
            this.btnSearchStepTrend.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labelControl17
            // 
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl17.Appearance.Options.UseFont = true;
            this.labelControl17.Location = new System.Drawing.Point(10, 167);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(51, 14);
            this.labelControl17.TabIndex = 1;
            this.labelControl17.Text = "To Time:";
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl18.Appearance.Options.UseFont = true;
            this.labelControl18.Location = new System.Drawing.Point(11, 141);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(66, 14);
            this.labelControl18.TabIndex = 1;
            this.labelControl18.Text = "From Time:";
            // 
            // accordionControl3
            // 
            this.accordionControl3.Controls.Add(this.accordionContentContainer2);
            this.accordionControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accordionControl3.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement3});
            this.accordionControl3.ExpandElementMode = DevExpress.XtraBars.Navigation.ExpandElementMode.Multiple;
            this.accordionControl3.Location = new System.Drawing.Point(0, 0);
            this.accordionControl3.Name = "accordionControl3";
            this.accordionControl3.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.False;
            this.accordionControl3.Size = new System.Drawing.Size(274, 696);
            this.accordionControl3.TabIndex = 0;
            this.accordionControl3.Text = "accordionControl3";
            // 
            // accordionContentContainer2
            // 
            this.accordionContentContainer2.Controls.Add(this.labelControl19);
            this.accordionContentContainer2.Controls.Add(this.numericUpDownTotalTimeYMinStepTrend);
            this.accordionContentContainer2.Controls.Add(this.labelControl20);
            this.accordionContentContainer2.Controls.Add(this.numericUpDownTotalTimeYMaxStepTrend);
            this.accordionContentContainer2.Controls.Add(this.labelControl21);
            this.accordionContentContainer2.Controls.Add(this.numericUpDownCommandCountYMinStepTrend);
            this.accordionContentContainer2.Controls.Add(this.labelControl22);
            this.accordionContentContainer2.Controls.Add(this.numericUpDownCommandCountYMaxStepTrend);
            this.accordionContentContainer2.Controls.Add(this.labelControl23);
            this.accordionContentContainer2.Controls.Add(this.labelControl24);
            this.accordionContentContainer2.Controls.Add(this.labelControl25);
            this.accordionContentContainer2.Controls.Add(this.elapsedMaxStepTrend);
            this.accordionContentContainer2.Controls.Add(this.elapsedMinStepTrend);
            this.accordionContentContainer2.Controls.Add(this.labelControl26);
            this.accordionContentContainer2.Controls.Add(this.labelControl27);
            this.accordionContentContainer2.Controls.Add(this.cboxEditTimeIntervalStepTrend);
            this.accordionContentContainer2.Name = "accordionContentContainer2";
            this.accordionContentContainer2.Size = new System.Drawing.Size(257, 307);
            this.accordionContentContainer2.TabIndex = 6;
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl19.Appearance.Options.UseFont = true;
            this.labelControl19.Location = new System.Drawing.Point(17, 184);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(183, 14);
            this.labelControl19.TabIndex = 65;
            this.labelControl19.Text = "Total Time(min) Chart Setting";
            // 
            // numericUpDownTotalTimeYMinStepTrend
            // 
            this.numericUpDownTotalTimeYMinStepTrend.DecimalPlaces = 1;
            this.numericUpDownTotalTimeYMinStepTrend.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownTotalTimeYMinStepTrend.Location = new System.Drawing.Point(129, 242);
            this.numericUpDownTotalTimeYMinStepTrend.Name = "numericUpDownTotalTimeYMinStepTrend";
            this.numericUpDownTotalTimeYMinStepTrend.Size = new System.Drawing.Size(103, 22);
            this.numericUpDownTotalTimeYMinStepTrend.TabIndex = 64;
            this.numericUpDownTotalTimeYMinStepTrend.ValueChanged += new System.EventHandler(this.numericUpDownTotalTimeYMin_ValueChanged);
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(9, 244);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(40, 14);
            this.labelControl20.TabIndex = 63;
            this.labelControl20.Text = "Y축 Min";
            // 
            // numericUpDownTotalTimeYMaxStepTrend
            // 
            this.numericUpDownTotalTimeYMaxStepTrend.DecimalPlaces = 1;
            this.numericUpDownTotalTimeYMaxStepTrend.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownTotalTimeYMaxStepTrend.Location = new System.Drawing.Point(129, 210);
            this.numericUpDownTotalTimeYMaxStepTrend.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDownTotalTimeYMaxStepTrend.Name = "numericUpDownTotalTimeYMaxStepTrend";
            this.numericUpDownTotalTimeYMaxStepTrend.Size = new System.Drawing.Size(103, 22);
            this.numericUpDownTotalTimeYMaxStepTrend.TabIndex = 62;
            this.numericUpDownTotalTimeYMaxStepTrend.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownTotalTimeYMaxStepTrend.ValueChanged += new System.EventHandler(this.numericUpDowntTotalTimeYMax_ValueChanged);
            // 
            // labelControl21
            // 
            this.labelControl21.Location = new System.Drawing.Point(9, 212);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(43, 14);
            this.labelControl21.TabIndex = 61;
            this.labelControl21.Text = "Y축 Max";
            // 
            // numericUpDownCommandCountYMinStepTrend
            // 
            this.numericUpDownCommandCountYMinStepTrend.Location = new System.Drawing.Point(129, 153);
            this.numericUpDownCommandCountYMinStepTrend.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownCommandCountYMinStepTrend.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownCommandCountYMinStepTrend.Name = "numericUpDownCommandCountYMinStepTrend";
            this.numericUpDownCommandCountYMinStepTrend.Size = new System.Drawing.Size(103, 22);
            this.numericUpDownCommandCountYMinStepTrend.TabIndex = 60;
            this.numericUpDownCommandCountYMinStepTrend.ValueChanged += new System.EventHandler(this.numericUpDownYMin_ValueChanged);
            // 
            // labelControl22
            // 
            this.labelControl22.Location = new System.Drawing.Point(9, 155);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(40, 14);
            this.labelControl22.TabIndex = 59;
            this.labelControl22.Text = "Y축 Min";
            // 
            // numericUpDownCommandCountYMaxStepTrend
            // 
            this.numericUpDownCommandCountYMaxStepTrend.Location = new System.Drawing.Point(129, 121);
            this.numericUpDownCommandCountYMaxStepTrend.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownCommandCountYMaxStepTrend.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownCommandCountYMaxStepTrend.Name = "numericUpDownCommandCountYMaxStepTrend";
            this.numericUpDownCommandCountYMaxStepTrend.Size = new System.Drawing.Size(103, 22);
            this.numericUpDownCommandCountYMaxStepTrend.TabIndex = 58;
            this.numericUpDownCommandCountYMaxStepTrend.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownCommandCountYMaxStepTrend.ValueChanged += new System.EventHandler(this.numericUpDownYMax_ValueChanged);
            // 
            // labelControl23
            // 
            this.labelControl23.Location = new System.Drawing.Point(9, 123);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(43, 14);
            this.labelControl23.TabIndex = 57;
            this.labelControl23.Text = "Y축 Max";
            // 
            // labelControl24
            // 
            this.labelControl24.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl24.Appearance.Options.UseFont = true;
            this.labelControl24.Location = new System.Drawing.Point(16, 92);
            this.labelControl24.Name = "labelControl24";
            this.labelControl24.Size = new System.Drawing.Size(167, 14);
            this.labelControl24.TabIndex = 56;
            this.labelControl24.Text = "Through-put Chart Setting";
            // 
            // labelControl25
            // 
            this.labelControl25.Location = new System.Drawing.Point(17, 63);
            this.labelControl25.Name = "labelControl25";
            this.labelControl25.Size = new System.Drawing.Size(33, 14);
            this.labelControl25.TabIndex = 26;
            this.labelControl25.Text = "Group";
            // 
            // elapsedMaxStepTrend
            // 
            this.elapsedMaxStepTrend.Location = new System.Drawing.Point(104, 31);
            this.elapsedMaxStepTrend.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.elapsedMaxStepTrend.Name = "elapsedMaxStepTrend";
            this.elapsedMaxStepTrend.Size = new System.Drawing.Size(120, 22);
            this.elapsedMaxStepTrend.TabIndex = 25;
            this.elapsedMaxStepTrend.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            // 
            // elapsedMinStepTrend
            // 
            this.elapsedMinStepTrend.Location = new System.Drawing.Point(104, 3);
            this.elapsedMinStepTrend.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.elapsedMinStepTrend.Name = "elapsedMinStepTrend";
            this.elapsedMinStepTrend.Size = new System.Drawing.Size(120, 22);
            this.elapsedMinStepTrend.TabIndex = 24;
            // 
            // labelControl26
            // 
            this.labelControl26.Location = new System.Drawing.Point(16, 36);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(76, 14);
            this.labelControl26.TabIndex = 23;
            this.labelControl26.Text = "Elapsed (Max)";
            // 
            // labelControl27
            // 
            this.labelControl27.Location = new System.Drawing.Point(17, 5);
            this.labelControl27.Name = "labelControl27";
            this.labelControl27.Size = new System.Drawing.Size(73, 14);
            this.labelControl27.TabIndex = 22;
            this.labelControl27.Text = "Elapsed (Min)";
            // 
            // cboxEditTimeIntervalStepTrend
            // 
            this.cboxEditTimeIntervalStepTrend.EditValue = "1Hour";
            this.cboxEditTimeIntervalStepTrend.Location = new System.Drawing.Point(103, 60);
            this.cboxEditTimeIntervalStepTrend.Name = "cboxEditTimeIntervalStepTrend";
            this.cboxEditTimeIntervalStepTrend.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxEditTimeIntervalStepTrend.Properties.Items.AddRange(new object[] {
            "1Min",
            "10Min",
            "1Hour",
            "1Day"});
            this.cboxEditTimeIntervalStepTrend.Size = new System.Drawing.Size(121, 20);
            this.cboxEditTimeIntervalStepTrend.TabIndex = 21;
            // 
            // accordionControlElement3
            // 
            this.accordionControlElement3.ContentContainer = this.accordionContentContainer2;
            this.accordionControlElement3.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement5});
            this.accordionControlElement3.Expanded = true;
            this.accordionControlElement3.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.accordionControlElement3.Name = "accordionControlElement3";
            this.accordionControlElement3.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement3.Text = "ETC";
            // 
            // accordionControlElement5
            // 
            this.accordionControlElement5.Expanded = true;
            this.accordionControlElement5.Name = "accordionControlElement5";
            this.accordionControlElement5.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement5.Text = "Element9";
            this.accordionControlElement5.Visible = false;
            // 
            // splitContainerControl8
            // 
            this.splitContainerControl8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl8.Horizontal = false;
            this.splitContainerControl8.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl8.Name = "splitContainerControl8";
            this.splitContainerControl8.Panel1.Controls.Add(this.label2);
            this.splitContainerControl8.Panel1.Controls.Add(this.toggleStepTrend);
            this.splitContainerControl8.Panel1.Controls.Add(this.btnGridSaveStepTrend);
            this.splitContainerControl8.Panel1.Text = "Panel1";
            this.splitContainerControl8.Panel2.Controls.Add(this.splitContainerControl9);
            this.splitContainerControl8.Panel2.Text = "Panel2";
            this.splitContainerControl8.Size = new System.Drawing.Size(1177, 907);
            this.splitContainerControl8.SplitterPosition = 30;
            this.splitContainerControl8.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(995, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "Total Time Label";
            // 
            // toggleStepTrend
            // 
            this.toggleStepTrend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toggleStepTrend.EditValue = true;
            this.toggleStepTrend.Location = new System.Drawing.Point(1099, 9);
            this.toggleStepTrend.Name = "toggleStepTrend";
            this.toggleStepTrend.Properties.OffText = "Off";
            this.toggleStepTrend.Properties.OnText = "On";
            this.toggleStepTrend.Size = new System.Drawing.Size(95, 19);
            this.toggleStepTrend.TabIndex = 8;
            this.toggleStepTrend.Toggled += new System.EventHandler(this.labelSwitch_Toggled);
            // 
            // btnGridSaveStepTrend
            // 
            this.btnGridSaveStepTrend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGridSaveStepTrend.Location = new System.Drawing.Point(734, 7);
            this.btnGridSaveStepTrend.Name = "btnGridSaveStepTrend";
            this.btnGridSaveStepTrend.Size = new System.Drawing.Size(240, 23);
            this.btnGridSaveStepTrend.TabIndex = 5;
            this.btnGridSaveStepTrend.Text = "Export to Excel";
            this.btnGridSaveStepTrend.Click += new System.EventHandler(this.buttonGridSave_Click);
            // 
            // splitContainerControl9
            // 
            this.splitContainerControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl9.Horizontal = false;
            this.splitContainerControl9.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl9.Name = "splitContainerControl9";
            this.splitContainerControl9.Panel1.Controls.Add(this.chartControlStepTrend);
            this.splitContainerControl9.Panel1.Text = "Panel1";
            this.splitContainerControl9.Panel2.Controls.Add(this.gridControlStepTrend);
            this.splitContainerControl9.Panel2.Text = "Panel2";
            this.splitContainerControl9.Size = new System.Drawing.Size(1177, 867);
            this.splitContainerControl9.SplitterPosition = 280;
            this.splitContainerControl9.TabIndex = 0;
            // 
            // chartControlStepTrend
            // 
            xyDiagram1.AxisX.DateTimeScaleOptions.AggregateFunction = DevExpress.XtraCharts.AggregateFunction.None;
            xyDiagram1.AxisX.DateTimeScaleOptions.MeasureUnit = DevExpress.XtraCharts.DateTimeMeasureUnit.Minute;
            xyDiagram1.AxisX.Label.Angle = -90;
            xyDiagram1.AxisX.Label.TextPattern = "{A:MM/dd \nHH:mm}";
            xyDiagram1.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram1.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisX.WholeRange.EndSideMargin = 20D;
            xyDiagram1.AxisX.WholeRange.SideMarginSizeUnit = DevExpress.XtraCharts.SideMarginSizeUnit.AxisRangePercentage;
            xyDiagram1.AxisX.WholeRange.StartSideMargin = 2D;
            xyDiagram1.AxisY.Tickmarks.MinorVisible = false;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.WholeRange.SideMarginSizeUnit = DevExpress.XtraCharts.SideMarginSizeUnit.AxisRangePercentage;
            xyDiagram1.EnableAxisXScrolling = true;
            xyDiagram1.EnableAxisXZooming = true;
            xyDiagram1.EnableAxisYScrolling = true;
            xyDiagram1.EnableAxisYZooming = true;
            secondaryAxisY1.AxisID = 0;
            secondaryAxisY1.Label.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(80)))), ((int)(((byte)(77)))));
            secondaryAxisY1.Name = "Secondary AxisY 1";
            secondaryAxisY1.Tickmarks.MinorVisible = false;
            secondaryAxisY1.VisibleInPanesSerializable = "-1";
            xyDiagram1.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY1});
            this.chartControlStepTrend.Diagram = xyDiagram1;
            this.chartControlStepTrend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControlStepTrend.Legend.Name = "Default Legend";
            this.chartControlStepTrend.Location = new System.Drawing.Point(0, 0);
            this.chartControlStepTrend.Name = "chartControlStepTrend";
            series1.ArgumentDataMember = "TIME";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series1.Name = "Through-put";
            series1.ValueDataMembersSerializable = "EqpPlanCount";
            series2.ArgumentDataMember = "TIME";
            series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            pointSeriesLabel1.Angle = 90;
            pointSeriesLabel1.TextPattern = "{V:f3}";
            series2.Label = pointSeriesLabel1;
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series2.Name = "Total Time(min)";
            series2.ValueDataMembersSerializable = "SimTotalTimeAvg";
            lineSeriesView1.AxisYName = "Secondary AxisY 1";
            series2.View = lineSeriesView1;
            this.chartControlStepTrend.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
            this.chartControlStepTrend.Size = new System.Drawing.Size(1177, 280);
            this.chartControlStepTrend.TabIndex = 2;
            // 
            // gridControlStepTrend
            // 
            this.gridControlStepTrend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlStepTrend.Location = new System.Drawing.Point(0, 0);
            this.gridControlStepTrend.MainView = this.gridViewStepTrend;
            this.gridControlStepTrend.Name = "gridControlStepTrend";
            this.gridControlStepTrend.Size = new System.Drawing.Size(1177, 577);
            this.gridControlStepTrend.TabIndex = 3;
            this.gridControlStepTrend.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewStepTrend});
            // 
            // gridViewStepTrend
            // 
            this.gridViewStepTrend.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Time,
            this.PartStepCount,
            this.SimTotalTimeAvg,
            this.StdevTotalTime,
            this.AvgQueuedTime,
            this.AvgImportingTime,
            this.AvgStepWaitingTime,
            this.AvgProcessingTime,
            this.AvgExportingTime});
            this.gridViewStepTrend.GridControl = this.gridControlStepTrend;
            this.gridViewStepTrend.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewStepTrend.Name = "gridViewStepTrend";
            this.gridViewStepTrend.OptionsBehavior.Editable = false;
            this.gridViewStepTrend.OptionsBehavior.ReadOnly = true;
            this.gridViewStepTrend.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewStepTrend.OptionsMenu.ShowFooterItem = true;
            this.gridViewStepTrend.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.gridViewStepTrend.OptionsMenu.ShowSummaryItemMode = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewStepTrend.OptionsSelection.MultiSelect = true;
            this.gridViewStepTrend.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridViewStepTrend.OptionsView.ShowDetailButtons = false;
            this.gridViewStepTrend.DoubleClick += new System.EventHandler(this.gridViewStepTrend_DoubleClick);
            // 
            // Time
            // 
            this.Time.AppearanceCell.Options.UseTextOptions = true;
            this.Time.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Time.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Time.AppearanceHeader.Options.UseTextOptions = true;
            this.Time.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Time.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Time.Caption = "Time";
            this.Time.DisplayFormat.FormatString = "yyyy/MM/dd HH:mm:ss";
            this.Time.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Time.FieldName = "Time";
            this.Time.Name = "Time";
            this.Time.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "Time", "MIN"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "Time", "MAX"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "Time", "AVG"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Time", "SUM")});
            this.Time.Visible = true;
            this.Time.VisibleIndex = 0;
            // 
            // PartStepCount
            // 
            this.PartStepCount.AppearanceCell.Options.UseTextOptions = true;
            this.PartStepCount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PartStepCount.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.PartStepCount.AppearanceHeader.Options.UseTextOptions = true;
            this.PartStepCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PartStepCount.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.PartStepCount.Caption = "Through-put";
            this.PartStepCount.FieldName = "PartStepCount";
            this.PartStepCount.Name = "PartStepCount";
            this.PartStepCount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "PartStepCount", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "PartStepCount", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "PartStepCount", "{0:0.##}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PartStepCount", "{0:0.##}")});
            this.PartStepCount.Visible = true;
            this.PartStepCount.VisibleIndex = 1;
            // 
            // SimTotalTimeAvg
            // 
            this.SimTotalTimeAvg.AppearanceCell.Options.UseTextOptions = true;
            this.SimTotalTimeAvg.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SimTotalTimeAvg.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SimTotalTimeAvg.AppearanceHeader.Options.UseTextOptions = true;
            this.SimTotalTimeAvg.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SimTotalTimeAvg.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SimTotalTimeAvg.Caption = "Total Time(min)";
            this.SimTotalTimeAvg.FieldName = "SimTotalTimeAvg";
            this.SimTotalTimeAvg.Name = "SimTotalTimeAvg";
            this.SimTotalTimeAvg.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "SimTotalTimeAvg", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "SimTotalTimeAvg", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "SimTotalTimeAvg", "{0:0.##}")});
            this.SimTotalTimeAvg.Visible = true;
            this.SimTotalTimeAvg.VisibleIndex = 2;
            // 
            // StdevTotalTime
            // 
            this.StdevTotalTime.AppearanceCell.Options.UseTextOptions = true;
            this.StdevTotalTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.StdevTotalTime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.StdevTotalTime.AppearanceHeader.Options.UseTextOptions = true;
            this.StdevTotalTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.StdevTotalTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.StdevTotalTime.Caption = "Stdev(min)";
            this.StdevTotalTime.FieldName = "StdevTotalTime";
            this.StdevTotalTime.Name = "StdevTotalTime";
            this.StdevTotalTime.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "StdevTotalTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "StdevTotalTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "StdevTotalTime", "{0:0.##}")});
            this.StdevTotalTime.Visible = true;
            this.StdevTotalTime.VisibleIndex = 3;
            // 
            // AvgQueuedTime
            // 
            this.AvgQueuedTime.AppearanceCell.Options.UseTextOptions = true;
            this.AvgQueuedTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.AvgQueuedTime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AvgQueuedTime.AppearanceHeader.Options.UseTextOptions = true;
            this.AvgQueuedTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.AvgQueuedTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AvgQueuedTime.Caption = "Queued Time(sec)";
            this.AvgQueuedTime.FieldName = "AvgQueuedTime";
            this.AvgQueuedTime.Name = "AvgQueuedTime";
            this.AvgQueuedTime.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "AvgQueuedTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "AvgQueuedTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "AvgQueuedTime", "{0:0.##}")});
            this.AvgQueuedTime.Visible = true;
            this.AvgQueuedTime.VisibleIndex = 4;
            // 
            // AvgImportingTime
            // 
            this.AvgImportingTime.AppearanceCell.Options.UseTextOptions = true;
            this.AvgImportingTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.AvgImportingTime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AvgImportingTime.AppearanceHeader.Options.UseTextOptions = true;
            this.AvgImportingTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.AvgImportingTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AvgImportingTime.Caption = "Importing Time(sec)";
            this.AvgImportingTime.FieldName = "AvgImportingTime";
            this.AvgImportingTime.Name = "AvgImportingTime";
            this.AvgImportingTime.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "AvgImportingTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "AvgImportingTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "AvgImportingTime", "{0:0.##}")});
            this.AvgImportingTime.Visible = true;
            this.AvgImportingTime.VisibleIndex = 5;
            // 
            // AvgStepWaitingTime
            // 
            this.AvgStepWaitingTime.AppearanceCell.Options.UseTextOptions = true;
            this.AvgStepWaitingTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.AvgStepWaitingTime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AvgStepWaitingTime.AppearanceHeader.Options.UseTextOptions = true;
            this.AvgStepWaitingTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.AvgStepWaitingTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AvgStepWaitingTime.Caption = "Step Waiting Time(sec)";
            this.AvgStepWaitingTime.FieldName = "AvgStepWaitingTime";
            this.AvgStepWaitingTime.Name = "AvgStepWaitingTime";
            this.AvgStepWaitingTime.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "AvgStepWaitingTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "AvgStepWaitingTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "AvgStepWaitingTime", "{0:0.##}")});
            this.AvgStepWaitingTime.Visible = true;
            this.AvgStepWaitingTime.VisibleIndex = 6;
            // 
            // AvgProcessingTime
            // 
            this.AvgProcessingTime.AppearanceCell.Options.UseTextOptions = true;
            this.AvgProcessingTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.AvgProcessingTime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AvgProcessingTime.AppearanceHeader.Options.UseTextOptions = true;
            this.AvgProcessingTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.AvgProcessingTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AvgProcessingTime.Caption = "Processing Time(sec)";
            this.AvgProcessingTime.FieldName = "AvgProcessingTime";
            this.AvgProcessingTime.Name = "AvgProcessingTime";
            this.AvgProcessingTime.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "AvgProcessingTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "AvgProcessingTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "AvgProcessingTime", "{0:0.##}")});
            this.AvgProcessingTime.Visible = true;
            this.AvgProcessingTime.VisibleIndex = 7;
            // 
            // AvgExportingTime
            // 
            this.AvgExportingTime.AppearanceCell.Options.UseTextOptions = true;
            this.AvgExportingTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.AvgExportingTime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AvgExportingTime.AppearanceHeader.Options.UseTextOptions = true;
            this.AvgExportingTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.AvgExportingTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AvgExportingTime.Caption = "Exporting Time(sec)";
            this.AvgExportingTime.FieldName = "AvgExportingTime";
            this.AvgExportingTime.Name = "AvgExportingTime";
            this.AvgExportingTime.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "AvgExportingTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "AvgExportingTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "AvgExportingTime", "{0:0.##}")});
            this.AvgExportingTime.Visible = true;
            this.AvgExportingTime.VisibleIndex = 8;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(112, 31);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(158, 23);
            this.simpleButton1.TabIndex = 11;
            this.simpleButton1.Text = "Inquiry Target";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(5, 114);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(19, 23);
            this.simpleButton2.TabIndex = 10;
            this.simpleButton2.Text = "◁";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(251, 114);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(19, 23);
            this.simpleButton3.TabIndex = 9;
            this.simpleButton3.Text = "▷";
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(196, 114);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(49, 23);
            this.simpleButton4.TabIndex = 8;
            this.simpleButton4.Text = "Month";
            // 
            // simpleButton5
            // 
            this.simpleButton5.Location = new System.Drawing.Point(141, 114);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(49, 23);
            this.simpleButton5.TabIndex = 7;
            this.simpleButton5.Text = "Week";
            // 
            // simpleButton6
            // 
            this.simpleButton6.Location = new System.Drawing.Point(84, 114);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(49, 23);
            this.simpleButton6.TabIndex = 6;
            this.simpleButton6.Text = "Day";
            // 
            // simpleButton7
            // 
            this.simpleButton7.Location = new System.Drawing.Point(29, 114);
            this.simpleButton7.Name = "simpleButton7";
            this.simpleButton7.Size = new System.Drawing.Size(49, 23);
            this.simpleButton7.TabIndex = 5;
            this.simpleButton7.Text = "Hour";
            // 
            // timeEdit1
            // 
            this.timeEdit1.EditValue = new System.DateTime(2020, 9, 7, 0, 0, 0, 0);
            this.timeEdit1.Location = new System.Drawing.Point(93, 169);
            this.timeEdit1.Name = "timeEdit1";
            this.timeEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEdit1.Properties.Mask.EditMask = "G";
            this.timeEdit1.Size = new System.Drawing.Size(180, 20);
            this.timeEdit1.TabIndex = 4;
            // 
            // timeEdit2
            // 
            this.timeEdit2.AllowDrop = true;
            this.timeEdit2.EditValue = new System.DateTime(2020, 9, 7, 0, 0, 0, 0);
            this.timeEdit2.Location = new System.Drawing.Point(93, 143);
            this.timeEdit2.Name = "timeEdit2";
            this.timeEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEdit2.Properties.Mask.EditMask = "G";
            this.timeEdit2.Size = new System.Drawing.Size(180, 20);
            this.timeEdit2.TabIndex = 3;
            // 
            // simpleButton8
            // 
            this.simpleButton8.Location = new System.Drawing.Point(191, 60);
            this.simpleButton8.Name = "simpleButton8";
            this.simpleButton8.Size = new System.Drawing.Size(79, 23);
            this.simpleButton8.TabIndex = 2;
            this.simpleButton8.Text = "Search";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(3, 7);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(103, 14);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "Control System :";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(12, 169);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(51, 14);
            this.labelControl5.TabIndex = 1;
            this.labelControl5.Text = "To Time:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(13, 143);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(66, 14);
            this.labelControl6.TabIndex = 1;
            this.labelControl6.Text = "From Time:";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.EditValue = "";
            this.comboBoxEdit1.Location = new System.Drawing.Point(112, 5);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(158, 20);
            this.comboBoxEdit1.TabIndex = 0;
            // 
            // accordionControl2
            // 
            this.accordionControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accordionControl2.ExpandElementMode = DevExpress.XtraBars.Navigation.ExpandElementMode.Multiple;
            this.accordionControl2.Location = new System.Drawing.Point(0, 0);
            this.accordionControl2.Name = "accordionControl2";
            this.accordionControl2.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.False;
            this.accordionControl2.Size = new System.Drawing.Size(0, 0);
            this.accordionControl2.TabIndex = 0;
            this.accordionControl2.Text = "accordionControl2";
            // 
            // EqpPlanOperationRate
            // 
            this.EqpPlanOperationRate.Controls.Add(this.splitContainerControl15);
            this.EqpPlanOperationRate.Name = "EqpPlanOperationRate";
            this.EqpPlanOperationRate.Size = new System.Drawing.Size(1469, 911);
            this.EqpPlanOperationRate.Text = "Eqp Operation Rate";
            // 
            // splitContainerControl15
            // 
            this.splitContainerControl15.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl15.Appearance.Options.UseBackColor = true;
            this.splitContainerControl15.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl15.Horizontal = false;
            this.splitContainerControl15.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl15.Name = "splitContainerControl15";
            this.splitContainerControl15.Panel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl15.Panel1.Appearance.Options.UseBackColor = true;
            this.splitContainerControl15.Panel1.Controls.Add(this.splitContainerControl1);
            this.splitContainerControl15.Panel1.Controls.Add(this.simpleButton18);
            this.splitContainerControl15.Panel1.Controls.Add(this.simpleButton27);
            this.splitContainerControl15.Panel1.Controls.Add(this.simpleButton28);
            this.splitContainerControl15.Panel1.Controls.Add(this.simpleButton29);
            this.splitContainerControl15.Panel1.Controls.Add(this.simpleButton30);
            this.splitContainerControl15.Panel1.Controls.Add(this.simpleButton31);
            this.splitContainerControl15.Panel1.Controls.Add(this.simpleButton32);
            this.splitContainerControl15.Panel1.Controls.Add(this.timeEdit7);
            this.splitContainerControl15.Panel1.Controls.Add(this.timeEdit8);
            this.splitContainerControl15.Panel1.Controls.Add(this.simpleButton33);
            this.splitContainerControl15.Panel1.Controls.Add(this.labelControl43);
            this.splitContainerControl15.Panel1.Controls.Add(this.labelControl44);
            this.splitContainerControl15.Panel1.Controls.Add(this.labelControl45);
            this.splitContainerControl15.Panel1.Controls.Add(this.comboBoxEdit6);
            this.splitContainerControl15.Panel1.Text = "Panel1";
            this.splitContainerControl15.Panel2.Controls.Add(this.accordionControl6);
            this.splitContainerControl15.Panel2.Text = "Panel2";
            this.splitContainerControl15.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            this.splitContainerControl15.Size = new System.Drawing.Size(1469, 911);
            this.splitContainerControl15.SplitterPosition = 540;
            this.splitContainerControl15.TabIndex = 3;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl1.Appearance.Options.UseBackColor = true;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl3);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1465, 907);
            this.splitContainerControl1.SplitterPosition = 278;
            this.splitContainerControl1.TabIndex = 12;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl2.Appearance.Options.UseBackColor = true;
            this.splitContainerControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl2.Panel1.Appearance.Options.UseBackColor = true;
            this.splitContainerControl2.Panel1.Controls.Add(this.btnInquiryTargetEqpPlanOperationRate);
            this.splitContainerControl2.Panel1.Controls.Add(this.btnInquiryTargetEqpPlanOperationRateBy);
            this.splitContainerControl2.Panel1.Controls.Add(this.btnPrevTimeEqpPlanOperationRate);
            this.splitContainerControl2.Panel1.Controls.Add(this.btnNextTimeEqpPlanOperationRate);
            this.splitContainerControl2.Panel1.Controls.Add(this.btnMonthUnitEqpPlanOperationRate);
            this.splitContainerControl2.Panel1.Controls.Add(this.btnWeekUnitEqpPlanOperationRate);
            this.splitContainerControl2.Panel1.Controls.Add(this.btnDayUnitEqpPlanOperationRate);
            this.splitContainerControl2.Panel1.Controls.Add(this.btnHourUnitEqpPlanOperationRate);
            this.splitContainerControl2.Panel1.Controls.Add(this.timeEditToEqpPlanOperationRate);
            this.splitContainerControl2.Panel1.Controls.Add(this.timeEditFromEqpPlanOperationRate);
            this.splitContainerControl2.Panel1.Controls.Add(this.btnSearchEqpPlanOperationRate);
            this.splitContainerControl2.Panel1.Controls.Add(this.labelControl1);
            this.splitContainerControl2.Panel1.Controls.Add(this.labelControl2);
            this.splitContainerControl2.Panel1.Controls.Add(this.labelControl3);
            this.splitContainerControl2.Panel1.Controls.Add(this.cboxEditEqpPlanOperationRate);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.accordionControl1);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(278, 907);
            this.splitContainerControl2.SplitterPosition = 197;
            this.splitContainerControl2.TabIndex = 1;
            // 
            // btnInquiryTargetEqpPlanOperationRate
            // 
            this.btnInquiryTargetEqpPlanOperationRate.Location = new System.Drawing.Point(110, 57);
            this.btnInquiryTargetEqpPlanOperationRate.Name = "btnInquiryTargetEqpPlanOperationRate";
            this.btnInquiryTargetEqpPlanOperationRate.Size = new System.Drawing.Size(158, 23);
            this.btnInquiryTargetEqpPlanOperationRate.TabIndex = 12;
            this.btnInquiryTargetEqpPlanOperationRate.Text = "Select Equipments";
            this.btnInquiryTargetEqpPlanOperationRate.Click += new System.EventHandler(this.btnGroupInquiryTarget_Click);
            // 
            // btnInquiryTargetEqpPlanOperationRateBy
            // 
            this.btnInquiryTargetEqpPlanOperationRateBy.Location = new System.Drawing.Point(110, 28);
            this.btnInquiryTargetEqpPlanOperationRateBy.Name = "btnInquiryTargetEqpPlanOperationRateBy";
            this.btnInquiryTargetEqpPlanOperationRateBy.Size = new System.Drawing.Size(158, 23);
            this.btnInquiryTargetEqpPlanOperationRateBy.TabIndex = 11;
            this.btnInquiryTargetEqpPlanOperationRateBy.Text = "Select Groups";
            this.btnInquiryTargetEqpPlanOperationRateBy.Click += new System.EventHandler(this.btnInquiryTarget_Click);
            // 
            // btnPrevTimeEqpPlanOperationRate
            // 
            this.btnPrevTimeEqpPlanOperationRate.Location = new System.Drawing.Point(3, 112);
            this.btnPrevTimeEqpPlanOperationRate.Name = "btnPrevTimeEqpPlanOperationRate";
            this.btnPrevTimeEqpPlanOperationRate.Size = new System.Drawing.Size(19, 23);
            this.btnPrevTimeEqpPlanOperationRate.TabIndex = 10;
            this.btnPrevTimeEqpPlanOperationRate.Text = "◁";
            this.btnPrevTimeEqpPlanOperationRate.Click += new System.EventHandler(this.btnPrevTime_Click);
            // 
            // btnNextTimeEqpPlanOperationRate
            // 
            this.btnNextTimeEqpPlanOperationRate.Location = new System.Drawing.Point(249, 112);
            this.btnNextTimeEqpPlanOperationRate.Name = "btnNextTimeEqpPlanOperationRate";
            this.btnNextTimeEqpPlanOperationRate.Size = new System.Drawing.Size(19, 23);
            this.btnNextTimeEqpPlanOperationRate.TabIndex = 9;
            this.btnNextTimeEqpPlanOperationRate.Text = "▷";
            this.btnNextTimeEqpPlanOperationRate.Click += new System.EventHandler(this.btnNextTime_Click);
            // 
            // btnMonthUnitEqpPlanOperationRate
            // 
            this.btnMonthUnitEqpPlanOperationRate.Location = new System.Drawing.Point(194, 112);
            this.btnMonthUnitEqpPlanOperationRate.Name = "btnMonthUnitEqpPlanOperationRate";
            this.btnMonthUnitEqpPlanOperationRate.Size = new System.Drawing.Size(49, 23);
            this.btnMonthUnitEqpPlanOperationRate.TabIndex = 8;
            this.btnMonthUnitEqpPlanOperationRate.Text = "Month";
            this.btnMonthUnitEqpPlanOperationRate.Click += new System.EventHandler(this.btnMonthUnit_Click);
            // 
            // btnWeekUnitEqpPlanOperationRate
            // 
            this.btnWeekUnitEqpPlanOperationRate.Location = new System.Drawing.Point(139, 112);
            this.btnWeekUnitEqpPlanOperationRate.Name = "btnWeekUnitEqpPlanOperationRate";
            this.btnWeekUnitEqpPlanOperationRate.Size = new System.Drawing.Size(49, 23);
            this.btnWeekUnitEqpPlanOperationRate.TabIndex = 7;
            this.btnWeekUnitEqpPlanOperationRate.Text = "Week";
            this.btnWeekUnitEqpPlanOperationRate.Click += new System.EventHandler(this.btnWeekUnit_Click);
            // 
            // btnDayUnitEqpPlanOperationRate
            // 
            this.btnDayUnitEqpPlanOperationRate.Location = new System.Drawing.Point(82, 112);
            this.btnDayUnitEqpPlanOperationRate.Name = "btnDayUnitEqpPlanOperationRate";
            this.btnDayUnitEqpPlanOperationRate.Size = new System.Drawing.Size(49, 23);
            this.btnDayUnitEqpPlanOperationRate.TabIndex = 6;
            this.btnDayUnitEqpPlanOperationRate.Text = "Day";
            this.btnDayUnitEqpPlanOperationRate.Click += new System.EventHandler(this.btnDayUnit_Click);
            // 
            // btnHourUnitEqpPlanOperationRate
            // 
            this.btnHourUnitEqpPlanOperationRate.Location = new System.Drawing.Point(27, 112);
            this.btnHourUnitEqpPlanOperationRate.Name = "btnHourUnitEqpPlanOperationRate";
            this.btnHourUnitEqpPlanOperationRate.Size = new System.Drawing.Size(49, 23);
            this.btnHourUnitEqpPlanOperationRate.TabIndex = 5;
            this.btnHourUnitEqpPlanOperationRate.Text = "Hour";
            this.btnHourUnitEqpPlanOperationRate.Click += new System.EventHandler(this.btnHourUnit_Click);
            // 
            // timeEditToEqpPlanOperationRate
            // 
            this.timeEditToEqpPlanOperationRate.EditValue = new System.DateTime(2020, 9, 7, 0, 0, 0, 0);
            this.timeEditToEqpPlanOperationRate.Location = new System.Drawing.Point(91, 167);
            this.timeEditToEqpPlanOperationRate.Name = "timeEditToEqpPlanOperationRate";
            this.timeEditToEqpPlanOperationRate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEditToEqpPlanOperationRate.Properties.Mask.EditMask = "G";
            this.timeEditToEqpPlanOperationRate.Size = new System.Drawing.Size(180, 20);
            this.timeEditToEqpPlanOperationRate.TabIndex = 4;
            // 
            // timeEditFromEqpPlanOperationRate
            // 
            this.timeEditFromEqpPlanOperationRate.AllowDrop = true;
            this.timeEditFromEqpPlanOperationRate.EditValue = new System.DateTime(2020, 9, 7, 0, 0, 0, 0);
            this.timeEditFromEqpPlanOperationRate.Location = new System.Drawing.Point(91, 141);
            this.timeEditFromEqpPlanOperationRate.Name = "timeEditFromEqpPlanOperationRate";
            this.timeEditFromEqpPlanOperationRate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEditFromEqpPlanOperationRate.Properties.Mask.EditMask = "G";
            this.timeEditFromEqpPlanOperationRate.Size = new System.Drawing.Size(180, 20);
            this.timeEditFromEqpPlanOperationRate.TabIndex = 3;
            // 
            // btnSearchEqpPlanOperationRate
            // 
            this.btnSearchEqpPlanOperationRate.Location = new System.Drawing.Point(189, 85);
            this.btnSearchEqpPlanOperationRate.Name = "btnSearchEqpPlanOperationRate";
            this.btnSearchEqpPlanOperationRate.Size = new System.Drawing.Size(79, 23);
            this.btnSearchEqpPlanOperationRate.TabIndex = 2;
            this.btnSearchEqpPlanOperationRate.Text = "Search";
            this.btnSearchEqpPlanOperationRate.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(36, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Group By:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(10, 167);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "To Time:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(11, 141);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(66, 14);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "From Time:";
            // 
            // cboxEditEqpPlanOperationRate
            // 
            this.cboxEditEqpPlanOperationRate.EditValue = "";
            this.cboxEditEqpPlanOperationRate.Location = new System.Drawing.Point(110, 3);
            this.cboxEditEqpPlanOperationRate.Name = "cboxEditEqpPlanOperationRate";
            this.cboxEditEqpPlanOperationRate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxEditEqpPlanOperationRate.Properties.Items.AddRange(new object[] {
            "EQP_GROUP",
            "STEP_GROUP"});
            this.cboxEditEqpPlanOperationRate.Size = new System.Drawing.Size(158, 20);
            this.cboxEditEqpPlanOperationRate.TabIndex = 0;
            this.cboxEditEqpPlanOperationRate.SelectedIndexChanged += new System.EventHandler(this.cboxEditEqpPlanOperationRate_SelectedIndexChanged);
            // 
            // accordionControl1
            // 
            this.accordionControl1.Controls.Add(this.accordionContentContainer1);
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1});
            this.accordionControl1.ExpandElementMode = DevExpress.XtraBars.Navigation.ExpandElementMode.Multiple;
            this.accordionControl1.Location = new System.Drawing.Point(0, 0);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.False;
            this.accordionControl1.Size = new System.Drawing.Size(274, 696);
            this.accordionControl1.TabIndex = 0;
            this.accordionControl1.Text = "accordionControl1";
            // 
            // accordionContentContainer1
            // 
            this.accordionContentContainer1.Controls.Add(this.labelControl7);
            this.accordionContentContainer1.Controls.Add(this.numericUpDownTotalTimeYMinEqpPlanOperationRate);
            this.accordionContentContainer1.Controls.Add(this.labelControl8);
            this.accordionContentContainer1.Controls.Add(this.numericUpDownTotalTimeYMaxEqpPlanOperationRate);
            this.accordionContentContainer1.Controls.Add(this.labelControl9);
            this.accordionContentContainer1.Controls.Add(this.numericUpDownCommandCountYMinEqpPlanOperationRate);
            this.accordionContentContainer1.Controls.Add(this.labelControl10);
            this.accordionContentContainer1.Controls.Add(this.numericUpDownCommandCountYMaxEqpPlanOperationRate);
            this.accordionContentContainer1.Controls.Add(this.labelControl11);
            this.accordionContentContainer1.Controls.Add(this.labelControl12);
            this.accordionContentContainer1.Controls.Add(this.labelControl13);
            this.accordionContentContainer1.Controls.Add(this.elapsedMaxEqpPlanOperationRate);
            this.accordionContentContainer1.Controls.Add(this.elapsedMinEqpPlanOperationRate);
            this.accordionContentContainer1.Controls.Add(this.labelControl14);
            this.accordionContentContainer1.Controls.Add(this.labelControl15);
            this.accordionContentContainer1.Controls.Add(this.cboxEditTimeIntervalEqpPlanOperationRate);
            this.accordionContentContainer1.Name = "accordionContentContainer1";
            this.accordionContentContainer1.Size = new System.Drawing.Size(257, 307);
            this.accordionContentContainer1.TabIndex = 6;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(17, 184);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(209, 14);
            this.labelControl7.TabIndex = 65;
            this.labelControl7.Text = "Eqp Operating Rate Chart Setting";
            // 
            // numericUpDownTotalTimeYMinEqpPlanOperationRate
            // 
            this.numericUpDownTotalTimeYMinEqpPlanOperationRate.DecimalPlaces = 1;
            this.numericUpDownTotalTimeYMinEqpPlanOperationRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownTotalTimeYMinEqpPlanOperationRate.Location = new System.Drawing.Point(129, 242);
            this.numericUpDownTotalTimeYMinEqpPlanOperationRate.Name = "numericUpDownTotalTimeYMinEqpPlanOperationRate";
            this.numericUpDownTotalTimeYMinEqpPlanOperationRate.Size = new System.Drawing.Size(103, 22);
            this.numericUpDownTotalTimeYMinEqpPlanOperationRate.TabIndex = 64;
            this.numericUpDownTotalTimeYMinEqpPlanOperationRate.ValueChanged += new System.EventHandler(this.numericUpDownTotalTimeYMin_ValueChanged);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(9, 244);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(40, 14);
            this.labelControl8.TabIndex = 63;
            this.labelControl8.Text = "Y축 Min";
            // 
            // numericUpDownTotalTimeYMaxEqpPlanOperationRate
            // 
            this.numericUpDownTotalTimeYMaxEqpPlanOperationRate.DecimalPlaces = 1;
            this.numericUpDownTotalTimeYMaxEqpPlanOperationRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownTotalTimeYMaxEqpPlanOperationRate.Location = new System.Drawing.Point(129, 210);
            this.numericUpDownTotalTimeYMaxEqpPlanOperationRate.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDownTotalTimeYMaxEqpPlanOperationRate.Name = "numericUpDownTotalTimeYMaxEqpPlanOperationRate";
            this.numericUpDownTotalTimeYMaxEqpPlanOperationRate.Size = new System.Drawing.Size(103, 22);
            this.numericUpDownTotalTimeYMaxEqpPlanOperationRate.TabIndex = 62;
            this.numericUpDownTotalTimeYMaxEqpPlanOperationRate.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownTotalTimeYMaxEqpPlanOperationRate.ValueChanged += new System.EventHandler(this.numericUpDowntTotalTimeYMax_ValueChanged);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(9, 212);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(43, 14);
            this.labelControl9.TabIndex = 61;
            this.labelControl9.Text = "Y축 Max";
            // 
            // numericUpDownCommandCountYMinEqpPlanOperationRate
            // 
            this.numericUpDownCommandCountYMinEqpPlanOperationRate.Location = new System.Drawing.Point(129, 153);
            this.numericUpDownCommandCountYMinEqpPlanOperationRate.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownCommandCountYMinEqpPlanOperationRate.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownCommandCountYMinEqpPlanOperationRate.Name = "numericUpDownCommandCountYMinEqpPlanOperationRate";
            this.numericUpDownCommandCountYMinEqpPlanOperationRate.Size = new System.Drawing.Size(103, 22);
            this.numericUpDownCommandCountYMinEqpPlanOperationRate.TabIndex = 60;
            this.numericUpDownCommandCountYMinEqpPlanOperationRate.ValueChanged += new System.EventHandler(this.numericUpDownYMin_ValueChanged);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(9, 155);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(40, 14);
            this.labelControl10.TabIndex = 59;
            this.labelControl10.Text = "Y축 Min";
            // 
            // numericUpDownCommandCountYMaxEqpPlanOperationRate
            // 
            this.numericUpDownCommandCountYMaxEqpPlanOperationRate.Location = new System.Drawing.Point(129, 121);
            this.numericUpDownCommandCountYMaxEqpPlanOperationRate.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownCommandCountYMaxEqpPlanOperationRate.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownCommandCountYMaxEqpPlanOperationRate.Name = "numericUpDownCommandCountYMaxEqpPlanOperationRate";
            this.numericUpDownCommandCountYMaxEqpPlanOperationRate.Size = new System.Drawing.Size(103, 22);
            this.numericUpDownCommandCountYMaxEqpPlanOperationRate.TabIndex = 58;
            this.numericUpDownCommandCountYMaxEqpPlanOperationRate.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownCommandCountYMaxEqpPlanOperationRate.ValueChanged += new System.EventHandler(this.numericUpDownYMax_ValueChanged);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(9, 123);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(43, 14);
            this.labelControl11.TabIndex = 57;
            this.labelControl11.Text = "Y축 Max";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Location = new System.Drawing.Point(16, 92);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(218, 14);
            this.labelControl12.TabIndex = 56;
            this.labelControl12.Text = "Eqp Operating Count Chart Setting";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(17, 63);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(33, 14);
            this.labelControl13.TabIndex = 26;
            this.labelControl13.Text = "Group";
            // 
            // elapsedMaxEqpPlanOperationRate
            // 
            this.elapsedMaxEqpPlanOperationRate.Location = new System.Drawing.Point(104, 31);
            this.elapsedMaxEqpPlanOperationRate.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.elapsedMaxEqpPlanOperationRate.Name = "elapsedMaxEqpPlanOperationRate";
            this.elapsedMaxEqpPlanOperationRate.Size = new System.Drawing.Size(120, 22);
            this.elapsedMaxEqpPlanOperationRate.TabIndex = 25;
            this.elapsedMaxEqpPlanOperationRate.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            // 
            // elapsedMinEqpPlanOperationRate
            // 
            this.elapsedMinEqpPlanOperationRate.Location = new System.Drawing.Point(104, 3);
            this.elapsedMinEqpPlanOperationRate.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.elapsedMinEqpPlanOperationRate.Name = "elapsedMinEqpPlanOperationRate";
            this.elapsedMinEqpPlanOperationRate.Size = new System.Drawing.Size(120, 22);
            this.elapsedMinEqpPlanOperationRate.TabIndex = 24;
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(16, 36);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(76, 14);
            this.labelControl14.TabIndex = 23;
            this.labelControl14.Text = "Elapsed (Max)";
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(17, 5);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(73, 14);
            this.labelControl15.TabIndex = 22;
            this.labelControl15.Text = "Elapsed (Min)";
            // 
            // cboxEditTimeIntervalEqpPlanOperationRate
            // 
            this.cboxEditTimeIntervalEqpPlanOperationRate.EditValue = "1Hour";
            this.cboxEditTimeIntervalEqpPlanOperationRate.Location = new System.Drawing.Point(103, 60);
            this.cboxEditTimeIntervalEqpPlanOperationRate.Name = "cboxEditTimeIntervalEqpPlanOperationRate";
            this.cboxEditTimeIntervalEqpPlanOperationRate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxEditTimeIntervalEqpPlanOperationRate.Properties.Items.AddRange(new object[] {
            "1Min",
            "10Min",
            "1Hour",
            "1Day"});
            this.cboxEditTimeIntervalEqpPlanOperationRate.Size = new System.Drawing.Size(121, 20);
            this.cboxEditTimeIntervalEqpPlanOperationRate.TabIndex = 21;
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.ContentContainer = this.accordionContentContainer1;
            this.accordionControlElement1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement2});
            this.accordionControlElement1.Expanded = true;
            this.accordionControlElement1.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement1.Text = "ETC";
            // 
            // accordionControlElement2
            // 
            this.accordionControlElement2.Expanded = true;
            this.accordionControlElement2.Name = "accordionControlElement2";
            this.accordionControlElement2.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement2.Text = "Element9";
            this.accordionControlElement2.Visible = false;
            // 
            // splitContainerControl3
            // 
            this.splitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl3.Horizontal = false;
            this.splitContainerControl3.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl3.Name = "splitContainerControl3";
            this.splitContainerControl3.Panel1.Controls.Add(this.label1);
            this.splitContainerControl3.Panel1.Controls.Add(this.toggleEqpPlanOperationRate);
            this.splitContainerControl3.Panel1.Controls.Add(this.btnGridSaveEqpPlanOperationRate);
            this.splitContainerControl3.Panel1.Text = "Panel1";
            this.splitContainerControl3.Panel2.Controls.Add(this.splitContainerControl6);
            this.splitContainerControl3.Panel2.Text = "Panel2";
            this.splitContainerControl3.Size = new System.Drawing.Size(1177, 907);
            this.splitContainerControl3.SplitterPosition = 30;
            this.splitContainerControl3.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(995, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "Total Time Label";
            // 
            // toggleEqpPlanOperationRate
            // 
            this.toggleEqpPlanOperationRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toggleEqpPlanOperationRate.EditValue = true;
            this.toggleEqpPlanOperationRate.Location = new System.Drawing.Point(1099, 9);
            this.toggleEqpPlanOperationRate.Name = "toggleEqpPlanOperationRate";
            this.toggleEqpPlanOperationRate.Properties.OffText = "Off";
            this.toggleEqpPlanOperationRate.Properties.OnText = "On";
            this.toggleEqpPlanOperationRate.Size = new System.Drawing.Size(95, 19);
            this.toggleEqpPlanOperationRate.TabIndex = 8;
            this.toggleEqpPlanOperationRate.Toggled += new System.EventHandler(this.labelSwitch_Toggled);
            // 
            // btnGridSaveEqpPlanOperationRate
            // 
            this.btnGridSaveEqpPlanOperationRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGridSaveEqpPlanOperationRate.Location = new System.Drawing.Point(734, 7);
            this.btnGridSaveEqpPlanOperationRate.Name = "btnGridSaveEqpPlanOperationRate";
            this.btnGridSaveEqpPlanOperationRate.Size = new System.Drawing.Size(240, 23);
            this.btnGridSaveEqpPlanOperationRate.TabIndex = 5;
            this.btnGridSaveEqpPlanOperationRate.Text = "Export to Excel";
            this.btnGridSaveEqpPlanOperationRate.Click += new System.EventHandler(this.buttonGridSave_Click);
            // 
            // splitContainerControl6
            // 
            this.splitContainerControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl6.Horizontal = false;
            this.splitContainerControl6.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl6.Name = "splitContainerControl6";
            this.splitContainerControl6.Panel1.Controls.Add(this.chartControlEqpPlanOperationRate);
            this.splitContainerControl6.Panel1.Text = "Panel1";
            this.splitContainerControl6.Panel2.Controls.Add(this.gridControlEqpPlanOperationRate);
            this.splitContainerControl6.Panel2.Text = "Panel2";
            this.splitContainerControl6.Size = new System.Drawing.Size(1177, 867);
            this.splitContainerControl6.SplitterPosition = 280;
            this.splitContainerControl6.TabIndex = 0;
            // 
            // chartControlEqpPlanOperationRate
            // 
            xyDiagram2.AxisX.DateTimeScaleOptions.AggregateFunction = DevExpress.XtraCharts.AggregateFunction.None;
            xyDiagram2.AxisX.DateTimeScaleOptions.MeasureUnit = DevExpress.XtraCharts.DateTimeMeasureUnit.Minute;
            xyDiagram2.AxisX.Label.Angle = -90;
            xyDiagram2.AxisX.Label.TextPattern = "{A:MM/dd \nHH:mm}";
            xyDiagram2.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram2.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram2.AxisX.WholeRange.EndSideMargin = 20D;
            xyDiagram2.AxisX.WholeRange.SideMarginSizeUnit = DevExpress.XtraCharts.SideMarginSizeUnit.AxisRangePercentage;
            xyDiagram2.AxisX.WholeRange.StartSideMargin = 2D;
            xyDiagram2.AxisY.Tickmarks.MinorVisible = false;
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.WholeRange.SideMarginSizeUnit = DevExpress.XtraCharts.SideMarginSizeUnit.AxisRangePercentage;
            xyDiagram2.EnableAxisXScrolling = true;
            xyDiagram2.EnableAxisXZooming = true;
            xyDiagram2.EnableAxisYScrolling = true;
            xyDiagram2.EnableAxisYZooming = true;
            secondaryAxisY2.AxisID = 0;
            secondaryAxisY2.Label.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(80)))), ((int)(((byte)(77)))));
            secondaryAxisY2.Name = "Secondary AxisY 1";
            secondaryAxisY2.Tickmarks.MinorVisible = false;
            secondaryAxisY2.VisibleInPanesSerializable = "-1";
            xyDiagram2.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY2});
            this.chartControlEqpPlanOperationRate.Diagram = xyDiagram2;
            this.chartControlEqpPlanOperationRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControlEqpPlanOperationRate.Legend.Name = "Default Legend";
            this.chartControlEqpPlanOperationRate.Location = new System.Drawing.Point(0, 0);
            this.chartControlEqpPlanOperationRate.Name = "chartControlEqpPlanOperationRate";
            series3.ArgumentDataMember = "TIME";
            series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series3.Name = "Eqp Operating Count";
            series3.ValueDataMembersSerializable = "PartStepCount";
            series4.ArgumentDataMember = "TIME";
            series4.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            pointSeriesLabel2.Angle = 90;
            pointSeriesLabel2.TextPattern = "{V:f1}";
            series4.Label = pointSeriesLabel2;
            series4.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series4.Name = "Eqp Operating Rate (%)";
            series4.ValueDataMembersSerializable = "SimTotalTimeAvg";
            lineSeriesView2.AxisYName = "Secondary AxisY 1";
            series4.View = lineSeriesView2;
            this.chartControlEqpPlanOperationRate.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3,
        series4};
            this.chartControlEqpPlanOperationRate.Size = new System.Drawing.Size(1177, 280);
            this.chartControlEqpPlanOperationRate.TabIndex = 2;
            // 
            // gridControlEqpPlanOperationRate
            // 
            this.gridControlEqpPlanOperationRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlEqpPlanOperationRate.Location = new System.Drawing.Point(0, 0);
            this.gridControlEqpPlanOperationRate.MainView = this.gridViewEqpPlanOperationRate;
            this.gridControlEqpPlanOperationRate.Name = "gridControlEqpPlanOperationRate";
            this.gridControlEqpPlanOperationRate.Size = new System.Drawing.Size(1177, 577);
            this.gridControlEqpPlanOperationRate.TabIndex = 3;
            this.gridControlEqpPlanOperationRate.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewEqpPlanOperationRate});
            // 
            // gridViewEqpPlanOperationRate
            // 
            this.gridViewEqpPlanOperationRate.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn2,
            this.gridColumn7});
            this.gridViewEqpPlanOperationRate.GridControl = this.gridControlEqpPlanOperationRate;
            this.gridViewEqpPlanOperationRate.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewEqpPlanOperationRate.Name = "gridViewEqpPlanOperationRate";
            this.gridViewEqpPlanOperationRate.OptionsBehavior.Editable = false;
            this.gridViewEqpPlanOperationRate.OptionsBehavior.ReadOnly = true;
            this.gridViewEqpPlanOperationRate.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewEqpPlanOperationRate.OptionsMenu.ShowFooterItem = true;
            this.gridViewEqpPlanOperationRate.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.gridViewEqpPlanOperationRate.OptionsMenu.ShowSummaryItemMode = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewEqpPlanOperationRate.OptionsSelection.MultiSelect = true;
            this.gridViewEqpPlanOperationRate.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridViewEqpPlanOperationRate.OptionsView.ShowDetailButtons = false;
            this.gridViewEqpPlanOperationRate.DoubleClick += new System.EventHandler(this.gridViewStepTrend_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.Caption = "Time";
            this.gridColumn1.DisplayFormat.FormatString = "yyyy/MM/dd HH:mm:ss";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "Time";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "Time", "MIN"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "Time", "MAX"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "Time", "AVG"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Time", "SUM")});
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.Caption = "Eqp Operating Rate (%)";
            this.gridColumn3.FieldName = "EqpPlanOperatingRate";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "EqpPlanOperatingRate", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "EqpPlanOperatingRate", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "EqpPlanOperatingRate", "{0:0.##}")});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.Caption = "Eqp Idle Rate (%)";
            this.gridColumn4.FieldName = "EqpPlanIdleRate";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "EqpPlanIdleRate", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "EqpPlanIdleRate", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "EqpPlanIdleRate", "{0:0.##}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.Caption = "Eqp Operating Time (sec)";
            this.gridColumn5.FieldName = "EqpPlanOperatingTime";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "EqpPlanOperatingTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "EqpPlanOperatingTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "EqpPlanOperatingTime", "{0:0.##}")});
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn6.Caption = "Eqp Idle Time (sec)";
            this.gridColumn6.FieldName = "EqpPlanIdleTime";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "EqpPlanIdleTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "EqpPlanIdleTime", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "EqpPlanIdleTime", "{0:0.##}")});
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.Caption = "Eqp Count";
            this.gridColumn2.FieldName = "TotalEqpCount";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "TotalEqpCount", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "TotalEqpCount", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "TotalEqpCount", "{0:0.##}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalEqpCount", "{0:0.##}")});
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 5;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn7.Caption = "Eqp Operating Count";
            this.gridColumn7.FieldName = "EqpPlanOperatingCount";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "EqpPlanOperatingCount", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "EqpPlanOperatingCount", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "EqpPlanOperatingCount", "{0:0.##}")});
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            // 
            // simpleButton18
            // 
            this.simpleButton18.Location = new System.Drawing.Point(112, 31);
            this.simpleButton18.Name = "simpleButton18";
            this.simpleButton18.Size = new System.Drawing.Size(158, 23);
            this.simpleButton18.TabIndex = 11;
            this.simpleButton18.Text = "Inquiry Target";
            // 
            // simpleButton27
            // 
            this.simpleButton27.Location = new System.Drawing.Point(5, 114);
            this.simpleButton27.Name = "simpleButton27";
            this.simpleButton27.Size = new System.Drawing.Size(19, 23);
            this.simpleButton27.TabIndex = 10;
            this.simpleButton27.Text = "◁";
            // 
            // simpleButton28
            // 
            this.simpleButton28.Location = new System.Drawing.Point(251, 114);
            this.simpleButton28.Name = "simpleButton28";
            this.simpleButton28.Size = new System.Drawing.Size(19, 23);
            this.simpleButton28.TabIndex = 9;
            this.simpleButton28.Text = "▷";
            // 
            // simpleButton29
            // 
            this.simpleButton29.Location = new System.Drawing.Point(196, 114);
            this.simpleButton29.Name = "simpleButton29";
            this.simpleButton29.Size = new System.Drawing.Size(49, 23);
            this.simpleButton29.TabIndex = 8;
            this.simpleButton29.Text = "Month";
            // 
            // simpleButton30
            // 
            this.simpleButton30.Location = new System.Drawing.Point(141, 114);
            this.simpleButton30.Name = "simpleButton30";
            this.simpleButton30.Size = new System.Drawing.Size(49, 23);
            this.simpleButton30.TabIndex = 7;
            this.simpleButton30.Text = "Week";
            // 
            // simpleButton31
            // 
            this.simpleButton31.Location = new System.Drawing.Point(84, 114);
            this.simpleButton31.Name = "simpleButton31";
            this.simpleButton31.Size = new System.Drawing.Size(49, 23);
            this.simpleButton31.TabIndex = 6;
            this.simpleButton31.Text = "Day";
            // 
            // simpleButton32
            // 
            this.simpleButton32.Location = new System.Drawing.Point(29, 114);
            this.simpleButton32.Name = "simpleButton32";
            this.simpleButton32.Size = new System.Drawing.Size(49, 23);
            this.simpleButton32.TabIndex = 5;
            this.simpleButton32.Text = "Hour";
            // 
            // timeEdit7
            // 
            this.timeEdit7.EditValue = new System.DateTime(2020, 9, 7, 0, 0, 0, 0);
            this.timeEdit7.Location = new System.Drawing.Point(93, 169);
            this.timeEdit7.Name = "timeEdit7";
            this.timeEdit7.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEdit7.Properties.Mask.EditMask = "G";
            this.timeEdit7.Size = new System.Drawing.Size(180, 20);
            this.timeEdit7.TabIndex = 4;
            // 
            // timeEdit8
            // 
            this.timeEdit8.AllowDrop = true;
            this.timeEdit8.EditValue = new System.DateTime(2020, 9, 7, 0, 0, 0, 0);
            this.timeEdit8.Location = new System.Drawing.Point(93, 143);
            this.timeEdit8.Name = "timeEdit8";
            this.timeEdit8.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEdit8.Properties.Mask.EditMask = "G";
            this.timeEdit8.Size = new System.Drawing.Size(180, 20);
            this.timeEdit8.TabIndex = 3;
            // 
            // simpleButton33
            // 
            this.simpleButton33.Location = new System.Drawing.Point(191, 60);
            this.simpleButton33.Name = "simpleButton33";
            this.simpleButton33.Size = new System.Drawing.Size(79, 23);
            this.simpleButton33.TabIndex = 2;
            this.simpleButton33.Text = "Search";
            // 
            // labelControl43
            // 
            this.labelControl43.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl43.Appearance.Options.UseFont = true;
            this.labelControl43.Location = new System.Drawing.Point(3, 7);
            this.labelControl43.Name = "labelControl43";
            this.labelControl43.Size = new System.Drawing.Size(103, 14);
            this.labelControl43.TabIndex = 1;
            this.labelControl43.Text = "Control System :";
            // 
            // labelControl44
            // 
            this.labelControl44.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl44.Appearance.Options.UseFont = true;
            this.labelControl44.Location = new System.Drawing.Point(12, 169);
            this.labelControl44.Name = "labelControl44";
            this.labelControl44.Size = new System.Drawing.Size(51, 14);
            this.labelControl44.TabIndex = 1;
            this.labelControl44.Text = "To Time:";
            // 
            // labelControl45
            // 
            this.labelControl45.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl45.Appearance.Options.UseFont = true;
            this.labelControl45.Location = new System.Drawing.Point(13, 143);
            this.labelControl45.Name = "labelControl45";
            this.labelControl45.Size = new System.Drawing.Size(66, 14);
            this.labelControl45.TabIndex = 1;
            this.labelControl45.Text = "From Time:";
            // 
            // comboBoxEdit6
            // 
            this.comboBoxEdit6.EditValue = "";
            this.comboBoxEdit6.Location = new System.Drawing.Point(112, 5);
            this.comboBoxEdit6.Name = "comboBoxEdit6";
            this.comboBoxEdit6.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit6.Size = new System.Drawing.Size(158, 20);
            this.comboBoxEdit6.TabIndex = 0;
            // 
            // accordionControl6
            // 
            this.accordionControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accordionControl6.ExpandElementMode = DevExpress.XtraBars.Navigation.ExpandElementMode.Multiple;
            this.accordionControl6.Location = new System.Drawing.Point(0, 0);
            this.accordionControl6.Name = "accordionControl6";
            this.accordionControl6.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.False;
            this.accordionControl6.Size = new System.Drawing.Size(0, 0);
            this.accordionControl6.TabIndex = 0;
            this.accordionControl6.Text = "accordionControl2";
            // 
            // FactoryInoutTab
            // 
            this.FactoryInoutTab.Controls.Add(this.splitContainerControl10);
            this.FactoryInoutTab.Name = "FactoryInoutTab";
            this.FactoryInoutTab.Size = new System.Drawing.Size(1469, 911);
            this.FactoryInoutTab.Text = "Factory Input / Output";
            // 
            // splitContainerControl10
            // 
            this.splitContainerControl10.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl10.Appearance.Options.UseBackColor = true;
            this.splitContainerControl10.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl10.Horizontal = false;
            this.splitContainerControl10.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl10.Name = "splitContainerControl10";
            this.splitContainerControl10.Panel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl10.Panel1.Appearance.Options.UseBackColor = true;
            this.splitContainerControl10.Panel1.Controls.Add(this.splitContainerControl11);
            this.splitContainerControl10.Panel1.Controls.Add(this.simpleButton19);
            this.splitContainerControl10.Panel1.Controls.Add(this.simpleButton20);
            this.splitContainerControl10.Panel1.Controls.Add(this.simpleButton21);
            this.splitContainerControl10.Panel1.Controls.Add(this.simpleButton22);
            this.splitContainerControl10.Panel1.Controls.Add(this.simpleButton23);
            this.splitContainerControl10.Panel1.Controls.Add(this.simpleButton24);
            this.splitContainerControl10.Panel1.Controls.Add(this.simpleButton25);
            this.splitContainerControl10.Panel1.Controls.Add(this.timeEdit5);
            this.splitContainerControl10.Panel1.Controls.Add(this.timeEdit6);
            this.splitContainerControl10.Panel1.Controls.Add(this.simpleButton26);
            this.splitContainerControl10.Panel1.Controls.Add(this.labelControl40);
            this.splitContainerControl10.Panel1.Controls.Add(this.labelControl41);
            this.splitContainerControl10.Panel1.Controls.Add(this.labelControl42);
            this.splitContainerControl10.Panel1.Controls.Add(this.comboBoxEdit7);
            this.splitContainerControl10.Panel1.Text = "Panel1";
            this.splitContainerControl10.Panel2.Controls.Add(this.accordionControl5);
            this.splitContainerControl10.Panel2.Text = "Panel2";
            this.splitContainerControl10.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            this.splitContainerControl10.Size = new System.Drawing.Size(1469, 911);
            this.splitContainerControl10.SplitterPosition = 540;
            this.splitContainerControl10.TabIndex = 3;
            // 
            // splitContainerControl11
            // 
            this.splitContainerControl11.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl11.Appearance.Options.UseBackColor = true;
            this.splitContainerControl11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl11.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl11.Name = "splitContainerControl11";
            this.splitContainerControl11.Panel1.Controls.Add(this.splitContainerControl12);
            this.splitContainerControl11.Panel1.Text = "Panel1";
            this.splitContainerControl11.Panel2.Controls.Add(this.splitContainerControl13);
            this.splitContainerControl11.Panel2.Text = "Panel2";
            this.splitContainerControl11.Size = new System.Drawing.Size(1465, 907);
            this.splitContainerControl11.SplitterPosition = 278;
            this.splitContainerControl11.TabIndex = 12;
            // 
            // splitContainerControl12
            // 
            this.splitContainerControl12.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl12.Appearance.Options.UseBackColor = true;
            this.splitContainerControl12.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl12.Horizontal = false;
            this.splitContainerControl12.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl12.Name = "splitContainerControl12";
            this.splitContainerControl12.Panel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.splitContainerControl12.Panel1.Appearance.Options.UseBackColor = true;
            this.splitContainerControl12.Panel1.Controls.Add(this.btnInquiryTargetInout);
            this.splitContainerControl12.Panel1.Controls.Add(this.btnPrevTimeInout);
            this.splitContainerControl12.Panel1.Controls.Add(this.btnNextTimeInout);
            this.splitContainerControl12.Panel1.Controls.Add(this.btnMonthUnitInout);
            this.splitContainerControl12.Panel1.Controls.Add(this.btnWeekUnitInout);
            this.splitContainerControl12.Panel1.Controls.Add(this.btnDayUnitInout);
            this.splitContainerControl12.Panel1.Controls.Add(this.btnHourUnitInout);
            this.splitContainerControl12.Panel1.Controls.Add(this.timeEditToInout);
            this.splitContainerControl12.Panel1.Controls.Add(this.timeEditFromInout);
            this.splitContainerControl12.Panel1.Controls.Add(this.btnSearchInout);
            this.splitContainerControl12.Panel1.Controls.Add(this.labelControl28);
            this.splitContainerControl12.Panel1.Controls.Add(this.labelControl29);
            this.splitContainerControl12.Panel1.Controls.Add(this.labelControl30);
            this.splitContainerControl12.Panel1.Controls.Add(this.cboxEditInoutBy);
            this.splitContainerControl12.Panel1.Text = "Panel1";
            this.splitContainerControl12.Panel2.Controls.Add(this.accordionControl4);
            this.splitContainerControl12.Panel2.Text = "Panel2";
            this.splitContainerControl12.Size = new System.Drawing.Size(278, 907);
            this.splitContainerControl12.SplitterPosition = 197;
            this.splitContainerControl12.TabIndex = 1;
            // 
            // btnInquiryTargetInout
            // 
            this.btnInquiryTargetInout.Location = new System.Drawing.Point(110, 29);
            this.btnInquiryTargetInout.Name = "btnInquiryTargetInout";
            this.btnInquiryTargetInout.Size = new System.Drawing.Size(158, 23);
            this.btnInquiryTargetInout.TabIndex = 11;
            this.btnInquiryTargetInout.Text = "Select Targets";
            this.btnInquiryTargetInout.Click += new System.EventHandler(this.btnInquiryTarget_Click);
            // 
            // btnPrevTimeInout
            // 
            this.btnPrevTimeInout.Location = new System.Drawing.Point(3, 112);
            this.btnPrevTimeInout.Name = "btnPrevTimeInout";
            this.btnPrevTimeInout.Size = new System.Drawing.Size(19, 23);
            this.btnPrevTimeInout.TabIndex = 10;
            this.btnPrevTimeInout.Text = "◁";
            this.btnPrevTimeInout.Click += new System.EventHandler(this.btnPrevTime_Click);
            // 
            // btnNextTimeInout
            // 
            this.btnNextTimeInout.Location = new System.Drawing.Point(249, 112);
            this.btnNextTimeInout.Name = "btnNextTimeInout";
            this.btnNextTimeInout.Size = new System.Drawing.Size(19, 23);
            this.btnNextTimeInout.TabIndex = 9;
            this.btnNextTimeInout.Text = "▷";
            this.btnNextTimeInout.Click += new System.EventHandler(this.btnNextTime_Click);
            // 
            // btnMonthUnitInout
            // 
            this.btnMonthUnitInout.Location = new System.Drawing.Point(194, 112);
            this.btnMonthUnitInout.Name = "btnMonthUnitInout";
            this.btnMonthUnitInout.Size = new System.Drawing.Size(49, 23);
            this.btnMonthUnitInout.TabIndex = 8;
            this.btnMonthUnitInout.Text = "Month";
            this.btnMonthUnitInout.Click += new System.EventHandler(this.btnMonthUnit_Click);
            // 
            // btnWeekUnitInout
            // 
            this.btnWeekUnitInout.Location = new System.Drawing.Point(139, 112);
            this.btnWeekUnitInout.Name = "btnWeekUnitInout";
            this.btnWeekUnitInout.Size = new System.Drawing.Size(49, 23);
            this.btnWeekUnitInout.TabIndex = 7;
            this.btnWeekUnitInout.Text = "Week";
            this.btnWeekUnitInout.Click += new System.EventHandler(this.btnWeekUnit_Click);
            // 
            // btnDayUnitInout
            // 
            this.btnDayUnitInout.Location = new System.Drawing.Point(82, 112);
            this.btnDayUnitInout.Name = "btnDayUnitInout";
            this.btnDayUnitInout.Size = new System.Drawing.Size(49, 23);
            this.btnDayUnitInout.TabIndex = 6;
            this.btnDayUnitInout.Text = "Day";
            this.btnDayUnitInout.Click += new System.EventHandler(this.btnDayUnit_Click);
            // 
            // btnHourUnitInout
            // 
            this.btnHourUnitInout.Location = new System.Drawing.Point(27, 112);
            this.btnHourUnitInout.Name = "btnHourUnitInout";
            this.btnHourUnitInout.Size = new System.Drawing.Size(49, 23);
            this.btnHourUnitInout.TabIndex = 5;
            this.btnHourUnitInout.Text = "Hour";
            this.btnHourUnitInout.Click += new System.EventHandler(this.btnHourUnit_Click);
            // 
            // timeEditToInout
            // 
            this.timeEditToInout.EditValue = new System.DateTime(2020, 9, 7, 0, 0, 0, 0);
            this.timeEditToInout.Location = new System.Drawing.Point(91, 167);
            this.timeEditToInout.Name = "timeEditToInout";
            this.timeEditToInout.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEditToInout.Properties.Mask.EditMask = "G";
            this.timeEditToInout.Size = new System.Drawing.Size(180, 20);
            this.timeEditToInout.TabIndex = 4;
            // 
            // timeEditFromInout
            // 
            this.timeEditFromInout.AllowDrop = true;
            this.timeEditFromInout.EditValue = new System.DateTime(2020, 9, 7, 0, 0, 0, 0);
            this.timeEditFromInout.Location = new System.Drawing.Point(91, 141);
            this.timeEditFromInout.Name = "timeEditFromInout";
            this.timeEditFromInout.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEditFromInout.Properties.Mask.EditMask = "G";
            this.timeEditFromInout.Size = new System.Drawing.Size(180, 20);
            this.timeEditFromInout.TabIndex = 3;
            // 
            // btnSearchInout
            // 
            this.btnSearchInout.Location = new System.Drawing.Point(189, 58);
            this.btnSearchInout.Name = "btnSearchInout";
            this.btnSearchInout.Size = new System.Drawing.Size(79, 23);
            this.btnSearchInout.TabIndex = 2;
            this.btnSearchInout.Text = "Search";
            this.btnSearchInout.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labelControl28
            // 
            this.labelControl28.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl28.Appearance.Options.UseFont = true;
            this.labelControl28.Location = new System.Drawing.Point(36, 6);
            this.labelControl28.Name = "labelControl28";
            this.labelControl28.Size = new System.Drawing.Size(60, 14);
            this.labelControl28.TabIndex = 1;
            this.labelControl28.Text = "Group By:";
            // 
            // labelControl29
            // 
            this.labelControl29.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl29.Appearance.Options.UseFont = true;
            this.labelControl29.Location = new System.Drawing.Point(10, 167);
            this.labelControl29.Name = "labelControl29";
            this.labelControl29.Size = new System.Drawing.Size(51, 14);
            this.labelControl29.TabIndex = 1;
            this.labelControl29.Text = "To Time:";
            // 
            // labelControl30
            // 
            this.labelControl30.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl30.Appearance.Options.UseFont = true;
            this.labelControl30.Location = new System.Drawing.Point(11, 141);
            this.labelControl30.Name = "labelControl30";
            this.labelControl30.Size = new System.Drawing.Size(66, 14);
            this.labelControl30.TabIndex = 1;
            this.labelControl30.Text = "From Time:";
            // 
            // cboxEditInoutBy
            // 
            this.cboxEditInoutBy.EditValue = "";
            this.cboxEditInoutBy.Location = new System.Drawing.Point(110, 3);
            this.cboxEditInoutBy.Name = "cboxEditInoutBy";
            this.cboxEditInoutBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxEditInoutBy.Properties.Items.AddRange(new object[] {
            "Node",
            "Product"});
            this.cboxEditInoutBy.Size = new System.Drawing.Size(158, 20);
            this.cboxEditInoutBy.TabIndex = 0;
            this.cboxEditInoutBy.SelectedIndexChanged += new System.EventHandler(this.cboxEditInout_SelectedIndexChanged);
            // 
            // accordionControl4
            // 
            this.accordionControl4.Controls.Add(this.accordionContentContainer3);
            this.accordionControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accordionControl4.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement4});
            this.accordionControl4.ExpandElementMode = DevExpress.XtraBars.Navigation.ExpandElementMode.Multiple;
            this.accordionControl4.Location = new System.Drawing.Point(0, 0);
            this.accordionControl4.Name = "accordionControl4";
            this.accordionControl4.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.False;
            this.accordionControl4.Size = new System.Drawing.Size(274, 696);
            this.accordionControl4.TabIndex = 0;
            this.accordionControl4.Text = "accordionControl4";
            // 
            // accordionContentContainer3
            // 
            this.accordionContentContainer3.Controls.Add(this.labelControl31);
            this.accordionContentContainer3.Controls.Add(this.numericUpDownTotalTimeYMinInout);
            this.accordionContentContainer3.Controls.Add(this.labelControl32);
            this.accordionContentContainer3.Controls.Add(this.numericUpDownTotalTimeYMaxInout);
            this.accordionContentContainer3.Controls.Add(this.labelControl33);
            this.accordionContentContainer3.Controls.Add(this.numericUpDownCommandCountYMinInout);
            this.accordionContentContainer3.Controls.Add(this.labelControl34);
            this.accordionContentContainer3.Controls.Add(this.numericUpDownCommandCountYMaxInout);
            this.accordionContentContainer3.Controls.Add(this.labelControl35);
            this.accordionContentContainer3.Controls.Add(this.labelControl36);
            this.accordionContentContainer3.Controls.Add(this.labelControl37);
            this.accordionContentContainer3.Controls.Add(this.elapsedMaxInout);
            this.accordionContentContainer3.Controls.Add(this.elapsedMinInout);
            this.accordionContentContainer3.Controls.Add(this.labelControl38);
            this.accordionContentContainer3.Controls.Add(this.labelControl39);
            this.accordionContentContainer3.Controls.Add(this.cboxEditTimeIntervalInout);
            this.accordionContentContainer3.Name = "accordionContentContainer3";
            this.accordionContentContainer3.Size = new System.Drawing.Size(257, 307);
            this.accordionContentContainer3.TabIndex = 6;
            // 
            // labelControl31
            // 
            this.labelControl31.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl31.Appearance.Options.UseFont = true;
            this.labelControl31.Location = new System.Drawing.Point(17, 184);
            this.labelControl31.Name = "labelControl31";
            this.labelControl31.Size = new System.Drawing.Size(182, 14);
            this.labelControl31.TabIndex = 65;
            this.labelControl31.Text = "Factory Output Chart Setting";
            // 
            // numericUpDownTotalTimeYMinInout
            // 
            this.numericUpDownTotalTimeYMinInout.Location = new System.Drawing.Point(129, 242);
            this.numericUpDownTotalTimeYMinInout.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownTotalTimeYMinInout.Name = "numericUpDownTotalTimeYMinInout";
            this.numericUpDownTotalTimeYMinInout.Size = new System.Drawing.Size(103, 22);
            this.numericUpDownTotalTimeYMinInout.TabIndex = 64;
            this.numericUpDownTotalTimeYMinInout.ValueChanged += new System.EventHandler(this.numericUpDownTotalTimeYMin_ValueChanged);
            // 
            // labelControl32
            // 
            this.labelControl32.Location = new System.Drawing.Point(9, 244);
            this.labelControl32.Name = "labelControl32";
            this.labelControl32.Size = new System.Drawing.Size(40, 14);
            this.labelControl32.TabIndex = 63;
            this.labelControl32.Text = "Y축 Min";
            // 
            // numericUpDownTotalTimeYMaxInout
            // 
            this.numericUpDownTotalTimeYMaxInout.Location = new System.Drawing.Point(129, 210);
            this.numericUpDownTotalTimeYMaxInout.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownTotalTimeYMaxInout.Name = "numericUpDownTotalTimeYMaxInout";
            this.numericUpDownTotalTimeYMaxInout.Size = new System.Drawing.Size(103, 22);
            this.numericUpDownTotalTimeYMaxInout.TabIndex = 62;
            this.numericUpDownTotalTimeYMaxInout.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownTotalTimeYMaxInout.ValueChanged += new System.EventHandler(this.numericUpDownYMax_ValueChanged);
            // 
            // labelControl33
            // 
            this.labelControl33.Location = new System.Drawing.Point(9, 212);
            this.labelControl33.Name = "labelControl33";
            this.labelControl33.Size = new System.Drawing.Size(43, 14);
            this.labelControl33.TabIndex = 61;
            this.labelControl33.Text = "Y축 Max";
            // 
            // numericUpDownCommandCountYMinInout
            // 
            this.numericUpDownCommandCountYMinInout.Location = new System.Drawing.Point(129, 153);
            this.numericUpDownCommandCountYMinInout.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownCommandCountYMinInout.Name = "numericUpDownCommandCountYMinInout";
            this.numericUpDownCommandCountYMinInout.Size = new System.Drawing.Size(103, 22);
            this.numericUpDownCommandCountYMinInout.TabIndex = 60;
            this.numericUpDownCommandCountYMinInout.ValueChanged += new System.EventHandler(this.numericUpDownYMin_ValueChanged);
            // 
            // labelControl34
            // 
            this.labelControl34.Location = new System.Drawing.Point(9, 155);
            this.labelControl34.Name = "labelControl34";
            this.labelControl34.Size = new System.Drawing.Size(40, 14);
            this.labelControl34.TabIndex = 59;
            this.labelControl34.Text = "Y축 Min";
            // 
            // numericUpDownCommandCountYMaxInout
            // 
            this.numericUpDownCommandCountYMaxInout.Location = new System.Drawing.Point(129, 121);
            this.numericUpDownCommandCountYMaxInout.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownCommandCountYMaxInout.Name = "numericUpDownCommandCountYMaxInout";
            this.numericUpDownCommandCountYMaxInout.Size = new System.Drawing.Size(103, 22);
            this.numericUpDownCommandCountYMaxInout.TabIndex = 58;
            this.numericUpDownCommandCountYMaxInout.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownCommandCountYMaxInout.ValueChanged += new System.EventHandler(this.numericUpDowntTotalTimeYMax_ValueChanged);
            // 
            // labelControl35
            // 
            this.labelControl35.Location = new System.Drawing.Point(9, 123);
            this.labelControl35.Name = "labelControl35";
            this.labelControl35.Size = new System.Drawing.Size(43, 14);
            this.labelControl35.TabIndex = 57;
            this.labelControl35.Text = "Y축 Max";
            // 
            // labelControl36
            // 
            this.labelControl36.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl36.Appearance.Options.UseFont = true;
            this.labelControl36.Location = new System.Drawing.Point(16, 92);
            this.labelControl36.Name = "labelControl36";
            this.labelControl36.Size = new System.Drawing.Size(172, 14);
            this.labelControl36.TabIndex = 56;
            this.labelControl36.Text = "Factory Input Chart Setting";
            // 
            // labelControl37
            // 
            this.labelControl37.Location = new System.Drawing.Point(17, 63);
            this.labelControl37.Name = "labelControl37";
            this.labelControl37.Size = new System.Drawing.Size(33, 14);
            this.labelControl37.TabIndex = 26;
            this.labelControl37.Text = "Group";
            // 
            // elapsedMaxInout
            // 
            this.elapsedMaxInout.Location = new System.Drawing.Point(104, 31);
            this.elapsedMaxInout.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.elapsedMaxInout.Name = "elapsedMaxInout";
            this.elapsedMaxInout.Size = new System.Drawing.Size(120, 22);
            this.elapsedMaxInout.TabIndex = 25;
            this.elapsedMaxInout.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            // 
            // elapsedMinInout
            // 
            this.elapsedMinInout.Location = new System.Drawing.Point(104, 3);
            this.elapsedMinInout.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.elapsedMinInout.Name = "elapsedMinInout";
            this.elapsedMinInout.Size = new System.Drawing.Size(120, 22);
            this.elapsedMinInout.TabIndex = 24;
            // 
            // labelControl38
            // 
            this.labelControl38.Location = new System.Drawing.Point(16, 36);
            this.labelControl38.Name = "labelControl38";
            this.labelControl38.Size = new System.Drawing.Size(76, 14);
            this.labelControl38.TabIndex = 23;
            this.labelControl38.Text = "Elapsed (Max)";
            // 
            // labelControl39
            // 
            this.labelControl39.Location = new System.Drawing.Point(17, 5);
            this.labelControl39.Name = "labelControl39";
            this.labelControl39.Size = new System.Drawing.Size(73, 14);
            this.labelControl39.TabIndex = 22;
            this.labelControl39.Text = "Elapsed (Min)";
            // 
            // cboxEditTimeIntervalInout
            // 
            this.cboxEditTimeIntervalInout.EditValue = "1Hour";
            this.cboxEditTimeIntervalInout.Location = new System.Drawing.Point(103, 60);
            this.cboxEditTimeIntervalInout.Name = "cboxEditTimeIntervalInout";
            this.cboxEditTimeIntervalInout.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxEditTimeIntervalInout.Properties.Items.AddRange(new object[] {
            "1Min",
            "10Min",
            "1Hour",
            "1Day"});
            this.cboxEditTimeIntervalInout.Size = new System.Drawing.Size(121, 20);
            this.cboxEditTimeIntervalInout.TabIndex = 21;
            // 
            // accordionControlElement4
            // 
            this.accordionControlElement4.ContentContainer = this.accordionContentContainer3;
            this.accordionControlElement4.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement6});
            this.accordionControlElement4.Expanded = true;
            this.accordionControlElement4.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.accordionControlElement4.Name = "accordionControlElement4";
            this.accordionControlElement4.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement4.Text = "ETC";
            // 
            // accordionControlElement6
            // 
            this.accordionControlElement6.Expanded = true;
            this.accordionControlElement6.Name = "accordionControlElement6";
            this.accordionControlElement6.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement6.Text = "Element9";
            this.accordionControlElement6.Visible = false;
            // 
            // splitContainerControl13
            // 
            this.splitContainerControl13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl13.Horizontal = false;
            this.splitContainerControl13.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl13.Name = "splitContainerControl13";
            this.splitContainerControl13.Panel1.Controls.Add(this.label3);
            this.splitContainerControl13.Panel1.Controls.Add(this.toggleInout);
            this.splitContainerControl13.Panel1.Controls.Add(this.btnGridSaveInout);
            this.splitContainerControl13.Panel1.Text = "Panel1";
            this.splitContainerControl13.Panel2.Controls.Add(this.splitContainerControl14);
            this.splitContainerControl13.Panel2.Text = "Panel2";
            this.splitContainerControl13.Size = new System.Drawing.Size(1177, 907);
            this.splitContainerControl13.SplitterPosition = 30;
            this.splitContainerControl13.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.label3.Location = new System.Drawing.Point(995, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "Total Time Label";
            // 
            // toggleInout
            // 
            this.toggleInout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toggleInout.Enabled = false;
            this.toggleInout.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toggleInout.Location = new System.Drawing.Point(1099, 9);
            this.toggleInout.Name = "toggleInout";
            this.toggleInout.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.toggleInout.Properties.Appearance.Options.UseForeColor = true;
            this.toggleInout.Properties.OffText = "Off";
            this.toggleInout.Properties.OnText = "On";
            this.toggleInout.Size = new System.Drawing.Size(95, 19);
            this.toggleInout.TabIndex = 8;
            // 
            // btnGridSaveInout
            // 
            this.btnGridSaveInout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGridSaveInout.Location = new System.Drawing.Point(734, 7);
            this.btnGridSaveInout.Name = "btnGridSaveInout";
            this.btnGridSaveInout.Size = new System.Drawing.Size(240, 23);
            this.btnGridSaveInout.TabIndex = 5;
            this.btnGridSaveInout.Text = "Export to Excel";
            this.btnGridSaveInout.Click += new System.EventHandler(this.buttonGridSave_Click);
            // 
            // splitContainerControl14
            // 
            this.splitContainerControl14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl14.Horizontal = false;
            this.splitContainerControl14.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl14.Name = "splitContainerControl14";
            this.splitContainerControl14.Panel1.Controls.Add(this.chartControlInout);
            this.splitContainerControl14.Panel1.Text = "Panel1";
            this.splitContainerControl14.Panel2.Controls.Add(this.gridControlInout);
            this.splitContainerControl14.Panel2.Text = "Panel2";
            this.splitContainerControl14.Size = new System.Drawing.Size(1177, 867);
            this.splitContainerControl14.SplitterPosition = 280;
            this.splitContainerControl14.TabIndex = 0;
            // 
            // chartControlInout
            // 
            chartCalculatedField1.DisplayName = "CalculatedField1";
            chartCalculatedField1.Name = "CalculatedField1";
            this.chartControlInout.CalculatedFields.AddRange(new DevExpress.XtraCharts.ChartCalculatedField[] {
            chartCalculatedField1});
            xyDiagram3.AxisX.DateTimeScaleOptions.AggregateFunction = DevExpress.XtraCharts.AggregateFunction.None;
            xyDiagram3.AxisX.DateTimeScaleOptions.MeasureUnit = DevExpress.XtraCharts.DateTimeMeasureUnit.Minute;
            xyDiagram3.AxisX.Label.Angle = -90;
            xyDiagram3.AxisX.Label.TextPattern = "{A:MM/dd \nHH:mm}";
            xyDiagram3.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram3.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram3.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram3.AxisX.WholeRange.EndSideMargin = 20D;
            xyDiagram3.AxisX.WholeRange.SideMarginSizeUnit = DevExpress.XtraCharts.SideMarginSizeUnit.AxisRangePercentage;
            xyDiagram3.AxisX.WholeRange.StartSideMargin = 2D;
            xyDiagram3.AxisY.Tickmarks.MinorVisible = false;
            xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram3.AxisY.WholeRange.SideMarginSizeUnit = DevExpress.XtraCharts.SideMarginSizeUnit.AxisRangePercentage;
            xyDiagram3.EnableAxisXScrolling = true;
            xyDiagram3.EnableAxisXZooming = true;
            xyDiagram3.EnableAxisYScrolling = true;
            xyDiagram3.EnableAxisYZooming = true;
            secondaryAxisY3.AxisID = 0;
            secondaryAxisY3.Label.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(80)))), ((int)(((byte)(77)))));
            secondaryAxisY3.Name = "Secondary AxisY 1";
            secondaryAxisY3.Tickmarks.MinorVisible = false;
            secondaryAxisY3.VisibleInPanesSerializable = "-1";
            xyDiagram3.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY3});
            this.chartControlInout.Diagram = xyDiagram3;
            this.chartControlInout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControlInout.Legend.Name = "Default Legend";
            this.chartControlInout.Location = new System.Drawing.Point(0, 0);
            this.chartControlInout.Name = "chartControlInout";
            series5.ArgumentDataMember = "TIME";
            series5.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series5.Name = "Input Count";
            series5.ValueDataMembersSerializable = "InputCount";
            series6.ArgumentDataMember = "TIME";
            series6.Name = "Output Count";
            series6.ValueDataMembersSerializable = "OutputCount";
            this.chartControlInout.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5,
        series6};
            this.chartControlInout.Size = new System.Drawing.Size(1177, 280);
            this.chartControlInout.TabIndex = 2;
            // 
            // gridControlInout
            // 
            this.gridControlInout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlInout.Location = new System.Drawing.Point(0, 0);
            this.gridControlInout.MainView = this.gridViewInout;
            this.gridControlInout.Name = "gridControlInout";
            this.gridControlInout.Size = new System.Drawing.Size(1177, 577);
            this.gridControlInout.TabIndex = 4;
            this.gridControlInout.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewInout});
            // 
            // gridViewInout
            // 
            this.gridViewInout.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.InoutTime,
            this.InputCount,
            this.OutputCount,
            this.InOutTotalTimeAvg,
            this.InOutStdevTotalTime});
            this.gridViewInout.GridControl = this.gridControlInout;
            this.gridViewInout.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewInout.Name = "gridViewInout";
            this.gridViewInout.OptionsBehavior.Editable = false;
            this.gridViewInout.OptionsBehavior.ReadOnly = true;
            this.gridViewInout.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewInout.OptionsMenu.ShowFooterItem = true;
            this.gridViewInout.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.gridViewInout.OptionsMenu.ShowSummaryItemMode = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewInout.OptionsSelection.MultiSelect = true;
            this.gridViewInout.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridViewInout.OptionsView.ShowDetailButtons = false;
            // 
            // InoutTime
            // 
            this.InoutTime.AppearanceCell.Options.UseTextOptions = true;
            this.InoutTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.InoutTime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.InoutTime.AppearanceHeader.Options.UseTextOptions = true;
            this.InoutTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.InoutTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.InoutTime.Caption = "Time";
            this.InoutTime.DisplayFormat.FormatString = "yyyy/MM/dd HH:mm:ss";
            this.InoutTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.InoutTime.FieldName = "InoutTime";
            this.InoutTime.Name = "InoutTime";
            this.InoutTime.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "InoutTime", "MIN"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "InoutTime", "MAX"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "InoutTime", "AVG"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InoutTime", "SUM")});
            this.InoutTime.Visible = true;
            this.InoutTime.VisibleIndex = 0;
            // 
            // InputCount
            // 
            this.InputCount.AppearanceCell.Options.UseTextOptions = true;
            this.InputCount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.InputCount.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.InputCount.AppearanceHeader.Options.UseTextOptions = true;
            this.InputCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.InputCount.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.InputCount.Caption = "Input Count";
            this.InputCount.FieldName = "InputCount";
            this.InputCount.Name = "InputCount";
            this.InputCount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "InputCount", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "InputCount", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "InputCount", "{0:0.##}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InputCount", "{0:0.##}")});
            this.InputCount.Visible = true;
            this.InputCount.VisibleIndex = 1;
            // 
            // OutputCount
            // 
            this.OutputCount.AppearanceCell.Options.UseTextOptions = true;
            this.OutputCount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.OutputCount.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.OutputCount.AppearanceHeader.Options.UseTextOptions = true;
            this.OutputCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.OutputCount.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.OutputCount.Caption = "Output Count";
            this.OutputCount.FieldName = "OutputCount";
            this.OutputCount.Name = "OutputCount";
            this.OutputCount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "OutputCount", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "OutputCount", "{0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "OutputCount", "{0:0.##}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "OutputCount", "{0:0.##}")});
            this.OutputCount.Visible = true;
            this.OutputCount.VisibleIndex = 2;
            // 
            // simpleButton19
            // 
            this.simpleButton19.Location = new System.Drawing.Point(112, 31);
            this.simpleButton19.Name = "simpleButton19";
            this.simpleButton19.Size = new System.Drawing.Size(158, 23);
            this.simpleButton19.TabIndex = 11;
            this.simpleButton19.Text = "Inquiry Target";
            // 
            // simpleButton20
            // 
            this.simpleButton20.Location = new System.Drawing.Point(5, 114);
            this.simpleButton20.Name = "simpleButton20";
            this.simpleButton20.Size = new System.Drawing.Size(19, 23);
            this.simpleButton20.TabIndex = 10;
            this.simpleButton20.Text = "◁";
            // 
            // simpleButton21
            // 
            this.simpleButton21.Location = new System.Drawing.Point(251, 114);
            this.simpleButton21.Name = "simpleButton21";
            this.simpleButton21.Size = new System.Drawing.Size(19, 23);
            this.simpleButton21.TabIndex = 9;
            this.simpleButton21.Text = "▷";
            // 
            // simpleButton22
            // 
            this.simpleButton22.Location = new System.Drawing.Point(196, 114);
            this.simpleButton22.Name = "simpleButton22";
            this.simpleButton22.Size = new System.Drawing.Size(49, 23);
            this.simpleButton22.TabIndex = 8;
            this.simpleButton22.Text = "Month";
            // 
            // simpleButton23
            // 
            this.simpleButton23.Location = new System.Drawing.Point(141, 114);
            this.simpleButton23.Name = "simpleButton23";
            this.simpleButton23.Size = new System.Drawing.Size(49, 23);
            this.simpleButton23.TabIndex = 7;
            this.simpleButton23.Text = "Week";
            // 
            // simpleButton24
            // 
            this.simpleButton24.Location = new System.Drawing.Point(84, 114);
            this.simpleButton24.Name = "simpleButton24";
            this.simpleButton24.Size = new System.Drawing.Size(49, 23);
            this.simpleButton24.TabIndex = 6;
            this.simpleButton24.Text = "Day";
            // 
            // simpleButton25
            // 
            this.simpleButton25.Location = new System.Drawing.Point(29, 114);
            this.simpleButton25.Name = "simpleButton25";
            this.simpleButton25.Size = new System.Drawing.Size(49, 23);
            this.simpleButton25.TabIndex = 5;
            this.simpleButton25.Text = "Hour";
            // 
            // timeEdit5
            // 
            this.timeEdit5.EditValue = new System.DateTime(2020, 9, 7, 0, 0, 0, 0);
            this.timeEdit5.Location = new System.Drawing.Point(93, 169);
            this.timeEdit5.Name = "timeEdit5";
            this.timeEdit5.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEdit5.Properties.Mask.EditMask = "G";
            this.timeEdit5.Size = new System.Drawing.Size(180, 20);
            this.timeEdit5.TabIndex = 4;
            // 
            // timeEdit6
            // 
            this.timeEdit6.AllowDrop = true;
            this.timeEdit6.EditValue = new System.DateTime(2020, 9, 7, 0, 0, 0, 0);
            this.timeEdit6.Location = new System.Drawing.Point(93, 143);
            this.timeEdit6.Name = "timeEdit6";
            this.timeEdit6.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEdit6.Properties.Mask.EditMask = "G";
            this.timeEdit6.Size = new System.Drawing.Size(180, 20);
            this.timeEdit6.TabIndex = 3;
            // 
            // simpleButton26
            // 
            this.simpleButton26.Location = new System.Drawing.Point(191, 60);
            this.simpleButton26.Name = "simpleButton26";
            this.simpleButton26.Size = new System.Drawing.Size(79, 23);
            this.simpleButton26.TabIndex = 2;
            this.simpleButton26.Text = "Search";
            // 
            // labelControl40
            // 
            this.labelControl40.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl40.Appearance.Options.UseFont = true;
            this.labelControl40.Location = new System.Drawing.Point(3, 7);
            this.labelControl40.Name = "labelControl40";
            this.labelControl40.Size = new System.Drawing.Size(103, 14);
            this.labelControl40.TabIndex = 1;
            this.labelControl40.Text = "Control System :";
            // 
            // labelControl41
            // 
            this.labelControl41.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl41.Appearance.Options.UseFont = true;
            this.labelControl41.Location = new System.Drawing.Point(12, 169);
            this.labelControl41.Name = "labelControl41";
            this.labelControl41.Size = new System.Drawing.Size(51, 14);
            this.labelControl41.TabIndex = 1;
            this.labelControl41.Text = "To Time:";
            // 
            // labelControl42
            // 
            this.labelControl42.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl42.Appearance.Options.UseFont = true;
            this.labelControl42.Location = new System.Drawing.Point(13, 143);
            this.labelControl42.Name = "labelControl42";
            this.labelControl42.Size = new System.Drawing.Size(66, 14);
            this.labelControl42.TabIndex = 1;
            this.labelControl42.Text = "From Time:";
            // 
            // comboBoxEdit7
            // 
            this.comboBoxEdit7.EditValue = "";
            this.comboBoxEdit7.Location = new System.Drawing.Point(112, 5);
            this.comboBoxEdit7.Name = "comboBoxEdit7";
            this.comboBoxEdit7.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit7.Size = new System.Drawing.Size(158, 20);
            this.comboBoxEdit7.TabIndex = 0;
            // 
            // accordionControl5
            // 
            this.accordionControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accordionControl5.ExpandElementMode = DevExpress.XtraBars.Navigation.ExpandElementMode.Multiple;
            this.accordionControl5.Location = new System.Drawing.Point(0, 0);
            this.accordionControl5.Name = "accordionControl5";
            this.accordionControl5.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.False;
            this.accordionControl5.Size = new System.Drawing.Size(0, 0);
            this.accordionControl5.TabIndex = 0;
            this.accordionControl5.Text = "accordionControl2";
            // 
            // cboxEditStepGroup
            // 
            this.cboxEditStepGroup.Location = new System.Drawing.Point(0, 0);
            this.cboxEditStepGroup.Name = "cboxEditStepGroup";
            this.cboxEditStepGroup.Size = new System.Drawing.Size(100, 20);
            this.cboxEditStepGroup.TabIndex = 0;
            // 
            // xtraSaveFileDialog1
            // 
            this.xtraSaveFileDialog1.FileName = "xtraSaveFileDialog1";
            this.xtraSaveFileDialog1.Filter = "CSV File (*.csv)|*.csv";
            this.xtraSaveFileDialog1.RestoreDirectory = true;
            // 
            // accordionControlElement23
            // 
            this.accordionControlElement23.ContentContainer = this.accordionContentContainer16;
            this.accordionControlElement23.Expanded = true;
            this.accordionControlElement23.Name = "accordionControlElement23";
            this.accordionControlElement23.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement23.Text = "To EQ Detail";
            // 
            // accordionContentContainer16
            // 
            this.accordionContentContainer16.Name = "accordionContentContainer16";
            this.accordionContentContainer16.Size = new System.Drawing.Size(261, 50);
            this.accordionContentContainer16.TabIndex = 4;
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(95, 16);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(117, 20);
            this.textEdit2.TabIndex = 2;
            // 
            // labelControl63
            // 
            this.labelControl63.Location = new System.Drawing.Point(31, 19);
            this.labelControl63.Name = "labelControl63";
            this.labelControl63.Size = new System.Drawing.Size(46, 14);
            this.labelControl63.TabIndex = 3;
            // 
            // accordionControlElement22
            // 
            this.accordionControlElement22.ContentContainer = this.accordionContentContainer15;
            this.accordionControlElement22.Expanded = true;
            this.accordionControlElement22.Name = "accordionControlElement22";
            this.accordionControlElement22.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement22.Text = "Type";
            // 
            // accordionContentContainer15
            // 
            this.accordionContentContainer15.Name = "accordionContentContainer15";
            this.accordionContentContainer15.Size = new System.Drawing.Size(261, 37);
            this.accordionContentContainer15.TabIndex = 3;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Checked = true;
            this.checkBox8.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox8.Location = new System.Drawing.Point(31, 8);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(38, 18);
            this.checkBox8.TabIndex = 25;
            this.checkBox8.Text = "All";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(75, 8);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(46, 18);
            this.checkBox7.TabIndex = 26;
            this.checkBox7.Text = "ZFS";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(118, 8);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBox6.Size = new System.Drawing.Size(48, 18);
            this.checkBox6.TabIndex = 27;
            this.checkBox6.Text = "STK";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(166, 8);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBox5.Size = new System.Drawing.Size(58, 18);
            this.checkBox5.TabIndex = 28;
            this.checkBox5.Text = "TOOL";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // accordionControlElement21
            // 
            this.accordionControlElement21.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement22,
            this.accordionControlElement23});
            this.accordionControlElement21.Expanded = true;
            this.accordionControlElement21.Name = "accordionControlElement21";
            this.accordionControlElement21.Text = "To EQ Type";
            // 
            // accordionContentContainer17
            // 
            this.accordionContentContainer17.Controls.Add(this.numericUpDown1);
            this.accordionContentContainer17.Controls.Add(this.labelControl64);
            this.accordionContentContainer17.Controls.Add(this.labelControl65);
            this.accordionContentContainer17.Controls.Add(this.numericUpDown2);
            this.accordionContentContainer17.Controls.Add(this.labelControl66);
            this.accordionContentContainer17.Controls.Add(this.numericUpDown3);
            this.accordionContentContainer17.Controls.Add(this.labelControl67);
            this.accordionContentContainer17.Controls.Add(this.numericUpDown4);
            this.accordionContentContainer17.Controls.Add(this.labelControl68);
            this.accordionContentContainer17.Controls.Add(this.numericUpDown5);
            this.accordionContentContainer17.Controls.Add(this.labelControl69);
            this.accordionContentContainer17.Controls.Add(this.labelControl70);
            this.accordionContentContainer17.Controls.Add(this.comboBoxEdit2);
            this.accordionContentContainer17.Controls.Add(this.comboBoxEdit3);
            this.accordionContentContainer17.Controls.Add(this.labelControl71);
            this.accordionContentContainer17.Controls.Add(this.labelControl72);
            this.accordionContentContainer17.Controls.Add(this.numericUpDown6);
            this.accordionContentContainer17.Controls.Add(this.labelControl73);
            this.accordionContentContainer17.Controls.Add(this.numericUpDown7);
            this.accordionContentContainer17.Controls.Add(this.labelControl74);
            this.accordionContentContainer17.Controls.Add(this.numericUpDown8);
            this.accordionContentContainer17.Controls.Add(this.labelControl75);
            this.accordionContentContainer17.Name = "accordionContentContainer17";
            this.accordionContentContainer17.Size = new System.Drawing.Size(261, 390);
            this.accordionContentContainer17.TabIndex = 5;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(142, 357);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(103, 21);
            this.numericUpDown1.TabIndex = 68;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelControl64
            // 
            this.labelControl64.Location = new System.Drawing.Point(22, 359);
            this.labelControl64.Name = "labelControl64";
            this.labelControl64.Size = new System.Drawing.Size(0, 14);
            this.labelControl64.TabIndex = 67;
            // 
            // labelControl65
            // 
            this.labelControl65.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl65.Appearance.Options.UseFont = true;
            this.labelControl65.Location = new System.Drawing.Point(26, 325);
            this.labelControl65.Name = "labelControl65";
            this.labelControl65.Size = new System.Drawing.Size(0, 14);
            this.labelControl65.TabIndex = 66;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(140, 286);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(103, 21);
            this.numericUpDown2.TabIndex = 65;
            // 
            // labelControl66
            // 
            this.labelControl66.Location = new System.Drawing.Point(20, 288);
            this.labelControl66.Name = "labelControl66";
            this.labelControl66.Size = new System.Drawing.Size(0, 14);
            this.labelControl66.TabIndex = 64;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(140, 254);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(103, 21);
            this.numericUpDown3.TabIndex = 63;
            this.numericUpDown3.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelControl67
            // 
            this.labelControl67.Location = new System.Drawing.Point(20, 256);
            this.labelControl67.Name = "labelControl67";
            this.labelControl67.Size = new System.Drawing.Size(0, 14);
            this.labelControl67.TabIndex = 62;
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.DecimalPlaces = 1;
            this.numericUpDown4.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown4.Location = new System.Drawing.Point(140, 219);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown4.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(103, 21);
            this.numericUpDown4.TabIndex = 61;
            // 
            // labelControl68
            // 
            this.labelControl68.Location = new System.Drawing.Point(20, 221);
            this.labelControl68.Name = "labelControl68";
            this.labelControl68.Size = new System.Drawing.Size(0, 14);
            this.labelControl68.TabIndex = 60;
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.DecimalPlaces = 1;
            this.numericUpDown5.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown5.Location = new System.Drawing.Point(140, 187);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(103, 21);
            this.numericUpDown5.TabIndex = 59;
            this.numericUpDown5.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelControl69
            // 
            this.labelControl69.Location = new System.Drawing.Point(20, 189);
            this.labelControl69.Name = "labelControl69";
            this.labelControl69.Size = new System.Drawing.Size(0, 14);
            this.labelControl69.TabIndex = 58;
            // 
            // labelControl70
            // 
            this.labelControl70.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl70.Appearance.Options.UseFont = true;
            this.labelControl70.Location = new System.Drawing.Point(27, 158);
            this.labelControl70.Name = "labelControl70";
            this.labelControl70.Size = new System.Drawing.Size(0, 14);
            this.labelControl70.TabIndex = 57;
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.EditValue = "1Hour";
            this.comboBoxEdit2.Location = new System.Drawing.Point(143, 123);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Properties.Items.AddRange(new object[] {
            "1Min",
            "10Min",
            "1Hour",
            "1Day"});
            this.comboBoxEdit2.Size = new System.Drawing.Size(100, 20);
            this.comboBoxEdit2.TabIndex = 44;
            // 
            // comboBoxEdit3
            // 
            this.comboBoxEdit3.EditValue = "Total Time";
            this.comboBoxEdit3.Location = new System.Drawing.Point(143, 97);
            this.comboBoxEdit3.Name = "comboBoxEdit3";
            this.comboBoxEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit3.Properties.Items.AddRange(new object[] {
            "Total Time",
            "Queued Time",
            "Waiting Time",
            "Transferring Time",
            "Deposit Time"});
            this.comboBoxEdit3.Size = new System.Drawing.Size(100, 20);
            this.comboBoxEdit3.TabIndex = 35;
            // 
            // labelControl71
            // 
            this.labelControl71.Location = new System.Drawing.Point(25, 126);
            this.labelControl71.Name = "labelControl71";
            this.labelControl71.Size = new System.Drawing.Size(33, 14);
            this.labelControl71.TabIndex = 43;
            this.labelControl71.Text = "Group";
            // 
            // labelControl72
            // 
            this.labelControl72.Location = new System.Drawing.Point(25, 100);
            this.labelControl72.Name = "labelControl72";
            this.labelControl72.Size = new System.Drawing.Size(60, 14);
            this.labelControl72.TabIndex = 42;
            this.labelControl72.Text = "Show Data";
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(143, 68);
            this.numericUpDown6.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(103, 21);
            this.numericUpDown6.TabIndex = 41;
            this.numericUpDown6.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            // 
            // labelControl73
            // 
            this.labelControl73.Location = new System.Drawing.Point(25, 70);
            this.labelControl73.Name = "labelControl73";
            this.labelControl73.Size = new System.Drawing.Size(76, 14);
            this.labelControl73.TabIndex = 40;
            this.labelControl73.Text = "Interval (Max)";
            // 
            // numericUpDown7
            // 
            this.numericUpDown7.Location = new System.Drawing.Point(143, 40);
            this.numericUpDown7.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new System.Drawing.Size(103, 21);
            this.numericUpDown7.TabIndex = 39;
            // 
            // labelControl74
            // 
            this.labelControl74.Location = new System.Drawing.Point(25, 42);
            this.labelControl74.Name = "labelControl74";
            this.labelControl74.Size = new System.Drawing.Size(73, 14);
            this.labelControl74.TabIndex = 38;
            this.labelControl74.Text = "Interval (Min)";
            // 
            // numericUpDown8
            // 
            this.numericUpDown8.Location = new System.Drawing.Point(143, 12);
            this.numericUpDown8.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown8.Name = "numericUpDown8";
            this.numericUpDown8.Size = new System.Drawing.Size(103, 21);
            this.numericUpDown8.TabIndex = 37;
            this.numericUpDown8.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // labelControl75
            // 
            this.labelControl75.Location = new System.Drawing.Point(25, 14);
            this.labelControl75.Name = "labelControl75";
            this.labelControl75.Size = new System.Drawing.Size(76, 14);
            this.labelControl75.TabIndex = 36;
            this.labelControl75.Text = "Distb. Interval";
            // 
            // accordionControlElement24
            // 
            this.accordionControlElement24.ContentContainer = this.accordionContentContainer17;
            this.accordionControlElement24.Expanded = true;
            this.accordionControlElement24.Name = "accordionControlElement24";
            this.accordionControlElement24.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement24.Text = "ETC";
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.MaxItemId = 52;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1471, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 935);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1471, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 935);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1471, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 935);
            // 
            // InOutTotalTimeAvg
            // 
            this.InOutTotalTimeAvg.AppearanceCell.Options.UseTextOptions = true;
            this.InOutTotalTimeAvg.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.InOutTotalTimeAvg.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.InOutTotalTimeAvg.AppearanceHeader.Options.UseTextOptions = true;
            this.InOutTotalTimeAvg.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.InOutTotalTimeAvg.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.InOutTotalTimeAvg.Caption = "Total Time(min)";
            this.InOutTotalTimeAvg.FieldName = "InOutTotalTimeAvg";
            this.InOutTotalTimeAvg.Name = "InOutTotalTimeAvg";
            this.InOutTotalTimeAvg.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "InOutTotalTimeAvg", "MIN={0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "InOutTotalTimeAvg", "MAX={0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "InOutTotalTimeAvg", "AVG={0:0.##}")});
            this.InOutTotalTimeAvg.Visible = true;
            this.InOutTotalTimeAvg.VisibleIndex = 3;
            // 
            // InOutStdevTotalTime
            // 
            this.InOutStdevTotalTime.AppearanceCell.Options.UseTextOptions = true;
            this.InOutStdevTotalTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.InOutStdevTotalTime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.InOutStdevTotalTime.AppearanceHeader.Options.UseTextOptions = true;
            this.InOutStdevTotalTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.InOutStdevTotalTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.InOutStdevTotalTime.Caption = "Stdev(min)";
            this.InOutStdevTotalTime.FieldName = "InOutStdevTotalTime";
            this.InOutStdevTotalTime.Name = "InOutStdevTotalTime";
            this.InOutStdevTotalTime.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "InOutStdevTotalTime", "MIN={0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "InOutStdevTotalTime", "MAX={0}"),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, "InOutStdevTotalTime", "AVG={0:0.##}")});
            this.InOutStdevTotalTime.Visible = true;
            this.InOutStdevTotalTime.VisibleIndex = 4;
            // 
            // ProductionReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1471, 935);
            this.Controls.Add(this.ProductionReportPages);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ProductionReportForm.IconOptions.SvgImage")));
            this.Name = "ProductionReportForm";
            this.Text = "Production Report";
            ((System.ComponentModel.ISupportInitialize)(this.ProductionReportPages)).EndInit();
            this.ProductionReportPages.ResumeLayout(false);
            this.StepTrendTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl4)).EndInit();
            this.splitContainerControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl5)).EndInit();
            this.splitContainerControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl7)).EndInit();
            this.splitContainerControl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboxEditStepGroupBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditToStepTrend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditFromStepTrend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl3)).EndInit();
            this.accordionControl3.ResumeLayout(false);
            this.accordionContentContainer2.ResumeLayout(false);
            this.accordionContentContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalTimeYMinStepTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalTimeYMaxStepTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCommandCountYMinStepTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCommandCountYMaxStepTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elapsedMaxStepTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elapsedMinStepTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxEditTimeIntervalStepTrend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl8)).EndInit();
            this.splitContainerControl8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toggleStepTrend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl9)).EndInit();
            this.splitContainerControl9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlStepTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStepTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStepTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl2)).EndInit();
            this.EqpPlanOperationRate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl15)).EndInit();
            this.splitContainerControl15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timeEditToEqpPlanOperationRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditFromEqpPlanOperationRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxEditEqpPlanOperationRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            this.accordionControl1.ResumeLayout(false);
            this.accordionContentContainer1.ResumeLayout(false);
            this.accordionContentContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalTimeYMinEqpPlanOperationRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalTimeYMaxEqpPlanOperationRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCommandCountYMinEqpPlanOperationRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCommandCountYMaxEqpPlanOperationRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elapsedMaxEqpPlanOperationRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elapsedMinEqpPlanOperationRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxEditTimeIntervalEqpPlanOperationRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).EndInit();
            this.splitContainerControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toggleEqpPlanOperationRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl6)).EndInit();
            this.splitContainerControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlEqpPlanOperationRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEqpPlanOperationRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEqpPlanOperationRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit7.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit8.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl6)).EndInit();
            this.FactoryInoutTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl10)).EndInit();
            this.splitContainerControl10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl11)).EndInit();
            this.splitContainerControl11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl12)).EndInit();
            this.splitContainerControl12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timeEditToInout.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditFromInout.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxEditInoutBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl4)).EndInit();
            this.accordionControl4.ResumeLayout(false);
            this.accordionContentContainer3.ResumeLayout(false);
            this.accordionContentContainer3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalTimeYMinInout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalTimeYMaxInout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCommandCountYMinInout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCommandCountYMaxInout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elapsedMaxInout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elapsedMinInout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxEditTimeIntervalInout.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl13)).EndInit();
            this.splitContainerControl13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toggleInout.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl14)).EndInit();
            this.splitContainerControl14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlInout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlInout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit7.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxEditStepGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            this.accordionContentContainer17.ResumeLayout(false);
            this.accordionContentContainer17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource bindingSource1;
        private XtraSaveFileDialog xtraSaveFileDialog1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement23;
        private DevExpress.XtraBars.Navigation.AccordionContentContainer accordionContentContainer16;
        private TextEdit textEdit2;
        private LabelControl labelControl63;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement22;
        private DevExpress.XtraBars.Navigation.AccordionContentContainer accordionContentContainer15;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement21;
        private DevExpress.XtraBars.Navigation.AccordionContentContainer accordionContentContainer17;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private LabelControl labelControl64;
        private LabelControl labelControl65;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private LabelControl labelControl66;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private LabelControl labelControl67;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private LabelControl labelControl68;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private LabelControl labelControl69;
        private LabelControl labelControl70;
        private ComboBoxEdit comboBoxEdit2;
        private ComboBoxEdit comboBoxEdit3;
        private LabelControl labelControl71;
        private LabelControl labelControl72;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private LabelControl labelControl73;
        private System.Windows.Forms.NumericUpDown numericUpDown7;
        private LabelControl labelControl74;
        private System.Windows.Forms.NumericUpDown numericUpDown8;
        private LabelControl labelControl75;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement24;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraTab.XtraTabPage StepTrendTab;
        private SplitContainerControl splitContainerControl4;
        private SplitContainerControl splitContainerControl5;
        private SplitContainerControl splitContainerControl7;
        private SimpleButton btnPrevTimeStepTrend;
        private SimpleButton btnNextTimeStepTrend;
        private SimpleButton btnMonthUnitStepTrend;
        private SimpleButton btnWeekUnitStepTrend;
        private SimpleButton btnDayUnitStepTrend;
        private SimpleButton btnHourUnitStepTrend;
        private TimeEdit timeEditToStepTrend;
        private TimeEdit timeEditFromStepTrend;
        private SimpleButton btnSearchStepTrend;
        private LabelControl labelControl17;
        private LabelControl labelControl18;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl3;
        private DevExpress.XtraBars.Navigation.AccordionContentContainer accordionContentContainer2;
        private LabelControl labelControl19;
        private System.Windows.Forms.NumericUpDown numericUpDownTotalTimeYMinStepTrend;
        private LabelControl labelControl20;
        private System.Windows.Forms.NumericUpDown numericUpDownTotalTimeYMaxStepTrend;
        private LabelControl labelControl21;
        private System.Windows.Forms.NumericUpDown numericUpDownCommandCountYMinStepTrend;
        private LabelControl labelControl22;
        private System.Windows.Forms.NumericUpDown numericUpDownCommandCountYMaxStepTrend;
        private LabelControl labelControl23;
        private LabelControl labelControl24;
        private LabelControl labelControl25;
        private System.Windows.Forms.NumericUpDown elapsedMaxStepTrend;
        private System.Windows.Forms.NumericUpDown elapsedMinStepTrend;
        private LabelControl labelControl26;
        private LabelControl labelControl27;
        private ComboBoxEdit cboxEditTimeIntervalStepTrend;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement3;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement5;
        private SplitContainerControl splitContainerControl8;
        private System.Windows.Forms.Label label2;
        private ToggleSwitch toggleStepTrend;
        private SimpleButton btnGridSaveStepTrend;
        private SplitContainerControl splitContainerControl9;
        private DevExpress.XtraCharts.ChartControl chartControlStepTrend;
        private DevExpress.XtraGrid.GridControl gridControlStepTrend;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewStepTrend;
        private DevExpress.XtraGrid.Columns.GridColumn colTrendTime;
        private DevExpress.XtraGrid.Columns.GridColumn colTrendEqpPlanCount;
        private DevExpress.XtraGrid.Columns.GridColumn colAvgTotalTime;
        private DevExpress.XtraGrid.Columns.GridColumn colStdevTotalTime;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private SimpleButton simpleButton3;
        private SimpleButton simpleButton4;
        private SimpleButton simpleButton5;
        private SimpleButton simpleButton6;
        private SimpleButton simpleButton7;
        private TimeEdit timeEdit1;
        private TimeEdit timeEdit2;
        private SimpleButton simpleButton8;
        private LabelControl labelControl4;
        private LabelControl labelControl5;
        private LabelControl labelControl6;
        private ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl2;
        public ComboBoxEdit cboxEditStepGroup;
        public DevExpress.XtraTab.XtraTabControl ProductionReportPages;
        private DevExpress.XtraGrid.Columns.GridColumn colAvgQueuedTime;
        private DevExpress.XtraGrid.Columns.GridColumn colAvgImportingTime;
        private DevExpress.XtraGrid.Columns.GridColumn colStepWaitingTime;
        private DevExpress.XtraGrid.Columns.GridColumn colProcessingTime;
        private DevExpress.XtraGrid.Columns.GridColumn colExportingTime;
        private DevExpress.XtraTab.XtraTabPage FactoryInoutTab;
        private SplitContainerControl splitContainerControl10;
        private SplitContainerControl splitContainerControl11;
        private SplitContainerControl splitContainerControl12;
        private SimpleButton btnInquiryTargetInout;
        private SimpleButton btnPrevTimeInout;
        private SimpleButton btnNextTimeInout;
        private SimpleButton btnMonthUnitInout;
        private SimpleButton btnWeekUnitInout;
        private SimpleButton btnDayUnitInout;
        private SimpleButton btnHourUnitInout;
        private TimeEdit timeEditToInout;
        private TimeEdit timeEditFromInout;
        private SimpleButton btnSearchInout;
        private LabelControl labelControl28;
        private LabelControl labelControl29;
        private LabelControl labelControl30;
        public ComboBoxEdit cboxEditInoutBy;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl4;
        private DevExpress.XtraBars.Navigation.AccordionContentContainer accordionContentContainer3;
        private LabelControl labelControl31;
        private System.Windows.Forms.NumericUpDown numericUpDownTotalTimeYMinInout;
        private LabelControl labelControl32;
        private System.Windows.Forms.NumericUpDown numericUpDownTotalTimeYMaxInout;
        private LabelControl labelControl33;
        private System.Windows.Forms.NumericUpDown numericUpDownCommandCountYMinInout;
        private LabelControl labelControl34;
        private System.Windows.Forms.NumericUpDown numericUpDownCommandCountYMaxInout;
        private LabelControl labelControl35;
        private LabelControl labelControl36;
        private LabelControl labelControl37;
        private System.Windows.Forms.NumericUpDown elapsedMaxInout;
        private System.Windows.Forms.NumericUpDown elapsedMinInout;
        private LabelControl labelControl38;
        private LabelControl labelControl39;
        private ComboBoxEdit cboxEditTimeIntervalInout;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement4;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement6;
        private SplitContainerControl splitContainerControl13;
        private System.Windows.Forms.Label label3;
        private ToggleSwitch toggleInout;
        private SimpleButton btnGridSaveInout;
        private SplitContainerControl splitContainerControl14;
        private DevExpress.XtraCharts.ChartControl chartControlInout;
        private SimpleButton simpleButton19;
        private SimpleButton simpleButton20;
        private SimpleButton simpleButton21;
        private SimpleButton simpleButton22;
        private SimpleButton simpleButton23;
        private SimpleButton simpleButton24;
        private SimpleButton simpleButton25;
        private TimeEdit timeEdit5;
        private TimeEdit timeEdit6;
        private SimpleButton simpleButton26;
        private LabelControl labelControl40;
        private LabelControl labelControl41;
        private LabelControl labelControl42;
        private ComboBoxEdit comboBoxEdit7;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl5;
        private DevExpress.XtraGrid.GridControl gridControlInout;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewInout;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colInput;
        private DevExpress.XtraGrid.Columns.GridColumn colOutput;
        private DevExpress.XtraGrid.Columns.GridColumn TrendTime;
        private DevExpress.XtraGrid.Columns.GridColumn EqpPlanCount;
        private DevExpress.XtraGrid.Columns.GridColumn SimTotalTimeAvg;
        private DevExpress.XtraGrid.Columns.GridColumn StdevTotalTime;
        private DevExpress.XtraGrid.Columns.GridColumn InoutTime;
        private DevExpress.XtraGrid.Columns.GridColumn InputCount;
        private DevExpress.XtraGrid.Columns.GridColumn OutputCount;
        private DevExpress.XtraGrid.Columns.GridColumn Time;
        private DevExpress.XtraGrid.Columns.GridColumn AvgQueuedTime;
        private DevExpress.XtraGrid.Columns.GridColumn AvgImportingTime;
        private DevExpress.XtraGrid.Columns.GridColumn AvgStepWaitingTime;
        private DevExpress.XtraGrid.Columns.GridColumn AvgProcessingTime;
        private DevExpress.XtraGrid.Columns.GridColumn AvgExportingTime;
        private DevExpress.XtraTab.XtraTabPage EqpPlanOperationRate;
        private SplitContainerControl splitContainerControl15;
        private SplitContainerControl splitContainerControl1;
        private SplitContainerControl splitContainerControl2;
        private SimpleButton btnInquiryTargetEqpPlanOperationRateBy;
        private SimpleButton btnPrevTimeEqpPlanOperationRate;
        private SimpleButton btnNextTimeEqpPlanOperationRate;
        private SimpleButton btnMonthUnitEqpPlanOperationRate;
        private SimpleButton btnWeekUnitEqpPlanOperationRate;
        private SimpleButton btnDayUnitEqpPlanOperationRate;
        private SimpleButton btnHourUnitEqpPlanOperationRate;
        private TimeEdit timeEditToEqpPlanOperationRate;
        private TimeEdit timeEditFromEqpPlanOperationRate;
        private SimpleButton btnSearchEqpPlanOperationRate;
        private LabelControl labelControl1;
        private LabelControl labelControl2;
        private LabelControl labelControl3;
        public ComboBoxEdit cboxEditEqpPlanOperationRate;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionContentContainer accordionContentContainer1;
        private LabelControl labelControl7;
        private System.Windows.Forms.NumericUpDown numericUpDownTotalTimeYMinEqpPlanOperationRate;
        private LabelControl labelControl8;
        private System.Windows.Forms.NumericUpDown numericUpDownTotalTimeYMaxEqpPlanOperationRate;
        private LabelControl labelControl9;
        private System.Windows.Forms.NumericUpDown numericUpDownCommandCountYMinEqpPlanOperationRate;
        private LabelControl labelControl10;
        private System.Windows.Forms.NumericUpDown numericUpDownCommandCountYMaxEqpPlanOperationRate;
        private LabelControl labelControl11;
        private LabelControl labelControl12;
        private LabelControl labelControl13;
        private System.Windows.Forms.NumericUpDown elapsedMaxEqpPlanOperationRate;
        private System.Windows.Forms.NumericUpDown elapsedMinEqpPlanOperationRate;
        private LabelControl labelControl14;
        private LabelControl labelControl15;
        private ComboBoxEdit cboxEditTimeIntervalEqpPlanOperationRate;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement2;
        private SplitContainerControl splitContainerControl3;
        private System.Windows.Forms.Label label1;
        private ToggleSwitch toggleEqpPlanOperationRate;
        private SimpleButton btnGridSaveEqpPlanOperationRate;
        private SplitContainerControl splitContainerControl6;
        private DevExpress.XtraCharts.ChartControl chartControlEqpPlanOperationRate;
        private DevExpress.XtraGrid.GridControl gridControlEqpPlanOperationRate;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewEqpPlanOperationRate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private SimpleButton simpleButton18;
        private SimpleButton simpleButton27;
        private SimpleButton simpleButton28;
        private SimpleButton simpleButton29;
        private SimpleButton simpleButton30;
        private SimpleButton simpleButton31;
        private SimpleButton simpleButton32;
        private TimeEdit timeEdit7;
        private TimeEdit timeEdit8;
        private SimpleButton simpleButton33;
        private LabelControl labelControl43;
        private LabelControl labelControl44;
        private LabelControl labelControl45;
        private ComboBoxEdit comboBoxEdit6;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl6;
        private LabelControl labelControl46;
        public ComboBoxEdit cboxEditStepGroupBy;
        private SimpleButton btnInquiryTargetStepTrend;
        private DevExpress.XtraGrid.Columns.GridColumn PartStepCount;
        private SimpleButton btnInquiryTargetEqpPlanOperationRate;
        private DevExpress.XtraGrid.Columns.GridColumn InOutTotalTimeAvg;
        private DevExpress.XtraGrid.Columns.GridColumn InOutStdevTotalTime;
    }
}