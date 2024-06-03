using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using devDept.Geometry;
using Logger;
using Pinokio.Model.Base;
using Pinokio.Model.User;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinokio.Animation.User
{
    public class RefBox : PartReference
    {

        public RefBox(string blockName, SimObj entity) : base(blockName)
        {
            Core = entity;
        }

        public RefBox(string blockName ) : base(blockName)
        {
        }

        public static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            try
            {
                string path = Path.Combine(UtillFunction.Get3DFolderPath(), "boxSteel.obj");

                {
                    string blockName = typeof(RefBox).Name;
                    Block bl = new Block(blockName);
                    double l = 2000;
                    Mesh mBox = Mesh.CreateBox(l, l, l);
                    mBox.Translate(-l / 2, -l / 2, 0);
                    mBox.ColorMethod = colorMethodType.byEntity;
                    mBox.Color = System.Drawing.Color.FromArgb(128, System.Drawing.Color.Blue);
                    bl.Entities.Add(mBox);

                    model.Blocks.Add(bl);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        //Transformation t;

        //public override bool IsInFrustum(FrustumParams data, Point3D center, double radius)
        //{
        //    return base.IsInFrustum(data, t* center, radius);
        //}
    }
}
