using DevExpress.Utils.UI;
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
    public class AddPoductEditor : CollectionEditor
    {
        public AddPoductEditor(Type type) : base(type)
        {
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {

            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));


            Dictionary<uint, ProductData> products = (Dictionary<uint, ProductData>)value;

            EditProductDlg makeProductionDlg = new EditProductDlg();
            makeProductionDlg.InitialzieGoods(products.Values.ToList());
            makeProductionDlg.StartPosition = FormStartPosition.CenterScreen;
            
            if (svc != null)
            {
                if (svc.ShowDialog(makeProductionDlg) == System.Windows.Forms.DialogResult.OK)
                {
                    List<ProductData> goods = makeProductionDlg.GetManufacturedGoodsDatas();
                    products = new Dictionary<uint, ProductData>();
                    for (int i = 0; i < goods.Count; i++)
                    {
                        products.Add(goods[i].ProductID, goods[i]);
                    }
                }
            }

            return products;
        }
    }
}
