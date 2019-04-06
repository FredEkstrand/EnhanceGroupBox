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
        private const int captionSpace = 7;     // Spacing before start/end of caption.
        private const int borderPadding = 3;    // Defined padding for border to edge of control container clipping edge.
        private const bool renderMatchingApplicationState = true;
		
        [ThreadStatic]
        private static GroupBox _eGroupBox = null;
        [ThreadStatic]
        private static Bitmap _bitmap;
        [ThreadStatic]
        private static Graphics _graphicsObj;
        private static LinearGradientBrush _gradientBrush = null;
        private static Pen _pen = null;
        private static SolidBrush _brush = null;
        [ThreadStatic]
        private static Rectangle _clientRectangle;
        private static EnhanceGroupBoxState _state;
        [ThreadStatic]
        private static Rectangle _textBounds;


        #endregion Fields

        #region Constructors

        private GroupBoxRenderer() { }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the renderer uses the application state to determine rendering style.
        /// </summary>
        public static bool RenderMatchingApplicationState
        {
            get
            {
                return renderMatchingApplicationState;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the control would match the current visual style setting of the operating system. 
        /// </summary>
        public static bool RenderWithVisualStyles
        {
            get
            {
                return !renderMatchingApplicationState;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Draws a group box control in the specified state and bounds, with the specified group-box, graphics, bounds, text, font, text-color, text format flags, and state.
        /// </summary>
        /// <param name="ecb">GroupBox object</param>
        /// <param name="g">The Graphics used to draw the group box.</param>
        /// <param name="bounds">The Rectangle that specifies the bounds of the group box.</param>
        /// <param name="state">One of the EnhanceGroupBoxState values that specifies the visual state of the group box.</param>
        public static void DrawEnhanceGroupBox(GroupBox ecb, Graphics g, Rectangle bounds, EnhanceGroupBoxState state)
        {
            _eGroupBox = ecb;
            _clientRectangle = bounds;
            _state = state;

            // Check if dirty bit is flipped if not return previously drawn image.
            if (_eGroupBox.RenderingDirty == false)
            {
                Graphics.FromImage(_bitmap);
                g.DrawImage(_bitmap, 0, 0, _bitmap.Width, _bitmap.Height);
                return;
            }

            // dirty bit is flipped and we have to redraw everything			
            _bitmap = new Bitmap(_clientRectangle.Width, _clientRectangle.Height, PixelFormat.Format32bppArgb);
            _graphicsObj = Graphics.FromImage(_bitmap);
            _graphicsObj.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            _graphicsObj.Clear(Color.Transparent);

            SetTextbounds(g);
            DrawElements(g);

            g.DrawImage(_bitmap, 0, 0, _bitmap.Width, _bitmap.Height);
            _graphicsObj.Dispose();


        }

        private static void DrawElements(Graphics g)
        {
            switch (_eGroupBox.GroupBoxStyle)
            {
                case GroupBoxStyle.Standard:
                    {
                        DrawBorder();
                        DrawTextBackground();
                        DrawText();
                    }
                    break;
                case GroupBoxStyle.Enhance:
                    {
                        DrawInsideBorder();
                        DrawBorder();                                               
                        DrawTextBackground();
                        DrawTextBorder();
                        DrawText();
                    }
                    break;
                case GroupBoxStyle.Excitative:
                    {
                        DrawExcitiveBorder();                        
                        DrawExcitiveTextBackground();
                        DrawExcitiveTextBorder();
                        DrawExcitiveText();                        
                    }
                break;
                case GroupBoxStyle.Header:
                    {
                        DrawHeaderBorder();
                        DrawHeaderBorderTrim();
                        DrawHeaderText();
                    }
                break;
            }
        }

        private static void DrawBorder()
        {
            Rectangle bounds = BorderRectangle();
            Pen p = PenBorder(_eGroupBox.BorderElements.BorderColor, _eGroupBox.BorderElements.Width, _eGroupBox.BorderElements.DashCap,
                            _eGroupBox.BorderElements.DashStyle, _eGroupBox.BorderElements.DashOffset, _eGroupBox.BorderElements.DashPattern);
            p.Alignment = PenAlignment.Inset;
            _graphicsObj.DrawRoundedRectangle(p, bounds, _eGroupBox.BorderElements.Radius, _eGroupBox.BorderElements.BorderCorners);
            p.Dispose();
        }

        private static void DrawExcitiveBorder()
        {
            if(_eGroupBox.BorderElements.Width == 0)
            {
                return;
            }

            Size crSize = _clientRectangle.Size;
            Rectangle r = new Rectangle();
            Rectangle tr = _textBounds;
            int hbwidth = _eGroupBox.HeaderElements.Width;

            Pen p = PenBorder(_eGroupBox.BorderElements.BorderColor, _eGroupBox.BorderElements.Width, _eGroupBox.BorderElements.DashCap,
                            _eGroupBox.BorderElements.DashStyle, _eGroupBox.BorderElements.DashOffset, _eGroupBox.BorderElements.DashPattern);
            p.Alignment = PenAlignment.Inset;
            
            if(GetTextSide() == TextSide.Top)
            {
                r.Y = (tr.Height/2) + borderPadding + hbwidth;
            }
            else
            {
                r.Y = crSize.Height - (tr.Height/2 + borderPadding + hbwidth + 2);
            }
            

            switch (_eGroupBox.HeaderElements.TextAlignment)
            {
////
                case BorderTextAlignment.TopLeft:
                    {
                        r.X += borderPadding + hbwidth;
                        r.Width += (crSize.Width - (borderPadding * 2)) + 1;
                        _graphicsObj.DrawLine(p, r.X, r.Y, r.Width, r.Y);
                    }
                break;
                case BorderTextAlignment.TopRight:
                    {
                        r.X += borderPadding + 1;
                        r.Width += crSize.Width - (borderPadding * 2 +  hbwidth * 2) + 2;
                        _graphicsObj.DrawLine(p, r.X, r.Y, r.Width - tr.Width, r.Y);
                    }
                    break;
                case BorderTextAlignment.TopCenter:
                    {
                        r.X += borderPadding + 1;
                        r.Width += (crSize.Width - (borderPadding * 2)) +1;
                        _graphicsObj.DrawLine(p, r.X, r.Y, r.Width, r.Y);
                    }
                    break;
                case BorderTextAlignment.BottomLeft:
                    {
                        r.X += borderPadding +  tr.Width;
                        r.Width += (crSize.Width - (borderPadding * 2)) + 1;
                        _graphicsObj.DrawLine(p, r.X, r.Y, r.Width, r.Y);
                    }
                    break;
                case BorderTextAlignment.BottomRight:
                    {
                        r.X += borderPadding - 1;
                        r.Width += crSize.Width - (borderPadding * 2 +  hbwidth * 2) + 2;
                        _graphicsObj.DrawLine(p, r.X + 1, r.Y, r.Width - tr.Width, r.Y);
                    }
                    break;
                case BorderTextAlignment.BottomCenter:
                    {
                        r.X += borderPadding + 1;
                        r.Width += (crSize.Width - (borderPadding * 2)) + 1;
                        _graphicsObj.DrawLine(p, r.X, r.Y, r.Width, r.Y);
                    }
                    break;
            }

            p.Dispose();
        }

        private static void DrawHeaderBorder()
        {
            
            Size crSize = _clientRectangle.Size;
            Rectangle r = new Rectangle();
            Rectangle tr = _textBounds;           
            _gradientBrush = null;
            r.Width = crSize.Width;

            if (GetTextSide() == TextSide.Top)
            {
                r.Height = tr.Height + borderPadding * 2;
            }
            else
            {
                r.Y = crSize.Height - (tr.Height + borderPadding * 2) - 1;
                r.Height = crSize.Height;
            }

            if (_eGroupBox.HeaderElements.GradientMode != EnhanceGroupBoxGradientMode.None)
            {
                _gradientBrush = GradientBrush(r, _eGroupBox.HeaderElements.GradientStartColor, _eGroupBox.HeaderElements.GradientEndColor, _eGroupBox.HeaderElements.GradientMode);
                _graphicsObj.FillRectangle(_gradientBrush, r);
                _gradientBrush.Dispose();
            }
            else
            {
                _brush = BackGroundBrush(_eGroupBox.HeaderElements.BackColor);
                _graphicsObj.FillRectangle(_brush, r);
                _brush.Dispose();
            }               
        }

        private static void DrawHeaderBorderTrim()
        {
            Size crSize = _clientRectangle.Size;
            Rectangle r = new Rectangle();
            Rectangle tr = _textBounds;
            int hbwidth = _eGroupBox.HeaderElements.Width /2;
            _pen = null;
            r.Width = crSize.Width;

            if (GetTextSide() == TextSide.Top)
            {
                r.Y = tr.Height + borderPadding * 2 + hbwidth;
            }
            else
            {
                r.Y = crSize.Height - (tr.Height + borderPadding * 2 + hbwidth) - 1;
                r.Height = crSize.Height;
            }

            if (_eGroupBox.HeaderElements.Width != 0)
            {
                Pen p = PenBorder(_eGroupBox.HeaderElements.BorderColor, _eGroupBox.HeaderElements.Width, _eGroupBox.HeaderElements.DashCap,
                                _eGroupBox.HeaderElements.DashStyle, _eGroupBox.HeaderElements.DashOffset, _eGroupBox.HeaderElements.DashPattern);
                p.Alignment = PenAlignment.Inset;
                _graphicsObj.DrawLine(p, r.X, r.Y, r.Width, r.Y);
                p.Dispose();
            }
        }

        private static void DrawText()
        {            
            if(_eGroupBox.Text == string.Empty)
            {
                return;
            }

            Rectangle r = _textBounds;            
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Trimming = StringTrimming.Character;
            _brush = _state == EnhanceGroupBoxState.Normal ? BackGroundBrush(_eGroupBox.ForeColor):BackGroundBrush(_eGroupBox.DisabledTextColor);
            _graphicsObj.DrawString(_eGroupBox.Text, _eGroupBox.Font, _brush, r, drawFormat); 
            
        }

        private static void DrawExcitiveText()
        {
            if (_eGroupBox.Text == string.Empty)
            {
                return;
            }

            Rectangle r = _textBounds;
            //r.Width += 10;
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Trimming = StringTrimming.Character;
            _brush = _state == EnhanceGroupBoxState.Normal ? BackGroundBrush(_eGroupBox.ForeColor) : BackGroundBrush(_eGroupBox.DisabledTextColor);
            _graphicsObj.DrawString(_eGroupBox.Text, _eGroupBox.Font, _brush, r, drawFormat);
        }

        private static void DrawHeaderText()
        {
            Rectangle r = _textBounds;
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            _brush = _state == EnhanceGroupBoxState.Normal ? BackGroundBrush(_eGroupBox.ForeColor) : BackGroundBrush(_eGroupBox.DisabledTextColor);
            _graphicsObj.DrawString(_eGroupBox.Text, _eGroupBox.Font, _brush, r, drawFormat);
        }

        private static void DrawInsideBorder()
        {
            
            Rectangle r =  BorderRectangle();
            int radius = _eGroupBox.BorderElements.BorderCorners != BorderCorners.None ? _eGroupBox.BorderElements.Radius : 0;

            if (_eGroupBox.InsideBorderElements.GradientMode != EnhanceGroupBoxGradientMode.None)
            {
                _gradientBrush = GradientBrush(r,_eGroupBox.InsideBorderElements.GradientStartColor, _eGroupBox.InsideBorderElements.GradientEndColor, _eGroupBox.InsideBorderElements.GradientMode);
                _graphicsObj.FillRoundedRectangle(_gradientBrush,r, radius);
            }
            else
            {
                _brush = BackGroundBrush(_eGroupBox.InsideBorderElements.BackColor);
                _graphicsObj.FillRoundedRectangle(_brush, r, radius);
                _brush.Dispose();
            }

            if (_eGroupBox.InsideBorderElements.Image != null)
            {
                Rectangle ir = r;
                Point pt = new Point(r.X, r.Y);
                DrawBackgroundImage(_graphicsObj, _eGroupBox.InsideBorderElements.Image, Color.White, _eGroupBox.InsideBorderElements.ImageLayout, r, ir, radius, pt, RightToLeft.No);
            }
        }

        private static void DrawTextBackground()
        {
            if(_eGroupBox.Text == string.Empty)
            {
                return;
            }

            Rectangle r = _textBounds;
            r.Inflate(0, 1); // added 1px padding to text edge.            

            _brush = _eGroupBox.GroupBoxStyle == GroupBoxStyle.Standard ? BackGroundBrush(_eGroupBox.BackColor) : BackGroundBrush(_eGroupBox.HeaderElements.BackColor);

            if(_eGroupBox.HeaderElements.BorderCorners != BorderCorners.None)
            {
                _graphicsObj.FillRoundedRectangle(_brush,r,_eGroupBox.HeaderElements.Radius);
            }
            else
            {
                _graphicsObj.FillRectangle(_brush, r.X, r.Y, r.Width, r.Height);
            }
            
            

            if(_eGroupBox.GroupBoxStyle == GroupBoxStyle.Enhance)
            {
                if(_eGroupBox.HeaderElements.GradientMode != EnhanceGroupBoxGradientMode.None)
                {

                    RectangleF rr = new RectangleF(r.X-.5f, r.Y, r.Width, r.Height);
                    LinearGradientBrush gbrush = GradientBrush(rr, _eGroupBox.HeaderElements.GradientStartColor,
                                _eGroupBox.HeaderElements.GradientEndColor, _eGroupBox.HeaderElements.GradientMode);


                    if (_eGroupBox.HeaderElements.Radius != 0)
                    {
                        _graphicsObj.FillRoundedRectangle(gbrush, rr, _eGroupBox.HeaderElements.Radius, _eGroupBox.HeaderElements.BorderCorners);
                    }
                    else
                    {
                        _graphicsObj.FillRectangle(gbrush, rr.X, rr.Y, rr.Width, rr.Height);
                    }

                    gbrush.Dispose();
                }
            }

            _brush.Dispose();
        }

        private static void DrawExcitiveTextBackground()
        {             
            Rectangle r = _textBounds;            
            r.Inflate(0,1);           
            _brush = BackGroundBrush(_eGroupBox.HeaderElements.BackColor);           
            
            if (_eGroupBox.HeaderElements.GradientMode != EnhanceGroupBoxGradientMode.None)
            {
                RectangleF rr = new RectangleF(r.X - .5f, r.Y, r.Width, r.Height);
                LinearGradientBrush gbrush = GradientBrush(rr, _eGroupBox.HeaderElements.GradientStartColor,
                            _eGroupBox.HeaderElements.GradientEndColor, _eGroupBox.HeaderElements.GradientMode);


                if (_eGroupBox.HeaderElements.Radius != 0)
                {                       
                    _graphicsObj.FillRoundedRectangle(gbrush, rr, _eGroupBox.HeaderElements.Radius, _eGroupBox.HeaderElements.BorderCorners);
                }
                else
                {
                    _graphicsObj.FillRectangle(gbrush, rr.X, rr.Y, rr.Width, rr.Height);
                }

                gbrush.Dispose();
            }
            else
            {
                _graphicsObj.FillRectangle(_brush, r);
            }
            

            _brush.Dispose();
        }

        private static void DrawTextBorder()
        {
            if(_eGroupBox.Text == string.Empty)
            {
                return;
            }

            if(_eGroupBox.HeaderElements.Width == 0)
            {
                return;
            }

            if(_eGroupBox.GroupBoxStyle == GroupBoxStyle.Standard)
            {
                return;
            }

            Rectangle r = _textBounds;
            r.Inflate(_eGroupBox.HeaderElements.Width/2, _eGroupBox.HeaderElements.Width/2 + 1);
            _pen = PenBorder(_eGroupBox.HeaderElements.BorderColor, _eGroupBox.HeaderElements.Width, _eGroupBox.HeaderElements.DashCap, 
                _eGroupBox.HeaderElements.DashStyle, _eGroupBox.HeaderElements.DashOffset, _eGroupBox.HeaderElements.DashPattern);
    
            _graphicsObj.DrawRoundedRectangle(_pen,r,_eGroupBox.HeaderElements.Radius, _eGroupBox.HeaderElements.BorderCorners);
            _pen.Dispose();
        }

        private static void DrawExcitiveTextBorder()
        {
            if (_eGroupBox.HeaderElements.Width == 0)
            {
                return;
            }

            if(_eGroupBox.Text == string.Empty)
            {
                return;
            }

            Rectangle r = _textBounds;
            r.Inflate(_eGroupBox.HeaderElements.Width / 2 , _eGroupBox.HeaderElements.Width / 2 + 1);
            _pen = PenBorder(_eGroupBox.HeaderElements.BorderColor, _eGroupBox.HeaderElements.Width, _eGroupBox.HeaderElements.DashCap,
                _eGroupBox.HeaderElements.DashStyle, _eGroupBox.HeaderElements.DashOffset, _eGroupBox.HeaderElements.DashPattern);

            _graphicsObj.DrawRoundedRectangle(_pen, r, _eGroupBox.HeaderElements.Radius, _eGroupBox.HeaderElements.BorderCorners);
            _pen.Dispose();
        }

        private static Rectangle BorderRectangle()
        {
            // we are assuming pen alignment is set to inset in these calculations for border bounds
            Size size = _clientRectangle.Size;
            int fontHeight = GetFontHeight();
            int beWidth = _eGroupBox.BorderElements.Width;
            int bhWidth = _eGroupBox.GroupBoxStyle == GroupBoxStyle.Enhance ? _eGroupBox.HeaderElements.Width + 1: 0;


            if (GetTextSide() == TextSide.Top && _eGroupBox.Text != string.Empty)
            {   //                      x,             y,                                       width,                                           height 
                return new Rectangle(borderPadding, fontHeight + borderPadding + bhWidth, Math.Max(size.Width - (borderPadding * 2) - 1, 0), Math.Max(size.Height - (borderPadding * 2 + fontHeight + bhWidth + 1), 0));
            } 
            
            if(GetTextSide() == TextSide.Bottom && _eGroupBox.Text != string.Empty)
            {   //                      x,             y,                             width,                                        height
                return new Rectangle(borderPadding, borderPadding, Math.Max(size.Width - (borderPadding * 2) - 1, 0), Math.Max(size.Height - (borderPadding * 2 + fontHeight - beWidth/2 + bhWidth) - 1, 0));
            }
            
            //                      x,             y,                                       width,                                           height 
            return new Rectangle(borderPadding, borderPadding, Math.Max(size.Width - (borderPadding * 2) - 1, 0), Math.Max(size.Height - (borderPadding * 2), 0));
        }

        private static void SetTextbounds(Graphics g)
        {
            switch(_eGroupBox.GroupBoxStyle)
            {
                case GroupBoxStyle.Standard:
                case GroupBoxStyle.Enhance:
                    _textBounds = GetTextBounds(g);
                    break;
                case GroupBoxStyle.Excitative:
                    _textBounds = GetExcitiveTextBounds(g);
                    break;
                case GroupBoxStyle.Header:
                    _textBounds = GetHeaderTextBounds(g);
                    break;
            }
        }

        private static Rectangle GetTextBounds(Graphics g)
        {
            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Trimming = StringTrimming.Character;
            drawFormat.FormatFlags = StringFormatFlags.FitBlackBox | StringFormatFlags.NoClip;
            SizeF sizef = g.MeasureString(_eGroupBox.Text, _eGroupBox.Font, _clientRectangle.Width, drawFormat);
            Rectangle bounds = _clientRectangle;
            Rectangle r = new Rectangle();
            int bhWidth = _eGroupBox.GroupBoxStyle == GroupBoxStyle.Enhance ? _eGroupBox.HeaderElements.Width: 0;
            

            switch(_eGroupBox.HeaderElements.TextAlignment)
            {
                case BorderTextAlignment.TopLeft:
                    {
                        r.X += captionSpace + borderPadding + RadiusSpacing(_eGroupBox.BorderElements.Radius) + bhWidth;
                        r.Y += borderPadding + bhWidth + 1;
                    }
                    break;
                case BorderTextAlignment.TopRight:
                    {
                        r.X += bounds.Width - (captionSpace + borderPadding + RadiusSpacing(_eGroupBox.BorderElements.Radius) + bhWidth + 2) - (int)sizef.Width;
                        r.Y += borderPadding + bhWidth + 1;
                    }
                    break;
                case BorderTextAlignment.TopCenter:
                    {
                        int widthalf = bounds.Width / 2;
                        r.X += widthalf - (int)(sizef.Width/2);
                        r.Y += borderPadding + bhWidth + 1;
                    }
                    break;
                case BorderTextAlignment.BottomLeft:
                    {
                        r.X += captionSpace + borderPadding + RadiusSpacing(_eGroupBox.BorderElements.Radius) + bhWidth;
                        r.Y += bounds.Height - ((int)sizef.Height + borderPadding + bhWidth + 1);
                    }
                    break;
                case BorderTextAlignment.BottomRight:
                    {
                        r.X += bounds.Width - (captionSpace + borderPadding + RadiusSpacing(_eGroupBox.BorderElements.Radius) + bhWidth + 2) - (int)sizef.Width;
                        r.Y += bounds.Height - ((int)sizef.Height + borderPadding + bhWidth + 1);
                    }
                    break;
                case BorderTextAlignment.BottomCenter:
                    {
                        int widthalf = bounds.Width / 2;
                        r.X += widthalf - (int)(sizef.Width / 2);
                        r.Y += bounds.Height - ((int)sizef.Height + borderPadding + bhWidth);                        
                    }
                    break;
            }

            r.Width = (int)sizef.Width + 1;
            r.Height = (int)Math.Round(sizef.Height, 0);
            return r;
        }
      
        private static Rectangle GetExcitiveTextBounds(Graphics g)
        {
            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Trimming = StringTrimming.Character;
            drawFormat.FormatFlags = StringFormatFlags.FitBlackBox | StringFormatFlags.NoClip;
            SizeF sizef = g.MeasureString(_eGroupBox.Text, _eGroupBox.Font, _clientRectangle.Width, drawFormat);
            Rectangle bounds = _clientRectangle;
            Rectangle r = new Rectangle();
            int bhWidth = _eGroupBox.HeaderElements.Width;


            switch (_eGroupBox.HeaderElements.TextAlignment)
            {
                case BorderTextAlignment.TopLeft:
                    {
                        r.X += borderPadding + bhWidth;
                        r.Y += borderPadding + bhWidth + 1;
                    }
                    break;
                case BorderTextAlignment.TopRight:
                    {
                        r.X += bounds.Width - (borderPadding + (int)sizef.Width + bhWidth + 2);
                        r.Y += borderPadding + bhWidth + 1;
                    }
                    break;
                case BorderTextAlignment.TopCenter:
                    {
                        int widthalf = bounds.Width / 2;
                        r.X += widthalf - (int)(sizef.Width / 2);
                        r.Y += borderPadding + bhWidth + 1;
                    }
                    break;
                case BorderTextAlignment.BottomLeft:
                    {
                        r.X += borderPadding + bhWidth;
                        r.Y += bounds.Height - ((int)sizef.Height + borderPadding + bhWidth + 2);
                    }
                    break;
                case BorderTextAlignment.BottomRight:
                    {
                        r.X += bounds.Width - (borderPadding + bhWidth + (int)sizef.Width + 2);
                        r.Y += bounds.Height - ((int)sizef.Height + borderPadding + bhWidth + 2);
                    }
                    break;
                case BorderTextAlignment.BottomCenter:
                    {
                        int widthalf = bounds.Width / 2;
                        r.X += widthalf - (int)(sizef.Width / 2);
                        r.Y += bounds.Height - ((int)sizef.Height + borderPadding + bhWidth + 2);
                    }
                    break;
            }

            r.Width = (int)sizef.Width + 1;
            r.Height = (int)Math.Round(sizef.Height, 0);
            return r;
        }

        private static Rectangle GetHeaderTextBounds(Graphics g)
        {
            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Trimming = StringTrimming.Character;
            drawFormat.FormatFlags = StringFormatFlags.FitBlackBox | StringFormatFlags.NoClip;
            SizeF sizef = g.MeasureString(_eGroupBox.Text, _eGroupBox.Font, _clientRectangle.Width, drawFormat);
            Rectangle bounds = _clientRectangle;
            Rectangle r = new Rectangle();
            int bhWidth = _eGroupBox.HeaderElements.Width;

            switch (_eGroupBox.HeaderElements.TextAlignment)
            {
                case BorderTextAlignment.TopLeft:
                    {
                        r.X += borderPadding;
                        r.Y += borderPadding;
                    }
                    break;
                case BorderTextAlignment.TopRight:
                    {
                        r.X += bounds.Width - (borderPadding + (int)sizef.Width + 2);
                        r.Y += borderPadding;
                    }
                    break;
                case BorderTextAlignment.TopCenter:
                    {
                        int widthalf = bounds.Width / 2;
                        r.X += widthalf - (int)(sizef.Width / 2);
                        r.Y += borderPadding;
                    }
                    break;
                case BorderTextAlignment.BottomLeft:
                    {
                        r.X += (borderPadding + 2);
                        r.Y += bounds.Height - (borderPadding  + (int)sizef.Height + 1);
                    }
                    break;
                case BorderTextAlignment.BottomRight:
                    {
                        r.X += bounds.Width - (borderPadding + (int)sizef.Width + 2);
                        r.Y += bounds.Height - (borderPadding + (int)sizef.Height + 1);
                    }
                    break;
                case BorderTextAlignment.BottomCenter:
                    {
                        int widthalf = bounds.Width / 2;
                        r.X += widthalf - (int)(sizef.Width / 2);
                        r.Y += bounds.Height - (borderPadding + (int)sizef.Height + 1);
                    }
                    break;
            }

            r.Width = (int)Math.Round(sizef.Width, 0);
            r.Height = (int)Math.Round(sizef.Height, 0);
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

            int iradius = _eGroupBox.BorderElements.BorderCorners != BorderCorners.None ? radius : 0;

            switch (backgroundImageLayout)
            {
                case ImageLayout.None:
                    {                   
                        using (TextureBrush textureBrush = new TextureBrush(backgroundImage, WrapMode.Clamp))
                        {
                            if (scrollOffset != Point.Empty)
                            {
                                Matrix transform = textureBrush.Transform;
                                transform.Translate(scrollOffset.X, scrollOffset.Y);
                                textureBrush.Transform = transform;
                            }
                            _graphicsObj.FillRoundedRectangle(textureBrush, bounds, iradius);
                        }                       
                    }
                    break;
                case ImageLayout.Center:
                    {
                        Rectangle result = bounds;

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

                        Bitmap cImage = new Bitmap(bounds.Width,bounds.Height);

                        using (var graphics = Graphics.FromImage(cImage))
                        {
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.Clear(Color.Transparent);
                            Point p = new Point(result.X, result.Y);
                            graphics.DrawImage(backgroundImage, p);
                        }

                        using (TextureBrush textureBrush = new TextureBrush(cImage, WrapMode.Clamp))
                        {
                            if (scrollOffset != Point.Empty)
                            {
                                Matrix transform = textureBrush.Transform;
                                transform.Translate(scrollOffset.X, scrollOffset.Y);
                                textureBrush.Transform = transform;
                            }
                            _graphicsObj.FillRoundedRectangle(textureBrush, clipRect, iradius);
                        }
                    }
                    break;
                case ImageLayout.Zoom:
                    {
                        Rectangle result = bounds;
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

                        var destRect = new Rectangle(0, 0, result.Width, result.Height);
                        Bitmap destImage = new Bitmap(result.Width, result.Height);
                        Bitmap fdestImage = new Bitmap(bounds.Width, bounds.Height);

                        destImage.SetResolution(backgroundImage.HorizontalResolution, backgroundImage.VerticalResolution);
                        using (var graphics = Graphics.FromImage(destImage))
                        {
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.Clear(Color.Transparent);

                            using (var wrapMode = new ImageAttributes())
                            {
                                wrapMode.SetWrapMode(WrapMode.Clamp);
                                graphics.DrawImage(backgroundImage, destRect, 0, 0, backgroundImage.Width, backgroundImage.Height, GraphicsUnit.Pixel, wrapMode);
                            }
                        }

                        using (var graphics = Graphics.FromImage(fdestImage))
                        {
                            graphics.Clear(Color.Transparent);

                            int offsetX = bounds.X + (bounds.Width - result.Width)/2;
                            Point p = new Point(offsetX,0);

                            graphics.DrawImage(destImage,p);
                        }


                        using (TextureBrush textureBrush = new TextureBrush(fdestImage, WrapMode.Clamp))
                        {
                            // Make sure the brush origin matches the display rectangle, not the client rectangle,
                            // so the background image scrolls on AutoScroll forms.
                            if (scrollOffset != Point.Empty)
                            {
                                Matrix transform = textureBrush.Transform;
                                transform.Translate(scrollOffset.X, scrollOffset.Y);
                                textureBrush.Transform = transform;
                            }

                            _graphicsObj.FillRoundedRectangle(textureBrush, clipRect, iradius);

                        }
                    }
                    break;
                case ImageLayout.Stretch:
                    {
                        var destRect = new Rectangle(0, 0, bounds.Width, bounds.Height);
                        Bitmap destImage = new Bitmap(bounds.Width, bounds.Height);

                        destImage.SetResolution(backgroundImage.HorizontalResolution, backgroundImage.VerticalResolution);
                        using (var graphics = Graphics.FromImage(destImage))
                        {
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.Clear(Color.Transparent);

                            using (var wrapMode = new ImageAttributes())
                            {
                                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                                graphics.DrawImage(backgroundImage, destRect, 0, 0, backgroundImage.Width, backgroundImage.Height, GraphicsUnit.Pixel, wrapMode);
                            }
                        }


                        using (TextureBrush textureBrush = new TextureBrush(destImage, WrapMode.Clamp))
                        {
                            // Make sure the brush origin matches the display rectangle, not the client rectangle,
                            // so the background image scrolls on AutoScroll forms.
                            if (scrollOffset != Point.Empty)
                            {
                                Matrix transform = textureBrush.Transform;
                                transform.Translate(scrollOffset.X, scrollOffset.Y);
                                textureBrush.Transform = transform;
                            }

                            _graphicsObj.FillRoundedRectangle(textureBrush, clipRect, iradius);

                        }
                     
                    }
                    break;
                case ImageLayout.Tile:
                    {
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
                            _graphicsObj.FillRoundedRectangle(textureBrush, bounds, iradius);
                        }
                    }
                    break;
            }    
        }

        private static int GetFontHeight()
        {
            int fontHeight = _eGroupBox.Font.Height;
            fontHeight = fontHeight / 2;

            if (_eGroupBox.BorderElements.Width > fontHeight)
            {
                fontHeight = (_eGroupBox.BorderElements.Width / 2);
            }

            return fontHeight;
        }

        private static TextSide GetTextSide()
        {
            switch (_eGroupBox.HeaderElements.TextAlignment)
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

        private static int RadiusSpacing(int radius)
        {
            if(_eGroupBox.BorderElements.BorderCorners == BorderCorners.None)
            {
                return 0;
            }

            switch (radius)
            {
                case 0:
                return captionSpace;

                case 1:
                return captionSpace + 1;

                case 2:
                case 3:
                return captionSpace + 2;

                case 4:
                case 5:
                return captionSpace + 3;

                case 6:
                case 7:
                return captionSpace + 4;

                case 8:
                case 9:
                case 10:
                return captionSpace + 5;
            }

            return captionSpace;
        }

        private static SolidBrush BackGroundBrush(Color c)
        {
            return _brush = new SolidBrush(c);
        }

        private static LinearGradientBrush GradientBrush(RectangleF r, Color start, Color end, EnhanceGroupBoxGradientMode gradientMode)
        {
            _gradientBrush = new LinearGradientBrush(r, start, end, (LinearGradientMode)gradientMode);
            return _gradientBrush;
        }

        private static Pen PenBorder(Color c, int width, DashCap dashCap, DashStyle dashStyle, float dashOffset, float[] dashPattern)
        {
            _pen = new Pen(c, width);
            _pen.DashCap = dashCap;
            _pen.DashOffset = dashOffset;
            _pen.DashStyle = dashStyle;

            if (dashPattern != null)
            {
                _pen.DashPattern = dashPattern;
            }

            return _pen;
        }


        #endregion Methods

        enum TextSide
        {
            Top,
            Bottom
        }

    }
}