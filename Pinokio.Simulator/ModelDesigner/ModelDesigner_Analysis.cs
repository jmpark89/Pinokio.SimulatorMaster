using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Pinokio.Model.Base;
using Logger;
using Pinokio.UI.Base;
using DevExpress.XtraBars.Docking2010.Customization;

namespace Pinokio.Designer
{
    public partial class ModelDesigner : UserControl
    {
        private void bbiProductionReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ProductionReportForm reportForm = new ProductionReportForm(false, this.pinokio3DModel1, this);
                reportForm.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        private void bbiAMHSReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            try
            {
                AMHSReportForm reportForm = new AMHSReportForm(false, this.pinokio3DModel1, this);
                reportForm.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

        }
        private void bbiLoadProductionReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDlg = new OpenFileDialog();
                openFileDlg.Filter = "Access File (*.accdb)|*.accdb|All files (*.*)|*.*";
                openFileDlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "\\Simulation Results";

                if (openFileDlg.ShowDialog() == DialogResult.OK)
                {
                    if (!openFileDlg.FileName.Contains(ModelManager.Instance.SimResultDBManager.ModelName))
                    {
                        FlyoutDialog.Show(Application.OpenForms[0], new FOAInfo("Failed. \nPlease open the model file with the same name"));
                        return;
                    }

                    ModelManager.Instance.SimResultDBManager.LoadDBPath = openFileDlg.FileName;

                    ProductionReportForm reportForm = new ProductionReportForm(true, this.pinokio3DModel1, this);
                    reportForm.Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void bbiLoadAMHSReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDlg = new OpenFileDialog();
                openFileDlg.Filter = "Access File (*.accdb)|*.accdb|All files (*.*)|*.*";
                openFileDlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "\\Simulation Results";

                if (openFileDlg.ShowDialog() == DialogResult.OK)
                {
                    if (!openFileDlg.FileName.Contains(ModelManager.Instance.SimResultDBManager.ModelName))
                    {
                        FlyoutDialog.Show(Application.OpenForms[0], new FOAInfo("Failed. \nPlease open the model file with the same name"));
                        return;
                    }
                    ModelManager.Instance.SimResultDBManager.LoadDBPath = openFileDlg.FileName;
                    AMHSReportForm reportForm = new AMHSReportForm(true, this.pinokio3DModel1, this);
                    reportForm.Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}

