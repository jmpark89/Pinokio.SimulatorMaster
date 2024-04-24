using DevExpress.XtraEditors;
using Logger;
using Simulation.Engine;
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
    public partial class DlgReservationPause : DevExpress.XtraEditors.XtraForm
    {
        public DateTime PauseTime = DateTime.Now;
        public DlgReservationPause()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                double addedTime = (timeEdit1.Time - SimEngine.Instance.StartDateTime).TotalSeconds;
                double timeNow = SimEngine.Instance.TimeNow.TotalSeconds;
                if (addedTime <= timeNow)
                {
                    MessageBox.Show("설정한 시간은 현재 시간 보다 늦어야 합니다.");
                    return;
                }
                DialogResult = DialogResult.OK;
                PauseTime = timeEdit1.Time;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}