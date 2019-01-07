using System;

namespace Ekstrand.Windows.Forms
{
    /// <summary>
    /// Specifies  which rectangle corner to be rounded.
    /// </summary>
    [Flags]
    public enum BorderCorners
    {
        /// <summary>
        /// Specifies no borders to be rounded.
        /// </summary>
        None = 0x01,
        /// <summary>
        /// Specifies top left corner to be rounded.
        /// </summary>
        TopLeft = 0x02,
        /// <summary>
        /// Specifies top right corner to be rounded.
        /// </summary>
        TopRight = 0x04,
        /// <summary>
        /// Specifies bottom left corner to be rounded.
        /// </summary>
        BottomLeft = 0x08,
        /// <summary>
        /// Specifies bottom right corner to be rounded.
        /// </summary>
        BottomRight = 0x10,
        /// <summary>
        /// Specifies all corners to be rounded.
        /// </summary>
        All = TopLeft | TopRight | BottomLeft | BottomRight
    }

    /// <summary>
    /// Specifies text alignment in the group box
    /// </summary>
    public enum BorderTextAlignment
    {
        /// <summary>
        /// Specifies top left side.
        /// </summary>
        TopLeft = 0,
        /// <summary>
        /// Specifies top right side.
        /// </summary>
        TopRight,
        /// <summary>
        /// Specifies top center.
        /// </summary>
        TopCenter,
        /// <summary>
        /// Specifies bottom left side.
        /// </summary>
        BottomLeft,
        /// <summary>
        /// Specifies bottom right side.
        /// </summary>
        BottomRight,
        /// <summary>
        /// Specifies bottom center.
        /// </summary>
        BottomCenter

    }

    /// <summary>
    /// Specifies the linear gradient direction.
    /// </summary>
    public enum EnhanceGroupBoxGradientMode
    {// Enum extended from LinearGradientMode
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

    /// <summary>
    /// Specifies the group box state in rendering.
    /// </summary>
    public enum EnhanceGroupBoxState
    {
        /// <summary>
        /// Specifies normal rendering.
        /// </summary>
        Normal = 1,
        /// <summary>
        /// Specifies rendering to be disabled.
        /// </summary>
        Disabled = 2
    }

    /// <summary>
    /// Specifies the group box rendering styles.
    /// </summary>
    public enum GroupBoxStyles
    {
        /// <summary>
        /// Specifies the standard style.
        /// </summary>
        Standard,
        /// <summary>
        /// Specifies the enhance style.
        /// </summary>
        Enhance,
        /// <summary>
        /// Specifies the excitative style.
        /// </summary>
        Excitative,
        /// <summary>
        /// Specifies the header style.
        /// </summary>
        Header
    }

    /// <summary>
    /// Specifies which side should the image be rendered to the header text.
    /// </summary>
    public enum ImageSide
    {
        /// <summary>
        /// Specifies the left hand side of the header text area.
        /// </summary>
        Left = 0,
        /// <summary>
        /// Specifies the right hand side of the header text area.
        /// </summary>
        Right = 1
    }
}