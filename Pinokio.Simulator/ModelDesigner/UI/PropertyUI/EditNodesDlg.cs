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
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using Logger;
using Simulation.Engine;
using Pinokio.Geometry;

namespace Pinokio.Designer
{
    public partial class EditNodesDlg : DevExpress.XtraEditors.XtraForm
    {
        private BindingList<NodeEdit> _collection = new BindingList<NodeEdit>();
        private BindingList<NodeDetailEdit> _detailCollection = new BindingList<NodeDetailEdit>();
        private Dictionary<string, List<string>> DicCategoryNodeTypes = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> DicNodeRefTypes = new Dictionary<string, List<string>>();
        private Dictionary<int, SimNodeTreeListNode> DicSelectedNodes = new Dictionary<int, SimNodeTreeListNode>();
        private Dictionary<string, string> DicDisplayProperty = new Dictionary<string, string>();
        private Dictionary<string, string> DicPropertyValue = new Dictionary<string, string>();
        private ModelDesigner _modelDesigner;
        private SimNodeTreeList _simNodeTreeList;
        private PinokioEditorModel _pinokio3DModel1;

        private Dictionary<int, string> _dicChangedRefTypes = new Dictionary<int, string>();

        private NodeReference NewRef = null;
        private RepositoryItemComboBox RefTypeEdit = new RepositoryItemComboBox();
        public Dictionary<int, string> DicChangedRefTypes
        {
            get { return _dicChangedRefTypes; }
            set { _dicChangedRefTypes = value; }
        }
        public EditNodesDlg(ModelDesigner model, PinokioEditorModel pinokio3DModel1, SimNodeTreeList simNodeTreeList)
        {
            _simNodeTreeList = simNodeTreeList;
            _modelDesigner = model;
            _pinokio3DModel1 = pinokio3DModel1;
            DicChangedRefTypes = model.ChangedRefTypes;
            InitializeComponent();
            InitializeGridControl();
        }

        private void InitializeGridControl()
        {
            _collection = new BindingList<NodeEdit>();
            DicCategoryNodeTypes.Clear();
            DicNodeRefTypes.Clear();
            TraverseTreeListNodes(null);
            DicNodeRefTypes = RefTypeDefine.GetRefTypes();

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
                ClearSelection();
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
                var nodeTypeEdit = gridViewNodeTypeList.Columns["NodeType"].ColumnEdit as RepositoryItemComboBox;
                nodeTypeEdit.Items.Clear();
                ClearSelection();
                return;
            }
        }
        private void gridViewNodeTypeList_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Category")
            {
                string category = e.Value?.ToString();
                gridViewNodeTypeList.SetRowCellValue(e.RowHandle, e.Column, category);
                gridViewNodeTypeList.SetRowCellValue(e.RowHandle, "NodeType", "...");
                UpdateNodeTypeComboBoxItems(category);

            }
            else if (e.Column.FieldName == "NodeType")
            {
                string nodeType = e.Value?.ToString();
                UpdateDetailCollection(nodeType);
            }
            gridViewNodeTypeList.RefreshData();
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
            if (_detailCollection.Count == 0) return;

            SimNodeTreeListNode selectedNode = DicSelectedNodes.Values.ToList()[0];
            Type objectType = selectedNode.SimNode.GetType();

            DicDisplayProperty.Clear();
            DicPropertyValue.Clear();

            Dictionary<string, List<string>> dicList = new Dictionary<string, List<string>>();

            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(objectType))
            {
                BrowsableAttribute browsableAttribute = (BrowsableAttribute)prop.Attributes[typeof(BrowsableAttribute)];
                if (browsableAttribute != null && browsableAttribute.Browsable && prop.IsReadOnly == false && prop.PropertyType.IsGenericType == false && !prop.PropertyType.GetTypeInfo().Name.Contains("Vector"))
                {
                    if (!dicList.ContainsKey(prop.Category))
                        dicList[prop.Category] = new List<string>();

                    dicList[prop.Category].Add(prop.DisplayName);
                    DicDisplayProperty.Add(prop.DisplayName, prop.Name);
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

        private void UpdateDetailCollection(string selectedNodeType)
        {
            if (selectedNodeType == "...")
                return;

            gridControlSelectedNodeList.DataSource = null;
            _detailCollection.Clear();
            DicSelectedNodes.Clear();


            foreach (TreeListNode parentNode in _simNodeTreeList.Nodes)
            {
                //모든 SimNode 대상으로 selectedNodeType과 매칭되는 Node들 검색
                AddMatchingNodesToDetailCollection(parentNode, selectedNodeType);
            }

            RefTypeEdit = new RepositoryItemComboBox();
            RefTypeEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            RefTypeEdit.Items.AddRange(DicNodeRefTypes[selectedNodeType]);

            gridControlSelectedNodeList.RepositoryItems.Add(RefTypeEdit);

            gridControlSelectedNodeList.DataSource = _detailCollection;
            gridViewSelectedNodeList.Columns["RefType"].ColumnEdit = RefTypeEdit;
            gridViewSelectedNodeList.Columns["ID"].OptionsColumn.AllowEdit = false;
            gridViewSelectedNodeList.Columns["NodeName"].OptionsColumn.AllowEdit = false;
            gridViewSelectedNodeList.Columns["NodeType"].OptionsColumn.AllowEdit = false;
            gridControlSelectedNodeList.RefreshDataSource();

            UpdateSelectedProperties();
        }

        private void gridViewSelectedNodeList_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "RefType")
            {
                string nodeType = gridViewSelectedNodeList.GetRowCellValue(e.RowHandle, "NodeType")?.ToString();
                if (nodeType.Contains("Line") || nodeType.Contains("Point") || nodeType.Contains("Station") || nodeType.Contains("Link"))
                {
                    gridViewSelectedNodeList.Columns["RefType"].ColumnEdit = null;
                    gridViewSelectedNodeList.Columns["RefType"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    gridViewSelectedNodeList.Columns["RefType"].ColumnEdit = RefTypeEdit;
                    gridViewSelectedNodeList.Columns["RefType"].OptionsColumn.AllowEdit = true;
                }
            }
        }

        private void TreeListSelectedProperties_ShowingEditor(object sender, CancelEventArgs e)
        {
            var treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList != null)
            {
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
            if (e.FocusedRowHandle < 0) return;

            int selectedGridNodeID = Convert.ToInt32(gridViewSelectedNodeList.GetRowCellValue(e.FocusedRowHandle, "ID"));

            SimNodeTreeListNode selectedNode = DicSelectedNodes[selectedGridNodeID];

            if (selectedNode == null)
                return;

            _modelDesigner.CloseObjectManipulator();

            SimNodeTreeListNode rootNode = (SimNodeTreeListNode)selectedNode.RootNode;

            if (rootNode != null)
            {
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
            if (node["NodeType"].ToString() == selectedNodeType)
            {
                int nodeID = Convert.ToInt32(node["ID"]);
                if (!DicChangedRefTypes.ContainsKey(nodeID))
                {
                    DicChangedRefTypes[nodeID] = node["RefType"].ToString();
                    _detailCollection.Add(new NodeDetailEdit(node["ID"].ToString(), node["Name"].ToString(), node["NodeType"].ToString(), node["RefType"].ToString()));
                }
                else
                    _detailCollection.Add(new NodeDetailEdit(node["ID"].ToString(), node["Name"].ToString(), node["NodeType"].ToString(), DicChangedRefTypes[nodeID]));

                DicSelectedNodes.Add(nodeID, (SimNodeTreeListNode)node);
            }

            // 모든 하위 노드에 대해 재귀적으로 함수 호출
            foreach (TreeListNode childNode in node.Nodes)
            {
                AddMatchingNodesToDetailCollection(childNode, selectedNodeType);
            }
        }

        private void TraverseTreeListNodes(DevExpress.XtraTreeList.Nodes.TreeListNode parentNode)
        {
            if (parentNode == null)
            {
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode rootNode in _simNodeTreeList.Nodes)
                {
                    string nodeType = rootNode["NodeType"].ToString();
                    string refType = rootNode["RefType"] != null ? rootNode["RefType"].ToString() : string.Empty;
                    string categoryName = ((SimNodeTreeListNode)rootNode).RefNode != null ? RefTypeDefine.GetCategoryType(((SimNodeTreeListNode)rootNode).RefNode.GetType()) : string.Empty;

                    if (!string.IsNullOrEmpty(categoryName) && !string.IsNullOrEmpty(refType))
                    {
                        if (!DicCategoryNodeTypes.ContainsKey(categoryName))
                            DicCategoryNodeTypes[categoryName] = new List<string>();

                        if (!DicCategoryNodeTypes[categoryName].Contains(nodeType))
                            DicCategoryNodeTypes[categoryName].Add(nodeType);
                    }

                    TraverseTreeListNodes(rootNode);
                }
            }
            else
            {
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode childNode in parentNode.Nodes)
                {
                    string nodeType = childNode["NodeType"].ToString();
                    string refType = childNode["RefType"] != null ? childNode["RefType"].ToString() : string.Empty;
                    string categoryName = ((SimNodeTreeListNode)childNode).RefNode != null ? RefTypeDefine.GetCategoryType(((SimNodeTreeListNode)childNode).RefNode.GetType()) : string.Empty;
                    if (!string.IsNullOrEmpty(categoryName) && !string.IsNullOrEmpty(refType))
                    {
                        if (!DicCategoryNodeTypes.ContainsKey(categoryName))
                            DicCategoryNodeTypes[categoryName] = new List<string>();

                        if (!DicCategoryNodeTypes[categoryName].Contains(nodeType))
                            DicCategoryNodeTypes[categoryName].Add(nodeType);
                    }

                    // childNode의 자식 노드들을 재귀적으로 순회합니다.
                    TraverseTreeListNodes(childNode);
                }
            }
        }

        private void RecursiveFindNonNullPropertyValue(TreeListNode node, Dictionary<string, string> nonNullPropertyValues)
        {
            // 현재 노드의 PropertyValue가 null이 아니면 리스트에 추가
            if (!string.IsNullOrEmpty(node["PropertyValue"].ToString()))
            {
                nonNullPropertyValues[node["CategoryName"].ToString()] = node["PropertyValue"].ToString();

                string propertyName = DicDisplayProperty[node["CategoryName"].ToString()];
                DicPropertyValue[propertyName] = node["PropertyValue"].ToString();
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
                    NodeDetailEdit selectedRow = gridViewSelectedNodeList.GetRow(selectIndex) as NodeDetailEdit;
                    SimNodeTreeListNode simNodeTree = DicSelectedNodes[Convert.ToInt32(selectedRow.ID)];
                    PropertyInfo[] properties = simNodeTree.SimNode.GetType().GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        if (DicPropertyValue.ContainsKey(property.Name))
                        {
                            object changeValue = Convert.ChangeType(DicPropertyValue[property.Name], property.PropertyType);
                            property.SetValue(simNodeTree.SimNode, changeValue);
                        }
                    }


                    // Shape 변경
                    string originRefType = simNodeTree.RefNode.GetType().Name;
                    string changedRefType = "Ref" + selectedRow.RefType;
                    string nodeType = selectedRow.NodeType;

                    if (originRefType != changedRefType)
                    {
                        Point3D insertPoint = simNodeTree.RefNode.CurrentPoint;
                        SimNode core = simNodeTree.SimNode;
                        List<RefLink> fromLinks = new List<RefLink>();
                        List<RefLink> toLinks = new List<RefLink>();

                        fromLinks = simNodeTree.RefNode.FromLinked;
                        toLinks = simNodeTree.RefNode.ToLinked;

                        UpdateNewReference(changedRefType, nodeType);

                        DeleteNodes(simNodeTree);

                        InsertNewRef(core, insertPoint);

                        if (simNodeTree.RefNode != null)
                        {
                            simNodeTree.RefNode.FromLinked = fromLinks;
                            simNodeTree.RefNode.ToLinked = toLinks;
                            simNodeTree.RefNode.MoveLink(_pinokio3DModel1);
                        }
                        else
                            ;

                        DicChangedRefTypes[Convert.ToInt32(selectedRow.ID)] = changedRefType.Remove(0, 3);
                    }
                }
                _modelDesigner.ChangedRefTypes = DicChangedRefTypes;
                int selectedGridNodeID = Convert.ToInt32(gridViewSelectedNodeList.GetRowCellValue(gridViewSelectedNodeList.FocusedRowHandle, "ID"));
                SimNodeTreeListNode selectedNode = DicSelectedNodes[selectedGridNodeID];
                _modelDesigner.CheangeSelectedSimObject4PropertyGrid(selectedNode.SimNode);
                _pinokio3DModel1.Entities.Regen();
                _pinokio3DModel1.Invalidate();
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

        private void ClearSelection()
        {
            treeListSelectedProperties.DataSource = null;
            treeListSelectedProperties.RefreshDataSource();
            gridControlSelectedNodeList.DataSource = null;
            gridControlSelectedNodeList.RefreshDataSource();
        }

        private void UpdateNewReference(string changedRefType, string nodeType)
        {
            NewRef = null;
            Type type = null;

            Assembly a = Assembly.LoadWithPartialName("Pinokio.Animation");
            if (a != null)
                type = a.GetType("Pinokio.Animation." + changedRefType);

            a = Assembly.LoadWithPartialName("Pinokio.Animation.User");
            if (a != null && type == null)
                type = a.GetType("Pinokio.Animation.User." + changedRefType);

            ConstructorInfo[] cs = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            foreach (ConstructorInfo c in cs)
            {
                if (c.GetParameters().Length == 1)
                {
                    if (c.GetParameters()[0].ParameterType.Equals(typeof(string)))
                    {
                        dynamic node = c.Invoke(new object[] { changedRefType });

                        NewRef = node;
                        NewRef.MatchingObjType = nodeType;

                        break;
                    }
                }
            }
        }

        private void InsertNewRef(SimNode simNode, Point3D point)
        {
            List<NodeReference> insertedRefNodes;

            Point3D insertPoint = point;
            if (_pinokio3DModel1.Blocks.FirstOrDefault(x => x.Name == simNode.Name) != null)
            {
                Block block = _pinokio3DModel1.Blocks.FirstOrDefault(x => x.Name == simNode.Name);
                _pinokio3DModel1.Blocks.Remove(block);
            }
            NewRef.Core = simNode;
            NewRef.Insert_MouseUp(_pinokio3DModel1, insertPoint, out insertedRefNodes);

            List<SimNode> insertedSimNodes = insertedRefNodes.ConvertAll(x => x.Core);

            if (insertedRefNodes.Count > 0)
            {
                string insertedNodeType = insertedRefNodes[0].GetType().Name;
                string insertedNodeMatchType = insertedRefNodes[0].MatchingObjType;
                double insertedNodeHeight = insertedRefNodes[0].Height;
                foreach (NodeReference nodeReference in insertedRefNodes)
                {
                    if (nodeReference.Core is CoupledModel)
                        continue;

                    nodeReference.FinishAddNode(_pinokio3DModel1);

                    _pinokio3DModel1.AddNodeReference(nodeReference);
                    if (_pinokio3DModel1.Entities.Contains(nodeReference) is false)
                        _pinokio3DModel1.Entities.Add(nodeReference);

                    if (nodeReference.Core != null)
                        nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

                    if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
                        ModelManager.Instance.AddNode(nodeReference.Core);

                    if (nodeReference.Core is Equipment && !FactoryManager.Instance.Eqps.ContainsKey(nodeReference.Core.ID))
                        FactoryManager.Instance.Eqps.Add(nodeReference.Core.ID, nodeReference.Core as Equipment);
                }
                ModifyNodeTreeList(insertedSimNodes);
            }
        }
        private void ModifyNodeTreeList(List<SimNode> insertedNodes)
        {
            //업데이트 일시 정지
            _simNodeTreeList.BeginUpdate();
            try
            {
                SimNodeTreeListNode floorTreeNode = null;
                //SimNodeTreeListNode parentTreeNode;

                var parentNodesDic = new Dictionary<uint, SimNodeTreeListNode>();

                foreach (SimNode simNode in insertedNodes)
                {
                    if (!parentNodesDic.TryGetValue(Convert.ToUInt32(simNode.ParentNodeID), out SimNodeTreeListNode parentTreeNode))
                    {
                        parentTreeNode = _simNodeTreeList.FindNodeByKeyID(Convert.ToUInt32(simNode.ParentNodeID)) as SimNodeTreeListNode;

                        if (parentTreeNode != null)
                        {
                            parentNodesDic[Convert.ToUInt32(simNode.ParentNodeID)] = parentTreeNode;
                        }
                    }
                    //parentTreeNode = simNodeTreeList.FindNodeByKeyID(Convert.ToUInt32(simNode.ParentNodeID)) as SimNodeTreeListNode;
                    if (parentTreeNode == null)
                    {
                        if (ModelManager.Instance.SimNodes.ContainsKey(simNode.ParentNodeID))
                        {
                            SimNode parentNode = ModelManager.Instance.SimNodes[simNode.ParentNodeID];
                            List<SimNode> parentNodeList = new List<SimNode>();
                            parentNodeList.Add(parentNode);
                            ModifyNodeTreeList(parentNodeList);
                            parentTreeNode = _simNodeTreeList.FindNodeByKeyID(Convert.ToUInt32(simNode.ParentNodeID)) as SimNodeTreeListNode;
                            floorTreeNode = parentTreeNode.RootNode as SimNodeTreeListNode;
                        }
                        else
                        {
                            floorTreeNode = (SimNodeTreeListNode)_simNodeTreeList.FindNodeByKeyID(_pinokio3DModel1.SelectedFloorID);
                            if (floorTreeNode != null)
                            {
                                parentTreeNode = floorTreeNode;
                                simNode.ParentNode = parentTreeNode.SimNode as CoupledModel;
                                if (simNode is TXNode)
                                    ((TXNode)simNode).UpdateFloor();
                            }
                        }
                    }
                    else
                        floorTreeNode = parentTreeNode.RootNode as SimNodeTreeListNode;

                    NodeReference refNode = null;
                    SimNodeTreeListNode addedSimTreeNode;
                    if (_pinokio3DModel1.NodeReferenceByID.ContainsKey(simNode.ID))
                    {
                        refNode = _pinokio3DModel1.NodeReferenceByID[simNode.ID];
                        refNode.LayerName = floorTreeNode.SimNode.ID.ToString();
                        addedSimTreeNode = _simNodeTreeList.FindNodeByKeyID(Convert.ToUInt32(simNode.ID)) as SimNodeTreeListNode;
                        _simNodeTreeList.SetRowCellValue(addedSimTreeNode, "RefType", refNode.GetType().Name.Remove(0, 3));
                    }
                    else
                        addedSimTreeNode = (SimNodeTreeListNode)_simNodeTreeList.AppendNode(new object[] { simNode.ID, simNode.Name, simNode.GetType().Name, null }, parentTreeNode);

                    if (!(simNode is Floor)
                        && parentTreeNode != null
                        && (simNode.ParentNode == null || simNode.ParentNode != parentTreeNode.SimNode))
                    {
                        simNode.ParentNode = parentTreeNode.SimNode as CoupledModel;
                        if (simNode is TXNode)
                            ((TXNode)simNode).UpdateFloor();
                    }

                    addedSimTreeNode.Checked = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
            finally
            {
                //UI 업데이트
                _simNodeTreeList.EndUpdate();
            }
        }
        private void DeleteNodes(SimNodeTreeListNode simNodeTree)
        {
            List<uint> nodeReferenceIds = new List<uint>();

            NodeReference node = simNodeTree.RefNode;

            nodeReferenceIds.Add(node.ID);

            _pinokio3DModel1.DeleteNodeReference(nodeReferenceIds);
            _modelDesigner.ClearSelection();
        }
    }
}