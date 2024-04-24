using Pinokio.Geometry;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;

namespace Pinokio.Model.User
{
    public class Elevator : Vehicle
    {
        
        #region [public Member]
        public LCS Lcs
        {
            get { return ParentNode as LCS; }
        }
        public List<PosData> LstHeightPD { get => _lstHeightPD; set => _lstHeightPD = value; }
        public List<PosData> LstAnglePD { get => _lstAnglePD; set => _lstAnglePD = value; }
        public PVector3 ElevatorEndPoint { get => _elevatorPoint; set => _elevatorPoint = value; }
        public double AccSpeed { get => _accSpeed; set => _accSpeed = value; }
        public double StartHeight
        { get; set; }

        public double StartAngle
        { get; set; }

        public double facilityDirection = 0;
        #endregion
        #region [Protected Member]
        [Storable(false)]
        protected double fromRadian = 0; //start radian
        [Storable(false)]
        protected double toRadian = 0;
        [Storable(false)]
        protected ElevatorLine _mainLine;
        protected TXNode fromNode = null;
        protected TXNode toNode = null;
        #endregion

        #region [private Member]
        private Time _delay;
        private int _heightSpeed;
        private double _accSpeed;
        private double _angleSpeed;
        private List<PosData> _lstHeightPD;
        private List<PosData> _lstAnglePD;
        private PVector3 _elevatorPoint;
        #endregion

        #region [Constructor]
        public Elevator()
            : base()
        {
            Initialize();
        }

        public Elevator(string name, int speed, double distance, ElevatorLine line, int width = 1500, int depth = 7100, int height = 1000) : base(name, distance, line, width, depth, height)
        {
            Initialize();
        }
        #endregion

        #region [Initialization]
        private void Initialize()
        {
            _delay = 2;
            LoadTime = 5;
            UnloadTime = 5;
            _heightSpeed = 1500;
            _accSpeed = 500;
            _angleSpeed = 30;
            Deceleration = -2940;
            Capa = 1;
            _lstHeightPD = new List<PosData>();
            _lstAnglePD = new List<PosData>();
        }
        #endregion

        #region [Vehicle Event Trigger]
        protected override void EndLoading(Time simTime, SimPort port)
        {

            State = VEHICLE_STATE.MOVE_TO_UNLOAD;
            if (ParentNode is VSubCS)
                ((VSubCS)ParentNode).SetTransferringCommand(simTime, Command);

            if (PosDatas.Count > 0)
                StartPos = PosDatas.Last()._endPos;
            if (LstHeightPD.Count > 0)
                Height = LstHeightPD.Last()._endPos;
            if (LstAnglePD.Count > 0)
                SetRotate(LstAnglePD.Last()._endPos, PVector3.UnitZ);

            SimPort newPort = new SimPort(INT_PORT.MOVE, this);
            InternalFunction(simTime, newPort);
        }
        protected override void EndUnloading(Time simTime, SimPort port)
        {
            Lcs.SetCompletedCommand(simTime, Command);
            ((Part)port.Object).Command = null;
            this.Command = null;


            if (PosDatas.Count > 0)
                StartPos = PosDatas.Last()._endPos;
            if (LstHeightPD.Count > 0)
                Height = LstHeightPD.Last()._endPos;
            if (LstAnglePD.Count > 0)
                SetRotate(LstAnglePD.Last()._endPos, PVector3.UnitZ);
            
            ClearMoveSchedule();

            if (Lcs.QueuedCommands.Count == 0)
                Lcs.SetIdleDestinationAndRoute(simTime, this);
            else
                Lcs.DispatchCommands(simTime);

            SimPort newPort = new SimPort(INT_PORT.MOVE, this);
            InternalFunction(simTime, newPort);
        }
        protected override void ArriveToLoad(Time simTime, SimPort port)
        {
            base.ArriveToLoad(simTime, port);
        }
        protected override void Enter(Time simTime, SimPort port)
        {
            base.Enter(simTime, port);
        }
        public override bool IsEnter(SimPort port)
        {
            return Capa > EnteredObjects.Count;
        }
        #endregion

        #region [Move]
        protected override void Move(Time simTime, SimObj obj, ref SimPort port)
        {
            double destHeight = 0;
            double destAngle = StartAngle;
            if (State is VEHICLE_STATE.MOVE_TO_LOAD)
            {
                destHeight = Destination.PosVec3.Z;
                destAngle = SetFromToRadian(Command.StartStation, Command.StartNode);
            }
            else if (State is VEHICLE_STATE.MOVE_TO_UNLOAD)
            {
                destHeight = Destination.PosVec3.Z;
                destAngle = SetFromToRadian(Command.EndStation, Command.EndNode);
            }
            else
            {
                destHeight = Destination.PosVec3.Z;
                destAngle = StartAngle;
            }


            Time arriveTime = 0;
            if (GetHeightAtTime(simTime) != destHeight)
            {
                Time elevEndTime = AddHeightPosData(simTime, GetHeightAtTime(simTime), destHeight);
                arriveTime = arriveTime < elevEndTime ? elevEndTime : arriveTime;
            }
            if (GetRadianAtTime(simTime) != destAngle)
            {
                Time angleEndTime = AddAnglePosData(simTime, GetRadianAtTime(simTime), destAngle);
                arriveTime = arriveTime < angleEndTime ? angleEndTime : arriveTime;
                StartAngle = destAngle;
            }


            SimPort newPort = new SimPort(INT_PORT.ARRIVE_TO_STATION, this);
            EvtCalendar.AddEvent(arriveTime, this, newPort);
        }
        #endregion

        #region [Get Time 4 EventCalender]
        private Time AddHeightPosData(Time simTime, double curHeight, double elevDest)
        {
            Time diffT = Math.Abs((elevDest - curHeight) / _heightSpeed);
            _lstHeightPD.Add(new PosData(_heightSpeed, curHeight, simTime, simTime + diffT, elevDest));
            return simTime + diffT;
        }
        private Time AddAnglePosData(Time simTime, double curAngle, double destAngle)
        {
            double rotateSpeed = MathHelper.ToRadians(_angleSpeed);
            Time diffT = Math.Abs((destAngle - curAngle) / rotateSpeed);
            _lstAnglePD.Add(new PosData(rotateSpeed, AngleInRadians, simTime, simTime + diffT, destAngle));
            return simTime + diffT;
        }
        #endregion

        #region [Update]
        public override void UpdateAnimationPos()
        {
            Time now = SimEngine.Instance.TimeNow;
            double curHei = GetHeightAtTime(now);

            PosVec3 = new PVector3(PosVec3.X, PosVec3.Y, curHei);

            double radian = GetRadianAtTime(SimEngine.Instance.TimeNow);
            SetRotate(radian, PVector3.UnitZ);

            foreach (SimObj obj in EnteredObjects)
            {
                obj.PosVec3 = new PVector3(PosVec3.X, PosVec3.Y, PosVec3.Z);
                obj.SetRotate(radian, PVector3.UnitZ);
            }
        }
        public override void ClearMoveSchedule()
        {
            base.ClearMoveSchedule();
            LstAnglePD.Clear();
            LstHeightPD.Clear();
        }
        public override void SetStartPosition(TransportLine line, double distance, Time initTime, double lastSpeed)
        {
            base.SetStartPosition(line, distance, initTime, lastSpeed);

            double totaldist = PVector3.Distance(line.EndPoint.PosVec3, line.StartPoint.PosVec3);
            double radian = AngleInRadians;
            SetRotate(radian, PVector3.UnitZ);
            StartHeight = (line.EndPoint.PosVec3.Z - line.StartPoint.PosVec3.Z) * (distance / totaldist);
            PosVec3 = new PVector3(line.StartPoint.PosVec3.X, line.StartPoint.PosVec3.Y, StartHeight);
            Height = StartHeight;
            StartAngle = radian;
        }
        protected override void ArriveToIdle(Time simTime, SimPort port)
        {
            if (PosDatas.Count > 0)
                StartPos = PosDatas.Last()._endPos;
            if (LstHeightPD.Count > 0)
                Height = LstHeightPD.Last()._endPos;
            if (LstAnglePD.Count > 0)
                SetRotate(LstAnglePD.Last()._endPos, PVector3.UnitZ);

            ClearMoveSchedule();

            Lcs.SetIdleDestinationAndRoute(simTime, this);
            SimPort simPort1 = new SimPort(INT_PORT.MOVE, this);
            InternalFunction(simTime, simPort1);
        }
        #endregion

        #region [Get Current Info]
        public double GetHeightAtTime(Time simTime)
        {
            double curHeight = 0;

            if (_lstHeightPD.Count == 0)
            {
                curHeight = Height;
            }
            else
            {
                for (int i = 0; i < _lstHeightPD.Count; i++)
                {
                    PosData tempPosData = _lstHeightPD[i];
                    if (tempPosData._startTime <= simTime && tempPosData._endTime > simTime)
                    {
                        PosData objPosData = tempPosData;
                        if (objPosData._endPos >= objPosData._startPos)
                            curHeight = objPosData._startPos + Physics.GetLength_v0_a_t(objPosData._startSpeed, objPosData._celerate, (simTime - objPosData._startTime).TotalSeconds);
                        if (objPosData._endPos < objPosData._startPos)
                            curHeight = objPosData._startPos - Physics.GetLength_v0_a_t(objPosData._startSpeed, objPosData._celerate, (simTime - objPosData._startTime).TotalSeconds);
                        //continue;
                        break;
                    }
                    //oht가 정차해 있는 경우
                    else if (simTime >= tempPosData._startTime)
                        curHeight = tempPosData._endPos;
                }
            }

            return curHeight;
        }
        public double GetRadianAtTime(Time simTime)
        {
            double curRadian = 0;

            if (_lstAnglePD.Count == 0)
            {
                curRadian = AngleInRadians;
            }
            else
            {
                for (int i = 0; i < _lstAnglePD.Count; i++)
                {
                    PosData tempPosData = _lstAnglePD[i];
                    if (tempPosData._startTime <= simTime && tempPosData._endTime > simTime)
                    {
                        PosData objPosData = tempPosData;
                        if (objPosData._endPos >= objPosData._startPos)
                            curRadian = objPosData._startPos + Physics.GetLength_v0_a_t(objPosData._startSpeed, objPosData._celerate, (simTime - objPosData._startTime).TotalSeconds);
                        if (objPosData._endPos < objPosData._startPos)
                            curRadian = objPosData._startPos - Physics.GetLength_v0_a_t(objPosData._startSpeed, objPosData._celerate, (simTime - objPosData._startTime).TotalSeconds);
                        //continue;
                        break;
                    }
                    //oht가 정차해 있는 경우
                    else if (simTime >= tempPosData._startTime)
                        curRadian = tempPosData._endPos;
                }
            }

            if (curRadian - Math.PI * 2 == fromRadian)
                curRadian = fromRadian;
            else if (curRadian + Math.PI * 2 == fromRadian)
                curRadian = fromRadian;

            return curRadian;
        }
        public override void AddStopSchedule(Time stopTime)
        {
            base.AddStopSchedule(stopTime);

            Time timeNow = SimEngine.Instance.TimeNow;
            double endHeight;
            Time endHeightTime;
            if (LstHeightPD.Count > 0)
            {
                endHeight = LstHeightPD.Last()._endPos;
                endHeightTime = LstHeightPD.Last()._endTime;
            }
            else
            {
                endHeight = Height;
                endHeightTime = timeNow;
            }

            if (endHeightTime < stopTime)
                LstHeightPD.Add(new PosData(0, 0, endHeightTime, endHeight, 0, stopTime, endHeight));

            double endAngle;
            Time endAngleTime;
            if (LstAnglePD.Count > 0)
            {
                endAngle = LstAnglePD.Last()._endPos;
                endAngleTime = LstAnglePD.Last()._endTime;
            }
            else
            {
                endAngle = AngleInRadians;
                endAngleTime = timeNow;
            }

            if (endAngleTime < stopTime)
                LstAnglePD.Add(new PosData(0, 0, endAngleTime, endAngle, 0, stopTime, endAngle));
        }
        #endregion

        #region [Method]
        private double SetFromToRadian(SimNode fromNode, SimNode toNode)
        {
            double fromToRad = 0;
            PVector3 fromNodePosition;
            if (fromNode is TransportLine)
            {
                if (InLinkNodes.Contains(fromNode))
                    fromNodePosition = ((TransportLine)fromNode).EndPoint.PosVec3;
                else
                    fromNodePosition = ((TransportLine)fromNode).StartPoint.PosVec3;
            }
            else
                fromNodePosition = fromNode.PosVec3;
            fromNodePosition.Z = 0;
            PVector3 toNodePosition;
            if (toNode is TransportLine)
            {
                if (OutLinkNodes.Contains(toNode))
                    toNodePosition = ((TransportLine)toNode).StartPoint.PosVec3;
                else
                    toNodePosition = ((TransportLine)toNode).EndPoint.PosVec3;
            }
            else
                toNodePosition = toNode.PosVec3;
            toNodePosition.Z = 0;
            fromToRad = PVector3.AngleRadian(new PVector3(0, 1, 0), PVector3.Direction(fromNodePosition, toNodePosition), PVector3.Coordinate.Z);
            return fromToRad;
        }

        public void SetFacilityDirection(double direction)
        {
            this.facilityDirection = direction;
        }
        #endregion
    }
}
