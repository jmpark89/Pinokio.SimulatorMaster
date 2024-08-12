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
    [Serializable]
    public class Battery : Part
    {
        public Battery(uint id, string name) : base(id, name)
        {
            Size = new PVector3(400, 200, 780);
        }
        public Battery(string name) : base( name)
        {
            Size = new PVector3(800, 200, 780);
        }
    }
}
