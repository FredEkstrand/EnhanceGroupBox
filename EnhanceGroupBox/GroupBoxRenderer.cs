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

        private const int _imagePlaceHolder = 16;
        private const int boxHeaderWidth = 7;           // The frame shows 7 pixels before the caption
        private const int textOffset = 8;               // A MAGIC NUMBER      
        private static Rectangle _clientRectangle;      // Control client area
        private static GroupBox _eGroupBox = null;
        private static LinearGradientBrush _gradientBrush = null;
        private static Pen _Pen = null;
        private static bool _renderMatchingApplicationState = false;
        private static Bitmap _bitmap;
        private static Graphics _graphicsObj;

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

            // check if dirty bit is flipped if not return previously drawn image.
            if (_eGroupBox.RenderingDirty == false)
            {
                //_bitmap = new Bitmap(_clientRectangle.Width, _clientRectangle.Height, PixelFormat.Format24bppRgb);
                Graphics.FromImage(_bitmap);
                g.DrawImage(_bitmap, 0, 0, _bitmap.Width, _bitmap.Height);
                _graphicsObj.Dispose();
                return;
            }
                       
            _bitmap = new Bitmap(_clientRectangle.Width, _clientRectangle.Height, PixelFormat.Format32bppArgb);
            _graphicsObj = Graphics.FromImage(_bitmap);
            _graphicsObj.Clear(Color.Transparent);
            
            DrawBorder(g, bounds, font);
            DrawText(g, bounds, text, font, textColor, flags, state);

            g.DrawImage(_bitmap, 0, 0, _bitmap.Width, _bitmap.Height);
            _graphicsObj.Dispose();

            _eGroupBox.RenderingDirty = false;
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
                    _graphicsObj.FillRectangle(textureBrush, clipRect);
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
                    _graphicsObj.FillRoundedRectangle(brush, clipRect, radius);
                }

                if (!clipRect.Contains(imageRectangle))
                {
                    if (backgroundImageLayout == ImageLayout.Stretch || backgroundImageLayout == ImageLayout.Zoom)
                    {
                        imageRectangle.Intersect(clipRect);
                        _graphicsObj.DrawImage(backgroundImage, imageRectangle);
                    }
                    else if (backgroundImageLayout == ImageLayout.None)
                    {
                        imageRectangle.Offset(clipRect.Location);
                        Rectangle imageRect = imageRectangle;
                        imageRect.Intersect(clipRect);
                        Rectangle partOfImageToDraw = new Rectangle(Point.Empty, imageRect.Size);
                        _graphicsObj.DrawImage(backgroundImage, imageRect, partOfImageToDraw.X, partOfImageToDraw.Y, partOfImageToDraw.Width,
                            partOfImageToDraw.Height, GraphicsUnit.Pixel);
                    }
                    else
                    {
                        Rectangle imageRect = imageRectangle;
                        imageRect.Intersect(clipRect);
                        Rectangle partOfImageToDraw = new Rectangle(new Point(imageRect.X - imageRectangle.X, imageRect.Y - imageRectangle.Y)
                                    , imageRect.Size);

                        _graphicsObj.DrawImage(backgroundImage, imageRect, partOfImageToDraw.X, partOfImageToDraw.Y, partOfImageToDraw.Width,
                            partOfImageToDraw.Height, GraphicsUnit.Pixel);
                    }
                }
                else
                {
                    ImageAttributes imageAttrib = new ImageAttributes();
                    imageAttrib.SetWrapMode(WrapMode.TileFlipXY);
                    _graphicsObj.DrawImage(backgroundImage, imageRectangle, 0, 0, backgroundImage.Width, backgroundImage.Height, GraphicsUnit.Pixel, imageAttrib);
                    imageAttrib.Dispose();

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
            Rectangle border = BorderRectangle(bounds, font);
            Rectangle tr = border;
            tr.Inflate(-2, -2);

            switch (_eGroupBox.GroupBoxStyles)
            {
                case GroupBoxStyles.Standard:
                    {
                        if (_eGroupBox.InsideBorder.GradientMode != EnhanceGroupBoxGradientMode.None)
                        {
                            LinearGradientBrush gb = GradientBrush(border, _eGroupBox.InsideBorder.GradientStartColor,
                                _eGroupBox.InsideBorder.GradientEndColor, _eGroupBox.InsideBorder.GradientMode);

                            _graphicsObj.FillRoundedRectangle(gb, border, _eGroupBox.BorderItems.Radius, _eGroupBox.BorderItems.BorderCorners);
                            gb.Dispose();
                        }

                        if (_eGroupBox.InsideBorder.Image != null)
                        {
                            Point pt = new Point(tr.X, tr.Y);
                            DrawBackgroundImage(_graphicsObj, _eGroupBox.InsideBorder.Image, _eGroupBox.InsideBorder.BackColor, _eGroupBox.InsideBorder.ImageLayout, _clientRectangle, tr, _eGroupBox.BorderItems.Radius, pt, RightToLeft.No);
                        }

                        Pen p = PenBorder(_eGroupBox.BorderItems.BorderColor, _eGroupBox.BorderItems.Width, _eGroupBox.BorderItems.DashCap,
                            _eGroupBox.BorderItems.DashStyle, _eGroupBox.BorderItems.DashOffset, _eGroupBox.BorderItems.DashPattern);

                        _graphicsObj.DrawRoundedRectangle(p, border, _eGroupBox.BorderItems.Radius, _eGroupBox.BorderItems.BorderCorners);
                        p.Dispose();
                    }
                    break;
                case GroupBoxStyles.Enhance:
                    {
                        if (_eGroupBox.InsideBorder.GradientMode != EnhanceGroupBoxGradientMode.None)
                        {
                            LinearGradientBrush gb = GradientBrush(border, _eGroupBox.InsideBorder.GradientStartColor,
                                _eGroupBox.InsideBorder.GradientEndColor, _eGroupBox.InsideBorder.GradientMode);

                            _graphicsObj.FillRoundedRectangle(gb, border, _eGroupBox.BorderItems.Radius, _eGroupBox.BorderItems.BorderCorners);
                            gb.Dispose();
                        }

                        if (_eGroupBox.InsideBorder.BackColor != SystemColors.Control)
                        {
                            LinearGradientBrush gb = GradientBrush(border, _eGroupBox.InsideBorder.BackColor,
                                _eGroupBox.InsideBorder.BackColor, EnhanceGroupBoxGradientMode.Horizontal);

                            _graphicsObj.FillRoundedRectangle(gb, border, _eGroupBox.BorderItems.Radius, _eGroupBox.BorderItems.BorderCorners);
                            gb.Dispose();
                        }

                        if (_eGroupBox.InsideBorder.Image != null)
                        {
                            Point pt = new Point(tr.X, tr.Y);
                            DrawBackgroundImage(_graphicsObj, _eGroupBox.InsideBorder.Image, _eGroupBox.InsideBorder.BackColor, _eGroupBox.InsideBorder.ImageLayout, _clientRectangle, tr, _eGroupBox.BorderItems.Radius, pt, RightToLeft.No);
                        }

                        Pen p = PenBorder(_eGroupBox.BorderItems.BorderColor, _eGroupBox.BorderItems.Width, _eGroupBox.BorderItems.DashCap,
                            _eGroupBox.BorderItems.DashStyle, _eGroupBox.BorderItems.DashOffset, _eGroupBox.BorderItems.DashPattern);

                        _graphicsObj.DrawRoundedRectangle(p, border, _eGroupBox.BorderItems.Radius, _eGroupBox.BorderItems.BorderCorners);
                        p.Dispose();
                    }
                    break;
                case GroupBoxStyles.Excitative:
                    {
                        if (_eGroupBox.InsideBorder.GradientMode != EnhanceGroupBoxGradientMode.None)
                        {
                            LinearGradientBrush gb = GradientBrush(_clientRectangle, _eGroupBox.InsideBorder.GradientStartColor,
                                _eGroupBox.InsideBorder.GradientEndColor, _eGroupBox.InsideBorder.GradientMode);

                            _graphicsObj.FillRectangle(gb, _clientRectangle);
                            gb.Dispose();
                        }

                        Pen p = PenBorder(_eGroupBox.BorderItems.BorderColor, _eGroupBox.BorderItems.Width, _eGroupBox.BorderItems.DashCap,
                            _eGroupBox.BorderItems.DashStyle, _eGroupBox.BorderItems.DashOffset, _eGroupBox.BorderItems.DashPattern);

                        if (GetTextSide() == TextSide.Top)
                        {

                            _graphicsObj.DrawLine(p, border.X + textOffset, border.Y, border.Width - textOffset + 2, border.Y);
                            p.Dispose();

                            if (_eGroupBox.InsideBorder.Image != null)
                            {
                                Point pt = new Point(tr.X, tr.Y);
                                DrawBackgroundImage(_graphicsObj, _eGroupBox.InsideBorder.Image, _eGroupBox.InsideBorder.BackColor, _eGroupBox.InsideBorder.ImageLayout, _clientRectangle, tr, _eGroupBox.BorderItems.Radius, pt, RightToLeft.No);
                            }
                        }
                        else
                        {
                            _graphicsObj.DrawLine(p, border.X + textOffset, border.Height + (font.Height / 2), border.Width - textOffset + 2, border.Height + (font.Height / 2));
                            p.Dispose();

                            if (_eGroupBox.InsideBorder.Image != null)
                            {
                                Point pt = new Point(tr.X, tr.Y);
                                DrawBackgroundImage(_graphicsObj, _eGroupBox.InsideBorder.Image, _eGroupBox.InsideBorder.BackColor, _eGroupBox.InsideBorder.ImageLayout, _clientRectangle, tr, _eGroupBox.BorderItems.Radius, pt, RightToLeft.No);
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

                            _graphicsObj.FillRectangle(gb, body);
                            gb.Dispose();
                        }
                    }
                    break;
            }
        }

        private static void DrawHeaderImage(Graphics g, RectangleF r)
        {
            if (_eGroupBox.Header.Image != null)
            {
                RectangleF ir = new Rectangle(0, 0, _eGroupBox.Header.Image.Width, _eGroupBox.Header.Image.Height);

                if (_eGroupBox.Header.ImageSide == ImageSide.Left)
                {
                    if (GetTextSide() == TextSide.Top)
                    {
                        PointF p = new PointF(r.X - _imagePlaceHolder, r.Y / 2);
                        ir.Location = p;
                        Brush ib = GradientBrush(ir, _eGroupBox.BackColor, _eGroupBox.BackColor, EnhanceGroupBoxGradientMode.Horizontal);
                        _graphicsObj.FillRectangle(ib, ir);
                        ib.Dispose();
                        _graphicsObj.DrawImage(_eGroupBox.Header.Image, p);
                    }
                    else
                    {
                        PointF p = new PointF(r.X - _imagePlaceHolder, r.Y - 4);
                        ir.Location = p;
                        Brush ib = GradientBrush(ir, _eGroupBox.BackColor, _eGroupBox.BackColor, EnhanceGroupBoxGradientMode.Horizontal);
                        _graphicsObj.FillRectangle(ib, ir);
                        ib.Dispose();
                        _graphicsObj.DrawImage(_eGroupBox.Header.Image, p);
                    }
                }
                else
                {
                    if (GetTextSide() == TextSide.Top)
                    {
                        PointF p = new PointF(r.X + r.Width + (_eGroupBox.Header.Width / 2), r.Y / 2);
                        ir.Location = p;
                        Brush ib = GradientBrush(ir, _eGroupBox.BackColor, _eGroupBox.BackColor, EnhanceGroupBoxGradientMode.Horizontal);
                        _graphicsObj.FillRectangle(ib, ir);
                        ib.Dispose();
                        _graphicsObj.DrawImage(_eGroupBox.Header.Image, p);
                    }
                    else
                    {
                        PointF p = new PointF(r.X + r.Width + (_eGroupBox.Header.Width / 2), r.Y - 2);
                        ir.Location = p;
                        Brush ib = GradientBrush(ir, _eGroupBox.BackColor, _eGroupBox.BackColor, EnhanceGroupBoxGradientMode.Horizontal);
                        _graphicsObj.FillRectangle(ib, ir);
                        ib.Dispose();
                        _graphicsObj.DrawImage(_eGroupBox.Header.Image, p);
                    }
                }
            }

        }

        private static void DrawText(Graphics g, Rectangle bounds, string text, Font font, Color textColor, TextFormatFlags flags, EnhanceGroupBoxState state)
        {
            RectangleF r = Textbounds(g, bounds, text, font, flags);  
            DrawTextBackground(g, r);

            SolidBrush sb = new SolidBrush(_eGroupBox.ForeColor);
            // StringFormat.GenericTypographic
            StringFormat drawFormat = new StringFormat();
            

            if (state == EnhanceGroupBoxState.Disabled)
            {
                _graphicsObj.DrawString(text,font,sb,r, drawFormat);
                //TextRenderer.DrawText(_graphicsObj, text, font, r, _eGroupBox.DisabledTextColor, flags);
				DrawHeaderImage(_graphicsObj, r);                
            }
            else
            {
                _graphicsObj.DrawString(text, font, sb, r, drawFormat);
                //TextRenderer.DrawText(_graphicsObj, text, font, r, textColor, flags);
                DrawHeaderImage(_graphicsObj, r);                
            }
        }
        private static void DrawTextBackground(Graphics g, RectangleF bounds)
        {
			RectangleF r = bounds;
            r.Inflate(1, 1);
			
            switch (_eGroupBox.GroupBoxStyles)
            {
                case GroupBoxStyles.Standard:
                    {                        
                        r.Width -= 2;
                        SolidBrush b = new SolidBrush(_eGroupBox.BackColor);
                        _graphicsObj.FillRectangle(b, r);
                        b.Dispose();
                    }
                    break;
                case GroupBoxStyles.Enhance:
                    {
                        RectangleF rr =  bounds;
                        rr.Inflate(1,1);
                        rr.Width -= 1.5f;

                        if (_eGroupBox.Header.GradientMode != EnhanceGroupBoxGradientMode.None)
                        {
                            LinearGradientBrush gb = GradientBrush(rr, _eGroupBox.Header.GradientStartColor,
                                _eGroupBox.Header.GradientEndColor, _eGroupBox.Header.GradientMode);
                            

                            if (_eGroupBox.Header.Radius != 0)
                            {
                                _graphicsObj.FillRoundedRectangle(gb, rr, _eGroupBox.Header.Radius, _eGroupBox.Header.BorderCorners);
                            }
                            else
                            {
                                _graphicsObj.FillRectangle(gb,rr.X+1,rr.Y, rr.Width-1, rr.Height);
                            }

                            gb.Dispose();
                        }
                        else
                        {
                            SolidBrush b = new SolidBrush(_eGroupBox.Header.BackColor);
                            _graphicsObj.FillRoundedRectangle(b, rr, _eGroupBox.Header.Radius);
                            b.Dispose();
                        }

                        if (_eGroupBox.Header.Width != 0)
                        {
                            Pen p = PenBorder(_eGroupBox.Header.BorderColor, _eGroupBox.Header.Width, _eGroupBox.Header.DashCap,
                                _eGroupBox.Header.DashStyle, _eGroupBox.Header.DashOffset, _eGroupBox.Header.DashPattern);

                            _graphicsObj.DrawRoundedRectangle(p, rr, _eGroupBox.Header.Radius, _eGroupBox.Header.BorderCorners);
                            p.Dispose();
                        }
                    }
                    break;
                case GroupBoxStyles.Excitative:
                    {
                        r.Width -= 3;

                        if (_eGroupBox.Header.GradientMode != EnhanceGroupBoxGradientMode.None)
                        {
                            LinearGradientBrush gb = GradientBrush(r, _eGroupBox.Header.GradientStartColor,
                                _eGroupBox.Header.GradientEndColor, _eGroupBox.Header.GradientMode);

                            _graphicsObj.FillRoundedRectangle(gb, r, _eGroupBox.Header.Radius, _eGroupBox.Header.BorderCorners);
                            gb.Dispose();
                        }
                        else
                        {
                            SolidBrush b = new SolidBrush(_eGroupBox.Header.BackColor);
                            _graphicsObj.FillRoundedRectangle(b, r, _eGroupBox.Header.Radius);
                            b.Dispose();
                        }

                        if (_eGroupBox.Header.Width != 0)
                        {
                            Pen p = PenBorder(_eGroupBox.Header.BorderColor, _eGroupBox.Header.Width, _eGroupBox.Header.DashCap,
                                _eGroupBox.Header.DashStyle, _eGroupBox.Header.DashOffset, _eGroupBox.Header.DashPattern);

                            _graphicsObj.DrawRoundedRectangle(p, r, _eGroupBox.Header.Radius, _eGroupBox.Header.BorderCorners);
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
                            _graphicsObj.FillRectangle(pp, border);
                            pp.Dispose();

                            _graphicsObj.DrawLine(p, border.X, border.Height, border.Width, border.Height);
                            p.Dispose();
                        }
                        else
                        {
                            _graphicsObj.FillRectangle(pp, border);
                            pp.Dispose();

                            _graphicsObj.DrawLine(p, 0, border.Y, _clientRectangle.Width, border.Y);
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
		
        private static RectangleF Textbounds(Graphics g, Rectangle bounds, string text, Font font, TextFormatFlags textFlags)
        {
            SizeF measured = g.MeasureString(text,font);
            //Size measured = TextRenderer.MeasureText(g, text, font, new Size(Int32.MaxValue, Int32.MaxValue), textFlags);
            RectangleF r = new Rectangle();
            PointF location = new Point(0,0);

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
                                    if (_eGroupBox.Header.Image == null)
                                    {
                                        location.X = boxHeaderWidth + _eGroupBox.BorderItems.Radius + 4;
                                        location.Y = measured.Height / 2;
                                    }
                                    else
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
                                        location.X = bounds.Width - measured.Width - _eGroupBox.BorderItems.Radius - boxHeaderWidth ;
                                        location.Y = measured.Height / 2;
                                    }
                                    else
                                    {
                                        location.X = bounds.Width - measured.Width - _eGroupBox.BorderItems.Radius - boxHeaderWidth - _imagePlaceHolder ;
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
