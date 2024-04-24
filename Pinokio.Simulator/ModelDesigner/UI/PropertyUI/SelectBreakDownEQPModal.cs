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
    public partial class SelectBreakDownEQPModal : DevExpress.XtraEditors.XtraForm
    {
        public SelectBreakDownEQPModal()
        {
            InitializeComponent();
        }
        public string GetCheckName(string currentName)
        {

            try
            {
                foreach (int index in this.checkedListBoxControlNodeList.CheckedIndices)
                {
                    return _refEntityTypes[index].Name.Remove(0, 3);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return currentName;
        }
        List<Type> _refEntityTypes = new List<Type>();

        public void InitializeRefNameList(List<Type> refEntityTypes, string currentEntityName)
        {
            try
            {
                _refEntityTypes = refEntityTypes;
                this.checkedListBoxControlNodeList.Items.Clear();
                for (int i = 0; i < refEntityTypes.Count; i++)
                {
                    Type s = refEntityTypes[i];
                    string displayedRefName = s.Name.Remove(0, 3);
                    this.checkedListBoxControlNodeList.Items.Add(displayedRefName);
                    if (currentEntityName == displayedRefName)
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