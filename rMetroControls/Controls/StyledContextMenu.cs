using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace rMetroControls.Controls
{
    /// <summary>
    /// A styled context menu.
    /// </summary>
    [DesignerCategory("Code")]
    [DisplayName("Styled ContextMenu")]
    [Description("A ContextMenu that uses the style provided by the StyleProvider class.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ContextMenu))]
    public class StyledContextMenu
        : ContextMenuStrip
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyledContextMenu"/> class.
        /// </summary>
        public StyledContextMenu()
            : base()
        {
            //Call initializer
            this.Initialize();
        }

        /// <summary>
        /// Gets the internal spacing, in pixels, of the control.
        /// </summary>
        protected override Padding DefaultPadding
        {
            get
            {
                Padding basePadding = base.DefaultPadding;
                return new Padding(basePadding.Left + 3, basePadding.Top + 2, basePadding.Right, basePadding.Bottom + 2);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyledContextMenu"/> class.
        /// </summary>
        /// <param name="container">A component that implements <see cref="T:System.ComponentModel.IContainer" /> that is the container of the <see cref="T:System.Windows.Forms.ContextMenuStrip" />.</param>
        public StyledContextMenu(IContainer container)
            : base(container)
        {
            //Call initializer
            this.Initialize();
        }

        /// <summary>
        /// Initializes the style.
        /// </summary>
        protected virtual void Initialize()
        {
            //Initialize renderer for this context menu
            this.Renderer = new StyledContextMenuRenderer();

            //Initialize Shadow property
            this.DisplayShadow = false;
        }

        private bool displayShadow = false;
        /// <summary>
        /// Gets or sets a value indicating whether a shadow should be displayed underneath this ContextMenu.
        /// </summary>
        /// <value>
        ///   <c>true</c> if a shadow should be displayed; otherwise, <c>false</c>.
        /// </value>
        [Category("Appearance")]
        [Description("Determines wheather a shadow should be displayed underneath this ContextMenu.")]
        [DefaultValue(false)]
        public bool DisplayShadow
        {
            get
            {
                return this.displayShadow;
            }
            set
            {
                //Refresh internal value
                this.displayShadow = value;

                //Set Shadow flag on main menu
                this.DropShadowEnabled = this.DisplayShadow;
            }
        }

        /// <summary>
        /// A styled renderer for the <see cref="StyledContextMenu"/>.
        /// </summary>
        public class StyledContextMenuRenderer
            : ToolStripProfessionalRenderer
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="StyledContextMenuRenderer"/> class.
            /// </summary>
            public StyledContextMenuRenderer()
                : base(new StyledContextMenuColorTable())
            { }

            protected override void InitializeItem(ToolStripItem item)
            {
                if (item is ToolStripDropDownItem)
                {
                    ToolStripDropDownItem dropDownItem = item as ToolStripDropDownItem;
                    dropDownItem.DropDown.Padding = new Padding(dropDownItem.DropDown.Padding.Left + 3, dropDownItem.DropDown.Padding.Top + 2, dropDownItem.DropDown.Padding.Right, dropDownItem.DropDown.Padding.Bottom + 2);
                }
                
                base.InitializeItem(item);
            }

            /// <summary>
            /// Renders the item's text.
            /// </summary>
            /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> that contains the event data.</param>
            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = StyleProvider.ActiveStyle.ForegroundColor; //Set foreground color, this is needed if you want to use the accent color for hovering items as then there are variable arrow colors.

                base.OnRenderItemText(e);
            }

            /// <summary>
            /// Renders the menu item's background.
            /// </summary>
            /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Color currentColor = e.Item.Selected ? StyleProvider.ActiveStyle.ControlBackHoverColor : StyleProvider.ActiveStyle.ControlBackColor; //Determine color
                e.Graphics.Clear(currentColor); //Clear graphics with the current color
                e.Item.BackColor = currentColor; //Set the item's backcolor to the current color

                base.OnRenderMenuItemBackground(e);
            }

            /// <summary>
            /// Renders the tool strip's background.
            /// </summary>
            /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                e.Graphics.Clear(StyleProvider.ActiveStyle.ControlBackColor); //Remove white border on the context menu

                //base.OnRenderToolStripBackground(e);
            }

            /// <summary>
            /// Renders the arrow displayed when there are sub-items in a menu item.
            /// </summary>
            /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripArrowRenderEventArgs" /> that contains the event data.</param>
            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                //Set Arrow Color. This is needed if you want to use the accent color for hovering items as then there are variable arrow colors.
                e.ArrowColor = StyleProvider.ActiveStyle.ForegroundColor;

                base.OnRenderArrow(e);
            }

            /// <summary>
            /// Renders the seperator.
            /// </summary>
            /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> that contains the event data.</param>
            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                e.Graphics.Clear(StyleProvider.ActiveStyle.ControlBackColor); //Background
                e.Graphics.FillRectangle(new SolidBrush(StyleProvider.ActiveStyle.BorderColor), new Rectangle(e.Item.ContentRectangle.Location, new Size(e.Item.ContentRectangle.Width, 1))); //Draw Seperator

                //base.OnRenderSeparator(e);
            }
        }

        /// <summary>
        /// A color table for the <see cref="StyledContextMenu"/> that returns the colors of the current style.
        /// </summary>
        public class StyledContextMenuColorTable
            : ProfessionalColorTable
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="StyledContextMenuColorTable"/> class.
            /// </summary>
            public StyledContextMenuColorTable()
                : base()
            { }

            /// <summary>
            /// Gets the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.
            /// </summary>
            public override Color MenuItemSelected
            {
                get
                {
                    return StyleProvider.ActiveStyle.ControlBackHoverColor;
                }
            }

            /// <summary>
            /// Gets the color that is the border color to use on a <see cref="T:System.Windows.Forms.MenuStrip" />.
            /// </summary>
            public override Color MenuBorder
            {
                get
                {
                    return StyleProvider.ActiveStyle.BorderColor;
                }
            }

            /// <summary>
            /// Gets the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.
            /// </summary>
            public override Color MenuItemBorder
            {
                get
                {
                    return StyleProvider.ActiveStyle.ControlBackHoverColor;
                }
            }

            /// <summary>
            /// Gets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.
            /// </summary>
            public override Color ImageMarginGradientBegin
            {
                get
                {
                    return StyleProvider.ActiveStyle.ControlBackColor;
                }
            }

            /// <summary>
            /// Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.
            /// </summary>
            public override Color ImageMarginGradientMiddle
            {
                get
                {
                    return StyleProvider.ActiveStyle.ControlBackColor;
                }
            }

            /// <summary>
            /// Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.
            /// </summary>
            public override Color ImageMarginGradientEnd
            {
                get
                {
                    return StyleProvider.ActiveStyle.ControlBackColor;
                }
            }

            /// <summary>
            /// Gets the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.
            /// </summary>
            public override Color ToolStripDropDownBackground
            {
                get
                {
                    return StyleProvider.ActiveStyle.ControlBackColor;
                }
            }
        }
    }
}
