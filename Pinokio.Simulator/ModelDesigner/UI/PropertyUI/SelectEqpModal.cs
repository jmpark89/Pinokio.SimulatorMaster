using DevExpress.Data;
using DevExpress.XtraEditors;
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
    public partial class SelectEqpModal : DevExpress.XtraEditors.XtraForm
    {
        BindingList<Equipment> _dataSource = new BindingList<Equipment>();
        public List<Equipment> SelectEqpDatas = new List<Equipment>();
        public delegate void MyFormClosedEventHandler(object sender, FormClosedEventArgs e);
        public static event MyFormClosedEventHandler MyFormClosed;

        public SelectEqpModal()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(SelectEqpModel_FormClosed);
        }

        public void InitializeNodeDatas(List<Equipment> nodeDatas, List<uint> nodeIDs)
        {
            try
            {
                _dataSource = new BindingList<Equipment>();
                foreach(Equipment ad in nodeDatas)
                {
                    _dataSource.Add(ad);
                }
                this.gridControlEqp.DataSource = _dataSource;
                this.gridControlEqp.RefreshDataSource();

                for(int i = 0; i < gridViewEqp.RowCount; i++)
                {
                    Equipment row = gridViewEqp.GetRow(i) as Equipment;
                    if (nodeIDs.Contains(row.ID))
                        gridViewEqp.SelectRow(i);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

        private void gridViewSteps_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

        }

        private void sbOK_Click(object sender, EventArgs e)
        {
            try
            {

                SelectEqpDatas = new List<Equipment>();
                foreach (int selectIndex in this.gridViewEqp.GetSelectedRows())
                {
                    SelectEqpDatas.Add((Equipment)_dataSource[selectIndex]);
                }

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
        private void SelectEqpModel_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MyFormClosed != null)
            {
                MyFormClosed(this, e);
            }
        }
    }
}