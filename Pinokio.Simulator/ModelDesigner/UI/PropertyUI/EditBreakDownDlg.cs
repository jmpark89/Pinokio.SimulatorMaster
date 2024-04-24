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
    public partial class EditBreakDownDlg : DevExpress.XtraEditors.XtraForm
    {
        BindingList<BreakDownEdit> _collection = new BindingList<BreakDownEdit>();
        public BindingList<BreakDownEdit> Collection { get => _collection; }
        public BindingList<StepData> _dataSource = new BindingList<StepData>();
        BindingList<ProductDataEdit> _productDataEdit = new BindingList<ProductDataEdit>();
        public BindingList<ProductDataEdit> ProductDataEdit { get => _productDataEdit; }
        BindingList<ProductionScheduleDataEdit> _productionScheduleDataEdits = new BindingList<ProductionScheduleDataEdit>();
        public BindingList<ProductionScheduleDataEdit> ProductionScheduleDataEdits { get => _productionScheduleDataEdits; }
        public static DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs check;
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxCate1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxCate2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();


        public EditBreakDownDlg()
        {
            InitializeComponent();
            repositoryItemComboBoxCate1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            repositoryItemComboBoxCate1.Items.Add("NONE");
            repositoryItemComboBoxCate2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            repositoryItemComboBoxCate2.Items.Add("NONE");

            InitialzieGoods();
            //ProductionScheduleEditingDlg.MyFormClosed += new ProductionScheduleEditingDlg.MyFormClosedEventHandler(productionScheduleEditingDlg_FormClosed);
            //SelectStepsModal.MyFormClosed += new SelectStepsModal.MyFormClosedEventHandler(productionStepsModel_FormClosed);
        }
        private void productionStepsModel_FormClosed(object s, FormClosedEventArgs e)
        {
            if (s != null)
            {
                _dataSource = null;

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

                
            }

        }
        private void productionScheduleEditingDlg_FormClosed(object s, FormClosedEventArgs e)
        {
            _dataSource = null;

            if (s!=null)
            {
                
                List<ProductionSchedule> b = ((ProductionScheduleEditingDlg)s).productionScheduleDataEdits as List<ProductionSchedule>;
                _productionScheduleDataEdits = new BindingList<ProductionScheduleDataEdit>();
                foreach (ProductionSchedule p in b)
                {
                    _productionScheduleDataEdits.Add(new ProductionScheduleDataEdit(p));
                }

            }
        }
        public void InitialzieGoods()
        {
            try
            {
                _collection = new BindingList<BreakDownEdit>();
                //_collection.Add(new BreakDownEdit(BreakDown.PMCategory.Step, BreakDown.PMType.TimeBased, BreakDown.PMPeriod.Monthly));
                //_collection.Add(new BreakDownEdit(BreakDown.PMCategory.EqpGroup, BreakDown.PMType.TimeBased, BreakDown.PMPeriod.Monthly));
                //_collection.Add(new BreakDownEdit(BreakDown.PMCategory.StepType, BreakDown.PMType.TimeBased, BreakDown.PMPeriod.Monthly));

                this.gridControlBreakDownList.DataSource = _collection;
                DevExpress.XtraGrid.Columns.GridColumn colDetailCate = this.gridViewBreakDownList.Columns["DetailCategory"];
                colDetailCate.ColumnEdit = repositoryItemComboBoxCate1;
                DevExpress.XtraGrid.Columns.GridColumn colEqpNames = this.gridViewBreakDownList.Columns["EQPNames"];
                colEqpNames.ColumnEdit = repositoryItemComboBoxCate2;
              

                this.gridViewBreakDownList.CellValueChanged += GridView_CellValueChanged;
                this.gridViewBreakDownList.FocusedRowChanged += GridView__FocusedRowChanged;
                this.gridControlBreakDownList.RefreshDataSource();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        private void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            

            if (e.Column.FieldName == "Category")
            {
                // Value in TestColumn1 has changed
                string newValue = e.Value.ToString();
                repositoryItemComboBoxCate1.Items.Clear();
                repositoryItemComboBoxCate2.Items.Clear();
                // Perform actions based on the new value
                // ...
                switch (newValue)
                {
                    case "Step":
                        // Code to handle Option3
                        var uniqueSteps = FactoryManager.Instance.StepDatas.Values.Select(value => value.Name).Distinct().ToList();
                        repositoryItemComboBoxCate1.Items.AddRange(uniqueSteps);
                        break;

                    case "StepGroup":
                        // Code to handle Option1
                        //List<Equipment> eqp = FactoryManager.Instance.Eqps.Values.ToList();
                        var uniqueStepGroupNames = FactoryManager.Instance.Eqps.Values.Select(value => value.StepGroupName).Distinct().ToList();
                        repositoryItemComboBoxCate1.Items.AddRange(uniqueStepGroupNames);
                        break;

                    case "StepType":
                        // Code to handle Option3
                        var uniqueStepTypes = FactoryManager.Instance.Eqps.Values.Select(value => value.StepType).Distinct().ToList();
                        repositoryItemComboBoxCate1.Items.AddRange(uniqueStepTypes);
                        break;

                    case "EqpGroup":
                        // Code to handle Option2
                        var uniqueEqpGroupNames = FactoryManager.Instance.Eqps.Values.Select(value => value.EqpGroupName).Distinct().ToList();
                        repositoryItemComboBoxCate1.Items.AddRange(uniqueEqpGroupNames);
                        break;
                    case "Eqp":
                        // Code to handle Option2
                        repositoryItemComboBoxCate1.Items.Add("-");
                        break;
                    default:
                        // Code to handle other cases or a default case
                        break;
                }
                if (repositoryItemComboBoxCate1.Items.Count > 0)
                {
                    gridViewBreakDownList.SetRowCellValue(e.RowHandle, "DetailCategory", repositoryItemComboBoxCate1.Items[0]);
                }
            }
            else if (e.Column.FieldName == "DetailCategory")
            {
                string categoryValue = gridViewBreakDownList.GetRowCellValue(e.RowHandle, "Category").ToString();
                string DetailValue = e.Value.ToString();
                repositoryItemComboBoxCate2.Items.Clear();
                switch (categoryValue)
                {
                    case "Step":
                        // Code to handle Option3
                        var uniqueSteps = FactoryManager.Instance.StepDatas.Values.Where(value => value.Name == DetailValue).SelectMany(value => value.IdsOfEqp).Distinct().ToList();
                        var uniqueStepsEqp = FactoryManager.Instance.Eqps.Values.Where(equipment => uniqueSteps.Contains(equipment.ID)).Select(equipment => equipment.Name).ToList();
                        repositoryItemComboBoxCate2.Items.AddRange(uniqueStepsEqp);
                        break;

                    case "StepGroup":
                        // Code to handle Option1
                        //List<Equipment> eqp = FactoryManager.Instance.Eqps.Values.ToList();
                        var uniqueStepGroupNames = FactoryManager.Instance.Eqps.Values.Where(value => value.StepGroupName == DetailValue).Select(value => value.Name).ToList();
                        repositoryItemComboBoxCate2.Items.AddRange(uniqueStepGroupNames);
                        break;

                    case "StepType":
                        // Code to handle Option3
                        var uniqueStepTypes = FactoryManager.Instance.Eqps.Values.Where(value => value.StepType.ToString() == DetailValue).Select(value => value.Name).ToList();
                        repositoryItemComboBoxCate2.Items.AddRange(uniqueStepTypes);
                        break;

                    case "EqpGroup":
                        // Code to handle Option2
                        var uniqueEqpGroupNames = FactoryManager.Instance.Eqps.Values.Where(value => value.EqpGroupName == DetailValue).Select(value => value.Name).ToList();
                        repositoryItemComboBoxCate2.Items.AddRange(uniqueEqpGroupNames);
                        break;
                    case "Eqp":
                        // Code to handle Option2
                        var uniqueEqpNames = FactoryManager.Instance.Eqps.Values.Select(value => value.Name).ToList();
                        repositoryItemComboBoxCate2.Items.AddRange(uniqueEqpNames);
                        break;
                    default:
                        // Code to handle other cases or a default case
                        break;
                }
                if (repositoryItemComboBoxCate2.Items.Count > 0)
                {
                    gridViewBreakDownList.SetRowCellValue(e.RowHandle, "EQPNames", repositoryItemComboBoxCate2.Items[0]);
                }
            }
            else if (e.Column.FieldName == "EQPNames")
            {
                UpdatePropertyGridByFocusRow(e.RowHandle);
            }
            gridControlBreakDownList.RefreshDataSource();
        }
        public List<ProductData> GetManufacturedGoodsDatas()
        {
            List<ProductData> manufacturedGoodsDatas = new List<ProductData>();
            try
            {
                //for (int i = 0; i < _collection.Count; i++)
                //{
                //    ProductData manufacturedGoodsData = (ProductData)_collection[i];
                //    manufacturedGoodsDatas.Add(manufacturedGoodsData);
                //}
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return manufacturedGoodsDatas;
        }

        private void GridView__FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                string categoryValue = gridViewBreakDownList.GetRowCellValue(e.FocusedRowHandle, "Category").ToString();
                string DetailValue = gridViewBreakDownList.GetRowCellValue(e.FocusedRowHandle, "DetailCategory").ToString();

                if (categoryValue == "None")
                {
                    repositoryItemComboBoxCate1.Items.Clear();
                    repositoryItemComboBoxCate2.Items.Clear();
                    repositoryItemComboBoxCate1.Items.Add("None");
                    repositoryItemComboBoxCate2.Items.Add("None");
                    return;
                }
                //if (DetailValue == "None")
                //{
                //    repositoryItemComboBoxCate2.Items.Clear();
                //    repositoryItemComboBoxCate2.Items.Add("None");
                //    return;
                //}
                repositoryItemComboBoxCate1.Items.Clear();
                repositoryItemComboBoxCate2.Items.Clear();
                // Perform actions based on the new value
                // ...
                switch (categoryValue)
                {
                    case "Step":
                        // Code to handle Option3
                        var uniqueSteps = FactoryManager.Instance.StepDatas.Values.Select(value => value.Name).Distinct().ToList();
                        repositoryItemComboBoxCate1.Items.AddRange(uniqueSteps);
                        break;

                    case "StepGroup":
                        // Code to handle Option1
                        //List<Equipment> eqp = FactoryManager.Instance.Eqps.Values.ToList();
                        var uniqueStepGroupNames = FactoryManager.Instance.Eqps.Values.Select(value => value.StepGroupName).Distinct().ToList();
                        repositoryItemComboBoxCate1.Items.AddRange(uniqueStepGroupNames);
                        break;

                    case "StepType":
                        // Code to handle Option3
                        var uniqueStepTypes = FactoryManager.Instance.Eqps.Values.Select(value => value.StepType).Distinct().ToList();
                        repositoryItemComboBoxCate1.Items.AddRange(uniqueStepTypes);
                        break;

                    case "EqpGroup":
                        // Code to handle Option2
                        var uniqueEqpGroupNames = FactoryManager.Instance.Eqps.Values.Select(value => value.EqpGroupName).Distinct().ToList();
                        repositoryItemComboBoxCate1.Items.AddRange(uniqueEqpGroupNames);
                        break;
                    case "Eqp":
                        // Code to handle Option2
                        repositoryItemComboBoxCate1.Items.Add("-");
                        break;
                    default:
                        // Code to handle other cases or a default case
                        break;
                }

                switch (categoryValue)
                {
                    case "Step":
                        // Code to handle Option3
                        var uniqueSteps = FactoryManager.Instance.StepDatas.Values.Where(value => value.Name == DetailValue).Select(value => value.Name).ToList();
                        repositoryItemComboBoxCate2.Items.AddRange(uniqueSteps);
                        break;

                    case "StepGroup":
                        // Code to handle Option1
                        //List<Equipment> eqp = FactoryManager.Instance.Eqps.Values.ToList();
                        var uniqueStepGroupNames = FactoryManager.Instance.Eqps.Values.Where(value => value.StepGroupName == DetailValue).Select(value => value.Name).ToList();
                        repositoryItemComboBoxCate2.Items.AddRange(uniqueStepGroupNames);
                        break;

                    case "StepType":
                        // Code to handle Option3
                        var uniqueStepTypes = FactoryManager.Instance.Eqps.Values.Where(value => value.StepType.ToString() == DetailValue).Select(value => value.Name).ToList();
                        repositoryItemComboBoxCate2.Items.AddRange(uniqueStepTypes);
                        break;

                    case "EqpGroup":
                        // Code to handle Option2
                        var uniqueEqpGroupNames = FactoryManager.Instance.Eqps.Values.Where(value => value.EqpGroupName == DetailValue).Select(value => value.Name).ToList();
                        repositoryItemComboBoxCate2.Items.AddRange(uniqueEqpGroupNames);
                        break;
                    case "Eqp":
                        // Code to handle Option2
                        var uniqueEqpNames = FactoryManager.Instance.Eqps.Values.Select(value => value.Name).ToList();
                        repositoryItemComboBoxCate2.Items.AddRange(uniqueEqpNames);
                        break;
                    default:
                        // Code to handle other cases or a default case
                        break;
                }
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

                string eqpName = _collection[rowIndex].EQPNames;
                Equipment eqp = FactoryManager.Instance.Eqps.Values.FirstOrDefault(item => item.Name == eqpName);

                this.propertyGridControlBreakDown.SelectedObject = eqp;

                //this.propertyGridControlBreakDown.Refresh();

                for (int i = 0; this.propertyGridControlBreakDown.VisibleRows.Count > i; i++)
                {
                    var visibleRow = this.propertyGridControlBreakDown.VisibleRows[i];
                    var visbleRowInfo = this.propertyGridControlBreakDown.ViewInfo;
                    // Value의 타입이 List를 포함하고 있는지 확인
                    if (visibleRow.Properties.Value != null && visibleRow.Properties.Value.GetType().Name.Contains("List"))
                    {
                        // List를 포함하는 경우 해당 row를 숨김 처리
                        visibleRow.Visible = false;
                        i--;  // i를 감소시켜서 한 칸 앞으로 이동
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void gridViewBreakDownLst_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)

        {
            try
            {
                if (e.Column.FieldName != "ProductName")
                {
                    UpdatePropertyGridByFocusRow(e.RowHandle);
                }

            }
            catch (Exception ex) 
            {
                ErrorLogger.SaveLog(ex);
            }

        }

        private void gridViewBreakDownLst_DataSourceChanged(object sender, EventArgs e)
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

        private void propertyGridControlBreakDown_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            gridViewBreakDownList.UpdateCurrentRow();
        }

        void _NestedObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _dataSource = null;
        }

        public void productionChanged_Update(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            propertyGridControlBreakDown_FocusedRowChanged(sender, e);
        }

        private void propertyGridControlBreakDown_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            _dataSource = null;
            check = e;
            try
            {
                etc_form_set(propertyGridControlBreakDown.FocusedRow);
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
                ProductDataEdit a = propertyGridControlBreakDown.SelectedObject as ProductDataEdit;

                if (row.Name.Contains("IdsOf"))
                {

                    _dataSource = new BindingList<StepData>();

                    _productDataEdit.Clear();
                    object cellkey = propertyGridControlBreakDown.GetCellValue(row, 0);
                    List<uint> list = cellkey as List<uint>;
                    List<StepData> productDatas = new List<StepData>();

                    foreach (var item in FactoryManager.Instance.StepDatas.Values)
                    {
                        productDatas.Add(item);
                    }
                    for (int i = 0; list.Count > i; i++)
                    {
                        for (int j = 0; productDatas.Count > j; j++)
                        {
                            if (list[i] == productDatas[j].ID)
                            {
                                _dataSource.Add(productDatas[j]);
                            }
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

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void gridControlBreakDownLst_etc_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {
            _dataSource = null;
        }

        private void propertyGridControlBreakDown_Click(object sender, EventArgs e)
        {

        }
    }
}

