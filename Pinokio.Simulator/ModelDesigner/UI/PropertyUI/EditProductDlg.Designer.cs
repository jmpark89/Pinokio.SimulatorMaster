
namespace Pinokio.Designer
{
    partial class EditProductDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditProductDlg));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlProductLst_etc = new DevExpress.XtraGrid.GridControl();
            this.gridViewProductLst_etc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbOK = new DevExpress.XtraEditors.SimpleButton();
            this.propertyGridControlProduct = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.gridControlProductLst = new DevExpress.XtraGrid.GridControl();
            this.gridViewProductLst = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProductLst_etc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProductLst_etc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProductLst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProductLst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControlProductLst_etc);
            this.layoutControl1.Controls.Add(this.sbCancel);
            this.layoutControl1.Controls.Add(this.sbOK);
            this.layoutControl1.Controls.Add(this.propertyGridControlProduct);
            this.layoutControl1.Controls.Add(this.gridControlProductLst);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1095, 524);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControlProductLst_etc
            // 
            this.gridControlProductLst_etc.Location = new System.Drawing.Point(723, 251);
            this.gridControlProductLst_etc.MainView = this.gridViewProductLst_etc;
            this.gridControlProductLst_etc.Name = "gridControlProductLst_etc";
            this.gridControlProductLst_etc.Size = new System.Drawing.Size(360, 235);
            this.gridControlProductLst_etc.TabIndex = 8;
            this.gridControlProductLst_etc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProductLst_etc});
            this.gridControlProductLst_etc.FocusedViewChanged += new DevExpress.XtraGrid.ViewFocusEventHandler(this.gridControlProductLst_etc_FocusedViewChanged);
            // 
            // gridViewProductLst_etc
            // 
            this.gridViewProductLst_etc.GridControl = this.gridControlProductLst_etc;
            this.gridViewProductLst_etc.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewProductLst_etc.Name = "gridViewProductLst_etc";
            this.gridViewProductLst_etc.OptionsView.ColumnAutoWidth = false;
            this.gridViewProductLst_etc.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // sbCancel
            // 
            this.sbCancel.Location = new System.Drawing.Point(549, 490);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(534, 22);
            this.sbCancel.StyleController = this.layoutControl1;
            this.sbCancel.TabIndex = 7;
            this.sbCancel.Text = "Cancel";
            this.sbCancel.Click += new System.EventHandler(this.sbCancel_Click);
            // 
            // sbOK
            // 
            this.sbOK.Location = new System.Drawing.Point(12, 490);
            this.sbOK.Name = "sbOK";
            this.sbOK.Size = new System.Drawing.Size(533, 22);
            this.sbOK.StyleController = this.layoutControl1;
            this.sbOK.TabIndex = 6;
            this.sbOK.Text = "OK";
            this.sbOK.Click += new System.EventHandler(this.sbOK_Click);
            // 
            // propertyGridControlProduct
            // 
            this.propertyGridControlProduct.Cursor = System.Windows.Forms.Cursors.Default;
            this.propertyGridControlProduct.Location = new System.Drawing.Point(723, 12);
            this.propertyGridControlProduct.Name = "propertyGridControlProduct";
            this.propertyGridControlProduct.Size = new System.Drawing.Size(360, 218);
            this.propertyGridControlProduct.TabIndex = 5;
            this.propertyGridControlProduct.FocusedRowChanged += new DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventHandler(this.propertyGridControlProduct_FocusedRowChanged);
            this.propertyGridControlProduct.CellValueChanged += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(this.propertyGridControlProduct_CellValueChanged);
            this.propertyGridControlProduct.Click += new System.EventHandler(this.propertyGridControlProduct_Click);
            // 
            // gridControlProductLst
            // 
            this.gridControlProductLst.Location = new System.Drawing.Point(12, 12);
            this.gridControlProductLst.MainView = this.gridViewProductLst;
            this.gridControlProductLst.Name = "gridControlProductLst";
            this.gridControlProductLst.Size = new System.Drawing.Size(707, 457);
            this.gridControlProductLst.TabIndex = 4;
            this.gridControlProductLst.UseEmbeddedNavigator = true;
            this.gridControlProductLst.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProductLst});
            // 
            // gridViewProductLst
            // 
            this.gridViewProductLst.GridControl = this.gridControlProductLst;
            this.gridViewProductLst.Name = "gridViewProductLst";
            this.gridViewProductLst.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewProductLst.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewProductLst_FocusedRowChanged);
            this.gridViewProductLst.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewProductLst_CellValueChanged);
            this.gridViewProductLst.DataSourceChanged += new System.EventHandler(this.gridViewProductLst_DataSourceChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1095, 524);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlProductLst;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(711, 478);
            this.layoutControlItem1.Text = "Product List";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Bottom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(68, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.propertyGridControlProduct;
            this.layoutControlItem2.Location = new System.Drawing.Point(711, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(364, 239);
            this.layoutControlItem2.Text = "Edit product";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Bottom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(68, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbOK;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 478);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(537, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sbCancel;
            this.layoutControlItem4.Location = new System.Drawing.Point(537, 478);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(538, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gridControlProductLst_etc;
            this.layoutControlItem5.Location = new System.Drawing.Point(711, 239);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(364, 239);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // EditProductDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 524);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("EditProductDlg.IconOptions.LargeImage")));
            this.Name = "EditProductDlg";
            this.Text = "Edit Product";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProductLst_etc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProductLst_etc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProductLst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProductLst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControlProduct;
        private DevExpress.XtraGrid.GridControl gridControlProductLst;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProductLst;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraEditors.SimpleButton sbOK;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.GridControl gridControlProductLst_etc;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProductLst_etc;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}