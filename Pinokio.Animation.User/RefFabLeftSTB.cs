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
                double height = 600;
                Entity buffer = Mesh.CreateBox(l, l, height);
                buffer.Translate(-buffer.BoxSize.X / 2, -buffer.BoxSize.Y / 2, 0);
                buffer.ColorMethod = colorMethodType.byParent;
                buffer.Color = System.Drawing.Color.FromArgb(100, System.Drawing.Color.LightGray);
                bl.Entities.Add(buffer);

                //// Scale the entities
                //foreach (Entity en in bl.Entities)
                //{
                //    en.Scale(0.2); // Scale(0.2)
                //}

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

        protected override void ConnectToLineStation(RefLineStation refLineStation, RefLineStationComponent refLeftSTB)
        {
            refLeftSTB.StationID = refLineStation.ID;
            refLeftSTB.LayerName = refLineStation.LayerName;

            LeftSTB stb = refLeftSTB.Core as LeftSTB;

            if (stb.Height == -1)
            {
                if (refLineStation.LeftSTBs.Count == 0)
                    stb.Height = Height;
                else
                    stb.Height = refLineStation.LeftSTBHighestHeight - refLineStation.Core.PosVec3.Z;
            }

            refLineStation.LeftSTBs.Add((RefFabLeftSTB)refLeftSTB);

            LineStation stationCore = refLineStation.Core as LineStation;
            stb.LineStation = stationCore;
            stationCore.ConnectNode(stb);
            stationCore.LeftSTBs.Add(stb.Height, stb);
            stb.ConnectNode(stationCore);
        }

        protected override void UnconnectToLineStation(RefLineStation refLineStation, RefLineStationComponent refLeftSTB)
        {
            LineStation stationCore = refLineStation.Core as LineStation;
            LeftSTB stb = refLeftSTB.Core as LeftSTB;
            refLineStation.LeftSTBs.Remove((RefFabLeftSTB)refLeftSTB);
            stb.LineStation = null;
            stationCore.UnconnectNode(stb);
            stationCore.LeftSTBs.Remove(stb.Height);
            stb.UnconnectNode(stationCore);
            stb.Height = -1;
        }
    }
}