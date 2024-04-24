using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinokio.Designer.UI.AssetEdit
{
    public class AssetItem
    {
        public string Name { get; set; }

        public string Path;

        public string ObjName;

        public Bitmap Thumnail { get; set; }
        public string Description { get; set; }

        public string UserID { get; set; }

        public Image Download { get; set; }

    }
}
