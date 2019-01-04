using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Ekstrand.Drawing;

namespace Ekstrand.Windows.Forms
{
    /// <summary>
    /// Provides methods used to render a group box control with visual styles. This class cannot be inherited.
    /// </summary>
    public sealed class GroupBoxRenderer
    {

        #region Fields

        private const int boxHeaderWidth = 7;           // The frame shows 7 pixels before the caption
        private const int textOffset = 8;               // A MAGIC NUMBER      
        private static Rectangle _clientRectangle;      // Control client area
        private static GroupBox _eGroupBox = null;
        private static LinearGradientBrush _gradientBrush = null;
        private static Pen _Pen = null;
        private static bool _renderMatchingApplicationState = false;

        #endregion Fields

        #region Constructors

        private GroupBoxRenderer() { }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the renderer uses the application state to determine rendering style.
        /// </summary>
        public static bool RenderMatchingApplicationState
        {
            get
            {
                return _renderMatchingApplicationState;
            }
        }

        private static bool RenderWithVisualStyles
        {
            get
            {
                return (!_renderMatchingApplicationState || Application.RenderWithVisualStyles);
            }
        }

        #endregion Properties

        #region Methods
        /// <summary>
        /// Draws a group box control in the specified state and bounds, with the specified groupbox, graphics, bounds, text, font, textcolor, text format flags, and state.
        /// </summary>
        /// <param name="ecb">GroupBox object</param>
        /// <param name="g">The Graphics used to draw the group box.</param>
        /// <param name="bounds">The Rectangle that specifies the bounds of the group box.</param>
        /// <param name="text">The String to draw with the group box.</param>
        /// <param name="font">The Font to apply to groupBoxText.</param>
        /// <param name="textColor">The Color to apply to groupBoxText.</param>
        /// <param name="flags">A bitwise combination of the TextFormatFlags values.</param>
        /// <param name="state">One of the EnhanceGroupBoxState values that specifies the visual state of the group box.</param>
        public static void DrawEnhanceGroupBox(GroupBox ecb, Graphics g, Rectangle bounds, string text, Font font, Color textColor, TextFormatFlags flags, EnhanceGroupBoxState state)
        {
            _eGroupBox = ecb;
            _clientRectangle = bounds;
            DrawBorder(g, bounds, font);
            DrawText(g, bounds, text, font, textColor, flags, state);
        }


        private static void DrawText(Graphics g, Rectangle bounds, string text, Font font, Color textColor, TextFormatFlags flags, EnhanceGroupBoxState state)
        {
            Rectangle r = Textbounds(g, bounds, text, font, flags);  
            DrawTextBackground(g, r);

            if (state == EnhanceGroupBoxState.Disabled)
            {
                TextRenderer.DrawText(g, text, font, r, _eGroupBox.DisabledTextColor, flags);
            }
            else
            {
                TextRenderer.DrawText(g, text, font, r, textColor, flags);
            }
        }

        private static Rectangle BorderRectangle(Rectangle bounds, Font font)
        {
            Size size = bounds.Size;
            int fontHeight = (int)font.Height;

            if (GetTextSide() == TextSide.Top)
            {
                return new Rectangle(_eGroupBox.Padding.Left, fontHeight - 1, Math.Max(size.Width - _eGroupBox.Padding.Horizontal - 1, 0), Math.Max(size.Height - fontHeight - _eGroupBox.Padding.Horizontal, 0));
            }

            return new Rectangle(_eGroupBox.Padding.Left, font.Height - 1 - _eGroupBox.Padding.Horizontal, Math.Max(size.Width - _eGroupBox.Padding.Horizontal - 1, 0), Math.Max(size.Height - font.Height - _eGroupBox.Padding.Horizontal, 0));
        }

        private static void DrawBorder(Graphics g, Rectangle bounds, Font font)
        {
            Rectangle border = new Rectangle();

            switch (_eGroupBox.GroupBoxStyles)
            {
                case GroupBoxStyles.Standard:
                    {
                        border = BorderRectangle(bounds, font);

                        if (_eGroupBox.InsideBorder.GradientMode != EnhanceGroupBoxGradientMode.None)
                        {
                            LinearGradientBrush gb = GradientBrush(border, _eGroupBox.InsideBorder.GradientStartColor,
                                _eGroupBox.InsideBorder.GradientEndColor, _eGroupBox.InsideBorder.GradientMode);

                            g.FillRoundedRectangle(gb, border, _eGroupBox.BorderItems.Radius, _eGroupBox.BorderItems.BorderCorners);
                            gb.Dispose();
                        }

                        Pen p = PenBorder(_eGroupBox.BorderItems.BorderColor, _eGroupBox.BorderItems.Width, _eGroupBox.BorderItems.DashCap,
                            _eGroupBox.BorderItems.DashStyle, _eGroupBox.BorderItems.DashOffset, _eGroupBox.BorderItems.DashPattern);

                        g.DrawRoundedRectangle(p, border, _eGroupBox.BorderItems.Radius, _eGroupBox.BorderItems.BorderCorners);
                        p.Dispose();
                    }
                    break;
                case GroupBoxStyles.Enhance:
                    {
                        border = BorderRectangle(bounds, font);
                        if (_eGroupBox.InsideBorder.GradientMode != EnhanceGroupBoxGradientMode.None)
                        {
                            LinearGradientBrush gb = GradientBrush(border, _eGroupBox.InsideBorder.GradientStartColor,
                                _eGroupBox.InsideBorder.GradientEndColor, _eGroupBox.InsideBorder.GradientMode);

                            g.FillRoundedRectangle(gb, border, _eGroupBox.BorderItems.Radius, _eGroupBox.BorderItems.BorderCorners);
                            gb.Dispose();
                        }

                        if(_eGroupBox.InsideBorder.BackColor != SystemColors.Control)
                        {
                            LinearGradientBrush gb = GradientBrush(border, _eGroupBox.InsideBorder.BackColor,
                                _eGroupBox.InsideBorder.BackColor, EnhanceGroupBoxGradientMode.Horizontal);

                            g.FillRoundedRectangle(gb, border, _eGroupBox.BorderItems.Radius, _eGroupBox.BorderItems.BorderCorners);
                            gb.Dispose();
                        }

                        Pen p = PenBorder(_eGroupBox.BorderItems.BorderColor, _eGroupBox.BorderItems.Width, _eGroupBox.BorderItems.DashCap,
                            _eGroupBox.BorderItems.DashStyle, _eGroupBox.BorderItems.DashOffset, _eGroupBox.BorderItems.DashPattern);

                        g.DrawRoundedRectangle(p, border, _eGroupBox.BorderItems.Radius, _eGroupBox.BorderItems.BorderCorners);
                        p.Dispose();
                    }
                    break;
                case GroupBoxStyles.Excitative:
                    {
                        border = BorderRectangle(bounds, font);
                        if (_eGroupBox.InsideBorder.GradientMode != EnhanceGroupBoxGradientMode.None)
                        {
                            LinearGradientBrush gb = GradientBrush(_clientRectangle, _eGroupBox.InsideBorder.GradientStartColor,
                                _eGroupBox.InsideBorder.GradientEndColor, _eGroupBox.InsideBorder.GradientMode);

                            g.FillRectangle(gb, _clientRectangle);
                            gb.Dispose();
                        }

                        Pen p = PenBorder(_eGroupBox.BorderItems.BorderColor, _eGroupBox.BorderItems.Width, _eGroupBox.BorderItems.DashCap,
                            _eGroupBox.BorderItems.DashStyle, _eGroupBox.BorderItems.DashOffset, _eGroupBox.BorderItems.DashPattern);

                        Pen pp = PenBorder(_eGroupBox.BorderItems.BackColor, font.Height, DashCap.Flat, DashStyle.Solid, 0, null);

                        if (GetTextSide() == TextSide.Top)
                        {
                            g.DrawLine(pp, border.X + textOffset, border.Y, border.Width - textOffset + 2, border.Y);
                            pp.Dispose();

                            g.DrawLine(p, border.X + textOffset, border.Y, border.Width - textOffset + 2, border.Y);
                            p.Dispose();
                        }
                        else
                        {
                            g.DrawLine(pp, border.X + textOffset, border.Height + (font.Height / 2), border.Width - textOffset + 2, border.Height + (font.Height / 2));
                            pp.Dispose();

                            g.DrawLine(p, border.X + textOffset, border.Height + (font.Height / 2), border.Width - textOffset + 2, border.Height + (font.Height / 2));
                            p.Dispose();
                        }

                    }
                    break;
                case GroupBoxStyles.Header:
                    {
                        Rectangle hrec = HeaderRectangle(bounds, _eGroupBox.Font);
                        Rectangle body = new Rectangle();

                        if (GetTextSide() == TextSide.Top)
                        {
                            body = new Rectangle(0, hrec.Height, bounds.Width, bounds.Height - hrec.Height);
                        }
                        else
                        {                            
                            body = new Rectangle(0, 0, bounds.Width, hrec.Y);
                        }

                        if (_eGroupBox.InsideBorder.GradientMode != EnhanceGroupBoxGradientMode.None)
                        {
                            LinearGradientBrush gb = GradientBrush(body, _eGroupBox.InsideBorder.GradientStartColor,
                                _eGroupBox.InsideBorder.GradientEndColor, _eGroupBox.InsideBorder.GradientMode);

                            g.FillRectangle(gb, body);
                            gb.Dispose();
                        }
                    }
                    break;
            }
        }

        private static void DrawTextBackground(Graphics g, Rectangle bounds)
        {

            switch (_eGroupBox.GroupBoxStyles)
            {
                case GroupBoxStyles.Standard:
                    {
                        Rectangle r = bounds;
                        r.Inflate(1, 1);
                        r.Width -= 2;
                        SolidBrush b = new SolidBrush(_eGroupBox.BackColor);
                        g.FillRectangle(b, r);
                        b.Dispose();
                    }
                    break;
                case GroupBoxStyles.Enhance:
                    {
                        RectangleF rr =  bounds;
                        rr.Width += .5f;

                        if (_eGroupBox.Header.GradientMode != EnhanceGroupBoxGradientMode.None)
                        {
                            LinearGradientBrush gb = GradientBrush(rr, _eGroupBox.Header.GradientStartColor,
                                _eGroupBox.Header.GradientEndColor, _eGroupBox.Header.GradientMode);
                            

                            if (_eGroupBox.Header.Radius != 0)
                            {
                                g.FillRoundedRectangle(gb, rr, _eGroupBox.Header.Radius, _eGroupBox.Header.BorderCorners);
                            }
                            else
                            {
                                g.FillRectangle(gb,rr.X+1,rr.Y, rr.Width-1, rr.Height);
                            }

                            gb.Dispose();
                        }
                        else
                        {
                            SolidBrush b = new SolidBrush(_eGroupBox.Header.BackColor);
                            g.FillRoundedRectangle(b, rr, _eGroupBox.Header.Radius);
                            b.Dispose();
                        }

                        if (_eGroupBox.Header.Width != 0)
                        {
                            Pen p = PenBorder(_eGroupBox.Header.BorderColor, _eGroupBox.Header.Width, _eGroupBox.Header.DashCap,
                                _eGroupBox.Header.DashStyle, _eGroupBox.Header.DashOffset, _eGroupBox.Header.DashPattern);

                            g.DrawRoundedRectangle(p, rr, _eGroupBox.Header.Radius, _eGroupBox.Header.BorderCorners);
                            p.Dispose();
                        }
                    }
                    break;
                case GroupBoxStyles.Excitative:
                    {
                        Rectangle r = bounds;
                        r.Inflate(1, 1);
                        r.Width -= 3;


                        if (_eGroupBox.Header.GradientMode != EnhanceGroupBoxGradientMode.None)
                        {
                            LinearGradientBrush gb = GradientBrush(r, _eGroupBox.Header.GradientStartColor,
                                _eGroupBox.Header.GradientEndColor, _eGroupBox.Header.GradientMode);

                            g.FillRoundedRectangle(gb, r, _eGroupBox.Header.Radius, _eGroupBox.Header.BorderCorners);
                            gb.Dispose();
                        }
                        else
                        {
                            SolidBrush b = new SolidBrush(_eGroupBox.Header.BackColor);
                            g.FillRoundedRectangle(b, r, _eGroupBox.Header.Radius);
                            b.Dispose();
                        }

                        if (_eGroupBox.Header.Width != 0)
                        {
                            Pen p = PenBorder(_eGroupBox.Header.BorderColor, _eGroupBox.Header.Width, _eGroupBox.Header.DashCap,
                                _eGroupBox.Header.DashStyle, _eGroupBox.Header.DashOffset, _eGroupBox.Header.DashPattern);

                            g.DrawRoundedRectangle(p, r, _eGroupBox.Header.Radius, _eGroupBox.Header.BorderCorners);
                            p.Dispose();
                        }
                    }
                    break;
                case GroupBoxStyles.Header:
                    {
                        Rectangle border = HeaderRectangle(_clientRectangle, _eGroupBox.Font);
                        Brush pp;

                        Pen p = PenBorder(_eGroupBox.Header.BorderColor, _eGroupBox.Header.Width, _eGroupBox.Header.DashCap,
                            _eGroupBox.Header.DashStyle, _eGroupBox.Header.DashOffset, _eGroupBox.Header.DashPattern);

                        if(_eGroupBox.Header.GradientMode != EnhanceGroupBoxGradientMode.None)
                        {
                            pp = GradientBrush(border, _eGroupBox.Header.GradientStartColor, _eGroupBox.Header.GradientEndColor, _eGroupBox.Header.GradientMode);
                        }
                        else
                        {
                            pp = GradientBrush(border, _eGroupBox.Header.BackColor, _eGroupBox.Header.BackColor, EnhanceGroupBoxGradientMode.Horizontal);
                        }


                        if (GetTextSide() == TextSide.Top)
                        {
                            g.FillRectangle(pp, border);
                            pp.Dispose();

                            g.DrawLine(p, border.X, border.Height, border.Width, border.Height);
                            p.Dispose();
                        }
                        else
                        {
                            g.FillRectangle(pp, border);
                            pp.Dispose();

                            g.DrawLine(p, 0, border.Y, _clientRectangle.Width, border.Y);
                            p.Dispose();
                        }
                    }
                break;
            }


        }

        private static TextSide GetTextSide()
        {
            switch (_eGroupBox.Header.TextAlignment)
            {
                case BorderTextAlignment.TopCenter:
                case BorderTextAlignment.TopLeft:
                case BorderTextAlignment.TopRight:
                return TextSide.Top;
                case BorderTextAlignment.BottomCenter:
                case BorderTextAlignment.BottomLeft:
                case BorderTextAlignment.BottomRight:
                return TextSide.Bottom;
            }

            return TextSide.Top;
        }

        private static LinearGradientBrush GradientBrush(RectangleF r, Color start, Color end, EnhanceGroupBoxGradientMode gradientMode)
        {
            _gradientBrush = new LinearGradientBrush(r, start, end, (LinearGradientMode)gradientMode);
            return _gradientBrush;
        }

        private static Rectangle HeaderRectangle(Rectangle bounds, Font font)
        {
            Rectangle r = bounds;
            int fontHeight = (int)font.Height;

            if (GetTextSide() == TextSide.Top)
            {
                return new Rectangle(-1, -1, r.Width+1, fontHeight + (font.Height / 2));
            }

            return new Rectangle(-1, r.Height - (font.Height / 2) - fontHeight, r.Width + 1, r.Height);

        }

        private static void InitializeRenderer(EnhanceGroupBoxState state)
        { }
        private static Pen PenBorder(Color c, int width, DashCap dashCap, DashStyle dashStyle, float dashOffset, float[] dashPattern)
        {

            _Pen = new Pen(c, width);
            _Pen.DashCap = dashCap;
            _Pen.DashOffset = dashOffset;
            _Pen.DashStyle = dashStyle;

            if (dashPattern != null)
            {
                _Pen.DashPattern = dashPattern;
            }

            return _Pen;
        }
        private static Rectangle Textbounds(Graphics g, Rectangle bounds, string text, Font font, TextFormatFlags textFlags)
        {
            Size measured = TextRenderer.MeasureText(g, text, font, new Size(Int32.MaxValue, Int32.MaxValue), textFlags);
            Rectangle r = new Rectangle();
            Point location = new Point(0,0);

            switch (_eGroupBox.Header.TextAlignment)
            {
                case BorderTextAlignment.TopLeft:
                    {
                        switch (_eGroupBox.GroupBoxStyles)
                        {
                            case GroupBoxStyles.Header:
                                {
                                    location.X = boxHeaderWidth;
                                    location.Y = (measured.Height - (measured.Height / 2)) / 2;
                                }
                                break;
                            case GroupBoxStyles.Excitative:
                                {
                                    location.X = boxHeaderWidth + 4;
                                    location.Y = measured.Height / 2;
                                }
                                break;
                            case GroupBoxStyles.Enhance:
                            case GroupBoxStyles.Standard:
                                {
                                    location.X = boxHeaderWidth + _eGroupBox.BorderItems.Radius + 4;
                                    location.Y = measured.Height / 2;
                                }
                                break;
                        }
                    }
                    break;
                case BorderTextAlignment.TopRight:
                    {
                        switch (_eGroupBox.GroupBoxStyles)
                        {
                            case GroupBoxStyles.Header:
                                {
                                    location.X = bounds.Width - measured.Width - boxHeaderWidth - 3;
                                    location.Y = (measured.Height - (measured.Height / 2)) / 2;
                                }
                                break;
                            case GroupBoxStyles.Excitative:
                                {
                                    location.X = bounds.Width - measured.Width - boxHeaderWidth - 3;
                                    location.Y = measured.Height / 2;
                                }
                                break;
                            case GroupBoxStyles.Enhance:
                            case GroupBoxStyles.Standard:
                                {
                                    location.X = bounds.Width - measured.Width - _eGroupBox.BorderItems.Radius - boxHeaderWidth - 3;
                                    location.Y = measured.Height / 2;
                                }
                                break;
                        }

                    }
                    break;
                case BorderTextAlignment.TopCenter:
                    {
                        location.X = (bounds.Width / 2) - (measured.Width / 2);

                        if (_eGroupBox.GroupBoxStyles == GroupBoxStyles.Header)
                        {
                            location.Y = (measured.Height - (measured.Height / 2)) / 2;
                        }
                        else
                        {
                            location.Y = measured.Height / 2;
                        }
                    }
                    break;
                case BorderTextAlignment.BottomLeft:
                    {
                        switch (_eGroupBox.GroupBoxStyles)
                        {
                            case GroupBoxStyles.Header:
                                {
                                    location.X = boxHeaderWidth + 4;
                                    location.Y = bounds.Height - (measured.Height + (measured.Height / 3) - 1);
                                }
                                break;
                            case GroupBoxStyles.Excitative:
                                {
                                    location.X = boxHeaderWidth + 4;
                                    location.Y = bounds.Height - font.Height - _eGroupBox.Padding.Horizontal;
                                }
                                break;
                            case GroupBoxStyles.Enhance:
                            case GroupBoxStyles.Standard:
                                {
                                    location.X = boxHeaderWidth + _eGroupBox.BorderItems.Radius + 4;
                                    location.Y = bounds.Height - measured.Height - _eGroupBox.Padding.Horizontal;
                                }
                                break;
                        }
                    }
                    break;
                case BorderTextAlignment.BottomRight:
                    {
                        switch (_eGroupBox.GroupBoxStyles)
                        {
                            case GroupBoxStyles.Header:
                                {
                                    location.X = bounds.Width - measured.Width - boxHeaderWidth - 3;
                                    location.Y = bounds.Height - (measured.Height + (measured.Height / 3) - 1);
                                }
                                break;
                            case GroupBoxStyles.Excitative:
                                {
                                    location.X = bounds.Width - measured.Width - boxHeaderWidth - 3;
                                    location.Y = bounds.Height - measured.Height - _eGroupBox.Padding.Horizontal;
                                }
                                break;
                            case GroupBoxStyles.Enhance:
                            case GroupBoxStyles.Standard:
                                {
                                    location.X = bounds.Width - measured.Width - _eGroupBox.BorderItems.Radius - boxHeaderWidth - 3;
                                    location.Y = bounds.Height - measured.Height - _eGroupBox.Padding.Horizontal;
                                }
                                break;
                        }
                    }
                    break;
                case BorderTextAlignment.BottomCenter:
                    {
                        location.X = (bounds.Width / 2) - (measured.Width / 2);
                        if (_eGroupBox.GroupBoxStyles == GroupBoxStyles.Header)
                        {
                            location.Y = bounds.Height - (measured.Height + (font.Height / 3) - 1);
                        }
                        else
                        {
                            location.Y = bounds.Height - measured.Height - _eGroupBox.Padding.Horizontal;
                        }
                    }
                    break;
            }

            r.Location = location;
            r.Width = measured.Width;
            r.Height = measured.Height;
            return r;
        }

        #endregion Methods

        #region Structs + Classes + Enums

        enum TextSide
        {
            Top,
            Bottom
        }

        #endregion Structs + Classes + Enums

    }
}
