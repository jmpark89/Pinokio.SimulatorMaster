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
    [Serializable]
    public class ACS : VSubCS
    {
        private Dictionary<string, AGVLine> _dicRailLine;
        private Dictionary<string, AGVPoint> _dicRailPoint;
        private Dictionary<string, AGVZCU> _zcus;
        
        [Browsable(true), StorableAttribute(true)]
        [CategoryAttribute("5. ACS Info"), DisplayName("1. Bump Distance")]
        public double BumpDistance { get; set; }

        [Browsable(true), StorableAttribute(true)]
        [CategoryAttribute("5. ACS Info"), DisplayName("2. Dispatching Interval Time")]
        public double DispatchingIntervalTime { get; set; }

        [Browsable(false)]
        public Dictionary<string, AGVLine> DicRailLine
        {
            get
            {
                return _dicRailLine;
            }
        }

        [Browsable(false)]
        public Dictionary<string, AGVPoint> DicRailPoint
        {
            get
            {
                return _dicRailPoint;
            }
        }

        [Browsable(false)]
        public Dictionary<string, AGVZCU> Zcus
        {
            get { return _zcus; }
        }

        [Browsable(true)]
        [CategoryAttribute("5. OCS Info"), DisplayName("3. Zcus")]
        public AGVZCU[] arrayZcus
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
        public ACS()
            :base()
        {
            Initialize();
        }

        public void Initialize()
        {
            BumpDistance = 4900;
            DispatchingIntervalTime = 1;
            _dicRailPoint = new Dictionary<string, AGVPoint>();
            _dicRailLine = new Dictionary<string, AGVLine>();
            _zcus = new Dictionary<string, AGVZCU>();
        }

        public override void InitializeNode(EventCalendar evtCal)
        {
            base.InitializeNode(evtCal);

            foreach (AGV agv in Vehicles)
            {
                SetIdleDestinationAndRoute(0, agv);
            }

            SimPort port = new SimPort(INT_PORT.DISPATCH);
            evtCal.AddEvent(0, this, port);
        }

        public override void InternalFunction(Time simTime, SimPort port)
        {
            base.InternalFunction(simTime, port);
            switch(port.PortType)
            {
                case INT_PORT.DISPATCH:
                    DispatchCommands(simTime);
                    EvtCalendar.AddEvent(simTime + DispatchingIntervalTime, this, port);
                    break;
            }
        }


        public AGVZCU AddZcu(string name)
        {
            AGVZCU zcu = new AGVZCU(name, this);

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
            else if (node is AGVLine && DicRailLine.ContainsKey(node.Name) is false)
                DicRailLine.Add(node.Name, node as AGVLine);
            else if (node is AGVPoint && DicRailPoint.ContainsKey(node.Name) is false)
            {
                DicRailPoint.Add(node.Name, node as AGVPoint);
                if (((AGVPoint)node).Zcu != null && Zcus.ContainsKey(((AGVPoint)node).Zcu.Name) is false)
                    Zcus.Add(((AGVPoint)node).Zcu.Name, ((AGVPoint)node).Zcu);
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
            else if (node is AGVLine)
                DicRailLine.Remove(node.Name);
            else if (node is AGVPoint)
            {
                DicRailPoint.Remove(node.Name);
                if (((AGVPoint)node).Zcu != null && IsAniPoint4Zcu(((AGVPoint)node).Zcu) is false)
                    Zcus.Remove(((AGVPoint)node).ZcuName);
            }
        }

        protected bool IsAniPoint4Zcu(AGVZCU zcu)
        {
            foreach (AGVPoint point in DicRailPoint.Values)
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

            cmd.StartStation = ((AGVLine)greetNode).InLinkNodeConnections[port.FromNode as TXNode];
            int endNodeIndex = part.MR.Route.IndexOf(cmd.EndNode);
            cmd.EndStation = ((AGVLine)part.MR.Route[endNodeIndex - 1]).OutLinkNodeConnections[cmd.EndNode];

            AddCommand(simTime, cmd);
        }
    }
}
