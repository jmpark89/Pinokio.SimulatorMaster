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
    public class RefFabUTB : RefUTB
    {
        public static bool IsInserted = true;

        public new static double InitialHeight = -1500;

        [Browsable(false), StorableAttribute(false)]
        public new static string[] UsableBaseSimNodeType =
            {
            typeof(Pinokio.Model.Base.UTB).Name
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

        public RefFabUTB(string blockName) : base(blockName)
        {
        }
        public static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            try
            {
                Block bl = new Block(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
                double l = 600;
                double hegith = 600;
                Entity buffer = Mesh.CreateBox(l, l, hegith);
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
            return nameof(RefFabUTB);
        }

        protected override void ConnectToLineStation(RefLineStation refLineStation, RefLineStationComponent refUTB)
        {
            refUTB.StationID = refLineStation.ID;
            refUTB.LayerName = refLineStation.LayerName;

            UTB utb = refUTB.Core as UTB;

            refLineStation.UTB = (RefFabUTB)refUTB;

            LineStation stationCore = refLineStation.Core as LineStation;
            utb.LineStation = stationCore;
            stationCore.ConnectNode(utb);
            stationCore.UTB = utb;
            utb.ConnectNode(stationCore);
        }

        protected override void UnconnectToLineStation(RefLineStation refLineStation, RefLineStationComponent refUTB)
        {
            LineStation stationCore = refLineStation.Core as LineStation;
            UTB stb = refUTB.Core as UTB;
            refLineStation.UTB = null;
            stb.LineStation = null;
            stationCore.UnconnectNode(stb);
            stationCore.UTB = null;
            stb.UnconnectNode(stationCore);
            stb.Height = -1;
        }
    }
}
