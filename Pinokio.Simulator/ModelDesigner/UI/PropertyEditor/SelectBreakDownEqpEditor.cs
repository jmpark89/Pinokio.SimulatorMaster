using DevExpress.Utils.UI;
using Pinokio.Animation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Pinokio.Designer
{
    class SelectBreakDownEqpEditor : CollectionEditor
    {
        public SelectBreakDownEqpEditor(Type type) : base(type)
        {
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            SelectBreakDownEQPModal selectNodeModal = new SelectBreakDownEQPModal();


            selectNodeModal.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            List<Type> types = new List<Type>();

            foreach (Type type in RefTypeDefine.PartReferenceTypes)
            {
                types.Add(type);
            }

            string refName = (string)value;
            selectNodeModal.InitializeRefNameList(types, refName);
            if (svc != null)
            {
                if (svc.ShowDialog(selectNodeModal) == System.Windows.Forms.DialogResult.OK)
                {
                    refName = selectNodeModal.GetCheckName(refName);
                }
            }

            return refName;
        }
    }
}
