using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using devDept.Geometry;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot;
using devDept.Graphics;
using devDept.Eyeshot.Translators;
using DevExpress.Utils.Extensions;
using System.IO;
using Pinokio.Database;
using Pinokio.Geometry;
using Pinokio.Object;
using Pinokio.Animation;
using System.Reflection;
using Pinokio.Model.Base;
using Logger;
using DevExpress.XtraSplashScreen;
using Pinokio.UI.Base;
using Simulation.Engine;
using DevExpress.XtraBars.Docking2010.Customization;

namespace Pinokio.Designer
{
    public partial class ModelDesigner : UserControl
    {
        private void LoadLayout()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Access File (*.accdb)|*.accdb|Xml File (*.xml)|*.xml";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SplashScreenManager.ShowForm(typeof(WaitFormSplash));
                string filePath = dlg.FileName;

                NewItem();
                List<SimNode> totalNodes = new List<SimNode>();
                List<FloorPlan> floors;
                bool isSuccess = pinokio3DModel1.LoadDB(filePath, out totalNodes, out floors);

                
                beiSimStartTimeSetting.EditValue = SimEngine.Instance.StartDateTime;
                beiSimEndTimeSetting.EditValue = SimEngine.Instance.EndDateTime;

                SplashScreenManager.CloseForm();

                if (isSuccess)
                {
                    _floorForm.ResetFloorPlan(floors);
                    InitializeSimulationUpdateNode();

                    InitializeFloorPlan(floors);

                    ModifyNodeTreeList(totalNodes);

                    SimNodeTreeListNode treeNode = simNodeTreeList.FindNodeByKeyID(pinokio3DModel1.SelectedFloorID) as SimNodeTreeListNode;
                    propertyGridControlSimObject.SelectedObject = treeNode.SimNode;

                    ModelManager.Instance.SimResultDBManager.ModelName = Path.GetFileNameWithoutExtension(filePath);
                    pinokio3DModel1.InitialFilePath = filePath;
                    //MessageBox.Show("Success");
                    FlyoutDialog.Show(Application.OpenForms[0], new FOAInfo("Success"));
                    
                    bbiLoadAMHSReport.Enabled = true;
                    bbiLoadProductionReport.Enabled = true;
                    bbiAMHSReport.Enabled = false;
                    bbiProductionReport.Enabled = false;

                    dicVisibleNodeTypeInfo.Clear();
                    dicSimNodeType.Clear();

                    FinishAddNode();
                    RefreshEntities();
                    CloseObjectManipulator();
                }
                else
                {
                    //MessageBox.Show("Failed");

                    MES mes = FactoryManager.Instance.MES;
                    MCS mcs = FactoryManager.Instance.MCS;
                    NewItem();
                    ModelManager.Instance.InitializeMESnMCS(mes, mcs);
                    ModifyNodeTreeList(ModelManager.Instance.SimNodes.Values.ToList());
                    _isInitialize = true;
                    FloorSetup();
                    _isInitialize = false;
                    FlyoutDialog.Show(Application.OpenForms[0], new FOAInfo("Failed."));
                }
            }
        }
    }
}

