
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Environment = devDept.Eyeshot.Environment;
using Image = System.Drawing.Image;

namespace Pinokio.Designer
{
    /// <summary>
    /// Utility class to browse assembly structure. The Tag (<see cref="NodeTag"/>) represents the currently examined BlockReference/Entity
    /// and is used later for Screen->Tree and Tree->Screen selection.
    /// </summary>
    public class AssemblyTreeView : MultiSelectTreeView
    {
        /// <summary>
        /// The model associated to this AssemblyTreeView control.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public devDept.Eyeshot.Model Model { get; set; }
        /// <summary>
        /// The list of current selected items.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Environment.SelectedItem> SelectedItems { get; set; }
        
        /// <summary>
        /// True if the <see cref="devDept.Eyeshot.EntityList.CurrentBlockReference"/> is set.
        /// </summary>
        private bool IsEditAssembly { get; set; }

        /// <summary>
        /// The level of the hierarchy assembly of the current BlockReference. It's needed to correctly handle frozen nodes of the TreeView during Edit-Assembly.
        /// </summary>
        private int CurrentLevelAssembly { get; set; }

        /// <summary>
        /// True if the ObjectManipulator is enabled.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMoving { get; private set; }

        /// <summary>
        /// True if the TreeView nodes are changing.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool TreeModify { get; private set; }

        private Camera prevView;

        /// <summary>
        /// The structure saved into <see cref="TreeNode.Tag"/> holding the data needed to identify the entity related to that node.
        /// </summary>
        public class NodeTag
        {
            public Entity Entity { get; set; }
            public Stack<BlockReference> Parents { get; set; }
            public NodeTag(Entity ent, Stack<BlockReference> parents)
            {
                Entity = ent;
                Parents = parents;
            }
        }

        public AssemblyTreeView()
        {
#if WPF
            System.Windows.Forms.Application.EnableVisualStyles();                            
#endif
            // filss imageList with icons
            ImageList imageList = new ImageList();
            imageList.ColorDepth = ColorDepth.Depth16Bit;
            imageList.TransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            imageList.ImageSize = new System.Drawing.Size(16, 16);
          
            imageList.Images.Add("entities", (Image)Properties.Resources.entities);
            imageList.Images.Add("component", (Image)Properties.Resources.component);
            imageList.Images.Add("component_frozen", (Image)Properties.Resources.component_frozen);
            imageList.Images.Add("NoSel", (Image)Properties.Resources.NoSel);
            imageList.Images.Add("NoVis", (Image)Properties.Resources.NoVis);
            imageList.Images.Add("Part", (Image)Properties.Resources.Part);
            imageList.Images.Add("part_frozen", (Image)Properties.Resources.part_frozen);

            this.ImageList = imageList;

            // to let the custom drawing of the icons and visibility/selectability flag icons
            DrawMode = TreeViewDrawMode.OwnerDrawText;

            SelectedItems = new List<Environment.SelectedItem>();
            HideSelection = false;
        }

        #region TreeView structure and synchronization  

        private static char space = '_';

        /// <summary>
        /// Returns the right string representing the imageKey needed for that node.
        /// </summary>
        /// <param name="leaf">False if the current node is associated to a BlockReference.</param>
        /// <param name="frozen">True if the current node is associated to an entity outside the hierarchy of the current BlockReference set.</param>
        /// <returns>The string to be used as <see cref="TreeNode.ImageKey"/>.</returns>
        private static string GetImageKey(bool leaf, bool frozen)
        {
            string key = leaf ? "Part" : "Component";

            if (frozen)
            {
                key += space + "Frozen";
            }

            return key;
        }

        /// <summary>
        /// Checks if the <param name="node"> is set with the frozen icon.</param>
        /// </summary>
        /// <param name="node">the interested node</param>
        /// <returns>True if the frozen icon is set.</returns>
        public static bool IsFrozen(TreeNode node)
        {
            return node.ImageKey.Split(space).Length == 2;
        }

        /// <summary>
        /// Remove the frozen state to the image icon of the <param name="node">, if it's set.
        /// </summary>
        /// <param name="node">the node of the TreeView</param>
        public static void UnFrozeNode(TreeNode node)
        {
            node.ImageKey = node.ImageKey.Split(space)[0];
        }

        /// <summary>
        /// Set the frozen state icon of the <param name="node">.
        /// </summary>
        /// <param name="node">the node of the TreeView</param>
        public static void FrozeNode(TreeNode node)
        {
            node.ImageKey += space + "Frozen";
        }

        private static string GetNodeName(string name, int index)
        {
            return String.Format("{0}", name);
            //version that adds the index of the entity in the list near the block name.
            //return String.Format("{0} ({1})", name, index);
        }

        /// <summary>
        /// Function to populate a single node of the tree. 
        /// </summary>
        /// <param name="entList">The list of the children to add to the parentNode.</param>
        /// <param name="parentNode">The parent node. Null if it's the root node.</param>
        public void PopulateTree(IList<Entity> entList, TreeNode parentNode = null)
        {
            TreeModify = true;

            BeginUpdate();
            // blocks the redrawing of all the elements of the treeview (like the scroolbar)
            // to speed up the node.Clear() (BeginUpdate() was not enough)
            LockWindowUpdate(this.Handle);

            Stack<BlockReference> currentParents = null;
            List<TreeNode> nodes = new List<TreeNode>();
            if (parentNode == null)
            {
                Nodes.Clear();

                //add a root node on top of the entities nodes
                if (Model.Entities.IsOpenCurrentBlockReference && Model.Entities.CurrentBlockReference != null)
                {
                    parentNode = new TreeNode(Model.Entities.CurrentBlockReference.BlockName);
                    parentNode.Tag = new NodeTag(Model.Entities.CurrentBlockReference, null/*, model.Entities.Parents*/);
                    parentNode.ImageKey = GetImageKey(false, false);
                    currentParents = new Stack<BlockReference>();
                    currentParents.Push(Model.Entities.CurrentBlockReference);                    
                }
                else
                {
                    parentNode = new TreeNode("Entities");
                    parentNode.Tag = null;
                    parentNode.ImageKey = "entities";
                }

                Nodes.Add(parentNode);
            }
            else if(parentNode.Tag != null)
            {
                NodeTag nodeTag = ((NodeTag)parentNode.Tag);
                currentParents = nodeTag.Parents == null ? new Stack<BlockReference>() : new Stack<BlockReference>(nodeTag.Parents.Reverse());
                currentParents.Push((BlockReference)nodeTag.Entity);
            }


            for (int i = 0; i < entList.Count; i++)
            {
                Entity ent = entList[i];
                if (ent is BlockReference)
                {
                    Block child;
                    string blockName = ((BlockReference)ent).BlockName;

                    if (Model.Blocks.TryGetValue(blockName, out child))
                    {
                        TreeNode parentTn = new TreeNode(GetNodeName(blockName, i));
                        parentTn.Tag = new NodeTag(ent, currentParents);


                        string imageKey = GetImageKey(false, false);
                        parentTn.ImageKey = imageKey;
                        parentTn.SelectedImageKey = imageKey;
                        parentTn.Nodes.Add("child");

                        nodes.Add(parentTn);
                    }
                }
                else
                {
                    string type = ent.GetType().ToString().Split('.').LastOrDefault();
                    var node = new TreeNode(GetNodeName(type, i));
                    node.Tag = new NodeTag(ent, currentParents);

                    string imageKey = GetImageKey(true, false);
                    node.ImageKey = imageKey;
                    node.SelectedImageKey = imageKey;
                    nodes.Add(node);
                }
            }

            parentNode.Nodes.Clear();
            parentNode.Nodes.AddRange(nodes.ToArray());

            if(!parentNode.IsExpanded)
                parentNode.Expand();
            
            LockWindowUpdate(IntPtr.Zero);
            EndUpdate();

            TreeModify = false;
        }

        /// <summary>
        /// Tree->Screen Selection. If the viewport entities are selected, they get marked as selected straight away.
        /// To check we are considering the correct entities we use the Entity stored in the Tag property of the MultiSelectTreeView
        /// </summary>
        /// <returns>The selected item.</returns>
        public void SynchTreeSelection()
        {
            // Fill a stack of entities and blockreferences starting from the node tags.
            Stack<BlockReference> parents = new Stack<BlockReference>();

            if (SelectedNodes.Count > 0)
            {
                TreeNode node = SelectedNodes[0];

                // the root node
                if (node.Tag == null) return;

                Entity entity = (node.Tag as NodeTag).Entity;

                // if the selected node is not the current blockReference, then fill the parents stack
                if (!ReferenceEquals(entity, Model.Entities.CurrentBlockReference))
                {
                    node = node.Parent;

                    while (node != null && node.Tag != null)
                    {
                        var ent = (node.Tag as NodeTag).Entity;
                        if (ent != null && !ReferenceEquals(ent, Model.Entities.CurrentBlockReference))
                            parents.Push((BlockReference) ent);
                        else
                            break;
                        node = node.Parent;
                    }
                }

                SelectedItems = new List<Environment.SelectedItem>();
                
                Model.Entities.ClearSelection();
                foreach (TreeNode selectedNode in SelectedNodes)
                {
                    // The top most parent is the root Blockreference: must reverse the order, creating a new Stack
                    var selItem = new Environment.SelectedItem(new Stack<BlockReference>(parents),
                        ((NodeTag) selectedNode.Tag).Entity);

                    // Selects the item
                    selItem.Select(Model ,true);

                    SelectedItems.Add(selItem);
                }
            }
        }

        /// <summary>
        /// Screen->Tree Selection.
        /// </summary>
        /// <param name="blockReferences">The BlockReference stack</param>
        public void SynchScreenSelection(Stack<BlockReference> blockReferences)
        {
            TreeModify = true;

            SelectedNode = null;
            SelectedNodes.Clear();

            blockReferences = Model.Entities.IsOpenCurrentBlockReference ? new Stack<BlockReference>() : blockReferences;

            if (SelectedItems != null && SelectedItems.Count > 0 && SelectedItems[0].Parents.Count > 0)
            {
                //// Add the parents of the first selectedEntity (must be all on the same level so the parents should be the same) to the BlockReferences stack

                // Reverse the stack so the one on top is the one at the root of the hierarchy
                var parentsReversed = SelectedItems[0].Parents.Reverse();

                var cumulativeStack = new Stack<BlockReference>(blockReferences);

                foreach (var br in parentsReversed)
                {
                    cumulativeStack.Push(br);
                }

                // Create a new stack with the reversed order so the one on top is the root.
                blockReferences = new Stack<BlockReference>(cumulativeStack);
            }

            BeginUpdate();
            SearchNodeInTree(blockReferences, SelectedItems);
            EndUpdate();

            TreeModify = false;
        }

        /// <summary>
        /// Screen->Tree Selection. To check we are considering the correct entities, we use the Entity stored in the Tag property of the MultiSelectTreeView.
        /// </summary>
        /// <param name="blockReferences">The block reference stack</param>
        /// <param name="selectedItems">The selected entity inside a block reference. Can be null when we click on a BlockReference.</param>
        /// <param name="parentTn">The parent TreeNode for searching inside its nodes. Can be null.</param>
        public void SearchNodeInTree(Stack<BlockReference> blockReferences,
            List<Environment.SelectedItem> selectedItems, TreeNode parentTn = null)
        { 
            if (blockReferences.Count == 0 && (selectedItems == null || selectedItems.Count == 0))
            {
                if (Nodes.Count > 0 && !Nodes[0].IsExpanded)
                {
                    // expand root node
                    Nodes[0].Expand();
                }

                return;
            }

            List<TreeNode> selectedNodes = new List<TreeNode>();
            TreeNodeCollection tnc = Nodes;
            if (parentTn != null)
                tnc = parentTn.Nodes;
            else
            {
                // expand root node
                if(!Nodes[0].IsExpanded)
                    Nodes[0].Expand();
                tnc = Nodes[0].Nodes;
            }

            if (blockReferences.Count > 0)
            {
                // Nested BlockReferences

                BlockReference br = blockReferences.Pop();

                foreach (TreeNode tn in tnc)
                {
                    if (ReferenceEquals(br, ((NodeTag)tn.Tag).Entity))
                    {
                        if (!tn.IsExpanded)
                            tn.Expand();

                        if (blockReferences.Count > 0)
                        {
                            SearchNodeInTree(blockReferences, selectedItems, tn);

                            // disabled node if setCurrent is enabled
                            if (IsEditAssembly && CurrentLevelAssembly > tn.Level)
                            {
                                //set frozen status if not set yet
                                if (!IsFrozen(tn))
                                    FrozeNode(tn);
                            }
                        }
                        else
                        {
                            // restore level during closeAssembly
                            if(IsEditAssembly && ReferenceEquals(br, Model.Entities.CurrentBlockReference))
                                CurrentLevelAssembly = tn.Level;

                            UnFrozeNode(tn);
                            if (selectedItems != null && selectedItems.Count > 0)
                            {
                                foreach (Environment.SelectedItem selectedEntity in selectedItems)
                                {
                                    foreach (TreeNode childNode in tn.Nodes)
                                    {
                                        UnFrozeNode(childNode);
                                        NodeTag nodeTag = (NodeTag) childNode.Tag;

                                        if (ReferenceEquals(selectedEntity.Item, nodeTag.Entity))
                                        {
                                            selectedNodes.Add(childNode);
                                        }
                                    }
                                }
                                if(selectedNodes.Count > 0)
                                    selectedNodes[0].EnsureVisible();
                            }
                            else
                            {
                                selectedNodes.Add(tn);
                                tn.EnsureVisible();
                            }
                            this.SelectedNodes = selectedNodes;
                        }
                    }
                    else
                    {
                        if(tn.IsExpanded)
                            tn.Collapse();

                        // disabled node if setCurrent is enabled
                        if (IsEditAssembly && CurrentLevelAssembly >= tn.Level)
                        {
                            //set frozen status if not set yet
                            if (!IsFrozen(tn))
                                FrozeNode(tn);
                        }
                    }
                }
            }
            else
            {
                // Root level

                if (selectedItems != null && selectedItems.Count > 0)
                {
                    foreach (Environment.SelectedItem selectedEntity in selectedItems)
                    {
                        foreach (TreeNode childNode in tnc)
                        {
                            if (ReferenceEquals(selectedEntity.Item, ((NodeTag) childNode.Tag).Entity))
                            {
                                selectedNodes.Add(childNode);
                                break;
                            }
                        }
                    }
                    if(selectedNodes.Count > 0)
                        selectedNodes[0].EnsureVisible();
                    this.SelectedNodes = selectedNodes;
                }

            }
        }

        /// <summary>
        /// Deletes all the tree nodes that are referring to the same entity instance
        /// </summary>
        /// <param name="entity">The entity instance.</param>
        private void DeleteNodes(TreeNode tn, Entity entity)
        {
            int count = tn.Nodes.Count;
            while (count > 0)
            {
                count--;
                TreeNode node = tn.Nodes[count];
                if (node.Tag != null && ReferenceEquals(entity, ((NodeTag)node.Tag).Entity))
                {
                    node.Remove();
                    count = -1;
                }
                else
                {
                    DeleteNodes(node, entity);
                }
            }
        }

        /// <summary>
        /// Deletes the selected tree node and all the others nodes that are linked to the same entity instance.
        /// </summary>
        public void DeleteSelectedNodes()
        {
            if (SelectedNodes != null && SelectedNodes.Count > 0)
            {
                BeginUpdate();
                foreach (TreeNode selectedNode in SelectedNodes)
                {
                    if (selectedNode.Tag != null)
                    {
                        Entity deletedEntity = (selectedNode.Tag as NodeTag).Entity;
                        // starts from the first node "Entities"
                        DeleteNodes(this.Nodes[0], deletedEntity);
                    }
                }
                EndUpdate();
                SelectedNodes.Clear();
            }
        }
        #endregion

        #region TreeView events
        [DllImport("user32.dll")]
        private static extern bool LockWindowUpdate(IntPtr hWndLock);
        protected override void WndProc(ref Message messg)
        {
            // turn the REDRAW background message into a null message, because
            // the PAINT message is always call after it cause a double useless repaint

            //if message is SETREDRAW
            if ((int) 11 == messg.Msg || 20 == messg.Msg)
            {
                messg.Msg = (int) 0x0000; //reset message to null
            }

            base.WndProc(ref messg);
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            if (e.Bounds.IsEmpty || !e.Node.IsVisible)
                return;

            // avoids to draw nodes in collapsed parents (not visible)
            if (e.Node.Parent != null && !e.Node.Parent.IsExpanded)
                return;
            
            base.OnDrawNode(e);
            
            int leftPos = e.Bounds.Left;

            // if it's selected, draw it in selection mode
            if (((MultiSelectTreeView)e.Node.TreeView).SelectedNodes.Contains(e.Node))
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), leftPos, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawString(e.Node.Text, this.Font, new SolidBrush(SystemColors.HighlightText), leftPos, e.Bounds.Top);
            }
            else
            {
                e.DrawDefault = true;
            }
            
            if (ImageList != null)
            {
                // Images Component/Part/Entities
                // since icon of ImageList are semi-transparent, clear it before redrawn it
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), leftPos - 18, e.Bounds.Y, 16, e.Bounds.Height);
                string key = e.Node.ImageKey;
                System.Drawing.Image image = ImageList.Images[key];
                e.Graphics.DrawImage(image, leftPos - 18, e.Bounds.Top);

                // Images Not-Visible & Locked
                if (e.Node.Tag != null)
                {
                    SizeF size = e.Graphics.MeasureString(e.Node.Text, this.Font);
                    leftPos += (int) size.Width;
                    // since icons are semi-transparent, clear them before redrawn it
                    e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), leftPos, e.Bounds.Y, 32, e.Bounds.Height);

                    AssemblyTreeView.NodeTag nodeTag = ((AssemblyTreeView.NodeTag) e.Node.Tag);
                    // draws non-visible icon image
                    if (!nodeTag.Entity.GetVisibility(nodeTag.Parents))
                    {
                        e.Graphics.DrawImage(ImageList.Images["NoVis"], leftPos, e.Bounds.Top);
                        leftPos += 16;
                    }
                    
                    // draws non-selectable icon image
                    if (!nodeTag.Entity.GetSelectability(nodeTag.Parents))
                        e.Graphics.DrawImage(ImageList.Images["NoSel"], leftPos, e.Bounds.Top);

                }
            }
        }

        /// <summary>
        /// Checks if the node is frozen before to perform any action.
        /// </summary>
        /// <returns>True if the event was canceled.</returns>
        private bool CancelTreeViewEvents(TreeViewCancelEventArgs e)
        {
            if (TreeModify)
                return false;

            // ensures to get the node under the mouse
            TreeNode node = e.Node;
            if (SelectedNode != null && SelectedNode.Tag != null && node.Tag == null)
            {
                node = SelectedNode;
            }

            bool frozen = IsFrozen(node);
            if (node.Tag == null || frozen)
            {
                // disables the tree node by removing the selection 
                SelectedNode = null;

                if (IsEditAssembly)
                {
                    //cancels the event and changes its color
                    e.Cancel = true;
                    return true;
                }
            }

            return false;
        }
        protected override void OnBeforeCheck(TreeViewCancelEventArgs e)
        {
            base.OnBeforeCheck(e);
            CancelTreeViewEvents(e);
        }

        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            base.OnBeforeExpand(e);
            if (!CancelTreeViewEvents(e))
            {
                NodeTag tagToExpand = (NodeTag) e.Node.Tag;
                if (tagToExpand != null && e.Node.Nodes[0].Tag == null)
                {
                    PopulateTree(Model.Blocks[((BlockReference) tagToExpand.Entity).BlockName].Entities, e.Node);
                }
            }
        }
        
        protected override void OnBeforeCollapse(TreeViewCancelEventArgs e)
        {
            base.OnBeforeCollapse(e);
            if (!CancelTreeViewEvents(e) && e.Node.Tag != null)
            {
                TreeModify = true;
                BeginUpdate();
                // blocks the redrawing of all the elements of the treeview (like the scroolbar)
                // to speed up the node.Clear() (BeginUpdate() was not enough)
                LockWindowUpdate(this.Handle);

                e.Node.Nodes.Clear();
                e.Node.Nodes.Add("child");
                
                LockWindowUpdate(IntPtr.Zero);
                EndUpdate();
                TreeModify = false;
            }
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            if (TreeModify)
            {
                return;
            }

            TreeModify = true;
            
            // gets selected items from the treeView selected nodes and update the selection on the scene
            SynchTreeSelection();

            Model.Invalidate();
            
            TreeModify = false;

            base.OnAfterSelect(e);
        }
        #endregion
        
        #region Context Menu
        
        System.Windows.Forms.ToolStripMenuItem openAssembly;
        System.Windows.Forms.ToolStripMenuItem closeAssembly;
        System.Windows.Forms.ToolStripMenuItem editAssembly;
        System.Windows.Forms.ToolStripMenuItem openPart;
        System.Windows.Forms.ToolStripMenuItem editPart;
        System.Windows.Forms.ToolStripMenuItem editParent;
        System.Windows.Forms.ToolStripMenuItem exit;
        System.Windows.Forms.ToolStripMenuItem hide;
        System.Windows.Forms.ToolStripMenuItem show;
        System.Windows.Forms.ToolStripMenuItem clearVisibility;
        System.Windows.Forms.ToolStripMenuItem lockmenuItem;
        System.Windows.Forms.ToolStripMenuItem unlock;
        System.Windows.Forms.ToolStripMenuItem clearLockStatus;
        System.Windows.Forms.ToolStripMenuItem makeIndependent;
        System.Windows.Forms.ToolStripMenuItem cut;
        System.Windows.Forms.ToolStripMenuItem paste;
        System.Windows.Forms.ToolStripMenuItem dissolve;
        System.Windows.Forms.ToolStripMenuItem formNew;
        System.Windows.Forms.ToolStripMenuItem moveWithTriad;
        System.Windows.Forms.ToolStripMenuItem apply;
        System.Windows.Forms.ToolStripMenuItem cancel;

        List<ToolStripItem> treeViewItemsCollection = new List<ToolStripItem>();
#if WPF
        System.Windows.Controls.MenuItem openAssemblyM;
        System.Windows.Controls.MenuItem closeAssemblyM;
        System.Windows.Controls.MenuItem editAssemblyM;
        System.Windows.Controls.MenuItem openPartM;
        System.Windows.Controls.MenuItem editPartM;
        System.Windows.Controls.MenuItem editParentM;
        System.Windows.Controls.MenuItem exitM;
        System.Windows.Controls.MenuItem hideM;
        System.Windows.Controls.MenuItem showM;
        System.Windows.Controls.MenuItem clearVisibilityM;
        System.Windows.Controls.MenuItem lockmenuItemM;
        System.Windows.Controls.MenuItem unlockM;
        System.Windows.Controls.MenuItem clearLockStatusM;
        System.Windows.Controls.MenuItem makeIndipendentM;
        System.Windows.Controls.MenuItem cutM;
        System.Windows.Controls.MenuItem pasteM;
        System.Windows.Controls.MenuItem dissolveM;
        System.Windows.Controls.MenuItem formNewM;
        System.Windows.Controls.MenuItem moveWithTriadM;
        System.Windows.Controls.MenuItem applyM;
        System.Windows.Controls.MenuItem cancelM;        

        List<System.Windows.FrameworkElement> modelItemsCollection = new List<System.Windows.FrameworkElement>();
        Dictionary<System.Windows.Forms.ToolStripMenuItem, System.Windows.Controls.MenuItem> menuItemsPair = new Dictionary<ToolStripMenuItem, System.Windows.Controls.MenuItem>();
#else
        List<System.Windows.Forms.ToolStripItem> modelItemsCollection = new List<System.Windows.Forms.ToolStripItem>();
#endif

        /// <summary>
        /// Inizialize data for treeView.ContextMenu and Model.ContextMenu.
        /// </summary>
        public void InitializeContextMenu()
        {
            // The most of ToolStripMenuItems are in common with both contextMenu.
            // To exploit this the contextMenu.Items list is fill only during the ContextMenu opening and empty at the closure.
            openAssembly = new ToolStripMenuItem("Open Assembly", null, OpenAssemblyOnClick);
            closeAssembly = new ToolStripMenuItem("Close Assembly", null, CloseAssemblyOnClick);
            editAssembly = new ToolStripMenuItem("Edit Assembly", null, EditAssemblyOnClick);
            openPart = new ToolStripMenuItem("Open Part", null, OpenAssemblyOnClick);
            editPart = new ToolStripMenuItem("Edit Part", null, EditAssemblyOnClick);
            editParent = new ToolStripMenuItem("Edit Parent", null, EditParentOnClick);
            exit = new ToolStripMenuItem("Exit", null, ExitAssemblyOnClick);
            hide = new ToolStripMenuItem("Hide", null, ChangeVisibilityOnClick);
            show = new ToolStripMenuItem("Show", null, ChangeVisibilityOnClick) {Enabled = false};
            clearVisibility = new ToolStripMenuItem("Clear instance visibility", null, ClearVisibilityOnClick);
            lockmenuItem = new ToolStripMenuItem("Lock", null, ChangeSelectabilityOnClick);
            unlock = new ToolStripMenuItem("Unlock", null, ChangeSelectabilityOnClick) {Enabled = false};
            clearLockStatus = new ToolStripMenuItem("Clear instance lock status", null, ClearSelectablilityOnClick);
            makeIndependent = new ToolStripMenuItem("Make Independent", null, MakeIndependentOnClick);
            cut = new ToolStripMenuItem("Cut (Edit structure)", null, EditStructureOnClick);
            paste = new ToolStripMenuItem("Paste (Edit structure)", null, PasteOnClick) {Enabled = false};
            dissolve = new ToolStripMenuItem("Dissolve Subassembly", null, DissolveOnClick);
            formNew = new ToolStripMenuItem("Form New Subassembly", null, FormNewOnClick);
            moveWithTriad = new ToolStripMenuItem("Move with Triad", null, MoveWithTriadOnClick);
            apply = new ToolStripMenuItem("Apply", null, ApplyOnClick) {Enabled = false};
            cancel = new ToolStripMenuItem("Cancel", null, CancelOnClick) {Enabled = false};
#if WPF
            openAssemblyM = new System.Windows.Controls.MenuItem() { Header = "Open Assembly" }; openAssemblyM.Click += OpenAssemblyOnClick;
            closeAssemblyM = new System.Windows.Controls.MenuItem() { Header = "Close Assembly" }; closeAssemblyM.Click += CloseAssemblyOnClick;
            editAssemblyM = new System.Windows.Controls.MenuItem() { Header = "Edit Assembly" }; editAssemblyM.Click += EditAssemblyOnClick;
            openPartM = new System.Windows.Controls.MenuItem() { Header = "Open Part" }; openPartM.Click += OpenAssemblyOnClick;
            editPartM = new System.Windows.Controls.MenuItem() { Header = "Edit Part" }; editPartM.Click += EditAssemblyOnClick;
            editParentM = new System.Windows.Controls.MenuItem() { Header = "Edit Parent" }; editParentM.Click += EditParentOnClick;
            exitM = new System.Windows.Controls.MenuItem() { Header = "Exit" }; exitM.Click += ExitAssemblyOnClick;
            hideM = new System.Windows.Controls.MenuItem() { Header = "Hide" }; hideM.Click += ChangeVisibilityOnClick;
            showM = new System.Windows.Controls.MenuItem() { Header = "Show", IsEnabled = false }; showM.Click += ChangeVisibilityOnClick;
            clearVisibilityM = new System.Windows.Controls.MenuItem() { Header = "Clear instance visibility" }; clearVisibilityM.Click += ClearVisibilityOnClick;
            lockmenuItemM = new System.Windows.Controls.MenuItem() { Header = "Lock" }; lockmenuItemM.Click += ChangeSelectabilityOnClick;
            unlockM = new System.Windows.Controls.MenuItem() { Header = "Unlock", IsEnabled = false }; unlockM.Click += ChangeSelectabilityOnClick;
            clearLockStatusM = new System.Windows.Controls.MenuItem() { Header = "Clear instance lock status" }; clearLockStatusM.Click += ClearSelectablilityOnClick;
            makeIndipendentM = new System.Windows.Controls.MenuItem() { Header = "Make Independent" }; makeIndipendentM.Click += MakeIndependentOnClick;
            cutM = new System.Windows.Controls.MenuItem() { Header = "Cut (Edit structure)" }; cutM.Click += EditStructureOnClick;
            pasteM = new System.Windows.Controls.MenuItem() { Header = "Paste (Edit structure)", IsEnabled = false }; pasteM.Click += PasteOnClick;
            dissolveM = new System.Windows.Controls.MenuItem() { Header = "Dissolve Subassembly" }; dissolveM.Click += DissolveOnClick;
            formNewM = new System.Windows.Controls.MenuItem() { Header = "Form New Subassembly" }; formNewM.Click += FormNewOnClick;
            moveWithTriadM = new System.Windows.Controls.MenuItem() { Header = "Move with Triad" }; moveWithTriadM.Click += MoveWithTriadOnClick;
            applyM = new System.Windows.Controls.MenuItem() { Header = "Apply", IsEnabled = false }; applyM.Click += ApplyOnClick;
            cancelM = new System.Windows.Controls.MenuItem() { Header = "Cancel", IsEnabled = false }; cancelM.Click += CancelOnClick;
            
            menuItemsPair.Add(openAssembly, openAssemblyM);
            menuItemsPair.Add(closeAssembly, closeAssemblyM);
            menuItemsPair.Add(editAssembly, editAssemblyM);
            menuItemsPair.Add(openPart, openPartM);
            menuItemsPair.Add(editPart, editPartM);
            menuItemsPair.Add(editParent, editParentM);
            menuItemsPair.Add(exit, exitM);
            menuItemsPair.Add(hide, hideM);
            menuItemsPair.Add(show, showM);
            menuItemsPair.Add(clearVisibility, clearVisibilityM);
            menuItemsPair.Add(lockmenuItem, lockmenuItemM);
            menuItemsPair.Add(unlock, unlockM);
            menuItemsPair.Add(clearLockStatus, clearLockStatusM);
            menuItemsPair.Add(makeIndependent, makeIndipendentM);
            menuItemsPair.Add(cut, cutM);
            menuItemsPair.Add(paste, pasteM);
            menuItemsPair.Add(dissolve, dissolveM);
            menuItemsPair.Add(formNew, formNewM);
            menuItemsPair.Add(moveWithTriad, moveWithTriadM);
            menuItemsPair.Add(apply, applyM);
            menuItemsPair.Add(cancel, cancelM);            
#endif

            if (ContextMenuStrip == null)
            {
                ContextMenuStrip = new ContextMenuStrip();
                ContextMenuStrip.Opening += ContextMenuStripOnOpening;
                ContextMenuStrip.Closing += ContextMenuStripOnClosing;
            }

            treeViewItemsCollection.Add(openAssembly);
            treeViewItemsCollection.Add(closeAssembly);
            treeViewItemsCollection.Add(editAssembly);
            treeViewItemsCollection.Add(openPart);
            treeViewItemsCollection.Add(editPart);
            treeViewItemsCollection.Add(editParent);
            treeViewItemsCollection.Add(exit);
            treeViewItemsCollection.Add(new ToolStripSeparator());
            treeViewItemsCollection.Add(hide);
            treeViewItemsCollection.Add(show);
            treeViewItemsCollection.Add(clearVisibility);
            treeViewItemsCollection.Add(lockmenuItem);
            treeViewItemsCollection.Add(unlock);
            treeViewItemsCollection.Add(clearLockStatus);
            treeViewItemsCollection.Add(new ToolStripSeparator());
            treeViewItemsCollection.Add(makeIndependent);
            treeViewItemsCollection.Add(cut);
            treeViewItemsCollection.Add(paste);
            treeViewItemsCollection.Add(dissolve);
            treeViewItemsCollection.Add(formNew);

#if WPF
            if (Model.ContextMenu == null)
            {                
                Model.ContextMenu = new System.Windows.Controls.ContextMenu();                
                Model.ContextMenuOpening += ContextMenuOnOpening;
                Model.ContextMenuClosing += ContextMenuOnClosing;
            }

                        
            modelItemsCollection.Add(openAssemblyM);
            modelItemsCollection.Add(closeAssemblyM);
            modelItemsCollection.Add(editAssemblyM);
            modelItemsCollection.Add(openPartM);
            modelItemsCollection.Add(editPartM);
            modelItemsCollection.Add(editParentM);
            modelItemsCollection.Add(exitM);
            modelItemsCollection.Add(new Separator());
            modelItemsCollection.Add(hideM);
            modelItemsCollection.Add(showM);
            modelItemsCollection.Add(clearVisibilityM);
            modelItemsCollection.Add(lockmenuItemM);
            modelItemsCollection.Add(unlockM);
            modelItemsCollection.Add(clearLockStatusM);
            modelItemsCollection.Add(new Separator());
            modelItemsCollection.Add(makeIndipendentM);
            modelItemsCollection.Add(dissolveM);
            modelItemsCollection.Add(formNewM);
            modelItemsCollection.Add(new Separator());
            modelItemsCollection.Add(moveWithTriadM);
            modelItemsCollection.Add(applyM);
            modelItemsCollection.Add(cancelM);
#else
            if (Model.ContextMenuStrip == null)
            {
                ContextMenuStrip contextMenuStripModel = new ContextMenuStrip();
                contextMenuStripModel.Opening += ContextMenuStripOnOpening;
                contextMenuStripModel.Closing += ContextMenuStripOnClosing;
                Model.ContextMenuStrip = contextMenuStripModel;
            }

            modelItemsCollection.Add(openAssembly);
            modelItemsCollection.Add(closeAssembly);
            modelItemsCollection.Add(editAssembly);
            modelItemsCollection.Add(openPart);
            modelItemsCollection.Add(editPart);
            modelItemsCollection.Add(editParent);
            modelItemsCollection.Add(exit);
            modelItemsCollection.Add(new ToolStripSeparator());
            modelItemsCollection.Add(hide);
            modelItemsCollection.Add(show);
            modelItemsCollection.Add(clearVisibility);
            modelItemsCollection.Add(lockmenuItem);
            modelItemsCollection.Add(unlock);
            modelItemsCollection.Add(clearLockStatus);
            modelItemsCollection.Add(new ToolStripSeparator());
            modelItemsCollection.Add(makeIndependent);
            modelItemsCollection.Add(dissolve);
            modelItemsCollection.Add(formNew);
            modelItemsCollection.Add(new ToolStripSeparator());
            modelItemsCollection.Add(moveWithTriad);
            modelItemsCollection.Add(apply);
            modelItemsCollection.Add(cancel);
#endif            
        }

#if WPF
        /// <summary>
        /// Synchronize model context menu items with the treeview one.
        /// </summary>
        private void SyncMenuItems()
        {
            foreach (var pair in menuItemsPair)
            {
                System.Windows.Controls.MenuItem m = pair.Value;
                ToolStripMenuItem tm = pair.Key;

                m.Header = tm.Text;
                m.IsEnabled = tm.Enabled;                

            }            
        }


        private void ContextMenuOnClosing(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {
            ContextMenuStripOnClosing(sender, new CancelEventArgs());
        }

        private void ContextMenuOnOpening(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {
            ContextMenuStripOnOpening(sender, new CancelEventArgs());
        }
#endif
        private void ContextMenuStripOnOpening(object sender, CancelEventArgs e)
        {
            // set to false to avoid the default behavior:
            // for empty menu it's set to true by default and the menu automatically close at first click
            e.Cancel = false;

            //fills only the opening contextMenuStrip of the right control with the needed menuItems
            if (ReferenceEquals(sender, ContextMenuStrip))
            {
                ContextMenuStrip.Items.AddRange(treeViewItemsCollection.ToArray());
            }
            else
            {
#if WPF
                foreach (var item in modelItemsCollection)
                {
                    Model.ContextMenu.Items.Add(item);
                }                
#else
                Model.ContextMenuStrip.Items.AddRange(modelItemsCollection.ToArray());
#endif
            }
            
            bool isOpenCurrent = Model.Entities.IsOpenCurrentBlockReference;
            if (IsEditAssembly)
            {
                if(Model.Entities.Parents.Count > 1)                
                    editParent.Text = "Edit Parent: " + Model.Entities.Parents.ToList()[1].BlockName;                    
                if (isOpenCurrent)
                {
                    closeAssembly.Text = "Close Assembly: " + Model.Entities.CurrentBlockReference.BlockName;
                    editAssembly.Text = "Edit Assembly (limitation)";
                    editPart.Text = "Edit Part (limitation)";
                    editParent.Text = "Edit Parent (limitation)";
                }
            }
            
            // if it's not the root "Entities" node and it is not frozen, checks what to show in the context menu
            if (SelectedNode != null && SelectedNode.Tag != null && SelectedNode.Level >= CurrentLevelAssembly &&
                !IsFrozen(SelectedNode))
            {
                // Edit/Open/Exit assembly menu items
                NodeTag nodeTag = SelectedNode.Tag as NodeTag;
                bool isRoot = SelectedNode.Level == CurrentLevelAssembly;
                if (!IsMoving)
                {
                    bool isComponent = nodeTag.Entity is BlockReference;

                    openAssembly.Enabled = isComponent;
                    closeAssembly.Enabled = isOpenCurrent;
                    editAssembly.Enabled = isComponent && !isOpenCurrent  && !isRoot;
                    openPart.Enabled = !isComponent  && !isRoot;
                    editPart.Enabled = !isComponent && !isOpenCurrent  && !isRoot;

                    exit.Enabled = IsEditAssembly;
                    editParent.Enabled = IsEditAssembly && !isOpenCurrent;
                }
                else
                {
                    openAssembly.Enabled = false;
                    closeAssembly.Enabled = false;
                    editAssembly.Enabled = false;
                    openPart.Enabled = false;
                    editPart.Enabled = false;

                    exit.Enabled = false;
                    editParent.Enabled = false;
                }

                // blocks the following actions for current root entity and during move with triad
                if (IsMoving || isRoot)
                {
                    // hide/show lock/unlock menu items
                    hide.Enabled = false;
                    show.Enabled = false;
                    clearVisibility.Enabled = false;
                    lockmenuItem.Enabled = false;
                    unlock.Enabled = false;
                    clearLockStatus.Enabled = false;

                    // custom features
                    cut.Enabled = false;
                    makeIndependent.Enabled = false;
                    dissolve.Enabled = false;
                    formNew.Enabled = false;
                }
                else
                {
                    // hide/show lock/unlock menu items
                    bool visible = nodeTag.Entity.GetVisibility(nodeTag.Parents);
                    hide.Enabled = visible;
                    show.Enabled = !visible;

                    bool selectability = nodeTag.Entity.GetSelectability(nodeTag.Parents);
                    lockmenuItem.Enabled = selectability;
                    unlock.Enabled = !selectability;
                }

                // modify assembly menu items
                paste.Enabled = _editStructureNodes.Count > 0;
                
                // move with triad menu items
                moveWithTriad.Enabled = !IsMoving;
                apply.Enabled = cancel.Enabled = IsMoving;
            }
            else
            {
                if (sender is devDept.Eyeshot.Model)
                {
#if WPF
                    SetAllMenuItemsEnableProperty(Model.ContextMenu, false);
#else
                SetAllMenuItemsEnableProperty(Model.ContextMenuStrip, false);
#endif
                }
                else
                    SetAllMenuItemsEnableProperty((ContextMenuStrip)sender, false);

                // the menuItems is not available for these options 
                if (IsEditAssembly)
                {
                    exit.Enabled = true;
                    editParent.Enabled = !isOpenCurrent;
                    closeAssembly.Enabled = isOpenCurrent;
                }
                else if(_editStructureNodes != null && _editStructureNodes.Count > 0)
                {
                    paste.Enabled = true;
                }
            }

#if WPF
            SyncMenuItems();
#endif
        }
        
        private void ContextMenuStripOnClosing(object sender, CancelEventArgs e)
        {
            if (sender is devDept.Eyeshot.Model)
            {                
#if WPF
                SetAllMenuItemsEnableProperty(Model.ContextMenu, true);
#else
                SetAllMenuItemsEnableProperty(Model.ContextMenuStrip, true);
#endif           
            }
            else
            {
                ContextMenuStrip menu = (ContextMenuStrip)sender;
                SetAllMenuItemsEnableProperty(menu, true);
                menu.Enabled = true;
            }
            
            
            // restores default values
            editAssembly.Text = "Edit Assembly";
            editPart.Text = "Edit Part";
            editParent.Text = "Edit Parent";
            closeAssembly.Text = "Close Assembly";

            ContextMenuStrip.Items.Clear();
#if WPF
            Model.ContextMenu.Items.Clear();
            SyncMenuItems();
#else
            Model.ContextMenuStrip.Items.Clear();
#endif
        }

        private static void SetAllMenuItemsEnableProperty(ContextMenuStrip menu, bool enableStatus)
        {
            foreach (ToolStripItem tItem in menu.Items)
            {
                tItem.Enabled = enableStatus;
            }
        }
#if WPF
        private static void SetAllMenuItemsEnableProperty(System.Windows.Controls.ContextMenu menu, bool enableStatus)
        {
            foreach (var tItem in menu.Items)
            {
                var menuItem = tItem as System.Windows.Controls.MenuItem;
                if (menuItem != null)
                    menuItem.IsEnabled = enableStatus;
            }
        }
#endif

#region assembly operations
        private void OpenAssemblyOnClick(object sender, EventArgs e)
        {
            if (SelectedNodes.Count > 0 && SelectedNodes[0].Tag != null)
            {
                NodeTag nodeTag = ((NodeTag)SelectedNodes[0].Tag);
                if ((SelectedNodes[0].Level > 1 || nodeTag.Entity is BlockReference) &&
                    (CurrentLevelAssembly < SelectedNodes[0].Level ||
                     ReferenceEquals(nodeTag.Entity, Model.Entities.CurrentBlockReference)))                     
                {
                    IsEditAssembly = true;
                    CurrentLevelAssembly = 0; // in open context the level is always root

                    // push selected br in the parent stack if not already present
                    BlockReference br = nodeTag.Entity as BlockReference;
                    if (br != null && !ReferenceEquals(br, Model.Entities.CurrentBlockReference))
                    {
                        SelectedItems[0].Parents.Push(br);
                    }

                    // set the new parent stack before to open it
                    if (SelectedItems[0].Parents.Count != 0)
                        Model.Entities.SetCurrentStack(SelectedItems[0].Parents);

                    if (!Model.Entities.IsOpenCurrentBlockReference)
                    {
                        // to avoid to call zoomFit to improve performances
                        Model.SaveView(out prevView);

                        Model.Entities.OpenCurrentBlockReference();                        
                    }

                    // clears selection data and rebuild the tree to show only the opened block hierarchy
                    ClearTreeSelection();
                    PopulateTree(Model.Entities);

                    Model.SetView(viewType.Isometric);
                    Model.ZoomFit();
                    Model.Invalidate();
                }
            }
        }
        
        private void CloseAssemblyOnClick(object sender, EventArgs e)
        {
            if (Model.Entities.IsOpenCurrentBlockReference)
            {
                Stack<BlockReference> parents = new Stack<BlockReference>(Model.Entities.Parents);

                // close the block and cleans parents without reset internal flags to be able to rebuild the treeView with also the nodes of the "frozen geometry"
                ClearCurrent(false);
                PopulateTree(Model.Entities);
                
                // restore previous SetCurrent context 
                Model.Entities.SetCurrentStack(new Stack<BlockReference>(parents));
                
                // restore level assembly of the entire tree
                CurrentLevelAssembly = Model.Entities.Parents.Count;
                
                SelectedItems.Add(new Environment.SelectedItem(Model.Entities.CurrentBlockReference));
                
                SynchScreenSelection(parents);
                
                Model.Invalidate();
            }
        }
        private void EditAssemblyOnClick(object sender, EventArgs e)
        {
            if (Model != null && SelectedNodes.Count > 0 && CurrentLevelAssembly < SelectedNodes[0].Level && !ReferenceEquals(((NodeTag)SelectedNodes[0].Tag).Entity, Model.Entities.CurrentBlockReference))
            {
                IsEditAssembly = true;
                CurrentLevelAssembly = SelectedNodes[0].Level;

                Stack<BlockReference> parents = SelectedItems[0].Parents;
                if (parents == null)
                    parents = new Stack<BlockReference>();

                // push selected br in the parent stack
                BlockReference br = ((NodeTag)SelectedNodes[0].Tag).Entity as BlockReference;
                if (br != null)
                    parents.Push(br);
                
                if (parents.Count > 0 && Model.Entities.Contains(parents.Last()))
                    Model.Entities.SetCurrentStack(parents);

                SelectedItems.Clear();

                SynchScreenSelection(new Stack<BlockReference>(Model.Entities.Parents));
                Model.Invalidate();
            }
        }

        private void EditParentOnClick(object sender, EventArgs e)
        {
            Environment.SelectedItem current = new Environment.SelectedItem(null, Model.Entities.CurrentBlockReference);
            
            if (Model.Entities.Parents!= null && Model.Entities.Parents.Count == 1)
            {
                ExitAssemblyOnClick(sender, e);
                return;
            }
            // otherwise comes back up to one level
            if(CurrentLevelAssembly > 1)
                CurrentLevelAssembly--;
            
            Model.Entities.SetParentAsCurrent();
            
            // set the previous currentBlockReference as selected
            SelectedItems.Clear();
            SelectedItems.Add(current);
            current.Select(Model, true);

            if (Model.Entities.IsOpenCurrentBlockReference)
            {
                PopulateTree(Model.Entities);
            }

            SynchScreenSelection(new Stack<BlockReference>(Model.Entities.Parents));
            
            Model.Invalidate();
        }

        private void ExitAssemblyOnClick(object sender, EventArgs e)
        {
            ClearCurrent(true);

            //restores the original tree
            PopulateTree(Model.Entities);
            
            Model.Invalidate();
        }
        
        /// <summary>
        /// Clear selection and Edit/Open Assembly for entities to avoid problems with the Tree->Screen synchronization
        /// </summary>
        /// <param name="exit"> need to be true if the <see cref="ExitAssemblyOnClick"/> is called.</param>
        public void ClearCurrent(bool exit)
        {             
            SelectedItems.Clear();
            SelectedNodes.Clear();
            Model.Entities.ClearSelection();

            if (Model.Entities.IsOpenCurrentBlockReference)
            {
                Model.Entities.CloseCurrentBlockReference();
                
                // avoids to call zoomFit to improve performances
                if (prevView != null)
                {
                    Model.RestoreView(prevView);
                    prevView = null;
                }
            }
            
            if (Model.Entities.CurrentBlockReference != null)            
                Model.Entities.SetCurrent(null);

            // if an ExitAssembly action was not called then we clean the parents but remaining into EditAssembly context
            if (exit)
            {
                IsEditAssembly = false;
                CurrentLevelAssembly = 0;
            }
        }

        /// <summary>
        /// Clear the collection and flags of the Control.
        /// </summary>
        public void ClearTree()
        {
            ClearTreeSelection();
            Nodes.Clear();

            IsEditAssembly = false;
            CurrentLevelAssembly = 0;
            IsMoving = false;
            TreeModify = false;
            _editStructureNodes.Clear();
            _editStructureTransformation = null;
        }

        public void ClearTreeSelection()
        {
            SelectedItems.Clear();
            ClearSelectedNodes();
        }
        #endregion

#region visibility/selectability
        private  void ChangeVisibilityOnClick(object sender, EventArgs eventArgs)
        {
            if (SelectedNodes.Count > 0)
            {
                foreach (TreeNode node in SelectedNodes)
                {
                    //changes the visibility of the current instance associated with this parents stack
                    NodeTag nodeTag = (NodeTag) node.Tag;
                    bool visibility = nodeTag.Entity.GetVisibility(nodeTag.Parents);
                    nodeTag.Entity.SetVisibility(!visibility, nodeTag.Parents);
                }

                ClearTreeSelection();
                Model.Entities.UpdateBoundingBox();
                Model.UpdateVisibleSelection();

                Model.Invalidate();
                Invalidate();
            }
        }
        private  void ClearVisibilityOnClick(object sender, EventArgs eventArgs)
        {
            if (SelectedNodes.Count > 0)
            {
                foreach (TreeNode node in SelectedNodes)
                {
                    NodeTag nodeTag = (NodeTag) node.Tag;
                    nodeTag.Entity.ClearVisibility(nodeTag.Parents);
                }

                Model.Entities.UpdateBoundingBox();
                Model.UpdateVisibleSelection();

                Model.Invalidate();
                Invalidate();
            }
        }
        private void ChangeSelectabilityOnClick(object sender, EventArgs eventArgs)
        {
            if (SelectedNodes.Count > 0)
            {
                foreach (TreeNode node in SelectedNodes)
                {
                    NodeTag nodeTag = (NodeTag) node.Tag;
                    bool selectable = nodeTag.Entity.GetSelectability(nodeTag.Parents);
                    nodeTag.Entity.SetSelectability(!selectable, nodeTag.Parents);

                    nodeTag.Entity.SetSelection(false);
                }

                ClearTreeSelection();
                Model.UpdateVisibleSelection();
                Model.Invalidate();
                Invalidate();
            }
        }
        private void ClearSelectablilityOnClick(object sender, EventArgs eventArgs)
        {
            if (SelectedNodes.Count > 0)
            {
                foreach (TreeNode node in SelectedNodes)
                {
                    NodeTag nodeTag = (NodeTag) node.Tag;
                    nodeTag.Entity.ClearSelectability(nodeTag.Parents);
                }

                Model.UpdateVisibleSelection();
                Model.Invalidate();
                Invalidate();
            }
        }
#endregion

#region custom functionality
        private void MakeIndependentOnClick(object sender, EventArgs e)
        {
            if (Model != null && SelectedItems[0] != null)
            {
                BlockReference br = SelectedItems[0].Item as BlockReference;
                if (br != null)
                {
                    Stack<BlockReference> parents = new Stack<BlockReference>(Model.Entities.Parents);

                    MakeIndependent(br);
                    
                    // update only the needed branch of the treeView
                    if (IsEditAssembly && !Model.Entities.IsOpenCurrentBlockReference)
                    {
                        // find node of current blockreference
                        TreeNode parentTn = SelectedNodes[0];
                        while (parentTn.Tag != null && !ReferenceEquals(((NodeTag) parentTn.Tag).Entity,
                                   Model.Entities.CurrentBlockReference))                                   
                        {
                            parentTn = parentTn.Parent;
                        }
                        PopulateTree(Model.Entities, parentTn);
                    }

                    SynchScreenSelection(parents);
                    Model.Invalidate();
                }
            }
        }

        private void FormNewOnClick(object sender, EventArgs e)
        {
            if (Model != null && SelectedNodes != null)
            {
                List<Entity> brList = new List<Entity>();
                foreach (TreeNode node in SelectedNodes)
                {
                    Entity br = ((NodeTag)node.Tag).Entity ;
                    if (br != null)
                        brList.Add(br);
                }

                if (brList.Count > 0)
                {
                    string newBlockName = UtilityEx.GetUnusedBlockName("NewBlock", Model.Blocks, true);
                    string parentBlockName = null;
                    TreeNode parentNode = SelectedNodes[0].Parent;
                    NodeTag parentTag = (NodeTag)parentNode.Tag;
                    if (parentTag != null)
                    {
                        BlockReference parent = ((NodeTag) SelectedNode.Parent.Tag).Entity as BlockReference;
                        parentBlockName = parent.BlockName;
                        newBlockName = UtilityEx.GetUnusedBlockName(parentBlockName, Model.Blocks, true);
                    }
                    
                    FormNew(brList, newBlockName);

                    ClearTreeSelection();

                    // set new parent as selected 
                    if (parentTag != null)
                    {
                        // finds the new parent BlockReference
                        BlockReference newParent = null;
                        foreach (Entity ent in Model.Blocks[parentBlockName].Entities)
                        {
                            if (ent is BlockReference)
                            {
                                BlockReference br = ((BlockReference) ent);
                                if (br.BlockName.Equals(newBlockName))
                                {
                                    newParent = br;
                                    break;
                                }
                            }
                        }

                        List<BlockReference> parents = parentTag.Parents == null ? new List<BlockReference>() : parentTag.Parents.Reverse().ToList();
                        parents.Add((BlockReference)parentTag.Entity);
                        Environment.SelectedItem item = new Environment.SelectedItem(new Stack<BlockReference>(parents), newParent);
                        item.Select(Model, true);
                        SelectedItems.Add(item);
                    }
                    
                    // update only the needed branch of the treeView
                    if (IsEditAssembly && !Model.Entities.IsOpenCurrentBlockReference)
                    {
                        // find node of current blockreference
                        TreeNode parentTn = parentNode;
                        while (parentTn != null && !ReferenceEquals(((NodeTag) parentTn.Tag).Entity,
                                   Model.Entities.CurrentBlockReference))                                   
                        {
                            parentTn = parentTn.Parent;
                        }
                        PopulateTree(Model.Entities, parentTn);
                    }
                    
                    SynchScreenSelection(new Stack<BlockReference>(/*Model.Entities.Parents*/));
                    Model.Invalidate();
                }
            }
        }

        private void DissolveOnClick(object sender, EventArgs e)
        {            
            if (Model != null && SelectedNode != null)
            {
                if (SelectedItems[0] != null && SelectedItems[0].Item != null && SelectedItems[0].Item is BlockReference)
                {
                    var br = SelectedItems[0].Item as BlockReference;
                    TreeNode selectedNode = (TreeNode)SelectedNode;

                    NodeTag parentNodeTag = (NodeTag)selectedNode.Parent.Tag;
                    string blockParentName = null;            
                    if (parentNodeTag != null && parentNodeTag.Entity is BlockReference)
                    {
                        blockParentName = ((BlockReference) parentNodeTag.Entity).BlockName;
                    }
                    Dissolve(br, blockParentName);
                    
                    // update only the needed branch of the treeView
                    if (IsEditAssembly && !Model.Entities.IsOpenCurrentBlockReference)
                    {
                        // find node of current blockreference
                        TreeNode parentTn = SelectedNodes[0].Parent;
                        while (parentTn != null && !ReferenceEquals(((NodeTag) parentTn.Tag).Entity,
                                   Model.Entities.CurrentBlockReference))                                   
                        {
                            parentTn = parentTn.Parent;
                        }
                        PopulateTree(Model.Entities, parentTn);
                    }
                    
                    Model.Entities.ClearSelection();
                    SelectedNodes.Clear();
                    SelectedItems.Clear();
                    Model.Entities.Regen();
                    Model.Invalidate();
                }
            }
        }

        private static List<TreeNode> _editStructureNodes = new List<TreeNode>();
        private static Transformation _editStructureTransformation = new Identity();
        private void EditStructureOnClick(object sender, EventArgs e)
        {
            if (Model != null && SelectedNodes != null)
            {
                foreach (TreeNode selectedNode in SelectedNodes)
                {
                        _editStructureNodes.Add(selectedNode);
                }

                if (_editStructureNodes.Count > 0)
                {
                    Entity selectedEnt = SelectedItems[0].Item as Entity;
                    _editStructureTransformation = selectedEnt != null ? Model.Entities.GetStackTransformation(SelectedItems[0].Parents) /** selectedBr.GetFullTransformation(model.Blocks)*/ : new Identity();
                    
                    // Paste menu items                
                    paste.Enabled = true;
                }
            }                        
        }

        private void PasteOnClick(object sender, EventArgs e)
        {
            if (_editStructureNodes.Count > 0)
            {                
                if (Model != null && SelectedNode != null)
                {
                    IEnumerable<Entity> entities = null;
                    Transformation destTransformation = new Identity();

                    NodeTag selectedNodeTag = SelectedNode.Tag as NodeTag;
                    if (selectedNodeTag == null || (IsEditAssembly && ReferenceEquals(Model.Entities.CurrentBlockReference, selectedNodeTag.Entity)))
                        entities = Model.Entities;
                    else
                    {
                        if (selectedNodeTag.Entity is BlockReference)
                            entities = Model.Blocks[((BlockReference)selectedNodeTag.Entity).BlockName].Entities;

                        // get transformation of the destination hierarchy to be inverted to preserve the actual position
                        destTransformation = selectedNodeTag.Entity is BlockReference ?  Model.Entities.GetStackTransformation(SelectedItems[0].Parents) * ((BlockReference)selectedNodeTag.Entity).GetFullTransformation(Model.Blocks)  : new Identity(); 
                        destTransformation.Invert();
                    }

                    if (entities != null)
                    {
                        _editStructureTransformation = destTransformation * _editStructureTransformation;

                        foreach (TreeNode selectedNode in _editStructureNodes)
                        {
                            Entity ent = ((NodeTag) selectedNode.Tag).Entity;
                            if (selectedNode.Parent != null)
                            {
                                NodeTag parentTag = (NodeTag)selectedNode.Parent.Tag;
                                IEnumerable<Entity> sourceCollection = (parentTag != null && (!IsEditAssembly || !ReferenceEquals(Model.Entities.CurrentBlockReference, parentTag.Entity)))
                                    ? (IEnumerable<Entity>) Model.Blocks[((BlockReference) parentTag.Entity).BlockName].Entities
                                    : (IEnumerable<Entity>) Model.Entities;

                                EditStructure(ent, sourceCollection, _editStructureTransformation, entities);
                                ClearInstanceRecoursively(ent, Model.Blocks);
                            }
                        }
                        
                        // update only the needed branch of the treeView
                        if (IsEditAssembly && !Model.Entities.IsOpenCurrentBlockReference)
                        {
                            // find node of current blockreference
                            TreeNode parentTn = SelectedNodes[0];
                            while (parentTn.Tag != null && !ReferenceEquals(((NodeTag) parentTn.Tag).Entity,
                                       Model.Entities.CurrentBlockReference))                                       
                            {
                                parentTn = parentTn.Parent;
                            }
                            PopulateTree(Model.Entities, parentTn);
                        }
                    
                        SynchScreenSelection(new Stack<BlockReference>(Model.Entities.Parents));
                        
                        Model.Entities.Regen();
                        Model.Invalidate();
                    }
                }
            }

            _editStructureNodes.Clear();            
            _editStructureTransformation = new Identity();

            // Paste menu items            
            paste.Enabled = false;
        }
        
        /// <summary>
        /// Save one or more component instances as a new block from within an assembly. The assembly points to the new block for those instances only; other unselected component instances in the assembly still point to the original block.
        /// </summary>
        /// <param name="br">The BlockReference to make independent</param>
        /// <param name="blocksMap">The blocks dictionary</param>
        public void MakeIndependent(BlockReference br, Dictionary<string, Block> blocksMap = null)
        {
            bool isRoot = false;
            if (blocksMap == null)
            {
                isRoot = true;
                blocksMap = new Dictionary<string, Block>(); // contains original block name and new Block created.
            }
             
            BlockKeyedCollection blocks = new BlockKeyedCollection(Model.Blocks);
            foreach (KeyValuePair<string, Block> pair in blocksMap)            
                blocks.TryAdd(pair.Value);            

            // duplicates block
            if (!blocksMap.ContainsKey(br.BlockName))
            {
                string newBlockName = UtilityEx.GetUnusedBlockName(br.BlockName, blocks, true);
                var newBlock = (Block)Model.Blocks[br.BlockName].Clone();
                newBlock.Name = newBlockName;
                blocksMap.Add(br.BlockName, newBlock);
                br.BlockName = newBlockName;

                // checks block components and duplicates nested blocks
                foreach (Entity entity in newBlock.Entities)
                {
                    if (entity is BlockReference)
                    {
                        MakeIndependent((BlockReference)entity, blocksMap);
                    }
                }
            }
            else
            {
                br.BlockName = blocksMap[br.BlockName].Name;
            }

            if (isRoot)
            {
                foreach (KeyValuePair<string, Block> pair in blocksMap)
                {
                    Model.Blocks.Add(pair.Value);
                }

                Model.Entities.Regen();
            }
            
            br.ClearVisibilityForAllInstances();
            br.ClearSelectabilityForAllInstances();
        }
        
        /// <summary>
        /// Change the order of the components within a level of the assembly hierarchy.
        /// </summary>
        /// <param name="ent">The BlockReference to move among assembly</param>
        /// <param name="sourceCollection">The collection which the br belongs to</param>
        /// <param name="fullTransformation">The transformation to apply to the br in the new assembly hierarchy to preserve its position</param>
        /// <param name="targetCollection">The destination collection</param>
        public static void EditStructure(Entity ent, IEnumerable<Entity> sourceCollection, Transformation fullTransformation, IEnumerable<Entity> targetCollection)
        {
            if (sourceCollection.Equals(targetCollection))
                return;

            if (sourceCollection is EntityList)
            {
                ((EntityList)sourceCollection).Remove(ent);
            }
            else if (sourceCollection is List<Entity>)
            {
                ((List<Entity>)sourceCollection).Remove(ent);
            }

            if (fullTransformation != null && fullTransformation.IsIdentity() == false)
            {
                ent.TransformBy(fullTransformation);
            }

            if (targetCollection is EntityList)
            {
                ((EntityList)targetCollection).Add(ent);
            }
            else if (targetCollection is List<Entity>)
            {
                ((List<Entity>)targetCollection).Add(ent);
            }

        }

        private static void ClearInstanceRecoursively(Entity ent, BlockKeyedCollection blocks)
        {
            ent.ClearVisibilityForAllInstances();
            ent.ClearSelectabilityForAllInstances();
            ent.ClearSelectionForAllInstances();

            if (ent is BlockReference)
            foreach (Entity entity in blocks[((BlockReference)ent).BlockName].Entities)
            {
                    ClearInstanceRecoursively(entity, blocks);
            }
        }

        /// <summary>
        /// Form a subassembly from components that are already in the assembly, thereby moving the components down one level in the assembly hierarchy.
        /// </summary>
        /// <param name="brList">The list of entities to use as entities for the new Block</param>
        /// <param name="newBlockName">The name of the new Block</param>
        /// <param name="model">The environment scene to insert the new block</param>
        public void FormNew(List<Entity> brList, string newBlockName)
        {
            Block bl = new Block(newBlockName);
            Model.Blocks.Add(bl);

            bool replaced = FormNewInternal(brList, Model.Entities, bl);

            foreach (var block in Model.Blocks)
            {
                replaced |= FormNewInternal(brList, block.Entities, bl);
            }

            if (replaced)
            {
                bl.Entities.AddRange(brList);
                Model.Entities.Regen();
            }
            else
            {
                Model.Blocks.Remove(bl);
            }

            foreach (Entity entity in brList)
            {
                ClearInstanceRecoursively(entity, Model.Blocks);
            }
        }

        private static bool FormNewInternal(List<Entity> entList, IEnumerable<Entity> entities, Block bl)
        {
            int matchFound = 0;
            List<Entity> entToRemove = new List<Entity>();
            foreach (Entity entity in entities)
            {
                    foreach (Entity reference in entList)
                    {
                        if (entity == reference)
                        {
                            matchFound++;
                            entToRemove.Add(entity);
                        }
                    }
            }

            if (matchFound == entList.Count)
            {
                string newBlockName = bl.Name;
                BlockReference reference = new BlockReference(newBlockName);
                if (entities is EntityList)
                {
                    int idx = ((EntityList)entities).IndexOf(entToRemove[0]);
                    foreach (Entity entity in entToRemove)
                        ((EntityList)entities).Remove(entity);

                    ((EntityList)entities).Insert(idx, reference);
                }
                else if (entities is List<Entity>)
                {
                    int idx = ((List<Entity>)entities).IndexOf(entToRemove[0]);
                    foreach (Entity entity in entToRemove)
                        ((List<Entity>)entities).Remove(entity);

                    ((List<Entity>)entities).Insert(idx, reference);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Dissolve a subassembly into individual components, thereby moving the components up one level in the assembly hierarchy.
        /// </summary>
        /// <param name="br">The BlockReference to dissolve</param>
        /// <param name="blockParentName">The name of the Block that contain the BockReference</param>
        public void Dissolve(BlockReference br, string blockParentName)
        {
            ClearInstanceRecoursively(br, Model.Blocks);
            Entity[] explodedEntities = br.Explode(Model.Blocks, false);
            
            if (blockParentName != null && (!IsEditAssembly || !blockParentName.Equals(Model.Entities.CurrentBlockReference.BlockName)))
            {
                Block bl = Model.Blocks[blockParentName];

                bl.Entities.Remove(br);
                bl.Entities.AddRange(explodedEntities);
                //Model.(0);
            }
            else
            {
                //it's a current root entity
                Model.Entities.Remove(br);
                Model.Entities.AddRange(explodedEntities);
            }
        }        
#endregion

#region move by ObjectManipulator
        private void MoveWithTriadOnClick(object sender, EventArgs e)
        {
            if (!IsMoving)
            {
                List<Entity> entities = new List<Entity>(SelectedItems.Count);

                if (SelectedItems.Count > 0)
                {
                    foreach (Environment.SelectedItem selectedItem in SelectedItems)
                    {
                        // it's a current root entity then add to the list to transform
                        if (selectedItem!= null && Model.Entities.Contains(selectedItem.Item))
                            entities.Add((Entity)selectedItem.Item);
                    }
                }
                // if there is at least a root entity selected then enable OM
                if (entities.Count > 0)
                {
                    apply.Enabled = true;
                    cancel.Enabled = true;
                    moveWithTriad.Enabled = false;
                    IsMoving = true;
                    Model.ObjectManipulator.Enable(null, true, entities);
                }
                else
                {
                    MessageBox.Show("Select an entity on the current root level first.");
                    moveWithTriad.Enabled = true;
                    apply.Enabled = false;
                    cancel.Enabled = false;
                }
            }
            else
            {
                Model.ObjectManipulator.Cancel();
                IsMoving = false;
                apply.Enabled = false;
                cancel.Enabled = false;
                moveWithTriad.Enabled = true;
            }

            Model.Invalidate();
        }
        private void ApplyOnClick(object sender, EventArgs e)
        {
            Model.ObjectManipulator.Apply();
            moveWithTriad.Enabled = true;
            apply.Enabled = false;
            cancel.Enabled = false;
            IsMoving = false;
            Model.Entities.Regen();
            Model.Invalidate();
        }
        
        private void CancelOnClick(object sender, EventArgs e)
        {
            Model.ObjectManipulator.Cancel();
            moveWithTriad.Enabled = true;
            apply.Enabled = false;
            cancel.Enabled = false;
            IsMoving = false;
            Model.Invalidate();
        }
#endregion
#endregion Context Menu
        
    }
}