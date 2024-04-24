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
   
    public partial class SelectProductModal : DevExpress.XtraEditors.XtraForm
    {
        uint _selectedID;

        public uint SelectedID { get => _selectedID; set => _selectedID = value; }

        public SelectProductModal()
        {
            InitializeComponent();
        }



        public void InitializeProductList(List<ProductData> productionScheduleDatas, uint selectedID)
        {
            try
            {
                clbProductLst.DataSource = productionScheduleDatas;

                int index = productionScheduleDatas.FindIndex(x => x.ProductID == selectedID);

                if (index != -1)
                    clbProductLst.SetItemChecked(index, true);

                clbProductLst.Refresh();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }


        }

        private void clbProductLst_CheckMemberChanged(object sender, EventArgs e)
        {

            try
            {
                foreach (ProductData item in clbProductLst.CheckedItems)
                {
                    SelectedID = item.ProductID;
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void sbOK_Click(object sender, EventArgs e)
        {
            try

            {
                if(clbProductLst.CheckedItems.Count > 0)
                {
                    //clbProductLst.CheckedItems[0]
                    SelectedID = ((ProductData)clbProductLst.CheckedItems[0]).ProductID;
                    this.DialogResult = DialogResult.OK;
                }
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
    }
}