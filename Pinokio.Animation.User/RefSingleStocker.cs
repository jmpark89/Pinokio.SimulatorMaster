using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using Logger;
using Pinokio.Geometry;
using Pinokio.Model.Base;
using Pinokio.Model.User;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Pinokio.Animation.User
{
    public class RefSingleStocker : NodeReference
    {
        [StorableAttribute(false)]
        public static bool IsInserted = true;
        private static string _blockName = typeof(RefSingleStocker).Name;
        protected static string InnerBlockName = typeof(RefSingleStocker).Name + "_INNER";

        [Browsable(false), StorableAttribute(false)]
        public new static string[] UsableBaseSimNodeType = { typeof(Stocker).Name };

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

        private RefTransportPoint _startPoint;
        private RefTransportPoint _endPoint;
        [StorableAttribute(true)]
        public uint StartStationID
        { get; set; }
        [StorableAttribute(true)]
        public uint EndStationID
        { get; set; }
        public RefTransportPoint StartStation
        {
            get => _startPoint;
            set
            {
                _startPoint = value;
                StartStationID = _startPoint.ID;
            }
        }
        public RefTransportPoint EndStation
        {
            get => _endPoint;
            set
            {
                _endPoint = value;
                EndStationID = _endPoint.ID;
            }
        }
        #region Private Veriable
        [StorableAttribute(false)]
        private bool _isFirstClick = true;
        private List<string> _rackBlockName = new List<string>();
        private List<Block> _createdStockerBlocks = new List<Block>();
        private List<BlockReference> _createdStockerReferences = new List<BlockReference>();
        [StorableAttribute(false)]
        private double _angle = 0;
        [StorableAttribute(false)]
        private Vector3D direction;
        [StorableAttribute(false)]
        private Point3D _start;
        [StorableAttribute(false)]
        private int _bufferY = 1;
        private int _bufferX = 1;
        private double _bufferInterval = 200; 
        private Block _createdLineBlock = null;
        private BlockReference _createdLineReference = null;
        [CategoryAttribute("노드 정보"), DescriptionAttribute("Rail의 높이")]
        public Block CreatedLineBlock { get => _createdLineBlock; set => _createdLineBlock = value; }
        public BlockReference CreatedLineReference { get => _createdLineReference; set => _createdLineReference = value; }
        #endregion


        public RefSingleStocker(string blockName) : base(blockName)
        {
        }

        public static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            try
            {
                AnimationModelManager.CreateCylinder(_blockName, model, 1, 10, System.Drawing.Color.DodgerBlue);
                AnimationModelManager.CreateCylinder(InnerBlockName, model, 1, 10, System.Drawing.Color.DodgerBlue);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        protected BlockReference ConstructInnerBlockReference(double x, double y, double z, double radian)
        {
            return new BlockReference(x, y, z, this.GetType().Name.ToString() + "_INNER", radian);
        }

        public override void Insert_MouseMove(PinokioBaseModel model, Point3D moveTo, Point3D moveFrom)
        {
            try
            {
                if (_isFirstClick)
                {
                    base.Insert_MouseMove(model, moveTo, moveFrom);
                }
                else
                {
                    double distance = Point3D.Distance(this.CurrentPoint, moveTo);
                    double innerCount = distance / ReferenceDefine.StraightConveyorInterver;
                    Vector3D echo = new Vector3D(this.CurrentPoint, moveTo);

                    CreatedLineBlock = new Block("InnerConv");

                    if (model.Blocks.Contains(CreatedLineBlock))
                        model.Blocks.Remove(CreatedLineBlock);

                    double x = this.CurrentPoint.X;
                    double y = this.CurrentPoint.Y;
                    double z = this.CurrentPoint.Z;
                    var radian = PVector2.AbsoluteAngleRadian(new PVector2(0, 1), new PVector2(echo.X, echo.Y));

                    for (int i = 0; i < innerCount; i++)
                    {
                        BlockReference conveyorInner = ConstructInnerBlockReference(x, y, z, 0);

                        conveyorInner.Rotate(radian, Vector3D.AxisZ, this.CurrentPoint);
                        conveyorInner.ColorMethod = colorMethodType.byEntity;
                        conveyorInner.Color = System.Drawing.Color.FromArgb(1, System.Drawing.Color.White);
                        CreatedLineBlock.Entities.Add(conveyorInner);
                        y += ReferenceDefine.StraightConveyorInterver;
                    }

                    model.Blocks.Add(CreatedLineBlock);
                    CreatedLineReference = new BlockReference("InnerConv");

                    if (model.Entities.Contains(CreatedLineReference))
                        model.Entities.Remove(CreatedLineReference);

                    model.Entities.Add(CreatedLineReference, System.Drawing.Color.FromArgb(100, model.Blocks[this.BlockName].Entities[0].Color));

                    Vector3D delta = Vector3D.Subtract(moveTo, moveFrom);

                    TranslateTempEntity(TempEntities[0], delta);

                    TempEntities[0].Rotate(-_angle, Vector3D.AxisZ, moveTo);
                    TempEntities[0].Rotate(radian, Vector3D.AxisZ, moveTo);

                    TempEntities[1].Rotate(-_angle, Vector3D.AxisZ, this.CurrentPoint);
                    TempEntities[1].Rotate(radian, Vector3D.AxisZ, this.CurrentPoint);

                    _angle = radian;

                    model.TempEntities.UpdateBoundingBox();
                    model.Entities.Regen();
                    model.Invalidate();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        public override bool Insert_MouseUp(PinokioBaseModel model, Point3D moveTo, out List<NodeReference> returnValues)
        {
            returnValues = new List<NodeReference>();
            if (_isFirstClick)
            {
                _start = moveTo;
                _isFirstClick = false;

                if (TempEntities != null && TempEntities.Count > 0)
                {
                    Vector3D delta = (Vector3D)TempEntities[0].EntityData;
                    CurrentPoint += delta;
                    this.TempEntities.Add(UtilityBR.GenerateObjectMesh(this, model.Blocks));
                    model.TempEntities.Add(this.TempEntities.LastOrDefault(), System.Drawing.Color.FromArgb(100, model.Blocks[this.BlockName].Entities[0].Color));

                    Vector3D ent2Delta = Vector3D.Subtract(moveTo, CurrentPoint);
                    this.TempEntities[1].Translate(ent2Delta);
                    this.TempEntities[1].Regen(0.1);
                    if (this.TempEntities[1].EntityData == null || this.TempEntities[1].EntityData.GetType() != typeof(Vector3D))
                    {
                        this.TempEntities[1].EntityData = ent2Delta;
                    }
                    else
                    {
                        this.TempEntities[1].EntityData = ((Vector3D)this.TempEntities[1].EntityData) + ent2Delta;
                    }

                    model.TempEntities.UpdateBoundingBox();
                }

                return true;
            }
            else
            {

                Dictionary<string, object> datas = new Dictionary<string, object>();

                ModifyUserNodeValueForm valueForm = new ModifyUserNodeValueForm();
                valueForm.SetUserData(nameof(_bufferX), _bufferX);
                valueForm.SetUserData(nameof(_bufferY), _bufferY);
                valueForm.SetUserData(nameof(_bufferInterval), _bufferInterval);
                valueForm.ShowDialog();

                if (valueForm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    _bufferX = Convert.ToInt32(valueForm.gridView1.GetRowCellValue(0, "Value").ToString());
                    _bufferY = Convert.ToInt32(valueForm.gridView1.GetRowCellValue(1, "Value").ToString());
                    _bufferInterval = Convert.ToDouble(valueForm.gridView1.GetRowCellValue(2, "Value").ToString());
                }

                List<NodeReference> node = new List<NodeReference>();

                if (model.Blocks.Contains(CreatedLineBlock))
                    model.Blocks.Remove(CreatedLineBlock);

                if (model.Entities.Contains(CreatedLineReference))
                    model.Entities.Remove(CreatedLineReference);

                Vector3D direction = new Vector3D(_start, moveTo);
                double distance = RefBuffer.Size * _bufferX + _bufferInterval * (_bufferX + 1);
                Vector3D Direction = new Vector3D(direction.X, direction.Y);
                Vector3D normalDirection = new Vector3D(direction.X, direction.Y);
                normalDirection.Normalize();
                Direction = normalDirection * distance;
                var end = Vector3D.Add(_start, Direction);

                CreateObject(model, _start, end, normalDirection, _bufferX, _bufferY, _bufferInterval, out returnValues);

                return true;
            }

        }

        public void CreateObject(PinokioBaseModel model, Point3D startPoint, Point3D endPoint, Vector3D normalDirection, int bufferXcount, int bufferYcount, double bufferInterval, out List<NodeReference> returnValues)
        {
            returnValues = new List<NodeReference>();

            startPoint.Z += Height;
            endPoint.Z += Height;

            Stocker stocker = new Stocker();
            stocker.ParentNode = ModelManager.Instance.SimNodes[model.SelectedFloorID] as CoupledModel;
            ModelManager.Instance.AddNode(stocker);

            SCS scs = new SCS();
            scs.ParentNode = stocker;
            ModelManager.Instance.AddNode(scs);

            //Line 및 Point 만들기
            RefTransportLine2D line = new RefTransportLine2D(typeof(RefTransportLine2D).Name);
            line.MatchingObjType = typeof(CraneLine).Name;
            line.Height = Height;            
            line.Insert_MouseUp(model, startPoint, out returnValues);
            line.Insert_MouseUp(model, endPoint, out returnValues);
            ((TransportLine)returnValues.First().Core).IsTwoWay = true;

            foreach (NodeReference nodeReference in returnValues)
            {
                nodeReference.Core.ParentNode = scs;
                AddNodeReference(model, nodeReference);
            }

            Point3D stationPoint = startPoint + normalDirection * bufferInterval;
            //일정 간격으로 Station 만들기

            List<NodeReference> subNodeReturnValues;
            for (int i = 0; i < bufferXcount; i++)
            {
                RefLineStation station = new RefLineStation(typeof(RefLineStation).Name);
                station.LineID = line.Core.ID;
                station.MatchingObjType = typeof(CraneLineStation).Name;
                subNodeReturnValues = new List<NodeReference>();
                station.Insert_MouseUp(model, stationPoint, out subNodeReturnValues);
                station.Core.ParentNode = scs;
                AddNodeReference(model, station);
                stationPoint += normalDirection * (bufferInterval + RefBuffer.Size);
                returnValues.AddRange(subNodeReturnValues);

                for (int j = 0; j < bufferYcount; j++)
                {
                    RefLeftSTB leftStb = new RefLeftSTB(typeof(RefLeftSTB).Name);
                    leftStb.StationID = station.Core.ID;
                    leftStb.MatchingObjType = typeof(LeftSTB).Name;
                    subNodeReturnValues = new List<NodeReference>();
                    leftStb.Insert_MouseUp(model, stationPoint, out subNodeReturnValues);
                    leftStb.Core.ParentNode = stocker;
                    AddNodeReference(model, subNodeReturnValues.First());
                    returnValues.AddRange(subNodeReturnValues);

                    RefRightSTB rightStb = new RefRightSTB(typeof(RefRightSTB).Name);
                    rightStb.StationID = station.Core.ID;
                    rightStb.MatchingObjType = typeof(RightSTB).Name;
                    subNodeReturnValues = new List<NodeReference>();
                    rightStb.Insert_MouseUp(model, stationPoint, out subNodeReturnValues);
                    rightStb.Core.ParentNode = stocker;
                    AddNodeReference(model, subNodeReturnValues.First());
                    returnValues.AddRange(subNodeReturnValues);
                }
            }

            //Crane 만들기
            RefCrane crane = new RefCrane(typeof(RefCrane).Name);
            crane.LineID = line.Core.ID;
            crane.MatchingObjType = typeof(Crane).Name;
            subNodeReturnValues = new List<NodeReference>();
            crane.Insert_MouseUp(model, (startPoint + endPoint)/2, out subNodeReturnValues);
            subNodeReturnValues.First().Core.ParentNode = scs;
            AddNodeReference(model, subNodeReturnValues.First());
            returnValues.AddRange(subNodeReturnValues);
        }

        private void AddNodeReference(PinokioBaseModel model, NodeReference nodeReference)
        {
            model.AddNodeReference(nodeReference);
            if (model.Entities.Contains(nodeReference) is false)
                model.Entities.Add(nodeReference);

            if (nodeReference.Core != null)
                nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

            if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
                ModelManager.Instance.AddNode(nodeReference.Core);
        }

        public override void DrawOverlay(PinokioBaseModel model, devDept.Eyeshot.Environment.DrawSceneParams myParams)
        {
            try
            {
                if (!_isFirstClick)
                {
                    Point3D lastpoint = _start;
                    Point3D point = model.MouseLocationSnapToCorrdinate;
                    UtillFunction.SnapToGrid(ref lastpoint);
                    UtillFunction.SnapToGrid(ref point);
                    Vector3D echo = new Vector3D(point, lastpoint);

                    var degree = PVector2.AbsoluteAngleDegree(new PVector2(0, 1), new PVector2(echo.X, echo.Y));
                    var distance = Point2D.Distance(model.MouseLocationSnapToCorrdinate, lastpoint);

                    //_start = GetPointAtDistance(lastpoint, GetRelativeAngleinCoordinate(degree), distance);

                    //ouble distance = Vector2D.Distance(model.MouseLocationSnapToCorrdinate, this.UpPoints.Last());
                    //int width = (int)Math.Ceiling((distance / RefBuffer.Size));
                }               
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        public Point3D GetPointAtDistance(Point2D startPoint, double angle, double distance)
        {
            // 각도를 라디안으로 변환
            double angleInRadians = angle * Math.PI / 180.0;

            // x, y 좌표 계산
            double x = startPoint.X + distance * Math.Cos(angleInRadians);
            double y = startPoint.Y + distance * Math.Sin(angleInRadians);

            return new Point3D(x, y, 0);

        }

        /// <summary>
        /// 4방향 기준으로 회전값을 보정합니다.
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static double GetRelativeAngleinCoordinate(double degree)
        {
            if (double.IsNaN(degree))
                return 0;
            else if (degree < 45 || degree >= 315)
                return 270;
            else if (degree >= 45 && degree < 135)
                return 0;
            else if (degree >= 135 && degree < 225)
                return 90;
            else if (degree >= 225 && degree < 315)
                return 180;
            else
                return 0;
        }

        public Block GetDynamicConveyorMesh(string blockID, Point3D start, Point3D end)
        {
            Block linkBlock = new Block(blockID);

            if (start == end)
                end = new Point3D(end.X + 1, end.Y, 100);

            end = end - start;
            start = new Point3D(0, 0, 0);
            Point3D registrationPoint = ((start + end) / 2);

            Mesh mes = Mesh.CreateArrow(start, new Vector3D(start, end), 10, Point3D.Distance(start, end), 10, 10, 32, Mesh.natureType.ColorPlain, Mesh.edgeStyleType.None);
            mes.Translate(-registrationPoint.X, -registrationPoint.Y, -registrationPoint.Z);
            mes.ApplyMaterial("RealWhite", textureMappingType.Cubic, 1, 1);
            mes.Color = System.Drawing.Color.Red;
            linkBlock.Entities.Add(mes);

            return linkBlock;
        }
    }
}
