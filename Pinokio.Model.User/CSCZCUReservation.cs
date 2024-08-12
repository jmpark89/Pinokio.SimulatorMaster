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
    public class CSCZCUReservation
    {
        private Time _reservationTime;
        private CSC _csc;
        private CSCPoint _stopPoint;
        private CSCPoint _resetPoint;

        public Time ReservationTime
        {
            get { return _reservationTime; }
            set { _reservationTime = value; }
        }

        public CSC CSC
        {
            get { return _csc; }
        }


        public CSCPoint StopPoint
        {
            get { return _stopPoint; }
            set { _stopPoint = value; }
        }

        public CSCPoint ResetPoint
        {
            get { return _resetPoint; }
            set { _resetPoint = value; }
        }


        public CSCZCUReservation(Time reservationTime, CSC csc, CSCPoint stopPoint, CSCPoint resetPoint)
        {
            _reservationTime = reservationTime;
            _csc = csc;
            _stopPoint = stopPoint;
            _resetPoint = resetPoint;
        }
    }
}
