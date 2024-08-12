using DevExpress.Utils.UI;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Pinokio.Model.Base;
using Pinokio.Model.Base.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using Pinokio.Geometry;

namespace Pinokio.Designer
{
  
    public class StepDataEdit : StepData
    {

        [Browsable(true),
            DisplayName("05. IDs of Eqp"),
            Description("IDs of Eqp"),
            Editor(typeof(EqpsEditor), typeof(UITypeEditor))]
        public List<uint> EditIdsOfEqp { get => _idsOfEqp; set => _idsOfEqp = value; }

        [Browsable(true),
            DisplayName("06. Processing Time(s)"),
            Description("Processing Time(s)"),
            Editor(typeof(ProcessingTimeEditor), typeof(UITypeEditor))]
        public ProcessingTime ProcessingTimeData 
        {
            get => _processingTimeData;
            
            set
            {
                if (value != null)
                {
                    _processingTimeData = value;
                    this.ProcessingTime = this.SetProcessingTime(_processingTimeData);
                }                
            }
        }


        /// <summary>
        /// Input Product 종류 + 개수
        /// </summary>
        [Browsable(false)]
        public new Dictionary<uint, Tuple<uint, UNIT_TYPE>> InputProducts { get => _inputProducts; set => _inputProducts = value; }
        /// <summary>
        /// Ouput Product 종류 + 개수
        /// </summary>
        [Browsable(false)]
        public new Dictionary<uint, Tuple<uint, UNIT_TYPE>> OutputProducts { get => _outputProducts; set => _outputProducts = value; }


        [Browsable(true),
            DisplayName("11. Input Products"),
            Description("Input Products"),
            Editor(typeof(StepInOutProductsEditor), typeof(UITypeEditor))]
        public Dictionary<uint, Tuple<uint, UNIT_TYPE>> EditInputProducts { get => _inputProducts; set => _inputProducts = value; }

        [Browsable(true),
            DisplayName("12. Output Products"),
            Description("Output Products"),
            Editor(typeof(StepInOutProductsEditor), typeof(UITypeEditor))]
        public Dictionary<uint, Tuple<uint, UNIT_TYPE>> EditOutputProducts { get => _outputProducts; set => _outputProducts = value; }

        public StepDataEdit(StepData sd) 
            : base(sd)
        {
            this._id = sd.ID;
            this.StepType = sd.StepType;
            this.IdsOfEqp = sd.IdsOfEqp.ToList();
            this.ProcessingTimeData = sd.ProcessingTimeData;
            this.ProcessingTime = sd.SetProcessingTime(ProcessingTimeData);
            this.CascadingIntervalTime = sd.CascadingIntervalTime;
            this.ProcessingProbability = sd.ProcessingProbability;
            this.SamplingProbability = sd.SamplingProbability;
            this.InputProducts = sd.InputProducts.ToDictionary(input => input.Key, input => input.Value);
            this.OutputProducts = sd.OutputProducts.ToDictionary(output => output.Key, output => output.Value);
        }
        public StepDataEdit(string name, string decription)
            :base(name, decription)
        {

        }

        public StepDataEdit()
            :base()
        {

        }
    }
}
