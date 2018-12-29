using System;

namespace Ekstrand.Windows.Forms
{
    [Flags]
    public enum BorderCorners
    {

        None = 0x01,
        TopLeft = 0x02,
        TopRight = 0x04,
        BottomLeft = 0x08,
        BottomRight = 0x10,
        All = TopLeft | TopRight | BottomLeft | BottomRight
    }

    public enum BorderTextAlignment
    {
        TopLeft = 0,
        TopRight,
        TopCenter,
        BottomLeft,
        BottomRight,
        BottomCenter

    }

    // Enum extended from LinearGradientMode
    public enum EnhanceGroupBoxGradientMode
    {
        /// <summary>No gradient.</summary>
        None = 4,
        /// <summary>Specifies a gradient from upper right to lower left.</summary>
        BackwardDiagonal = 3,
        /// <summary>Specifies a gradient from upper left to lower right.</summary>
        ForwardDiagonal = 2,
        /// <summary>Specifies a gradient from left to right.</summary>
        Horizontal = 0,
        /// <summary>Specifies a gradient from top to bottom.</summary>
        Vertical = 1


    }

    public enum EnhanceGroupBoxState
    {
        Normal = 1,
        Disabled = 2
    }

    public enum GroupBoxStyles
    {
        Standard,
        Enhance,
        Excitative,
        Header
    }
}