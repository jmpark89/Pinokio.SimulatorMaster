using System;
using System.Collections.Generic;
using System.ComponentModel;
using Pinokio.Model.Base.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinokio.Designer
{
    class NodeEdit
    {
        private string _categoryName;
        private string _nodeType;

        [Browsable(true), Description("Category Name"),
        DisplayName("Category")]
        public string Category
        {
            get => _categoryName; 
            set => _categoryName = value; 
        }
        [Browsable(true), Description("Node Type"),
        DisplayName("Node Type")]
        public string NodeType
        {
            get => _nodeType;
            set => _nodeType = value;
        }
        public NodeEdit(string categoryName, string nodeTypeName)
        {
            _categoryName = categoryName;
            _nodeType = nodeTypeName;
        }
        public NodeEdit()
        {
            _categoryName = "...";
            _nodeType = "...";
        }
    }

    class NodeDetailEdit
    {
        private string _id;
        private string _nodeName;
        private string _nodeType;
        private string _refType;
        
        [Browsable(true), Description("Node ID"),
        DisplayName("ID")]
        public string ID
        {
            get => _id;
            set => _id = value;
        }

        [Browsable(true), Description("Node Name"),
        DisplayName("Name")]
        public string NodeName
        {
            get => _nodeName;
            set => _nodeName = value;
        }
        [Browsable(true), Description("Node Type"),
        DisplayName("NodeType")]
        public string NodeType
        {
            get => _nodeType;
            set => _nodeType = value;
        }
        [Browsable(true), Description("Ref Type"),
        DisplayName("RefType")]
        public string RefType
        {
            get => _refType;
            set => _refType = value;
        }
        public NodeDetailEdit(string iD, string nodeName, string nodeTypeName, string refTypeName)
        {
            _id = iD;
            _nodeName = nodeName;
            _nodeType = nodeTypeName;
            _refType = refTypeName;
        }
    }
    class NodeProperties
    {
        public int ID { get; set; }
        public int ParentID { get; set; }

        private string _categoryName;
        private string _propertyValue;

        [Browsable(true), Description("Node Name"),
        DisplayName("Category")]
        public string CategoryName
        {
            get => _categoryName;
            set => _categoryName = value;
        }
        [Browsable(true), Description("Node Name"),
        DisplayName("Value")]
        public string PropertyValue
        {
            get => _propertyValue;
            set => _propertyValue = value;
        }
    }
}
