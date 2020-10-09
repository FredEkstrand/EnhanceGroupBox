using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Ekstrand.Windows.Forms
{
    /// <summary>
    /// Interface properties for rendering group box header area.
    /// </summary>
    public interface IHeaderElements
    {
        /// <summary>
        /// Gets or sets color of the text background.
        /// </summary>
        Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets color of the border in the text header area.
        /// </summary>
        Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the corners to have a rounded edge.
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
        /// Gets or sets the gradient end color.
        /// </summary>
        Color GradientEndColor { get; set; }

        /// <summary>
        /// Gets or sets the gradient mode.
        /// </summary>
        EnhanceGroupBoxGradientMode GradientMode { get; set; }

        /// <summary>
        /// Gets or sets the gradient start color.
        /// </summary>
        Color GradientStartColor { get; set; }

        /// <summary>
        /// Gets or sets the radius for each corner.
        /// </summary>
        int Radius { get; set; }

        /// <summary>
        /// Gets or sets the text aligment.
        /// </summary>
        BorderTextAlignment TextAlignment { get; set; }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        int Width { get; set; }

        #region Methods

        /// <summary>
        /// Raise property change event.
        /// </summary>
        /// <param name="types">PropertyChangeType indicator.</param>
        void OnRaisePropertyChanged(PropertyChangeTypes types);

        #endregion Methods

        #region Delegates + Events

        /// <summary>
        /// Occurs when the value for one of the class property changes.
        /// </summary>
        event EventHandler RaisePropertyChanged;

        #endregion Delegates + Events
    }
}