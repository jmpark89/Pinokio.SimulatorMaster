using DevExpress.XtraEditors;
using Logger;
using Pinokio.Model.Base.Structure;
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
    public partial class ScheduleProductionDlg : DevExpress.XtraEditors.XtraForm
    {
        
        BindingList<ProductionScheduleDataEdit> _productionScheduleDataEdits = new BindingList<ProductionScheduleDataEdit>();
        List<ProductData> _products = new List<ProductData>();
        public BindingList<ProductionScheduleDataEdit> ProductionScheduleDataEdits { get => _productionScheduleDataEdits; }

        public ScheduleProductionDlg()
        {
            InitializeComponent();
        }

        public void InitializeScheduleProduction(List<ProductionSchedule> productionScheduleDataEdits, List<ProductData> products )
        {
            try
            {
                _products = products;
                _productionScheduleDataEdits = new BindingList<ProductionScheduleDataEdit>();
                foreach (ProductionSchedule p in productionScheduleDataEdits)
                {
                    _productionScheduleDataEdits.Add(new ProductionScheduleDataEdit(p));
                }

                this.gridControlProductionSchedule.DataSource = _productionScheduleDataEdits;
                this.gridViewProductionSchedule.RefreshData();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public List<ProductionSchedule> GetProductionScheduleDatas()
        {
            List<ProductionSchedule> productionScheduleDatas = new List<ProductionSchedule>();

            try
            {
          
                foreach(ProductionScheduleDataEdit productionScheduleDataEdit in _productionScheduleDataEdits)
                {
                    productionScheduleDatas.Add(new ProductionSchedule(productionScheduleDataEdit));
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

            return productionScheduleDatas;

        }


        private void gridViewProductionSchedule_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void gridViewProductionSchedule_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                ProductionScheduleDataEdit productionScheduleDataEdit = (ProductionScheduleDataEdit)ProductionScheduleDataEdits[gridViewProductionSchedule.FocusedRowHandle];
                this.pgdProduction.SelectedObject = productionScheduleDataEdit;
                this.pgdProduction.RefreshAllProperties();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void sbOK_Click(object sender, EventArgs e)
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