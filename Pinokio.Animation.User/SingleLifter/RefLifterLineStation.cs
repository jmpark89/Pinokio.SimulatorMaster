using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using Logger;
using Pinokio.Database;
using Pinokio.Geometry;
using Pinokio.Model.Base;

using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinokio.Animation.User
{
    public class RefLifterLineStation : RefLineStation
    {
        public static bool IsInserted = false;
        private int _lastEntityIndex = -1;
        private double _stationDirection = 0;

        public RefLifterLineStation(string blockName) : base(blockName)
        {
        }

        public RefLifterLineStation(string blockName, SimObj simObj) : base(blockName, simObj)
        {
        }

        public static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            Block bl = new Block(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
            Entity buffer = Mesh.CreateBox(500, 50, 50);
            buffer.Translate(-buffer.BoxSize.X / 2, -buffer.BoxSize.Y / 2, 0);
            buffer.ColorMethod = colorMethodType.byEntity;
            buffer.Color = System.Drawing.Color.LightSkyBlue;
            bl.Entities.Add(buffer);
            model.Blocks.Add(bl);
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
                }

                // gets the entity index under mouse cursor
                int entityIndex = model.GetEntityUnderMouseCursor(model.MouseLocation);
                if (entityIndex >= 0)
                    refNode = (NodeReference)model.Entities[entityIndex];
                if (refNode == null && LineID != 0)
                    refNode = model.NodeReferenceByID[LineID];
                if (model.NodeReferenceByID[LineID] != null)
                {
                    refNode = model.NodeReferenceByID[LineID];
                }
                if (refNode is RefTransportLine)
                {
                    RefTransportLine refLine = refNode as RefTransportLine;
                    TransportLine line = refLine.Core as TransportLine;

                    double distance = 0;

                    if (moveTo.X == 0 && moveTo.Y == 0)
                    {
                        if (Core is Sensor && ((Sensor)Core).Length > 0)
                            distance = ((Sensor)Core).Length;
                        else if (Core is LineStation && ((LineStation)Core).Length > 0)
                            distance = ((LineStation)Core).Length;
                    }
                    else
                    {
                        PVector3 PVector3 = new PVector3(moveTo.X, moveTo.Y, line.StartPoint.PosVec3.Z);
                        distance = Math.Round(moveTo.Z);
                    }

                    ConnectToLine(refLine, this, distance);

                    if (Core is Sensor)
                        ((Sensor)Core).UpdateAnimationPos();
                    else if (Core is LineStation)
                        ((LineStation)Core).UpdateAnimationPos();

                    PVector3 pos = _core.PosVec3;
                    double[,] rotationMatrix = GetRotationMatrix();
                    Transformation tr = new Transformation(rotationMatrix);
                    Transformation tl = new Translation(pos.X, pos.Y, pos.Z) * tr;

                    this.Transformation = tl;

                    returnValues.Add(this);
                    _lastEntityIndex = -1; // 그냥 마우스 오버됐을 시 새로 갱신함
                    return true;
                }
                else
                {
                    MessageBox.Show("해당 Node는 라인 위에 삽입되어야 합니다.");
                    _lastEntityIndex = -1; // 그냥 마우스 오버됐을 시 새로 갱신함
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
                _lastEntityIndex = -1; // 그냥 마우스 오버됐을 시 새로 갱신함
                return false;
            }
        }

        private double[,] GetRotationMatrix()
        {
            Rotation rotation = new Rotation(_stationDirection, Vector3D.AxisZ);
            return rotation.Matrix;
        }

        public void SetDirection(double direction)
        {
            this._stationDirection = MathHelper.ToRadians(direction);
        }
    }
}
