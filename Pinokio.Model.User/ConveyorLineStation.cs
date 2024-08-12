using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinokio.Model.User
{
    [Serializable]
    public class ConveyorLineStation : LineStation
    {
        public ConveyorLineStation()
            : base()
        {
            
        }
        public ConveyorLineStation(string name, TransportLine line)
            : base(name, line)
        {

        }
        public ConveyorLineStation(string name, TransportLine line, double length)
            : base(name, line, length)
        {

        }
        public override void InitializeNode(EventCalendar evtCal)
        {
            EvtCalendar = evtCal;
        }

        public override void InternalFunction(Time simTime, SimPort port)
        {
            base.InternalFunction(simTime, port);
        }

        public override void ExternalFunction(Time simTime, SimPort port)
        {
            base.ExternalFunction(simTime, port);
        }
    }
}
