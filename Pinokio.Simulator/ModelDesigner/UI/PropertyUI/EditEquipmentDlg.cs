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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinokio.Designer
{
    public partial class EditEquipmentDlg : DevExpress.XtraEditors.XtraForm
    {
        BindingList<Equipment> _collection = new BindingList<Equipment>();

        public EditEquipmentDlg()
        {
            InitializeComponent();
        }

        public void InitializeEquipment(List<Equipment> euipments)
        {
            try
            {
                gridViewEquipment.BeginUpdate();
                _collection.Clear();
                foreach (Equipment eqp in euipments)
                {
                    _collection.Add(eqp);
                }
                gridControlEquipment.DataSource = _collection;
                gridControlEquipment.RefreshDataSource();
                ColumnsSet();

                gridViewEquipment.EndUpdate();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        public void ColumnsSet()
        {
            for (int i=0; gridViewEquipment.Columns.Count > i; i++)
            {
                if (i == 0 || i == 1 || i == 2||i==17||i==18)
                {

                }
                else
                {
                    gridViewEquipment.Columns[i].Visible = false;
                }
            }
            //사용
            gridViewEquipment.Columns[0].Caption = "Step Type";
            gridViewEquipment.Columns[1].Caption = "Step Group";
            gridViewEquipment.Columns[2].Caption = "Eqp Group";
            gridViewEquipment.Columns[17].Caption = "ID";
            gridViewEquipment.Columns[18].Caption = "이름";

            this.gridViewEquipment.OptionsBehavior.ReadOnly = true;
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
                
               
                Equipment p = _collection[rowIndex];

                this.propertyGridControlEquipment.SelectedObject = p;
                
                this.propertyGridControlEquipment.Refresh();

                for (int i = 0; this.propertyGridControlEquipment.VisibleRows.Count > i; i++)
                {
                    if (this.propertyGridControlEquipment.VisibleRows[i].Properties.Value!= null && this.propertyGridControlEquipment.VisibleRows[i].Properties.Value.GetType().Name.Contains("List"))
                    {
                        this.propertyGridControlEquipment.VisibleRows[i].Visible = false;
                        i--;
                    }
                }
                
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }       
       
        private void propertyGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            gridViewEquipment.UpdateCurrentRow();
        }

        private void gridViewEquipment_DataSourceChanged(object sender, EventArgs e)
        {
            UpdatePropertyGridByFocusRow(0);
        }

        private void gridViewEquipment_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.propertyGridControlEquipment.Refresh();
        }

        private void propertyGridControl1_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            
        }

    }
}