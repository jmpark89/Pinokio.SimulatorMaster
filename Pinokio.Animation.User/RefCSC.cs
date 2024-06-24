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
    public class RefCSC : RefVehicle
    {
        public static bool IsInserted = true;
        public new static double InitialHeight = 500;

        public RefCSC() : base(nameof(RefCSC))
        {

        }
        public RefCSC(string blockName) : base(blockName)
        {
            this.IsPossibleMoved = false;
        }

        public RefCSC(string blockName, SimObj simObj) : base(blockName)
        {
            this.IsPossibleMoved = false;
            Core = simObj as SimNode;
        }

        public static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            Block block = LoadModelFile("C:\\Carlo\\Pinokio\\Pinokio.Asset\\ohs.obj", nameof(RefCSC), model);

            foreach (Entity ent in block.Entities)
            {
                ent.Rotate(Math.PI / 2, new Vector3D(0, 0, 1));
                ent.Scale(0.65);
                ent.Translate(0, 0, ent.BoxSize.Z / 2 - 1500);
                ent.ColorMethod = colorMethodType.byParent;
            }

            model.Blocks.Add(block);
        }
        protected override string GetTypeName()
        {
            return nameof(RefCSC);
        }
    }
}
