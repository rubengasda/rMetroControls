using System.Drawing;

namespace rMetroControls
{
    /// <summary>
    /// Provides color styling for the controls in this library.
    /// </summary>
    public partial class Style
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the default back color.
        /// </summary>
        /// <value>
        /// The default back color.
        /// </value>
        public Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets the default hover version of the back color.
        /// </summary>
        /// <value>
        /// The default hover version of the back color.
        /// </value>
        public Color BackHoverColor { get; set; }

        /// <summary>
        /// Gets or sets the default accent color.
        /// </summary>
        /// <value>
        /// The default accent color.
        /// </value>
        public Color AccentColor { get; set; }

        /// <summary>
        /// Gets or sets the default hover version of the accent color.
        /// </summary>
        /// <value>
        /// The default hover version of the accent color.
        /// </value>
        public Color AccentHoverColor { get; set; }

        /// <summary>
        /// Gets or sets the default foreground color (for text, icons, ...).
        /// </summary>
        /// <value>
        /// The default foreground text color.
        /// </value>
        public Color ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the default accent foreground color (for text, icons, ...).
        /// </summary>
        /// <value>
        /// The default accent foreground text color.
        /// </value>
        public Color AccentForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the default border color.
        /// </summary>
        /// <value>
        /// The default border color.
        /// </value>
        public Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the default control background color.
        /// </summary>
        /// <value>
        /// The default control background color.
        /// </value>
        public Color ControlBackColor { get; set; }

        /// <summary>
        /// Gets or sets the default hover version of the control background color.
        /// </summary>
        /// <value>
        /// The default hover version of the control background color.
        /// </value>
        public Color ControlBackHoverColor { get; set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left object.</param>
        /// <param name="right">The right object.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Style left, Style right)
        {
            return ((object)left != null) == ((object)right != null)
                    || ((object)left != null)
                        && left.BackColor == right.BackColor
                        && left.BackHoverColor == right.BackHoverColor
                        && left.AccentColor == right.AccentColor
                        && left.AccentHoverColor == right.AccentHoverColor
                        && left.ForegroundColor == right.ForegroundColor
                        && left.AccentForegroundColor == right.AccentForegroundColor
                        && left.BorderColor == right.BorderColor
                        && left.ControlBackColor == right.ControlBackColor
                        && left.ControlBackHoverColor == right.ControlBackHoverColor;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left object.</param>
        /// <param name="right">The right object.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Style left, Style right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return (obj != null) && (obj is Style) && (Style)obj == this;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.BackColor.GetHashCode()
                 ^ this.BackHoverColor.GetHashCode()
                 ^ this.AccentColor.GetHashCode()
                 ^ this.AccentHoverColor.GetHashCode()
                 ^ this.ForegroundColor.GetHashCode()
                 ^ this.AccentForegroundColor.GetHashCode()
                 ^ this.BorderColor.GetHashCode()
                 ^ this.ControlBackColor.GetHashCode()
                 ^ this.ControlBackHoverColor.GetHashCode();
        }
    }
}
