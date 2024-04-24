using DevExpress.Charts.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraVerticalGrid.Rows;
using Logger;
using Pinokio.Model.Base;
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
    public partial class EditStepDlg : DevExpress.XtraEditors.XtraForm
    {
        
        BindingList<StepDataEdit> _collection = new BindingList<StepDataEdit>();
        BindingList<Equipment> _dataSource = new BindingList<Equipment>();

        public BindingList<StepDataEdit> Collection { get => _collection; }
        BindingList<Equipment> _equipment = new BindingList<Equipment>();
        public BindingList<Equipment> Equipment { get => _equipment; }

        BindingList<InOutProductData> _inoutProductDatas = new BindingList<InOutProductData>();
        public BindingList<InOutProductData> InOutProductDatas { get => _inoutProductDatas; set => _inoutProductDatas = value; }




        public EditStepDlg()
        {
            InitializeComponent();
            SelectEqpModal.MyFormClosed += new SelectEqpModal.MyFormClosedEventHandler(selectEqpModel_FormClosed);
            StepInOutProductsModal.MyFormClosed += new StepInOutProductsModal.MyFormClosedEventHandler(stepInOutProductsModal_FormClosed);
        }

        private void selectEqpModel_FormClosed(object s, FormClosedEventArgs e)
        {
            _dataSource = null;
            gridControlStepData_etc.DataSource = _dataSource;
            _dataSource = new BindingList<Equipment>();

            _equipment.Clear();
            gridViewStepData_etc.Columns.Clear();

            if (s != null)
            {
                List<Equipment> p = ((SelectEqpModal)s).SelectEqpDatas as List<Equipment>;

                foreach (Equipment ad in p)
                {
                    _dataSource.Add(ad);
                }
                this.gridControlStepData_etc.DataSource = _dataSource;
                this.gridControlStepData_etc.RefreshDataSource();
                this.gridViewStepData_etc.OptionsBehavior.ReadOnly = true;
                gridViewStepData_etc.BestFitColumns();
            }
            
        }
        private void stepInOutProductsModal_FormClosed(object s, FormClosedEventArgs e)
        {
            BindingList<InOutProductData> p = ((StepInOutProductsModal)s).InOutProductDatas as BindingList<InOutProductData>;

            this.gridControlStepData_etc.DataSource = p;
            this.gridControlStepData_etc.RefreshDataSource();
            this.gridViewStepData_etc.OptionsBehavior.ReadOnly = true;
            gridViewStepData_etc.BestFitColumns();
        }

        public void InitializeStepData(List<StepData> stepDatas)
        {
            try
            {
                _collection.Clear();
                foreach(StepData sd in stepDatas)
                {
                    StepDataEdit stepDataUI = new StepDataEdit(sd);
                    
                    _collection.Add(stepDataUI);
                } 

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void ModifyStepInfo_Load(object sender, EventArgs e)
        {
            try
            {
                gridControlStepData.DataSource = _collection;

                this.gridViewStepData.Columns["EditInputProducts"].Visible = false;
                this.gridViewStepData.Columns["EditOutputProducts"].Visible = false;
                gridControlStepData.RefreshDataSource();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void gridViewStepData_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            try
            {
                UpdatePropertyGridByFocusRow(e.RowHandle);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

        private void UpdatePropertyGridByFocusRow(int focusedRowHandle)
        {
            try
            {
                int rowIndex = focusedRowHandle;
                if (_collection.Count == 0)
                    return;
                else if (_collection.Count > 0 && focusedRowHandle < 0)
                    rowIndex = _collection.Count - 1;

                StepDataEdit p = _collection[rowIndex];
                this.propertyGridControl1.SelectedObject = p;
                this.propertyGridControl1.Refresh();

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }
        
        
       

        private void propertyGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            gridViewStepData.UpdateCurrentRow();
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

        private void gridViewStepData_DataSourceChanged(object sender, EventArgs e)
        {
            UpdatePropertyGridByFocusRow(0);
        }

        private void gridViewStepData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.propertyGridControl1.Refresh();
        }
        //----------------------------------------------------

        private void propertyGridControl1_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            _dataSource = null;
            InOutProductDatas = null;
            gridControlStepData_etc.DataSource = _dataSource;
            gridControlStepData_etc.DataSource = InOutProductDatas;
            gridViewStepData_etc.Columns.Clear();
            try
            {
                InOutProductDatas = new BindingList<InOutProductData>();

                if (e.Row.Name.Contains("rowEditIdsOfEqp"))
                {
                    StepDataEdit a = propertyGridControl1.SelectedObject as StepDataEdit;


                    _dataSource = new BindingList<Equipment>();

                    _equipment.Clear();
                    List<Equipment> eqpDatas = new List<Equipment>();

                    object cellkey = propertyGridControl1.GetCellValue(e.Row, 0);
                    List<uint> list = cellkey as List<uint>;
                    
                    foreach (var item in FactoryManager.Instance.Eqps.Values)
                    {
                        if (list.Contains(item.ID))
                        {
                            eqpDatas.Add(item);
                        }
                    }

                    foreach (Equipment ad in eqpDatas)
                    {
                        _dataSource.Add(ad);
                    }

                    this.gridControlStepData_etc.DataSource = _dataSource;
                    this.gridControlStepData_etc.RefreshDataSource();
                    this.gridViewStepData_etc.OptionsBehavior.ReadOnly = true;

                    for (int i = 0; i < gridViewStepData_etc.RowCount; i++)
                    {
                        Equipment row = gridViewStepData_etc.GetRow(i) as Equipment;

                        if (a.EditIdsOfEqp.Contains(row.ID))
                        {
                            gridViewStepData_etc.SelectRow(i);
                        }
                    }
                    
                }

                else if (e.Row.Name.Contains("rowEditInputProducts"))
                {
                    InOutProductDatas = new BindingList<InOutProductData>();
                    List<ProductData> products = new List<ProductData>();
                    products = FactoryManager.Instance.ProductDatas.Values.ToList();
                    propertyGridControl1.GetCellValue(e.Row, 0);
                    Dictionary<uint, uint> productCounts = (Dictionary<uint, uint>)propertyGridControl1.GetCellValue(e.Row, 0);

                    foreach (ProductData ad in products)
                    {
                        InOutProductData newPD = new InOutProductData(ad);
                        
                        if (productCounts.ContainsKey(newPD.ProductID))
                        {
                            newPD.Count = productCounts[newPD.ProductID];
                            InOutProductDatas.Add(newPD);
                        }
                    }
                    this.gridControlStepData_etc.DataSource = InOutProductDatas;
                    this.gridControlStepData_etc.RefreshDataSource();
                    

                }

                else if (e.Row.Name.Contains("rowEditOutputProducts"))
                {
                    InOutProductDatas = new BindingList<InOutProductData>();
                    List<ProductData> products = new List<ProductData>();
                    products = FactoryManager.Instance.ProductDatas.Values.ToList();
                    propertyGridControl1.GetCellValue(e.Row, 0);
                    Dictionary<uint, uint> productCounts = (Dictionary<uint, uint>)propertyGridControl1.GetCellValue(e.Row, 0);

                    foreach (ProductData ad in products)
                    {
                        InOutProductData newPD = new InOutProductData(ad);
                        if (productCounts.ContainsKey(newPD.ProductID))
                        {
                            newPD.Count = productCounts[newPD.ProductID];
                            InOutProductDatas.Add(newPD);
                        }
                    }

                    this.gridControlStepData_etc.DataSource = InOutProductDatas;
                    this.gridControlStepData_etc.RefreshDataSource();
                    

                }
                gridViewStepData_etc.BestFitColumns();
                //복사는되고, 입력못받게
                this.gridViewStepData_etc.OptionsBehavior.ReadOnly = true;
                //복사도안되고, 입력못받게
                //this.gridViewStepData_etc.OptionsBehavior.Editable = false;

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        
        //----------------------------------------------------
        

        private void PropertyGrid_Click(object sender, EventArgs e)
        {

        }

    }
}