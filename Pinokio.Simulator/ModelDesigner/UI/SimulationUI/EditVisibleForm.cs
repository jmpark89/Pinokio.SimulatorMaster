using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pinokio.Designer;
using Pinokio.Animation;
using Simulation.Engine;
using Pinokio.Model.Base;
using devDept.Eyeshot.Entities;

namespace Pinokio.Designer
{
    public partial class EditVisibleForm : DevExpress.XtraEditors.XtraForm
    {
        List<DataInVisualListGrid> dataInVisualListGrid = new List<DataInVisualListGrid>();
        public Dictionary<string, Tuple<bool, bool>> _visibleCheckedInfo = new Dictionary<string, Tuple<bool, bool>>();
        public EditVisibleForm(Dictionary<string, Tuple<bool,bool>> visibleCheckedInfo)
        {
            InitializeComponent();
            _visibleCheckedInfo = visibleCheckedInfo;
            InitializeGirdInVisualList();
        }

        public class DataInVisualListGrid
        {
            public bool Visible { get; set; }
            private bool text;
            public bool Text 
            {
                get { return text; }
                set 
                {
                    if (Visible)
                        text = value;
                    else
                        text = false;
                }
            }
            public string NodeType { get; set; }
        }

        private void InitializeGirdInVisualList()
        {
            try
            {
                dataInVisualListGrid.Clear();
                foreach (var dic in _visibleCheckedInfo)
                {
                    dataInVisualListGrid.Add(new DataInVisualListGrid() { Visible = dic.Value.Item1, Text = dic.Value.Item2, NodeType = dic.Key });
                }

                this.gridControlEditVisible.DataSource = dataInVisualListGrid;
                this.gridControlEditVisible.RefreshDataSource();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("InitializeGirdInVisualList() Error");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            foreach (var rowData in dataInVisualListGrid)
            {
                if (rowData.NodeType == "Blueprint")
                    _visibleCheckedInfo[rowData.NodeType] = new Tuple<bool, bool>(rowData.Visible, false);
                else
                    _visibleCheckedInfo[rowData.NodeType] = new Tuple<bool, bool>(rowData.Visible, rowData.Text);
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gridViewEditVisible_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var VisibleValue = (bool)gridViewEditVisible.GetRowCellValue(e.RowHandle, "Visible");
            var TextValue = (bool)gridViewEditVisible.GetRowCellValue(e.RowHandle, "Text");
            if (VisibleValue && TextValue && e.Column.FieldName == "Visible") // Icon off시 Text도 off
                gridViewEditVisible.SetFocusedRowCellValue("Text", false);
            if (!VisibleValue && !TextValue && e.Column.FieldName == "Text") // Icon off인데 Text true 시도할 경우 막음
                gridViewEditVisible.SetFocusedRowCellValue("Text", false);
        }
    }
}