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
    public enum LINE_WAY
    {
        FORWARD, TWO_WAY, REVERSE
    }

    [Serializable]
    public class AGVLine : GuidedLine
    {
        //---------------------충돌 방지 위한 변수-----------------------

        private List<AGVLineStation> _lstWaitingRailPort;

        public ACS Acs
        {
            get { return ParentNode as ACS; }
        }

        [StorableAttribute(false)]
        public new static string TransportPointType = typeof(AGVPoint).Name;

        [StorableAttribute(false)]
        private double maximumScheduleDistance;
        private double dispatchDistance = 5779;
        //--------------------------------------------------------------

        public double MinimumDistance
        { get; set; }

        [Browsable(false), StorableAttribute(true)]
        private LINE_WAY _way = LINE_WAY.FORWARD;

        [Browsable(true), StorableAttribute(false)]
        [CategoryAttribute("3. Link Info"), DisplayName("5. Way")]
        public LINE_WAY Way
        {
            get { return _way; }
            set
            {
                _way = value;
                InitializeAfterLoad();
            }
        }

        public List<AGVLineStation> DicWaitingRailPort
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
        public AGVZCU Zcu
        {
            get; set;
        }

        public double MaximumScheduleDistance
        {
            get { return maximumScheduleDistance; }
            set { maximumScheduleDistance = value; }
        }

        public AGVLine() : base()
        {
            Initialize();
        }

        public AGVLine(uint id, string name) : base(id, name)
        {
            Initialize();
        }

        public AGVLine(string name, TransportPoint start, TransportPoint end, int width, int height)
            : base(name, start, end, width, height)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();

            maximumScheduleDistance = dispatchDistance - 4252;
            MinimumDistance = 300;
            _lstWaitingRailPort = new List<AGVLineStation>();
            IsCurve = false;
        }

        public override void InitializeAfterLoad()
        {
            try
            {
                Dictionary<TXNode, Station> outNodes = OutLinkNodeConnections.Where(x => x.Value.ID == EndPoint.ID).ToDictionary(pair => pair.Key, pair => pair.Value);

                foreach (TXNode outNode in outNodes.Keys)
                {
                    if (_way is LINE_WAY.FORWARD)
                    {
                        this.ConnectNode(outNode);
                        outNode.UnconnectNode(this);
                    }
                    else if (_way is LINE_WAY.REVERSE)
                    {
                        this.UnconnectNode(outNode);
                        outNode.ConnectNode(this);
                    }
                    else
                    {
                        this.ConnectNode(outNode);
                        outNode.ConnectNode(this);
                    }
                }

                Dictionary<TXNode, Station> inNodes = InLinkNodeConnections.Where(x => x.Value.ID == StartPoint.ID).ToDictionary(pair => pair.Key, pair => pair.Value);
                foreach (TXNode inNode in inNodes.Keys)
                {
                    if (_way is LINE_WAY.FORWARD)
                    {
                        inNode.ConnectNode(this);
                        this.UnconnectNode(inNode);
                    }
                    else if (_way is LINE_WAY.REVERSE)
                    {
                        inNode.UnconnectNode(this);
                        this.ConnectNode(inNode);
                    }
                    else
                    {
                        this.ConnectNode(inNode);
                        inNode.ConnectNode(this);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public override double GetCost()
        {
            return MinPassingTime;
        }

        public override bool IsEnter(SimPort port)
        {
            if (port.Object is AGV)
            {
                if (((AGVPoint)StartPoint).CheckEnter(port.Time, port.Object as AGV))
                    return true;
                else
                    return false;
            }
            else if (port.Object is Part)
                return false;
            else
                return false;
        }

        public double GetEndPointZCUReservationPos(double deceleration)
        {
            double v = 0;
            double v0 = MaxSpeed;
            double reservationPosition = Length - Physics.GetLength_v_v0_a(v, v0, deceleration);

            return reservationPosition;
        }

        public double GetToEndPointZCUReservationPos(AGVLine toLine, double deceleration)
        {
            double toLineStartVelocityForStopPoint = Physics.GetVelocity0_v_a_s(0, deceleration, toLine.Length);
            double toLineMaxStartVelocity = toLine.MaxSpeed > toLineStartVelocityForStopPoint ? toLineStartVelocityForStopPoint : toLine.MaxSpeed;
            double maxVelocity = toLineMaxStartVelocity > MaxSpeed ? MaxSpeed : toLineMaxStartVelocity;
            return Length - Physics.GetLength_v_v0_a(maxVelocity, MaxSpeed, deceleration);
        }

        public double GetToToEndPointZCUReservationPos(AGVLine toLine, AGVLine totoLine, double deceleration)
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
