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
using static devDept.Eyeshot.Environment;
using Pinokio.Animation;
using DevExpress.XtraVerticalGrid;
using Logger;
using Simulation.Engine;
using System.Diagnostics;

namespace Pinokio.Designer
{
    public partial class ModelDesigner : UserControl
    {
        public List<NodeReference> SelectedNodeReferences { get => pinokio3DModel1.SelectedNodeReferences; set => pinokio3DModel1.SelectedNodeReferences = value; }
        public List<PartReference> SelectedEntityReferences { get => pinokio3DModel1.SelectedEntityReferences; set => pinokio3DModel1.SelectedEntityReferences = value; }

        public void ClearSelection()
        {
            try
            {
                ClearPropertyGrid();
                ClearTreeList();

                propertyGridControlSimObject.SelectedObject = null;
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

        private void Selection(int selectedIndex)
        {
            if (selectedIndex < 0)
                return;

            if (pinokio3DModel1.Entities[selectedIndex] is NodeReference)
            {
                if (!SelectedNodeReferences.Contains((NodeReference)pinokio3DModel1.Entities[selectedIndex]))
                    SelectedNodeReferences.Add((NodeReference)pinokio3DModel1.Entities[selectedIndex]);

                pinokio3DModel1.Entities[selectedIndex].Selected = true;
            }
            else if (pinokio3DModel1.Entities[selectedIndex] is PartReference)
            {
                if (!SelectedEntityReferences.Contains((PartReference)pinokio3DModel1.Entities[selectedIndex]))
                    SelectedEntityReferences.Add((PartReference)pinokio3DModel1.Entities[selectedIndex]);

                pinokio3DModel1.Entities[selectedIndex].Selected = true;
            }

        }


        private void Selection(System.Drawing.Point startSelectPoint, System.Drawing.Point curentMousePoint)
        {
            try
            {
                int x = startSelectPoint.X < curentMousePoint.X ? startSelectPoint.X : curentMousePoint.X;
                int y = startSelectPoint.Y < curentMousePoint.Y ? startSelectPoint.Y : curentMousePoint.Y;
                int width = Math.Abs(curentMousePoint.X - startSelectPoint.X);
                int height = Math.Abs(curentMousePoint.Y - startSelectPoint.Y);
                                
                if (width == 0)
                    width = 1;
                if (height == 0)
                    height = 1;
                ClearSelection();
                int[] indexs = pinokio3DModel1.GetAllCrossingEntities(new Rectangle(x, y, width, height), true);

                if (indexs.Count() == 0)
                {
                    CloseObjectManipulator();

                    return;
                }
                else
                {
                    //_modelActionType = ModelActionType.None;
                    foreach (int index in indexs)
                    {
                        Selection(index);
                    }
                }

                //pinokio3DModel1.Entities.Regen();
                //pinokio3DModel1.Invalidate();
                //if (SimEngine.Instance.EngineState != ENGINE_STATE.RUNNING
                //    && SimEngine.Instance.EngineState != ENGINE_STATE.PAUSE)
                //{
                //    _modelActionType = ModelActionType.EditTransformation;
                //    OpenObjectManipulator();
                //}
            }
            catch
            {

            }
        }


        private void MouseUpMultiSelection()
        {
            try
            {
                if (SelectedNodeReferences.Count == 0 && SelectedEntityReferences.Count == 0)
                    return;

                UpdateTreeListBySelectedReferences();

                if(SelectedNodeReferences.Count == 1 && SelectedEntityReferences.Count == 0)
                {
                    CheangeSelectedSimObject4PropertyGrid(SelectedNodeReferences[0].Core);
                }
                else if (SelectedNodeReferences.Count == 0 && SelectedEntityReferences.Count == 1)
                {
                    CheangeSelectedSimObject4PropertyGrid(SelectedEntityReferences[0].Core);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void UpdateTreeListBySelectedReferences()
        {
            foreach (NodeReference nodeReference in SelectedNodeReferences)
            {
                SimNodeTreeListNode node = (SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(nodeReference.ID);
                simNodeTreeList.Invoke(new Action(() => simNodeTreeList.SelectNode(node)));
            }
            simNodeTreeList.Update();

            foreach (PartReference entityReference in SelectedEntityReferences)
            {
                PartTreeListNode entity = (PartTreeListNode)partTreeList.FindNodeByKeyID(entityReference.ID);
                simNodeTreeList.Invoke(new Action(() => partTreeList.SelectNode(entity)));
            }

            partTreeList.Update();
        }

        public void ClearPropertyGrid()
        {
            SelectedNodeReferences.Clear();
            SelectedEntityReferences.Clear();
            pinokio3DModel1.Entities.ClearSelection();
            propertyGridControlSimObject.SelectedObject = null;
            propertyGridControlSimObject.Refresh();
            pinokio3DModel1.TempEntities.Clear();
            if (CurrentRef != null)
                CurrentRef.CancelInsert(pinokio3DModel1);

            if (pinokio3DModel1.Blocks.Contains("InnerConv"))
                pinokio3DModel1.Blocks.Remove("InnerConv");

            CurrentRef = null;
        }
        private void ClearTreeList()
        {
            simNodeTreeList.BeginUpdate();
            simNodeTreeList.ClearSelection();
            simNodeTreeList.FocusedNode = null;
            simNodeTreeList.EndUpdate();

            partTreeList.BeginUpdate();
            partTreeList.ClearSelection();
            partTreeList.FocusedNode = null;
            partTreeList.EndUpdate();
        }
    }
}
