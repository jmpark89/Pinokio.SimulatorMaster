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
        public BindingList<StepDataEdit> Collection { get => _collection; }

        BindingList<Equipment> _dataSource = new BindingList<Equipment>();

        BindingList<Equipment> _equipment = new BindingList<Equipment>();
        public BindingList<Equipment> Equipment { get => _equipment; }

        BindingList<InOutProductData> _inoutProductDatas = new BindingList<InOutProductData>();
        public BindingList<InOutProductData> InOutProductDatas { get => _inoutProductDatas; set => _inoutProductDatas = value; }

        BindingList<ProcessingTime> _procTimedataSource = new BindingList<ProcessingTime>();
        public BindingList<ProcessingTime> ProcTimedataSource { get => _procTimedataSource; set => _procTimedataSource = value; }



        public EditStepDlg()
        {
            InitializeComponent();
            SelectEqpModal.MyFormClosed += new SelectEqpModal.MyFormClosedEventHandler(selectEqpModel_FormClosed);
            StepInOutProductsModal.MyFormClosed += new StepInOutProductsModal.MyFormClosedEventHandler(stepInOutProductsModal_FormClosed);
            EditProcessingTimeDlg.MyFormClosed += new EditProcessingTimeDlg.MyFormClosedEventHandler(EditProcessingTimeDlg_FormClosed);

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

        private void EditProcessingTimeDlg_FormClosed(object s, FormClosedEventArgs e)
        {
            _procTimedataSource = null;
            _procTimedataSource = new BindingList<ProcessingTime>();
            gridViewStepData_etc.Columns.Clear();

            if (s != null)
            {
                ProcessingTime b = ((EditProcessingTimeDlg)s).processingTimeDataEdit as ProcessingTime;
                _procTimedataSource = new BindingList<ProcessingTime>();

                _procTimedataSource.Add(b);

                this.gridControlStepData_etc.DataSource = _procTimedataSource;
                this.gridViewStepData_etc.RefreshData();
            }
            //그리드뷰컬럼크기조정
            gridViewStepData_etc.BestFitColumns();
            //복사되고,수정안되게
            this.gridViewStepData_etc.OptionsBehavior.ReadOnly = true;
        }

        public void InitializeStepData(List<StepData> stepDatas)
        {
            try
            {
                _collection.Clear();
                foreach (StepData sd in stepDatas)
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
                
                this.gridViewStepData.Columns["ProcessingTimeData"].Visible = false;
                this.gridViewStepData.Columns["ProcessingUnit"].Visible = false;
                this.gridViewStepData.Columns["CascadingIntervalTime"].Visible = false;
                this.gridViewStepData.Columns["ProcessingProbability"].Visible = false;
                this.gridViewStepData.Columns["SamplingProbability"].Visible = false;
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
                this.propertyGridControl1.RefreshAllProperties();

                STEP_TYPE stepType = p.StepType;
                SetReadOnlyPropertyGrid(stepType);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }




        private void propertyGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            gridViewStepData.UpdateCurrentRow();

            StepDataEdit p = this.propertyGridControl1.SelectedObject as StepDataEdit;
            STEP_TYPE stepType = p.StepType;
            SetReadOnlyPropertyGrid(stepType);
        }
        private void setNonStayStepData()
        {
            foreach (StepDataEdit sd in this._collection)
            {
                if (sd.StepType != STEP_TYPE.STAY)
                {
                    sd.ProcessingUnit = PROCESSING_UNIT.BATCH;
                    sd.CascadingIntervalTime = 0;
                    sd.ProcessingProbability = 0;
                    sd.SamplingProbability = 0;
                }
            }
        }
        private void sbOk_Click(object sender, EventArgs e)
        {
            try
            {
                setNonStayStepData();
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
            this.propertyGridControl1.RefreshAllProperties();
            
            StepDataEdit p = this.propertyGridControl1.SelectedObject as StepDataEdit;
            STEP_TYPE stepType = p.StepType;
            SetReadOnlyPropertyGrid(stepType);
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
                    Dictionary<uint, Tuple<uint, UNIT_TYPE>> productCounts = (Dictionary<uint, Tuple<uint, UNIT_TYPE>>)propertyGridControl1.GetCellValue(e.Row, 0);

                    foreach (ProductData ad in products)
                    {
                        InOutProductData newPD = new InOutProductData(ad);

                        if (productCounts.ContainsKey(newPD.ProductID))
                        {
                            newPD.Value = productCounts[newPD.ProductID].Item1;
                            newPD.UnitType = productCounts[newPD.ProductID].Item2;
                           
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
                    Dictionary<uint, Tuple<uint, UNIT_TYPE>> productCounts = (Dictionary<uint, Tuple<uint, UNIT_TYPE>>)propertyGridControl1.GetCellValue(e.Row, 0);

                    foreach (ProductData ad in products)
                    {
                        InOutProductData newPD = new InOutProductData(ad);
                        if (productCounts.ContainsKey(newPD.ProductID))
                        {
                            newPD.Value = productCounts[newPD.ProductID].Item1;
                            newPD.UnitType = productCounts[newPD.ProductID].Item2;

                            InOutProductDatas.Add(newPD);
                        }
                    }
                    
                    this.gridControlStepData_etc.DataSource = InOutProductDatas;
                    this.gridControlStepData_etc.RefreshDataSource();
                }
                else if (e.Row.Name.Contains("rowProcessingTime"))
                {
                    ProcessingTime procTime = (ProcessingTime)propertyGridControl1.GetCellValue(e.Row, 0);
                    //ProcTimedataSource = procTime;

                    this.gridControlStepData_etc.DataSource = new List<ProcessingTime> { procTime };
                    this.gridControlStepData_etc.RefreshDataSource();
                    this.gridViewStepData_etc.OptionsBehavior.ReadOnly = true;
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

        private void gridViewStepData_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            int rowIndex = e.RowHandle;
            if (_collection.Count == 0)
                return;
            else if (_collection.Count > 0 && rowIndex < 0)
                rowIndex = _collection.Count - 1;

            StepDataEdit p = _collection[rowIndex];

            STEP_TYPE stepType = p.StepType;

            if (e.Column.Name == "colStepType")
            {
                p.StepType = (STEP_TYPE)e.Value;
                stepType = p.StepType;
            }
            else if (e.Column.Name == "colDescription")
                p.Description = e.Value.ToString();

            else if (e.Column.Name == "colName")
                p.Name = e.Value.ToString();

            this.propertyGridControl1.RefreshAllProperties();

            SetReadOnlyPropertyGrid(stepType);
        }

        private void SetReadOnlyPropertyGrid(STEP_TYPE stepType)
        {
            BaseRow procUnitRow = this.propertyGridControl1.GetRowByFieldName("ProcessingUnit");
            BaseRow cacadingRow = this.propertyGridControl1.GetRowByFieldName("CascadingIntervalTime");
            BaseRow procProbRow = this.propertyGridControl1.GetRowByFieldName("ProcessingProbability");
            BaseRow sampleProbRow = this.propertyGridControl1.GetRowByFieldName("SamplingProbability");
            BaseRow outputProductRow = this.propertyGridControl1.GetRowByFieldName("EditOutputProducts");

            if (stepType != STEP_TYPE.STAY)
            {
                
                procUnitRow.Properties.Value = PROCESSING_UNIT.BATCH;
                procUnitRow.Properties.ReadOnly = true;
                
                cacadingRow.Properties.Value = 0;
                cacadingRow.Properties.ReadOnly = true;
                
                procProbRow.Properties.Value = 0;
                procProbRow.Properties.ReadOnly = true;
                
                sampleProbRow.Properties.Value = 0;
                sampleProbRow.Properties.ReadOnly = true;
            }
            else
            {
                StepDataEdit p = this.propertyGridControl1.SelectedObject as StepDataEdit;
                PROCESSING_UNIT procUnit = p.ProcessingUnit;

                if (procUnit != PROCESSING_UNIT.UNIT)
                {
                    cacadingRow.Properties.Value = 0;
                    cacadingRow.Properties.ReadOnly = true;
                }
                else
                    cacadingRow.Properties.ReadOnly = false;
            }

            if (stepType == STEP_TYPE.STAY || stepType == STEP_TYPE.ASSEMBLE)
            {
                outputProductRow.Properties.Value = new Dictionary<uint, Tuple<uint, UNIT_TYPE>>();
                outputProductRow.Properties.ReadOnly = true;
            }
        }
    }
}