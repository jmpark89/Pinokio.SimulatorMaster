using DevExpress.Utils.UI;
using Pinokio.Model.Base;
using Pinokio.Model.Base.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace Pinokio.Designer
{
    internal class StepsEditor : CollectionEditor
    {
        public StepsEditor(Type type) : base(type)
        {
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            SelectStepsModal selectStepModal = new SelectStepsModal();
            List<uint> ids = (List<uint>)value;
            selectStepModal.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            selectStepModal.InitializeStepDatas(ids, FactoryManager.Instance.StepDatas.Values.ToList());


            if (svc != null)
            {
                if (svc.ShowDialog(selectStepModal) == System.Windows.Forms.DialogResult.OK)
                {
                    ids.Clear();
                    foreach (StepData sd in selectStepModal.StepDatas)
                    {
                        ids.Add(sd.ID);
                    }
                }
            }

            return ids;
        }

    }
}
