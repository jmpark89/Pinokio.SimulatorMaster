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
                switch(undoRedoClass.ActionType)
                {
                    case eUndoRedoActionType.Add:
                        List<SimNode> nodes = new List<SimNode>();
                        List<NodeReference> nodeReferences = new List<NodeReference>();
                        foreach (uint id in undoRedoClass.NodeIDs)
                        {
                            NodeReference node = undoRedoClass.NodeReferences[id];
                            if (node is RefTransportLine)
                            {
                                List<NodeReference> returnValues;
                                ((RefTransportLine)node).CreateObject(pinokio3DModel1, node.Core, ((RefTransportLine)node).StartStation.CurrentPoint - new Point3D(0, 0, node.Height), ((RefTransportLine)node).EndStation.CurrentPoint - new Point3D(0, 0, node.Height), ((RefTransportLine)node).StartStation.ID, ((RefTransportLine)node).EndStation.ID, out returnValues);
                                RefTransportLine newLine = returnValues.First() as RefTransportLine;
                                nodeReferences.Add(newLine);
                                node = newLine;
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
                                    Block line = ((RefLink)node).GetDynamicLinkMesh(refLink.FromNodePoint, refLink.ToNodePoint, BlockID);
                                    pinokio3DModel1.Blocks.Add(line);
                                }
                            }
                            // RefLink conncection recovery + block modify
                            if (!ModelManager.Instance.SimNodes.ContainsKey(node.ID) && !ModelManager.Instance.SimNodesByName.ContainsKey(node.Name))
                                ModelManager.Instance.AddNode(node.Core);

                            List<NodeReference> insertNodes = new List<NodeReference>();
                            if (!(node is TransportLine))
                            {
                                node.Insert_MouseUp(pinokio3DModel1, node.CurrentPoint, out insertNodes);
                                if (insertNodes.Count > 0)
                                {
                                    node = insertNodes[0];
                                    undoRedoClass.NodeReferences[id] = node;
                                    nodeReferences.AddRange(insertNodes);
                                }
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
                        break;
                    case eUndoRedoActionType.Delete:
                        List<NodeReference> node2Delete = new List<NodeReference>();
                        foreach (uint id in undoRedoClass.NodeIDs)
                        {
                            NodeReference refNode = undoRedoClass.NodeReferences[id];
                            node2Delete.Add(refNode);
                        }
                        DeleteNodeReference(node2Delete);
                        break;
                    case eUndoRedoActionType.Move:
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
                        break;
                    case eUndoRedoActionType.PointMerge:
                        points = (List<Point3D>)undoRedoClass.ParameterPriorValue;
                        fromPoints = (List<Point3D>)undoRedoClass.ParameterTransferValue;
                        Point3D point = points[0];
                        Point3D fromPoint = fromPoints[0];
                        RefTransportPoint refMergingPoint = (RefTransportPoint)undoRedoClass.NodeReferences.Values.ElementAt(0);
                        RefTransportPoint refUnderPoint = (RefTransportPoint)undoRedoClass.NodeReferences.Values.ElementAt(1);
                        //refMergingPoint.Move_MouseMove(this.pinokio3DModel1, fromPoint, point);
                        refMergingPoint.Move_MouseUp(this.pinokio3DModel1, refMergingPoint.CurrentPoint, refMergingPoint.CurrentPoint);

                        MergeLinePoints(pinokio3DModel1, refMergingPoint, refUnderPoint);

                        SelectedNodeReferences.Add(refMergingPoint);
                        refMergingPoint.Selected = true;

                        break;
                    case eUndoRedoActionType.LineSeparate:
                        node2Delete = new List<NodeReference>();
                        foreach (uint id in undoRedoClass.NodeIDs)
                            node2Delete.Add(undoRedoClass.NodeReferences[id]);

                        RefTransportLine refFrontLine;
                        RefTransportLine refBackLine;
                        RefTransportPoint refStartPoint;
                        RefTransportPoint refEndPoint;
                        RefTransportPoint refMidPoint;
                        RefTransportLine refBranchLine;
                        RefTransportPoint refBranchStartPoint;
                        if(node2Delete.Count == 5)
                        {
                            refFrontLine = ((RefTransportLine)node2Delete[2]);
                            refBackLine = ((RefTransportLine)node2Delete[3]);
                            refStartPoint = ((RefTransportLine)node2Delete[2]).StartStation;
                            refEndPoint = ((RefTransportLine)node2Delete[3]).EndStation;
                            refMidPoint = (RefTransportPoint)node2Delete[0];
                            refBranchLine = (RefTransportLine)node2Delete[4];
                            refBranchStartPoint = (RefTransportPoint)node2Delete[1];
                        }
                        else
                        {
                            refFrontLine = ((RefTransportLine)node2Delete[1]);
                            refBackLine = ((RefTransportLine)node2Delete[2]);
                            refStartPoint = ((RefTransportLine)node2Delete[1]).StartStation;
                            refEndPoint = ((RefTransportLine)node2Delete[2]).EndStation;
                            refMidPoint = (RefTransportPoint)node2Delete[0];
                            refBranchLine = null;
                            refBranchStartPoint = null;
                        }

                        List<NodeReference> addValues = new List<NodeReference>();
                        RefTransportLine refAsisLine = (RefTransportLine)pinokio3DModel1.NodeReferenceByID[refFrontLine.ID];
                        TransportLine frontLine = (TransportLine)refFrontLine.Core;
                        node2Delete.Remove(refMidPoint);
                        node2Delete.Remove(refFrontLine);
                        node2Delete.Remove(refBackLine);
                        if(refBranchLine != null)
                            node2Delete.Remove(refBranchLine);

                        node2Delete.Add(refAsisLine);
                        DeleteNodeReference(node2Delete);
                        frontLine.ChangeEndPoint(refMidPoint.Core as TransportPoint);

                        pinokio3DModel1.AddNodeReference(refMidPoint);
                        if (pinokio3DModel1.Entities.Contains(refMidPoint) is false)
                            pinokio3DModel1.Entities.Add(refMidPoint);

                        refFrontLine.ReconnectToSeparatedLine(pinokio3DModel1, refAsisLine, refFrontLine, refBackLine, frontLine.Length);

                        List<NodeReference> returnValuesTemp;
                        refFrontLine.CreateObject(pinokio3DModel1, refFrontLine.Core, refStartPoint.CurrentPoint - new Point3D(0, 0, refFrontLine.Height), refMidPoint.CurrentPoint - new Point3D(0, 0, refFrontLine.Height), refStartPoint.ID, refMidPoint.ID, out returnValuesTemp);
                        RefTransportLine newFrontRefLine = returnValuesTemp.First() as RefTransportLine;
                        addValues.AddRange(returnValuesTemp);
                        refEndPoint.InLines.Remove(refAsisLine);

                        returnValuesTemp = new List<NodeReference>();
                        refBackLine.CreateObject(pinokio3DModel1, refBackLine.Core, refMidPoint.CurrentPoint - new Point3D(0, 0, refBackLine.Height), refEndPoint.CurrentPoint - new Point3D(0, 0, refBackLine.Height), refMidPoint.ID, refEndPoint.ID, out returnValuesTemp);
                        RefTransportLine newBackRefLine = returnValuesTemp.First() as RefTransportLine;
                        addValues.AddRange(returnValuesTemp);

                        pinokio3DModel1.AddNodeReference(newFrontRefLine);
                        if (pinokio3DModel1.Entities.Contains(newFrontRefLine) is false)
                            pinokio3DModel1.Entities.Add(newFrontRefLine);
                        pinokio3DModel1.AddNodeReference(newBackRefLine);
                        if (pinokio3DModel1.Entities.Contains(newBackRefLine) is false)
                            pinokio3DModel1.Entities.Add(newBackRefLine);

                        if (!ModelManager.Instance.SimNodes.ContainsKey(refBackLine.Core.ID) && !ModelManager.Instance.SimNodesByName.ContainsKey(refBackLine.Name))
                            ModelManager.Instance.AddNode(refBackLine.Core);
                        if (!ModelManager.Instance.SimNodes.ContainsKey(refMidPoint.Core.ID) && !ModelManager.Instance.SimNodesByName.ContainsKey(refMidPoint.Name))
                            ModelManager.Instance.AddNode(refMidPoint.Core);

                        if (refBranchLine != null)
                        {
                            pinokio3DModel1.AddNodeReference(refBranchStartPoint);
                            if (pinokio3DModel1.Entities.Contains(refBranchStartPoint) is false)
                                pinokio3DModel1.Entities.Add(refBranchStartPoint);

                            returnValuesTemp = new List<NodeReference>();
                            refBranchLine.CreateObject(pinokio3DModel1, refBranchLine.Core, refBranchStartPoint.CurrentPoint - new Point3D(0, 0, refBranchLine.Height), refMidPoint.CurrentPoint - new Point3D(0, 0, refBranchLine.Height), refBranchStartPoint.ID, refMidPoint.ID, out returnValuesTemp);
                            RefTransportLine newBranchRefLine = returnValuesTemp.First() as RefTransportLine;
                            addValues.AddRange(returnValuesTemp);
                            pinokio3DModel1.AddNodeReference(newBranchRefLine);
                            if (pinokio3DModel1.Entities.Contains(newBranchRefLine) is false)
                                pinokio3DModel1.Entities.Add(newBranchRefLine);

                            if (!ModelManager.Instance.SimNodes.ContainsKey(refBranchLine.Core.ID) && !ModelManager.Instance.SimNodesByName.ContainsKey(refBranchLine.Name))
                                ModelManager.Instance.AddNode(refBranchLine.Core);
                            if (!ModelManager.Instance.SimNodes.ContainsKey(refBranchStartPoint.Core.ID) && !ModelManager.Instance.SimNodesByName.ContainsKey(refBranchStartPoint.Name))
                                ModelManager.Instance.AddNode(refBranchStartPoint.Core);
                        }

                        List<SimNode> insertedSimNodes = addValues.ConvertAll(x => x.Core);

                        ModifyNodeTreeList(insertedSimNodes);
                        break;
                    case eUndoRedoActionType.SimParameterModify:
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
                        break;
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
                switch(undoRedoClass.ActionType)
                {
                    case eUndoRedoActionType.Add:
                        List<NodeReference> node2Delete = new List<NodeReference>();
                        foreach (uint id in undoRedoClass.NodeIDs)
                            node2Delete.Add(undoRedoClass.NodeReferences[id]);

                        DeleteNodeReference(node2Delete);
                        break;
                    case eUndoRedoActionType.Delete:
                        List<SimNode> nodes = new List<SimNode>();
                        List<NodeReference> nodeReferences = new List<NodeReference>();
                        foreach (uint id in undoRedoClass.NodeIDs)
                        {
                            NodeReference node = undoRedoClass.NodeReferences[id];
                            if (node is RefTransportLine)
                            {
                                List<NodeReference> returnValues;
                                ((RefTransportLine)node).CreateObject(pinokio3DModel1, node.Core, ((RefTransportLine)node).StartStation.CurrentPoint, ((RefTransportLine)node).EndStation.CurrentPoint, ((RefTransportLine)node).StartStation.ID, ((RefTransportLine)node).EndStation.ID, out returnValues);
                                RefTransportLine nLine = returnValues.First() as RefTransportLine;
                                nodeReferences.Add(nLine);
                                node = nLine;
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
                                    Block line = ((RefLink)node).GetDynamicLinkMesh(refLink.FromNodePoint, refLink.ToNodePoint, BlockID);
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
                            if (!(node is TransportLine))
                            {
                                node.Insert_MouseUp(pinokio3DModel1, node.CurrentPoint, out insertNodes);
                                if (insertNodes.Count > 0)
                                {
                                    node = insertNodes[0];
                                    undoRedoClass.NodeReferences[id] = node;
                                    nodeReferences.AddRange(insertNodes);
                                }
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
                        break;
                    case eUndoRedoActionType.Move:
                        List<Point3D> points = (List<Point3D>)undoRedoClass.ParameterPriorValue;
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
                        break;
                    case eUndoRedoActionType.PointMerge:
                        points = (List<Point3D>)undoRedoClass.ParameterPriorValue;
                        fromPoints = (List<Point3D>)undoRedoClass.ParameterTransferValue;
                        Point3D point = points[0]; 
                        Point3D fromPoint = fromPoints[0];
                        RefTransportPoint refMergingPoint = (RefTransportPoint)undoRedoClass.NodeReferences.Values.ElementAt(0);
                        RefTransportPoint refUnderPoint = (RefTransportPoint)undoRedoClass.NodeReferences.Values.ElementAt(1);

                        if (!ModelManager.Instance.SimNodes.ContainsKey(refMergingPoint.ID) && !ModelManager.Instance.SimNodesByName.ContainsKey(refMergingPoint.Name))
                            ModelManager.Instance.AddNode(refMergingPoint.Core);

                        List<NodeReference> insertPoints = new List<NodeReference>();
                        refMergingPoint.Insert_MouseUp(pinokio3DModel1, refMergingPoint.CurrentPoint, out insertPoints);
                        if (insertPoints.Count > 0)
                        {
                            RefTransportPoint newPoint = insertPoints[0] as RefTransportPoint;
                            newPoint.InLines = refMergingPoint.InLines.ToList();
                            newPoint.OutLines = refMergingPoint.OutLines.ToList();
                            refMergingPoint = newPoint;
                            undoRedoClass.NodeReferences[refMergingPoint.ID] = refMergingPoint;
                        }

                        pinokio3DModel1.AddNodeReference(refMergingPoint);
                        if(!pinokio3DModel1.Entities.Contains(refMergingPoint))
                            pinokio3DModel1.Entities.Add(refMergingPoint);
                        //refMergingPoint.Move_MouseMove(this.pinokio3DModel1, point, fromPoint);
                        refMergingPoint.Move_MouseUp(this.pinokio3DModel1, refMergingPoint.CurrentPoint, refMergingPoint.CurrentPoint);
                        SelectedNodeReferences.Add(refMergingPoint);
                        refMergingPoint.Selected = true;

                        SplitLinePoints(pinokio3DModel1, refMergingPoint, refUnderPoint);

                        break;
                    case eUndoRedoActionType.LineSeparate:
                        node2Delete = new List<NodeReference>();
                        foreach (uint id in undoRedoClass.NodeIDs)
                            node2Delete.Add(undoRedoClass.NodeReferences[id]);

                        RefTransportPoint startPoint;
                        RefTransportPoint endPoint;
                        RefTransportLine refLine;
                        RefTransportLine refBackLine;
                        if(node2Delete.Count == 5)
                        {
                            startPoint = ((RefTransportLine)node2Delete[2]).StartStation;
                            endPoint = ((RefTransportLine)node2Delete[3]).EndStation;
                            refLine = (RefTransportLine)node2Delete[2];
                            refBackLine = (RefTransportLine)node2Delete[3];
                        }
                        else
                        {
                            startPoint = ((RefTransportLine)node2Delete[1]).StartStation;
                            endPoint = ((RefTransportLine)node2Delete[2]).EndStation;
                            refLine = (RefTransportLine)node2Delete[1];
                            refBackLine = (RefTransportLine)node2Delete[2];
                        }
                        List<NodeReference> addValues = new List<NodeReference>();
                        DeleteNodeReference(node2Delete);
                        refLine.CreateObject(pinokio3DModel1, refLine.Core, startPoint.CurrentPoint - new Point3D(0, 0, refLine.Height), endPoint.CurrentPoint - new Point3D(0, 0, refLine.Height), startPoint.ID, endPoint.ID, out addValues);
                        RefTransportLine newLine = (RefTransportLine)addValues.First();
                        newLine.ReconnectToCombinedLine(pinokio3DModel1, refLine, refBackLine, newLine);
                        pinokio3DModel1.AddNodeReference(newLine);
                        if (pinokio3DModel1.Entities.Contains(newLine) is false)
                            pinokio3DModel1.Entities.Add(newLine);

                        ModelManager.Instance.AddNode(newLine.Core);
                        List<SimNode> insertedSimNodes = addValues.ConvertAll(x => x.Core);

                        ModifyNodeTreeList(insertedSimNodes);

                        break;
                    case eUndoRedoActionType.SimParameterModify:
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
                        break;
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
                    if (!undoRedoClass.NodeReferences.ContainsKey(id))
                        undoRedoClass.NodeReferences.Add(id, refNode);
                    else
                        ;
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
        RefParameterModify,
        LineSeparate,
        PointMerge,
    }
}
