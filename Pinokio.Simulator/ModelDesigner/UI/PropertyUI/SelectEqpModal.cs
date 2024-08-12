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
                gridViewEqp.Columns["NodeID"].VisibleIndex = 1;
                gridViewEqp.Columns["NodeID"].Caption = "ID";
                gridViewEqp.Columns["Name"].VisibleIndex = 2;
                gridViewEqp.Columns["Name"].Caption = "Name";
                gridViewEqp.Columns["ParentNodeName"].VisibleIndex = 3;
                gridViewEqp.Columns["ParentNodeName"].Caption = "Parent Name";
                gridViewEqp.Columns["StepType"].Caption = "Step Type";
                gridViewEqp.Columns["StepGroupName"].Caption = "Step Group";
                gridViewEqp.Columns["EqpGroupName"].Caption = "Eqp Group";
                gridViewEqp.Columns["DispatchingCapa"].Caption = "Dispatching Capa";
                gridViewEqp.Columns["ProcessingCapa"].Caption = "Processing Capa";
                gridViewEqp.Columns["DispatchingType"].Caption = "Dispatching Type";
                gridViewEqp.Columns["Bay"].Caption = "Bay Name";
                gridViewEqp.Columns["Direction"].Visible = false;
                gridViewEqp.Columns["Size"].Visible = false;
                gridViewEqp.Columns["Quantity"].Visible = false;
                gridViewEqp.Columns["AngleInRadians"].Visible = false;
                gridViewEqp.Columns["RotateAxis"].Visible = false;
                gridViewEqp.Columns["LoadLevel"].Visible = false;
                gridViewEqp.Columns["WayPointDistance"].Visible = false;
                gridViewEqp.Columns["ReadyQuantities"].Visible = false;
                gridViewEqp.Columns["Floor"].Visible = false;
                gridViewEqp.Columns["InLinkCount"].Visible = false;
                gridViewEqp.Columns["OutLinkCount"].Visible = false;
                gridViewEqp.Columns["PosVec3"].Visible = false;
                gridViewEqp.Columns["Height"].Visible = false;
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
            if (MyFormClosed != null && this.DialogResult == DialogResult.Cancel)
            {
                MyFormClosed(this, e);
            }
        }
    }
}