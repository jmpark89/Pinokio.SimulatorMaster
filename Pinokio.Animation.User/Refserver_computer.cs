//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

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
    
    
    public class Refserver_computer : NodeReference
    {
        
        // Is this object Inserted ?
        public static bool IsInserted = true;
        
        public Refserver_computer(string blockName) : 
                base(blockName)
        {
            // Write your code here.
        }
        
        public static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            try
            {
                // Write your code here.
                Block block = NodeReference.LoadModelFile("C:\\Carlo\\Pinokio\\Pinokio.Asset\\3D\\server_computer.obj", "Refserver_computer", model);
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
