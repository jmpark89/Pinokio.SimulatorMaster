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

namespace Pinokio.Designer
{
    public partial class KPIGridChartExampleForm : DevExpress.XtraEditors.XtraForm
    {
        public KPIGridChartExampleForm()
        {
            InitializeComponent();
            InitGridControl();
            InitChartControls();
        }

        #region Init 함수
        private void InitGridControl()
        {
            this.gridKPI.BeginInit();

            this.gridKPI.EndInit();
        }

        private void InitChartControls()
        {
            InitChartLineKPI();
            InitChartBarKPI();
        }

        private void InitChartLineKPI()
        {
            this.chartLineKPI.BeginInit();

            this.chartLineKPI.EndInit();
        }

        private void InitChartBarKPI()
        {
            this.chartBarKPI.BeginInit();

            this.chartBarKPI.EndInit();
        }
        #endregion

        #region Update 함수 - 외부 접근 가능
        public void UpdateKPIData(DataTable dt)
        {
            UpdateGridControl(dt);
            UpdateChartLineControl(dt);
            UpdateChartBarControl(dt);
        }

        public void UpdateGridControl(DataTable dt)
        {
            this.gridKPI.BeginUpdate();
            this.gridKPI.DataSource = dt;
            this.gridKPI.EndUpdate();
        }
        public void UpdateChartLineControl(DataTable dt)
        {
            this.chartLineKPI.Series.Clear();

            foreach (DataRow drow in dt.Rows)
            {

            }

            this.chartLineKPI.Update();
        }
        public void UpdateChartBarControl(DataTable dt)
        {
            this.chartBarKPI.Series.Clear();

            foreach (DataRow drow in dt.Rows)
            {

            }

            this.chartBarKPI.Update();
        } 
        #endregion
    }
}