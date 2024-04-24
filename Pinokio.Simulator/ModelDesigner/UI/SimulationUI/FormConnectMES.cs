using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraEditors;
using Pinokio.Database;
using Pinokio.UI.Base;
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
    public partial class FormConnectMES : DevExpress.XtraEditors.XtraForm
    {
        private DBType _dbType;
        private List<string> _tableList;

        public DBType MES_DB_Type { get { return _dbType; } }
        public List<string> TableList { get { return _tableList; } }
        public FormConnectMES()
        {
            InitializeComponent();
            _tableList = new List<string>();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            // 비어 있는 정보 있는지 체크
            if (IsNullorEmptyInputData())
            {
                peCheck.Visible = false;
                peError.Visible = true;
                FlyoutDialog.Show(this, new FOAInputError());
            }
            else
            {
                _tableList.Clear();
                // 나중에 MySQL 또는 Oracle과 같은 DB Type을 선택하는 UI구현 필요
                ConnectionDB(DBType.MYSQL);
                _tableList = teTableName.Text.Split(',').ToList();
            }
        }

        private void ConnectionDB(DBType dbType)
        {
            _dbType = dbType;

            if (_dbType == DBType.MYSQL)
            {
                MySQLDBOption.SetOption(teIPAddress.Text, teDBName.Text, tePort.Text, teUserName.Text, tePassword.Text);
                if (DBUtils.CanConnect(_dbType))
                {
                    peCheck.Visible = true;
                    peError.Visible = false;
                    welcomeWizardPage1.AllowNext = true;

                    MessageBox.Show(this, "A successful MySQL connection was made with the parameters defined for this connection.",
                        "Successfully made the MySQL Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    peCheck.Visible = false;
                    peError.Visible = true;
                    welcomeWizardPage1.AllowNext = false;

                    MessageBox.Show(this, "Access denied for user '" + teUserName.Text + "'@'" + teIPAddress.Text + "' (using password: YES)",
                        "Failed to Connect to MySQL at " + teIPAddress.Text + ":" + tePort.Text + " with user " + teUserName.Text
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool IsNullorEmptyInputData()
        {
            return string.IsNullOrEmpty(teIPAddress.Text) || string.IsNullOrEmpty(tePort.Text) || string.IsNullOrEmpty(teDBName.Text) ||
                    string.IsNullOrEmpty(teTableName.Text) || string.IsNullOrEmpty(teUserName.Text) || string.IsNullOrEmpty(tePassword.Text);
        }
    }
}