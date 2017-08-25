using System;

namespace RuerteControls
{
    /// <summary>
    /// Provides the color schemes for the controls in this library.
    /// </summary>
    public static class StyleProvider
    {
        /// <summary>
        /// The active style internal variable.
        /// </summary>
        private static Style activeStyle = Style.VisualStudioLightStyle; //<- Set default style here - ignored when debugging.
        /// <summary>
        /// Gets or sets the active style.
        /// </summary>
        /// <value>
        /// The active style.
        /// </value>
        public static Style ActiveStyle
        {
            get
            {
                return StyleProvider.activeStyle;
            }
            set
            {
                StyleProvider.activeStyle = value;
                StyleProvider.OnActiveStyleChanged(); //Fire event to tell every control that the style refreshed.
            }
        }

        /// <summary>
        /// Occurs when the active style changed.
        /// </summary>
        public static event EventHandler ActiveStyleChanged;
        /// <summary>
        /// Raises the ActiveStyleChanged event.
        /// </summary>
        public static void OnActiveStyleChanged()
        {
            if (StyleProvider.ActiveStyleChanged != null)
            {
                StyleProvider.ActiveStyleChanged(null, EventArgs.Empty);
            }
        }
    }
}
