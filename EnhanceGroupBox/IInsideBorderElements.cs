using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ekstrand.Windows.Forms
{
    /// <summary>
    /// Interface properties for rendering group box inside border area.
    /// </summary>
    public interface IInsideBorderElements
    {
        #region Properties

        /// <summary>
        /// Gets or sets the inside border area color.
        /// </summary>
        Color BackColor { get; set; }

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
        /// Gets or sets the image inside the border area.
        /// </summary>
        Image Image { get; set; }

        /// <summary>
        /// Gets or sets the image layout.
        /// </summary>
        ImageLayout ImageLayout { get; set; }

        #endregion Properties

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