using System;
using System.Drawing;

namespace Ekstrand.Windows.Forms
{
    /// <summary>
    /// Interface properties for control appearance
    /// </summary>
    public interface IControlElements
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        Color BackColor
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Rectangle Bounds
        { get; set; }

        /// <summary>
        /// Gets or sets the disabled text color.
        /// </summary>
        Color DisabledTextColor
        { get; set; }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        Font Font
        { get; set; }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        Color ForeColor
        { get; set; }

        /// <summary>
        /// The Graphics used to draw the group box.
        /// </summary>
        Graphics Graphics
        { get; set; }

        /// <summary>
        /// The styles used to draw the group box.
        /// </summary>
        GroupBoxStyle GroupBoxStyle
        { get; set; }

        /// <summary>
        /// The String to draw with the group box.
        /// </summary>
        String Text
        { get; set; }
        #endregion Properties

        
    }
}
