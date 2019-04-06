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
    public class InsideBorderElements
    {
        #region Fields

        private Color _backColor = SystemColors.Control;
        private EnhanceGroupBoxGradientMode _BoxGradientMode = EnhanceGroupBoxGradientMode.None;
        private GroupBox _egroupBox;
        private Color _gradientEndColor;
        private Color _gradientStartColor = Color.Empty;
        private Image _image = null;
        private ImageLayout _imageLayout = ImageLayout.Tile;

        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the HeaderElements class.
        /// </summary>
        /// <param name="egb">GroupBox instance</param>
        public InsideBorderElements(GroupBox egb) 
        {
            _egroupBox = egb;
            _backColor = _egroupBox.BackColor;
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
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
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
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
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
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
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
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
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
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the image layout
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
                _egroupBox.RenderingDirty = true;
                _egroupBox.Invalidate();
            }
        }


        #endregion Properties

    }
}
