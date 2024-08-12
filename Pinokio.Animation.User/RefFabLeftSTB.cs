using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using Logger;
using Pinokio.Database;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pinokio.Model.Base;
using System.ComponentModel;
using Pinokio.Geometry;
using System.Windows.Forms;

namespace Pinokio.Animation.User
{
    public class RefFabLeftSTB : RefLeftSTB
    {
        public static bool IsInserted = true;

        public RefFabLeftSTB(string blockName) : base(blockName)
        {
        }

        public new static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            try
            {
                Block bl = new Block(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
                double l = 600;
                double height = 200;
                Entity buffer = Mesh.CreateBox(l, l, height);
                buffer.Translate(-buffer.BoxSize.X / 2, -buffer.BoxSize.Y / 2, 0);
                buffer.ColorMethod = colorMethodType.byParent;
                buffer.Color = System.Drawing.Color.FromArgb(100, System.Drawing.Color.LightGray);
                bl.Entities.Add(buffer);


                model.Blocks.Add(bl);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        protected override string GetTypeName()
        {
            return nameof(RefFabLeftSTB);
        }
    }
}