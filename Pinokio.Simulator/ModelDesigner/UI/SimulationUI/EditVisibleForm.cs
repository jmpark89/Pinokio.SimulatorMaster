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
        Dictionary<string, List<SimNodeTreeListNode>> _dicSimNodeType = new Dictionary<string, List<SimNodeTreeListNode>>();
        public Dictionary<bool, List<SimNodeTreeListNode>> visibilityNodes = new Dictionary<bool, List<SimNodeTreeListNode>>();
        public Dictionary<string, bool> _visibleCheckedInfo = new Dictionary<string, bool>();
        public EditVisibleForm(Dictionary<string, List<SimNodeTreeListNode>> dicSimNodeType, Dictionary<string, bool> visibleCheckedInfo)
        {
            InitializeComponent();
            _dicSimNodeType = dicSimNodeType;
            _visibleCheckedInfo = visibleCheckedInfo;
            InitializeGirdInVisualList(_dicSimNodeType);
        }
        public class DataInVisualListGrid
        {
            public bool Visible
            {
                get; set;
            }

            public string NodeType { get; set; }
        }
        private void InitializeGirdInVisualList(Dictionary<string, List<SimNodeTreeListNode>> dicSimNodeType)
        {
            try
            {
                dataInVisualListGrid.Clear();
                foreach (var dic in _visibleCheckedInfo)
                {
                    dataInVisualListGrid.Add(new DataInVisualListGrid() { Visible = dic.Value, NodeType = dic.Key });
                }
                foreach (var dic in dicSimNodeType)
                {
                    if (_visibleCheckedInfo.ContainsKey(dic.Key))
                        dataInVisualListGrid.Add(new DataInVisualListGrid() { Visible = _visibleCheckedInfo[dic.Key], NodeType = dic.Key});
                    else
                        dataInVisualListGrid.Add(new DataInVisualListGrid() { Visible = !dic.Value.Any(n => n.Checked == false), NodeType = dic.Key });
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
            visibilityNodes = new Dictionary<bool, List<SimNodeTreeListNode>>();
            visibilityNodes[true] = new List<SimNodeTreeListNode>();
            visibilityNodes[false] = new List<SimNodeTreeListNode>();
            _visibleCheckedInfo = new Dictionary<string, bool>();

            foreach (var rowData in dataInVisualListGrid)
            {
                #region cho 추가
                if (rowData.NodeType == "Blueprint")
                {
                    _visibleCheckedInfo[rowData.NodeType] = rowData.Visible;
                    continue;
                }
                if (rowData.NodeType == "Label")
                {
                    _visibleCheckedInfo[rowData.NodeType] = rowData.Visible;
                    continue;
                } 
                #endregion
                if (rowData.Visible == false)
                {
                    foreach (SimNodeTreeListNode node in _dicSimNodeType[rowData.NodeType])
                    {
                        visibilityNodes[false].Add(node);
                    }
                }
                else
                {
                    foreach (SimNodeTreeListNode node in _dicSimNodeType[rowData.NodeType])
                    {
                        visibilityNodes[true].Add(node);
                    }
                }
                _visibleCheckedInfo[rowData.NodeType] = rowData.Visible;
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}