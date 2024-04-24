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
    public class BreakDownEdit : BreakDown
    {
        public enum MTTRDist
        {
            Uniform,
        }
        public enum FOAdist
        {
            Const,
        }
        private PMCategory _category = PMCategory.None;
        private string _eqpNames = "None";
        private string _detailCategory = "None";
        [Browsable(true),
    Description("Category"),
        DisplayName("01. Category")]
        public PMCategory Category { get; set; }

        [Browsable(true), Description("Equipment according to category")]
        [DisplayName("02. Detail Category")]
        public string DetailCategory
        {
            get => _detailCategory; set => _detailCategory = value;
        }
        [Browsable(true), Description("Equipment according to category")]
        [DisplayName("03. EQP Name")]
        public string EQPNames
        {
            get => _eqpNames; set => _eqpNames = value;
        }
        
        [Browsable(true),
    Description("PM Type"),
        DisplayName("04. PM Type")]
        public PMType PMType { get; set; }

        [Browsable(true),
    Description("MTTR Distribution"),
        DisplayName("05. MTTR Distribution")]
        public MTTRDist MTTRDistribution { get; set; }

        [Browsable(true),
    Description("MTTR MEAN"),
        DisplayName("06. MTTR MEAN")]
        public double MTTRMean { get; set; }

        [Browsable(true),
    Description("MTTR OFFSET"),
        DisplayName("07. MTTR OFFSET")]
        public double MTTROffset { get; set; }

        [Browsable(true),
    Description("FOA Distribution"),
        DisplayName("08. FOA Distribution")]
        public FOAdist FOAdistribution { get; set; }

        [Browsable(true),
    Description("FOA"),
        DisplayName("09. FOA")]
        public uint FOA { get; set; }


        public BreakDownEdit( )
            : base()
        {

        }
        public BreakDownEdit(PMCategory category, PMType pmType, PMPeriod pmPeriod)
            : base()
        {

        }
    }
}
