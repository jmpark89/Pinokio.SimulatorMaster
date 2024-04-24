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

namespace Pinokio.Designer
{
  
    public class StepDataEdit : StepData
    {

        [Browsable(true),
            Description("IDs of Eqp"),
            Editor(typeof(EqpsEditor), typeof(UITypeEditor))]
            [DisplayName("04. IDs of Eqp")]
        public List<uint> EditIdsOfEqp { get => _idsOfEqp; set => _idsOfEqp = value; }

        /// <summary>
        /// Input Product 종류 + 개수
        /// </summary>
        [Browsable(false)]
        public new Dictionary<uint, uint> InputProducts { get => _inputProducts; set => _inputProducts = value; }
        /// <summary>
        /// Ouput Product 종류 + 개수
        /// </summary>
        [Browsable(false)]
        public new Dictionary<uint, uint> OutputProducts { get => _outputProducts; set => _outputProducts = value; }


        [Browsable(true),
            Description("Input Products"),
            Editor(typeof(StepInOutProductsEditor), typeof(UITypeEditor))]
        [DisplayName("07. Input Products")]
        public Dictionary<uint, uint> EditInputProducts { get => _inputProducts; set => _inputProducts = value; }

        [Browsable(true),
            Description("Output Products"),
            Editor(typeof(StepInOutProductsEditor), typeof(UITypeEditor))]
        [DisplayName("08. Output Products")]
        public Dictionary<uint, uint> EditOutputProducts { get => _outputProducts; set => _outputProducts = value; }

        public StepDataEdit(StepData pd) 
            : base(pd)
        {
            this._id =  pd.ID;
    
            this.IdsOfEqp = pd.IdsOfEqp.ToList();
            this.ProcessingTime = pd.ProcessingTime;
            this.StepType = pd.StepType;
            this.InputProducts = pd.InputProducts.ToDictionary(input => input.Key, input => input.Value);
            this.OutputProducts = pd.OutputProducts.ToDictionary(output => output.Key, output => output.Value);
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
