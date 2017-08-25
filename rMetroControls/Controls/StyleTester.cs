using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace RuerteControls
{
    /// <summary>
    /// A Control for testing Styles
    /// </summary>
    [DesignerCategory("Code")]
    [DisplayName("Style Tester")]
    [Description("A Control used to display the colors of the current style provided by the StyleProvider class.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Control))]
    public class StyleTester
        : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleTester"/> class.
        /// </summary>
        public StyleTester()
        {
            //Set some styles to allow faster drawing
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            //Make it display the current style
            this.Style = StyleProvider.ActiveStyle;
            StyleProvider.ActiveStyleChanged += (s, e) => this.Style = StyleProvider.ActiveStyle;
        }

        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new Size(200, 100);
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
                if (this.Created)
                {
                    //Invalidate to refresh the displayed style
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //Make rendered text look good.
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            //Declare helper variables
            StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
            SolidBrush foreBrush = new SolidBrush(this.Style.ForegroundColor);
            SolidBrush accentForeBrush = new SolidBrush(this.Style.AccentForegroundColor);
            int widthSplit = (this.Width - 8) / 2;
            int heightSplit = (this.Height - 8 - 20) / 3;

            //Draw the background
            e.Graphics.Clear(this.Style.ControlBackColor);
            
            //Draw the accent color field
            Rectangle accentRect = new Rectangle(4, 20 + 4, widthSplit, heightSplit);
            e.Graphics.FillRectangle(new SolidBrush(this.Style.AccentColor), accentRect);
            e.Graphics.DrawString("Accent", this.Font, accentForeBrush, accentRect, format);

            //Draw the accent-hover color field
            Rectangle accentHoverRect = new Rectangle(widthSplit + 4, 20 + 4, widthSplit, heightSplit);
            e.Graphics.FillRectangle(new SolidBrush(this.Style.AccentHoverColor), accentHoverRect);
            e.Graphics.DrawString("Accent Hover", this.Font, accentForeBrush, accentHoverRect, format);

            //Draw the control background color field
            Rectangle controlBackRect = new Rectangle(4, heightSplit + 20 + 4, widthSplit, heightSplit);
            e.Graphics.FillRectangle(new SolidBrush(this.Style.ControlBackColor), controlBackRect);
            e.Graphics.DrawString("Control Back", this.Font, foreBrush, controlBackRect, format);

            //Draw the control background-hover color field
            Rectangle controlBackHoverRect = new Rectangle(widthSplit + 4, heightSplit + 20 + 4, widthSplit, heightSplit);
            e.Graphics.FillRectangle(new SolidBrush(this.Style.ControlBackHoverColor), controlBackHoverRect);
            e.Graphics.DrawString("Control Back H.", this.Font, foreBrush, controlBackHoverRect, format);

            //Draw the background color field
            Rectangle backRect = new Rectangle(4, heightSplit * 2 + 20 + 4, widthSplit, heightSplit);
            e.Graphics.FillRectangle(new SolidBrush(this.Style.BackColor), backRect);
            e.Graphics.DrawString("Background", this.Font, foreBrush, backRect, format);

            //Draw the background-hover color field
            Rectangle backHoverRect = new Rectangle(widthSplit + 4, heightSplit * 2 + 20 + 4, widthSplit, heightSplit);
            e.Graphics.FillRectangle(new SolidBrush(this.Style.BackHoverColor), backHoverRect);
            e.Graphics.DrawString("Back Hover", this.Font, foreBrush, backHoverRect, format);

            //Draw the border
            e.Graphics.DrawRectangle(new Pen(this.Style.BorderColor), new Rectangle(this.DisplayRectangle.Location, this.DisplayRectangle.Size - new Size(1, 1)));

            //Draw the style's name
            e.Graphics.DrawString(this.Style.Name, this.Font, foreBrush, new Rectangle(4, 4, widthSplit * 2, 16), format);

            //Submit call to the base class
            base.OnPaint(e);
        }
    }
}
