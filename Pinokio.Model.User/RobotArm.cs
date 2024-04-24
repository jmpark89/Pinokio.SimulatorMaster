namespace Pinokio.Model.User
{
    using Logger;
    using Pinokio.Database;
    using Pinokio.Geometry;
    using Pinokio.Model.Base;
    using global::Simulation.Engine;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RobotArm : Diverter
    {

        public RobotArm()
            : base()
        {
        }

        public RobotArm(uint id, string name) :
                base(id, name)
        {
        }

        public override void UpdateAnimationPos()
        {

            try
            {
                double radian = GetRadianAtTime(SimEngine.Instance.TimeNow);
                SetRotate(radian, PVector3.UnitZ);

                foreach (SimObj obj in EnteredObjects)
                {
//                    obj.PosVec3 = PosVec3 + new PVector3(-Math.Sin(radian), Math.Cos(radian), 0) * (arcRadius + obj.Size.Y) + new PVector3(0, 0, Size.Z - (obj.Size.Z / 2));
                    obj.PosVec3 = PosVec3 + new PVector3(-Math.Sin(radian), Math.Cos(radian), 0) * (arcRadius) + new PVector3(0, 0, Size.Z - (obj.Size.Z / 2));
                    obj.SetRotate(radian, PVector3.UnitZ);
                }
            }
            catch (System.Exception ex)
            {
                // Handle any other exception type here.
                ErrorLogger.SaveLog(ex);
            }
        }
    }
}