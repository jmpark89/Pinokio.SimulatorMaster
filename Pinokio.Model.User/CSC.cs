using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Logger;
using Simulation.Engine;
using Pinokio.Model.Base;
using Pinokio.Geometry;

namespace Pinokio.Model.User
{
    public class CSC : Vehicle
    {
        private List<double> _lstFollowControlLength;
        private List<double> _lstFollowControlSpeed;
        private List<double> _lstVelocity;
        private List<double> _lstAcceleration;
        [StorableAttribute(false)]
        private bool _isInZCU;
        [StorableAttribute(false)]
        private double _size;
        private double _intervalLength;
        private double _minimumDistance;
        private Dictionary<uint, Time> _reservationStopPoints;
        private List<CSCZCU> _reservationZCUs;
        private const int BYPASS = 102;
        private Time bypassStartTime = 0;
        private bool _isSimpleMoveMode = true;

        [StorableAttribute(true)]
        public bool IsSimpleMoveMode
        {
            get { return _isSimpleMoveMode; }
            set { _isSimpleMoveMode = value; }
        }

        public CSCCS CSCcs
        {
            get { return ParentNode as CSCCS; }
        }


        [Browsable(true), StorableAttribute(true)]
        public double ByPassWaitingTime { get; set; }

        [Browsable(true), StorableAttribute(true)]
        public double StraightSpeed
        {
            get;set;
        }

        [Browsable(true), StorableAttribute(true)]
        public double CurveSpeed
        {
            get;set;
        }

        [Browsable(true), StorableAttribute(false)]
        public double IntervalLength
        {
            get => _intervalLength;
        }

        [Browsable(true), StorableAttribute(true)]
        public double VehicleSize
        {
            get => _size;
            set
            {
                _size = value;
                _intervalLength = _size + _minimumDistance;
            }
        }

        [Browsable(true), StorableAttribute(true)]
        public double MinimumDistance
        {
            get => _minimumDistance;
            set
            {
                _minimumDistance = value;
                _intervalLength = _size + _minimumDistance;
            }
        }


        [Browsable(true), StorableAttribute(true)]
        public bool UseTrackingControl
        {
            get;set;
        }
        public double MaxFollowControlLength
        {
            get
            {
                if (_lstFollowControlLength.Count > 0)
                    return _lstFollowControlLength[0];
                else
                    return 0;
            }
        }

        public CSCZCU CurZcu
        {
            get; set;
        }

        public new CSCLine Line
        {
            get
            {
                if (_route != null && _route.Count > 0)
                    return _route[0] as CSCLine;
                else
                    return null;
            }
        }

        [Browsable(true)]
        public string ReservationZcuResetPointName
        {
            get;set;
        }

        [Browsable(true)]
        public string CurZcuResetPointName
        {
            get; set;
        }

        public double CSCListVelocity(int idx)
        {
            return _lstVelocity[idx];
        }


        public Part Part
        {
            get
            {
                if (EnteredObjects.Count > 0)
                    return EnteredObjects.First() as Part;
                else
                    return null;
            }
        }

        [Browsable(true)]
        public Time ReservationTime
        {
            get;set;
        }

        [Browsable(false)]
        public List<CSCZCU> ReservationZCUs
        {
            get { return _reservationZCUs; }
        }

        [Browsable(false)]
        public Dictionary<uint, Time> ReservationStopPoints
        {
            get { return _reservationStopPoints; }
            set { _reservationStopPoints = value; }
        }

        public CSCZCU FirstReservationZCU
        {
            get
            {
                if (_reservationZCUs.Count > 0)
                    return _reservationZCUs[0];
                else
                    return null;
            }
        }

        public CSC()
            : base()
        {
            Speed = 100;
            Initialize();
        }

        public CSC(string name, double speed, double distance, GuidedLine curLine, int width = 1300, int depth = 1300, int height = 50)
            : base(name, distance, curLine, width, depth, height)
        {
            Speed = speed;
            Initialize();
        }

        private void Initialize()
        {
            LoadTime = 1;
            UnloadTime = 1;
            ByPassWaitingTime = 15;
            Deceleration = -2940;
            ReservationZcuResetPointName = string.Empty;
            ReservationTime = Time.Zero;
            _reservationStopPoints = new Dictionary<uint, Time>();
            _reservationZCUs = new List<CSCZCU>();
            CurZcuResetPointName = string.Empty;
            StraightSpeed = 3000;
            CurveSpeed = 1000;
            _size = 784;
            _minimumDistance = 500;
            _dispatchingDistance = 5779;
            _intervalLength = _size + _minimumDistance;
            InitAcceleration();
            initFollowControl(0);
            UseTrackingControl = true;
        }

        public override void InternalFunction(Time simTime, SimPort port)
        {
            base.InternalFunction(simTime, port);

            switch(port.PortType)
            {
                case BYPASS:
                    if (bypassStartTime != 0 && Command != null && State is VEHICLE_STATE.MOVE_TO_UNLOAD)
                    {
                        SimPort partPort = Command.EndNode.ArrivedPorts.Find(x => x.Object.ID == Part.ID);
                        Command.EndNode.ArrivedPorts.Remove(partPort);
                        Route = CSCcs.GetPath(simTime, this);
                        SimPort newPort = new SimPort(INT_PORT.MOVE, this);
                        InternalFunction(simTime, newPort);
                    }
                    else if(Route == null || Route.Count() == 0)
                    {
                        SimPort newPort = new SimPort(INT_PORT.MOVE, this);
                        InternalFunction(simTime, newPort);
                    }
                    break;
            }
        }

        /// <summary>
        /// Route 입력
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="saveLastRoute"></param>
        public void SetLstRailLine(List<TransportLine> lst)
        {
            try
            {
                Route = lst;
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void InitAcceleration()
        {
            try
            {
                _lstVelocity = new List<double>();
                _lstVelocity.Add(1333);
                _lstVelocity.Add(3333);
                _lstVelocity.Add(5000);

                _lstAcceleration = new List<double>();
                _lstAcceleration.Add(1960);
                _lstAcceleration.Add(1960);
                _lstAcceleration.Add(1000);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public double CSCAccelerationBySpeed(double speed)
        {
            if (0 <= speed && speed < _lstVelocity[0])
                return _lstAcceleration[0];
            else if (_lstVelocity[0] <= speed && speed < _lstVelocity[1])
                return _lstAcceleration[1];
            else// if (_lstVelocity[1] <= speed && speed < _lstVelocity[2])
                return _lstAcceleration[2];
        }

        /// <summary>
        /// 추종제어 거리, 속도 기준 초기화
        /// </summary>
        public void initFollowControl(double extraDistance)
        {
            try
            {
                _lstFollowControlSpeed = new List<double>();
                _lstFollowControlLength = new List<double>();
                _lstFollowControlLength.Add(Physics.GetLength_v_v0_a(0, 300 * 1000 / 60, Deceleration) + _intervalLength + extraDistance);
                _lstFollowControlLength.Add(Physics.GetLength_v_v0_a(0, 260 * 1000 / 60, Deceleration) + _intervalLength + extraDistance);
                _lstFollowControlLength.Add(Physics.GetLength_v_v0_a(0, 200 * 1000 / 60, Deceleration) + _intervalLength + extraDistance);
                _lstFollowControlLength.Add(Physics.GetLength_v_v0_a(0, 170 * 1000 / 60, Deceleration) + _intervalLength + extraDistance);
                _lstFollowControlLength.Add(Physics.GetLength_v_v0_a(0, 130 * 1000 / 60, Deceleration) + _intervalLength + extraDistance);
                _lstFollowControlLength.Add(Physics.GetLength_v_v0_a(0, 90 * 1000 / 60, Deceleration) + _intervalLength + extraDistance);
                _lstFollowControlLength.Add(Physics.GetLength_v_v0_a(0, 60 * 1000 / 60, Deceleration) + _intervalLength + extraDistance);
                _lstFollowControlLength.Add(Physics.GetLength_v_v0_a(0, 30 * 1000 / 60, Deceleration) + _intervalLength + extraDistance);
                _lstFollowControlLength.Add(Physics.GetLength_v_v0_a(0, 10 * 1000 / 60, Deceleration) + _intervalLength);
                _lstFollowControlLength.Add(0 + _intervalLength);

                _lstFollowControlSpeed.Add((300 * 1000) / 60);
                _lstFollowControlSpeed.Add((260 * 1000) / 60);
                _lstFollowControlSpeed.Add((200 * 1000) / 60);
                _lstFollowControlSpeed.Add((170 * 1000) / 60);
                _lstFollowControlSpeed.Add((130 * 1000) / 60);
                _lstFollowControlSpeed.Add((90 * 1000) / 60);
                _lstFollowControlSpeed.Add((60 * 1000) / 60);
                _lstFollowControlSpeed.Add((30 * 1000) / 60);
                _lstFollowControlSpeed.Add((10 * 1000) / 60);
                _lstFollowControlSpeed.Add(0);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        //-------------------------------

        /// <summary>
        /// 레일 길이를 고려한 최대속도 출력
        /// </summary>
        /// <param name="tempMaxSpeed"></param>
        /// <param name="lineMaxSpeed"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public double getMaxVelocityByLength(double tempMaxSpeed, double lineMaxSpeed, double length)
        {
            double returnVel = 0;
            for (int i = 0; i < _lstFollowControlLength.Count; i++)
            {
                if (length > _lstFollowControlLength[i])
                {
                    returnVel = _lstFollowControlSpeed[i];
                    break;
                }
            }

            returnVel = returnVel <= lineMaxSpeed ? returnVel : lineMaxSpeed;
            returnVel = returnVel <= tempMaxSpeed ? returnVel : tempMaxSpeed;
            return returnVel;
        }

        public double getChangeTimeUsing2CSC(double length, double v0A, double v0B, double aA, double aB)
        {
            double infiniteNum = double.MaxValue;
            double minimumLength = 0.01;

            int idx = -1;
            for (int i = 0; i < _lstFollowControlLength.Count; i++)
            {
                if (length > _lstFollowControlLength[i])
                {
                    idx = i;
                    break;
                }
            }

            //220 이하인 경우 error
            if (idx == -1)
            {
                double targetLength = _lstFollowControlLength[_lstFollowControlLength.Count - 1];
                targetLength += minimumLength;
                double A = 0.5 * (aB - aA);
                double B = v0B - v0A;
                double C = length - targetLength;

                double D = B * B - 4 * A * C;
                double t1, t2;
                if (A == 0)
                {
                    t1 = (targetLength - length) / B;
                    t1 = t1 < 0 ? infiniteNum : t1;
                    return double.IsNaN(t1) ? infiniteNum : t1;
                }
                else if (D < 0)
                    return infiniteNum;
                else
                {
                    t1 = (-B + Math.Sqrt(D)) / (2 * A);
                    t2 = (-B - Math.Sqrt(D)) / (2 * A);

                    t1 = t1 < 0 ? infiniteNum : t1;
                    t2 = t2 < 0 ? infiniteNum : t2;

                    if (t1 != infiniteNum || t2 != infiniteNum)
                    {
                        double returnTime = t1 <= t2 ? t1 : t2;

                        return double.IsNaN(returnTime) ? infiniteNum : returnTime;
                    }

                    else
                        return infiniteNum;
                }
            }
            //최대 속력인 경우 (= 낮아지는 변경점만 고려하는 경우) 
            else if (idx == 0)
            {
                double targetLength = _lstFollowControlLength[idx];
                targetLength -= minimumLength;
                double A = 0.5 * (aB - aA);
                double B = v0B - v0A;
                double C = length - targetLength;

                double D = B * B - 4 * A * C;
                double t1, t2;
                if (A == 0)
                {
                    t1 = (targetLength - length) / B;
                    t1 = t1 < 0 ? infiniteNum : t1;
                    return double.IsNaN(t1) ? infiniteNum : t1;
                }
                else if (D < 0)
                    return infiniteNum;
                else
                {
                    t1 = (-B + Math.Sqrt(D)) / (2 * A);
                    t2 = (-B - Math.Sqrt(D)) / (2 * A);

                    t1 = t1 < 0 ? infiniteNum : t1;
                    t2 = t2 < 0 ? infiniteNum : t2;

                    if (t1 != infiniteNum || t2 != infiniteNum)
                    {
                        double returnTime = t1 <= t2 ? t1 : t2;

                        return double.IsNaN(returnTime) ? infiniteNum : returnTime;
                    }

                    else
                        return infiniteNum;
                }
            }
            //양방향 변경점 고려해야 하는 경우
            else
            {
                double targetLength1 = _lstFollowControlLength[idx];
                targetLength1 -= minimumLength;

                double targetLength2 = _lstFollowControlLength[idx - 1];
                targetLength2 += minimumLength;

                double A = 0.5 * (aB - aA);
                double B = v0B - v0A;
                double C1 = length - targetLength1;
                double C2 = length - targetLength2;

                double D1 = B * B - 4 * A * C1;
                double D2 = B * B - 4 * A * C2;

                double t1, t2, t3, t4;

                if (A == 0)
                {
                    t1 = (targetLength1 - length) / B;
                    t1 = t1 < 0 ? infiniteNum : t1;
                    t2 = infiniteNum;
                }
                else if (D1 < 0)
                {
                    t1 = infiniteNum;
                    t2 = infiniteNum;
                }
                else
                {
                    t1 = (-B + Math.Sqrt(D1)) / (2 * A);
                    t2 = (-B - Math.Sqrt(D1)) / (2 * A);

                    t1 = t1 < 0 ? infiniteNum : t1;
                    t2 = t2 < 0 ? infiniteNum : t2;
                }

                if (A == 0)
                {
                    t3 = (targetLength2 - length) / B;
                    t3 = t3 < 0 ? infiniteNum : t3;
                    t4 = infiniteNum;
                }
                else if (D2 < 0)
                {
                    t3 = infiniteNum;
                    t4 = infiniteNum;
                }
                else
                {
                    t3 = (-B + Math.Sqrt(D2)) / (2 * A);
                    t4 = (-B - Math.Sqrt(D2)) / (2 * A);

                    t3 = t3 < 0 ? infiniteNum : t3;
                    t4 = t4 < 0 ? infiniteNum : t4;
                }

                double returnTime = t1 < t2 ? t1 : t2;
                returnTime = returnTime < t3 ? returnTime : t3;
                returnTime = returnTime < t4 ? returnTime : t4;



                return double.IsNaN(returnTime) ? infiniteNum : returnTime;
            }
        }

        protected override void ArriveToIdle(Time simTime, SimPort port)
        {
            CSCcs.SetIdleDestinationAndRoute(simTime, this);
            SimPort simPort1 = new SimPort(INT_PORT.MOVE, this);
            InternalFunction(simTime, simPort1);
        }

        protected override void Enter(Time simTime, SimPort port)
        {
            EndLoading(simTime, port);
        }

        protected override void Leave(Time simTime, SimPort port)
        {
            ArriveToIdle(simTime, port);
        }

        /// <summary>
        /// Load를 위해 도착했을 때 실행되는 함수
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="port"></param>
        protected override void ArriveToLoad(Time simTime, SimPort port)
        {
            SimPort partPort = null;

            if (Command != null)
                partPort = Line.ArrivedPorts.Find(x => x.Object.ID == Command.Part.ID);
            else
                partPort = Line.ArrivedPorts.First();

            Line.ArrivedPorts.Remove(partPort);

            if (ParentNode is VSubCS)
                ((VSubCS)ParentNode).SetLoadingCommand(simTime, Command);

            State = VEHICLE_STATE.LOADING;
            SimPort requestPort = new SimPort(EXT_PORT.REQUEST_ENTITY, Command.Part, this);
            Command.Part.CurTXNode.ExternalFunction(simTime, requestPort);
        }

        protected override void ArriveToUnload(Time simTime, SimPort port)
        {
            SimPort requestUnloadPort;
            if (Command != null)
            {
                requestUnloadPort = new SimPort(EXT_PORT.ARRIVE, Command.Part, this);
                requestUnloadPort.ToNode = Command.EndNode;
            }
            else
            {
                requestUnloadPort = new SimPort(EXT_PORT.ARRIVE, EnteredObjects.First(), this);
                requestUnloadPort.ToNode = Line.OutLinkNodeConnections.Where(x => x.Value == port.ToNode).First().Key;
            }

            if (ParentNode is VSubCS)
                ((VSubCS)ParentNode).SetUnloadingCommand(simTime, Command);

            State = VEHICLE_STATE.UNLOADING;
            requestUnloadPort.ToNode.ExternalFunction(simTime, requestUnloadPort);
        }


        protected override void Move(Time simTime, SimObj obj, ref SimPort port)
        {
            if (Line.LstObjectIndex.ContainsKey(obj.ID))
            {
                int objIndex = Line.LstObjectIndex[obj.ID];
                UpdateCSCPosition(simTime, objIndex);
            }
        }

        #region CSC Line 전용

        public void UpdateCSCPosition(Time curTime, int cscIdx)
        {
            try
            {
                if (cscIdx == -1 && Line.Vehicles.Count == 0)
                {
                    return;
                }
                else if (cscIdx == -1 && Line.Vehicles.Count > 0)
                {
                    cscIdx = 0;
                }

                for (int i = cscIdx; i < Line.Vehicles.Count; i++)
                {
                    if (((CSC)Line.Vehicles[i]).IsStoped)
                    {
                        if (!(Line.Vehicles[i].State is VEHICLE_STATE.IDLE))
                            ((CSC)Line.Vehicles[i]).IsStoped = false;
                        else
                            break;
                    }

                    bool isUpdatedThisCSC = UpdatePosData(curTime, (CSC)Line.Vehicles[i]);

                    if (!isUpdatedThisCSC)
                    {
                        break;
                    }

                    UpdateLateCSCPosition(curTime, (CSC)Line.Vehicles[i]);
                }

                CheckWaitingCSC(cscIdx, CSCcs.BumpDistance, curTime);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void UpdateLateCSCPosition(Time curTime, CSC csc)
        {

            try
            {
                if (!Line.LstObjectIndex.ContainsKey(csc.ID))
                    return;

                int cscIdx = Line.LstObjectIndex[csc.ID];

                Time frontCSCEndTime = Line.LstPosData[cscIdx].Last()._endTime;

                foreach (CSCZCU zcu in csc.ReservationZCUs.ToArray())
                {
                    MakeStopScheduleForZCUStopCSCs(curTime, zcu, csc, frontCSCEndTime);
                }

                if (csc.CurZcu != null)
                    MakeStopScheduleForZCUStopCSCs(curTime, csc.CurZcu, csc, frontCSCEndTime);

                Line.CallBackObject(curTime);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void CheckWaitingCSC(int cscIdx, double standLen, Time simTime)
        {
            try
            {
                CSC csc = Line.Vehicles[cscIdx] as CSC;
                double cscDistance = csc.Line.GetDistanceAtTime(csc, simTime);

                if (0 < cscIdx)
                {
                    CSC preCsc = Line.Vehicles[cscIdx - 1] as CSC;
                    double preCscDistance = preCsc.Line.GetDistanceAtTime(preCsc, simTime);
                    if (preCscDistance - cscDistance <= standLen)
                    {
                        if (preCsc.IsStoped)
                        {
                            SimPort preCscPort = new SimPort(INT_PORT.CSC_MOVE_TO_IDLE, this, preCsc);
                            EvtCalendar.AddEvent(simTime, this, preCscPort);

                            csc.Destination = preCsc.Destination;
                            double stationLength = 0;
                            TransportLine line = csc.Destination.GetLineNStationLength(ref stationLength);
                            csc.SetLstRailLine(CSCcs.FindPath(csc.Line, cscDistance, line, stationLength));
                        }
                    }
                }
                else
                {
                    double len = Line.Length - cscDistance;
                    foreach (GuidedLine line in csc.Route)
                    {
                        if (this.Name != line.Name)
                        {
                            if (line.Vehicles.Count > 0)
                            {
                                CSC preCsc = line.Vehicles[line.Vehicles.Count - 1] as CSC;
                                double preCscDistance = preCsc.Line.GetDistanceAtTime(preCsc, simTime);
                                len += preCscDistance;
                                if (len <= standLen)
                                {
                                    if (preCsc.IsStoped)
                                    {
                                        SimPort port = new SimPort(INT_PORT.CSC_MOVE_TO_IDLE, line, preCsc);
                                        EvtCalendar.AddEvent(simTime, line, port);
                                        Station asIsRailPort = preCsc.Destination;

                                        csc.Destination = preCsc.Destination;
                                        double stationLength = 0;
                                        TransportLine stationLine = csc.Destination.GetLineNStationLength(ref stationLength);
                                        csc.SetLstRailLine(CSCcs.FindPath(csc.Line, cscDistance, stationLine, stationLength));
                                    }
                                }
                                return;
                            }
                            else
                            {
                                len += line.Length;
                                if (len > standLen)
                                    return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void MakeStopScheduleForZCUStopCSCs(Time curTime, CSCZCU zcu, CSC csc, Time frontCSCEndTime)
        {
            try
            {
                foreach (CSC stopCSC in zcu.StopCSCs)
                {
                    if ((csc.ReservationZcuResetPointName != stopCSC.ReservationZcuResetPointName && csc.CurZcuResetPointName != stopCSC.ReservationZcuResetPointName) || csc.Name == stopCSC.Name)
                        continue;

                    List<PosData> lstStopPosData = ((CSCLine)stopCSC.Line).LstPosData[0];
                    double stopPos = lstStopPosData[lstStopPosData.Count - 1]._endPos;
                    if (lstStopPosData.Last()._endTime < frontCSCEndTime)
                    {
                        if (stopCSC.Line.Vehicles.Count > 0)
                        {
                            SimPort port = new SimPort(EXT_PORT.MOVE, stopCSC.Line, stopCSC);
                            port.Time = lstStopPosData.Last()._endTime;
                            stopCSC.ExternalFunction(curTime, port);
                        }
                        else
                            ((CSCLine)stopCSC.Line).CallBackObject(curTime);
                    }
                }

                foreach (List<CSCZCUReservation> zcuReservations in zcu.Reservations.Values)
                {
                    foreach (CSCZCUReservation zcuReservation in zcuReservations.ToArray())
                    {
                        CSC reservCSC = zcuReservation.CSC;

                        if (csc.ReservationZcuResetPointName != reservCSC.ReservationZcuResetPointName || csc.Name == reservCSC.Name)
                            continue;

                        SimPort port = new SimPort(EXT_PORT.MOVE, reservCSC.Line, reservCSC);
                        List<PosData> lstStopPosData = ((CSCLine)reservCSC.Line).LstPosData[0];

                        if (lstStopPosData.Count() == 0)
                            port.Time = frontCSCEndTime;
                        else if (lstStopPosData.Last()._endTime < frontCSCEndTime)
                            port.Time = lstStopPosData.Last()._endTime;

                        reservCSC.ExternalFunction(curTime, port);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public bool UpdatePosData(Time curTime, CSC csc) //cscIdx 0부터 시작
        {
            try
            {
                if (csc.Route[0].Name != Line.Name || csc.State is VEHICLE_STATE.LOADING || csc.State is VEHICLE_STATE.UNLOADING)
                    return false;

                bool isScheduled = false;
                int cscIdx = Line.LstObjectIndex[csc.ID];
                
                List<PosData> cscPosData = csc.PosDatas;
                int cscPosDatasCount = cscPosData.Count;
                double destPos = CalculateDestPos(curTime, csc, csc.Destination);

                double lastScheduledPos = 0;
                Time lastScheduledTime = Time.MinValue;
                double lastScheduledSpeed = 0;
                Line.GetLastSchedule(cscIdx, curTime, ref lastScheduledPos, ref lastScheduledTime, ref lastScheduledSpeed);

                if (lastScheduledTime > curTime)
                {
                    return false;
                }

                //destPos까지 opd 작성 안된 경우
                if (!(Math.Round(lastScheduledPos, 0) >= Math.Round(destPos, 0) && cscPosDatasCount > 0))
                {
                    MakeReservation(csc, lastScheduledPos, lastScheduledTime);
                    EvtData evtData;
                    if (_isSimpleMoveMode)
                        evtData = csc.GenerateSimpledPosData(curTime, destPos);
                    else
                        evtData = csc.GeneratePosData(curTime, destPos);

                    if (evtData._time > 0)
                    {
                        isScheduled = true;
                        if (evtData._isArrive)
                        {
                            SimPort port = new SimPort(INT_PORT.ARRIVE_TO_STATION, csc);
                            EvtCalendar.AddEvent(evtData._time, csc, port);
                        }
                        else
                        {
                            SimPort port = new SimPort(INT_PORT.MOVE, csc);
                            EvtCalendar.AddEvent(cscPosData.Last()._endTime, csc, port);
                        }
                    }
                }
                else if (Math.Round(lastScheduledPos, 0) == Math.Round(destPos, 0) && csc.Route.Count == 1 && lastScheduledTime < curTime)
                {
                    SimPort port = null;
                    cscPosData.Add(new PosData(0, 0, lastScheduledTime, curTime, lastScheduledPos, lastScheduledPos));
                    if (csc.State is VEHICLE_STATE.IDLE)
                    {
                        port = new SimPort(INT_PORT.MOVE, csc);
                        EvtCalendar.AddEvent(curTime, csc, port);
                    }
                }
                else if (Math.Round(lastScheduledPos, 0) == Math.Round(destPos, 0) && csc.Route.Count > 1)
                {
                    if (csc.Name == "CSC_94" && Line.Name == "CSCLine_64")
                        ;

                    SimPort newPort = new SimPort(EXT_PORT.REQUEST_TO_ENTER, csc, this);
                    csc.Route[1].ExternalFunction(curTime, newPort);
                }

                if (isScheduled == false
                && (cscPosData.Count > 0 && Math.Round(cscPosData.Last()._endSpeed, 1) < 20)
                && ((cscPosData.Count > 0 && curTime >= cscPosData.Last()._endTime) || (cscPosData.Count == 0 && curTime == csc.StartTime)))
                {
                    isScheduled = AddStoppedPosData();
                }

                return isScheduled;
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
                return false;
            }
        }

        public double CalculateDestPos(Time simTime, CSC csc, Station station) //cscIdx 0부터 시작
        {
            int cscIdx = Line.LstObjectIndex[csc.ID];
            double curPos = Line.LstStartPos[cscIdx];
            double curSpeed = Line.LstStartSpeed[cscIdx];
            if (Line.LstPosData[cscIdx].Count > 0)
            {
                curPos = Line.LstPosData[cscIdx][Line.LstPosData[cscIdx].Count - 1]._endPos;
                curSpeed = Line.LstPosData[cscIdx][Line.LstPosData[cscIdx].Count - 1]._endSpeed;
            }

            double stationDistance = Line.GetStationLength(station);
            //현재 라인에 목적지 있고 지나치지 않은 경우
            if (csc.Route.Count == 1)
            {
                if (Math.Round(stationDistance, 0) == Math.Round(curPos, 0) && Math.Round(curSpeed, 1) == 0)
                    return stationDistance;

                if (!(csc.State is VEHICLE_STATE.IDLE))
                {
                    GuidedLine stopLine = null;
                    double stopAvailableDistance = 0;
                    List<GuidedLine> listStoppingLine = new List<GuidedLine>();

                    csc.GetStopAvailableDistance(simTime, ref listStoppingLine, ref stopLine, ref stopAvailableDistance);

                    if (stopLine == null)
                    {
                        return Line.Length;
                    }

                    if (Line.Name == stopLine.Name && Math.Round(stationDistance, 0) >= Math.Round(stopAvailableDistance, 0))
                        return stationDistance;
                }
                else if (Math.Round(stationDistance, 0) > Math.Round(curPos, 0))
                {
                    return stationDistance;
                }
            }
            //현재 라인 통과해야 하는 경우

            return Line.Length;
        }

        private void MakeReservation(CSC csc, double lastSchedulePos, Time lastScheduleTime)
        {
            try
            {
                // 다음 point가 ZCU Stop인데 시작점 또는 마지막 이동한 곳이 다음 Point 예약지점이거나 지났는데 예약 안했을 때 
                if (((CSCPoint)Line.EndPoint).ZcuType == ZCU_TYPE.STOP
                && !(((CSCPoint)Line.EndPoint).Zcu.ContainsReservation(csc))
                && lastSchedulePos >= Line.GetEndPointZCUReservationPos(csc.Deceleration) // Port에 Loading 또는 Unloading 하고 늦게 예약하거나 늦게 디스패칭 됨.
                )
                {
                    ((CSCPoint)Line.EndPoint).Zcu.AddReservation(lastScheduleTime, csc, (CSCPoint)Line.EndPoint);
                }
                //다음다음 Point가  ZCU Stop인데 마지막 이동한 곳이 다음다음 Point 예약지점이거나 지났는데 예약 안했을 때  
                else if (csc.Route.Count > 1 && ((CSCPoint)csc.Route[1].EndPoint).ZcuType == ZCU_TYPE.STOP
                    && ((CSCLine)csc.Route[1]).GetEndPointZCUReservationPos(csc.Deceleration) < 0
                    && !(((CSCPoint)csc.Route[1].EndPoint).Zcu.ContainsReservation(csc))
                    && lastSchedulePos >= Line.GetToEndPointZCUReservationPos((CSCLine)csc.Route[1], csc.Deceleration)  // Port에 Loading 또는 Unloading 하고 늦게 예약하거나 늦게 디스패칭 됨.
                )
                {
                    ((CSCPoint)csc.Route[1].EndPoint).Zcu.AddReservation(lastScheduleTime, csc, (CSCPoint)csc.Route[1].EndPoint);
                }
                //다다다음 Point가 ZCU Stop인데 마지막 이동한 곳이 다다다음 Point 예약지점이거나 지났는데 예약 안했을 때
                else if (csc.Route.Count > 2 && ((CSCPoint)csc.Route[2].EndPoint).ZcuType == ZCU_TYPE.STOP
                    && ((CSCLine)csc.Route[2]).GetEndPointZCUReservationPos(csc.Deceleration) < 0
                    && ((CSCLine)csc.Route[1]).GetToEndPointZCUReservationPos((CSCLine)csc.Route[2], csc.Deceleration) < 0
                    && !(((CSCPoint)csc.Route[2].EndPoint).Zcu.ContainsReservation(csc))
                    && lastSchedulePos >= Line.GetToToEndPointZCUReservationPos((CSCLine)csc.Route[1], (CSCLine)csc.Route[2], csc.Deceleration)  // Port에 Loading 또는 Unloading 하고 늦게 예약하거나 늦게 디스패칭 됨.
                )
                {
                    ((CSCPoint)csc.Route[2].EndPoint).Zcu.AddReservation(lastScheduleTime, csc, (CSCPoint)csc.Route[2].EndPoint);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        /// <summary>
        /// CSC Spec, Rail Spec을 고려했을 때 가장 낮은 Speed를 MaxSpeed로 출력하는 함수
        /// </summary>
        /// <param name="csc"></param>
        /// <returns></returns>
        private double GetMaxSpeed(CSCLine line, CSC csc)
        {
            double cscSpeed = line.IsCurve ? csc.CurveSpeed : csc.StraightSpeed;
            return cscSpeed > line.MaxSpeed ? line.MaxSpeed : cscSpeed;
        }

        private void GetEndSpeedNCutPos(CSC csc, double lastPos, ref double endSpeed, ref double cutPos)
        {
            try
            {
                if (((CSCPoint)Line.EndPoint).ZcuType == ZCU_TYPE.STOP)
                {
                    if (Math.Round(lastPos, 0) < Math.Round(Line.GetEndPointZCUReservationPos(csc.Deceleration), 0) && !(((CSCPoint)Line.EndPoint).Zcu.ContainsReservation(csc)))
                    {
                        double toNodeZCUReservationPos = Line.GetEndPointZCUReservationPos(csc.Deceleration);

                        if (Math.Round(cutPos, 0) > Math.Round(toNodeZCUReservationPos, 0))
                            cutPos = toNodeZCUReservationPos;
                    }
                    else if (!((CSCPoint)Line.EndPoint).Zcu.KeepGoing(csc))   //통과했는데 우선순위에 의해 멈춰야할 때
                    {
                        endSpeed = 0;
                    }
                }

                //다다음 Point가  ZCU Stop인데 마지막 이동한 곳이 다다음 Point 예약지점일때              
                if (csc.Route.Count > 2)
                {
                    if (((CSCPoint)csc.Route[1].EndPoint).ZcuType == ZCU_TYPE.STOP)
                    {
                        if (((CSCLine)csc.Route[1]).GetEndPointZCUReservationPos(csc.Deceleration) < 0
                        && Math.Round(lastPos, 0) < Math.Round(Line.GetToEndPointZCUReservationPos((CSCLine)csc.Route[1], csc.Deceleration), 0))
                        {
                            double totoNodeZCUReservationPos = Line.GetToEndPointZCUReservationPos((CSCLine)csc.Route[1], csc.Deceleration);

                            if (cutPos > totoNodeZCUReservationPos)
                                cutPos = totoNodeZCUReservationPos;
                        }
                        else if (!((CSCPoint)csc.Route[1].EndPoint).Zcu.KeepGoing(csc)) //통과했는데 우선순위에 의해 멈춰야할 때
                        {
                            double toLineStartVelocityForStopPoint = Physics.GetVelocity0_v_a_s(0, csc.Deceleration, csc.Route[1].Length);
                            endSpeed = endSpeed > toLineStartVelocityForStopPoint ? toLineStartVelocityForStopPoint : endSpeed;
                        }
                    }
                }

                //다다다음 Point가  ZCU Stop인데 마지막 이동한 곳이 다다다음 Point 예약지점일때              
                if (csc.Route.Count > 3)
                {
                    if (((CSCPoint)csc.Route[2].EndPoint).ZcuType == ZCU_TYPE.STOP)
                    {
                        if (((CSCLine)csc.Route[2]).GetEndPointZCUReservationPos(csc.Deceleration) < 0
                        && ((CSCLine)csc.Route[1]).GetToEndPointZCUReservationPos((CSCLine)csc.Route[2], csc.Deceleration) < 0
                        && Math.Round(lastPos, 0) < Math.Round(Line.GetToToEndPointZCUReservationPos((CSCLine)csc.Route[1], (CSCLine)csc.Route[2], csc.Deceleration), 0)
                        && !(((CSCPoint)csc.Route[2].EndPoint).Zcu.ContainsReservation(csc)))
                        {
                            double tototoNodeZCUReservationPos = Line.GetToToEndPointZCUReservationPos((CSCLine)csc.Route[1], (CSCLine)csc.Route[2], csc.Deceleration);

                            if (cutPos > tototoNodeZCUReservationPos)
                                cutPos = tototoNodeZCUReservationPos;
                        }
                        else if (!((CSCPoint)csc.Route[2].EndPoint).Zcu.KeepGoing(csc)) //통과했는데 우선순위에 의해 멈춰야할 때
                        {
                            double totoLineStartVelocityForStopPoint = Physics.GetVelocity0_v_a_s(0, csc.Deceleration, csc.Route[2].Length);
                            double toLineStartVelocityForStopPoint = Physics.GetVelocity0_v_a_s(totoLineStartVelocityForStopPoint, csc.Deceleration, csc.Route[1].Length);

                            endSpeed = endSpeed > toLineStartVelocityForStopPoint ? toLineStartVelocityForStopPoint : endSpeed;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }


        public void AddPosdata_simlogs_Opd(double tempMaxSpeed, CSC csc, Time startTime, double startSpeed, double startPos, double destPos, PosData opd, double lengthForToLine, double endSpeed, double cutPos, ref List<PosData> lstRef)
        {
            try
            {
                if (startTime >= opd._endTime)
                    return;
                else
                {
                    Time withinTime = startTime - opd._startTime;
                    double curNextCSCPos, curNextCSCVel;
                    if (withinTime != 0)
                    {
                        curNextCSCPos = opd._startPos + lengthForToLine + Physics.GetLength_v0_a_t(opd._startSpeed, opd._celerate, (double)withinTime);
                        curNextCSCVel = Physics.GetVelocity_v0_a_s(opd._startSpeed, opd._celerate, curNextCSCPos - opd._startPos - lengthForToLine);
                    }
                    else
                    {
                        curNextCSCPos = opd._startPos + lengthForToLine;
                        curNextCSCVel = opd._startSpeed;
                    }
                    //csc간 거리 확인
                    double frontCSCLength = curNextCSCPos - startPos;
                    //최대 속도 확인
                    double maxVelocity = csc.getMaxVelocityByLength(tempMaxSpeed, Line.MaxSpeed, frontCSCLength);


                    if (maxVelocity == 0)
                    {
                        if (startSpeed > 0)
                        {
                            double changeTime = Physics.GetTime_v_v0_a(0, startSpeed, csc.Deceleration);
                            double changePos = Physics.GetLength_v0_a_t(startSpeed, csc.Deceleration, changeTime);

                            lstRef.Add(new PosData(csc.Deceleration, startSpeed, startTime, startPos, 0, startTime + changeTime, startPos + changePos));

                            if (lstRef.Last()._endTime > opd._endTime)
                            {
                                lstRef = cutListPosDataByTime(lstRef, (double)opd._endTime);
                                return;
                            }

                            if (Math.Round(cutPos, 0) < Math.Round(lstRef.Last()._endPos, 0))
                            {
                                lstRef = cutListPosDataByPos(lstRef, cutPos);
                                return;
                            }
                            else if (Math.Round(cutPos, 0) == Math.Round(lstRef.Last()._endPos, 0))
                                return;
                            else
                            {
                                startSpeed = lstRef.Last()._endSpeed;
                                startPos = lstRef.Last()._endPos;
                                startTime = lstRef.Last()._endTime;
                            }
                        }
                        else //startSpeed == 0
                        {
                            double changeTime = csc.getChangeTimeUsing2CSC(frontCSCLength, startSpeed, curNextCSCVel, 0, opd._celerate);
                            //                        if (changeTime < _minchangeTime)
                            //                            changeTime = _minchangeTime;

                            if ((opd._celerate == 0 && opd._startSpeed == 0) || changeTime == double.MaxValue)
                            {
                                lstRef.Add(new PosData(0, 0, startTime, startPos, 0, opd._endTime, startPos));
                                if (Math.Round(cutPos, 0) < Math.Round(lstRef.Last()._endPos, 0))
                                    lstRef = cutListPosDataByPos(lstRef, cutPos);

                                return;
                            }
                            else
                            {
                                lstRef.Add(new PosData(0, 0, startTime, startPos, 0, startTime + changeTime, startPos));
                                if (Math.Round(cutPos, 0) < Math.Round(lstRef.Last()._endPos, 0))
                                {
                                    lstRef = cutListPosDataByPos(lstRef, cutPos);
                                    return;
                                }
                                else if (Math.Round(cutPos, 0) == Math.Round(lstRef.Last()._endPos, 0))
                                    return;
                                else
                                {
                                    startSpeed = lstRef.Last()._endSpeed;
                                    startPos = lstRef.Last()._endPos;
                                    startTime = lstRef.Last()._endTime;
                                }
                            }
                        }

                        AddPosdata_simlogs_Opd(tempMaxSpeed, csc, startTime, startSpeed, startPos, destPos, opd, lengthForToLine, endSpeed, cutPos, ref lstRef);
                        return;
                    }
                    else
                    {
                        //현재 속도가 최대 속도인 경우
                        if (Math.Round(startSpeed, 2) == Math.Round(maxVelocity, 2))
                        {
                            //현재 속도로 등속운동 할때 변경 시점 확인
                            double changeTime = csc.getChangeTimeUsing2CSC(frontCSCLength, startSpeed, curNextCSCVel, 0, opd._celerate);

                            double stopReadyLength = 0; //if (endSpeed >= startSpeed) 
                            if (endSpeed < startSpeed)
                                stopReadyLength = Physics.GetLength_v_v0_a(endSpeed, startSpeed, csc.Deceleration);
                            //목적지 - 필요거리 = 여유거리(감속 시작해야 하는 위치)
                            double stopReadyPos = destPos - stopReadyLength;
                            //감속 시작위치 까지 걸리는 시간
                            double stopReadyTime = Math.Round(Physics.GetTime_v_s(startSpeed, Math.Round(stopReadyPos - startPos, 3)), 5);
                            //opd._endTime 보다 같거나 낮은 경우
                            if (startTime + changeTime <= opd._endTime || startTime + stopReadyTime <= opd._endTime)
                            {
                                //여유시간이 없는 경우(발생하면 안되는 경우)!! (커멘드 할당하는 순간에만 적용)
                                if (stopReadyTime < 0)
                                {
                                    //Console.WriteLine("커맨드 할당 시 급하게 멈추는 경우!!------- " + csc.Name);
                                    double posibleEndSpeed = endSpeed > maxVelocity ? maxVelocity : endSpeed;
                                    posibleEndSpeed = posibleEndSpeed > startSpeed ? startSpeed : posibleEndSpeed;
                                    double inputEndPos = startPos + Physics.GetLength_v_v0_a(posibleEndSpeed, startSpeed, csc.Deceleration);
                                    double inputEndTime = (double)startTime + Physics.GetTime_v_v0_a(posibleEndSpeed, startSpeed, csc.Deceleration);
                                    lstRef.Add(new PosData(csc.Deceleration, startSpeed, startTime, startPos, posibleEndSpeed, inputEndTime, inputEndPos));
                                    if (Math.Round(cutPos, 0) < Math.Round(lstRef.Last()._endPos, 0))
                                        lstRef = cutListPosDataByPos(lstRef, cutPos);

                                    return;
                                }
                                else if (startSpeed > endSpeed && stopReadyTime <= changeTime)
                                {
                                    double inputEndPos = stopReadyPos;
                                    double inputEndTime = (double)startTime + stopReadyTime;
                                    lstRef.Add(new PosData(0, startSpeed, startTime, startPos, startSpeed, inputEndTime, inputEndPos));
                                    lstRef.Add(new PosData(csc.Deceleration, startSpeed, inputEndTime, inputEndPos, endSpeed, inputEndTime + Physics.GetTime_v_v0_a(endSpeed, startSpeed, csc.Deceleration), destPos));
                                    if (Math.Round(cutPos, 0) < Math.Round(lstRef.Last()._endPos, 0))
                                        lstRef = cutListPosDataByPos(lstRef, cutPos);

                                    return;
                                }
                                else
                                {
                                    //startSpeed <= endSpeed || stopReadyTime > changeTime)
                                    if (changeTime < stopReadyTime)
                                    {
                                        //변경시점까지 스케줄링(등속도 스케줄링)
                                        double inputEndPos = startPos + startSpeed * changeTime;
                                        double inputEndTime = (double)startTime + changeTime;
                                        lstRef.Add(new PosData(0, startSpeed, startTime, startPos, startSpeed, inputEndTime, inputEndPos));
                                        //dest 고려
                                        if (Math.Round(cutPos, 0) < Math.Round(lstRef.Last()._endPos, 0))
                                        {
                                            lstRef = cutListPosDataByPos(lstRef, cutPos);
                                            return;
                                        }
                                        else //재귀                                  
                                            AddPosdata_simlogs_Opd(tempMaxSpeed, csc, inputEndTime, startSpeed, inputEndPos, destPos, opd, lengthForToLine, endSpeed, cutPos, ref lstRef);

                                        return;
                                    }
                                    else
                                    {
                                        if (stopReadyTime == 0)
                                            return;

                                        //startSpeed == endSpeed


                                        double celeration = 0;//if(startSpeed == maxVelocity)
                                        if (startSpeed < maxVelocity)
                                        {
                                            if (startSpeed > endSpeed)
                                                celeration = csc.Deceleration;
                                            if (startSpeed < endSpeed)
                                                celeration = csc.CSCAccelerationBySpeed(startSpeed);
                                        }
                                        celeration = 0;
                                        //stopReadyTime 은 등속도 운동을 기준으로 만들어짐, 여기선 등속도 운동으로 만들어진 걸로 가속도를 계산 Error
                                        double inputEndPos = startPos + Physics.GetLength_v0_a_t(startSpeed, celeration, stopReadyTime);
                                        double inputEndTime = (double)startTime + stopReadyTime;
                                        double inputEndSpeed = Physics.GetVelocity_v0_a_t(startSpeed, celeration, stopReadyTime);
                                        lstRef.Add(new PosData(celeration, startSpeed, startTime, startPos, inputEndSpeed, inputEndTime, inputEndPos));
                                        //dest 고려
                                        if (Math.Round(cutPos, 0) < Math.Round(lstRef.Last()._endPos, 0))
                                        {
                                            lstRef = cutListPosDataByPos(lstRef, cutPos);
                                            return;
                                        }
                                        else //재귀                                  
                                            AddPosdata_simlogs_Opd(tempMaxSpeed, csc, inputEndTime, inputEndSpeed, inputEndPos, destPos, opd, lengthForToLine, endSpeed, cutPos, ref lstRef);

                                        return;
                                    }
                                }
                            }
                            else
                            {
                                //opd._endTime까지 스케줄링 (등속도 스케줄링)
                                double inputEndTime = (double)opd._endTime;
                                double inputEndPos = startPos + (inputEndTime - (double)(startTime)) * startSpeed;
                                lstRef.Add(new PosData(0, startSpeed, startTime, startPos, startSpeed, inputEndTime, inputEndPos));

                                if (Math.Round(cutPos, 0) < Math.Round(lstRef.Last()._endPos, 0))
                                    lstRef = cutListPosDataByPos(lstRef, cutPos);
                                //종료
                                return;
                            }
                        }
                        else if (startSpeed > maxVelocity)
                        {
                            double inputEndTime = (double)startTime + Physics.GetTime_v_v0_a(maxVelocity, startSpeed, csc.Deceleration);
                            double inputEndPos = startPos + Physics.GetLength_v_v0_a(maxVelocity, startSpeed, csc.Deceleration);

                            lstRef.Add(new PosData(csc.Deceleration, startSpeed, startTime, startPos, maxVelocity, inputEndTime, inputEndPos));

                            if (lstRef.Last()._endTime > opd._endTime)
                            {
                                lstRef = cutListPosDataByTime(lstRef, (double)opd._endTime);
                                if (Math.Round(cutPos, 0) < Math.Round(lstRef.Last()._endPos, 0))
                                    lstRef = cutListPosDataByPos(lstRef, cutPos);
                            }
                            else if (Math.Round(cutPos, 0) < Math.Round(lstRef.Last()._endPos, 0))
                            {
                                lstRef = cutListPosDataByPos(lstRef, cutPos);
                                if (lstRef.Last()._endTime > opd._endTime)
                                    lstRef = cutListPosDataByTime(lstRef, (double)opd._endTime);
                            }
                            else
                                AddPosdata_simlogs_Opd(tempMaxSpeed, csc, lstRef.Last()._endTime, lstRef.Last()._endSpeed, lstRef.Last()._endPos, destPos, opd, lengthForToLine, endSpeed, cutPos, ref lstRef);

                            return;
                        }
                        else
                        {
                            double changeTime = csc.getChangeTimeUsing2CSC(frontCSCLength, startSpeed, curNextCSCVel, csc.CSCAccelerationBySpeed(startSpeed), opd._celerate);
                            double decelChangeTime = csc.getChangeTimeUsing2CSC(frontCSCLength, startSpeed, curNextCSCVel, csc.Deceleration, opd._celerate);
                            double maxTime = Physics.GetTime_v_v0_a(maxVelocity, startSpeed, csc.CSCAccelerationBySpeed(startSpeed));

                            List<PosData> tmpLst = new List<PosData>();
                            AddPosdata_simlogs(maxVelocity, csc, startTime, startSpeed, startPos, endSpeed, destPos, cutPos, ref tmpLst);

                            for (int k = 0; k < tmpLst.Count; k++)
                            {
                                // 바로 감속하는 경우
                                if (tmpLst[k]._celerate <= 0)
                                {
                                    lstRef.Add(tmpLst[k]);
                                }
                                else
                                {
                                    if (startTime + changeTime <= tmpLst[k]._endTime)
                                    {
                                        if (changeTime <= maxTime)
                                        {
                                            double inputEndPos = startPos + Physics.GetLength_v0_a_t(startSpeed, csc.CSCAccelerationBySpeed(startSpeed), changeTime);
                                            double inputEndTime = (double)startTime + changeTime;
                                            double v = Physics.GetVelocity_v0_a_t(startSpeed, csc.CSCAccelerationBySpeed(startSpeed), changeTime);

                                            lstRef.Add(new PosData(csc.CSCAccelerationBySpeed(startSpeed), startSpeed, startTime, startPos, v, inputEndTime, inputEndPos));

                                        }
                                        else
                                        {
                                            double inputEndPos = startPos + Physics.GetLength_v0_a_t(startSpeed, csc.CSCAccelerationBySpeed(startSpeed), maxTime);
                                            double inputEndTime = (double)startTime + maxTime;
                                            lstRef.Add(new PosData(csc.CSCAccelerationBySpeed(startSpeed), startSpeed, startTime, startPos, maxVelocity, inputEndTime, inputEndPos));
                                        }
                                    }
                                    else
                                    {
                                        lstRef.Add(tmpLst[k]);
                                    }
                                }

                                if (lstRef.Last()._endTime > opd._endTime)
                                {
                                    lstRef = cutListPosDataByTime(lstRef, (double)opd._endTime);
                                    if (Math.Round(cutPos, 0) < Math.Round(lstRef.Last()._endPos, 0))
                                        lstRef = cutListPosDataByPos(lstRef, cutPos);
                                }
                                else if (Math.Round(cutPos, 0) < Math.Round(lstRef.Last()._endPos, 0))
                                {
                                    lstRef = cutListPosDataByPos(lstRef, cutPos);
                                    if (lstRef.Last()._endTime > opd._endTime)
                                        lstRef = cutListPosDataByTime(lstRef, (double)opd._endTime);
                                }
                                else
                                    AddPosdata_simlogs_Opd(tempMaxSpeed, csc, lstRef.Last()._endTime, lstRef.Last()._endSpeed, lstRef.Last()._endPos, destPos, opd, lengthForToLine, endSpeed, cutPos, ref lstRef);

                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void AddPosdata_simlogs_limitTime(double tempMaxSpeed, CSC csc, Time startTime, double startSpeed, double startPos, double endPos, double preCSCVel, double limitTime, double destPos, double endSpeed, double cutPos, ref List<PosData> lstRef)
        {
            try
            {
                //csc간 거리 확인
                double length = endPos - startPos;
                //최대 속도 확인
                double maxVelocity = csc.getMaxVelocityByLength(tempMaxSpeed, Line.MaxSpeed, length);

                if (length == csc.IntervalLength)
                    return;
                //움직일 여력 없는 경우
                else if (maxVelocity == 0)
                {
                    if (startSpeed != 0)
                    {
                        AddPosdata_simlogs(0, csc, startTime, startSpeed, startPos, 0, destPos, cutPos, ref lstRef);
                        lstRef = cutListPosDataByTime(lstRef, limitTime);
                    }
                    return;
                }
                //스케줄 계획할 여지 있는 경우
                else
                {
                    //목적지에 방해없이 갈 수 있는 경우
                    if (destPos + Physics.GetLength_v_v0_a(0, endSpeed, csc.Deceleration) + csc.IntervalLength <= endPos + Physics.GetLength_v_v0_a(0, preCSCVel, csc.Deceleration))
                    {
                        AddPosdata_simlogs(maxVelocity, csc, startTime, startSpeed, startPos, endSpeed, destPos, cutPos, ref lstRef);
                        return;
                    }
                    //목적지까지 갈 수 없는 경우
                    else
                    {
                        //앞에 멈춰있는 CSC의 위치 전까지 멈춘다는 스케줄
                        AddPosdata_simlogs(maxVelocity, csc, startTime, startSpeed, startPos, 0, endPos + Physics.GetLength_v_v0_a(0, preCSCVel, csc.Deceleration) - csc.IntervalLength, cutPos, ref lstRef);
                        if (lstRef.Count == 0)
                            return;

                        if (Math.Round(lstRef.Last()._endPos, 0) >= Math.Round(cutPos, 0))
                        {
                            lstRef = cutListPosDataByPos(lstRef, cutPos);
                            if (limitTime == double.MaxValue)
                                return;
                            else
                            {
                                if (lstRef.Last()._endTime > limitTime)
                                    lstRef = cutListPosDataByTime(lstRef, limitTime);

                                return;
                            }
                        }
                        else
                        {
                            if (lstRef.Last()._endTime > limitTime)
                                lstRef = cutListPosDataByTime(lstRef, limitTime);
                            else if (lstRef.Last()._endTime < limitTime)
                            {
                                if (limitTime == double.MaxValue)
                                    lstRef.Add(new PosData(0, 0, lstRef.Last()._endTime, lstRef.Last()._endPos, 0, lstRef.Last()._endTime + 0.1, lstRef.Last()._endPos));
                                else
                                    lstRef.Add(new PosData(0, 0, lstRef.Last()._endTime, lstRef.Last()._endPos, 0, limitTime, lstRef.Last()._endPos));
                            }
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        /// <summary>
        /// 최고속도, 시작시간/속도/위치, 도착속도/위치 기반으로opd 생성
        /// </summary>
        /// <param name="maxSpeed"></param>
        /// <param name="csc"></param>
        /// <param name="startTime"></param>
        /// <param name="startSpeed"></param>
        /// <param name="startPos"></param>
        /// <param name="endSpeed"></param>
        /// <param name="endPos"></param>
        /// <param name="lstRef"></param>

        public void AddPosdata_simlogs(double maxSpeed, CSC csc, Time startTime, double startSpeed, double startPos, double endSpeed, double endPos, double cutPos, ref List<PosData> lstRef)
        {
            try
            {
                double diffLength = endPos - startPos;
                if (Math.Round(diffLength, 9) == 0)
                    return;
                //startSpeed가 maxSpeed 초과하는 경우
                if (startSpeed > maxSpeed)
                {
                    if (endSpeed > maxSpeed)
                        endSpeed = maxSpeed;

                    double deceleration_length_1 = Physics.GetLength_v_v0_a(maxSpeed, startSpeed, csc.Deceleration);
                    double const_length = 0;
                    double deceleration_length_2 = 0;

                    if (diffLength <= deceleration_length_1)
                    {
                        deceleration_length_1 = diffLength;
                        //1감속 구간 정의
                    }
                    else
                    {
                        diffLength -= deceleration_length_1;
                        deceleration_length_2 = Physics.GetLength_v_v0_a(endSpeed, maxSpeed, csc.Deceleration);
                        if (diffLength <= deceleration_length_2)
                            deceleration_length_2 = diffLength;
                        else
                            const_length = diffLength - deceleration_length_2;
                    }


                    if (const_length == 0)
                    {
                        //startSpeed로 diffLength를 스케줄링 끝
                        double input_endSpeed = Physics.GetVelocity_v0_a_s(startSpeed, csc.Deceleration, deceleration_length_1);

                        if (double.IsNaN(input_endSpeed))
                            input_endSpeed = 0;

                        double movingTime = Physics.GetTime_v_v0_a(input_endSpeed, startSpeed, csc.Deceleration);

                        lstRef.Add(new PosData(csc.Deceleration, startSpeed, startTime, startPos, input_endSpeed, startTime + movingTime, startPos + deceleration_length_1));

                        if (deceleration_length_2 > 0)
                        {
                            input_endSpeed = endSpeed;// getVelocity_v0_a_s(input_endSpeed, csc.Deceleration, deceleration_length_2);
                            movingTime = Physics.GetTime_v_v0_a(input_endSpeed, lstRef.Last()._endSpeed, csc.Deceleration);

                            if (double.IsNaN(input_endSpeed))
                                input_endSpeed = 0;

                            lstRef.Add(new PosData(csc.Deceleration, lstRef.Last()._endSpeed, lstRef.Last()._endTime, lstRef.Last()._endPos, input_endSpeed, lstRef.Last()._endTime + movingTime, lstRef.Last()._endPos + deceleration_length_2));
                        }

                        if (Math.Round(lstRef.Last()._endPos, 0) > Math.Round(cutPos, 0))
                        {
                            lstRef = cutListPosDataByPos(lstRef, cutPos);
                            return;
                        }
                        else if (Math.Round(lstRef.Last()._endPos, 0) == Math.Round(cutPos, 0))
                            return;
                    }
                    else
                    {
                        double input_startSpeed = startSpeed;
                        double input_startTime = (double)startTime;
                        double input_startPos = startPos;

                        double input_endSpeed = 0;
                        double input_endTime = 0;
                        double input_endPos = 0;

                        if (deceleration_length_1 > 0)
                        {
                            //감속 스케줄링
                            input_endSpeed = Physics.GetVelocity_v0_a_s(startSpeed, csc.Deceleration, deceleration_length_1);
                            if (input_endSpeed < 0.0001 || double.IsNaN(input_endSpeed))
                                input_endSpeed = 0;

                            input_endTime = input_startTime + Physics.GetTime_v_v0_a(input_endSpeed, startSpeed, csc.Deceleration);
                            input_endPos = input_startPos + deceleration_length_1;

                            if (!double.IsNaN(input_endSpeed))
                            {
                                lstRef.Add(new PosData(csc.Deceleration, input_startSpeed, input_startTime, input_startPos, input_endSpeed, input_endTime, input_endPos));
                                if (Math.Round(lstRef.Last()._endPos, 0) > Math.Round(cutPos, 0))
                                {
                                    lstRef = cutListPosDataByPos(lstRef, cutPos);
                                    return;
                                }
                                else if (Math.Round(lstRef.Last()._endPos, 0) == Math.Round(cutPos, 0))
                                    return;

                                input_startSpeed = input_endSpeed;
                                input_startTime = input_endTime;
                                input_startPos = input_endPos;
                            }
                        }
                        if (const_length > 0 && input_endSpeed != 0)
                        {
                            input_endSpeed = maxSpeed;
                            input_endTime = input_startTime + const_length / input_endSpeed;
                            input_endPos = input_startPos + (input_endSpeed * (input_endTime - input_startTime));

                            if (input_endSpeed < 0.0001 || double.IsNaN(input_endSpeed))
                                input_endSpeed = 0;

                            //등속 스케줄링
                            lstRef.Add(new PosData(0, input_startSpeed, input_startTime, input_startPos, input_endSpeed, input_endTime, input_endPos));
                            if (Math.Round(lstRef.Last()._endPos, 0) > Math.Round(cutPos, 0))
                            {
                                lstRef = cutListPosDataByPos(lstRef, cutPos);
                                return;
                            }
                            else if (Math.Round(lstRef.Last()._endPos, 0) == Math.Round(cutPos, 0))
                                return;

                            input_startSpeed = input_endSpeed;
                            input_startTime = input_endTime;
                            input_startPos = input_endPos;
                        }
                        if (deceleration_length_2 > 0)
                        {
                            //감속 스케줄링
                            input_endSpeed = double.IsNaN(Physics.GetVelocity_v0_a_s(input_startSpeed, csc.Deceleration, deceleration_length_2)) ? 0 : Physics.GetVelocity_v0_a_s(input_startSpeed, csc.Deceleration, deceleration_length_2);
                            input_endTime = input_startTime + Physics.GetTime_v_v0_a(input_endSpeed, input_startSpeed, csc.Deceleration);
                            input_endPos = endPos;// input_startPos + deceleration_length_2;

                            lstRef.Add(new PosData(csc.Deceleration, input_startSpeed, input_startTime, input_startPos, input_endSpeed, input_endTime, input_endPos));
                            if (Math.Round(lstRef.Last()._endPos, 0) > Math.Round(cutPos, 0))
                            {
                                lstRef = cutListPosDataByPos(lstRef, cutPos);
                                return;
                            }
                            else if (Math.Round(lstRef.Last()._endPos, 0) == Math.Round(cutPos, 0))
                                return;
                        }
                    }
                    return;
                }
                else
                {
                    int idx = 0;
                    List<double> lstLen = new List<double>();
                    List<double> lstAccelerate = new List<double>();
                    List<double> lstEndSpeed = new List<double>();

                    double curSpeed = startSpeed;
                    double sumLen = 0;
                    // 정해진 거리까지 최고 속도를 기반으로 위치정보 계산
                    while (true)
                    {
                        if (idx > 2)
                            break;

                        if (curSpeed < csc.CSCListVelocity(idx))
                        {
                            double len = Physics.GetLength_v_v0_a(csc.CSCListVelocity(idx), curSpeed, csc.CSCAccelerationBySpeed(curSpeed));
                            if (sumLen + len <= diffLength)
                            {
                                sumLen += len;
                                lstLen.Add(len);
                                lstAccelerate.Add(csc.CSCAccelerationBySpeed(curSpeed));
                                curSpeed = csc.CSCListVelocity(idx);
                                lstEndSpeed.Add(curSpeed);
                                idx++;
                            }
                            else
                            {
                                len = diffLength - sumLen;

                                curSpeed = Physics.GetVelocity_v0_a_s(curSpeed, csc.CSCAccelerationBySpeed(curSpeed), len);
                                sumLen += len;
                                lstLen.Add(len);
                                lstAccelerate.Add(csc.CSCAccelerationBySpeed(curSpeed));
                                lstEndSpeed.Add(curSpeed);
                                break;
                            }
                        }
                        else
                            idx++;
                    }

                    //arrivalVelocity : 현재 낼 수 있는 최고 속도
                    double arrivalVelocity = lstEndSpeed.Count == 0 ? startSpeed : lstEndSpeed.Last();

                    var oriEndSpeed = endSpeed;

                    //속도가 arrivalVelocity을 넘을 수 없기 때문에, arrivalVelocity보다 endSpeed가 클 경우 End Speed 조정
                    if (endSpeed > arrivalVelocity)
                        endSpeed = arrivalVelocity;

                    if (endSpeed > maxSpeed)
                        endSpeed = maxSpeed;

                    double acceleration_length = 0;
                    double startV = startSpeed;
                    double startP = 0;

                    //max Speed까지 도달 까지 위치정보 계산
                    for (int i = 0; i < lstEndSpeed.Count; i++)
                    {
                        if (startV <= maxSpeed && maxSpeed <= lstEndSpeed[i])
                        {
                            //acceleration_length 구하고 나감
                            lstLen[i] = Physics.GetLength_v_v0_a(maxSpeed, startV, lstAccelerate[i]);
                            acceleration_length += lstLen[i]; // - (diffLength - startP);
                            lstEndSpeed[i] = Physics.GetVelocity_v0_a_s(startV, lstAccelerate[i], lstLen[i]);// endSpeed;

                            while (true)
                            {
                                if (lstLen.Count - 1 > i)
                                {
                                    lstLen.RemoveAt(lstLen.Count - 1);
                                    lstEndSpeed.RemoveAt(lstEndSpeed.Count - 1);
                                    lstAccelerate.RemoveAt(lstAccelerate.Count - 1);
                                }
                                else
                                    break;
                            }

                            break;
                        }
                        else
                        {
                            if (diffLength >= lstLen[i])
                            {
                                acceleration_length += lstLen[i];
                                startV = lstEndSpeed[i];
                                startP += lstLen[i];
                            }
                        }
                    }

                    //endSpeed가 maxSpeed 같거나 작다., 감속 충분 거리
                    double deceleration_length = 0;


                    if (oriEndSpeed < arrivalVelocity)
                        deceleration_length = Physics.GetLength_v_v0_a(oriEndSpeed, maxSpeed, csc.Deceleration);

                    //등속 구간이 존재하는지 확인

                    double const_velocity_length = 0;
                    if (maxSpeed == 0)
                        return;
                    double common_max_speed = maxSpeed;
                    double acceleration_time = 0;
                    //등속도 구간 길이가 0이상인 경우
                    if (Math.Round(acceleration_length + deceleration_length, 3) <= Math.Round(diffLength, 3))
                    {
                        const_velocity_length = diffLength - (acceleration_length + deceleration_length); //Math.Round(diffLength, 3) - Math.Round(acceleration_length + deceleration_length, 3);
                    }//등속도 구간 길이가 0미만인 경우... 가속 감속 합의점 찾아야 하는 경우
                    else
                    {
                        double vA = lstEndSpeed.Count == 0 ? endSpeed : lstEndSpeed[lstEndSpeed.Count - 1];

                        double diffLen = diffLength;
                        double startVel = startSpeed;
                        acceleration_length = 0;

                        for (int i = 0; i < lstAccelerate.Count; i++)
                        {
                            double tmp = Math.Sqrt((2 * diffLen * lstAccelerate[i] * csc.Deceleration + csc.Deceleration * startVel * startVel - lstAccelerate[i] * endSpeed * endSpeed) / (csc.Deceleration - lstAccelerate[i]));
                            if (vA >= tmp)
                            {
                                vA = tmp;
                                acceleration_length = 0;
                                acceleration_time = 0;
                                for (int j = 0; j < i; j++)
                                {
                                    acceleration_length += lstLen[j];
                                    acceleration_time += Physics.GetTime_v_v0_a(lstEndSpeed[j], startVel, lstAccelerate[j]);
                                }


                                acceleration_length += Physics.GetLength_v_v0_a(vA, startVel, lstAccelerate[i]);
                                acceleration_time += Physics.GetTime_v_v0_a(vA, startVel, lstAccelerate[i]);
                            }

                            startVel = lstEndSpeed[i];
                            diffLen -= lstLen[i];
                        }

                        //double vA = Math.Sqrt((2 * diffLength * csc.CSCAcceleration * csc.Deceleration + csc.Deceleration * startSpeed * startSpeed - csc.CSCAcceleration * endSpeed * endSpeed) / (csc.Deceleration - csc.CSCAcceleration));
                        //추후 식 다시 확인해보자.!
                        //acceleration_length = getLength_v_v0_a(vA, startSpeed, csc.CSCAcceleration);
                        deceleration_length = diffLength - acceleration_length;

                        common_max_speed = vA;// getVelocity_v0_a_s(startSpeed, csc.CSCAcceleration, acceleration_length);
                    }

                    {

                        //감속 1차원 그래프


                        double vel1 = startSpeed;
                        Time t1 = startTime;
                        for (int i = 0; i < lstAccelerate.Count; i++)
                        {
                            double a = lstAccelerate[i];
                            Time t2 = Physics.GetTime_v_v0_a(lstEndSpeed[i], vel1, lstAccelerate[i]);
                            double b = vel1 - a * t1.TotalSeconds;

                            // y = ax + b    : x (t1 ~ t2)
                            // t1 * a + b <=   



                            vel1 = lstEndSpeed[i];
                        }
                    }



                    double const_velocity_time = const_velocity_length / common_max_speed;
                    //acceleration_time = 0;// getTime_v_v0_a(common_max_speed, startSpeed, csc.CSCAcceleration);getTime

                    //startV = startSpeed;
                    //for (int i = 0; i < lstLen.Count; i++)
                    //{
                    //    acceleration_time += getTimeByLength(startV, csc.CSCAccelerationBySpeed(startV), lstLen[i]);
                    //    startV = lstEndSpeed[i];
                    //}

                    double deceleration_time = Physics.GetTime_v_v0_a(endSpeed, common_max_speed, csc.Deceleration);

                    double inputEndTime = (double)startTime;
                    double inputEndPos = startPos;

                    startV = startSpeed;
                    startP = startPos;
                    double startT = (double)startTime;





                    //가속             
                    if (acceleration_length > 0.000001)
                    {
                        if (common_max_speed < 0.0001 || double.IsNaN(common_max_speed))
                            common_max_speed = 0;

                        for (int i = 0; i < lstAccelerate.Count; i++)
                        {

                            double tp = Math.Round(Physics.GetLength_v_v0_a(lstEndSpeed[i], startV, lstAccelerate[i]), 10);


                            if (acceleration_length >= tp)
                            {
                                double t = Physics.GetTimeByLength(startV, lstAccelerate[i], tp);

                                inputEndTime += t;
                                inputEndPos += tp;
                                lstRef.Add(new PosData(lstAccelerate[i], startV, startT, startP, lstEndSpeed[i], inputEndTime, inputEndPos));
                                if (Math.Round(lstRef.Last()._endPos, 0) > Math.Round(cutPos, 0))
                                {
                                    lstRef = cutListPosDataByPos(lstRef, cutPos);
                                    return;
                                }
                                else if (Math.Round(lstRef.Last()._endPos, 0) == Math.Round(cutPos, 0))
                                    return;

                                acceleration_length -= tp;
                                startP = inputEndPos;
                                startV = lstRef[lstRef.Count - 1]._endSpeed;//lstEndSpeed[i];
                                startT = inputEndTime;
                            }
                            else
                            {
                                double t = Physics.GetTimeByLength(startV, lstAccelerate[i], acceleration_length);
                                inputEndTime += t;
                                inputEndPos += acceleration_length;
                                double v = Physics.GetVelocity_v0_a_t(startV, lstAccelerate[i], t);
                                lstRef.Add(new PosData(lstAccelerate[i], startV, startT, startP, v, inputEndTime, inputEndPos));
                                if (Math.Round(lstRef.Last()._endPos, 0) > Math.Round(cutPos, 0))
                                {
                                    lstRef = cutListPosDataByPos(lstRef, cutPos);
                                    return;
                                }
                                else if (Math.Round(lstRef.Last()._endPos, 0) == Math.Round(cutPos, 0))
                                    return;

                                startP = inputEndPos;
                                startV = lstRef[lstRef.Count - 1]._endSpeed;//lstEndSpeed[i];
                                startT = inputEndTime;
                                break;
                            }
                        }
                    }

                    //등속
                    if (const_velocity_length > 0.000001)
                    {
                        inputEndTime += const_velocity_time;
                        inputEndPos += const_velocity_length;
                        lstRef.Add(new PosData(0, common_max_speed, startT, startP, common_max_speed, inputEndTime, inputEndPos));
                        if (Math.Round(lstRef.Last()._endPos, 0) > Math.Round(cutPos, 0))
                        {
                            lstRef = cutListPosDataByPos(lstRef, cutPos);
                            return;
                        }
                        else if (Math.Round(lstRef.Last()._endPos, 0) == Math.Round(cutPos, 0))
                            return;

                        startP = inputEndPos;
                        startV = common_max_speed;
                        startT = inputEndTime;
                    }

                    //감속
                    if (deceleration_length > 0.000001)
                    {

                        inputEndTime += deceleration_time;
                        inputEndPos += deceleration_length;
                        lstRef.Add(new PosData(csc.Deceleration, startV, startT, startP, endSpeed, inputEndTime, inputEndPos));
                        if (Math.Round(lstRef.Last()._endPos, 0) > Math.Round(cutPos, 0))
                        {
                            lstRef = cutListPosDataByPos(lstRef, cutPos);
                            return;
                        }
                        else if (Math.Round(lstRef.Last()._endPos, 0) == Math.Round(cutPos, 0))
                            return;
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public EvtData FinishScheduling(Time curTime, List<PosData> lst, double destPos, Time lastTime, List<PosData> lstOpd, double totalMaxSpeed)
        {
            if (lst.Count == 0)
                return new EvtData(false, -1);

            double maximumScheduleLength = Line.GetDistanceAtTime(this, curTime) + Line.MaximumScheduleDistance;
            List<PosData> plst = new List<PosData>();
            if (Math.Round(lst.Last()._endPos, 0) > Math.Round(maximumScheduleLength, 0))
            {
                plst = lst.ToList();
                lst = cutListPosDataByPos(lst, maximumScheduleLength);
            }

            if (lst.Count == 0)
                return new EvtData(false, -1);

            bool isArrive = false;

            if (lst.Count == 0)
            {
                lst = cutListPosDataByPos(plst, maximumScheduleLength);
            }
            //도착지에 도착 한 경우
            if (Math.Round(lst.Last()._endPos, 0) >= Math.Round(destPos, 0))
                isArrive = true;

            List<PosData> lstSum = new List<PosData>();

            if (lst.Count == 1)
                lstSum = lst;
            else
            {
                for (int i = 0; i < lst.Count - 1;)
                {
                    PosData opdStd = lst[i];
                    if (Math.Round(opdStd._endPos, 0) >= Math.Round(destPos, 0))
                        opdStd._endPos = destPos;

                    PosData opdNew = new PosData(opdStd._celerate, opdStd._startSpeed, opdStd._startTime, opdStd._startPos, opdStd._endSpeed, opdStd._endTime, opdStd._endPos);

                    for (int j = i + 1; j < lst.Count; j++)
                    {
                        PosData opdComp = lst[j];
                        if (Math.Round(opdComp._endPos, 0) >= Math.Round(destPos, 0))
                            opdComp._endPos = destPos;

                        if (opdStd._celerate == opdComp._celerate)
                        {
                            opdNew._endPos = opdComp._endPos;
                            opdNew._endSpeed = opdComp._endSpeed;
                            opdNew._endTime = opdComp._endTime;
                        }
                        else
                        {
                            lstSum.Add(opdNew);
                            i = j;
                        }

                        if (j == lst.Count - 1)
                        {
                            if (opdStd._celerate == opdComp._celerate)
                            {
                                lstSum.Add(opdNew);
                                i = j;
                            }
                            else
                            {
                                lstSum.Add(opdComp);
                                i = j;
                            }
                        }

                        if (i == j)
                            break;
                    }
                }
            }

            for (int i = 0; i < lstSum.Count; i++)
            {
                if (lstSum[i]._startTime > lstSum[i]._endTime)
                {
                    lstSum.RemoveRange(i, lstSum.Count - i);
                    break;
                }
                else if (i < lstSum.Count - 1 && Route[Route.Count - 1].Name == Name && lstSum[i]._endPos == Route[Route.Count - 1].GetStationLength(Destination) && lstSum[i]._endSpeed == 0)
                {
                    lstSum.RemoveRange(i + 1, lstSum.Count - (i + 1));
                    break;
                }
            }

            bool everyThingIsSame = true;
            for (int i = 0; i < lstSum.Count; i++)
            {
                if (Math.Round(lstSum[i]._startTime.TotalSeconds, 13) == Math.Round(lstSum[i]._endTime.TotalSeconds, 13) && Math.Round(lstSum[i]._startPos, 9) == Math.Round(lstSum[i]._endPos, 9))
                    continue;

                everyThingIsSame = false;

                PosDatas.Add(lstSum[i]);
            }

            if (everyThingIsSame)
                return new EvtData(false, -1);

            Time inputEndTime = lastTime;
            if (lstOpd.Count > 0)
                inputEndTime = lstOpd[lstOpd.Count - 1]._endTime;

            return new EvtData(isArrive, inputEndTime);
        }

        public EvtData GeneratePosData(Time curTime, double destPos)
        {
            double lastPos = 0;
            Time lastTime = Time.MinValue;
            double lastSpeed = 0;
            Line.GetLastSchedule(Line.LstObjectIndex[this.ID], curTime, ref lastPos, ref lastTime, ref lastSpeed);
            double totalMaxSpeed = GetMaxSpeed(Line, this);

            List<PosData> lstOpd = PosDatas;

            if (lastTime < curTime) //이상상황
                lastTime = curTime; // 없어야 할 상황.!! 확인 후 삭제방향으로..

            if (lastPos == destPos)
                return FinishScheduling(curTime, new List<PosData>(), destPos, lastTime, lstOpd, totalMaxSpeed);

            //-----------------------------------------------------------------    

            List<List<PosData>> lstlstPreOpd = new List<List<PosData>>();
            List<double> lstLengthForToLine = new List<double>();
            List<double> lstPreCscStartPosition = new List<double>();
            List<TransportPoint> visitedPoints = new List<TransportPoint>();
            List<bool> lstPreCSCStoped = new List<bool>();
            double searchStopLength = MaxFollowControlLength;

            int cscIdx = Line.LstObjectIndex[this.ID];
            //라인의 가장 뒤쪽에 있는 경우 다음 라인의 CSC 상황을 파악하기 위한 정보를 수집. 내 경로가 아닌 갈라지는 라인에 대해서도 고려(충돌 방지).
            if (cscIdx == 0)
                FindFrontObject(Line.EndPoint, MinimumDistance, Route, Line.Length, searchStopLength, ref visitedPoints, ref lstlstPreOpd, ref lstLengthForToLine, ref lstPreCscStartPosition, ref lstPreCSCStoped);
            else//라인의 가장 뒤쪽이 아닌 경우
            {
                lstlstPreOpd.Add(Line.LstPosData[cscIdx - 1]);
                lstPreCscStartPosition.Add(Line.LstStartPos[cscIdx - 1]);
                lstLengthForToLine.Add(0);
                lstPreCSCStoped.Add(((CSC)Line.Vehicles[cscIdx - 1]).IsStoped);
            }

            //lstlstPreOpd가 3개 이상일 경우 제일 먼경우 제거
            if (lstlstPreOpd.Count >= 3)
            {
                double val = 0;
                int ix = 0;
                for (int i = 0; i < lstLengthForToLine.Count; i++)
                {
                    double len = lstLengthForToLine[i];
                    for (int j = 0; j < lstlstPreOpd[i].Count; j++)
                    {
                        if (lstlstPreOpd[i][j]._startTime <= curTime && lstlstPreOpd[i][j]._endTime >= curTime)
                        {
                            len = lstLengthForToLine[i] + lstlstPreOpd[i][j]._startPos + Physics.GetLength_v0_a_t(lstlstPreOpd[i][j]._startSpeed, lstlstPreOpd[i][j]._celerate, (double)(curTime - lstlstPreOpd[i][j]._startTime));
                            break;
                        }
                        else if (lstlstPreOpd[i][j]._startTime > curTime)
                            ;
                        else
                            len = lstLengthForToLine[i] + lstlstPreOpd[i][j]._endPos;
                    }

                    if (val < len)
                    {
                        val = len;
                        ix = i;
                    }
                }

                lstPreCSCStoped.RemoveAt(ix);
                lstlstPreOpd.RemoveAt(ix);
                lstLengthForToLine.RemoveAt(ix);
                lstPreCscStartPosition.RemoveAt(ix);
            }

            //목적지 및 라인 최대속도 기준으로 도착 속도 결정
            double endSpeed = CalculateEndSpeedCurAtDest(this, destPos);

            //예약지점까지만 계획하기 위해서 CutPos 결정, 예약 지점 이후인 경우 예약 우선순위에 의해 endSpeed 결정
            double cutPos = destPos;

            //ZCU를 고려한 도착 속도 결정
            GetEndSpeedNCutPos(this, lastPos, ref endSpeed, ref cutPos);

            // lstPreOpd == null : 앞에 csc 없는 경우(다음라인, 다다음라인 포함)           
            if (lstlstPreOpd.Count == 0)
            {
                //스케줄링!!!!  
                List<PosData> lst = new List<PosData>();

                AddPosdata_simlogs(totalMaxSpeed, this, lastTime, lastSpeed, lastPos, endSpeed, destPos, cutPos, ref lst);
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i]._startTime > lst[i]._endTime)
                    {
                        lst.RemoveRange(i, lst.Count - i); //error!!!
                    }
                }

                return FinishScheduling(curTime, lst, destPos, lastTime, lstOpd, totalMaxSpeed);
            }
            //고려해야할 앞 csc가 존재하는 경우(고려해야할 csc가 다음라인 혹인 다다음 라인에 존재할 수 있음)
            else
            {
                List<PosData> lstBetter = null;
                //라스트 시점에서 이미 끝나버린 opd 무시

                Time finalLastTime = lastTime;

                for (int i = 0; i < lstlstPreOpd.Count; i++)
                {
                    List<PosData> lstPreOpd = lstlstPreOpd[i];
                    double lengthForToLine = lstLengthForToLine[i];
                    double preCscStartPosition = lstPreCscStartPosition[i];
                    List<PosData> lst = new List<PosData>();

                    Time tempLastTime = lastTime;
                    double tempLastPos = lastPos;
                    double tempLastSpeed = lastSpeed;

                    double nextCSCPos = lengthForToLine + preCscStartPosition;
                    double nextCSCVel = 0;
                    bool nextCscStoped = lstPreCSCStoped[i];
                    List<PosData> lstNewPreOpd = new List<PosData>();
                    for (int j = 0; j < lstPreOpd.Count(); j++)
                    {
                        if (tempLastTime >= lstPreOpd[j]._endTime)
                        {
                            nextCSCPos = lengthForToLine + lstPreOpd[j]._endPos;
                            nextCSCVel = lstPreOpd[j]._endSpeed;
                        }

                        else
                            lstNewPreOpd.Add(lstPreOpd[j]);
                    }

                    //앞 csc가 멈춰있고 이동 예약도 없는 경우
                    if (lstNewPreOpd.Count == 0)
                    {
                        //lastTime 부터 curTime까지 && 멈춰있는 CSC 전까지 스케줄링!!!!
                        double betweenLength = lengthForToLine - tempLastPos;
                        double maxStopReadyLength = Physics.GetLength_v_v0_a(0, totalMaxSpeed, Deceleration);
                        double maxStopReadyPos = nextCSCPos - IntervalLength - maxStopReadyLength;

                        if (maxStopReadyPos > cutPos)
                            maxStopReadyPos = cutPos;

                        AddPosdata_simlogs_limitTime(totalMaxSpeed, this, tempLastTime, tempLastSpeed, tempLastPos, nextCSCPos, Physics.GetLength_v_v0_a(0, nextCSCVel, Deceleration), double.MaxValue, destPos, endSpeed, cutPos, ref lst);

                        if (lst.Count > 0)
                        {
                            double stopReadyLength = Physics.GetLength_v_v0_a(0, lst.Last()._endSpeed, Deceleration);
                            double stopReadyPos = nextCSCPos - IntervalLength - stopReadyLength;

                            if (stopReadyPos > cutPos)
                                stopReadyPos = cutPos;

                            if (Math.Round(lst.Last()._endPos, 0) > Math.Round(stopReadyPos, 0))
                                lst = cutListPosDataByPos(lst, stopReadyPos);
                        }

                        //멈춰있어야하는 경우를 짜준다. 
                        if (lst.Count > 0
                            && lst.Last()._endSpeed == 0
                            && curTime > lst.Last()._endTime)
                            lst.Add(new PosData(0, 0, lst.Last()._endTime, lst.Last()._endPos, 0, curTime + 0.1, lst.Last()._endPos));
                        else if (lst.Count == 0 && Math.Round(nextCSCPos - lastPos, 0) == IntervalLength
                            && lastSpeed == 0
                            && curTime > lastTime)
                            lst.Add(new PosData(0, 0, lastTime, lastPos, 0, curTime + 0.1, lastPos));
                    }
                    else
                    {
                        PosData lastPreOpd = lstNewPreOpd.Last();
                        double reservationPoint = Line.GetEndPointZCUReservationPos(Deceleration);

                        //앞 csc가 멈춰있고 이동 전까지 
                        if (tempLastTime < lstNewPreOpd[0]._startTime)
                        {
                            AddPosdata_simlogs_limitTime(totalMaxSpeed, this, tempLastTime, tempLastSpeed, tempLastPos, nextCSCPos, 0, (double)lstNewPreOpd[0]._startTime, destPos, endSpeed, cutPos, ref lst);

                            if (lst.Count > 0)
                            {
                                tempLastTime = lst[lst.Count - 1]._endTime;
                                tempLastSpeed = lst[lst.Count - 1]._endSpeed;
                                tempLastPos = lst[lst.Count - 1]._endPos;
                                nextCSCPos = lengthForToLine + lstNewPreOpd[0]._endPos;
                                nextCSCVel = lstNewPreOpd[0]._endSpeed;
                            }
                        }

                        //앞 csc의 스케줄을 돌며 destpos 혹은 미래 스케줄 끝날때까지 스케줄링!!
                        for (int j = 0; j < lstNewPreOpd.Count; j++)
                        {
                            if (tempLastTime >= lstNewPreOpd[j]._startTime)//앞CSC 스케줄의 시작시간보다 지금시간이 늦을 경우
                            {
                                AddPosdata_simlogs_Opd(totalMaxSpeed, this, tempLastTime, tempLastSpeed, tempLastPos, destPos, lstNewPreOpd[j], lengthForToLine, endSpeed, cutPos, ref lst);
                                if (lst.Count > 0)
                                {
                                    tempLastTime = lst[lst.Count - 1]._endTime;
                                    tempLastSpeed = lst[lst.Count - 1]._endSpeed;
                                    tempLastPos = lst[lst.Count - 1]._endPos;
                                    nextCSCPos = lengthForToLine + lstNewPreOpd[j]._endPos;
                                    nextCSCVel = lstNewPreOpd[j]._endSpeed;
                                }
                            }

                            if (j < lstNewPreOpd.Count - 1) //다음 스케줄링할 opd 남아 있는 경우
                            {
                                if (lst.Count > 0)
                                {
                                    tempLastTime = lst[lst.Count - 1]._endTime;
                                    tempLastSpeed = lst[lst.Count - 1]._endSpeed;
                                    tempLastPos = lst[lst.Count - 1]._endPos;
                                    nextCSCPos = lengthForToLine + lstNewPreOpd[j]._endPos;
                                    nextCSCVel = lstNewPreOpd[j]._endSpeed;
                                }

                                if (tempLastTime < lstNewPreOpd[j + 1]._startTime)
                                {
                                    AddPosdata_simlogs_limitTime(totalMaxSpeed, this, tempLastTime, tempLastSpeed, tempLastPos, nextCSCPos, Physics.GetLength_v_v0_a(0, nextCSCVel, Deceleration), (double)lstNewPreOpd[j + 1]._startTime, destPos, endSpeed, cutPos, ref lst);

                                    if (lst.Count > 0)
                                    {
                                        tempLastTime = lst[lst.Count - 1]._endTime;
                                        tempLastSpeed = lst[lst.Count - 1]._endSpeed;
                                        tempLastPos = lst[lst.Count - 1]._endPos;
                                        nextCSCPos = lengthForToLine + lstNewPreOpd[j + 1]._endPos;
                                    }
                                }
                            }
                        }

                        double maxStopReadyLength = 0;
                        if(Deceleration > 0)
                            maxStopReadyLength = Physics.GetLength_v_v0_a(0, totalMaxSpeed, Deceleration);
                        double maxStopReadyPos = nextCSCPos - IntervalLength - maxStopReadyLength;

                        if (maxStopReadyPos > cutPos)
                            maxStopReadyPos = cutPos;

                        double lastScheduleLength = 0;
                        if (lst.Count() > 0)
                            lastScheduleLength = lst.Last()._endPos;

                        if (lastScheduleLength < maxStopReadyPos || (destPos == cutPos && endSpeed == 0))
                        {
                            AddPosdata_simlogs_limitTime(totalMaxSpeed, this, tempLastTime, tempLastSpeed, tempLastPos, nextCSCPos, Physics.GetLength_v_v0_a(0, nextCSCVel, Deceleration), double.MaxValue, destPos, endSpeed, cutPos, ref lst);

                            if (lst.Count() > 0)
                            {
                                double stopReadyLength = Physics.GetLength_v_v0_a(0, lst.Last()._endSpeed, Deceleration);
                                double stopReadyPos = nextCSCPos - IntervalLength - stopReadyLength;

                                if (stopReadyPos > cutPos)
                                    stopReadyPos = cutPos;

                                if (Math.Round(lst.Last()._endPos, 0) > Math.Round(stopReadyPos, 0))
                                    lst = cutListPosDataByPos(lst, stopReadyPos);
                            }
                        }

                        //멈춰있어야하는 경우를 짜준다. 
                        if (lst.Count > 0 && Math.Round(nextCSCPos - lst.Last()._endPos, 0) == IntervalLength
                            && lstNewPreOpd.Last()._endSpeed == 0
                            && lst.Last()._endSpeed == 0
                            && lstNewPreOpd.Last()._endTime > lst.Last()._endTime)
                            lst.Add(new PosData(0, 0, lst.Last()._endTime, lst.Last()._endPos, 0, lstNewPreOpd.Last()._endTime + 0.1, lst.Last()._endPos));
                        else if (lst.Count == 0 && Math.Round(nextCSCPos - lastPos, 0) == IntervalLength
                            && lstNewPreOpd.Last()._endSpeed == 0
                            && lastSpeed == 0
                            && lstNewPreOpd.Last()._endTime > lastTime)
                            lst.Add(new PosData(0, 0, lastTime, lastPos, 0, lstNewPreOpd.Last()._endTime + 0.1, lastPos));
                    }

                    if (i == 0)
                    {
                        lstBetter = lst;
                        finalLastTime = lastTime;
                    }
                    else
                    {
                        double mp = lstLengthForToLine[0], np = lstLengthForToLine[1];
                        for (int m = 0; m < lstlstPreOpd[0].Count(); m++)
                        {
                            if (lstlstPreOpd[0][m]._startTime <= curTime && lstlstPreOpd[0][m]._endTime >= curTime)
                            {
                                mp = lstLengthForToLine[0] + lstlstPreOpd[0][m]._startPos + Physics.GetLength_v0_a_t(lstlstPreOpd[0][m]._startSpeed, lstlstPreOpd[0][m]._celerate, (double)(curTime - lstlstPreOpd[0][m]._startTime));
                                break;
                            }
                            else if (lstlstPreOpd[0][m]._startTime > curTime)
                                ;
                            else
                                mp = lstLengthForToLine[0] + lstlstPreOpd[0][m]._endPos;
                        }

                        for (int n = 0; n < lstlstPreOpd[1].Count(); n++)
                        {
                            if (lstlstPreOpd[1][n]._startTime <= curTime && lstlstPreOpd[1][n]._endTime >= curTime)
                            {
                                np = lstLengthForToLine[1] + lstlstPreOpd[1][n]._startPos + Physics.GetLength_v0_a_t(lstlstPreOpd[1][n]._startSpeed, lstlstPreOpd[1][n]._celerate, (double)(curTime - lstlstPreOpd[1][n]._startTime));
                                break;
                            }
                            else if (lstlstPreOpd[1][n]._startTime > curTime)
                                ;
                            else
                                np = lstLengthForToLine[1] + lstlstPreOpd[1][n]._endPos;
                        }

                        if (mp > np)
                        {
                            lstBetter = lst;
                            finalLastTime = lastTime;
                            break;
                        }
                    }
                }

                for (int i = 0; i < lstBetter.Count; i++)
                {
                    if (lstBetter[i]._startTime > lstBetter[i]._endTime)
                    {
                        lstBetter.RemoveRange(i, lstBetter.Count - i);
                    }
                }

                return FinishScheduling(curTime, lstBetter, destPos, finalLastTime, lstOpd, totalMaxSpeed);
            }
        }

        public EvtData GenerateSimpledPosData(Time simTime, double destPos)
        {
            Time endTime = simTime;
            double startPos;
            double startSpeed;
            int index = Line.LstObjectIndex[this.ID];
            if (PosDatas.Count == 0)
            {
                int idx = Line.LstObjectIndex[this.ID];
                startPos = Line.LstStartPos[idx];
                startSpeed = Line.MaxSpeed;
            }
            else
            {
                if (simTime < PosDatas[0]._endTime)
                    startPos = PosDatas[0]._startPos + (simTime - PosDatas[0]._startTime).TotalSeconds * Line.MaxSpeed;
                else
                    startPos = PosDatas.Last()._endPos;
            }

            double moveLength = 0;

            if (index > 0)
            {
                // 현재 라인의 앞에 누가 있음

                if (Line.LstPosData[index - 1].Count == 0)
                {
//                    ErrorLogger.SaveLog("앞에 누가있는데 멈춰 있음");
                    // 앞에 누가있는데 멈춰 있음
                }
                else
                {
                    PosData frontPosData = Line.LstPosData[index - 1].LastOrDefault();
                    moveLength = (frontPosData._endPos - this.Size.X - _minimumDistance) - startPos;
                }
            }
            else
            {
                // 현재 라인의 앞에 누가 없을 경우 셋팅. 이후 진행방향 앞쪽 라인 확인
                moveLength = Line.Length - startPos;

                foreach (TransportLine toLine in Line.EndPoint.OutLines)
                {
                    if (toLine.EnteredObjects.Count > 0)
                    {
                        //진행경로가 아닌데 intervalLength보다 더 가는 계획이 있으면 경로짜는 csc가 지나가면서 충돌에 영향을 주지않는다고 판단하고 고려하지 않음.
                        if (!Route.Contains(toLine)
                            &&
                            ((toLine.LstPosData[toLine.EnteredObjects.Count - 1].Count > 0 && toLine.LstPosData[toLine.EnteredObjects.Count - 1].Last()._endPos > MinimumDistance)
                            || (toLine.LstPosData[toLine.EnteredObjects.Count - 1].Count == 0 && toLine.LstStartPos[toLine.EnteredObjects.Count - 1] > MinimumDistance))
                            )
                            continue;
                        else
                            ;
                        PosData frontPosData = toLine.LstPosData[toLine.EnteredObjects.Count - 1].LastOrDefault();

                        moveLength = (frontPosData._endPos - this.Size.X - _minimumDistance);
                        if (moveLength <= 0)
                        {
                            moveLength = CalPositionAtTime(toLine, startPos, simTime);
                        }
                        else
                            moveLength += (Line.Length - startPos);
                        break;
                    }
                }
            }

            if (moveLength > 0)
            {
                if (startPos + moveLength > destPos)
                    moveLength = destPos - startPos;

                if (moveLength > _dispatchingDistance)
                    moveLength = _dispatchingDistance;

                endTime = simTime + moveLength / Line.MaxSpeed;
                PosData posData = new PosData(0, Line.MaxSpeed, simTime, endTime, startPos, startPos + moveLength);
                PosDatas.Add(posData);
            }
            else
                return new EvtData(false, 0);

            bool isArrive = false;
            if (Math.Round(PosDatas.Last()._endPos, 0) >= Math.Round(destPos, 0))
                isArrive = true;

            return new EvtData(isArrive, PosDatas.Last()._endTime);
        }
        private double CalPositionAtTime(TransportLine toLine, double startPos, Time simTime)
        {
            PosData frontPosData = toLine.LstPosData[toLine.EnteredObjects.Count - 1].LastOrDefault();

            if (frontPosData._endTime == 0)
                return Line.Length - startPos;

            double vehiclePosition = frontPosData._startPos;

            if (Math.Round((double)frontPosData._startTime, 2) == Math.Round((double)simTime, 2))
                vehiclePosition = frontPosData._startPos;
            else if (Math.Round((double)frontPosData._endTime, 2) >= Math.Round((double)simTime, 2))
                vehiclePosition = frontPosData._endPos;
            else
                vehiclePosition = frontPosData._startPos + Physics.GetLength_v0_a_t(frontPosData._startSpeed, frontPosData._celerate, (double)(simTime - frontPosData._startTime));
            
            vehiclePosition = Math.Min(vehiclePosition, frontPosData._endPos);
            
            double betweenAandB = vehiclePosition + (Line.Length - startPos);

            double moveLength = betweenAandB - this.Size.X - _minimumDistance;

            if (Math.Round(betweenAandB) <= this.Size.X + _minimumDistance)
                return 0;

            return moveLength;
        }
        private Time GetPosDataFromStartPointToEndPoint(uint objID, Time simTime, double startPos, PVector3 fromPoint, PVector3 endPoint)
        {
            Time endTime = simTime;
            try
            {
                int index = Line.LstObjectIndex[objID];
                double length = (endPoint - fromPoint).Length() - startPos;
                endTime = simTime + length / Line.MaxSpeed;
                PosData posData = new PosData(0, Line.MaxSpeed, simTime, endTime, startPos, startPos + length);
                Line.LstPosData[index].Add(posData);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return endTime;
        }

        public void FindFrontObject(TransportPoint point, double intervalLength, List<TransportLine> route, double length, double searchStopLength, ref List<TransportPoint> visitedPoints, ref List<List<PosData>> lstlstPreOpd, ref List<double> lstLengthForToLine, ref List<double> lstPreCscStartPosition, ref List<bool> lstPreCscStoped)
        {
            try
            {
                visitedPoints.Add(point);
                foreach (TransportLine toLine in point.OutLines)
                {
                    //다음 라인에 csc 있는 경우
                    if (toLine.EnteredObjects.Count > 0)
                    {
                        //진행경로가 아닌데 intervalLength보다 더 가는 계획이 있으면 경로짜는 csc가 지나가면서 충돌에 영향을 주지않는다고 판단하고 고려하지 않음.
                        if (!route.Contains(toLine)
                            &&
                            ((toLine.LstPosData[toLine.EnteredObjects.Count - 1].Count > 0 && toLine.LstPosData[toLine.EnteredObjects.Count - 1].Last()._endPos > intervalLength)
                            || (toLine.LstPosData[toLine.EnteredObjects.Count - 1].Count == 0 && toLine.LstStartPos[toLine.EnteredObjects.Count - 1] > intervalLength))
                            )
                            continue;

                        lstlstPreOpd.Add(toLine.LstPosData[toLine.EnteredObjects.Count - 1].ToList());
                        lstPreCscStartPosition.Add(toLine.LstStartPos[toLine.EnteredObjects.Count - 1]);
                        lstPreCscStoped.Add(((CSC)toLine.EnteredObjects[toLine.EnteredObjects.Count - 1]).IsStoped);
                        lstLengthForToLine.Add(length);
                    }
                    else
                    {
                        if (route.Contains(toLine) && !visitedPoints.Contains(toLine.EndPoint) && length < searchStopLength)
                            FindFrontObject(toLine.EndPoint, intervalLength, route, length + toLine.Length, searchStopLength, ref visitedPoints, ref lstlstPreOpd, ref lstLengthForToLine, ref lstPreCscStartPosition, ref lstPreCscStoped);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        //목적지 및 라인 최대속도 기준 EndSpeed 출력
        public double CalculateEndSpeedCurAtDest(CSC csc, double destPos)
        {
            double endSpeed = 0; //현재 railLineNode에 목적지 있는 경우

            //목적지에서 속도 0인 경우
            if (destPos < Line.Length || (destPos == Line.Length && csc.Route.Count == 1))
                endSpeed = 0;
            else if (csc.Route.Count > 1) //현재 railLineNode에 목적지 없는 경우
            {
                CSCLine railLine = csc.Route[csc.Route.Count - 1] as CSCLine;

                //마지막 레일의 스펙 최대 속도
                double destMaxSpeed = GetMaxSpeed(railLine, csc);

                //마지막 레일의 시작점일 때 최대 속도
                endSpeed = Physics.GetVelocity0_v_a_s(0, csc.Deceleration, railLine.GetStationLength(csc.Destination));

                //스펙까지 고려한 마지막 레일의 시작점일 때 최대 속도
                if (endSpeed > destMaxSpeed)
                    endSpeed = destMaxSpeed;

                //마지막 레일부터 현재 레일까지 거꾸로 계산해서 최대 속도 도출 
                for (int i = csc.Route.Count - 2; i >= 1; i--)
                {
                    railLine = csc.Route[i] as CSCLine;

                    endSpeed = Physics.GetVelocity0_v_a_s(endSpeed, csc.Deceleration, railLine.Length);

                    destMaxSpeed = GetMaxSpeed(railLine, csc);

                    if (endSpeed > destMaxSpeed)
                        endSpeed = destMaxSpeed;
                }

                if (Line.MaxSpeed < endSpeed)
                    endSpeed = Line.MaxSpeed;
            }

            return endSpeed;
        }

        public bool AddStoppedPosData()
        {
            double lastLength = 0;
            Time lastTime = Time.MinValue;
            if (PosDatas.Count == 0)
            {
                lastLength = StartPos;
                lastTime = StartTime;
            }
            else
            {
                lastLength = PosDatas.Last()._endPos;
                lastTime = PosDatas.Last()._endTime;
            }

            if (((CSCPoint)Line.EndPoint).Zcu != null && ((CSCPoint)Line.EndPoint).ZcuType == ZCU_TYPE.STOP && !((CSCPoint)Line.EndPoint).Zcu.IsAvailableToEnter(this)) //ZCU 관련 Schedule 생성
            {
                Time zcuFrontCSCScheduleTime = ((CSCPoint)Line.EndPoint).Zcu.GetScheduleTimeOfFrontCSC(this);

                if (zcuFrontCSCScheduleTime != -1 && zcuFrontCSCScheduleTime > lastTime)
                {
                    PosDatas.Add(new PosData(0, 0, lastTime, zcuFrontCSCScheduleTime, lastLength, lastLength));
                    SimPort port = new SimPort(INT_PORT.MOVE, this, Line);
                    EvtCalendar.AddEvent(zcuFrontCSCScheduleTime, Line, port);

                    return true;
                }
                else
                {
                    PosDatas.Add(new PosData(0, 0, lastTime, lastTime + 0.1, lastLength, lastLength));
                    SimPort port = new SimPort(INT_PORT.MOVE, this, Line);
                    EvtCalendar.AddEvent(lastTime + 0.1, Line, port);

                    return false;
                }
            }
            else
            {
                foreach (CSCLine toLine in Line.EndPoint.OutLines)
                {
                    if (toLine.Vehicles.Count == 0)
                        continue;

                    Time frontCSCScheduleTime;
                    double frontCSCSchedulePos;
                    double frontCSCShceduleSpeed;
                    if (toLine.LstPosData.Last().Count == 0)
                    {
                        frontCSCScheduleTime = toLine.LstStartTime.Last();
                        frontCSCSchedulePos = toLine.LstStartPos.Last();
                        frontCSCShceduleSpeed = toLine.LstStartSpeed.Last();
                    }
                    else
                    {
                        frontCSCScheduleTime = toLine.LstPosData.Last().Last()._endTime;
                        frontCSCSchedulePos = toLine.LstPosData.Last().Last()._endPos;
                        frontCSCShceduleSpeed = toLine.LstPosData.Last().Last()._endSpeed;
                    }
                    if (Math.Round(frontCSCSchedulePos + Line.Length - lastLength, 0) == IntervalLength
                        && frontCSCShceduleSpeed == 0
                        && frontCSCScheduleTime > lastTime)
                    {
                        PosDatas.Add(new PosData(0, 0, lastTime, frontCSCScheduleTime, lastLength, lastLength));
                        SimPort port = new SimPort(INT_PORT.MOVE, this, Line);
                        EvtCalendar.AddEvent(frontCSCScheduleTime, Line, port);
                        return true;
                    }
                }
            }

            return false;
        }

        public PosData CutPosDataByTime(PosData opd, double time)
        {
            double v0 = opd._startSpeed;
            double a = opd._celerate;
            double t = time;

            double endPos = v0 * t + 0.5 * a * t * t;
            double endSpeed = v0 + a * t;

            if (double.IsNaN(endSpeed))
                endSpeed = 0;

            return new PosData(a, v0, opd._startTime, opd._startPos, endSpeed, opd._startTime + t, opd._startPos + endPos);
        }
        public PosData CutPosDataByPos(PosData opd, double pos)
        {
            double a = opd._celerate;
            double s = pos - opd._startPos;
            double v0 = opd._startSpeed;
            double v = Math.Sqrt(2 * a * s + v0 * v0);

            if (double.IsNaN(v))
                v = 0;

            double t = 0;
            if (v + v0 > 0)
                t = (2 * s) / (v + v0);
            else
            {
                t = 0;
                pos = opd._startPos;
            }

            return new PosData(a, v0, opd._startTime, opd._startPos, v, opd._startTime + t, pos);
        }

        public List<PosData> cutListPosDataByTime(List<PosData> lstOpd, double cutTime)
        {
            List<PosData> returnList = new List<PosData>();
            for (int i = 0; i < lstOpd.Count; i++)
            {
                if (lstOpd[i]._endTime <= cutTime)
                    returnList.Add(lstOpd[i]);
                else
                {
                    if (lstOpd[i]._startTime < cutTime)
                    {
                        returnList.Add(CutPosDataByTime(lstOpd[i], (double)(cutTime - lstOpd[i]._startTime)));
                        break;
                    }
                }
            }
            return returnList;
        }
        public List<PosData> cutListPosDataByPos(List<PosData> lstOpd, double cutPos)
        {
            List<PosData> returnList = new List<PosData>();
            for (int i = 0; i < lstOpd.Count; i++)
            {
                if (Math.Round(lstOpd[i]._endPos, 3) <= Math.Round(cutPos, 3))
                    returnList.Add(lstOpd[i]);
                else
                {
                    if (lstOpd[i]._startPos < cutPos)
                    {
                        returnList.Add(CutPosDataByPos(lstOpd[i], cutPos));
                        break;
                    }
                    else
                        break;
                }
            }
            return returnList;
        }

        public void CutListPosDataByPos(List<PosData> lstOpd, double cutPos)
        {
            for (int i = 0; i < lstOpd.Count; i++)
            {
                if (lstOpd[i]._startPos < cutPos)
                {
                    lstOpd[i] = CutPosDataByPos(lstOpd[i], cutPos);
                    break;
                }
                else
                    break;
            }
        }
    }
    #endregion
}
