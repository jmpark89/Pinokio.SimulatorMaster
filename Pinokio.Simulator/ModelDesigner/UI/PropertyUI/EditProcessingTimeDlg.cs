using DevExpress.XtraEditors;
using Logger;
using Pinokio.Model.Base.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Pinokio.Designer
{
    public partial class EditProcessingTimeDlg : DevExpress.XtraEditors.XtraForm
    {
        public delegate void MyFormClosedEventHandler(object sender, FormClosedEventArgs e);

        public static event MyFormClosedEventHandler MyFormClosed;

        BindingList<ProcessingTime> _processingTimeDataEdits = new BindingList<ProcessingTime>();
        public BindingList<ProcessingTime> ProcessingTimeDataEdits { get => _processingTimeDataEdits; }

        public ProcessingTime processingTimeDataEdit = null;

        public EditProcessingTimeDlg()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(EditProcessingTimeDlg_FormClosed);
        }
        public void InitializeProcessingTimeData(ref ProcessingTime processingTimeDataEdit)
        {
            try
            {
                this.processingTimeDataEdit = processingTimeDataEdit;
                _processingTimeDataEdits = new BindingList<ProcessingTime>();

                _processingTimeDataEdits.Add(new ProcessingTime(processingTimeDataEdit));
                
                this.gridControlProcessingTime.DataSource = _processingTimeDataEdits;
                this.gridViewProcessingTime.RefreshData();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
        public ProcessingTime GetProcessingTimeData()
        {
            ProcessingTime processingTimeData = new ProcessingTime();

            try
            {
                foreach (ProcessingTime productionScheduleDataEdit in _processingTimeDataEdits)
                {
                    processingTimeData = new ProcessingTime(productionScheduleDataEdit);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            return processingTimeData;
        }

        private void EditProcessingTimeDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MyFormClosed != null && this.DialogResult != DialogResult.Cancel)
            {
                MyFormClosed(this, e);
            }
        }

        private void gridViewProcessingTime_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            processingTimeDataEdit = GetProcessingTimeData();
        }

        private void sb_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void sb_OK_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }
    }
}