using rMetroControls.Win32Interop;
using System;
using System.Windows.Forms;

namespace rMetroControls
{
    /// <summary>
    /// A shadow provider that uses an aero-based mechanism to create a bold, hardware-accelerated shadow.
    /// </summary>
    public class AeroShadowProvider
        : ShadowProvider
    {
        public AeroShadowProvider(Form targetForm)
            : base(targetForm)
        { }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ShadowProvider" /> is supported.
        /// </summary>
        /// <value>
        ///   <c>true</c> if supported; otherwise, <c>false</c>.
        /// </value>
        public override bool Supported
        {
            get
            {
                return Environment.OSVersion.Version.Major > 5
                    && SystemInformation.IsDropShadowEnabled
                    && NativeWrappers.DwmIsCompositionEnabled();
            }
        }

        /// <summary>
        /// Sets the shadow effect up.
        /// </summary>
        public override void Setup()
        {
            //Extend the frame into the client area on the bottom by 1 pixel so we get our nice, bold shadow effect.
            int val = 2;
            NativeMethods.DwmSetWindowAttribute(this.TargetForm.Handle, 2, ref val, 4);
            var margins = new NativeStructs.MARGINS(0, 0, 0, 1);
            NativeMethods.DwmExtendFrameIntoClientArea(this.TargetForm.Handle, ref margins);
        }
    }
}
