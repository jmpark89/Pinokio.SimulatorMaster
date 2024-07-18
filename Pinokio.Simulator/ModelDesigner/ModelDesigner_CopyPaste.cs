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
using Pinokio.Model.Base;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using Simulation.Engine;

namespace Pinokio.Designer
{
    public partial class ModelDesigner : UserControl
    {
        // public List<string> CopyDatas { get => _CopyDatas; set => _CopyDatas = value; }
        List<NodeReference> CopiedClipBoard = new List<NodeReference>();
        bool IsCutMode = false;


        private void ClearCopy()
        {
            CurrentRef = null;
            CopiedClipBoard.Clear();
        }
        public void CopyNode()
        {
            ClearCopy();      
            CopiedClipBoard.AddRange(SelectedNodeReferences);                       
        }

        public void PasteNode()
        {
            Point3D centerPosition = new Point3D(0, 0, 0);
            CopiedClipBoard = CopiedClipBoard.OrderBy(x => x.Core.LoadLevel).ToList();

            foreach (NodeReference refNode in CopiedClipBoard)
                centerPosition = centerPosition + refNode.CurrentPoint;
            centerPosition /= CopiedClipBoard.Count;

            centerPosition.Z = 0;
            
            Point3D tempPositionEditor = pinokio3DModel1.MouseLocationSnapToCorrdinate - centerPosition;
            double newX = Math.Floor(tempPositionEditor.X / 100) * 100;
            double newY = Math.Floor(tempPositionEditor.Y / 100) * 100;
            double newZ = Math.Floor(tempPositionEditor.Z / 100) * 100;

            Point3D positionEditor = new Point3D(newX, newY, newZ);

            if (IsCutMode)
            {
                CutNodeVisible(true);       // Make cut object visible (or Opacitiy adjust)
                AddUndo(eUndoRedoActionType.Move, CopiedClipBoard, null, GetNodeReferenceCurrentPoints(CopiedClipBoard));
                foreach (NodeReference refNode in CopiedClipBoard)
                    refNode.CurrentPoint += positionEditor;
                IsCutMode = false;
                ClearCopy();
            }
            else
            {
                Dictionary<Point3D, List<NodeReference>> dicRefNodesbyPoint = new Dictionary<Point3D, List<NodeReference>>();
                List<NodeReference> undoRedos = new List<NodeReference>();
                List<NodeReference> nodes = new List<NodeReference>();

                CopiedClipBoard = CopiedClipBoard.OrderBy(x => x.LoadLevel).ToList();

                try
                {
                    foreach (NodeReference node in CopiedClipBoard)
                    {
                        Point3D moveTo = new Point3D();
                        moveTo = node.CurrentPoint + positionEditor;

                        moveTo.X = Math.Round(moveTo.X, 0);
                        moveTo.Y = Math.Round(moveTo.Y, 0);
                        moveTo.Z = Math.Round(moveTo.Z, 0);

                        node.Paste(pinokio3DModel1, moveTo, ref dicRefNodesbyPoint, out nodes);

                        if (nodes.Count == 0)
                            continue;
                        else
                        {
                            NodeReference newNode = nodes[0];

                            newNode.Core.ParentNode = ((SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(pinokio3DModel1.SelectedFloorID)).SimNode as CoupledModel;
                            if (newNode.Core is TXNode)
                                ((TXNode)newNode.Core).UpdateFloor();

                            newNode.LayerName = pinokio3DModel1.SelectedFloorID.ToString();
                            pinokio3DModel1.AddNodeReference(newNode);
                            pinokio3DModel1.Entities.Add(newNode);
                        }

                        undoRedos.AddRange(nodes);

                        List<SimNode> insertedSimNodes = nodes.ConvertAll(x => x.Core);
                        foreach (NodeReference nodeReference in nodes)
                        {
                            nodeReference.FinishAddNode(pinokio3DModel1);
                            ModelManager.Instance.AddNode(nodeReference.Core);

                            if (nodeReference.Core is Equipment)
                                FactoryManager.Instance.Eqps.Add(nodeReference.Core.ID, nodeReference.Core as Equipment);
                        }
                        ModifyNodeTreeList(insertedSimNodes);
                    }
                }
                catch (Exception ex)
                {
                    ClearSelectedNode(undoRedos);
                    SetFloorform();
                    FloorPlanUpdate();
                    MessageBox.Show("Paste ERROR. TRY AGAIN");
                    return;
                }

                undoRedos = undoRedos.OrderBy(x => x.LoadLevel).ToList();
                simNodeTreeList.FindNodeByKeyID(pinokio3DModel1.SelectedFloorID).ExpandAll();
                AddUndo(eUndoRedoActionType.Add, undoRedos, null, null);
            }
            pinokio3DModel1.Entities.Regen();
            pinokio3DModel1.Invalidate();
        }
        private byte[] Serialize(object obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            stream.Position = 0;
            return stream.ToArray();
        }

        //이진 형식으로 직렬화된 데이터를 클래스 인스턴스로 역직렬화
        private T Deserialize<T>(byte[] buffer)
        {

            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(buffer);
            return (T)formatter.Deserialize(stream);

        }
    }

   
   
}
