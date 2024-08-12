using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using Logger;
using Pinokio.Database;
using Pinokio.Geometry;
using Pinokio.Model.Base;
using Pinokio.Model.User;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinokio.Animation.User
{

    [Serializable]
    public class RefCrane : RefVehicle
    {
        public new static bool IsInserted = true;

        [Browsable(false), StorableAttribute(false)]
        public new static string[] UsableBaseSimNodeType = {typeof(Crane).Name};

        [Browsable(false), StorableAttribute(false)]
        private static List<string> _usableSimNodeTypes;

        public new static List<string> UsableSimNodeTypes
        {
            get
            {
                if (_usableSimNodeTypes == null)
                    _usableSimNodeTypes = BaseUtill.GetUsableSimNodeTypes(UsableBaseSimNodeType, InterfaceConstraints);

                return _usableSimNodeTypes;
            }
        }

        public new static double InitialHeight = 0;

        public RefCrane() : base(nameof(RefCrane))
        {
        }
        public RefCrane(string blockName) : base(blockName)
        {
            this.IsPossibleMoved = false;
        }

        public RefCrane(string blockName, Simulation.Engine.SimObj simObj) : base(blockName)
        {
            this.IsPossibleMoved = false;
            Core = simObj as SimNode;
        }

        public static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            string blockName = nameof(RefCrane);

            CreateCraneBlock(model, blockName, null);
        }

        public static void CreateCraneBlock(PinokioBaseModel model, string blockName, Crane crane)
        {
            Block bl = new Block(blockName);

            //bottom
            Entity entity = Mesh.CreateBox(1500, 5000, 500);
            entity.Translate(-entity.BoxSize.X / 2, -entity.BoxSize.Y / 2, 0);
            entity.ColorMethod = colorMethodType.byParent;
            entity.Color = System.Drawing.Color.FromArgb(255, System.Drawing.Color.PapayaWhip);
            bl.Entities.Add(entity);
            //pillar
            entity = Mesh.CreateBox(200, 200, 6000);
            entity.Translate(-entity.BoxSize.X / 2, -entity.BoxSize.Y / 2 + 1700 + 100, 0);
            entity.ColorMethod = colorMethodType.byParent;
            entity.Color = System.Drawing.Color.FromArgb(255, System.Drawing.Color.PapayaWhip);
            bl.Entities.Add(entity);

            entity = Mesh.CreateBox(200, 200, 6000);
            entity.Translate(-entity.BoxSize.X / 2, -entity.BoxSize.Y / 2 - 1700 - 100, 0);
            entity.ColorMethod = colorMethodType.byParent;
            entity.Color = System.Drawing.Color.FromArgb(255, System.Drawing.Color.PapayaWhip);
            bl.Entities.Add(entity);

            if (crane != null)
            {
                //elevator
                entity = Mesh.CreateBox(1500, 3400, 100);
                entity.Translate(-entity.BoxSize.X / 2, -entity.BoxSize.Y / 2, 500);
                entity.ColorMethod = colorMethodType.byEntity;
                entity.Color = System.Drawing.Color.FromArgb(255, System.Drawing.Color.LightSeaGreen);
                string elevBlockName = blockName + "_elevator";
                Block bElv = new Block(elevBlockName);
                bElv.Entities.Add(entity);

                //fork
                string forkBlockName = blockName + "_fork";
                Block forkBlock = NodeReference.LoadModelFile("C:\\Carlo\\Pinokio\\Pinokio.Asset\\crane2.obj", forkBlockName, model);

                foreach (Entity ent in forkBlock.Entities)
                {
                    ent.Rotate(Math.PI / 2, new Vector3D(1, 0, 0));
                    ent.Scale(2);
                    ent.Translate(0, 0, ent.BoxSize.Z / 2 + 400);
                    ent.ColorMethod = colorMethodType.byParent;
                }

                model.Blocks.Add(forkBlock);
                bElv.Entities.Add(new RefStockerFork(forkBlockName, crane));

                model.Blocks.Add(bElv);

                bl.Entities.Add(new RefStockerElevator(elevBlockName, crane));
            }

            model.Blocks.Add(bl);
        }

        protected override string GetTypeName()
        {
            return nameof(RefCrane);
        }

        public override void UpdateCorePos()
        {
            if (_core != null)
            {
                Crane stocker = (Crane)_core;
                PVector3 pos = new PVector3(_core.PosVec3.X, _core.PosVec3.Y, stocker.Floor.FloorBottom);

                PVector3 di = new PVector3(stocker.Line.Direction.X, stocker.Line.Direction.Y, stocker.Line.Direction.Z);
                double radian = PVector3.AngleRadian(new PVector3(0, 1, 0), di, PVector3.Coordinate.Z);

                double[,] rotateMatrix = GetRotate(radian, PVector3.UnitZ);
                Transformation tr = new Transformation(rotateMatrix);
                Transformation tl = new Translation(pos.X, pos.Y, pos.Z) * tr;

                if (!EqualTransformation(tl, this.Transformation) && SimEngine.Instance.EngineState == ENGINE_STATE.RUNNING)
                {
                    CurrentTransformationForAnimation = tl;
                }
            }
        }

        public override bool Insert_MouseUp(PinokioBaseModel model, Point3D moveTo, out List<NodeReference> returnValues)
        {
            returnValues = new List<NodeReference>();
            try
            {
                NodeReference refNode = null;
                if (_core == null)
                {
                    _core = CreateMatchSimObj() as SimNode;
                    ((Vehicle)_core).Height = Height;
                    // gets the entity index under mouse cursor
                }

                CreateCraneBlock(model, _core.Name, (Crane)_core);

                RefVehicle refVehicle = (RefVehicle)CreateNodeReference(_core.Name);
                refVehicle.Core = (SimNode)_core;
                refVehicle.Height = Height;
                int entityIndex = model.GetEntityUnderMouseCursor(model.MouseLocation);
                if (entityIndex >= 0)
                    refNode = (NodeReference)model.Entities[entityIndex];

                if ((refNode == null || refNode is RefTransportPoint) && LineID != 0)
                    refNode = model.NodeReferenceByID[LineID];

                if (refNode is RefTransportLine && refNode.Core is GuidedLine)
                {
                    RefTransportLine refLine = refNode as RefTransportLine;
                    GuidedLine line = refLine.Core as GuidedLine;
                    Vehicle vehicle = this.Core as Vehicle;

                    double distance = 0;
                    if (moveTo.X == 0 && moveTo.Y == 0 && vehicle.Distance > 0)
                        distance = vehicle.Distance;
                    else
                    {
                        PVector3 PVector3 = new PVector3(moveTo.X, moveTo.Y, line.StartPoint.PosVec3.Z);
                        distance = Math.Round(PVector3.Distance(line.StartPoint.PosVec3, PVector3), 0);
                    }

                    ConnectToLine(refLine, refVehicle, distance);

                    PVector3 di = new PVector3(vehicle.Line.Direction.X, vehicle.Line.Direction.Y, vehicle.Line.Direction.Z);
                    double radian = PVector3.AngleRadian(new PVector3(0, 1, 0), di, PVector3.Coordinate.Z);

                    vehicle.SetRotate(radian, PVector3.UnitZ);
                    vehicle.UpdateAnimationPos();

                    PVector3 pos = _core.PosVec3;

                    Transformation tr = new Transformation(_core.RotateMatrix);
                    Transformation tl = new Translation(pos.X, pos.Y, pos.Z) * tr;

                    refVehicle.Transformation = tl;

                    returnValues.Add(refVehicle);
                    return true;
                }
                else
                {
                    MessageBox.Show("Vehicle은 Guided Line 위에 삽입되어야 합니다");
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
                return false;
            }
        }
    }

    public class RefStockerElevator : SubObjectReference
    {
        Crane StockerCrane = null;

        public RefStockerElevator(string blockName, Crane crane)
            : base(blockName, crane)
        {
            StockerCrane = crane;
        }

        public override void UpdatePos()
        {
            if (StockerCrane != null)
            {
                PVector3 pos = new PVector3(0, 0, StockerCrane.PosVec3.Z - StockerCrane.Floor.FloorBottom);
                double radian = 0;
                double[,] rotateMatrix = GetRotate(radian, PVector3.UnitZ);
                UpdateTransformation(pos, rotateMatrix);
            }
        }
    }

    public class RefStockerFork : SubObjectReference
    {
        Crane StockerCrane = null;
        public RefStockerFork(string blockName, SimNode crane)
            : base(blockName, crane)
        {
            StockerCrane = crane as Crane;
        }

        public override void UpdatePos()
        {
            if (StockerCrane != null && SimEngine.Instance.EngineState is ENGINE_STATE.STOP
                || (ModelManager.Instance.AnimationNode != null && ModelManager.Instance.AnimationNode.IsUse))
            {
                PVector3 pos = new PVector3(0, 0, 0);
                double[,] rotateMatrix = StockerCrane.RotateMatrix;
                UpdateTransformation(pos, rotateMatrix);
            }
        }
    }
}
