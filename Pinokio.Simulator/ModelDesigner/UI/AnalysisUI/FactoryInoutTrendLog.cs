using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pinokio.Model.Base;

namespace Pinokio.Designer
{
    /// <summary>
    /// FactoryInout Chart에 Input Count를 기록하기 위한 Class
    /// </summary>
    public class ChartFactoryInLog
    {
        public DateTime Time { get; set; }
        public int InputCount { get; set; }
    }
    /// <summary>
    /// FactoryInout Chart에 Output Count를 기록하기 위한 Class
    /// </summary>
    public class ChartFactoryOutLog
    {
        public DateTime Time { get; set; }
        public int OutputCount { get; set; }
    }
    public class GridFactoryInoutLog
    {
        private DateTime _inoutTime;
        private int _inputCount;
        private int _outputCount;
        private double _inoutTotalTimeAvg;
        private double _inoutstdevTotalTime;
        public DateTime InoutTime { get { return _inoutTime; } set { _inoutTime = value; } }
        public int InputCount { get { return _inputCount; } set { _inputCount = value; } }
        public int OutputCount { get { return _outputCount; } set { _outputCount = value; } }
        public double InOutTotalTimeAvg { get { return _inoutTotalTimeAvg; } set { _inoutTotalTimeAvg = value; } }
        public double InOutStdevTotalTime { get { return _inoutstdevTotalTime; } set { _inoutstdevTotalTime = value; } }
        public GridFactoryInoutLog(DateTime time, int inputCount, int outputCount, double inoutTotalTimeAvg, double inoutstdevTotalTime)
        {
            this.InoutTime = time;
            InputCount = inputCount;
            OutputCount = outputCount;
            InOutTotalTimeAvg = inoutTotalTimeAvg;
            InOutStdevTotalTime = inoutstdevTotalTime;
        }
    }
}


