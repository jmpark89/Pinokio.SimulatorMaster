using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Pinokio.Designer
{
    // https://www.codeproject.com/Articles/20581/Multiselect-Treeview-Implementation
    public class MultiSelectTreeView : TreeView
	{

        #region Enum
        /// <summary>
        /// Decides the multi select characteristics of an MWTreeView Control.
        /// </summary>
        public enum SelectionType
        {
            /// <summary>
            /// Standard single select.
            /// </summary>
            Standard = 0,
            
            /// <summary>
            /// Standard multi select with no particular constraints.
            /// </summary>
            Multi = 1,

            /// <summary>
            /// Multi select where the selected TreeNodes have to reside on the same branch and level.
            /// </summary>
            MultiSameBranchAndLevel = 2            
        }
        
        /// <summary>
        /// Decides which button of the mouse perform the selection.
        /// </summary>
        public enum SelectionMouseButtonType
        {
            /// <summary>
            /// Selects with left mouse button.
            /// </summary>
            Left = 0,
            
            /// <summary>
            /// Selects with right mouse button.
            /// </summary>
            Right = 1,
            
            /// <summary>
            /// Selects with left and right mouse button.
            /// </summary>
            Both = 2,
        }
        #endregion Enum

        #region Selected Node(s) Properties

        private List<TreeNode> m_SelectedNodes = null;
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<TreeNode> SelectedNodes
		{
			get
			{
				return m_SelectedNodes;
			}
			set
			{
				ClearSelectedNodes();
				if( value != null )
				{
					foreach( TreeNode node in value )
					{
						ToggleNode( node, true );
					}
				}
			}
		}

		// Note we use the new keyword to Hide the native treeview's SelectedNode property.
		private TreeNode m_SelectedNode;
		public new TreeNode SelectedNode
		{
			get { return m_SelectedNode; }
			set
			{
				ClearSelectedNodes();
				if( value != null )
				{
					SelectNode( value );
				}
			}
		}

        private SelectionType m_SelectionMode = SelectionType.MultiSameBranchAndLevel;

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public SelectionType SelectionMode
        {
            get { return m_SelectionMode; }
            set { m_SelectionMode = value; }
        }

        private SelectionMouseButtonType m_SelectionMouseButtonMode = SelectionMouseButtonType.Both;

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public SelectionMouseButtonType SelectionMouseButtonMode
        {
            get { return m_SelectionMouseButtonMode; }
            set { m_SelectionMouseButtonMode = value; }
        }

        #endregion

        public MultiSelectTreeView()
		{
			m_SelectedNodes = new List<TreeNode>();
			base.SelectedNode = null;
		}

		#region Overridden Events

		protected override void OnGotFocus( EventArgs e )
		{
			// Make sure at least one node has a selection
			// this way we can tab to the ctrl and use the 
			// keyboard to select nodes
			try
			{
                if (m_SelectedNode == null)
                {

                    if (this.TopNode != null)
                    {
                        ToggleNode(this.TopNode, true);
                    }
                }

                base.OnGotFocus( e );
			}
			catch( Exception ex )
			{
				HandleException( ex );
			}
		}

		protected override void OnMouseDown( MouseEventArgs e )
		{
            if ((e.Button != MouseButtons.Left && m_SelectionMouseButtonMode == SelectionMouseButtonType.Left) ||
                (e.Button != MouseButtons.Right && m_SelectionMouseButtonMode == SelectionMouseButtonType.Right) ||
                (e.Button == MouseButtons.Middle && m_SelectionMouseButtonMode == SelectionMouseButtonType.Both))
            {
                base.OnMouseDown(e);
                return;
            }

			// If the user clicks on a node that was not
			// previously selected, select it now.

			try
			{
				base.SelectedNode = null;

				TreeNode node = this.GetNodeAt( e.Location );
				if( node != null )
				{
					int leftBound = node.Bounds.X; // - 20; // Allow user to click on image
					int rightBound = node.Bounds.Right + 10; // Give a little extra room
					if( e.Location.X > leftBound && e.Location.X < rightBound )
					{
                        // Removed this lines (and OnMouseUp code too) to prevent double selection at same time when there is no change in the tree.
                        // To restore the original code uncomment the following lines and the entire OnMouseUp() method code below.
                        /*
						  if( ModifierKeys == Keys.None && ( m_SelectedNodes.Contains( node ) ) )
						  {
						  	// Potential Drag Operation
						  	// Let Mouse Up do select
						  }
						  else
						  {
                            SelectNode( node );
                          }
                       */
                        if(e.Button != MouseButtons.Left && m_SelectedNodes.Contains(node))
                        {
                            base.OnMouseDown(e);
                            return;
                        }
                        SelectNode(node);
                        
					}
				}

				base.OnMouseDown( e );
			}
			catch( Exception ex )
			{
				HandleException( ex );
			}
		}

        // commented it because it was performing an useless double selection that affected big tree collection performances.
        /* 
		protected override void OnMouseUp( MouseEventArgs e )
		{

            if (e.Button != MouseButtons.Left)
            {
                base.OnMouseUp(e);
                return;
            }

            // If the clicked on a node that WAS previously
            // selected then, reselect it now. This will clear
            // any other selected nodes. e.g. A B C D are selected
            // the user clicks on B, now A C & D are no longer selected.
            try
			{
				// Check to see if a node was clicked on 
				TreeNode node = this.GetNodeAt( e.Location );
				if( node != null )
				{
					if( ModifierKeys == Keys.None && m_SelectedNodes.Contains( node ) )
					{
						int leftBound = node.Bounds.X; // -20; // Allow user to click on image
						int rightBound = node.Bounds.Right + 10; // Give a little extra room
						if( e.Location.X > leftBound && e.Location.X < rightBound )
						{

                            SelectNode( node );
                        }
					}
				}

				base.OnMouseUp( e );
			}
			catch( Exception ex )
			{
				HandleException( ex );
			}
		}	
        */

		protected override void OnBeforeSelect( TreeViewCancelEventArgs e )
		{
			// Never allow base.SelectedNode to be set!
			try
			{
				base.SelectedNode = null;
				e.Cancel = true;

				base.OnBeforeSelect( e );
			}
			catch( Exception ex )
			{
				HandleException( ex );
			}
		}

		protected override void OnAfterSelect( TreeViewEventArgs e )
		{
			// Never allow base.SelectedNode to be set!
			try
			{
				base.OnAfterSelect( e );
				base.SelectedNode = null;
			}
			catch( Exception ex )
			{
				HandleException( ex );
			}
		}

		protected override void OnKeyDown( KeyEventArgs e )
		{
			// Handle all possible key strokes for the control.
			// including navigation, selection, etc.

			base.OnKeyDown( e );

			if( e.KeyCode == Keys.ShiftKey ) return;

			//this.BeginUpdate();
			bool bShift = ( ModifierKeys == Keys.Shift );

			try
			{
				// Nothing is selected in the tree, this isn't a good state
				// select the top node
				if( m_SelectedNode == null && this.TopNode != null )
				{
					ToggleNode( this.TopNode, true );
				}

				// Nothing is still selected in the tree, this isn't a good state, leave.
				if( m_SelectedNode == null ) return;

				if( e.KeyCode == Keys.Left )
				{
					if( m_SelectedNode.IsExpanded && m_SelectedNode.Nodes.Count > 0 )
					{
						// Collapse an expanded node that has children
						m_SelectedNode.Collapse();
					}
					else if( m_SelectedNode.Parent != null )
					{
						// Node is already collapsed, try to select its parent.
						SelectSingleNode( m_SelectedNode.Parent );
					}
				}
				else if( e.KeyCode == Keys.Right )
				{
					if( !m_SelectedNode.IsExpanded )
					{
						// Expand a collpased node's children
						m_SelectedNode.Expand();
					}
					else
					{
						// Node was already expanded, select the first child
						SelectSingleNode( m_SelectedNode.FirstNode );
					}
				}
				else if( e.KeyCode == Keys.Up )
				{
					// Select the previous node
					if( m_SelectedNode.PrevVisibleNode != null )
					{
						SelectNode( m_SelectedNode.PrevVisibleNode );
					}
				}
				else if( e.KeyCode == Keys.Down )
				{
					// Select the next node
					if( m_SelectedNode.NextVisibleNode != null )
					{
						SelectNode( m_SelectedNode.NextVisibleNode );
					}
				}
				else if( e.KeyCode == Keys.Home )
				{
					if( bShift )
					{
						if( m_SelectedNode.Parent == null )
						{
							// Select all of the root nodes up to this point 
							if( this.Nodes.Count > 0 )
							{
								SelectNode( this.Nodes[0] );
							}
						}
						else
						{
							// Select all of the nodes up to this point under this nodes parent
							SelectNode( m_SelectedNode.Parent.FirstNode );
						}
					}
					else
					{
						// Select this first node in the tree
						if( this.Nodes.Count > 0 )
						{
							SelectSingleNode( this.Nodes[0] );
						}
					}
				}
				else if( e.KeyCode == Keys.End )
				{
					if( bShift )
					{
						if( m_SelectedNode.Parent == null )
						{
							// Select the last ROOT node in the tree
							if( this.Nodes.Count > 0 )
							{
								SelectNode( this.Nodes[this.Nodes.Count - 1] );
							}
						}
						else
						{
							// Select the last node in this branch
							SelectNode( m_SelectedNode.Parent.LastNode );
						}
					}
					else
					{
						if( this.Nodes.Count > 0 )
						{
							// Select the last node visible node in the tree.
							// Don't expand branches incase the tree is virtual
							TreeNode ndLast = this.Nodes[0].LastNode;
							while( ndLast.IsExpanded && ( ndLast.LastNode != null ) )
							{
								ndLast = ndLast.LastNode;
							}
							SelectSingleNode( ndLast );
						}
					}
				}
				else if( e.KeyCode == Keys.PageUp )
				{
					// Select the highest node in the display
					int nCount = this.VisibleCount;
					TreeNode ndCurrent = m_SelectedNode;
					while( ( nCount ) > 0 && ( ndCurrent.PrevVisibleNode != null ) )
					{
						ndCurrent = ndCurrent.PrevVisibleNode;
						nCount--;
					}
					SelectSingleNode( ndCurrent );
				}
				else if( e.KeyCode == Keys.PageDown )
				{
					// Select the lowest node in the display
					int nCount = this.VisibleCount;
					TreeNode ndCurrent = m_SelectedNode;
					while( ( nCount ) > 0 && ( ndCurrent.NextVisibleNode != null ) )
					{
						ndCurrent = ndCurrent.NextVisibleNode;
						nCount--;
					}
					SelectSingleNode( ndCurrent );
				}
				else
				{
					// Assume this is a search character a-z, A-Z, 0-9, etc.
					// Select the first node after the current node that 
					// starts with this character
					string sSearch = ( (char) e.KeyValue ).ToString();

					TreeNode ndCurrent = m_SelectedNode;
					while( ( ndCurrent.NextVisibleNode != null ) )
					{
						ndCurrent = ndCurrent.NextVisibleNode;
						if( ndCurrent.Text.StartsWith( sSearch ) )
						{
							SelectSingleNode( ndCurrent );
							break;
						}
					}
				}
			}
			catch( Exception ex )
			{
				HandleException( ex );
			}
			finally
			{
				this.EndUpdate();
			}
		}

        #region Drag&Drop

        protected override void OnDragEnter(DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;

            base.OnDragEnter(e);
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            // If the user drags a node and the node being dragged is NOT
            // selected, then clear the active selection, select the
            // node being dragged and drag it. Otherwise if the node being
            // dragged is selected, drag the entire selection.
            try
            {
                TreeNode node = e.Item as TreeNode;

                if (node != null)
                {
                    if (!m_SelectedNodes.Contains(node))
                    {
                        SelectSingleNode(node);
                        ToggleNode(node, true);
                    }
                }

                base.OnItemDrag(e);

                DoDragDrop(e.Item, DragDropEffects.Move);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);

            // Retrieve the client coordinates of the drop location.
            Point targetPoint = PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNode targetNode = GetNodeAt(targetPoint);

            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // Sanity check
            if (draggedNode == null)
            {
                return;
            }

            // Did the user drop on a valid target node?
            if (targetNode == null)
            {
                // The user dropped the node on the treeview control instead
                // of another node so lets place the node at the bottom of the tree.
                draggedNode.Remove();
                Nodes.Add(draggedNode);
                draggedNode.Expand();
            }
            else
            {
                TreeNode parentNode = targetNode;

                // Confirm that the node at the drop location is not 
                // the dragged node and that target node isn't null
                // (for example if you drag outside the control)
                if (!draggedNode.Equals(targetNode) && targetNode != null)
                {
                    bool canDrop = true;

                    // Crawl our way up from the node we dropped on to find out if
                    // if the target node is our parent. 
                    while (canDrop && (parentNode != null))
                    {
                        canDrop = !System.Object.ReferenceEquals(draggedNode, parentNode);
                        parentNode = parentNode.Parent;
                    }

                    // Is this a valid drop location?
                    if (canDrop)
                    {
                        // Yes. Move the node, expand it, and select it.
                        draggedNode.Remove();
                        targetNode.Nodes.Add(draggedNode);
                        targetNode.Expand();
                    }
                }
            }

            // Optional: Select the dropped node and navigate (however you do it)
            SelectedNode = draggedNode;
            // NavigateToContent(draggedNode.Tag);
        }

        #endregion Drag&Drop

        #endregion

        #region Helper Methods

        private void SelectNode( TreeNode node )
		{
			try
			{
				this.BeginUpdate();

				if( m_SelectedNode == null || (m_SelectionMode != SelectionType.Standard && ModifierKeys == Keys.Control) )
				{
					// Ctrl+Click selects an unselected node, or unselects a selected node.
					bool bIsSelected = m_SelectedNodes.Contains( node );
					ToggleNode( node, !bIsSelected );
				}
				else if(m_SelectionMode != SelectionType.Standard && ModifierKeys == Keys.Shift )
				{
					// Shift+Click selects nodes between the selected node and here.
					TreeNode ndStart = m_SelectedNode;
					TreeNode ndEnd = node;

					if( ndStart.Parent == ndEnd.Parent )
					{
						// Selected node and clicked node have same parent, easy case.
						if( ndStart.Index < ndEnd.Index )
						{							
							// If the selected node is beneath the clicked node walk down
							// selecting each Visible node until we reach the end.
							while( ndStart != ndEnd )
							{
								ndStart = ndStart.NextVisibleNode;
								if( ndStart == null ) break;
								ToggleNode( ndStart, true );
							}
						}
						else if( ndStart.Index == ndEnd.Index )
						{
							// Clicked same node, do nothing
						}
						else
						{
							// If the selected node is above the clicked node walk up
							// selecting each Visible node until we reach the end.
							while( ndStart != ndEnd )
							{
								ndStart = ndStart.PrevVisibleNode;
								if( ndStart == null ) break;
								ToggleNode( ndStart, true );
							}
						}
					}
					else
					{
						// Selected node and clicked node have same parent, hard case.
						// We need to find a common parent to determine if we need
						// to walk down selecting, or walk up selecting.

						TreeNode ndStartP = ndStart;
						TreeNode ndEndP = ndEnd;
						int startDepth = Math.Min( ndStartP.Level, ndEndP.Level );

						// Bring lower node up to common depth
						while( ndStartP.Level > startDepth )
						{
							ndStartP = ndStartP.Parent;
						}

						// Bring lower node up to common depth
						while( ndEndP.Level > startDepth )
						{
							ndEndP = ndEndP.Parent;
						}

						// Walk up the tree until we find the common parent
						while( ndStartP.Parent != ndEndP.Parent )
						{
							ndStartP = ndStartP.Parent;
							ndEndP = ndEndP.Parent;
						}

						// Select the node
						if( ndStartP.Index < ndEndP.Index )
						{
							// If the selected node is beneath the clicked node walk down
							// selecting each Visible node until we reach the end.
							while( ndStart != ndEnd )
							{
								ndStart = ndStart.NextVisibleNode;
								if( ndStart == null ) break;
								ToggleNode( ndStart, true );
							}
						}
						else if( ndStartP.Index == ndEndP.Index )
						{
							if( ndStart.Level < ndEnd.Level )
							{
								while( ndStart != ndEnd )
								{
									ndStart = ndStart.NextVisibleNode;
									if( ndStart == null ) break;
									ToggleNode( ndStart, true );
								}
							}
							else
							{
								while( ndStart != ndEnd )
								{
									ndStart = ndStart.PrevVisibleNode;
									if( ndStart == null ) break;
									ToggleNode( ndStart, true );
								}
							}
						}
						else
						{
							// If the selected node is above the clicked node walk up
							// selecting each Visible node until we reach the end.
							while( ndStart != ndEnd )
							{
								ndStart = ndStart.PrevVisibleNode;
								if( ndStart == null ) break;
								ToggleNode( ndStart, true );
							}
						}
					}
				}
				else
				{
					// Just clicked a node, select it
					SelectSingleNode( node );
				}

				OnAfterSelect( new TreeViewEventArgs( m_SelectedNode ) );
			}
			finally
			{
				this.EndUpdate();
			}
		}

		internal void ClearSelectedNodes()
		{
			try
			{
				foreach( TreeNode node in m_SelectedNodes )
				{
					node.BackColor = this.BackColor;
					node.ForeColor = this.ForeColor;
				}
			}
			finally
			{
				m_SelectedNodes.Clear();
				m_SelectedNode = null;
			}
		}

		private void SelectSingleNode( TreeNode node )
		{
			if( node == null )
			{
				return;
			}

			ClearSelectedNodes();
			ToggleNode( node, true );
			node.EnsureVisible();
		}

		private void ToggleNode( TreeNode node, bool bSelectNode )
		{
			if( bSelectNode )
			{
                if (m_SelectedNodes.Count > 0 && m_SelectionMode == SelectionType.MultiSameBranchAndLevel)
                {
                    if (m_SelectedNodes[0].Parent != node.Parent ||
                        GetTreeNodeLevel(m_SelectedNodes[0]) != GetTreeNodeLevel(node))
                    {
                        node.BackColor = this.BackColor;
                        node.ForeColor = this.ForeColor;
                        return;
                    }
                }

				m_SelectedNode = node;
				if( !m_SelectedNodes.Contains( node ) )
				{
					m_SelectedNodes.Add( node );
				}
				node.BackColor = SystemColors.Highlight;
				node.ForeColor = SystemColors.HighlightText;
			}
			else
			{
				m_SelectedNodes.Remove( node );
				node.BackColor = this.BackColor;
				node.ForeColor = this.ForeColor;
			}
		}

		private void HandleException( Exception ex )
		{
			// Perform some error handling here.
			// We don't want to bubble errors to the CLR. 
			MessageBox.Show( ex.Message );            
        }

        /// <summary>
        /// Get the GrandParent of the TreeNode supplied.
        /// The GrandParent is the outermost Parent of a TreeNode.
        /// </summary>
        /// <param name="tn">TreeNode to get GrandParent of.</param>
        /// <returns>The GrandParent of the TreeNode supplied.</returns>
        private TreeNode GetTreeNodeGrandParent(TreeNode tn)
        {
            if (tn != null && tn.Parent != null)
            {
                return GetTreeNodeGrandParent(tn.Parent);
            }
            else
            {
                return tn;
            }
        }

        /// <summary>
        /// Get the TreeNodeLevel of the TreeNode supplied.
        /// Note that the TreeNodes in the TreeView's Nodes collection are considered to be of TreeNodeLevel zero (0) and each consecutive level thereafter is one more (1, 2, etc).
        /// </summary>
        /// <param name="tn">TreeNode whose TreeNodeLevel should be checked.</param>
        /// <returns>The TreeNodeLevel of the TreeNode supplied.</returns>
        private int GetTreeNodeLevel(TreeNode tn)
        {
            int i = 0;

            if (tn != null && tn.Parent != null)
            {
                return GetTreeNodeLevel(tn.Parent) + 1;
            }
            else
            {
                return i;
            }
        }

        #endregion
    }
}
