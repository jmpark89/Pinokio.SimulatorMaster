using Logger;
using Pinokio.Object;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinokio.Designer
{
    public class AssetUtill
    {
        public static string Get3DFolderPath()
        {
            string folder3D = string.Empty;
            try
            {
                string folderPath = Application.StartupPath;
                folder3D = Path.Combine(Application.StartupPath, "3D");
                DirectoryInfo di = new DirectoryInfo(folder3D);
                if (!di.Exists)
                    di.Create();
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return folder3D;

        }
        public static string SearchFolderPath(string folderName, string currentPath)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(currentPath);

                foreach (DirectoryInfo subD in di.GetDirectories())
                {
                    if (subD.Name == folderName)
                    {
                        return subD.FullName;
                    }
                }

                return SearchFolderPath(folderName, Path.GetDirectoryName(currentPath));
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

            return string.Empty;

        }


        public static string GetAssetPath()
        {
            string folder3D = string.Empty;
            try
            {
                string assetFolderPath = PINOKIO_INSTALL_PATH.DEFAULT_C_DRIVE + PINOKIO_SETTINGTOOL.ASSET;


                DirectoryInfo di = new DirectoryInfo(assetFolderPath);
                if (!di.Exists)
                    di.Create();

                return assetFolderPath;
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return folder3D;

        }
    }
}
