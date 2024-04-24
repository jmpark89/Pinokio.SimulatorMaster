using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simulation.Engine;
using DevExpress.XtraSplashScreen;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.IO;
using DevExpress.XtraWaitForm;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using Pinokio.Model.Base;
using Pinokio.Animation;
using Link = DevExpress.XtraPrinting.Link;
using Pinokio.UI.Base;

namespace Pinokio.Designer
{
    public partial class ProductionReportForm : DevExpress.XtraEditors.XtraForm
    {
        private bool _isChartModified;
        private bool _loadReport;
        private bool _isEmpty;
        private bool _isStep;
        private bool _isProduct;
        private bool _isEqpGroup;
        private bool _isNode;


        private DateTime _simStartTime;
        private DateTime _simEndTime;
        public List<string> _selectedStepNames;
        public List<string> _selectedProductNames;
        public List<string> _selectedStepEqpNames;
        public List<string> _selectedEqpGroupByNames;
        public List<string> _selectedStepGroupByNames;
        public List<string> _selectedEqpGroupNames;
        public List<string> _selectedStepGroupNames;
        public List<string> _selectedNodeInoutNames;
        public List<string> _selectedProductInoutNames;
        public bool _isGroupCheck = false;
        


        private int _trendTimeInterval;
        private TIME_UNIT _trendKPITimeUnit;
        public enum TIME_UNIT
        {
            Hour, Day, Week, Month
        }

        private Dictionary<DateTime, GridPartStepTrendLog> _dicGridPartStepTrend { get; set; }
        private Dictionary<DateTime, ChartPartStepCountTrendLog> _dicChartPartStepCount { get; set; }
        private Dictionary<DateTime, ChartPartStepTimeTrendLog> _dicChartPartStepSimTime { get; set; }

        private Dictionary<DateTime, GridFactoryInoutLog> _dicGridInoutTrend { get; set; }
        private Dictionary<DateTime, ChartFactoryInLog> _dicFactoryInCount { get; set; }
        private Dictionary<DateTime, ChartFactoryOutLog> _dicFactoryOutCount { get; set; }

        private Dictionary<DateTime, GridEqpStepTrendLog> _dicGridEqpPlanOperationRate { get; set; }
        private Dictionary<DateTime, double> _dicChartEqpPlanOperationRate { get; set; }

        private PinokioBaseModel pinokio3DModel1;
        private ModelDesigner _modelDesigner;
        public PinokioBaseModel PinokioMain3Dmodel { get { return pinokio3DModel1; } }

        public ProductionReportForm(bool isLoad, PinokioBaseModel pinokio3DModel, ModelDesigner modelDesigner)
        {
            InitializeComponent();

            _loadReport = isLoad;
            _selectedStepNames = new List<string>();
            _selectedProductNames = new List<string>();
            _selectedStepEqpNames = new List<string>();
            _selectedEqpGroupByNames = new List<string>();
            _selectedEqpGroupNames = new List<string>();
            _selectedStepGroupByNames = new List<string>();
            _selectedStepGroupNames = new List<string>();
            _selectedNodeInoutNames = new List<string>();
            _selectedProductInoutNames = new List<string>();

            cboxEditStepGroupBy.SelectedIndex = 0;
            cboxEditInoutBy.SelectedIndex = 0;
            cboxEditStepGroup.SelectedIndex = 0;
            cboxEditEqpPlanOperationRate.SelectedIndex = 0;

            SimResultDBManager.Instance.SelectResultMain(isLoad);
            SimResultDBManager.Instance.SelectEqpNameInEqpPlans(isLoad);
            SimResultDBManager.Instance.SelectNodeNameInout(isLoad);
            SimResultDBManager.Instance.SelectGroupNameInEqpPlans(isLoad);

            if (isLoad)
            {
                _simStartTime = SimResultDBManager.Instance.LoadedSimulationStartTime;
                _simEndTime = SimResultDBManager.Instance.LoadedSimulationEndTime;
            }
            else
            {
                _simStartTime = SimResultDBManager.Instance.SimulationStartTime;
                _simEndTime = SimResultDBManager.Instance.SimulationEndTime;
            }

            ProductionReportPages.SelectedTabPageIndex = 0;

            _trendTimeInterval = 60;
            _trendKPITimeUnit = TIME_UNIT.Hour;

            GetFromTime().Time = _simStartTime;
            GetToTime().Time = _simEndTime;
            timeEditFromEqpPlanOperationRate.Time = _simStartTime;
            timeEditFromInout.Time = _simStartTime;
            timeEditToEqpPlanOperationRate.Time = _simEndTime;
            timeEditToInout.Time = _simEndTime;

            _dicGridPartStepTrend = new Dictionary<DateTime, GridPartStepTrendLog>();
            _dicChartPartStepCount = new Dictionary<DateTime, ChartPartStepCountTrendLog>();
            _dicChartPartStepSimTime = new Dictionary<DateTime, ChartPartStepTimeTrendLog>();

            _dicGridInoutTrend = new Dictionary<DateTime, GridFactoryInoutLog>();
            _dicFactoryInCount = new Dictionary<DateTime, ChartFactoryInLog>();
            _dicFactoryOutCount = new Dictionary<DateTime, ChartFactoryOutLog>();

            _dicGridEqpPlanOperationRate = new Dictionary<DateTime, GridEqpStepTrendLog>();
            _dicChartEqpPlanOperationRate = new Dictionary<DateTime, double>();

            pinokio3DModel1 = pinokio3DModel;
            _modelDesigner = modelDesigner;

            foreach (string eqpName in SimResultDBManager.Instance.EqpNames)
            {
                _selectedStepEqpNames.Add(eqpName);
            }
            foreach (string nodeName in SimResultDBManager.Instance.NodeInoutNames)
            {
                _selectedNodeInoutNames.Add(nodeName);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            SplashScreenManager.ShowForm(this, typeof(WaitFormSplash), true, true);

            try
            {
                GetGridView().OptionsView.ShowFooter = true;
                switch (ProductionReportPages.SelectedTabPageIndex)
                {
                    case 0:
                        _dicGridPartStepTrend.Clear();
                        _dicChartPartStepCount.Clear();
                        _dicChartPartStepSimTime.Clear();
                        break;
                    case 1:
                        _dicGridPartStepTrend.Clear();
                        _dicGridEqpPlanOperationRate.Clear();
                        _dicChartEqpPlanOperationRate.Clear();
                        break;
                    case 2:
                        _dicGridInoutTrend.Clear();
                        _dicFactoryInCount.Clear();
                        _dicFactoryOutCount.Clear();
                        break;
                }
                UpdateTimeInterval();
                UpdateTabPage();
                UpdateGridControl();
                UpdateChart();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            SplashScreenManager.CloseForm(false, true);
        }

        private void btnInquiryTarget_Click(object sender, EventArgs e)
        {
            _selectedStepNames.Clear();
            _selectedProductNames.Clear();
            _selectedStepEqpNames.Clear();
            _selectedStepGroupNames.Clear();
            _selectedEqpGroupNames.Clear();
            _selectedNodeInoutNames.Clear();
            _selectedProductInoutNames.Clear();
            _isGroupCheck = false;

            ProductionInquiryTargetForm targetForm = new ProductionInquiryTargetForm(this);
            targetForm.StartPosition = FormStartPosition.Manual;
            Point p = GetInquiryTargetForm().PointToScreen(new Point(GetInquiryTargetForm().Width, 0));
            targetForm.SetDesktopLocation(p.X, p.Y);

            
            if (targetForm.ShowDialog() == DialogResult.OK)
            {
                switch (ProductionReportPages.SelectedTabPageIndex)
                {
                    case 0:
                        if (_isStep) {
                            foreach (var item in targetForm.checkedListBox.CheckedItems)
                                _selectedStepNames.Add(item.ToString()); 
                        }
                        else if (_isProduct)
                        {
                            foreach (var item in targetForm.checkedListBox.CheckedItems)
                                _selectedProductNames.Add(item.ToString());
                        }
                        else
                        {
                            foreach (var item in targetForm.checkedListBox.CheckedItems)
                                _selectedStepEqpNames.Add(item.ToString());
                        }
                        break;
                    case 1:
                        if (_isEqpGroup)
                        {
                                _selectedEqpGroupByNames.Clear();
                                foreach (var item in targetForm.checkedListBox.CheckedItems)
                                {
                                    _selectedEqpGroupByNames.Add(item.ToString());
                                }
                        }
                        else if(!_isEqpGroup)
                        {
                           
                                _selectedStepGroupByNames.Clear();
                                foreach (var item in targetForm.checkedListBox.CheckedItems)
                                {
                                    _selectedStepGroupByNames.Add(item.ToString());
                                }
                        }
                        break;
                    case 2:
                        if (_isNode)
                        {
                            foreach (var item in targetForm.checkedListBox.CheckedItems)
                                _selectedNodeInoutNames.Add(item.ToString());
                        }
                        else
                        {
                            foreach (var item in targetForm.checkedListBox.CheckedItems)
                                _selectedProductInoutNames.Add(item.ToString());
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private void btnGroupInquiryTarget_Click(object sender, EventArgs e)
        {
            _selectedEqpGroupNames.Clear();
            _selectedStepGroupNames.Clear();
            _isGroupCheck = true;
            ProductionInquiryTargetForm targetForm = new ProductionInquiryTargetForm(this);
            targetForm.StartPosition = FormStartPosition.Manual;
            Point p = GetInquiryTargetForm().PointToScreen(new Point(GetInquiryTargetForm().Width, 0));
            targetForm.SetDesktopLocation(p.X, p.Y);
            

            if (targetForm.ShowDialog() == DialogResult.OK)
            {
                if (_isGroupCheck)
                {
                    if (_isEqpGroup && _selectedEqpGroupByNames.Count >= 1)
                    {
                        foreach (var item in targetForm.checkedListBox.CheckedItems)
                        {
                            _selectedStepGroupNames.Add(item.ToString());
                        }
                    }
                    else if (!_isEqpGroup && _selectedStepGroupByNames.Count >= 1)
                    {
                        foreach (var item in targetForm.checkedListBox.CheckedItems)
                        {
                            _selectedEqpGroupNames.Add(item.ToString());
                        }
                    }
                }
            }
        }


        private void UpdateTimeInterval()
        {
            switch (GetTimeIntervalCbox().Text)
            {
                case "1Min":
                    _trendTimeInterval = 1;
                    break;
                case "10Min":
                    _trendTimeInterval = 10;
                    break;
                case "1Hour":
                    _trendTimeInterval = 60;
                    break;
                case "1Day":
                    _trendTimeInterval = 1440;
                    break;
            }
        }

        private void UpdateTabPage()
        {
            try
            {
                DateTime fromTime = (DateTime)GetFromTime().EditValue;
                DateTime toTime = (DateTime)GetToTime().EditValue;

                string selectedItem = GetCbox().SelectedItem.ToString();
                
                List<uint> eqpIDs = new List<uint>();
                Dictionary<PartStep, string> partSteps = new Dictionary<PartStep, string>();
                Dictionary<EqpStep, string> eqpSteps = new Dictionary<EqpStep, string>();
                Dictionary<FactoryInout, string> inouts = new Dictionary<FactoryInout, string>();

                switch (ProductionReportPages.SelectedTabPageIndex)
                {
                    case 0:
                        if (_isStep) {
                            partSteps.Clear();
                            if (_selectedStepNames.Count > 0)
                            {
                                partSteps = SimResultDBManager.Instance.SelectPartStepLog(fromTime, toTime, _selectedStepNames, _loadReport, _isStep, _isProduct);
                                partSteps = partSteps.OrderBy(x => x.Key.WipStartDateTime).ToDictionary(pair => pair.Key, pair => pair.Value);
                            }
                        }
                        else if (_isProduct)
                        {
                            partSteps.Clear();
                            if (_selectedProductNames.Count > 0)
                            {
                                partSteps = SimResultDBManager.Instance.SelectPartStepLog(fromTime, toTime, _selectedProductNames, _loadReport, _isStep, _isProduct);
                                partSteps = partSteps.OrderBy(x => x.Key.WipStartDateTime).ToDictionary(pair => pair.Key, pair => pair.Value);
                            }
                        }
                        else
                        {
                            partSteps.Clear();
                            if (_selectedStepEqpNames.Count > 0)
                            {
                                partSteps = SimResultDBManager.Instance.SelectPartStepLog(fromTime, toTime, _selectedStepEqpNames, _loadReport, _isStep, _isProduct);
                                partSteps = partSteps.OrderBy(x => x.Key.WipStartDateTime).ToDictionary(pair => pair.Key, pair => pair.Value);
                            }
                        }
                        GetFromTime().Time = _simStartTime;
                        GetToTime().Time = _simEndTime;
                        break;
                    case 1:
                        if (_isEqpGroup&&!_isGroupCheck)
                        {
                            eqpSteps.Clear();
                            if (_selectedEqpGroupByNames.Count > 0)
                            {
                                eqpSteps = SimResultDBManager.Instance.SelectEqpStepLog(fromTime, toTime, _selectedEqpGroupByNames, _selectedStepGroupNames, _loadReport,_isEqpGroup,_isGroupCheck);
                                eqpSteps = eqpSteps.OrderBy(x => x.Key.ActivatedDateTime).ToDictionary(pair => pair.Key, pair => pair.Value);
                                foreach (EqpStep eqpPlan in eqpSteps.Keys)
                                {
                                    if (!eqpIDs.Contains(eqpPlan.EqpID))
                                        eqpIDs.Add(eqpPlan.EqpID);
                                }
                            }
                        }
                        else if(!_isEqpGroup && !_isGroupCheck)
                        {
                            eqpSteps.Clear();
                            if (_selectedStepGroupByNames.Count > 0)
                            {
                                eqpSteps = SimResultDBManager.Instance.SelectEqpStepLog(fromTime, toTime, _selectedStepGroupByNames, _selectedEqpGroupNames, _loadReport,_isEqpGroup, _isGroupCheck);
                                eqpSteps = eqpSteps.OrderBy(x => x.Key.ActivatedDateTime).ToDictionary(pair => pair.Key, pair => pair.Value);
                                foreach (EqpStep eqpPlan in eqpSteps.Keys)
                                {
                                    if (!eqpIDs.Contains(eqpPlan.EqpID))
                                        eqpIDs.Add(eqpPlan.EqpID);
                                }
                            }
                        }
                        if (_isGroupCheck&& _isEqpGroup)
                        {
                            eqpSteps = SimResultDBManager.Instance.SelectEqpStepLog(fromTime, toTime, _selectedEqpGroupByNames, _selectedStepGroupNames, _loadReport, _isEqpGroup, _isGroupCheck);
                            eqpSteps = eqpSteps.OrderBy(x => x.Key.ActivatedDateTime).ToDictionary(pair => pair.Key, pair => pair.Value);
                            foreach (EqpStep eqpPlan in eqpSteps.Keys)
                            {
                                if (!eqpIDs.Contains(eqpPlan.EqpID))
                                    eqpIDs.Add(eqpPlan.EqpID);
                            }
                        }
                        else if (_isGroupCheck &&!_isEqpGroup)
                        {
                            eqpSteps = SimResultDBManager.Instance.SelectEqpStepLog(fromTime, toTime, _selectedStepGroupByNames, _selectedEqpGroupNames, _loadReport, _isEqpGroup, _isGroupCheck);
                            eqpSteps = eqpSteps.OrderBy(x => x.Key.ActivatedDateTime).ToDictionary(pair => pair.Key, pair => pair.Value);
                            foreach (EqpStep eqpPlan in eqpSteps.Keys)
                            {
                                if (!eqpIDs.Contains(eqpPlan.EqpID))
                                    eqpIDs.Add(eqpPlan.EqpID);
                            }
                        }
                        else
                        {
                            eqpSteps = SimResultDBManager.Instance.SelectEqpStepLog(fromTime, toTime, _selectedEqpGroupByNames, _selectedStepGroupNames, _loadReport, _isEqpGroup, _isGroupCheck);
                            eqpSteps = eqpSteps.OrderBy(x => x.Key.ActivatedDateTime).ToDictionary(pair => pair.Key, pair => pair.Value);
                            foreach (EqpStep eqpPlan in eqpSteps.Keys)
                            {
                                if (!eqpIDs.Contains(eqpPlan.EqpID))
                                    eqpIDs.Add(eqpPlan.EqpID);
                            }
                        }
                        

                        break;
                    case 2:
                        if (_isNode)
                        {

                            inouts.Clear();
                            if (_selectedNodeInoutNames.Count > 0)
                            {
                                inouts = SimResultDBManager.Instance.SelectFactoryInoutLog(fromTime, toTime, _selectedNodeInoutNames, _loadReport, _isNode);
                                inouts = inouts.OrderBy(x => x.Key.InoutTime).ToDictionary(pair => pair.Key, pair => pair.Value);
                            }
                        }
                        else
                        {
                            inouts.Clear();
                            if (_selectedProductInoutNames.Count > 0)
                            {
                                inouts = SimResultDBManager.Instance.SelectFactoryInoutLog(fromTime, toTime, _selectedProductInoutNames, _loadReport, _isNode);
                                inouts = inouts.OrderBy(x => x.Key.InoutTime).ToDictionary(pair => pair.Key, pair => pair.Value);
                            }
                        }
                        
                        break;
                    default:
                        break;
                }

                _isEmpty = true;
                Dictionary<FactoryInout, string> inputDatas = new Dictionary<FactoryInout, string>();

                for (; fromTime < toTime; fromTime = fromTime.AddSeconds(_trendTimeInterval * 60))
                {
                    DateTime endTime = fromTime.AddSeconds(_trendTimeInterval * 60);

                    switch (ProductionReportPages.SelectedTabPageIndex)
                    {
                        case 0:
                            Dictionary<PartStep, bool> dicEqpPlans = new Dictionary<PartStep, bool>();
                            foreach (PartStep eqpPlan in partSteps.Keys.ToList())
                            {
                                if (fromTime <= eqpPlan.WipStartDateTime && eqpPlan.WipStartDateTime < endTime)
                                {
                                    dicEqpPlans.Add(eqpPlan, true);
                                    partSteps.Remove(eqpPlan);
                                }
                                if ((eqpPlan.TrackOutDateTime - eqpPlan.WipStartDateTime).TotalSeconds / 60 < (double)GetElapsedMin().Value)
                                    dicEqpPlans.Remove(eqpPlan);
                                if ((eqpPlan.TrackOutDateTime - eqpPlan.WipStartDateTime).TotalSeconds / 60 > (double)GetElapsedMax().Value)
                                    dicEqpPlans.Remove(eqpPlan);
                                if (endTime <= eqpPlan.WipStartDateTime)
                                    break;
                            }
                            if (dicEqpPlans.Count != 0)
                            {
                                GridPartStepTrendLog partStepLog = new GridPartStepTrendLog(fromTime, dicEqpPlans.Keys.ToList());

                                _dicGridPartStepTrend.Add(fromTime, partStepLog);

                                ChartPartStepCountTrendLog chartPartStepCountTrendLog = new ChartPartStepCountTrendLog();
                                chartPartStepCountTrendLog.Time = fromTime;
                                chartPartStepCountTrendLog.EqpPlanCount = dicEqpPlans.Count;
                                _dicChartPartStepCount.Add(fromTime, chartPartStepCountTrendLog);

                                ChartPartStepTimeTrendLog chartPartStepTimeTrendLog = new ChartPartStepTimeTrendLog();
                                chartPartStepTimeTrendLog.Time = fromTime;
                                chartPartStepTimeTrendLog.SimTotalTimeAvg = partStepLog.SimTotalTimeAvg;
                                _dicChartPartStepSimTime.Add(fromTime, chartPartStepTimeTrendLog);
                                _isEmpty = false;
                            }
                            break;
                        case 1:
                            Dictionary<EqpStep, bool> dicEqpStepOperationRate = new Dictionary<EqpStep, bool>();
                            foreach (EqpStep eqpPlan in eqpSteps.Keys.ToList())
                            {
                                if (fromTime >= eqpPlan.StepStartDateTime && fromTime < eqpPlan.StepEndDateTime)
                                {
                                    dicEqpStepOperationRate.Add(eqpPlan, true);
                                }
                                else if (endTime >= eqpPlan.StepStartDateTime && endTime < eqpPlan.StepEndDateTime)
                                {
                                    dicEqpStepOperationRate.Add(eqpPlan, true);
                                }
                                else if (fromTime <= eqpPlan.StepStartDateTime && endTime > eqpPlan.StepEndDateTime)
                                {
                                    dicEqpStepOperationRate.Add(eqpPlan, true);
                                    eqpSteps.Remove(eqpPlan);
                                }
                            }

                            if (dicEqpStepOperationRate.Count != 0)
                            {
                                Dictionary<EqpStep, bool> dicEqpPlan = new Dictionary<EqpStep, bool>();
                                foreach (EqpStep eqpPlan in dicEqpStepOperationRate.Keys)
                                {
                                    if (eqpIDs.Contains(eqpPlan.EqpID))
                                        dicEqpPlan.Add(eqpPlan, true);
                                    else
                                        continue;
                                }            
                                GridEqpStepTrendLog eqpPlanLog = new GridEqpStepTrendLog(fromTime, dicEqpPlan.Keys.ToList());
                                eqpPlanLog.IntervalMin = _trendTimeInterval;
                                eqpPlanLog.TotalEqpCount = eqpIDs.Count;
                                eqpPlanLog.dicOperateRate = new Dictionary<DateTime, double>();

                                eqpPlanLog.GetEqpPlansOperatingRate(dicEqpPlan.Keys.ToList(), fromTime, endTime);

                                _dicGridEqpPlanOperationRate.Add(fromTime, eqpPlanLog);

                                eqpPlanLog.dicOperateRate.Add(fromTime, eqpPlanLog.EqpPlanOperatingRate);

                                _dicChartEqpPlanOperationRate = eqpPlanLog.dicOperateRate;
                                _isEmpty = false;
                            }
                            break;
                        case 2:
                            Dictionary<FactoryInout, string> dicFactoryInouts = new Dictionary<FactoryInout, string>();
                            ChartFactoryInLog chartFactoryInLog = new ChartFactoryInLog();
                            chartFactoryInLog.Time = fromTime;
                            chartFactoryInLog.InputCount = 0;

                            ChartFactoryOutLog chartFactoryOutLog = new ChartFactoryOutLog();
                            chartFactoryOutLog.Time = fromTime;
                            chartFactoryOutLog.OutputCount = 0;

                            foreach (FactoryInout Inout in inouts.Keys.ToList())
                            {
                                if (Inout.InoutTime < endTime)
                                {
                                    if (Inout.State == "Input")
                                    {
                                        dicFactoryInouts.Add(Inout, "Input");
                                        inputDatas.Add(Inout, "Input");
                                        chartFactoryInLog.InputCount++;
                                        inouts.Remove(Inout);
                                    }
                                    else
                                    {
                                        dicFactoryInouts.Add(Inout, "Output");
                                        chartFactoryOutLog.OutputCount++;
                                        inouts.Remove(Inout);
                                    }
                                }
                            }
                            if (dicFactoryInouts.Count != 0)
                            {
                                double inoutTotalTimeAvg;
                                double inoutStdevTotalTime;
                                var nodeIds = dicFactoryInouts.Where(kv => kv.Value == "Output").Select(kv => kv.Key.PartID);

                                if (nodeIds.Count() != 0)
                                {
                                    List<double> totalTimes = nodeIds
                                    .Select(nodeId =>
                                    {
                                        var outputTime = dicFactoryInouts.FirstOrDefault(kv => kv.Value == "Output" && kv.Key.PartID == nodeId).Key.InoutTime;
                                        var inputTimeData = dicFactoryInouts.FirstOrDefault(kv => kv.Value == "Input" && kv.Key.PartID == nodeId);
                                        if (inputTimeData.Equals(default(KeyValuePair<FactoryInout, string>)))
                                        {
                                            inputTimeData = inputDatas.FirstOrDefault(kv => kv.Value == "Input" && kv.Key.PartID == nodeId);
                                        }
                                        var inputTime = inputTimeData.Key.InoutTime;

                                        return (outputTime - inputTime).TotalSeconds;
                                    })
                                    .ToList();

                                    inoutTotalTimeAvg = Math.Round(totalTimes.Average() / 60, 3);
                                    inoutStdevTotalTime = Math.Round(Math.Sqrt(totalTimes.Average(time => time * time) - Math.Pow(totalTimes.Average(), 2)) / 60, 3);
                                }
                                else
                                {
                                    inoutTotalTimeAvg = 0;
                                    inoutStdevTotalTime = 0;
                                }
                                


                                GridFactoryInoutLog factoryInoutLog = new GridFactoryInoutLog(fromTime, chartFactoryInLog.InputCount, chartFactoryOutLog.OutputCount, inoutTotalTimeAvg, inoutStdevTotalTime);

                                _dicGridInoutTrend.Add(fromTime, factoryInoutLog);
                                _dicFactoryInCount.Add(fromTime, chartFactoryInLog);
                                _dicFactoryOutCount.Add(fromTime, chartFactoryOutLog);
                                _isEmpty = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (_isEmpty)
                {
                    GetGridView().OptionsView.ShowFooter = false;
                    switch (ProductionReportPages.SelectedTabPageIndex)
                    {
                        case 0:
                            MessageBox.Show("EqpPlan이 없습니다.");
                            break;
                        case 1:
                            MessageBox.Show("EqpPlan이 없습니다.");
                            break;
                        case 2:
                            MessageBox.Show("Input, Output이 없습니다.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void UpdateGridControl()
        {
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    GetGridControl().DataSource = _dicGridPartStepTrend.Values.ToList();
                    break;
                case 1:
                    GetGridControl().DataSource = _dicGridEqpPlanOperationRate.Values.ToList();
                    break;
                case 2:
                    GetGridControl().DataSource = _dicGridInoutTrend.Values.ToList();
                    break;
                default:
                    break;
            }
        }

        private void UpdateChart()
        {
            try
            {
                _isChartModified = true;

                int minCount = 0;
                int maxCount = 10;

                double minTime = 0;
                double maxTime = 10;

                switch (ProductionReportPages.SelectedTabPageIndex)
                {
                    case 0:
                        GetChartControl().Series["Through-put"].DataSource = _dicChartPartStepCount.Values;
                        GetChartControl().Series["Total Time(min)"].DataSource = _dicChartPartStepSimTime.Values;
                        if (_dicChartPartStepCount.Count > 0)
                            maxCount = _dicChartPartStepCount.Values.Max(x => x.EqpPlanCount);
                        if (_dicChartPartStepSimTime.Count > 0)
                            maxTime = _dicChartPartStepSimTime.Values.Max(x => x.SimTotalTimeAvg);
                        break;
                    case 1:
                        maxTime = 100;
                        GetChartControl().Series["Eqp Operating Count"].DataSource = _dicGridEqpPlanOperationRate.Values;
                        GetChartControl().Series["Eqp Operating Rate (%)"].Points.Clear();
                        foreach (GridEqpStepTrendLog gridEqpPlanOperationRate in _dicGridEqpPlanOperationRate.Values)
                        {
                            foreach (DateTime time in gridEqpPlanOperationRate.dicOperateRate.Keys)
                                GetChartControl().Series["Eqp Operating Rate (%)"].Points.AddPoint(time, Math.Round(gridEqpPlanOperationRate.dicOperateRate[time], 1));
                        }
                        GetChartControl().Series["Eqp Operating Rate (%)"].ChangeView(ViewType.Line);

                        maxCount = _dicGridEqpPlanOperationRate.Values.Max(x => x.EqpPlanOperatingCount);
                        break;
                    case 2:
                        GetChartControl().Series["Input Count"].DataSource = _dicFactoryInCount.Values;
                        GetChartControl().Series["Output Count"].DataSource = _dicFactoryOutCount.Values;

                        foreach (ChartFactoryInLog InCount in _dicFactoryInCount.Values)
                            if (InCount.InputCount > maxCount)
                                maxCount = InCount.InputCount;

                        foreach (ChartFactoryOutLog OutCount in _dicFactoryOutCount.Values)
                            if (OutCount.OutputCount > maxCount)
                                maxCount = OutCount.OutputCount;
                        break;
                    default:
                        break;
                }

                ((XYDiagram)GetChartControl().Diagram).AxisX.DateTimeScaleOptions.MeasureUnitMultiplier = _trendTimeInterval;
                ((XYDiagram)GetChartControl().Diagram).AxisX.WholeRange.SideMarginsValue = 10;
                ((XYDiagram)GetChartControl().Diagram).AxisY.WholeRange.SideMarginsValue = 10;
                ((XYDiagram)GetChartControl().Diagram).AxisY.WholeRange.SetMinMaxValues(minCount, maxCount + maxCount * 0.2);
                ((XYDiagram)GetChartControl().Diagram).SecondaryAxesY[0].WholeRange.SetMinMaxValues(minTime, maxTime + maxTime * 0.2);

                GetChartControl().RefreshData();

                object Ymax = ((XYDiagram)GetChartControl().Diagram).AxisY.WholeRange.MaxValue;

                GetCommandCountMax().Value = decimal.Parse(Ymax.ToString());

                object Ymin = ((XYDiagram)GetChartControl().Diagram).AxisY.WholeRange.MinValue;

                GetCommandCountMin().Value = decimal.Parse(Ymin.ToString());

                object YTotalmax = ((XYDiagram)GetChartControl().Diagram).SecondaryAxesY[0].WholeRange.MaxValue;

                GetTimeMax().Value = decimal.Parse(YTotalmax.ToString());

                object YTotalmin = ((XYDiagram)GetChartControl().Diagram).SecondaryAxesY[0].WholeRange.MinValue;

                GetTimeMin().Value = decimal.Parse(YTotalmin.ToString());

                _isChartModified = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void buttonGridSave_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(WaitFormSplash), true, true);

            string ExportExcelPath = Directory.GetCurrentDirectory() + "/" + ProductionReportPages.SelectedTabPage.Name.ToString();
            DirectoryInfo di = new DirectoryInfo(ExportExcelPath);

            if (di.Exists == false)
                di.Create();

            CompositeLink composLink = new CompositeLink(new PrintingSystem());
            PrintableComponentLink pcLink1 = new PrintableComponentLink();
            PrintableComponentLink pcLink2 = new PrintableComponentLink();

            Link linkMainReport = new Link();
            linkMainReport.CreateDetailArea +=
                new CreateAreaEventHandler(linkMainReport_CreateDetailArea);
            Link linkGrid1Report = new Link();
            linkGrid1Report.CreateDetailArea +=
                new CreateAreaEventHandler(linkGrid1Report_CreateDetailArea);
            Link linkGrid2Report = new Link();
            linkGrid2Report.CreateDetailArea +=
                new CreateAreaEventHandler(linkGrid2Report_CreateDetailArea);

            pcLink1.Component = this.GetGridControl();
            pcLink2.Component = this.GetChartControl();

            composLink.Links.Add(linkGrid1Report);
            composLink.Links.Add(pcLink1);
            composLink.Links.Add(linkMainReport);
            composLink.Links.Add(linkGrid2Report);
            composLink.Links.Add(pcLink2);
            XlsxExportOptions sd = new XlsxExportOptions();

            string path = ExportExcelPath + "/" + ProductionReportPages.SelectedTabPage.Name.ToString() + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + DateTime.Now.Second + ".xlsx";
            composLink.ExportToXlsx(path, new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });

            SplashScreenManager.CloseForm(false, true);
            MessageBox.Show("Export 완료되었습니다.");
        }

        private void labelSwitch_Toggled(object sender, EventArgs e)
        {
            if ((sender as ToggleSwitch).IsOn)
                GetChartControl().Series[GetChartControl().Series.Count - 1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            else
                GetChartControl().Series[GetChartControl().Series.Count - 1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        }

        private void numericUpDownYMax_ValueChanged(object sender, EventArgs e)
        {
            if (_isChartModified)
                return;

            double v = (double)(sender as NumericUpDown).Value;
            ((XYDiagram)GetChartControl().Diagram).AxisY.WholeRange.MaxValue = v;
            ((XYDiagram)GetChartControl().Diagram).AxisY.WholeRange.SideMarginsValue = 0;
        }

        private void numericUpDownYMin_ValueChanged(object sender, EventArgs e)
        {
            if (_isChartModified)
                return;

            double v = (double)(sender as NumericUpDown).Value;
            ((XYDiagram)GetChartControl().Diagram).AxisY.WholeRange.MinValue = v;
            ((XYDiagram)GetChartControl().Diagram).AxisY.WholeRange.SideMarginsValue = 0;
        }

        private void numericUpDowntTotalTimeYMax_ValueChanged(object sender, EventArgs e)
        {
            if (_isChartModified)
                return;

            double v = (double)(sender as NumericUpDown).Value;
            ((XYDiagram)GetChartControl().Diagram).SecondaryAxesY[0].WholeRange.MaxValue = v;
            ((XYDiagram)GetChartControl().Diagram).SecondaryAxesY[0].WholeRange.SideMarginsValue = 0;
        }

        private void numericUpDownTotalTimeYMin_ValueChanged(object sender, EventArgs e)
        {
            if (_isChartModified)
                return;

            double v = (double)(sender as NumericUpDown).Value;
            ((XYDiagram)GetChartControl().Diagram).SecondaryAxesY[0].WholeRange.MinValue = v;
            ((XYDiagram)GetChartControl().Diagram).SecondaryAxesY[0].WholeRange.SideMarginsValue = 0;
        }

        private void btnPrevTime_Click(object sender, EventArgs e)
        {
            switch (_trendKPITimeUnit)
            {
                case TIME_UNIT.Hour:
                    GetFromTime().Time = GetFromTime().Time.AddHours(-1);
                    GetToTime().Time = GetToTime().Time.AddHours(-1);
                    break;
                case TIME_UNIT.Day:
                    GetFromTime().Time = GetFromTime().Time.AddDays(-1);
                    GetToTime().Time = GetToTime().Time.AddDays(-1);
                    break;
                case TIME_UNIT.Week:
                    GetFromTime().Time = GetFromTime().Time.AddDays(-7);
                    GetToTime().Time = GetToTime().Time.AddDays(-7);
                    break;
                case TIME_UNIT.Month:
                    GetFromTime().Time = GetFromTime().Time.AddMonths(-1);
                    GetToTime().Time = GetToTime().Time.AddMonths(-1);
                    break;
            }
        }

        private void btnHourUnit_Click(object sender, EventArgs e)
        {
            _trendKPITimeUnit = TIME_UNIT.Hour;
            GetFromTime().Time = SimEngine.Instance.StartDateTime;
            GetToTime().Time = SimEngine.Instance.StartDateTime.AddHours(1);
        }

        private void btnDayUnit_Click(object sender, EventArgs e)
        {
            _trendKPITimeUnit = TIME_UNIT.Day;

            GetFromTime().Time = SimEngine.Instance.StartDateTime;
            GetToTime().Time = SimEngine.Instance.StartDateTime.AddDays(1);
        }

        private void btnWeekUnit_Click(object sender, EventArgs e)
        {
            _trendKPITimeUnit = TIME_UNIT.Week;

            GetFromTime().Time = SimEngine.Instance.StartDateTime;
            GetToTime().Time = SimEngine.Instance.StartDateTime.AddDays(7);
        }

        private void btnMonthUnit_Click(object sender, EventArgs e)
        {
            _trendKPITimeUnit = TIME_UNIT.Month;

            GetFromTime().Time = SimEngine.Instance.StartDateTime;
            GetToTime().Time = SimEngine.Instance.StartDateTime.AddMonths(1);
        }

        private void btnNextTime_Click(object sender, EventArgs e)
        {
            switch (_trendKPITimeUnit)
            {
                case TIME_UNIT.Hour:
                    GetFromTime().Time = GetFromTime().Time.AddHours(1);
                    GetToTime().Time = GetToTime().Time.AddHours(1);
                    break;
                case TIME_UNIT.Day:
                    GetFromTime().Time = GetFromTime().Time.AddDays(1);
                    GetToTime().Time = GetToTime().Time.AddDays(1);
                    break;
                case TIME_UNIT.Week:
                    GetFromTime().Time = GetFromTime().Time.AddDays(7);
                    GetToTime().Time = GetToTime().Time.AddDays(7);
                    break;
                case TIME_UNIT.Month:
                    GetFromTime().Time = GetFromTime().Time.AddMonths(1);
                    GetToTime().Time = GetToTime().Time.AddMonths(1);
                    break;
            }
        }

        private void linkMainReport_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            TextBrick tb = new TextBrick();
            tb.Rect = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
            tb.BackColor = Color.Gray;
            e.Graph.DrawBrick(tb);
        }

        private void linkGrid1Report_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            TextBrick tb = new TextBrick();
            tb.Text = "Data Source";
            tb.Font = new Font("Arial", 15);
            tb.Rect = new RectangleF(0, 0, 900, 25);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            e.Graph.DrawBrick(tb);
        }

        private void linkGrid2Report_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            TextBrick tb = new TextBrick();
            tb.Text = "Chart";
            tb.Font = new Font("Arial", 15);
            tb.Rect = new RectangleF(0, 0, 900, 25);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            e.Graph.DrawBrick(tb);
        }

        private ComboBoxEdit GetCbox()
        {
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    return cboxEditStepGroupBy;
                case 1:
                    return cboxEditEqpPlanOperationRate;
                case 2:
                    return cboxEditInoutBy;
                default:
                    return null;
            }
        }

        private ComboBoxEdit GetTimeIntervalCbox()
        {
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    return cboxEditTimeIntervalStepTrend;
                case 1:
                    return cboxEditTimeIntervalEqpPlanOperationRate;
                case 2:
                    return cboxEditTimeIntervalInout;
                default:
                    return null;
            }
        }

        private TimeEdit GetFromTime()
        {
            
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    return timeEditFromStepTrend;
                case 1:
                    return timeEditFromEqpPlanOperationRate;
                case 2:
                    return timeEditFromInout;
                default:
                    return null;
            }
        }

        private TimeEdit GetToTime()
        {
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    return timeEditToStepTrend;
                case 1:
                    return timeEditToEqpPlanOperationRate;
                case 2:
                    return timeEditToInout;
                default:
                    return null;
            }
        }

        private GridView GetGridView()
        {
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    return gridViewStepTrend;
                case 1:
                    return gridViewEqpPlanOperationRate;
                case 2:
                    return gridViewInout;
                default:
                    return null;
            }
        }

        private GridControl GetGridControl()
        {
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    return gridControlStepTrend;
                case 1:
                    return gridControlEqpPlanOperationRate;
                case 2:
                    return gridControlInout;
                default:
                    return null;
            }
        }

        private ChartControl GetChartControl()
        {
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    return chartControlStepTrend;
                case 1:
                    return chartControlEqpPlanOperationRate;
                case 2:
                    return chartControlInout;
                default:
                    return null;
            }
        }

        private NumericUpDown GetElapsedMin()
        {
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    return elapsedMinStepTrend;
                case 1:
                    return elapsedMinEqpPlanOperationRate;
                case 2:
                    return elapsedMinInout;
                default:
                    return null;
            }
        }

        private NumericUpDown GetElapsedMax()
        {
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    return elapsedMaxStepTrend;
                case 1:
                    return elapsedMaxEqpPlanOperationRate;
                case 2:
                    return elapsedMaxInout;
                default:
                    return null;
            }
        }

        private NumericUpDown GetCommandCountMax()
        {
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    return numericUpDownCommandCountYMaxStepTrend;
                case 1:
                    return numericUpDownCommandCountYMaxEqpPlanOperationRate;
                case 2:
                    return numericUpDownCommandCountYMaxInout;
                default:
                    return null;
            }
        }

        private NumericUpDown GetCommandCountMin()
        {
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    return numericUpDownCommandCountYMinStepTrend;
                case 1:
                    return numericUpDownCommandCountYMinEqpPlanOperationRate;
                case 2:
                    return numericUpDownCommandCountYMinInout;
                default:
                    return null;
            }
        }

        private NumericUpDown GetTimeMax()
        {
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    return numericUpDownTotalTimeYMaxStepTrend;
                case 1:
                    return numericUpDownTotalTimeYMaxEqpPlanOperationRate;
                case 2:
                    return numericUpDownTotalTimeYMaxInout;
                default:
                    return null;
            }
        }

        private NumericUpDown GetTimeMin()
        {
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    return numericUpDownTotalTimeYMinStepTrend;
                case 1:
                    return numericUpDownTotalTimeYMinEqpPlanOperationRate;
                case 2:
                    return numericUpDownTotalTimeYMinInout;
                default:
                    return null;
            }
        }

        private SimpleButton GetInquiryTargetForm()
        {
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    return btnInquiryTargetStepTrend;
                case 1:
                    return btnInquiryTargetEqpPlanOperationRateBy;
                case 2:
                    return btnInquiryTargetInout;
                default:
                    return null;
            }
        }


        private void gridViewStepTrend_DoubleClick(object sender, EventArgs e)
        {
            
            switch (ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    GridPartStepTrendLog rowStep = _dicGridPartStepTrend.ElementAt(gridViewStepTrend.FocusedRowHandle).Value;

                    ProductionReportFormDetails reportFormPartStepDetails = new ProductionReportFormDetails(rowStep.PartSteps, PinokioMain3Dmodel, _modelDesigner);
                    reportFormPartStepDetails.Show();
                    break;
                case 1:
                    GridEqpStepTrendLog rowEqp = _dicGridEqpPlanOperationRate.ElementAt(gridViewEqpPlanOperationRate.FocusedRowHandle).Value;

                    ProductionReportFormDetails reportFormEqpStepDetails = new ProductionReportFormDetails(rowEqp.EqpSteps, PinokioMain3Dmodel, _modelDesigner);
                    reportFormEqpStepDetails.Show();
                    break;

            }
        }
        

        private void cboxEditStepGroupBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedStepEqpNames.Clear();
            _selectedProductNames.Clear();
            _selectedStepNames.Clear();
            if (cboxEditStepGroupBy.SelectedItem.ToString() == "Equipment")
            {
                _isProduct = false;
                _isStep = false;
            }
            else if(cboxEditStepGroupBy.SelectedItem.ToString() == "Step")
            {
                _isProduct = false;
                _isStep = true;
            }
            else if (cboxEditStepGroupBy.SelectedItem.ToString() == "Product")
            {
                _isProduct = true;
                _isStep = false;
            }
            GetFromTime().Time = _simStartTime;
            GetToTime().Time = _simEndTime;
        }
        private void cboxEditEqpPlanOperationRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedStepGroupByNames.Clear();
            _selectedEqpGroupNames.Clear();
            _selectedStepGroupNames.Clear();
            _selectedStepGroupByNames.Clear();
            _selectedEqpGroupByNames.Clear();
            _isGroupCheck = false;
            
            if (cboxEditEqpPlanOperationRate.SelectedItem.ToString() == "EQP_GROUP")
            {
                _isEqpGroup = true;
            }
           else if (cboxEditEqpPlanOperationRate.SelectedItem.ToString() == "STEP_GROUP")
            {
                _isEqpGroup = false;
            }
            GetFromTime().Time = _simStartTime;
            GetToTime().Time = _simEndTime;
        }
        private void cboxEditInout_SelectedIndexChanged(object sender, EventArgs e)
        {

            _selectedNodeInoutNames.Clear();
            _selectedProductInoutNames.Clear();
            if (cboxEditInoutBy.SelectedItem.ToString() == "Node")
            {
                _isNode = true;
            }
            else if (cboxEditInoutBy.SelectedItem.ToString() == "Product")
            {
                _isNode = false;
            }
            GetFromTime().Time = _simStartTime;
            GetToTime().Time = _simEndTime;
        }

        
    }
}