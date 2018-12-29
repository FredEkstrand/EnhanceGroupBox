using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;

namespace Ekstrand.Windows.Forms
{
    [TypeConverter(typeof(InsideBorderElementsConverter))]
    [Serializable]
    public class InsideBorderElements
    {
        #region Fields

        private Color _backColorIBorder = SystemColors.Control;
        private EnhanceGroupBoxGradientMode _BoxGradientModeIBorder = EnhanceGroupBoxGradientMode.None;
        private GroupBox _egroupBox;
        private Color _gradientEndColorIBorder;
        private Color _gradientStartColorIBorder = Color.Empty;
        private Image _image = null;

        #endregion Fields

        #region Constructors

        public InsideBorderElements(GroupBox egb) //EnhanceGroupBox egb
        {
            _egroupBox = egb;
        }

        #endregion Constructors

        #region Properties

        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Inside Border Area")]
        [Description("Background color.")]
        [DefaultValue(typeof(Color), "Control")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackColor
        {
            get { return _backColorIBorder; }
            set
            {
                _backColorIBorder = value;
                _egroupBox.Invalidate();
            }
        }

        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Inside Border Area")]
        [Description("Gradient end color.")]
        [DefaultValue(typeof(Color), null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color GradientEndColor
        {
            get { return _gradientEndColorIBorder; }
            set
            {
                _gradientEndColorIBorder = value;
                _egroupBox.Invalidate();
            }
        }

        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Inside Border Area")]
        [Description("Gradient mode of rendering")]
        [DefaultValue(EnhanceGroupBoxGradientMode.None)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public EnhanceGroupBoxGradientMode GradientMode
        {
            get { return _BoxGradientModeIBorder; }
            set
            {
                _BoxGradientModeIBorder = value;
                _egroupBox.Invalidate();
            }
        }

        [Browsable(true)]
        [NotifyParentProperty(true)]
        [Category("Appearance Inside Border Area")]
        [Description("Gradient start color.")]
        [DefaultValue(typeof(Color), null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color GradientStartColor
        {
            get { return _gradientStartColorIBorder; }
            set
            {
                _gradientStartColorIBorder = value;
                _egroupBox.Invalidate();
            }
        }
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
                _egroupBox.Invalidate();
            }
        }

        #endregion Properties

        #region Methods

        public override string ToString()
        {
            return "Expand for more options";
        }

        #endregion Methods
    }
}
