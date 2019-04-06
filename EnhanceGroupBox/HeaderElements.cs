using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

namespace Ekstrand.Windows.Forms
{
    /// <summary>
    /// Properties grouped for rendering group box header area.
    /// </summary>
    [TypeConverter(typeof(HeaderElementsConverter))]
    [Serializable]
    public class HeaderElements
    {
        #region Fields

        private Color _backColorHeader = SystemColors.Control;
        private int _radiusHeader = 0;
        private int _widthHeader = 1;
        private Color _borderColorHeader = SystemColors.ControlDark;
        private BorderCorners _borderCornersHeader = BorderCorners.None;
        private EnhanceGroupBoxGradientMode _BoxGradientModeHeader = EnhanceGroupBoxGradientMode.None;
        private DashCap _dashCapHeader = DashCap.Flat;
        private float _dashOffsetHeader = 0f;
        private float[] _dashPatternHeader = null;
        private DashStyle _dashStyleHeader = DashStyle.Solid;
        private GroupBox _egroupBox;
        private Color _gradientEndColorHeader = Color.Empty;
        private Color _gradientStartColorHeader = Color.Empty;       
        private BorderTextAlignment m_TextAlignment = BorderTextAlignment.TopLeft;

        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the HeaderElements class.
        /// </summary>
        /// <param name="ecb">GroupBox instance</param>
        public HeaderElements(GroupBox ecb) //EnhanceGroupBox ecb
        {
            _egroupBox = ecb;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets color of the text background.
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Header Area")]
        [Description("Background color.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackColor
        {
            get { return _backColorHeader; }
            set
            {
                _backColorHeader = value;
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets color of the border in the text header area.
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Header Area")]
        [Description("Color of the border")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
        {
            get { return _borderColorHeader; }
            set
            {
                _borderColorHeader = value;
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the corners to have a rounded edge
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [DefaultValue("None")]
        [Category("Appearance Header Area")]
        [Description("Use to defined which corner to be rounded")]
        [EditorAttribute(typeof(BorderCornerEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public BorderCorners BorderCorners
        {
            get { return _borderCornersHeader; }
            set
            {
                _borderCornersHeader = value;
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
            }
        }

        /// <summary>
        /// Specifies the type of graphic shape to use on both ends of each dash in a dashed line.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(DashCap.Flat)]
        [NotifyParentProperty(true)]
        [Category("Appearance Header Area")]
        [Description("Specifies the type of graphic shape to use on both ends of each dash in a dashed line.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DashCap DashCap
        {
            get { return _dashCapHeader; }
            set
            {
                _dashCapHeader = value;
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the distance from the start of a line to the beginning of a dash pattern.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(0.0f)]
        [NotifyParentProperty(true)]
        [Category("Appearance Header Area")]
        [Description("Gets or sets the distance from the start of a line to the beginning of a dash pattern.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float DashOffset
        {
            get { return _dashOffsetHeader; }
            set
            {
                _dashOffsetHeader = value;
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets an array of custom dashes and spaces.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float[] DashPattern
        {
            get { return _dashPatternHeader; }
            set
            {
                _dashPatternHeader = value;
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the style used for dashed lines drawn.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(DashStyle.Solid)]
        [NotifyParentProperty(true)]
        [Category("Appearance Header Area")]
        [Description("Specifies the style of dashed lines drawn for borader")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DashStyle DashStyle
        {
            get { return _dashStyleHeader; }
            set
            {
                _dashStyleHeader = value;
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the gradient end color.
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Header Area")]
        [Description("Gradient end color.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color GradientEndColor
        {
            get { return _gradientEndColorHeader; }
            set
            {
                _gradientEndColorHeader = value;
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the gradient mode.
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Header Area")]
        [Description("Gradient mode of rendering")]
        [DefaultValue(EnhanceGroupBoxGradientMode.None)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public EnhanceGroupBoxGradientMode GradientMode
        {
            get { return _BoxGradientModeHeader; }
            set
            {
                _BoxGradientModeHeader = value;
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the gradient start color.
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Header Area")]
        [Description("Gradient start color.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color GradientStartColor
        {
            get { return _gradientStartColorHeader; }
            set
            {
                _gradientStartColorHeader = value;
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the radius for each corner.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        [Category("Appearance Header Area")]
        [Description("Specifies the radius for each corner")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Radius
        {
            get { return _radiusHeader; }
            set
            {
                if (value != _radiusHeader)
                {
                    if (value > 10)
                    {
                        value = 10;
                    }

                    if (value < 0)
                    {
                        value = 0;
                    }

                    _radiusHeader = value;
                    _egroupBox.RenderingDirty = true;
                    _egroupBox.Invalidate();
                }

            }
        }

        /// <summary>
        /// Gets or sets the text aligment.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(BorderTextAlignment.TopLeft)]
        [NotifyParentProperty(true)]
        [Category("Appearance Header Area")]
        [Description("Specifies the text placement.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public BorderTextAlignment TextAlignment
        {
            get { return m_TextAlignment; }
            set
            {
                m_TextAlignment = value;
                _egroupBox.RenderingDirty = true;
                _egroupBox.RequestLayout();
                _egroupBox.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(1)]
        [NotifyParentProperty(true)]
        [Category("Appearance Header Area")]
        [Description("Specifies the width of the boarder to be drawn")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Width
        {
            get { return _widthHeader; }
            set
            {
                if (_widthHeader != value)
                {
                    if (value > 8)
                    {
                        value = 8;
                    }

                    if (value < 0)
                    {
                        value = 0;
                    }

                    _widthHeader = value;
                    _egroupBox.RenderingDirty = true;
                    _egroupBox.RequestLayout();
                    _egroupBox.Invalidate();
                }
            }
        }

        #endregion Properties
    }
}
