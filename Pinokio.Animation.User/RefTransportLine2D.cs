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
using System.Linq;

namespace Pinokio.Animation.User
{
    [Serializable]
    public class RefTransportLine2D : RefTransportLine
    {
        [StorableAttribute(false)]
        public new static bool IsInserted = true;

        public new static double InitialHeight = 0;

        private static string _blockName = typeof(RefTransportLine2D).Name;
        protected new static string InnerBlockName = typeof(RefTransportLine2D).Name + "_INNER";

        [Browsable(false), StorableAttribute(false)]
        public new static string[] UsableBaseSimNodeType = { typeof(TransportLine).Name };

        public RefTransportLine2D(string blockName) : base(blockName)
        {
            IsPossibleMoved = false;
        }
        public RefTransportLine2D(string blockName, SimObj simObj) : base(blockName)
        {
            Core = simObj as SimNode;
            IsPossibleMoved = false;
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

        public override Block GetDynamicConveyorMesh(string blockID, Point3D start, Point3D end)
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

        public override void MoveTo(DrawParams data)
        {
            base.MoveTo(data);
        }
    }
}
