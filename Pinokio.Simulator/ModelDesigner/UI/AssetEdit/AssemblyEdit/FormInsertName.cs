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

namespace Pinokio.Designer
{

    public partial class FormInsertName : DevExpress.XtraEditors.XtraForm
    {
        public string NodeName = string.Empty;
        public FormInsertName()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            NodeName = this.textEditNodeName.Text;

            this.Close();
        }
    }
}