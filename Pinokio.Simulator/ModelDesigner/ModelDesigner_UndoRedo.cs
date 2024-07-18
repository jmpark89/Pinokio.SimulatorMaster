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
using Pinokio.Designer.DataClass;
using System.Reflection;
using Simulation.Engine;
using Pinokio.Model.Base;

namespace Pinokio.Designer
{
    public partial class ModelDesigner
    {
        UndoRedoHistory<UndoRedoClass> _undoRedoHistory = new UndoRedoHistory<UndoRedoClass>(10);

        public UndoRedoHistory<UndoRedoClass> UndoRedoData { get => _undoRedoHistory; }


        private void Redo()
        {
            if (this.UndoRedoData.IsCanRedo)
            {
                ClearSelection();

                UndoRedoClass undoRedoClass = this.UndoRedoData.Redo();                                  
                if (undoRedoClass.ActionType == eUndoRedoActionType.Add)
                {
                    List<SimNode> nodes = new List<SimNode>();
                    List<NodeReference> nodeReferences = new List<NodeReference>();
                    foreach (uint id in undoRedoClass.NodeIDs)
                    {
                        NodeReference node = undoRedoClass.NodeReferences[id];
                        if (node is RefTransportLine)
                        {
                            string BlockID = node.Name;
                            if (!pinokio3DModel1.Blocks.Contains(BlockID))
                            {
                                Block line = ((RefTransportLine)node).GetDynamicConveyorMesh(BlockID, ((RefTransportLine)node).StartStation.CurrentPoint, ((RefTransportLine)node).EndStation.CurrentPoint);
                                pinokio3DModel1.Blocks.Add(line);
                            }
                            ((TransportLine)node.Core).StartPoint.OutLines.Add((TransportLine)node.Core);
                            ((TransportLine)node.Core).EndPoint.InLines.Add((TransportLine)node.Core);
                            ((RefTransportLine)node).StartStation.OutLines.Add((RefTransportLine)node);
                            ((RefTransportLine)node).EndStation.InLines.Add((RefTransportLine)node);
                            ((RefTransportLine)node)._isFirstClick = true;
                        }   // RefTransPortLine connection recovery + block modify
                        if (node is RefLink) 
                        {
                            RefLink refLink = (RefLink)node;
                            string BlockID = node.Name;
                            bool hasLinked = false;
                            foreach (RefLink tempNode in refLink.FromNode.ToLinked)
                            {
                                if (refLink.ID == tempNode.ID)
                                {
                                    hasLinked = true;
                                    break;
                                }
                            }
                            if (!hasLinked)
                                refLink.FromNode.ToLinked.Add(refLink);

                            hasLinked = false;

                            foreach (RefLink tempNode in refLink.ToNode.FromLinked)
                            {
                                if (refLink.ID == tempNode.ID)
                                {
                                    hasLinked = true;
                                    break;
                                }
                            }
                            if (!hasLinked)
                                refLink.ToNode.FromLinked.Add(refLink);

                            if (!pinokio3DModel1.Blocks.Contains(node.Name))
                            {
                                Block line = RefLink.GetDynamicLinkMesh(refLink.LINK3DType, refLink.FromNodePoint, refLink.ToNodePoint, BlockID,
                                            Color.FromArgb(255, Color.White), Color.FromArgb(150, Color.Red));
                                pinokio3DModel1.Blocks.Add(line);
                            }
                        }           // RefLink conncection recovery + block modify
                        if (!ModelManager.Instance.SimNodes.ContainsKey(node.ID) && !ModelManager.Instance.SimNodesByName.ContainsKey(node.Name))
                            ModelManager.Instance.AddNode(node.Core);

                        List<NodeReference> insertNodes = new List<NodeReference>();
                        node.Insert_MouseUp(pinokio3DModel1, node.CurrentPoint, out insertNodes);
                        if (insertNodes.Count > 0)
                        {
                            node = insertNodes[0];
                            undoRedoClass.NodeReferences[id] = node;
                            nodeReferences.AddRange(insertNodes);
                        }

                        pinokio3DModel1.AddNodeReference(node);
                        node.Move_MouseUp(pinokio3DModel1, node.CurrentPoint, node.CurrentPoint); 
                        SelectedNodeReferences.Add(node);
                        node.Selected = true;
                        if (simNodeTreeList.FindNodeByKeyID(node.ID) == null)
                            nodes.Add(node.Core);
                    }

                    pinokio3DModel1.Entities.AddRange(nodeReferences);
                    ModifyNodeTreeList(nodes);
                }
                else if (undoRedoClass.ActionType == eUndoRedoActionType.Delete)
                {
                    List<NodeReference> node2Delete = new List<NodeReference>();
                    foreach (uint id in undoRedoClass.NodeIDs)
                    {
                        NodeReference refNode = undoRedoClass.NodeReferences[id];
                        node2Delete.Add(refNode);
                    }
                    DeleteNodeReference(node2Delete);
                }
                else if (undoRedoClass.ActionType == eUndoRedoActionType.Move)
                {
                    List<Point3D> points = (List<Point3D>)undoRedoClass.ParameterTransferValue;
                    List<Point3D> fromPoints = (List<Point3D>)undoRedoClass.ParameterPriorValue;
                    int index = 0;
                    foreach (uint id in undoRedoClass.NodeIDs)
                    {
                        if (this.pinokio3DModel1.NodeReferenceByID.ContainsKey(id))
                        {
                            NodeReference node = this.pinokio3DModel1.NodeReferenceByID[id];
                            node.Move_MouseMove(this.pinokio3DModel1, points[index], fromPoints[index]);
                            node.Move_MouseUp(this.pinokio3DModel1, node.CurrentPoint, node.CurrentPoint);
                            SelectedNodeReferences.Add(node);
                            node.Selected = true;
                        }
                        ++index;
                    }
                }
                else if (undoRedoClass.ActionType == eUndoRedoActionType.RefParameterModify)
                {
                    foreach (uint id in undoRedoClass.NodeIDs)
                    {
                        if (this.pinokio3DModel1.NodeReferenceByID.ContainsKey(id))
                        {
                            NodeReference refNode = this.pinokio3DModel1.NodeReferenceByID[id];
                            PropertyInfo propertyInfo = refNode.GetType().GetRuntimeProperty(undoRedoClass.ParameterName.ToString());
                            undoRedoClass.ParameterPriorValue = propertyInfo.GetValue(refNode);
                            propertyInfo.SetValue(refNode, undoRedoClass.ParameterTransferValue);
                            SelectedNodeReferences.Add(refNode);
                            refNode.Selected = true;
                        }
                    }
                }
                else if (undoRedoClass.ActionType == eUndoRedoActionType.SimParameterModify)
                {
                    foreach (uint id in undoRedoClass.NodeIDs)
                    {
                        if (simNodeTreeList.FindNodeByKeyID(id) != null)
                        {
                            SimNode node = ((SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(id)).SimNode;
                            PropertyInfo propertyInfo = node.GetType().GetRuntimeProperty(undoRedoClass.ParameterName.ToString());
                            undoRedoClass.ParameterPriorValue = propertyInfo.GetValue(node);
                            propertyInfo.SetValue(node, undoRedoClass.ParameterTransferValue);
                            NodeReference refNode = ((SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(id)).RefNode;

                            if (refNode != null)
                            {
                                SelectedNodeReferences.Add(refNode);
                                refNode.Selected = true;
                            }
                            else
                                propertyGridControlSimObject.SelectedObject = node;

                        }
                    }
                }

                MouseUpMultiSelection();
                pinokio3DModel1.Entities.Regen();
                pinokio3DModel1.Invalidate();
            }
        }
        private void Undo()
        {
            if (this.UndoRedoData.IsCanUndo)
            {
                ClearSelection();
                UndoRedoClass undoRedoClass = this.UndoRedoData.Undo();
                if (undoRedoClass.ActionType == eUndoRedoActionType.Add)
                {
                    List<NodeReference> node2Delete = new List<NodeReference>();
                    foreach (uint id in undoRedoClass.NodeIDs)
                        node2Delete.Add(undoRedoClass.NodeReferences[id]);

                    DeleteNodeReference(node2Delete);
                }
                else if (undoRedoClass.ActionType == eUndoRedoActionType.Delete)
                {
                    List<SimNode> nodes = new List<SimNode>();
                    List<NodeReference> nodeReferences = new List<NodeReference>(); 
                    foreach (uint id in undoRedoClass.NodeIDs)
                    {
                        NodeReference node = undoRedoClass.NodeReferences[id];
                        if (node is RefTransportLine)
                        {
                            string BlockID = node.Name;
                            if (!pinokio3DModel1.Blocks.Contains(BlockID))
                            {
                                Block line = ((RefTransportLine)node).GetDynamicConveyorMesh(BlockID, ((RefTransportLine)node).StartStation.CurrentPoint, ((RefTransportLine)node).EndStation.CurrentPoint);
                                pinokio3DModel1.Blocks.Add(line);
                            }
                            ((TransportLine)node.Core).StartPoint.OutLines.Add((TransportLine)node.Core);
                            ((TransportLine)node.Core).StartPoint.OutNodes.Add(node.Core);
                            ((TransportLine)node.Core).EndPoint.InLines.Add((TransportLine)node.Core);
                            ((TransportLine)node.Core).EndPoint.InNodes.Add(node.Core);
                            ((RefTransportLine)node).StartStation = (RefTransportPoint)undoRedoClass.NodeReferences[((TransportLine)node.Core).StartPoint.ID];
                            ((RefTransportLine)node).EndStation = (RefTransportPoint)undoRedoClass.NodeReferences[((TransportLine)node.Core).EndPoint.ID];
                            ((RefTransportLine)node).StartStation.OutLines.Add((RefTransportLine)node);
                            ((RefTransportLine)node).EndStation.InLines.Add((RefTransportLine)node);
                        }   // RefTransPortLine connection recovery + block modify
                        else if (node is RefLink)
                        {
                            RefLink refLink = (RefLink)node;
                            string BlockID = node.Name;
                            bool hasLinked = false;
                            refLink.FromNode = pinokio3DModel1.NodeReferenceByID[refLink.FromNode.ID];
                            foreach (RefLink tempNode in refLink.FromNode.ToLinked)
                            {
                                if (refLink.ID == tempNode.ID)
                                {
                                    hasLinked = true;
                                    break;
                                }
                            }
                            if (!hasLinked)
                                refLink.FromNode.ToLinked.Add(refLink);

                            hasLinked = false;

                            refLink.ToNode = pinokio3DModel1.NodeReferenceByID[refLink.ToNode.ID];
                            foreach (RefLink tempNode in refLink.ToNode.FromLinked)
                            {
                                if (refLink.ID == tempNode.ID)
                                {
                                    hasLinked = true;
                                    break;
                                }
                            }
                            if (!hasLinked)
                                refLink.ToNode.FromLinked.Add(refLink);

                            if (!pinokio3DModel1.Blocks.Contains(node.Name))
                            {
                                Block line = RefLink.GetDynamicLinkMesh(refLink.LINK3DType, refLink.FromNodePoint, refLink.ToNodePoint, BlockID,
                                            Color.FromArgb(255, Color.White), Color.FromArgb(150, Color.Red));
                                pinokio3DModel1.Blocks.Add(line);
                            }
                            Link link = node.Core as Link;
                            if (ModelManager.Instance.SimNodes.ContainsKey(link.FromID))
                            {
                                if (ModelManager.Instance.SimNodes[link.FromID] is TransportPoint)
                                {
                                    TransportPoint transportPoint = ModelManager.Instance.SimNodes[link.FromID] as TransportPoint;
                                    transportPoint.ConnectNode(ModelManager.Instance.SimNodes[link.ToID]);
                                }
                                else if (ModelManager.Instance.SimNodes[link.FromID] is TXNode)
                                {
                                    TXNode linkFromTXNode = ModelManager.Instance.SimNodes[link.FromID] as TXNode;
                                    linkFromTXNode.ConnectNode(ModelManager.Instance.SimNodes[link.ToID]);
                                }
                            }
                        }           // RefLink conncection recovery + block modify
                        if (!ModelManager.Instance.SimNodes.ContainsKey(node.ID) && !ModelManager.Instance.SimNodesByName.ContainsKey(node.Name))
                            ModelManager.Instance.AddNode(node.Core);

                        List<NodeReference> insertNodes = new List<NodeReference>();
                        node.Insert_MouseUp(pinokio3DModel1, node.CurrentPoint, out insertNodes);
                        if (insertNodes.Count > 0)
                        {
                            node = insertNodes[0] as NodeReference;
                            undoRedoClass.NodeReferences[id] = node;
                            nodeReferences.AddRange(insertNodes);
                        }

                        pinokio3DModel1.AddNodeReference(node);
                        node.Move_MouseUp(pinokio3DModel1, node.CurrentPoint, node.CurrentPoint);
                        SelectedNodeReferences.Add(node);
                        node.Selected = true;
                        if (simNodeTreeList.FindNodeByKeyID(node.ID) == null)
                            nodes.Add(node.Core);
                    }
                    pinokio3DModel1.Entities.AddRange(nodeReferences);
                    ModifyNodeTreeList(nodes);
                }
                else if (undoRedoClass.ActionType == eUndoRedoActionType.Move)
                {
                    List<Point3D > points = (List<Point3D>)undoRedoClass.ParameterPriorValue;
                    List<Point3D> fromPoints = (List<Point3D>)undoRedoClass.ParameterTransferValue;
                    int index = 0;
                    foreach (uint id in undoRedoClass.NodeIDs)
                    {
                        if (this.pinokio3DModel1.NodeReferenceByID.ContainsKey(id))
                        {
                            NodeReference node = this.pinokio3DModel1.NodeReferenceByID[id];
                            node.Move_MouseMove(this.pinokio3DModel1, points[index], fromPoints[index]);
                            node.Move_MouseUp(this.pinokio3DModel1, node.CurrentPoint, node.CurrentPoint);
                            SelectedNodeReferences.Add(node);
                            node.Selected = true;
                        }
                        ++index;
                    }
                }
                else if (undoRedoClass.ActionType == eUndoRedoActionType.RefParameterModify)
                {
                    foreach (uint id in undoRedoClass.NodeIDs)
                    {
                        if (this.pinokio3DModel1.NodeReferenceByID.ContainsKey(id))
                        {
                            NodeReference refNode = this.pinokio3DModel1.NodeReferenceByID[id];
                            PropertyInfo propertyInfo = refNode.GetType().GetRuntimeProperty(undoRedoClass.ParameterName.ToString());
                            undoRedoClass.ParameterTransferValue = propertyInfo.GetValue(refNode);
                            propertyInfo.SetValue(refNode, undoRedoClass.ParameterPriorValue);
                            SelectedNodeReferences.Add(refNode);
                            refNode.Selected = true;
                        }
                    }
                }
                else if (undoRedoClass.ActionType == eUndoRedoActionType.SimParameterModify)
                {
                    foreach (uint id in undoRedoClass.NodeIDs)
                    {
                        if (simNodeTreeList.FindNodeByKeyID(id) != null)
                        {
                            SimNode node = ((SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(id)).SimNode;
                            PropertyInfo propertyInfo = node.GetType().GetRuntimeProperty(undoRedoClass.ParameterName.ToString());
                            undoRedoClass.ParameterTransferValue = propertyInfo.GetValue(node);
                            propertyInfo.SetValue(node, undoRedoClass.ParameterPriorValue);
                            NodeReference refNode = ((SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(id)).RefNode;

                            if (refNode != null)
                            {
                                SelectedNodeReferences.Add(refNode);
                                refNode.Selected = true;
                            }
                            else
                                propertyGridControlSimObject.SelectedObject = node;

                        }
                    }
                }

                MouseUpMultiSelection();
                pinokio3DModel1.Entities.Regen();
                pinokio3DModel1.Invalidate();
            }
        }

        public void AddUndo(eUndoRedoActionType eUndoRedoActionType, List<uint> ids, object parameterName, object parameterValue,object transferValue =null)
        {
            UndoRedoClass undoRedoClass = new UndoRedoClass() { ActionType = eUndoRedoActionType, NodeIDs = ids, ParameterName = parameterName, ParameterPriorValue = parameterValue, ParameterTransferValue = transferValue };

            foreach (uint id in ids)
            {
                if(pinokio3DModel1.NodeReferenceByID.Keys.Contains(id))
                {
                    NodeReference refNode = pinokio3DModel1.NodeReferenceByID[id];
                    undoRedoClass.NodeReferences.Add(id, refNode);
                }
            }

            undoRedoClass.NodeReferences = undoRedoClass.NodeReferences.OrderBy(x => x.Value.Core.LoadLevel).ToDictionary(x => x.Key, x => x.Value);
            
            _undoRedoHistory.AddState(undoRedoClass);
        }
        /// <summary>
        /// UndoList 추가
        /// </summary>
        public void AddUndo(eUndoRedoActionType eUndoRedoActionType, List<NodeReference> nodes, object parameterName, object parameterValue,object transferValue = null)
        {
            List<uint> ids = new List<uint>();
            nodes = nodes.OrderBy(x => x.LoadLevel).ToList();
            foreach (NodeReference nodeReference in nodes)
                ids.Add(nodeReference.ID);
            AddUndo(eUndoRedoActionType, ids, parameterName, parameterValue, transferValue);
        }      
    }

    public class UndoRedoClass
    {

        public eUndoRedoActionType ActionType { get; set; }

        public List<uint> NodeIDs = new List<uint>();

        public Dictionary<uint, NodeReference> NodeReferences = new Dictionary<uint, NodeReference>();

        public object ParameterName { get; set; }

        public object ParameterPriorValue { get; set; }

        public object ParameterTransferValue { get; set; }
    }
    public enum eUndoRedoActionType
    {
        Add,
        Delete,
        Move,
        SimParameterModify,
        RefParameterModify
    }
}
