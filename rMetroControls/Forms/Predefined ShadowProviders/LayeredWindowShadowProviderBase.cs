using rMetroControls.Win32Interop;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace rMetroControls
{
    /// <summary>
    /// A base class for creating shadows based on the layered window principle.
    /// </summary>
    public abstract class LayeredWindowShadowProviderBase
        : ShadowProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LayeredWindowShadowProviderBase"/> class.
        /// </summary>
        /// <param name="targetForm">The target form.</param>
        public LayeredWindowShadowProviderBase(Form targetForm)
            : base(targetForm)
        {
            //Set the shadow form up
            this.ShadowForm = new LayeredWindow() { Bounds = this.CalculateShadowBounds() };

            //Make it the owner of the target form
            if (this.TargetForm.Owner != null)
            {
                this.ShadowForm.Owner = this.TargetForm.Owner;
            }
            this.TargetForm.Owner = this.ShadowForm;
            
            //Add event handlers
            bool bringingToFront = false;
            this.ShadowForm.Deactivate += (s, e) =>
            {
                bringingToFront = true;
            };

            this.TargetForm.Activated += (s, e) =>
            {
                if (this.ShadowForm.Visible)
                {
                    this.ShadowForm.Update();
                }
                if (bringingToFront)
                {
                    this.ShadowForm.Visible = true;
                    bringingToFront = false;
                    return;
                }
                this.ShadowForm.BringToFront();
            };

            this.TargetForm.VisibleChanged += (s, e) =>
            {
                this.ShadowForm.Visible = this.TargetForm.Visible && this.TargetForm.WindowState != FormWindowState.Minimized;
                this.ShadowForm.Update();
            };

            this.TargetForm.Move += (s, e) =>
            {
                if (!this.TargetForm.Visible | targetForm.WindowState != FormWindowState.Normal)
                {
                    this.ShadowForm.Visible = false;
                }
                else
                {
                    //this.ShadowForm.Visible = true;
                    this.ShadowForm.Bounds = this.CalculateShadowBounds();
                }
            };

            //Maybe: TargetForm resize => clear shadow

            this.TargetForm.SizeChanged += (s, e) =>
            {
                this.ShadowForm.Bounds = this.CalculateShadowBounds();

                this.RefreshShadow();
                this.ShadowForm.Visible = true;
            };

            this.TargetForm.ResizeEnd += (s, e) =>
            {
                this.ShadowForm.Bounds = this.CalculateShadowBounds();

                this.RefreshShadow();
                this.ShadowForm.Visible = true;
            };

            this.ShadowForm.Load += (s, e) =>
            {
                this.RefreshShadow();
                this.ShadowForm.Visible = true;
            };

            this.ShadowForm.Paint += (s, e) =>
            {
                this.RefreshShadow();
            };

            this.RefreshShadow();
            this.ShadowForm.Show();
        }

        /// <summary>
        /// Gets the padding.
        /// </summary>
        /// <value>
        /// The padding.
        /// </value>
        public abstract Padding Padding { get; }

        /// <summary>
        /// Calculates the shadow bounds.
        /// </summary>
        /// <returns>A rectangle that represents the shadow bounds.</returns>
        protected Rectangle CalculateShadowBounds()
        {
            return new Rectangle(
                this.TargetForm.Bounds.X - this.Padding.Left,
                this.TargetForm.Bounds.Y - this.Padding.Top,
                this.TargetForm.Bounds.Width + this.Padding.Vertical,
                this.TargetForm.Bounds.Height + this.Padding.Horizontal);
        }

        /// <summary>
        /// Gets the form used for displaying the shadow.
        /// </summary>
        /// <value>
        /// The shadow form.
        /// </value>
        public LayeredWindow ShadowForm { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="LayeredWindowGlowShadowProvider"/> is supported.
        /// </summary>
        /// <value>
        ///   <c>true</c> if supported; otherwise, <c>false</c>.
        /// </value>
        public override bool Supported
        {
            get
            {
                return OSFeature.Feature.IsPresent(OSFeature.LayeredWindows);
            }
        }

        /// <summary>
        /// Draws the shadow.
        /// </summary>
        /// <param name="args">The <see cref="PaintEventArgs"/> instance containing the data necessary for painting the shadow.</param>
        public abstract void DrawShadow(PaintEventArgs args);

        /// <summary>
        /// Refreshes the shadow.
        /// </summary>
        protected void RefreshShadow()
        {
            //Test if the shadow should be refreshed
            if (this.TargetForm.Visible && this.TargetForm.WindowState == FormWindowState.Normal) //!= FormWindowState.Minimized
            {
                //Create bitmap and graphics objects for the shadow
                Bitmap bitmap = new Bitmap(this.ShadowForm.Width, this.ShadowForm.Height, PixelFormat.Format32bppArgb);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    //Let the inherited class paint the shadow
                    this.DrawShadow(new PaintEventArgs(graphics, new Rectangle(new Point(0, 0), bitmap.Size)));
                }
                //Display the Bitmap
                this.ShadowForm.SetBitmap(bitmap);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.TargetForm.Owner = null;
                this.ShadowForm.Dispose();
            }
            
            base.Dispose(disposing);
        }


        
        /// <summary>
        /// A layered window with methods for custom drawing.
        /// </summary>
        public class LayeredWindow
            : Form
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="LayeredWindow"/> class.
            /// </summary>
            public LayeredWindow()
            {
                this.MinimizeBox = false;
                this.MaximizeBox = false;
                this.ShowInTaskbar = false;
                this.ShowIcon = false;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Text = "Layered Glow Window";
            }

            /// <summary>
            /// Creates the window parameters.
            /// </summary>
            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;

                    //Make the form a clickthrough, layered window that cannot be activated.
                    cp.ExStyle |= NativeConstants.WS_EX_LAYERED | NativeConstants.WS_EX_TRANSPARENT | NativeConstants.WS_EX_NOACTIVATE;

                    return cp;
                }
            }

            /// <summary>
            /// Sets the bitmap of this layered window.
            /// </summary>
            /// <param name="bitmap">The bitmap.</param>
            public void SetBitmap(Bitmap bitmap)
            {
                //Test if the bitmap is compatible
                if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                {
                    throw new ApplicationException("The bitmap must be 32bpp with alpha-channel.");
                }

                //Get the device contexts 
                IntPtr screenDc = NativeMethods.GetDC(IntPtr.Zero);
                IntPtr memDc = NativeMethods.CreateCompatibleDC(screenDc);
                IntPtr hBitmap = IntPtr.Zero;
                IntPtr hOldBitmap = IntPtr.Zero;

                try
                {
                    //Get handle to the new bitmap and select it into the current device context
                    hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                    hOldBitmap = NativeMethods.SelectObject(memDc, hBitmap);

                    //Set parameters for layered window update
                    NativeStructs.SIZE newSize = new NativeStructs.SIZE(bitmap.Width, bitmap.Height);
                    NativeStructs.POINT sourceLocation = new NativeStructs.POINT(0, 0);
                    NativeStructs.POINT newLocation = new NativeStructs.POINT(this.Left, this.Top);
                    NativeStructs.BLENDFUNCTION blend = new NativeStructs.BLENDFUNCTION();
                    blend.BlendOp = NativeConstants.AC_SRC_OVER;
                    blend.BlendFlags = 0;
                    blend.SourceConstantAlpha = 255;
                    blend.AlphaFormat = NativeConstants.AC_SRC_ALPHA;

                    //Update the window
                    NativeMethods.UpdateLayeredWindow(this.Handle, screenDc, ref newLocation, ref newSize, memDc, ref sourceLocation, 0, ref blend, NativeConstants.ULW_ALPHA);
                }
                finally
                {
                    //Release the device context
                    NativeMethods.ReleaseDC(IntPtr.Zero, screenDc);
                    if (hBitmap != IntPtr.Zero)
                    {
                        NativeMethods.SelectObject(memDc, hOldBitmap);
                        NativeMethods.DeleteObject(hBitmap);
                    }

                    //Delete the device context
                    NativeMethods.DeleteDC(memDc);
                }
            }
        }
    }
}