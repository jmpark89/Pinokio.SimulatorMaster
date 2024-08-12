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
using System.Text.RegularExpressions;
using System.IO;
using Pinokio.Animation;
using Pinokio.Object;
using DevExpress.XtraTreeList;
using Simulation.Engine;
using Pinokio.Model.Base;

namespace Pinokio.Designer
{
    public partial class ModelDesigner : UserControl
    {
        private bool _isCheckBoxClick = false;

        private TreeNode GetTreeNode(string name, uint id)
        {
            TreeNode node = new TreeNode(name);
            node.Text = name;
            node.Name = id.ToString();
            node.Checked = true;
            return node;
        }

        private string GetRootTreeNode(string path)
        {
            if (path.Contains(Path.DirectorySeparatorChar))
                return path.Split(Path.DirectorySeparatorChar)[0];
            else
                return path;
        }

        private bool isRootTreeNode(SimNode node)
        {
            if (node is Floor || node is MES || node is MCS)
                return true;
            else
                return false;
        }

        private int GetIntegerInString(string str)
        {
            int FloorStr = Convert.ToInt32(Regex.Replace(str, @"\D", ""));
            return FloorStr;
        }

        /// <summary>
        /// 체크박스 클릭시의 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimNodesTreeList_AfterCheckNode(object sender, NodeEventArgs e)
        {
            SimNodeTreeListNode treeNode = e.Node as SimNodeTreeListNode;

            if (treeNode.SimNode != null)
            {
                if (isRootTreeNode(treeNode.SimNode))
                {
                    pinokio3DModel1.SelectedFloorID = treeNode.SimNode.ID;

                    for (int i = 0; i < pinokio3DModel1.Layers.Count; i++)
                    {
                        if (pinokio3DModel1.Layers[i].Name == treeNode.SimNode.ID.ToString())
                        {
                            pinokio3DModel1.Grids[i].Visible = treeNode.Checked;
                            pinokio3DModel1.Layers[i].Visible = treeNode.Checked;
                            break;
                        }
                    }
                }
            }
            ChildCheckIfParentCheck(treeNode);
            pinokio3DModel1.Invalidate();

            _isCheckBoxClick = true;

        }


        private void SimNodesTreeList_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_isCheckBoxClick == true)
            {
                _isCheckBoxClick = false;
                return;
            }
            SimNodeTreeListNode selectedNode = (SimNodeTreeListNode)simNodeTreeList.FocusedNode;

            if (selectedNode == null)
                return;

            CloseObjectManipulator();

            SimNodeTreeListNode rootNode = (SimNodeTreeListNode)selectedNode.RootNode;

            if (rootNode != null)
            {
                //simNodeTreeList.ClearSelection();

                if (rootNode.SimNode is Floor)
                {
                    pinokio3DModel1.SelectedFloorID = rootNode.SimNode.ID;
                    pinokio3DModel1.SelectedFloorHeight = GetFloorPlanTreeNode(pinokio3DModel1.SelectedFloorID).FloorBottom;
                }

                ClearPropertyGrid();

                AddSelectedNodeReferenceFromTreeSelect(selectedNode);

                BeginInvoke(new Action(() => pinokio3DModel1.Focus()));
                ChangeCameraPositionBySelectedReferences();

                CheangeSelectedSimObject4PropertyGrid(selectedNode.SimNode);
            }
            else
            {
                ClearSelection();
                initiallzeFloor(_floorForm.LstFloorPlan);
            }
        }

        public void AddSelectedNodeReferenceFromTreeSelect(SimNodeTreeListNode treeNode)
        {
            treeNode.Selected = true;
            if (treeNode.RefNode != null)
            {
                SelectedNodeReferences.Add(treeNode.RefNode);
                treeNode.RefNode.Selected = true;
            }

            foreach (SimNodeTreeListNode child in treeNode.Nodes)
                AddSelectedNodeReferenceFromTreeSelect(child);
        }

        //private void SimEntityTreeList_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        //{
        //    if (_isCheckBoxClick == true)
        //    {
        //        _isCheckBoxClick = false;
        //        return;
        //    }
        //    PartTreeListNode selectedNode = (PartTreeListNode)partTreeList.FocusedNode;

        //    if (selectedNode == null)
        //        return;

        //    CloseObjectManipulator();

        //    ClearPropertyGrid();

        //    SelectedEntityReferences.Add(selectedNode.RefPart);
        //    selectedNode.RefPart.Selected = true;
                        
        //    BeginInvoke(new Action(() => pinokio3DModel1.Focus()));
        //    ChangeCameraPositionBySelectedReferencesEntitiy();
        //    BeginInvoke(new Action(() => pinokio3DModel1.Entities.Regen()));
        //    BeginInvoke(new Action(() => Invalidate()));

        //    CheangeSelectedSimObject4PropertyGrid(selectedNode.Part);          
        //}

        bool isDropCoupledModel = false;

        private void SimNodesTreeList_BeforeDropNode(object sender, BeforeDropNodeEventArgs e)
        {
            if (isDropCoupledModel != true)
            {
                if (e.DestinationNode == null)
                    e.Cancel = true;
                else if ((e.DestinationNode as SimNodeTreeListNode).SimNode.ParentNode != null && (e.DestinationNode as SimNodeTreeListNode).RefNode != null)
                    e.Cancel = true;
                else if (((e.DestinationNode as SimNodeTreeListNode).SimNode is CoupledModel) is false)
                    e.Cancel = true;
            }
        }

        private void SimNodesTreeList_BeforeDragNode(object sender, BeforeDragNodeEventArgs e)
        {
            foreach(SimNodeTreeListNode node in e.Nodes.ToList())
            {
                if (node.SimNode is CoupledModel)
                {
                    foreach(SimNodeTreeListNode compNode in e.Nodes.ToList())
                    {
                        if (compNode.SimNode.ParentNode == node.SimNode)
                            e.Nodes.Remove(compNode);
                    }
                }
            }

            if (isRootTreeNode((e.Node as SimNodeTreeListNode).SimNode))
                e.CanDrag = false;
            else if ((e.Node as SimNodeTreeListNode).SimNode is CoupledModel)
                e.CanDrag = true;
            else if ((e.Node as SimNodeTreeListNode).RefNode is RefLink)
                e.CanDrag = false;
            else
                e.CanDrag = true;
        }

        private void SimNodesTreeList_AfterDropNode(object sender, AfterDropNodeEventArgs e)
        {
            SimNodeTreeListNode fromNode = e.Node as SimNodeTreeListNode;
            SimNodeTreeListNode parentNode = e.Node.ParentNode as SimNodeTreeListNode;
            fromNode.SimNode.ParentNode = parentNode.SimNode as CoupledModel;
            if (fromNode.RefNode == null)
                ChildsLayerNameEqualParentsLayerName(fromNode);

            ClearPropertyGrid();
            Move2NewFloor(fromNode.SimNode);
            ChildCheckIfParentCheck(e.DestinationNode as SimNodeTreeListNode);
            ChangeCameraPositionBySelectedReferences();
            pinokio3DModel1.Invalidate();
            pinokio3DModel1.Focus();
        }

        private void ChildCheckIfParentCheck(SimNodeTreeListNode parentNode)
        {
            NodeReference refNode = parentNode.RefNode;
            if (refNode != null)
                parentNode.RefNode.Visible = parentNode.Checked;

            for (int i = 0; i < parentNode.Nodes.Count; i++)
            {
                parentNode.Nodes[i].Checked = parentNode.Checked;
                refNode = ((SimNodeTreeListNode)parentNode.Nodes[i]).RefNode;
                if (refNode != null)
                    refNode.Visible = parentNode.Nodes[i].Checked;
            }

            foreach (SimNodeTreeListNode childNode in parentNode.Nodes)
                ChildCheckIfParentCheck(childNode);
        }
        private void ChildCheckIfParentCheckforVisibility(SimNodeTreeListNode parentNode, bool isChecked)
        {
            NodeReference refNode = parentNode.RefNode;
            if (refNode != null)
                parentNode.RefNode.Visible = isChecked;

            for (int i = 0; i < parentNode.Nodes.Count; i++)
            {
                parentNode.Nodes[i].Checked = isChecked;
                refNode = ((SimNodeTreeListNode)parentNode.Nodes[i]).RefNode;
                if (refNode != null)
                    refNode.Visible = parentNode.Nodes[i].Checked;
            }

            foreach (SimNodeTreeListNode childNode in parentNode.Nodes)
                ChildCheckIfParentCheck(childNode);
        }
        //private void ChangeCameraPositionBySelectedReferencesEntitiy()
        //{
        //    PartTreeListNode selectedNode = (PartTreeListNode)partTreeList.FocusedNode;

        //    if (selectedNode == null)
        //        return;

        //    PartTreeListNode rootNode = (PartTreeListNode)selectedNode.RootNode;

        //    if (rootNode != null)
        //    {
        //        if (selectedNode.Part != null)
        //        {
        //            pinokio3DModel1.Camera.Location = new Point3D(selectedNode.Part.PosVec3.X, selectedNode.Part.PosVec3.Y);
        //        }
        //    }
        //    else
        //    { BeginInvoke(new Action(() => pinokio3DModel1.ZoomFit())); return; }
        //}

        public void ChangeCameraPositionBySelectedReferences()
        {
            //Floor floorInfo = ModelManager.Instance.SimNodes[pinokio3DModel1.SelectedFloorID] as Floor;

            //Point3D maxPosition = new Point3D(0, 0, 0);
            //Point3D minPosition = new Point3D(floorInfo.FloorWidth, floorInfo.FloorDepth, 100000);

            //foreach (NodeReference refNode in SelectedNodeReferences)
            //{
            //    if (maxPosition.X < refNode.Core.PosVec3.X + refNode.BoxSize.X)
            //        maxPosition.X = refNode.Core.PosVec3.X + refNode.BoxSize.X;

            //    if (maxPosition.Y < refNode.Core.PosVec3.Y + refNode.BoxSize.Y)
            //        maxPosition.Y = refNode.Core.PosVec3.Y + refNode.BoxSize.Y;

            //    if (maxPosition.Z < refNode.Core.PosVec3.Z + refNode.BoxSize.Z)
            //        maxPosition.Z = refNode.Core.PosVec3.Z + refNode.BoxSize.Z;

            //    if (minPosition.X > refNode.Core.PosVec3.X - refNode.BoxSize.X)
            //        minPosition.X = refNode.Core.PosVec3.X - refNode.BoxSize.X;

            //    if (minPosition.Y > refNode.Core.PosVec3.Y - refNode.BoxSize.Y)
            //        minPosition.Y = refNode.Core.PosVec3.Y - refNode.BoxSize.Y;

            //    if (minPosition.Z > refNode.Core.PosVec3.Z - refNode.BoxSize.Z)
            //        minPosition.Z = refNode.Core.PosVec3.Z - refNode.BoxSize.Z;
            //}
            //foreach (EntityReference refEntity in SelectedEntityReferences)
            //{
            //    if (maxPosition.X < refEntity.Core.PosVec3.X + refEntity.BoxSize.X)
            //        maxPosition.X = refEntity.Core.PosVec3.X + refEntity.BoxSize.X;

            //    if (maxPosition.Y < refEntity.Core.PosVec3.Y + refEntity.BoxSize.Y)
            //        maxPosition.Y = refEntity.Core.PosVec3.Y + refEntity.BoxSize.Y;

            //    if (maxPosition.Z < refEntity.Core.PosVec3.Z + refEntity.BoxSize.Z)
            //        maxPosition.Z = refEntity.Core.PosVec3.Z + refEntity.BoxSize.Z;

            //    if (minPosition.X > refEntity.Core.PosVec3.X - refEntity.BoxSize.X)
            //        minPosition.X = refEntity.Core.PosVec3.X - refEntity.BoxSize.X;

            //    if (minPosition.Y > refEntity.Core.PosVec3.Y - refEntity.BoxSize.Y)
            //        minPosition.Y = refEntity.Core.PosVec3.Y - refEntity.BoxSize.Y;

            //    if (minPosition.Z > refEntity.Core.PosVec3.Z - refEntity.BoxSize.Z)
            //        minPosition.Z = refEntity.Core.PosVec3.Z - refEntity.BoxSize.Z;
            //}

            //Point3D cameraPosition = (maxPosition + minPosition) / 2;
            //Point3D zoomSource = maxPosition - minPosition;

            //pinokio3DModel1.Camera.Location = cameraPosition;
            //pinokio3DModel1.Camera.Target = cameraPosition;
            //pinokio3DModel1.Rotate.Center = cameraPosition;
            //double zoomFactor = Math.Abs(1000 / (zoomSource.X + zoomSource.Y + zoomSource.Z));
            //pinokio3DModel1.Camera.ZoomFactor = zoomFactor;

            //카메라 시점 선택한 Node로 이동
            SimNodeTreeListNode selectedNode = (SimNodeTreeListNode)simNodeTreeList.FocusedNode;

            if (selectedNode == null)
                return;

            SimNodeTreeListNode rootNode = (SimNodeTreeListNode)selectedNode.RootNode;

            if (rootNode != null)
            {
                if (rootNode.SimNode is Floor)
                    pinokio3DModel1.SelectedFloorID = rootNode.SimNode.ID;
                if (selectedNode.RefNode != null)
                {
                    pinokio3DModel1.Camera.Location = new Point3D(selectedNode.RefNode.Core.PosVec3.X, selectedNode.RefNode.Core.PosVec3.Y);
                }
            }
            else
            { BeginInvoke(new Action(() => pinokio3DModel1.ZoomFit())); return; }
        }

        private void ModifyTreeViewByText(NodeReference node)
        {
            foreach (SimNodeTreeListNode tree in simNodeTreeList.Nodes)
            {
                SimNodeTreeListNode childNode = null;
                childNode = (SimNodeTreeListNode)simNodeTreeList.FindNodeByFieldValue(simNodeTreeList.ColumnName4NodeName, node.Name);
            }
        }


        public FloorPlan GetFloorPlanTreeNode(uint lastSelectedFloor)
        {
            for (int i = 0; i < _floorForm.LstFloorPlan.Count; i++)
                if (_floorForm.LstFloorPlan[i].Floor.ID == pinokio3DModel1.SelectedFloorID)
                    return _floorForm.LstFloorPlan[i];
            return null;
        }

        private void ChildsLayerNameEqualParentsLayerName(SimNodeTreeListNode parentNode)
        {
            NodeReference refNode = parentNode.RefNode;
            if (refNode != null)
                parentNode.RefNode.LayerName = (parentNode.RootNode as SimNodeTreeListNode).SimNode.ID.ToString();

            for (int i = 0; i < parentNode.Nodes.Count; i++)
            {
                refNode = ((SimNodeTreeListNode)parentNode.Nodes[i]).RefNode;
                if (refNode != null)
                {
                    refNode.LayerName = (parentNode.RootNode as SimNodeTreeListNode).SimNode.ID.ToString();
                    for (int j = 0; j < refNode.ToLinked.Count; j++)
                    {
                        refNode.ToLinked[j].LayerName = (parentNode.RootNode as SimNodeTreeListNode).SimNode.ID.ToString();
                        simNodeTreeList.MoveNode(simNodeTreeList.FindNodeByKeyID(refNode.ToLinked[j].ID), (parentNode.Nodes[i]).ParentNode);
                    }
                }
            }
            foreach (SimNodeTreeListNode childNode in parentNode.Nodes)
                ChildsLayerNameEqualParentsLayerName(childNode);
        }

    }
}
