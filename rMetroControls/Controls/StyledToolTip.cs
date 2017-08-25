using rMetroControls.Win32Interop;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace rMetroControls
{
    /// <summary>
    /// A styled version of the normal ToolTip.
    /// </summary>
    [DesignerCategory("Code")]
    [DisplayName("Styled ToolTip")]
    [Description("A ToolTip that uses the style provided by the StyleProvider class.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ToolTip))]
    public class StyledToolTip
        : ToolTip
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyledToolTip"/> class.
        /// </summary>
        public StyledToolTip()
            : base()
        {
            //Default settings
            this.ShowAlways = true;
            this.OwnerDraw = true;
            this.DisplayShadow = false;

            this.Popup += (s, e) =>
                {
                    //Set the tooltip to a nice size like in VS.
                    e.ToolTipSize = new Size(e.ToolTipSize.Width + 15, e.ToolTipSize.Height + 10);

                    //Remove the shadow via Win32 interop (I'm disabling the DROPSHADOW-Classstyle)
                    try
                    {
                        if (!this.DisplayShadow && this.Handle != IntPtr.Zero)
                        {
                            HandleRef hRef = new HandleRef(this, this.Handle);
                            IntPtr exStyle = NativeWrappers.GetClassLongPtr(hRef, NativeConstants.GCL_STYLE);
                            if (((int)exStyle & NativeConstants.CS_DROPSHADOW) == NativeConstants.CS_DROPSHADOW)
                            {
                                exStyle = new IntPtr((int)exStyle & ~NativeConstants.CS_DROPSHADOW);
                                NativeWrappers.SetClassLongPtr(hRef, NativeConstants.GCL_STYLE, exStyle);
                            }
                        }
                    }
                    catch
                    {
                        //If anything goes wrong here, for example because we're using this tooltip on an unsupported platform, we don't want the application to crash but just to suppress the exception.
                    }
                };

            //Render the tooltip
            this.Draw += (s, e) =>
                {
                    e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit; //Make the text look nice
                    e.Graphics.FillRectangle(new SolidBrush(StyleProvider.ActiveStyle.BackColor), e.Bounds); //Draw the background
                    e.Graphics.DrawRectangle(new Pen(StyleProvider.ActiveStyle.BorderColor), new Rectangle(e.Bounds.Location, new Size(e.Bounds.Width - 1, e.Bounds.Height - 1))); //Draw the border
                    e.Graphics.DrawString(e.ToolTipText, e.Font, new SolidBrush(StyleProvider.ActiveStyle.ForegroundColor), e.Bounds, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisWord }); //Draw the text
                };
        }

        /// <summary>
        /// Gets or sets a value indicating whether a shadow should be displayed underneath this tooltip.
        /// </summary>
        /// <value>
        ///   <c>true</c> if a shadow should be displayed; otherwise, <c>false</c>.
        /// </value>
        [Category("Appearance")]
        [Description("Determines wheather a shadow should be displayed underneath this ToolTip.")]
        [DefaultValue(false)]
        public bool DisplayShadow { get; set; }

        /// <summary>
        /// Gets the internally used handle of this tooltip.
        /// </summary>
        /// <value>
        /// The handle.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected IntPtr Handle
        {
            get
            {
                try
                {
                    //Try to get the value of the internal "Handle" variable that is usually hidden for us via Reflection.
                    return (IntPtr)typeof(ToolTip).InvokeMember("Handle", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty, null, this, null);
                }
                catch
                {
                    //If anything went wrong, we just return an invalid handle.
                    return IntPtr.Zero;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a ToolTip window is displayed, even when its parent control is not active.
        /// </summary>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [DefaultValue(true)]
        [Browsable(false)]
        public new bool ShowAlways
        {
            get { return base.ShowAlways; }
            set { base.ShowAlways = true; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the ToolTip is drawn by the operating system or by code that you provide.
        /// </summary>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
        /// </PermissionSet>
        [DefaultValue(true)]
        [Browsable(false)]
        public new bool OwnerDraw
        {
            get { return base.OwnerDraw; }
            set { base.OwnerDraw = true; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the ToolTip should use a balloon window.
        /// </summary>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Browsable(false)]
        public new bool IsBalloon
        {
            get { return base.IsBalloon; }
            set { base.IsBalloon = false; }
        }

        /// <summary>
        /// Gets or sets the background color for the ToolTip.
        /// </summary>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Browsable(false)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>
        /// Gets or sets the foreground color for the ToolTip.
        /// </summary>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Browsable(false)]
        public new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        /// <summary>
        /// Gets or sets a title for the ToolTip window.
        /// </summary>
        [Browsable(false)]
        public new string ToolTipTitle
        {
            get { return base.ToolTipTitle; }
            set { base.ToolTipTitle = ""; }
        }

        /// <summary>
        /// Gets or sets a value that defines the type of icon to be displayed alongside the ToolTip text.
        /// </summary>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Browsable(false)]
        public new ToolTipIcon ToolTipIcon
        {
            get { return base.ToolTipIcon; }
            set { base.ToolTipIcon = ToolTipIcon.None; }
        }
    }
}
