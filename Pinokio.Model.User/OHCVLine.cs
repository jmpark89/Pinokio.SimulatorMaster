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
using Pinokio.Object;
using Simulation.Engine;
namespace Pinokio.Model.User
{
    [Serializable]
    public class OHCVLine : GuidedLine
    {
        public OHCV Ohcv
        {
            get 
            {
                if (EnteredObjects.Count > 0)
                    return EnteredObjects[0] as OHCV;
                else 
                    return null;
            }
        }

        public OHCVLine() : base()
        {

        }

        public OHCVLine(uint id, string name) : base(id, name)
        {

        }

        public OHCVLine(string name, TransportPoint start, TransportPoint end, int width, int height)
            : base(name, start, end, width, height)
        {

        }

        public override void InitializeNode(EventCalendar evtCal)
        {
            base.InitializeNode(evtCal);
        }

        protected override void Enter(Time simTime, SimPort port)
        {
            EnteredObjects.Remove(port.Object);
            Ohcv.EnteredObjects.Add(port.Object as Part);
            Ohcv.State = VEHICLE_STATE.MOVE_TO_UNLOAD;
            SimPort newPort = new SimPort(EXT_PORT.MOVE, port.Object, this);
            Ohcv.ExternalFunction(simTime, newPort);
        }

        protected override void Leave(Time simTime, SimPort port)
        {
            base.Leave(simTime, port);
            Ohcv.EnteredObjects.Remove(port.Object as Part);
            Ohcv.Destination = StartPoint;
            SimPort newPort = new SimPort(INT_PORT.MOVE, Ohcv, this);
            InternalFunction(simTime, newPort);
        }

        public override bool IsEnter(SimPort port)
        {
            if (GetDistanceAtTime(Ohcv, SimEngine.Instance.TimeNow) == 0 && Ohcv.EnteredObjects.Count == 0 && RequestedObjects.Count == 0)
            {
                ArrivedPorts.Remove(port);
                SimPort newPort = new SimPort(EXT_PORT.LOAD, port.Object, port.FromNode);
                Ohcv.ExternalFunction(SimEngine.Instance.TimeNow, newPort);
                return false;
            }
            else
                return false;
        }
    }
}
