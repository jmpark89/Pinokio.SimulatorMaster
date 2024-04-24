using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using devDept.Geometry;
using devDept.Graphics;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Logger;
using Pinokio.Communication;
using Pinokio.Database;
using Pinokio.Designer.UI.AssetEdit;
using Pinokio.Designer.UI.AssetEdit.AssemblyEdit;
using Pinokio.DevTool;
using Pinokio.DevTool.GenCode;
using Pinokio.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using static devDept.Eyeshot.Environment;

namespace Pinokio.Designer
{


    public partial class FormAssemblyEdit : DevExpress.XtraEditors.XtraForm
    {
        private bool Editing = false;
        private Dictionary<Entity,ReferenceSelectClass> referenceTypes = new Dictionary<Entity, ReferenceSelectClass>();
        public FormAssemblyEdit()
        {
            InitializeComponent();
            assemblyModel1.Unlock("US2-RMKUX-N12YW-WAMY-S738");
            InitializeTreeView();
        }

        private void InitializeTreeView()
        {
            try
            {

                assemblyTreeView1.Model = assemblyModel1;
                assemblyTreeView1.InitializeContextMenu();
                ((MyEntityList)assemblyModel1.Entities).assemblyTree = assemblyTreeView1;

                // Listens the events to handle the deletion of the selected entity
                assemblyModel1.KeyDown += Model1_KeyDown;
                assemblyTreeView1.KeyDown += assemblyTreeView1_KeyDown;
                assemblyTreeView1.AfterSelect += assemblyTreeView1_AfterSelect;

                // Listens the events to synchronize selection from screen to treeView
                assemblyModel1.SelectionChanged += Model1_SelectionChanged;

                // helper for Turbo button color
                assemblyModel1.CameraMoveBegin += model1_CameraMoveBegin;


                assemblyModel1.ObjectManipulator.ShowOriginalWhileEditing = false;

                // settings to improve performance for heavy geometry
                assemblyModel1.Rendered.SilhouettesDrawingMode = silhouettesDrawingType.Never;
                assemblyModel1.Rendered.ShadowMode = shadowType.None;
                assemblyModel1.Turbo.OperatingMode = operatingType.Boxes;
                assemblyModel1.WriteDepthForTransparents = true;
                assemblyModel1.Rotate.ShowCenter = true;
                assemblyModel1.Grid.AutoSize = true;
                assemblyModel1.Grid.AutoStep  = true;
                assemblyModel1.Backface.ColorMethod = backfaceColorMethodType.Cull;
                assemblyModel1.Units = linearUnitsType.Millimeters;
                assemblyModel1.ActionMode = actionType.SelectVisibleByPick;
                assemblyModel1.ShortcutKeys.CopySelection = Keys.C | Keys.Control;
                assemblyModel1.ShortcutKeys.PasteSelection = Keys.V | Keys.Control;
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

        private void assemblyTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (assemblyTreeView1.SelectedItems.Count > 0)
                {
                    this.propertyGridControl1.SelectedObject = assemblyTreeView1.SelectedItems[0].Item;
                    this.propertyGridControl1.Refresh();

                    if (assemblyTreeView1.SelectedItems[0].Item is Entity)
                    {
                        Entity entity = (Entity)assemblyTreeView1.SelectedItems[0].Item;

                        if (!referenceTypes.ContainsKey(entity))
                        {
                            referenceTypes.Add(entity, new ReferenceSelectClass(entity));
                        }
                        this.propertyGridControlReferenceType.SelectedObject = referenceTypes[entity];
                        this.propertyGridControlReferenceType.Refresh();
                    }

                }
            }

            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }
       private void ClearModel()
        {
            try
            {
                assemblyTreeView1.ClearTree();
                if (assemblyModel1.Entities.IsOpenCurrentBlockReference)
                    assemblyModel1.Entities.CloseCurrentBlockReference();
                assemblyModel1.Clear();

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private ReadFileAsync GetReader(string fileName)
        {
            try
            {
                string ext = System.IO.Path.GetExtension(fileName);

                if (ext != null)
                {
                    ext = ext.TrimStart('.').ToLower();

                    switch (ext)
                    {
                        case "igs":
                        case "iges":
                            return new ReadIGES(fileName);
                        case "stp":
                        case "step":
                            return new ReadSTEP(fileName);
                        case "obj":
                            return new ReadOBJ(fileName);
                        case "stl":
                            return new ReadSTL(fileName);
                        case "3ds":
                            return new Read3DS(fileName);
                    }
                }
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

            return null;
        }


        private void assemblyModel1_WorkCompleted(object sender, WorkCompletedEventArgs e)
        {
            try
            {
                if (e.WorkUnit is ReadFileAsync)
                {
                    assemblyTreeView1.ClearCurrent(true);

                    ReadFileAsync ra = (ReadFileAsync)e.WorkUnit;

                    if (this.barCheckItemGeometryInAxis.Checked)
                        ra.RotateEverythingAroundX();

    
                    ra.AddToScene(assemblyModel1, new RegenOptions() { Async = true });
                    
                    assemblyModel1.Invalidate();
                }
                else if (e.WorkUnit is Regeneration)
                {
                    assemblyTreeView1.PopulateTree(assemblyModel1.Entities);

                    assemblyModel1.Entities.UpdateBoundingBox();
                    assemblyModel1.ZoomFit();
                    assemblyModel1.Invalidate();
                }
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void model1_CameraMoveBegin(object sender, devDept.Eyeshot.Environment.CameraMoveEventArgs e)
        {

        }

        private void Model1_SelectionChanged(object sender, devDept.Eyeshot.Environment.SelectionChangedEventArgs e)
        {
            // we avoid entities selection when ObjectManipulator is enabled 
            if (assemblyTreeView1.IsMoving || assemblyTreeView1.TreeModify)
                return;

            if (e.AddedItems.Count > 0 || e.RemovedItems.Count > 0)
            {
                // updates the selection data
                if (assemblyTreeView1.SelectedItems == null)
                {
                    assemblyTreeView1.SelectedItems = new List<devDept.Eyeshot.Environment.SelectedItem>();
                }
                else
                {
                    foreach (devDept.Eyeshot.Environment.SelectedItem item in e.RemovedItems)
                    {
                        assemblyTreeView1.SelectedItems.RemoveAll( x => x.Item== item);
                    }
                }



                assemblyTreeView1.SelectedItems.AddRange(e.AddedItems);

                assemblyTreeView1.SynchScreenSelection(new Stack<BlockReference>(this.assemblyModel1.Entities.Parents));
            }
        }



        private void assemblyTreeView1_KeyDown(object sender, KeyEventArgs e)
        {
            Model1_KeyDown(sender, e);

        }

        private void Model1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                for (var i = assemblyTreeView1.SelectedNodes.Count - 1; i >= 0; i--)
                {
                    TreeNode selectedNode = assemblyTreeView1.SelectedNodes[i];
                    if (selectedNode != null)
                    {
                        if (assemblyTreeView1.SelectedItems[i] != null && assemblyTreeView1.SelectedItems[i].Item != null)
                        {
                            var entity = assemblyTreeView1.SelectedItems[i].Item as Entity;

                            if (selectedNode.Parent != null && selectedNode.Parent.Tag != null)
                            {
                                var parent =
                                    ((AssemblyTreeView.NodeTag)selectedNode.Parent.Tag).Entity as BlockReference;

                                var parentBlockName = parent.BlockName;

                                // removes the entity from the block where it's present
                                assemblyModel1.Blocks[parentBlockName].Entities.Remove(entity);
                            }
                            else
                            {
                                // in case the entity to delete is a root level entity
                                assemblyModel1.Entities.DeleteSelected();
                            }
                        }

                    }

                }

                assemblyTreeView1.DeleteSelectedNodes();

                // update selection data
                assemblyTreeView1.SelectedNodes.Clear();
                assemblyTreeView1.SelectedItems.Clear();

                assemblyModel1.Entities.UpdateBoundingBox();
                assemblyModel1.Invalidate();
            }

        }
        private void InsertEntities(string[] paths)
        {
            try
            {
                foreach (string path in paths)
                {
                    ReadFileAsync readFileAsync = GetReader(path);
                    if (readFileAsync != null)
                    {
                        readFileAsync.DoWork();
                        readFileAsync.AddToScene(assemblyModel1);
                        foreach (Entity en in readFileAsync.Entities)
                            assemblyModel1.Entities.Remove(en);
                        if (this.barCheckItemGeometryInAxis.Checked)
                            readFileAsync.RotateEverythingAroundX();


                        Block bl = new Block(Path.GetFileNameWithoutExtension(path));
                        bl.Entities.AddRange(readFileAsync.Entities);
                        assemblyModel1.Blocks.Add(bl);
                        BlockReference blockReference = new BlockReference(Path.GetFileNameWithoutExtension(path));

                        assemblyModel1.Entities.Add(blockReference);

                        assemblyModel1.SetView(viewType.Trimetric, true, assemblyModel1.AnimateCamera);
                    }
                }

                assemblyTreeView1.PopulateTree(assemblyModel1.Entities);
                assemblyModel1.Entities.UpdateBoundingBox();
                assemblyModel1.ZoomFit();
                assemblyModel1.Invalidate();
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        private void barButtonItemAddModel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Multiselect = true;
                //if (this.xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)

                dlg.Filter = "3D Format (*.igs; *.iges;*.obj;*.stp; *.step;*.stl; *.3ds)|*.igs; *.iges;*.obj;*." +
    "stp; *.step;*.stl; *.3ds";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //InsertEntities(this.xtraOpenFileDialog1.FileNames);
                    InsertEntities(dlg.FileNames);
                }
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }


        }
        private void barButtonItemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {


                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Multiselect = true;
                dlg.Filter = "3D Format (*.igs; *.iges;*.obj;*.stp; *.step;*.stl; *.3ds)|*.igs; *.iges;*.obj;*." +
"stp; *.step;*.stl; *.3ds";
                //if (this.xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ClearModel();


                    //InsertEntities(this.xtraOpenFileDialog1.FileNames);
                    InsertEntities(dlg.FileNames);
                }
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void barButtonItemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                //if (this.xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
                dlg.Filter = "Obj (*.obj)|*.obj";
                if (dlg.ShowDialog() == DialogResult.OK)
                {

   
                    //Export3DModel(xtraSaveFileDialog1.FileName);
                    Export3DModel(dlg.FileName);
                }

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void Export3DModel(string filePath)
        {
            try
            {
                this.assemblyModel1.ObjectManipulator.Apply();

                WriteFileAsync wfa = null;
                WriteParamsWithMaterials dataParams = null;
                switch (this.xtraSaveFileDialog1.FilterIndex)
                {

                    case 1:
                        dataParams = new WriteParamsWithMaterials(this.assemblyModel1);
                        wfa = new WriteOBJ(dataParams, filePath);
                        break;
                }

                this.assemblyModel1.StartWork(wfa);

                this.assemblyModel1.SetView(viewType.Trimetric, true, this.assemblyModel1.AnimateCamera);
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        private Entity GetSelectedEntity(out int countSelected)
        {
            countSelected = 0;
            Entity selectedEnt = null;

            foreach (Entity ent in assemblyModel1.Entities)
            {
                if (ent.Selected)
                {
                    countSelected++;
                    selectedEnt = ent;
                }
            }
            return selectedEnt;
        }

        private void barButtonItemObjectManipulator_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (Editing)
                {
                    // Cancels the ObjectManipulator editing
                    this.assemblyModel1.ObjectManipulator.Cancel();
                    Editing = false;
                }
                else
                {
                    // Starts the edit the selected parts with the ObjectManipulator
                    int countSelected;
                    Entity selectedEnt = GetSelectedEntity(out countSelected);

                    if (countSelected > 0)
                    {
                        Editing = true;

                        Transformation initialTransformation = null;
                        bool center = true;


                        // If there is only one selected entity, position and orient the manipulator using the rotation point saved in its
                        // EntityData property and its transformation

                        Point3D rotationPoint = null;

                        if (selectedEnt.EntityData is Point3D)
                        {
                            center = false;
                            rotationPoint = (Point3D)selectedEnt.EntityData;
                        }

                        if (rotationPoint != null)

                            initialTransformation = new Translation(rotationPoint.X, rotationPoint.Y,
                                rotationPoint.Z);
                        else

                            initialTransformation = new Identity();

                        // Enables the ObjectManipulator to start editing the selected objects
                        this.assemblyModel1.ObjectManipulator.Enable(initialTransformation, center);
                    }
                }
                this.assemblyModel1.Invalidate();
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void barButtonItemSetSelectionAsCurrent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (Editing)
                {
                    // Applies the transformation from the ObjectManipulator
                    this.assemblyModel1.ObjectManipulator.Apply();
                    this.assemblyModel1.Entities.Regen();
                    Editing = false;
                }
                else
                    this.assemblyModel1.Entities.SetSelectionAsCurrent();
                    
                this.assemblyModel1.Invalidate();
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

 
        private void barEditItemManipulatorType_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string type = barEditItemManipulatorType.EditValue.ToString();
                if (type == string.Empty)
                    return;

                devDept.Eyeshot.ObjectManipulator.styleType t = (devDept.Eyeshot.ObjectManipulator.styleType)Enum.Parse(typeof(devDept.Eyeshot.ObjectManipulator.styleType), type);
                this.assemblyModel1.ObjectManipulator.StyleMode = t;


            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void repositoryItemCheckedComboBoxEditManipulatorActionType_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.assemblyModel1.ObjectManipulator.TranslateX.Visible =  (bool)DevExpress.XtraEditors.Controls.CheckedListBoxItem.GetCheckValue(repositoryItemCheckedComboBoxEditManipulatorActionType.Items[0].CheckState);
                this.assemblyModel1.ObjectManipulator.TranslateY.Visible = (bool)DevExpress.XtraEditors.Controls.CheckedListBoxItem.GetCheckValue(repositoryItemCheckedComboBoxEditManipulatorActionType.Items[0].CheckState);
                this.assemblyModel1.ObjectManipulator.TranslateZ.Visible = (bool)DevExpress.XtraEditors.Controls.CheckedListBoxItem.GetCheckValue(repositoryItemCheckedComboBoxEditManipulatorActionType.Items[0].CheckState);

                this.assemblyModel1.ObjectManipulator.RotateX.Visible = (bool)DevExpress.XtraEditors.Controls.CheckedListBoxItem.GetCheckValue(repositoryItemCheckedComboBoxEditManipulatorActionType.Items[1].CheckState);
                this.assemblyModel1.ObjectManipulator.RotateY.Visible = (bool)DevExpress.XtraEditors.Controls.CheckedListBoxItem.GetCheckValue(repositoryItemCheckedComboBoxEditManipulatorActionType.Items[1].CheckState);
                this.assemblyModel1.ObjectManipulator.RotateZ.Visible = (bool)DevExpress.XtraEditors.Controls.CheckedListBoxItem.GetCheckValue(repositoryItemCheckedComboBoxEditManipulatorActionType.Items[1].CheckState);

                this.assemblyModel1.ObjectManipulator.ScaleX.Visible = (bool)DevExpress.XtraEditors.Controls.CheckedListBoxItem.GetCheckValue(repositoryItemCheckedComboBoxEditManipulatorActionType.Items[2].CheckState);
                this.assemblyModel1.ObjectManipulator.ScaleY.Visible = (bool)DevExpress.XtraEditors.Controls.CheckedListBoxItem.GetCheckValue(repositoryItemCheckedComboBoxEditManipulatorActionType.Items[2].CheckState);
                this.assemblyModel1.ObjectManipulator.ScaleZ.Visible = (bool)DevExpress.XtraEditors.Controls.CheckedListBoxItem.GetCheckValue(repositoryItemCheckedComboBoxEditManipulatorActionType.Items[2].CheckState);

                this.assemblyModel1.CompileUserInterfaceElements();
                this.assemblyModel1.Invalidate();
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void repositoryItemComboBoxManipulatorAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string type = barEditItemManipulatorAction.EditValue.ToString();
                if (type == string.Empty)
                    return;

                devDept.Eyeshot.ObjectManipulator.ballActionType t = (devDept.Eyeshot.ObjectManipulator.ballActionType)Enum.Parse(typeof(devDept.Eyeshot.ObjectManipulator.ballActionType), type);
                this.assemblyModel1.ObjectManipulator.BallActionMode = t;


                this.assemblyModel1.Invalidate();
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void barButtonItemCreateNode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FormInsertName formInsertName = new FormInsertName();
                if (formInsertName.ShowDialog() == DialogResult.OK)
                {

                    string refName = formInsertName.NodeName;
                    Create3DObjReferenceClass(refName);

                }

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void Create3DObjReferenceClass(string refName)
        {try
            {
                string folder3D = CreateBlockByCurrent3D(refName);

                string folderPath = AssetUtill.SearchFolderPath("Pinokio.Animation.User", Application.StartupPath);

                PinokioCodeDomRefBy3DModel pinokioCodeDomRefBy3DModel = new PinokioCodeDomRefBy3DModel(refName, Path.GetDirectoryName(folderPath), folder3D);
                pinokioCodeDomRefBy3DModel.GenerateCode();

          
                PinokioCodeDomNode pinokioCodeDomNode = new PinokioCodeDomNode(refName, Path.GetDirectoryName(folderPath));
                pinokioCodeDomNode.GenerateCode();

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        
        public string CreateBlockByCurrent3D(string refName)
        {
            string folder3D = AssetUtill.GetAssetPath();
            folder3D = Path.Combine(folder3D, PINOKIO_3D_PATH.MODEL3D_NAME);
            try
            {
                
                string fileName = refName + ".obj";
                Export3DModel(Path.Combine(folder3D, fileName));

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return folder3D;
        }


        private void barButtonItemDownLoadAsset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FormAssetStore formAssetStore = new FormAssetStore();

                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this, typeof(Pinokio.UI.Base.WaitFormSplash), true, true);
                formAssetStore.ShowDialog();
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }


        private string GetImortedName()
        {
            string returnValue = string.Empty;
            try
            {
                if (this.assemblyModel1.Entities.Count > 0)
                {
                    for(int i = 0; i < this.assemblyModel1.Entities.Count; i ++)
                    {
                        if (this.assemblyModel1.Entities[i] is BlockReference)
                        {
                            string blockName = ((BlockReference)this.assemblyModel1.Entities[i]).BlockName;
                            if (blockName.Substring(0, 3) == "Ref")
                                return blockName.Substring(3, blockName.Length - 3);
                            return ((BlockReference)this.assemblyModel1.Entities[i]).BlockName;

                        }
                    }
                   
                }
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return returnValue;
        }

        private void barButtonItemImportReferenceFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FormImportReference formImportReference = new FormImportReference();
                if (formImportReference.ShowDialog() == DialogResult.OK)
                {
                    ClearModel();
                    string referenceName = formImportReference.ImportedReferenceName;
                    Type referenceType = formImportReference.ImportedType;


                    if (!this.assemblyModel1.Blocks.Contains(referenceName))
                    {
                        MethodInfo m = (referenceType.GetMethods().ToList().Find(x => x.Name == "CreateBlock"));
                        if (m != null)
                            m.Invoke(null, new object[] { this.assemblyModel1, new object[] { } });
                    }
                    BlockReference bl = new BlockReference(referenceName);
                    this.assemblyModel1.Entities.Add(bl);

                    assemblyModel1.SetView(viewType.Trimetric, true, assemblyModel1.AnimateCamera);
                    assemblyTreeView1.PopulateTree(assemblyModel1.Entities);
                    assemblyModel1.Entities.UpdateBoundingBox();
                    assemblyModel1.ZoomFit();
                    assemblyModel1.Invalidate();


                }
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void barButtonItemAddReferenceItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FormImportReference formImportReference = new FormImportReference();
                formImportReference.IsImport3D = true;
                if (formImportReference.ShowDialog() == DialogResult.OK)
                {
                    string referenceName = formImportReference.ImportedReferenceName;
                    Type referenceType = formImportReference.ImportedType;


                    if (!this.assemblyModel1.Blocks.Contains(referenceName))
                    {
                        MethodInfo m = (referenceType.GetMethods().ToList().Find(x => x.Name == "CreateBlock"));
                        if (m != null)
                            m.Invoke(null, new object[] { this.assemblyModel1, new object[] { } });
                    }
                    BlockReference bl = new BlockReference(referenceName);
                    this.assemblyModel1.Entities.Add(bl);

                    assemblyModel1.SetView(viewType.Trimetric, true, assemblyModel1.AnimateCamera);
                    assemblyTreeView1.PopulateTree(assemblyModel1.Entities);
                    assemblyModel1.Entities.UpdateBoundingBox();
                    assemblyModel1.ZoomFit();
                    assemblyModel1.Invalidate();


                }
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

        private void barButtonItemUploadSim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                try
                {
                    FormImportReference formImportReference = new FormImportReference();
                    formImportReference.IsImport3D = false;
                    if (formImportReference.ShowDialog() == DialogResult.OK)
                    {


                        string simName = formImportReference.ImportedReferenceName;
                        Type simType = formImportReference.ImportedType;
                        string serverPath = PINOKIO_SFTP_PATH.MAIN + "/" + PINOKIO_3D_PATH.FOLDER_NAME + "/" + PINOKIO_3D_PATH.CLASS_NAME;
                        string folderPath = AssetUtill.SearchFolderPath(simType.Namespace, Application.StartupPath);

    
                        bool isExist =   SFTP.IsExistFile(serverPath + "/" + simName + ".cs");
                        if (isExist)
                        {
                           if (  MessageBox.Show(simName + " File이 존재합니다. 덮어씌우겠습니까?", "Simulation Node Upload", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                SFTP.UploadFileByString(serverPath, Path.Combine(folderPath, simType.Name + ".cs"), simName + ".cs");
                                MessageBox.Show("Simulation Class Upload Success");
                            }
                        }else
                        {
                            SFTP.UploadFileByString(serverPath, Path.Combine(folderPath, simType.Name + ".cs") , simName + ".cs");
                            MessageBox.Show("Simulation Class Upload Success");
                        }

                
                    }
                }
                catch (System.Exception ex)
                {
                    ErrorLogger.SaveLog(ex);
                }
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void barButtonItemUpload3DItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FormUploadAssetItem formUploadAssetItem = new FormUploadAssetItem();
                formUploadAssetItem.textEditFileName.Text = GetImortedName();
                if (formUploadAssetItem.ShowDialog() == DialogResult.OK)
                {
                    string name =  "Ref"+ formUploadAssetItem.textEditFileName.Text + ".cs";
                    string description = formUploadAssetItem.memoEditDescription.Text;

                    List<string> datas = MySQLDB.GetRefClass3DObjFile(name);
                    if (datas.Count != 0)
                    {
                        if (MessageBox.Show("현재 File이 존재합니다. 덮어씌우시겠습니까? ", "Exist file..", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {

                            MySQLDB.UpdateRefClass3DObjFile(name, formUploadAssetItem.textEditFileName.Text + ".obj", description, Pinokio.Auth.PinokioLincense.CurrentUSERID);
                            Upload3DFiles(formUploadAssetItem);
                        }
                    }
                    else
                    {
                        MySQLDB.InsertRefClass3DObjFile(name, formUploadAssetItem.textEditFileName.Text + ".obj", description, Pinokio.Auth.PinokioLincense.CurrentUSERID);
                        Upload3DFiles(formUploadAssetItem);
                    }
                }
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        private void SaveThumnailImage(string refName)
        { try
            {
                //Bitmap image = this.assemblyModel1.GetThumbnail(64);
                //string serverPath = PINOKIO_SFTP_PATH.MAIN + "/" + PINOKIO_3D_PATH.FOLDER_NAME + "/" + PINOKIO_3D_PATH.THUMBNAIL_NAME;
                //SFTP.sFtpUpLoadBitmap(serverPath, image, refName + ".bmp");


                Bitmap image = this.assemblyModel1.CreateReferenceImage();
                string serverPath = PINOKIO_SFTP_PATH.MAIN + "/" + PINOKIO_3D_PATH.FOLDER_NAME + "/" + PINOKIO_3D_PATH.THUMBNAIL_NAME;
                SFTP.sFtpUpLoadBitmap(serverPath, image, refName + ".bmp");
            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        private void Upload3DFiles(FormUploadAssetItem formUploadAssetItem)
        {
            try
            {
                foreach (CheckedListBoxItem checkedListBoxItem in formUploadAssetItem.comboBoxEditUploadType.Properties.Items)
                {
                    if (checkedListBoxItem.CheckState == CheckState.Checked)
                    {
                        if (checkedListBoxItem.Value.ToString() == "3D Class")
                        {
                            string simulationClassName= formUploadAssetItem.textEditFileName.Text;
                            string folder3DPath = CreateBlockByCurrent3D(formUploadAssetItem.textEditFileName.Text);

                            SaveThumnailImage("Ref" + simulationClassName);


                            string folderPath = AssetUtill.SearchFolderPath("Pinokio.Animation.User", Application.StartupPath);
                            bool isExistFile = false;
                            DirectoryInfo di = new DirectoryInfo(folderPath);
                            foreach (FileInfo fi in di.GetFiles())
                            {
                                if (fi.Name == "Ref" + simulationClassName + ".cs")
                                {
                                    isExistFile = true;
                                    break;
                                }
                            }
                            if (!isExistFile)
                            {
                                PinokioCodeDomRefBy3DModel pinokioCodeDomRefBy3DModel = new PinokioCodeDomRefBy3DModel(simulationClassName, Path.GetDirectoryName(folderPath), folder3DPath);
                                pinokioCodeDomRefBy3DModel.GenerateCodeNotProjectFile();
                                pinokioCodeDomRefBy3DModel.GenerateCSharpCodeNotAddProject();

                                PinokioCodeDomNode pinokioCodeDomNode = new PinokioCodeDomNode(simulationClassName, Path.GetDirectoryName(folderPath));
                                pinokioCodeDomNode.GenerateCodeNotProjectFile();
                                pinokioCodeDomNode.GenerateCSharpCodeNotAddProject();
                            }

                            string serverPath = PINOKIO_SFTP_PATH.MAIN + "/" + PINOKIO_3D_PATH.FOLDER_NAME + "/" + PINOKIO_3D_PATH.REFERENCE_CLASS_NAME;
                            SFTP.sFtpUpLoadFile(serverPath, Path.Combine(folderPath, "Ref" + simulationClassName + ".cs"));
                            MessageBox.Show("3D Reference Class Upload Success");
                        }
                        else if (checkedListBoxItem.Value.ToString() == "Only 3D Object")
                        {
                            string folder3DPath = CreateBlockByCurrent3D(formUploadAssetItem.textEditFileName.Text);

                            string obj3DPath = Path.Combine(folder3DPath, formUploadAssetItem.textEditFileName.Text + ".obj");
                            string objMtl3DPath = Path.Combine(folder3DPath, formUploadAssetItem.textEditFileName.Text + ".mtl");
                            string objMaterialFolderPath = Path.Combine(folder3DPath, formUploadAssetItem.textEditFileName.Text );
                            SaveThumnailImage(formUploadAssetItem.textEditFileName.Text);

                            string serverPath = PINOKIO_SFTP_PATH.MAIN + "/" + PINOKIO_3D_PATH.FOLDER_NAME + "/" + PINOKIO_3D_PATH.MODEL3D_NAME;
                            SFTP.sFtpUpLoadFile(serverPath, obj3DPath);
                            if (File.Exists(objMtl3DPath))
                            {
                                SFTP.sFtpUpLoadFile(serverPath, objMtl3DPath);

                            }

                            DirectoryInfo di = new DirectoryInfo(objMaterialFolderPath);
                            if (di.Exists)
                            {
                                SFTP.sFtpUpLoadFolder(serverPath, objMaterialFolderPath);
                            }
                  

                            MessageBox.Show("3D Model(.obj) Upload Success");


                        }
                    }
                }


            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void barButtonItemCreate3DRef_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

        private void UpdateReferenceClass()
        {
            try
            {
                if(this.referenceTypes.Count == 0)
                {

                }

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }

        }

    }

    public class MyEntityList : EntityList
    {
        // The tree of the current EntityList assembly
        internal AssemblyTreeView assemblyTree;

        public override void Paste()
        {
            base.Paste();
            CheckDuplicatedBlockReferences();

            // if there is an AssemblyTreeView Tree associated, then update the tree
            if (assemblyTree != null)
                assemblyTree.PopulateTree(this);
        }

        public override void AddRange(IEnumerable<Entity> collection)
        {
            base.AddRange(collection);
            CheckDuplicatedBlockReferences();
        }

        public override void Add(Entity entity)
        {
            base.Add(entity);
            CheckDuplicatedBlockReferences();
        }

        public override bool Remove(Entity entity)
        {
            bool remove = base.Remove(entity);
            CheckDuplicatedBlockReferences();
            return remove;
        }

        public override void RemoveRange(int index, int count)
        {
            base.RemoveRange(index, count);
            CheckDuplicatedBlockReferences();
        }

        /// <summary>
        /// Checks if there are multiple references to the same block in the current EntityList to display an error message on the Model.
        /// </summary>
        private void CheckDuplicatedBlockReferences()
        {
            if (CurrentBlockReference != null)
                return;

            HashSet<string> blocksNames = new HashSet<string>();

            foreach (Entity entity in this)
            {
                if (entity is BlockReference)
                {
                    BlockReference br = (BlockReference)entity;
                    if (blocksNames.Contains(br.BlockName))
                    {

                        break;
                    }
                    blocksNames.Add(br.BlockName);
                }
            }
        }
    }


    public class ReferenceSelectClass
    {
        /// <summary>
        /// 
        /// </summary>
        public ReferenceSelectClass ParentReferenceClsss { get; set; }

        [Editor(typeof(ReferenceTypeEditor), typeof(UITypeEditor))]
        public string ReferenceTypeName { get; set; }

        public Entity Entity { get; set; }

        public ReferenceSelectClass(Entity e )
        {
            Entity = e;
        }

    }
    internal class ReferenceTypeEditor : DevExpress.Utils.UI.CollectionEditor
    {
        public ReferenceTypeEditor(Type type) : base(type)
        {
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));



            FormImportReference formImportReference = new FormImportReference();
            formImportReference.IsImport3D = true;

            if (svc != null)
            {
                if (svc.ShowDialog(formImportReference) == System.Windows.Forms.DialogResult.OK)
                {
                    string referenceName = formImportReference.ImportedReferenceName;
                    Type referenceType = formImportReference.ImportedType;

                    return referenceName;

                }
            }

            return null;
        }

    }

    internal class ReferenceParentTypeEditor : DevExpress.Utils.UI.CollectionEditor
    {
        public ReferenceParentTypeEditor(Type type) : base(type)
        {
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));



            FormImportReference formImportReference = new FormImportReference();
            formImportReference.IsImport3D = true;

            if (svc != null)
            {
                if (svc.ShowDialog(formImportReference) == System.Windows.Forms.DialogResult.OK)
                {
                    string referenceName = formImportReference.ImportedReferenceName;
                    Type referenceType = formImportReference.ImportedType;

                    return referenceName;

                }
            }

            return null;
        }

    }
}