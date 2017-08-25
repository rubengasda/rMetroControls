using RuerteControls.Win32Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace RuerteControls
{
    /// <summary>
    /// A headerless form with AeroSnap capability, mouse dragging and the capability to use different shadows.
    /// </summary>
    public class StyledHeaderlessForm
        : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyledHeaderlessForm"/> class.
        /// </summary>
        public StyledHeaderlessForm()
            : base()
        {
            //Basic settings
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Text = "";
            this.ControlBox = false;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.ResizeRedraw = true;
            this.DoubleBuffered = true;

            this.BackColor = StyleProvider.ActiveStyle.BackColor;
            this.BorderColor = StyleProvider.ActiveStyle.AccentColor;

            this.EnableDragging = true;

            //Default ShadowProvider
            this.ShadowProvider = new NoShadowProvider(this);
        }

        private Color borderColor;
        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>
        /// The color of the border.
        /// </value>
        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                this.borderColor = value;

                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the shadow provider.
        /// </summary>
        /// <value>
        /// The shadow provider.
        /// </value>
        public ShadowProvider ShadowProvider { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether dragging the window is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if dragging the window is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool EnableDragging { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="StyledHeaderlessForm"/> is movable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if movable; otherwise, <c>false</c>.
        /// </value>
        public bool Movable { get; set; }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //Draw Background
            e.Graphics.Clear(this.BackColor);

            //Draw Border
            e.Graphics.DrawRectangle(new Pen(this.BorderColor), new Rectangle(this.DisplayRectangle.Location, this.DisplayRectangle.Size - new Size(1, 1)));
            
            base.OnPaint(e);
        }

        /// <summary>
        /// Processes incoming Windows Messages.
        /// </summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
        protected override void WndProc(ref Message m)
        {
            if (!this.DesignMode)
            {
                switch (m.Msg)
                {
                    //Remove NoClientArea
                    case NativeConstants.WM_NCCALCSIZE:
                        if (m.WParam == IntPtr.Zero)
                        {
                            NativeStructs.RECT rect = (NativeStructs.RECT)m.GetLParam(typeof(NativeStructs.RECT));
                            Rectangle drawingRect = rect.ToRectangle();
                            drawingRect.Inflate(8, 8);
                            Marshal.StructureToPtr(new NativeStructs.RECT(drawingRect), m.LParam, true);
                        }
                        else
                        {
                            NativeStructs.NCCALCSIZE_PARAMS calcSizeParams = (NativeStructs.NCCALCSIZE_PARAMS)m.GetLParam(typeof(NativeStructs.NCCALCSIZE_PARAMS));
                            Rectangle drawingRect = calcSizeParams.rgrc0.ToRectangle();
                            drawingRect.Inflate(8, 8);
                            calcSizeParams.rgrc0 = new NativeStructs.RECT(drawingRect);
                            Marshal.StructureToPtr(calcSizeParams, m.LParam, true);
                        }
                        m.Result = IntPtr.Zero;
                        break;
                    //Prevent activation of the nonclientarea
                    case NativeConstants.WM_NCACTIVATE:
                        m.Result = IntPtr.Zero;
                        return;
                }

                base.WndProc(ref m);

                switch (m.Msg)
                {
                    case NativeConstants.WM_SIZE:
                        m.Result = IntPtr.Zero;
                        return;
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.EnableDragging)
            {
                //Release current capture
                NativeMethods.ReleaseCapture();

                //Start dragging process
                Message message = Message.Create(this.Handle, NativeConstants.WM_NCLBUTTONDOWN, new IntPtr(NativeConstants.HT_CAPTION), IntPtr.Zero);

                this.WndProc(ref message);
            }
            
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Activated" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnActivated(EventArgs e)
        {
            if (this.ShadowProvider.Supported)
            {
                //Refresh ShadowProvider
                this.ShadowProvider.Setup();
            }
            
            base.OnActivated(e);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.ShadowProvider.Dispose();
            }
            
            base.Dispose(disposing);
        }
    }
}