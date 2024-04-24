using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Pinokio.UI.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simulation.Engine;
using Pinokio.Model.Base;
using Pinokio.Model.User;

namespace Pinokio.Layout
{
    public partial class MainFrame : XtraForm
    {

        public MainFrame()
        {
            SplashScreenManager.ShowForm(typeof(WaitFormSplash));

            new SimEngine();
            new ModelManager();
            MES mes = new MES();
            MCS mcs = new MCS();
            new FactoryManager(mes, mcs);

            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;


            SplashScreenManager.CloseForm();
        }        

        public void VisibleToolbar(bool bVisible)
        {
            this._3DModelDesigner1.VisibleToolbar(bVisible);
        }
        /// Launcher Load AccessDB ///
        private void MainFrame_Shown(object sender, EventArgs e)
        {

        }
        private void MainFrame_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FlyoutDialog.Show(this, new FOAExitApp()) == DialogResult.Yes)
            {
                e.Cancel = false;

                var monitoringProcess = Process.GetProcessesByName("Layout");

                if (monitoringProcess.Any())
                {
                    foreach (var p in monitoringProcess)
                    { p.Kill(); }
                }
            }
            else
                e.Cancel = true;
        }
    }
}
