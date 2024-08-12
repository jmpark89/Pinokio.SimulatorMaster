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
using DevExpress.XtraPrinting;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using System.IO;
using DevExpress.Utils.DPI;
using devDept.Eyeshot.Fem;
using System.Threading;
using DevExpress.XtraBars.Docking;
using Pinokio.Database;
using Logger;
using Pinokio.Animation;
using Pinokio.Designer;
using DevExpress.XtraTab;
using DevExpress.XtraVerticalGrid.Rows;
using System.Reflection;
using Pinokio.Model.Base;
using DevExpress.XtraVerticalGrid.Events;
using Simulation.Engine;
using Pinokio.UI.Base;
using System.Diagnostics;
using Point = System.Drawing.Point;
using DevExpress.Utils;
using Pinokio.Auth;
using static Pinokio.Animation.PinokioBaseModel;
using DevExpress.XtraBars.Docking2010.Customization;
using Pinokio.Designer.DataClass;
using Pinokio.Geometry;
using Pinokio.Model.User;
using DevExpress.XtraSplashScreen;
using Simulation;
using Pinokio.Simulation;
using devDept.Eyeshot.Labels;

namespace Pinokio.Designer
{
    [Serializable]
    public partial class ModelDesigner : UserControl
    {
        #region MEMBER VARIABLES

        public bool _isFloorSetting = false;
        public bool _isInitialize = true;
        private Point3D _mouseSnapPointBeforeMouseMove;
        private Point3D _mouseSnapPointBeforeMouseDown;
        private Point3D _lastClickedPoint;
        [NonSerialized]
        private Pinokio.Animation.PinokioBaseModel _detailModel;
        private ModelActionType _modelActionType { get => pinokio3DModel1.ModelActionType; set => pinokio3DModel1.ModelActionType = value; }
        private FormFloorSelect _floorForm;
        public dynamic ScriptDynamicFunction;
        public string DynamicBufferScript;
        private int _entityIndex = -1;
        [NonSerialized]
        private Dictionary<string, List<SimNodeTreeListNode>> dicSimNodeType = new Dictionary<string, List<SimNodeTreeListNode>>();
        [NonSerialized]
        private Dictionary<string, Tuple<bool,bool>> dicVisibleNodeTypeInfo = new Dictionary<string, Tuple<bool, bool>>();
        private Plane _plane;
        public Plane Plane { get => _plane; set => _plane = value; }
        public int EntityIndex { get => _entityIndex; set => _entityIndex = value; }
        public FormFloorSelect FloorForm { get => _floorForm; set => _floorForm = value; }
        public PinokioBaseModel Pinokio3Dmodel { get { return pinokio3DModel1; } }

        public NodeReference CurrentRef { get => Pinokio3Dmodel.CurrentRef; set => Pinokio3Dmodel.CurrentRef = value; }
        public Point3D MouseSnapPointBeforeMouseMove { get => _mouseSnapPointBeforeMouseMove; set => _mouseSnapPointBeforeMouseMove = value; }
        public Point3D MouseSnapPointBeforeMouseDown { get => _mouseSnapPointBeforeMouseDown; set => _mouseSnapPointBeforeMouseDown = value; }
        public PinokioBaseModel DetailModel { get => _detailModel; set => _detailModel = value; }
        public Point3D LastClickedPoint { get => _lastClickedPoint; set => _lastClickedPoint = value; }

        #endregion

        public ModelDesigner()
        {
            InitializeComponent();

            pinokio3DModel1 = new PinokioEditorModel();
            pinokio3DModel1.Unlock("US2-RMKUX-N12YW-WAMY-S738");
            this.simNodeTreeList.Model = pinokio3DModel1;
            //this.partTreeList.Model = pinokio3DModel1;
            //if (!PinokioLincense.isAuth())
            //{
            //    MessageBox.Show("NotLicense");
            //    Process.GetCurrentProcess().Kill();
            //}

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                ModelManager.Instance.GenerateParts += new deleAlarmGenerateParts(Pinokio3Dmodel.AddPartsReference);
                ModelManager.Instance.GeneratePart += new deleAlarmGeneratePart(Pinokio3Dmodel.AddPartReference);
                ModelManager.Instance.DeletePart += new deleAlarmDeletePart(Pinokio3Dmodel.RemovePartReference);
                ModelManager.Instance.SimulationEnd += new deleSimulationEnd(SimulationEnd);
                ModelManager.Instance.FailSimulation += new deleFaliSimulation(FaliSimulation);
                ModelManager.Instance.SimResultDBManager = new UserSimResultDBManager();
                (new Simulator()).OnStartSimulation += Simulator_OnStartSimulation;

                Initialize3DModel();
                Initialize3DOptions();
                InitializeLight();
                InitializeDB();
                InitializeTreeAction();
                InitializeInsertNodes();
                //InitializeInsertTreeNodes();
                InitializeInsertCoupledModels();
                InitializeInsertedSimNodesTreeList(simNodeTreeList);
                //InitializeSimEntityTreeList(partTreeList);
                InitializeSimTimeSetting();
                InitialzieObjectManipulator();

                _floorForm = new FormFloorSelect(new List<FloorPlan>());
                cbTree_Interval.Text = "OFF";
                lbCount.Text = "0 Counts";

                ModifyNodeTreeList(ModelManager.Instance.SimNodes.Values.ToList());

                FloorSetup();
                _isInitialize = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }





        private void SetFloorform()
        {
            if (_isInitialize == true)
            {
                _floorForm.LstFloorPlan.Add(new FloorPlan(1, "Default", 100000, 100000, 0));
            }

            if (_floorForm.LstFloorPlan.Count > 0)
            {
                List<Grid> grids = new List<Grid>();

                for (int i = 0; i < _floorForm.LstFloorPlan.Count; i++)
                {
                    FloorPlan plan = _floorForm.LstFloorPlan[i];
                    string id = plan.Floor.ID.ToString();
                    if (!pinokio3DModel1.Layers.Contains(id))
                    {
                        if (i == 0)
                        {
                            pinokio3DModel1.Layers[0].Name = plan.Floor.ID.ToString();

                        }
                        else
                        {
                            pinokio3DModel1.Layers.Add(plan.Floor.ID.ToString());
                        }
                    }

                    pinokio3DModel1.AddFloor(plan, ref grids);
                }
                pinokio3DModel1.Grids = grids.ToArray();

                for (int i = 0; i < pinokio3DModel1.Grids.Length; i++)
                    pinokio3DModel1.Grids[i].Visible = true;

                #region cho 추가
                List<Entity> entitiesToRemove = new List<Entity>();
                Dictionary<string, bool> isvisible = new Dictionary<string, bool>();
                foreach (Entity en in pinokio3DModel1.Entities.ToList())
                {
                    if (en.GetType().Name.Contains("Picture"))
                    {
                        isvisible.Add(en.LayerName, en.Visible);
                        entitiesToRemove.Add(en);
                    }
                }
                foreach (Entity en in entitiesToRemove)
                {
                    pinokio3DModel1.Entities.Remove(en);
                }
                if (isvisible.Count == 0)
                {
                    foreach (FloorPlan floorPlan in _floorForm.LstFloorPlan)
                    {
                        isvisible.Add(floorPlan.Floor.ID.ToString(), true);
                    }
                }
                for (int i = 0; i < _floorForm.LstFloorPlan.Count; i++)
                {
                    if (_floorForm.LstFloorPlan[i].FloorPlanPath != "")
                    {
                        Picture pic = new Picture(pinokio3DModel1.Grids[i].Plane, _floorForm.LstFloorPlan[i].FloorWidth, _floorForm.LstFloorPlan[i].FloorDepth, new Bitmap(_floorForm.LstFloorPlan[i].FloorPlanPath));
                        pic.Selectable = false;
                        pic.LayerName = pinokio3DModel1.Layers[i].Name;
                        pic.Visible = isvisible[_floorForm.LstFloorPlan[i].Floor.ID.ToString()];
                        pinokio3DModel1.Entities.Add(pic);
                    }
                }
                #endregion
                pinokio3DModel1.Invalidate();
                pinokio3DModel1.Entities.Regen();
            }
            else
            {
                List<Grid> grids = new List<Grid>();
                pinokio3DModel1.Grids = grids.ToArray();
            }
        }

        public void ShowDetailForm(NodeReference node)
        {
            try
            {
                List<Type> totalTypes = new List<Type>();


                //디테일 폼 컨스트럭터 리플렉션 처리
                // 이름이 FormDetailProperties_XXXXX 중 XXXX가 노드이름과 같아야 자동으로 찾아짐                
                Assembly a = Assembly.LoadWithPartialName("Pinokio.Animation");
                if (a != null)
                    totalTypes.AddRange(a.GetTypes().ToList());
                a = Assembly.LoadWithPartialName("Pinokio.Animation.User");
                if (a != null)
                    totalTypes.AddRange(a.GetTypes().ToList());

                List<Type> typeList = new List<Type>();
                string nodeName = BaseUtill.GetObjName(node.GetType());
                foreach (Type t in totalTypes)
                    if (BaseUtill.IsSameBaseType(t, typeof(FormDetailProperties)))
                    {

                        if (t.Name.Split('_')[1].Equals(nodeName))
                            typeList.Add(t);
                    }

                foreach (Type t in typeList)
                {
                    ConstructorInfo publicConstructor = t.GetConstructor(new Type[] { typeof(PinokioEditorModel), typeof(NodeReference) });
                    if (publicConstructor != null)
                        publicConstructor.Invoke(new object[] { pinokio3DModel1, node });

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        //protected override void OnMouseWheel(MouseEventArgs e)
        //{

        //}

        #region 3DView Mouse + Key EVENTS

        /// <summary>
        /// 키다운 액션
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void pinokio3DModel1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ClearSelectedNode();
                SetFloorform();
                FloorPlanUpdate();
            }
            if (e.KeyCode == Keys.Escape)
            {
                _modelActionType = ModelActionType.None;
                pinokio3DModel1.ObjectManipulator.Cancel();
                ClearSelection();
                ChangeCameraPositionBySelectedReferences();
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopyNode();
            }
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteNode();
            }
            if (e.Control && e.KeyCode == Keys.X)
            {
                CutNode();
            }
            if (e.Control && e.KeyCode == Keys.Z && e.Shift == false)
            {
                Undo();
            }
            if ((e.Control && e.KeyCode == Keys.Y) || (e.Control && e.KeyCode == Keys.Z && e.Shift))
            {
                Redo();
            }
            pinokio3DModel1.Entities.Regen();
            pinokio3DModel1.Invalidate();
        }

        /// <summary>
        /// 더블클릭 액션
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pinokio3DModel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (pinokio3DModel1.ToolBar.Contains(e.Location))
                return;

            if (_modelActionType == ModelActionType.Insert && CurrentRef != null)
            {
                InsertingNode(true);
                return;
            }
            else if (_modelActionType == ModelActionType.Insert)
            {
                _modelActionType = ModelActionType.None;
                return;
            }

            int entityIndex = pinokio3DModel1.GetEntityUnderMouseCursor(e.Location);

            if (entityIndex < 0)
                return;

            ObjectReference objectReference = pinokio3DModel1.Entities[entityIndex] as ObjectReference;

            if (objectReference == null)
                return;

            if (pinokio3DModel1.Entities[entityIndex] is Picture)
                return;

            ClearSelection();

            if (objectReference is RefLink)
                return;

            string priorName = objectReference.Name;


            //            ShowDetailForm(node);
            //            tabPage_Refrash(objectReference);

            if (priorName != objectReference.Name)
            {
                // TreeList 변경
                SimNodeTreeListNode treeNode = (SimNodeTreeListNode)simNodeTreeList.FindNodeByFieldValue(simNodeTreeList.ColumnName4NodeName, objectReference.Name);
                simNodeTreeList.SelectNode(treeNode);
                simNodeTreeList.BeginInvoke(new Action(() => simNodeTreeList.Update()));
            }

            Selection(entityIndex);
            pinokio3DModel1.Entities.Regen();
            pinokio3DModel1.Invalidate();
        }

        /// <summary>
        /// 클릭한 뒤 마우스를 뗐을 때 적용되는 액션 ex) 노드를 옮길 때, 링크,라우트를 완료할때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pinokio3DModel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (pinokio3DModel1.ActionMode != actionType.None || pinokio3DModel1.ToolBar.Contains(e.Location) || pinokio3DModel1.ViewCubeIcon.Contains(e.Location))
            {
                return;
            }

            //ObjectManipulator가 열려 있을 때에만 셀렉션 하도록?
            if (_modelActionType == ModelActionType.Selecting)
            {
                Selection(pinokio3DModel1.StartPosition, e.Location);
                MouseUpMultiSelection();
            }
            else if (_modelActionType == ModelActionType.Insert && CurrentRef != null)
            {
                InsertingNode(false);

                pinokio3DModel1.Entities.Regen();
                pinokio3DModel1.Invalidate();

                return;
            }
            else if (_modelActionType == ModelActionType.Insert)
            {
                _modelActionType = ModelActionType.None;
                return;
            }

            this.propertyGridControlSimObject.Refresh();

            if (_modelActionType == ModelActionType.Selecting)
            {
                _modelActionType = ModelActionType.None;
                OpenObjectManipulator();
                pinokio3DModel1.Entities.Regen();
                pinokio3DModel1.Invalidate();
            }
            if (_modelActionType == ModelActionType.Moving)
            {
                List<NodeReference> nodeReferences;
                if (pinokio3DModel1.MouseLocationSnapToCorrdinate != MouseSnapPointBeforeMouseDown)
                {
                    Move_MouseUpMulti(pinokio3DModel1, pinokio3DModel1.MouseLocationSnapToCorrdinate, MouseSnapPointBeforeMouseDown, out nodeReferences);
                }

                if (SelectedNodeReferences.Count > 0)
                    OpenObjectManipulator();

                pinokio3DModel1.Entities.Regen();
                pinokio3DModel1.Invalidate();
                MouseUpMultiSelection();
            }
        }

        private void InsertingNode(bool isDoubleClick)
        {
            List<NodeReference> insertedRefNodes;
            bool success = false;
            if (!isDoubleClick)
                success = CurrentRef.Insert_MouseUp(pinokio3DModel1, pinokio3DModel1.MouseLocationSnapToCorrdinate, out insertedRefNodes);
            else
                success = CurrentRef.Insert_MouseDoubleUp(pinokio3DModel1, pinokio3DModel1.MouseLocationSnapToCorrdinate, out insertedRefNodes);

            List<SimNode> insertedSimNodes = insertedRefNodes.ConvertAll(x => x.Core);

            if (success && insertedRefNodes.Count > 0)
            {
                string insertedNodeType = insertedRefNodes[0].GetType().Name;
                string insertedNodeMatchType = insertedRefNodes[0].MatchingObjType;
                double insertedNodeHeight = insertedRefNodes[0].Height;
                foreach (NodeReference nodeReference in insertedRefNodes)
                {
                    if (nodeReference.Core is CoupledModel)
                        continue;

                    nodeReference.FinishAddNode(pinokio3DModel1);

                    pinokio3DModel1.AddNodeReference(nodeReference);
                    if (pinokio3DModel1.Entities.Contains(nodeReference) is false)
                        pinokio3DModel1.Entities.Add(nodeReference);

                    if (nodeReference.Core != null)
                        nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

                    if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
                        ModelManager.Instance.AddNode(nodeReference.Core);

                    if (nodeReference is RefLink)
                        simNodeTreeList.MoveNode(simNodeTreeList.FindNodeByKeyID(nodeReference.ID), simNodeTreeList.FindNodeByKeyID((nodeReference as RefLink).FromNode.ID).ParentNode);

                    if (nodeReference.Core is Equipment && !FactoryManager.Instance.Eqps.ContainsKey(nodeReference.Core.ID))
                        FactoryManager.Instance.Eqps.Add(nodeReference.Core.ID, nodeReference.Core as Equipment);
                }
                ModifyNodeTreeList(insertedSimNodes);
                if(insertedRefNodes.Count > 0 && insertedRefNodes.Where(x => x is RefTransportLine).ToList().Count >= 2)
                    AddUndo(eUndoRedoActionType.LineSeparate, insertedRefNodes, null, null);
                else
                    AddUndo(eUndoRedoActionType.Add, insertedRefNodes, null, null);

                if (insertedRefNodes.Count > 0 && insertedRefNodes.Where(x => x is RefTransportLine).ToList().Count < 2)
                    RefreshEntities();

                if (insertedRefNodes.Count <= 3 && insertedRefNodes.Where(x => x is RefTransportLine).ToList().Count < 2)
                    PrepareInsertingRefNode(insertedNodeType, insertedNodeMatchType, insertedNodeHeight, pinokio3DModel1.MouseLocationSnapToCorrdinate);
            }
            else if (success is false)
            {
                ClearSelection();

                int rowHandle = this.gridViewInsertRefNode.FocusedRowHandle;
                if (rowHandle < 0)
                    return;
                string refNodeType = "Ref" + ((InserteNodeTreeData)(this.gridViewInsertRefNode.GetRow(rowHandle))).RefType;
                string simNodeType = ((InserteNodeTreeData)(this.gridViewInsertRefNode.GetRow(rowHandle))).NodeType;
                double height = ((InserteNodeTreeData)(this.gridViewInsertRefNode.GetRow(rowHandle))).Height;
                PrepareInsertingRefNode(refNodeType, simNodeType, height, new Point3D(0, 0, 0));
            }
        }


        /// <summary>
        /// 마우스가 움직일 떄 적용되는 액션
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pinokio3DModel1_MouseMove(object sender, MouseEventArgs e)
        {
            System.Drawing.Point mouseLocation = pinokio3DModel1.MouseLocation;

            SimNodeTreeListNode floorTreeNode = (SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(pinokio3DModel1.SelectedFloorID);

            if (_floorForm.LstFloorPlan.Count != 0 && simNodeTreeList.Nodes.Count != 0)
                pinokio3DModel1.MouseLocationSnapToCorrdinate = pinokio3DModel1.MouseLocationSnapToCorrdinate + new Point3D(0, 0, ((Floor)floorTreeNode.SimNode).FloorBottom);

            if (pinokio3DModel1.ToolBar.Contains(mouseLocation) || pinokio3DModel1.ViewCubeIcon.Contains(mouseLocation))
                return;

            if (ModifierKeys == Keys.Shift && _modelActionType == ModelActionType.Insert && LastClickedPoint != null)
            {
                int xVar = (int)Math.Abs(pinokio3DModel1.MouseLocationSnapToCorrdinate.X - LastClickedPoint.X);
                int yVar = (int)Math.Abs(pinokio3DModel1.MouseLocationSnapToCorrdinate.Y - LastClickedPoint.Y);

                Point3D nowPoint = pinokio3DModel1.MouseLocationSnapToCorrdinate;

                if (xVar >= yVar)
                    nowPoint.Y = LastClickedPoint.Y;
                else
                    nowPoint.X = LastClickedPoint.X;

                pinokio3DModel1.MouseLocationSnapToCorrdinate = nowPoint;
                //                return;
            }
            if (_floorForm.LstFloorPlan.Count == 0)
            {
                //FloorSetup();
            }
            if (_modelActionType == ModelActionType.Moving)
            {
                Move_MouseMoveMultl(pinokio3DModel1, pinokio3DModel1.MouseLocationSnapToCorrdinate, MouseSnapPointBeforeMouseMove);
                pinokio3DModel1.ObjectManipulator.Cancel();
            }
            else if (_modelActionType == ModelActionType.Selecting)
            {

            }
            else if (_modelActionType == ModelActionType.Insert && CurrentRef != null)
            {
                CurrentRef.Insert_MouseMove(pinokio3DModel1, pinokio3DModel1.MouseLocationSnapToCorrdinate, MouseSnapPointBeforeMouseMove);
            }
            MouseSnapPointBeforeMouseMove = pinokio3DModel1.MouseLocationSnapToCorrdinate;
        }


        /// <summary>
        /// 마우스를 클릭하자 마자 발생하는 액션 ex) 이동을 시작할 때 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pinokio3DModel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            System.Drawing.Point mouseLocation = pinokio3DModel1.MouseLocation;

            if (pinokio3DModel1.ActionMode != actionType.None || pinokio3DModel1.ToolBar.Contains(mouseLocation) || pinokio3DModel1.ViewCubeIcon.Contains(mouseLocation))
                return;
            foreach (devDept.Eyeshot.ToolBarButton toolBar in pinokio3DModel1.ToolBar.Buttons)
            {
                if (toolBar.Pushed)
                {
                    _modelActionType = ModelActionType.None;
                    return;
                }
            }

            // 1. Entity가 있을 경우,
            //  a. 화살표 위
            //  b. 윤곽선  위

            // 2. Entity가 없을 경우,
            //  a. 화살표 위
            //  b. 윤곽선  위
            if (_modelActionType == ModelActionType.Insert)
            {
                if (CurrentRef != null)
                    CurrentRef.Insert_MouseDown(pinokio3DModel1, pinokio3DModel1.MouseLocationSnapToCorrdinate);
                LastClickedPoint = pinokio3DModel1.MouseLocationSnapToCorrdinate;
            }
            else
            {
                MouseSnapPointBeforeMouseDown = pinokio3DModel1.MouseLocationSnapToCorrdinate;

                int entityIndexInMousePos;
                if (objectManipulatorEditing == ObjectManipulator.actionType.None)
                {
                    if (ExistSelectedEntity(e, out entityIndexInMousePos))
                    {
                        if (SimEngine.Instance.EngineState != ENGINE_STATE.RUNNING
                            && SimEngine.Instance.EngineState != ENGINE_STATE.PAUSE)
                        {
                            _modelActionType = ModelActionType.Moving;
                            if (SelectedNodeReferences.Count == 1 && SelectedNodeReferences.First() is RefTransportLine)
                            {
                                RefTransportLine refLine = SelectedNodeReferences.First() as RefTransportLine;
                                SelectedNodeReferences.Add(refLine.StartStation);
                                refLine.StartStation.Selected = true;
                                SelectedNodeReferences.Add(refLine.EndStation);
                                refLine.EndStation.Selected = true;
                            }
                            else if (SelectedNodeReferences.Count > 0)
                            {
                                foreach(NodeReference refnode in SelectedNodeReferences.ToArray())
                                {
                                    if(refnode is RefTransportPoint)
                                    {
                                        RefTransportPoint refPoint = refnode as RefTransportPoint;
                                        foreach (RefTransportLine inLine in refPoint.InLines)
                                        {
                                            if (inLine is RefCurvedTransportLine)
                                            {
                                                if (!SelectedNodeReferences.Contains(inLine))
                                                {
                                                    SelectedNodeReferences.Add(inLine);
                                                    inLine.Selected = true;
                                                }

                                                if (!SelectedNodeReferences.Contains(inLine.StartStation))
                                                {
                                                    SelectedNodeReferences.Add(inLine.StartStation);
                                                    inLine.StartStation.Selected = true;
                                                }
                                                if (!SelectedNodeReferences.Contains(inLine.EndStation))
                                                {
                                                    SelectedNodeReferences.Add(inLine.EndStation);
                                                    inLine.EndStation.Selected = true;
                                                }
                                            }
                                        }

                                        foreach (RefTransportLine outLine in refPoint.OutLines)
                                        {
                                            if (outLine is RefCurvedTransportLine)
                                            {
                                                if (!SelectedNodeReferences.Contains(outLine))
                                                {
                                                    SelectedNodeReferences.Add(outLine);
                                                    outLine.Selected = true;
                                                }

                                                if (!SelectedNodeReferences.Contains(outLine.StartStation))
                                                {
                                                    SelectedNodeReferences.Add(outLine.StartStation);
                                                    outLine.StartStation.Selected = true;
                                                }

                                                if (!SelectedNodeReferences.Contains(outLine.EndStation))
                                                {
                                                    SelectedNodeReferences.Add(outLine.EndStation);
                                                    outLine.EndStation.Selected = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                            _modelActionType = ModelActionType.None;

                    }
                    else
                    {
                        if (SimEngine.Instance.EngineState != ENGINE_STATE.RUNNING
                            && SimEngine.Instance.EngineState != ENGINE_STATE.PAUSE)
                            _modelActionType = ModelActionType.Selecting;
                        else
                            _modelActionType = ModelActionType.None;
                    }
                }
                pinokio3DModel1.StartPosition = e.Location;
            }
        }

        private bool ExistSelectedEntity(MouseEventArgs e, out int entityIndex)
        {
            entityIndex = -1;
            try
            {
                pinokio3DModel1.Entities.CloseCurrentBlockReference();
                entityIndex = pinokio3DModel1.GetEntityUnderMouseCursor(e.Location, true);
                int[] indexs = pinokio3DModel1.GetAllCrossingEntities(new System.Drawing.Rectangle(e.Location.X - 1, e.Location.Y - 1, 2, 2));
                if (entityIndex < 0)
                {
                    return false;
                }
                else
                {
                    if ((SelectedNodeReferences.Count <= 1
                    || (SelectedNodeReferences.Count == 3 && (SelectedNodeReferences[0] is RefTransportLine || SelectedNodeReferences[1] is RefTransportLine || SelectedNodeReferences[2] is RefTransportLine)))
                    && SelectedEntityReferences.Count <= 1)
                    {
                        ClearSelection();
                        SelectedNodeReferences.Clear();
                        SelectedEntityReferences.Clear();
                        pinokio3DModel1.Entities.ClearSelection();

                        Selection(entityIndex);
                        MouseUpMultiSelection();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
                return false;
            }
        }

        public void CheangeSelectedSimObject4PropertyGrid(SimObj obj)
        {
            propertyGridControlSimObject.SelectedObject = obj;
            propertyGridControlSimObject.Refresh();
        }

        #endregion

        private void Move_MouseMoveMultl(PinokioBaseModel model, Point3D moveTo, Point3D moveFrom)
        {
            SelectedNodeReferences = SelectedNodeReferences.OrderBy(x => x.Core.LoadLevel).ToList();
            foreach (NodeReference node in SelectedNodeReferences)
            {
                try
                {
                    pinokio3DModel1.NodeReferenceByID[node.ID].Move_MouseMove(model, moveTo, moveFrom);
                }
                catch (Exception ex)
                {
                    ErrorLogger.SaveLog(ex);
                }
            }

            pinokio3DModel1.Entities.Regen();
            pinokio3DModel1.Invalidate();
        }
        private bool checkPosibilityMoveLine(NodeReference node)
        {
            foreach (RefVehicle vehicle in ((RefTransportLine)node).Vehicles)
            {
                if (((TransportLine)node.Core).Length < ((Vehicle)vehicle.Core).StartPos)
                    return false;
            }

            foreach (RefLineComponent lineComponent in ((RefTransportLine)node).LineComponents)
            {
                if (lineComponent is RefLineStation)
                {
                    if (((TransportLine)node.Core).Length < ((LineStation)lineComponent.Core).Length)
                        return false;
                }
            }
            return true;
        }

        private bool checkPossibilityMove()
        {
            foreach (NodeReference node in SelectedNodeReferences)
            {
                if (node is RefTransportPoint)
                {
                    foreach (TransportLine inLine in ((TransportPoint)node.Core).InLines)
                    {
                        if (!checkPosibilityMoveLine(pinokio3DModel1.NodeReferenceByID[inLine.ID]))
                            return false;
                    }
                    foreach (TransportLine outLine in ((TransportPoint)node.Core).OutLines)
                    {
                        if (!checkPosibilityMoveLine(pinokio3DModel1.NodeReferenceByID[outLine.ID]))
                            return false;
                    }
                }
            }

            return true;
        }
        private void Move_MouseUpMulti(PinokioBaseModel model, Point3D moveTo, Point3D moveFrom, out List<NodeReference> nodeReferences)
        {
            nodeReferences = new List<NodeReference>();
            List<int> nodeIndex = new List<int>();
            List<Point3D> priorValues = new List<Point3D>();
            List<Point3D> transferValues = new List<Point3D>();
            try
            {
                bool isMerged = false;
                foreach (NodeReference node in SelectedNodeReferences)
                {
                    priorValues.Add(moveFrom);
                    transferValues.Add(moveTo);

                    if (SelectedNodeReferences.Count == 1
                        || (SelectedNodeReferences.Count > 1 && !(node is RefVehicle) && !(node is RefLineComponent)))
                    {
                        var convertToGridLoaction = model.WorldToScreen(new Point3D(Convert.ToInt32(node.CurrentPoint.X), Convert.ToInt32(node.CurrentPoint.Y)));
                        var underNode = model.NodeReferenceByID.Values.ToList().FirstOrDefault(x => Math.Abs(x.Core.PosVec3.X - node.CurrentPoint.X) < 100 && Math.Abs(x.Core.PosVec3.Y - node.CurrentPoint.Y) < 100 && x.Name != node.Name && x is RefTransportPoint);

                        if (SelectedNodeReferences.Count == 1 && node is RefTransportPoint && underNode != null && underNode is RefTransportPoint)
                        {
                            List<NodeReference> mergedNodes = new List<NodeReference>();
                            mergedNodes.Add(node);
                            mergedNodes.Add(underNode);
                            AddUndo(eUndoRedoActionType.PointMerge, mergedNodes, null, priorValues, transferValues);
                            RefTransportPoint refMergingPoint = node as RefTransportPoint;
                            refMergingPoint.Move_MouseMove(this.pinokio3DModel1, priorValues[0], transferValues[0]);
                            refMergingPoint.Move_MouseUp(this.pinokio3DModel1, refMergingPoint.CurrentPoint, refMergingPoint.CurrentPoint);
                            MergeLinePoints(model, refMergingPoint, underNode as RefTransportPoint);
                            isMerged = true;
                            break;
                        }
                        else
                            node.Move_MouseUp(model, moveTo, moveFrom);
                    }

                    ModifyTreeViewByText(pinokio3DModel1.NodeReferenceByID[node.ID]);

                    nodeReferences.Add(pinokio3DModel1.NodeReferenceByID[node.ID]);
                }

                if (isMerged == false && SelectedNodeReferences.Count > 0)
                {
                    AddUndo(eUndoRedoActionType.Move, SelectedNodeReferences, null, priorValues, transferValues);
                }

                foreach (NodeReference node in SelectedNodeReferences.ToList())
                {
                    if (node.Selected is false)
                        SelectedNodeReferences.Remove(node);
                }

                if (!checkPossibilityMove())
                {
                    MessageBox.Show("수정된 Line의 길이가 LineComponent 위치보다 짧습니다.");
                    Undo();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private bool MergeLinePoints(PinokioBaseModel model, RefTransportPoint refMergingPoint, RefTransportPoint refUnderPoint)
        {
            if (refUnderPoint != null)
            {
                TransportPoint mergingPoint = (TransportPoint)refMergingPoint.Core;
                RefTransportPoint currentRefUnderPoint = (RefTransportPoint)model.NodeReferenceByID[refUnderPoint.ID];
                foreach (TransportLine inLine in ((TransportPoint)refMergingPoint.Core).InLines)
                {
                    if(inLine.EndPoint == mergingPoint)
                        ((RefTransportLine)model.NodeReferenceByID[inLine.ID]).ModifyConveyor(model, (RefTransportPoint)model.NodeReferenceByID[inLine.StartPoint.ID], currentRefUnderPoint);
                }
                foreach (TransportLine outLine in ((TransportPoint)refMergingPoint.Core).OutLines)
                {
                    if (outLine.StartPoint == mergingPoint)
                        ((RefTransportLine)model.NodeReferenceByID[outLine.ID]).ModifyConveyor(model, currentRefUnderPoint, (RefTransportPoint)model.NodeReferenceByID[outLine.EndPoint.ID]);
                }

                List<NodeReference> node2Delete = new List<NodeReference>();
                node2Delete.Add(refMergingPoint);
                for (int j = 0; j < refMergingPoint.ToLinked.Count; j++)
                {
                    node2Delete.Add(refMergingPoint.ToLinked[j]);
                    refMergingPoint.ToLinked[j].ToNode.FromLinked.Remove(refMergingPoint.ToLinked[j]);
                    refMergingPoint.ToLinked.Remove(refMergingPoint.ToLinked[j]);
                }

                for (int j = 0; j < refMergingPoint.FromLinked.Count; j++)
                {
                    node2Delete.Add(refMergingPoint.FromLinked[j]);
                    refMergingPoint.FromLinked[j].FromNode.ToLinked.Remove(refMergingPoint.FromLinked[j]);
                    refMergingPoint.FromLinked.Remove(refMergingPoint.FromLinked[j]);
                }

                List<uint> tempIDs = new List<uint>();
                foreach (NodeReference delNode in node2Delete)
                {
                    if (tempIDs.Contains(delNode.ID))
                        continue;
                    else
                        tempIDs.Add(delNode.ID);
                }

                node2Delete.Clear();
                foreach (uint ID in tempIDs)
                {
                    if (pinokio3DModel1.NodeReferenceByID.Keys.Contains(ID))
                        node2Delete.Add(pinokio3DModel1.NodeReferenceByID[ID]);
                }

                node2Delete = node2Delete.OrderBy(x => x.LoadLevel).ToList();

                DeleteNodeReference(node2Delete);    // Use Delete Function already exiest

                if (SelectedFloorClear())
                    CloseObjectManipulator();

                ClearSelection();
                FinishAddNode();
                CloseObjectManipulator();
                pinokio3DModel1.Entities.Regen();
                pinokio3DModel1.Invalidate();

                return true;
            }
            return false;
        }

        private bool SplitLinePoints(PinokioBaseModel model, RefTransportPoint refMergingPoint, RefTransportPoint refUnderPoint)
        {
            if (refUnderPoint != null)
            {
                TransportPoint mergingPoint = (TransportPoint)refMergingPoint.Core;

                RefTransportLine[] inLines = refMergingPoint.InLines.ToArray();
                RefTransportLine[] outLines = refMergingPoint.OutLines.ToArray();

                refMergingPoint.InLines.Clear();
                foreach (RefTransportLine refInLine in inLines)
                {
                    ((RefTransportLine)model.NodeReferenceByID[refInLine.ID]).ModifyConveyor(model, (RefTransportPoint)model.NodeReferenceByID[refInLine.StartStation.ID], refMergingPoint);
                }
                refMergingPoint.OutLines.Clear();
                foreach (RefTransportLine refOutLine in outLines)
                {
                    ((RefTransportLine)model.NodeReferenceByID[refOutLine.ID]).ModifyConveyor(model, refMergingPoint, (RefTransportPoint)model.NodeReferenceByID[refOutLine.EndStation.ID]);
                }

                if (SelectedFloorClear())
                    CloseObjectManipulator();

                ClearSelection();
                FinishAddNode();
                CloseObjectManipulator();
                pinokio3DModel1.Entities.Regen();
                pinokio3DModel1.Invalidate();

                return true;
            }
            return false;
        }

        private void Rotate_MouseUpMulti()
        {
            try
            {
                foreach (NodeReference node in SelectedNodeReferences)
                {
                    if (node.Core != null)
                    {
                        Transformation rT = node.Transformation.GetTransformationForNormals();
                        node.Core.RotateMatrix = rT.Matrix;
                        double radian = PVector2.AngleRadian(new PVector2(0, 1), new PVector2(rT[1, 0], rT[1, 1]));
                        node.Core.AngleInRadians = radian;
                        node.Core.PosVec3 = new Geometry.PVector3(node.Transformation[0, 3], node.Transformation[1, 3], node.Transformation[2, 3]);

                        if (node.Core is TransportLine)
                            ((TransportLine)node.Core).Direction = PVector3.Direction(((TransportLine)node.Core).StartPoint.PosVec3, ((TransportLine)node.Core).EndPoint.PosVec3);
                        if (node.Core is ICurvedLine)
                            ((ICurvedLine)node.Core).StartDegree = radian * 180 / Math.PI;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void MouseDown_Move(MouseEventArgs e)
        {
            EntityIndex = pinokio3DModel1.GetEntityUnderMouseCursor(e.Location);

            if (EntityIndex < 0)
                return;

            if (!(pinokio3DModel1.Entities[EntityIndex] is NodeReference))
                return;
            System.Drawing.Point mouseLocation = pinokio3DModel1.PointToClient(Cursor.Position);

            CurrentRef = pinokio3DModel1.Entities[EntityIndex] as NodeReference;

            CurrentRef.Move_MouseDown(pinokio3DModel1, pinokio3DModel1.MouseLocationSnapToCorrdinate);

            if (SelectedNodeReferences.Count > 0)
            {
                List<Point3D> pts = new List<Point3D>();
                foreach (NodeReference node in SelectedNodeReferences)
                    pts.Add(node.InsertionPoint);

                AddUndo(eUndoRedoActionType.Move, SelectedNodeReferences, null, pts);
            }
            else
            {
                AddUndo(eUndoRedoActionType.Move, new List<NodeReference>() { CurrentRef }, null, CurrentRef.InsertionPoint);
            }

            _modelActionType = ModelActionType.Moving;
        }
        private void ModifyNodeTreeList(List<SimNode> insertedNodes)
        {
            //업데이트 일시 정지
            simNodeTreeList.BeginUpdate();
            try
            {
                SimNodeTreeListNode floorTreeNode = null;
                //SimNodeTreeListNode parentTreeNode;

                var parentNodesDic = new Dictionary<uint, SimNodeTreeListNode>();

                foreach (SimNode simNode in insertedNodes)
                {
                    if (!parentNodesDic.TryGetValue(Convert.ToUInt32(simNode.ParentNodeID), out SimNodeTreeListNode parentTreeNode))
                    {
                        parentTreeNode = simNodeTreeList.FindNodeByKeyID(Convert.ToUInt32(simNode.ParentNodeID)) as SimNodeTreeListNode;

                        if (parentTreeNode != null)
                        {
                            parentNodesDic[Convert.ToUInt32(simNode.ParentNodeID)] = parentTreeNode;
                        }
                    }

                    if (parentTreeNode == null)
                    {
                        if (ModelManager.Instance.SimNodes.ContainsKey(simNode.ParentNodeID))
                        {
                            SimNode parentNode = ModelManager.Instance.SimNodes[simNode.ParentNodeID];
                            List<SimNode> parentNodeList = new List<SimNode>();
                            parentNodeList.Add(parentNode);
                            ModifyNodeTreeList(parentNodeList);
                            parentTreeNode = simNodeTreeList.FindNodeByKeyID(Convert.ToUInt32(simNode.ParentNodeID)) as SimNodeTreeListNode;
                            floorTreeNode = parentTreeNode.RootNode as SimNodeTreeListNode;
                        }
                        else
                        {
                            floorTreeNode = (SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(pinokio3DModel1.SelectedFloorID);
                            if (floorTreeNode != null)
                            {
                                parentTreeNode = floorTreeNode;
                                simNode.ParentNode = parentTreeNode.SimNode as CoupledModel;
                                if (simNode is TXNode)
                                    ((TXNode)simNode).UpdateFloor();
                            }
                        }
                    }
                    else
                        floorTreeNode = parentTreeNode.RootNode as SimNodeTreeListNode;

                    NodeReference refNode = null;
                    SimNodeTreeListNode addedSimTreeNode;

                    if (pinokio3DModel1.NodeReferenceByID.ContainsKey(simNode.ID))
                    {
                        refNode = pinokio3DModel1.NodeReferenceByID[simNode.ID];
                        refNode.LayerName = floorTreeNode.SimNode.ID.ToString();

                        addedSimTreeNode = (SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(refNode.ID);
                        if (addedSimTreeNode == null)
                            addedSimTreeNode = (SimNodeTreeListNode)simNodeTreeList.AppendNode(new object[] { simNode.ID, simNode.Name, simNode.GetType().Name, refNode.GetType().Name.Remove(0, 3) }, parentTreeNode);
                    }
                    else
                        addedSimTreeNode = (SimNodeTreeListNode)simNodeTreeList.AppendNode(new object[] { simNode.ID, simNode.Name, simNode.GetType().Name, null }, parentTreeNode);

                    if (!(simNode is Floor)
                        && parentTreeNode != null
                        && (simNode.ParentNode == null || simNode.ParentNode != parentTreeNode.SimNode))
                    {
                        simNode.ParentNode = parentTreeNode.SimNode as CoupledModel;
                        if (simNode is TXNode)
                            ((TXNode)simNode).UpdateFloor();
                    }

                    addedSimTreeNode.Checked = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            finally
            {
                //UI 업데이트
                simNodeTreeList.EndUpdate();
            }
        }

        private void RefreshEntities()
        {
            try
            {
                CurrentRef = null;
                _modelActionType = ModelActionType.None;
                pinokio3DModel1.Entities.Regen();
                pinokio3DModel1.Invalidate();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void SettingFloor()
        {
            try
            {
                _floorForm.LstFloorplanHasChildernIds = new List<uint>();
                for (int i = 0; i < simNodeTreeList.Nodes.Count; i++)
                {
                    if (simNodeTreeList.Nodes[i].HasChildren)
                    {
                        _floorForm.LstFloorplanHasChildernIds.Add(((SimNodeTreeListNode)simNodeTreeList.Nodes[i]).SimNode.ID);
                    }
                }
                _floorForm.CopiedDicFloorPlan = new Dictionary<uint, FormFloorSelect.CopiedFloorPlan>();
                foreach (FloorPlan floorPlan in _floorForm.LstFloorPlan)
                {
                    FormFloorSelect.CopiedFloorPlan copiedFloorPlan = new FormFloorSelect.CopiedFloorPlan(floorPlan.Floor.ID, floorPlan.FloorNum, floorPlan.FloorName, floorPlan.FloorWidth, floorPlan.FloorDepth, floorPlan.FloorBottom);
                    _floorForm.CopiedDicFloorPlan.Add(floorPlan.Floor.ID, copiedFloorPlan);
                }
                _floorForm.LstRemovedPlanIds = new List<uint>();
                _floorForm.LstUpdatedPlanIds = new List<uint>();
                _floorForm.LstRemovedFloorPlan = new List<FloorPlan>();
                _floorForm.IsOkay = false;
                _floorForm.IsChanged = false;

                if (_isInitialize == false)
                    _floorForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        #region Mouse Down 시 조건문
        private bool IsExistEntity(MouseEventArgs e, out int entityIndex)
        {
            entityIndex = -1;
            try
            {
                entityIndex = pinokio3DModel1.GetEntityUnderMouseCursor(e.Location, false);
                if (entityIndex < 0)
                {
                    return false;
                }
                else return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return false;
        }

        #endregion

        #region RIBBON BUTTON Click EVENTS

        private void RB_BTN_NEW_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("LAYOUT WILL BE REMOVED!", "WARNING", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                MES mes = FactoryManager.Instance.MES;
                MCS mcs = FactoryManager.Instance.MCS;
                NewItem(false);
                ModelManager.Instance.InitializeMESnMCS(mes, mcs);
                ModifyNodeTreeList(ModelManager.Instance.SimNodes.Values.ToList());
                _isInitialize = true;
                FloorSetup();
                InitWarmUpPeriod();
                _isInitialize = false;
                bbiProductionReport.Enabled = false;
                bbiAMHSReport.Enabled = false;
                bbiLoadAMHSReport.Enabled = false;
                bbiLoadProductionReport.Enabled = false;
            }
        }

        private void RB_BTN_LOAD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            LoadLayout();
            InitWarmUpPeriod();
            stopwatch.Stop();
            Console.WriteLine("Load: " + stopwatch.ElapsedMilliseconds);
        }

        private void RB_BTN_SAVE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            {
                string filePath = "";
                bool isOK = false;
                if (AccessDB.Instance.DBPath == "")
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.Filter = "Access File (*.accdb)|*.accdb|Xml File (*.xml)|*.xml";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        filePath = dlg.FileName;
                        isOK = true;
                    }
                }
                else
                {
                    filePath = pinokio3DModel1.InitialFilePath;
                    isOK = true;
                }

                if (isOK)
                {
                    SplashScreenManager.ShowForm(typeof(WaitFormSplash));
                    List<SimNodeIntegrityLog> simNodeLogList = ModelManager.Instance.CheckIntegrities();
                    List<FactoryIntegrityLog> factoryLogList = FactoryManager.Instance.CheckIntegrities();

                    if (simNodeLogList.Count > 0 || factoryLogList.Count > 0)
                    {
                        IntegrityTestForm integrityForm = new IntegrityTestForm(this);
                        integrityForm.SetSimNodeLogData(simNodeLogList);
                        integrityForm.SetFactoryLogData(factoryLogList);
                        integrityForm.StartPosition = FormStartPosition.CenterParent;
                        integrityForm.ShowDialog();
                        if (integrityForm.DialogResult == DialogResult.Cancel)
                        {
                            SplashScreenManager.CloseForm();
                            return;
                        }
                    }

                    if (pinokio3DModel1.SaveModel(filePath, _floorForm.LstFloorPlan))
                    {
                        SplashScreenManager.CloseForm();
                        FlyoutDialog.Show(Application.OpenForms[0], new FOAInfo("Success"));
                    }
                    else
                    {
                        SplashScreenManager.CloseForm();
                        FlyoutDialog.Show(Application.OpenForms[0], new FOAInfo("Failed."));
                    }
                }
            }
        }
        private void RB_BTN_SAVE_AS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Access File (*.accdb)|*.accdb|Xml File (*.xml)|*.xml";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string filePath = dlg.FileName;
                SplashScreenManager.ShowForm(typeof(WaitFormSplash));
                List<SimNodeIntegrityLog> simNodeLogList = ModelManager.Instance.CheckIntegrities();
                List<FactoryIntegrityLog> factoryLogList = FactoryManager.Instance.CheckIntegrities();

                if (simNodeLogList.Count > 0 || factoryLogList.Count > 0)
                {
                    IntegrityTestForm integrityForm = new IntegrityTestForm(this);
                    integrityForm.SetSimNodeLogData(simNodeLogList);
                    integrityForm.SetFactoryLogData(factoryLogList);
                    integrityForm.StartPosition = FormStartPosition.CenterParent;
                    integrityForm.ShowDialog();
                    if (integrityForm.DialogResult == DialogResult.Cancel)
                    {
                        SplashScreenManager.CloseForm();
                        return;
                    }
                }

                if (pinokio3DModel1.SaveModel(filePath, _floorForm.LstFloorPlan))
                {
                    SplashScreenManager.CloseForm();
                    FlyoutDialog.Show(Application.OpenForms[0], new FOAInfo("Success"));
                }
                else
                {
                    SplashScreenManager.CloseForm();
                    FlyoutDialog.Show(Application.OpenForms[0], new FOAInfo("Failed."));
                }
            }
        }
        private void RB_BTN_FLOORSETUP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FloorSetup();
        }

        private void RB_BTN_LINK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //GernerateNode(NodeType.Link);
        }


        #endregion

        private void barButtonItemImPortModel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FormAssemblyEdit formAssemblyEdit = new FormAssemblyEdit();
                formAssemblyEdit.ShowDialog();
                //using (var importFileDialog1 = new XtraOpenFileDialog())
                //{
                //    string theFilter = "Initial Graphics Exchange Specification (*.igs; *.iges)|*.igs; *.iges|" + "STandard for the Exchange of Product (*.stp; *.step)|*.stp; *.step|" +
                //        "OBJ (*.obj)|*.obj| STL(*.stl)|*.stl ;";
                //    importFileDialog1.Filter = theFilter;
                //    importFileDialog1.Multiselect = false;
                //    importFileDialog1.AddExtension = true;
                //    importFileDialog1.CheckFileExists = true;
                //    importFileDialog1.CheckPathExists = true;

                //    if (importFileDialog1.ShowDialog() == DialogResult.OK)
                //    {
                //        ReadFileAsync rfa = GetReader(importFileDialog1.FileName);
                //        if (rfa != null)
                //        {

                //            if (pinokio3DModel1.Entities.IsOpenCurrentBlockReference)
                //                pinokio3DModel1.Entities.CloseCurrentBlockReference();
                //            this.pinokio3DModel1.Clear();
                //            this.pinokio3DModel1.StartWork(rfa);
                //            this.pinokio3DModel1.ZoomFit();
                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

        private void SimpleButtonAddCoupledModel_Click(object sender, EventArgs e)
        {
            try
            {
                if (_floorForm.LstFloorPlan.Count == 0)
                {
                    MessageBox.Show("생성 가능한 Floor가 없습니다.");
                    FloorSetup();
                    return;
                }
                string coupledModelName = gridViewInsertCoupledModel.GetFocusedRowCellValue(gridViewInsertCoupledModel.Columns.ElementAt(1)).ToString();

                Type nodeType = GetSimulationType(coupledModelName);

                SimNodeTreeListNode floorTreeNode = (SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(pinokio3DModel1.SelectedFloorID);
                ConstructorInfo[] cs = nodeType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
                SimNode createdNode = null;

                foreach (ConstructorInfo c in cs)
                {
                    if (c.GetParameters().Length == 0)
                    {
                        dynamic simNode = c.Invoke(new object[] { });
                        createdNode = simNode;
                        ModelManager.Instance.AddNode(simNode);
                        SimNodeTreeListNode treeNode = (SimNodeTreeListNode)simNodeTreeList.AppendNode(new object[] { simNode.ID, simNode.Name, nodeType.Name, null }, floorTreeNode);
                        createdNode.ParentNode = floorTreeNode.SimNode as CoupledModel;
                        if (createdNode is TXNode)
                            ((TXNode)createdNode).UpdateFloor();

                        treeNode.Checked = true;
                    }
                }
                floorTreeNode.ExpandAll();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public Type GetSimulationType(string simulationClassName)
        {
            try
            {
                List<Type> totalTypes = TypeDefine.SimObjTypes;
                foreach (Type t in totalTypes)
                {
                    if (t.Name.Equals(simulationClassName))
                    {
                        return t;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return null;
        }

        private void simpleButtonRemoveTreeNode_Click(object sender, EventArgs e)
        {
            ClearSelectedNode();
            SetFloorform();
            FloorPlanUpdate();
        }

        private void propertyGridControlSimNode_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (ModelManager.Instance.SimNodesByName.ContainsKey(e.Value.ToString()))
            {
                //                propertyGridControlSimNode.CancelUpdate();
                MessageBox.Show("동일한 이름의 Node를 생성할 수 없습니다.");
            }
        }

        private void propertyGridControlSimNode_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                if (propertyGridControlSimObject.SelectedObject is SimNode)
                {
                    SimNode node = (SimNode)propertyGridControlSimObject.SelectedObject;
                    string fieldName = e.Row.Properties.FieldName;
                    PropertyInfo propertyInfo = node.GetType().GetRuntimeProperty(fieldName);
                    if (propertyInfo == null)
                        return;
                    object value = propertyInfo.GetValue(propertyGridControlSimObject.SelectedObject);
                    object oldValue = propertyGridControlSimObject.ActiveEditor.OldEditValue;

                    if (value.GetType() == oldValue.GetType())
                    {
                        if (fieldName == "Name")
                        {
                            if (ModelManager.Instance.SimNodesByName.ContainsKey(e.Value.ToString()))
                            {
                                propertyGridControlSimObject.CancelUpdate();
                                MessageBox.Show("동일한 이름의 Node를 생성할 수 없습니다.");
                            }
                            else
                            {
                                if (pinokio3DModel1.NodeReferenceByID[node.ID].Label != null)
                                {
                                    pinokio3DModel1.NodeReferenceByID[node.ID].Label.Text = (string)value;
                                    pinokio3DModel1.Labels.Regen();
                                }

                                SimNodeTreeListNode treeNode = (SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(node.ID);
                                treeNode[simNodeTreeList.ColumnName4NodeName] = node.Name;
                                AddUndo(eUndoRedoActionType.SimParameterModify, new List<uint>() { node.ID, }, propertyInfo.Name, oldValue, value);
                            }
                        }
                        else if (propertyGridControlSimObject.SelectedObject is TransportLine && fieldName == "IsTwoWay")
                        {
                            AddUndo(eUndoRedoActionType.SimParameterModify, new List<uint>() { node.ID, }, propertyInfo.Name, oldValue, value);
                            pinokio3DModel1.NodeReferenceByID[node.ID].Regen(0.1);
                        }
                        else
                        {
                            AddUndo(eUndoRedoActionType.SimParameterModify, new List<uint>() { node.ID, }, propertyInfo.Name, oldValue, value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

        private void contextMenuStripEditNode_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ContextMenuStrip menu = sender as ContextMenuStrip;
            Point pointShown = menu.Bounds.Location;

            //TODO devDept: Now you need to pass through the Model.ActiveViewport property to get/set the active viewport grid.
            DevExpress.XtraGrid.GridControl gc = menu.SourceControl as DevExpress.XtraGrid.GridControl;
            if (gc != null)
            {
                Point gridClientPoint = gc.PointToClient(pointShown);
                //TODO devDept: Now you need to pass through the Model.ActiveViewport property to get/set the active viewport grid.
                DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)gc.GetViewAt(gridClientPoint);

                if (view != null)
                {
                    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = view.CalcHitInfo(gridClientPoint);
                    //TODO devDept: Now you need to pass through the Model.ActiveViewport property to get/set the active viewport grid.

                    int rowHandle = hitInfo.RowHandle;
                    var column = hitInfo.Column;
                    if (e.ClickedItem.Text == "Add")
                    {

                    }
                    else if (e.ClickedItem.Text == "Delete")
                    {

                    }


                    //...
                }
            }
        }

        private void gridViewInsertRefNode_Click(object sender, EventArgs e)
        {
            try
            {
                if (_floorForm.LstFloorPlan.Count == 0)
                {
                    MessageBox.Show("생성 가능한 Floor가 없습니다.");
                    FloorSetup();
                    return;
                }
                DXMouseEventArgs dXMouseEventArgs = (DXMouseEventArgs)e;
                if (dXMouseEventArgs.Button != MouseButtons.Right)
                    return;

                contextMenuStripEditNode.Show(gridControlInsertRefNode, dXMouseEventArgs.Location);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        private void FloorPlanUpdate()
        {
            InitializeFloorPlan(_floorForm.LstFloorPlan);

            if (_floorForm.LstFloorPlan != null)
            {
                foreach (FloorPlan plan in _floorForm.LstFloorPlan)
                {
                    if (plan.Floor != null)
                    {
                        if ((SimNodeTreeListNode)simNodeTreeList.FindNodeByFieldValue(simNodeTreeList.ColumnName4NodeID, plan.Floor.NodeID) == null)
                        {
                            SimNodeTreeListNode cmTreeNode = (SimNodeTreeListNode)simNodeTreeList.AppendNode(new object[] { plan.Floor.ID, plan.Floor.Name, plan.Floor.GetType().Name, null }, null);
                            cmTreeNode.Checked = true;
                        }
                    }
                }
            }

            if (_floorForm.LstUpdatedPlanIds != null)
            {
                foreach (uint ID in _floorForm.LstUpdatedPlanIds)
                {
                    if (simNodeTreeList.FindNodeByKeyID(ID) != null)
                    {
                        simNodeTreeList.FindNodeByKeyID(ID).SetValue(simNodeTreeList.ColumnName4NodeName, ((SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(ID)).SimNode.Name);
                        Move2NewFloor(((SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(ID)).SimNode);
                    }
                }
                _floorForm.LstUpdatedPlanIds.Clear();
            }

            if (_floorForm.LstRemovedPlanIds != null)
            {
                foreach (uint ID in _floorForm.LstRemovedPlanIds)
                {
                    if (simNodeTreeList.FindNodeByKeyID(ID) != null)
                    {
                        if (pinokio3DModel1.Layers.Count > 1)
                            pinokio3DModel1.Layers.Remove(((Floor)(simNodeTreeList.FindNodeByKeyID(ID) as SimNodeTreeListNode).SimNode).ID.ToString());
                        simNodeTreeList.FindNodeByKeyID(ID).Remove();
                    }
                    ModelManager.Instance.DicCoupledModel.Remove(ID);
                    ModelManager.Instance.RemoveNode(ID);
                }
                _floorForm.LstRemovedPlanIds.Clear();
            }

            _floorForm.ResetFloorPlan(_floorForm.LstFloorPlan);
            _floorForm.RefreshGridView1();
            propertyGridControlSimObject.Refresh();
            simNodeTreeList.ClearSelection();
            if (_floorForm.LstFloorPlan.Count == 0)
                return;

            BeginInvoke(new Action(() => pinokio3DModel1.Entities.Regen()));
            BeginInvoke(new Action(() => Invalidate()));
        }

        private void InitializeTreeAction()
        {
            simNodeTreeList.AfterDropNode += SimNodesTreeList_AfterDropNode;
            simNodeTreeList.MouseClick += SimNodesTreeList_MouseClick;
            simNodeTreeList.BeforeDropNode += SimNodesTreeList_BeforeDropNode;
            simNodeTreeList.BeforeDragNode += SimNodesTreeList_BeforeDragNode;
            simNodeTreeList.AfterCheckNode += SimNodesTreeList_AfterCheckNode;
            //partTreeList.MouseClick += SimEntityTreeList_MouseClick;
            //partTreeList.AfterCheckNode += SimEntityTreeList_AfterCheckNode;
        }

        private void RemoveEventBeforeSimulation()
        {
            simNodeTreeList.AfterDropNode -= SimNodesTreeList_AfterDropNode;
            simNodeTreeList.BeforeDropNode -= SimNodesTreeList_BeforeDropNode;
            simNodeTreeList.BeforeDragNode -= SimNodesTreeList_BeforeDragNode;
            //simNodeTreeList.AfterCheckNode -= SimNodesTreeList_AfterCheckNode; -- 시뮬레이션 동작 중 Visible 처리 위해 수정
            this.pinokio3DModel1.ObjectManipulator.MouseOver -= OnObjectManipulatorMouseOver;
            this.pinokio3DModel1.ObjectManipulator.MouseDown -= OnObjectManipulatorMouseDown;
            this.pinokio3DModel1.ObjectManipulator.MouseDrag -= OnObjectManipulatorMouseDrag;
            this.pinokio3DModel1.ObjectManipulator.MouseUp -= OnObjectManipulatorMouseUp;
            this.pinokio3DModel1.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.pinokio3DModel1_MouseMove);
            this.pinokio3DModel1.KeyDown -= new KeyEventHandler(this.pinokio3DModel1_KeyDown);

        }

        private void AddEventAfterSimulation()
        {
            simNodeTreeList.AfterDropNode += SimNodesTreeList_AfterDropNode;
            simNodeTreeList.BeforeDropNode += SimNodesTreeList_BeforeDropNode;
            simNodeTreeList.BeforeDragNode += SimNodesTreeList_BeforeDragNode;
            simNodeTreeList.AfterCheckNode += SimNodesTreeList_AfterCheckNode;
            this.pinokio3DModel1.ObjectManipulator.MouseOver += OnObjectManipulatorMouseOver;
            this.pinokio3DModel1.ObjectManipulator.MouseDown += OnObjectManipulatorMouseDown;
            this.pinokio3DModel1.ObjectManipulator.MouseDrag += OnObjectManipulatorMouseDrag;
            this.pinokio3DModel1.ObjectManipulator.MouseUp += OnObjectManipulatorMouseUp;
            this.pinokio3DModel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pinokio3DModel1_MouseMove);
            this.pinokio3DModel1.KeyDown += new KeyEventHandler(this.pinokio3DModel1_KeyDown);
        }

        private void ClearSelectedNode()
        {
            try
            {
                SimNodeTreeListNode focusedNode = simNodeTreeList.FocusedNode as SimNodeTreeListNode;

                List<NodeReference> node2Delete = new List<NodeReference>();
                List<SimNode> simNode2Delete = new List<SimNode>();

                for (int i = 0; i < SelectedNodeReferences.Count; i++)
                {
                    bool shouldContinue = false;

                    SimNodeTreeListNode selectedNode = simNodeTreeList.FindNodeByKeyID(SelectedNodeReferences[i].ID) as SimNodeTreeListNode;
                    if (selectedNode is null)
                        return;

                    if (selectedNode.RefNode is RefTransportLine)      // 1. TransPortLine + Its End / Start Station Delete
                    {
                        if (((TransportLine)selectedNode.SimNode).StartPoint.InLines.Count == 0 && ((TransportLine)selectedNode.SimNode).StartPoint.OutLines.Count == 1)
                            node2Delete.Add((selectedNode.RefNode as RefTransportLine).StartStation);

                        if (((TransportLine)selectedNode.SimNode).EndPoint.InLines.Count == 1 && ((TransportLine)selectedNode.SimNode).EndPoint.OutLines.Count == 0)
                            node2Delete.Add((selectedNode.RefNode as RefTransportLine).EndStation);

                        foreach (RefLineComponent refLineComponent in ((RefTransportLine)selectedNode.RefNode).LineComponents)
                            node2Delete.Add(refLineComponent);

                        if (selectedNode.RefNode is RefTransportLine)
                        {
                            foreach (RefVehicle refVehicle in ((RefTransportLine)selectedNode.RefNode).Vehicles)
                                node2Delete.Add(refVehicle);
                        }
                    }
                    else if (selectedNode.RefNode is RefTransportPoint)     // 2. TransPortStation + Its Inline / Outline Delete
                    {
                        for (int j = 0; j < ((TransportPoint)selectedNode.SimNode).InLines.Count; j++)
                        {
                            if (SelectedNodeReferences.Any(n => n.ID == (((TransportPoint)selectedNode.SimNode).InLines[j].ID)))
                            {
                                if (((TransportPoint)selectedNode.SimNode).OutLines.Count == 0 && ((TransportPoint)selectedNode.SimNode).InLines.Count == 1)
                                {
                                    node2Delete.Add(selectedNode.RefNode);
                                    shouldContinue = true;
                                    break;
                                }
                                else if (((TransportPoint)selectedNode.SimNode).OutLines.Count == 1 && ((TransportPoint)selectedNode.SimNode).InLines.Count == 1)
                                {
                                    if (((TransportPoint)selectedNode.SimNode).OutLines[0] is CraneLine)
                                    {
                                        node2Delete.Add(selectedNode.RefNode);
                                        shouldContinue = true;
                                        break;
                                    }
                                }

                            }
                            //if (!(((TransportPoint)selectedNode.SimNode).InLines[j].StartPoint.InLines.Count > 0 || ((TransportPoint)selectedNode.SimNode).InLines[j].StartPoint.OutLines.Count > 1))
                            //    node2Delete.Add(pinokio3DModel1.NodeReferenceByID[((TransportPoint)selectedNode.SimNode).InLines[j].StartPoint.ID]);

                            //node2Delete.Add(pinokio3DModel1.NodeReferenceByID[((TransportPoint)selectedNode.SimNode).InLines[j].ID]);
                        }
                        if (shouldContinue)
                            continue;
                        for (int j = 0; j < ((TransportPoint)selectedNode.SimNode).OutLines.Count; j++)
                        {
                            if (SelectedNodeReferences.Any(n => n.ID == (((TransportPoint)selectedNode.SimNode).OutLines[j].ID)))
                            {
                                if (((TransportPoint)selectedNode.SimNode).InLines.Count == 0 && ((TransportPoint)selectedNode.SimNode).OutLines.Count == 1)
                                {
                                    node2Delete.Add(selectedNode.RefNode);
                                    shouldContinue = true;
                                    break;
                                }
                            }
                            //if (!(((TransportPoint)selectedNode.SimNode).OutLines[j].EndPoint.InLines.Count > 1 || ((TransportPoint)selectedNode.SimNode).OutLines[j].EndPoint.OutLines.Count > 0))
                            //    node2Delete.Add(pinokio3DModel1.NodeReferenceByID[((TransportPoint)selectedNode.SimNode).OutLines[j].EndPoint.ID]);

                            //node2Delete.Add(pinokio3DModel1.NodeReferenceByID[((TransportPoint)selectedNode.SimNode).OutLines[j].ID]);
                        }
                        for (int j = 0; j < ((TransportPoint)selectedNode.SimNode).OutLines.Count; j++)
                        {
                            if (!SelectedNodeReferences.Any(n => n.ID == (((TransportPoint)selectedNode.SimNode).OutLines[j].ID)))
                            {
                                shouldContinue = true;
                                break;
                            }
                        }
                        for (int j = 0; j < ((TransportPoint)selectedNode.SimNode).InLines.Count; j++)
                        {
                            if (!SelectedNodeReferences.Any(n => n.ID == (((TransportPoint)selectedNode.SimNode).InLines[j].ID)))
                            {
                                shouldContinue = true;
                                break;
                            }
                        }
                        if (shouldContinue)
                            continue;
                    }

                    node2Delete.Add(selectedNode.RefNode);
                }
                for (int i = 0; i < node2Delete.Count; i++)
                {
                    for (int j = 0; j < node2Delete[i].ToLinked.Count; j++)
                    {
                        node2Delete.Add(node2Delete[i].ToLinked[j]);
                        node2Delete[i].ToLinked[j].ToNode.FromLinked.Remove(node2Delete[i].ToLinked[j]);
                        node2Delete[i].ToLinked.Remove(node2Delete[i].ToLinked[j]);
                    }

                    for (int j = 0; j < node2Delete[i].FromLinked.Count; j++)
                    {
                        node2Delete.Add(node2Delete[i].FromLinked[j]);
                        node2Delete[i].FromLinked[j].FromNode.ToLinked.Remove(node2Delete[i].FromLinked[j]);
                        node2Delete[i].FromLinked.Remove(node2Delete[i].FromLinked[j]);
                    }
                }

                List<uint> tempIDs = new List<uint>();
                foreach (NodeReference node in node2Delete)
                {
                    if (tempIDs.Contains(node.ID))
                        continue;
                    else
                        tempIDs.Add(node.ID);
                }

                node2Delete.Clear();
                foreach (uint ID in tempIDs)
                {
                    if (pinokio3DModel1.NodeReferenceByID.Keys.Contains(ID))
                        node2Delete.Add(pinokio3DModel1.NodeReferenceByID[ID]);
                }

                node2Delete = node2Delete.OrderBy(x => x.LoadLevel).ToList();

                if (node2Delete.Count != 0)
                    AddUndo(eUndoRedoActionType.Delete, node2Delete, null, null);

                DeleteNodeReference(node2Delete);    // Use Delete Function already exiest

                if (focusedNode != null && focusedNode.SimNode != null && focusedNode.SimNode is CoupledModel)    // 1. CoupledModel Delete
                {
                    SelectEveryNodes(focusedNode, ref simNode2Delete);
                }

                simNode2Delete = simNode2Delete.Distinct().ToList();
                simNode2Delete = simNode2Delete.OrderByDescending(x => x.LoadLevel).ToList();

                foreach (SimNode simNode in simNode2Delete)
                {
                    if (simNodeTreeList.FindNodeByKeyID(simNode.ID).HasChildren)
                    {
                        MessageBox.Show("하위 Node가 존재하여 삭제할 수 없습니다.");
                        continue;
                    }
                    if (isRootTreeNode(simNode))
                    {
                        if (pinokio3DModel1.Layers.Count > 1)
                            pinokio3DModel1.Layers.Remove(simNode.ID.ToString());
                        _floorForm.LstFloorPlan.Remove(GetFloorPlanTreeNode(simNode.ID));
                    }
                    ModelManager.Instance.DicCoupledModel.Remove(simNode.ID);
                    ModelManager.Instance.RemoveNode(simNode.ID);
                    simNodeTreeList.DeleteNode(simNodeTreeList.FindNodeByKeyID(simNode.ID));
                }

                if (SelectedFloorClear())
                    CloseObjectManipulator();

                ClearSelection();
                FinishAddNode();
                CloseObjectManipulator();
                pinokio3DModel1.Entities.Regen();
                pinokio3DModel1.Invalidate();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        private void ClearSelectedNode(List<NodeReference> pasteFailNodes)
        {
            try
            {
                List<NodeReference> node2Delete = pasteFailNodes;
                List<SimNode> simNode2Delete = new List<SimNode>();

                for (int i = 0; i < node2Delete.Count; i++)
                {
                    for (int j = 0; j < node2Delete[i].ToLinked.Count; j++)
                    {
                        node2Delete.Add(node2Delete[i].ToLinked[j]);
                        node2Delete[i].ToLinked[j].ToNode.FromLinked.Remove(node2Delete[i].ToLinked[j]);
                        node2Delete[i].ToLinked.Remove(node2Delete[i].ToLinked[j]);
                    }

                    for (int j = 0; j < node2Delete[i].FromLinked.Count; j++)
                    {
                        node2Delete.Add(node2Delete[i].FromLinked[j]);
                        node2Delete[i].FromLinked[j].FromNode.ToLinked.Remove(node2Delete[i].FromLinked[j]);
                        node2Delete[i].FromLinked.Remove(node2Delete[i].FromLinked[j]);
                    }
                }

                List<uint> tempIDs = new List<uint>();
                foreach (NodeReference node in node2Delete)
                {
                    if (tempIDs.Contains(node.ID))
                        continue;
                    else
                        tempIDs.Add(node.ID);
                }

                node2Delete.Clear();
                foreach (uint ID in tempIDs)
                    node2Delete.Add(pinokio3DModel1.NodeReferenceByID[ID]);

                node2Delete = node2Delete.OrderBy(x => x.LoadLevel).ToList();

                if (node2Delete.Count != 0)
                    AddUndo(eUndoRedoActionType.Delete, node2Delete, null, null);

                DeleteNodeReference(node2Delete);    // Use Delete Function already exiest

                if (SelectedFloorClear())
                    CloseObjectManipulator();

                ClearSelection();
                FinishAddNode();
                CloseObjectManipulator();
                pinokio3DModel1.Entities.Regen();
                pinokio3DModel1.Invalidate();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        private List<SimNode> GetChildCoupledModels(List<SimNode> coupledModels)
        {
            List<SimNode> tempList = new List<SimNode>();

            foreach (SimNode coupledModel in coupledModels)
            {
                for (int i = 0; i < simNodeTreeList.FindNodeByKeyID(coupledModel.ID).Nodes.Count; i++)
                    if (!coupledModels.Contains(((SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(coupledModel.ID).Nodes[i]).SimNode))
                        tempList.Add(((SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(coupledModel.ID).Nodes[i]).SimNode);
            }
            coupledModels.AddRange(tempList);
            if (tempList.Count == 0)
                return coupledModels;
            else
                return GetChildCoupledModels(coupledModels);
        }

        private void CutNodeVisible(bool OnOff)
        {
            foreach (NodeReference refNode in CopiedClipBoard)
            {
                refNode.Visible = OnOff;
            }
        }

        private List<RefLink> GetConnectedLinks(NodeReference refNode)
        {
            List<RefLink> refLinks = new List<RefLink>();

            foreach (RefLink refLink in refNode.FromLinked)
                refLinks.Add(refLink);

            foreach (RefLink refLink in refNode.ToLinked)
                refLinks.Add(refLink);

            return refLinks;
        }


        private List<SimNode> GetConnectedPointAndLine(List<SimNode> LinesOrPoints)
        {
            List<SimNode> tempList = new List<SimNode>();

            foreach (SimNode LineOrPoint in LinesOrPoints)
            {
                if (LineOrPoint is TransportLine)
                {
                    if (!LinesOrPoints.Contains(((TransportLine)LineOrPoint).StartPoint))
                        tempList.Add(((TransportLine)LineOrPoint).StartPoint);
                    if (!LinesOrPoints.Contains(((TransportLine)LineOrPoint).EndPoint))
                        tempList.Add(((TransportLine)LineOrPoint).EndPoint);

                    foreach (LineStation lineStation in ((TransportLine)LineOrPoint).LineStations)
                        if (!LinesOrPoints.Contains(lineStation))
                            tempList.Add(lineStation);

                    foreach (Sensor sensor in ((TransportLine)LineOrPoint).Sensors)
                        if (!LinesOrPoints.Contains(sensor))
                            tempList.Add(sensor);

                    if (LineOrPoint is GuidedLine)
                    {
                        foreach (Vehicle vehicle in ((GuidedLine)LineOrPoint).Vehicles)
                            if (!LinesOrPoints.Contains(vehicle))
                                tempList.Add(vehicle);
                    }
                }
                else if (LineOrPoint is TransportPoint)
                {
                    foreach (TransportLine inline in ((TransportPoint)LineOrPoint).InLines)
                    {
                        if (!LinesOrPoints.Contains(inline))
                            tempList.Add(inline);
                    }
                    foreach (TransportLine outline in ((TransportPoint)LineOrPoint).OutLines)
                    {
                        if (!LinesOrPoints.Contains(outline))
                            tempList.Add(outline);
                    }
                }
                else if (LineOrPoint is Vehicle)
                {
                    if (!LinesOrPoints.Contains(LineOrPoint))
                        tempList.Add(LineOrPoint);
                    if (!LinesOrPoints.Contains(((Vehicle)LineOrPoint).Line))
                        tempList.Add(((Vehicle)LineOrPoint).Line);
                }
                else if (LineOrPoint is Sensor)
                {
                    if (!LinesOrPoints.Contains(LineOrPoint))
                        tempList.Add(LineOrPoint);
                    if (!LinesOrPoints.Contains(((Sensor)LineOrPoint).Line))
                        tempList.Add(((Sensor)LineOrPoint).Line);
                }
                else if (LineOrPoint is LineStation)
                {
                    if (!LinesOrPoints.Contains(LineOrPoint))
                        tempList.Add(LineOrPoint);
                    if (!LinesOrPoints.Contains(((LineStation)LineOrPoint).Line))
                        tempList.Add(((LineStation)LineOrPoint).Line);
                }
                //else if(LineOrPoint is Pinokio.Model.Base.Buffer)
                //{
                //    if (!LinesOrPoints.Contains(LineOrPoint))
                //        tempList.Add(LineOrPoint);

                //    foreach(TXNode node in ((TXNode)LineOrPoint).InLinkNodes)
                //    {
                //        if (!LinesOrPoints.Contains(node))
                //            tempList.Add(node);
                //    }

                //    foreach (TXNode node in ((TXNode)LineOrPoint).OutLinkNodes)
                //    {
                //        if (!LinesOrPoints.Contains(node))
                //            tempList.Add(node);
                //    }
                //}
            }

            foreach (SimNode node in tempList)
            {
                if (LinesOrPoints.Contains(node) is false)
                    LinesOrPoints.Add(node);
            }

            if (tempList.Count == 0)
                return LinesOrPoints;
            else
                return GetConnectedPointAndLine(LinesOrPoints);
        }

        private void Move2NewFloor(SimNode simNode)
        {
            SimNodeTreeListNode startNode = (SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(simNode.ID);
            SimNode rootNode = ((SimNodeTreeListNode)startNode.RootNode).SimNode;
            List<SimNode> nodeList = new List<SimNode>();
            nodeList.Add(simNode);

            if (simNode is TransportLine || simNode is GuidedLine || simNode is TransportPoint || simNode is Vehicle || simNode is Sensor || simNode is LineStation)  // Gather Lines and Points connected
                GetConnectedPointAndLine(nodeList);

            nodeList.Distinct().ToList();
            nodeList = nodeList.OrderBy(x => x.LoadLevel).ToList();

            foreach (SimNode node in nodeList)
            {
                SimNodeTreeListNode nodeHasLink = ((SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(node.ID));
                if (nodeHasLink != null && nodeHasLink.RefNode != null)
                {
                    SimNode floor = node;
                    while (!(floor is Floor))
                        floor = floor.ParentNode;

                    Point3D toPoint = new Point3D(nodeHasLink.RefNode.CurrentPoint.X, nodeHasLink.RefNode.CurrentPoint.Y, ((Floor)rootNode).FloorBottom + nodeHasLink.RefNode.CurrentPoint.Z - ((Floor)floor).FloorBottom);

                    if (!(nodeHasLink.RefNode is RefTransportLine)
                        && !(nodeHasLink.RefNode is RefVehicle)
                        && !(nodeHasLink.RefNode is RefLineComponent))
                    {
                        nodeHasLink.RefNode.Move_MouseMove(pinokio3DModel1, toPoint, nodeHasLink.RefNode.CurrentPoint);     // Move refNode
                        nodeHasLink.RefNode.Move_MouseUp(pinokio3DModel1, toPoint, nodeHasLink.RefNode.CurrentPoint);     // Move refNode
                    }

                    //                    Point3D toPoint = new Point3D(nodeHasLink.RefNode.CurrentPoint.X, nodeHasLink.RefNode.CurrentPoint.Y, ((Floor)rootNode).FloorBottom + nodeHasLink.RefNode.Height);

                    //                    nodeHasLink.RefNode.Move_MouseMove(pinokio3DModel1, toPoint, nodeHasLink.RefNode.CurrentPoint);     // Move refNode

                    simNodeTreeList.MoveNode(nodeHasLink, startNode.ParentNode);      // Move to new parentNode on treeList
                    nodeHasLink.RefNode.LayerName = rootNode.ID.ToString();        // new layerName
                    nodeHasLink.SimNode.ParentNode = ((SimNodeTreeListNode)startNode.ParentNode).SimNode as CoupledModel;
                    if (nodeHasLink.SimNode is TXNode)
                        ((TXNode)nodeHasLink.SimNode).UpdateFloor();

                    List<RefLink> refLinks = GetConnectedLinks(nodeHasLink.RefNode);
                    nodeHasLink.RefNode.FromLinked.Clear();
                    nodeHasLink.RefNode.ToLinked.Clear();
                    pinokio3DModel1.Entities.Regen();

                    List<RefLink> newLinks = new List<RefLink>();
                    foreach (RefLink refLink in refLinks)   // Move RefLink
                    {
                        pinokio3DModel1.RemoveNodeReference(refLink);
                        pinokio3DModel1.Entities.Remove(refLink);
                        Point3D fromPoint = new Point3D();
                        fromPoint = (refLink.FromNode.BoxMax + refLink.FromNode.BoxMin) / 2;
                        toPoint = (refLink.ToNode.BoxMax + refLink.ToNode.BoxMin) / 2;
                        RefLink newLink = ((RefLink)refLink).CreateRefLink(pinokio3DModel1, fromPoint, toPoint, refLink.FromNode, refLink.ToNode, refLink.Core);
                        newLink.LayerName = refLink.FromNode.LayerName;
                        newLink.ToWay = refLink.ToWay;
                        newLink.CurrentPoint = fromPoint;
                        newLinks.Add(newLink);

                        pinokio3DModel1.AddNodeReference(newLink);
                        if (refLink.FromNode == nodeHasLink.RefNode)
                        {
                            simNodeTreeList.MoveNode(simNodeTreeList.FindNodeByKeyID(refLink.ID), startNode.RootNode);      // Move to new layer(floor) on treeList
                            refLink.LayerName = rootNode.ID.ToString();    // new layerName
                        }
                    }

                    pinokio3DModel1.Entities.AddRange(newLinks);

                    refLinks.Clear();
                }
            }
            for (int i = 0; i < startNode.Nodes.Count; i++)
                Move2NewFloor(((SimNodeTreeListNode)startNode.Nodes[i]).SimNode);

            pinokio3DModel1.Entities.Regen();
            pinokio3DModel1.Invalidate();
        }

        private void AddMoveFloorUndo()
        {

        }

        private void FloorSetup()
        {
            try
            {
                SettingFloor();
                SetFloorform();
                FloorPlanUpdate();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void NewItem(bool isSnapShot)
        {
            ClearSelection();
            if (!isSnapShot)
                ModelManager.Instance.init();

            simNodeTreeList.Nodes.Clear();
            _floorForm.LstFloorPlan.Clear();
            UndoRedoData.Clear();
            List<Grid> grids = new List<Grid>();
            pinokio3DModel1.Layers.Clear();
            pinokio3DModel1.Grids = grids.ToArray();
            pinokio3DModel1.NodeReferenceByID.Clear();
            pinokio3DModel1.IDByNames.Clear();
            pinokio3DModel1.Entities.Regen();
            dicVisibleNodeTypeInfo.Clear();
            dicSimNodeType.Clear();
            ChangedRefTypes.Clear();

            List<Block> blockList = pinokio3DModel1.Blocks.ToList();
            for (int i = blockList.Count - 1; i >= 0; i--)
            {
                Block block = blockList[i];
                if (!block.Name.Contains("Ref"))
                {
                    pinokio3DModel1.Blocks.Remove(block);
                }
            }

            pinokio3DModel1.Labels.Clear();
            pinokio3DModel1.Invalidate();
        }

        private bool SelectedFloorClear()
        {
            if (_floorForm.LstFloorPlan.Count > 0)
            {
                pinokio3DModel1.SelectedFloorID = _floorForm.LstFloorPlan[0].Floor.ID;
                pinokio3DModel1.SelectedFloorHeight = _floorForm.LstFloorPlan[0].FloorBottom;
                return true;
            }
            else
                return false;
        }

        private List<Point3D> GetNodeReferenceCurrentPoints(List<NodeReference> refNodes)
        {
            List<Point3D> NodeCurrentPoints = new List<Point3D>();
            foreach (NodeReference refNode in refNodes)
                NodeCurrentPoints.Add(refNode.CurrentPoint);
            return NodeCurrentPoints;
        }


        //TODO devDept: Now you need to pass through the Model.ActiveViewport property to get/set the active viewport grid.
        private void gridViewInsertNode_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
                string refNodeType = "Ref" + ((InserteNodeTreeData)(this.gridViewInsertRefNode.GetRow(e.RowHandle))).RefType;
                string simNodeType = ((InserteNodeTreeData)(this.gridViewInsertRefNode.GetRow(e.RowHandle))).NodeType;
                double height = ((InserteNodeTreeData)(this.gridViewInsertRefNode.GetRow(e.RowHandle))).Height;
                PrepareInsertingRefNode(refNodeType, simNodeType, height, new Point3D(0, 0, 0));
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }
        //TODO devDept: Now you need to pass through the Model.ActiveViewport property to get/set the active viewport grid.
        private void gridViewInsertCoupledModel_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
                string name = ((InserteNodeTreeData)(this.gridViewInsertCoupledModel.GetRow(e.RowHandle))).RefType;

                Type type = null;
                Assembly a = Assembly.LoadWithPartialName("Pinokio.Model.Base");


            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }
        #region Window 조작

        private void bbiInsertNode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dockPanelInsertRefNode.Visibility == DockVisibility.Hidden)
            {
                dockPanelInsertRefNode.Show();

                if (dockPanelInsertCoupledModel.Visibility == DockVisibility.Hidden && dockPanelInsertedSimNodes.Visibility == DockVisibility.Visible)
                    dockPanelInsertRefNode.DockAsTab(dockPanelInsertedSimNodes);
                else if (dockPanelInsertedSimNodes.Visibility == DockVisibility.Hidden && dockPanelInsertCoupledModel.Visibility == DockVisibility.Visible)
                    dockPanelInsertRefNode.DockAsTab(dockPanelInsertCoupledModel);
                else if (dockPanelInsertedSimNodes.Visibility == DockVisibility.Visible && dockPanelInsertCoupledModel.Visibility == DockVisibility.Visible)
                    dockPanelInsertRefNode.DockAsTab(dockPanelInsertedSimNodes);
            }
            else
                dockPanelInsertRefNode.Show();
        }

        private void bbiInsertCoupledModel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dockPanelInsertCoupledModel.Visibility == DockVisibility.Hidden)
            {
                dockPanelInsertCoupledModel.Show();

                if (dockPanelInsertRefNode.Visibility == DockVisibility.Hidden && dockPanelInsertedSimNodes.Visibility == DockVisibility.Visible)
                    dockPanelInsertCoupledModel.DockAsTab(dockPanelInsertedSimNodes);
                else if (dockPanelInsertedSimNodes.Visibility == DockVisibility.Hidden && dockPanelInsertRefNode.Visibility == DockVisibility.Visible)
                    dockPanelInsertCoupledModel.DockAsTab(dockPanelInsertRefNode);
                else if (dockPanelInsertedSimNodes.Visibility == DockVisibility.Visible && dockPanelInsertRefNode.Visibility == DockVisibility.Visible)
                    dockPanelInsertCoupledModel.DockAsTab(dockPanelInsertedSimNodes);
            }
            else
                dockPanelInsertCoupledModel.Show();
        }

        private void bbiNodes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dockPanelInsertedSimNodes.Visibility == DockVisibility.Hidden)
            {
                dockPanelInsertedSimNodes.Show();

                if (dockPanelInsertRefNode.Visibility == DockVisibility.Hidden && dockPanelInsertCoupledModel.Visibility == DockVisibility.Visible)
                    dockPanelInsertedSimNodes.DockAsTab(dockPanelInsertCoupledModel);
                else if (dockPanelInsertCoupledModel.Visibility == DockVisibility.Hidden && dockPanelInsertRefNode.Visibility == DockVisibility.Visible)
                    dockPanelInsertedSimNodes.DockAsTab(dockPanelInsertRefNode);
                else if (dockPanelInsertCoupledModel.Visibility == DockVisibility.Visible && dockPanelInsertRefNode.Visibility == DockVisibility.Visible)
                    dockPanelInsertedSimNodes.DockAsTab(dockPanelInsertCoupledModel);

            }
            else
                dockPanelInsertedSimNodes.Show();
        }



        private void bbiObjectProperties_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dockPanelSimNodeProperties.Show();
        }

        private void bbiDockPart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dockPanelParts.Show();
        }
        private void bbiDockLineStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dockPanelLineStatus.Show();
        }

        private void bbiDockLineStatusDetail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dockPanelLineStatusDetail.Show();
        }

        private void dockPanelInsertRefNode_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            dockPanelInsertRefNode.Hide();
        }
        private void dockPanelInsertCoupledModel_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            dockPanelInsertCoupledModel.Hide();
        }
        private void dockPanelInsertedSimNodes_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            dockPanelInsertedSimNodes.Hide();
        }
        private void dockPanelSimNodeProperties_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            dockPanelSimNodeProperties.Hide();
        }
        private void dockPanelParts_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            dockPanelParts.Hide();
        }
        private void dockPanelLineStatus_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            dockPanelLineStatus.Hide();
        }
        private void dockPanelLineStatusDetail_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            dockPanelLineStatusDetail.Hide();
        }
        #endregion

        private void bbiEditScript_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditScriptCodeForm form = new EditScriptCodeForm();
            form.ShowDialog();
        }

        private void bbiEditVisible_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dicSimNodeType = new Dictionary<string, List<SimNodeTreeListNode>>();

            TraverseTreeListNodes(null);

            bool isIconVisible;
            bool isTextVisible;

            if (pinokio3DModel1.Entities.ToList().FirstOrDefault(x => x.GetType().Name.Contains("Picture")) != null)
            {
                isIconVisible = pinokio3DModel1.Entities.ToList().Find(x => x.GetType().Name.Contains("Picture")).Visible;

                if (!dicVisibleNodeTypeInfo.ContainsKey("Blueprint"))
                    dicVisibleNodeTypeInfo.Add("Blueprint", new Tuple<bool, bool>(isIconVisible, false));
                else
                    dicVisibleNodeTypeInfo["Blueprint"] = new Tuple<bool, bool>(isIconVisible, false);
            }

            foreach(var kvp in dicSimNodeType)
            {
                NodeReference nodeReference = null;
                uint key = kvp.Value.First().SimNode.ID;

                if (pinokio3DModel1.NodeReferenceByID.TryGetValue(key, out NodeReference foundNodeReference))
                {
                    nodeReference = foundNodeReference;
                }
                
                isIconVisible = !kvp.Value.Any(n => n.Checked == false);
                isTextVisible = (nodeReference?.Label != null && nodeReference.Label.Visible);
                

                if (!dicVisibleNodeTypeInfo.ContainsKey(kvp.Key))
                    dicVisibleNodeTypeInfo.Add(kvp.Key, new Tuple<bool, bool>(isIconVisible, isTextVisible));
                else
                    dicVisibleNodeTypeInfo[kvp.Key] = new Tuple<bool, bool>(isIconVisible, isTextVisible);
            }

            EditVisibleForm form = new EditVisibleForm(dicVisibleNodeTypeInfo);

            if (form.ShowDialog() == DialogResult.OK)
            {
                dicVisibleNodeTypeInfo = form._visibleCheckedInfo;

                if (pinokio3DModel1.Entities.ToList().FirstOrDefault(x => x.GetType().Name.Contains("Picture")) != null)
                {
                    List<Entity> picList = pinokio3DModel1.Entities.ToList().FindAll(x => x.GetType().Name.Contains("Picture"));
                    foreach (Entity pic in picList)
                    {
                        pic.Visible = form._visibleCheckedInfo["Blueprint"].Item1;
                    }
                }

                foreach (var kvp in dicVisibleNodeTypeInfo)
                {
                    if (kvp.Key == "Blueprint")
                        continue;
                    foreach (SimNodeTreeListNode node in dicSimNodeType[kvp.Key])
                    {
                        NodeReference nodeReference = null;
                        uint key = node.SimNode.ID;

                        if (pinokio3DModel1.NodeReferenceByID.TryGetValue(key, out NodeReference foundNodeReference))
                        {
                            nodeReference = foundNodeReference;
                        }
                        if (nodeReference?.Label != null)
                        {
                            nodeReference.Label.Visible = kvp.Value.Item2;
                        }
                        if (node.Checked == kvp.Value.Item1)
                            continue;
                        if (node.SimNode != null)
                        {
                            if (isRootTreeNode(node.SimNode))
                            {
                                pinokio3DModel1.SelectedFloorID = node.SimNode.ID;

                                for (int i = 0; i < pinokio3DModel1.Layers.Count; i++)
                                {
                                    if (pinokio3DModel1.Layers[i].Name == node.SimNode.ID.ToString())
                                    {
                                        pinokio3DModel1.Grids[i].Visible = kvp.Value.Item1;
                                        pinokio3DModel1.Layers[i].Visible = kvp.Value.Item1;
                                        node.Checked = kvp.Value.Item1;
                                        break;
                                    }
                                }
                            }
                        }
                        ChildCheckIfParentCheckforVisibility(node, kvp.Value.Item1);
                        node.Checked = kvp.Value.Item1;
                    }
                }
                pinokio3DModel1.Invalidate();
            }
        }
        void TraverseTreeListNodes(DevExpress.XtraTreeList.Nodes.TreeListNode parentNode)
        {
            if (parentNode == null)
            {
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode rootNode in simNodeTreeList.Nodes)
                {
                    if (!dicSimNodeType.ContainsKey(rootNode["NodeType"].ToString()))
                    {
                        dicSimNodeType.Add(rootNode["NodeType"].ToString(), new List<SimNodeTreeListNode>());
                    }
                    dicSimNodeType[rootNode["NodeType"].ToString()].Add(rootNode as SimNodeTreeListNode);

                    TraverseTreeListNodes(rootNode);
                }
            }
            else
            {
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode childNode in parentNode.Nodes)
                {
                    if (!dicSimNodeType.ContainsKey(childNode["NodeType"].ToString()))
                    {
                        dicSimNodeType.Add(childNode["NodeType"].ToString(), new List<SimNodeTreeListNode>());
                    }
                    dicSimNodeType[childNode["NodeType"].ToString()].Add(childNode as SimNodeTreeListNode);

                    //Node Label 추가
                    if (pinokio3DModel1.NodeReferenceByID.TryGetValue(((SimNodeTreeListNode)childNode).SimNode.ID, out NodeReference foundNodeReference))
                        AddToNodeLabel(foundNodeReference);

                    // childNode의 자식 노드들을 재귀적으로 순회합니다.
                    TraverseTreeListNodes(childNode);
                }
            }
        }

        public void AddToNodeLabel(NodeReference node)
        {
            if (node.Label == null)
            {
                Point3D point = node.CurrentPoint;

                pinokio3DModel1.NodeReferenceByID[node.ID].Label = new LeaderAndText(point.X, point.Y, point.Z, node.Core.Name,
                                new Font("Tahoma", 8f, FontStyle.Bold), Color.FromArgb(100, Color.Yellow), new Vector2D(10, 10));

                node.Label.Visible = false;
                pinokio3DModel1.Labels.Add(node.Label);
            }
        }

        private List<Vehicle> GetVehicleList(bool updateVehicleInfo = false)
        {
            var vSubCsLst = FactoryManager.Instance.MCS.DicVSubCs.Values.ToList();
            var vehicleLst = new List<Vehicle>();

            foreach (var vSubCs in vSubCsLst)
                vehicleLst.AddRange(vSubCs.Vehicles);

            return UpdateVegicleInfo(vehicleLst, updateVehicleInfo);
        }

        private List<Vehicle> UpdateVegicleInfo(List<Vehicle> vehicleList, bool updateVehicleInfo)
        {
            if (updateVehicleInfo)
            {
                foreach (var vehicle in vehicleList)
                    vehicle.UpdateVehicleInfo();
            }
            return vehicleList;
        }

        private void BeiDays_ShownEditor(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BeiDays.Manager.ActiveEditor.KeyPress += new KeyPressEventHandler(ActiveEditor_KeyPress);
        }

        private void BeiHours_ShownEditor(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BeiHours.Manager.ActiveEditor.KeyPress += new KeyPressEventHandler(ActiveEditor_KeyPress);
        }

        private void BeiMinutes_ShownEditor(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BeiMinutes.Manager.ActiveEditor.KeyPress += new KeyPressEventHandler(ActiveEditor_KeyPress);
        }

        private void ActiveEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == '.'))
                e.Handled = true;
        }

        public void InitializeBays()
        {
            foreach (BAY bay in FactoryManager.Instance.Bays.Values)
            {
                bay.Lines.Clear();
                bay.TransportLines.Clear();
                bay.BumpingOHTs.Clear();
                bay.BumpingStations.Clear();
                bay.NeighborBay.Clear();
                bay.ToLines.Clear();
                bay.FromLines.Clear();
                bay.FromBumpingStations.Clear();
                bay.ToBumpingStations.Clear();
            }

            foreach (SimNode node in ModelManager.Instance.SimNodes.Values)
            {
                if (node.GetType().Name == "OHTLine")
                {
                    foreach (BAY bay in FactoryManager.Instance.Bays.Values)
                    {
                        if (bay.Name == ((OHTLine)((TransportLine)node)).Bay)
                        {
                            if (!bay.TransportLines.Contains((TransportLine)node))
                            {
                                bay.TransportLines.Add((TransportLine)node);
                                break;
                            }
                            else
                                ;
                        }
                    }
                }
            }
            foreach (BAY bay in FactoryManager.Instance.Bays.Values)
            {
                List<BAY> tempBaysForInLines = new List<BAY>();
                List<BAY> tempBaysForOutLines = new List<BAY>();
                List<TransportLine> FromLines = new List<TransportLine>();
                List<TransportLine> ToLines = new List<TransportLine>();

                List<LineStation> lineStations = new List<LineStation>();
                List<LineStation> FromBumpingStations = new List<LineStation>();
                List<LineStation> ToBumpingStations = new List<LineStation>();

                int previousInCount = tempBaysForInLines.Count;
                int previousOutCount = tempBaysForOutLines.Count;

                foreach (TransportLine line in bay.TransportLines)
                {
                    TraverseLines(((OHTLine)line), 0, tempBaysForInLines, bay, true);
                    TraverseLines(((OHTLine)line), 0, tempBaysForOutLines, bay, false);
                    //List<OHTLine> path = null;
                    //TraverseLines(((OHTLine)line), 0, tempBaysForInLines, bay, true, path);
                    //path = null;
                    //TraverseLines(((OHTLine)line), 0, tempBaysForOutLines, bay, false, path);
                    if (tempBaysForInLines.Count != previousInCount)
                    {
                        FromLines.Add(line);
                        previousInCount = tempBaysForInLines.Count;
                    }
                    if (tempBaysForOutLines.Count != previousOutCount)
                    {
                        ToLines.Add(line);
                        previousOutCount = tempBaysForOutLines.Count;
                    }
                    lineStations.AddRange(line.LineStations);
                }
                lineStations.Sort((p1, p2) =>
                {
                    int result = p1.PosVec3.X.CompareTo(p2.PosVec3.X);
                    if (result == 0)
                    {
                        if (p1.Line.Direction.Y == 1) // Y 값 증가 방향
                        {
                            result = p1.PosVec3.Y.CompareTo(p2.PosVec3.Y);
                        }
                        else if (p1.Line.Direction.Y == -1) // Y 값 감소 방향
                        {
                            result = p2.PosVec3.Y.CompareTo(p1.PosVec3.Y);
                        }
                    }
                    return result;
                });

                for (int i = 0; i < lineStations.Count; i++)
                {
                    LineStation currentStation = lineStations[i];
                    LineStation nextStation = null;

                    if (currentStation.UTB != null)
                    {
                        for (int j = i + 1; j < lineStations.Count; j++)
                        {
                            if (lineStations[j].UTB == null)
                            {
                                nextStation = lineStations[j];

                                bay.BumpingStations.Add(currentStation, nextStation);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (i == lineStations.Count - 1)
                        {
                            nextStation = lineStations[0];
                            if (nextStation.UTB != null)
                                ;
                            bay.BumpingStations.Add(currentStation, nextStation);
                        }
                        else
                        {
                            int idx = i;
                            for (int k = idx + 1; k < lineStations.Count; k++)
                            {
                                if (lineStations[k].UTB == null)
                                {
                                    nextStation = lineStations[k];
                                    break;
                                }
                                else
                                    ;
                            }
                            bay.BumpingStations.Add(currentStation, nextStation);
                        }
                    }
                }

                bay.NeighborBay = tempBaysForInLines;
                bay.ToLines = ToLines;
                bay.FromLines = FromLines;
                bay.FromBumpingStations = FromLines.Select(line => line.LineStations[0]).ToList();
                bay.ToBumpingStations = ToLines.Select(line => line.LineStations.Last()).ToList();
                if (bay.NeighborBay.Count == 1)
                {
                    if (bay.Name.Contains("A"))
                    {
                        string neighborName = "B" + bay.Name.Substring(bay.Name.IndexOf('_'));
                        bay.NeighborBay.Add(FactoryManager.Instance.Bays[neighborName]);
                    }
                    else
                    {
                        string neighborName = "A" + bay.Name.Substring(bay.Name.IndexOf('_'));
                        bay.NeighborBay.Add(FactoryManager.Instance.Bays[neighborName]);
                    }
                }
            }
        }
        private void TraverseLines(OHTLine searchLine, int depth, List<BAY> tempBays, BAY currentBay, bool isInLines)
        {
            if (depth >= 4)
                return;
            if (isInLines)
            {
                foreach (OHTLine deeperInLine in searchLine.StartPoint.InLines)
                {
                    if (deeperInLine.Bay == currentBay.Name || deeperInLine.Bay == "" || deeperInLine.Bay.Contains("InterBay"))
                    {
                        TraverseLines(deeperInLine, depth + 1, tempBays, currentBay, isInLines);
                    }
                    else
                    {
                        if (!tempBays.Contains(FactoryManager.Instance.Bays[deeperInLine.Bay]))
                            tempBays.Add(FactoryManager.Instance.Bays[deeperInLine.Bay]);
                        else
                            ;
                    }
                }
            }
            else
            {
                foreach (OHTLine deeperOutLine in searchLine.EndPoint.OutLines)
                {
                    if (deeperOutLine.Bay == currentBay.Name || deeperOutLine.Bay == "" || deeperOutLine.Bay.Contains("InterBay"))
                    {
                        TraverseLines(deeperOutLine, depth + 1, tempBays, currentBay, isInLines);
                    }
                    else
                    {
                        if (!tempBays.Contains(FactoryManager.Instance.Bays[deeperOutLine.Bay]))
                            tempBays.Add(FactoryManager.Instance.Bays[deeperOutLine.Bay]);
                        else
                            ;
                    }
                }
            }
        }
        private void TraverseLines(OHTLine searchLine, int depth, List<BAY> tempBays, BAY currentBay, bool isInLines, List<OHTLine> path)
        {
            if (depth == 0)
                path = new List<OHTLine>();



            if (depth >= 4)
            {
                if (searchLine.Bay == currentBay.Name)
                {
                    //경로상의 모든 라인을 동일한 베이로 설정
                    foreach (var line in path)
                    {
                        line.Bay = currentBay.Name;
                    }
                }
                return;
            }

            if (isInLines)
            {
                foreach (OHTLine deeperInLine in searchLine.StartPoint.InLines)
                {
                    if (deeperInLine.Bay == currentBay.Name || deeperInLine.Bay == "" || deeperInLine.Bay.Contains("InterBay"))
                    {
                        path.Add(searchLine);
                        TraverseLines(deeperInLine, depth + 1, tempBays, currentBay, isInLines, path);
                    }
                    else
                    {
                        if (!tempBays.Contains(FactoryManager.Instance.Bays[deeperInLine.Bay]))
                            tempBays.Add(FactoryManager.Instance.Bays[deeperInLine.Bay]);
                        else
                            ;
                    }
                }
            }
            else
            {
                foreach (OHTLine deeperOutLine in searchLine.EndPoint.OutLines)
                {
                    if (deeperOutLine.Bay == currentBay.Name || deeperOutLine.Bay == "" || deeperOutLine.Bay.Contains("InterBay"))
                    {
                        path.Add(searchLine);
                        TraverseLines(deeperOutLine, depth + 1, tempBays, currentBay, isInLines, path);
                    }
                    else
                    {
                        if (!tempBays.Contains(FactoryManager.Instance.Bays[deeperOutLine.Bay]))
                            tempBays.Add(FactoryManager.Instance.Bays[deeperOutLine.Bay]);
                        else
                            ;
                    }
                }
            }
        }

        private void bbiLoadSnapShot_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "DAT File(*.dat)| *.dat";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SnapShotManager snapShotManager = new SnapShotManager(ModelManager.Instance, FactoryManager.Instance, SimEngine.Instance, ModelManager.Instance.SnapShotModelDBPath);
                snapShotManager.DeserializeSingleton(dlg.FileName);
                LoadSnapShotLayout(AccessDB.Instance.DBPath);
                InitWarmUpPeriod();
            }
        }

        private void bbiSaveSnapShot_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "DAT File (*.dat)|*.dat";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.pinokio3DModel1.ObjectManipulator.MouseOver -= OnObjectManipulatorMouseOver;
                this.pinokio3DModel1.ObjectManipulator.MouseDown -= OnObjectManipulatorMouseDown;
                this.pinokio3DModel1.ObjectManipulator.MouseDrag -= OnObjectManipulatorMouseDrag;
                this.pinokio3DModel1.ObjectManipulator.MouseUp -= OnObjectManipulatorMouseUp;
                ModelManager.Instance.GenerateParts -= new deleAlarmGenerateParts(Pinokio3Dmodel.AddPartsReference);
                ModelManager.Instance.GeneratePart -= new deleAlarmGeneratePart(Pinokio3Dmodel.AddPartReference);
                ModelManager.Instance.DeletePart -= new deleAlarmDeletePart(Pinokio3Dmodel.RemovePartReference);
                ModelManager.Instance.SimulationEnd -= new deleSimulationEnd(SimulationEnd);
                ModelManager.Instance.FailSimulation -= new deleFaliSimulation(FaliSimulation);
                ModelManager.Instance.AnimationNode.AnimationEvent -= this.AnimationEvent;
                //(new Simulator()).OnStartSimulation -= Simulator_OnStartSimulation;
                simNodeTreeList.AfterDropNode -= SimNodesTreeList_AfterDropNode;
                simNodeTreeList.MouseClick -= SimNodesTreeList_MouseClick;
                simNodeTreeList.BeforeDropNode -= SimNodesTreeList_BeforeDropNode;
                simNodeTreeList.BeforeDragNode -= SimNodesTreeList_BeforeDragNode;
                simNodeTreeList.AfterCheckNode -= SimNodesTreeList_AfterCheckNode;

                ModelManager.Instance.SnapShotModelDBPath = dlg.FileName.Replace("dat", "accdb");
                SnapShotManager snapShotManager = new SnapShotManager(ModelManager.Instance, FactoryManager.Instance, SimEngine.Instance, ModelManager.Instance.SnapShotModelDBPath);
                snapShotManager.SerializeSingleton(dlg.FileName);
                this.pinokio3DModel1.ObjectManipulator.MouseOver += OnObjectManipulatorMouseOver;
                this.pinokio3DModel1.ObjectManipulator.MouseDown += OnObjectManipulatorMouseDown;
                this.pinokio3DModel1.ObjectManipulator.MouseDrag += OnObjectManipulatorMouseDrag;
                this.pinokio3DModel1.ObjectManipulator.MouseUp += OnObjectManipulatorMouseUp;
                ModelManager.Instance.GenerateParts += new deleAlarmGenerateParts(Pinokio3Dmodel.AddPartsReference);
                ModelManager.Instance.GeneratePart += new deleAlarmGeneratePart(Pinokio3Dmodel.AddPartReference);
                ModelManager.Instance.DeletePart += new deleAlarmDeletePart(Pinokio3Dmodel.RemovePartReference);
                ModelManager.Instance.SimulationEnd += new deleSimulationEnd(SimulationEnd);
                ModelManager.Instance.FailSimulation += new deleFaliSimulation(FaliSimulation);
                ModelManager.Instance.AnimationNode.AnimationEvent += this.AnimationEvent;
                //(new Simulator()).OnStartSimulation += Simulator_OnStartSimulation;
                simNodeTreeList.AfterDropNode += SimNodesTreeList_AfterDropNode;
                simNodeTreeList.MouseClick += SimNodesTreeList_MouseClick;
                simNodeTreeList.BeforeDropNode += SimNodesTreeList_BeforeDropNode;
                simNodeTreeList.BeforeDragNode += SimNodesTreeList_BeforeDragNode;
                simNodeTreeList.AfterCheckNode += SimNodesTreeList_AfterCheckNode;

                string filePath = ModelManager.Instance.SnapShotModelDBPath;
                SplashScreenManager.ShowForm(typeof(WaitFormSplash));
                if (pinokio3DModel1.SaveSnapShotModel(filePath, _floorForm.LstFloorPlan))
                {
                    SplashScreenManager.CloseForm();
                    FlyoutDialog.Show(Application.OpenForms[0], new FOAInfo("Success"));
                }
                else
                {
                    SplashScreenManager.CloseForm();
                    FlyoutDialog.Show(Application.OpenForms[0], new FOAInfo("Failed."));
                }
            }
        }

        //private void AutoZCUSetting()
        //{
        //    try
        //    {
        //        OHTZCU _zcu = null;
        //        ZCU_TYPE _zcuType;
        //        int cnt = 1;
        //        foreach (SimNode node in ModelManager.Instance.SimNodes.Values)
        //        {
        //            if (node.GetType().Name == "OHTPoint" && node.ParentNode.GetType().Name.Contains("OCS"))
        //            {
        //                if (node.Name == "OHTPoint_13950")
        //                    ;
        //                if (((OHTPoint)((TransportPoint)node)).Zcu == null)
        //                {
        //                    if (((TransportPoint)node).InLines.Count == 1 && ((TransportPoint)node).OutLines.Count == 1)
        //                    {
        //                        //OHTZCU zcu = new OHTZCU()
        //                    }
        //                    else if (((TransportPoint)node).InLines.Count == 2 && ((TransportPoint)node).OutLines.Count == 1)
        //                    {
        //                        _zcu = ((OHTPoint)((TransportPoint)node)).Ocs.AddZcu("ZCU_" + cnt);
        //                        AddToZCUmembers(((OHTPoint)((TransportPoint)node)), _zcu);
        //                    }
        //                    else if (((TransportPoint)node).InLines.Count == 1 && ((TransportPoint)node).OutLines.Count == 2)
        //                    {
        //                        _zcu = ((OHTPoint)((TransportPoint)node)).Ocs.AddZcu("ZCU_" + cnt);
        //                        AddFromZCUmembers(((OHTPoint)((TransportPoint)node)), _zcu);
        //                    }
        //                }
        //                else
        //                {
        //                    //((OHTPoint)((TransportPoint)node)).Zcu = null;
        //                    if (((TransportPoint)node).InLines.Count == 1 && ((TransportPoint)node).OutLines.Count == 1)
        //                    {
        //                        if (((OHTPoint)((TransportPoint)node).InLines[0].StartPoint).ZcuType == ZCU_TYPE.STOP && ((OHTPoint)((TransportPoint)node).OutLines[0].EndPoint).ZcuType == ZCU_TYPE.RESET)
        //                        {
        //                            ((OHTPoint)((TransportPoint)node)).Zcu = null;

        //                            foreach (var ho in ((OHTPoint)((TransportPoint)node).OutLines[0].EndPoint).InLines)
        //                            {
        //                                if ((OHTPoint)ho.StartPoint != ((OHTPoint)((TransportPoint)node)))
        //                                {
        //                                    ((OHTPoint)ho.StartPoint).Zcu = null;
        //                                    ((OHTPoint)ho.StartPoint).ZcuName = ((OHTPoint)((TransportPoint)node).InLines[0].StartPoint).ZcuName;
        //                                    ((OHTPoint)ho.StartPoint).ZcuType = ZCU_TYPE.STOP;
        //                                }
        //                            }
        //                            var hi = ((OHTPoint)((TransportPoint)node).OutLines[0].EndPoint).Zcu.ToPoints;
        //                            foreach (var ho in ((OHTPoint)((TransportPoint)node).OutLines[0].EndPoint).OutLines)
        //                            {
        //                                if ((OHTPoint)ho.StartPoint != ((OHTPoint)((TransportPoint)node)))
        //                                {
        //                                    ((OHTPoint)ho.StartPoint).Zcu = null;
        //                                    ((OHTPoint)ho.StartPoint).ZcuName = ((OHTPoint)((TransportPoint)node).InLines[0].StartPoint).ZcuName;
        //                                    ((OHTPoint)ho.StartPoint).ZcuType = ZCU_TYPE.RESET;
        //                                }
        //                            }
        //                            ((OHTPoint)((TransportPoint)node)).ZCUDONE = true;
        //                            //((OHTPoint)((TransportPoint)node)).Ocs.Zcus.Remove(((OHTPoint)((TransportPoint)node).OutLines[0].EndPoint).Zcu.Name);
        //                        }
        //                        else if (((OHTPoint)((TransportPoint)node).InLines[0].StartPoint).ZcuType == ZCU_TYPE.STOP && ((OHTPoint)((TransportPoint)node).OutLines[0].EndPoint).ZcuType == ZCU_TYPE.STOP && ((OHTPoint)((TransportPoint)node)).ZcuType == ZCU_TYPE.RESET)
        //                        {
        //                            ((OHTPoint)((TransportPoint)node)).Zcu = null;
        //                            ((OHTPoint)((TransportPoint)node)).ZcuName = ((OHTPoint)((TransportPoint)node).InLines[0].StartPoint).ZcuName;
        //                            ((OHTPoint)((TransportPoint)node)).ZcuType = ZCU_TYPE.RESET;
        //                        }
        //                    }
        //                    else if (((TransportPoint)node).InLines.Count == 2 || ((TransportPoint)node).OutLines.Count == 2)
        //                    {
        //                        if (((OHTPoint)((TransportPoint)node)).ZcuType == ZCU_TYPE.STOP)
        //                        {
        //                            OHTZCU tempZcu = null;
        //                            foreach (var ho in ((OHTPoint)((TransportPoint)node)).OutLines)
        //                            {
        //                                if (((OHTPoint)ho.EndPoint).Zcu != null)
        //                                    tempZcu = ((OHTPoint)ho.EndPoint).Zcu;
        //                            }
        //                            ((OHTPoint)((TransportPoint)node)).Zcu = null;
        //                            ((OHTPoint)((TransportPoint)node)).ZcuName = tempZcu.Name;
        //                            ((OHTPoint)((TransportPoint)node)).ZcuType = ZCU_TYPE.STOP;

        //                            foreach (var ha in ((OHTPoint)((TransportPoint)node)).OutLines)
        //                            {
        //                                if (((OHTPoint)ha.EndPoint).Zcu == null && ((OHTPoint)ha.EndPoint).ZCUDONE == false)
        //                                {
        //                                    ((OHTPoint)ha.EndPoint).ZcuName = tempZcu.Name;
        //                                    ((OHTPoint)ha.EndPoint).ZcuType = ZCU_TYPE.RESET;
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            OHTZCU tempZcu = null;
        //                            foreach (var ho in ((OHTPoint)((TransportPoint)node)).InLines)
        //                            {
        //                                if (((OHTPoint)ho.StartPoint).Zcu != null)
        //                                    tempZcu = ((OHTPoint)ho.StartPoint).Zcu;
        //                            }
        //                            ((OHTPoint)((TransportPoint)node)).Zcu = null;
        //                            ((OHTPoint)((TransportPoint)node)).ZcuName = tempZcu.Name;
        //                            ((OHTPoint)((TransportPoint)node)).ZcuType = ZCU_TYPE.RESET;

        //                            foreach (var ha in ((OHTPoint)((TransportPoint)node)).InLines)
        //                            {
        //                                if (((OHTPoint)ha.StartPoint).Zcu == null && ((OHTPoint)ha.StartPoint).ZCUDONE == false)
        //                                {
        //                                    ((OHTPoint)ha.StartPoint).ZcuName = tempZcu.Name;
        //                                    ((OHTPoint)ha.StartPoint).ZcuType = ZCU_TYPE.STOP;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                cnt += 1;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.SaveLog(ex);

        //    }
        //}
        //private void bbiMakeZcu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    //confirmPoint();
        //    OHTZCU _zcu = null;
        //    ZCU_TYPE _zcuType;
        //    int cnt = 1;
        //    foreach (SimNode node in ModelManager.Instance.SimNodes.Values)
        //    {
        //        if (node.GetType().Name == "OHTPoint" && node.ParentNode.GetType().Name == "OCS")
        //        {
        //            if (node.Name == "OHTPoint_9495")
        //                ;
        //            if (((OHTPoint)((TransportPoint)node)).Zcu == null)
        //            {
        //                if (((TransportPoint)node).InLines.Count == 1 && ((TransportPoint)node).OutLines.Count == 1)
        //                {
        //                    //OHTZCU zcu = new OHTZCU()
        //                }
        //                else if (((TransportPoint)node).InLines.Count == 2 && ((TransportPoint)node).OutLines.Count == 1)
        //                {
        //                    _zcu = ((OHTPoint)((TransportPoint)node)).Ocs.AddZcu("ZCU_" + cnt);
        //                    AddToZCUmembers(((OHTPoint)((TransportPoint)node)), _zcu);
        //                }
        //                else if (((TransportPoint)node).InLines.Count == 1 && ((TransportPoint)node).OutLines.Count == 2)
        //                {
        //                    _zcu = ((OHTPoint)((TransportPoint)node)).Ocs.AddZcu("ZCU_" + cnt);
        //                    AddFromZCUmembers(((OHTPoint)((TransportPoint)node)), _zcu);
        //                }
        //            }
        //            else
        //            {
        //                //((OHTPoint)((TransportPoint)node)).Zcu = null;
        //                if (((TransportPoint)node).InLines.Count == 1 && ((TransportPoint)node).OutLines.Count == 1)
        //                {
        //                    if (((OHTPoint)((TransportPoint)node).InLines[0].StartPoint).ZcuType == ZCU_TYPE.STOP && ((OHTPoint)((TransportPoint)node).OutLines[0].EndPoint).ZcuType == ZCU_TYPE.RESET)
        //                    {
        //                        ((OHTPoint)((TransportPoint)node)).Zcu = null;

        //                        foreach (var ho in ((OHTPoint)((TransportPoint)node).OutLines[0].EndPoint).InLines)
        //                        {
        //                            if ((OHTPoint)ho.StartPoint != ((OHTPoint)((TransportPoint)node)))
        //                            {
        //                                ((OHTPoint)ho.StartPoint).Zcu = null;
        //                                ((OHTPoint)ho.StartPoint).ZcuName = ((OHTPoint)((TransportPoint)node).InLines[0].StartPoint).ZcuName;
        //                                ((OHTPoint)ho.StartPoint).ZcuType = ZCU_TYPE.STOP;
        //                            }
        //                        }
        //                        var hi = ((OHTPoint)((TransportPoint)node).OutLines[0].EndPoint).Zcu.ToPoints;
        //                        foreach (var ho in ((OHTPoint)((TransportPoint)node).OutLines[0].EndPoint).OutLines)
        //                        {
        //                            if ((OHTPoint)ho.StartPoint != ((OHTPoint)((TransportPoint)node)))
        //                            {
        //                                ((OHTPoint)ho.StartPoint).Zcu = null;
        //                                ((OHTPoint)ho.StartPoint).ZcuName = ((OHTPoint)((TransportPoint)node).InLines[0].StartPoint).ZcuName;
        //                                ((OHTPoint)ho.StartPoint).ZcuType = ZCU_TYPE.RESET;
        //                            }
        //                        }
        //                        ((OHTPoint)((TransportPoint)node)).ZCUDONE = true;
        //                        //((OHTPoint)((TransportPoint)node)).Ocs.Zcus.Remove(((OHTPoint)((TransportPoint)node).OutLines[0].EndPoint).Zcu.Name);
        //                    }
        //                    else if (((OHTPoint)((TransportPoint)node).InLines[0].StartPoint).ZcuType == ZCU_TYPE.STOP && ((OHTPoint)((TransportPoint)node).OutLines[0].EndPoint).ZcuType == ZCU_TYPE.STOP && ((OHTPoint)((TransportPoint)node)).ZcuType == ZCU_TYPE.RESET)
        //                    {
        //                        ((OHTPoint)((TransportPoint)node)).Zcu = null;
        //                        ((OHTPoint)((TransportPoint)node)).ZcuName = ((OHTPoint)((TransportPoint)node).InLines[0].StartPoint).ZcuName;
        //                        ((OHTPoint)((TransportPoint)node)).ZcuType = ZCU_TYPE.RESET;
        //                    }
        //                }
        //                else if (((TransportPoint)node).InLines.Count == 2 || ((TransportPoint)node).OutLines.Count == 2)
        //                {
        //                    if (((OHTPoint)((TransportPoint)node)).ZcuType == ZCU_TYPE.STOP)
        //                    {
        //                        OHTZCU tempZcu = null;
        //                        foreach (var ho in ((OHTPoint)((TransportPoint)node)).OutLines)
        //                        {
        //                            if (((OHTPoint)ho.EndPoint).Zcu != null)
        //                                tempZcu = ((OHTPoint)ho.EndPoint).Zcu;
        //                        }
        //                        ((OHTPoint)((TransportPoint)node)).Zcu = null;
        //                        ((OHTPoint)((TransportPoint)node)).ZcuName = tempZcu.Name;
        //                        ((OHTPoint)((TransportPoint)node)).ZcuType = ZCU_TYPE.STOP;

        //                        foreach (var ha in ((OHTPoint)((TransportPoint)node)).OutLines)
        //                        {
        //                            if (((OHTPoint)ha.EndPoint).Zcu == null && ((OHTPoint)ha.EndPoint).ZCUDONE == false)
        //                            {
        //                                ((OHTPoint)ha.EndPoint).ZcuName = tempZcu.Name;
        //                                ((OHTPoint)ha.EndPoint).ZcuType = ZCU_TYPE.RESET;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        OHTZCU tempZcu = null;
        //                        foreach (var ho in ((OHTPoint)((TransportPoint)node)).InLines)
        //                        {
        //                            if (((OHTPoint)ho.StartPoint).Zcu != null)
        //                                tempZcu = ((OHTPoint)ho.StartPoint).Zcu;
        //                        }
        //                        ((OHTPoint)((TransportPoint)node)).Zcu = null;
        //                        ((OHTPoint)((TransportPoint)node)).ZcuName = tempZcu.Name;
        //                        ((OHTPoint)((TransportPoint)node)).ZcuType = ZCU_TYPE.RESET;

        //                        foreach (var ha in ((OHTPoint)((TransportPoint)node)).InLines)
        //                        {
        //                            if (((OHTPoint)ha.StartPoint).Zcu == null && ((OHTPoint)ha.StartPoint).ZCUDONE == false)
        //                            {
        //                                ((OHTPoint)ha.StartPoint).ZcuName = tempZcu.Name;
        //                                ((OHTPoint)ha.StartPoint).ZcuType = ZCU_TYPE.STOP;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            cnt += 1;
        //        }
        //    }
        //}
        //public void AddFromZCUmembers(OHTPoint node, OHTZCU _zcu)
        //{
        //    _zcu.FromPoints.Add(node.Name, node);
        //    node.ZcuName = _zcu.Name;
        //    node.ZcuType = ZCU_TYPE.STOP;

        //    ((OHTPoint)node.OutLines[0].EndPoint).ZcuName = _zcu.Name;
        //    ((OHTPoint)node.OutLines[0].EndPoint).ZcuType = ZCU_TYPE.RESET;
        //    ((OHTPoint)node.OutLines[1].EndPoint).ZcuName = _zcu.Name;
        //    ((OHTPoint)node.OutLines[1].EndPoint).ZcuType = ZCU_TYPE.RESET;
        //}
        //public void AddToZCUmembers(OHTPoint node, OHTZCU _zcu)
        //{
        //    _zcu.ToPoints.Add(node.Name, node);
        //    node.ZcuName = _zcu.Name;
        //    node.ZcuType = ZCU_TYPE.RESET;

        //    ((OHTPoint)node.InLines[0].StartPoint).ZcuName = _zcu.Name;
        //    ((OHTPoint)node.InLines[0].StartPoint).ZcuType = ZCU_TYPE.STOP;
        //    ((OHTPoint)node.InLines[1].StartPoint).ZcuName = _zcu.Name;
        //    ((OHTPoint)node.InLines[1].StartPoint).ZcuType = ZCU_TYPE.STOP;
        //}
        //public void AutoOHTSetting()
        //{
        //    List<NodeReference> insertedRefNodes;
        //    bool success = false;

        //    int cnt = 0;
        //    Point3D centerPoint;
        //    foreach (SimNode node in ModelManager.Instance.SimNodes.Values.ToArray())
        //    {
        //        if (node.GetType().Name == "OHTLine" && cnt <= 500)
        //        {
        //            if (((TransportLine)node).Length > 9000)
        //            {
        //                //if (cnt % 3 == 0)
        //                //{ cnt += 1; continue; }
        //                //if (cnt % 2 == 0)
        //                //    centerPoint = new Point3D(((TransportLine)node).PosVec3.X, ((TransportLine)node).PosVec3.Y + 1200, ((TransportLine)node).PosVec3.Z);
        //                //else
        //                //    centerPoint = new Point3D(((TransportLine)node).PosVec3.X, ((TransportLine)node).PosVec3.Y - 1200, ((TransportLine)node).PosVec3.Z);
        //                centerPoint = new Point3D(((TransportLine)node).PosVec3.X, ((TransportLine)node).PosVec3.Y, ((TransportLine)node).PosVec3.Z);

        //                ////if (pinokio3DModel1.MouseLocationSnapToCorrdinate.Y - 1700 != centerPoint.Y)
        //                ////    continue;

        //                uint idx = ((TransportLine)node).ID;
        //                success = CurrentRef.Insert_MouseUp_second(pinokio3DModel1, centerPoint, ((TransportLine)node).ID, out insertedRefNodes);
        //                //return;
        //                //insertedRefNodes = insertedObjects.ConvertAll(x => (NodeReference)x);
        //                List<SimNode> insertedSimNodes = insertedRefNodes.ConvertAll(x => x.Core);

        //                if (success && insertedRefNodes.Count > 0)
        //                {
        //                    string insertedNodeType = insertedRefNodes[0].GetType().Name;
        //                    string insertedNodeMatchType = insertedRefNodes[0].MatchingObjType;
        //                    double insertedNodeHeight = insertedRefNodes[0].Height;
        //                    foreach (NodeReference nodeReference in insertedRefNodes)
        //                    {
        //                        if (nodeReference.Core is CoupledModel)
        //                            continue;

        //                        nodeReference.FinishAddNode(pinokio3DModel1);

        //                        pinokio3DModel1.AddNodeReference(nodeReference);
        //                        if (pinokio3DModel1.Entities.Contains(nodeReference) is false)
        //                            pinokio3DModel1.Entities.Add(nodeReference);

        //                        if (nodeReference.Core != null)
        //                            nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

        //                        if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
        //                            ModelManager.Instance.AddNode(nodeReference.Core);

        //                        if (nodeReference is RefLink)
        //                            simNodeTreeList.MoveNode(simNodeTreeList.FindNodeByKeyID(nodeReference.ID), simNodeTreeList.FindNodeByKeyID((nodeReference as RefLink).FromNode.ID).ParentNode);

        //                        if (nodeReference.Core is Equipment)
        //                            FactoryManager.Instance.Eqps.Add(nodeReference.Core.ID, nodeReference.Core as Equipment);
        //                    }
        //                    ModifyNodeTreeList(insertedSimNodes);
        //                    AddUndo(eUndoRedoActionType.Add, insertedRefNodes, null, null);
        //                    RefreshEntities();

        //                    if (insertedRefNodes.Count <= 3)
        //                        PrepareInsertingRefNode(insertedNodeType, insertedNodeMatchType, insertedNodeHeight, pinokio3DModel1.MouseLocationSnapToCorrdinate);
        //                }
        //            }
        //        }
        //    }
        //    pinokio3DModel1.Entities.Regen();
        //    pinokio3DModel1.Invalidate();
        //    string ho = "HOHO";
        //}










        //public void AutoStationSetting()
        //{
        //    List<ObjectReference> insertedObjects;
        //    List<NodeReference> insertedRefNodes;
        //    NodeReference refNode = null;

        //    bool success = false;

        //    Point3D centerPoint;
        //    Point3D centerPoint2;
        //    Point3D realCenterPoint = new Point3D(0,0,0);
        //    int entityIndex = pinokio3DModel1.GetEntityUnderMouseCursor(pinokio3DModel1.MouseLocation);
        //    uint refNodeID = 0;
        //    if (entityIndex >= 0 && (NodeReference)pinokio3DModel1.Entities[entityIndex] is RefTransportLine)
        //        refNode = (NodeReference)pinokio3DModel1.Entities[entityIndex];
        //    //if (refNode == null) return;
        //    //return;
        //    foreach (SimNode node in ModelManager.Instance.SimNodes.Values.ToArray())
        //    {
        //        if (node.GetType().Name == "Equipment")
        //        {
        //            //continue;
        //            centerPoint = new Point3D(node.PosVec3.X + 1500, node.PosVec3.Y, node.PosVec3.Z);
        //            centerPoint2 = new Point3D(node.PosVec3.X - 1500, node.PosVec3.Y, node.PosVec3.Z);
        //            RefTransportLine refLine = null;
        //            if (ModelManager.Instance.SimNodes.Values.FirstOrDefault(x => x.PosVec3.X == centerPoint.X) != null)
        //            {

        //                List<SimNode> filteredEntities = ModelManager.Instance.SimNodes.Values.Where(x => x.PosVec3.X == centerPoint.X).ToList();
        //                foreach (SimNode simnmodeee in filteredEntities)
        //                {
        //                    if (simnmodeee.GetType().Name.Contains("Line"))
        //                    {
        //                        if (simnmodeee.PosVec3.Y + ((TransportLine)simnmodeee).Length / 2 > centerPoint.Y && simnmodeee.PosVec3.Y - ((TransportLine)simnmodeee).Length / 2 < centerPoint.Y)
        //                        {
        //                            refNodeID = simnmodeee.ID;
        //                            realCenterPoint = new Point3D(centerPoint.X, centerPoint.Y + 1850, centerPoint.Z);
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            else if (ModelManager.Instance.SimNodes.Values.FirstOrDefault(x => x.PosVec3.X == centerPoint2.X) != null)
        //            {

        //                List<SimNode> filteredEntities = ModelManager.Instance.SimNodes.Values.Where(x => x.PosVec3.X == centerPoint2.X).ToList();
        //                foreach (SimNode simnmodeee in filteredEntities)
        //                {
        //                    if (simnmodeee.GetType().Name.Contains("Line"))
        //                    {
        //                        if (simnmodeee.PosVec3.Y + ((TransportLine)simnmodeee).Length / 2 > centerPoint2.Y && simnmodeee.PosVec3.Y - ((TransportLine)simnmodeee).Length / 2 < centerPoint2.Y)
        //                        {
        //                            refNodeID = simnmodeee.ID;
        //                            realCenterPoint = new Point3D(centerPoint2.X, centerPoint2.Y - 1850, centerPoint2.Z);
        //                            break;
        //                        }
        //                    }
        //                }
        //            }


        //            if (refNodeID == 0) return;


        //            //centerPoint = new Point3D(((TransportLine)node).PosVec3.X, ((TransportLine)node).PosVec3.Y, ((TransportLine)node).PosVec3.Z);
        //            //centerPoint = new Point3D(((TransportLine)node).PosVec3.X, pinokio3DModel1.MouseLocationSnapToCorrdinate.Y, ((TransportLine)node).PosVec3.Z);
        //            //int idx = Convert.ToUInt32(refLine.ID);
        //            success = CurrentRef.Insert_MouseUp_second(pinokio3DModel1, realCenterPoint, refNodeID, out insertedRefNodes);

        //            // = insertedObjects.ConvertAll(x => (NodeReference)x);

        //            List<SimNode> insertedSimNodes = insertedRefNodes.ConvertAll(x => x.Core);

        //            if (success && insertedRefNodes.Count > 0)
        //            {
        //                string insertedNodeType = insertedRefNodes[0].GetType().Name;
        //                string insertedNodeMatchType = insertedRefNodes[0].MatchingObjType;
        //                double insertedNodeHeight = insertedRefNodes[0].Height;
        //                foreach (NodeReference nodeReference in insertedRefNodes)
        //                {
        //                    nodeReference.FinishAddNode(pinokio3DModel1);

        //                    pinokio3DModel1.AddNodeReference(nodeReference);
        //                    if (pinokio3DModel1.Entities.Contains(nodeReference) is false)
        //                        pinokio3DModel1.Entities.Add(nodeReference);
        //                    if (nodeReference.Core != null)
        //                        nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

        //                    if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
        //                        ModelManager.Instance.AddNode(nodeReference.Core);
        //                }
        //                ModifyNodeTreeList(insertedSimNodes);
        //                AddUndo(eUndoRedoActionType.Add, insertedRefNodes, null, null);
        //                RefreshEntities();

        //                if (insertedRefNodes.Count <= 3)
        //                    PrepareInsertingRefNode(insertedNodeType, insertedNodeMatchType, insertedNodeHeight, pinokio3DModel1.MouseLocationSnapToCorrdinate);
        //            }
        //        }
        //    }
        //    pinokio3DModel1.Entities.Regen();
        //    pinokio3DModel1.Invalidate();
        //}
        //public void AutoLinkSetting()
        //{
        //    List<ObjectReference> insertedObjects;
        //    List<NodeReference> insertedRefNodes;
        //    bool success = false;
        //    //return;
        //    Point3D centerPoint;
        //    NodeReference refNode = null;

        //    int entityIndex = pinokio3DModel1.GetEntityUnderMouseCursor(pinokio3DModel1.MouseLocation);
        //    if (entityIndex >= 0 && (NodeReference)pinokio3DModel1.Entities[entityIndex] is RefLineStation)
        //        refNode = (NodeReference)pinokio3DModel1.Entities[entityIndex];
        //    //if (refNode == null) return;
        //    foreach (SimNode node in ModelManager.Instance.SimNodes.Values.ToArray())
        //    {
        //        if (node.GetType().Name.Contains("OHTLine"))
        //        {
        //            TransportLine transportLine = node as TransportLine;
        //            if (transportLine.LineStations.Count > 0)
        //            {
        //                double xAxis = 1500;
        //                double yAxis = 450;
        //                double yAxix2 = 1850;
        //                // return;
        //                if (transportLine.Direction.Y == 1)
        //                { xAxis = -1500; yAxis = -450; yAxix2 = -1850; }
        //                SimNode eqpNode = null;
        //                Equipment toEQP = null;
        //                for (int i = 0; i < transportLine.LineStations.Count; i++)
        //                {
        //                    if (i % 2 == 0)
        //                    {
        //                        UTB fabUtb = transportLine.LineStations[i].UTB;
        //                        eqpNode = ModelManager.Instance.SimNodes.Values.FirstOrDefault(x => x.PosVec3.X == fabUtb.PosVec3.X + xAxis && x.PosVec3.Y == fabUtb.PosVec3.Y + yAxis);

        //                        if (eqpNode != null)
        //                            toEQP = eqpNode as Equipment;
        //                        else
        //                            ;
        //                        centerPoint = new Point3D(fabUtb.PosVec3.X - 200, fabUtb.PosVec3.Y - 200, fabUtb.PosVec3.Z);

        //                        success = CurrentRef.Insert_MouseUp_second(pinokio3DModel1, centerPoint, fabUtb.ID, out insertedRefNodes);

        //                        List<SimNode> insertedSimNodes = insertedRefNodes.ConvertAll(x => x.Core);

        //                        if (success && insertedRefNodes.Count > 0)
        //                        {
        //                            string insertedNodeType = insertedRefNodes[0].GetType().Name;
        //                            string insertedNodeMatchType = insertedRefNodes[0].MatchingObjType;
        //                            double insertedNodeHeight = insertedRefNodes[0].Height;
        //                            foreach (NodeReference nodeReference in insertedRefNodes)
        //                            {
        //                                nodeReference.FinishAddNode(pinokio3DModel1);

        //                                pinokio3DModel1.AddNodeReference(nodeReference);
        //                                if (pinokio3DModel1.Entities.Contains(nodeReference) is false)
        //                                    pinokio3DModel1.Entities.Add(nodeReference);
        //                                if (nodeReference.Core != null)
        //                                    nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

        //                                if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
        //                                    ModelManager.Instance.AddNode(nodeReference.Core);
        //                            }
        //                            ModifyNodeTreeList(insertedSimNodes);
        //                            AddUndo(eUndoRedoActionType.Add, insertedRefNodes, null, null);
        //                            RefreshEntities();

        //                            if (insertedRefNodes.Count <= 3)
        //                                PrepareInsertingRefNode(insertedNodeType, insertedNodeMatchType, insertedNodeHeight, pinokio3DModel1.MouseLocationSnapToCorrdinate);
        //                        }

        //                        centerPoint = new Point3D(toEQP.PosVec3.X, toEQP.PosVec3.Y, toEQP.PosVec3.Z);
        //                        success = CurrentRef.Insert_MouseUp_second(pinokio3DModel1, centerPoint, toEQP.ID, out insertedRefNodes);

        //                        insertedSimNodes = insertedRefNodes.ConvertAll(x => x.Core);

        //                        if (success && insertedRefNodes.Count > 0)
        //                        {
        //                            string insertedNodeType = insertedRefNodes[0].GetType().Name;
        //                            string insertedNodeMatchType = insertedRefNodes[0].MatchingObjType;
        //                            double insertedNodeHeight = insertedRefNodes[0].Height;
        //                            foreach (NodeReference nodeReference in insertedRefNodes)
        //                            {
        //                                nodeReference.FinishAddNode(pinokio3DModel1);

        //                                pinokio3DModel1.AddNodeReference(nodeReference);
        //                                if (pinokio3DModel1.Entities.Contains(nodeReference) is false)
        //                                    pinokio3DModel1.Entities.Add(nodeReference);
        //                                if (nodeReference.Core != null)
        //                                    nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

        //                                if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
        //                                    ModelManager.Instance.AddNode(nodeReference.Core);
        //                            }
        //                            ModifyNodeTreeList(insertedSimNodes);
        //                            AddUndo(eUndoRedoActionType.Add, insertedRefNodes, null, null);
        //                            RefreshEntities();

        //                            if (insertedRefNodes.Count <= 3)
        //                                PrepareInsertingRefNode(insertedNodeType, insertedNodeMatchType, insertedNodeHeight, pinokio3DModel1.MouseLocationSnapToCorrdinate);
        //                        }

        //                    }
        //                    else
        //                    {
        //                        UTB fabUtb = transportLine.LineStations[i].UTB;

        //                        centerPoint = new Point3D(toEQP.PosVec3.X, toEQP.PosVec3.Y, toEQP.PosVec3.Z);

        //                        success = CurrentRef.Insert_MouseUp_second(pinokio3DModel1, centerPoint, toEQP.ID, out insertedRefNodes);

        //                        List<SimNode> insertedSimNodes = insertedRefNodes.ConvertAll(x => x.Core);

        //                        if (success && insertedRefNodes.Count > 0)
        //                        {
        //                            string insertedNodeType = insertedRefNodes[0].GetType().Name;
        //                            string insertedNodeMatchType = insertedRefNodes[0].MatchingObjType;
        //                            double insertedNodeHeight = insertedRefNodes[0].Height;
        //                            foreach (NodeReference nodeReference in insertedRefNodes)
        //                            {
        //                                nodeReference.FinishAddNode(pinokio3DModel1);

        //                                pinokio3DModel1.AddNodeReference(nodeReference);
        //                                if (pinokio3DModel1.Entities.Contains(nodeReference) is false)
        //                                    pinokio3DModel1.Entities.Add(nodeReference);
        //                                if (nodeReference.Core != null)
        //                                    nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

        //                                if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
        //                                    ModelManager.Instance.AddNode(nodeReference.Core);
        //                            }
        //                            ModifyNodeTreeList(insertedSimNodes);
        //                            AddUndo(eUndoRedoActionType.Add, insertedRefNodes, null, null);
        //                            RefreshEntities();

        //                            if (insertedRefNodes.Count <= 3)
        //                                PrepareInsertingRefNode(insertedNodeType, insertedNodeMatchType, insertedNodeHeight, pinokio3DModel1.MouseLocationSnapToCorrdinate);
        //                        }


        //                        centerPoint = new Point3D(fabUtb.PosVec3.X - 200, fabUtb.PosVec3.Y - 200, fabUtb.PosVec3.Z);
        //                        success = CurrentRef.Insert_MouseUp_second(pinokio3DModel1, centerPoint, fabUtb.ID, out insertedRefNodes);

        //                        insertedSimNodes = insertedRefNodes.ConvertAll(x => x.Core);

        //                        if (success && insertedRefNodes.Count > 0)
        //                        {
        //                            string insertedNodeType = insertedRefNodes[0].GetType().Name;
        //                            string insertedNodeMatchType = insertedRefNodes[0].MatchingObjType;
        //                            double insertedNodeHeight = insertedRefNodes[0].Height;
        //                            foreach (NodeReference nodeReference in insertedRefNodes)
        //                            {
        //                                nodeReference.FinishAddNode(pinokio3DModel1);

        //                                pinokio3DModel1.AddNodeReference(nodeReference);
        //                                if (pinokio3DModel1.Entities.Contains(nodeReference) is false)
        //                                    pinokio3DModel1.Entities.Add(nodeReference);
        //                                if (nodeReference.Core != null)
        //                                    nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

        //                                if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
        //                                    ModelManager.Instance.AddNode(nodeReference.Core);
        //                            }
        //                            ModifyNodeTreeList(insertedSimNodes);
        //                            AddUndo(eUndoRedoActionType.Add, insertedRefNodes, null, null);
        //                            RefreshEntities();

        //                            if (insertedRefNodes.Count <= 3)
        //                                PrepareInsertingRefNode(insertedNodeType, insertedNodeMatchType, insertedNodeHeight, pinokio3DModel1.MouseLocationSnapToCorrdinate);
        //                        }
        //                    }
        //                }

        //            }
        //            //}
        //        }
        //    }
        //    //pinokio3DModel1.Entities.Regen();
        //    //pinokio3DModel1.Invalidate();
        //}
        //public void AutoStationForSTB()
        //{
        //    List<ObjectReference> insertedObjects;
        //    List<NodeReference> insertedRefNodes;
        //    NodeReference refNode = null;

        //    bool success = false;

        //    Point3D centerPoint;
        //    Point3D centerPoint2;
        //    Point3D realCenterPoint = new Point3D(0, 0, 0);
        //    int entityIndex = pinokio3DModel1.GetEntityUnderMouseCursor(pinokio3DModel1.MouseLocation);
        //    uint refNodeID = 0;
        //    if (entityIndex >= 0 && (NodeReference)pinokio3DModel1.Entities[entityIndex] is RefTransportLine)
        //        refNode = (NodeReference)pinokio3DModel1.Entities[entityIndex];
        //    //if (refNode == null) return;
        //    //return;
        //    foreach (SimNode node in ModelManager.Instance.SimNodes.Values.ToArray())
        //    {
        //        if (node.GetType().Name == "OHTLine")
        //        {
        //            if(((TransportLine)node).LineStations.Count > 1)
        //            {
        //                double lineLen = ((TransportLine)node).Length;
        //                double firstStationLen = ((TransportLine)node).LineStations[0].Length;
        //                double lastStationLen = ((TransportLine)node).LineStations.Last().Length;
        //                double startPos = firstStationLen;
        //                while (startPos > 0)
        //                {
        //                    startPos -= 1000;
        //                    if (startPos <= 600)
        //                        break;

        //                    double distanceToNextStation = startPos;

        //                    if (node.Direction.Y == 1)
        //                    {
        //                        realCenterPoint = new Point3D(((TransportLine)node).LineStations[0].PosVec3.X, ((TransportLine)node).StartPoint.PosVec3.Y - distanceToNextStation, ((TransportLine)node).LineStations[0].PosVec3.Z);
        //                    }
        //                    else
        //                    {
        //                        realCenterPoint = new Point3D(((TransportLine)node).LineStations[0].PosVec3.X, ((TransportLine)node).StartPoint.PosVec3.Y + distanceToNextStation, ((TransportLine)node).LineStations[0].PosVec3.Z);
        //                    }

        //                    success = CurrentRef.Insert_MouseUp_second(pinokio3DModel1, realCenterPoint, node.ID, out insertedRefNodes);
        //                    List<SimNode> insertedSimNodes = insertedRefNodes.ConvertAll(x => x.Core);

        //                    if (success && insertedRefNodes.Count > 0)
        //                    {
        //                        string insertedNodeType = insertedRefNodes[0].GetType().Name;
        //                        string insertedNodeMatchType = insertedRefNodes[0].MatchingObjType;
        //                        double insertedNodeHeight = insertedRefNodes[0].Height;
        //                        foreach (NodeReference nodeReference in insertedRefNodes)
        //                        {
        //                            nodeReference.FinishAddNode(pinokio3DModel1);

        //                            pinokio3DModel1.AddNodeReference(nodeReference);
        //                            if (pinokio3DModel1.Entities.Contains(nodeReference) is false)
        //                                pinokio3DModel1.Entities.Add(nodeReference);
        //                            if (nodeReference.Core != null)
        //                                nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

        //                            if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
        //                                ModelManager.Instance.AddNode(nodeReference.Core);
        //                        }
        //                        ModifyNodeTreeList(insertedSimNodes);
        //                        AddUndo(eUndoRedoActionType.Add, insertedRefNodes, null, null);
        //                        RefreshEntities();

        //                        if (insertedRefNodes.Count <= 3)
        //                            PrepareInsertingRefNode(insertedNodeType, insertedNodeMatchType, insertedNodeHeight, pinokio3DModel1.MouseLocationSnapToCorrdinate);
        //                    }
        //                }
        //                startPos = lastStationLen;
        //                while (startPos < lineLen)
        //                {
        //                    startPos += 1000;
        //                    if (lineLen - startPos <= 600)
        //                        break;

        //                    double distanceToNextStation = startPos;

        //                    if (node.Direction.Y == 1)
        //                    {
        //                        realCenterPoint = new Point3D(((TransportLine)node).LineStations[0].PosVec3.X, ((TransportLine)node).StartPoint.PosVec3.Y - distanceToNextStation, ((TransportLine)node).LineStations[0].PosVec3.Z);
        //                    }
        //                    else
        //                    {
        //                        realCenterPoint = new Point3D(((TransportLine)node).LineStations[0].PosVec3.X, ((TransportLine)node).StartPoint.PosVec3.Y + distanceToNextStation, ((TransportLine)node).LineStations[0].PosVec3.Z);
        //                    }

        //                    success = CurrentRef.Insert_MouseUp_second(pinokio3DModel1, realCenterPoint, node.ID, out insertedRefNodes);
        //                    List<SimNode> insertedSimNodes = insertedRefNodes.ConvertAll(x => x.Core);

        //                    if (success && insertedRefNodes.Count > 0)
        //                    {
        //                        string insertedNodeType = insertedRefNodes[0].GetType().Name;
        //                        string insertedNodeMatchType = insertedRefNodes[0].MatchingObjType;
        //                        double insertedNodeHeight = insertedRefNodes[0].Height;
        //                        foreach (NodeReference nodeReference in insertedRefNodes)
        //                        {
        //                            nodeReference.FinishAddNode(pinokio3DModel1);

        //                            pinokio3DModel1.AddNodeReference(nodeReference);
        //                            if (pinokio3DModel1.Entities.Contains(nodeReference) is false)
        //                                pinokio3DModel1.Entities.Add(nodeReference);
        //                            if (nodeReference.Core != null)
        //                                nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

        //                            if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
        //                                ModelManager.Instance.AddNode(nodeReference.Core);
        //                        }
        //                        ModifyNodeTreeList(insertedSimNodes);
        //                        AddUndo(eUndoRedoActionType.Add, insertedRefNodes, null, null);
        //                        RefreshEntities();

        //                        if (insertedRefNodes.Count <= 3)
        //                            PrepareInsertingRefNode(insertedNodeType, insertedNodeMatchType, insertedNodeHeight, pinokio3DModel1.MouseLocationSnapToCorrdinate);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    pinokio3DModel1.Entities.Regen();
        //    pinokio3DModel1.Invalidate();
        //}
        //public void AutoSTBSetting()
        //{
        //    List<ObjectReference> insertedObjects;
        //    List<NodeReference> insertedRefNodes;
        //    bool success = false;
        //    //return;
        //    Point3D centerPoint;
        //    NodeReference refNode = null;

        //    int entityIndex = pinokio3DModel1.GetEntityUnderMouseCursor(pinokio3DModel1.MouseLocation);
        //    if (entityIndex >= 0 && (NodeReference)pinokio3DModel1.Entities[entityIndex] is RefLineStation)
        //        refNode = (NodeReference)pinokio3DModel1.Entities[entityIndex];
        //    if (refNode == null) return;
        //    foreach (SimNode node in ModelManager.Instance.SimNodes.Values.ToArray())
        //    {
        //        if (node.GetType().Name.Contains("Station"))
        //        {
        //            //if (node.PosVec3.Y == refNode.Core.PosVec3.Y)
        //            //{
        //            if (((LineStation)node).UTB != null)
        //                continue;
        //            centerPoint = new Point3D(node.PosVec3.X, node.PosVec3.Y, node.PosVec3.Z);
        //            int idx = Convert.ToInt32(node.ID);
        //            success = CurrentRef.Insert_MouseUp_second(pinokio3DModel1, centerPoint, node.ID, out insertedRefNodes);

        //            // = insertedObjects.ConvertAll(x => (NodeReference)x);

        //            List<SimNode> insertedSimNodes = insertedRefNodes.ConvertAll(x => x.Core);

        //            if (success && insertedRefNodes.Count > 0)
        //            {
        //                string insertedNodeType = insertedRefNodes[0].GetType().Name;
        //                string insertedNodeMatchType = insertedRefNodes[0].MatchingObjType;
        //                double insertedNodeHeight = insertedRefNodes[0].Height;
        //                foreach (NodeReference nodeReference in insertedRefNodes)
        //                {
        //                    nodeReference.FinishAddNode(pinokio3DModel1);

        //                    pinokio3DModel1.AddNodeReference(nodeReference);
        //                    if (pinokio3DModel1.Entities.Contains(nodeReference) is false)
        //                        pinokio3DModel1.Entities.Add(nodeReference);
        //                    if (nodeReference.Core != null)
        //                        nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

        //                    if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
        //                        ModelManager.Instance.AddNode(nodeReference.Core);
        //                }
        //                ModifyNodeTreeList(insertedSimNodes);
        //                AddUndo(eUndoRedoActionType.Add, insertedRefNodes, null, null);
        //                RefreshEntities();

        //                if (insertedRefNodes.Count <= 3)
        //                    PrepareInsertingRefNode(insertedNodeType, insertedNodeMatchType, insertedNodeHeight, pinokio3DModel1.MouseLocationSnapToCorrdinate);
        //            }
        //            //}
        //        }
        //    }
        //    pinokio3DModel1.Entities.Regen();
        //    pinokio3DModel1.Invalidate();
        //}
        //public void AutoUTBSetting()
        //{
        //    List<ObjectReference> insertedObjects;
        //    List<NodeReference> insertedRefNodes;
        //    bool success = false;
        //    //return;
        //    Point3D centerPoint;
        //    NodeReference refNode = null;

        //    int entityIndex = pinokio3DModel1.GetEntityUnderMouseCursor(pinokio3DModel1.MouseLocation);
        //    if (entityIndex >= 0 && (NodeReference)pinokio3DModel1.Entities[entityIndex] is RefLineStation)
        //        refNode = (NodeReference)pinokio3DModel1.Entities[entityIndex];
        //    if (refNode == null) return;
        //    foreach (SimNode node in ModelManager.Instance.SimNodes.Values.ToArray())
        //    {
        //        if (node.GetType().Name.Contains("Station"))
        //        {
        //            //if (node.PosVec3.Y == refNode.Core.PosVec3.Y)
        //            //{
        //            centerPoint = new Point3D(node.PosVec3.X, node.PosVec3.Y, node.PosVec3.Z);
        //            int idx = Convert.ToInt32(node.ID);
        //            success = CurrentRef.Insert_MouseUp_second(pinokio3DModel1, centerPoint, node.ID, out insertedRefNodes);

        //            // = insertedObjects.ConvertAll(x => (NodeReference)x);

        //            List<SimNode> insertedSimNodes = insertedRefNodes.ConvertAll(x => x.Core);

        //            if (success && insertedRefNodes.Count > 0)
        //            {
        //                string insertedNodeType = insertedRefNodes[0].GetType().Name;
        //                string insertedNodeMatchType = insertedRefNodes[0].MatchingObjType;
        //                double insertedNodeHeight = insertedRefNodes[0].Height;
        //                foreach (NodeReference nodeReference in insertedRefNodes)
        //                {
        //                    nodeReference.FinishAddNode(pinokio3DModel1);

        //                    pinokio3DModel1.AddNodeReference(nodeReference);
        //                    if (pinokio3DModel1.Entities.Contains(nodeReference) is false)
        //                        pinokio3DModel1.Entities.Add(nodeReference);
        //                    if (nodeReference.Core != null)
        //                        nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

        //                    if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
        //                        ModelManager.Instance.AddNode(nodeReference.Core);
        //                }
        //                ModifyNodeTreeList(insertedSimNodes);
        //                AddUndo(eUndoRedoActionType.Add, insertedRefNodes, null, null);
        //                RefreshEntities();

        //                if (insertedRefNodes.Count <= 3)
        //                    PrepareInsertingRefNode(insertedNodeType, insertedNodeMatchType, insertedNodeHeight, pinokio3DModel1.MouseLocationSnapToCorrdinate);
        //            }
        //        //}
        //        }
        //    }
        //    pinokio3DModel1.Entities.Regen();
        //    pinokio3DModel1.Invalidate();
        //}
        //public void AutoEQPSetting()
        //{
        //    List<ObjectReference> insertedObjects;
        //    List<NodeReference> insertedRefNodes;
        //    bool success = false;

        //    Point3D centerPoint;
        //    NodeReference refNode = null;

        //    int entityIndex = pinokio3DModel1.GetEntityUnderMouseCursor(pinokio3DModel1.MouseLocation);
        //    if (entityIndex >= 0 && (NodeReference)pinokio3DModel1.Entities[entityIndex] is RefTransportLine)
        //        refNode = (NodeReference)pinokio3DModel1.Entities[entityIndex];
        //    if (refNode == null) return;
        //    SimNode line = ModelManager.Instance.GetSimNodebyID(refNode.ID);
        //    double xAxis = 0;
        //    double yAxis = 0;
        //    if (line.Direction.Y == -1) { xAxis = +1500; yAxis = +1150; } //-1150; 250 -2450
        //    else if (line.Direction.Y == 1) { xAxis = -1500; yAxis = -1150; } //1150; 2550 -150

        //    //SimNode line = ModelManager.Instance.GetSimNodebyID(refNode.ID);

        //    centerPoint = new Point3D(((TransportLine)line).PosVec3.X + xAxis, pinokio3DModel1.MouseLocationSnapToCorrdinate.Y + yAxis, pinokio3DModel1.MouseLocationSnapToCorrdinate.Z);
        //    success = CurrentRef.Insert_MouseUp_second(pinokio3DModel1, centerPoint, ((TransportLine)line).ID, out insertedRefNodes);
        //    List<SimNode> insertedSimNodes = insertedRefNodes.ConvertAll(x => x.Core);

        //    if (success && insertedRefNodes.Count > 0)
        //    {
        //        string insertedNodeType = insertedRefNodes[0].GetType().Name;
        //        string insertedNodeMatchType = insertedRefNodes[0].MatchingObjType;
        //        double insertedNodeHeight = insertedRefNodes[0].Height;
        //        foreach (NodeReference nodeReference in insertedRefNodes)
        //        {
        //            nodeReference.FinishAddNode(pinokio3DModel1);

        //            pinokio3DModel1.AddNodeReference(nodeReference);
        //            if (pinokio3DModel1.Entities.Contains(nodeReference) is false)
        //                pinokio3DModel1.Entities.Add(nodeReference);
        //            if (nodeReference.Core != null)
        //                nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

        //            if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
        //                ModelManager.Instance.AddNode(nodeReference.Core);

        //            if (nodeReference.Core is Equipment)
        //                FactoryManager.Instance.Eqps.Add(nodeReference.Core.ID, nodeReference.Core as Equipment);
        //        }
        //        ModifyNodeTreeList(insertedSimNodes);
        //        AddUndo(eUndoRedoActionType.Add, insertedRefNodes, null, null);
        //        RefreshEntities();

        //        if (insertedRefNodes.Count <= 3)
        //            PrepareInsertingRefNode(insertedNodeType, insertedNodeMatchType, insertedNodeHeight, pinokio3DModel1.MouseLocationSnapToCorrdinate);
        //    }
        //    //foreach (SimNode node in ModelManager.Instance.SimNodes.Values.ToArray())
        //    //{
        //    //    if (node.GetType().Name == "OHTLine"/* && node.ID == refNode.ID*/)
        //    //    {
        //    //        if (((TransportLine)node).Length > 5000 && (node.Direction.Y == 1 || node.Direction.Y == -1)/* && pinokio3DModel1.MouseLocationSnapToCorrdinate.Y ==  ((TransportLine)node).PosVec3.Y*/)
        //    //        {
        //    //            double xAxis = 0;
        //    //            double yAxis = 0;
        //    //            if (node.Direction.Y == -1) { xAxis = -1500; yAxis = 250; } //-1150; 250 -2450
        //    //            else if (node.Direction.Y == 1) { xAxis = +1500; yAxis = 2550; } //1150; 2550 -150
        //    //            //centerPoint = new Point3D(((TransportLine)node).PosVec3.X + xAxis, ((TransportLine)node).PosVec3.Y + yAxis, pinokio3DModel1.MouseLocationSnapToCorrdinate.Z);
        //    //            centerPoint = new Point3D(((TransportLine)node).PosVec3.X + xAxis, pinokio3DModel1.MouseLocationSnapToCorrdinate.Y - 1150, pinokio3DModel1.MouseLocationSnapToCorrdinate.Z);
        //    //            int idx = Convert.ToInt32(((TransportLine)node).ID);
        //    //            success = CurrentRef.Insert_MouseUp_second(pinokio3DModel1, centerPoint, ((TransportLine)node).ID, out insertedRefNodes);

        //    //            List<SimNode> insertedSimNodes = insertedRefNodes.ConvertAll(x => x.Core);

        //    //            if (success && insertedRefNodes.Count > 0)
        //    //            {
        //    //                string insertedNodeType = insertedRefNodes[0].GetType().Name;
        //    //                string insertedNodeMatchType = insertedRefNodes[0].MatchingObjType;
        //    //                double insertedNodeHeight = insertedRefNodes[0].Height;
        //    //                foreach (NodeReference nodeReference in insertedRefNodes)
        //    //                {
        //    //                    nodeReference.FinishAddNode(pinokio3DModel1);

        //    //                    pinokio3DModel1.AddNodeReference(nodeReference);
        //    //                    if (pinokio3DModel1.Entities.Contains(nodeReference) is false)
        //    //                        pinokio3DModel1.Entities.Add(nodeReference);
        //    //                    if (nodeReference.Core != null)
        //    //                        nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

        //    //                    if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
        //    //                        ModelManager.Instance.AddNode(nodeReference.Core);

        //    //                    if (nodeReference.Core is Equipment)
        //    //                        FactoryManager.Instance.Eqps.Add(nodeReference.Core.ID, nodeReference.Core as Equipment);
        //    //                }
        //    //                ModifyNodeTreeList(insertedSimNodes);
        //    //                AddUndo(eUndoRedoActionType.Add, insertedRefNodes, null, null);
        //    //                RefreshEntities();

        //    //                if (insertedRefNodes.Count <= 3)
        //    //                    PrepareInsertingRefNode(insertedNodeType, insertedNodeMatchType, insertedNodeHeight, pinokio3DModel1.MouseLocationSnapToCorrdinate);
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    pinokio3DModel1.Entities.Regen();
        //    pinokio3DModel1.Invalidate();
        //}
    }
}