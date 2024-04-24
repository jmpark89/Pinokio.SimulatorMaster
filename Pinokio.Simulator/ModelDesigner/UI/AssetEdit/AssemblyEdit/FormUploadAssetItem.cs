using DevExpress.XtraEditors;
using Logger;
using Pinokio.Communication;
using Pinokio.Database;
using Pinokio.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinokio.Designer.UI.AssetEdit.AssemblyEdit
{
    public partial class FormUploadAssetItem : DevExpress.XtraEditors.XtraForm
    {


        public FormUploadAssetItem()
        {
            InitializeComponent();
        }



  
        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (System.Exception ex)
            {

                ErrorLogger.SaveLog(ex);
            }
        }

        private void simpleButtonUpload_Click(object sender, EventArgs e)
        {
            try
            {

                this.DialogResult = DialogResult.OK;
                this.Close();


            }
            catch (System.Exception ex)
            {

                ErrorLogger.SaveLog(ex);
            }
        }
    }
}