using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ekstrand.Windows.Forms
{
    /// <summary>
    /// Properties grouped for rendering group box inside border area.
    /// </summary>
    [TypeConverter(typeof(InsideBorderElementsConverter))]
    [Serializable]
    public class InsideBorderElements : IInsideBorderElements
    {

        #region Fields

        private Color _backColor = SystemColors.Control;
        private EnhanceGroupBoxGradientMode _BoxGradientMode = EnhanceGroupBoxGradientMode.None;        
        private Color _gradientEndColor;
        private Color _gradientStartColor = Color.Empty;
        private Image _image = null;
        private ImageLayout _imageLayout = ImageLayout.Tile;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the HeaderElements class.
        /// </summary>
        public InsideBorderElements()
        {
            
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the inside border area color.
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Inside Border Area")]
        [Description("Background color.")]
        [DefaultValue(typeof(Color), "Control")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                OnRaisePropertyChanged(PropertyChangeTypes.GraphicElements);
            }
        }

        /// <summary>
        /// Gets or sets the gradient end color.
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Inside Border Area")]
        [Description("Gradient end color.")]
        [DefaultValue(typeof(Color), null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color GradientEndColor
        {
            get { return _gradientEndColor; }
            set
            {
                _gradientEndColor = value;
                OnRaisePropertyChanged(PropertyChangeTypes.GraphicElements);
            }
        }

        /// <summary>
        /// Gets or sets the gradient mode.
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Inside Border Area")]
        [Description("Gradient mode of rendering")]
        [DefaultValue(EnhanceGroupBoxGradientMode.None)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public EnhanceGroupBoxGradientMode GradientMode
        {
            get { return _BoxGradientMode; }
            set
            {
                _BoxGradientMode = value;
                OnRaisePropertyChanged(PropertyChangeTypes.GraphicElements);
            }
        }

        /// <summary>
        /// Gets or sets the gradient start color.
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Inside Border Area")]
        [Description("Gradient start color.")]
        [DefaultValue(typeof(Color), null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color GradientStartColor
        {
            get { return _gradientStartColor; }
            set
            {
                _gradientStartColor = value;
                OnRaisePropertyChanged(PropertyChangeTypes.GraphicElements);
            }
        }

        /// <summary>
        /// Gets or sets the image inside the border area.
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Inside Border Area")]
        [Description("Specifies the background image inside the border.")]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnRaisePropertyChanged(PropertyChangeTypes.GraphicElements);
            }
        }

        /// <summary>
        /// Gets or sets the image layout.
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Inside Border Area")]
        [Description("Seecifies the image layout to use in presenting the image.")]
        [DefaultValue(ImageLayout.Tile)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ImageLayout ImageLayout
        {
            get { return _imageLayout; }
            set
            {
                _imageLayout = value;
                OnRaisePropertyChanged(PropertyChangeTypes.GraphicElements);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Raise property change event.
        /// </summary>
        public void OnRaisePropertyChanged(PropertyChangeTypes types)
        {
            PropertyChanged e = new PropertyChanged(types);
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler handler = RaisePropertyChanged;

            // Event will be null if there are no subscribers
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, e);
            }
        }

        #endregion Methods

        #region Delegates + Events

        /// <summary>
        /// Occurs when the value for one of the class property changes.
        /// </summary>
        public event EventHandler RaisePropertyChanged;

        #endregion Delegates + Events
    }
}
