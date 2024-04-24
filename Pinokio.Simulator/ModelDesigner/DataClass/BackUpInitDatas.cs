using Pinokio.Animation;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinokio.Designer
{
    public class BackUpInitDatas
    {

        public Dictionary<SimNode, List<Tuple<string, string>>> SimNodeInitDatas = new Dictionary<SimNode, List<Tuple<string, string>>>();

        public Dictionary<NodeReference, List<Tuple<string, string>>> ReferenceNodeInitDatas = new Dictionary<NodeReference, List<Tuple<string, string>>>();


    }
}
