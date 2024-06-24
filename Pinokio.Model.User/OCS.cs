using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pinokio.Model.Base;
using Logger;

namespace Pinokio.Model.User
{
    public class OCS : VSubCS
    {
        private Dictionary<string, OHTLine> _dicRailLine;
        private Dictionary<string, OHTPoint> _dicRailPoint;
        private Dictionary<string, OHTZCU> _zcus;
        private Dictionary<string, BAY> _bays;

        [Browsable(true), StorableAttribute(true)]
        [CategoryAttribute("5. OCS Info"), DisplayName("1. Bump Distance")]
        public double BumpDistance { get; set; }

        [Browsable(true), StorableAttribute(true)]
        [CategoryAttribute("5. OCS Info"), DisplayName("2. Dispatching Interval Time")]
        public double DispatchingIntervalTime { get; set; }

        [Browsable(false)]
        public Dictionary<string, OHTLine> DicRailLine
        {
            get
            {
                return _dicRailLine;
            }
        }

        [Browsable(false)]
        public Dictionary<string, OHTPoint> DicRailPoint
        {
            get
            {
                return _dicRailPoint;
            }
        }

        [Browsable(false)]
        public Dictionary<string, OHTZCU> Zcus
        {
            get { return _zcus; }
        }
        public Dictionary<string, BAY> Bays
        {
            get { return _bays; }
        }
        [Browsable(true)]
        [CategoryAttribute("5. OCS Info"), DisplayName("3. Zcus")]
        public OHTZCU[] arrayZcus
        {
            get
            {
                if (_zcus != null)
                    return _zcus.Values.ToArray();
                else
                    return null;
            }
        }

        [Browsable(false), StorableAttribute(true)]
        public new static bool IsInserted = true;
        public OCS()
            : base()
        {
            Initialize();
        }

        public void Initialize()
        {
            BumpDistance = 8000;
            DispatchingIntervalTime = 5;
            _dicRailPoint = new Dictionary<string, OHTPoint>();
            _dicRailLine = new Dictionary<string, OHTLine>();
            _zcus = new Dictionary<string, OHTZCU>();
            _bays = new Dictionary<string, BAY>();
        }

        public override void InitializeNode(EventCalendar evtCal)
        {
            base.InitializeNode(evtCal);

            foreach (Vehicle oht in Vehicles)
            {
                SetIdleDestinationAndRoute(0, oht);
            }

            SimPort port = new SimPort(INT_PORT.DISPATCH);
            evtCal.AddEvent(0, this, port);
        }

        public override void InternalFunction(Time simTime, SimPort port)
        {
            base.InternalFunction(simTime, port);
            switch (port.PortType)
            {
                case INT_PORT.DISPATCH:
                    DispatchCommands(simTime);
                    EvtCalendar.AddEvent(simTime + DispatchingIntervalTime, this, port);
                    break;
            }
        }


        public OHTZCU AddZcu(string name)
        {
            OHTZCU zcu = new OHTZCU(name, this);

            Zcus.Add(zcu.Name, zcu);

            return zcu;
        }

        /// <summary>
        /// OCS에서 Node를 추가할 때 자동 실행되는 함수
        /// </summary>
        /// <param name="node"></param>
        public override void AddChildNode(SimNode node)
        {
            if (node is LineStation && LineStations.Contains(node) is false)
                LineStations.Add(node as LineStation);
            else if (node is Vehicle && Vehicles.Contains(node) is false)
                Vehicles.Add(node as Vehicle);
            else if (node is OHTLine && DicRailLine.ContainsKey(node.Name) is false)
                DicRailLine.Add(node.Name, node as OHTLine);
            else if (node is OHTPoint && DicRailPoint.ContainsKey(node.Name) is false)
            {
                DicRailPoint.Add(node.Name, node as OHTPoint);
                if (((OHTPoint)node).Zcu != null && Zcus.ContainsKey(((OHTPoint)node).Zcu.Name) is false)
                    Zcus.Add(((OHTPoint)node).Zcu.Name, ((OHTPoint)node).Zcu);
            }
        }

        /// <summary>
        /// OCS에서 Node를 삭제할 때 자동 실행되는 함수
        /// </summary>
        /// <param name="node"></param>
        public override void RemoveChildNode(SimNode node)
        {
            if (node is LineStation)
                LineStations.Remove(node as LineStation);
            else if (node is Vehicle)
                Vehicles.Remove(node as Vehicle);
            else if (node is OHTLine)
                DicRailLine.Remove(node.Name);
            else if (node is OHTPoint)
            {
                DicRailPoint.Remove(node.Name);
                if (((OHTPoint)node).Zcu != null && IsAniPoint4Zcu(((OHTPoint)node).Zcu) is false)
                    Zcus.Remove(((OHTPoint)node).ZcuName);
            }
        }

        protected bool IsAniPoint4Zcu(OHTZCU zcu)
        {
            foreach (OHTPoint point in DicRailPoint.Values)
            {
                if (point.ZcuName == zcu.Name)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Object가 C
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="port"></param>
        /// <param name="greetNode"></param>
        public override void ArriveAtCoupled(Time simTime, SimPort port, SimNode greetNode)
        {
            Part part = port.Object as Part;

            Command cmd = MCS.MakeCommand(port.FromNode as TXNode, greetNode as TXNode, part, this);

            cmd.StartStation = ((OHTLine)greetNode).InLinkNodeConnections[port.FromNode as TXNode];
            int endNodeIndex = part.MR.Route.IndexOf(cmd.EndNode);
            cmd.EndStation = ((OHTLine)part.MR.Route[endNodeIndex - 1]).OutLinkNodeConnections[cmd.EndNode];

            AddCommand(simTime, cmd);
        }

        public override void SetIdleDestinationAndRoute(Time simTime, Vehicle vehicle)
        {
            try
            {
                bool isStop = false;

                if (FactoryManager.Instance.Bays.Count() != 0)
                {
                    List<TransportLine> vehicleRoutes = new List<TransportLine>();

                    BAY currentBay = vehicle.Bay != "" && !vehicle.Bay.Contains("InterBay") ?
                                     FactoryManager.Instance.Bays[vehicle.Bay] : null;

                    if (currentBay == null) //InterBay 혹은 Bay가 아닌 경우
                    {
                        BAY findBay = FindLeastOccupiedBay();
                        vehicle.Destination = FindLineStation(findBay, vehicle, ref vehicleRoutes);
                        findBay.AddBumpingOHT(vehicle.Name, VEHICLE_STATE.IDLE);
                    }
                    else if (currentBay.BumpingOHTs.Count <= currentBay.IdleVehicleMaxCount) // Bay에 잔류 가능
                    {
                        if (currentBay.BumpingOHTs.ContainsKey(vehicle.Name) && currentBay.BumpingOHTs[vehicle.Name] == false) // 한번 BumpingStation에 왔었음. 정지. (이때는 도착했을 경우.
                        {
                            currentBay.BumpingOHTs[vehicle.Name] = true;
                            isStop = true;
                        }
                        else
                        {
                            currentBay.AddBumpingOHT(vehicle.Name, VEHICLE_STATE.IDLE); // 현재 Bay에서 처음으로 IDLE 상태. BumpingStation으로 최초 보냄.
                            currentBay.BumpingOHTs[vehicle.Name] = false;
                            vehicle.Destination = FindLineStation(currentBay, vehicle, ref vehicleRoutes);
                        }
                    }
                    else //가장 BumpingOHT가 적은 곳으로 이동
                    {
                        BAY destinationBay = FindAvailableNeighborBay(currentBay, vehicle) ?? FindLeastOccupiedBay();
                        currentBay.RemoveBumpingOHT(vehicle.Name);
                        vehicle.Destination = FindLineStation(destinationBay, vehicle, ref vehicleRoutes);
                        destinationBay.AddBumpingOHT(vehicle.Name, VEHICLE_STATE.IDLE);
                    }
                    // 경로 설정
                    if (isStop == true)
                    {
                        ((OHT)vehicle).IsStoped = true;
                        return;
                    }
                    else
                        ((OHT)vehicle).IsStoped = false;

                    if (vehicleRoutes.Count() != 0)
                        vehicle.Route = vehicleRoutes;
                    else
                        vehicle.Route = GetPath(simTime, vehicle);
                }
                else
                {
                    int iteration = 0;
                    BAY bay = null;
                    List<int> ranList = new List<int>();

                    LineStation lineStation = LineStations.Find(x => x != ((LineStation)vehicle.Destination));

                    vehicle.Destination = lineStation;
                    vehicle.Route = GetPath(simTime, vehicle);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private BAY FindAvailableNeighborBay(BAY currentBay, Vehicle vehicle)
        {
            double minOHT = double.MaxValue;
            BAY possibleNeighborBay = null;
            foreach (BAY neighborBay in currentBay.NeighborBay)
            {
                if (neighborBay.BumpingOHTs.Count < neighborBay.IdleVehicleMaxCount && neighborBay.BumpingOHTs.Count < minOHT)
                {
                    minOHT = neighborBay.BumpingOHTs.Count;
                    possibleNeighborBay = neighborBay;
                }
            }
            if (possibleNeighborBay != null)
                return possibleNeighborBay;
            else
                return SearchNearestBayBFS(currentBay, vehicle);
            return null;
        }
        public BAY SearchNearestBayBFS(BAY currentBay, Vehicle vehicle)
        {
            TransportLine line = vehicle.Line;
            Queue<TransportLine> checkLines = new Queue<TransportLine>();
            checkLines.Enqueue(line);
            Dictionary<uint, bool> isUsed = new Dictionary<uint, bool>();
            BAY possibleNeighborBay = null;

            while (checkLines.Count != 0)
            {
                TransportLine currentLine = checkLines.Dequeue();
                List<TransportLine> toLines = currentLine.EndPoint.OutLines;

                try
                {
                    foreach (TransportLine toLine in toLines)
                    {
                        if (isUsed.ContainsKey(toLine.ID))
                            continue;

                        if (((OHTLine)toLine).Bay != null && ((OHTLine)toLine).Bay != "" && !((OHTLine)toLine).Bay.Contains("Inter"))
                        {
                            BAY comparedBay = FactoryManager.Instance.Bays[((OHTLine)toLine).Bay];
                            if (comparedBay.BumpingStations.Count > 0 && ((OHTLine)line).Bay != ((OHTLine)toLine).Bay && comparedBay.BumpingOHTs.Count < comparedBay.IdleVehicleMaxCount)
                            {
                                possibleNeighborBay = comparedBay;
                                return possibleNeighborBay;
                            }
                        }
                        checkLines.Enqueue(toLine);
                        isUsed[toLine.ID] = true;
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.SaveLog(ex);
                }
            }
            return null;
        }

        private BAY FindLeastOccupiedBay()
        {
            return FactoryManager.Instance.Bays.Values.OrderBy(bay => bay.BumpingOHTs.Count).First();
        }

        private LineStation FindLineStation(BAY bay, Vehicle vehicle, ref List<TransportLine> RealRoute)
        {
            if (((OHTLine)vehicle.Route[0]).Bay != bay.Name)
            {
                return bay.BumpingStations.Keys.ToList().Last();
            }
            
            double dispatchDist = 0;

             if (vehicle.PosDatas.Count == 0)
                dispatchDist = vehicle.DispatchingDistance;
            else
                dispatchDist = vehicle.DispatchingDistance + vehicle.PosDatas.Last()._endPos;

            TransportLine line = null;
            if (vehicle.Destination != null)
            {
                LineStation station = vehicle.Destination as LineStation; 
                //line = bay.BumpingStations[station].Line;
                double stationLength = 0;
                TransportLine stationLine = vehicle.Destination.GetLineNStationLength(ref stationLength); 
                LineStation tempStation_A = station;//현재목적지
                LineStation tempStation_B = null;//다음목적지
                while (dispatchDist > stationLength)
                {
                    try
                    {
                        tempStation_B = bay.BumpingStations[tempStation_A];
                        if (tempStation_A.Line == tempStation_B.Line)
                        {
                            stationLength += tempStation_B.Length - tempStation_A.Length;
                        }
                        else
                            stationLength += tempStation_B.Length;

                        tempStation_A = tempStation_B;
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.SaveLog(ex);
                        tempStation_B = bay.BumpingStations.Keys.ToList().Last();
                        line = tempStation_B.Line;
                        RealRoute = CalculateRouteToCurrent(line, vehicle.Route[0], bay.Name);
                        return tempStation_B;
                    }
                }
                if (tempStation_B == null)
                    tempStation_B = bay.BumpingStations[tempStation_A];
                line = tempStation_B.Line;

                RealRoute = CalculateRouteToCurrent(line, vehicle.Route[0], bay.Name);
                return tempStation_B;
            }
            else
            {
                line = bay.BumpingStations.Keys.ToList().Last().Line;
                RealRoute = CalculateRouteToCurrent(line, vehicle.Route[0], bay.Name);
                return bay.BumpingStations.Keys.ToList().Last();
            }

        }

        private List<TransportLine> CalculateRouteToCurrent(TransportLine endLine, TransportLine curLine, string bayName)
        {
            List<TransportLine> tempRoute = new List<TransportLine>();
            TransportLine tempLine = endLine;

            tempRoute.Add(tempLine);
            while (tempLine != curLine)
            {
                foreach (TransportLine inputLine in tempLine.StartPoint.InLines)
                {
                    if (((OHTLine)inputLine).Bay != bayName)
                        continue;
                    tempRoute.Add(inputLine);
                    tempLine = inputLine;
                }
            }
            tempRoute.Reverse();
            return tempRoute;
        }

        /// <summary>
        /// Dispatching을 통해 선택된 최적의 Vehicle이 있을 경우 Command에 할당하는 함수. 
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="simLogs"></param>
        /// <param name="command"></param>
        /// <param name="vehicle"></param>
        protected override void AssignCommand(Time simTime, Command command, Vehicle vehicle)
        {
            LstReadyVehicle.Remove(vehicle);

            SetWaitingCommand(simTime, command);

            vehicle.Route = vehicle.CandidateRoute;
            vehicle.Command = command;
            vehicle.State = VEHICLE_STATE.MOVE_TO_LOAD;
            vehicle.Destination = command.StartStation;
            command.Vehicle = vehicle;
            command.AcquireRoute = vehicle.Route;

            foreach (TransportLine line in command.AcquireRoute)
            {
                if (line.Name == command.AcquireRoute[0].Name)
                    command.WaitingDistance += vehicle.Line.Length - vehicle.Distance;
                else if (line.Name != command.AcquireRoute.Last().Name)
                    command.WaitingDistance += line.Length;
            }
            TransportLine lineName = LineStations.FirstOrDefault(x => x.Name == vehicle.Destination.Name).Line;
            BAY bayss = FactoryManager.Instance.Bays.FirstOrDefault(kv => kv.Value.TransportLines.Contains(vehicle.Line)).Value;
            List<BAY> bays = FactoryManager.Instance.Bays.Values.Where(x => x.BumpingOHTs.ContainsKey(vehicle.Name)).ToList();
            foreach(BAY bay in bays)
            {
                bay.RemoveBumpingOHT(vehicle.Name);
            }
            if (bayss != null)
                bayss.RemoveBumpingOHT(vehicle.Name);

            SimPort port = new SimPort(EXT_PORT.MOVE, vehicle, this);
            vehicle.ExternalFunction(simTime, port);
        }
    }
}
