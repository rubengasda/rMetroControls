using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace rMetroControls
{
    /// <summary>
    /// A glow shadow based on the layered window principle.
    /// </summary>
    public class LayeredWindowGlowShadowProvider
        : LayeredWindowShadowProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LayeredWindowGlowShadowProvider"/> class.
        /// </summary>
        /// <param name="targetForm">The target form.</param>
        public LayeredWindowGlowShadowProvider(Form targetForm)
            : base(targetForm)
        { }

        /// <summary>
        /// Gets the padding.
        /// </summary>
        /// <value>
        /// The padding.
        /// </value>
        public override Padding Padding
        {
            get
            {
                return new Padding(8);
            }
        }
        
        /// <summary>
        /// The internal glow color variable.
        /// </summary>
        private Color glowColor = Color.Black;
        /// <summary>
        /// Gets or sets the color of the glow.
        /// </summary>
        /// <value>
        /// The color of the glow.
        /// </value>
        public virtual Color GlowColor
        {
            get
            {
                return this.glowColor;
            }
            set
            {
                this.glowColor = value;
                this.RefreshShadow();
            }
        }

        /// <summary>
        /// The base opacity internal variable.
        /// </summary>
        private byte baseOpacity = 255;
        /// <summary>
        /// Gets or sets the base opacity.
        /// </summary>
        /// <value>
        /// The base opacity.
        /// </value>
        public virtual byte BaseOpacity
        {
            get
            {
                return this.baseOpacity;
            }
            set
            {
                this.baseOpacity = value;

                this.RefreshShadow();
            }
        }

        /// <summary>
        /// The shadow factor internal variable.
        /// </summary>
        private double shadowFactor = .75;
        /// <summary>
        /// Gets or sets the shadow factor responsible for the opacity decay of the shadow.
        /// </summary>
        /// <value>
        /// The shadow factor.
        /// </value>
        public virtual double ShadowFactor
        {
            get
            {
                return this.shadowFactor;
            }
            set
            {
                this.shadowFactor = value;

                this.RefreshShadow();
            }
        }

        /// <summary>
        /// Draws the shadow.
        /// </summary>
        /// <param name="args">The <see cref="PaintEventArgs" /> instance containing the data necessary for painting the shadow.</param>
        public override void DrawShadow(PaintEventArgs args)
        {
            double baseOpacity = this.BaseOpacity; //The shadow's opacity
            for (int i = this.Padding.All; i >= 0; i--) //Loop from the inside to the outside
            {
                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb((int)baseOpacity, this.GlowColor))) //Create a brush with the current values
                {
                    //Fill a rounded rectangle with the brush
                    args.Graphics.FillPath(shadowBrush, this.CreateRoundedRectangle(new Rectangle(i, i, this.ShadowForm.Width - 2 * i, this.ShadowForm.Height - 2 * i), (this.Padding.All - i) * 1.2f));
                }

                //Reduce the opacity exponentially
                baseOpacity = Math.Pow(baseOpacity, this.ShadowFactor);
            }
        }

        /// <summary>
        /// Sets the shadow effect up.
        /// </summary>
        public override void Setup()
        {
            this.RefreshShadow();
        }

        /// <summary>
        /// Creates a rounded rectangle.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <param name="radius">The radius.</param>
        /// <returns></returns>
        private GraphicsPath CreateRoundedRectangle(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2;

            if (radius < 1)
            {
                path.AddRectangle(rect);
            }
            else
            {
                path.AddArc(rect.X + rect.Width - d, rect.Y, d, d, 270, 90);
                path.AddArc(rect.X + rect.Width - d, rect.Y + rect.Height - d, d, d, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - d, d, d, 90, 90);
                path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            }

            return path;
        }
    }
}