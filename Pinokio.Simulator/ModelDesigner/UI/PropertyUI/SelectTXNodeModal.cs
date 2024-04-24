using DevExpress.XtraEditors;
using Logger;
using Pinokio.Model.Base;
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
    public partial class SelectTXNodeModal : DevExpress.XtraEditors.XtraForm
    {
        public uint GetCheckIndex(uint currentID)
        {

            try
            {
                foreach (int index in this.checkedListBoxControlNodeList.CheckedIndices)
                {
                    return _sources[index].ID;
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return currentID;
        }
        List<TXNode> _sources = new List<TXNode>();
        public SelectTXNodeModal()
        {
            InitializeComponent();
        }

        public void InitializeNodeList(List<TXNode> nodes, uint currentSourceID)
        {
            try
            {
                _sources = nodes;
                this.checkedListBoxControlNodeList.Items.Clear();
                for(int i = 0; i < nodes.Count; i++)
                {
                    TXNode s = nodes[i];
                    this.checkedListBoxControlNodeList.Items.Add(s.Name);
                    if (currentSourceID == s.ID)
                    {
                        this.checkedListBoxControlNodeList.SetItemCheckState(i, CheckState.Checked);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.checkedListBoxControlNodeList.CheckedItems.Count > 0)
                {
                    this.DialogResult = DialogResult.OK;

                }
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }
    }
}