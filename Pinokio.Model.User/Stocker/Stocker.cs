using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinokio.Model.User
{
    public class Stocker : CoupledModel
    {
        [Browsable(false), StorableAttribute(false)]
        public new static bool IsInserted = true;

        protected List<Base.Buffer> _buffers;
        public List<Base.Buffer> Buffers
        { 
            get { return _buffers; } 
            set { _buffers = value; }
        }

        [Browsable(true), StorableAttribute(false)]
        public double LoadFactor
        {
            get 
            {
                double loadCount = 0;
                foreach(Base.Buffer buffer in _buffers)
                {
                    loadCount += buffer.EnteredObjects.Count;
                }

                return Math.Round(loadCount/ TotalStorageCapa, 2);

            }
        }

        [Browsable(true), StorableAttribute(false)]
        public int TotalStorageCapa
        { get;set; }

        public Stocker()
            :base(ModelManager.Instance.GetNodeAbleID)
        {
            _buffers = new List<Base.Buffer>();
            TotalStorageCapa = 0;
        }

        public override void AddChildNode(SimNode simNode)
        {
            base.AddChildNode(simNode);
            if (simNode is Base.Buffer)
            {
                Base.Buffer buffer = simNode as Base.Buffer;
                _buffers.Add(buffer);
                TotalStorageCapa += buffer.Capa;
            }
        }

        public override void RemoveChildNode(SimNode simNode)
        {
            base.RemoveChildNode(simNode);
            if(simNode is Base.Buffer)
            {
                Base.Buffer buffer = simNode as Base.Buffer;
                _buffers.Remove(buffer);
                TotalStorageCapa -= buffer.Capa;
            }
        }
    }
}
