using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using Logger;
using Pinokio.Database;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pinokio.Geometry;

namespace Pinokio.Animation.User
{
    [Serializable]
    public class RefFabEquipment : NodeReference
    {
        public static bool IsInserted = true;

        [Browsable(false), StorableAttribute(false)]
        public new static string[] UsableBaseSimNodeType =
            {
            typeof(Pinokio.Model.Base.Equipment).Name,
            typeof(Pinokio.Model.User.Stay).Name
        };

        [Browsable(false), StorableAttribute(false)]
        private static List<string> _usableSimNodeTypes;

        [Browsable(false), StorableAttribute(false)]
        public new static List<string> UsableSimNodeTypes
        {
            get
            {
                if (_usableSimNodeTypes == null)
                    _usableSimNodeTypes = BaseUtill.GetUsableSimNodeTypes(UsableBaseSimNodeType);

                return _usableSimNodeTypes;
            }
        }

        public RefFabEquipment(string blockName) : base(blockName)
        {
        }

        public static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            try
            {

                string blockName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;

                Block bl = UtilityBR.CreateFabEqp3DBlockModel(blockName, blockName);
                model.Blocks.Add(bl);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        public override bool Insert_MouseUp_second(PinokioBaseModel model, Point3D moveTo, uint idx, out List<NodeReference> returnValues)
        {
            returnValues = new List<NodeReference>();
            try
            {
                if (Core == null)
                    Core = CreateMatchSimObj() as SimNode;

                Block newBlock = new Block(_core.Name);
                Block typeBlock = model.Blocks[this.GetType().Name];
                foreach (Entity entity in typeBlock.Entities)
                {
                    newBlock.Entities.Add(entity);
                }
                if (model.Blocks.Contains(_core.Name) is false)
                    model.Blocks.Add(newBlock);
                NodeReference node = CreateNodeReference(_core.Name);
                node.Core = Core;
                if (_core.PosVec3.X == 0 && _core.PosVec3.Y == 0)
                    node.CurrentPoint = new Point3D(moveTo.X, moveTo.Y, moveTo.Z + Height);

                node.Core.UpdateAnimationPos();

                PVector3 pos = _core.PosVec3;
                NodeReference refNode = null;
                refNode = model.NodeReferenceByID[idx];
                RefTransportLine refLine = refNode as RefTransportLine;
                TransportLine line = refLine.Core as TransportLine;


                Transformation tr = null;
                Vector3D rotationAxis = null;
                double angle = 0;
                if (line.Direction.Y == 1)
                {
                    tr = new Transformation(_core.RotateMatrix);
                    rotationAxis = Vector3D.AxisZ; // Z축을 중심으로 회전하는 예시
                    angle = -Math.PI / 2; // 회전 각도 (도 단위)
                }
                else if (line.Direction.Y == -1)
                {
                    tr = new Transformation(_core.RotateMatrix);
                    rotationAxis = Vector3D.AxisZ; // Z축을 중심으로 회전하는 예시
                    angle = Math.PI / 2; // 회전 각도 (도 단위)
                }
                // 회전 변환 생성
                Transformation rotation = new Rotation(angle, rotationAxis);
                Transformation tl = new Translation(pos.X, pos.Y, pos.Z) * rotation;

                node.Transformation = tl;

                this.LayerName = (model.SelectedFloorID).ToString();
                foreach (Entity e in TempEntities)
                    model.TempEntities.Remove(e);

                returnValues.Add(node);


               
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
                return false;
            }
            return true;
        }
    }
}
