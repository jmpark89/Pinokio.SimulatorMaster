namespace Pinokio.Animation.User
{
    using devDept.Eyeshot;
    using devDept.Eyeshot.Entities;
    using devDept.Geometry;
    using Logger;
    using Pinokio.Database;
    using Pinokio.Model.Base;
    using Pinokio.Model.User;
    using global::Simulation.Engine;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    
    
    public class RefRobotArm : NodeReference
    {
        
        // Is this object Inserted ?
        public static bool IsInserted = true;

        [Browsable(false), StorableAttribute(false)]
        public new static string[] UsableBaseSimNodeType =
    {
            typeof(Equipment).Name,
            typeof(Stay).Name,
            typeof(Diverter).Name
        };

        [Browsable(false), StorableAttribute(false)]
        private static List<string> _usableSimNodeTypes;

        [Browsable(false), StorableAttribute(false)]
        public new static List<string> UsableSimNodeTypes
        {
            get
            {
                if (_usableSimNodeTypes == null)
                    _usableSimNodeTypes = BaseUtill.GetUsableSimNodeTypes(UsableBaseSimNodeType, InterfaceConstraints);

                return _usableSimNodeTypes;
            }
        }

        public RefRobotArm(string blockName) : 
                base(blockName)
        {
            // Write your code here.
        }
        
        public static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            try
            {
                // Write your code here.
                Block block = NodeReference.LoadModelFile("C:\\Carlo\\Pinokio\\Pinokio.Asset\\3D\\RobotArm2.obj", "RefRobotArm", model);
                model.Blocks.Add(block);
            }
            catch (System.Exception ex)
            {
                // Handle any other exception type here.
                ErrorLogger.SaveLog(ex);
            }
        }
    }
}
