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
    
    
    public class Tree_1 : TXNode
    {
        
        public Tree_1()
        {
        }
        
        public Tree_1(uint id, string name) : 
                base(id, name)
        {
            // Write your code here.
        }
        
        public override void InitializeNode(EventCalendar evtCal)
        {
            try
            {
                // Write your code here.
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
			switch (port.PortType)
 			{
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
    }
}
