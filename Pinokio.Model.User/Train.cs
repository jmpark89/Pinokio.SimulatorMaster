using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;
using Pinokio.Database;
using Pinokio.Geometry;
using Pinokio.Model.Base;
using Simulation.Engine;
namespace Pinokio.Model.User
{
    [Serializable]
    public class Train : Vehicle, IPlaybackSave
    {
        public bool IsRightDirection { get; set; }

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

        public Train()
            : base()
        {
            Speed = 1000;
            LoadTime = 5;
            UnloadTime = 5;
        }

        public Train(string name, double speed, double distance, OHCVLine curLine, int width = 1300, int depth = 1300, int height = 50)
            : base(name, distance, curLine, width, depth, height)
        {
            Speed = speed;
            LoadTime = 5;
            UnloadTime = 5;
        }

        public override void InitializeNode(EventCalendar evtCal)
        {
            base.InitializeNode(evtCal);
        }

        protected override void ArriveToStation(Time simTime, SimPort port)
        {
            base.ArriveToStation(simTime, port);
        }

        public void SaveFunction()
        {
            byte[] Distance = BitConverter.GetBytes((short)((GuidedLine)this.Line).GetDistanceAtTime(this, SimEngine.Instance.TimeNow));
            ModelManager.Instance.PlayBackNode.TimeByte.AddRange(Distance);
            // OHCVCount = OHCVCount + 1;
            // ModelManager.Instance.PlayBackNode.OHCVCount += 1;
        }

        protected override void Enter(Time simTime, SimPort port)
        {
            EndLoading(simTime, port);
        }

        protected override void Leave(Time simTime, SimPort port)
        {
            ArriveToIdle(simTime, port);
        }

        protected override void Move(Time simTime, SimObj obj, ref SimPort port)
        {
            if (PosDatas.Count > 0 && PosDatas.Last()._endTime > simTime)
                return;

            double destLen = 0;
            double startDistance = Line.GetDistanceAtTime(this, Line.LstStartTime[Line.LstObjectIndex[this.ID]]);
            double nowDistance = Line.GetDistanceAtTime(this, simTime);
            double lineLength = Line.Length;
            bool arriveToStation = true;
            if (Destination == null)
                return;
            else if (startDistance == 0 && nowDistance == 0 && !Line.IsCurve)
            {
                destLen = Size.X / 2;
                arriveToStation = false;
            }
            else if (startDistance == lineLength && nowDistance == lineLength && !Line.IsCurve)
            {
                destLen = lineLength - Size.X / 2;
                arriveToStation = false;
            }
            else if (State is VEHICLE_STATE.MOVE_TO_LOAD && Line.LineStations.Contains(Command.StartStation))
            {
                destLen = Line.GetStationLength(Command.StartStation);
            }
            else if (State is VEHICLE_STATE.MOVE_TO_UNLOAD && Line.LineStations.Contains(Command.EndStation))
            {
                destLen = Line.GetStationLength(Command.EndStation);
            }
            else if(Line.LineStations.Contains(Destination))
            {
                destLen = Line.GetStationLength(Destination);
            }
            else
            {
                if (Line.OutLinkNodeConnections[Route[1]].ID == Line.EndPoint.ID)
                    destLen = Line.GetStationLength(Line.EndPoint);
                else
                    destLen = Line.GetStationLength(Line.StartPoint);
            }

            Time arriveTime = 0;
            //레일 거리 체크
            if (Distance != destLen)
                arriveTime = AddPosData(simTime, destLen);

            AddStopSchedule(arriveTime);

            if (arriveToStation)
            {
                SimPort newPort = new SimPort(INT_PORT.ARRIVE_TO_STATION, this);
                EvtCalendar.AddEvent(arriveTime, this, newPort);
            }
            else
            {
                SimPort newPort = new SimPort(INT_PORT.MOVE, this);
                EvtCalendar.AddEvent(arriveTime, this, newPort);
            }
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

        private Time AddPosData(Time simTime, double railDest)
        {
            Time diffT = Math.Abs(railDest - Distance) / _speed;
            PosDatas.Add(new PosData(_speed, Distance, simTime, simTime + diffT, railDest));
            return simTime + diffT;
        }

        public byte[] WriterFunction(ref int index)
        {
            byte[] data = ModelManager.Instance.PlayBackNode.TimeByte.GetRange(index, 2).ToArray();
            index = index + 2;
            return data;
        }
    }
}
