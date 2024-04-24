using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pinokio.Model.Base;

namespace Pinokio.Designer
{
    /// <summary>
    /// Trend KPI Chart에 EqpPlan Count를 기록하기 위한 Class
    /// </summary>
    public class ChartPartStepCountTrendLog
    {
        public DateTime Time { get; set; }
        public int EqpPlanCount { get; set; }
    }
    /// <summary>
    /// Trend KPI Chart에 Sim Total Time를 기록하기 위한 Class
    /// </summary>
    public class ChartPartStepTimeTrendLog
    {
        public DateTime Time { get; set; }
        public double SimTotalTimeAvg { get; set; }
    }

    public partial class GridPartStepTrendLog
    {
        DateTime _time;
        double _simTotalTimeAvg;
        int _partStepCount;

        List<PartStep> _partSteps;
        double stdevTotalTime;
        double avgQueuedTime;
        double avgImportingTime;
        double avgStepWaitingTime;
        double avgProcessingTime;
        double avgExportingTime;

        public DateTime Time
        {
            get { return _time; }
        }
        public int PartStepCount
        {
            get { return _partStepCount; }
        }
        public double SimTotalTimeAvg
        {
            get { return _simTotalTimeAvg; }
        }
        public List<PartStep> PartSteps
        {
            get { return _partSteps; }
        }

        public double StdevTotalTime
        {
            get { return stdevTotalTime; }
        }

        public double AvgQueuedTime
        {
            get { return avgQueuedTime; }
        }

        public double AvgImportingTime
        {
            get { return avgImportingTime; }
        }

        public double AvgStepWaitingTime
        {
            get { return avgStepWaitingTime; }
        }

        public double AvgProcessingTime
        {
            get { return avgProcessingTime; }
        }

        public double AvgExportingTime
        {
            get { return avgExportingTime; }
        }

        private double getStandardDeviation(List<PartStep> eqpPlans)
        {
            double average = eqpPlans.Average(x => x.TotalTime);
            double sumOfDerivation = 0;
            foreach (PartStep eqpPlan in eqpPlans)
            {
                sumOfDerivation += (eqpPlan.TotalTime) * (eqpPlan.TotalTime);
            }
            double sumOfDerivationAverage = sumOfDerivation / eqpPlans.Count;
            return Math.Sqrt(sumOfDerivationAverage - (average * average));
        }
        private double getStandardDeviation(List<MR> commands)
        {
            double average = commands.Average(x => x.TotalTime);
            double sumOfDerivation = 0;
            foreach (MR command in commands)
            {
                sumOfDerivation += (command.TotalTime) * (command.TotalTime);
            }
            double sumOfDerivationAverage = sumOfDerivation / commands.Count;
            return Math.Sqrt(sumOfDerivationAverage - (average * average));
        }
        public GridPartStepTrendLog(DateTime time, List<PartStep> eqpPlans)
        {
            this._time = time;
            GetKPI(eqpPlans);
        }

        /// <summary>
        /// 수집된 Command의 평균 
        /// </summary>
        /// <param name="commands"></param>
        private void GetKPI(List<PartStep> eqpPlans)
        {
            _partStepCount = eqpPlans.Count;
            _partSteps = eqpPlans;
            _simTotalTimeAvg = Math.Round(eqpPlans.Average(x => x.TotalTime) / 60, 3);
            stdevTotalTime = Math.Round(getStandardDeviation(eqpPlans) / 60, 3);
            avgQueuedTime = Math.Round(eqpPlans.Average(x => ((PartStep)x).QueuedTime), 3);
            avgImportingTime = Math.Round(eqpPlans.Average(x => ((PartStep)x).ImportingTime), 3);
            avgStepWaitingTime = Math.Round(eqpPlans.Average(x => ((PartStep)x).StepWaitingTime), 3);
            avgProcessingTime = Math.Round(eqpPlans.Average(x => ((PartStep)x).ProcessingTime), 3);
            avgExportingTime = Math.Round(eqpPlans.Average(x => ((PartStep)x).ExportingTime), 3);
        }
    }

    public partial class GridPartStepTrendLog
    {
        public Dictionary<DateTime, double> dicOperateRate { get; set; }

        public int IntervalMin;
        double eqpPlanOperatingTime;
        double eqpPlanIdleTime;
        int eqpPlanOperatingCount;
        double eqpPlanTotalTime;

        public int TotalEqpCount
        {
            get; set;
        }
        public double EqpPlanOperatingRate
        {
            get { return Math.Round(eqpPlanOperatingTime / eqpPlanTotalTime * 100, 1); }
        }
        public double EqpPlanIdleRate
        {
            get { return Math.Round(100 - EqpPlanOperatingRate, 1); }
        }
        public double EqpPlanOperatingTime
        {
            get { return eqpPlanOperatingTime; }
        }
        public double EqpPlanIdleTime
        {
            get { return eqpPlanIdleTime; }
        }
        public int EqpPlanOperatingCount
        {
            get { return eqpPlanOperatingCount; }
        }
        public void GetEqpPlansOperatingRate(List<PartStep> eqpPlans, DateTime startTime, DateTime endTime)
        {
            eqpPlanOperatingCount = 0;
            eqpPlanOperatingTime = 0;
            foreach (PartStep eqpPlan in eqpPlans)
            {
                if(eqpPlan.TrackInDateTime >= startTime && eqpPlan.TrackOutDateTime <= endTime)
                    eqpPlanOperatingTime += (eqpPlan.TrackOutDateTime - eqpPlan.TrackInDateTime).TotalSeconds;
                else if(eqpPlan.TrackInDateTime < startTime && eqpPlan.TrackOutDateTime <= endTime)
                    eqpPlanOperatingTime += (eqpPlan.TrackOutDateTime - startTime).TotalSeconds;
                else if (eqpPlan.TrackInDateTime >= startTime && eqpPlan.TrackOutDateTime > endTime)
                    eqpPlanOperatingTime += (endTime - eqpPlan.TrackInDateTime).TotalSeconds;
                else if (eqpPlan.TrackInDateTime < startTime && eqpPlan.TrackOutDateTime > endTime)
                    eqpPlanOperatingTime += (endTime - startTime).TotalSeconds;

                eqpPlanOperatingCount++;
            }
            eqpPlanOperatingTime = Math.Round(eqpPlanOperatingTime, 3);
            eqpPlanTotalTime = TotalEqpCount * IntervalMin * 60;
            eqpPlanIdleTime = Math.Round(eqpPlanTotalTime - eqpPlanOperatingTime, 3);
        }
    }
    //public partial class GridEqpPlanTrendLog
    //{
    //    private DateTime _inoutTime;
    //    private int _inputCount;
    //    private int _outputCount;
    //    public DateTime InoutTime { get { return _inoutTime; } set { _inoutTime = value; } }
    //    public int InputCount { get { return _inputCount; } set { _inputCount = value; } }
    //    public int OutputCount { get { return _outputCount; } set { _outputCount = value; } }
    //    public GridEqpPlanTrendLog(DateTime time, int inputCount, int outputCount)
    //    {
    //        this.InoutTime = time;
    //        InputCount = inputCount;
    //        OutputCount = outputCount;
    //    }
    //}
}
