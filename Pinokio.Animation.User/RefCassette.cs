using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using devDept.Geometry;
using Logger;
using Pinokio.Geometry;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinokio.Animation.User
{
    public  class RefCassette : PartReference
    {
        private Transformation _customTransform = new Transformation();

        public RefCassette(string blockName ) : base(blockName)
        {

        }

        public RefCassette(string blockName,SimObj entity) : base(blockName)
        {
            Core = entity;

        }

        public static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            try
            {
                string path = "C:\\Carlo\\Pinokio\\Pinokio.Asset\\LCD\\LCDPart.obj";
                if (File.Exists(path))
                {
                    string blockName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                    Block bl = new Block(blockName);
      
                    ReadOBJ readObj = new ReadOBJ(path);

                    readObj.DoWork();


                    for (int i = 0; i < readObj.Entities.Length; i++)
                        bl.Entities.Add(readObj.Entities[i]);
                    Point3D avg = new Point3D();
                    foreach (Entity en in bl.Entities)
                    {
                        avg += en.BoxMax;
                        avg += en.BoxMin;
                    }

                    foreach (Entity en in bl.Entities)
                    {
                        en.Scale(0.9);
                    }

                    model.Blocks.Add(bl);
                }
                else
                {
                    string blockName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                    Block bl = new Block(blockName);
                    double l = 1000;
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
        //    return base.IsInFrustum(data, t * center, radius);
        //}
    }
}
