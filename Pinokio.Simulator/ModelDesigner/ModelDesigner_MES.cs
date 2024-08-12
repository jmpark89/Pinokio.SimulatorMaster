using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Pinokio.Model.Base;
using Logger;
using Pinokio.Model.Base.Structure;
using Pinokio.Database;

namespace Pinokio.Designer
{
    public partial class ModelDesigner : UserControl
    {
        [NonSerialized]
        public Dictionary<int, string> ChangedRefTypes = new Dictionary<int, string>();

        private void bbiModelingProductionData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                EditProductDlg makeProductionDlg = new EditProductDlg();
                makeProductionDlg.InitialzieGoods(FactoryManager.Instance.ProductDatas.Values.ToList());
                makeProductionDlg.StartPosition = FormStartPosition.CenterScreen;
                if (makeProductionDlg.ShowDialog() == DialogResult.OK)
                {
                    List<ProductData> goods = makeProductionDlg.GetManufacturedGoodsDatas();
                    FactoryManager.Instance.ProductDatas = new Dictionary<uint, ProductData>();
                    for (int i = 0; i < goods.Count; i++)
                    {
                        for(int j = 0; j < goods[i].ProductionSchedules.Count; j++)
                        {
                            goods[i].ProductionSchedules[j].ProductID = goods[i].ProductID;
                        }

                        FactoryManager.Instance.ProductDatas.Add(goods[i].ProductID, goods[i]);
                    }
                }

                ProductData.CurrentCreateProductID = FactoryManager.Instance.ProductDatas.Keys.Max();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void bbiSettingSteps_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                EditStepDlg modifyStepInfoInEQP = new EditStepDlg();
                modifyStepInfoInEQP.InitializeStepData(FactoryManager.Instance.StepDatas.Values.ToList());
                modifyStepInfoInEQP.StartPosition = FormStartPosition.CenterScreen;
                if (modifyStepInfoInEQP.ShowDialog() == DialogResult.OK)
                {
                    FactoryManager.Instance.StepDatas = new Dictionary<uint, StepData>();
                    foreach (StepData sd in modifyStepInfoInEQP.Collection)
                    {
                        FactoryManager.Instance.AddStep(sd);
                    }
                    foreach (ProductData pd in FactoryManager.Instance.ProductDatas.Values)
                    {
                        for (int i = pd.IdsOfStep.Count - 1; i >= 0; i--)
                        {
                            if (!FactoryManager.Instance.StepDatas.Keys.Contains(pd.IdsOfStep[i]))
                            {
                                pd.IdsOfStep.RemoveAt(i);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void bbiEditEquipment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                EditEquipmentDlg modifyStepInfoInEQP = new EditEquipmentDlg();
                modifyStepInfoInEQP.InitializeEquipment(FactoryManager.Instance.Eqps.Values.ToList());
                modifyStepInfoInEQP.StartPosition = FormStartPosition.CenterScreen;
                modifyStepInfoInEQP.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        private void bbiEditBreakDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditBreakDownDlg makeBreakDownDlg = new EditBreakDownDlg();

            //makeBreakDownDlg.InitialzieGoods(FactoryManager.Instance.ProductDatas.Values.ToList());
            makeBreakDownDlg.StartPosition = FormStartPosition.CenterScreen;
            if (makeBreakDownDlg.ShowDialog() == DialogResult.OK)
            {
                ;
            }
        }
        private void bbiConnectMES_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormConnectMES conMES = new FormConnectMES();
            conMES.StartPosition = FormStartPosition.CenterParent;
            if (conMES.ShowDialog() == DialogResult.OK)
            {
                ModelManager.Instance.OnConnectMES = true;
                ModelManager.Instance.MES_DB_Type = conMES.MES_DB_Type;

                // DB에 Table이 존재하는지 확인
                // 없는 경우 Columns와 Table 생성
                if (conMES.MES_DB_Type == DBType.MYSQL)
                {
                    DBUtils.AddDBTableInOptionFromMES(DBType.MYSQL, conMES.TableList);
                    MySQLDBOption.CheckTableConfiguration();
                }
                else if (conMES.MES_DB_Type == DBType.ORACLE) { }
                    
            }
            else
                ModelManager.Instance.OnConnectMES = false;
        }
        private void bbiEditNodes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditNodesDlg editNodeDlg = new EditNodesDlg(this, pinokio3DModel1, simNodeTreeList);
            editNodeDlg.StartPosition = FormStartPosition.CenterScreen;
            editNodeDlg.Show();
        }
    }
}

