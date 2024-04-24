
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
    public class InOutProductData : ProductData
    {

        [Browsable(true)]
        [DisplayName("06. Reference Name")]
        [Description("Reference Class Name")]
        public string EditReferenceName { get => DisplayedReferenceName; }

        [Browsable(true),
Description("IDs of Step")]
        [DisplayName("07. IDs of Step")]
        public List<uint> EditIdsOfStep { get => IdsOfStep; }

        [Browsable(true),
Description("Production Schedules")]
        [DisplayName("08. Production Scheduless")]
        public List<ProductionSchedule> EditProductionSchedules
        {
            get => ProductionSchedules;
        }

        public uint Count { get; set; }

        public InOutProductData(ProductData manufacturedGoodsData)
            : base(manufacturedGoodsData)
        {
            ProductID = manufacturedGoodsData.ProductID;
            ProductName = manufacturedGoodsData.ProductName;
            Xdimension = manufacturedGoodsData.Xdimension;
            Ydimension = manufacturedGoodsData.Ydimension;
            Zdimension = manufacturedGoodsData.Zdimension;
            IdsOfStep = manufacturedGoodsData.IdsOfStep;
            DisplayedReferenceName = manufacturedGoodsData.DisplayedReferenceName;
        }
        public InOutProductData()
   : base()
        {

        }
    }
}

