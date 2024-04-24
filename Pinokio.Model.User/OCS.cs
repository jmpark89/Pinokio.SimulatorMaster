using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pinokio.Model.Base;

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
            BumpDistance = 4900;
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
            int iteration = 0;
            BAY bay = null;
            List<int> ranList = new List<int>();
            try
            {
                if (FactoryManager.Instance.Bays.Count() != 0)
                {
                    while (bay == null)
                    {
                        if (vehicle.Bay != string.Empty)
                        {
                            if (!ranList.Contains(FactoryManager.Instance.Bays.Keys.ToList().IndexOf(vehicle.Bay)))
                            {
                                FactoryManager.Instance.Bays[vehicle.Bay].RemoveBumpingOHT(vehicle.Name);
                                ranList.Add(FactoryManager.Instance.Bays.Keys.ToList().IndexOf(vehicle.Bay));
                                continue;
                            }
                        }

                        int randomNumber = random.Next(0, FactoryManager.Instance.Bays.Keys.Count);

                        bay = FactoryManager.Instance.Bays.Values.ElementAt(randomNumber);

                        if (bay.IdleVehicleMaxCount == 0)
                            bay.IdleVehicleMaxCount = 40;

                        if (bay.BumpingOHTs.Count() > bay.IdleVehicleMaxCount)
                        {
                            bay = null;
                        }
                        if (iteration > 10000)
                        {
                            ;
                        }
                        iteration += 1;
                    }


                    LineStation lineStation = null;
                    //LineStation lineStation = LstLineStation.ElementAt(randomNumber);

                    iteration = 0;

                    while (lineStation == null)
                    {

                        int randomNum2 = random.Next(0, bay.Lines.Count);
                        string lineName = bay.Lines.ElementAt(randomNum2);

                        lineStation = LineStations.Find(x => x.Line.Name == lineName);

                        if (iteration > 10000)
                        {
                            //throw new Exception("무한 루프를 중지합니다.");
                            ;
                        }
                        iteration += 1;
                    }
                    vehicle.Destination = lineStation;

                    vehicle.Route = GetPath(simTime, vehicle);
                    vehicle.Bay = bay.Name;
                    //bay.IdleVehicleMaxCount += 1;
                    bay.AddBumpingOHT(vehicle.Name, VEHICLE_STATE.IDLE);
                }
                else
                {
                    LineStation lineStation = LineStations.Find(x => x != ((LineStation)vehicle.Destination));

                    vehicle.Destination = lineStation;
                    vehicle.Route = GetPath(simTime, vehicle);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
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
            string lineName = LineStations.FirstOrDefault(x => x.Name == vehicle.Destination.Name).Line.Name;
            BAY bay = FactoryManager.Instance.Bays.FirstOrDefault(kv => kv.Value.Lines.Contains(lineName)).Value;
            if (bay != null)
                bay.RemoveBumpingOHT(vehicle.Name);

            SimPort port = new SimPort(EXT_PORT.MOVE, vehicle, this);
            vehicle.ExternalFunction(simTime, port);
        }
    }
}
