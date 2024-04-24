using Logger;
using Pinokio.Database;
using Pinokio.Geometry;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Pinokio.Model.User
{
    public class CraneLine : GuidedLine, IPlaybackSave
    {
        [Browsable(true), StorableAttribute(true)]
        public bool IsCommonUse { get; set; }

        public SCS Scs
        {
            get { return ParentNode as SCS; }
        }

        public CraneLine() : base()
        {
            Initialize();
        }

        public CraneLine(uint id, string name) : base(id, name)
        {
            Initialize();
        }

        public CraneLine(string name, Base.TransportPoint start, Base.TransportPoint end, int width, int height)
    : base(name, start, end, width, height)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsCommonUse = false;
        }

        public override double GetCost()
        {
            return MinPassingTime;
        }

        public void SaveFunction()
        {
            throw new NotImplementedException();
        }
        public byte[] WriterFunction(ref int index)
        {
            throw new NotImplementedException();
        }

        public override bool IsEnter(SimPort port)
        {
            if (port.Object is Crane)
            {
                return true;
            }
            else if (port.Object is Part)
                return false;
            else
                return false;
        }
    }
}
