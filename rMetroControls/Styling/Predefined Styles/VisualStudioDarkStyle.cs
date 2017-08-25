using System.Drawing;

namespace rMetroControls
{
    public partial class Style
    {
        /// <summary>
        /// The visual studio dark style internal variable.
        /// </summary>
        private static Style visualStudioDarkStyle = new Style()
        {
            Name = "Visual Studio Dark",
            AccentColor = Color.FromArgb(0, 122, 204),
            AccentHoverColor = Color.FromArgb(28, 151, 234),
            BackColor = Color.FromArgb(45, 45, 48),
            BackHoverColor = Color.FromArgb(62, 62, 64),
            ForegroundColor = Color.FromArgb(241, 241, 241),
            AccentForegroundColor = Color.FromArgb(241, 241, 241),
            BorderColor = Color.FromArgb(51, 51, 55),
            ControlBackColor = Color.FromArgb(27, 27, 28),
            ControlBackHoverColor = Color.FromArgb(62, 62, 64)
        };

        /// <summary>
        /// Gets the visual studio dark style.
        /// </summary>
        /// <value>
        /// The visual studio dark style.
        /// </value>
        public static Style VisualStudioDarkStyle
        {
            get
            {
                return Style.visualStudioDarkStyle;
            }
        }
    }
}
