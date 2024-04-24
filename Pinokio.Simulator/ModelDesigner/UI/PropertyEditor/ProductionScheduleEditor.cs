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
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Pinokio.Designer
{
    internal class ProductionScheduleEditor : CollectionEditor
    {
        //public delegate void MyFormClosedEventHandler(object sender, FormClosedEventArgs e);
        //public static event MyFormClosedEventHandler MyFormClosed;
        public ProductionScheduleEditor(Type type) : base(type)
        {
               
        }


        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            List<ProductionSchedule> datas = (List<ProductionSchedule>)value;
            ProductionScheduleEditingDlg scheduleProductionDlg = new ProductionScheduleEditingDlg();

            scheduleProductionDlg.InitializeScheduleProduction(ref datas);
            if (svc != null)
            {
                if (svc.ShowDialog(scheduleProductionDlg) == System.Windows.Forms.DialogResult.OK)
                {
                    datas.Clear();
                    datas = scheduleProductionDlg.GetProductionScheduleDatas();
                }
            }

            return datas;
        }
   


        protected override Type[] CreateNewItemTypes()
        {
            return base.CreateNewItemTypes();
        }

        protected override object SetItems(object editValue, object[] value)
        {
            return base.SetItems(editValue, value);
        }

        protected override object CreateInstance(Type itemType)
        {
            return base.CreateInstance(itemType);
        }
    }
}
