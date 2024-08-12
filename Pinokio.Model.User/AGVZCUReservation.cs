using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pinokio.Model.Base;

namespace Pinokio.Model.User
{
    [Serializable]
    public class AGVZCUReservation
    {
        private Time _reservationTime;
        private AGV _agv;
        private AGVPoint _stopPoint;
        private AGVPoint _resetPoint;

        public Time ReservationTime
        {
            get { return _reservationTime; }
            set { _reservationTime = value; }
        }

        public AGV AGV
        {
            get { return _agv; }
        }


        public AGVPoint StopPoint
        {
            get { return _stopPoint; }
            set { _stopPoint = value; }
        }

        public AGVPoint ResetPoint
        {
            get { return _resetPoint; }
            set { _resetPoint = value; }
        }


        public AGVZCUReservation(Time reservationTime, AGV agv, AGVPoint stopPoint, AGVPoint resetPoint)
        {
            _reservationTime = reservationTime;
            _agv = agv;
            _stopPoint = stopPoint;
            _resetPoint = resetPoint;
        }
    }
}
