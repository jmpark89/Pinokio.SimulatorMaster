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
    public class RefConveyorLine : RefTransportLine
    {
        public new static bool IsInserted = true;
        public new static double InitialHeight = 1000;

        private static string _blockName = typeof(RefConveyorLine).Name;
        protected new static string InnerBlockName = typeof(RefConveyorLine).Name + "_INNER";
        public RefConveyorLine(string blockName) : base(blockName)
        {
        }
        public RefConveyorLine(string blockName, SimObj simObj) : base(blockName)
        {
            Core = simObj as SimNode;
        }

        public new static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            AnimationModelManager.CreatePart(_blockName, "C:\\Carlo\\Pinokio\\Pinokio.Asset\\StraightConveyor_Leg.obj", model, 30, new Point3D(0, 0, 0), 255, System.Drawing.Color.Gray);
            AnimationModelManager.CreatePart(InnerBlockName, "C:\\Carlo\\Pinokio\\Pinokio.Asset\\StraightConveyor_Inner.obj", model, 30, new Point3D(0, 0, 0), 255, System.Drawing.Color.Gray);
        }
    }
}