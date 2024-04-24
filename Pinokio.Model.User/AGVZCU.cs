using Logger;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pinokio.Model.User
{
    /// <summary>
    /// ZCU의 기능
    /// Stop, Reset Point가 모두 같으면 후발 AGV도 같이 지나갈 수 있다.
    /// Stop은 같은데 Reset은 다르면 후발 AGV도 같이 지나갈 수 있다.
    /// Stop도 다르고 Reset도 다르면 후발 AGV도 같이 지나갈 수 있다.
    /// Stop은 다르고 Reset은 같으면 후발 AGV가 Stop Point에서 기다렸다가 선발 AGV가 나가면 들어감.
    /// </summary>
    public class AGVZCU
    {
        private string _name;
        private Dictionary<string, AGVPoint> _fromPoints; // Key: stopPointName
        private Dictionary<string, AGVPoint> _toPoints; // Key: resetPointName
        private List<AGVLine> _lines;
        private List<AGV> _agvs;
        private Dictionary<Tuple<string, string>, List<AGVZCUReservation>> _reservations; //Key: stopPointName, resetPointName
        private Dictionary<Tuple<string, string>, List<AGVZCUReservation>> _routeReservations; //Key: stopPointName, resetPointName
        private Dictionary<Tuple<string, string>, List<AGV>> _routeAGVs; //Key: stopPointName, resetPointName
        private List<AGV> _stopAGVs;
        private ACS _acs;

        public string Name
        {
            get { return _name; }
        }

        public ACS Acs
        {
            get { return _acs; }
        }

        public List<AGV> Agvs
        {
            get
            {
                return _agvs;
            }

            set
            {
                _agvs = value;
            }
        }

        public bool IsBusy
        {
            get
            {
                if (_agvs.Count > 0)
                    return true;
                else
                    return false;
            }
        }
        public Dictionary<string, AGVPoint> FromPoints
        {
            get { return _fromPoints; }
        }

        public Dictionary<string, AGVPoint> ToPoints
        {
            get { return _toPoints; }
        }

        public List<AGVLine> Lines
        {
            get { return _lines; }
        }

        public Dictionary<Tuple<string, string>, List<AGVZCUReservation>> Reservations
        {
            get { return _reservations; }
        }

        public Dictionary<Tuple<string, string>, List<AGVZCUReservation>> RouteReservations
        {
            get { return _routeReservations; }
        }

        public Dictionary<Tuple<string, string>, List<AGV>> RouteAGVs
        {
            get { return _routeAGVs; }
            set { _routeAGVs = value; }
        }
        public List<AGV> StopAGVs
        {
            get
            {
                return _stopAGVs;
            }
            set
            {
                _stopAGVs = value;
            }
        }


        public AGVZCU(string name, ACS acs)
        {
            _name = name;
            _acs = acs;
            _agvs = new List<AGV>();
            _fromPoints = new Dictionary<string, AGVPoint>();
            _toPoints = new Dictionary<string, AGVPoint>();
            _lines = new List<AGVLine>();
            _reservations = new Dictionary<Tuple<string, string>, List<AGVZCUReservation>>();
            _routeReservations = new Dictionary<Tuple<string, string>, List<AGVZCUReservation>>();
            _routeAGVs = new Dictionary<Tuple<string, string>, List<AGV>>();
            _stopAGVs = new List<AGV>();
        }

        public void SetLines()
        {
            try
            {
                _lines.Clear();
                _reservations.Clear();
                _routeReservations.Clear();
                _routeAGVs.Clear();

                foreach (AGVPoint inPoint in _fromPoints.Values)
                {
                    List<AGVPoint> railPointNodes = new List<AGVPoint>();
                    List<AGVLine> lines = new List<AGVLine>();
                    InitializeReservationNohtCount(inPoint, railPointNodes, lines, inPoint.Name);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void InitializeReservationNohtCount(AGVPoint currentPoint, List<AGVPoint> visitedPoints, List<AGVLine> zoneOutLines, string stopPointName)
        {

            try
            {
                visitedPoints.Add(currentPoint);
                List<AGVLine> nextLines = currentPoint.OutLines.Where(line => !zoneOutLines.Contains(line)).ToList().ConvertAll(x => (AGVLine)x);

                foreach (AGVLine nextLine in nextLines)
                {
                    Lines.Add(nextLine);
                    nextLine.Zcu = this;

                    foreach (OHTLineStation railPort in nextLine.LineStations)
                    {
                        if (railPort.WaitAllowed)
                        {
                            railPort.WaitAllowed = false;
                            nextLine.DicWaitingRailPort.Remove(railPort);
                        }
                    }
                }
                List<AGVPoint> nextNodes = nextLines.Select(line => line.EndPoint).ToList().ConvertAll(x=>(AGVPoint)x);

                while (nextNodes.Any())
                {
                    AGVPoint nextNode = nextNodes.First();

                    nextNodes.Remove(nextNode);

                    if (nextNode.ZcuName == Name && nextNode.ZcuType == ZCU_TYPE.RESET && nextNode.OutLines.Count > 0)
                    {

                        AGVPoint resetFindingNode = nextNode.OutLines[0].EndPoint as AGVPoint;
                        List<AGVLine> additionalLines = new List<AGVLine>();
                        additionalLines.Add(nextNode.OutLines[0] as AGVLine);
                        while (resetFindingNode.ZcuType != ZCU_TYPE.RESET)
                        {
                            if (resetFindingNode.OutLines.Count == 0)
                                break;
                            resetFindingNode = resetFindingNode.OutLines[0].EndPoint as AGVPoint;
                            if (resetFindingNode.OutLines.Count == 0)
                                break;

                            if (additionalLines.Count > Acs.DicRailLine.Count)
                                break;

                            additionalLines.Add(resetFindingNode.OutLines[0] as AGVLine);
                        }

                        if (!ToPoints.ContainsKey(nextNode.Name))
                            ToPoints.Add(nextNode.Name, nextNode);

                        Tuple<string, string> key = new Tuple<string, string>(stopPointName, nextNode.Name);

                        if (!Reservations.ContainsKey(key))
                        {
                            Reservations.Add(key, new List<AGVZCUReservation>());
                            RouteReservations.Add(key, new List<AGVZCUReservation>());
                            RouteAGVs.Add(key, new List<AGV>());
                            Lines.AddRange(zoneOutLines);

                            foreach (AGVLine line in zoneOutLines)
                            {
                                foreach (OHTLineStation railPort in line.LineStations)
                                {
                                    if (railPort.WaitAllowed)
                                    {
                                        railPort.WaitAllowed = false;
                                        line.DicWaitingRailPort.Remove(railPort);
                                    }
                                }
                            }
                        }
                    }
                    else if (nextNode.ZcuType != ZCU_TYPE.NON && nextNode.ZcuName != Name)
                    {
                        Tuple<string, string> key = new Tuple<string, string>(stopPointName, stopPointName);

                        if (!Reservations.ContainsKey(key))
                        {
                            Reservations.Add(new Tuple<string, string>(stopPointName, stopPointName), new List<AGVZCUReservation>());
                            RouteReservations.Add(new Tuple<string, string>(stopPointName, stopPointName), new List<AGVZCUReservation>());
                            RouteAGVs.Add(new Tuple<string, string>(stopPointName, stopPointName), new List<AGV>());
                        }
                    }
                    else if (!visitedPoints.Contains(nextNode))
                    {
                        InitializeReservationNohtCount(nextNode, visitedPoints, zoneOutLines, stopPointName);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void AddAGV(AGV agv)
        {
            try
            {
                string stopPointName = string.Empty;
                string resetPointName = string.Empty;
                GetStopNResetPoint(agv, ref stopPointName, ref resetPointName);
                Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);

                if (!_routeAGVs[stopNreset].Contains(agv))
                    _routeAGVs[stopNreset].Add(agv);

                agv.CurZcu = this;
                agv.CurZcuResetPointName = resetPointName;
                RemoveReservation(agv);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void RemoveAGV(AGV oht)
        {
            try
            {
                foreach (KeyValuePair<Tuple<string, string>, List<AGV>> kvpRouteAGVList in RouteAGVs)
                {
                    if (kvpRouteAGVList.Value.Contains(oht))
                    {
                        kvpRouteAGVList.Value.Remove(oht);

                        foreach (AGVZCUReservation zcuReservation in RouteReservations[kvpRouteAGVList.Key].ToArray())
                        {
                            if (zcuReservation.AGV == oht)
                                RouteReservations[kvpRouteAGVList.Key].Remove(zcuReservation);
                        }
                    }
                }

                oht.CurZcu = null;
                oht.CurZcuResetPointName = string.Empty;

                RemoveOutAGVs();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void RemoveOutAGVs()
        {
            try
            {
                foreach (List<AGV> routeAGVList in RouteAGVs.Values)
                {
                    int idx = 0;
                    while (idx < routeAGVList.Count)
                    {
                        AGV routeAGV = routeAGVList[idx];

                        if (!this.Lines.Contains(routeAGV.Line))
                            routeAGVList.Remove(routeAGV);
                        else
                            ++idx;
                    }
                }
            }
            catch (Exception ex) {
                ErrorLogger.SaveLog(ex);
            }
        }

        /// Stop, Reset Point가 모두 같고 다른 Stop, 같은 Reset Point인 예약이 없으면 후발 AGV도 같이 지나갈 수 있다.
        /// Stop은 같은데 Reset은 다르면 후발 AGV도 같이 지나갈 수 있다.
        /// Stop도 다르고 Reset도 다르면 후발 AGV도 같이 지나갈 수 있다.
        /// Stop은 다르고 Reset은 같으면 후발 AGV가 Stop Point에서 기다렸다가 선발 AGV가 나가면 들어감.
        /// StopPoint에 도착해서 들어갈 수 있는지 확인하는 함수
        public bool IsAvailableToEnter(AGV agv)
        {
            try
            {
                string stopPointName = string.Empty;
                string resetPointName = string.Empty;
                GetStopNResetPoint(agv, ref stopPointName, ref resetPointName);
                Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);

                if (Acs.DicRailPoint.ContainsKey(stopPointName))
                {
                    GetStopNResetPoint(agv, ref stopPointName, ref resetPointName);
                }
                if (stopPointName == string.Empty)
                    return false;
                AGVPoint stopPoint = Acs.DicRailPoint[stopPointName];
                AGVPoint resetPoint = Acs.DicRailPoint[resetPointName];
                bool isSameRouteAGV = false;

                foreach (KeyValuePair<Tuple<string, string>, List<AGV>> kvpAGVs in _routeAGVs)
                {
                    if (kvpAGVs.Key.Item1 != stopPointName
                        && kvpAGVs.Key.Item2 == resetPointName
                        && kvpAGVs.Value.Count > 0)
                    {
                        return false; //Reset 같은 AGV가 이미 안에 있는 경우 false
                    }
                    else if (kvpAGVs.Key.Item1 == stopPointName
                        && kvpAGVs.Key.Item2 == resetPointName
                        && kvpAGVs.Value.Count > 0)
                    {
                        isSameRouteAGV = true;

                        foreach (AGVLine fromLine in resetPoint.InLines)
                        {
                            if (fromLine.StartPoint.Name == stopPointName)
                                return false; //Stop, reset이 같은데 중간에 via Point가 없으면 단독 진입
                        }
                    }
                }

                AGVZCUReservation ohtReservation = null;

                foreach (AGVZCUReservation reservation in Reservations[stopNreset])
                {
                    if (reservation.AGV.Name == agv.Name)
                        ohtReservation = reservation;
                }

                Time curTime = SimEngine.Instance.TimeNow;

                foreach (KeyValuePair<Tuple<string, string>, List<AGVZCUReservation>> kvpReservation in Reservations)
                {
                    List<AGVZCUReservation> compReservationList = kvpReservation.Value;

                    // Reset 같은 경우
                    if (kvpReservation.Key.Item1 != stopPointName && kvpReservation.Key.Item2 == resetPointName)
                    {
                        AGVPoint compStopPoint = Acs.DicRailPoint[kvpReservation.Key.Item1];

                        if (ohtReservation == null && compReservationList.Count > 0)
                        {
                            return false;
                        }
                        else if (compStopPoint.InLines.Count > 0 && resetPoint.OutLines.Count > 0)
                        {
                            if (compReservationList.Count > 0 && compReservationList[0].ReservationTime < ohtReservation.ReservationTime)
                            {
                                int compAGVSequence = compReservationList[0].AGV.Line.LstObjectIndex[compReservationList[0].AGV.ID];
                                AGV compAGV = compReservationList[0].AGV;
                                List<PosData> compAGVPosData = compAGV.Line.LstPosData[compAGVSequence];

                                int ohtSequence = agv.Line.LstObjectIndex[agv.ID];
                                List<PosData> ohtPosData = agv.Line.LstPosData[ohtSequence];

                                // 나는 이미 Stop Point에 도착했는데, 같은 경로에 먼저 가는 AGV가 없고 다른 경로에서 먼저 예약한 AGV가 현재보다 더 멈춰있는 스케줄이 있을 경우 먼저 지나감.
                                if (ohtPosData.Count > 0 && compAGVPosData.Count > 0 && Math.Round(ohtPosData.Last()._endPos, 0) >= Math.Round(agv.Line.Length, 0) && !isSameRouteAGV
                                    && (!(compAGV.State is VEHICLE_STATE.LOADING || compAGV.State is VEHICLE_STATE.UNLOADING) && compAGVPosData.Last()._endSpeed == 0)
                                    && (compAGVPosData.Last()._endTime > curTime || Math.Round(compAGVPosData.Last()._endPos, 0) < Math.Round(compAGV.Line.Length, 0)))
                                    return true;
                                else //  이외의 모든 상황은 못 지나감.
                                    return false;
                            }
                            else if (compReservationList.Count > 0 && compReservationList[0].ReservationTime == ohtReservation.ReservationTime)
                            {
                                int ohtIndex = Acs.Vehicles.IndexOf(agv);
                                int compOhtIndex = Acs.Vehicles.IndexOf(compReservationList[0].AGV);

                                if (ohtIndex > compOhtIndex)
                                    return false;   // 같은 시간에 예약했다면, AGV ID 번호가 느린쪽이 false;
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
                return false;
            }
        }

        public bool KeepGoing(AGV agv)
        {
            try
            {
                string stopPointName = string.Empty;
                string resetPointName = string.Empty;
                GetStopNResetPoint(agv, ref stopPointName, ref resetPointName);
                Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);

                if (!Acs.DicRailPoint.ContainsKey(stopPointName))
                {
                    GetStopNResetPoint(agv, ref stopPointName, ref resetPointName);
                }
                if (stopPointName == string.Empty)
                    return false;
                AGVPoint stopPoint = Acs.DicRailPoint[stopPointName];
                AGVPoint resetPoint = Acs.DicRailPoint[resetPointName];
                bool isSameRouteAGV = false;

                foreach (KeyValuePair<Tuple<string, string>, List<AGV>> kvpAGVs in _routeAGVs)
                {
                    if (kvpAGVs.Value.Count > 0)
                    {
                        return false; //Reset 같은 AGV가 이미 안에 있는 경우 false
                    }
                }

                AGVZCUReservation ohtReservation = null;

                foreach (AGVZCUReservation reservation in Reservations[stopNreset])
                {
                    if (reservation.AGV.Name == agv.Name)
                        ohtReservation = reservation;
                }

                if (ohtReservation == null)
                    return false;

                Time curTime = SimEngine.Instance.TimeNow;

                foreach (KeyValuePair<Tuple<string, string>, List<AGVZCUReservation>> kvpReservation in Reservations)
                {
                    List<AGVZCUReservation> compReservationList = kvpReservation.Value;

                    if (compReservationList.Count > 0)
                    {
                        if (compReservationList[0].AGV.Name != agv.Name)
                            return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
                return false;
            }
        }

        public bool GetStopNResetPoint(AGV agv, ref string stopPointName, ref string resetPointName)
        {
            // oht
            foreach (AGVLine line in agv.Route)
            {
                if (line.Zcu != null && line.ZcuName == this.Name)
                {
                    stopPointName = GetStopPoint(line);
                    resetPointName = GetResetPoint(line);
                    return true;
                }
            }

            AGVLine searchLine = agv.Line;
            while (searchLine.ZcuName != this.Name)
            {
                AGVLine sameDirectionToLine = null;
                double minimumDirectionError = double.MaxValue;
                foreach (AGVLine toLine in searchLine.EndPoint.OutLines)
                {
                    double directionError = (searchLine.Direction - toLine.Direction).Length();
                    if (directionError < minimumDirectionError)
                    {
                        sameDirectionToLine = toLine;
                        minimumDirectionError = directionError;
                    }
                }

                if (sameDirectionToLine.Zcu != null && sameDirectionToLine.ZcuName == this.Name)
                {
                    stopPointName = GetStopPoint(sameDirectionToLine);
                    resetPointName = GetResetPoint(sameDirectionToLine);
                    return true;
                }

                searchLine = sameDirectionToLine;
            }

            return false;
        }

        private string GetStopPoint(AGVLine line)
        {
            if (line.Zcu == null)
            {
                return string.Empty;
            }

            if (line.Zcu != this)
            {
                return string.Empty;
            }

            if (((AGVPoint)line.StartPoint).Zcu != null)
            {
                if (((AGVPoint)line.StartPoint).ZcuType == ZCU_TYPE.STOP)
                {
                    return line.StartPoint.Name;
                }
            }

            if (line.StartPoint.InLines.Count == 1)
            {

                return GetStopPoint(line.StartPoint.InLines[0] as AGVLine);
            }
            else
            {
                //    Console.WriteLine("ZCU Error 발생  ZCU: " + line.Zcu.Name + "  Reset Stop 사이 분기 존재");
                foreach (AGVLine fromLine in line.StartPoint.InLines)
                {
                    if (fromLine.Zcu != null)
                    {
                        if (fromLine.Zcu == this)
                        {
                            return GetStopPoint(fromLine);
                        }
                    }

                }
            }

            return string.Empty;
        }

        private string GetResetPoint(AGVLine line)
        {
            if (line.Zcu == null)
            {
                return string.Empty;
            }

            if (line.Zcu != this)
            {
                return string.Empty;
            }

            if (((AGVPoint)line.EndPoint).Zcu != null)
            {
                if (((AGVPoint)line.EndPoint).ZcuType == ZCU_TYPE.RESET)
                {
                    return line.EndPoint.Name;
                }
            }

            if (line.EndPoint.OutLines.Count == 1)
            {
                return GetResetPoint(line.EndPoint.OutLines[0] as AGVLine);
            }
            else
            {
                // Console.WriteLine("ZCU Error 발생  ZCU: " + line.Zcu.Name + "  Reset Stop 사이 분기 존재");
                foreach (AGVLine toLine in line.EndPoint.OutLines)
                {
                    if (toLine.Zcu != null)
                    {
                        if (toLine.Zcu == this)
                        {
                            return GetResetPoint(toLine);
                        }
                    }
                }
            }

            return string.Empty;
        }
        /// <summary>
        /// 변경된 경로를 반영해서 resetpoint 수정하고 지나갈 수 있는지 확인 후 무브 고고 
        /// </summary>
        /// <param name="oht"></param>
        public void ChangeReservationResetPoint(Time simTime, AGV oht)
        {
            try
            {
                Tuple<string, string> oldKey = null;
                AGVZCUReservation reservation = null;
                foreach (KeyValuePair<Tuple<string, string>, List<AGVZCUReservation>> kvpReservation in Reservations)
                {
                    foreach (AGVZCUReservation reserTemp in kvpReservation.Value)
                    {
                        if (reserTemp.AGV.Name == oht.Name)
                        {
                            oldKey = kvpReservation.Key;
                            reservation = reserTemp;
                        }
                    }
                }

                if (reservation == null)
                    return;

                string newStopPointName = string.Empty;
                string newResetPointName = string.Empty;

                GetStopNResetPoint(oht, ref newStopPointName, ref newResetPointName);

                if (newStopPointName == string.Empty || newResetPointName == string.Empty)
                    return;

                AGVPoint newStopPoint = Acs.DicRailPoint[newStopPointName];
                AGVPoint newResetPoint = Acs.DicRailPoint[newResetPointName];

                if (newStopPointName != reservation.StopPoint.Name || newResetPointName != reservation.ResetPoint.Name)
                {
                    Tuple<string, string> newKey = new Tuple<string, string>(newStopPoint.Name, newResetPoint.Name);

                    if (Reservations.ContainsKey(oldKey) && Reservations[oldKey].Contains(reservation))
                    {
                        Reservations[oldKey].Remove(reservation);
                        reservation.StopPoint = newStopPoint;
                        reservation.ResetPoint = newResetPoint;
                        Reservations[newKey].Add(reservation);
                        Reservations[newKey].Sort((x1, x2) => x1.ReservationTime.CompareTo(x2.ReservationTime));
                        oht.ReservationZcuResetPointName = newResetPointName;
                    }
                    else if (Reservations.ContainsKey(newKey) && Reservations[newKey].Contains(reservation))
                        return;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public bool ContainsReservation(AGV agv)
        {
            string stopPointName = string.Empty;
            string resetPointName = string.Empty;
            GetStopNResetPoint(agv, ref stopPointName, ref resetPointName);

            if (stopPointName == string.Empty && resetPointName == string.Empty)
                return false;

            if (!Reservations.ContainsKey(new Tuple<string, string>(stopPointName, resetPointName)))
            {
                GetStopNResetPoint(agv, ref stopPointName, ref resetPointName);
            }

            foreach (AGVZCUReservation reservation in Reservations[new Tuple<string, string>(stopPointName, resetPointName)])
            {
                if (agv.Name == reservation.AGV.Name)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// resetPoint에서 AGV가 나갈 때
        /// 해당 resetPoint로 이동중인 AGV가 없고,
        /// 해당 resetPoint로 들어오고싶은 AGV가 StopPoint에서 대기중이면 들어오게 하는 함수
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="simLogs"></param>
        /// <param name="resetPoint"></param>
        public bool ProcessReservation(Time simTime, AGVPoint resetPoint)
        {

            if (_stopAGVs.Count == 0)
                return false;

            foreach (KeyValuePair<Tuple<string, string>, List<AGV>> kvpAGVs in _routeAGVs)
            {
                if (kvpAGVs.Key.Item2 == resetPoint.Name
                    && kvpAGVs.Value.Count > 0)
                    return false; //Stop은 다른데 Reset은 같은 AGV가 이미 안에 있는 경우 false
            }

            AGV fastestReservationStopAGV = null;
            Time reservationTime = -1;
            foreach (AGV stopAGV in _stopAGVs)
            {
                if (IsAvailableToEnter(stopAGV) && stopAGV.ReservationZcuResetPointName == resetPoint.Name)
                {
                    if (reservationTime == -1)
                    {
                        fastestReservationStopAGV = stopAGV;
                        reservationTime = stopAGV.ReservationTime;
                    }
                    else if (reservationTime > stopAGV.ReservationTime)
                    {
                        fastestReservationStopAGV = stopAGV;
                        reservationTime = stopAGV.ReservationTime;
                    }
                }
            }

            if (fastestReservationStopAGV == null)
                return false;

            AGVZCUReservation reservation = null;
            foreach (KeyValuePair<Tuple<string, string>, List<AGVZCUReservation>> kvpReservation in Reservations)
            {
                if (kvpReservation.Key.Item2 == resetPoint.Name)
                {
                    foreach (AGVZCUReservation tempReservation in kvpReservation.Value)
                    {
                        if (tempReservation.AGV.Name == fastestReservationStopAGV.Name)
                        {
                            if (IsAvailableToEnter(tempReservation.AGV))
                            {
                                reservation = tempReservation;
                                break;
                            }
                        }
                    }
                }
            }

            //예약이 있고, 예약 건 AGV이름이랑 StopPoint에 대기하고 있는 AGV랑 이름이 같을 때,
            if (reservation != null)
            {
                string newStopPointName = string.Empty;
                string newResetPointName = string.Empty;
                GetStopNResetPoint(reservation.AGV, ref newStopPointName, ref newResetPointName);

                AGVPoint stopPoint = reservation.StopPoint;

                if (stopPoint.StopPorts.Count == 0 || (stopPoint.StopPorts.Count > 0 && stopPoint.StopPorts[0].Object != null && stopPoint.StopPorts[0].Object.Name != reservation.AGV.Name))
                    return false;

                if (reservation.AGV != null && (reservation.AGV.State is VEHICLE_STATE.LOADING || reservation.AGV.State is VEHICLE_STATE.UNLOADING))
                    return false;

                stopPoint.StopPorts[0].PortType = EXT_PORT.OHT_IN;
                stopPoint.ExternalFunction(simTime, stopPoint.StopPorts[0]);

                return true;
            }

            return false;
        }

        public bool ProcessReservation(Time simTime)
        {
            AGVZCUReservation earlestReservation = null;
            AGVPoint earlestPoint = null;

            if (_stopAGVs.Count == 0)
                return false;

            AGV fastestReservationStopAGV = null;
            Time reservationTime = -1;
            string resetPointName = string.Empty;
            foreach (AGV stopAGV in _stopAGVs)
            {
                if (IsAvailableToEnter(stopAGV))
                {
                    if (reservationTime == -1)
                    {
                        fastestReservationStopAGV = stopAGV;
                        reservationTime = stopAGV.ReservationTime;
                        resetPointName = stopAGV.ReservationZcuResetPointName;
                    }
                    else if (reservationTime > stopAGV.ReservationTime)
                    {
                        fastestReservationStopAGV = stopAGV;
                        reservationTime = stopAGV.ReservationTime;
                        resetPointName = stopAGV.ReservationZcuResetPointName;
                    }
                }
            }

            if (fastestReservationStopAGV == null)
                return false;


            foreach (KeyValuePair<Tuple<string, string>, List<AGV>> kvpAGVs in _routeAGVs)
            {
                if (kvpAGVs.Key.Item2 == resetPointName
                    && kvpAGVs.Value.Count > 0)
                    return false; //Stop은 다른데 Reset은 같은 AGV가 이미 안에 있는 경우 false
            }

            AGVZCUReservation reservation = null;
            foreach (KeyValuePair<Tuple<string, string>, List<AGVZCUReservation>> kvpReservation in Reservations)
            {
                if (kvpReservation.Key.Item2 == resetPointName)
                {
                    foreach (AGVZCUReservation tempReservation in kvpReservation.Value)
                    {
                        if (tempReservation.AGV.Name == fastestReservationStopAGV.Name)
                        {
                            reservation = tempReservation;
                            break;
                        }
                    }
                }
            }

            //예약이 있고, 예약 건 AGV이름이랑 StopPoint에 대기하고 있는 AGV랑 이름이 같을 때,
            if (reservation != null)
            {
                string newStopPointName = string.Empty;
                string newResetPointName = string.Empty;
                GetStopNResetPoint(reservation.AGV, ref newStopPointName, ref newResetPointName);

                AGVPoint stopPoint = reservation.StopPoint;

                if (stopPoint.StopPorts.Count == 0 || (stopPoint.StopPorts.Count > 0 && stopPoint.StopPorts[0].Object != null && stopPoint.StopPorts[0].Object.Name != reservation.AGV.Name))
                    return false;

                if (reservation.AGV != null && (reservation.AGV.State is VEHICLE_STATE.LOADING || reservation.AGV.State is VEHICLE_STATE.UNLOADING))
                    return false;

                stopPoint.StopPorts[0].PortType = EXT_PORT.OHT_IN;
                stopPoint.ExternalFunction(simTime, stopPoint.StopPorts[0]);

                return true;
            }

            return false;
        }

        public void AddReservation(Time time, AGV agv, AGVPoint stopPoint)
        {
            try
            {
                string stopPointName = string.Empty;
                string resetPointName = string.Empty;
                GetStopNResetPoint(agv, ref stopPointName, ref resetPointName);
                Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);
                AGVPoint resetPoint = Acs.DicRailPoint[resetPointName];
                AGVZCUReservation reservation = new AGVZCUReservation(time, agv, stopPoint, resetPoint);

                //이미 있으면 추가 하지 않음.
                AGVZCUReservation pastAGVReservation = null;
                foreach (List<AGVZCUReservation> listZcuReservation in _reservations.Values)
                {
                    foreach (AGVZCUReservation zcuReserv in listZcuReservation)
                    {
                        if (zcuReserv.AGV.Name == agv.Name && (zcuReserv.StopPoint.Name != stopPointName || zcuReserv.ResetPoint.Name != resetPointName))
                        {
                            pastAGVReservation = zcuReserv;
                            break;
                        }
                        else if (zcuReserv.AGV.Name == agv.Name && zcuReserv.StopPoint.Name == stopPointName && zcuReserv.ResetPoint.Name == resetPointName)
                            return;
                    }
                }

                //이전 AGV예약을 삭제
                if (pastAGVReservation != null)
                {
                    reservation.ReservationTime = pastAGVReservation.ReservationTime > reservation.ReservationTime ? reservation.ReservationTime : pastAGVReservation.ReservationTime;
                    Tuple<string, string> pastStopNreset = new Tuple<string, string>(pastAGVReservation.StopPoint.Name, pastAGVReservation.ResetPoint.Name);
                    Reservations[pastStopNreset].Remove(pastAGVReservation);
                    agv.ReservationZCUs.Remove(this);
                    if (agv.ReservationZCUs.Count == 0)
                    {
                        agv.ReservationTime = Time.Zero;
                        agv.ReservationZcuResetPointName = string.Empty;
                    }
                }

                Time backReservationTime = Time.MaxValue;
                foreach (List<AGVZCUReservation> listZcuReservation in _reservations.Values)
                {
                    foreach (AGVZCUReservation zcuReservation in listZcuReservation)
                    {
                        if (zcuReservation.StopPoint.Name != stopPointName)
                            continue;

                        bool isBackReservationAGV = false;

                        //먼저 예약된 AGV가 나보다 뒤쪽 라인에 있는 AGV인지 확인.
                        foreach (AGVLine fromLine in agv.Line.StartPoint.InLines)
                        {
                            if (zcuReservation.AGV.Line == fromLine)
                            {
                                isBackReservationAGV = true;
                                break;
                            }
                        }

                        //먼저 예약한 애가 같은 라인에 있는데 나보다 뒤쪽에 있거나 뒤쪽 라인에 있으면 
                        if ((zcuReservation.AGV.Line.Name == agv.Line.Name && zcuReservation.AGV.Line.GetDistanceAtTime(zcuReservation.AGV, time) < agv.Line.GetDistanceAtTime(agv, time) && zcuReservation.ReservationTime < reservation.ReservationTime
                            || (isBackReservationAGV && zcuReservation.ReservationTime < reservation.ReservationTime))
                            && backReservationTime > zcuReservation.ReservationTime)
                        {
                            backReservationTime = zcuReservation.ReservationTime - 0.01;
                        }
                    }
                }

                if (reservation.ReservationTime > backReservationTime)
                    reservation.ReservationTime = backReservationTime;

                _reservations[stopNreset].Add(reservation);
                _reservations[stopNreset].Sort((x1, x2) => x1.ReservationTime.CompareTo(x2.ReservationTime));

                if (agv.ReservationZCUs.Count == 0)
                {
                    agv.ReservationTime = reservation.ReservationTime;
                    agv.ReservationZcuResetPointName = resetPointName;
                }

                if (!agv.ReservationStopPoints.ContainsKey(reservation.StopPoint.ID))
                    agv.ReservationStopPoints.Add(reservation.StopPoint.ID, reservation.ReservationTime);
                else
                {
                    if (agv.ReservationStopPoints[reservation.StopPoint.ID] != reservation.ReservationTime)
                        agv.ReservationStopPoints[reservation.StopPoint.ID] = reservation.ReservationTime;
                }

                if (!agv.ReservationZCUs.Contains(this))
                    agv.ReservationZCUs.Add(this);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public bool RemoveReservation(AGV oht)
        {
            foreach (KeyValuePair<Tuple<string, string>, List<AGVZCUReservation>> kvpReservationList in _reservations)
            {
                foreach (AGVZCUReservation reservation in kvpReservationList.Value.ToList())
                {
                    if (reservation.AGV.Name == oht.Name)
                    {
                        kvpReservationList.Value.Remove(reservation);
                        RouteReservations[kvpReservationList.Key].Add(reservation);

                        if (oht.ReservationZCUs.Count == 0)
                        {
                            oht.ReservationTime = Time.Zero;
                            oht.ReservationZcuResetPointName = string.Empty;
                        }
                        else if (oht.ReservationZCUs.Count == 1)
                        {
                            oht.ReservationZCUs.Remove(this);
                            oht.ReservationTime = Time.Zero;
                            oht.ReservationZcuResetPointName = string.Empty;
                        }
                        else if (oht.ReservationZCUs.Count > 1)
                        {
                            Time reservationTime = Time.Zero;
                            string resetPointName = string.Empty;
                            oht.ReservationZCUs.Remove(this);
                            oht.ReservationZCUs[0].getReservationInfo(oht.Name, ref reservationTime, ref resetPointName);
                            oht.ReservationTime = reservationTime;
                            oht.ReservationZcuResetPointName = resetPointName;
                        }
                        oht.ReservationStopPoints.Remove(reservation.StopPoint.ID);

                        return true;
                    }
                }
            }

            return false;
        }

        public void getReservationInfo(string ohtName, ref Time time, ref string resetPointName)
        {
            try
            {
                foreach (KeyValuePair<Tuple<string, string>, List<AGVZCUReservation>> kvpReservationList in _reservations)
                {
                    foreach (AGVZCUReservation reservation in kvpReservationList.Value)
                    {
                        if (reservation.AGV.Name == ohtName)
                        {
                            time = reservation.ReservationTime;
                            resetPointName = reservation.ResetPoint.Name;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public Time GetScheduleTimeOfFrontAGV(AGV oht)
        {
            Time scheduleTime = -1;
            string stopPointName = string.Empty;
            string resetPointName = string.Empty;
            GetStopNResetPoint(oht, ref stopPointName, ref resetPointName);
            Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);

            foreach (KeyValuePair<Tuple<string, string>, List<AGV>> kvpAGVs in _routeAGVs)
            {
                if (kvpAGVs.Key.Item2 == resetPointName && kvpAGVs.Value.Count > 0)
                {
                    AGV frontAGV = kvpAGVs.Value.Last();

                    if (Lines.Contains(frontAGV.Line) && frontAGV.Line.LstObjectIndex.ContainsKey(frontAGV.ID) && frontAGV.Line.LstPosData[frontAGV.Line.LstObjectIndex[frontAGV.ID]].Count > 0)
                    {
                        scheduleTime = frontAGV.Line.LstPosData[frontAGV.Line.LstObjectIndex[frontAGV.ID]].Last()._endTime;
                        break;
                    }
                }
            }


            foreach (KeyValuePair<Tuple<string, string>, List<AGVZCUReservation>> kvpReservation in Reservations)
            {
                if (kvpReservation.Key.Item1 != stopPointName && kvpReservation.Key.Item2 == resetPointName)
                {
                    List<AGVZCUReservation> compReservationList = kvpReservation.Value;
                    if (compReservationList.Count > 0)
                    {
                        AGV frontAGV = compReservationList[0].AGV;

                        if (frontAGV.Line.LstObjectIndex.ContainsKey(frontAGV.ID) && oht.ReservationTime > frontAGV.ReservationTime && frontAGV.Line.LstPosData[frontAGV.Line.LstObjectIndex[frontAGV.ID]].Count > 0)
                        {
                            Time tempEndTime = frontAGV.Line.LstPosData[frontAGV.Line.LstObjectIndex[frontAGV.ID]].Last()._endTime;

                            scheduleTime = tempEndTime < scheduleTime ? scheduleTime : tempEndTime;
                        }
                    }
                }
            }

            return scheduleTime;
        }
    }
}
