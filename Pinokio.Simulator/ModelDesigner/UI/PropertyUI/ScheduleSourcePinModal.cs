using DevExpress.XtraEditors;
using Logger;
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
    public partial class ScheduleSourcePinModal : DevExpress.XtraEditors.XtraForm
    {
        ProductionScheduleDataEdit _productionScheduleData;
        public ProductionScheduleDataEdit ProductionScheduleData { get => _productionScheduleData; set => _productionScheduleData = value; }

        public ScheduleSourcePinModal()
        {
            InitializeComponent();
        }


        public void IniatializeProductionData(ProductionScheduleDataEdit productionScheduleDataEdit)
        {

            this.propertyGridControl1.SelectedObject = productionScheduleDataEdit;
            _productionScheduleData = productionScheduleDataEdit;
           this.propertyGridControl1.RefreshEditor();
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
    }
}