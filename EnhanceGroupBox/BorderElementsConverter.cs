using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Ekstrand.Windows.Forms
{
    /// <summary>
    /// Provides a type converter to convert BorderElements objects to and from various other representations.
    /// </summary>
    public class BorderElementsConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// Converts the given value object to the specified type, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="culture">A CultureInfo. If null is passed, the current culture is assumed.</param>
        /// <param name="value">The Object to convert.</param>
        /// <param name="destinationType">The Type to convert the value parameter to.</param>
        /// <returns>An Object that represents the converted value.</returns>
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return "";
            }

            return base.ConvertTo(
                context,
                culture,
                value,
                destinationType);
        }
    }
}
