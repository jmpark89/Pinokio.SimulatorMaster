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
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinokio.Animation.User
{
    [Serializable]
    public class RefSingleForkElevator : RefVehicle
    {
        public new static bool IsInserted = true;

        [Browsable(false), StorableAttribute(false)]
        public new static string[] UsableBaseSimNodeType = { typeof(Elevator).Name };

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

        private double _lifterDirection = 0;
        public RefSingleForkElevator() : base(nameof(RefSingleForkElevator))
        {
        }
        public RefSingleForkElevator(string blockName) : base(blockName)
        {
            this.IsPossibleMoved = false;

        }

        public RefSingleForkElevator(string blockName, Simulation.Engine.SimObj simObj) : base(blockName)
        {
            this.IsPossibleMoved = false;
            Core = simObj as SimNode;
        }

        public static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            string blockName = nameof(RefSingleForkElevator);

            CreateElevatorBlock(model, blockName, null);
        }
        public static void CreateElevatorBlock(PinokioBaseModel model, string blockName, Elevator elevator)
        {
            Block bl = new Block(blockName);

            if (elevator != null)
            {
                string elevatorBlockName = blockName + "_liftElevator";
                Block bElv = NodeReference.LoadModelFile("C:\\Carlo\\Pinokio\\Pinokio.Asset\\3D\\Elevator_1.obj", elevatorBlockName, model);
                
                foreach (Entity ent in bElv.Entities)
                {
                    ent.Rotate(Math.PI, new Vector3D(0, 0, 1));
                    ent.Translate(0, 0, 700);
                }

                string forkBlockName = blockName + "_liftfork";
                Block forkBlock = NodeReference.LoadModelFile("C:\\Carlo\\Pinokio\\Pinokio.Asset\\crane2.obj", forkBlockName, model);
                foreach (Entity ent in forkBlock.Entities)
                {   
                    ent.Rotate(Math.PI / 2, new Vector3D(1, 0, 0));
                    ent.Scale(0.7);
                    ent.Translate(0, 100,  600);
                }
                model.Blocks.Add(forkBlock);
                bElv.Entities.Add(new RefElevatorFork(forkBlockName, elevator));

                model.Blocks.Add(bElv);

                bl.Entities.Add(new RefLiftElevator(elevatorBlockName, elevator));
            }
            
            model.Blocks.Add(bl);
        }

        protected override string GetTypeName()
        {
            return nameof(RefSingleForkElevator);
        }

        public override void UpdateCorePos()
        {
            if (_core != null)
            {
                Elevator elevator = (Elevator)_core;
                PVector3 pos = new PVector3(_core.PosVec3.X, _core.PosVec3.Y, _core.PosVec3.Z);

                double radian = 0;

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
                }

                CreateElevatorBlock(model, _core.Name, (Elevator)_core);

                RefVehicle refVehicle = (RefVehicle)CreateNodeReference(_core.Name);
                refVehicle.Core = (SimNode)_core;
                refVehicle.Height = Height;
                int entityIndex = model.GetEntityUnderMouseCursor(model.MouseLocation);
                if (entityIndex >= 0)
                    refNode = (NodeReference)model.Entities[entityIndex];

                if (refNode == null && LineID != 0)
                    refNode = model.NodeReferenceByID[LineID];
                //Line 찾는 코드
                if (model.NodeReferenceByID[LineID] != null)
                {
                    refNode = model.NodeReferenceByID[LineID];
                }

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
                        distance = moveTo.Z;
                    }

                    ConnectToLine(refLine, refVehicle, distance);

                    PVector3 di = new PVector3(vehicle.Line.Direction.X, vehicle.Line.Direction.Y, vehicle.Line.Direction.Z);
                    double radian = PVector3.AngleRadian(new PVector3(0, 1, 0), di, PVector3.Coordinate.Z);

                    vehicle.SetRotate(_lifterDirection, PVector3.UnitZ);
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

        public void SetDirection(double direction)
        {
            this._lifterDirection = MathHelper.ToRadians(direction);
        }
    }

    #region [SubObjectReference]
    public class RefLiftElevator : SubObjectReference
    {
        Elevator LiftElevator = null;

        public RefLiftElevator(string blockName, Elevator liftElevator)
            : base(blockName, liftElevator)
        {
            LiftElevator = liftElevator;
        }

        public override void UpdatePos()
        {
            if (LiftElevator != null)
            {
                PVector3 pos = new PVector3(0, 0, 0);
                double radian = 0;
                double[,] rotateMatrix = GetRotate(radian, PVector3.UnitZ);
                UpdateTransformation(pos, rotateMatrix);
            }
        }
    }

    public class RefElevatorFork : SubObjectReference
    {
        Elevator LiftElevator = null;
        public RefElevatorFork(string blockName, SimNode liftElevator)
            : base(blockName, liftElevator)
        {
            LiftElevator = liftElevator as Elevator;
        }

        public override void UpdatePos()
        {
            if (LiftElevator != null)
            {
                PVector3 pos = new PVector3(0, 0, 0);
                double[,] rotateMatrix = LiftElevator.RotateMatrix;
                UpdateTransformation(pos, rotateMatrix);
            }
        }
    }
    #endregion


}
