using DevExpress.Utils.UI;
using Pinokio.Model.Base;
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
    internal class EqpsEditor : CollectionEditor
    {
        public EqpsEditor(Type type) : base(type)
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            SelectEqpModal selectEqpModal = new SelectEqpModal();
            selectEqpModal.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            List<Equipment> eqpDatas = new List<Equipment>();
            List<uint> eqpIDs = ((StepDataEdit)context.Instance).EditIdsOfEqp;
            
            string idsStepType = null;
            idsStepType = ((StepDataEdit)context.Instance).StepType.ToString();
            foreach (var item in FactoryManager.Instance.Eqps.Values)
            {
                if(item.StepType.ToString()== idsStepType)
                    eqpDatas.Add(item); 
            }
            selectEqpModal.InitializeNodeDatas(eqpDatas, eqpIDs);

            if (svc != null)
            {
                if (svc.ShowDialog(selectEqpModal) == System.Windows.Forms.DialogResult.OK)
                {
                    eqpIDs = new List<uint>();
                    foreach (Equipment eqp in selectEqpModal.SelectEqpDatas)
                        eqpIDs.Add(eqp.ID);

                    return eqpIDs;
                }
            }

            return value;
        }

    }
}
