using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinokio
{
    public partial class ucSPSBufferSummary : UserControl
    {
        public ucSPSBufferSummary()
        {
            InitializeComponent();
            //InitChartData();
            //InitializeKPIData();
        }

        private void InitChartData()
        {
            InitDoughnutChart();
            InitHorizontalChart1();
            InitHorizontalChart2();
        }
        private void InitializeKPIData()
        {
            Thread threadTPState = new Thread(new ThreadStart(() => UpdateTPState()));
            threadTPState.Start();

            Thread threadBufferState = new Thread(new ThreadStart(() => UpdateBufferState()));
            threadBufferState.Start();
        }

        private void UpdateTPState()
        {
            /*while (true)
            {
                Thread.Sleep(500);

                if (DataMart.Instance == null)
                    continue;

                var SPSonValue = DataMart.Instance._dicSPSState.Where(x => x.Value.Equals("OnBox")).Count();
                var SPSoffValue = DataMart.Instance._dicSPSState.Where(x => x.Value.Equals("OffBox")).Count();
                var SPSMatchValue = DataMart.Instance._dicSPSState.Where(x => x.Value.Equals("MatchBox")).Count();

                UpdateInvokeTPState(SPSonValue, SPSoffValue, SPSMatchValue);
            }*/
        }

        delegate void UpdateTPStateDelegate(int SPSonValue, int SPSoffValue, int SPSMatchValue);
        private void UpdateInvokeTPState(int SPSonValue, int SPSoffValue, int SPSMatchValue)
        {
            this.Invoke(new UpdateTPStateDelegate(UpdateTPStateData), new object[] { SPSonValue, SPSoffValue, SPSMatchValue });
        }

        private void UpdateTPStateData(int SPSonValue, int SPSoffValue, int SPSMatchValue)
        {
            double[] SPSonArray = new double[1];
            SPSonArray[0] = (double)SPSonValue;

            double[] SPSoffArray = new double[1];
            SPSoffArray[0] = (double)SPSoffValue;

            double[] SPSMatchArray = new double[1];
            SPSMatchArray[0] = (double)SPSMatchValue;

            this.chartControl1.Series[0].Points[0].Values = SPSonArray;
            this.chartControl1.Series[0].Points[1].Values = SPSoffArray;
            this.chartControl1.Series[0].Points[2].Values = SPSMatchArray;
        }

        private void UpdateBufferState()
        {
            /*while (true)
            {
                Thread.Sleep(500);

                if (DataMart.Instance == null)
                    continue;

                Dictionary<string, ModelChangeGridData> modelChangeGridDatas = new Dictionary<string, ModelChangeGridData>();

                if (true*//*"SPSBuffer"*//*)
                {
                    foreach (var pair in DataMart.Instance._dicFrontSetLine)
                    {
                        if (modelChangeGridDatas.ContainsKey(pair.Value.ProductID) == false)
                        {
                            //A이면
                            if (pair.Value.Mix == MIX.A)
                            {
                                ModelChangeGridData currentData = new ModelChangeGridData();
                                currentData.AModelSuffix = pair.Value.ProductID;
                                currentData.ModelType = "A";
                                currentData.ASetCount++;
                                modelChangeGridDatas.Add(pair.Value.ProductID, currentData);
                            }
                            //B이면
                            else
                            {
                                ModelChangeGridData currentData = new ModelChangeGridData();
                                currentData.BModelSuffix = pair.Value.ProductID;
                                currentData.ModelType = "B";
                                currentData.BSetCount++;
                                modelChangeGridDatas.Add(pair.Value.ProductID, currentData);
                            }
                        }
                        else
                        {
                            //A이면
                            if (pair.Value.Mix == MIX.A)
                            {
                                ModelChangeGridData existData = modelChangeGridDatas[pair.Value.ProductID];
                                existData.ASetCount++;
                            }
                            //B이면
                            else
                            {
                                ModelChangeGridData existData = modelChangeGridDatas[pair.Value.ProductID];
                                existData.BSetCount++;
                            }
                        }
                    }

                    foreach (var queue in DataMart.Instance._dicSPSBuffer.Values)
                    {
                        foreach (var pair in queue)
                        {
                            if (modelChangeGridDatas.ContainsKey(pair.ProductID) == false)
                            {
                                //A이면
                                if (pair.Mix == MIX.A)
                                {
                                    ModelChangeGridData currentData = new ModelChangeGridData();
                                    currentData.AModelSuffix = pair.ProductID;
                                    currentData.ModelType = "A";
                                    currentData.ASPSCount++;
                                    modelChangeGridDatas.Add(pair.ProductID, currentData);
                                }
                                //B이면
                                else
                                {
                                    ModelChangeGridData currentData = new ModelChangeGridData();
                                    currentData.BModelSuffix = pair.ProductID;
                                    currentData.ModelType = "B";
                                    currentData.BSPSCount++;
                                    modelChangeGridDatas.Add(pair.ProductID, currentData);
                                }
                            }
                            else
                            {
                                //A이면
                                if (pair.Mix == MIX.A)
                                {
                                    ModelChangeGridData existData = modelChangeGridDatas[pair.ProductID];
                                    existData.ASPSCount++;
                                }
                                //B이면
                                else
                                {
                                    ModelChangeGridData existData = modelChangeGridDatas[pair.ProductID];
                                    existData.BSPSCount++;
                                }
                            }
                        }
                    }
                }

                *//*DataMart.Instance._dicSPSBuffer.Where(x => x.Value.Where(y => y.Mix.Equals(MIX.A)))

                var SPSonValue = DataMart.Instance._dicSPSState.Where(x => x.Value.Equals(false)).Count();
                var SPSoffValue = DataMart.Instance._dicSPSState.Where(x => x.Value.Equals(true)).Count();

                UpdateInvokeBufferState(SPSonValue, SPSoffValue);*//*
            }*/
        }

        delegate void UpdateBufferStateDelegate(int SPSonValue, int SPSoffValue);
        private void UpdateInvokeBufferState(int SPSonValue, int SPSoffValue)
        {
            this.Invoke(new UpdateBufferStateDelegate(UpdateBufferStateData), new object[] { SPSonValue, SPSoffValue });
        }

        private void UpdateBufferStateData(int SPSonValue, int SPSoffValue)
        {
            double[] SPSonArray = new double[1];
            SPSonArray[0] = (double)SPSonValue;

            double[] SPSoffArray = new double[1];
            SPSoffArray[0] = (double)SPSoffValue;

            this.chartControl1.Series[0].Points[0].Values = SPSonArray;
            this.chartControl1.Series[0].Points[1].Values = SPSoffArray;
        }

        private string GetCountString(string num)
        {
            string result = string.Empty;

            if (Convert.ToInt32(num) < 10)
                result = "0" + num;
            else
                result = num;

            return result;
        }

        private void InitDoughnutChart()
        {
            this.chartControl1.Series[0].Points.Add(new SeriesPoint("실박스", 0));
            this.chartControl1.Series[0].Points.Add(new SeriesPoint("공박스", 0));
            this.chartControl1.Series[0].Points.Add(new SeriesPoint("매핑 완료", 0));
            this.chartControl1.Titles[0].Text = "SPS 박스 물류 공급 현황";
        }

        private void InitHorizontalChart1()
        {
            this.chartControl2.Series[0].Points.Add(new SeriesPoint("F873S11E\r\n.AKOR", 2));
            this.chartControl2.Series[0].Points.Add(new SeriesPoint("J823MT75V\r\n.AKOR", 4));
            this.chartControl2.Series[0].Points.Add(new SeriesPoint("GR-B24FMQPL\r\n.AMCRGAP", 1));

            this.chartControl2.Series[1].Points.Add(new SeriesPoint("F873S11E\r\n.AKOR", 4));
            this.chartControl2.Series[1].Points.Add(new SeriesPoint("J823MT75V\r\n.AKOR", 6));
            this.chartControl2.Series[1].Points.Add(new SeriesPoint("GR-B24FMQPL\r\n.AMCRGAP", 3));

            XYDiagram diagram = this.chartControl2.Diagram as XYDiagram;
            diagram.Rotated = true;
        }

        private void InitHorizontalChart2()
        {
            this.chartControl3.Series[0].Points.AddRange(new SeriesPoint("1", 1, 13), new SeriesPoint("2", 3, 10),
                new SeriesPoint("3", 4, 6), new SeriesPoint("4", 7, 11), new SeriesPoint("5", 16, 20), new SeriesPoint("6", 17, 21),
                new SeriesPoint("7", 19, 32), new SeriesPoint("8", 22, 32), new SeriesPoint("9", 23, 32), new SeriesPoint("10", 28, 32),
                new SeriesPoint("11", 37, 52), new SeriesPoint("12", 47, 52), new SeriesPoint("13", 48, 52), new SeriesPoint("14", 50, 52),
                new SeriesPoint("15", 51, 89), new SeriesPoint("16", 56, 89), new SeriesPoint("17", 57, 89), new SeriesPoint("18", 77, 89),
                new SeriesPoint("19", 78, 89), new SeriesPoint("20", 79, 89), new SeriesPoint("21", 80, 89), new SeriesPoint("22", 89, 91),
                new SeriesPoint("23", 91, 101), new SeriesPoint("24", 92, 101), new SeriesPoint("25", 93, 101), new SeriesPoint("26", 94, 101));

            this.chartControl3.Series[1].Points.Add(new SeriesPoint("2", 1));
            this.chartControl3.Series[1].Points.Add(new SeriesPoint("5", 2));
            this.chartControl3.Series[1].Points.Add(new SeriesPoint("7", 1));
            this.chartControl3.Series[1].Points.Add(new SeriesPoint("11", 4));
            this.chartControl3.Series[1].Points.Add(new SeriesPoint("23", 2.2));

            LineSeriesView lineView = this.chartControl3.Series[0].View as LineSeriesView;
            lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
        }

        private void ucSPSBufferSummary_Load(object sender, EventArgs e)
        {
            InitChartData();
            InitializeKPIData();
        }
    }
}
