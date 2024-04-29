using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Logger;
using Pinokio.Geometry;
using Pinokio.Model.Base;
using Pinokio.Object;
using Simulation.Engine;

namespace Pinokio.Model.User
{
    public class OHTLine : GuidedLine
    {
        //---------------------충돌 방지 위한 변수-----------------------

        private List<OHTLineStation> _lstWaitingRailPort;

        public OCS Ocs
        {
            get { return ParentNode as OCS; }
        }

        [StorableAttribute(false)]
        public new static string TransportPointType = typeof(OHTPoint).Name;

        [StorableAttribute(false)]
        private double maximumScheduleDistance;
        private double dispatchDistance = 5779;
        //--------------------------------------------------------------
        [StorableAttribute(true)]
        private string _bay = string.Empty;

        public bool IsCurve
        { get; set; }

        public double MinimumDistance
        { get; set; }

        public List<OHTLineStation> DicWaitingRailPort
        {
            get { return _lstWaitingRailPort; }
        }


        [Browsable(false)]
        public string ZcuName
        {
            get
            {
                if (Zcu != null)
                    return Zcu.Name;
                else
                    return string.Empty;
            }
        }

        [Browsable(false)]
        public OHTZCU Zcu
        {
            get; set;
        }
        [StorableAttribute(true)]
        public string Bay
        {
            get { return _bay; }
            set { _bay = value; }
        }
        public double MaximumScheduleDistance
        {
            get { return maximumScheduleDistance; }
            set { maximumScheduleDistance = value; }
        }

        public OHTLine() : base()
        {
            Initialize();
        }

        public OHTLine(uint id, string name) : base(id, name)
        {
            Initialize();
        }

        public OHTLine(string name, TransportPoint start, TransportPoint end, int width, int height)
            : base(name, start, end, width, height)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();

            maximumScheduleDistance = dispatchDistance - 4252;
            MinimumDistance = 300;
            _lstWaitingRailPort = new List<OHTLineStation>();
            IsCurve = false;
            _bay = string.Empty;
        }

        public override bool IsEnter(SimPort port)
        {
            if (Name == "OHTLine_61" && SimEngine.Instance.TimeNow > 550)
                ;
            if (port.Object is OHT)
            {
                if (((OHTPoint)StartPoint).CheckEnter(port.Time, port.Object as OHT))
                    return true;
                else
                    return false;
            }
            else if (port.Object is Part)
                return false;
            else
                return false;
        }

        public override double GetCost()
        {
            return MinPassingTime;
        }

        public double GetEndPointZCUReservationPos(double deceleration)
        {
            double v = 0;
            double v0 = MaxSpeed;
            double reservationPosition = Length;
            if(deceleration > 0)
                reservationPosition -= Physics.GetLength_v_v0_a(v, v0, deceleration);

            return reservationPosition;
        }

        public double GetToEndPointZCUReservationPos(OHTLine toLine, double deceleration)
        {
            double toLineStartVelocityForStopPoint = Physics.GetVelocity0_v_a_s(0, deceleration, toLine.Length);
            double toLineMaxStartVelocity = toLine.MaxSpeed > toLineStartVelocityForStopPoint ? toLineStartVelocityForStopPoint : toLine.MaxSpeed;
            double maxVelocity = toLineMaxStartVelocity > MaxSpeed ? MaxSpeed : toLineMaxStartVelocity;
            return Length - Physics.GetLength_v_v0_a(maxVelocity, MaxSpeed, deceleration);
        }

        public double GetToToEndPointZCUReservationPos(OHTLine toLine, OHTLine totoLine, double deceleration)
        {
            double totoLineStartVelocityForStopPoint = Physics.GetVelocity0_v_a_s(0, deceleration, totoLine.Length);
            double totoLineMaxStartVelocity = totoLine.MaxSpeed > totoLineStartVelocityForStopPoint ? totoLineStartVelocityForStopPoint : totoLine.MaxSpeed;
            double toLineStartVelocityForStopPoint = Physics.GetVelocity0_v_a_s(totoLineMaxStartVelocity, deceleration, toLine.Length);
            double toLineMaxStartVelocity = toLine.MaxSpeed > toLineStartVelocityForStopPoint ? toLineStartVelocityForStopPoint : toLine.MaxSpeed;
            double maxVelocity = toLineMaxStartVelocity > MaxSpeed ? MaxSpeed : toLineMaxStartVelocity;
            return Length - Physics.GetLength_v_v0_a(maxVelocity, MaxSpeed, deceleration);
        }
    }
}
