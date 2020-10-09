using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Ekstrand.Windows.Forms
{
    /// <summary>
    /// Properties grouped for EnhanceGroupBox control shape.
    /// </summary>
    [TypeConverter(typeof(EnhanceGroupBoxControlShapeConverter))]
    [Serializable]
    public class ControlShape
    {
        private int _radiusBorder = 0;
        private BorderCorners _borderCorners = BorderCorners.None;

        /// <summary>
        /// Initializes a new instance of the ControlShape class.
        /// </summary>
        public ControlShape()
        {

        }

        /// <summary>
        /// Gets or sets the radius for each corner.
        /// </summary>
        /// <remarks>Radius value is restricted to the range of 0 to 10.</remarks>
        [Browsable(true)]
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        [Category("Control Shape")]
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
                }

            }
        }

        /// <summary>
        /// Corners to have a rounded appearance.
        /// </summary>
        [Browsable(true)]
        [NotifyParentProperty(true)]
        [DefaultValue("None")]
        [Category("Control Shape")]
        [Description("Use to defined which corner to be rounded")]
        [EditorAttribute(typeof(BorderCornerEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public BorderCorners BorderCorners
        {
            get { return _borderCorners; }
            set
            {
                _borderCorners = value;
            }
        }
    }
}
