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

namespace Pinokio.Designer
{
    public partial class SelectStepsModal : DevExpress.XtraEditors.XtraForm
    {
        BindingList<StepData> _stepDatas = new BindingList<StepData>();

        public BindingList<StepData> StepDatas { get => _stepDatas; set => _stepDatas = value; }

        public delegate void MyFormClosedEventHandler(object sender, FormClosedEventArgs e);

        public static event MyFormClosedEventHandler MyFormClosed;


        public SelectStepsModal()
        {
            InitializeComponent();
            DragDropManager.Default.DragOver += OnDragOver;
            DragDropManager.Default.DragDrop += OnDragDrop;
            this.FormClosed += new FormClosedEventHandler(ProductionStepsModel_FormClosed);
        }

        public void InitializeStepDatas( List<uint> stepIdsOfGood, List<StepData> steps)
        {
            try
            {
                
                List<StepData> stepListBoxDatas = new List<StepData>();
                foreach (StepData sd in steps)
                {
                    stepListBoxDatas.Add(sd);
                }
                this.listBoxControlStepList.DataSource = stepListBoxDatas;
                this.listBoxControlStepList.Refresh();


                StepDatas = new BindingList<StepData>();

                foreach (uint id  in stepIdsOfGood)
                {
                    if (stepListBoxDatas.FirstOrDefault(x => x.ID == id) == null)
                    {
                        MessageBox.Show("STEP이 수정되었습니다. 다시 등록해주세요. ID :" + id);
                        continue;
                    }

                    StepDatas.Add(new StepData(steps.Find(x => x.ID == id)));
                }
                this.gridControlSteps.DataSource = StepDatas;
                this.gridControlSteps.RefreshDataSource();
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

            if (e.Target == gridViewSteps)
                OnGridControlDrop(e);
            else if (e.Target == listBoxControlStepList)
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
            gridViewSteps.BeginUpdate();
            gridViewSteps.ClearSelection();
            StepDatas.Insert(index, (StepData)nodes[0]);

            gridViewSteps.EndUpdate();
        }
        //</listBoxControl>
     
        void OnListBoxDrop(DragDropEventArgs e)
        {
            int[] list = e.Data as int[];
            int index = list.First();
            gridViewSteps.BeginUpdate();
            gridViewSteps.ClearSelection();
            StepDatas.RemoveAt(index);

            gridViewSteps.EndUpdate();
        }

        int CalcDestItemIndex(DragDropEventArgs e)
        {
              
            Point hitPoint = gridControlSteps.PointToClient(e.Location);
            int index = gridViewSteps.CalcHitInfo(hitPoint).RowHandle;
            if (e.InsertType == InsertType.After)
                index += 1;
            if (index < 0 && gridViewSteps.RowCount == 0)
                return 0;
            else if (index < 0 && gridViewSteps.RowCount > 0)
                return gridViewSteps.RowCount;
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
        private void ProductionStepsModel_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MyFormClosed != null && this.DialogResult == DialogResult.Cancel)
            {
                MyFormClosed(this, e);
            }
        }
    }
}