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
using Pinokio.Animation;
using Logger;

namespace Pinokio.Designer
{
    public partial class ModelDesigner : UserControl
    {
        Mesh[] tempRotationArrows;
        System.Drawing.Point downLocatioin;
        Point3D centerOfArrowsRotation;
        ObjectManipulator.actionType objectManipulatorEditing = ObjectManipulator.actionType.None;

        private void InitialzieObjectManipulator()
        {
            try
            {
                this.pinokio3DModel1.ObjectManipulator.MouseOver += OnObjectManipulatorMouseOver;
                this.pinokio3DModel1.ObjectManipulator.MouseDown += OnObjectManipulatorMouseDown;
                this.pinokio3DModel1.ObjectManipulator.MouseDrag += OnObjectManipulatorMouseDrag;
                this.pinokio3DModel1.ObjectManipulator.MouseUp += OnObjectManipulatorMouseUp;



                this.pinokio3DModel1.ObjectManipulator.RotationStep = 0.174533;
                this.pinokio3DModel1.ObjectManipulator.TranslationStep = 100;

                this.pinokio3DModel1.ObjectManipulator.RotateX.Visible = false;
                this.pinokio3DModel1.ObjectManipulator.RotateY.Visible = false;
                this.pinokio3DModel1.ObjectManipulator.Ball.Visible = false;


                this.pinokio3DModel1.ObjectManipulator.StyleMode = ObjectManipulator.styleType.Rings;


                this.pinokio3DModel1.CompileUserInterfaceElements();
                // this.pinokio3DModel1.ActionMode = actionType.SelectVisibleByPick;
                this.pinokio3DModel1.ObjectManipulator.ShowOriginalWhileEditing = false;

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void OnObjectManipulatorMouseUp(object sender, ObjectManipulator.ObjectManipulatorEventArgs args)
        {
            try
            {
                List<NodeReference> nodeReferences;

                if (args.ActionMode != ObjectManipulator.actionType.None)
                {
                    _modelActionType = ModelActionType.EditTransformation;

                    this.pinokio3DModel1.ObjectManipulator.Apply();
                    if (args.ActionMode == ObjectManipulator.actionType.TranslateOnAxis || args.ActionMode == ObjectManipulator.actionType.TranslateOnView)
                    {
                        Move_MouseUpMulti(pinokio3DModel1, pinokio3DModel1.MouseLocationSnapToCorrdinate, MouseSnapPointBeforeMouseDown, out nodeReferences);
                    }
                    else if (args.ActionMode == ObjectManipulator.actionType.Rotate || args.ActionMode == ObjectManipulator.actionType.RotateOnView)
                    {
                        Rotate_MouseUpMulti();
                    }
                    else if (args.ActionMode == ObjectManipulator.actionType.Scale)
                    {


                    }


                    pinokio3DModel1.Entities.Regen();
                    pinokio3DModel1.Invalidate();
                    OpenObjectManipulator();
                }
                else if (_modelActionType == ModelActionType.Moving)
                {
                    _modelActionType = ModelActionType.None;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void OnObjectManipulatorMouseDown(object sender, ObjectManipulator.ObjectManipulatorEventArgs args)
        {
            // force a new display of the tooltip
            string newString = string.Empty;

            if (args.ActionMode != ObjectManipulator.actionType.None)
            {
                _modelActionType = ModelActionType.EditTransformation;
            }

        }

        private void OnObjectManipulatorMouseDrag(object sender, ObjectManipulator.ObjectManipulatorEventArgs args)
        {
            try
            {
                //Move_MouseMoveMultl(pinokio3DModel1, pinokio3DModel1.MouseLocationSnapToCorrdinate, MouseSnapPointBeforeMouseMove);
            }
                catch (Exception ex)
                {
                    ErrorLogger.SaveLog(ex);
                }
        }



        private void OnObjectManipulatorMouseOver(object sender, ObjectManipulator.ObjectManipulatorEventArgs args)
        {
            try
            {
                if (args.ActionMode != ObjectManipulator.actionType.None)
                {
                    _modelActionType = ModelActionType.EditTransformation;
                }

                objectManipulatorEditing = args.ActionMode;

                // force a new display of the tooltip
                switch (args.ActionMode)
                {
                    case ObjectManipulator.actionType.Rotate:

                        break;

                    case ObjectManipulator.actionType.RotateOnView:

                        break;

                    case ObjectManipulator.actionType.TranslateOnAxis:

                        break;

                    case ObjectManipulator.actionType.TranslateOnView:

                        break;

                    case ObjectManipulator.actionType.Scale:
                        break;

                    case ObjectManipulator.actionType.UniformScale:

                        break;

                    case ObjectManipulator.actionType.None:
                        break;

                }

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void OpenObjectManipulator()
        {

            try
            {

                Transformation initialTransformation = null;
                bool center = true;
                objectManipulatorEditing = ObjectManipulator.actionType.None;

                // If there is only one selected entity, position and orient the manipulator using the rotation point saved in its
                // EntityData property and its transformation

                Point3D rotationPoint = null;

                rotationPoint = new Point3D();

                if (rotationPoint != null)

                    initialTransformation = new Translation(rotationPoint.X, rotationPoint.Y,
                        rotationPoint.Z);
                else

                    initialTransformation = new Identity();

                // Enables the ObjectManipulator to start editing the selected objects
                this.pinokio3DModel1.ObjectManipulator.Enable(initialTransformation, center);
                //this.pinokio3DModel1.ModelActionType = ModelActionType.Selection;
                //_modelActionType
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }
        public void CloseObjectManipulator()
        {
            try
            {
                objectManipulatorEditing = ObjectManipulator.actionType.None;

                // Enables the ObjectManipulator to start editing the selected objects

                this.pinokio3DModel1.ObjectManipulator.Apply();
               
                this.pinokio3DModel1.Entities.Regen();

                this.pinokio3DModel1.Invalidate();
                //this.pinokio3DModel1.Entities.SetSelectionAsCurrent();
                _modelActionType = ModelActionType.None;
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }



        }

        private void CreateRotationArrowsDirections()
        {
            // removes previous arrows if present
            if (tempRotationArrows != null)
            {
                pinokio3DModel1.TempEntities.Remove(tempRotationArrows[0]);
                pinokio3DModel1.TempEntities.Remove(tempRotationArrows[1]);
            }

            //creates 4 temporary arrows on the current moving plane to display when the mouse is over an entity
            tempRotationArrows = new Mesh[2];

            //devDept.Eyeshot.Entities.Region arrowShape = new devDept.Eyeshot.Entities.Region(new LinearPath(Plane, new Point2D[]
            //{
            //    new Point2D(0,-2),
            //    new Point2D(4,-2),
            //    new Point2D(4,-4),
            //    new Point2D(10,0),
            //    new Point2D(4,4),
            //    new Point2D(4,2),
            //    new Point2D(0,2),
            //    new Point2D(0,-2),
            //}), Plane);

            ////right arrow
            //tempRotationArrows[0] = arrowShape.ExtrudeAsMesh(2, 0.1, Mesh.natureType.Plain);
            //tempRotationArrows[0].Regen(0.1);
            //tempRotationArrows[0].Color = Color.FromArgb(100, Color.Red);


            //left arrow
            //tempRotationArrows[1] = (Mesh)tempRotationArrows[0].Clone();
            //tempRotationArrows[1].Rotate(Math.PI, Plane.AxisZ);
            //tempRotationArrows[1].Regen(0.1);


            //Vector3D diagonalV = new Vector3D(tempRotationArrows[0].BoxMin, tempRotationArrows[0].BoxMax);
            //double offset = Math.Max(Vector3D.Dot(diagonalV, Plane.AxisX), Vector3D.Dot(diagonalV, Plane.AxisY));
            //Vector3D translateX = Plane.AxisX * offset / 2;
            //Vector3D translateY = Plane.AxisY * offset / 2;

            //tempRotationArrows[0].Translate(translateX);
            //tempRotationArrows[1].Translate(-1 * translateX);

            centerOfArrowsRotation = Point3D.Origin;
        }

        private void TranslateAndShowRotationArrows()
        {
            System.Drawing.Point mouseLocation = pinokio3DModel1.PointToClient(Cursor.Position);

            // gets the entity index under mouse cursor
            EntityIndex = pinokio3DModel1.GetEntityUnderMouseCursor(mouseLocation);

            if (EntityIndex < 0  || !(pinokio3DModel1.Entities[EntityIndex] is NodeReference))
            {
                return;
            }

            NodeReference ent = pinokio3DModel1.Entities[EntityIndex] as NodeReference;
            Point3D center = new Point3D(ent.Transformation[0, 3], ent.Transformation[1, 3], ent.Transformation[2, 3]);

            Vector3D trans = new Vector3D(centerOfArrowsRotation, center);

            // translates arrows
            tempRotationArrows[0].Translate(trans);
            tempRotationArrows[1].Translate(trans);
            centerOfArrowsRotation = center;


            if (pinokio3DModel1.TempEntities.Count < 2)
            {
                pinokio3DModel1.TempEntities.Add(tempRotationArrows[0]);
                pinokio3DModel1.TempEntities.Add(tempRotationArrows[1]);

                // updates camera Near and Far planes to avoid clipping temp entity on the scene during translation
                pinokio3DModel1.TempEntities.UpdateBoundingBox();
            }
        }




    }
}
