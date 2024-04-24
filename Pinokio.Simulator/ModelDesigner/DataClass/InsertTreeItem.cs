using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinokio.Designer.DataClass
{
    interface IFileImage
    {
        int StateImageIndex { get; }
    }

    public class InsertTreeItem : IFileImage
    {
        static int currentID = 1;

        public InsertTreeItem(string categoryName, int stateImageIndex)
        {
            ID = currentID;
            ++currentID;
            StateImageIndex = stateImageIndex;
            Category = categoryName;
            RefType = categoryName;
        }
        public InsertTreeItem(string category, string refType, int parentID, string nodeType, double height, int stateImageIndex)
        {
            ID = currentID;
            ++currentID;
            this.ParentID = parentID;
            this.StateImageIndex = stateImageIndex;
            this.Category = category;
            this.RefType = refType;
            this.NodeType = nodeType;
            this.Height = height;
            
        }
        public int ID
        {
            get;
            private set;
        }


        public int ParentID
        {
            get;
            private set;
        }

        public string Category
        {
            get;
            protected set;
        }
        public void SetName(string name)
        {
            Category = name;
        }
        public string NodeType
        {
            get;
            private set;
        }

        public string RefType
        { 
            get; 
            set; 
        }

        public double Height
        {
            get;
            private set;
        }


        public int StateImageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Couple, CoupleObject , Node
        /// </summary>

    }
    public class InsertNodeTreeItem : InsertTreeItem
    {


        public InsertNodeTreeItem(string name, int stateImageIndex) : base(name, stateImageIndex)
        {
        }
        public InsertNodeTreeItem(string category, string refType, int parentID, string nodeType, double height, int stateImageIndex) :
            base(category, refType, parentID, nodeType, height, stateImageIndex)
        {
        }
    }
}
