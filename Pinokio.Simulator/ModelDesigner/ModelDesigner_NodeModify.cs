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
using Pinokio.Object;
using Pinokio.Model.Base;
using Simulation.Engine;

namespace Pinokio.Designer
{
    public partial class ModelDesigner
    {
        /// <summary>
        /// 고유한 노드 아이디를 노드 타입에 따라 생성
        /// </summary>
        /// <param name="nodeType"></param>
        /// <returns></returns>

        /// <summary>
        /// Node를 추가한 뒤에 공통적으로 발생되는 이벤트
        /// </summary>
        private void FinishAddNode()
        {
            if (pinokio3DModel1.TempEntities.Count > 0)
                pinokio3DModel1.TempEntities.Clear();

            CurrentRef = null;
            _modelActionType = ModelActionType.None;

           // pinokio3DModel1.Entities.Regen();
        }
        /// <summary>
        /// 노드를 삭제하고 트리에 반영함
        /// </summary>
        private void DeleteNodeReference(List<NodeReference> nodeReferences)
        {
            //pinokio3DModel1.RefreshCtr();

            List<uint> nodeReferenceIds = new List<uint>();

            if (nodeReferences.Count == 0)
                return;

            foreach(NodeReference deleteReference in nodeReferences)
            {
                deleteReference.Clear(pinokio3DModel1);
            }

            foreach (NodeReference deleteReference in nodeReferences)
            {
                SimNodeTreeListNode treeNode = (SimNodeTreeListNode)simNodeTreeList.FindNodeByKeyID(deleteReference.ID);
                simNodeTreeList.DeleteNode(treeNode);
                ModelManager.Instance.RemoveNode(deleteReference.ID);
                nodeReferenceIds.Add(deleteReference.ID);
            }
            pinokio3DModel1.DeleteNodeReference(nodeReferenceIds);
            ClearSelection();
        }

        private void DeleteNodeReference(List<uint> ids)
        {
            //pinokio3DModel1.RefreshCtr();
            List<NodeReference> nodeReferences = new List<NodeReference>();
            foreach (uint id in ids)
            {
                if (this.pinokio3DModel1.NodeReferenceByID.ContainsKey(id))
                    nodeReferences.Add(this.pinokio3DModel1.NodeReferenceByID[id]);
            }
            DeleteNodeReference(nodeReferences);
        }

        private void SelectEveryNodes(SimNodeTreeListNode treeNode, ref List<SimNode> selectedNodes)
        {
            selectedNodes.Add(treeNode.SimNode);
            foreach(SimNodeTreeListNode childNode in treeNode.Nodes)
            {
                SelectEveryNodes(childNode, ref selectedNodes);
            }
        }

        private void DeletePartReference(PartReference EntityReference)
        {
            if (ModelManager.Instance.AnimationNode != null && ModelManager.Instance.AnimationNode.IsUse)
                partTreeList.BeginInvoke(new Action(() =>
                {
                    PartTreeListNode treeNode = (PartTreeListNode)partTreeList.FindNodeByKeyID(EntityReference.ID);
                    partTreeList.DeleteNode(treeNode);
                }));
        }

        private void CutNode()
        {
            if (SelectedNodeReferences.Count == 0)  // if no node selected, function does not work
                return;

            CopyNode();
            IsCutMode = true;
            CutNodeVisible(false);      // Make cut object invisible  (or Opacitiy adjust)
        }
    }
}
