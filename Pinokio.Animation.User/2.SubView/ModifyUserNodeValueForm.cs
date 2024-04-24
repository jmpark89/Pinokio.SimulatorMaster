using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
namespace Pinokio.Animation.User
{
    public partial class ModifyUserNodeValueForm : DevExpress.XtraEditors.XtraForm
    {
        private DataTable _table = new DataTable();
        public ModifyUserNodeValueForm()
        {
            InitializeComponent();
            _table.Columns.Add("Veriable");
            _table.Columns.Add("Value");
            _table.Columns[0].ReadOnly = true;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        public void SetUserData(string Name, object data)
        {
            _table.Rows.Add(Name, data);


            gridControl1.DataSource = _table;
        }
        public void SetUserDataList(Dictionary<string, object> datas)
        {



            foreach (KeyValuePair<string, object> dataOBJ in datas)
            {

                _table.Rows.Add(dataOBJ.Key, dataOBJ.Value);
            }

            gridControl1.DataSource = _table;



        }
    }
}