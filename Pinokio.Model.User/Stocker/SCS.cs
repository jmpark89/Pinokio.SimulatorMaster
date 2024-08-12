using Logger;
using Pinokio.Geometry;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace Pinokio.Model.User
{
    /// <summary>
    /// StockerCrane을 운용하는 SubCS
    /// </summary>
    [Serializable]
    public class SCS : VSubCS
    {
        private Dictionary<string, CraneLine> _dicRailLine;
        private Dictionary<string, TransportPoint> _dicRailPoint;

        [Browsable(false), StorableAttribute(true)]
        public new static bool IsInserted = true;

        [Browsable(false)]
        public Dictionary<string, CraneLine> DicRailLine
        {
            get
            {
                return _dicRailLine;
            }
        }

        [Browsable(false)]
        public Dictionary<string, TransportPoint> DicRailPoint
        {
            get
            {
                return _dicRailPoint;
            }
        }

        public SCS()
            : base()
        {
            Initialize();
        }

        protected override void SetParentNode(CoupledModel coupledModel)
        {
            base.SetParentNode(coupledModel);
        }

        public void Initialize()
        {
            _dicRailPoint = new Dictionary<string, TransportPoint>();
            _dicRailLine = new Dictionary<string, CraneLine>();
        }

        public override void InitializeNode(EventCalendar evtCal)
        {
            base.InitializeNode(evtCal);

            foreach (Crane stocker in Vehicles)
            {
                SetIdleDestinationAndRoute(0, stocker);
            }

            SimPort port = new SimPort(INT_PORT.DISPATCH);
            evtCal.AddEvent(0, this, port);
        }
        public override void InternalFunction(Time simTime, SimPort port)
        {
            try
            {
                base.InternalFunction(simTime, port);
                switch (port.PortType)
                {
                    case INT_PORT.DISPATCH:
                        {
                            DispatchCommands(simTime);
                            break;
                        }

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public override void ExternalFunction(Time simTime, SimPort port)
        {
            try
            {
                base.ExternalFunction(simTime, port);
                switch (port.PortType)
                {
                    case EXT_PORT.DISPATCH:
                        {
                            DispatchCommands(simTime);
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
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
            else if (node is CraneLine && DicRailLine.ContainsKey(node.Name) is false)
                DicRailLine.Add(node.Name, node as CraneLine);
            else if (node is TransportPoint && DicRailPoint.ContainsKey(node.Name) is false)
            {
                DicRailPoint.Add(node.Name, node as OHTPoint);
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
            }
        }


        public override void ArriveAtCoupled(Time simTime, SimPort port, SimNode greetNode)
        {
            Part part = port.Object as Part;

            Command cmd = MCS.MakeCommand(port.FromNode as TXNode, greetNode as TXNode, part, this);

            cmd.StartStation = ((CraneLine)greetNode).InLinkNodeConnections[port.FromNode as TXNode];
            int endNodeIndex = part.MR.Route.IndexOf(cmd.EndNode);
            cmd.EndStation = ((CraneLine)part.MR.Route[endNodeIndex - 1]).OutLinkNodeConnections[cmd.EndNode];

            AddCommand(simTime, cmd);
            DispatchCommands(simTime);
        }

        public override void SetIdleDestinationAndRoute(Time simTime, Vehicle vehicle)
        {
            try
            {
                Crane stocker = (Crane)vehicle;

                stocker.Destination = LineStations.First();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}