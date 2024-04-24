using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinokio.Designer
{

    public enum RouteRailType
    {
        Acquire,
        Deposit,
        Waiting,
        Transferring,
        StartNode,
        EndNode,
        Select
    }
    public class DetailRouteRailInfo
    {
        public string Name { get; set; }

        public RouteRailType Type { get; set; }

        public int Order { get; set; }
    }

}
