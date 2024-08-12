using DevExpress.Utils.DragDrop;
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
using Pinokio.Model.Base;

namespace Pinokio.Designer
{
    public partial class StepInOutProductsModal : DevExpress.XtraEditors.XtraForm
    {
        BindingList<InOutProductData> _inoutProductDatas = new BindingList<InOutProductData>();
        BindingList<InOutProductData> _candidatedProductDatas = new BindingList<InOutProductData>();

        public BindingList<InOutProductData> InOutProductDatas { get => _inoutProductDatas; set => _inoutProductDatas = value; }
        public BindingList<InOutProductData> CandidatedProductDatas { get => _candidatedProductDatas; set => _candidatedProductDatas = value; }

        public delegate void MyFormClosedEventHandler(object sender, FormClosedEventArgs e);

        public static event MyFormClosedEventHandler MyFormClosed;

        public StepInOutProductsModal()
        {
            InitializeComponent();
            DragDropManager.Default.DragOver += OnDragOver;
            DragDropManager.Default.DragDrop += OnDragDrop;
            this.FormClosed += new FormClosedEventHandler(StepInOutProductsModal_FormClosed);
        }

        public void InitializeProductDatas(Dictionary<uint, Tuple<uint, UNIT_TYPE>> productCounts, List<ProductData> products)
        {
            try
            {
                InOutProductDatas = new BindingList<InOutProductData>();
                CandidatedProductDatas = new BindingList<InOutProductData>();
                foreach (ProductData pd in products)
                {
                    InOutProductData newPD = new InOutProductData(pd);
                    if (productCounts.ContainsKey(newPD.ProductID))
                    {
                        newPD.Value = productCounts[newPD.ProductID].Item1;
                        newPD.UnitType = productCounts[newPD.ProductID].Item2;

                        InOutProductDatas.Add(newPD);
                    }
                    else
                    {
                        CandidatedProductDatas.Add(newPD);
                    }
                }

                this.gridControlProducts.DataSource = InOutProductDatas;
                this.gridControlProducts.RefreshDataSource();
                this.listBoxControlProductList.DataSource = CandidatedProductDatas;
                this.listBoxControlProductList.Refresh();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        void OnDragDrop(object sender, DragDropEventArgs e)
        {
            if (object.ReferenceEquals(e.Source, e.Target))
                return;
            e.Handled = true;

            if (e.Target == gridViewProducts)
                OnGridControlDrop(e);
            else if (e.Target == listBoxControlProductList)
                OnListBoxDrop(e);

            Cursor.Current = Cursors.Default;
        }
        //<listBoxControl>
        void OnGridControlDrop(DragDropEventArgs e)
        {
            var nodes = e.GetData<IList<object>>();
            if (nodes == null)
                return;
            int index = CalcDestItemIndex(e);
            gridViewProducts.BeginUpdate();
            gridViewProducts.ClearSelection();
            InOutProductDatas.Insert(index, (InOutProductData)nodes[0]);
            gridViewProducts.EndUpdate();

            listBoxControlProductList.BeginUpdate();
            CandidatedProductDatas.Remove((InOutProductData)nodes[0]);
            listBoxControlProductList.EndUpdate();
        }
        //</listBoxControl>
     
        void OnListBoxDrop(DragDropEventArgs e)
        {
            int[] list = e.Data as int[];
            int index = list.First();
            listBoxControlProductList.BeginUpdate();
            CandidatedProductDatas.Insert(index, (InOutProductData)_inoutProductDatas[index]);
            listBoxControlProductList.EndUpdate();
            gridViewProducts.BeginUpdate();
            gridViewProducts.ClearSelection();
            InOutProductDatas.RemoveAt(index);
            gridViewProducts.EndUpdate();
        }

        int CalcDestItemIndex(DragDropEventArgs e)
        {
              
            Point hitPoint = gridControlProducts.PointToClient(e.Location);
            int index = gridViewProducts.CalcHitInfo(hitPoint).RowHandle;
            if (e.InsertType == InsertType.After)
                index += 1;
            if (index < 0 && gridViewProducts.RowCount == 0)
                return 0;
            else if (index < 0 && gridViewProducts.RowCount > 0)
                return gridViewProducts.RowCount;
            return index;
        }

        void OnDragOver(object sender, DragOverEventArgs e)
        {
            if (object.ReferenceEquals(e.Source, e.Target))
                return;
            e.Default();
            e.Action = IsCopy(e.KeyState) ? DragDropActions.Copy : DragDropActions.Move;
            Cursor current = Cursors.No;
            if (e.Action != DragDropActions.None)
                current = Cursors.Default;
            e.Cursor = current;
        }
        bool IsCopy(DragDropKeyState key)
        {
            return (key & DragDropKeyState.Control) != 0;
        }

        private void sbOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void StepInOutProductsModal_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MyFormClosed != null && this.DialogResult != DialogResult.Cancel)
            {
                MyFormClosed(this, e);
            }
        }

    }
}