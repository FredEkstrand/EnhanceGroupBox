using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Ekstrand.Windows.Forms
{
    /// <summary>
    /// Interface properties for border appearance
    /// </summary>
    public interface IBorderElements
    {
        /// <summary>
        /// In Excitative style the border back color.
        /// </summary>
        Color BackColor { get; set; }

        /// <summary>
        /// Color of the border.
        /// </summary>
        Color BorderColor { get; set; }

        /// <summary>
        /// Corners to have a rounded appearance.
        /// </summary>
        BorderCorners BorderCorners { get; set; }

        /// <summary>
        /// Specifies the type of graphic shape to use on both ends of each dash in a dashed line.
        /// </summary>
        DashCap DashCap { get; set; }

        /// <summary>
        /// Gets or sets the distance from the start of a line to the beginning of a dash pattern.
        /// </summary>
        float DashOffset { get; set; }

        /// <summary>
        /// Gets or sets an array of custom dashes and spaces.
        /// </summary>
        float[] DashPattern { get; set; }

        /// <summary>
        /// Gets or sets the style used for dashed lines drawn.
        /// </summary>
        DashStyle DashStyle { get; set; }

        /// <summary>
        /// Gets or sets the radius for each corner.
        /// </summary>
        int Radius { get; set; }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// Occurs when the value for one of the class property changes.
        /// </summary>
        event EventHandler RaisePropertyChanged;

        /// <summary>
        /// Raise property change event.
        /// </summary>
        /// <param name="types">PropertyChangeType indicator.</param>
        void OnRaisePropertyChanged(PropertyChangeTypes types);
    }
}