using Pinokio.Geometry;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinokio.Model.User
{
    public class ElevatorLineStation : LineStation, IPlaybackSave
    {
        private int _bufferDepth = 0;

        public int BufferDepth { get => _bufferDepth; set => _bufferDepth = value; }
        

        public ElevatorLineStation() : base()
        {
        }

        public void SaveFunction()
        {
            throw new NotImplementedException();
        }
   
        public byte[] WriterFunction(ref int index)
        {
            throw new NotImplementedException();
        }
        
    }
}
