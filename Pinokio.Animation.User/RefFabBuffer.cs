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

namespace Pinokio.Animation.User
{
    public class RefFabBuffer : NodeReference
    {
        public static bool IsInserted = true;

        [Browsable(false), StorableAttribute(false)]
        public new static string[] UsableBaseSimNodeType = 
            { 
            typeof(Pinokio.Model.Base.Buffer).Name,
            typeof(Pinokio.Model.Base.Equipment).Name,
            typeof(Pinokio.Model.Base.EqpPort).Name,
            typeof(Pinokio.Model.User.SimpleAssemblyMachine).Name,
            typeof(Pinokio.Model.User.Stay).Name
        };

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

        public RefFabBuffer(string blockName) : base(blockName)
        {
        }
        public static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            try
            {
                Block bl = new Block(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
                double l = 1000;
                double hegith = 1000;
                Entity buffer = Mesh.CreateBox(l, l, hegith);
                buffer.Translate(-buffer.BoxSize.X / 2, -buffer.BoxSize.Y / 2, 0);
                buffer.ColorMethod = colorMethodType.byParent;
                buffer.Color = System.Drawing.Color.LightGray;
                buffer.Scale(0.2);
                bl.Entities.Add(buffer);
                model.Blocks.Add(bl);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
    }
}
