using DevExpress.XtraEditors;
using Pinokio.Animation;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinokio.Designer
{
    public partial class IntegrityTestForm : DevExpress.XtraEditors.XtraForm
    {
        #region Private Veriable
        private List<SimNodeIntegrityLog> _simNodeLogDataList = new List<SimNodeIntegrityLog>();
        private List<FactoryIntegrityLog> _factoryLogDataList = new List<FactoryIntegrityLog>();
        private ModelDesigner _mainForm;
        #endregion

        public IntegrityTestForm(ModelDesigner _mainform)
        {            
            InitializeComponent();
            _mainForm = _mainform;
        }
        public void SetSimNodeLogData(List<SimNodeIntegrityLog> data)
        {
            _simNodeLogDataList = data;
            InitializeSimNodeGridView();
        }
        public void SetFactoryLogData(List<FactoryIntegrityLog> data)
        {
            _factoryLogDataList = data;
            InitializeFactoryGridView();
        }
        public void InitializeSimNodeGridView()
        {
            gridControlSimNode.DataSource = _simNodeLogDataList;

            gridViewSimNode.BestFitColumns();
        }

        public void InitializeFactoryGridView()
        {
            gridControlFactory.DataSource = _factoryLogDataList;

            gridViewFactory.BestFitColumns();
        }


        private void IgnoreSaveBT_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelBT_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}