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
    public partial class AMHSInquiryTargetForm : DevExpress.XtraEditors.XtraForm
    {
        public AMHSInquiryTargetForm(AMHSReportForm reportKPIForm)
        {
            InitializeComponent();
            InitCheckBoxList(reportKPIForm);
        }

        /// 선택된 CS의 하위항목들 Checkbox List에 추가
        private void InitCheckBoxList(AMHSReportForm reportKPIForm)
        {
            string stringAll = "ALL";
            checkedListBox.Items.Add(stringAll);
            bool isStartBtn;
            bool isEndBtn;
            bool checkOk = false;
            bool isStartCheck;
            bool isEndCheck;
            List<string> selectedSubCSNames = reportKPIForm._selectedSubCSNames;
            List<string> selectedStartMRNames = reportKPIForm._selectedStartMRNames;
            List<string> selectedEndMRNames = reportKPIForm._selectedEndMRNames;

            isStartBtn = reportKPIForm._isStartbtn;
            isEndBtn = reportKPIForm._isEndbtn;
            isEndCheck = reportKPIForm._isEndCheck;
            isStartCheck = reportKPIForm._isStartCheck;

            string csType = "";

            switch (reportKPIForm.AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    break;
                case 1:
                    csType = reportKPIForm.cboxEditCMDTrend.SelectedItem.ToString();
                    break;
                case 2:
                    csType = reportKPIForm.cboxEditCMDDistribution.SelectedItem.ToString();
                    break;
                case 3:
                    csType = reportKPIForm.cboxEditVehicleOperationRate.SelectedItem.ToString();
                    break;
                default:
                    break;
            }

            switch (reportKPIForm.AMHSTabPages.SelectedTabPageIndex)
            {
                case 0:
                    if (isStartBtn&& !isEndBtn)
                    {
                        foreach (string startName in SimResultDBManager.Instance.StartMRNames)
                        {

                            if (selectedStartMRNames.Contains(startName))
                            {
                                checkedListBox.Items.Add(startName, true);
                                checkOk = true;
                            }
                            else
                            {
                                checkedListBox.Items.Add(startName, false);
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
                    else if (isEndBtn&& selectedStartMRNames.Count>0)
                    {
                        foreach (string endName in SimResultDBManager.Instance.EndMRNames)
                        {

                            if (selectedEndMRNames.Contains(endName))
                            {
                                checkedListBox.Items.Add(endName, true);
                                checkOk = true;
                            }
                            else
                            {
                                checkedListBox.Items.Add(endName, false);
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

                    break;
                case 1:
                    foreach (string csName in SimResultDBManager.Instance.SubCSNames)
                    {
                        if ((csType == stringAll || csName.Contains(csType)) && selectedSubCSNames.Contains(csName))
                            checkedListBox.Items.Add(csName, true);
                        else if ((csType == stringAll || csName.Contains(csType)) && !selectedSubCSNames.Contains(csType))
                            checkedListBox.Items.Add(csName, false);
                    }
                    break;
                case 2:
                    foreach (string csName in SimResultDBManager.Instance.SubCSNames)
                    {
                        if ((csType == stringAll || csName.Contains(csType)) && selectedSubCSNames.Contains(csName))
                            checkedListBox.Items.Add(csName, true);
                        else if ((csType == stringAll || csName.Contains(csType)) && !selectedSubCSNames.Contains(csType))
                            checkedListBox.Items.Add(csName, false);
                    }
                    break;
                case 3:
                    foreach (string csName in SimResultDBManager.Instance.SubCSNames)
                    {
                        if ((csType == stringAll || csName.Contains(csType)) && selectedSubCSNames.Contains(csName))
                            checkedListBox.Items.Add(csName, true);
                        else if ((csType == stringAll || csName.Contains(csType)) && !selectedSubCSNames.Contains(csType))
                            checkedListBox.Items.Add(csName, false);
                    }
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
            if (checkCounter == checkedListBox.Items.Count - 1)
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