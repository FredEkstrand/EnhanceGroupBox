using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Ekstrand.Windows.Forms
{
    /// <summary>
    /// Properties grouped for border appearance
    /// </summary>
    [TypeConverter(typeof(BorderElementsConverter))]
    [Serializable]
    public class BorderElements : IBorderElements
    {
        #region Fields

        private Color _backColor = SystemColors.Control;
        private Color _borderColor = SystemColors.ControlDark;
        private BorderCorners _borderCorners = BorderCorners.None;
        private DashCap _dashCapBorder = DashCap.Flat;
        private float _dashOffsetBorder = 0f;
        private float[] _dashPatternBorder = null;
        private DashStyle _dashStyleBorder = DashStyle.Solid;        
        private int _radiusBorder = 0;
        private int _widthBorder = 1;

        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the BorderElements class.
        /// </summary>
        public BorderElements()
        {
            
        }

        #endregion Constructors

        #region Properties
        /// <summary>
        /// In Excitative style the border back color.
        /// </summary>
        [Browsable(true)]
        [DefaultValue("SystemColors.Control")]
        [NotifyParentProperty(true)]
        [Category("Appearance Border")]
        [Description("Specifies in Excitative style the border back color")]
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
        /// Color of the border.
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Description("Color of the border")]
        [Category("Appearance Border")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                OnRaisePropertyChanged(PropertyChangeTypes.GraphicElements);
            }
        }

        /// <summary>
        /// Corners to have a rounded appearance.
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [DefaultValue("None")]
        [Category("Appearance Border")]
        [Description("Use to defined which corner to be rounded")]
        [EditorAttribute(typeof(BorderCornerEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public BorderCorners BorderCorners
        {
            get { return _borderCorners; }
            set
            {
                _borderCorners = value;
                OnRaisePropertyChanged(PropertyChangeTypes.GraphicElements);
            }
        }

        /// <summary>
        /// Specifies the type of graphic shape to use on both ends of each dash in a dashed line.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(DashCap.Flat)]
        [NotifyParentProperty(true)]
        [Category("Appearance Border")]
        [Description("Specifies the type of graphic shape to use on both ends of each dash in a dashed line.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DashCap DashCap
        {
            get { return _dashCapBorder; }
            set
            {
                _dashCapBorder = value;
                OnRaisePropertyChanged(PropertyChangeTypes.GraphicElements);
            }
        }

        /// <summary>
        /// Gets or sets the distance from the start of a line to the beginning of a dash pattern.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(0.0f)]
        [NotifyParentProperty(true)]
        [Category("Appearance Border")]
        [Description("Gets or sets the distance from the start of a line to the beginning of a dash pattern.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float DashOffset
        {
            get { return _dashOffsetBorder; }
            set
            {
                _dashOffsetBorder = value;
                OnRaisePropertyChanged(PropertyChangeTypes.GraphicElements);
            }
        }

        /// <summary>
        /// Gets or sets an array of custom dashes and spaces.
        /// </summary>
        [Browsable(false)]
        public float[] DashPattern
        {
            get { return _dashPatternBorder; }
            set
            {
                _dashPatternBorder = value;
                OnRaisePropertyChanged(PropertyChangeTypes.GraphicElements);
            }
        }

        /// <summary>
        /// Gets or sets the style used for dashed lines drawn.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(DashStyle.Solid)]
        [NotifyParentProperty(true)]
        [Category("Appearance Border")]
        [Description("Specifies the style of dashed lines drawn for border")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DashStyle DashStyle
        {
            get { return _dashStyleBorder; }
            set
            {
                _dashStyleBorder = value;
                OnRaisePropertyChanged(PropertyChangeTypes.GraphicElements);
            }
        }

        /// <summary>
        /// Gets or sets the radius for each corner.
        /// </summary>
        /// <remarks>Radius value is restricted to the range of 0 to 10.</remarks>
        [Browsable(true)]
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        [Category("Appearance Border")]
        [Description("Specifies the radius for each corner")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Radius
        {
            get { return _radiusBorder; }
            set
            {
                if (value != _radiusBorder)
                {
                    if (value > 10)
                    {
                        value = 10;
                    }

                    if (value < 0)
                    {
                        value = 0;
                    }

                    _radiusBorder = value;
                    OnRaisePropertyChanged(PropertyChangeTypes.GraphicElements | PropertyChangeTypes.LayoutNeeded);
                }

            }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(1)]
        [NotifyParentProperty(true)]
        [Description("Specifies the width of the boarder to be drawn")]
        [Category("Appearance Border")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Width
        {
            get { return _widthBorder; }
            set
            {
                if (_widthBorder != value)
                {
                    if (value > 8)
                    {
                        value = 8;
                    }

                    if (value < 1)
                    {
                        value = 1;
                    }

                    _widthBorder = value;
                    OnRaisePropertyChanged(PropertyChangeTypes.GraphicElements | PropertyChangeTypes.LayoutNeeded);
                }
            }
        }

        /// <summary>
        /// Occurs when the value for one of the class property changes.
        /// </summary>
        public event EventHandler RaisePropertyChanged;

        /// <summary>
        /// Occurs when the value for one of the class property changes.
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

        #endregion Properties
    }
}
