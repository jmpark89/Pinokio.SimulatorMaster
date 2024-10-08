//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pinokio.Model.User
{
    using Logger;
    using Pinokio.Database;
    using Pinokio.Geometry;
    using Pinokio.Model.Base;
    using global::Simulation.Engine;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    [Serializable]
    public class SourcePin : TXNode
    {
        [StorableAttribute(false)]
        private int intervalTime = 3;

        [StorableAttribute(false)]
        int index = 0;

        [StorableAttribute(true)]
        public int IntervalTime { get => intervalTime; set => intervalTime = value; }

        public SourcePin() : base()
        {
        }

        public SourcePin(uint id, string name) : 
                base(id, name)
        {
            // Write your code here.
        }
        

        public override bool IsEnter(SimPort port)
        {
            return true;
        }

        protected override void Enter(Time simTime,   SimPort port)
        {
        }

        public override void InitializeNode(EventCalendar evtCal)
        {
            try
            {
                // Write your code here.
                base.InitializeNode(evtCal);
                SimPort simPort = new SimPort(INT_PORT.GO_IN);
                EvtCalendar.AddEvent(1, this, simPort);

            }
            catch (System.Exception ex)
            {
                // Handle any other exception type here.
                ErrorLogger.SaveLog(ex);
            }
        }

        public override void InternalFunction(Time simTime,   SimPort port)
        {
            base.InternalFunction(simTime,   port);
            // Write your code here.
			switch (port.PortType)
 			{
                case INT_PORT.GO_IN:
                    Battery part = new Battery("RefBattery" );
                    part.ProductName = "Battery";
                    ModelManager.Instance.AddPartWith3D(this, part, part.Name, PosVec3);
                     if ( this.OutLinkNodes.Count > 0)
                    {
                        index = index % this.OutLinkNodes.Count;
                        this.OutLinkNodes[index].ExternalFunction(simTime,  new SimPort(EXT_PORT.ARRIVE, part, this));
                        ++index;
                    }

                    SimPort simPort = new SimPort(INT_PORT.GO_IN);
                    EvtCalendar.AddEvent(simTime + intervalTime , this, simPort);

                    break;

 				default: 
 					break; 
			}
        }
        
        public override void ExternalFunction(Time simTime,   SimPort port)
        {
            base.ExternalFunction(simTime,   port);
            // Write your code here.
			switch (port.PortType)
 			{
 				default: 
 					break; 
			}
        }
    }
}
