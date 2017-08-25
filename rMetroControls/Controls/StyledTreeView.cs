using rMetroControls.Win32Interop;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace rMetroControls.Controls
{
    /// <summary>
    /// A TreeView that matches the currently enabled style of the application.
    /// </summary>
    [DesignerCategory("Code")]
    [DisplayName("Styled TreeView")]
    [Description("A TreeView that matches the currently enabled style of the application.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(TreeView))]
    public class StyledTreeView
        : TreeView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewStyledTreeView"/> class.
        /// </summary>
        public StyledTreeView()
            : base()
        {
            //Enable custom rendering for the treeview & set up necessary settings
            this.BorderStyle = BorderStyle.FixedSingle;
            this.DrawMode = TreeViewDrawMode.OwnerDrawText; //OwnerDrawAll would be right but when OwnerDrawText is specified, the system does the node layout for us.
            this.FullRowSelect = true;
            this.HideSelection = false;
            this.ItemHeight = 19;
            
            //Make it display the current style
            this.Style = StyleProvider.ActiveStyle;
            StyleProvider.ActiveStyleChanged += (s, e) => this.Style = StyleProvider.ActiveStyle;

#if DEBUG

            //Add dummy nodes for testing
            this.Nodes.Add(new TreeNode("Root", new TreeNode[] { new TreeNode("Child 1"), new TreeNode("Child 2"), new TreeNode("Child and Parent", new TreeNode[] { new TreeNode("Sub-Child") }) }));

#endif
        }
        
        /// <summary>
        /// Gets the default size.
        /// </summary>
        /// <value>
        /// The default size.
        /// </value>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(250, 400);
            }
        }

        /// <summary>
        /// The current style internal variable.
        /// </summary>
        private Style currentStyle;
        /// <summary>
        /// Gets or sets the style that should be displayed.
        /// </summary>
        /// <value>
        /// The style.
        /// </value>
        [Category("Appearance")]
        [Description("The style currently displayed by this style tester.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Style Style
        {
            get
            {
                return this.currentStyle;
            }
            set
            {
                this.currentStyle = value;

                //Refresh properties
                this.BackColor = this.Style.ControlBackColor;

                //Refresh
                this.Invalidate();
            }
        }
        
        /// <summary>
        /// Raises the <see cref="E:MouseDown" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            //Allow node selection anywhere from the left side of the control to the right side
            if (e.Button == MouseButtons.Left)
            {
                //Find the right node
                TreeNode node = this.FindNode(e.Y);

                //Select the underlying node if one was found
                if (node != null)
                {
                    //Prevent the activation if the cursor is in the arrow rectangle
                    Rectangle arrowRect = new Rectangle(new Point(node.Bounds.X - 19, node.Bounds.Y), new Size(19, node.Bounds.Height));
                    if (arrowRect.Contains(e.Location))
                    {
                        return;
                    }

                    //Select the node
                    this.SelectedNode = node;
                }
            }

            base.OnMouseDown(e);
        }
        
        /// <summary>
        /// Raises the <see cref="E:MouseDoubleClick" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //Find the right node
                TreeNode node = this.FindNode(e.Y);

                //Toggle expanded-value of the underlying node if one was found
                if (node != null)
                {
                    //Prevent the toggle if the cursor is in the arrow rectangle or if it is in the node rectangle -> more intuitive and less buggy behaviour
                    Rectangle arrowRect = new Rectangle(new Point(node.Bounds.X - 19, node.Bounds.Y), new Size(19, node.Bounds.Height));
                    if (arrowRect.Contains(e.Location) || node.Bounds.Contains(e.Location))
                    {
                        return;
                    }

                    //Toggle the node's expanded state
                    node.Toggle();
                }
            }

            base.OnMouseDoubleClick(e);
        }

        /// <summary>
        /// Raises the <see cref="E:MouseMove" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //Invalidate the control to handle hover painting on the arrows.
            this.Invalidate();

            base.OnMouseMove(e);
        }

        /// <summary>
        /// Finds the node at the specified y level.
        /// </summary>
        /// <param name="y">The y coordinate.</param>
        /// <returns>The node at the specified y level.</returns>
        protected virtual TreeNode FindNode(int y)
        {
            //Iterate through all points from left to right and test if there's a node underneath - if one was found, exit the loop
            TreeNode node = null;
            for (int x = 0; x < this.Width && node == null; x++)
            {
                node = this.GetNodeAt(x, y);
            }

            return node;
        }

        /// <summary>
        /// Raises the <see cref="E:DrawNode" /> event.
        /// </summary>
        /// <param name="e">The <see cref="DrawTreeNodeEventArgs"/> instance containing the event data.</param>
        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            //General information
            // - The specified bounds of the node is always { 1 + indent * level, 16 * visible index }
            // - The arrow bounds can be calculated as follows: { { e bounds X - 18 OR node bounds X - 19, node bounds Y }, { 19, node bounds Height } }
            // - If an icon is displayed, the display size is { 16, 16 } and it is displayed left to the content; the content is moved 21 pixels to the right

            //Sometimes there are invalid calls to the OnDrawNode method - ignore these
            if (e.Bounds.X == -1)
            {
                return;
            }

            //Set up graphics
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            //Calculate the right rectangles
            Rectangle fullItemRect = new Rectangle(new Point(0, e.Bounds.Y), new Size(this.DisplayRectangle.Width, e.Bounds.Height));
            Rectangle contentRect = new Rectangle(new Point(e.Bounds.X, e.Bounds.Y), new Size(this.DisplayRectangle.Width - e.Bounds.X, e.Bounds.Height));
            Rectangle iconRect = new Rectangle(contentRect.Location + new Size(0, (contentRect.Height - 16) / 2), new Size(16, 16));
            Rectangle textRect = e.Node.ImageIndex == -1 ? contentRect : new Rectangle(contentRect.Location + new Size(21, 0), contentRect.Size - new Size(21 + 2, 0));
            Rectangle arrowRect = new Rectangle(new Point(e.Bounds.X - 18, e.Bounds.Y), new Size(19, e.Bounds.Height - 1));

            //Get the right colors
            Color backColor = this.BackColor;
            Color foreColor = this.Style.ForegroundColor;
            bool showArrow = e.Node.Nodes.Count > 0;
            bool arrowHover = arrowRect.Contains(PointToClient(Cursor.Position));
            Color arrowColor = !arrowHover ? this.Style.ForegroundColor : this.Style.AccentColor;
            bool arrowOutline = !e.Node.IsExpanded;
            if (e.State.HasFlag(TreeNodeStates.Grayed))
            {
                backColor = this.Style.ControlBackHoverColor;
            }
            else if (e.State.HasFlag(TreeNodeStates.Selected))
            {
                backColor = this.Style.AccentHoverColor;
                foreColor = this.Style.AccentForegroundColor;

                arrowOutline = !arrowHover ^ e.Node.IsExpanded; //Some magic to make it work like VS ^^
                arrowColor = this.Style.AccentForegroundColor;
            }

            //Calculate the points for the arrow
            PointF[] arrowPoints;
            if (e.Node.IsExpanded)
            {
                //Arrow bounds: { 5, 5 }
                //Offset to top left: { 7, 6 }
                Rectangle arrowInnerRect = new Rectangle(arrowRect.Location + new Size(7, 6), new Size(5, 5));
                arrowPoints = new PointF[]
                {
                    arrowInnerRect.Location + new Size(0, arrowInnerRect.Height),
                    arrowInnerRect.Location + arrowInnerRect.Size,
                    arrowInnerRect.Location + new Size(arrowInnerRect.Width, 0)
                };
            }
            else
            {
                //Arrow bounds: { 4, 8 }
                //Offset to top left: { 8, 5 }
                Rectangle arrowInnerRect = new Rectangle(arrowRect.Location + new Size(8, 5), new Size(4, 8));
                arrowPoints = new PointF[]
                {
                    arrowInnerRect.Location,
                    arrowInnerRect.Location + new Size(0, arrowInnerRect.Height),
                    (PointF)arrowInnerRect.Location + new SizeF(arrowInnerRect.Width, arrowInnerRect.Height / 2f)
                };
            }

            //Create brushes for the back- and foreground
            using (SolidBrush backBrush = new SolidBrush(backColor))
            using (SolidBrush foreBrush = new SolidBrush(foreColor))
            {
                //Draw the background
                e.Graphics.FillRectangle(backBrush, fullItemRect);

                //Draw the arrow if needed
                if (e.Node.Nodes.Count > 0)
                {
                    //Enable antialiasing for smoother arrows
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    //Draw outline
                    using (Pen arrowPen = new Pen(arrowColor))
                    {
                        e.Graphics.DrawPolygon(arrowPen, arrowPoints);
                    }
                    if (!arrowOutline) //Draw filling if necessary
                    {
                        using (SolidBrush arrowBrush = new SolidBrush(arrowColor))
                        {
                            e.Graphics.FillPolygon(arrowBrush, arrowPoints);
                        }
                    }
                }

                //Draw the text
                e.Graphics.DrawString(e.Node.Text, e.Node.NodeFont ?? this.Font, foreBrush, textRect, new StringFormat() { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter });
                
                //Draw the icon if one exists
                if (e.Node.ImageIndex != -1)
                {
                    this.ImageList.Draw(e.Graphics, iconRect.X, iconRect.Y, iconRect.Width, iconRect.Height, e.Node.ImageIndex);
                }
            }
            
            base.OnDrawNode(e);
        }

        /// <summary>
        /// Raises the <see cref="E:HandleCreated" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            //Enable doublebuffering
            //The Control.DoubleBuffered method was disabled my Microsoft to enable support for Windows 2000 and 98 as they did not support this feature.
            //To be able to enable doublebuffering, we have to set the TVS_EX_DOUBLEBUFFER flag of the treeview.
            NativeMethods.SendMessage(this.Handle, NativeConstants.TVM_SETEXTENDEDSTYLE, new IntPtr(NativeConstants.TVS_EX_DOUBLEBUFFER), new IntPtr(NativeConstants.TVS_EX_DOUBLEBUFFER));

            base.OnHandleCreated(e);
        }
    }
}