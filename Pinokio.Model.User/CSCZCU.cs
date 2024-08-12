using Logger;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pinokio.Model.User
{
    //public enum ZCU_TYPE
    //{
    //    NON, STOP, RESET
    //}

    /// <summary>
    /// ZCU의 기능
    /// Stop, Reset Point가 모두 같으면 후발 CSC도 같이 지나갈 수 있다.
    /// Stop은 같은데 Reset은 다르면 후발 CSC도 같이 지나갈 수 있다.
    /// Stop도 다르고 Reset도 다르면 후발 CSC도 같이 지나갈 수 있다.
    /// Stop은 다르고 Reset은 같으면 후발 CSC가 Stop Point에서 기다렸다가 선발 CSC가 나가면 들어감.
    /// </summary>
    [Serializable]
    public class CSCZCU
    {
        private string _name;
        private Dictionary<string, CSCPoint> _fromPoints; // Key: stopPointName
        private Dictionary<string, CSCPoint> _toPoints; // Key: resetPointName
        private List<CSCLine> _lines;
        private List<CSC> _cscs;
        private Dictionary<Tuple<string, string>, List<CSCZCUReservation>> _reservations; //Key: stopPointName, resetPointName
        private Dictionary<Tuple<string, string>, List<CSCZCUReservation>> _routeReservations; //Key: stopPointName, resetPointName
        private Dictionary<Tuple<string, string>, List<CSC>> _routeCSCs; //Key: stopPointName, resetPointName
        private List<CSC> _stopCSCs;
        private CSCCS _csccs;
        public string Name
        {
            get { return _name; }
        }

        public CSCCS CSCcs
        {
            get { return _csccs; }
        }

        public List<CSC> Cscs
        {
            get
            {
                return _cscs;
            }

            set
            {
                _cscs = value;
            }
        }

        public bool IsBusy
        {
            get
            {
                if (_cscs.Count > 0)
                    return true;
                else
                    return false;
            }
        }
        public Dictionary<string, CSCPoint> FromPoints
        {
            get { return _fromPoints; }
        }

        public Dictionary<string, CSCPoint> ToPoints
        {
            get { return _toPoints; }
        }

        public List<CSCLine> Lines
        {
            get { return _lines; }
        }

        public Dictionary<Tuple<string, string>, List<CSCZCUReservation>> Reservations
        {
            get { return _reservations; }
        }

        public Dictionary<Tuple<string, string>, List<CSCZCUReservation>> RouteReservations
        {
            get { return _routeReservations; }
        }

        public Dictionary<Tuple<string, string>, List<CSC>> RouteCSCs
        {
            get { return _routeCSCs; }
            set { _routeCSCs = value; }
        }
        public List<CSC> StopCSCs
        {
            get
            {
                return _stopCSCs;
            }
            set
            {
                _stopCSCs = value;
            }
        }


        public CSCZCU(string name, CSCCS csccs)
        {
            _name = name;
            _csccs = csccs;
            _cscs = new List<CSC>();
            _fromPoints = new Dictionary<string, CSCPoint>();
            _toPoints = new Dictionary<string, CSCPoint>();
            _lines = new List<CSCLine>();
            _reservations = new Dictionary<Tuple<string, string>, List<CSCZCUReservation>>();
            _routeReservations = new Dictionary<Tuple<string, string>, List<CSCZCUReservation>>();
            _routeCSCs = new Dictionary<Tuple<string, string>, List<CSC>>();
            _stopCSCs = new List<CSC>();
        }

        public void SetLines()
        {
            try
            {
                _lines.Clear();
                _reservations.Clear();
                _routeReservations.Clear();
                _routeCSCs.Clear();

                foreach (CSCPoint inPoint in _fromPoints.Values)
                {
                    List<CSCPoint> railPointNodes = new List<CSCPoint>();
                    List<CSCLine> lines = new List<CSCLine>();
                    InitializeReservationNcscCount(inPoint, railPointNodes, lines, inPoint.Name);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void InitializeReservationNcscCount(CSCPoint currentPoint, List<CSCPoint> visitedPoints, List<CSCLine> zoneOutLines, string stopPointName)
        {

            try
            {
                visitedPoints.Add(currentPoint);
                List<CSCLine> nextLines = currentPoint.OutLines.Where(line => !zoneOutLines.Contains(line)).ToList().ConvertAll(x => (CSCLine)x);

                foreach (CSCLine nextLine in nextLines)
                {
                    Lines.Add(nextLine);
                    nextLine.Zcu = this;

                    foreach (CSCLineStation railPort in nextLine.LineStations)
                    {
                        if (railPort.WaitAllowed)
                        {
                            railPort.WaitAllowed = false;
                            nextLine.DicWaitingRailPort.Remove(railPort);
                        }
                    }
                }
                List<CSCPoint> nextNodes = nextLines.Select(line => line.EndPoint).ToList().ConvertAll(x=>(CSCPoint)x);

                while (nextNodes.Any())
                {
                    CSCPoint nextNode = nextNodes.First();

                    nextNodes.Remove(nextNode);

                    if (nextNode.ZcuName == Name && nextNode.ZcuType == ZCU_TYPE.RESET && nextNode.OutLines.Count > 0)
                    {

                        CSCPoint resetFindingNode = nextNode.OutLines[0].EndPoint as CSCPoint;
                        List<CSCLine> additionalLines = new List<CSCLine>();
                        additionalLines.Add(nextNode.OutLines[0] as CSCLine);
                        while (resetFindingNode.ZcuType != ZCU_TYPE.RESET)
                        {
                            if (resetFindingNode.OutLines.Count == 0)
                                break;
                            resetFindingNode = resetFindingNode.OutLines[0].EndPoint as CSCPoint;
                            if (resetFindingNode.OutLines.Count == 0)
                                break;

                            if (additionalLines.Count > CSCcs.DicRailLine.Count)
                                break;

                            additionalLines.Add(resetFindingNode.OutLines[0] as CSCLine);
                        }

                        if (!ToPoints.ContainsKey(nextNode.Name))
                            ToPoints.Add(nextNode.Name, nextNode);

                        Tuple<string, string> key = new Tuple<string, string>(stopPointName, nextNode.Name);

                        if (!Reservations.ContainsKey(key))
                        {
                            Reservations.Add(key, new List<CSCZCUReservation>());
                            RouteReservations.Add(key, new List<CSCZCUReservation>());
                            RouteCSCs.Add(key, new List<CSC>());
                            Lines.AddRange(zoneOutLines);

                            foreach (CSCLine line in zoneOutLines)
                            {
                                foreach (CSCLineStation railPort in line.LineStations)
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
                            Reservations.Add(new Tuple<string, string>(stopPointName, stopPointName), new List<CSCZCUReservation>());
                            RouteReservations.Add(new Tuple<string, string>(stopPointName, stopPointName), new List<CSCZCUReservation>());
                            RouteCSCs.Add(new Tuple<string, string>(stopPointName, stopPointName), new List<CSC>());
                        }
                    }
                    else if (!visitedPoints.Contains(nextNode))
                    {
                        InitializeReservationNcscCount(nextNode, visitedPoints, zoneOutLines, stopPointName);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void AddCSC(CSC csc)
        {
            try
            {
                string stopPointName = string.Empty;
                string resetPointName = string.Empty;
                GetStopNResetPoint(csc, ref stopPointName, ref resetPointName);
                Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);

                if (!_routeCSCs[stopNreset].Contains(csc))
                    _routeCSCs[stopNreset].Add(csc);

                csc.CurZcu = this;
                csc.CurZcuResetPointName = resetPointName;
                RemoveReservation(csc);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void RemoveCSC(CSC csc)
        {
            try
            {
                foreach (KeyValuePair<Tuple<string, string>, List<CSC>> kvpRouteCSCList in RouteCSCs)
                {
                    if (kvpRouteCSCList.Value.Contains(csc))
                    {
                        kvpRouteCSCList.Value.Remove(csc);

                        foreach (CSCZCUReservation zcuReservation in RouteReservations[kvpRouteCSCList.Key].ToArray())
                        {
                            if (zcuReservation.CSC == csc)
                                RouteReservations[kvpRouteCSCList.Key].Remove(zcuReservation);
                        }
                    }
                }

                csc.CurZcu = null;
                csc.CurZcuResetPointName = string.Empty;

                RemoveOutCSCs();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void RemoveOutCSCs()
        {
            try
            {
                foreach (List<CSC> routeCSCList in RouteCSCs.Values)
                {
                    int idx = 0;
                    while (idx < routeCSCList.Count)
                    {
                        CSC routeCSC = routeCSCList[idx];

                        if (!this.Lines.Contains(routeCSC.Line))
                            routeCSCList.Remove(routeCSC);
                        else
                            ++idx;
                    }
                }
            }
            catch (Exception ex) {
                ErrorLogger.SaveLog(ex);
            }
        }

        /// Stop, Reset Point가 모두 같고 다른 Stop, 같은 Reset Point인 예약이 없으면 후발 CSC도 같이 지나갈 수 있다.
        /// Stop은 같은데 Reset은 다르면 후발 CSC도 같이 지나갈 수 있다.
        /// Stop도 다르고 Reset도 다르면 후발 CSC도 같이 지나갈 수 있다.
        /// Stop은 다르고 Reset은 같으면 후발 CSC가 Stop Point에서 기다렸다가 선발 CSC가 나가면 들어감.
        /// StopPoint에 도착해서 들어갈 수 있는지 확인하는 함수
        public bool IsAvailableToEnter(CSC csc)
        {
            try
            {
                string stopPointName = string.Empty;
                string resetPointName = string.Empty;
                GetStopNResetPoint(csc, ref stopPointName, ref resetPointName);
                Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);

                if (CSCcs.DicRailPoint.ContainsKey(stopPointName))
                {
                    GetStopNResetPoint(csc, ref stopPointName, ref resetPointName);
                }
                if (stopPointName == string.Empty)
                    return false;
                CSCPoint stopPoint = CSCcs.DicRailPoint[stopPointName];
                CSCPoint resetPoint = CSCcs.DicRailPoint[resetPointName];
                bool isSameRouteCSC = false;

                foreach (KeyValuePair<Tuple<string, string>, List<CSC>> kvpCSCs in _routeCSCs)
                {
                    if (kvpCSCs.Key.Item1 != stopPointName
                        && kvpCSCs.Key.Item2 == resetPointName
                        && kvpCSCs.Value.Count > 0)
                    {
                        return false; //Reset 같은 CSC가 이미 안에 있는 경우 false
                    }
                    else if (kvpCSCs.Key.Item1 == stopPointName
                        && kvpCSCs.Key.Item2 == resetPointName
                        && kvpCSCs.Value.Count > 0)
                    {
                        isSameRouteCSC = true;

                        foreach (CSCLine fromLine in resetPoint.InLines)
                        {
                            if (fromLine.StartPoint.Name == stopPointName)
                                return false; //Stop, reset이 같은데 중간에 via Point가 없으면 단독 진입
                        }
                    }
                }

                CSCZCUReservation cscReservation = null;

                foreach (CSCZCUReservation reservation in Reservations[stopNreset])
                {
                    if (reservation.CSC.Name == csc.Name)
                        cscReservation = reservation;
                }

                Time curTime = SimEngine.Instance.TimeNow;

                foreach (KeyValuePair<Tuple<string, string>, List<CSCZCUReservation>> kvpReservation in Reservations)
                {
                    List<CSCZCUReservation> compReservationList = kvpReservation.Value;

                    // Reset 같은 경우
                    if (kvpReservation.Key.Item1 != stopPointName && kvpReservation.Key.Item2 == resetPointName)
                    {
                        CSCPoint compStopPoint = CSCcs.DicRailPoint[kvpReservation.Key.Item1];

                        if (cscReservation == null && compReservationList.Count > 0)
                        {
                            return false;
                        }
                        else if (compStopPoint.InLines.Count > 0 && resetPoint.OutLines.Count > 0)
                        {
                            if (compReservationList.Count > 0 && compReservationList[0].ReservationTime < cscReservation.ReservationTime)
                            {
                                int compCSCSequence = compReservationList[0].CSC.Line.LstObjectIndex[compReservationList[0].CSC.ID];
                                CSC compCSC = compReservationList[0].CSC;
                                List<PosData> compCSCPosData = compCSC.Line.LstPosData[compCSCSequence];

                                int cscSequence = csc.Line.LstObjectIndex[csc.ID];
                                List<PosData> cscPosData = csc.Line.LstPosData[cscSequence];

                                // 나는 이미 Stop Point에 도착했는데, 같은 경로에 먼저 가는 CSC가 없고 다른 경로에서 먼저 예약한 CSC가 현재보다 더 멈춰있는 스케줄이 있을 경우 먼저 지나감.
                                if (cscPosData.Count > 0 && compCSCPosData.Count > 0 && Math.Round(cscPosData.Last()._endPos, 0) >= Math.Round(csc.Line.Length, 0) && !isSameRouteCSC
                                    && (!(compCSC.State is VEHICLE_STATE.LOADING || compCSC.State is VEHICLE_STATE.UNLOADING) && compCSCPosData.Last()._endSpeed == 0)
                                    && (compCSCPosData.Last()._endTime > curTime || Math.Round(compCSCPosData.Last()._endPos, 0) < Math.Round(compCSC.Line.Length, 0)))
                                    return true;
                                else //  이외의 모든 상황은 못 지나감.
                                    return false;
                            }
                            else if (compReservationList.Count > 0 && compReservationList[0].ReservationTime == cscReservation.ReservationTime)
                            {
                                int cscIndex = CSCcs.Vehicles.IndexOf(csc);
                                int compCscIndex = CSCcs.Vehicles.IndexOf(compReservationList[0].CSC);

                                if (cscIndex > compCscIndex)
                                    return false;   // 같은 시간에 예약했다면, CSC ID 번호가 느린쪽이 false;
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

        public bool KeepGoing(CSC csc)
        {
            try
            {
                string stopPointName = string.Empty;
                string resetPointName = string.Empty;
                GetStopNResetPoint(csc, ref stopPointName, ref resetPointName);
                Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);

                if (!CSCcs.DicRailPoint.ContainsKey(stopPointName))
                {
                    GetStopNResetPoint(csc, ref stopPointName, ref resetPointName);
                }
                if (stopPointName == string.Empty)
                    return false;
                CSCPoint stopPoint = CSCcs.DicRailPoint[stopPointName];
                CSCPoint resetPoint = CSCcs.DicRailPoint[resetPointName];
                bool isSameRouteCSC = false;

                foreach (KeyValuePair<Tuple<string, string>, List<CSC>> kvpCSCs in _routeCSCs)
                {
                    if (kvpCSCs.Value.Count > 0)
                    {
                        return false; //Reset 같은 CSC가 이미 안에 있는 경우 false
                    }
                }

                CSCZCUReservation cscReservation = null;

                foreach (CSCZCUReservation reservation in Reservations[stopNreset])
                {
                    if (reservation.CSC.Name == csc.Name)
                        cscReservation = reservation;
                }

                if (cscReservation == null)
                    return false;

                Time curTime = SimEngine.Instance.TimeNow;

                foreach (KeyValuePair<Tuple<string, string>, List<CSCZCUReservation>> kvpReservation in Reservations)
                {
                    List<CSCZCUReservation> compReservationList = kvpReservation.Value;

                    if (compReservationList.Count > 0)
                    {
                        if (compReservationList[0].CSC.Name != csc.Name)
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

        public bool GetStopNResetPoint(CSC csc, ref string stopPointName, ref string resetPointName)
        {
            // csc
            foreach (CSCLine line in csc.Route)
            {
                if (line.Zcu != null && line.ZcuName == this.Name)
                {
                    stopPointName = GetStopPoint(line);
                    resetPointName = GetResetPoint(line);
                    return true;
                }
            }

            CSCLine searchLine = csc.Line;
            while (searchLine.ZcuName != this.Name)
            {
                CSCLine sameDirectionToLine = null;
                double minimumDirectionError = double.MaxValue;
                foreach (CSCLine toLine in searchLine.EndPoint.OutLines)
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

        private string GetStopPoint(CSCLine line)
        {
            if (line.Zcu == null)
            {
                return string.Empty;
            }

            if (line.Zcu != this)
            {
                return string.Empty;
            }

            if (((CSCPoint)line.StartPoint).Zcu != null)
            {
                if (((CSCPoint)line.StartPoint).ZcuType == ZCU_TYPE.STOP)
                {
                    return line.StartPoint.Name;
                }
            }

            if (line.StartPoint.InLines.Count == 1)
            {

                return GetStopPoint(line.StartPoint.InLines[0] as CSCLine);
            }
            else
            {
                //    Console.WriteLine("ZCU Error 발생  ZCU: " + line.Zcu.Name + "  Reset Stop 사이 분기 존재");
                foreach (CSCLine fromLine in line.StartPoint.InLines)
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

        private string GetResetPoint(CSCLine line)
        {
            if (line.Zcu == null)
            {
                return string.Empty;
            }

            if (line.Zcu != this)
            {
                return string.Empty;
            }

            if (((CSCPoint)line.EndPoint).Zcu != null)
            {
                if (((CSCPoint)line.EndPoint).ZcuType == ZCU_TYPE.RESET)
                {
                    return line.EndPoint.Name;
                }
            }

            if (line.EndPoint.OutLines.Count == 1)
            {
                return GetResetPoint(line.EndPoint.OutLines[0] as CSCLine);
            }
            else
            {
                // Console.WriteLine("ZCU Error 발생  ZCU: " + line.Zcu.Name + "  Reset Stop 사이 분기 존재");
                foreach (CSCLine toLine in line.EndPoint.OutLines)
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
        /// <param name="csc"></param>
        public void ChangeReservationResetPoint(Time simTime, CSC csc)
        {
            try
            {
                Tuple<string, string> oldKey = null;
                CSCZCUReservation reservation = null;
                foreach (KeyValuePair<Tuple<string, string>, List<CSCZCUReservation>> kvpReservation in Reservations)
                {
                    foreach (CSCZCUReservation reserTemp in kvpReservation.Value)
                    {
                        if (reserTemp.CSC.Name == csc.Name)
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

                GetStopNResetPoint(csc, ref newStopPointName, ref newResetPointName);

                if (newStopPointName == string.Empty || newResetPointName == string.Empty)
                    return;

                CSCPoint newStopPoint = CSCcs.DicRailPoint[newStopPointName];
                CSCPoint newResetPoint = CSCcs.DicRailPoint[newResetPointName];

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
                        csc.ReservationZcuResetPointName = newResetPointName;
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

        public bool ContainsReservation(CSC csc)
        {
            if (csc.Name == "CSC_131")
                ;

            string stopPointName = string.Empty;
            string resetPointName = string.Empty;
            GetStopNResetPoint(csc, ref stopPointName, ref resetPointName);

            if (stopPointName == string.Empty && resetPointName == string.Empty)
                return false;

            if (!Reservations.ContainsKey(new Tuple<string, string>(stopPointName, resetPointName)))
            {
                GetStopNResetPoint(csc, ref stopPointName, ref resetPointName);
            }

            foreach (CSCZCUReservation reservation in Reservations[new Tuple<string, string>(stopPointName, resetPointName)])
            {
                if (csc.Name == reservation.CSC.Name)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// resetPoint에서 CSC가 나갈 때
        /// 해당 resetPoint로 이동중인 CSC가 없고,
        /// 해당 resetPoint로 들어오고싶은 CSC가 StopPoint에서 대기중이면 들어오게 하는 함수
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="simLogs"></param>
        /// <param name="resetPoint"></param>
        public bool ProcessReservation(Time simTime, CSCPoint resetPoint)
        {

            if (_stopCSCs.Count == 0)
                return false;

            foreach (KeyValuePair<Tuple<string, string>, List<CSC>> kvpCSCs in _routeCSCs)
            {
                if (kvpCSCs.Key.Item2 == resetPoint.Name
                    && kvpCSCs.Value.Count > 0)
                    return false; //Stop은 다른데 Reset은 같은 CSC가 이미 안에 있는 경우 false
            }

            CSC fastestReservationStopCSC = null;
            Time reservationTime = -1;
            foreach (CSC stopCSC in _stopCSCs)
            {
                if (IsAvailableToEnter(stopCSC) && stopCSC.ReservationZcuResetPointName == resetPoint.Name)
                {
                    if (reservationTime == -1)
                    {
                        fastestReservationStopCSC = stopCSC;
                        reservationTime = stopCSC.ReservationTime;
                    }
                    else if (reservationTime > stopCSC.ReservationTime)
                    {
                        fastestReservationStopCSC = stopCSC;
                        reservationTime = stopCSC.ReservationTime;
                    }
                }
            }

            if (fastestReservationStopCSC == null)
                return false;

            CSCZCUReservation reservation = null;
            foreach (KeyValuePair<Tuple<string, string>, List<CSCZCUReservation>> kvpReservation in Reservations)
            {
                if (kvpReservation.Key.Item2 == resetPoint.Name)
                {
                    foreach (CSCZCUReservation tempReservation in kvpReservation.Value)
                    {
                        if (tempReservation.CSC.Name == fastestReservationStopCSC.Name)
                        {
                            if (IsAvailableToEnter(tempReservation.CSC))
                            {
                                reservation = tempReservation;
                                break;
                            }
                        }
                    }
                }
            }

            //예약이 있고, 예약 건 CSC이름이랑 StopPoint에 대기하고 있는 CSC랑 이름이 같을 때,
            if (reservation != null)
            {
                string newStopPointName = string.Empty;
                string newResetPointName = string.Empty;
                GetStopNResetPoint(reservation.CSC, ref newStopPointName, ref newResetPointName);

                CSCPoint stopPoint = reservation.StopPoint;

                if (stopPoint.StopPorts.Count == 0 || (stopPoint.StopPorts.Count > 0 && stopPoint.StopPorts[0].Object != null && stopPoint.StopPorts[0].Object.Name != reservation.CSC.Name))
                    return false;

                if (reservation.CSC != null && (reservation.CSC.State is VEHICLE_STATE.LOADING || reservation.CSC.State is VEHICLE_STATE.UNLOADING))
                    return false;

                stopPoint.StopPorts[0].PortType = EXT_PORT.CSC_IN;
                stopPoint.ExternalFunction(simTime, stopPoint.StopPorts[0]);

                return true;
            }

            return false;
        }

        public bool ProcessReservation(Time simTime)
        {
            CSCZCUReservation earlestReservation = null;
            CSCPoint earlestPoint = null;

            if (_stopCSCs.Count == 0)
                return false;

            CSC fastestReservationStopCSC = null;
            Time reservationTime = -1;
            string resetPointName = string.Empty;
            foreach (CSC stopCSC in _stopCSCs)
            {
                if (IsAvailableToEnter(stopCSC))
                {
                    if (reservationTime == -1)
                    {
                        fastestReservationStopCSC = stopCSC;
                        reservationTime = stopCSC.ReservationTime;
                        resetPointName = stopCSC.ReservationZcuResetPointName;
                    }
                    else if (reservationTime > stopCSC.ReservationTime)
                    {
                        fastestReservationStopCSC = stopCSC;
                        reservationTime = stopCSC.ReservationTime;
                        resetPointName = stopCSC.ReservationZcuResetPointName;
                    }
                }
            }

            if (fastestReservationStopCSC == null)
                return false;


            foreach (KeyValuePair<Tuple<string, string>, List<CSC>> kvpCSCs in _routeCSCs)
            {
                if (kvpCSCs.Key.Item2 == resetPointName
                    && kvpCSCs.Value.Count > 0)
                    return false; //Stop은 다른데 Reset은 같은 CSC가 이미 안에 있는 경우 false
            }

            CSCZCUReservation reservation = null;
            foreach (KeyValuePair<Tuple<string, string>, List<CSCZCUReservation>> kvpReservation in Reservations)
            {
                if (kvpReservation.Key.Item2 == resetPointName)
                {
                    foreach (CSCZCUReservation tempReservation in kvpReservation.Value)
                    {
                        if (tempReservation.CSC.Name == fastestReservationStopCSC.Name)
                        {
                            reservation = tempReservation;
                            break;
                        }
                    }
                }
            }

            //예약이 있고, 예약 건 CSC이름이랑 StopPoint에 대기하고 있는 CSC랑 이름이 같을 때,
            if (reservation != null)
            {
                string newStopPointName = string.Empty;
                string newResetPointName = string.Empty;
                GetStopNResetPoint(reservation.CSC, ref newStopPointName, ref newResetPointName);

                CSCPoint stopPoint = reservation.StopPoint;

                if (stopPoint.StopPorts.Count == 0 || (stopPoint.StopPorts.Count > 0 && stopPoint.StopPorts[0].Object != null && stopPoint.StopPorts[0].Object.Name != reservation.CSC.Name))
                    return false;

                if (reservation.CSC != null && (reservation.CSC.State is VEHICLE_STATE.LOADING || reservation.CSC.State is VEHICLE_STATE.UNLOADING))
                    return false;

                stopPoint.StopPorts[0].PortType = EXT_PORT.CSC_IN;
                stopPoint.ExternalFunction(simTime, stopPoint.StopPorts[0]);

                return true;
            }

            return false;
        }

        public void AddReservation(Time time, CSC csc, CSCPoint stopPoint)
        {
            try
            {
                string stopPointName = string.Empty;
                string resetPointName = string.Empty;
                GetStopNResetPoint(csc, ref stopPointName, ref resetPointName);
                Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);
                CSCPoint resetPoint = CSCcs.DicRailPoint[resetPointName];
                CSCZCUReservation reservation = new CSCZCUReservation(time, csc, stopPoint, resetPoint);

                //이미 있으면 추가 하지 않음.
                CSCZCUReservation pastCSCReservation = null;
                foreach (List<CSCZCUReservation> listZcuReservation in _reservations.Values)
                {
                    foreach (CSCZCUReservation zcuReserv in listZcuReservation)
                    {
                        if (zcuReserv.CSC.Name == csc.Name && (zcuReserv.StopPoint.Name != stopPointName || zcuReserv.ResetPoint.Name != resetPointName))
                        {
                            pastCSCReservation = zcuReserv;
                            break;
                        }
                        else if (zcuReserv.CSC.Name == csc.Name && zcuReserv.StopPoint.Name == stopPointName && zcuReserv.ResetPoint.Name == resetPointName)
                            return;
                    }
                }

                //이전 CSC예약을 삭제
                if (pastCSCReservation != null)
                {
                    reservation.ReservationTime = pastCSCReservation.ReservationTime > reservation.ReservationTime ? reservation.ReservationTime : pastCSCReservation.ReservationTime;
                    Tuple<string, string> pastStopNreset = new Tuple<string, string>(pastCSCReservation.StopPoint.Name, pastCSCReservation.ResetPoint.Name);
                    Reservations[pastStopNreset].Remove(pastCSCReservation);
                    csc.ReservationZCUs.Remove(this);
                    if (csc.ReservationZCUs.Count == 0)
                    {
                        csc.ReservationTime = Time.Zero;
                        csc.ReservationZcuResetPointName = string.Empty;
                    }
                }

                Time backReservationTime = Time.MaxValue;
                foreach (List<CSCZCUReservation> listZcuReservation in _reservations.Values)
                {
                    foreach (CSCZCUReservation zcuReservation in listZcuReservation)
                    {
                        if (zcuReservation.StopPoint.Name != stopPointName)
                            continue;

                        bool isBackReservationCSC = false;

                        //먼저 예약된 CSC가 나보다 뒤쪽 라인에 있는 CSC인지 확인.
                        foreach (CSCLine fromLine in csc.Line.StartPoint.InLines)
                        {
                            if (zcuReservation.CSC.Line == fromLine)
                            {
                                isBackReservationCSC = true;
                                break;
                            }
                        }

                        //먼저 예약한 애가 같은 라인에 있는데 나보다 뒤쪽에 있거나 뒤쪽 라인에 있으면 
                        if ((zcuReservation.CSC.Line.Name == csc.Line.Name && zcuReservation.CSC.Line.GetDistanceAtTime(zcuReservation.CSC, time) < csc.Line.GetDistanceAtTime(csc, time) && zcuReservation.ReservationTime < reservation.ReservationTime
                            || (isBackReservationCSC && zcuReservation.ReservationTime < reservation.ReservationTime))
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

                if (csc.ReservationZCUs.Count == 0)
                {
                    csc.ReservationTime = reservation.ReservationTime;
                    csc.ReservationZcuResetPointName = resetPointName;
                }

                if (!csc.ReservationStopPoints.ContainsKey(reservation.StopPoint.ID))
                    csc.ReservationStopPoints.Add(reservation.StopPoint.ID, reservation.ReservationTime);
                else
                {
                    if (csc.ReservationStopPoints[reservation.StopPoint.ID] != reservation.ReservationTime)
                        csc.ReservationStopPoints[reservation.StopPoint.ID] = reservation.ReservationTime;
                }

                if (!csc.ReservationZCUs.Contains(this))
                    csc.ReservationZCUs.Add(this);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public bool RemoveReservation(CSC csc)
        {
            foreach (KeyValuePair<Tuple<string, string>, List<CSCZCUReservation>> kvpReservationList in _reservations)
            {
                foreach (CSCZCUReservation reservation in kvpReservationList.Value.ToList())
                {
                    if (reservation.CSC.Name == csc.Name)
                    {
                        kvpReservationList.Value.Remove(reservation);
                        RouteReservations[kvpReservationList.Key].Add(reservation);

                        if (csc.ReservationZCUs.Count == 0)
                        {
                            csc.ReservationTime = Time.Zero;
                            csc.ReservationZcuResetPointName = string.Empty;
                        }
                        else if (csc.ReservationZCUs.Count == 1)
                        {
                            csc.ReservationZCUs.Remove(this);
                            csc.ReservationTime = Time.Zero;
                            csc.ReservationZcuResetPointName = string.Empty;
                        }
                        else if (csc.ReservationZCUs.Count > 1)
                        {
                            Time reservationTime = Time.Zero;
                            string resetPointName = string.Empty;
                            csc.ReservationZCUs.Remove(this);
                            csc.ReservationZCUs[0].getReservationInfo(csc.Name, ref reservationTime, ref resetPointName);
                            csc.ReservationTime = reservationTime;
                            csc.ReservationZcuResetPointName = resetPointName;
                        }
                        csc.ReservationStopPoints.Remove(reservation.StopPoint.ID);

                        return true;
                    }
                }
            }

            return false;
        }

        public void getReservationInfo(string cscName, ref Time time, ref string resetPointName)
        {
            try
            {
                foreach (KeyValuePair<Tuple<string, string>, List<CSCZCUReservation>> kvpReservationList in _reservations)
                {
                    foreach (CSCZCUReservation reservation in kvpReservationList.Value)
                    {
                        if (reservation.CSC.Name == cscName)
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

        public Time GetScheduleTimeOfFrontCSC(CSC csc)
        {
            Time scheduleTime = -1;
            string stopPointName = string.Empty;
            string resetPointName = string.Empty;
            GetStopNResetPoint(csc, ref stopPointName, ref resetPointName);
            Tuple<string, string> stopNreset = new Tuple<string, string>(stopPointName, resetPointName);

            foreach (KeyValuePair<Tuple<string, string>, List<CSC>> kvpCSCs in _routeCSCs)
            {
                if (kvpCSCs.Key.Item2 == resetPointName && kvpCSCs.Value.Count > 0)
                {
                    CSC frontCSC = kvpCSCs.Value.Last();

                    if (Lines.Contains(frontCSC.Line) && frontCSC.Line.LstObjectIndex.ContainsKey(frontCSC.ID) && frontCSC.Line.LstPosData[frontCSC.Line.LstObjectIndex[frontCSC.ID]].Count > 0)
                    {
                        scheduleTime = frontCSC.Line.LstPosData[frontCSC.Line.LstObjectIndex[frontCSC.ID]].Last()._endTime;
                        break;
                    }
                }
            }


            foreach (KeyValuePair<Tuple<string, string>, List<CSCZCUReservation>> kvpReservation in Reservations)
            {
                if (kvpReservation.Key.Item1 != stopPointName && kvpReservation.Key.Item2 == resetPointName)
                {
                    List<CSCZCUReservation> compReservationList = kvpReservation.Value;
                    if (compReservationList.Count > 0)
                    {
                        CSC frontCSC = compReservationList[0].CSC;

                        if (frontCSC.Line.LstObjectIndex.ContainsKey(frontCSC.ID) && csc.ReservationTime > frontCSC.ReservationTime && frontCSC.Line.LstPosData[frontCSC.Line.LstObjectIndex[frontCSC.ID]].Count > 0)
                        {
                            Time tempEndTime = frontCSC.Line.LstPosData[frontCSC.Line.LstObjectIndex[frontCSC.ID]].Last()._endTime;

                            scheduleTime = tempEndTime < scheduleTime ? scheduleTime : tempEndTime;
                        }
                    }
                }
            }

            return scheduleTime;
        }
    }
}
