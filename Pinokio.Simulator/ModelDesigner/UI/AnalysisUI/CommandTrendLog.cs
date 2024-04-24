using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pinokio.Model.Base;

namespace Pinokio.Designer
{
    /// <summary>
    /// Trend KPI Chart에 Command Count를 기록하기 위한 Class
    /// </summary>
    public class ChartCommandCountTrendLog
    {
        public DateTime Time { get; set; }
        public int CommandCount { get; set; }
    }
    /// <summary>
    /// Trend KPI Chart에 Sim Total Time를 기록하기 위한 Class
    /// </summary>
    public class ChartCommandTimeTrendLog
    {
        public DateTime Time { get; set; }
        public double SimTotalTimeAvg { get; set; }
    }

    public partial class GridCommandTrendLog
    {
        DateTime _time;
        double _simTotalTimeAvg;
        int _commandCount;
        int _mrCount;

        List<Command> _commands;
        List<MR> _mrs;
        double stdevTotalTime;
        double avgWaitingSpeed;
        double avgTransferSpeed;
        double avgQueuedTime;
        double avgWaitingTime;
        double avgAcquiringTime;
        double avgTransferingTime;
        double avgDepositTime;
        double avgMRSubCSCount;

        public DateTime Time
        {
            get { return _time; }
        }
        public int CommandCount
        {
            get { return _commandCount; }
        }
        public double SimTotalTimeAvg
        {
            get { return _simTotalTimeAvg; }
        }
        public List<Command> Commands
        {
            get { return _commands; }
        }
        public List<MR> MRs
        {
            get { return _mrs; }
        }
        public double StdevTotalTime
        {
            get { return stdevTotalTime; }
        }

        public double AvgWaitingSpeed
        {
            get { return avgWaitingSpeed; }
        }

        public double AvgTransferSpeed
        {
            get { return avgTransferSpeed; }
        }

        public double AvgQueuedTime
        {
            get { return avgQueuedTime; }
        }

        public double AvgWaitingTime
        {
            get { return avgWaitingTime; }
        }

        public double AvgAcquiringTime
        {
            get { return avgAcquiringTime; }
        }

        public double AvgTransferingTime
        {
            get { return avgTransferingTime; }
        }

        public double AvgDepositTime
        {
            get { return avgDepositTime; }
        }
        public double AvgMRSubCSCount
        { 
            get { return avgMRSubCSCount; } 
        }
        public int MRCount
        {
            get { return _mrCount; }
        }
        public GridCommandTrendLog(DateTime time, List<MR> commands)
        {
            this._time = time;
            GetKPI(commands);
        }
        private void GetKPI(List<MR> commands)
        {
            _mrCount = commands.Count;
            this._mrs = commands;
            _simTotalTimeAvg = Math.Round(commands.Average(x => x.TotalTime) / 60, 3);
            stdevTotalTime = Math.Round(getStandardDeviation(commands) / 60, 3);
            avgMRSubCSCount = Math.Round(commands.Average(x => x.CommandCount), 1);
        }
        private double getStandardDeviation(List<MR> mrs)
        {
            double average = mrs.Average(x => x.TotalTime);
            double sumOfDerivation = 0;
            foreach (MR mr in mrs)
            {
                sumOfDerivation += (mr.TotalTime) * (mr.TotalTime);
            }
            double sumOfDerivationAverage = sumOfDerivation / mrs.Count;
            return Math.Sqrt(sumOfDerivationAverage - (average * average));
        }
        public GridCommandTrendLog(DateTime time, List<Command> commands)
        {
            this._time = time;
            GetKPI(commands);
        }
        private void GetKPI(List<Command> commands)
        {
            _commandCount = commands.Count;
            this._commands = commands;
            _simTotalTimeAvg = Math.Round(commands.Average(x => x.TotalTime) / 60, 3);
            stdevTotalTime = Math.Round(getStandardDeviation(commands) / 60, 3);
            avgWaitingSpeed = Math.Round(commands.Sum(x => x.WaitingDistance) / commands.Sum(x => x.WaitingTime) / 1000 * 60, 3);
            avgTransferSpeed = Math.Round(commands.Sum(x => x.TransferringDistance) / commands.Sum(x => x.TransferringTime) / 1000 * 60, 3);
            avgQueuedTime = Math.Round(commands.Average(x => x.QueuedTime), 3);
            avgWaitingTime = Math.Round(commands.Average(x => x.WaitingTime), 3);
            avgAcquiringTime = Math.Round(commands.Average(x => x.LoadingTime), 3);
            avgTransferingTime = Math.Round(commands.Average(x => x.TransferringTime), 3);
            avgDepositTime = Math.Round(commands.Average(x => x.UnloadingTime), 3);
        }
        private double getStandardDeviation(List<Command> commands)
        {
            double average = commands.Average(x => x.TotalTime);
            double sumOfDerivation = 0;
            foreach (Command command in commands)
            {
                sumOfDerivation += (command.TotalTime) * (command.TotalTime);
            }
            double sumOfDerivationAverage = sumOfDerivation / commands.Count;
            return Math.Sqrt(sumOfDerivationAverage - (average * average));
        }
    }

    public partial class GridCommandTrendLog
    {
        public Dictionary<int, int> dicSigma { get; set; }

        public double GetNSigmaTime(int n)
        {
            return Math.Round(_simTotalTimeAvg * 60 + this.getStandardDeviation(_commands) * n, 3);
        }
    }


    public partial class GridCommandTrendLog
    {
        public Dictionary<DateTime, double> dicOperateRate { get; set; }

        public int IntervalMin;
        double vehicleOperatingTime;
        double vehicleIdleTime;
        int vehicleOperatingCount;
        double vehicleTotalTime;

        public int VehicleTotalCount 
        { 
            get; set; 
        }
        public double VehicleOperatingRate
        {
            get { return Math.Round(Math.Round(Convert.ToDouble(vehicleOperatingTime / vehicleTotalTime) , 3) * 100, 1)  ; }
        }
        public double VehicleIdleRate
        {
            get { return 100 - VehicleOperatingRate; }
        }
        public double VehicleOperatingTime
        {
            get { return vehicleOperatingTime; }
        }
        public double VehicleIdleTime
        {
            get { return vehicleIdleTime; }
        }
        public int VehicleOperatingCount
        {
            get { return vehicleOperatingCount; }
        }
        public void GetVehiclesOperatingRate(List<Command> commands, DateTime startTime, DateTime endTime)
        {
            vehicleOperatingCount = 0;
            vehicleOperatingTime = 0;
            foreach (Command command in commands)
            {
                if (command.Vehicle == null)
                    continue;
                
                if(command.AssignedDateTime >= startTime && command.CompletedDateTime <= endTime)
                    vehicleOperatingTime += (command.CompletedDateTime - command.AssignedDateTime).TotalSeconds;
                else if (command.AssignedDateTime < startTime && command.CompletedDateTime <= endTime)
                    vehicleOperatingTime += (command.AssignedDateTime - startTime).TotalSeconds;
                else if (command.AssignedDateTime >= startTime && command.CompletedDateTime > endTime)
                    vehicleOperatingTime += (endTime - command.AssignedDateTime).TotalSeconds;
                else if (command.AssignedDateTime < startTime && command.CompletedDateTime > endTime)
                    vehicleOperatingTime += (endTime - startTime).TotalSeconds;

                vehicleOperatingCount++;
            }
            vehicleOperatingTime = Math.Round(vehicleOperatingTime, 3);
            vehicleTotalTime = VehicleTotalCount * IntervalMin * 60;
            vehicleIdleTime = Math.Round(vehicleTotalTime - vehicleOperatingTime, 3);
        }
    }
}
