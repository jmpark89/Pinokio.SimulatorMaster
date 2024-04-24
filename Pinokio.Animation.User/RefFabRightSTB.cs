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
    public class RefFabRightSTB : RefRightSTB
    {
        public static bool IsInserted = true;

        public RefFabRightSTB(string blockName) : base(blockName)
        {
        }

        public new static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            try
            {
                Block bl = new Block(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
                double l = 600;
                double height = 600;
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
            return nameof(RefFabRightSTB);
        }

        protected override void ConnectToLineStation(RefLineStation refLineStation, RefLineStationComponent refRightSTB)
        {
            refRightSTB.StationID = refLineStation.ID;
            refRightSTB.LayerName = refLineStation.LayerName;

            RightSTB stb = refRightSTB.Core as RightSTB;

            if (stb.Height == -1)
            {
                if (refLineStation.RightSTBs.Count == 0)
                    stb.Height = Height;
                else
                    stb.Height = refLineStation.RightSTBHighestHeight - refLineStation.Core.PosVec3.Z;
            }

            refLineStation.RightSTBs.Add((RefFabRightSTB)refRightSTB);

            LineStation stationCore = refLineStation.Core as LineStation;
            stb.LineStation = stationCore;
            stationCore.ConnectNode(stb);
            stationCore.RightSTBs.Add(stb.Height, stb);
            stb.ConnectNode(stationCore);
        }

        protected override void UnconnectToLineStation(RefLineStation refLineStation, RefLineStationComponent refRightSTB)
        {
            LineStation stationCore = refLineStation.Core as LineStation;
            RightSTB stb = refRightSTB.Core as RightSTB;
            refLineStation.RightSTBs.Remove((RefFabRightSTB)refRightSTB);
            stb.LineStation = null;
            stationCore.UnconnectNode(stb);
            stationCore.RightSTBs.Remove(stb.Height);
            stb.UnconnectNode(stationCore);
            stb.Height = -1;
        }
    }
}
