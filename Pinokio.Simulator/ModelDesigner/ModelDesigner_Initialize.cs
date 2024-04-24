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
using Simulation.Engine;
using DevExpress.XtraBars.Docking;
using Logger;
using System.Reflection;
using Pinokio.Animation;
using Pinokio.Animation.User;
using Pinokio.Model.Base;
using Pinokio.Designer.DataClass;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors.Repository;

namespace Pinokio.Designer
{
    public partial class ModelDesigner : UserControl
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModelDesigner_Load(object sender, EventArgs e)
        {
            CreateRotationArrowsDirections();
            InitializeTreeListLineStatus();
        }

        private void InitializeDB()
        {
            new AccessDB();
        }

        private void InitializeFloorPlan(List<FloorPlan> floorPlans)
        {
            _isFloorSetting = true;

            if (_floorForm.LstFloorPlan.Count > 0)
            {
                pinokio3DModel1.SelectedFloorID = floorPlans[floorPlans.Count - 1].Floor.ID;
                pinokio3DModel1.SelectedFloorHeight = floorPlans[floorPlans.Count - 1].FloorBottom;
            }

            initiallzeFloor(floorPlans);
        }

        public void initiallzeFloor(List<FloorPlan> floorPlans)
        {
            List<Entity> lstPic = pinokio3DModel1.Entities.ToList().FindAll(x => x is Picture);
            for (int i = 0; i < floorPlans.Count(); i++)
            {
                pinokio3DModel1.Grids[i].Plane = new Plane(new Point3D(0D, 0D, floorPlans[i].FloorBottom), new devDept.Geometry.Vector3D(0D, 0D, 1D));

                Picture pic = lstPic.Find(x => Convert.ToInt32((x as Picture).LayerName) == i) as Picture;
                if (pic != null)
                {
                    pic.Translate(0, 0, -pic.Plane.Origin.Z);
                    pic.Translate(0, 0, pinokio3DModel1.Grids[i].Plane.Origin.Z);
                }
            }
        }

        private void Initialize3DModel()
        {
            devDept.Eyeshot.CancelToolBarButton cancelToolBarButton3 = new devDept.Eyeshot.CancelToolBarButton("Cancel", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ProgressBar progressBar3 = new devDept.Eyeshot.ProgressBar(devDept.Eyeshot.ProgressBar.styleType.Circular, 0, "Idle", System.Drawing.Color.Black, System.Drawing.Color.Transparent, System.Drawing.Color.Green, 1D, true, cancelToolBarButton3, false, 0.1D, true);
            devDept.Graphics.BackgroundSettings backgroundSettings3 = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.LinearGradient, System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245))))), System.Drawing.Color.DodgerBlue, System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(163)))), ((int)(((byte)(210))))), 0.75D, null, devDept.Graphics.colorThemeType.Auto, 0.33D);
            devDept.Eyeshot.Camera camera3 = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(0D, 0D, 0D), 5000D, new devDept.Geometry.Quaternion(0.49999999999999989D, 0.5D, 0.5D, 0.50000000000000011D), devDept.Graphics.projectionType.Orthographic, 500D, 0.05D, false, 1D);
            devDept.Eyeshot.HomeToolBarButton homeToolBarButton3 = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton3 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton3 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton3 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton3 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.RotateToolBarButton rotateToolBarButton3 = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton3 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.ToolBar toolBar3 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopCenter, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(homeToolBarButton3)),
            ((devDept.Eyeshot.ToolBarButton)(magnifyingGlassToolBarButton3)),
            ((devDept.Eyeshot.ToolBarButton)(zoomWindowToolBarButton3)),
            ((devDept.Eyeshot.ToolBarButton)(zoomToolBarButton3)),
            ((devDept.Eyeshot.ToolBarButton)(panToolBarButton3)),
            ((devDept.Eyeshot.ToolBarButton)(rotateToolBarButton3)),
            ((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton3))});


            devDept.Eyeshot.RotateSettings rotateSettings3 = new devDept.Eyeshot.RotateSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.None), 10D, true, 1D, devDept.Eyeshot.rotationType.Trackball, devDept.Eyeshot.rotationCenterType.CursorLocation, new devDept.Geometry.Point3D(0D, 0D, 0D), false);
            devDept.Eyeshot.ZoomSettings zoomSettings3 = new devDept.Eyeshot.ZoomSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Shift), 25, true, devDept.Eyeshot.zoomStyleType.AtCursorLocation, false, 1D, System.Drawing.Color.Empty, devDept.Eyeshot.Camera.perspectiveFitType.Accurate, false, 10, true);
            devDept.Eyeshot.PanSettings panSettings3 = new devDept.Eyeshot.PanSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Ctrl), 25, true);
            devDept.Eyeshot.NavigationSettings navigationSettings3 = new devDept.Eyeshot.NavigationSettings(devDept.Eyeshot.Camera.navigationType.Examine, new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Left, devDept.Eyeshot.modifierKeys.None), new devDept.Geometry.Point3D(-1000D, -1000D, -1000D), new devDept.Geometry.Point3D(1000D, 1000D, 1000D), 8D, 50D, 50D);
            devDept.Eyeshot.Viewport.SavedViewsManager savedViewsManager3 = new devDept.Eyeshot.Viewport.SavedViewsManager(8);

            devDept.Eyeshot.Viewport viewport3 = new devDept.Eyeshot.Viewport(new System.Drawing.Point(0, 0), new System.Drawing.Size(1181, 1517), backgroundSettings3, camera3, new devDept.Eyeshot.ToolBar[] {
            toolBar3}, devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[] { }, false, rotateSettings3, zoomSettings3, panSettings3, navigationSettings3, savedViewsManager3, devDept.Eyeshot.viewType.Trimetric
            );

            devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon3 = new devDept.Eyeshot.CoordinateSystemIcon(System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.OrangeRed, "Origin", "X", "Y", "Z", true, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, false);
            devDept.Eyeshot.OriginSymbol originSymbol3 = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", true, null, false);

            devDept.Eyeshot.ViewCubeIcon viewCubeIcon3 = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, true, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(147))))), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), 'S', 'N', 'W', 'E', true, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false);


            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.pinokio3DModel1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(0, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(1181, 1517);
            this.dockPanel1_Container.TabIndex = 0;

            coordinateSystemIcon3.LabelFont = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            viewport3.CoordinateSystemIcon = coordinateSystemIcon3;
            viewport3.Legends = new devDept.Eyeshot.Legend[0];
            originSymbol3.LabelFont = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            viewport3.OriginSymbol = originSymbol3;
            viewCubeIcon3.Font = null;
            viewCubeIcon3.InitialRotation = new devDept.Geometry.Quaternion(0D, 0D, 0D, 1D);
            viewport3.ViewCubeIcon = viewCubeIcon3;

            pinokio3DModel1.Viewports.Add(viewport3);

            // 
            // pinokio3DModel1
            // 
            pinokio3DModel1.AllowDrop = true;
            pinokio3DModel1.Cursor = System.Windows.Forms.Cursors.Default;
            pinokio3DModel1.Dock = System.Windows.Forms.DockStyle.Fill;
            pinokio3DModel1.Location = new System.Drawing.Point(0, 0);
            pinokio3DModel1.Name = "pinokio3DModel1";
            pinokio3DModel1.ProgressBar = progressBar3;
            pinokio3DModel1.Size = new System.Drawing.Size(1181, 1517);
            pinokio3DModel1.TabIndex = 0;
            pinokio3DModel1.Text = "pinokio3DModel1";
            pinokio3DModel1.Units = devDept.Geometry.linearUnitsType.Millimeters;
            pinokio3DModel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pinokio3DModel1_MouseDown);
            pinokio3DModel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pinokio3DModel1_MouseMove);
            pinokio3DModel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pinokio3DModel1_MouseUp);
            pinokio3DModel1.KeyDown += new KeyEventHandler(this.pinokio3DModel1_KeyDown);
            pinokio3DModel1.InitialView = viewType.Top;
        }

        private void Initialize3DOptions()
        {
            //TODO devDept: Now you need to pass through the Model.ActiveViewport property to get/set the active viewport origin symbol.
            pinokio3DModel1.OriginSymbol.Visible = false;
            pinokio3DModel1.CoordinateSystemIcon.Visible = true;
            pinokio3DModel1.Rendered.PlanarReflections = false;
            pinokio3DModel1.ViewCubeIcon.Visible = true;
            pinokio3DModel1.PlanarShadowOpacity = 1;
            pinokio3DModel1.DisplayMode = displayType.Shaded;
            pinokio3DModel1.Background.TopColor = Color.White;
            pinokio3DModel1.Background.BottomColor = Color.White;
            pinokio3DModel1.AntiAliasing = false;
            pinokio3DModel1.AskForAntiAliasing = false;
            //pinokio3DModel1.MinimumFramerate = 20;
            pinokio3DModel1.Turbo.MaxComplexity = 100000;
//            pinokio3DModel1.ActiveViewport.SmallSizeRatioMoving = 0.1;
//            pinokio3DModel1.ActiveViewport.SmallSizeRatioStill = 0.1;
            pinokio3DModel1.Rendered.ShowInternalWires = false;
            pinokio3DModel1.Rendered.ShadowMode = shadowType.None;
            pinokio3DModel1.Shaded.ShowEdges = false;
            pinokio3DModel1.Shaded.ShowInternalWires = false;
            pinokio3DModel1.Shaded.ShadowMode = shadowType.None;
            pinokio3DModel1.Shaded.EdgeThickness = 100000000;
            pinokio3DModel1.Wireframe.ShowEdges = false;
            pinokio3DModel1.Wireframe.ShowInternalWires = false;
            pinokio3DModel1.Wireframe.EdgeThickness = 10000000;
            pinokio3DModel1.Rendered.EdgeThickness = 10000000;
            pinokio3DModel1.Rendered.ShadowMode = shadowType.None;
            pinokio3DModel1.Rendered.ShowEdges = false;
            pinokio3DModel1.ForceHardwareAcceleration = true;
            pinokio3DModel1.Wireframe.SilhouettesDrawingMode = silhouettesDrawingType.Never;
            pinokio3DModel1.Shaded.SilhouettesDrawingMode = silhouettesDrawingType.Never;
            pinokio3DModel1.Rendered.SilhouettesDrawingMode = silhouettesDrawingType.Never;
            pinokio3DModel1.Flat.SilhouettesDrawingMode = silhouettesDrawingType.Never;
            pinokio3DModel1.HiddenLines.SilhouettesDrawingMode = silhouettesDrawingType.Never;
            pinokio3DModel1.Rendered.ShadowMode = shadowType.None;
            pinokio3DModel1.Rendered.RealisticShadowQuality = realisticShadowQualityType.Low;
            pinokio3DModel1.Rendered.PlanarReflections = false;
            pinokio3DModel1.Rendered.PlanarReflectionsIntensity = 1;
            pinokio3DModel1.Entities.AsParallel();
            pinokio3DModel1.ShowFps = true;
            pinokio3DModel1.WaitCursorMode = waitCursorType.Never;
            //pinokio3DModel1.AmbientOcclusion.Enabled = false;
            //pinokio3DModel1.EnableAmbientOcclusion = false;
            pinokio3DModel1.Invalidate();

        }

        private void InitializeLight()
        {
            // 1
            pinokio3DModel1.Light1.Active = true;

            pinokio3DModel1.Light1.Stationary = true;

            // sets the Spot Exponent value (used only in spot light)
            pinokio3DModel1.Light1.SpotExponent = (10 < 128) ? 10 : 128;

            // sets the Linear Attenuation value (used only in spot light)
            pinokio3DModel1.Light1.LinearAttenuation = 0.01;

            // sets the Angle value (used only in spot light)
            pinokio3DModel1.Light1.SpotHalfAngle = Utility.DegToRad(1);

            // sets if YieldShadow is active (only one light at time)
            pinokio3DModel1.Light1.YieldShadow = false;

            // sets the start Position of the light (used only in non-directional light)
            pinokio3DModel1.Light1.Position = new Point3D(5, -15, 10);
            pinokio3DModel1.Light1.Type = lightType.Directional;
            // sets the direction of the light (used only in spot and directional light)

            pinokio3DModel1.Light1.Direction = new Vector3D(1, 1, -1);
            pinokio3DModel1.Light1.Direction.Normalize();

            // 2
            pinokio3DModel1.Light2.Active = true;

            pinokio3DModel1.Light2.Stationary = true;

            // sets the Spot Exponent value (used only in spot light)
            pinokio3DModel1.Light2.SpotExponent = (10 < 128) ? 10 : 128;

            // sets the Linear Attenuation value (used only in spot light)
            pinokio3DModel1.Light2.LinearAttenuation = 0.01;

            // sets the Angle value (used only in spot light)
            pinokio3DModel1.Light2.SpotHalfAngle = Utility.DegToRad(1);

            // sets if YieldShadow is active (only one light at time)
            pinokio3DModel1.Light2.YieldShadow = false;

            // sets the start Position of the light (used only in non-directional light)
            pinokio3DModel1.Light2.Position = new Point3D(-5, -15, 10);
            pinokio3DModel1.Light2.Type = lightType.Directional;
            // sets the direction of the light (used only in spot and directional light)

            pinokio3DModel1.Light2.Direction = new Vector3D(-1, 0.5, -1);
            pinokio3DModel1.Light2.Direction.Normalize();

            // 3
            pinokio3DModel1.Light3.Active = true;

            pinokio3DModel1.Light3.Stationary = true;

            // sets the Spot Exponent value (used only in spot light)
            pinokio3DModel1.Light3.SpotExponent = (10 < 128) ? 10 : 128;

            // sets the Linear Attenuation value (used only in spot light)
            pinokio3DModel1.Light3.LinearAttenuation = 0.01;

            // sets the Angle value (used only in spot light)
            pinokio3DModel1.Light3.SpotHalfAngle = Utility.DegToRad(1);

            // sets if YieldShadow is active (only one light at time)
            pinokio3DModel1.Light3.YieldShadow = false;

            // sets the start Position of the light (used only in non-directional light)
            pinokio3DModel1.Light3.Position = new Point3D(5, 15, 10);
            pinokio3DModel1.Light3.Type = lightType.Directional;
            // sets the direction of the light (used only in spot and directional light)

            pinokio3DModel1.Light3.Direction = new Vector3D(-1, 0.5, 11);
            pinokio3DModel1.Light3.Direction.Normalize();


            pinokio3DModel1.Light4.Active = false;
            pinokio3DModel1.Light5.Active = false;
            pinokio3DModel1.Light6.Active = false;
            pinokio3DModel1.Light7.Active = false;
            pinokio3DModel1.Light8.Active = false;

        }

        public void VisibleToolbar(bool bVisible)
        {
            this.pinokio3DModel1.OriginSymbol.Visible = bVisible;
            this.pinokio3DModel1.CoordinateSystemIcon.Visible = bVisible;
            this.pinokio3DModel1.Grid.Visible = bVisible;

            this.dockPanelInsertedSimNodes.Visibility = bVisible == true ? DockVisibility.Visible : DockVisibility.Hidden;
            this.dockPanelSimNodeProperties.Visibility = bVisible == true ? DockVisibility.Visible : DockVisibility.Hidden;
        }

        private bool IsInsertNodeReference(Type t)
        {
            if (BaseUtill.IsSameBaseType(t, typeof(NodeReference)))
            {
                FieldInfo fieldInfo = t.GetField("IsInserted");
                if (fieldInfo != null && (bool)fieldInfo.GetValue(null))
                {
                    ConstructorInfo[] cs = t.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
                    foreach (ConstructorInfo c2 in cs)
                    {
                        if (c2.GetParameters().Length == 1)
                        {
                            if (c2.GetParameters()[0].ParameterType.Equals(typeof(string)))
                            {
                                return true;

                            }
                        }
                    }
                }
            }
            return false;
        }
        private bool IsInsertCoupledModel(Type t)
        {
            if (BaseUtill.IsSameBaseType(t, typeof(CoupledModel)))
            {
                FieldInfo fieldInfo = t.GetField("IsInserted");
                if (fieldInfo != null && (bool)fieldInfo.GetValue(null))
                {
                    ConstructorInfo[] cs = t.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
                    foreach (ConstructorInfo c2 in cs)
                    {
                        if (c2.GetParameters().Length == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private void InitializeInsertTreeNodes()
        {
            try
            {
                List<InsertTreeItem> insertedItems = new List<InsertTreeItem>();

                List<string> nodeTypeList = new List<string>();
                foreach (Type type in TypeDefine.SimNodeTypes)
                    nodeTypeList.Add(type.Name);

                Assembly a = Assembly.LoadWithPartialName("Pinokio.Animation");
                if (a != null)
                {
                    List<Type> ts = a.GetTypes().ToList();
                    for (int i = 0; i < ts.Count; i++)
                    {
                        Type t = ts[i];
                        if (IsInsertNodeReference(t))
                        {
                            string category = RefTypeDefine.GetCategoryType(t);
                            InsertTreeItem stdItem = null;
                            var existingIStdItem = insertedItems.FirstOrDefault(item => item.Category.Contains(RefTypeDefine.GetCategoryType(t)));
                            if (existingIStdItem != null)
                            {
                                stdItem = existingIStdItem;
                            }
                            else
                            {
                                stdItem = new InsertTreeItem(category, 0);
                                insertedItems.Add(stdItem);
                            }

                            string name = t.Name.Remove(0, 3);
                            List<string> value = RefTypeDefine.GetUsableSimNodeTypes(t);

                            if (value.Contains(name))
                            {
                                
                                InsertNodeTreeItem item = new InsertNodeTreeItem(category, name, stdItem.ID, name, BaseUtill.GetInitialHeight(t), FindestateImageIndex(name));
                                insertedItems.Add(item);
                            }
                            else
                            {
                                InsertNodeTreeItem item = new InsertNodeTreeItem(category, name, stdItem.ID, value[0], BaseUtill.GetInitialHeight(t), FindestateImageIndex(name));
                                insertedItems.Add(item);
                            }
                        }
                    }
                }

                a = Assembly.LoadWithPartialName("Pinokio.Animation.User");

                if (a != null)
                {
                    List<Type> ts = a.GetTypes().ToList();
                    for (int i = 0; i < ts.Count; i++)
                    {
                        Type t = ts[i];
                        if (IsInsertNodeReference(t))
                        {
                            string category = RefTypeDefine.GetCategoryType(t);
                            InsertTreeItem stdItem = null;
                            var existingIStdItem = insertedItems.FirstOrDefault(item => item.Category.Contains(category));
                            if (existingIStdItem != null)
                            {
                                stdItem = existingIStdItem;
                            }
                            else
                            {
                                stdItem = new InsertTreeItem(RefTypeDefine.GetCategoryType(t), 0);
                                insertedItems.Add(stdItem);
                            }

                            string name = t.Name.Remove(0, 3);
                            List<string> value = RefTypeDefine.GetUsableSimNodeTypes(t);

                            if (value.Contains(name))
                            {

                                InsertNodeTreeItem item = new InsertNodeTreeItem(category, name, stdItem.ID, name, BaseUtill.GetInitialHeight(t), FindestateImageIndex(name));
                                insertedItems.Add(item);
                            }
                            else
                            {
                                InsertNodeTreeItem item = new InsertNodeTreeItem(category, name, stdItem.ID, value[0], BaseUtill.GetInitialHeight(t), FindestateImageIndex(name));
                                insertedItems.Add(item);
                            }
                        }
                    }
                }



                RepositoryItemComboBox comboBox = new RepositoryItemComboBox();
                comboBox.Items.AddRange(nodeTypeList.ToArray());
                comboBox.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                
                this.treeListInsertNodeTree.DataSource = insertedItems;
                this.treeListInsertNodeTree.RefreshDataSource();
                this.treeListInsertNodeTree.RepositoryItems.Add(comboBox);
                this.treeListInsertNodeTree.Columns["NodeType"].ColumnEdit = comboBox;
                this.treeListInsertNodeTree.Columns["NodeType"].OptionsColumn.AllowEdit = false;
                InitializeTreeListInsertNodeTreeComboBox(this.treeListInsertNodeTree.GetRow(0));
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        private int FindestateImageIndex(string CategoryName)
        {
            int stateImageIndex;
            if (CategoryName.Contains("Link"))
                stateImageIndex = 6;
            else
                stateImageIndex = 1;

            return stateImageIndex;
        }
        private void InitializeInsertNodes()
        {
            try
            {              
                List<string> nodeTypeList = new List<string>();
                foreach (Type type in TypeDefine.SimNodeTypes)
                    nodeTypeList.Add(type.Name);

                List<InserteNodeTreeData> vs = new List<InserteNodeTreeData>();
                Assembly a = Assembly.LoadWithPartialName("Pinokio.Animation");
                if (a != null)
                {
                    List<Type> ts = a.GetTypes().ToList();
                    for (int i = 0; i < ts.Count; i++)
                    {
                        Type t = ts[i];
                        if (IsInsertNodeReference(t))
                        {
                            string name = t.Name.Remove(0, 3);
                            List<string> value = RefTypeDefine.GetUsableSimNodeTypes(t);
                            InserteNodeTreeData stdData;
                            if (value.Contains(name))
                                stdData = new InserteNodeTreeData() { Category = RefTypeDefine.GetCategoryType(t), RefType = name, NodeType = name, Height = BaseUtill.GetInitialHeight(t) };
                            else
                                stdData = new InserteNodeTreeData() { Category = RefTypeDefine.GetCategoryType(t), RefType = name, NodeType = value[0], Height = BaseUtill.GetInitialHeight(t) };

                            vs.Add(stdData);
                        }
                    }
                }

                a = Assembly.LoadWithPartialName("Pinokio.Animation.User");

                if (a != null)
                {
                    List<Type> ts = a.GetTypes().ToList();
                    for (int i = 0; i < ts.Count; i++)
                    {
                        Type t = ts[i];
                        if (IsInsertNodeReference(t))
                        {
                            string name = t.Name.Remove(0, 3);

                            List<string> value = RefTypeDefine.GetUsableSimNodeTypes(t); 
                            InserteNodeTreeData stdData;
                            if (value.Contains(name))
                                stdData = new InserteNodeTreeData() { Category = RefTypeDefine.GetCategoryType(t), RefType = name, NodeType = name, Height = BaseUtill.GetInitialHeight(t) };
                            else
                                stdData = new InserteNodeTreeData() { Category = RefTypeDefine.GetCategoryType(t), RefType = name, NodeType = value[0], Height = BaseUtill.GetInitialHeight(t) };

                            vs.Add(stdData);
                        }
                    }
                }
                vs = vs.OrderByDescending(x => x.Category).ToList();

                RepositoryItemComboBox comboBox = new RepositoryItemComboBox();
                comboBox.Items.AddRange(nodeTypeList.ToArray());
                comboBox.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                this.gridControlInsertRefNode.DataSource = vs;
                this.gridControlInsertRefNode.RefreshDataSource();
                this.gridViewInsertRefNode.Columns[2].ColumnEdit = comboBox;
                this.gridViewInsertRefNode.Columns[0].OptionsColumn.AllowEdit = false;
                this.gridViewInsertRefNode.Columns[1].OptionsColumn.AllowEdit = false;
                this.gridViewInsertRefNode.ExpandAllGroups();
                InitializeGridViewInsertRefNodeComboBox(0);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }




        private void InitializeInsertCoupledModels()
        {
            try
            {
                List<InserteNodeTreeData> vs = new List<InserteNodeTreeData>();
                Assembly a = Assembly.LoadWithPartialName("Pinokio.Model.Base");
                if (a != null)
                {
                    List<Type> ts = a.GetTypes().ToList();

                    for (int i = 0; i < ts.Count; i++)
                    {
                        Type t = ts[i];
                        if (IsInsertCoupledModel(t))
                        {
                            Type baseType = t.BaseType;
                            while (baseType != null)
                            {
                                if ((baseType.ToString() == typeof(CoupledModel).ToString()))
                                {
                                    string name = t.Name;
                                    InserteNodeTreeData stdData = null;
                                    stdData = new InserteNodeTreeData() { Category = "Standard", NodeType = name };

                                    if (!stdData.NodeType.Contains("Floor"))
                                        vs.Add(stdData);
                                    break;
                                }
                                else
                                    baseType = baseType.BaseType;
                            }
                        }
                    }
                    vs = vs.OrderByDescending(x => x.Category).ToList();
                }

                a = Assembly.LoadWithPartialName("Pinokio.Model.User");

                if (a != null)
                {
                    List<Type> ts = a.GetTypes().ToList();
                    for (int i = 0; i < ts.Count; i++)
                    {
                        Type t = ts[i];
                        if (IsInsertCoupledModel(t))
                        {

                            Type baseType = t.BaseType;
                            while (baseType != null)
                            {
                                if ((baseType.ToString() == typeof(CoupledModel).ToString()))
                                {
                                    string name = t.Name;
                                    InserteNodeTreeData userData = null;
                                    userData = new InserteNodeTreeData() { Category = "User", NodeType = name };
                                    vs.Add(userData);
                                    break;
                                }
                                else
                                    baseType = baseType.BaseType;
                            }
                        }
                    }
                }

                this.gridControlInsertCoupledModel.DataSource = vs;
                this.gridViewInsertCoupledModel.Columns[1].Visible = false;
                this.gridViewInsertCoupledModel.Columns[3].Visible = false;
                this.gridControlInsertCoupledModel.RefreshDataSource();
                this.gridViewInsertCoupledModel.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }



        private void PrepareInsertingRefNode(string refNodeType, string simNodeType, double height, Point3D startPoint)
        {
            Type type = null;
            try
            {
                Assembly a = Assembly.LoadWithPartialName("Pinokio.Animation");
                if (a != null)
                    type = a.GetType("Pinokio.Animation." + refNodeType);

                a = Assembly.LoadWithPartialName("Pinokio.Animation.User");
                if (a != null && type == null)
                    type = a.GetType("Pinokio.Animation.User." + refNodeType);

                ConstructorInfo[] cs = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

                FinishAddNode();
                _modelActionType = ModelActionType.Insert;
                foreach (ConstructorInfo c in cs)
                {
                    if (c.GetParameters().Length == 1)
                    {
                        if (c.GetParameters()[0].ParameterType.Equals(typeof(string)))
                        {
                            dynamic node = c.Invoke(new object[] { refNodeType });

                            CurrentRef = node;
                            CurrentRef.MatchingObjType = simNodeType;
                            CurrentRef.Height = height;
                            Entity temp = CurrentRef.CreateTempEntity(this.pinokio3DModel1);

                            pinokio3DModel1.TempEntities.Add(temp, Color.FromArgb(100, pinokio3DModel1.Blocks[refNodeType].Entities[0].Color));

                            temp.Translate(startPoint.X, startPoint.Y, startPoint.Z);
                            temp.EntityData = new Vector3D(startPoint.X, startPoint.Y, startPoint.Z);

                            MouseSnapPointBeforeMouseMove = startPoint;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

 
        private void InitializeInsertedSimNodesTreeList(TreeList tl)
        {
            tl.BeginUpdate();

            tl.OptionsSelection.MultiSelect = true;

            TreeListColumn col1 = tl.Columns.Add();
            col1.FieldName = "ID";
            col1.Caption = "ID";
            col1.VisibleIndex = 0;
            col1.Visible = true;

            TreeListColumn col2 = tl.Columns.Add();
            col2.FieldName = "Name";
            col2.Caption = "Name";
            col2.VisibleIndex = 1;
            col2.Visible = true;

            TreeListColumn col3 = tl.Columns.Add();
            col3.FieldName = "NodeType";
            col3.Caption = "NodeType";
            col3.VisibleIndex = 2;
            col3.Visible = true;

            TreeListColumn col4 = tl.Columns.Add();
            col4.FieldName = "RefType";
            col4.Caption = "RefType";
            col4.VisibleIndex = 3;
            col4.Visible = true;

            tl.KeyFieldName = col1.FieldName;
            tl.EndUpdate();
        }

        private void InitializeSimEntityTreeList(TreeList tl)
        {
            tl.BeginUpdate();

            tl.OptionsSelection.MultiSelect = true;

            TreeListColumn col1 = tl.Columns.Add();
            col1.FieldName = "ID";
            col1.Caption = "ID";
            col1.VisibleIndex = 0;
            col1.Visible = true;

            TreeListColumn col2 = tl.Columns.Add();
            col2.FieldName = "Name";
            col2.Caption = "Name";
            col2.VisibleIndex = 1;
            col2.Visible = true;

            TreeListColumn col3 = tl.Columns.Add();
            col3.FieldName = "SimType";
            col3.Caption = "SimType";
            col3.VisibleIndex = 2;
            col3.Visible = true;

            TreeListColumn col4 = tl.Columns.Add();
            col4.FieldName = "RefType";
            col4.Caption = "RefType";
            col4.VisibleIndex = 3;
            col4.Visible = true;

            tl.KeyFieldName = col1.FieldName;
            tl.EndUpdate();
        }

        private void InitializeSimTimeSetting()
        {
            SimEngine.Instance.StartDateTime = DateTime.Now;
            beiSimStartTimeSetting.EditValue = SimEngine.Instance.StartDateTime;
            SimEngine.Instance.EndDateTime = DateTime.Now.AddHours(4);
            beiSimEndTimeSetting.EditValue = SimEngine.Instance.EndDateTime;

            InitWarmUpPeriod();
        }

        /// <summary>
        /// 시뮬레이션 Stop 후에 다시 Run할 때, 기존에 생성되어 있던 Entity들은 담고 있는
        /// 모든 컨테이너와 정보들을 Clear하는 함수
        /// </summary>
        private void InitializePreviousEntities()
        {
            Entity[] arrEntities = new Entity[pinokio3DModel1.Entities.Count];
            pinokio3DModel1.Entities.CopyTo(arrEntities, 0);

            for (int i = 0; i < arrEntities.Length; i++)
            {
                if (arrEntities[i] as PartReference != null)
                    pinokio3DModel1.Entities.Remove(arrEntities[i]);
            }

            pinokio3DModel1.PartReferences.Clear();
            ModelManager.Instance.Parts.Clear();
            partTreeList.ClearNodes();
        }

        private void InitializePreviousSimNodes()
        {
            InitializeInsertNodes();
            InitializeInsertCoupledModels();
        }

        BackUpInitDatas backUpInitDatas;
        public void CopyInitializeState()
        {
            try
            {
                backUpInitDatas = new BackUpInitDatas();

                foreach (NodeReference nr in this.pinokio3DModel1.NodeReferenceByID.Values)
                {
                    List<Tuple<string, string>> values = ObjectReference.GetSaveDatas(nr);
                    backUpInitDatas.ReferenceNodeInitDatas.Add(nr, values);
                }

                foreach (SimNode node in ModelManager.Instance.SimNodes.Values)
                {
                    List<Tuple<string, string>> values = SimObj.GetSaveDatas(node);
                    backUpInitDatas.SimNodeInitDatas.Add(node, values);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public void PasteSimulationInitializeState()
        {
            try
            {
                Dictionary<Type, Dictionary<string, MemberInfo>> typeMemberInfos = new Dictionary<Type, Dictionary<string, MemberInfo>>();
                foreach (SimNode node in ModelManager.Instance.SimNodes.Values)
                {
                    if (backUpInitDatas.SimNodeInitDatas.ContainsKey(node))
                    {
                        foreach (Tuple<string, string> v in backUpInitDatas.SimNodeInitDatas[node])
                        {
                            if (node is TXNode)
                                ((TXNode)node).RemoveEnteredObjects();

                            ModelManager.Instance.SetParameterValueInSimulationClass(node.GetType(), node, ref typeMemberInfos, v.Item1, v.Item2);
                        }
                    }
                }

                foreach (NodeReference nr in this.pinokio3DModel1.NodeReferenceByID.Values)
                {
                    nr.CurrentTransformationForAnimation = null;
                    if (backUpInitDatas.ReferenceNodeInitDatas.ContainsKey(nr))
                    {
                        foreach(Tuple<string, string> v in backUpInitDatas.ReferenceNodeInitDatas[nr] )
                        {
                            this.pinokio3DModel1.SetParameterValueInReferenceClass(nr.GetType(), nr, ref typeMemberInfos, v.Item1, v.Item2);
                        }
                    }
                }

                foreach (NodeReference nr in this.pinokio3DModel1.NodeReferenceByID.Values.ToArray())
                {
                    if(nr is RefVehicle)
                        nr.Move_MouseUp(this.pinokio3DModel1, nr.CurrentPoint, nr.CurrentPoint);
                }


                this.pinokio3DModel1.Entities.Regen();
                this.pinokio3DModel1.Invalidate();
                ClearSelection();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
  
        private void Simulator_OnStartSimulation(object sender, EventArgs e)
        {
            try
            {
                CopyInitializeState();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void gridViewInsertRefNode_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            InitializeGridViewInsertRefNodeComboBox(e.FocusedRowHandle);
        }
        
        private void InitializeGridViewInsertRefNodeComboBox(int rowHandle)
        {
            string refNodeTypeName = "Ref" + ((InserteNodeTreeData)(this.gridViewInsertRefNode.GetRow(rowHandle))).RefType;
            Type refNodeType = RefTypeDefine.NodeReferenceTypes.Find(x => x.Name == refNodeTypeName);
            List<string> refNodeSimNodeTypeNames = RefTypeDefine.GetUsableSimNodeTypes(refNodeType);
            RepositoryItemComboBox comboBox = new RepositoryItemComboBox();
            comboBox.Items.AddRange(refNodeSimNodeTypeNames.ToArray());
            comboBox.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            gridViewInsertRefNode.Columns[2].ColumnEdit = comboBox;
        }

        private void gridViewInsertRefNode_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //string refNodeTypeName = ((InserteNodeTreeData)(this.gridViewInsertRefNode.GetRow(e.RowHandle))).RefType;
            //Type refNodeType = RefTypeDefine.NodeReferenceTypes.Find(x => x.Name == refNodeTypeName);
            //List<string> refNodeSimNodeTypeNames = RefTypeDefine.GetUsableSimNodeTypes(refNodeType);
            //RepositoryItemComboBox comboBox = new RepositoryItemComboBox();
            //comboBox.Items.AddRange(refNodeSimNodeTypeNames.ToArray());
            //comboBox.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            //gridViewInsertRefNode.Columns[2].ColumnEdit = comboBox;

            string refNodeType = "Ref" + ((InserteNodeTreeData)(this.gridViewInsertRefNode.GetRow(e.RowHandle))).RefType;
            string simNodeType = ((InserteNodeTreeData)(this.gridViewInsertRefNode.GetRow(e.RowHandle))).NodeType;
            double height = ((InserteNodeTreeData)(this.gridViewInsertRefNode.GetRow(e.RowHandle))).Height;

            PrepareInsertingRefNode(refNodeType, simNodeType, height, new Point3D(0, 0, 0));
        }
        private void treeListInsertNodeTree_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            if (this.treeListInsertNodeTree.IsAutoFilterNode(e.Node))
                return;
            object o = e.Node.GetValue("StateImageIndex");
            if (o == null)
                return;
            int stateIndex = (int)o;
            e.NodeImageIndex = stateIndex;
        }


        private void treeListInsertNodeTree_RowClick(object sender, DevExpress.XtraTreeList.RowClickEventArgs e)
        {
            try
            {
                if (treeListInsertNodeTree.GetRow(e.Node.Id).GetType().Name != "InsertNodeTreeItem")
                    return;
                
                string refNodeType = "Ref" + ((InsertNodeTreeItem)treeListInsertNodeTree.GetRow(e.Node.Id)).RefType;
                string simNodeType = ((InsertNodeTreeItem)treeListInsertNodeTree.GetRow(e.Node.Id)).NodeType;
                double height = ((InsertNodeTreeItem)treeListInsertNodeTree.GetRow(e.Node.Id)).Height;
                PrepareInsertingRefNode(refNodeType, simNodeType, height, new Point3D(0, 0, 0));
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void treeListInsertNodeTree_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            string refNodeType = "Ref" + ((InserteNodeTreeData)(this.treeListInsertNodeTree.GetRow(e.Node.Id))).RefType;
            string simNodeType = ((InserteNodeTreeData)(this.treeListInsertNodeTree.GetRow(e.Node.Id))).NodeType;
            double height = ((InserteNodeTreeData)(this.treeListInsertNodeTree.GetRow(e.Node.Id))).Height;

            PrepareInsertingRefNode(refNodeType, simNodeType, height, new Point3D(0, 0, 0));
        }
        private void treeListInsertNodeTree_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (treeListInsertNodeTree.GetRow(e.Node.Id).GetType().Name != "InsertNodeTreeItem")
                return;
            InitializeTreeListInsertNodeTreeComboBox(treeListInsertNodeTree.GetFocusedRow());
        }

        private void InitializeTreeListInsertNodeTreeComboBox(object row)
        {
            //string refNodeTypeName = "Ref" + ((InsertNodeTreeItem)row).RefType;
            //Type refNodeType = RefTypeDefine.NodeReferenceTypes.Find(x => x.Name == refNodeTypeName);
            //List<string> refNodeSimNodeTypeNames = RefTypeDefine.GetUsableSimNodeTypes(refNodeType);
            //RepositoryItemComboBox comboBox = new RepositoryItemComboBox();
            //comboBox.Items.AddRange(refNodeSimNodeTypeNames.ToArray());
            //comboBox.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            ////treeListInsertNodeTree.RepositoryItems.Add(comboBox);
            //treeListInsertNodeTree.Columns["NodeType"].ColumnEdit = comboBox;
            ////treeListInsertNodeTree.Columns["NodeType"].OptionsColumn.AllowEdit = false; // NodeType 컬럼만 편집 가능
        }
    }
}
