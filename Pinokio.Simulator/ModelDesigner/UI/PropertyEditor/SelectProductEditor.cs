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
    internal class   SelectProductEditor : CollectionEditor
    {
        public SelectProductEditor(Type type) : base(type)
        {
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            SelectProductModal selectProductModal = new SelectProductModal();
            List<ProductData> productDatas = new List<ProductData>();
            selectProductModal.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            foreach (var item in FactoryManager.Instance.ProductDatas.Values)
            {
                productDatas.Add(item);
            }

            uint productID = (uint)value;
            selectProductModal.InitializeProductList(productDatas, productID);

            if (svc != null)
            {
                if (svc.ShowDialog(selectProductModal) == System.Windows.Forms.DialogResult.OK)
                {
                    productID = selectProductModal.SelectedID;

                }
        }

            return productID;
        }
}
}
