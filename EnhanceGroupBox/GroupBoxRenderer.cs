using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
        private const int _imagePlaceHolder = 16;

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
                if (_eGroupBox.Header.Image != null)
                {
                    if (_eGroupBox.Header.ImageSide == ImageSide.Left)
                    {
                        Point p = new Point(r.X - _imagePlaceHolder, r.Y);
                        g.DrawImage(_eGroupBox.Header.Image, p);
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                TextRenderer.DrawText(g, text, font, r, textColor, flags);
                
                if (_eGroupBox.Header.Image != null)
                {
                    Rectangle ir = new Rectangle(0, 0, _eGroupBox.Header.Image.Width + 4, _eGroupBox.Header.Image.Height);

                    if (_eGroupBox.Header.ImageSide == ImageSide.Left)
                    {
                        if (GetTextSide() == TextSide.Top)
                        {
                            Point p = new Point(r.X - _imagePlaceHolder - 4, r.Y / 2);
                            ir.Location = p;
                            Brush ib = GradientBrush(ir,_eGroupBox.BackColor,_eGroupBox.BackColor, EnhanceGroupBoxGradientMode.Horizontal);
                            g.FillRectangle(ib, ir);
                            ib.Dispose();
                            g.DrawImage(_eGroupBox.Header.Image, p);
                        }
                        else
                        {
                            Point p = new Point(r.X - _imagePlaceHolder - 4, r.Y - 4);
                            ir.Location = p;
                            Brush ib = GradientBrush(ir, _eGroupBox.BackColor, _eGroupBox.BackColor, EnhanceGroupBoxGradientMode.Horizontal);
                            g.FillRectangle(ib, ir);
                            ib.Dispose();
                            g.DrawImage(_eGroupBox.Header.Image, p);
                        }
                    }
                    else
                    {
                        if (GetTextSide() == TextSide.Top)
                        {
                            Point p = new Point(r.X + r.Width, r.Y / 2);
                            ir.Location = p;
                            Brush ib = GradientBrush(ir, _eGroupBox.BackColor, _eGroupBox.BackColor, EnhanceGroupBoxGradientMode.Horizontal);
                            g.FillRectangle(ib, ir);
                            ib.Dispose();
                            g.DrawImage(_eGroupBox.Header.Image, p);
                        }
                        else
                        {
                            Point p = new Point(r.X + r.Width, r.Y - 4);
                            ir.Location = p;
                            Brush ib = GradientBrush(ir, _eGroupBox.BackColor, _eGroupBox.BackColor, EnhanceGroupBoxGradientMode.Horizontal);
                            g.FillRectangle(ib, ir);
                            ib.Dispose();
                            g.DrawImage(_eGroupBox.Header.Image, p);
                        }
                    }
                }
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

                        if (_eGroupBox.InsideBorder.Image != null)
                        {
                            Rectangle tr = border;
                            tr.Inflate(-2, -2);
                            Point pt = new Point(tr.X, tr.Y);
                            DrawBackgroundImage(g, _eGroupBox.InsideBorder.Image, _eGroupBox.InsideBorder.BackColor, _eGroupBox.InsideBorder.ImageLayout, _clientRectangle, tr, _eGroupBox.BorderItems.Radius, pt, RightToLeft.No);
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

                        if (_eGroupBox.InsideBorder.Image != null)
                        {
                            Rectangle tr = border;
                            tr.Inflate(-2, -2);
                            Point pt = new Point(tr.X, tr.Y);
                            DrawBackgroundImage(g, _eGroupBox.InsideBorder.Image, _eGroupBox.InsideBorder.BackColor, _eGroupBox.InsideBorder.ImageLayout, _clientRectangle, tr, _eGroupBox.BorderItems.Radius, pt, RightToLeft.No);
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

                        //Pen pp = PenBorder(_eGroupBox.BorderItems.BackColor, font.Height, DashCap.Flat, DashStyle.Solid, 0, null);

                        if (GetTextSide() == TextSide.Top)
                        {
                            //g.DrawLine(pp, border.X + textOffset, border.Y, border.Width - textOffset + 2, border.Y);
                            //pp.Dispose();

                            g.DrawLine(p, border.X + textOffset, border.Y, border.Width - textOffset + 2, border.Y);
                            p.Dispose();

                            if (_eGroupBox.InsideBorder.Image != null)
                            {
                                Rectangle tr = border;
                                tr.Inflate(-2, -2);
                                Point pt = new Point(tr.X, tr.Y);
                                DrawBackgroundImage(g, _eGroupBox.InsideBorder.Image, _eGroupBox.InsideBorder.BackColor, _eGroupBox.InsideBorder.ImageLayout, _clientRectangle, tr, _eGroupBox.BorderItems.Radius, pt, RightToLeft.No);
                            }
                        }
                        else
                        {
                            //g.DrawLine(pp, border.X + textOffset, border.Height + (font.Height / 2), border.Width - textOffset + 2, border.Height + (font.Height / 2));
                            //pp.Dispose();

                            g.DrawLine(p, border.X + textOffset, border.Height + (font.Height / 2), border.Width - textOffset + 2, border.Height + (font.Height / 2));
                            p.Dispose();

                            if (_eGroupBox.InsideBorder.Image != null)
                            {
                                Rectangle tr = border;
                                tr.Inflate(-2, -2);
                                Point pt = new Point(tr.X, tr.Y);
                                DrawBackgroundImage(g, _eGroupBox.InsideBorder.Image, _eGroupBox.InsideBorder.BackColor, _eGroupBox.InsideBorder.ImageLayout, _clientRectangle, tr, _eGroupBox.BorderItems.Radius, pt, RightToLeft.No);
                            }
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
                                    if (_eGroupBox.Header.Image == null || _eGroupBox.Header.ImageSide == ImageSide.Right)
                                    {
                                        location.X = boxHeaderWidth + _eGroupBox.BorderItems.Radius + 4;
                                        location.Y = measured.Height / 2;
                                    }
                                    else
                                    {
                                        location.X = boxHeaderWidth + _eGroupBox.BorderItems.Radius + 4 + _imagePlaceHolder;
                                        location.Y = measured.Height / 2;
                                    }
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
                                    if (_eGroupBox.Header.Image == null || _eGroupBox.Header.ImageSide == ImageSide.Left)
                                    {
                                        location.X = bounds.Width - measured.Width - _eGroupBox.BorderItems.Radius - boxHeaderWidth - 3;
                                        location.Y = measured.Height / 2;
                                    }
                                    else
                                    {
                                        location.X = bounds.Width - measured.Width - _eGroupBox.BorderItems.Radius - boxHeaderWidth - _imagePlaceHolder - 3;
                                        location.Y = measured.Height / 2;
                                    }
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
                                    if (_eGroupBox.Header.Image == null || _eGroupBox.Header.ImageSide == ImageSide.Right)
                                    {
                                        location.X = boxHeaderWidth + _eGroupBox.BorderItems.Radius + 4;
                                        location.Y = bounds.Height - measured.Height - _eGroupBox.Padding.Horizontal;
                                    }
                                    else
                                    {
                                        location.X = boxHeaderWidth + _eGroupBox.BorderItems.Radius + 4 + _imagePlaceHolder;
                                        location.Y = bounds.Height - measured.Height - _eGroupBox.Padding.Horizontal;
                                    }
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
                                    if (_eGroupBox.Header.Image == null || _eGroupBox.Header.ImageSide == ImageSide.Left)
                                    {
                                        location.X = bounds.Width - measured.Width - _eGroupBox.BorderItems.Radius - boxHeaderWidth - 3;
                                        location.Y = bounds.Height - measured.Height - _eGroupBox.Padding.Horizontal;
                                    }
                                    else
                                    {
                                        location.X = bounds.Width - measured.Width - _eGroupBox.BorderItems.Radius - boxHeaderWidth - 3 - _imagePlaceHolder;
                                        location.Y = bounds.Height - measured.Height - _eGroupBox.Padding.Horizontal;
                                    }
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

        
        internal static void DrawBackgroundImage(Graphics g, Image backgroundImage, Color backColor, ImageLayout backgroundImageLayout, Rectangle bounds, Rectangle clipRect, int radius, Point scrollOffset, RightToLeft rightToLeft)
        {
            // taken from ControlPaint.cs file
            // modify for use with rounded rectangle rendering.

            if (g == null)
            {
                throw new ArgumentNullException("Graphics");
            }

            if (backgroundImageLayout == ImageLayout.Tile)
            {
                // tile

                using (TextureBrush textureBrush = new TextureBrush(backgroundImage, WrapMode.Tile))
                {
                    // Make sure the brush origin matches the display rectangle, not the client rectangle,
                    // so the background image scrolls on AutoScroll forms.
                    if (scrollOffset != Point.Empty)
                    {
                        Matrix transform = textureBrush.Transform;
                        transform.Translate(scrollOffset.X, scrollOffset.Y);
                        textureBrush.Transform = transform;
                    }
                    g.FillRectangle(textureBrush, clipRect);
                }
            }

            else
            {
                // write into bitmap then clip to region if needed.
                Bitmap bmp = new Bitmap(backgroundImage.Width, backgroundImage.Height);

                // Center, Stretch, Zoom

                Rectangle imageRectangle = CalculateBackgroundImageRectangle(bounds, backgroundImage, backgroundImageLayout);

                //flip the coordinates only if we don't do any layout, since otherwise the image should be at the center of the
                //displayRectangle anyway.

                if (rightToLeft == RightToLeft.Yes && backgroundImageLayout == ImageLayout.None)
                {
                    imageRectangle.X += clipRect.Width - imageRectangle.Width;
                }

                // We fill the entire cliprect with the backcolor in case the image is transparent.
                // Also, if gdi+ can't quite fill the rect with the image, they will interpolate the remaining
                // pixels, and make them semi-transparent. This is another reason why we need to fill the entire rect.
                // If we didn't where ever the image was transparent, we would get garbage. VS Whidbey #504388
                using (SolidBrush brush = new SolidBrush(backColor))
                {
                    //g.FillRectangle(brush, clipRect);
                    g.FillRoundedRectangle(brush,clipRect,radius);
                }

                if (!clipRect.Contains(imageRectangle))
                {
                    if (backgroundImageLayout == ImageLayout.Stretch || backgroundImageLayout == ImageLayout.Zoom)
                    {
                        imageRectangle.Intersect(clipRect);
                        g.DrawImage(backgroundImage, imageRectangle);
                    }
                    else if (backgroundImageLayout == ImageLayout.None)
                    {
                        imageRectangle.Offset(clipRect.Location);
                        Rectangle imageRect = imageRectangle;
                        imageRect.Intersect(clipRect);
                        Rectangle partOfImageToDraw = new Rectangle(Point.Empty, imageRect.Size);
                        g.DrawImage(backgroundImage, imageRect, partOfImageToDraw.X, partOfImageToDraw.Y, partOfImageToDraw.Width,
                            partOfImageToDraw.Height, GraphicsUnit.Pixel);
                    }
                    else
                    {
                        Rectangle imageRect = imageRectangle;
                        imageRect.Intersect(clipRect);
                        Rectangle partOfImageToDraw = new Rectangle(new Point(imageRect.X - imageRectangle.X, imageRect.Y - imageRectangle.Y)
                                    , imageRect.Size);

                        g.DrawImage(backgroundImage, imageRect, partOfImageToDraw.X, partOfImageToDraw.Y, partOfImageToDraw.Width,
                            partOfImageToDraw.Height, GraphicsUnit.Pixel);
                    }
                }
                else
                {
                    ImageAttributes imageAttrib = new ImageAttributes();
                    imageAttrib.SetWrapMode(WrapMode.TileFlipXY);
                    g.DrawImage(backgroundImage, imageRectangle, 0, 0, backgroundImage.Width, backgroundImage.Height, GraphicsUnit.Pixel, imageAttrib);
                    imageAttrib.Dispose();

                }

            }

        }

        internal static Rectangle CalculateBackgroundImageRectangle(Rectangle bounds, Image backgroundImage, ImageLayout imageLayout)
        {// taken from ControlPaint.cs file

            Rectangle result = bounds;

            if (backgroundImage != null)
            {
                switch (imageLayout)
                {
                    case ImageLayout.Stretch:
                    result.Size = bounds.Size;
                    break;

                    case ImageLayout.None:
                    result.Size = backgroundImage.Size;
                    break;

                    case ImageLayout.Center:
                    result.Size = backgroundImage.Size;
                    Size szCtl = bounds.Size;

                    if (szCtl.Width > result.Width)
                    {
                        result.X = (szCtl.Width - result.Width) / 2;
                    }
                    if (szCtl.Height > result.Height)
                    {
                        result.Y = (szCtl.Height - result.Height) / 2;
                    }
                    break;

                    case ImageLayout.Zoom:
                    Size imageSize = backgroundImage.Size;
                    float xRatio = (float)bounds.Width / (float)imageSize.Width;
                    float yRatio = (float)bounds.Height / (float)imageSize.Height;
                    if (xRatio < yRatio)
                    {
                        //width should fill the entire bounds.
                        result.Width = bounds.Width;
                        // preserve the aspect ratio by multiplying the xRatio by the height
                        // adding .5 to round to the nearest pixel
                        result.Height = (int)((imageSize.Height * xRatio) + .5);
                        if (bounds.Y >= 0)
                        {
                            result.Y = (bounds.Height - result.Height) / 2;
                        }
                    }
                    else
                    {
                        // width should fill the entire bounds
                        result.Height = bounds.Height;
                        // preserve the aspect ratio by multiplying the xRatio by the height
                        // adding .5 to round to the nearest pixel
                        result.Width = (int)((imageSize.Width * yRatio) + .5);
                        if (bounds.X >= 0)
                        {
                            result.X = (bounds.Width - result.Width) / 2;
                        }
                    }

                    break;
                }
            }
            return result;
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
