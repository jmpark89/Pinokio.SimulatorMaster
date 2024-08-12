using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pinokio.Model.Base;

namespace Pinokio.Designer
{
    public partial class ProductionInquiryTargetForm : DevExpress.XtraEditors.XtraForm
    {
        
        public ProductionInquiryTargetForm(ProductionReportForm reportKPIForm)
        {
            InitializeComponent();
            InitCheckBoxList(reportKPIForm);
        }
        /// 선택된 CS의 하위항목들 Checkbox List에 추가
        private void InitCheckBoxList(ProductionReportForm reportKPIForm)
        {
            string stringAll = "ALL";
            checkedListBox.Items.Add(stringAll);
            bool checkOk = false;
            bool isGroupCheck ; //ProductionReoportForm에서 Step페이지의 InquiryTarget 위에거를 이미 골랐을때
            
            List<string> selectedStepEqpNames = reportKPIForm._selectedStepEqpNames;
            List<string> selectedStepNames = reportKPIForm._selectedStepNames;
            List<string> selectedProductNames = reportKPIForm._selectedProductNames;
            List<string> selectedEqpGroupByNames = reportKPIForm._selectedEqpGroupByNames;
            List<string> selectedEqpGroupNames = reportKPIForm._selectedEqpGroupNames;
            List<string> selectedStepGroupByNames = reportKPIForm._selectedStepGroupByNames;
            List<string> selectedStepGroupNames = reportKPIForm._selectedStepGroupNames;
            List<string> selectedNodeInoutNames = reportKPIForm._selectedNodeInoutNames;
            List<string> selectedProductInoutNames = reportKPIForm._selectedProductInoutNames;
            isGroupCheck = reportKPIForm._isGroupCheck;
            
            string stepType = "";
            string eqpType = "";
            string NodeInoutType = "";

            switch (reportKPIForm.ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    stepType = reportKPIForm.cboxEditStepGroupBy.SelectedItem.ToString();
                    break;
                case 1:
                    
                    eqpType = reportKPIForm.cboxEditEqpPlanOperationRate.SelectedItem.ToString();
                    break;
                case 2:
                    NodeInoutType = reportKPIForm.cboxEditInoutBy.SelectedItem.ToString();
                    break;
                default:
                    break;
            }


            switch (reportKPIForm.ProductionReportPages.SelectedTabPageIndex)
            {
                case 0:
                    if (stepType.Contains("Equipment"))
                    {
                        foreach (string eqpName in ModelManager.Instance.SimResultDBManager.EqpNames)
                        {
                            
                                if (selectedStepEqpNames.Contains(eqpName))
                                {
                                    checkedListBox.Items.Add(eqpName, true);
                                checkOk = true;
                                }
                                else
                                {
                                    checkedListBox.Items.Add(eqpName, false);
                                }
                        }
                        if (!checkOk)
                        {
                            for (int i = 1; i < checkedListBox.Items.Count; i++)
                            {
                                checkedListBox.SetItemChecked(i, true);
                            }
                        }
                    }
                    else if (stepType.Contains("Step"))
                    {
                        foreach (string eqpStepName in ModelManager.Instance.SimResultDBManager.EqpStepNames)
                        {
                            if (selectedStepNames.Contains(eqpStepName))
                            {
                                checkedListBox.Items.Add(eqpStepName, true);
                                checkOk = true;
                            }
                            else
                            {
                                checkedListBox.Items.Add(eqpStepName, false);
                            }
                        }
                        if (!checkOk)
                        {
                            for (int i = 1; i < checkedListBox.Items.Count; i++)
                            {
                                checkedListBox.SetItemChecked(i, true);
                            }
                        }
                    }
                    else if (stepType.Contains("Product"))
                    {
                        foreach (string productName in ModelManager.Instance.SimResultDBManager.ProductNames)
                        {
                            if (selectedProductNames.Contains(productName))
                            {
                                checkedListBox.Items.Add(productName, true);
                                checkOk = true;
                            }
                            else
                            {
                                checkedListBox.Items.Add(productName, false);
                            }
                        }
                        if (!checkOk)
                        {
                            for (int i = 1; i < checkedListBox.Items.Count; i++)
                            {
                                checkedListBox.SetItemChecked(i, true);
                            }
                        }
                    }
                    checkOk = false;
                    break;
                case 1:
                    if (!isGroupCheck)
                    {
                        if (eqpType.Contains("EQP_GROUP"))
                        {
                            foreach (string eqpGroup in ModelManager.Instance.SimResultDBManager.EqpGroupNames)
                            {
                                if (selectedEqpGroupByNames.Contains(eqpGroup))
                                {
                                    checkedListBox.Items.Add(eqpGroup, true);
                                    checkOk = true;
                                }
                                else
                                {
                                    checkedListBox.Items.Add(eqpGroup, false);
                                }
                            }
                            if (!checkOk)
                            {
                                for (int i = 1; i < checkedListBox.Items.Count; i++)
                                {
                                    checkedListBox.SetItemChecked(i, true);
                                }
                            }
                            

                        }
                        if (eqpType.Contains("STEP_GROUP"))
                        {
                                foreach (string stepGroup in ModelManager.Instance.SimResultDBManager.StepGroupNames)
                                {

                                    if (selectedStepGroupByNames.Contains(stepGroup))
                                    {
                                        checkedListBox.Items.Add(stepGroup, true);
                                        checkOk = true;
                                    }
                                    else
                                    {
                                        checkedListBox.Items.Add(stepGroup, false);
                                    }
                                }
                                if (!checkOk)
                                {
                                    for (int i = 1; i < checkedListBox.Items.Count; i++)
                                    {
                                        checkedListBox.SetItemChecked(i, true);
                                    }
                                }
                            
                        }
                        checkOk = false;
                    }
                    else
                    {
                        if (selectedStepGroupByNames.Count > 0)
                        {
                            for (int i = 0; selectedStepGroupByNames.Count > i; i++)
                            {
                                if (!selectedStepGroupByNames[i].Contains("ALL"))
                                {
                                    for (int j = 0; FactoryManager.Instance.Eqps.Count > j; j++)
                                    {
                                        if (selectedStepGroupByNames[i].Contains(FactoryManager.Instance.Eqps.ElementAt(j).Value.StepGroupName)
                                            && selectedStepGroupNames.Contains(FactoryManager.Instance.Eqps.ElementAt(j).Value.Name))
                                        {
                                            checkedListBox.Items.Add(FactoryManager.Instance.Eqps.ElementAt(j).Value.Name, true);
                                            checkOk = true;
                                        }
                                        else if (selectedStepGroupByNames[i].Contains(FactoryManager.Instance.Eqps.ElementAt(j).Value.StepGroupName))
                                        {
                                            checkedListBox.Items.Add(FactoryManager.Instance.Eqps.ElementAt(j).Value.Name, false);
                                        }
                                    }
                                }
                            }
                            if (!checkOk)
                            {
                                for (int i = 1; i < checkedListBox.Items.Count; i++)
                                {
                                    checkedListBox.SetItemChecked(i, true);
                                }
                            }
                        }
                        else if(selectedEqpGroupByNames.Count> 0)
                        {
                            for(int i = 0; selectedEqpGroupByNames.Count > i; i++)
                            {
                                if (!selectedEqpGroupByNames[i].Contains("ALL"))
                                {
                                    for (int j = 0; FactoryManager.Instance.Eqps.Count > j; j++)
                                    {
                                        if (selectedEqpGroupByNames[i].Contains(FactoryManager.Instance.Eqps.ElementAt(j).Value.EqpGroupName)
                                            && selectedEqpGroupNames.Contains(FactoryManager.Instance.Eqps.ElementAt(j).Value.Name))
                                        {                                            
                                            checkedListBox.Items.Add(FactoryManager.Instance.Eqps.ElementAt(j).Value.Name, true);
                                            checkOk = true;
                                        }
                                        else if(selectedEqpGroupByNames[i].Contains(FactoryManager.Instance.Eqps.ElementAt(j).Value.EqpGroupName))
                                        {
                                            checkedListBox.Items.Add(FactoryManager.Instance.Eqps.ElementAt(j).Value.Name, false);
                                        }
                                    }
                                }

                            }
                            if (!checkOk)
                            {
                                for (int i = 1; i < checkedListBox.Items.Count; i++)
                                {
                                    checkedListBox.SetItemChecked(i, true);
                                }
                            }
                        }
                        checkOk = false;
                    }
                        
                    break;
                case 2:
                    if (NodeInoutType.Contains("Node"))
                    {
                        foreach (string nodeName in ModelManager.Instance.SimResultDBManager.NodeInoutNames)
                        {
                            if (selectedNodeInoutNames.Contains(nodeName))
                            {
                                checkedListBox.Items.Add(nodeName, true);
                                checkOk = true;
                            }
                            else
                            {
                                checkedListBox.Items.Add(nodeName, false);
                            }
                        }
                        if (!checkOk)
                        {
                            for (int i = 1; i < checkedListBox.Items.Count; i++)
                            {
                                checkedListBox.SetItemChecked(i, true);
                            }
                        }
                    }

                    else if (NodeInoutType.Contains("Product"))
                    {
                        foreach (string productName in ModelManager.Instance.SimResultDBManager.ProductNames)
                        {
                            if (selectedProductInoutNames.Contains(productName))
                            {
                                checkedListBox.Items.Add(productName, true);
                                checkOk = true;
                            }
                            else
                            {
                                checkedListBox.Items.Add(productName, false);
                            }
                        }
                        if (!checkOk)
                        {
                            for (int i = 1; i < checkedListBox.Items.Count; i++)
                            {
                                checkedListBox.SetItemChecked(i, true);
                            }
                        }
                    }
                    checkOk = false;
                    
                    break;
                default:
                    break;
            }
            if (allOn())
                checkedListBox.SetItemChecked(0, true);
        }
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public bool allOn()
        {
            int checkCounter = 0;

            for (int i = 1; i < checkedListBox.Items.Count; i++)
            {
                if (checkedListBox.GetItemChecked(i))
                {
                    checkCounter++;
                }
            }
            if (checkCounter == checkedListBox.Items.Count-1)
            {
                return true;
            }
            else
            {
                return false;
            }          
        }
        private void checkedListBox_MouseUp(object sender, MouseEventArgs e)
        {
            bool allCheck = false;
            int allCheckNum = 0;

            if (checkedListBox.SelectedIndex == 0)
            {
                Console.WriteLine("All: " + checkedListBox.GetItemChecked(0).ToString());
                if (checkedListBox.GetItemChecked(0))
                {
                    for (int i = 1; i < checkedListBox.Items.Count; i++)
                    {
                        checkedListBox.SetItemChecked(i, true);
                    }
                    allCheck = true;
                }
                else
                {
                    for (int i = 1; i < checkedListBox.Items.Count; i++)
                    {
                        checkedListBox.SetItemChecked(i, false);
                    }
                    allCheck = false;
                }
                return;
            }
            else
            {
                for (int i = 1; i < checkedListBox.Items.Count; i++)
                {
                    if (checkedListBox.GetItemChecked(i))
                    {
                        allCheckNum++;
                    }
                }
                if (allCheckNum == checkedListBox.Items.Count - 1)
                {
                    allCheck = true;
                }
                else
                {
                    allCheck = false;
                }
                if (allCheck)
                {
                    checkedListBox.SetItemChecked(0, true);
                }
                else
                {
                    if (checkedListBox.GetItemChecked(0) == true && allCheckNum == checkedListBox.Items.Count - 2)
                    {
                        checkedListBox.SetItemChecked(0, false);
                    }
                }
            }
        }
    }
}