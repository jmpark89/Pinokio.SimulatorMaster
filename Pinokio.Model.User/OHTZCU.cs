using Logger;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pinokio.Model.User
{
    public enum ZCU_TYPE
    {
        NON, STOP, RESET
    }

    /// <summary>
    /// ZCU의 기능
    /// Stop, Reset Point가 모두 같으면 후발 OHT도 같이 지나갈 수 있다.
    /// Stop은 같은데 Reset은 다르면 후발 OHT도 같이 지나갈 수 있다.
    /// Stop도 다르고 Reset도 다르면 후발 OHT도 같이 지나갈 수 있다.
    /// Stop은 다르고 Reset은 같으면 후발 OHT가 Stop Point에서 기다렸다가 선발 OHT가 나가면 들어감.
    /// </summary>
    public class OHTZCU
    {
        private string _name;
        private Dictionary<string, OHTPoint> _fromPoints; // Key: stopPointName
        private Dictionary<string, OHTPoint> _toPoints; // Key: resetPointName
        private List<OHTLine> _lines;
        private List<OHT> _ohts;
        private Dictionary<Tuple<string, string>, List<OHTZCUReservation>> _reservations; //Key: stopPointName, resetPointName
        private Dictionary<Tuple<string, string>, List<OHTZCUReservation>> _routeReservations; //Key: stopPointName, resetPointName
        private Dictionary<Tuple<string, string>, List<OHT>> _routeOHTs; //Key: stopPointName, resetPointName
        private List<OHT> _stopOHTs;
        private OCS _ocs;
        public string Name
        {
            get { return _name; }
        }

        public OCS Ocs
        {
            get { return _ocs; }
        }

        public List<OHT> Ohts
        {
            get
            {
                return _ohts;
            }

            set
            {
                _ohts = value;
            }
        }

        public bool IsBusy
        {
            get
            {
                if (_ohts.Count > 0)
                    return true;
                else
                    return false;
            }
        }
        public Dictionary<string, OHTPoint> FromPoints
        {
            get { return _fromPoints; }
        }

        public Dictionary<string, OHTPoint> ToPoints
        {
            get { return _toPoints; }
        }

        public List<OHTLine> Lines
        {
            get { return _lines; }
        }

        public Dictionary<Tuple<string, string>, List<OHTZCUReservation>> Reservations
        {
            get { return _reservations; }
        }

        public Dictionary<Tuple<string, string>, List<OHTZCUReservation>> RouteReservations
        {
            get { return _routeReservations; }
        }

        public Dictionary<Tuple<string, string>, List<OHT>> RouteOHTs
        {
            get { return _routeOHTs; }
            set { _routeOHTs = value; }
        }
        public List<OHT> StopOHTs
        {
            get
            {
                return _stopOHTs;
            }
            set
            {
                _stopOHTs = value;
            }
        }


        public OHTZCU(string name, OCS ocs)
        {
            _name = name;
            _ocs = ocs;
            _ohts = new List<OHT>();
            _fromPoints = new Dictionary<string, OHTPoint>();
            _toPoints = new Dictionary<string, OHTPoint>();
            _lines = new List<OHTLine>();
            _reservations = new Dictionary<Tuple<string, string>, List<OHTZCUReservation>>();
            _routeReservations = new Dictionary<Tuple<string, string>, List<OHTZCUReservation>>();
            _routeOHTs = new Dictionary<Tuple<string, string>, List<OHT>>();
            _stopOHTs = new List<OHT>();
        }

        public void SetLines()
        {
            try
            {
                _lines.Clear();
                _reservations.Clear();
                _routeReservations.Clear();
                _routeOHTs.Clear();

                foreach (OHTPoint inPoint in _fromPoints.Values)
                {
                    List<OHTPoint> railPointNodes = new List<OHTPoint>();
                    List<OHTLine> lines = new List<OHTLine>();
                    InitializeReservationNohtCount(inPoint, railPointNodes, lines, inPoint.Name);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void InitializeReservationNohtCount(OHTPoint currentPoint, List<OHTPoint> visitedPoints, List<OHTLine> zoneOutLines, string stopPointName)
        {

            try
            {
                visitedPoints.Add(currentPoint);
                List<OHTLine> nextLines = currentPoint.OutLines.Where(line => !zoneOutLines.Contains(line)).ToList().ConvertAll(x => (OHTLine)x);

                foreach (OHTLine nextLine in nextLines)
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
                List<OHTPoint> nextNodes = nextLines.Select(line => line.EndPoint).ToList().ConvertAll(x=>(OHTPoint)x);

                while (nextNodes.Any())
                {
                    OHTPoint nextNode = nextNodes.First();

                    nextNodes.Remove(nextNode);

                    if (nextNode.ZcuName == Name && nextNode.ZcuType == ZCU_TYPE.RESET && nextNode.OutLines.Count > 0)
                    {

                        OHTPoint resetFindingNode = nextNode.OutLines[0].EndPoint as OHTPoint;
                        List<OHTLine> additionalLines = new List<OHTLine>();
                        additionalLines.Add(nextNode.OutLines[0] as OHTLine);
                        while (resetFindingNode.ZcuType != ZCU_TYPE.RESET)
                        {
                            if (resetFindingNode.OutLines.Count == 0)
                                break;
                            resetFindingNode = resetFindingNode.OutLines[0].EndPoint as OHTPoint;
                            if (resetFindingNode.OutLines.Count == 0)
                                break;

                            if (additionalLines.Count > Ocs.DicRailLine.Count)
                                break;

                            additionalLines.Add(resetFindingNode.OutLines[0] as OHTLine);
                        }

                        if (!ToPoints.ContainsKey(nextNode.Name))
                            ToPoints.Add(nextNode.Name, nextNode);

                        Tuple<string, string> key = new Tuple<string, string>(stopPointName, nextNode.Name);

                        if (!Reservations.ContainsKey(key))
                        {
                            Reservations.Add(key, new List<OHTZCUReservation>());
                            RouteReservations.Add(key, new List<OHTZCUReservation>());
                            RouteOHTs.Add(key, new List<OHT>());
                            Lines.AddRange(zoneOutLines);

                            foreach (OHTLine line in zoneOutLines)
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
                            Reservations.Add(new Tuple<string, string>(stopPointName, stopPointName), new List<OHTZCUReservation>());
                            RouteReservations.Add(new Tuple<string, string>(stopPointName, stopPointName), new List<OHTZCUReservation>());
                            RouteOHTs.Add(new Tuple<string, string>(stopPointName, stopPointName), new List<OHT>());
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

        public void AddOHT(OHT oht)
        {
            try
            {
                string stopPointName = string.Empty;
                string resetPointName = string.Empty;
                GetStopNResetPoint(oht, ref stopPointName, ref resetPointName);
                Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);

                if (!_routeOHTs[stopNreset].Contains(oht))
                    _routeOHTs[stopNreset].Add(oht);

                oht.CurZcu = this;
                oht.CurZcuResetPointName = resetPointName;
                RemoveReservation(oht);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void RemoveOHT(OHT oht)
        {
            try
            {
                foreach (KeyValuePair<Tuple<string, string>, List<OHT>> kvpRouteOHTList in RouteOHTs)
                {
                    if (kvpRouteOHTList.Value.Contains(oht))
                    {
                        kvpRouteOHTList.Value.Remove(oht);

                        foreach (OHTZCUReservation zcuReservation in RouteReservations[kvpRouteOHTList.Key].ToArray())
                        {
                            if (zcuReservation.OHT == oht)
                                RouteReservations[kvpRouteOHTList.Key].Remove(zcuReservation);
                        }
                    }
                }

                oht.CurZcu = null;
                oht.CurZcuResetPointName = string.Empty;

                RemoveOutOHTs();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void RemoveOutOHTs()
        {
            try
            {
                foreach (List<OHT> routeOHTList in RouteOHTs.Values)
                {
                    int idx = 0;
                    while (idx < routeOHTList.Count)
                    {
                        OHT routeOHT = routeOHTList[idx];

                        if (!this.Lines.Contains(routeOHT.Line))
                            routeOHTList.Remove(routeOHT);
                        else
                            ++idx;
                    }
                }
            }
            catch (Exception ex) {
                ErrorLogger.SaveLog(ex);
            }
        }

        /// Stop, Reset Point가 모두 같고 다른 Stop, 같은 Reset Point인 예약이 없으면 후발 OHT도 같이 지나갈 수 있다.
        /// Stop은 같은데 Reset은 다르면 후발 OHT도 같이 지나갈 수 있다.
        /// Stop도 다르고 Reset도 다르면 후발 OHT도 같이 지나갈 수 있다.
        /// Stop은 다르고 Reset은 같으면 후발 OHT가 Stop Point에서 기다렸다가 선발 OHT가 나가면 들어감.
        /// StopPoint에 도착해서 들어갈 수 있는지 확인하는 함수
        public bool IsAvailableToEnter(OHT oht)
        {
            try
            {
                string stopPointName = string.Empty;
                string resetPointName = string.Empty;
                GetStopNResetPoint(oht, ref stopPointName, ref resetPointName);
                Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);

                if (Ocs.DicRailPoint.ContainsKey(stopPointName))
                {
                    GetStopNResetPoint(oht, ref stopPointName, ref resetPointName);
                }
                if (stopPointName == string.Empty)
                    return false;
                OHTPoint stopPoint = Ocs.DicRailPoint[stopPointName];
                OHTPoint resetPoint = Ocs.DicRailPoint[resetPointName];
                bool isSameRouteOHT = false;

                foreach (KeyValuePair<Tuple<string, string>, List<OHT>> kvpOHTs in _routeOHTs)
                {
                    if (kvpOHTs.Key.Item1 != stopPointName
                        && kvpOHTs.Key.Item2 == resetPointName
                        && kvpOHTs.Value.Count > 0)
                    {
                        return false; //Reset 같은 OHT가 이미 안에 있는 경우 false
                    }
                    else if (kvpOHTs.Key.Item1 == stopPointName
                        && kvpOHTs.Key.Item2 == resetPointName
                        && kvpOHTs.Value.Count > 0)
                    {
                        isSameRouteOHT = true;

                        foreach (OHTLine fromLine in resetPoint.InLines)
                        {
                            if (fromLine.StartPoint.Name == stopPointName)
                                return false; //Stop, reset이 같은데 중간에 via Point가 없으면 단독 진입
                        }
                    }
                }

                OHTZCUReservation ohtReservation = null;

                foreach (OHTZCUReservation reservation in Reservations[stopNreset])
                {
                    if (reservation.OHT.Name == oht.Name)
                        ohtReservation = reservation;
                }

                Time curTime = SimEngine.Instance.TimeNow;

                foreach (KeyValuePair<Tuple<string, string>, List<OHTZCUReservation>> kvpReservation in Reservations)
                {
                    List<OHTZCUReservation> compReservationList = kvpReservation.Value;

                    // Reset 같은 경우
                    if (kvpReservation.Key.Item1 != stopPointName && kvpReservation.Key.Item2 == resetPointName)
                    {
                        OHTPoint compStopPoint = Ocs.DicRailPoint[kvpReservation.Key.Item1];

                        if (ohtReservation == null && compReservationList.Count > 0)
                        {
                            return false;
                        }
                        else if (compStopPoint.InLines.Count > 0 && resetPoint.OutLines.Count > 0)
                        {
                            if (compReservationList.Count > 0 && compReservationList[0].ReservationTime < ohtReservation.ReservationTime)
                            {
                                int compOHTSequence = compReservationList[0].OHT.Line.LstObjectIndex[compReservationList[0].OHT.ID];
                                OHT compOHT = compReservationList[0].OHT;
                                List<PosData> compOHTPosData = compOHT.Line.LstPosData[compOHTSequence];

                                int ohtSequence = oht.Line.LstObjectIndex[oht.ID];
                                List<PosData> ohtPosData = oht.Line.LstPosData[ohtSequence];

                                // 나는 이미 Stop Point에 도착했는데, 같은 경로에 먼저 가는 OHT가 없고 다른 경로에서 먼저 예약한 OHT가 현재보다 더 멈춰있는 스케줄이 있을 경우 먼저 지나감.
                                if (ohtPosData.Count > 0 && compOHTPosData.Count > 0 && Math.Round(ohtPosData.Last()._endPos, 0) >= Math.Round(oht.Line.Length, 0) && !isSameRouteOHT
                                    && (!(compOHT.State is VEHICLE_STATE.LOADING || compOHT.State is VEHICLE_STATE.UNLOADING) && compOHTPosData.Last()._endSpeed == 0)
                                    && (compOHTPosData.Last()._endTime > curTime || Math.Round(compOHTPosData.Last()._endPos, 0) < Math.Round(compOHT.Line.Length, 0)))
                                    return true;
                                else //  이외의 모든 상황은 못 지나감.
                                    return false;
                            }
                            else if (compReservationList.Count > 0 && compReservationList[0].ReservationTime == ohtReservation.ReservationTime)
                            {
                                int ohtIndex = Ocs.Vehicles.IndexOf(oht);
                                int compOhtIndex = Ocs.Vehicles.IndexOf(compReservationList[0].OHT);

                                if (ohtIndex > compOhtIndex)
                                    return false;   // 같은 시간에 예약했다면, OHT ID 번호가 느린쪽이 false;
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

        public bool KeepGoing(OHT oht)
        {
            try
            {
                string stopPointName = string.Empty;
                string resetPointName = string.Empty;
                GetStopNResetPoint(oht, ref stopPointName, ref resetPointName);
                Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);

                if (!Ocs.DicRailPoint.ContainsKey(stopPointName))
                {
                    GetStopNResetPoint(oht, ref stopPointName, ref resetPointName);
                }
                if (stopPointName == string.Empty)
                    return false;
                OHTPoint stopPoint = Ocs.DicRailPoint[stopPointName];
                OHTPoint resetPoint = Ocs.DicRailPoint[resetPointName];
                bool isSameRouteOHT = false;

                foreach (KeyValuePair<Tuple<string, string>, List<OHT>> kvpOHTs in _routeOHTs)
                {
                    if (kvpOHTs.Value.Count > 0)
                    {
                        return false; //Reset 같은 OHT가 이미 안에 있는 경우 false
                    }
                }

                OHTZCUReservation ohtReservation = null;

                foreach (OHTZCUReservation reservation in Reservations[stopNreset])
                {
                    if (reservation.OHT.Name == oht.Name)
                        ohtReservation = reservation;
                }

                if (ohtReservation == null)
                    return false;

                Time curTime = SimEngine.Instance.TimeNow;

                foreach (KeyValuePair<Tuple<string, string>, List<OHTZCUReservation>> kvpReservation in Reservations)
                {
                    List<OHTZCUReservation> compReservationList = kvpReservation.Value;

                    if (compReservationList.Count > 0)
                    {
                        if (compReservationList[0].OHT.Name != oht.Name)
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

        public bool GetStopNResetPoint(OHT oht, ref string stopPointName, ref string resetPointName)
        {
            // oht
            foreach (OHTLine line in oht.Route)
            {
                if (line.Zcu != null && line.ZcuName == this.Name)
                {
                    stopPointName = GetStopPoint(line);
                    resetPointName = GetResetPoint(line);
                    return true;
                }
            }

            OHTLine searchLine = oht.Line;
            while (searchLine.ZcuName != this.Name)
            {
                OHTLine sameDirectionToLine = null;
                double minimumDirectionError = double.MaxValue;
                foreach (OHTLine toLine in searchLine.EndPoint.OutLines)
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

        private string GetStopPoint(OHTLine line)
        {
            if (line.Zcu == null)
            {
                return string.Empty;
            }

            if (line.Zcu != this)
            {
                return string.Empty;
            }

            if (((OHTPoint)line.StartPoint).Zcu != null)
            {
                if (((OHTPoint)line.StartPoint).ZcuType == ZCU_TYPE.STOP)
                {
                    return line.StartPoint.Name;
                }
            }

            if (line.StartPoint.InLines.Count == 1)
            {

                return GetStopPoint(line.StartPoint.InLines[0] as OHTLine);
            }
            else
            {
                //    Console.WriteLine("ZCU Error 발생  ZCU: " + line.Zcu.Name + "  Reset Stop 사이 분기 존재");
                foreach (OHTLine fromLine in line.StartPoint.InLines)
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

        private string GetResetPoint(OHTLine line)
        {
            if (line.Zcu == null)
            {
                return string.Empty;
            }

            if (line.Zcu != this)
            {
                return string.Empty;
            }

            if (((OHTPoint)line.EndPoint).Zcu != null)
            {
                if (((OHTPoint)line.EndPoint).ZcuType == ZCU_TYPE.RESET)
                {
                    return line.EndPoint.Name;
                }
            }

            if (line.EndPoint.OutLines.Count == 1)
            {
                return GetResetPoint(line.EndPoint.OutLines[0] as OHTLine);
            }
            else
            {
                // Console.WriteLine("ZCU Error 발생  ZCU: " + line.Zcu.Name + "  Reset Stop 사이 분기 존재");
                foreach (OHTLine toLine in line.EndPoint.OutLines)
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
        public void ChangeReservationResetPoint(Time simTime, OHT oht)
        {
            try
            {
                Tuple<string, string> oldKey = null;
                OHTZCUReservation reservation = null;
                foreach (KeyValuePair<Tuple<string, string>, List<OHTZCUReservation>> kvpReservation in Reservations)
                {
                    foreach (OHTZCUReservation reserTemp in kvpReservation.Value)
                    {
                        if (reserTemp.OHT.Name == oht.Name)
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

                OHTPoint newStopPoint = Ocs.DicRailPoint[newStopPointName];
                OHTPoint newResetPoint = Ocs.DicRailPoint[newResetPointName];

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

        public bool ContainsReservation(OHT oht)
        {
            if (oht.Name == "OHT_131")
                ;

            string stopPointName = string.Empty;
            string resetPointName = string.Empty;
            GetStopNResetPoint(oht, ref stopPointName, ref resetPointName);

            if (stopPointName == string.Empty && resetPointName == string.Empty)
                return false;

            if (!Reservations.ContainsKey(new Tuple<string, string>(stopPointName, resetPointName)))
            {
                GetStopNResetPoint(oht, ref stopPointName, ref resetPointName);
            }

            foreach (OHTZCUReservation reservation in Reservations[new Tuple<string, string>(stopPointName, resetPointName)])
            {
                if (oht.Name == reservation.OHT.Name)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// resetPoint에서 OHT가 나갈 때
        /// 해당 resetPoint로 이동중인 OHT가 없고,
        /// 해당 resetPoint로 들어오고싶은 OHT가 StopPoint에서 대기중이면 들어오게 하는 함수
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="simLogs"></param>
        /// <param name="resetPoint"></param>
        public bool ProcessReservation(Time simTime, OHTPoint resetPoint)
        {

            if (_stopOHTs.Count == 0)
                return false;

            foreach (KeyValuePair<Tuple<string, string>, List<OHT>> kvpOHTs in _routeOHTs)
            {
                if (kvpOHTs.Key.Item2 == resetPoint.Name
                    && kvpOHTs.Value.Count > 0)
                    return false; //Stop은 다른데 Reset은 같은 OHT가 이미 안에 있는 경우 false
            }

            OHT fastestReservationStopOHT = null;
            Time reservationTime = -1;
            foreach (OHT stopOHT in _stopOHTs)
            {
                if (IsAvailableToEnter(stopOHT) && stopOHT.ReservationZcuResetPointName == resetPoint.Name)
                {
                    if (reservationTime == -1)
                    {
                        fastestReservationStopOHT = stopOHT;
                        reservationTime = stopOHT.ReservationTime;
                    }
                    else if (reservationTime > stopOHT.ReservationTime)
                    {
                        fastestReservationStopOHT = stopOHT;
                        reservationTime = stopOHT.ReservationTime;
                    }
                }
            }

            if (fastestReservationStopOHT == null)
                return false;

            OHTZCUReservation reservation = null;
            foreach (KeyValuePair<Tuple<string, string>, List<OHTZCUReservation>> kvpReservation in Reservations)
            {
                if (kvpReservation.Key.Item2 == resetPoint.Name)
                {
                    foreach (OHTZCUReservation tempReservation in kvpReservation.Value)
                    {
                        if (tempReservation.OHT.Name == fastestReservationStopOHT.Name)
                        {
                            if (IsAvailableToEnter(tempReservation.OHT))
                            {
                                reservation = tempReservation;
                                break;
                            }
                        }
                    }
                }
            }

            //예약이 있고, 예약 건 OHT이름이랑 StopPoint에 대기하고 있는 OHT랑 이름이 같을 때,
            if (reservation != null)
            {
                string newStopPointName = string.Empty;
                string newResetPointName = string.Empty;
                GetStopNResetPoint(reservation.OHT, ref newStopPointName, ref newResetPointName);

                OHTPoint stopPoint = reservation.StopPoint;

                if (stopPoint.StopPorts.Count == 0 || (stopPoint.StopPorts.Count > 0 && stopPoint.StopPorts[0].Object != null && stopPoint.StopPorts[0].Object.Name != reservation.OHT.Name))
                    return false;

                if (reservation.OHT != null && (reservation.OHT.State is VEHICLE_STATE.LOADING || reservation.OHT.State is VEHICLE_STATE.UNLOADING))
                    return false;

                stopPoint.StopPorts[0].PortType = EXT_PORT.OHT_IN;
                stopPoint.ExternalFunction(simTime, stopPoint.StopPorts[0]);

                return true;
            }

            return false;
        }

        public bool ProcessReservation(Time simTime)
        {
            OHTZCUReservation earlestReservation = null;
            OHTPoint earlestPoint = null;

            if (_stopOHTs.Count == 0)
                return false;

            OHT fastestReservationStopOHT = null;
            Time reservationTime = -1;
            string resetPointName = string.Empty;
            foreach (OHT stopOHT in _stopOHTs)
            {
                if (IsAvailableToEnter(stopOHT))
                {
                    if (reservationTime == -1)
                    {
                        fastestReservationStopOHT = stopOHT;
                        reservationTime = stopOHT.ReservationTime;
                        resetPointName = stopOHT.ReservationZcuResetPointName;
                    }
                    else if (reservationTime > stopOHT.ReservationTime)
                    {
                        fastestReservationStopOHT = stopOHT;
                        reservationTime = stopOHT.ReservationTime;
                        resetPointName = stopOHT.ReservationZcuResetPointName;
                    }
                }
            }

            if (fastestReservationStopOHT == null)
                return false;


            foreach (KeyValuePair<Tuple<string, string>, List<OHT>> kvpOHTs in _routeOHTs)
            {
                if (kvpOHTs.Key.Item2 == resetPointName
                    && kvpOHTs.Value.Count > 0)
                    return false; //Stop은 다른데 Reset은 같은 OHT가 이미 안에 있는 경우 false
            }

            OHTZCUReservation reservation = null;
            foreach (KeyValuePair<Tuple<string, string>, List<OHTZCUReservation>> kvpReservation in Reservations)
            {
                if (kvpReservation.Key.Item2 == resetPointName)
                {
                    foreach (OHTZCUReservation tempReservation in kvpReservation.Value)
                    {
                        if (tempReservation.OHT.Name == fastestReservationStopOHT.Name)
                        {
                            reservation = tempReservation;
                            break;
                        }
                    }
                }
            }

            //예약이 있고, 예약 건 OHT이름이랑 StopPoint에 대기하고 있는 OHT랑 이름이 같을 때,
            if (reservation != null)
            {
                string newStopPointName = string.Empty;
                string newResetPointName = string.Empty;
                GetStopNResetPoint(reservation.OHT, ref newStopPointName, ref newResetPointName);

                OHTPoint stopPoint = reservation.StopPoint;

                if (stopPoint.StopPorts.Count == 0 || (stopPoint.StopPorts.Count > 0 && stopPoint.StopPorts[0].Object != null && stopPoint.StopPorts[0].Object.Name != reservation.OHT.Name))
                    return false;

                if (reservation.OHT != null && (reservation.OHT.State is VEHICLE_STATE.LOADING || reservation.OHT.State is VEHICLE_STATE.UNLOADING))
                    return false;

                stopPoint.StopPorts[0].PortType = EXT_PORT.OHT_IN;
                stopPoint.ExternalFunction(simTime, stopPoint.StopPorts[0]);

                return true;
            }

            return false;
        }

        public void AddReservation(Time time, OHT oht, OHTPoint stopPoint)
        {
            try
            {
                string stopPointName = string.Empty;
                string resetPointName = string.Empty;
                GetStopNResetPoint(oht, ref stopPointName, ref resetPointName);
                Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);
                OHTPoint resetPoint = Ocs.DicRailPoint[resetPointName];
                OHTZCUReservation reservation = new OHTZCUReservation(time, oht, stopPoint, resetPoint);

                //이미 있으면 추가 하지 않음.
                OHTZCUReservation pastOHTReservation = null;
                foreach (List<OHTZCUReservation> listZcuReservation in _reservations.Values)
                {
                    foreach (OHTZCUReservation zcuReserv in listZcuReservation)
                    {
                        if (zcuReserv.OHT.Name == oht.Name && (zcuReserv.StopPoint.Name != stopPointName || zcuReserv.ResetPoint.Name != resetPointName))
                        {
                            pastOHTReservation = zcuReserv;
                            break;
                        }
                        else if (zcuReserv.OHT.Name == oht.Name && zcuReserv.StopPoint.Name == stopPointName && zcuReserv.ResetPoint.Name == resetPointName)
                            return;
                    }
                }

                //이전 OHT예약을 삭제
                if (pastOHTReservation != null)
                {
                    reservation.ReservationTime = pastOHTReservation.ReservationTime > reservation.ReservationTime ? reservation.ReservationTime : pastOHTReservation.ReservationTime;
                    Tuple<string, string> pastStopNreset = new Tuple<string, string>(pastOHTReservation.StopPoint.Name, pastOHTReservation.ResetPoint.Name);
                    Reservations[pastStopNreset].Remove(pastOHTReservation);
                    oht.ReservationZCUs.Remove(this);
                    if (oht.ReservationZCUs.Count == 0)
                    {
                        oht.ReservationTime = Time.Zero;
                        oht.ReservationZcuResetPointName = string.Empty;
                    }
                }

                Time backReservationTime = Time.MaxValue;
                foreach (List<OHTZCUReservation> listZcuReservation in _reservations.Values)
                {
                    foreach (OHTZCUReservation zcuReservation in listZcuReservation)
                    {
                        if (zcuReservation.StopPoint.Name != stopPointName)
                            continue;

                        bool isBackReservationOHT = false;

                        //먼저 예약된 OHT가 나보다 뒤쪽 라인에 있는 OHT인지 확인.
                        foreach (OHTLine fromLine in oht.Line.StartPoint.InLines)
                        {
                            if (zcuReservation.OHT.Line == fromLine)
                            {
                                isBackReservationOHT = true;
                                break;
                            }
                        }

                        //먼저 예약한 애가 같은 라인에 있는데 나보다 뒤쪽에 있거나 뒤쪽 라인에 있으면 
                        if ((zcuReservation.OHT.Line.Name == oht.Line.Name && zcuReservation.OHT.Line.GetDistanceAtTime(zcuReservation.OHT, time) < oht.Line.GetDistanceAtTime(oht, time) && zcuReservation.ReservationTime < reservation.ReservationTime
                            || (isBackReservationOHT && zcuReservation.ReservationTime < reservation.ReservationTime))
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

                if (oht.ReservationZCUs.Count == 0)
                {
                    oht.ReservationTime = reservation.ReservationTime;
                    oht.ReservationZcuResetPointName = resetPointName;
                }

                if (!oht.ReservationStopPoints.ContainsKey(reservation.StopPoint.ID))
                    oht.ReservationStopPoints.Add(reservation.StopPoint.ID, reservation.ReservationTime);
                else
                {
                    if (oht.ReservationStopPoints[reservation.StopPoint.ID] != reservation.ReservationTime)
                        oht.ReservationStopPoints[reservation.StopPoint.ID] = reservation.ReservationTime;
                }

                if (!oht.ReservationZCUs.Contains(this))
                    oht.ReservationZCUs.Add(this);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public bool RemoveReservation(OHT oht)
        {
            foreach (KeyValuePair<Tuple<string, string>, List<OHTZCUReservation>> kvpReservationList in _reservations)
            {
                foreach (OHTZCUReservation reservation in kvpReservationList.Value.ToList())
                {
                    if (reservation.OHT.Name == oht.Name)
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
                foreach (KeyValuePair<Tuple<string, string>, List<OHTZCUReservation>> kvpReservationList in _reservations)
                {
                    foreach (OHTZCUReservation reservation in kvpReservationList.Value)
                    {
                        if (reservation.OHT.Name == ohtName)
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

        public Time GetScheduleTimeOfFrontOHT(OHT oht)
        {
            Time scheduleTime = -1;
            string stopPointName = string.Empty;
            string resetPointName = string.Empty;
            GetStopNResetPoint(oht, ref stopPointName, ref resetPointName);
            Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);

            foreach (KeyValuePair<Tuple<string, string>, List<OHT>> kvpOHTs in _routeOHTs)
            {
                if (kvpOHTs.Key.Item2 == resetPointName && kvpOHTs.Value.Count > 0)
                {
                    OHT frontOHT = kvpOHTs.Value.Last();

                    if (Lines.Contains(frontOHT.Line) && frontOHT.Line.LstObjectIndex.ContainsKey(frontOHT.ID) && frontOHT.Line.LstPosData[frontOHT.Line.LstObjectIndex[frontOHT.ID]].Count > 0)
                    {
                        scheduleTime = frontOHT.Line.LstPosData[frontOHT.Line.LstObjectIndex[frontOHT.ID]].Last()._endTime;
                        break;
                    }
                }
            }


            foreach (KeyValuePair<Tuple<string, string>, List<OHTZCUReservation>> kvpReservation in Reservations)
            {
                if (kvpReservation.Key.Item1 != stopPointName && kvpReservation.Key.Item2 == resetPointName)
                {
                    List<OHTZCUReservation> compReservationList = kvpReservation.Value;
                    if (compReservationList.Count > 0)
                    {
                        OHT frontOHT = compReservationList[0].OHT;

                        if (frontOHT.Line.LstObjectIndex.ContainsKey(frontOHT.ID) && oht.ReservationTime > frontOHT.ReservationTime && frontOHT.Line.LstPosData[frontOHT.Line.LstObjectIndex[frontOHT.ID]].Count > 0)
                        {
                            Time tempEndTime = frontOHT.Line.LstPosData[frontOHT.Line.LstObjectIndex[frontOHT.ID]].Last()._endTime;

                            scheduleTime = tempEndTime < scheduleTime ? scheduleTime : tempEndTime;
                        }
                    }
                }
            }

            return scheduleTime;
        }
    }
}
