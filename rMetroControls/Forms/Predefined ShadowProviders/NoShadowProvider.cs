using System.Windows.Forms;

namespace rMetroControls
{
    /// <summary>
    /// A shadow provider that doesn't do anything.
    /// </summary>
    public class NoShadowProvider
        : ShadowProvider
    {
        public NoShadowProvider(Form targetForm)
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
                return true;
            }
        }

        /// <summary>
        /// Sets the shadow effect up.
        /// </summary>
        public override void Setup()
        { }
    }
}
