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
    public class SinkPin : TXNode
    {
        [StorableAttribute(false)]
        private int intervalTime = 3;

        [StorableAttribute(false)]
        int index = 0;

        [StorableAttribute(true)]
        public int IntervalTime { get => intervalTime; set => intervalTime = value; }

        public SinkPin() : base()
        {
        }

        public SinkPin(uint id, string name) : 
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
            if (port.Object is Part)
            {
                Part part = port.Object as Part;
                SimPort newP = new SimPort(EXT_PORT.LEAVE, part, this);
                ExternalFunction(simTime, newP);
                FactoryInout factoryInout = new FactoryInout(SimEngine.Instance.SimDateTime, Convert.ToUInt32(this.ID.ToString()), this.Name, part.ID.ToString(), part.Name, "Output", part.ProductID, part.ProductName);
                ModelManager.Instance.SimResultDBManager.UploadFactoryInoutLog(factoryInout);
                ModelManager.Instance.RemovePart(part);
                ModelManager.Instance.DeletePart(part);

                foreach (Part subPart in part.Parts)
                {
                    factoryInout = new FactoryInout(SimEngine.Instance.SimDateTime, Convert.ToUInt32(this.ID.ToString()), this.Name, subPart.ID.ToString(), subPart.Name, "Output", subPart.ProductID, subPart.ProductName);
                    ModelManager.Instance.SimResultDBManager.UploadFactoryInoutLog(factoryInout);
                    ModelManager.Instance.RemovePart(subPart);
                    ModelManager.Instance.DeletePart(subPart);
                }
            }
        }
    }
}
