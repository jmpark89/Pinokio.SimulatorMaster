using Logger;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Pinokio.Model.Base;

namespace Pinokio.Model.User
{
    [Serializable]
    public class TrainPoint : TransportPoint
    {
        private TransportLine _inLine; // in Rail
        private List<SimPort> _lstStopEvent; //Train의 대기 예약 리스트

        public TCS Tcs
        {
            get { return ParentNode as TCS; }
        }

        [Browsable(true)]
        public List<SimPort> StopPorts
        { get { return _lstStopEvent; } }

        public TrainPoint()
            : base()
        {
            Initialize();
        }

        private void Initialize()
        {
            _lstStopEvent = new List<SimPort>();
        }

        public override void InitializeNode(EventCalendar evtCal)
        {
            base.InitializeNode(evtCal);
        }
        /// <summary>
        /// 지나갈 수 있는지 확인하는 함수
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="train"></param>
        /// <returns></returns>
        public bool CheckEnter(Time simTime, Train train)
        {
            return true;
        }
    }
}
