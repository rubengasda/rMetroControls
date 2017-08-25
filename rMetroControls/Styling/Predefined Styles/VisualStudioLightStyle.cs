using System.Drawing;

namespace RuerteControls
{
    public partial class Style
    {
        /// <summary>
        /// The visual studio light style internal variable.
        /// </summary>
        private static Style visualStudioLightStyle = new Style()
        {
            Name = "Visual Studio Light",
            AccentColor = Color.FromArgb(0, 122, 204),
            AccentHoverColor = Color.FromArgb(28, 151, 234),
            BackColor = Color.FromArgb(238, 238, 238),
            BackHoverColor = Color.FromArgb(201, 222, 245),
            ForegroundColor = Color.FromArgb(66, 66, 66),
            AccentForegroundColor = Color.FromArgb(255, 255, 255),
            BorderColor = Color.FromArgb(204, 206, 219),
            ControlBackColor = Color.FromArgb(246, 246, 246),
            ControlBackHoverColor = Color.FromArgb(201, 222, 245)
        };

        /// <summary>
        /// Gets the visual studio light style.
        /// </summary>
        /// <value>
        /// The visual studio light style.
        /// </value>
        public static Style VisualStudioLightStyle
        {
            get
            {
                return Style.visualStudioLightStyle;
            }
        }
    }
}
