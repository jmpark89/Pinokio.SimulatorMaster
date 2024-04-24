
namespace Pinokio.Designer
{
    partial class KPIGridChartExampleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram4 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            this.gridKPI = new DevExpress.XtraGrid.GridControl();
            this.gridViewKPI = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chartLineKPI = new DevExpress.XtraCharts.ChartControl();
            this.chartBarKPI = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridKPI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewKPI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLineKPI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBarKPI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            this.SuspendLayout();
            // 
            // gridKPI
            // 
            this.gridKPI.Dock = System.Windows.Forms.DockStyle.Left;
            this.gridKPI.Location = new System.Drawing.Point(0, 0);
            this.gridKPI.MainView = this.gridViewKPI;
            this.gridKPI.Name = "gridKPI";
            this.gridKPI.Size = new System.Drawing.Size(400, 612);
            this.gridKPI.TabIndex = 0;
            this.gridKPI.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewKPI});
            // 
            // gridViewKPI
            // 
            this.gridViewKPI.GridControl = this.gridKPI;
            this.gridViewKPI.Name = "gridViewKPI";
            this.gridViewKPI.OptionsView.ShowGroupPanel = false;
            // 
            // chartLineKPI
            // 
            xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
            this.chartLineKPI.Diagram = xyDiagram3;
            this.chartLineKPI.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartLineKPI.Legend.Name = "Default Legend";
            this.chartLineKPI.Location = new System.Drawing.Point(400, 0);
            this.chartLineKPI.Name = "chartLineKPI";
            series3.Name = "Series 1";
            series3.View = lineSeriesView2;
            this.chartLineKPI.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3};
            this.chartLineKPI.Size = new System.Drawing.Size(755, 338);
            this.chartLineKPI.TabIndex = 1;
            // 
            // chartBarKPI
            // 
            xyDiagram4.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram4.AxisY.VisibleInPanesSerializable = "-1";
            this.chartBarKPI.Diagram = xyDiagram4;
            this.chartBarKPI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBarKPI.Legend.Name = "Default Legend";
            this.chartBarKPI.Location = new System.Drawing.Point(400, 338);
            this.chartBarKPI.Name = "chartBarKPI";
            series4.Name = "Series 1";
            this.chartBarKPI.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series4};
            this.chartBarKPI.Size = new System.Drawing.Size(755, 274);
            this.chartBarKPI.TabIndex = 2;
            // 
            // KPIGridChartExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 612);
            this.Controls.Add(this.chartBarKPI);
            this.Controls.Add(this.chartLineKPI);
            this.Controls.Add(this.gridKPI);
            this.Name = "KPIGridChartExampleForm";
            this.Text = "KPIGridChartExampleForm";
            ((System.ComponentModel.ISupportInitialize)(this.gridKPI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewKPI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLineKPI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBarKPI)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridKPI;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewKPI;
        private DevExpress.XtraCharts.ChartControl chartLineKPI;
        private DevExpress.XtraCharts.ChartControl chartBarKPI;
    }
}