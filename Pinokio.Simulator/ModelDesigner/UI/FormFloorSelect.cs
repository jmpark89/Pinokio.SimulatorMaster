using DevExpress.XtraEditors;
//TODO devDept: Now you need to pass through the Model.ActiveViewport property to get/set the active viewport grid.
using DevExpress.XtraGrid.Views.Grid;
using Pinokio.Animation;
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
    public partial class FormFloorSelect : DevExpress.XtraEditors.XtraForm
    {
        public double LastZoomRatio;

        public bool IsOkay;
        public bool IsChanged;

        public Dictionary<uint, CopiedFloorPlan> CopiedDicFloorPlan;
        public List<FloorPlan> LstFloorPlan;
        public List<FloorPlan> LstRemovedFloorPlan;
        public List<uint> LstFloorplanHasChildernIds;
        public List<uint> LstRemovedPlanIds;
        public List<uint> LstUpdatedPlanIds;

        public struct CopiedFloorPlan
        {
            public uint ID;
            public int FloorNum;
            public string FloorName;
            public int FloorWidth;
            public int FloorDepth;
            public int FloorBottom;
            public int ZoomRatio;


            public CopiedFloorPlan(uint ID, int floorNum, string floorName, int floorWidth, int floorDepth, int floorBottom)
            {
                this.ID = ID;
                FloorNum = floorNum;
                FloorName = floorName;
                FloorWidth = floorWidth;
                FloorDepth = floorDepth;
                FloorBottom = floorBottom;
                this.ZoomRatio = 1;
            }
        }



        public FormFloorSelect(List<FloorPlan> lstFloorPlan)
        {
            InitializeComponent();
            LstFloorPlan = lstFloorPlan;
            gridControl1.DataSource = LstFloorPlan;
            gridView1.Columns[8].OptionsColumn.AllowEdit = false;
            gridView1.Columns[9].Visible = false;
        }

        public void ResetFloorPlan(List<FloorPlan> lstFloorPlan)
        {
            LstFloorPlan = lstFloorPlan;
            gridControl1.DataSource = LstFloorPlan;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            IsOkay = true;
            ResetFloorPlan(this.LstFloorPlan);
            LstFloorplanHasChildernIds = new List<uint>();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (LstFloorPlan.Count > 0)
            {
                LstFloorPlan.Add(new FloorPlan(LstFloorPlan[LstFloorPlan.Count - 1].FloorNum + 1, (LstFloorPlan[LstFloorPlan.Count - 1].FloorNum + 1).ToString(),
                   100000, 100000, LstFloorPlan[LstFloorPlan.Count - 1].FloorBottom + ModelManager.Instance.FloorHeight));
            }
            else
            {
                LstFloorPlan.Add(new FloorPlan(1, "Default", 100000, 100000, 0));
            }
            RefreshGridView1();
            IsChanged = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            uint ID = LstFloorPlan[gridView1.FocusedRowHandle].Floor.ID;
            if (ID > 0)
            {
                if (LstFloorplanHasChildernIds.Contains(ID))
                    MessageBox.Show("하위 Node가 존재하여 삭제할 수 없습니다.");
                else
                {
                    LstRemovedPlanIds.Add(ID);
                    LstRemovedFloorPlan.Add(LstFloorPlan[gridView1.FocusedRowHandle]);
                    LstFloorPlan.Remove(LstFloorPlan[gridView1.FocusedRowHandle]);
                    if (LstUpdatedPlanIds.Contains(ID))
                        LstUpdatedPlanIds.Remove(ID);
                    RefreshGridView1();
                }
            }
            IsChanged = true;
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridView selview = sender as GridView;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "png file (*.png)|*.png|jpg file (*.jpg)|*.jpg|All files (*.*)|*.*";
            if (selview == gridView1 && e.Column.AbsoluteIndex == 8)
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string pathValue = openFileDialog.FileName;
                    Bitmap bmp = new Bitmap(pathValue);

                    //사진 로드했을 시 디폴트 값 100
                    LstFloorPlan[e.RowHandle].ZoomRatio = 100;

                    bmp.Save(LstFloorPlan[e.RowHandle].FloorNum.ToString() + ".jpg");
                    LstFloorPlan[e.RowHandle].FloorPlanPath = pathValue;
                    LstFloorPlan[e.RowHandle].FloorWidth = Convert.ToInt32(bmp.Width * LstFloorPlan[e.RowHandle].ZoomRatio);
                    LstFloorPlan[e.RowHandle].FloorDepth = Convert.ToInt32(bmp.Height * LstFloorPlan[e.RowHandle].ZoomRatio);

                    LstFloorPlan[e.RowHandle].ImgWidth = bmp.Width;
                    LstFloorPlan[e.RowHandle].ImgHeight = bmp.Height;

                    RefreshGridView1();
                    IsChanged = true;
                }
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            FloorPlan EdittingFloor = LstFloorPlan[e.RowHandle];
            int i = 0;
            switch (e.Column.AbsoluteIndex)
            {
                case 0:
                    if (int.TryParse((string)e.Value, out i))
                        EdittingFloor.FloorNum = Convert.ToInt32(e.Value);
                    break;
                case 1:
                    EdittingFloor.FloorName = e.Value.ToString();
                    break;
                case 2:
                    if(int.TryParse((string)e.Value, out i))
                        EdittingFloor.FloorWidth = i;
                    break;
                case 3:
                    if (int.TryParse((string)e.Value, out i))
                        EdittingFloor.FloorDepth = i;
                    break;
                case 4:
                    if (int.TryParse((string)e.Value, out i))
                        EdittingFloor.FloorBottom = i;
                    break;
                case 5:
                    if (int.TryParse((string)e.Value, out i))
                        EdittingFloor.ZoomRatio = i;
                    break;
                case 6:
                    if (int.TryParse((string)e.Value, out i))
                        EdittingFloor.ImgWidth = i;
                    break;
                case 7:
                    if (int.TryParse((string)e.Value, out i))
                        EdittingFloor.ImgHeight = i;
                    break;
            }
            if (!LstUpdatedPlanIds.Contains(LstFloorPlan[e.RowHandle].Floor.ID))
                LstUpdatedPlanIds.Add(LstFloorPlan[e.RowHandle].Floor.ID);
            RefreshGridView1();
            IsChanged = true;
        }

        public void RefreshGridView1()
        {
            gridView1.RefreshData();
        }

        private void FormFloorSelect_Resize(object sender, EventArgs e)
        {
            this.splitContainerControl1.SplitterPosition = this.splitContainerControl1.Size.Height - 50;
        }

        private void FormFloorSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsOkay && IsChanged)
            {
                if (MessageBox.Show("CHANGES WILL NOT BE SAVED!", "WARNING", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    LstFloorPlan.AddRange(LstRemovedFloorPlan);
                    LstRemovedFloorPlan.Clear();

                    foreach (FloorPlan floorPlan in LstFloorPlan)
                    {
                        if (!CopiedDicFloorPlan.ContainsKey(floorPlan.Floor.ID))
                        {
                            ModelManager.Instance.DicCoupledModel.Remove(floorPlan.Floor.ID);
                            ModelManager.Instance.SimNodes.Remove(floorPlan.Floor.ID);
                            ModelManager.Instance.SimNodesByName.Remove(floorPlan.FloorName);
                            LstRemovedFloorPlan.Add(floorPlan);
                        }
                        else
                        {
                            floorPlan.FloorNum = CopiedDicFloorPlan[floorPlan.Floor.ID].FloorNum;
                            floorPlan.FloorName = CopiedDicFloorPlan[floorPlan.Floor.ID].FloorName;
                            floorPlan.FloorWidth = CopiedDicFloorPlan[floorPlan.Floor.ID].FloorWidth;
                            floorPlan.FloorDepth = CopiedDicFloorPlan[floorPlan.Floor.ID].FloorDepth;
                            floorPlan.FloorBottom = CopiedDicFloorPlan[floorPlan.Floor.ID].FloorBottom;
                        }
                    }
                    foreach (FloorPlan floorPlan in LstRemovedFloorPlan)
                        LstFloorPlan.Remove(floorPlan);

                    LstRemovedPlanIds.Clear();
                    LstUpdatedPlanIds.Clear();
                }
            }
        }
    }
}