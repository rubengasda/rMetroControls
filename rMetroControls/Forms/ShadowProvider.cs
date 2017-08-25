using System;
using System.Windows.Forms;

namespace rMetroControls
{
    /// <summary>
    /// Provides a Shadow for an instance of a styled form.
    /// </summary>
    public abstract class ShadowProvider
        : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShadowProvider"/> class.
        /// </summary>
        /// <param name="targetForm">The target form.</param>
        public ShadowProvider(Form targetForm)
        {
            this.TargetForm = targetForm;
        }

        /// <summary>
        /// Gets the target form.
        /// </summary>
        /// <value>
        /// The target form.
        /// </value>
        public Form TargetForm { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ShadowProvider"/> is supported.
        /// </summary>
        /// <value>
        ///   <c>true</c> if supported; otherwise, <c>false</c>.
        /// </value>
        public abstract bool Supported { get; }

        /// <summary>
        /// Sets the shadow effect up.
        /// </summary>
        public abstract void Setup();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        { }
    }
}
