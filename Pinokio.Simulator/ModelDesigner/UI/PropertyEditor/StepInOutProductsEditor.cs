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
    internal class StepInOutProductsEditor : CollectionEditor
    {
        public StepInOutProductsEditor(Type type) : base(type)
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            StepInOutProductsModal selectInOutProductModal = new StepInOutProductsModal();
            selectInOutProductModal.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Dictionary<uint, uint> productCounts = (Dictionary<uint, uint>)value;
            selectInOutProductModal.InitializeProductDatas(productCounts, FactoryManager.Instance.ProductDatas.Values.ToList());

            if (svc != null)
            {
                if (svc.ShowDialog(selectInOutProductModal) == System.Windows.Forms.DialogResult.OK)
                {
                    productCounts.Clear();
                    foreach (InOutProductData ioProductData in selectInOutProductModal.InOutProductDatas)
                        productCounts.Add(ioProductData.ProductID, ioProductData.Count);
                }
            }

            return productCounts;
        }

    }
}
