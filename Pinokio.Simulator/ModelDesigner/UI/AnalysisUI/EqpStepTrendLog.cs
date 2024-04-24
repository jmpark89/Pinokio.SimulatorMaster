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
    public class ChartEqpStepCountTrendLog
    {
        public DateTime Time { get; set; }
        public int EqpPlanCount { get; set; }
    }
    /// <summary>
    /// Trend KPI Chart에 Sim Total Time를 기록하기 위한 Class
    /// </summary>
    public class ChartEqpStepTimeTrendLog
    {
        public DateTime Time { get; set; }
        public double SimTotalTimeAvg { get; set; }
    }

    public partial class GridEqpStepTrendLog
    {
        DateTime _time;
        double _simTotalTimeAvg;
        int _eqpStepCount;

        List<EqpStep> _eqpSteps;
        double stdevTotalTime;
        double avgQueuedTime;
        double avgImportingTime;
        double avgProcessingTime;

        public DateTime Time
        {
            get { return _time; }
        }
        public int PartStepCount
        {
            get { return _eqpStepCount; }
        }
        public double SimTotalTimeAvg
        {
            get { return _simTotalTimeAvg; }
        }
        public List<EqpStep> EqpSteps
        {
            get { return _eqpSteps; }
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


        public double AvgProcessingTime
        {
            get { return avgProcessingTime; }
        }

        private double getStandardDeviation(List<EqpStep> eqpPlans)
        {
            double average = eqpPlans.Average(x => x.TotalTime);
            double sumOfDerivation = 0;
            foreach (EqpStep eqpPlan in eqpPlans)
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
        public GridEqpStepTrendLog(DateTime time, List<EqpStep> eqpPlans)
        {
            this._time = time;
            GetKPI(eqpPlans);
        }

        /// <summary>
        /// 수집된 EqpStep의 평균 
        /// </summary>
        /// <param name="commands"></param>
        private void GetKPI(List<EqpStep> eqpPlans)
        {
            _eqpStepCount = eqpPlans.Count;
            _eqpSteps = eqpPlans;
            _simTotalTimeAvg = Math.Round(eqpPlans.Average(x => x.TotalTime) / 60, 3);
            stdevTotalTime = Math.Round(getStandardDeviation(eqpPlans) / 60, 3);
            avgQueuedTime = Math.Round(eqpPlans.Average(x => ((EqpStep)x).DispatchingTime), 3);
            avgImportingTime = Math.Round(eqpPlans.Average(x => ((EqpStep)x).WaitingTime), 3);
            avgProcessingTime = Math.Round(eqpPlans.Average(x => ((EqpStep)x).ProcessingTime), 3);
        }
    }

    public partial class GridEqpStepTrendLog
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
        public void GetEqpPlansOperatingRate(List<EqpStep> eqpPlans, DateTime startTime, DateTime endTime)
        {
            eqpPlanOperatingCount = 0;
            eqpPlanOperatingTime = 0;
            foreach (EqpStep eqpPlan in eqpPlans)
            {
                if(eqpPlan.StepStartDateTime >= startTime && eqpPlan.StepEndDateTime <= endTime)
                    eqpPlanOperatingTime += (eqpPlan.StepEndDateTime - eqpPlan.StepStartDateTime).TotalSeconds;
                else if(eqpPlan.StepStartDateTime < startTime && eqpPlan.StepEndDateTime <= endTime)
                    eqpPlanOperatingTime += (eqpPlan.StepEndDateTime - startTime).TotalSeconds;
                else if (eqpPlan.StepStartDateTime >= startTime && eqpPlan.StepEndDateTime > endTime)
                    eqpPlanOperatingTime += (endTime - eqpPlan.StepStartDateTime).TotalSeconds;
                else if (eqpPlan.StepStartDateTime < startTime && eqpPlan.StepEndDateTime > endTime)
                    eqpPlanOperatingTime += (endTime - startTime).TotalSeconds;

                eqpPlanOperatingCount++;
            }
            eqpPlanOperatingTime = Math.Round(eqpPlanOperatingTime, 3);
            eqpPlanTotalTime = TotalEqpCount * IntervalMin * 60;
            eqpPlanIdleTime = Math.Round(eqpPlanTotalTime - eqpPlanOperatingTime, 3);
        }
    }
}
