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
    public class ElevatorLine : GuidedLine, IPlaybackSave
    {
        [Browsable(true), StorableAttribute(true)]
        public bool IsCommonUse { get; set; }

        public LCS Lcs
        {
            get { return ParentNode as LCS; }
        }

        public ElevatorLine() : base()
        {
            Initialize();
        }

        public ElevatorLine(uint id, string name) : base(id, name)
        {
            Initialize();

        }

        public ElevatorLine(string name, Base.TransportPoint start, Base.TransportPoint end, int width, int height)
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
            if (port.Object is Elevator)
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
