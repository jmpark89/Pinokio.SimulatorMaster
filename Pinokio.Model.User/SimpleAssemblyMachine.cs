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
    
    
    public class SimpleAssemblyMachine : TXNode
    {
        SimObj entity = null;
        int currentState = 0;
        int assemblyTime = 1000;
        [StorableAttribute(true)]
        public int assemblyPartCount { get; set; }

        public SimpleAssemblyMachine()
        {
            Capa = 2;
            assemblyPartCount = 9;
        }
        
        public SimpleAssemblyMachine(uint id, string name) : 
                base(id, name)
        {
            // Write your code here.
            Capa = 2;
            assemblyPartCount = 9;
        }

        public override void InitializeNode(EventCalendar evtCal)
        {
            try
            {
                // Write your code here.
                base.InitializeNode(evtCal);
                EnteredObjects.Clear();
                ArrivedPorts.Clear();
                RequestedObjects.Clear();
            }
            catch (System.Exception ex)
            {
                // Handle any other exception type here.
                ErrorLogger.SaveLog(ex);
            }
        }
        
        public override void InternalFunction(Time simTime, SimPort port)
        {
            base.InternalFunction(simTime, port);

            // Write your code here.
            currentState = port.PortType;

            switch (port.PortType)
 			{
                case INT_PORT.MOVE:
                    // 다음 노드로 전달.
                    SimPort newPort = new SimPort(EXT_PORT.ARRIVE, port.Object, this);
                    OutLinkNodes[0].ExternalFunction(simTime, newPort);
                    break;
                default: 
 					break; 
			}
        }
        
        public override void ExternalFunction(Time simTime, SimPort port)
        {
            base.ExternalFunction(simTime, port);
            // Write your code here.
			switch (port.PortType)
 			{
 				default: 
 					break; 
			}
        }

        protected override void Leave(Time simTime, SimPort port)
        {
            try
            {
                Part ent = port.Object as Part;

                // 이전 TXNode한테 Request Part를 요청해야한다.
                Request2EnterArrivedObjects(simTime);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public override bool IsEnter(SimPort port)
        {
            if ((port.FromNode == InLinkNodes[0] && EnteredObjects.Count == 0 )
                ||
                (port.FromNode == InLinkNodes[1] && EnteredObjects.Count > 0 && ((Part)EnteredObjects[0]).Parts.Count < assemblyPartCount))
                return true;
            else
                return false;
        }

        protected override void Enter(Time simTime, SimPort port)
        {
            entity = port.Object;
            ((Part)port.Object).PosVec3 = this.PosVec3;

            if (EnteredObjects.Count > 1)
            {
                Part part1 = EnteredObjects[0] as Part;
                Part part2 = EnteredObjects[1] as Part;
                part1.Parts.Add(part2);
                part1.PosVec3 = this.PosVec3;
                EnteredObjects.Remove(part2);

                if (part1.Parts.Count == 9)
                {
                    port = new SimPort(INT_PORT.MOVE, part1);
                    EvtCalendar.AddEvent(simTime, this, port);
                }
            }
            else if( EnteredObjects.Count == 1)
            {
                Request2EnterArrivedObjects(simTime);
            }
        }
    }
}
