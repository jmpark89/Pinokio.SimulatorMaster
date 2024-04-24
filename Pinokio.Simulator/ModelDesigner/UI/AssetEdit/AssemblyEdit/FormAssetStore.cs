using DevExpress.Utils;
using DevExpress.XtraBars;
//TODO devDept: Now you need to pass through the Model.ActiveViewport property to get/set the active viewport grid.
using DevExpress.XtraGrid.Views.Grid;
//TODO devDept: Now you need to pass through the Model.ActiveViewport property to get/set the active viewport grid.
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Logger;
using Pinokio.Communication;
using Pinokio.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinokio.Designer.UI.AssetEdit.AssemblyEdit
{
    public partial class FormAssetStore : DevExpress.XtraBars.ToolbarForm.ToolbarForm
    {
        public FormAssetStore()
        {
            InitializeComponent();
        }
        private void simpleButtonRefresh_Click(object sender, EventArgs e)
        {
            GetOnlineAsset();
        }
        private void GetOnlineAsset()
        {
            try
            {
                string serverSimClassFolderPath = PINOKIO_SFTP_PATH.MAIN + "/" + PINOKIO_3D_PATH.FOLDER_NAME + "/" + PINOKIO_3D_PATH.CLASS_NAME;
                string serverReferenceClassFolderPath = PINOKIO_SFTP_PATH.MAIN + "/" + PINOKIO_3D_PATH.FOLDER_NAME + "/" + PINOKIO_3D_PATH.REFERENCE_CLASS_NAME;
                string server3DObjClassFolderPath = PINOKIO_SFTP_PATH.MAIN + "/" + PINOKIO_3D_PATH.FOLDER_NAME + "/" + PINOKIO_3D_PATH.MODEL3D_NAME;
                string server3DOThumnailFolderPath = PINOKIO_SFTP_PATH.MAIN + "/" + PINOKIO_3D_PATH.FOLDER_NAME + "/" + PINOKIO_3D_PATH.THUMBNAIL_NAME;

                List<string> objfiles =  SFTP.GetFileList(server3DObjClassFolderPath, ".obj");
                List<string> reffiles = SFTP.GetFileList(serverReferenceClassFolderPath, ".cs");
                List<string> simfiles = SFTP.GetFileList(serverSimClassFolderPath, ".cs");
                List<string> thumnatilfiles = SFTP.GetFileList(server3DOThumnailFolderPath, ".bmp");
                Dictionary<string, int> thumnailNames = new Dictionary<string, int>();

                for(int i = 0; i < thumnatilfiles.Count; i ++)
                {
                    string name = Path.GetFileNameWithoutExtension(thumnatilfiles[i]);
                    if (!thumnailNames.ContainsKey(name))
                    {
                        thumnailNames.Add(name, i);
                    }
                }
                List<AssetItem> assetItems = new List<AssetItem>(); 
                for(int i = 0; i < objfiles.Count;  i++ )
                {

                    AssetItem assetItem = new AssetItem() { Name = objfiles[i], Description = string.Empty, 
                        Path = server3DObjClassFolderPath + "/" + objfiles[i],
                        Download = imageCollection1.Images[0]
                    };

                    string key = Path.GetFileNameWithoutExtension(objfiles[i]);
                    if (thumnailNames.ContainsKey(key))
                    {
                        assetItem.Thumnail = SFTP.DownloadBitmap(server3DOThumnailFolderPath + "/" + thumnatilfiles[thumnailNames[key]]);
                    }

                     assetItems.Add(assetItem);
                }
                List<List<string>> values =  Database.MySQLDB.GetRefClass3DObjFile();
                List<AssetItem> assetItems2 = new List<AssetItem>();
                for (int i = 0;i < values.Count; i++)
                {
                    List<string> value = values[i];
                    AssetItem assetItem = new AssetItem()
                    {
                        Name =  value[0],
                        ObjName =Path.ChangeExtension( value[1], ".obj"),
                        Description = value[2],
                        Path = serverReferenceClassFolderPath + "/"+ value[0],
                        Download = imageCollection1.Images[0],
                        UserID = value[3]
                    };


                    string key = Path.GetFileNameWithoutExtension(value[0]);
                    if (thumnailNames.ContainsKey(key))
                    {
                        assetItem.Thumnail = SFTP.DownloadBitmap(server3DOThumnailFolderPath + "/" + thumnatilfiles[thumnailNames[key]]);
                    }

                    assetItems2.Add(assetItem);

                }
            

                List<AssetItem> assetItems3 = new List<AssetItem>();
                for (int i = 0; i < simfiles.Count; i++)
                {
                    AssetItem assetItem = new AssetItem() { Name = simfiles[i], Description = string.Empty,
                        Path = serverSimClassFolderPath + "/" +  simfiles[i] , 
                        Download= imageCollection1.Images[0]};
                    assetItems3.Add(assetItem);
                }

                this.gridControlOnline3DObj.DataSource = assetItems;
                this.gridControlOnlineRefClass.DataSource = assetItems2;
                this.gridControlOnlineSimClass.DataSource = assetItems3;


                this.gridControlOnline3DObj.RefreshDataSource();
                this.gridControlOnlineRefClass.RefreshDataSource();
                this.gridControlOnlineSimClass.RefreshDataSource();



                string downLoadPath = PINOKIO_INSTALL_PATH.DEFAULT_C_DRIVE + PINOKIO_SETTINGTOOL.ASSET;

                string modelPath3D = Path.Combine(downLoadPath, PINOKIO_3D_PATH.MODEL3D_NAME);
                string classPath3D = Path.Combine(downLoadPath, PINOKIO_3D_PATH.CLASS_NAME);
                string yhumbnailPath3D = Path.Combine(downLoadPath, PINOKIO_3D_PATH.THUMBNAIL_NAME);




                GetModelPaths(modelPath3D);
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void GetModelPaths(string folderPath)
        {
            try

            {
                DirectoryInfo di = new DirectoryInfo(folderPath);
                if (!di.Exists)
                    di.Create();

                foreach(FileInfo fi in di.GetFiles())
                {
                    if (PINOKIO_3D_PATH.MODEL_EXTENSION.ContainsKey(fi.Extension.ToLower()))
                    {

                    }
                }

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void FormAssetStore_Load(object sender, EventArgs e)
        {
            try
            {
                GetOnlineAsset();
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

        private void Download3DObj(string zillarFileName)
        {
            try
            {
                string serverObjPath = PINOKIO_SFTP_PATH.MAIN + "/" + PINOKIO_3D_PATH.FOLDER_NAME + "/" + PINOKIO_3D_PATH.MODEL3D_NAME + "/" + zillarFileName;

                string downloadPath = Path.Combine(Path.Combine(AssetUtill.GetAssetPath(), PINOKIO_3D_PATH.MODEL3D_NAME), zillarFileName);
                SFTP.DownloadFile(serverObjPath, downloadPath);


                string downloadMaterialPath = Path.Combine(Path.Combine(AssetUtill.GetAssetPath(), PINOKIO_3D_PATH.MODEL3D_NAME), Path.ChangeExtension(zillarFileName, ".mtl"));

                string assetMaterialPath = Path.ChangeExtension(downloadPath, ".mtl");

                SFTP.DownloadFile(assetMaterialPath, downloadMaterialPath);


                string assetMaterialFolderPath = assetMaterialPath.Replace(".mtl", "");

                string downloadMaterialFolderPath = Path.Combine(Path.GetDirectoryName(downloadMaterialPath), Path.GetFileNameWithoutExtension(zillarFileName));

                SFTP.DownloadDirectory(assetMaterialFolderPath, downloadMaterialFolderPath);
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        private void Download3DObj(AssetItem assetItem)
        {
            try
            {
                string downloadPath = Path.Combine(Path.Combine(AssetUtill.GetAssetPath(), PINOKIO_3D_PATH.MODEL3D_NAME), assetItem.Name);

                SFTP.DownloadFile(assetItem.Path, downloadPath);


                string downloadMaterialPath = Path.Combine(Path.Combine(AssetUtill.GetAssetPath(), PINOKIO_3D_PATH.MODEL3D_NAME), Path.ChangeExtension(assetItem.Name, ".mtl"));

                string assetMaterialPath = Path.ChangeExtension(assetItem.Path, ".mtl");

                SFTP.DownloadFile(assetMaterialPath, downloadMaterialPath);

                string assetMaterialFolderPath = assetMaterialPath.Replace(".mtl", "");

                string downloadMaterialFolderPath =Path.Combine( Path.GetDirectoryName(downloadMaterialPath) , Path.GetFileNameWithoutExtension(assetItem.Name));

                SFTP.DownloadDirectory(assetMaterialFolderPath, downloadMaterialFolderPath);

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        private void DoubleClickEvent(object sender, EventArgs e, int gridviewType)
        {
            try
            {




                DXMouseEventArgs ea = e as DXMouseEventArgs;
                GridView view = sender as GridView;
                GridHitInfo info = view.CalcHitInfo(ea.Location);
                if (info.InRow || info.InRowCell)
                {
                    string colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
                    AssetItem assetItem = (AssetItem)view.GetRow(info.RowHandle);
                    if (colCaption == "Download")
                    {
                        if (gridviewType == 0)
                        {
                            //Online3DObj
                            Download3DObj(assetItem);
                        }
                        else if (gridviewType == 1)
                        {
                            //OnlineRefClass
                            Download3DObj(assetItem.ObjName);

                            string downloadPath = Path.Combine(Path.Combine(PINOKIO_INSTALL_PATH.DEFAULT_C_DRIVE, PINOKIO_PRODUCT.REF_USER), assetItem.Name );
                            string folderPath = AssetUtill.SearchFolderPath("Pinokio.Animation.User", Application.StartupPath);
                            SFTP.DownloadFile(assetItem.Path, downloadPath);
                            File.Copy(downloadPath , Path.Combine(folderPath, assetItem.Name), true);
                            DevTool.PinokioCodeDomRef.AddCSharpFile2ProjectFile(folderPath, assetItem.Name);

                        }
                        else if (gridviewType == 2)
                        {
                            //OnlineSimClass
                            string downloadPath = Path.Combine(Path.Combine(PINOKIO_INSTALL_PATH.DEFAULT_C_DRIVE, PINOKIO_PRODUCT.SIM_USER), assetItem.Name);

                            string folderPath = AssetUtill.SearchFolderPath("Pinokio.Model.User", Application.StartupPath);

                            SFTP.DownloadFile(assetItem.Path, downloadPath);
                            File.Copy(downloadPath, Path.Combine(folderPath, assetItem.Name), true);
                            DevTool.PinokioCodeDomNode.AddCSharpFile2ProjectFile(folderPath, assetItem.Name);
                        }

                        assetItem.Download = imageCollection1.Images[1];
                        view.RefreshData();
                    }

                }
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }


        private void gridViewOnline3DObj_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DoubleClickEvent(sender, e, 0);
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void gridViewOnlineRefClass_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DoubleClickEvent(sender, e, 1);

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void gridViewOnlineSimClass_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DoubleClickEvent(sender, e, 2);

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }


    }
}