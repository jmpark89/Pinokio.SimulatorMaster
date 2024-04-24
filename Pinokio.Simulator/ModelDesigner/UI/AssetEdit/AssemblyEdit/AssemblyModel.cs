using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using devDept.Graphics;
using Logger;
using Pinokio.Animation;

namespace Pinokio.Designer
{
    public class AssemblyModel : PinokioBaseModel
    {
        private double _standardHeight = 0;
        private Mesh[] _tempMoveArrows;
        private Point3D _centerOfArrowsMove = new Point3D(0, 0, 0);
        public Mesh[] TempMoveArrows { get => _tempMoveArrows; set => _tempMoveArrows = value; }

        public AssemblyModel()
        {
            //Entities = new MyEntityList();
            CreateMoveArrowsDirections();
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

            _centerOfArrowsMove = Point3D.Origin;
        }


        protected override void OnMouseUp(MouseEventArgs e)
        { 
            // we avoid mouse up actions for the right mouse button click
            // becuse we need that button just to for the ContextMenu
            if(e.Button != MouseButtons.Right)
                base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            // we avoid mouse down actions for the right mouse button click
            // becuse we need that button just to for the ContextMenu
            if(e.Button != MouseButtons.Right)
                base.OnMouseDown(e);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
     
            try
            {
                Point3D currentMousePoint;

                MouseLocation = e.Location;

                //    paint the viewport surface
                PaintBackBuffer();

                // consolidates the drawing
                SwapBuffers();

                ScreenToPlane(MouseLocation, Plane.XY, out currentMousePoint);

                UtillFunction.SnapToGrid(ref currentMousePoint,10);

                MouseLocationSnapToCorrdinate = currentMousePoint;

                TranslateAndShowMoveArrows();

                base.OnMouseMove(e);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        protected override void DrawOverlay(DrawSceneParams myParams)
        {

            try
            {
                DrawPositionMark(MouseLocation, MouseLocationSnapToCorrdinate, this, this.renderContext, myParams);


                base.DrawOverlay(myParams);

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
          

        }

        public void DrawPositionMark(System.Drawing.Point mouseLocation, Point3D current, devDept.Eyeshot.Model model, RenderContextBase RenderContextBase, DrawSceneParams myParams, double crossSide = 20.0)
        {
            RenderContextBase.EnableXOR(true);

            RenderContextBase.SetState(depthStencilStateType.DepthTestOff);

            RenderContextBase.SetLineSize(5);

            Point3D currentScreen = model.WorldToScreen(current);


            // Compute the direction on screen of the horizontal line
            Point2D left = model.WorldToScreen(current.X - 1, current.Y, this._standardHeight);
            Vector2D dirHorizontal = Vector2D.Subtract(left, currentScreen);
            dirHorizontal.Normalize();

            // Compute the position on screen of the line endpoints
            left = currentScreen + dirHorizontal * crossSide;
            Point2D right = currentScreen - dirHorizontal * crossSide;

            RenderContextBase.DrawLine(left, right);

            // Compute the direction on screen of the vertical line
            Point2D bottom = model.WorldToScreen(current.X, current.Y - 1, this._standardHeight);
            Vector2D dirVertical = Vector2D.Subtract(bottom, currentScreen);
            dirVertical.Normalize();

            // Compute the position on screen of the line endpoints
            bottom = currentScreen + dirVertical * crossSide;
            Point2D top = currentScreen - dirVertical * crossSide;

            RenderContextBase.DrawLine(bottom, top);

            RenderContextBase.SetLineSize(1);

            RenderContextBase.EnableXOR(false);

            RenderContextBase.EnableXORForTexture(true, myParams.ShaderParams);

            PublicDrawText(mouseLocation, current);

            RenderContextBase.EnableXORForTexture(false, myParams.ShaderParams);

        }
        public void PublicDrawText(System.Drawing.Point point, Point3D currentPoint)
        {
            DrawText(point.X, Height - point.Y + 10,
    "X = " + currentPoint.X.ToString("f2") + ", " +
    "Y = " + currentPoint.Y.ToString("f2") + ", " +
      "Z = " + this._standardHeight.ToString("f2"),
    new Font("Tahoma", 8.25f),
    Color.DarkGray, ContentAlignment.BottomLeft);
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


                Entity ent = Entities[entityIndex];

                Point3D center = (ent.BoxMax + ent.BoxMin) / 2;
                center.Z = ent.BoxMax.Z;

                Vector3D trans = new Vector3D(_centerOfArrowsMove, center);
                TempMoveArrows[0].Translate(trans);
                TempMoveArrows[2].Translate(trans);
                TempMoveArrows[1].Translate(trans);
                TempMoveArrows[3].Translate(trans);

                // updates center position
                _centerOfArrowsMove = center;

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

    }


}
