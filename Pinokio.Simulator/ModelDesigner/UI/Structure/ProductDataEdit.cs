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
    public class ProductDataEdit : ProductData
    {
        [Browsable(true)]
        [DisplayName("08. Reference Name")]
        [Editor(typeof(SelectEntityReferenceEditor), typeof(UITypeEditor))]
        [Description("Reference Class Name")]
        public string EditReferenceName { get => DisplayedReferenceName; set => DisplayedReferenceName = value; }

        [Browsable(true), Description("IDs of Step"), Editor(typeof(StepsEditor), typeof(UITypeEditor))]
        [DisplayName("09. IDs of Step")]
        public List<uint> EditIdsOfStep { get => IdsOfStep; set => IdsOfStep = value; }

        [Browsable(true), Description("Production Schedules"), Editor(typeof(ProductionScheduleEditor), typeof(UITypeEditor))]
        [DisplayName("10. Production Scheduless")]
        public List<ProductionSchedule> EditProductionSchedules
        {
            get => ProductionSchedules;
            set
            {
                foreach (ProductionSchedule ps in value)
                {
                    ps.ProductID = ProductID;
                }
                ProductionSchedules = value;
            }
        }

        public ProductDataEdit(ProductData manufacturedGoodsData)
            : base(manufacturedGoodsData)
        {

        }
        public ProductDataEdit()
   : base()
        {

        }


    }
}
