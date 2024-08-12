using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using devDept.Eyeshot;
using DevExpress.XtraBars.Docking;
using Logger;
using Pinokio.Animation;
using Pinokio.Model.Base;
using Simulation.Engine;
using System.Diagnostics;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.Data;
using Pinokio.Simulation;
using devDept.Eyeshot.Entities;
using devDept.Geometry;

namespace Pinokio.Designer
{
    public partial class ModelDesigner : UserControl
    {
        [NonSerialized]
        public static List<string> vehicleCommandTreeNodeNames = new List<string> { "Idle", "Move To Load", "Loading", "Move To Unload", "Unloading", "Stage" };
        [NonSerialized] 
        public static List<string> commandTreeNodeNames = new List<string> { "Queued Commands", "Waiting Commands", "Loading Commands", "Transferring Commands", "Unloading Commands" };
        [NonSerialized] 
        public static List<string> eqpStepTreeNodeNames = new List<string> { "Dispatching EqpSteps", "Waiting EqpSteps", "Processing EqpSteps" };
        [NonSerialized] 
        public static List<string> partStepTreeNodeNames = new List<string> { "WIP PartSteps", "Assigned PartSteps", "Track-In PartSteps", "Processing PartSteps", "Step-End PartSteps" };
        [NonSerialized] 
        TreeListNode selectedNode = new TreeListNode();
        [NonSerialized] 
        private List<string> _csNames = new List<string>();
        [NonSerialized]
        System.Timers.Timer Treetimer = new System.Timers.Timer();
        public double AccelRate { get { return Convert.ToDouble(beSimulationAcceleration.EditValue); } }
        public string SimTime { get { return beiCurrentSimTimeView.EditValue == null ? "0" : beiCurrentSimTimeView.EditValue.ToString(); } }
        [NonSerialized]
        System.Timers.Timer lineStatusTimer = new System.Timers.Timer();

        [NonSerialized] 
        public Dictionary<string, Tuple<NodeReference, Color>> changedNodeRef = new Dictionary<string, Tuple<NodeReference, Color>>();

        private bool IsExistUpdateNode(UpdateNode node)
        {
            try
            {
                if (node == null || !(ModelManager.Instance.SimNodes.ContainsKey(node.ID) && ModelManager.Instance.SimNodes[node.ID] == node))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return false;
        }
        private bool IsExistSettingNode(SettingNode node)
        {
            try
            {
                if (node == null || !(ModelManager.Instance.SimNodes.ContainsKey(node.ID) && ModelManager.Instance.SimNodes[node.ID] == node))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return false;
        }
        private void InitializeSimulationUpdateNode()
        {
            try
            {
                if (!IsExistUpdateNode(ModelManager.Instance.AnimationNode))
                {
                    ModelManager.Instance.AddAnimationNode(0.1, true);
                    ModelManager.Instance.AnimationNode.AnimationEvent += this.AnimationEvent;
                }

                if (!IsExistUpdateNode(ModelManager.Instance.SetAccelerationTimeNode))
                {
                    ModelManager.Instance.AddSetAccelerationTimeNode(0.1, true);
                }

                if (!IsExistUpdateNode(ModelManager.Instance.MeasureAccelerationTimeNode))
                {
                    ModelManager.Instance.AddAccelerationTimeNode(10, true);
                }

                if (!IsExistSettingNode(ModelManager.Instance.EngineStopNode))
                {
                    ModelManager.Instance.EngineStopNode = ModelManager.Instance.AddEngineStopNode(true);
                    ModelManager.Instance.EngineStopNode.EvtCalendar = SimEngine.Instance.EventCalender;
                }

                if (!IsExistSettingNode(ModelManager.Instance.EnginePauseNode))
                {
                    ModelManager.Instance.EnginePauseNode = ModelManager.Instance.AddEnginePauseNode(true);
                    ModelManager.Instance.EnginePauseNode.EvtCalendar = SimEngine.Instance.EventCalender;

                    if (!ModelManager.Instance.EnginePauseNode.CreatedPauseSetting)
                    {
                        ModelManager.Instance.EnginePauseNode.CreatedPauseSetting = true;
                        ModelManager.Instance.EnginePauseNode.PauseSetting += new EventHandler(SettingPause);
                    }
                }

                if (!IsExistSettingNode(ModelManager.Instance.EngineWarmUpNode))
                {
                    ModelManager.Instance.EngineWarmUpNode = ModelManager.Instance.AddEngineWarmUpNode(true);
                    ModelManager.Instance.EngineWarmUpNode.EvtCalendar = SimEngine.Instance.EventCalender;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void AnimationEvent(object sender, EventArgs e)
        {
            //AnimationNode animationNode = (AnimationNode)sender;
            try
            {
                UpdateAnimation();
//                Task _t = new Task(() => UpdateAnimation());
//                _t.Start();
//                _t.Wait();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

        static int propertyGridAnimationUpdateCount = 0;
        private void UpdateAnimation()
        {
            try
            {
                if (propertyGridControlSimObject.SelectedObject != null)
                {
                    int objRowIdx = propertyGridControlSimObject.TopVisibleRowIndexPixel;
                    propertyGridControlSimObject.Invoke(new Action(() => propertyGridControlSimObject.RefreshAllProperties()));
                    propertyGridControlSimObject.Invoke(new Action(() => propertyGridControlSimObject.TopVisibleRowIndexPixel = objRowIdx));
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void UpdateSimTime()
        {
            try
            {
                beiCurrentSimTimeView.BeginUpdate();
                TimeSpan timeSpan = TimeSpan.FromSeconds(SimEngine.Instance.TimeNow.TotalSeconds);
                beiCurrentSimTimeView.EditValue = timeSpan.ToString("dd\\/hh\\:mm\\:ss\\.fff");
                beiCurrentSimTimeView.EndUpdate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void UpdateAccelerationRate()
        {
            try
            {
                beSimulationAcceleration.BeginUpdate();
                beSimulationAcceleration.EditValue = ModelManager.Instance.AccelerationRate;
                beSimulationAcceleration.EndUpdate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void propertyGridUpdateEvent(object sender, EventArgs e)
        {
            if (propertyGridControlSimObject.SelectedObject != null)
                propertyGridControlSimObject.Refresh();
        }



        private void Animation_Tog_Switch_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                AnimationOnOff();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void AnimationOnOff()
        {
            if (Animation_Tog_Switch.Checked) //켰을때
            {
                if (SimEngine.Instance.EngineState != ENGINE_STATE.STOP)
                {
                    ModelManager.Instance.UpdateAnimationPose();
                    ModelManager.Instance.SetAccelerationTimeNode.Queue = 0;
                    ModelManager.Instance.SetAccelerationTimeNode.stopwatch = new Stopwatch();
                }

                this.pinokio3DModel1.StartAnimation(100);

                if (ModelManager.Instance.AnimationNode != null)
                {
                    ModelManager.Instance.AnimationNode.OnEvent(SimEngine.Instance.TimeNow);
                }

                if (ModelManager.Instance.SetAccelerationTimeNode != null)
                {
                    ModelManager.Instance.SetAccelerationTimeNode.OnEvent(SimEngine.Instance.TimeNow);
                }
            }
            else
            {
                this.pinokio3DModel1.StopAnimation();

                if (ModelManager.Instance.AnimationNode != null)
                {
                    ModelManager.Instance.AnimationNode.OffEvent();
                }

                if (ModelManager.Instance.SetAccelerationTimeNode != null)
                {
                    ModelManager.Instance.SetAccelerationTimeNode.OffEvent();
                }
            }
        }

        private void AnimationSpeedTrack_EditValueChanged(object sender, EventArgs e)//애니메이션 배속 변경 track bar
        {
            try
            {
                beiAnimationSpeed.EditValue = AnimationSpeedTrack.EditValue.ToString();

                if (Animation_Tog_Switch.Checked && ModelManager.Instance.AnimationNode != null && ModelManager.Instance.AnimationNode.IsUse && SimEngine.Instance.EngineState != ENGINE_STATE.STOP) //애니메이션 켰을 때 적용 && 엔진 진행중일 때만
                {
                    ModelManager.Instance.AnimationSpeedRate = double.Parse(AnimationSpeedTrack.EditValue.ToString());
                    ModelManager.Instance.SetAccelerationTimeNode.ChangeRate = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void beiAnimationSpeed_EditValueChanged(object sender, EventArgs e) // 애니메이션 배속 값
        {
        }

        private void BeiSimEndTimeSetting_EditValueChanged(object sender, System.EventArgs e)
        {
            SimEngine.Instance.EndDateTime = (DateTime)beiSimEndTimeSetting.EditValue;
        }

        private void BeiSimStartTimeSetting_EditValueChanged(object sender, System.EventArgs e)
        {
            SimEngine.Instance.StartDateTime = (DateTime)beiSimStartTimeSetting.EditValue;
        }

        private void BliWarmUpPeriod_CloseUp(object sender, EventArgs e)
        {
            BliWarmUpPeriod.BeginUpdate();
            BeiDays.EditValue = string.IsNullOrEmpty(BeiDays.EditValue.ToString()) ? 0 : Convert.ToInt32(BeiDays.EditValue);
            BeiHours.EditValue = string.IsNullOrEmpty(BeiHours.EditValue.ToString()) ? 0 : Convert.ToInt32(BeiHours.EditValue);
            BeiMinutes.EditValue = string.IsNullOrEmpty(BeiMinutes.EditValue.ToString()) ? 0 : Convert.ToInt32(BeiMinutes.EditValue);
            BliWarmUpPeriod.Caption = "Warn Up Period: " + BeiDays.EditValue + "(D) / " + BeiHours.EditValue + "(H) : " + BeiMinutes.EditValue + "(M)";
            BliWarmUpPeriod.EndUpdate();
        }

        private void Background_Color_Tog_Switch_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (_floorForm.LstFloorPlan.Count > 0)
                {
                    //TODO devDept: Now you need to pass through the Model.ActiveViewport property to get/set the active viewport grid.
                    //TODO devDept: Now you need to pass through the Model.ActiveViewport property to get/set the active viewport grids.
                    //pinokio3DModel1.Grids = grids.ToArray();
                    //pinokio3DModel1.ActiveViewport.Grids = grids.ToArray();
                    pinokio3DModel1.LightBackGround = Background_Color_Tog_Switch.Checked;
                    List<Grid> grids = new List<Grid>();
                    foreach (FloorPlan plan in _floorForm.LstFloorPlan)
                    {
                        pinokio3DModel1.AddFloor(plan, ref grids);
                    }

                    pinokio3DModel1.Grids = grids.ToArray();

                    for (int i = 0; i < simNodeTreeList.Nodes.Count; i++)
                    {
                        SimNodeTreeListNode treeNode = simNodeTreeList.Nodes[i] as SimNodeTreeListNode;
                        if (treeNode.SimNode != null)
                        {
                            if (isRootTreeNode(treeNode.SimNode))
                            {
                                pinokio3DModel1.SelectedFloorID = treeNode.SimNode.ID;

                                for (int j = 0; j < pinokio3DModel1.Layers.Count; j++)
                                {
                                    if (pinokio3DModel1.Layers[j].Name == treeNode.SimNode.ID.ToString())
                                    {
                                        pinokio3DModel1.Grids[j].Visible = treeNode.Checked;
                                        pinokio3DModel1.Layers[j].Visible = treeNode.Checked;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    pinokio3DModel1.Invalidate();
                    pinokio3DModel1.Entities.Regen();
                    FloorPlanUpdate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void bbiSimRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChangeButtonEnabledForSimulation();
            startSimulation();
        }

        private void ChangeButtonEnabledForSimulation()
        {
            RB_BTN_NEW.Enabled = false;
            RB_BTN_LOAD.Enabled = false;
            RB_BTN_SAVE.Enabled = false;
            RB_BTN_FLOORSETUP.Enabled = false;
            barButtonItemImPortModel.Enabled = false;
            bbiAMHSReport.Enabled = true;
            bbiProductionReport.Enabled = true;
        }

        private void changeVisibleNeedlessNodes(bool isVisible)
        {
            dicSimNodeType = new Dictionary<string, List<SimNodeTreeListNode>>();

            TraverseTreeListNodes(null);

            foreach (string nodeType in dicSimNodeType.Keys)
            {
                if (nodeType.Contains("Point") || nodeType.Contains("Link"))
                {
                    List<SimNodeTreeListNode> nodeList = dicSimNodeType[nodeType];
                    foreach (SimNodeTreeListNode node in nodeList)
                    {
                        ChildCheckIfParentCheckforVisibility(node, isVisible);
                        node.Checked = isVisible;
                    }
                    if (dicVisibleNodeTypeInfo.Count != 0)
                    {
                        bool isTextVisible = dicVisibleNodeTypeInfo[nodeType].Item2;
                        dicVisibleNodeTypeInfo[nodeType] = new Tuple<bool, bool>(isVisible, isTextVisible);
                    }
                }
            }
        }

        public void startSimulation()
        {
            try
            {
                _modelActionType = ModelActionType.None;
                pinokio3DModel1.ObjectManipulator.Cancel();
                ClearSelection();

                if (ModelManager.Instance.EnginePauseNode != null)
                    ModelManager.Instance.EnginePauseNode.IsPause = false;

                RemoveEventBeforeSimulation();
                if(SimEngine.Instance.EngineState is ENGINE_STATE.STOP)
                    InitializePreviousEntities();

                changeVisibleNeedlessNodes(false);

                DoChangeVisibleOfBBI(bbiSimRun, DevExpress.XtraBars.BarItemVisibility.Never);
                DoChangeVisibleOfBBI(bbiSimPause, DevExpress.XtraBars.BarItemVisibility.Always);
                DoChangeVisibleOfBBI(bbiSimStop, DevExpress.XtraBars.BarItemVisibility.Always);
                DoChangeVisibleOfBBI(bbiDockPart, DevExpress.XtraBars.BarItemVisibility.Always);
                DoChangeVisibleOfBBI(bbiDockLineStatus, DevExpress.XtraBars.BarItemVisibility.Always);
                DoChangeVisibleOfBBI(bbiDockLineStatusDetail, DevExpress.XtraBars.BarItemVisibility.Always);
                bbiDockInsertNode.Enabled = false;
                bbiDockInsertCoupledModel.Enabled = false;
                this.dockPanelSimNodeProperties.Visibility = DockVisibility.Visible;
                this.dockPanelSimNodeProperties.Show();

                this.dockPanelInsertRefNode.Hide();
                this.dockPanelInsertCoupledModel.Hide();
                //this.dockPanelInsertNodeTree.Hide();
                this.dockPanelInsertedSimNodes.Visibility = DockVisibility.Visible;
                this.dockPanelInsertedSimNodes.Dock = DockingStyle.Right;
                this.dockPanelParts.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                this.dockPanelParts.DockTo(dockPanelInsertedSimNodes);
                simpleButtonRemoveTreeNode.Enabled = false;
                            
                layoutControlItemRemoveSimNode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.simNodeTreeList.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.None;

                AddTreeListLineStatus();

                this.dockPanelLineStatus.Visibility = DockVisibility.Visible;
                this.dockPanelLineStatusDetail.Visibility = DockVisibility.Visible;

                lineStatusTimer = new System.Timers.Timer();
                lineStatusTimer.Interval = 1000;
                lineStatusTimer.Elapsed += new System.Timers.ElapsedEventHandler(LineStatusTimer_Elapsed);
                lineStatusTimer.Start();

                this.pinokio3DModel1.Invalidate();

                InitializeSimulationUpdateNode();

                var startTime = (DateTime)beiSimStartTimeSetting.EditValue;
                var endTime = (DateTime)beiSimEndTimeSetting.EditValue;

                AnimationOnOff();

                double warmUpPeriod = Convert.ToDouble(BeiDays.EditValue) * 24 * 60 + Convert.ToDouble(BeiHours.EditValue) * 60 + Convert.ToDouble(BeiMinutes.EditValue);
                Simulator.Instance.Run(startTime, endTime, warmUpPeriod * 60);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void startSnapShotSimulation()
        {
            try
            {
                _modelActionType = ModelActionType.None;
                pinokio3DModel1.ObjectManipulator.Cancel();
                ClearSelection();

                if (ModelManager.Instance.EnginePauseNode != null)
                    ModelManager.Instance.EnginePauseNode.IsPause = false;

                changeVisibleNeedlessNodes(false);

                DoChangeVisibleOfBBI(bbiSimRun, DevExpress.XtraBars.BarItemVisibility.Never);
                DoChangeVisibleOfBBI(bbiSimPause, DevExpress.XtraBars.BarItemVisibility.Always);
                DoChangeVisibleOfBBI(bbiSimStop, DevExpress.XtraBars.BarItemVisibility.Always);
                DoChangeVisibleOfBBI(bbiDockPart, DevExpress.XtraBars.BarItemVisibility.Always);
                DoChangeVisibleOfBBI(bbiDockLineStatus, DevExpress.XtraBars.BarItemVisibility.Always);
                DoChangeVisibleOfBBI(bbiDockLineStatusDetail, DevExpress.XtraBars.BarItemVisibility.Always);
                bbiDockInsertNode.Enabled = false;
                bbiDockInsertCoupledModel.Enabled = false;


                simpleButtonRemoveTreeNode.Enabled = false;

                layoutControlItemRemoveSimNode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.simNodeTreeList.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.None;

                AddTreeListLineStatus();

                this.dockPanelLineStatus.Visibility = DockVisibility.Visible;
                this.dockPanelLineStatusDetail.Visibility = DockVisibility.Visible;

                lineStatusTimer = new System.Timers.Timer();
                lineStatusTimer.Interval = 1000;
                lineStatusTimer.Elapsed += new System.Timers.ElapsedEventHandler(LineStatusTimer_Elapsed);
                lineStatusTimer.Start();

                this.pinokio3DModel1.Invalidate();

                var startTime = (DateTime)beiSimStartTimeSetting.EditValue;
                var endTime = (DateTime)beiSimEndTimeSetting.EditValue;

                AnimationOnOff();

                double warmUpPeriod = Convert.ToDouble(BeiDays.EditValue) * 24 * 60 + Convert.ToDouble(BeiHours.EditValue) * 60 + Convert.ToDouble(BeiMinutes.EditValue);
                Simulator.Instance.RunSnapShot(warmUpPeriod * 60);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }


        private void SimulationEnd()
        {
            DoChangeVisibleOfBBI(bbiSimRun, DevExpress.XtraBars.BarItemVisibility.Never);
            DoChangeVisibleOfBBI(bbiSimPause, DevExpress.XtraBars.BarItemVisibility.Never);
        }

        private void FaliSimulation()
        {
            RB_BTN_NEW.Enabled = true;
            RB_BTN_LOAD.Enabled = true;
            RB_BTN_SAVE.Enabled = true;
            RB_BTN_FLOORSETUP.Enabled = true;
            barButtonItemImPortModel.Enabled = false;

            changeVisibleNeedlessNodes(true);

            AddEventAfterSimulation();
            DoChangeVisibleOfBBI(bbiSimRun, DevExpress.XtraBars.BarItemVisibility.Always);
            DoChangeVisibleOfBBI(bbiSimPause, DevExpress.XtraBars.BarItemVisibility.Never);
            DoChangeVisibleOfBBI(bbiSimStop, DevExpress.XtraBars.BarItemVisibility.Never);
            DoChangeVisibleOfBBI(bbiDockPart, DevExpress.XtraBars.BarItemVisibility.Never);
            DoChangeVisibleOfBBI(bbiDockLineStatus, DevExpress.XtraBars.BarItemVisibility.Never);
            DoChangeVisibleOfBBI(bbiDockLineStatusDetail, DevExpress.XtraBars.BarItemVisibility.Never);
            bbiDockInsertNode.Enabled = true;
            bbiDockInsertCoupledModel.Enabled = true;
            lineStatusTimer.Stop();
            this.dockPanelInsertedSimNodes.Dock = DockingStyle.Left;
            this.dockPanelParts.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            simpleButtonRemoveTreeNode.Enabled = true;
            layoutControlItemRemoveSimNode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            this.simNodeTreeList.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
            this.dockPanelLineStatus.Visibility = DockVisibility.Hidden;
            this.dockPanelLineStatusDetail.Visibility = DockVisibility.Hidden;
            this.dockPanelInsertCoupledModel.Visibility = DockVisibility.Visible;
            this.dockPanelInsertRefNode.Visibility = DockVisibility.Visible;
            this.dockPanelInsertCoupledModel.DockAsTab(this.dockPanelInsertRefNode);
            this.dockPanelInsertedSimNodes.DockAsTab(this.dockPanelInsertRefNode);
            this.dockPanelInsertRefNode.Show();
            this.dockPanelSimNodeProperties.Show();
            this.pinokio3DModel1.StopAnimation();
            SimEngine.Instance.EngineState = ENGINE_STATE.STOP;

            InitializePreviousEntities();
            InitializePreviousSimNodes();

            if (backUpInitDatas != null) PasteSimulationInitializeState();
            this.pinokio3DModel1.Entities.Regen();
            this.pinokio3DModel1.Invalidate();
        }

        private void SettingPause(object sender, EventArgs e)
        {
            SimEngine.Instance.EngineState = ENGINE_STATE.PAUSE;
            ModelManager.Instance.EnginePauseNode.IsPause = true;
            DoChangeVisibleOfBBI(bbiSimRun, DevExpress.XtraBars.BarItemVisibility.Always);
            DoChangeVisibleOfBBI(bbiSimPause, DevExpress.XtraBars.BarItemVisibility.Never);
        }

        private void bbiSimPause_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PauseSimulation();
        }

        public void PauseSimulation()
        {
            if (SimEngine.Instance.EventCalender.DicEvt.Count != 0)
            {
                SimEngine.Instance.EngineState = ENGINE_STATE.PAUSE;
                ModelManager.Instance.EnginePauseNode.IsPause = true;
                SimPort port = new SimPort(INT_PORT.SETTING_ASSIGN);
                SimEngine.Instance.EventCalender.AddEvent(SimEngine.Instance.TimeNow, ModelManager.Instance.EnginePauseNode, port);
                DoChangeVisibleOfBBI(bbiSimRun, DevExpress.XtraBars.BarItemVisibility.Always);
                DoChangeVisibleOfBBI(bbiSimPause, DevExpress.XtraBars.BarItemVisibility.Never);
            }
        }
        private async Task PauseSimulationAsync()
        {
            if (SimEngine.Instance.EventCalender.DicEvt.Count != 0 && ModelManager.Instance.EnginePauseNode.IsPause == false)
            {
                ModelManager.Instance.EnginePauseNode.IsPause = true;
                SimPort port = new SimPort(INT_PORT.SETTING_ASSIGN);
                SimEngine.Instance.EventCalender.AddEvent(SimEngine.Instance.TimeNow, ModelManager.Instance.EnginePauseNode, port);

                // 100ms 대기
                await Task.Delay(100);
            }
        }
        private async void bbiSimStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await PauseSimulationAsync();

            RB_BTN_NEW.Enabled = true;
            RB_BTN_LOAD.Enabled = true;
            RB_BTN_SAVE.Enabled = true;
            RB_BTN_FLOORSETUP.Enabled = true;
            barButtonItemImPortModel.Enabled = false;

            changeVisibleNeedlessNodes(true);

            AddEventAfterSimulation();
            DoChangeVisibleOfBBI(bbiSimRun, DevExpress.XtraBars.BarItemVisibility.Always);
            DoChangeVisibleOfBBI(bbiSimPause, DevExpress.XtraBars.BarItemVisibility.Never);
            DoChangeVisibleOfBBI(bbiSimStop, DevExpress.XtraBars.BarItemVisibility.Never);
            DoChangeVisibleOfBBI(bbiDockPart, DevExpress.XtraBars.BarItemVisibility.Never);
            DoChangeVisibleOfBBI(bbiDockLineStatus, DevExpress.XtraBars.BarItemVisibility.Never);
            DoChangeVisibleOfBBI(bbiDockLineStatusDetail, DevExpress.XtraBars.BarItemVisibility.Never);
            bbiDockInsertNode.Enabled = true;
            bbiDockInsertCoupledModel.Enabled = true;
            lineStatusTimer.Stop();
            this.dockPanelInsertedSimNodes.Dock = DockingStyle.Left;
            this.dockPanelParts.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            simpleButtonRemoveTreeNode.Enabled = true;
            layoutControlItemRemoveSimNode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            this.simNodeTreeList.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
            this.dockPanelLineStatus.Visibility = DockVisibility.Hidden;
            this.dockPanelLineStatusDetail.Visibility = DockVisibility.Hidden;
            this.dockPanelInsertCoupledModel.Visibility = DockVisibility.Visible;
            //this.dockPanelInsertNodeTree.Visibility = DockVisibility.Visible;
            this.dockPanelInsertRefNode.Visibility = DockVisibility.Visible;
            this.dockPanelInsertCoupledModel.DockAsTab(this.dockPanelInsertRefNode);
            this.dockPanelInsertedSimNodes.DockAsTab(this.dockPanelInsertRefNode);
            //this.dockPanelInsertNodeTree.DockAsTab(this.dockPanelInsertRefNodeTree);
            this.dockPanelInsertRefNode.Show();
            this.dockPanelSimNodeProperties.Show();
            this.pinokio3DModel1.StopAnimation();
            SimEngine.Instance.EngineState = ENGINE_STATE.STOP;
            ModelManager.Instance.CurAblePartID = 1;
            this.beiCurrentSimTimeView.EditValue = "00/00:00:00.000";
            this.beSimulationAcceleration.EditValue = 0;
            InitWarmUpPeriod();
            InitializePreviousEntities();
            InitializePreviousSimNodes();
            InitializeBays();

            PasteSimulationInitializeState();

            this.pinokio3DModel1.Entities.Regen();
            this.pinokio3DModel1.Invalidate();
        }

        public void InitWarmUpPeriod()
        {
            repositoryItemComboBox4.Items.Clear();
            repositoryItemComboBox5.Items.Clear();
            repositoryItemComboBox6.Items.Clear();

            for (int i = 1; i < 74; i++)
            {
                repositoryItemComboBox4.Items.Add(i * 5);
                if (i < 13)
                {
                    repositoryItemComboBox5.Items.Add(i * 5);
                    repositoryItemComboBox6.Items.Add(i * 5);
                }
            }

            this.BeiDays.EditValue = 0;
            this.BeiHours.EditValue = 0;
            this.BeiMinutes.EditValue = 0;
            this.BliWarmUpPeriod.Caption = "Warm Up Period: 0(D) / 0(H) : 0(M)";
        }

        private void DoChangeVisibleOfBBI(DevExpress.XtraBars.BarButtonItem bbi, DevExpress.XtraBars.BarItemVisibility barItemVisibility)
        {
            try
            {
                bbi.Visibility = barItemVisibility;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        public void DoChangeEnabledOfBBI_DetailForm(bool isRun)
        {
            try
            {
                if (isRun)
                {
                    bbiSimRun.Enabled = true;
                    bbiSimStop.Enabled = true;
                }
                else
                {
                    bbiSimRun.Enabled = false;
                    bbiSimStop.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        private void bbiSimReservePause_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ModelManager.Instance.EnginePauseNode == null)
                return;

            DlgReservationPause dlgReservationPause = new DlgReservationPause();
            dlgReservationPause.StartPosition = FormStartPosition.CenterScreen;
            double timeNowSec = SimEngine.Instance.TimeNow.TotalSeconds;
            dlgReservationPause.timeEdit1.EditValue = SimEngine.Instance.StartDateTime.AddSeconds(timeNowSec);
            if (dlgReservationPause.ShowDialog() == DialogResult.OK)
            {
                double s = (dlgReservationPause.PauseTime - SimEngine.Instance.StartDateTime).TotalSeconds;
                if (SimEngine.Instance.EventCalender.DicEvt.Count == 0)
                {
                }
                else
                {
                    SimPort port = new SimPort(INT_PORT.SETTING_ASSIGN);
                    SimEngine.Instance.EventCalender.AddEvent(new Time(s), ModelManager.Instance.EnginePauseNode, port);
                }
            }
        }


        private void InitializeTreeListLineStatus()
        {
#if !DEBUG
            try
            {
#endif
            treeListLineStatus.BeginUpdate();

            treeListLineStatus.ClearNodes();            // Node Clear
            treeListLineStatus.Columns.Clear();         // Colunm Clear 

            treeListLineStatus.RowHeight = 22;
            treeListLineStatus.OptionsBehavior.Editable = false;   // 트리 노드의 텍스트 수정 안되게
            treeListLineStatus.OptionsView.ShowColumns = false;    // 컬럼 Header 안보이게
            treeListLineStatus.OptionsView.ShowIndicator = false;   // 트리 맨 앞에 있는 Indicator 안보이게

            TreeListColumn nameColumn = GetTreeListColumn("Contents", "Contents", UnboundColumnType.String, 200, HorzAlignment.Near, false, FormatType.None, null, 0);
            TreeListColumn countColumn = GetTreeListColumn("Count", "Count", UnboundColumnType.Integer, 100, HorzAlignment.Far, false, FormatType.Numeric, "#,##0", 0);

            treeListLineStatus.Columns.Add(nameColumn);
            treeListLineStatus.Columns.Add(countColumn);

            treeListLineStatus.EndUpdate();
#if !DEBUG
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
#endif
        }

        private TreeListColumn GetTreeListColumn(string caption, string fieldName, UnboundColumnType unboundColumnType, int width,
    HorzAlignment horzAlignment, bool allowEdit, FormatType formatType, string formatString, int visibleIndex) // cho 체크
        {
            TreeListColumn treeListColumn = new TreeListColumn();

            treeListColumn.Name = string.Empty;
            treeListColumn.Caption = caption;
            treeListColumn.FieldName = fieldName;
            treeListColumn.Width = width;
            treeListColumn.AppearanceHeader.Options.UseTextOptions = true;
            treeListColumn.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            treeListColumn.AppearanceCell.Options.UseTextOptions = true;
            treeListColumn.AppearanceCell.TextOptions.HAlignment = horzAlignment;
            treeListColumn.OptionsColumn.AllowEdit = allowEdit;
            treeListColumn.Format.FormatType = formatType;
            treeListColumn.Format.FormatString = formatString;
            treeListColumn.VisibleIndex = visibleIndex;
            treeListColumn.Visible = true;

            return treeListColumn;
        }
        delegate void TimerTreeCommand();

        private void RowSelectParentTotal(string selectData)
        {
            switch (selectData)
            {
                case "MRs":
                    List<MR> MRs = FactoryManager.Instance.MCS.DicMR.Values.OrderBy(x => x.Id).ToList();
                    UpdateListGridControlLineStatusDetail<MR>(MRs);
                    lbCount.Text = "MR " + MRs.Count.ToString() + " Counts";
                    break;

                case "Commands":
                    List<Command> totalCommands = new List<Command>();

                    totalCommands.AddRange(FactoryManager.Instance.MCS.QueuedCommandList);
                    totalCommands.AddRange(FactoryManager.Instance.MCS.WaitingCommandList);
                    totalCommands.AddRange(FactoryManager.Instance.MCS.LoadingCommandList);
                    totalCommands.AddRange(FactoryManager.Instance.MCS.TransferringCommandList);
                    totalCommands.AddRange(FactoryManager.Instance.MCS.UnloadingCommandList);

                    UpdateListGridControlLineStatusDetail<Command>(totalCommands.OrderBy(x => x.Id).ToList());
                    lbCount.Text = "Command " + totalCommands.Count.ToString() + " Counts";
                    break;
                case "PartSteps":
                    List<PartStep> partSteps = FactoryManager.Instance.MES.DicPartStep.Values.ToList();
                    UpdateListGridControlLineStatusDetail(partSteps);
                    lbCount.Text = "PartStep " + partSteps.Count.ToString() + " Counts";
                    break;
                case "EqpSteps":
                    List<EqpStep> eqpSteps = FactoryManager.Instance.MES.EqpSteps.ToList();
                    UpdateListGridControlLineStatusDetail(eqpSteps);
                    lbCount.Text = "EqpStep " + eqpSteps.Count.ToString() + " Counts";
                    break;
                case "Vehicles":
                    List<Vehicle.VehicleInfo> vehicles = GetVehicleList(true).Select(x => x._vehicleInfo).ToList();
                    UpdateListGridControlLineStatusDetail(vehicles);
                    lbCount.Text = "Vehicle " + vehicles.Count.ToString() + " Counts";
                    break;
            }
        }
        private void RowSelectTotal(string selectData)
        {
            gridControlLineStatusDetail.Visible = true;
            switch (selectData)
            {
                case "Queued Commands":
                    List<Command> queuedCommands = FactoryManager.Instance.MCS.QueuedCommandList.OrderBy(x => x.Id).ToList();
                    lbCount.Text = COMMAND_STATE.QUEUED + " " + queuedCommands.Count.ToString() + " Counts";
                    lock (queuedCommands)
                    {
                        UpdateListGridControlLineStatusDetail<Command>(queuedCommands);
                    }
                    break;
                case "Waiting Commands":
                    List<Command> waitingCommands = FactoryManager.Instance.MCS.WaitingCommandList.OrderBy(x => x.Id).ToList();
                    lbCount.Text = COMMAND_STATE.WAITING + " " + waitingCommands.Count.ToString() + " Counts";
                    lock (waitingCommands)
                    {
                        UpdateListGridControlLineStatusDetail<Command>(waitingCommands);
                    }
                    break;
                case "Loading Commands":
                    List<Command> loadingCommands = FactoryManager.Instance.MCS.LoadingCommandList.OrderBy(x => x.Id).ToList();
                    lbCount.Text = COMMAND_STATE.LOADING + " " + loadingCommands.Count.ToString() + " Counts";
                    lock (loadingCommands)
                    {
                        UpdateListGridControlLineStatusDetail<Command>(loadingCommands);
                    }
                    break;
                case "Transferring Commands":
                    List<Command> transferringCommands = FactoryManager.Instance.MCS.TransferringCommandList.OrderBy(x => x.Id).ToList();
                    lbCount.Text = COMMAND_STATE.TRANSFERRING + " " + transferringCommands.Count.ToString() + " Counts";
                    lock (transferringCommands)
                    {
                        UpdateListGridControlLineStatusDetail<Command>(transferringCommands);
                    }
                    break;
                case "Unloading Commands":
                    List<Command> unloadingCommands = FactoryManager.Instance.MCS.UnloadingCommandList.OrderBy(x => x.Id).ToList();
                    lbCount.Text = COMMAND_STATE.UNLOADING + " " + unloadingCommands.Count.ToString() + " Counts";
                    lock (unloadingCommands)
                    {
                        UpdateListGridControlLineStatusDetail<Command>(unloadingCommands);
                    }
                    break;
                case "WIP PartSteps":
                    List<PartStep> wip = new List<PartStep>();
                    wip.AddRange(FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.WIP).ToList());
                    lbCount.Text = PART_STEP_STATE.WIP + " " + wip.Count.ToString() + " Counts";
                    lock (wip)
                    {
                        UpdateListGridControlLineStatusDetail(wip.OrderBy(x => x.State == PART_STEP_STATE.WIP).ToList());
                    }
                    break;
                case "Assigned PartSteps":
                    List<PartStep> assigned = new List<PartStep>();
                    assigned.AddRange(FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.ASSIGNED).ToList());
                    lbCount.Text = PART_STEP_STATE.ASSIGNED + " " + assigned.Count.ToString() + " Counts";
                    lock (assigned)
                    {
                        UpdateListGridControlLineStatusDetail(assigned.OrderBy(x => x.State == PART_STEP_STATE.ASSIGNED).ToList());
                    }
                    break;
                case "Track-In PartSteps":
                    List<PartStep> trackIn = new List<PartStep>();
                    trackIn.AddRange(FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.TRACK_IN).ToList());
                    lbCount.Text = PART_STEP_STATE.TRACK_IN + " " + trackIn.Count.ToString() + " Counts";
                    lock (trackIn)
                    {
                        UpdateListGridControlLineStatusDetail(trackIn.OrderBy(x => x.State == PART_STEP_STATE.TRACK_IN).ToList());
                    }
                    break;
                case "Processing PartSteps":
                    List<PartStep> processing = new List<PartStep>();
                    processing.AddRange(FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.PROCESSING).ToList());
                    lbCount.Text = PART_STEP_STATE.PROCESSING + " " + processing.Count.ToString() + " Counts";
                    lock (processing)
                    {
                        UpdateListGridControlLineStatusDetail(processing.OrderBy(x => x.State == PART_STEP_STATE.PROCESSING).ToList());
                    }
                    break;
                case "Step-End PartSteps":
                    List<PartStep> stepEnd = new List<PartStep>();
                    stepEnd.AddRange(FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.STEP_END).ToList());
                    lbCount.Text = PART_STEP_STATE.STEP_END + " " + stepEnd.Count.ToString() + " Counts";
                    lock (stepEnd)
                    {
                        UpdateListGridControlLineStatusDetail(stepEnd.OrderBy(x => x.State == PART_STEP_STATE.STEP_END).ToList());
                    }
                    break;
                case "Dispatching EqpSteps":
                    List<EqpStep> dispatchingSteps = new List<EqpStep>();
                    dispatchingSteps.AddRange(FactoryManager.Instance.MES.EqpSteps.Where(x => x.State == EQP_STEP_STATE.DISPATCHING).ToList());
                    lbCount.Text = EQP_STEP_STATE.DISPATCHING + " " + dispatchingSteps.Count.ToString() + " Counts";
                    lock (dispatchingSteps)
                    {
                        UpdateListGridControlLineStatusDetail(dispatchingSteps.OrderBy(x => x.State == EQP_STEP_STATE.DISPATCHING).ToList());
                    }
                    break;
                case "Waiting EqpSteps":
                    List<EqpStep> waitingSteps = new List<EqpStep>();
                    waitingSteps.AddRange(FactoryManager.Instance.MES.EqpSteps.Where(x => x.State == EQP_STEP_STATE.WAITING).ToList());
                    lbCount.Text = EQP_STEP_STATE.WAITING + " " + waitingSteps.Count.ToString() + " Counts";
                    lock (waitingSteps)
                    {
                        UpdateListGridControlLineStatusDetail(waitingSteps.OrderBy(x => x.State == EQP_STEP_STATE.WAITING).ToList());
                    }
                    break;
                case "Processing EqpSteps":
                    List<EqpStep> processingSteps = new List<EqpStep>();
                    processingSteps.AddRange(FactoryManager.Instance.MES.EqpSteps.Where(x => x.State == EQP_STEP_STATE.PROCESSING).ToList());
                    lbCount.Text = EQP_STEP_STATE.PROCESSING + " " + processingSteps.Count.ToString() + " Counts";
                    lock (processingSteps)
                    {
                        UpdateListGridControlLineStatusDetail(processingSteps.OrderBy(x => x.State == EQP_STEP_STATE.PROCESSING).ToList());
                    }
                    break;
                case "Idle":
                    List<Vehicle.VehicleInfo> idleVehicles = GetVehicleList(true).Select(x => x._vehicleInfo).ToList();
                    
                    idleVehicles = idleVehicles.FindAll(x => x.State.Equals(VEHICLE_STATE.IDLE)).ToList();
                    lbCount.Text = VEHICLE_STATE.IDLE + " " + idleVehicles.Count.ToString() + " Counts";
                    lock (idleVehicles)
                    {
                        UpdateListGridControlLineStatusDetail(idleVehicles);
                    }
                    break;
                case "Move To Load":
                    List<Vehicle.VehicleInfo> moveToLoadVehicles = GetVehicleList(true).Select(x => x._vehicleInfo).ToList();
                    moveToLoadVehicles = moveToLoadVehicles.FindAll(x => x.State.Equals(VEHICLE_STATE.MOVE_TO_LOAD)).ToList();
                    lbCount.Text = VEHICLE_STATE.MOVE_TO_LOAD + " " + moveToLoadVehicles.Count.ToString() + " Counts";
                    lock (moveToLoadVehicles)
                    {
                        UpdateListGridControlLineStatusDetail(moveToLoadVehicles);
                    }
                    break;
                case "Loading":
                    List<Vehicle.VehicleInfo> loadingVehicles = GetVehicleList(true).Select(x => x._vehicleInfo).ToList();
                    loadingVehicles = loadingVehicles.FindAll(x => x.State.Equals(VEHICLE_STATE.LOADING)).ToList();
                    lbCount.Text = VEHICLE_STATE.LOADING + " " + loadingVehicles.Count.ToString() + " Counts";
                    lock (loadingVehicles)
                    {
                        UpdateListGridControlLineStatusDetail(loadingVehicles);
                    }
                    break;
                case "Move To Unload":
                    List<Vehicle.VehicleInfo> moveToUnloadVehicles = GetVehicleList(true).Select(x => x._vehicleInfo).ToList();
                    moveToUnloadVehicles = moveToUnloadVehicles.FindAll(x => x.State.Equals(VEHICLE_STATE.MOVE_TO_UNLOAD)).ToList();
                    lbCount.Text = VEHICLE_STATE.MOVE_TO_UNLOAD + " " + moveToUnloadVehicles.Count.ToString() + " Counts";
                    lock (moveToUnloadVehicles)
                    {
                        UpdateListGridControlLineStatusDetail(moveToUnloadVehicles);
                    }
                    break;
                case "Unloading":
                    List<Vehicle.VehicleInfo> unloadingVehicles = GetVehicleList(true).Select(x => x._vehicleInfo).ToList();
                    unloadingVehicles = unloadingVehicles.FindAll(x => x.State.Equals(VEHICLE_STATE.UNLOADING)).ToList();
                    lbCount.Text = VEHICLE_STATE.UNLOADING + " " + unloadingVehicles.Count.ToString() + " Counts";
                    lock (unloadingVehicles)
                    {
                        UpdateListGridControlLineStatusDetail(unloadingVehicles);
                    }
                    break;
                case "Stage":
                    List<Vehicle.VehicleInfo> stageVehicles = GetVehicleList(true).Select(x => x._vehicleInfo).ToList();
                    stageVehicles = stageVehicles.FindAll(x => x.State.Equals(VEHICLE_STATE.STAGE)).ToList();
                    lbCount.Text = VEHICLE_STATE.STAGE + " " + stageVehicles.Count.ToString() + " Counts";
                    lock (stageVehicles)
                    {
                        UpdateListGridControlLineStatusDetail(stageVehicles);
                    }
                    break;
                default:
                    break;
            }
        }

        private void RowSelectTotalSubCS(string selectData, string parentNode)
        {
            string csName = parentNode.Split(' ')[0];

            switch (selectData)
            {
                case "Queued Commands":
                    List<Command> queuedCommands = FactoryManager.Instance.MCS.QueuedCommandList.ToList().FindAll(x => x.CSName.Split('_')[0] == csName).OrderBy(x => x.Id).ToList();
                    lbCount.Text = queuedCommands.Count.ToString() + " Counts";
                    lock (queuedCommands)
                    {
                        UpdateListGridControlLineStatusDetail<Command>(queuedCommands);
                    }
                    break;
                case "Waiting Commands":
                    List<Command> waitingCommands = FactoryManager.Instance.MCS.WaitingCommandList.ToList().FindAll(x => x.CSName.Split('_')[0] == csName).OrderBy(x => x.Id).ToList();
                    lbCount.Text = waitingCommands.Count.ToString() + " Counts";
                    lock (waitingCommands)
                    {
                        UpdateListGridControlLineStatusDetail<Command>(waitingCommands);
                    }
                    break;
                case "Loading Commands":
                    List<Command> loadingCommands = FactoryManager.Instance.MCS.LoadingCommandList.ToList().FindAll(x => x.CSName.Split('_')[0] == csName).OrderBy(x => x.Id).ToList();
                    lbCount.Text = loadingCommands.Count.ToString() + " Counts";
                    lock (loadingCommands)
                    {
                        UpdateListGridControlLineStatusDetail<Command>(loadingCommands);
                    }
                    break;
                case "Transferring Commands":
                    List<Command> transferringCommands = FactoryManager.Instance.MCS.TransferringCommandList.ToList().FindAll(x => x.CSName.Split('_')[0] == csName).OrderBy(x => x.Id).ToList();

                    lbCount.Text = transferringCommands.Count.ToString() + " Counts";
                    lock (transferringCommands)
                    {
                        UpdateListGridControlLineStatusDetail<Command>(transferringCommands);
                    }
                    break;
                case "Unloading Commands":
                    List<Command> unloadingCommands = FactoryManager.Instance.MCS.UnloadingCommandList.ToList().FindAll(x => x.CSName.Split('_')[0] == csName).OrderBy(x => x.Id).ToList();
                    lbCount.Text = unloadingCommands.Count.ToString() + " Counts";
                    lock (unloadingCommands)
                    {
                        UpdateListGridControlLineStatusDetail<Command>(unloadingCommands);
                    }
                    break;
                default:
                    break;
            }
        }

        private void UpdateListGridControlLineStatusDetail<T>(List<T> list)
        {
            try
            {
                gridViewLineStatusDetail.BeginUpdate();
                gridControlLineStatusDetail.BeginUpdate();
                gridViewLineStatusDetail.Columns.Clear();

                gridControlLineStatusDetail.DataSource = list;

                if (gridViewLineStatusDetail.Columns.ColumnByFieldName("StringAssignedTime") != null) // Vcommand일 경우 LineStatusDetail Column 순서 변경 (Command와 동일하게)
                {
                    gridViewLineStatusDetail.Columns["Id"].VisibleIndex = 0;
                    gridViewLineStatusDetail.Columns["StringActivatedTime"].VisibleIndex = 1;
                    gridViewLineStatusDetail.Columns["StringCompletedTime"].VisibleIndex = 2;
                    gridViewLineStatusDetail.Columns["Priority"].VisibleIndex = 3;
                    gridViewLineStatusDetail.Columns["State"].VisibleIndex = 4;
                    gridViewLineStatusDetail.Columns["TotalTime"].VisibleIndex = 5;
                    gridViewLineStatusDetail.Columns["TransferringDistance"].VisibleIndex = 6;
                }

                gridViewLineStatusDetail.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
                gridViewLineStatusDetail.OptionsScrollAnnotations.ShowFocusedRow = DefaultBoolean.True;

                gridControlLineStatusDetail.EndUpdate();
                gridViewLineStatusDetail.BestFitColumns();
                gridViewLineStatusDetail.EndUpdate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void gridViewPart_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int index = gridViewPart.FocusedRowHandle;
            //gridViewPart.Columns.Clear();
            Part part = gridViewPart.GetRow(index) as Part;

            if (!pinokio3DModel1.PartReferences.ContainsKey(part.ID))
                return;

            PartReference selectedPart = pinokio3DModel1.PartReferences[part.ID];

            if (selectedPart == null)
                return;

            ChangeUpdateViewerBySelectedPart(part, selectedPart);
        }

        private void ChangeUpdateViewerBySelectedPart(Part part, PartReference refPart)
        {
            CloseObjectManipulator();
            ClearPropertyGrid();

            BeginInvoke(new Action(() => pinokio3DModel1.Focus()));

            if (part != null && pinokio3DModel1.PartReferences.ContainsKey(part.ID))
            {
                refPart = pinokio3DModel1.PartReferences[part.ID];
                if (!SelectedEntityReferences.Contains(refPart))
                    SelectedEntityReferences.Add(refPart);
                refPart.Selected = true;
                pinokio3DModel1.Camera.Location = new Point3D(part.PosVec3.X, part.PosVec3.Y);
            }
            else
                BeginInvoke(new Action(() => pinokio3DModel1.ZoomFit()));

            BeginInvoke(new Action(() => pinokio3DModel1.Entities.Regen()));
            BeginInvoke(new Action(() => Invalidate()));

            CheangeSelectedSimObject4PropertyGrid(part);
        }

        private void UpdateListGridControlPart(List<Part> list)
        {
            try
            {
                gridViewPart.BeginUpdate();
                gridControlPart.BeginUpdate();
                int index = gridViewPart.FocusedRowHandle;
                //gridViewPart.Columns.Clear();
                Part part = gridViewPart.GetRow(index) as Part;
                gridControlPart.DataSource = list;
                int newIndex = 0;
                if (part != null)
                    newIndex = list.FindIndex(x => x.ID == part.ID);
                gridViewPart.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
                gridViewPart.OptionsScrollAnnotations.ShowFocusedRow = DefaultBoolean.True;
                gridViewPart.FocusedRowHandle = newIndex;
                gridViewPart.Columns["PosVec3"].Visible = false;
                gridViewPart.Columns["AngleInRadians"].Visible = false;
                gridViewPart.Columns["Size"].Visible = false;
                gridViewPart.Columns["GenDateTime"].Visible = false;
                gridViewPart.Columns["ProductID"].Visible = false;
                gridViewPart.Columns["Xdimension"].Visible = false;
                gridViewPart.Columns["Ydimension"].Visible = false;
                gridViewPart.Columns["Zdimension"].Visible = false;
                gridViewPart.Columns["PartCapa"].Visible = false;
                gridViewPart.Columns["CMDStartStationName"].Visible = false;
                gridViewPart.Columns["CMDEndStationName"].Visible = false;
                gridViewPart.Columns["Direction"].Visible = false;
                gridViewPart.Columns["Quantity"].Visible = false;
                gridViewPart.Columns["RotateAxis"].Visible = false;
                gridViewPart.Columns["NodeID"].Visible = false;
                gridViewPart.Columns["Name"].VisibleIndex = 0;
                gridViewPart.Columns["ProductName"].VisibleIndex = 1;
                gridViewPart.Columns["CurTXNodeName"].VisibleIndex = 2;
                gridViewPart.Columns["MRDestination"].VisibleIndex = 3;
                gridViewPart.Columns["CommandName"].VisibleIndex = 4;
                gridViewPart.Columns["Name"].Caption = "Name";
                gridViewPart.Columns["ProductName"].Caption = "Product Name";
                gridViewPart.Columns["CurTXNodeName"].Caption = "Current";
                gridViewPart.Columns["MRDestination"].Caption = "Destination";
                gridViewPart.Columns["CommandName"].Caption = "Command";

                gridControlPart.EndUpdate();
                gridViewPart.BestFitColumns();
                gridViewPart.EndUpdate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        void timer_TreeCommand(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (ModelManager.Instance.AnimationNode != null && ModelManager.Instance.AnimationNode.IsUse)
                BeginInvoke(new TimerTreeCommand(treeList_Row)); 
        }
        private void cbTree_Interval_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbTree_Interval_TimerSetting();
        }
        private void cbTree_Interval_TimerSetting()
        {
            if (cbTree_Interval.Text != "OFF")
            {
                double interval_value = Convert.ToDouble(cbTree_Interval.SelectedItem);

                Treetimer.Interval = interval_value * 1000;
                Treetimer.Elapsed += new System.Timers.ElapsedEventHandler(timer_TreeCommand);
                Treetimer.Start();
            }
        }
        private void AddTreeListLineStatus()
        {
            try
            {
                treeListLineStatus.BeginUnboundLoad();
                treeListLineStatus.Nodes.Clear();

                TreeListNode partStepNode = AddTreeListNode(null, new object[] { "Total PartSteps", FactoryManager.Instance.MES.DicPartStep.Count });

                AddTreeListNode(partStepNode, new object[] { partStepTreeNodeNames[0], FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.WIP).ToList().Count() });
                AddTreeListNode(partStepNode, new object[] { partStepTreeNodeNames[1], FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.ASSIGNED).ToList().Count() });
                AddTreeListNode(partStepNode, new object[] { partStepTreeNodeNames[2], FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.TRACK_IN).ToList().Count() });
                AddTreeListNode(partStepNode, new object[] { partStepTreeNodeNames[3], FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.PROCESSING).ToList().Count() });
                AddTreeListNode(partStepNode, new object[] { partStepTreeNodeNames[4], FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.STEP_END).ToList().Count() });

                TreeListNode eqpStepNode = AddTreeListNode(null, new object[] { "Total EqpSteps", FactoryManager.Instance.MES.DicPartStep.Count });

                AddTreeListNode(eqpStepNode, new object[] { eqpStepTreeNodeNames[0], FactoryManager.Instance.MES.EqpSteps.Where(x => x.State == EQP_STEP_STATE.DISPATCHING).ToList().Count() });
                AddTreeListNode(eqpStepNode, new object[] { eqpStepTreeNodeNames[1], FactoryManager.Instance.MES.EqpSteps.Where(x => x.State == EQP_STEP_STATE.WAITING).ToList().Count() });
                AddTreeListNode(eqpStepNode, new object[] { eqpStepTreeNodeNames[2], FactoryManager.Instance.MES.EqpSteps.Where(x => x.State == EQP_STEP_STATE.PROCESSING).ToList().Count() });

                TreeListNode mrNode = AddTreeListNode(null, new object[] { "Total MRs", FactoryManager.Instance.MCS.DicMR.Count });

                TreeListNode commandNode = AddTreeListNode(null, new object[] { "Total Commands", FactoryManager.Instance.MCS.TotalCommandListCount });

                AddTreeListNode(commandNode, new object[] { commandTreeNodeNames[0], FactoryManager.Instance.MCS.QueuedCommandList.Count });
                AddTreeListNode(commandNode, new object[] { commandTreeNodeNames[1], FactoryManager.Instance.MCS.WaitingCommandList.Count });
                AddTreeListNode(commandNode, new object[] { commandTreeNodeNames[2], FactoryManager.Instance.MCS.LoadingCommandList.Count });
                AddTreeListNode(commandNode, new object[] { commandTreeNodeNames[3], FactoryManager.Instance.MCS.TransferringCommandList.Count });
                AddTreeListNode(commandNode, new object[] { commandTreeNodeNames[4], FactoryManager.Instance.MCS.UnloadingCommandList.Count });

                var vehicleLst = GetVehicleList();
                TreeListNode vehicleCommandNode = AddTreeListNode(null, new object[] { "Total Vehicles", vehicleLst.Count });

                AddTreeListNode(vehicleCommandNode, new object[] { vehicleCommandTreeNodeNames[0], vehicleLst.FindAll(x => x.State.Equals(VEHICLE_STATE.IDLE)).Count });
                AddTreeListNode(vehicleCommandNode, new object[] { vehicleCommandTreeNodeNames[1], vehicleLst.FindAll(x => x.State.Equals(VEHICLE_STATE.MOVE_TO_LOAD)).Count });
                AddTreeListNode(vehicleCommandNode, new object[] { vehicleCommandTreeNodeNames[2], vehicleLst.FindAll(x => x.State.Equals(VEHICLE_STATE.LOADING)).Count });
                AddTreeListNode(vehicleCommandNode, new object[] { vehicleCommandTreeNodeNames[3], vehicleLst.FindAll(x => x.State.Equals(VEHICLE_STATE.MOVE_TO_UNLOAD)).Count });
                AddTreeListNode(vehicleCommandNode, new object[] { vehicleCommandTreeNodeNames[4], vehicleLst.FindAll(x => x.State.Equals(VEHICLE_STATE.UNLOADING)).Count });
                AddTreeListNode(vehicleCommandNode, new object[] { vehicleCommandTreeNodeNames[5], vehicleLst.FindAll(x => x.State.Equals(VEHICLE_STATE.STAGE)).Count });

                //var a = FloorForm.LstFloorPlan[0].Floor.Nodes.FindAll(x => x is VSubCS);

                var listSubCS = FactoryManager.Instance.DicSubCS.Values.ToList();
                _csNames = new List<string>();
                foreach (SubCS subCS in listSubCS)
                {
                    string csName = subCS.Name.Split('_')[0];
                    if (!_csNames.Contains(csName))
                    {
                        _csNames.Add(csName);
                        TreeListNode subCSTree = AddTreeListNode(null, new object[] { $"{csName} Commands", 0 });
                        AddTreeListNode(subCSTree, new object[] { commandTreeNodeNames[0], 0 });
                        AddTreeListNode(subCSTree, new object[] { commandTreeNodeNames[1], 0 });
                        AddTreeListNode(subCSTree, new object[] { commandTreeNodeNames[2], 0 });
                        AddTreeListNode(subCSTree, new object[] { commandTreeNodeNames[3], 0 });
                    }
                }

                treeListLineStatus.EndUnboundLoad();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void treeList_RowCellClick(object sender, DevExpress.XtraTreeList.RowCellClickEventArgs e)
        {
            Treetimer.Stop();
            lbCount.Text = "0 Counts";

            selectedNode = e.Node;
            string selectedNodeText = selectedNode.GetDisplayText("Contents");
            TreeListNode parentNode = selectedNode.ParentNode;

            if (parentNode == null)
            {
                string[] types = selectedNodeText.Split(' ');
                if (types[0].ToUpper().Contains("TOTAL"))
                {
                    RowSelectParentTotal(types[1]);
                }
                else if (types[1].ToUpper().Contains("COMMANDS")) // select SubCS
                {
                    RowSelectParentTotal(types[0]);
                }
            }
            else
            {
                string type = parentNode.GetDisplayText("Contents").Split(' ')[0];
                if (type.ToUpper().Contains("TOTAL"))
                {
                    RowSelectTotal(selectedNodeText);
                }
                else
                {
                    RowSelectTotalSubCS(selectedNodeText, parentNode.GetDisplayText("Contents"));
                }
            }
            if (gridViewLineStatusDetail.RowCount > 0)
                UpdateGridViewLineStatusDetail(gridViewLineStatusDetail.FocusedRowHandle);

            cbTree_Interval_TimerSetting();
        }

        private TreeListNode AddTreeListNode(TreeListNode parentTreeListNode, object value)
        {
            TreeListNode treeListNode = treeListLineStatus.AppendNode(value, parentTreeListNode);

            return treeListNode;
        }

        private void treeList_Row()
        {
            string selectedNodeText = selectedNode.GetDisplayText("Contents");
            TreeListNode parentNode = selectedNode.ParentNode;

            if (parentNode == null)
            {
                string[] types = selectedNodeText.Split(' ');
                if (types[0].ToUpper().Contains("TOTAL"))
                {
                    RowSelectParentTotal(types[1]);
                }
            }
            else
            {
                string type = parentNode.GetDisplayText("Contents").Split(' ')[0];
                if (type.ToUpper().Contains("TOTAL"))
                {
                    RowSelectTotal(selectedNodeText);
                }
            }
        }

        private void UpdatePartStatus()
        {
            UpdateListGridControlPart(ModelManager.Instance.Parts.Values.ToList());
        }

        private void UpdateLineStatus()
        {
            try
            {
                List<int> line_status = new List<int>();
                treeListLineStatus.BeginUpdate();

                List<TreeListNode> nodes = treeListLineStatus.GetNodeList();

                nodes[0].SetValue(1, FactoryManager.Instance.MES.DicPartStep.Count);
                nodes[1].SetValue(1, FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.WIP).ToList().Count());
                nodes[2].SetValue(1, FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.ASSIGNED).ToList().Count());
                nodes[3].SetValue(1, FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.TRACK_IN).ToList().Count());
                nodes[4].SetValue(1, FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.PROCESSING).ToList().Count());
                nodes[5].SetValue(1, FactoryManager.Instance.MES.DicPartStep.Values.Where(x => x.State == PART_STEP_STATE.STEP_END).ToList().Count());

                nodes[6].SetValue(1, FactoryManager.Instance.MES.EqpSteps.Count);
                nodes[7].SetValue(1, FactoryManager.Instance.MES.EqpSteps.Where(x => x.State == EQP_STEP_STATE.DISPATCHING).ToList().Count());
                nodes[8].SetValue(1, FactoryManager.Instance.MES.EqpSteps.Where(x => x.State == EQP_STEP_STATE.WAITING).ToList().Count());
                nodes[9].SetValue(1, FactoryManager.Instance.MES.EqpSteps.Where(x => x.State == EQP_STEP_STATE.PROCESSING).ToList().Count());

                nodes[10].SetValue(1, FactoryManager.Instance.MCS.DicMR.Count); // Total MRs

                nodes[11].SetValue(1, FactoryManager.Instance.MCS.TotalCommandListCount); // Total Commands
                nodes[12].SetValue(1, FactoryManager.Instance.MCS.QueuedCommandList.Count); // Total Commans - Queued Command
                nodes[13].SetValue(1, FactoryManager.Instance.MCS.WaitingCommandList.Count); // Total Commans - Waiting Command
                nodes[14].SetValue(1, FactoryManager.Instance.MCS.LoadingCommandList.Count); // Total Commans - Loading Command
                nodes[15].SetValue(1, FactoryManager.Instance.MCS.TransferringCommandList.Count); // Total Commans - Transferring Command
                nodes[16].SetValue(1, FactoryManager.Instance.MCS.UnloadingCommandList.Count); // Total Commans - Unloading Command

                var vehicleLst = GetVehicleList();

                nodes[17].SetValue(1, vehicleLst.Count);
                nodes[18].SetValue(1, vehicleLst.FindAll(x => x.State.Equals(VEHICLE_STATE.IDLE)).Count);
                nodes[19].SetValue(1, vehicleLst.FindAll(x => x.State.Equals(VEHICLE_STATE.MOVE_TO_LOAD)).Count);
                nodes[20].SetValue(1, vehicleLst.FindAll(x => x.State.Equals(VEHICLE_STATE.LOADING)).Count);
                nodes[21].SetValue(1, vehicleLst.FindAll(x => x.State.Equals(VEHICLE_STATE.MOVE_TO_UNLOAD)).Count);
                nodes[22].SetValue(1, vehicleLst.FindAll(x => x.State.Equals(VEHICLE_STATE.UNLOADING)).Count);
                nodes[23].SetValue(1, vehicleLst.FindAll(x => x.State.Equals(VEHICLE_STATE.STAGE)).Count);

                int idx = 6;
                foreach (string csName in _csNames)
                {
                    int queuedCnt = FactoryManager.Instance.MCS.QueuedCommandList.ToList().FindAll(x => x.CSName.Split('_')[0] == csName).Count;
                    int waitingCnt = FactoryManager.Instance.MCS.WaitingCommandList.ToList().FindAll(x => x.CSName.Split('_')[0] == csName).Count;
                    int transferringCnt = FactoryManager.Instance.MCS.TransferringCommandList.ToList().FindAll(x => x.CSName.Split('_')[0] == csName).Count;
                    int completedCnt = FactoryManager.Instance.MCS.CompletedCommandCount;
                    int totalCnt = queuedCnt + waitingCnt + transferringCnt;
                    nodes[idx].SetValue(1, totalCnt);
                    nodes[idx + 1].SetValue(1, queuedCnt);
                    nodes[idx + 2].SetValue(1, waitingCnt);
                    nodes[idx + 3].SetValue(1, transferringCnt);
                    nodes[idx + 4].SetValue(1, completedCnt);
                    idx = idx + 5;
                }

                treeListLineStatus.EndUpdate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void GridViewPart_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                UpdateGridViewPart(e.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                gridViewPart.FocusedRowHandle = -1;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void ListGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                UpdateGridViewLineStatusDetail(e.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                gridViewLineStatusDetail.FocusedRowHandle = -1;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        delegate void TimerEventFiredDelegate();


        private void UpdateGridViewPart(int rowHandle)
        {
            if (rowHandle < 0 || !gridViewPart.IsFocusedView)
                return;

            Part part = gridViewPart.GetRow(rowHandle) as Part;
            PartStep partStep = part.PartStep;
            NodeReference refNode = null;
            ClearSelection();
            PartReference refPart = null; 
            if(pinokio3DModel1.PartReferences.ContainsKey(part.ID))
                refPart = pinokio3DModel1.PartReferences[part.ID];

            if (partStep != null)
            {
                if (partStep.State is PART_STEP_STATE.WIP)
                {
                    if (Pinokio3Dmodel.NodeReferenceByID.ContainsKey(part.CurTXNode.ID))
                        refNode = pinokio3DModel1.NodeReferenceByID[part.CurTXNode.ID];
                }
                else
                {
                    if (Pinokio3Dmodel.NodeReferenceByID.ContainsKey(partStep.EqpID))
                        refNode = pinokio3DModel1.NodeReferenceByID[partStep.EqpID];
                }
            }

            if (refNode != null)
            {
                SelectedNodeReferences.Add(refNode);
                refNode.Selected = true;
            }

            ChangeUpdateViewerBySelectedPart(part, refPart);
        }

        private void UpdateGridViewLineStatusDetail(int rowHandle)
        {
            if (rowHandle < 0)
                return;

            NodeReference refNode = null;
            PartReference refPart = null;
            Part part = null;
            List<Part> parts = new List<Part>();

            ClearSelection();
            switch (gridViewLineStatusDetail.GetRow(rowHandle))
            {
                case PartStep partStep:
                    part = ModelManager.Instance.Parts[partStep.PartID];
                    if (partStep.State is PART_STEP_STATE.WIP)
                    {
                        if (Pinokio3Dmodel.NodeReferenceByID.ContainsKey(part.CurTXNode.ID))
                            refNode = pinokio3DModel1.NodeReferenceByID[part.CurTXNode.ID];
                    }
                    else
                    {
                        if (Pinokio3Dmodel.NodeReferenceByID.ContainsKey(partStep.EqpID))
                            refNode = pinokio3DModel1.NodeReferenceByID[partStep.EqpID];
                    }

                    break;
                case EqpStep eqpStep:
                    parts = eqpStep.InputParts.ToList();

                    if (Pinokio3Dmodel.NodeReferenceByID.ContainsKey(eqpStep.EqpID))
                        refNode = pinokio3DModel1.NodeReferenceByID[eqpStep.EqpID];


                    break;

                case MR mr:
                    part = mr.Part;
                    if (Pinokio3Dmodel.NodeReferenceByID.ContainsKey(part.CurTXNode.ID))
                        refNode = pinokio3DModel1.NodeReferenceByID[part.CurTXNode.ID];

                    expectedNodeChangeColor(mr);

                    break;
                case Command command:
                    part = command.Part;
                    if (command.State is COMMAND_STATE.WAITING
                        || command.State is COMMAND_STATE.LOADING)
                    {
                        if (Pinokio3Dmodel.NodeReferenceByID.ContainsKey(command.Vehicle.ID))
                            refNode = pinokio3DModel1.NodeReferenceByID[command.Vehicle.ID];
                    }
                    else
                    {
                        if (Pinokio3Dmodel.NodeReferenceByID.ContainsKey(part.CurTXNode.ID))
                            refNode = pinokio3DModel1.NodeReferenceByID[command.Part.CurTXNode.ID];
                    }

                    expectedNodeChangeColor(command);

                    break;
            }

            if (refNode != null)
            {
                SelectedNodeReferences.Add(refNode);
                refNode.Selected = true;
            }

            if (part != null && pinokio3DModel1.PartReferences.ContainsKey(part.ID))
            {
                refPart = pinokio3DModel1.PartReferences[part.ID];
                SelectedEntityReferences.Add(refPart);
                refPart.Selected = true;
            }

            foreach(Part partTemp in parts)
            {
                if (pinokio3DModel1.PartReferences.ContainsKey(partTemp.ID))
                {
                    refPart = pinokio3DModel1.PartReferences[partTemp.ID];
                    SelectedEntityReferences.Add(refPart);
                    refPart.Selected = true;
                }
            }

            MouseUpMultiSelection();
        }

        void LineStatusTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (!(SimEngine.Instance.EngineState is ENGINE_STATE.STOP || SimEngine.Instance.EngineState is ENGINE_STATE.PAUSE))
                {
                    UpdateSimTime();
                    UpdateAccelerationRate();

                    if (ModelManager.Instance.AnimationNode != null && ModelManager.Instance.AnimationNode.IsUse)
                    {
                        treeListLineStatus.BeginInvoke(new TimerEventFiredDelegate(UpdateLineStatus));
                        gridControlPart.BeginInvoke(new TimerEventFiredDelegate(UpdatePartStatus));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void expectedNodeChangeColor(Command command)
        {
            foreach (var nodeRef in changedNodeRef.Values)
            {
                nodeRef.Item1.Color = nodeRef.Item2;
                nodeRef.Item1.Selected = false;
            }
            changedNodeRef.Clear();
            if (!(command.State is COMMAND_STATE.QUEUED))
            {
                foreach (TransportLine AcqLine in command.AcquireRoute)
                {
                    if (AcqLine == command.AcquireRoute.LastOrDefault())
                    {
                        changeNodeColor(AcqLine, RouteRailType.Acquire);
                    }
                    else
                    {
                        changeNodeColor(AcqLine, RouteRailType.Waiting);
                    }
                }
                if (!(command.State is COMMAND_STATE.WAITING)
                            && !(command.State is COMMAND_STATE.LOADING))
                {
                    //Actual Route 그리기
                    foreach (TransportLine ActLine in command.ActualRoute)
                    {
                        if (ActLine == command.ActualRoute.LastOrDefault())
                        {
                            changeNodeColor(ActLine, RouteRailType.Deposit);
                        }
                        else if (ActLine == command.ActualRoute[0])
                        {
                            ;
                        }
                        else
                        {
                            changeNodeColor(ActLine, RouteRailType.Transferring);
                        }
                    }
                }
            }
        }
        private void expectedNodeChangeColor(MR mr)
        {
            foreach (var changedNode in changedNodeRef.Values)
            {
                changedNode.Item1.Color = changedNode.Item2;
                changedNode.Item1.Selected = false;
            }
            changedNodeRef.Clear();

            NodeReference nodeRef = null;
            Color originColor = Color.Gray;

            foreach (var node in mr.Route)
            {
                nodeRef = Pinokio3Dmodel.NodeReferenceByID[node.ID];
                originColor = nodeRef.Color;
                changeTXNodeColor(node, RouteRailType.StartNode);
                //nodeRef.Color = Color.FromArgb(255, 0, 0, 254);
            }
            if (!changedNodeRef.ContainsKey(nodeRef.Name))
                changedNodeRef.Add(nodeRef.Name, new Tuple<NodeReference, Color>(nodeRef, originColor));
        }

        public void changeNodeColor(TransportLine line, RouteRailType type)
        {
            NodeReference nodeRef = null;
            Color originColor = Color.Gray;

            switch (type)
            {
                case RouteRailType.Acquire:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[line.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 0, 129, 0);

                    break;
                case RouteRailType.Waiting:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[line.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 173, 255, 50);

                    break;
                case RouteRailType.Deposit:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[line.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 0, 0, 254);

                    break;
                case RouteRailType.Transferring:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[line.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 255, 166, 0);

                    break;
                case RouteRailType.Select:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[line.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 254, 0, 0);

                    break;
                case RouteRailType.StartNode:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[line.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 0, 129, 0);

                    break;
                case RouteRailType.EndNode:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[line.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 0, 0, 254);

                    break;
            }
            if (!changedNodeRef.ContainsKey(nodeRef.Name))
                changedNodeRef.Add(nodeRef.Name, new Tuple<NodeReference, Color>(nodeRef, originColor));
        }
        public void changeTXNodeColor(TXNode node, RouteRailType type)
        {
            NodeReference nodeRef = null;
            Color originColor = Color.Gray;

            switch (type)
            {
                case RouteRailType.Transferring:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[node.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 255, 166, 0);

                    break;
                case RouteRailType.StartNode:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[node.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 0, 129, 0);

                    break;
                case RouteRailType.EndNode:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[node.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 0, 0, 254);

                    break;
            }
            if (!changedNodeRef.ContainsKey(nodeRef.Name))
                changedNodeRef.Add(nodeRef.Name, new Tuple<NodeReference, Color>(nodeRef, originColor));
        }

        private void ListGridView_gridControl_MouseUp(object sender, MouseEventArgs e)
        {
            var gridView = sender as GridView;
            var hitInfo = gridView.CalcHitInfo(new System.Drawing.Point(e.X, e.Y));

            // 클릭된 위치가 빈 공간인지 확인
            if (!hitInfo.InRow && !hitInfo.InRowCell && e.Button == MouseButtons.Left)
            {
                ClearSelection();

                foreach (var nodeRef in changedNodeRef.Values)
                {
                    nodeRef.Item1.Color = nodeRef.Item2;
                    nodeRef.Item1.Selected = false;
                }
            }
        }

        private void State_Based_Vehicle_Tog_Switch_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!State_Based_Vehicle_Tog_Switch.Checked)
            {
                foreach (NodeReference node in pinokio3DModel1.NodeReferenceByID.Values)
                {
                    if (node is RefVehicle)
                    {
                        Block nodeBlock = pinokio3DModel1.Blocks.FirstOrDefault(x => x.Name == node.BlockName);
                        foreach (Entity en in nodeBlock.Entities)
                        {
                            en.ColorMethod = colorMethodType.byEntity;
                        }
                    }
                }
            }
            else
            {
                foreach (NodeReference node in pinokio3DModel1.NodeReferenceByID.Values)
                {
                    if (node is RefVehicle)
                    {
                        Block nodeBlock = pinokio3DModel1.Blocks.FirstOrDefault(x => x.Name == node.BlockName);
                        foreach (Entity en in nodeBlock.Entities)
                        {
                            en.ColorMethod = colorMethodType.byParent;
                        }
                    }
                }
            }
            pinokio3DModel1.Invalidate();
        }

        private void State_Based_Equipment_Tog_Switch_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!State_Based_Equipment_Tog_Switch.Checked)
            {
                foreach(NodeReference node in pinokio3DModel1.NodeReferenceByID.Values)
                {
                    if (node.Core is Equipment)
                    {
                        Block nodeBlock = pinokio3DModel1.Blocks.FirstOrDefault(x => x.Name == node.BlockName);
                        foreach (Entity en in nodeBlock.Entities)
                        {
                            en.ColorMethod = colorMethodType.byEntity;
                        }
                    }
                }
            }
            else
            {
                foreach (NodeReference node in pinokio3DModel1.NodeReferenceByID.Values)
                {
                    if (node.Core is Equipment)
                    {
                        Block nodeBlock = pinokio3DModel1.Blocks.FirstOrDefault(x => x.Name == node.BlockName);
                        foreach (Entity en in nodeBlock.Entities)
                        {
                            en.ColorMethod = colorMethodType.byParent;
                        }
                    }
                }
            }
            pinokio3DModel1.Invalidate();

        }

        private void State_Based_TransportLine_Tog_Switch_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        private void StateBasedColorChange(bool isCheckd)
        {
            if (isCheckd)
            { 

            }
        }
    }
}
