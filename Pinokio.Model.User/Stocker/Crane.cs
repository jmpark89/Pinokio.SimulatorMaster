using Logger;
using Pinokio.Database;
using Pinokio.Geometry;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using static Pinokio.Model.Base.SimResultDBManager;

namespace Pinokio.Model.User
{
    [Serializable]
    public class Crane : Vehicle
    {
        private Time _delay;
        private double _heightSpeed;
        private List<PosData> _lstHeightPD;
        private List<PosData> _lstAnglePD;
        private double _accSpeed;
        private double _angleSpeed;
        private PVector3 _cranePoint;
        [Storable(false)]
        protected double fromRadian = 0; //start radian
        [Storable(false)]
        protected double toRadian = 0;
        [Storable(false)]
        protected double arcRadius = 1000;
        protected CraneLine _mainLine;
        protected TXNode fromNode = null;
        protected TXNode toNode = null;

        private DateTime _workStartTime;
        private DateTime _deliveryStartTime;
        private List<Tuple<DateTime, DateTime>> _lstWorkTime;
        private List<Tuple<DateTime, DateTime, bool>> _lstLoadTime; // bool : isImport
        private List<Tuple<DateTime, DateTime, bool>> _lstUnloadTime;// bool : isImport

        public SCS Scs
        {
            get { return ParentNode as SCS; }
        }

        public List<PosData> LstHeightPD { get => _lstHeightPD; set => _lstHeightPD = value; }
        public List<PosData> LstAnglePD { get => _lstAnglePD; set => _lstAnglePD = value; }
        public PVector3 CraneEndPoint { get => _cranePoint; set => _cranePoint = value; }
        public double AccSpeed { get => _accSpeed; set => _accSpeed = value; }

        /// <summary>
        /// 현재 라인 내 진입 Position
        /// </summary>
        public double StartHeight
        { get; set; }

        public double StartAngle
        { get; set; }

        [StorableAttribute(true)]
        public double AngleSpeed { get => _angleSpeed; set => _angleSpeed = value; }
        [StorableAttribute(true)]
        public double HeightSpeed { get => _heightSpeed; set => _heightSpeed = value; }


        public Crane()
            : base()
        {
            Initialize();
        }

        public Crane(string name, int speed, double distance, CraneLine line, int width = 1500, int depth = 7100, int height = 1000) : base(name, distance, line, width, depth, height)
        {
            Initialize();
        }

        private void Initialize()
        {
            _delay = 2;
            LoadTime = 4;
            UnloadTime = 4;
            Speed = 1000;
            _heightSpeed = 667;
            _accSpeed = 500;
            _angleSpeed = 30;
            Deceleration = -2940;
            Capa = 1;
            _lstHeightPD = new List<PosData>();
            _lstAnglePD = new List<PosData>();
        }



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
            Scs.SetCompletedCommand(simTime, Command);

            ((Part)port.Object).Command = null;
            this.Command = null;

            if (PosDatas.Count > 0)
                StartPos = PosDatas.Last()._endPos;
            if (LstHeightPD.Count > 0)
                Height = LstHeightPD.Last()._endPos;
            if (LstAnglePD.Count > 0)
                SetRotate(LstAnglePD.Last()._endPos, PVector3.UnitZ);

            ClearMoveSchedule();

            if (Scs.QueuedCommands.Count == 0)
                Scs.SetIdleDestinationAndRoute(simTime, this);
            else
                Scs.DispatchCommands(simTime);



            SimPort newPort = new SimPort(INT_PORT.MOVE, this);
            InternalFunction(simTime, newPort);
        }

        protected override void Enter(Time simTime, SimPort port)
        {
            base.Enter(simTime, port);
        }
        public override bool IsEnter(SimPort port)
        {
            return Capa > EnteredObjects.Count;
        }

        public override void UpdateAnimationPos()
        {
            Time now = SimEngine.Instance.TimeNow;
            double curHei = GetHeightAtTime(now);

            PosVec3 = GetPosition(SimEngine.Instance.TimeNow);
            PosVec3 = new PVector3(PosVec3.X, PosVec3.Y, curHei);

            _angleInRadians = GetRadianAtTime(SimEngine.Instance.TimeNow);
            SetRotate(_angleInRadians, PVector3.UnitZ);

            SetEnteredObjectsPosNAngle();
        }

        protected void SetEnteredObjectsPosNAngle()
        {
            foreach (SimObj obj in EnteredObjects)
            {
                if (obj is Part)
                {
                    Part part = obj as Part;
                    part.PosVec3 = PosVec3 + new PVector3(0, 0, 800);
                    part.AngleInRadians = _angleInRadians;
                }
                else
                {
                    obj.PosVec3 = PosVec3 + new PVector3(0, 0, Size.Z);
                    obj.SetRotate(_angleInRadians, PVector3.UnitZ);
                }
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
            PVector3 di = new PVector3(line.Direction.X, line.Direction.Y, line.Direction.Z);
            double radian = PVector3.AngleRadian(new PVector3(0, 1, 0), di, PVector3.Coordinate.Z);
            SetRotate(radian, PVector3.UnitZ);

            StartAngle = radian;
            StartHeight = line.PosVec3.Z;
        }

        protected override void ArriveToIdle(Time simTime, SimPort port)
        {
            if(PosDatas.Count > 0)
                StartPos = PosDatas.Last()._endPos;
            if (LstHeightPD.Count > 0)
                Height = LstHeightPD.Last()._endPos;
            if (LstAnglePD.Count > 0)
                SetRotate(LstAnglePD.Last()._endPos, PVector3.UnitZ);

            ClearMoveSchedule();

            Scs.SetIdleDestinationAndRoute(simTime, this);
            SimPort simPort1 = new SimPort(INT_PORT.MOVE, this);
            InternalFunction(simTime, simPort1);
        }

        protected override void Move(Time simTime, SimObj obj, ref SimPort port)
        {
            double destLen = 0;
            PVector3 destPos;
            double destHeight = 0;
            double destAngle = StartAngle;
            if (State is VEHICLE_STATE.MOVE_TO_LOAD)
            {
                destPos = new PVector3(Command.StartStation.PosVec3.X, Command.StartStation.PosVec3.Y, Command.StartNode.PosVec3.Z);
                destHeight = Command.StartNode.PosVec3.Z;
                destLen = Line.GetStationLength(Command.StartStation);
                destAngle = SetFromToRadian(Command.StartStation, Command.StartNode);
            }
            else if (State is VEHICLE_STATE.MOVE_TO_UNLOAD)
            {
                destPos = new PVector3(Command.EndStation.PosVec3.X, Command.EndStation.PosVec3.Y, Command.EndNode.PosVec3.Z);
                destHeight = Command.EndNode.PosVec3.Z;
                destLen = Line.GetStationLength(Command.EndStation);
                destAngle = SetFromToRadian(Command.EndStation, Command.EndNode);
            }
            else
            {
                destPos = new PVector3(Destination.PosVec3.X, Destination.PosVec3.Y, Destination.PosVec3.Z);
                destHeight = Destination.PosVec3.Z;
                destAngle = StartAngle;
            }

            Time arriveTime = 0;
            //레일 거리 체크
            if (Distance != destLen)
                arriveTime = AddPosData(simTime, destLen);

            //엘리베이터 높이 체크
            if (Height != destHeight)
            {
                Time elevEndTime = AddHeightPosData(simTime, destHeight);
                arriveTime = arriveTime < elevEndTime ? elevEndTime : arriveTime;
            }

            if (AngleInRadians != destAngle)
            {
                Time angleEndTime = AddAnglePosData(simTime, destAngle);
                arriveTime = arriveTime < angleEndTime ? angleEndTime : arriveTime;
                StartAngle = destAngle;
            }

            AddStopSchedule(arriveTime);

            SimPort newPort = new SimPort(INT_PORT.ARRIVE_TO_STATION, this);
            EvtCalendar.AddEvent(arriveTime, this, newPort);
        }

        #region Move 

        private Time AddPosData(Time simTime, double railDest)
        {
            Time diffT = Math.Abs(railDest - Distance) / _speed;
            PosDatas.Add(new PosData(_speed, Distance, simTime, simTime + diffT, railDest));
            return simTime + diffT;
        }
        private Time AddHeightPosData(Time simTime, double elevDest)
        {
            Time diffT = Math.Abs((elevDest - Height) / _heightSpeed);
            _lstHeightPD.Add(new PosData(_heightSpeed, Height, simTime, simTime + diffT, elevDest));
            return simTime + diffT;
        }
        private Time AddAnglePosData(Time simTime, double destAngle)
        {
            double rotateSpeed = MathHelper.ToRadians(_angleSpeed);
            Time diffT = Math.Abs((destAngle - AngleInRadians) / rotateSpeed);
            _lstAnglePD.Add(new PosData(rotateSpeed, AngleInRadians, simTime, simTime + diffT, destAngle));
            return simTime + diffT;
        }
        private bool CheckArriveCranePos(Time simTime, double destLen, PVector3 destPos, ref Time arriveTime)
        {
            //레일 거리 체크
            if (Distance != destLen)
                arriveTime = AddPosData(simTime, destLen);

            //엘리베이터 높이 체크
            if (PosVec3.Z != destPos.Z)
            {
                Time elevEndTime = AddHeightPosData(simTime, destPos.Z);
                arriveTime = arriveTime < elevEndTime ? elevEndTime : arriveTime;
            }

            //좌,우 체크
            bool isLeft = IsRackLeft(destPos);

            if ((isLeft && AngleInRadians != 0) || (!isLeft && AngleInRadians == 0))
            {
                Time angleEndTime = AddAnglePosData(simTime, isLeft ? 0 : 180);
                arriveTime = arriveTime < angleEndTime ? angleEndTime : arriveTime;
            }

            if (arriveTime == 0)
                return true;
            else
                return false;
        }

        private bool IsRackLeft(PVector3 destPos)
        {
            PVector3 forward = destPos - Line.StartPoint.PosVec3;
            PVector3 cross = PVector3.Cross(forward, Line.Direction);
            if (PVector3.Dot(new PVector3(0, 0, 1), cross) < 0)
                return true;
            else
                return false;
        }

        public double ModifyPos4Ani_Len(Time simTime)
        {
            double dis = Distance;

            foreach (PosData pd in PosDatas)
            {
                if (simTime >= pd._endTime)
                    dis = pd._endPos;
                else if (simTime < pd._startTime)
                    break;

                if (simTime >= pd._startTime && simTime < pd._endTime)
                {
                    dis = pd._startPos > pd._endPos ? pd._startPos - ((double)(simTime - pd._startTime) * pd._startSpeed) : pd._startPos + ((double)(simTime - pd._startTime) * pd._startSpeed);

                    break;
                }
            }
            return dis;
        }
        public double ModifyPos4Ani_Height(Time simTime)
        {
            double hei = Height;

            foreach (PosData pd in _lstHeightPD)
            {
                if (simTime >= pd._endTime)
                    hei = pd._endPos;
                else if (simTime < pd._startTime)
                    break;

                if (simTime >= pd._startTime && simTime < pd._endTime)
                {
                    hei = pd._startPos > pd._endPos ? pd._startPos - ((double)(simTime - pd._startTime) * pd._startSpeed) : pd._startPos + ((double)(simTime - pd._startTime) * pd._startSpeed);

                    break;
                }
            }
            return hei;
        }

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

            fromToRad = PVector3.AngleRadian(new PVector3(1, 0, 0), PVector3.Direction(fromNodePosition, toNodePosition), PVector3.Coordinate.Z);

            return fromToRad;
        }

        protected override void Leave(Time simTime, SimPort port)
        {
            SetEnteredObjectsPosNAngle();
            base.Leave(simTime, port);
        }

        #endregion
    }
}
