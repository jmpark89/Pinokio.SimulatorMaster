using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Eyeshot;
using devDept.Graphics;
using devDept.Geometry;
using devDept.Eyeshot.Entities;
using System.Windows.Forms;
using System.Drawing;
using Logger;
using Pinokio.Animation;
using System.Data;
using System.Reflection;
using Pinokio.Model.Base;
using Pinokio.Database;
using Simulation.Engine;
using System.Diagnostics;

namespace Pinokio.Designer
{
    [Serializable]
    public partial class PinokioEditorModel : PinokioBaseModel
    {
        private Point3D _mouseLocationSnapToCorrdinate = new Point3D();
        private Mesh[] _tempMoveArrows ;
        private Point3D _centerOfArrowsMove = new Point3D(0,0,0);
        private ModelActionType _modelActionType;
        private FormDetailProperties _detailform;
        private uint _lastSelectedFloor = 1;
        private TreeView _multitreeview;
        private System.Drawing.Point _startPosition;

        public System.Drawing.Point StartPosition { get => _startPosition; set => _startPosition = value; }
        public Mesh[] TempMoveArrows { get => _tempMoveArrows; set => _tempMoveArrows = value; }
        public Point3D CenterOfArrowsMove { get => _centerOfArrowsMove; set => _centerOfArrowsMove = value; }
        public ModelActionType ModelActionType { get => _modelActionType; set => _modelActionType = value; }
        public override Point3D MouseLocationSnapToCorrdinate { get => _mouseLocationSnapToCorrdinate; set => _mouseLocationSnapToCorrdinate = value; }
        public FormDetailProperties Detailform { get => _detailform; set => _detailform = value; }
        public override uint SelectedFloorID { get => _lastSelectedFloor; set => _lastSelectedFloor = value; }
        public PinokioEditorModel()
        {
            this.Unlock("US2-RMKUX-N12YW-WAMY-S738");
            //_detailform = new FormDetailProperties(this);
        }

        private void CreateMoveArrowsDirections()
        {
            // removes previous arrows if present
            if (TempMoveArrows != null)
            {
                TempEntities.Remove(TempMoveArrows[0]);
                TempEntities.Remove(TempMoveArrows[1]);
                TempEntities.Remove(TempMoveArrows[2]);
                TempEntities.Remove(TempMoveArrows[3]);
            }

            //creates 4 temporary arrows on the current moving plane to display when the mouse is over an entity
            TempMoveArrows = new Mesh[4];
            Plane plane = new Plane();
            devDept.Eyeshot.Entities.Region arrowShape = new devDept.Eyeshot.Entities.Region(new LinearPath(plane, new Point2D[]
            {
                new Point2D(0,-200),
                new Point2D(400,-200),
                new Point2D(400,-400),
                new Point2D(1000,0),
                new Point2D(400,400),
                new Point2D(400,200),
                new Point2D(0,200),
                new Point2D(0,-200),
            }), plane);

            //right arrow
            TempMoveArrows[0] = arrowShape.ExtrudeAsMesh(2, 0.1, devDept.Eyeshot.Entities.Mesh.natureType.Plain);
            TempMoveArrows[0].Regen(0.1);
            TempMoveArrows[0].Color = Color.FromArgb(100, Color.Red);

            //top arrow
            TempMoveArrows[1] = (Mesh)TempMoveArrows[0].Clone();
            TempMoveArrows[1].Rotate(Math.PI / 2, plane.AxisZ);
            TempMoveArrows[1].Regen(0.1);

            //left arrow
            TempMoveArrows[2] = (Mesh)TempMoveArrows[0].Clone();
            TempMoveArrows[2].Rotate(Math.PI, plane.AxisZ);
            TempMoveArrows[2].Regen(0.1);

            //bottom arrow
            TempMoveArrows[3] = (Mesh)TempMoveArrows[0].Clone();
            TempMoveArrows[3].Rotate(-Math.PI / 2, plane.AxisZ);
            TempMoveArrows[3].Regen(0.1);

            Vector3D diagonalV = new Vector3D(TempMoveArrows[0].BoxMin, TempMoveArrows[0].BoxMax);
            double offset = Math.Max(Vector3D.Dot(diagonalV, plane.AxisX), Vector3D.Dot(diagonalV, plane.AxisY));
            Vector3D translateX = plane.AxisX * offset / 2;
            Vector3D translateY = plane.AxisY * offset / 2;

            TempMoveArrows[0].Translate(translateX);
            TempMoveArrows[1].Translate(translateY);
            TempMoveArrows[2].Translate(-1 * translateX);
            TempMoveArrows[3].Translate(-1 * translateY);

            CenterOfArrowsMove = Point3D.Origin;
        }


        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            InitalizeBlockPart();
            CreateMoveArrowsDirections();
        }

        protected override void CreateNodeReferenceBlock(devDept.Eyeshot.Model model)        
        {
            try
            {

                List<Type> totalTypes = new List<Type>();
                Assembly a = Assembly.LoadWithPartialName("Pinokio.Animation");
                if (a != null)
                    totalTypes.AddRange(a.GetTypes().ToList());

                a = Assembly.LoadWithPartialName("Pinokio.Animation.User");
                if (a != null)
                    totalTypes.AddRange(a.GetTypes().ToList());

                List<Type> typeList = new List<Type>();
                foreach (Type t in totalTypes)
                {
                    if (BaseUtill.IsSameBaseType(t, typeof(NodeReference)) || BaseUtill.IsSameBaseType(t, typeof(PartReference)))
                    {
                        typeList.Add(t);
                    }
                }
                foreach (Type t in typeList)
                {
                    MethodInfo m = (t.GetMethods().ToList().Find(x => x.Name == "CreateBlock"));

                    try
                    {
                        if (m != null)
                            m.Invoke(null, new object[] { model, new object[] { } });

                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.SaveLog(ex);
                        ErrorLogger.SaveLog(t.Name + " Type CreateBlock Fail...");
                    }


                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(" CreateNodeReferenceBlock Fail...");
                ErrorLogger.SaveLog(ex);
            }


        }


        public void DeleteNodeReference(List<NodeReference> nodeReferences)
        {
            foreach (NodeReference DeleteEntity in nodeReferences)
            {
                #region cho 추가 * bool removed로 불필요한 계산 방지
                bool removed = this.Entities.Remove(DeleteEntity);
                NodeReferenceByID.Remove(DeleteEntity.ID);
                if (!removed)
                    Debugger.Break();
                //Entity findEn = this.Entities.Where(en => ((NodeReference)en).ID == DeleteEntity.ID).FirstOrDefault() as Entity;
                //if (findEn != null)
                //{
                //    List<Entity> enList = new List<Entity>();
                //    foreach (Entity en in this.Entities)
                //    {
                //        enList.Add(en);
                //    }

                //    int newidx = enList.IndexOf(findEn);

                //    this.Entities.RemoveAt(newidx);
                //} 
                #endregion
            }
            this.Entities.Regen();
            this.Invalidate();
        }

        public void DeleteNodeReference(List<uint> nodeIds)
        {
            List<NodeReference> nodes = new List<NodeReference>();
            foreach(uint id in nodeIds)
            {
                if (this.NodeReferenceByID.ContainsKey(id))
                    nodes.Add(this.NodeReferenceByID[id]);
            }
            DeleteNodeReference(nodes);
        }

        public void ShowDetailForm(NodeReference node)
        {
            try
            {
                //_detailform.RefreshProperties(this, node);
                //_detailform.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        protected override void DrawOverlay(devDept.Eyeshot.Model.DrawSceneParams myParams)
        {
            if (FocusedLine != null)
                DrawRailDistance(MouseLocation, MouseLocationSnapToCorrdinate);
            else
                DrawPositionMark(MouseLocation, MouseLocationSnapToCorrdinate, this, this.renderContext, myParams);

            if (CurrentRef != null)
                CurrentRef.DrawOverlay(this, myParams);

            else if (ModelActionType == ModelActionType.Selecting)
            {
                DrawSelectionBox(StartPosition, MouseLocation, Color.WhiteSmoke, true, true);
            }
            base.DrawOverlay(myParams);
            
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                if (!(SimEngine.Instance.EngineState is ENGINE_STATE.PAUSE || SimEngine.Instance.EngineState is ENGINE_STATE.RUNNING))
                {
                    MouseLocation = e.Location;

                    ReviseMousePoint();

                    //    paint the viewport surface
                    PaintBackBuffer();

                    // consolidates the drawing
                    SwapBuffers();

                    Point3D currentMousePoint;

                    Plane p = new Plane(new Point3D(0, 0, this.SelectedFloorHeight), Vector3D.AxisZ);

                    ScreenToPlane(MouseLocation, p, out currentMousePoint);

                    UtillFunction.SnapToGrid(ref currentMousePoint);

                    MouseLocationSnapToCorrdinate = currentMousePoint;

                    //TranslateAndShowMoveArrows();

                }

                base.OnMouseMove(e);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            
            base.OnKeyDown(e);
        }

        private void ReviseMousePoint()
        {
            try
            {
                //if (ModifierKeys == (Keys.Shift))
                //{
                //    if (_modelActionType == ModelActionType.DragLine || _modelActionType == ModelActionType.Routing
                //         || _modelActionType == ModelActionType.Moving) //컨베이어 / OHT 라인등 드래그라인 등으 ㅣ액션시 직선이동(쉬프트)
                //    {
                //        if (Cursor.Position != _firstClickMouseLocation && _firstClick ||
                //            Cursor.Position != _firstClickMouseLocation && _modelActionType == ModelActionType.Moving)
                //        {
                //            System.Drawing.Point _linedPoint = _firstClickMouseLocation;
                //            int xVar = (int)Math.Abs(Cursor.Position.X - _linedPoint.X);
                //            int yVar = (int)Math.Abs(Cursor.Position.Y - _linedPoint.Y);

                //            System.Drawing.Point nowPoint = Cursor.Position;

                //            if (xVar >= yVar)
                //                nowPoint.Y = _linedPoint.Y;
                //            else
                //                nowPoint.X = _linedPoint.X;

                //            Cursor.Position = nowPoint;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

        private void TranslateAndShowMoveArrows()
        {
            try
            {
                int entityIndex = GetEntityUnderMouseCursor(MouseLocation);
                if (entityIndex < 0)
                {
                    RemoveMoveArrow();
                    return;
                }

                if (Entities[entityIndex] is NodeReference)
                {
                    NodeReference ent = Entities[entityIndex] as NodeReference;

                    if (ent.IsPossibleMoved)
                    {
                        Point3D center = ent.CurrentPoint;

                        Vector3D trans = new Vector3D(CenterOfArrowsMove, center);
                        TempMoveArrows[0].Translate(trans);
                        TempMoveArrows[2].Translate(trans);
                        TempMoveArrows[1].Translate(trans);
                        TempMoveArrows[3].Translate(trans);

                        // updates center position
                        CenterOfArrowsMove = center;

                        // if not already added, adds them to TempEntities list
                        if (TempEntities.Count < 4)
                        {
                            TempEntities.Add(TempMoveArrows[0]);
                            TempEntities.Add(TempMoveArrows[1]);
                            TempEntities.Add(TempMoveArrows[2]);
                            TempEntities.Add(TempMoveArrows[3]);

                            // updates camera Near and Far planes to avoid clipping temp entity on the scene during translation
                            TempEntities.UpdateBoundingBox();
                        }
                        return;
                    }
                }
                RemoveMoveArrow();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
         

           
        }

        private void RemoveMoveArrow()
        {
            try
            {
                bool isRemove = TempEntities.Remove(TempMoveArrows[0]);
                if (!isRemove)
                    return;
                isRemove = TempEntities.Remove(TempMoveArrows[1]);
                isRemove = TempEntities.Remove(TempMoveArrows[2]);
                isRemove = TempEntities.Remove(TempMoveArrows[3]);
                if (isRemove)
                {
                    TempEntities.UpdateBoundingBox();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

        public  void DrawPositionMark(System.Drawing.Point mouseLocation, Point3D current, PinokioEditorModel model, RenderContextBase renderContextBase, devDept.Eyeshot.Model.DrawSceneParams myParams, double crossSide = 20.0)
        {
            //RenderContextBase.EnableXOR(true);
            renderContextBase.EnableXOR(true);

            renderContextBase.SetState(depthStencilStateType.DepthTestOff);
            
            renderContextBase.SetLineSize(5);
            
            Point3D currentScreen = model.WorldToScreen(current);


            // Compute the direction on screen of the horizontal line
            Point2D left = model.WorldToScreen(current.X - 1, current.Y, SelectedFloorHeight);
            Vector2D dirHorizontal = Vector2D.Subtract(left, currentScreen);
            dirHorizontal.Normalize();

            // Compute the position on screen of the line endpoints
            left = currentScreen + dirHorizontal * crossSide;
            Point2D right = currentScreen - dirHorizontal * crossSide;

            renderContextBase.DrawLine(left, right);
            
            // Compute the direction on screen of the vertical line
            Point2D bottom = model.WorldToScreen(current.X, current.Y - 1, SelectedFloorHeight);
            Vector2D dirVertical = Vector2D.Subtract(bottom, currentScreen);
            dirVertical.Normalize();

            // Compute the position on screen of the line endpoints
            bottom = currentScreen + dirVertical * crossSide;
            Point2D top = currentScreen - dirVertical * crossSide;

            renderContextBase.DrawLine(bottom, top);
            
            renderContextBase.SetLineSize(1);
            
            renderContextBase.EnableXOR(false);
            
            renderContextBase.EnableXORForTexture(true, myParams.ShaderParams);
            
            model.PublicDrawText(mouseLocation, current);

            renderContextBase.EnableXORForTexture(false, myParams.ShaderParams);            
        }

        public void DrawRailDistance(System.Drawing.Point mouseLocation, Point3D current)
        {
            try
            {
                RefTransportLine focusedLine = FocusedLine;
                double distance = Point3D.Distance(new Point3D(focusedLine.RailStartPoint.X, focusedLine.RailStartPoint.Y, 0), new Point3D(current.X, current.Y, 0));

                DrawText(mouseLocation.X, Height - mouseLocation.Y + 10, "Distance = " + (Math.Round(distance, 0)).ToString(),
                  new Font("Tahoma", 8.25f), Color.DarkGray, ContentAlignment.BottomLeft);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Mouse SelectDrag시 SelectArea 그리기
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="transparentColor"></param>
        /// <param name="drawBorder"></param>
        /// <param name="dottedBorder"></param>
        public void DrawSelectionBox(System.Drawing.Point p1, System.Drawing.Point p2, Color transparentColor, bool drawBorder,
    bool dottedBorder)
        {
            p1.Y = (int)(Height - p1.Y);
            p2.Y = (int)(Height - p2.Y);

            NormalizeBox(ref p1, ref p2);

            int[] viewFrame = Viewports[ActiveViewport].GetViewFrame();
            int left = viewFrame[0];
            int top = viewFrame[1] + viewFrame[3];
            int right = left + viewFrame[2];
            int bottom = viewFrame[1];

            if (p2.X > right - 1)
                p2.X = right - 1;

            if (p2.Y > top - 1)
                p2.Y = top - 1;

            if (p1.X < left + 1)
                p1.X = left + 1;

            if (p1.Y < bottom + 1)
                p1.Y = bottom + 1;

            renderContext.SetState(blendStateType.Blend);
            renderContext.SetColorWireframe(Color.FromArgb(40, transparentColor.R, transparentColor.G,
                transparentColor.B));
            renderContext.SetState(rasterizerStateType.CCW_PolygonFill_CullFaceBack_NoPolygonOffset);
            
            int w = p2.X - p1.X;
            int h = p2.Y - p1.Y;

            renderContext.DrawQuad(new System.Drawing.RectangleF(p1.X + 1, p1.Y + 1, w - 1, h - 1));
            renderContext.SetState(blendStateType.NoBlend);
            
            if (drawBorder)
            {
                renderContext.SetColorWireframe(Color.FromArgb(255, transparentColor.R,
                    transparentColor.G, transparentColor.B));

                List<Point3D> pts = null;

                if (dottedBorder)
                {
                    renderContext.SetLineStipple(1, 0x0F0F, Viewports[0].Camera);
                    renderContext.EnableLineStipple(true);
                }

                int l = p1.X;
                int r = p2.X;
                if (renderContext.IsDirect3D) // In Eyeshot 9 use renderContext.IsDirect3D
                {
                    l += 1;
                    r += 1;
                }

                pts = new List<Point3D>(new Point3D[]
                {
                new Point3D(l, p1.Y), new Point3D(p2.X, p1.Y),
                new Point3D(r, p1.Y), new Point3D(r, p2.Y),
                new Point3D(r, p2.Y), new Point3D(l, p2.Y),
                new Point3D(l, p2.Y), new Point3D(l, p1.Y),
                });


                renderContext.DrawLines(pts.ToArray());
                
                if (dottedBorder)
                    renderContext.EnableLineStipple(false);
            }
        }

        internal static void NormalizeBox(ref System.Drawing.Point p1, ref System.Drawing.Point p2)
        {

            int firstX = Math.Min(p1.X, p2.X);
            int firstY = Math.Min(p1.Y, p2.Y);
            int secondX = Math.Max(p1.X, p2.X);
            int secondY = Math.Max(p1.Y, p2.Y);

            p1.X = firstX;
            p1.Y = firstY;
            p2.X = secondX;
            p2.Y = secondY;
        }

        #region DB Load
        protected override List<Type> GetNodeReferenceTypes(out Dictionary<string, Type> dicNodeReferenceTypeByName)
        {
            dicNodeReferenceTypeByName = new Dictionary<string, Type>();
            List<Type> returnValue = new List<Type>();
            try
            {
                List<Type> totalTypes = new List<Type>();
                Assembly a = Assembly.LoadWithPartialName("Pinokio.Animation");
                if (a != null)
                    totalTypes.AddRange(a.GetTypes().ToList());



                a = Assembly.LoadWithPartialName("Pinokio.Animation.User");
                if (a != null)
                    totalTypes.AddRange(a.GetTypes().ToList());

                List<Type> typeList = new List<Type>();
                foreach (Type t in totalTypes)
                {
                    if (t.Equals(typeof(RefLink)))
                        continue;

                    if (BaseUtill.IsSameBaseType(t, typeof(NodeReference)))
                    {
                        returnValue.Add(t);
                        dicNodeReferenceTypeByName.Add(t.Name, t);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return returnValue;
        }

        public override List<FloorPlan> LoadFloor(DataTable dt)
        {
            List<FloorPlan> lstfloor = new List<FloorPlan>();
            foreach (DataRow reader in dt.Rows)
            {
                string name = reader[FLOOR_COL.NAME.ToString()].ToString();
                int floorNum = Convert.ToInt32(reader[FLOOR_COL.FLOOR.ToString()].ToString());
                int width = Convert.ToInt32(reader[FLOOR_COL.WIDTH.ToString()].ToString());
                int depth = Convert.ToInt32(reader[FLOOR_COL.DEPTH.ToString()].ToString());
                int bottom = Convert.ToInt32(reader[FLOOR_COL.BOTTOM.ToString()].ToString());
                int zoomRatio = Convert.ToInt32(reader[FLOOR_COL.ZOOM_RATIO.ToString()].ToString());
                string path = reader[FLOOR_COL.PATH.ToString()].ToString();
                Floor floor = ModelManager.Instance.DicCoupledModel.Values.First(x => x.Name == name) as Floor;
                FloorPlan floorPlan = new FloorPlan(floor, zoomRatio, path);
                floorPlan.FloorNum = floorNum;
                floorPlan.FloorWidth = width;
                floorPlan.FloorDepth = depth;
                floorPlan.FloorBottom = bottom;
                lstfloor.Add(floorPlan);
            }

            SetFloorform(lstfloor);

            return lstfloor;
        }


        #endregion

    }

}
