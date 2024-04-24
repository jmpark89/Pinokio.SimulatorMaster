
namespace Pinokio.Designer
{
    partial class EditNodesDlg
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.treeListSelectedProperties = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumnCategoryName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnPropertyValue = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.gridControlSelectedNodeList = new DevExpress.XtraGrid.GridControl();
            this.gridViewSelectedNodeList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbOK = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlNodeTypeList = new DevExpress.XtraGrid.GridControl();
            this.gridViewNodeTypeList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListSelectedProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSelectedNodeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSelectedNodeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNodeTypeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNodeTypeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.treeListSelectedProperties);
            this.layoutControl1.Controls.Add(this.gridControlSelectedNodeList);
            this.layoutControl1.Controls.Add(this.sbCancel);
            this.layoutControl1.Controls.Add(this.sbOK);
            this.layoutControl1.Controls.Add(this.gridControlNodeTypeList);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(3190, 411, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1095, 524);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // treeListSelectedProperties
            // 
            this.treeListSelectedProperties.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnCategoryName,
            this.treeListColumnPropertyValue});
            this.treeListSelectedProperties.Location = new System.Drawing.Point(723, 12);
            this.treeListSelectedProperties.Name = "treeListSelectedProperties";
            this.treeListSelectedProperties.Size = new System.Drawing.Size(360, 474);
            this.treeListSelectedProperties.TabIndex = 8;
            // 
            // treeListColumnCategoryName
            // 
            this.treeListColumnCategoryName.Caption = "CategoryName";
            this.treeListColumnCategoryName.FieldName = "CategoryName";
            this.treeListColumnCategoryName.Name = "treeListColumnCategoryName";
            this.treeListColumnCategoryName.Visible = true;
            this.treeListColumnCategoryName.VisibleIndex = 0;
            // 
            // treeListColumnPropertyValue
            // 
            this.treeListColumnPropertyValue.Caption = "PropertyValue";
            this.treeListColumnPropertyValue.FieldName = "PropertyValue";
            this.treeListColumnPropertyValue.Name = "treeListColumnPropertyValue";
            this.treeListColumnPropertyValue.Visible = true;
            this.treeListColumnPropertyValue.VisibleIndex = 1;
            // 
            // gridControlSelectedNodeList
            // 
            this.gridControlSelectedNodeList.Location = new System.Drawing.Point(12, 165);
            this.gridControlSelectedNodeList.MainView = this.gridViewSelectedNodeList;
            this.gridControlSelectedNodeList.Name = "gridControlSelectedNodeList";
            this.gridControlSelectedNodeList.Size = new System.Drawing.Size(707, 321);
            this.gridControlSelectedNodeList.TabIndex = 6;
            this.gridControlSelectedNodeList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSelectedNodeList});
            // 
            // gridViewSelectedNodeList
            // 
            this.gridViewSelectedNodeList.GridControl = this.gridControlSelectedNodeList;
            this.gridViewSelectedNodeList.Name = "gridViewSelectedNodeList";
            this.gridViewSelectedNodeList.OptionsSelection.MultiSelect = true;
            this.gridViewSelectedNodeList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            // 
            // sbCancel
            // 
            this.sbCancel.Location = new System.Drawing.Point(549, 490);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(534, 22);
            this.sbCancel.StyleController = this.layoutControl1;
            this.sbCancel.TabIndex = 5;
            this.sbCancel.Text = "Cancel";
            this.sbCancel.Click += new System.EventHandler(this.sbCancel_Click);
            // 
            // sbOK
            // 
            this.sbOK.Location = new System.Drawing.Point(12, 490);
            this.sbOK.Name = "sbOK";
            this.sbOK.Size = new System.Drawing.Size(533, 22);
            this.sbOK.StyleController = this.layoutControl1;
            this.sbOK.TabIndex = 4;
            this.sbOK.Text = "OK";
            this.sbOK.Click += new System.EventHandler(this.sbOK_Click);
            // 
            // gridControlNodeTypeList
            // 
            this.gridControlNodeTypeList.Location = new System.Drawing.Point(12, 12);
            this.gridControlNodeTypeList.MainView = this.gridViewNodeTypeList;
            this.gridControlNodeTypeList.Name = "gridControlNodeTypeList";
            this.gridControlNodeTypeList.Size = new System.Drawing.Size(707, 149);
            this.gridControlNodeTypeList.TabIndex = 0;
            this.gridControlNodeTypeList.UseEmbeddedNavigator = true;
            this.gridControlNodeTypeList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewNodeTypeList});
            // 
            // gridViewNodeTypeList
            // 
            this.gridViewNodeTypeList.GridControl = this.gridControlNodeTypeList;
            this.gridViewNodeTypeList.Name = "gridViewNodeTypeList";
            this.gridViewNodeTypeList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1095, 524);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlNodeTypeList;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(711, 153);
            this.layoutControlItem1.Text = "Node List";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Bottom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
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
            this.layoutControlItem5.Control = this.gridControlSelectedNodeList;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 153);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(711, 325);
            this.layoutControlItem5.Text = "SimNodes";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.treeListSelectedProperties;
            this.layoutControlItem2.Location = new System.Drawing.Point(711, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(364, 478);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // EditNodesDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 524);
            this.Controls.Add(this.layoutControl1);
            this.Name = "EditNodesDlg";
            this.Text = "Edit Nodes";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListSelectedProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSelectedNodeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSelectedNodeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNodeTypeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNodeTypeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraEditors.SimpleButton sbOK;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.GridControl gridControlNodeTypeList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNodeTypeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControlSelectedNodeList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSelectedNodeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraTreeList.TreeList treeListSelectedProperties;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnCategoryName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnPropertyValue;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}