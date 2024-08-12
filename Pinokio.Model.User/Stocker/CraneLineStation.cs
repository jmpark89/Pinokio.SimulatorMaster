using Logger;
using Pinokio.Database;
using Pinokio.Geometry;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Pinokio.Model.User
{
    [Serializable]
    public class CraneLineStation : LineStation, IPlaybackSave
    {
        private int _bufferDepth = 0;
        public int BufferDepth { get => _bufferDepth; set => _bufferDepth = value; }

        public CraneLineStation() : base()
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
