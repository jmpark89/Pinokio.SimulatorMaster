using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinokio.Designer.DataClass
{
    public class InserteNodeTreeData
    {
        [DevExpress.Utils.Filtering.FilterGroup("Category")]
        public string Category { get; set; }

        private string refType = string.Empty;
        private string nodeType = string.Empty;
        private double height = 0;

        public string NodeType { get => nodeType; set => nodeType = value; }
        public string RefType { get => refType; set => refType = value; }

        

        public double Height { get => height; set => height = value; }
    }
}
