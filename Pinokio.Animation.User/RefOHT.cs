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
    public class RefOHT : RefVehicle
    {
        public new static bool IsInserted = true;

        public new static double InitialHeight = -500;

        public RefOHT() : base(nameof(RefOHT))
        {

        }
        public RefOHT(string blockName) : base(blockName)
        {
            this.IsPossibleMoved = false;
        }

        public RefOHT(string blockName, Simulation.Engine.SimObj simObj) : base(blockName)
        {
            this.IsPossibleMoved = false;
            Core = simObj as SimNode;
        }

        public static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            Block block = LoadModelFile("C:\\Carlo\\Pinokio\\Pinokio.Asset\\oht.obj", nameof(RefOHT), model);
            model.Blocks.Add(block);
        }

        protected override string GetTypeName()
        {
            return nameof(RefOHT);
        }
    }
}
