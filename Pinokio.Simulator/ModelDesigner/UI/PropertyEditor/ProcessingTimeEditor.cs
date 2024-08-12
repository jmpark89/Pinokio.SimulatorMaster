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
    internal class ProcessingTimeEditor : CollectionEditor
    {
        public ProcessingTimeEditor(Type type) : base(type)
        {

        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            EditProcessingTimeDlg editProcessingTimeDlg = new EditProcessingTimeDlg();
            ProcessingTime processingTimeData = ((StepDataEdit)context.Instance).ProcessingTimeData;
            editProcessingTimeDlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            if (value == null)
                processingTimeData = new ProcessingTime();

            editProcessingTimeDlg.InitializeProcessingTimeData(ref processingTimeData);

            if (svc != null)
            {
                if (svc.ShowDialog(editProcessingTimeDlg) == System.Windows.Forms.DialogResult.OK)
                    processingTimeData = editProcessingTimeDlg.GetProcessingTimeData();

                
            }
            processingTimeData = editProcessingTimeDlg.GetProcessingTimeData();
            return processingTimeData;
        }
    }
}
