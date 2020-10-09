using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Ekstrand.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public class ControlElements : IControlElements
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ControlElements()
        {
            BackColor = Color.Empty;
            ForeColor = Color.Empty;
            Bounds = new Rectangle();
            DisabledTextColor = Color.Empty;
            Font = SystemFonts.DefaultFont;
            Graphics = null;
            GroupBoxStyle = GroupBoxStyle.Standard;
            Text = string.Empty;
        }
        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets the size and location of the control including its nonclient elements, in pixels, relative to the parent control.
        /// </summary>
        public Rectangle Bounds { get; set; }
        
        /// <summary>
        /// Gets or sets the disabled text color.
        /// </summary>
        public Color DisabledTextColor { get; set; }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        public Color ForeColor { get; set; }

        /// <summary>
        /// The Graphics used to draw the group box.
        /// </summary>
        public Graphics Graphics { get; set; }
        
        /// <summary>
        /// The styles used to draw the group box.
        /// </summary>
        public GroupBoxStyle GroupBoxStyle { get; set; }

        /// <summary>
        /// The String to draw with the group box.
        /// </summary>
        public string Text { get; set; }

        #endregion Properties

        
    }
}
