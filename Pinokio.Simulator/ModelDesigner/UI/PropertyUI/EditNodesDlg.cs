using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using DevExpress.XtraEditors.Repository;
using Pinokio.Model.Base;
using Pinokio.Animation;
using DevExpress.XtraTreeList.Nodes;
using devDept.Geometry;
using Logger;

namespace Pinokio.Designer
{
    public partial class EditNodesDlg : DevExpress.XtraEditors.XtraForm
    {
        BindingList<NodeEdit> _collection = new BindingList<NodeEdit>();
        BindingList<NodeDetailEdit> _detailCollection = new BindingList<NodeDetailEdit>();
        Dictionary<string, List<string>> DicCategoryNodeTypes = new Dictionary<string, List<string>>();
        Dictionary<int, SimNodeTreeListNode> DicSelectedNodes = new Dictionary<int, SimNodeTreeListNode>();
        ModelDesigner _modelDesigner;

        SimNodeTreeList _simNodeTreeList;
        public EditNodesDlg(ModelDesigner model, SimNodeTreeList simNodeTreeList)
        {
            InitializeComponent();
            InitializeGridControl();

            _simNodeTreeList = simNodeTreeList;
            _modelDesigner = model;

        }

        private void InitializeGridControl()
        {
            _collection = new BindingList<NodeEdit>();
            DicCategoryNodeTypes = findCategoryNames();

            RepositoryItemComboBox categoryEdit = new RepositoryItemComboBox();
            categoryEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            
            categoryEdit.Items.AddRange(DicCategoryNodeTypes.Keys.ToList());
            gridControlNodeTypeList.RepositoryItems.Add(categoryEdit);

            RepositoryItemComboBox nodeTypeEdit = new RepositoryItemComboBox();
            nodeTypeEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            nodeTypeEdit.EditValueChanged += nodeTypeEdit_EditValueChanged;

            gridControlNodeTypeList.RepositoryItems.Add(nodeTypeEdit);

            gridControlNodeTypeList.DataSource = _collection;

            gridViewNodeTypeList.Columns["Category"].ColumnEdit = categoryEdit;
            gridViewNodeTypeList.Columns["NodeType"].ColumnEdit = nodeTypeEdit;

            // CellValueChanged 이벤트 처리
            gridViewNodeTypeList.CellValueChanged += gridViewNodeTypeList_CellValueChanged;
            gridViewNodeTypeList.FocusedRowChanged += gridViewNodeTypeList_FocusedRowChanged;
            gridViewSelectedNodeList.FocusedRowChanged += gridViewSelectedNodeList_FocusedRowChanged;

            this.gridControlNodeTypeList.RefreshDataSource();
        }
        private void nodeTypeEdit_EditValueChanged(object sender, EventArgs e)
        {
            gridViewNodeTypeList.PostEditor();
            gridViewNodeTypeList.UpdateCurrentRow();
        }

        private void gridViewNodeTypeList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                treeListSelectedProperties.DataSource = null;
                treeListSelectedProperties.RefreshDataSource();
                gridControlSelectedNodeList.DataSource = null;
                gridControlSelectedNodeList.RefreshDataSource();
                return;
            }
            string selectedCategory = gridViewNodeTypeList.GetRowCellValue(e.FocusedRowHandle, "Category").ToString();
            if (selectedCategory != "...")
            {
                UpdateNodeTypeComboBoxItems(selectedCategory);

                string selectedNodeType = gridViewNodeTypeList.GetRowCellValue(e.FocusedRowHandle, "NodeType")?.ToString();
                if (!string.IsNullOrEmpty(selectedNodeType))
                {
                    UpdateDetailCollection(selectedNodeType);
                }
            }
            else
            {
                treeListSelectedProperties.DataSource = null;
                treeListSelectedProperties.RefreshDataSource();
                gridControlSelectedNodeList.DataSource = null;
                gridControlSelectedNodeList.RefreshDataSource();
                return;
            }
        }
        private void gridViewNodeTypeList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Category")
            {
                string category = e.Value?.ToString();
                UpdateNodeTypeComboBoxItems(category);
            }
            else if (e.Column.FieldName == "NodeType")
            {
                string nodeType = e.Value?.ToString();
                UpdateDetailCollection(nodeType);
            }
        }

        private void UpdateNodeTypeComboBoxItems(string category)
        {
            var nodeTypeEdit = gridViewNodeTypeList.Columns["NodeType"].ColumnEdit as RepositoryItemComboBox;
            if (nodeTypeEdit != null)
            {
                nodeTypeEdit.Items.Clear();
                if (DicCategoryNodeTypes.ContainsKey(category))
                {
                    nodeTypeEdit.Items.AddRange(DicCategoryNodeTypes[category]);
                }
            }
        }
        private void UpdateSelectedProperties()
        {
            if (_detailCollection.Count != 0)
            {
                SimNodeTreeListNode selectedNode = DicSelectedNodes.Values.ToList()[0];
                Type objectType = selectedNode.SimNode.GetType();

                Dictionary<string, List<string>> dicList = new Dictionary<string, List<string>>();
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(objectType))
                {
                    BrowsableAttribute browsableAttribute = (BrowsableAttribute)prop.Attributes[typeof(BrowsableAttribute)];
                    if (browsableAttribute != null && browsableAttribute.Browsable && prop.IsReadOnly == false && prop.PropertyType.IsGenericType == false && !prop.PropertyType.GetTypeInfo().Name.Contains("Vector") /*&& !prop.PropertyType.IsClass*/)
                    {
                        if (!dicList.ContainsKey(prop.Category))
                            dicList.Add(prop.Category, new List<string>());

                        dicList[prop.Category].Add(prop.DisplayName);
                    }
                }
                List<NodeProperties> nodeProperties = new List<NodeProperties>();
                int id = 1;
                int parentId = -1;
                dicList = dicList.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

                foreach (var category in dicList.Keys)
                {
                    var categoryNode = new NodeProperties() { ID = id++, ParentID = parentId, CategoryName = category, PropertyValue = string.Empty };
                    nodeProperties.Add(categoryNode);
                    int currentParentId = categoryNode.ID;

                    foreach (var name in dicList[category])
                    {
                        nodeProperties.Add(new NodeProperties() { ID = id++, ParentID = currentParentId, CategoryName = name, PropertyValue = string.Empty });
                    }
                }
                nodeProperties = nodeProperties.OrderBy(np => np.CategoryName).ToList();

                treeListSelectedProperties.ClearNodes();
                treeListSelectedProperties.DataSource = nodeProperties;
                treeListSelectedProperties.ParentFieldName = "ParentID"; // 부모 노드를 식별하는 필드
                treeListSelectedProperties.KeyFieldName = "ID"; // 각 노드를 고유하게 식별하는 필드
                treeListSelectedProperties.Columns["CategoryName"].Visible = true; //
                treeListSelectedProperties.ExpandAll();
                treeListSelectedProperties.RefreshDataSource();
                treeListSelectedProperties.ShowingEditor += TreeListSelectedProperties_ShowingEditor;
            }
        }
        private void UpdateDetailCollection(string selectedNodeType)
        {
            //모든 SimNode 대상으로 selectedNodeType과 매칭되는 Node들 검색
            _detailCollection.Clear();
            DicSelectedNodes.Clear();

            foreach (TreeListNode parentNode in _simNodeTreeList.Nodes)
            {
                AddMatchingNodesToDetailCollection(parentNode, selectedNodeType);
            }
            gridControlSelectedNodeList.DataSource = _detailCollection;
            gridControlSelectedNodeList.RefreshDataSource();

            UpdateSelectedProperties();


        }
        private void TreeListSelectedProperties_ShowingEditor(object sender, CancelEventArgs e)
        {
            var treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList != null)
            {
                // 현재 선택된 노드와 컬럼을 확인합니다.
                var focusedNode = treeList.FocusedNode;
                var focusedColumn = treeList.FocusedColumn;

                // PropertyValue 컬럼의 편집 여부를 결정합니다.
                if (focusedColumn.FieldName == "PropertyValue")
                {
                    // 부모 노드인 경우 (ParentID가 -1인 경우를 부모 노드로 가정)
                    if ((int)focusedNode.GetValue("ParentID") == -1)
                    {
                        // 부모 노드의 PropertyValue 컬럼 편집을 취소합니다.
                        e.Cancel = true;
                    }
                }
            }
        }
        private void gridViewSelectedNodeList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;
            int selectedGridNodeID = Convert.ToInt32(gridViewSelectedNodeList.GetRowCellValue(e.FocusedRowHandle, "ID"));
            SimNodeTreeListNode selectedNode = DicSelectedNodes[selectedGridNodeID];

            if (selectedNode == null)
                return;

            _modelDesigner.CloseObjectManipulator();

            SimNodeTreeListNode rootNode = (SimNodeTreeListNode)selectedNode.RootNode;

            if (rootNode != null)
            {
                //simNodeTreeList.ClearSelection();

                if (rootNode.SimNode is Floor)
                {
                    _modelDesigner.Pinokio3Dmodel.SelectedFloorID = rootNode.SimNode.ID;
                    _modelDesigner.Pinokio3Dmodel.SelectedFloorHeight = _modelDesigner.GetFloorPlanTreeNode(_modelDesigner.Pinokio3Dmodel.SelectedFloorID).FloorBottom;
                }

                _modelDesigner.ClearPropertyGrid();

                _modelDesigner.AddSelectedNodeReferenceFromTreeSelect(selectedNode);

                BeginInvoke(new Action(() => _modelDesigner.Pinokio3Dmodel.Focus()));

                if (selectedNode.RefNode != null)
                {
                    _modelDesigner.Pinokio3Dmodel.Camera.Location = new Point3D(selectedNode.RefNode.Core.PosVec3.X, selectedNode.RefNode.Core.PosVec3.Y);
                }

                _modelDesigner.CheangeSelectedSimObject4PropertyGrid(selectedNode.SimNode);
            }
            else
            {
                _modelDesigner.ClearSelection();
                _modelDesigner.initiallzeFloor(_modelDesigner.FloorForm.LstFloorPlan);
                BeginInvoke(new Action(() => _modelDesigner.Pinokio3Dmodel.ZoomFit())); return;
            }
            
            
        }

        private void AddMatchingNodesToDetailCollection(TreeListNode node, string selectedNodeType)
        {
            // 현재 노드가 조건을 만족하는지 검사
            if (node["NodeType"] != null && node["NodeType"].ToString() == selectedNodeType || node["NodeType"] != null && selectedNodeType.Contains(node["NodeType"].ToString()))
            {
                _detailCollection.Add(new NodeDetailEdit(node["ID"].ToString(), node["Name"].ToString(), node["NodeType"].ToString(), node["RefType"].ToString()));
                DicSelectedNodes.Add(Convert.ToInt32(node["ID"]), (SimNodeTreeListNode)node);
            }

            // 모든 하위 노드에 대해 재귀적으로 함수 호출
            foreach (TreeListNode childNode in node.Nodes)
            {
                AddMatchingNodesToDetailCollection(childNode, selectedNodeType);
            }
        }
        private Dictionary<string, List<string>> findCategoryNames()
        {
            Dictionary<string, List<string>> categoryNames = new Dictionary<string, List<string>>();

            Assembly a = Assembly.LoadWithPartialName("Pinokio.Animation");
            if (a != null)
            {
                List<Type> ts = a.GetTypes().ToList();
                for (int i = 0; i < ts.Count; i++)
                {
                    Type t = ts[i];

                    if (IsInsertNodeReference(t))
                    {
                        string _categoryName = RefTypeDefine.GetCategoryType(t);
                        if (!categoryNames.Keys.Contains(_categoryName))
                            categoryNames.Add(RefTypeDefine.GetCategoryType(t), new List<string>());

                        string name = t.Name.Remove(0, 3);
                        List<string> value = RefTypeDefine.GetUsableSimNodeTypes(t);

                        categoryNames[_categoryName].Add(name);
                        //if (value.Contains(name))
                        //{
                        //    categoryNames[_categoryName].Add(name);
                        //}
                        //else
                        //{
                        //    categoryNames[_categoryName].Add(value[0]);
                        //}
                    }
                }
            }

            a = Assembly.LoadWithPartialName("Pinokio.Animation.User");

            if (a != null)
            {
                List<Type> ts = a.GetTypes().ToList();
                for (int i = 0; i < ts.Count; i++)
                {
                    Type t = ts[i];
                    if (IsInsertNodeReference(t))
                    {
                        string _categoryName = RefTypeDefine.GetCategoryType(t);
                        if (!categoryNames.Keys.Contains(_categoryName))
                            categoryNames.Add(RefTypeDefine.GetCategoryType(t), new List<string>());

                        string name = t.Name.Remove(0, 3);
                        List<string> value = RefTypeDefine.GetUsableSimNodeTypes(t);

                        categoryNames[_categoryName].Add(name);
                        //if (value.Contains(name))
                        //{
                        //    categoryNames[_categoryName].Add(name);
                        //}
                        //else
                        //{
                        //    categoryNames[_categoryName].Add(value[0]);
                        //}
                    }
                }
            }
            return categoryNames;
        }

        private bool IsInsertNodeReference(Type t)
        {
            if (BaseUtill.IsSameBaseType(t, typeof(NodeReference)))
            {
                FieldInfo fieldInfo = t.GetField("IsInserted");
                if (fieldInfo != null && (bool)fieldInfo.GetValue(null))
                {
                    ConstructorInfo[] cs = t.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
                    foreach (ConstructorInfo c2 in cs)
                    {
                        if (c2.GetParameters().Length == 1)
                        {
                            if (c2.GetParameters()[0].ParameterType.Equals(typeof(string)))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        private void RecursiveFindNonNullPropertyValue(TreeListNode node, Dictionary<string, string> nonNullPropertyValues)
        {
            // 현재 노드의 PropertyValue가 null이 아니면 리스트에 추가
            if (node["PropertyValue"] != null && !string.IsNullOrEmpty(node["PropertyValue"].ToString()))
            {
                nonNullPropertyValues.Add(node["CategoryName"].ToString(), node["PropertyValue"].ToString());
            }

            // 현재 노드의 하위 노드에 대해서도 동일한 검사를 재귀적으로 수행
            foreach (TreeListNode childNode in node.Nodes)
            {
                RecursiveFindNonNullPropertyValue(childNode, nonNullPropertyValues);
            }
        }
        private void sbOK_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> nonNullPropertyValues = new Dictionary<string, string>();

                foreach (TreeListNode node in treeListSelectedProperties.Nodes)
                {
                    RecursiveFindNonNullPropertyValue(node, nonNullPropertyValues);
                }

                foreach (int selectIndex in this.gridViewSelectedNodeList.GetSelectedRows())
                {
                    SimNodeTreeListNode simNodeTree = DicSelectedNodes[Convert.ToInt32(_detailCollection[selectIndex].ID)];
                    PropertyInfo[] properties = simNodeTree.SimNode.GetType().GetProperties();
                    
                    foreach (PropertyInfo property in properties)
                    {
                        if (nonNullPropertyValues.ContainsKey(property.Name))
                        {
                            object changeValue = Convert.ChangeType(nonNullPropertyValues[property.Name], property.PropertyType);
                            property.SetValue(simNodeTree.SimNode, changeValue);
                        }
                    }
                }
                int selectedGridNodeID = Convert.ToInt32(gridViewSelectedNodeList.GetRowCellValue(gridViewSelectedNodeList.FocusedRowHandle, "ID"));
                SimNodeTreeListNode selectedNode = DicSelectedNodes[selectedGridNodeID];
                _modelDesigner.CheangeSelectedSimObject4PropertyGrid(selectedNode.SimNode);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
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
    }
}