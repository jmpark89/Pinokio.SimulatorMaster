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
using ComboBox = DevExpress.XtraEditors.ComboBox;

namespace Pinokio.Designer
{
    public partial class AMHSReportForm : DevExpress.XtraEditors.XtraForm
    {
        private bool _isChartModified;
        private bool _loadReport;
        private bool _isEmpty;
        public bool _isStartCheck = true;
        public bool _isEndCheck = true;
        public bool _isStartbtn = false;
        public bool _isEndbtn = false;
        public bool _isSearchClick = false;
        private DateTime _simStartTime;
        private DateTime _simEndTime;
        public List<string> _selectedSubCSNames;
        public List<string> _selectedStartMRNames;
        public List<string> _selectedEndMRNames;

        private PinokioBaseModel pinokio3DModel1;
        private ModelDesigner _modelDesigner;
        public PinokioBaseModel PinokioMain3Dmodel { get { return pinokio3DModel1; } }
        private int _trendTimeInterval;
        private TIME_UNIT _trendKPITimeUnit;
        public enum TIME_UNIT
        {
            Hour, Day, Week, Month
        }

        private Dictionary<DateTime, GridCommandTrendLog> _dicGridCommandTrend { get; set; }
        private Dictionary<DateTime, ChartCommandCountTrendLog> _dicChartCommandCount { get; set; }
        private Dictionary<DateTime, ChartCommandTimeTrendLog> _dicChartCommandSimTime { get; set; }

        private Dictionary<DateTime, GridCommandTrendLog> _dicGridCommandDistribution { get; set; }
        private Dictionary<DateTime, Dictionary<int, int>> _dicChartCommandDistribution { get; set; }

        private Dictionary<DateTime, GridCommandTrendLog> _dicGridVehicleOperationRate { get; set; }
        private Dictionary<DateTime, double> _dicChartVehicleOperationRate { get; set; }

        public AMHSReportForm(bool isLoad, PinokioBaseModel pinokio3DModel, ModelDesigner modelDesigner)
        {
            InitializeComponent();

            ModelManager.Instance.SimResultDBManager.Select4CommandTrend(isLoad);
            ModelManager.Instance.SimResultDBManager.Select4MRTrend(isLoad);

            if (isLoad)
            {
                _simStartTime = ModelManager.Instance.SimResultDBManager.LoadedSimulationStartTime;
                _simEndTime = ModelManager.Instance.SimResultDBManager.LoadedSimulationEndTime;
            }
            else
            {
                _simStartTime = ModelManager.Instance.SimResultDBManager.SimulationStartTime;
                _simEndTime = ModelManager.Instance.SimResultDBManager.SimulationEndTime;
            }

            for (int i = 0; i < AMHSTabPages.TabPages.Count; i++)
                InitcboxTabPages(i);

            AMHSTabPages.SelectedTabPageIndex = 0;

            _selectedSubCSNames = new List<string>();
            _selectedStartMRNames = new List<string>();
            _selectedEndMRNames = new List<string>();

            _trendTimeInterval = 60;
            _trendKPITimeUnit = TIME_UNIT.Hour;

            foreach(string startMR in ModelManager.Instance.SimResultDBManager.StartMRNames)
            {
                _selectedStartMRNames.Add(startMR);
            }
            foreach (string endMR in ModelManager.Instance.SimResultDBManager.EndMRNames)
            {
                _selectedEndMRNames.Add(endMR);
            }

            _dicGridCommandTrend = new Dictionary<DateTime, GridCommandTrendLog>();
            _dicChartCommandCount = new Dictionary<DateTime, ChartCommandCountTrendLog>();
            _dicChartCommandSimTime = new Dictionary<DateTime, ChartCommandTimeTrendLog>();

            _dicGridCommandDistribution = new Dictionary<DateTime, GridCommandTrendLog>();
            _dicChartCommandDistribution = new Dictionary<DateTime, Dictionary<int, int>>();

            _dicGridVehicleOperationRate = new Dictionary<DateTime, GridCommandTrendLog>();
            _dicChartVehicleOperationRate = new Dictionary<DateTime, double>();

            pinokio3DModel1 = pinokio3DModel;
            _modelDesigner = modelDesigner;
        }

        private void InitcboxTabPages(int tabNum)
        {
            AMHSTabPages.SelectedTabPageIndex = tabNum;
            if (tabNum != 0)
            {
                GetCbox().Properties.Items.Add("ALL");
            }
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    
                    break;
                case 1: 
                    foreach (string subCSName in ModelManager.Instance.SimResultDBManager.SubCSNames)
                        GetCbox().Properties.Items.Add(subCSName);
                    break;
                case 2:
                    foreach (string subCSName in ModelManager.Instance.SimResultDBManager.SubCSNames)
                        GetCbox().Properties.Items.Add(subCSName);
                    break;
                case 3:
                    foreach (string subCSName in ModelManager.Instance.SimResultDBManager.SubCSNames)
                        GetCbox().Properties.Items.Add(subCSName);
                    break;
            }
            if (tabNum != 0)
            {
                GetCbox().SelectedIndex = 0;
            }
            GetFromTime().Time = _simStartTime;
            GetToTime().Time = _simEndTime;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            SplashScreenManager.ShowForm(this, typeof(WaitFormSplash), true, true);

            try
            {
                GetGridView().OptionsView.ShowFooter = true;
                switch (AMHSTabPages.SelectedTabPageIndex)
                {
                    case 0:
                        _dicGridCommandTrend.Clear();
                        _dicChartCommandCount.Clear();
                        _dicChartCommandSimTime.Clear();
                        _isSearchClick = true;
                        break;
                    case 1:
                        _dicGridCommandTrend.Clear();
                        _dicChartCommandCount.Clear();
                        _dicChartCommandSimTime.Clear();
                        break;
                    case 2:
                        _dicGridCommandTrend.Clear();
                        _dicGridCommandDistribution.Clear();
                        _dicChartCommandDistribution.Clear();
                        break;
                    case 3:
                        _dicGridCommandTrend.Clear();
                        _dicGridVehicleOperationRate.Clear();
                        _dicChartVehicleOperationRate.Clear();
                        break;
                }
                UpdateTimeInterval();
                UpdateTabPage();
                UpdateGridControl();
                UpdateChart();
            }
            catch (Exception ex)
            {
//                throw;
            }
            SplashScreenManager.CloseForm(false, true);
        }

        private void btnInquiryTarget_Click(object sender, EventArgs e)
        {
            
            AMHSInquiryTargetForm targetForm = new AMHSInquiryTargetForm(this);
            targetForm.StartPosition = FormStartPosition.Manual;
            Point p = GetInquiryTargetForm().PointToScreen(new Point(GetInquiryTargetForm().Width, 0));
            targetForm.SetDesktopLocation(p.X, p.Y);

            _selectedSubCSNames = new List<string>();

            if (targetForm.ShowDialog() == DialogResult.OK)
            {
                foreach (var item in targetForm.checkedListBox.CheckedItems)
                    _selectedSubCSNames.Add(item.ToString());
            }
        }
        private void btnStartTarget_Click(object sender, EventArgs e)
        {
            
            _isStartbtn = true;
            _isEndbtn = false;
            MR mr = new MR();
            AMHSInquiryTargetForm targetForm = new AMHSInquiryTargetForm(this);
            targetForm.StartPosition = FormStartPosition.Manual;
            Point p = GetInquiryTargetForm().PointToScreen(new Point(GetInquiryTargetForm().Width, 0));
            targetForm.SetDesktopLocation(p.X, p.Y);

            _selectedStartMRNames = new List<string>();

            if (targetForm.ShowDialog() == DialogResult.OK)
            {
                _selectedStartMRNames.Clear();
                foreach (var item in targetForm.checkedListBox.CheckedItems)
                { _selectedStartMRNames.Add(item.ToString()); }
                if (_selectedStartMRNames.Count > 0)
                {
                    _isStartCheck = true;
                }
                else
                {
                    _isStartCheck = false;
                }
                UpdateTabPage();
            }
        }
        private void btnEndTarget_Click(object sender, EventArgs e)
        {
           
            _isEndbtn = true;
            _isStartCheck = false;
            _isStartbtn = false;
            AMHSInquiryTargetForm targetForm = new AMHSInquiryTargetForm(this);
            targetForm.StartPosition = FormStartPosition.Manual;
            Point p = GetInquiryTargetForm().PointToScreen(new Point(GetInquiryTargetForm().Width, 0));
            targetForm.SetDesktopLocation(p.X, p.Y);

            _selectedEndMRNames = new List<string>();

            if (targetForm.ShowDialog() == DialogResult.OK)
            {
                _selectedEndMRNames.Clear();
                foreach (var item in targetForm.checkedListBox.CheckedItems)
                { _selectedEndMRNames.Add(item.ToString()); }
                if (_selectedEndMRNames.Count > 0)
                {
                    _isEndCheck = true;
                }
                else
                {
                    _isEndCheck = false;
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
                string selectedItem=null;
                DateTime fromTime = (DateTime)GetFromTime().EditValue;
                DateTime toTime = (DateTime)GetToTime().EditValue;
                if (AMHSTabPages.SelectedTabPageIndex != 0)
                {
                    selectedItem = GetCbox().SelectedItem.ToString();
                }
                
                List<Vehicle> vehicles = new List<Vehicle>();
                Dictionary<MR, double> mrs = new Dictionary<MR, double>();
                Dictionary<Command, string> commands = new Dictionary<Command, string>();

                switch (AMHSTabPages.SelectedTabPageIndex)
                {
                    case 0:
                        if (_isStartCheck)
                        {
                            mrs = ModelManager.Instance.SimResultDBManager.SelectMRLog(fromTime, toTime, _selectedStartMRNames, _selectedEndMRNames, _loadReport, _isStartCheck);
                            mrs = mrs.OrderBy(x => x.Key.ActivatedDateTime).ToDictionary(pair => pair.Key, pair => pair.Value);
                            
                            _selectedEndMRNames.Clear();
                            foreach (string endMR in ModelManager.Instance.SimResultDBManager.EndMRNames)
                            {
                                _selectedEndMRNames.Add(endMR);
                            }

                        }
                        else
                        {
                            
                                mrs = ModelManager.Instance.SimResultDBManager.SelectMRLog(fromTime, toTime, _selectedStartMRNames, _selectedEndMRNames, _loadReport, _isStartCheck);
                                mrs = mrs.OrderBy(x => x.Key.ActivatedDateTime).ToDictionary(pair => pair.Key, pair => pair.Value);
                            
                            
                        }
                        break;
                    case 1:
                        commands = ModelManager.Instance.SimResultDBManager.SelectCommandLog(fromTime, toTime, selectedItem, _selectedSubCSNames, _loadReport);
                        commands = commands.OrderBy(x => x.Key.ActivatedDateTime).ToDictionary(pair => pair.Key, pair => pair.Value);
                        break;
                    case 2:
                        commands = ModelManager.Instance.SimResultDBManager.SelectCommandLog(fromTime, toTime, selectedItem, _selectedSubCSNames, _loadReport);
                        commands = commands.OrderBy(x => x.Key.ActivatedDateTime).ToDictionary(pair => pair.Key, pair => pair.Value);
                                       
                        GridCommandTrendLog ChartDistribution = new GridCommandTrendLog(fromTime, commands.Keys.OrderBy(x => x.TotalTime).ToList());
                        ChartDistribution.dicSigma = new Dictionary<int, int>();
                        for (int i = 1; i < SigmaColumnNumCMDDistribution.Value + 1; i++)
                        {
                            int counter = 0;
                            foreach (Command command in commands.Keys)
                            {
                                if (Convert.ToDouble((i - 1) * DistributionIntervalCMDDistribution.Value) <= command.TotalTime && command.TotalTime < Convert.ToDouble(i * DistributionIntervalCMDDistribution.Value))
                                    counter++;
                            }
                            ChartDistribution.dicSigma.Add(Convert.ToInt32(i * DistributionIntervalCMDDistribution.Value), counter);
                        }
                        _dicChartCommandDistribution.Add(fromTime, ChartDistribution.dicSigma);                       
                        break;
                    case 3:
                        commands = ModelManager.Instance.SimResultDBManager.SelectCommandLog(fromTime, toTime, selectedItem, _selectedSubCSNames, _loadReport);
                        commands = commands.OrderBy(x => x.Key.ActivatedDateTime).ToDictionary(pair => pair.Key, pair => pair.Value);

                        foreach (Command command in commands.Keys)                        
                            if (!vehicles.Contains(command.Vehicle))                            
                                vehicles.Add(command.Vehicle);                                                    
                        break;
                    default:
                        break;
                }

                _isEmpty = true;

                for (; fromTime < toTime; fromTime = fromTime.AddSeconds(_trendTimeInterval * 60))
                {
                    DateTime endTime = fromTime.AddSeconds(_trendTimeInterval * 60);

                    switch (AMHSTabPages.SelectedTabPageIndex)
                    {
                        case 0:
                            Dictionary<MR, bool> dicMRs = new Dictionary<MR, bool>();
                            foreach (MR mr in mrs.Keys.ToList())
                            {
                                if (fromTime <= mr.ActivatedDateTime && mr.ActivatedDateTime < endTime)
                                {
                                    dicMRs.Add(mr, true);
                                    mrs.Remove(mr);
                                }
                                if ((mr.CompletedDateTime - mr.ActivatedDateTime).TotalSeconds / 60 < (double)GetElapsedMin().Value)
                                    dicMRs.Remove(mr);
                                if ((mr.CompletedDateTime - mr.ActivatedDateTime).TotalSeconds / 60 > (double)GetElapsedMax().Value)
                                    dicMRs.Remove(mr);
                                if (endTime <= mr.ActivatedDateTime)
                                    break;
                            }
                            if (dicMRs.Count != 0)
                            {
                                GridCommandTrendLog commandLog = new GridCommandTrendLog(fromTime, dicMRs.Keys.ToList());

                                _dicGridCommandTrend.Add(fromTime, commandLog);

                                ChartCommandCountTrendLog chartCommandCountTrendLog = new ChartCommandCountTrendLog();
                                chartCommandCountTrendLog.Time = fromTime;
                                chartCommandCountTrendLog.CommandCount = dicMRs.Count;
                                _dicChartCommandCount.Add(fromTime, chartCommandCountTrendLog);

                                ChartCommandTimeTrendLog chartCommandTimeTrendLog = new ChartCommandTimeTrendLog();
                                chartCommandTimeTrendLog.Time = fromTime;
                                chartCommandTimeTrendLog.SimTotalTimeAvg = commandLog.SimTotalTimeAvg;
                                _dicChartCommandSimTime.Add(fromTime, chartCommandTimeTrendLog);
                                _isEmpty = false;
                            }
                            break;
                        case 1:
                            Dictionary<Command, bool> dicCommands = new Dictionary<Command, bool>();
                            foreach (Command command in commands.Keys.ToList())
                            {
                                if (fromTime <= command.ActivatedDateTime && command.ActivatedDateTime < endTime)
                                {
                                    dicCommands.Add(command, true);
                                    commands.Remove(command);
                                }
                                if ((command.CompletedDateTime - command.ActivatedDateTime).TotalSeconds / 60 < (double)GetElapsedMin().Value)
                                    dicCommands.Remove(command);
                                if ((command.CompletedDateTime - command.ActivatedDateTime).TotalSeconds / 60 > (double)GetElapsedMax().Value)
                                    dicCommands.Remove(command);
                                if (endTime <= command.ActivatedDateTime)
                                    break;
                            }
                            if (dicCommands.Count != 0)
                            {
                                GridCommandTrendLog commandLog = new GridCommandTrendLog(fromTime, dicCommands.Keys.ToList());

                                _dicGridCommandTrend.Add(fromTime, commandLog);

                                ChartCommandCountTrendLog chartCommandCountTrendLog = new ChartCommandCountTrendLog();
                                chartCommandCountTrendLog.Time = fromTime;
                                chartCommandCountTrendLog.CommandCount = dicCommands.Count;
                                _dicChartCommandCount.Add(fromTime, chartCommandCountTrendLog);

                                ChartCommandTimeTrendLog chartCommandTimeTrendLog = new ChartCommandTimeTrendLog();
                                chartCommandTimeTrendLog.Time = fromTime;
                                chartCommandTimeTrendLog.SimTotalTimeAvg = commandLog.SimTotalTimeAvg;
                                _dicChartCommandSimTime.Add(fromTime, chartCommandTimeTrendLog);
                                _isEmpty = false;
                            }
                            break;
                        case 2:
                            Dictionary<Command, bool> dicDistribution = new Dictionary<Command, bool>();
                            foreach (Command command in commands.Keys.ToList())
                            {
                                if (fromTime <= command.ActivatedDateTime && command.ActivatedDateTime < endTime)
                                {
                                    dicDistribution.Add(command, true);
                                    commands.Remove(command);
                                }
                                if ((command.CompletedDateTime - command.ActivatedDateTime).TotalSeconds / 60 < (double)GetElapsedMin().Value)
                                    dicDistribution.Remove(command);
                                if ((command.CompletedDateTime - command.ActivatedDateTime).TotalSeconds / 60 > (double)GetElapsedMax().Value)
                                    dicDistribution.Remove(command);
                                if (endTime <= command.ActivatedDateTime)
                                    break;
                            }
                            if (dicDistribution.Count != 0)
                            {
                                GridCommandTrendLog GridDistribution = new GridCommandTrendLog(fromTime, dicDistribution.Keys.OrderBy(x => x.TotalTime).ToList());

                                GridDistribution.dicSigma = new Dictionary<int, int>();

                                for (int i = 1; i < SigmaColumnNumCMDDistribution.Value + 1; i++)
                                {
                                    int counter = 0;
                                    foreach (Command command in dicDistribution.Keys)
                                        if (Convert.ToDouble((i - 1) * DistributionIntervalCMDDistribution.Value) <= command.TotalTime && command.TotalTime < Convert.ToDouble(i * DistributionIntervalCMDDistribution.Value))
                                            counter++;

                                    GridDistribution.dicSigma.Add(Convert.ToInt32(i * DistributionIntervalCMDDistribution.Value), counter);
                                }
                                _dicGridCommandDistribution.Add(fromTime, GridDistribution);
                                _isEmpty = false;
                            }
                            break;
                        case 3:
                            Dictionary<Command, bool> dicVehicleOperationRate = new Dictionary<Command, bool>();
                            foreach (Command command in commands.Keys.ToList())
                            {
                                if (fromTime >= command.AssignedDateTime && fromTime < command.CompletedDateTime)
                                {
                                    dicVehicleOperationRate.Add(command, true);
                                }
                                else if (endTime >= command.AssignedDateTime && endTime < command.CompletedDateTime)
                                {
                                    dicVehicleOperationRate.Add(command, true);
                                }
                                else if (fromTime <= command.AssignedDateTime && endTime > command.CompletedDateTime)
                                {
                                    dicVehicleOperationRate.Add(command, true);
                                    commands.Remove(command);
                                }
                            }
                            if (dicVehicleOperationRate.Count != 0)
                            {     
                                GridCommandTrendLog GridVehicleOperationRate = new GridCommandTrendLog(fromTime, dicVehicleOperationRate.Keys.OrderBy(x => x.TotalTime).ToList());
                                GridVehicleOperationRate.IntervalMin = _trendTimeInterval;
                                GridVehicleOperationRate.VehicleTotalCount = vehicles.Count;
                                GridVehicleOperationRate.dicOperateRate = new Dictionary<DateTime, double>();

                                GridVehicleOperationRate.GetVehiclesOperatingRate(dicVehicleOperationRate.Keys.ToList(), fromTime, endTime);

                                _dicGridVehicleOperationRate.Add(fromTime, GridVehicleOperationRate);

                                GridVehicleOperationRate.dicOperateRate.Add(fromTime, GridVehicleOperationRate.VehicleOperatingRate);

                                _dicChartVehicleOperationRate = GridVehicleOperationRate.dicOperateRate;
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
                    MessageBox.Show("Command가 없습니다.");
                }
                if (_selectedStartMRNames.Count == 0&& _isSearchClick)
                {
                    GetGridView().OptionsView.ShowFooter = false;
                    MessageBox.Show("선택한 Node가 없습니다.");
                    _dicGridCommandTrend.Clear();
                    _dicChartCommandCount.Clear();
                    _dicChartCommandSimTime.Clear();
                }
                _isSearchClick = false;
            }
            catch (Exception ex)
            {

            }
        }

        private void UpdateGridControl()
        {
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    GetGridControl().DataSource = _dicGridCommandTrend.Values.ToList();
                    break;
                case 1:
                    GetGridControl().DataSource = _dicGridCommandTrend.Values.ToList();
                    break;
                case 2:
                    GetGridControl().DataSource = GetCommandDistributionDataTable(Convert.ToInt32(SigmaColumnNumCMDDistribution.Value), _dicGridCommandDistribution.Values.ToList());
                    SetCommandDistributionDataTable(Convert.ToInt32(SigmaColumnNumCMDDistribution.Value));
                    break;
                case 3:
                    GetGridControl().DataSource = _dicGridVehicleOperationRate.Values.ToList();
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

                switch (AMHSTabPages.SelectedTabPageIndex)
                {
                    case 0:
                        GetChartControl().Series["MR Count"].DataSource = _dicChartCommandCount.Values;
                        GetChartControl().Series["Total Time(min)"].DataSource = _dicChartCommandSimTime.Values;
                        if (_dicChartCommandCount.Count > 0)
                            maxCount = _dicChartCommandCount.Values.Max(x => x.CommandCount);
                        if (_dicChartCommandSimTime.Count > 0)
                            maxTime = _dicChartCommandSimTime.Values.Max(x => x.SimTotalTimeAvg);
                        ((XYDiagram)GetChartControl().Diagram).AxisX.DateTimeScaleOptions.MeasureUnitMultiplier = _trendTimeInterval;
                        break;
                    case 1:
                        GetChartControl().Series["Command Count"].DataSource = _dicChartCommandCount.Values;
                        GetChartControl().Series["Total Time(min)"].DataSource = _dicChartCommandSimTime.Values;
                        if (_dicChartCommandCount.Count > 0)
                            maxCount = _dicChartCommandCount.Values.Max(x => x.CommandCount);
                        if (_dicChartCommandSimTime.Count > 0)
                            maxTime = _dicChartCommandSimTime.Values.Max(x => x.SimTotalTimeAvg);
                        ((XYDiagram)GetChartControl().Diagram).AxisX.DateTimeScaleOptions.MeasureUnitMultiplier = _trendTimeInterval;
                        break;
                    case 2:
                        foreach (Dictionary<int, int> DicDistribution in _dicChartCommandDistribution.Values)
                        {
                            foreach (int key in DicDistribution.Keys)
                                if (key != 0)
                                {
                                    GetChartControl().Series["Command Count"].Points.AddPoint(key, DicDistribution[key]);
                                    if (DicDistribution[key] > maxCount)
                                        maxCount = DicDistribution[key];
                                }
                        }
                        maxTime = Convert.ToDouble(DistributionIntervalCMDDistribution.Value * SigmaColumnNumCMDDistribution.Value);
                        ((XYDiagram)GetChartControl().Diagram).AxisX.WholeRange.SetMinMaxValues(minTime, maxTime);
                        GetChartControl().Series["Command Count"].ChangeView(ViewType.Bar);
                        GetChartControl().Dock = DockStyle.Left;
                        ((XYDiagram)GetChartControl().Diagram).AxisX.DateTimeScaleOptions.MeasureUnitMultiplier = Convert.ToInt32(DistributionIntervalCMDDistribution.Value / 10);

                        ((SideBySideBarSeriesView)GetChartControl().Series[0].View).BarWidth = Convert.ToInt32(DistributionIntervalCMDDistribution.Value);
                        ((SideBySideBarSeriesView)GetChartControl().Series[0].View).BarDistance = 0;
                        break;
                    case 3:
                        maxTime = 100;
                        GetChartControl().Series["Command Count"].DataSource = _dicGridVehicleOperationRate.Values;
                        GetChartControl().Series["Vehicle Operating Rate (%)"].Points.Clear();
                        foreach (GridCommandTrendLog gridCommandTrendLog in _dicGridVehicleOperationRate.Values)
                        {
                            foreach (DateTime time in gridCommandTrendLog.dicOperateRate.Keys)
                                GetChartControl().Series["Vehicle Operating Rate (%)"].Points.AddPoint(time, Math.Round(gridCommandTrendLog.dicOperateRate[time], 1));
                        }
                        GetChartControl().Series["Vehicle Operating Rate (%)"].ChangeView(ViewType.Line);

                        maxCount = _dicGridVehicleOperationRate.Values.Max(x => x.CommandCount);
                        
                        ((XYDiagram)GetChartControl().Diagram).AxisX.DateTimeScaleOptions.MeasureUnitMultiplier = _trendTimeInterval;
                        break;
                    default:
                        break;
                }

                ((XYDiagram)GetChartControl().Diagram).AxisY.WholeRange.SideMarginsValue = 20;
                ((XYDiagram)GetChartControl().Diagram).AxisX.WholeRange.SideMarginsValue = 20;

                ((XYDiagram)GetChartControl().Diagram).AxisY.WholeRange.SetMinMaxValues(minCount, maxCount + maxCount * 0.2);

                if (AMHSTabPages.SelectedTabPageIndex != 2)
                    ((XYDiagram)GetChartControl().Diagram).SecondaryAxesY[0].WholeRange.SetMinMaxValues(minTime, maxTime + maxTime * 0.2);

                GetChartControl().RefreshData();

                object Ymax = ((XYDiagram)GetChartControl().Diagram).AxisY.WholeRange.MaxValue;

                GetCommandCountMax().Value = decimal.Parse(Ymax.ToString());

                object Ymin = ((XYDiagram)GetChartControl().Diagram).AxisY.WholeRange.MinValue;

                GetCommandCountMin().Value = decimal.Parse(Ymin.ToString());

                if (AMHSTabPages.SelectedTabPageIndex != 2)
                {
                    object YTotalmax = ((XYDiagram)GetChartControl().Diagram).SecondaryAxesY[0].WholeRange.MaxValue;

                    GetTimeMax().Value = decimal.Parse(YTotalmax.ToString());

                    object YTotalmin = ((XYDiagram)GetChartControl().Diagram).SecondaryAxesY[0].WholeRange.MinValue;

                    GetTimeMin().Value = decimal.Parse(YTotalmin.ToString());
                }
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

            string ExportExcelPath = Directory.GetCurrentDirectory() + "/" + AMHSTabPages.SelectedTabPage.Name.ToString();
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

            string path = ExportExcelPath + "/" + AMHSTabPages.SelectedTabPage.Name.ToString() + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + DateTime.Now.Second + ".xlsx";
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

        private void numericUpDowntTotalTimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (_isChartModified)
                return;

            double v = (double)(sender as NumericUpDown).Value;
            ((XYDiagram)GetChartControl().Diagram).SecondaryAxesY[0].WholeRange.MaxValue = v;
            ((XYDiagram)GetChartControl().Diagram).SecondaryAxesY[0].WholeRange.SideMarginsValue = 0;
        }

        private void numericUpDownTotalTimeMin_ValueChanged(object sender, EventArgs e)
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
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    return null;
                case 1:
                    return cboxEditCMDTrend;
                case 2:
                    return cboxEditCMDDistribution;
                case 3:
                    return cboxEditVehicleOperationRate;
                default:
                    return null;
            }
        }

        private ComboBoxEdit GetTimeIntervalCbox()
        {
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    return cboxEditTimeIntervalMRTrend;
                case 1:
                    return cboxEditTimeIntervalCMDTrend;
                case 2:
                    return cboxEditTimeIntervalCMDDistribution;
                case 3:
                    return cboxEditTimeIntervalVehicleOperationRate;
                default:
                    return null;
            }
        }

        private TimeEdit GetFromTime()
        {
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    return timeEditFromMRTrend;
                case 1:
                    return timeEditFromCMDTrend;
                case 2:
                    return timeEditFromCMDDistribution;
                case 3:
                    return timeEditFromVehicleOperationRate;
                default:
                    return null;
            }
        }

        private TimeEdit GetToTime()
        {
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    return timeEditToMRTrend;
                case 1:
                    return timeEditToCMDTrend;
                case 2:
                    return timeEditToCMDDistribution;
                case 3:
                    return timeEditToVehicleOperationRate;
                default:
                    return null;
            }
        }

        private GridView GetGridView()
        {
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    return gridViewMRTrend;
                case 1:
                    return gridViewCMDTrend;
                case 2:
                    return gridViewCMDDistribution;
                case 3:
                    return gridViewVehicleOperationRate;
                default:
                    return null;
            }
        }

        private GridControl GetGridControl()
        {
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    return gridControlMRTrend;
                case 1:
                    return gridControlCMDTrend;
                case 2:
                    return gridControlCMDDistribution;
                case 3:
                    return gridControlVehicleOperationRate;
                default:
                    return null;
            }
        }

        private ChartControl GetChartControl()
        {
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    return chartControlMRTrend;
                case 1:
                    return chartControlCMDTrend;
                case 2:
                    return chartControlCMDDistribution;
                case 3:
                    return chartControlVehicleOperationRate;
                default:
                    return null;
            }
        }

        private NumericUpDown GetElapsedMin()
        {
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    return elapsedMinMRTrend;
                case 1:
                    return elapsedMinCMDTrend;
                case 2:
                    return elapsedMinCMDDistribution;
                case 3:
                    return elapsedMinVehicleOperationRate;
                default:
                    return null;
            }
        }

        private NumericUpDown GetElapsedMax()
        {
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    return elapsedMaxMRTrend;
                case 1:
                    return elapsedMaxCMDTrend;
                case 2:
                    return elapsedMaxCMDDistribution;
                case 3:
                    return elapsedMaxVehicleOperationRate;
                default:
                    return null;
            }
        }

        private NumericUpDown GetCommandCountMax()
        {
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    return numericUpDownMRCountYMaxMRTrend;
                case 1:
                    return numericUpDownCommandCountYMaxCMDTrend;
                case 2:
                    return numericUpDownCommandCountYMaxCMDDistribution;
                case 3:
                    return numericUpDownCommandCountYMaxVehicleOperationRate;
                default:
                    return null;
            }
        }

        private NumericUpDown GetCommandCountMin()
        {
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    return numericUpDownMRCountYMinMRTrend;
                case 1:
                    return numericUpDownCommandCountYMinCMDTrend;
                case 2:
                    return numericUpDownCommandCountYMinCMDDistribution;
                case 3:
                    return numericUpDownCommandCountYMinVehicleOperationRate;
                default:
                    return null;
            }
        }

        private NumericUpDown GetTimeMax()
        {
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    return numericUpDownTotalTimeYMaxMRTrend;
                case 1:
                    return numericUpDownTotalTimeYMaxCMDTrend;
                case 2:
                    return null;
                case 3:
                    return numericUpDownTotalTimeYMaxVehicleOperationRate;
                default:
                    return null;
            }
        }

        private NumericUpDown GetTimeMin()
        {
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    return numericUpDownTotalTimeYMinMRTrend;
                case 1:
                    return numericUpDownTotalTimeYMinCMDTrend;
                case 2:
                    return null;
                case 3:
                    return numericUpDownTotalTimeYMinVehicleOperationRate;
                default:
                    return null;
            }
        }

        private SimpleButton GetInquiryTargetForm()
        {
            switch (AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    return btnStartTarget;
                case 1:
                    return btnInquiryTargetCMDTrend;
                case 2:
                    return btnInquiryTargetCMDDistribution;
                case 3:
                    return btnInquiryTargetVehicleOperationRate;
                default:
                    return null;
            }
        }

        private DataTable GetCommandDistributionDataTable(int columnCount, List<GridCommandTrendLog> gridCommandTrendLog)
        {

            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn();
            dc1.ColumnName = "Time";

            DataColumn dc2 = new DataColumn();
            dc2.ColumnName = "Command Count";

            DataColumn dc3 = new DataColumn();
            dc3.ColumnName = "Avg Time";

            DataColumn dc4 = new DataColumn();
            dc4.ColumnName =  SigmaLevelCMDDistribution.Value.ToString() + "σ Time";

            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);

            for (int i = 0; i < columnCount; i++)
            {
                DataColumn dataColumn = new DataColumn(i.ToString());
                dt.Columns.Add(dataColumn);
            }

            for (int i = 0; i < gridCommandTrendLog.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = gridCommandTrendLog[i].Time;
                dr[1] = gridCommandTrendLog[i].CommandCount;
                dr[2] = gridCommandTrendLog[i].SimTotalTimeAvg * 60;
                dr[3] = gridCommandTrendLog[i].GetNSigmaTime(Convert.ToInt32(SigmaLevelCMDDistribution.Value));

                for (int j = 4; j < columnCount + 4; j++)
                    dr[j] = gridCommandTrendLog[i].dicSigma[Convert.ToInt32((j - 3) * DistributionIntervalCMDDistribution.Value)];

                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void SetCommandDistributionDataTable(int columnCount)
        {
            try
            {
                gridViewCMDDistribution.BeginUpdate();
                gridViewCMDDistribution.Columns.Clear();
                GridColumn gridColumnTime = GetCommandDistributionColumn("Time", "Time");
                GridColumn gridColumnCommandCount = GetCommandDistributionColumn("Command Count", "Command Count");
                GridColumn gridColumnAvgTime = GetCommandDistributionColumn("Avg Time", "Avg Time (sec)");
                GridColumn gridColumnSigmaTime = GetCommandDistributionColumn(SigmaLevelCMDDistribution.Value.ToString() + "σ Time", SigmaLevelCMDDistribution.Value.ToString() + "σ Time (sec)");

                gridViewCMDDistribution.Columns.Add(gridColumnTime);
                gridViewCMDDistribution.Columns.Add(gridColumnCommandCount);
                gridViewCMDDistribution.Columns.Add(gridColumnAvgTime);
                gridViewCMDDistribution.Columns.Add(gridColumnSigmaTime);

                double interval = (double)DistributionIntervalCMDDistribution.Value;
                for (int i = 0; i < columnCount; i++)
                {
                    GridColumn gc = GetCommandDistributionColumn(i.ToString(), (interval * (i + 1)).ToString() + " (sec)");
                    gridViewCMDDistribution.Columns.Add(gc);
                }

                gridViewCMDDistribution.EndUpdate();
            }
            catch (Exception ex)
            {

            }
        }

        private GridColumn GetCommandDistributionColumn(string name, string caption)
        {
            GridColumn gridColumn = new GridColumn();
            gridColumn.Name = name;
            gridColumn.FieldName = name;
            gridColumn.Caption = caption;
            gridColumn.AppearanceCell.Options.UseTextOptions = true;
            gridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridColumn.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            if (name == "Time")
            {
                gridColumn.Summary.AddRange(new GridSummaryItem[] {
                new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, name, "Min"),
                new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, name, "Max"),
                new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, name, "Average"),
                new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, name, "Sum")});
            }
            else if (name == "Command Count")
            {
                gridColumn.Summary.AddRange(new GridSummaryItem[] {
                new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, name, "{0}"),
                new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, name, "{0}"),
                new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, name, "{0:0.##}"),
                new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, name, "{0:0.##}")});
            }
            else
            {
                gridColumn.Summary.AddRange(new GridSummaryItem[] {
                new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, name, "{0}"),
                new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, name, "{0}"),
                new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Average, name, "{0:0.##}")});
            }

            gridColumn.Visible = true;
            return gridColumn;
        }

        private void cboxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedSubCSNames = new List<string>();
            if (GetCbox().Text == "ALL")
                _selectedSubCSNames.Add(GetCbox().Properties.Items.ToString());
            else
                _selectedSubCSNames.Add(GetCbox().Text);
        }

        private void gridViewCMDTrend_DoubleClick(object sender, EventArgs e)
        {
            GridCommandTrendLog row = _dicGridCommandTrend.ElementAt(gridViewCMDTrend.FocusedRowHandle).Value;

     
               
            AMHSReportFormDetails reportFormDetails = new AMHSReportFormDetails(row.Commands, PinokioMain3Dmodel, _modelDesigner);
            reportFormDetails.Show();
        }

        private void gridViewMRTrend_DoubleClick(object sender, EventArgs e)
        {
            GridCommandTrendLog row = _dicGridCommandTrend.ElementAt(gridViewMRTrend.FocusedRowHandle).Value;
            AMHSReportFormDetails reportFormDetails = new AMHSReportFormDetails(row.MRs, PinokioMain3Dmodel, _modelDesigner);
            reportFormDetails.Show();
        }
    }
}