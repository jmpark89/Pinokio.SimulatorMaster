using DevExpress.XtraEditors;
using Logger;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinokio.Designer.UI.AssetEdit.AssemblyEdit
{
    public partial class FormImportReference : DevExpress.XtraEditors.XtraForm
    {
        public string ImportedReferenceName = string.Empty;
        public Type ImportedType = null;
        public bool IsImport3D = true;
        private List<Type> referenceTypes = new List<Type>();
        private Dictionary<string, Type> dicReferenceTypes = new Dictionary<string, Type>();
        public FormImportReference()
        {
            InitializeComponent();
        }

        private List<Type> GetBlockReferences()
        {
            List<Type> returnValue = new List<Type>(); 
            try
            {
                List<Type> totalTypes = new List<Type>();
                Assembly a = Assembly.LoadWithPartialName("Pinokio.Animation");
                if (a != null)
                    totalTypes.AddRange(a.GetTypes().ToList());
                a = Assembly.LoadWithPartialName("Pinokio.Animation.User");
                if (a != null)
                    totalTypes.AddRange(a.GetTypes().ToList());


                foreach (Type t in totalTypes)
                {
                    MethodInfo m = (t.GetMethods().ToList().Find(x => x.Name == "CreateBlock"));
                    try
                    {
                        if (m != null)
                        {
                            returnValue.Add(t);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return returnValue;
        }

        public void InitializeReferenceList()
        {
            try
            {
                List<Type> referenceList = GetBlockReferences();
                referenceTypes = referenceList;
                foreach (Type t in referenceList)
                    dicReferenceTypes.Add(t.Name, t);
                this.listBoxControlReferenceList.Items.Clear();
                for(int i = 0; i< referenceList.Count; i ++)
                {
                    this.listBoxControlReferenceList.Items.Add(referenceList[i].Name);
                }
            }
            catch (System.Exception ex)
            {

                ErrorLogger.SaveLog(ex);
            }
        }
        private List<Type> GetSimulationSource()
        {
            List<Type> totalTypes = new List<Type>();
            try
            {
      
                Assembly a = Assembly.LoadWithPartialName("Pinokio.Model.Base");
                if (a != null)
                    totalTypes.AddRange(a.GetTypes().ToList());
                a = Assembly.LoadWithPartialName("Pinokio.Model.User");
                if (a != null)
                    totalTypes.AddRange(a.GetTypes().ToList());



                for (int i = 0; i < totalTypes.Count; i++)
                {
                    Type t = totalTypes[i];
                    if (!BaseUtill.IsSameBaseType(t, typeof(SimNode)))
                    {
                        totalTypes.RemoveAt(i);
                        --i;
                    }
                }

            }
            catch (System.Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return totalTypes;
        }


        public void InitializeSimulationList()
        {
            try
            {
                List<Type> referenceList = GetSimulationSource();
                referenceTypes = referenceList;

                foreach (Type t in referenceList)
                    dicReferenceTypes.Add(t.Name, t);

                this.listBoxControlReferenceList.Items.Clear();
                for (int i = 0; i < referenceList.Count; i++)
                {
                    this.listBoxControlReferenceList.Items.Add(referenceList[i].Name);
                }
            }
            catch (System.Exception ex)
            {

                ErrorLogger.SaveLog(ex);
            }
        }
        private void FormImportReference_Load(object sender, EventArgs e)
        {

            try
            {
                if (IsImport3D)
                    InitializeReferenceList();
                else
                    InitializeSimulationList();
            }
            catch (System.Exception ex)
            {

                ErrorLogger.SaveLog(ex);
            }
        }

        private void listBoxControlReferenceList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Left)
                    return;
                if (listBoxControlReferenceList.SelectedIndex > -1 && listBoxControlReferenceList.SelectedIndex < listBoxControlReferenceList.Items.Count)
                {
                    string referenceName = listBoxControlReferenceList.SelectedItem.ToString();

                    ImportedReferenceName = referenceName;
                    ImportedType = dicReferenceTypes[referenceName];

         

                    this.DialogResult = DialogResult.OK;

                    this.Close();
                }


            }
            catch (System.Exception ex)
            {

                ErrorLogger.SaveLog(ex);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (System.Exception ex)
            {

                ErrorLogger.SaveLog(ex);
            }
        }
    }
}