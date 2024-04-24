
using DevExpress.Utils.UI;
using Pinokio.Model.Base;
using Simulation.Engine;
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
    internal class SelectNodeEditor : CollectionEditor
    {
        public SelectNodeEditor(Type type) : base(type)
        {
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            SelectTXNodeModal selectNodeModal = new SelectTXNodeModal();


            selectNodeModal.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            List<TXNode> sources = new List<TXNode>(); 

            foreach (SimNode s in ModelManager.Instance.SimNodes.Values)
            {
               if (s is TXNode)
                    sources.Add((TXNode)s);
            }

            uint sourceID = (uint)value;
            selectNodeModal.InitializeNodeList(sources, sourceID);
            if (svc != null)
            {
                if (svc.ShowDialog(selectNodeModal) == System.Windows.Forms.DialogResult.OK)
                {
                    sourceID = selectNodeModal.GetCheckIndex(sourceID);
                }
            }

            return sourceID;
        }
    }
}
