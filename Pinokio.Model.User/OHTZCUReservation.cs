using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pinokio.Model.Base;

namespace Pinokio.Model.User
{
    public class OHTZCUReservation
    {
        private Time _reservationTime;
        private OHT _oht;
        private OHTPoint _stopPoint;
        private OHTPoint _resetPoint;

        public Time ReservationTime
        {
            get { return _reservationTime; }
            set { _reservationTime = value; }
        }

        public OHT OHT
        {
            get { return _oht; }
        }


        public OHTPoint StopPoint
        {
            get { return _stopPoint; }
            set { _stopPoint = value; }
        }

        public OHTPoint ResetPoint
        {
            get { return _resetPoint; }
            set { _resetPoint = value; }
        }


        public OHTZCUReservation(Time reservationTime, OHT oht, OHTPoint stopPoint, OHTPoint resetPoint)
        {
            _reservationTime = reservationTime;
            _oht = oht;
            _stopPoint = stopPoint;
            _resetPoint = resetPoint;
        }
    }
}
