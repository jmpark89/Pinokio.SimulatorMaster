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
using System.Windows;
using System.Windows.Forms;


namespace Pinokio.Designer
{
    public partial class ProductionScheduleEditingDlg : DevExpress.XtraEditors.XtraForm
    {

        BindingList<ProductionScheduleDataEdit> _productionScheduleDataEdits = new BindingList<ProductionScheduleDataEdit>();
        public BindingList<ProductionScheduleDataEdit> ProductionScheduleDataEdits { get => _productionScheduleDataEdits; }

        public delegate void MyFormClosedEventHandler(object sender,FormClosedEventArgs e);

        public static event MyFormClosedEventHandler MyFormClosed;

        //public delegate void DataPassEventHandler(int selected_items);
        public List<ProductionSchedule> productionScheduleDataEdits = null;

        public ProductionScheduleEditingDlg()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(ProductionScheduleEditingDlg_FormClosed);
        }

        public void InitializeScheduleProduction(ref List<ProductionSchedule> productionScheduleDataEdits)
        {
            try
            {
                this.productionScheduleDataEdits = productionScheduleDataEdits;
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
                int rowIndex = e.FocusedRowHandle;
                if (_productionScheduleDataEdits.Count == 0)
                    return;
                else if (_productionScheduleDataEdits.Count > 0 && rowIndex < 0)
                    rowIndex = _productionScheduleDataEdits.Count - 1;

                ProductionScheduleDataEdit productionScheduleDataEdit = (ProductionScheduleDataEdit)ProductionScheduleDataEdits[rowIndex];
                this.pgdProduction.SelectedObject = productionScheduleDataEdit;
                this.pgdProduction.RefreshAllProperties();
                productionScheduleDataEdits = GetProductionScheduleDatas();
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

        private void pgdProduction_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            gridViewProductionSchedule.UpdateCurrentRow();
        }

        private void gridViewProductionSchedule_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            pgdProduction.RefreshAllProperties();
            productionScheduleDataEdits = GetProductionScheduleDatas();
        }


        private void ProductionScheduleEditingDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MyFormClosed != null)
            {
                MyFormClosed(this, e);
            }
        }

    }
}