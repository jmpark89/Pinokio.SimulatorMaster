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
    public class RefOHTLine : RefTransportLine
    {
        public new static bool IsInserted = true;

        public new static double InitialHeight = 2000;

        private static string _blockName = typeof(RefOHTLine).Name;
        protected new static string InnerBlockName = typeof(RefOHTLine).Name + "_INNER";

        [Browsable(false), StorableAttribute(false)]
        public new static string[] UsableBaseSimNodeType = { typeof(GuidedLine).Name };

        public RefOHTLine(string blockName) : base(blockName)
        {

        }
        public RefOHTLine(string blockName, SimObj simObj) : base(blockName)
        {
            Core = simObj as SimNode;
        }

        public new static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            AnimationModelManager.CreateCylinder(_blockName, model, 1, 50, System.Drawing.Color.DodgerBlue);
            AnimationModelManager.CreateCylinder(InnerBlockName, model, 1, 50, System.Drawing.Color.DodgerBlue);
        }

        public override Block GetDynamicConveyorMesh(string blockID, Point3D start, Point3D end)
        {
            Block linkBlock = new Block(blockID);

            if (start == end)
                end = new Point3D(end.X + 1, end.Y, 100);

            end = end - start;
            start = new Point3D(0, 0, 0);
            Point3D registrationPoint = ((start + end) / 2);

            Mesh mes = Mesh.CreateArrow(start, new Vector3D(start, end), 50, Point3D.Distance(start, end), 10, 10, 32, Mesh.natureType.ColorPlain, Mesh.edgeStyleType.None);
            mes.Translate(-registrationPoint.X, -registrationPoint.Y, -registrationPoint.Z);
            mes.ApplyMaterial("RealWhite", textureMappingType.Cubic, 1, 1);
            mes.ColorMethod = colorMethodType.byParent;
            mes.Color = System.Drawing.Color.Gray; //Color.FromArgb(255, Color.White);
            linkBlock.Entities.Add(mes);

            return linkBlock;
        }

        public override void MoveTo(DrawParams data)
        {
            base.MoveTo(data);
        }
    }
}