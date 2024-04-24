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
    public class OHCV : Vehicle, IPlaybackSave
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

        public OHCV()
            : base()
        {
            Speed = 1000;
            LoadTime = 5;
            UnloadTime = 5;
        }

        public OHCV(string name, double speed, double distance, OHCVLine curLine, int width = 1300, int depth = 1300, int height = 50)
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

        protected override void ArriveToLoad(Time simTime, SimPort port)
        {
            PosDatas.Clear();
            StartPos = 0;

            base.ArriveToLoad(simTime, port);
        }

        protected override void ArriveToUnload(Time simTime, SimPort port)
        {
            base.ArriveToUnload(simTime, port);
        }

        protected override void ArriveToIdle(Time simTime, SimPort port)
        {
            if (Line.ArrivedPorts.Count > 0) 
                ;
            if (Line.ArrivedPorts.Count > 0 && GetDistanceAtTime(simTime) == 0)
            {
                ArriveToLoad(simTime, Line.ArrivedPorts.First());
            }
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
            base.Enter(simTime, port);
            Move(simTime, port.Object, ref port);
        }

        protected override void Leave(Time simTime, SimPort port)
        {
            Move(simTime, port.Object, ref port);
        }

        protected override void Move(Time simTime, SimObj obj, ref SimPort port)
        {
            double endPos = 0;
            double remainDistance = 0;
            double vehicleLength = GetDistanceAtTime(simTime);
            if (vehicleLength == 0 && EnteredObjects.Count == 0)
                return;
            else if (EnteredObjects.Count == 0)
            {
                IsRightDirection = false;
                remainDistance = vehicleLength;
                Destination = Line.StartPoint;
                endPos = 0;
            }
            else
            {
                IsRightDirection = true;
                remainDistance = Line.Length - vehicleLength;
                Destination = Line.EndPoint;
                endPos = Line.Length;
            }

            //현재 위치에서 이동                       
            Time diffTime = remainDistance / Speed;
            PosDatas.Add(new PosData(Speed, vehicleLength, simTime, simTime + diffTime, endPos));

            SimPort newPort = new SimPort(INT_PORT.ARRIVE_TO_STATION);
            newPort.Object = this;
            newPort.ToNode = this.Destination;
            EvtCalendar.AddEvent(simTime + diffTime, this, newPort);
        }

        public byte[] WriterFunction(ref int index)
        {
            byte[] data = ModelManager.Instance.PlayBackNode.TimeByte.GetRange(index, 2).ToArray();
            index = index + 2;
            return data;
        }
    }
}
