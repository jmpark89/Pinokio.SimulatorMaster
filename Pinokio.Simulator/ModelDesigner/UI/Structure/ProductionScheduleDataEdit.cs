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
    public class ProductionScheduleDataEdit : ProductionSchedule
    {
        [Browsable(true)]
        [DisplayName("Limit time")]
        [Description("생산 납기")]
        public double EditLimitTime { get => LimitTime.TotalSeconds; set => LimitTime = value; }
        [Browsable(true)]
        [DisplayName("Creation Time")]
        [Description("만들어진 날자")]
        public double EditCreationTime { get => CreationTime.TotalSeconds; set => CreationTime = value; }

        [Browsable(true)]
        [DisplayName("Interval Time")]
        [Description("투입 시점의 간격")]
        public double EditIntervalTime { get => IntervalTime.TotalSeconds; set => IntervalTime = value; }


        [Browsable(true)]
        [DisplayName("Source Node")]
        [Editor(typeof(SelectNodeEditor), typeof(UITypeEditor))]
        [Description("투입 Node ID")]
        public  override uint InputNodeID { get => _inputNodeID; set => _inputNodeID = value; }

        public ProductionScheduleDataEdit()
            : base()
        {
        }

        public ProductionScheduleDataEdit(ProductionSchedule productionScheduleData)
      : base(productionScheduleData)
        {
        }

    }
}
