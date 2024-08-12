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
    [Serializable]
    public class CurvedTrainLine : TrainLine, ICurvedLine
    {
        private double _radius;
        private double _startDegree;
        private double _arcDegree;
        private PVector2 _origin;

        [StorableAttribute(true)]
        public double Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                _origin = PVector2.GetOrigin(_radius, _startDegree, _arcDegree);
            }
        }

        [StorableAttribute(true)]
        public double StartDegree
        {
            get { return _startDegree; }
            set
            {
                _startDegree = value;
                _origin = PVector2.GetOrigin(_radius, _startDegree, _arcDegree);
            }
        }

        public PVector2 Origin
        {
            get { return _origin; }
        }

        [StorableAttribute(true)]
        public double ArcDegree
        {
            get { return _arcDegree; }
            set
            {
                _arcDegree = value;
            }
        }
        //---------------------충돌 방지 위한 변수-----------------------

        private List<TrainLineStation> _lstWaitingRailPort;

        public TCS Tcs
        {
            get { return ParentNode as TCS; }
        }

        [StorableAttribute(false)]
        public new static string TransportPointType = typeof(TrainPoint).Name;

        [StorableAttribute(false)]
        private double maximumScheduleDistance;
        private double dispatchDistance = 5779;
        //--------------------------------------------------------------
        [StorableAttribute(true)]
        private string _bay = string.Empty;

        public double MinimumDistance
        { get; set; }

        public List<TrainLineStation> DicWaitingRailPort
        {
            get { return _lstWaitingRailPort; }
        }


        //[Browsable(false)]
        //public string ZcuName
        //{
        //    get
        //    {
        //        if (Zcu != null)
        //            return Zcu.Name;
        //        else
        //            return string.Empty;
        //    }
        //}

        //[Browsable(false)]
        //public TrainZCU Zcu
        //{
        //    get; set;
        //}
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

        public CurvedTrainLine() : base()
        {
            Initialize();
        }

        public void InitializeLineNetworkByAngle()
        {
            foreach (TrainLine conLine in OutLinkNodes.Where(x => x is TrainLine).ToList())
            {
                double angle = 0;
                if (OutLinkNodeConnections[conLine] == EndPoint && conLine.EndPoint == EndPoint)
                    angle = Geometry.PVector3.AngleDegree(new PVector3(-Direction.X, -Direction.Y, 0), conLine.Direction, Geometry.PVector3.Coordinate.Z);
                else if(OutLinkNodeConnections[conLine] == EndPoint && conLine.StartPoint == EndPoint)
                    angle = Geometry.PVector3.AngleDegree(Direction, conLine.Direction, Geometry.PVector3.Coordinate.Z);
                else if (OutLinkNodeConnections[conLine] == StartPoint && conLine.EndPoint == StartPoint)
                    angle = Geometry.PVector3.AngleDegree(Direction, conLine.Direction, Geometry.PVector3.Coordinate.Z);
                else if (OutLinkNodeConnections[conLine] == StartPoint && conLine.StartPoint == StartPoint)
                    angle = Geometry.PVector3.AngleDegree(new PVector3(-Direction.X, -Direction.Y, 0), conLine.Direction, Geometry.PVector3.Coordinate.Z);

                if (angle > 90 && angle < 270)
                {
                    OutLinkNodes.Remove(conLine);
                    OutLinkNodeConnections.Remove(conLine);
                    conLine.InLinkNodes.Remove(this);
                    conLine.InLinkNodeConnections.Remove(this);

                    InLinkNodes.Remove(conLine);
                    InLinkNodeConnections.Remove(conLine);
                    conLine.OutLinkNodes.Remove(this);
                    conLine.OutLinkNodeConnections.Remove(this);
                }
                else
                {
                    if (!InLinkNodes.Contains(conLine))
                        InLinkNodes.Add(conLine);
                    if(!conLine.OutLinkNodes.Contains(this))
                        conLine.OutLinkNodes.Add(this);
                    if (!InLinkNodeConnections.ContainsKey(conLine))
                        InLinkNodeConnections.Add(conLine, OutLinkNodeConnections[conLine]);
                    if(!conLine.OutLinkNodeConnections.ContainsKey(this))
                        conLine.OutLinkNodeConnections.Add(this, OutLinkNodeConnections[conLine]);
                }
            }

            foreach (TrainLine conLine in InLinkNodes.Where(x => x is TrainLine).ToList())
            {
                double angle = 0;
                if (InLinkNodeConnections[conLine] == EndPoint && conLine.EndPoint == EndPoint)
                    angle = Geometry.PVector3.AngleDegree(new PVector3(-Direction.X, -Direction.Y, 0), conLine.Direction, Geometry.PVector3.Coordinate.Z);
                else if (InLinkNodeConnections[conLine] == EndPoint && conLine.StartPoint == EndPoint)
                    angle = Geometry.PVector3.AngleDegree(Direction, conLine.Direction, Geometry.PVector3.Coordinate.Z);
                else if (InLinkNodeConnections[conLine] == StartPoint && conLine.EndPoint == StartPoint)
                    angle = Geometry.PVector3.AngleDegree(Direction, conLine.Direction, Geometry.PVector3.Coordinate.Z);
                else if (InLinkNodeConnections[conLine] == StartPoint && conLine.StartPoint == StartPoint)
                    angle = Geometry.PVector3.AngleDegree(new PVector3(-Direction.X, -Direction.Y, 0), conLine.Direction, Geometry.PVector3.Coordinate.Z);

                if (angle > 90 && angle < 270)
                {
                    OutLinkNodes.Remove(conLine);
                    OutLinkNodeConnections.Remove(conLine);
                    conLine.InLinkNodes.Remove(this);
                    conLine.InLinkNodeConnections.Remove(this);

                    InLinkNodes.Remove(conLine);
                    InLinkNodeConnections.Remove(conLine);
                    conLine.OutLinkNodes.Remove(this);
                    conLine.OutLinkNodeConnections.Remove(this);
                }
                else
                {
                    if (!OutLinkNodes.Contains(conLine))
                        OutLinkNodes.Add(conLine);
                    if (!conLine.InLinkNodes.Contains(this))
                        conLine.InLinkNodes.Add(this);
                    if (!OutLinkNodeConnections.ContainsKey(conLine))
                        OutLinkNodeConnections.Add(conLine, InLinkNodeConnections[conLine]);
                    if (!conLine.InLinkNodeConnections.ContainsKey(this))
                        conLine.InLinkNodeConnections.Add(this, InLinkNodeConnections[conLine]);
                }
            }
        }

        public CurvedTrainLine(uint id, string name) : base(id, name)
        {
            Initialize();
        }

        public CurvedTrainLine(string name, TransportPoint start, TransportPoint end, int width, int height)
            : base(name, start, end, width, height)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();

            maximumScheduleDistance = dispatchDistance - 4252;
            MinimumDistance = 300;
            _lstWaitingRailPort = new List<TrainLineStation>();
            IsTwoWay = true;
            _bay = string.Empty;
            IsCurve = true;
        }

        public override void InitializeNode(EventCalendar evtCal)
        {
            base.InitializeNode(evtCal);
            InitializeLineNetworkByAngle();
        }

        public override bool IsEnter(SimPort port)
        {
            if (port.Object is Train)
            {
                if (((TrainPoint)StartPoint).CheckEnter(port.Time, port.Object as Train))
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

        public double GetToEndPointZCUReservationPos(TrainLine toLine, double deceleration)
        {
            double toLineStartVelocityForStopPoint = Physics.GetVelocity0_v_a_s(0, deceleration, toLine.Length);
            double toLineMaxStartVelocity = toLine.MaxSpeed > toLineStartVelocityForStopPoint ? toLineStartVelocityForStopPoint : toLine.MaxSpeed;
            double maxVelocity = toLineMaxStartVelocity > MaxSpeed ? MaxSpeed : toLineMaxStartVelocity;
            return Length - Physics.GetLength_v_v0_a(maxVelocity, MaxSpeed, deceleration);
        }

        public double GetToToEndPointZCUReservationPos(TrainLine toLine, TrainLine totoLine, double deceleration)
        {
            double totoLineStartVelocityForStopPoint = Physics.GetVelocity0_v_a_s(0, deceleration, totoLine.Length);
            double totoLineMaxStartVelocity = totoLine.MaxSpeed > totoLineStartVelocityForStopPoint ? totoLineStartVelocityForStopPoint : totoLine.MaxSpeed;
            double toLineStartVelocityForStopPoint = Physics.GetVelocity0_v_a_s(totoLineMaxStartVelocity, deceleration, toLine.Length);
            double toLineMaxStartVelocity = toLine.MaxSpeed > toLineStartVelocityForStopPoint ? toLineStartVelocityForStopPoint : toLine.MaxSpeed;
            double maxVelocity = toLineMaxStartVelocity > MaxSpeed ? MaxSpeed : toLineMaxStartVelocity;
            return Length - Physics.GetLength_v_v0_a(maxVelocity, MaxSpeed, deceleration);
        }

        public override PVector3 GetObjectPosition(SimObj entity, Time simTime)
        {
            double distance = GetDistanceAtTime(entity, simTime);

            return GetPositionByDistance(distance);
        }

        public override PVector3 GetPositionByDistance(double distance)
        {
            return GetPositionByDistance(StartPoint.PosVec3, distance);
        }

        public PVector3 GetPositionByDistance(PVector3 startPoint, double distance)
        {
            double angle;
            if (_arcDegree > 0)
                angle = distance / (2 * Math.PI * _radius) * 360 - 90;
            else
                angle = -distance / (2 * Math.PI * _radius) * 360 + 90;

            PVector3 pos = startPoint + new PVector3(_origin.X, _origin.Y, 0) + PVector3.DegreeToDirection(_startDegree + angle) * _radius;

            return pos;
        }

        public override double GetObjectDegree(SimObj entity, Time simTime)
        {
            double angle = GetObjectRadian(entity, simTime) * 180 / Math.PI;
            return angle;
        }

        public override double GetObjectRadian(SimObj entity, Time simTime)
        {
            double distance = GetDistanceAtTime(entity, simTime);
            double radian = GetObjectRadianAtDistance(distance);
            return radian;
        }

        public double GetObjectRadianAtDistance(double distance)
        {
            double radian;
            if (_arcDegree > 0)
                radian = (_startDegree + distance / (2 * Math.PI * _radius) * 360) / 180 * Math.PI;
            else
                radian = (_startDegree - (distance / (2 * Math.PI * _radius) * 360)) / 180 * Math.PI;

            return radian;
        }

        public double GetLengthByRadiusNArcDegree(double radius, double arcDegree)
        {
            return radius * 2 * Math.PI * arcDegree / 360;
        }
    }
}
