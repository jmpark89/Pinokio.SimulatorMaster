using DevExpress.XtraEditors;
using Logger;
using Pinokio.Animation;
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
    public partial class EditProductDlg : DevExpress.XtraEditors.XtraForm
    {
        BindingList<ProductDataEdit> _collection = new BindingList<ProductDataEdit>();
        public BindingList<ProductDataEdit> Collection { get => _collection; }
        public BindingList<StepData> _dataSource = new BindingList<StepData>();
        BindingList<ProductDataEdit> _productDataEdit = new BindingList<ProductDataEdit>();
        public BindingList<ProductDataEdit> ProductDataEdit { get => _productDataEdit; }
        BindingList<ProductionScheduleDataEdit> _productionScheduleDataEdits = new BindingList<ProductionScheduleDataEdit>();
        public BindingList<ProductionScheduleDataEdit> ProductionScheduleDataEdits { get => _productionScheduleDataEdits; }
        public static DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs check;
        
        

        public EditProductDlg()
        {
            InitializeComponent();
            ProductionScheduleEditingDlg.MyFormClosed += new ProductionScheduleEditingDlg.MyFormClosedEventHandler(productionScheduleEditingDlg_FormClosed);
            SelectStepsModal.MyFormClosed += new SelectStepsModal.MyFormClosedEventHandler(productionStepsModel_FormClosed);
        }
        private void productionStepsModel_FormClosed(object s, FormClosedEventArgs e)
        {
            if (s != null)
            {
                _dataSource = null;
                gridControlProductLst_etc.DataSource = _dataSource;
                gridViewProductLst_etc.Columns.Clear();

                _dataSource = new BindingList<StepData>();

                _productDataEdit.Clear();

                BindingList<StepData> a = ((SelectStepsModal)s).StepDatas as BindingList<StepData>;
                List<StepData> productDatas = new List<StepData>();

                foreach (var item in FactoryManager.Instance.StepDatas.Values)
                {
                    productDatas.Add(item);
                }

                for (int i = 0; a.Count > i; i++)
                {
                    for (int j = 0; productDatas.Count > j; j++)
                    {
                        if (a[i].ID == productDatas[j].ID)
                        {
                            _dataSource.Add(productDatas[j]);
                        }
                    }
                }

                this.gridControlProductLst_etc.DataSource = _dataSource;
                this.gridControlProductLst_etc.RefreshDataSource();
                
            }
            //그리드뷰컬럼크기조정
            gridViewProductLst_etc.BestFitColumns();
            //복사되고,수정안되게
            this.gridViewProductLst_etc.OptionsBehavior.ReadOnly = true;

        }
        private void productionScheduleEditingDlg_FormClosed(object s, FormClosedEventArgs e)
        {
            _dataSource = null;
            gridControlProductLst_etc.DataSource = _dataSource;
           gridViewProductLst_etc.Columns.Clear();

            if (s!=null)
            {
                
                List<ProductionSchedule> b = ((ProductionScheduleEditingDlg)s).productionScheduleDataEdits as List<ProductionSchedule>;
                _productionScheduleDataEdits = new BindingList<ProductionScheduleDataEdit>();
                foreach (ProductionSchedule p in b)
                {
                    _productionScheduleDataEdits.Add(new ProductionScheduleDataEdit(p));
                }

                this.gridControlProductLst_etc.DataSource = _productionScheduleDataEdits;
                this.gridViewProductLst_etc.RefreshData();
            }
            //그리드뷰컬럼크기조정
            gridViewProductLst_etc.BestFitColumns();
            //복사되고,수정안되게
            this.gridViewProductLst_etc.OptionsBehavior.ReadOnly = true;
        }


        public void InitialzieGoods(List<ProductData> manufacturedGoodsDatas)
        {
            try
            {
                _collection = new BindingList<ProductDataEdit>();
                for (int i = 0; i < manufacturedGoodsDatas.Count; i++)
                {
                    _collection.Add(new ProductDataEdit(manufacturedGoodsDatas[i]));
                }
                this.gridControlProductLst.DataSource = _collection;
                this.gridViewProductLst.Columns["EditReferenceName"].Visible = false;
                this.gridControlProductLst.RefreshDataSource();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public List<ProductData> GetManufacturedGoodsDatas()
        {
            List<ProductData> manufacturedGoodsDatas = new List<ProductData>();
            try
            {
                for (int i = 0; i < _collection.Count; i++)
                {
                    ProductData manufacturedGoodsData = (ProductData)_collection[i];
                    manufacturedGoodsDatas.Add(manufacturedGoodsData);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return manufacturedGoodsDatas;
        }

        private void gridViewProductLst_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                UpdatePropertyGridByFocusRow(e.FocusedRowHandle);
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

                ProductDataEdit m = _collection[rowIndex];
                this.propertyGridControlProduct.SelectedObject = m;
                
                this.propertyGridControlProduct.Refresh();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        
        private void gridViewProductLst_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        
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

        private void gridViewProductLst_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                UpdatePropertyGridByFocusRow(0);
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
        
        private void propertyGridControlProduct_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            gridViewProductLst.UpdateCurrentRow();
        }

        
        void _NestedObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _dataSource = null;
            gridControlProductLst_etc.DataSource = _dataSource;
            gridViewProductLst_etc.Columns.Clear();        }
        public void productionChanged_Update(object sender,DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            propertyGridControlProduct_FocusedRowChanged(sender, e);
        }
 
        
        private void propertyGridControlProduct_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            _dataSource = null;
            gridControlProductLst_etc.DataSource = _dataSource;
            gridViewProductLst_etc.Columns.Clear();
            check = e;
            try
            {
                etc_form_set(propertyGridControlProduct.FocusedRow);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }
        

        public void etc_form_set(DevExpress.XtraVerticalGrid.Rows.BaseRow row)
        {
            try
            {
                ProductDataEdit a = propertyGridControlProduct.SelectedObject as ProductDataEdit;

                if (row.Name.Contains("IdsOf"))
                {

                    _dataSource = new BindingList<StepData>();

                    _productDataEdit.Clear();
                    object cellkey = propertyGridControlProduct.GetCellValue(row, 0);
                    List<uint> list = cellkey as List<uint>;
                    List<StepData> productDatas = new List<StepData>();

                    foreach (var item in FactoryManager.Instance.StepDatas.Values)
                    {
                        productDatas.Add(item);
                    }
                    for (int i = 0; list.Count > i; i++)
                    {
                        for(int j = 0; productDatas.Count > j; j++)
                        {
                            if (list[i] == productDatas[j].ID)
                            {
                                _dataSource.Add(productDatas[j]);
                            }
                        }
                    }

                    this.gridControlProductLst_etc.DataSource = _dataSource;
                    this.gridControlProductLst_etc.RefreshDataSource();

                    for (int i = 0; i < gridViewProductLst_etc.RowCount; i++)
                    {
                        StepData step_row = gridViewProductLst_etc.GetRow(i) as StepData;

                        if (a.IdsOfStep.Contains(step_row.ID))
                        {
                            gridViewProductLst_etc.SelectRow(i);
                        }
                    }
                }

                else if (row.Name.Contains("Reference"))
                {
                    //굳이 출력하지않아도됨

                }
                else if (row.Name.Contains("rowEditProductionSchedules"))
                {
                    List<ProductionSchedule> datas = new List<ProductionSchedule>();

                    foreach (ProductionSchedule b in a.ProductionSchedules)
                    {
                        datas.Add(b);
                    }

                    _productionScheduleDataEdits = new BindingList<ProductionScheduleDataEdit>();
                    foreach (ProductionSchedule p in datas)
                    {
                        _productionScheduleDataEdits.Add(new ProductionScheduleDataEdit(p));
                    }

                    this.gridControlProductLst_etc.DataSource = _productionScheduleDataEdits;
                    this.gridViewProductLst_etc.RefreshData();
                }
                else
                {

                }

                //그리드뷰컬럼크기조정
                gridViewProductLst_etc.BestFitColumns();
                //복사되고,수정안되게
                this.gridViewProductLst_etc.OptionsBehavior.ReadOnly = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

       


        private void gridControlProductLst_etc_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {
            _dataSource = null;
            gridControlProductLst_etc.DataSource = _dataSource;
            gridViewProductLst_etc.Columns.Clear();
        }

        private void propertyGridControlProduct_Click(object sender, EventArgs e)
        {

        }
    }
}