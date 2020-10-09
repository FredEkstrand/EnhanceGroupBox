using Ekstrand.Drawing;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Ekstrand.Windows.Forms
{
    /// <summary>
    /// Represents a control that creates a container that has a border and a header for user interface (UI) content.
    /// </summary>
    [Serializable]
    [ComVisible(false),
    ClassInterface(ClassInterfaceType.AutoDispatch),
    DefaultEvent("Enter"),
    DefaultProperty("Text"),
    Description("Enhance GroupBox"),
    ToolboxItemFilter("Ekstrand.Windows.Forms"),
    ToolboxItem(true),
    ToolboxBitmap(typeof(EnhanceGroupBox), "Resources.GroupBox_16x"),
    Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class EnhanceGroupBox : Control
    {

        #region Fields

        private bool _dirty;
        private Color _disabledTextColor = SystemColors.GrayText;
        private GroupBoxStyle _groupBoxStyles = GroupBoxStyle.Standard;
        private HeaderElements _headerElements;
        private InsideBorderElements _insideBoarderElements;
        private BorderElements _borderElements;
        private ControlElements _controlElements;
        private ControlShape _controlShape;
        #endregion Fields

        #region Constructors

        /// <summary>
        /// Represents a control that creates a container that has a border and a header for user interface (UI) content.
        /// </summary>
        public EnhanceGroupBox() : base()
        {
            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw, true);

            SetStyle(ControlStyles.Selectable, false);
            TabStop = false;
            _dirty = true;
            _headerElements = new HeaderElements();
            _headerElements.RaisePropertyChanged += RaisePropertyChanged;
            _insideBoarderElements = new InsideBorderElements();
            _insideBoarderElements.RaisePropertyChanged += RaisePropertyChanged;
            _borderElements = new BorderElements();
            _borderElements.RaisePropertyChanged += RaisePropertyChanged;
            _controlElements = new ControlElements();
            _controlShape = new ControlShape();

        }

        private void RaisePropertyChanged(object sender, EventArgs e)
        {
            _dirty = true;
            this.Invalidate();
            UpdateLayout();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the control can accept data that the user drags onto it.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public override bool AllowDrop
        {
            get
            {
                return base.AllowDrop;
            }
            set
            {
                base.AllowDrop = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the GroupBox resizes based on its contents.
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
            }
        }

        /// <summary>
        /// Gets or sets border items properties
        /// </summary>
        [NotifyParentProperty(true)]
        [CategoryAttribute("Appearance")]
        [Description("Properties for Border Elements appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BorderElements BorderElements
        {
            get { return _borderElements; }
            private set { _borderElements = value; }
        }

        /// <summary>
        /// Gets or sets EnhanceGroupBox control shape.
        /// </summary>
        [NotifyParentProperty(true)]
        [CategoryAttribute("Appearance")]
        [Description("Properties for EnhanceGroupBox control shape.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlShape ControlShape
        {
            get { return _controlShape; }
            private set { _controlShape = value; }
        }

        /// <summary>
        /// Gets or sets text disabled color
        /// </summary>
        [NotifyParentProperty(true)]
        [CategoryAttribute("Appearance")]
        [DefaultValue("SystemColors.GrayText")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color DisabledTextColor
        {
            get { return _disabledTextColor; }
            set
            {
                _disabledTextColor = value;
                _dirty = true;
            }
        }

        /// <summary>
        /// Gets the rectangle that represents the display area of the control.
        /// </summary>
        [
         Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
         Description("A Rectangle that represents the display area of the control.")
         ]
        public override Rectangle DisplayRectangle
        {
            get
            {
                return CalculateDisplayRectangle();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control can respond to user interaction.
        /// </summary>
        [Category("Behavior")]
        [Localizable(true)]
        [Description("Gets or sets a value indicating whether the control can respond to user interaction.")]
        public new bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;
                _dirty = true;
                Invalidate();
            }
        }

        /// <summary>
        /// Get or sets GroupBox Style used for rendering
        /// </summary>
        [Browsable(true)]
        [DefaultValue(GroupBoxStyle.Standard)]
        [NotifyParentProperty(true)]
        [Category("Appearance")]
        [Description("Specifies the rendering style to use")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public GroupBoxStyle GroupBoxStyle
        {
            get { return _groupBoxStyles; }
            set
            {
                if (value != _groupBoxStyles)
                {
                    _groupBoxStyles = value;
                    _dirty = true;
                    Invalidate();
                    LayoutEngine.Layout(this, new LayoutEventArgs(this, "DisplayRectangle"));
                }
            }
        }

        /// <summary>
        /// Gets or sets header properties
        /// </summary>
        [NotifyParentProperty(true)]
        [CategoryAttribute("Appearance")]
        [Description("Properties for header area appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public HeaderElements HeaderElements
        {
            get { return _headerElements; }
            set { _headerElements = value; }

        }

        /// <summary>
        /// Gets or sets inside border properties
        /// </summary>
        [NotifyParentProperty(true)]
        [CategoryAttribute("Appearance")]
        [Description("Properties for InsideBorder area appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public InsideBorderElements InsideBorderElements
        {
            get { return _insideBoarderElements; }
            private set { _insideBoarderElements = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.
        /// </summary>
        public override RightToLeft RightToLeft
        {
            get { return base.RightToLeft; }
            set
            {
                if (base.RightToLeft != RightToLeft.No)
                {
                    base.RightToLeft = RightToLeft.No;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the user can press the TAB key to give the focus to the GroupBox.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        new public bool TabStop
        {
            get
            {
                return base.TabStop;
            }
            set
            {
                base.TabStop = value;
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        [Localizable(true)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                bool empty = base.Text.Equals(string.Empty);
                base.Text = value;
                _dirty = true;

                Invalidate();

                if (empty && !base.Text.Equals(string.Empty))
                {
                    UpdateLayout();
                }
                if (!empty && base.Text.Equals(string.Empty))
                {
                    UpdateLayout();
                }
            }
        }

        /// <summary>
        /// Gets or sets the rendering dirty boolean value to invalidate internal drawing buffering.
        /// </summary>
        internal bool RenderingDirty
        {
            get { return _dirty; }
            set
            {
                _dirty = true;
            }
        }

        /// <summary>
        /// Gets the required creation parameters when the control handle is created.
        /// </summary>
        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand)]
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassName = null;
                cp.ExStyle |= 0x00010000; //WS_EX_CONTROLPARENT
                return cp;
            }
        }

        /// <summary>
        /// Gets the Padding structure that contains the default padding settings for a GroupBox control.
        /// </summary>
        protected override Padding DefaultPadding
        {
            get
            {
                return new Padding(3);
            }
        }

        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(200, 100);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        ///  Returns a String containing the name of the Component, if any.This method should not be overridden.
        /// </summary>
        /// <returns>Return the text representation of the control</returns>
        public override string ToString()
        {

            string s = base.ToString();
            return s + ", Text: " + Text;
        }

        internal void RequestLayout()
        {
            LayoutEngine.Layout(this, new LayoutEventArgs(this, "DisplayRectangle"));
        }

        /// <summary>
        /// Creates a new accessibility object for the GroupBox.
        /// </summary>
        /// <returns>A new AccessibleObject for the GroupBox.</returns>
        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new EnhanceGroupBoxAccessibleObject(this);
        }

        /// <summary>
        /// Raises the FontChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            Invalidate();
        }

        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            SmoothingMode cachSmothingMode = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            ControlShapeInternal(e);

            EnhanceGroupBoxState state = Enabled ? EnhanceGroupBoxState.Normal : EnhanceGroupBoxState.Disabled;
            _controlElements.BackColor = BackColor;
            _controlElements.Bounds = this.ClientRectangle;
            _controlElements.DisabledTextColor = DisabledTextColor;
            _controlElements.Font = Font;
            _controlElements.ForeColor = ForeColor;
            _controlElements.Graphics = e.Graphics;
            _controlElements.GroupBoxStyle = GroupBoxStyle;
            _controlElements.Text = Text;

            EnhanceGroupBoxRenderer.DrawEnhanceGroupBox(_controlElements, _borderElements, HeaderElements, InsideBorderElements, _dirty, state);

            e.Graphics.SmoothingMode = cachSmothingMode;

            // pass to base last to raise paint event
            base.OnPaint(e);
        }

        /// <summary>
        /// Processes a mnemonic character.
        /// </summary>
        /// <param name="charCode">The character to process.</param>
        /// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
        [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
        protected override bool ProcessMnemonic(char charCode)
        {
            if (IsMnemonic(charCode, Text) && CanSelect)
            {
                SelectNextControl(null, true, true, true, false);
                return true;
            }
            return false;
        }

        private Rectangle CalculateDisplayRectangle()
        {
            const int borderPadding = 3;
            int bWidth = BorderElements.Width;
            int hWidth = Text == string.Empty ? 0 : HeaderElements.Width;
            int fHeight = Text == string.Empty ? 0 : Font.Height;
            Rectangle r = new Rectangle();
            TextSide tSide = GetTextSide();

            switch (tSide)
            {
                case TextSide.Top:
                    {
                        switch (GroupBoxStyle)
                        {
                            case GroupBoxStyle.Standard:
                                {
                                    r.X = bWidth + borderPadding + 1;
                                    r.Y = fHeight + borderPadding * 2 + RadiusSpacing() - 2;
                                    r.Height = ClientSize.Height - r.Y - r.X - RadiusSpacing();
                                    r.Width = ClientSize.Width - r.X * 2;
                                }
                                break;
                            case GroupBoxStyle.Enhance:
                                {
                                    r.X = bWidth + borderPadding + 1;
                                    r.Y = fHeight + hWidth * 2 + borderPadding * 2 + RadiusSpacing() - 2;
                                    r.Height = ClientSize.Height - r.Y - r.X - RadiusSpacing();
                                    r.Width = ClientSize.Width - r.X * 2;
                                }
                                break;
                            case GroupBoxStyle.Excitative:
                                {
                                    r.X = borderPadding;
                                    r.Y = fHeight + hWidth * 2 + borderPadding + 3;
                                    r.Height = ClientSize.Height - r.Y - r.X;
                                    r.Width = ClientSize.Width - r.X * 2;
                                }
                                break;
                            case GroupBoxStyle.Header:
                                {
                                    r.X = borderPadding;
                                    r.Y = fHeight + hWidth * 2 + borderPadding + 3;
                                    r.Height = ClientSize.Height - r.Y - r.X;
                                    r.Width = ClientSize.Width - r.X * 2;
                                }
                                break;
                        }

                    }
                    break;
                case TextSide.Bottom:
                    {
                        switch (GroupBoxStyle)
                        {
                            case GroupBoxStyle.Standard:
                                {
                                    r.X = bWidth + borderPadding + 1;
                                    r.Y = bWidth + borderPadding + RadiusSpacing();
                                    r.Height = ClientSize.Height - r.Y - fHeight - hWidth - RadiusSpacing() - 2;
                                    r.Width = ClientSize.Width - r.X * 2;
                                }
                                break;
                            case GroupBoxStyle.Enhance:
                                {
                                    r.X = bWidth + borderPadding + 1;
                                    r.Y = bWidth + borderPadding * 2 + RadiusSpacing();
                                    r.Height = ClientSize.Height - r.Y - fHeight - hWidth * 2 - RadiusSpacing() - 2;
                                    r.Width = ClientSize.Width - r.X * 2;
                                }
                                break;
                            case GroupBoxStyle.Excitative:
                                {
                                    r.X = borderPadding;
                                    r.Y = fHeight + hWidth * 2 + borderPadding + 3;
                                    r.Height = ClientSize.Height - r.Y - r.X;
                                    r.Width = ClientSize.Width - r.X * 2;
                                }
                                break;
                            case GroupBoxStyle.Header:
                                {
                                    r.X = borderPadding;
                                    r.Y = fHeight + hWidth * 2 + borderPadding + 3;
                                    r.Height = ClientSize.Height - r.Y - r.X;
                                    r.Width = ClientSize.Width - r.X * 2;
                                }
                                break;
                        }
                    }
                    break;
            }

            return r;
        }

        private void ControlShapeInternal(PaintEventArgs e)
        {
            GraphicsPath controlEdge = new GraphicsPath();
            RectangleF rec = ClientRectangle;
            controlEdge.AddPath(e.Graphics.RoundedRectanglePath(rec, _controlShape.Radius, _controlShape.BorderCorners), true);
            Region = new Region(controlEdge);
            Region.Dispose();
        }

        private TextSide GetTextSide()
        {
            switch (HeaderElements.TextAlignment)
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

        private int RadiusSpacing()
        {
            if (BorderElements.BorderCorners == BorderCorners.None)
            {
                return 0;
            }

            switch (BorderElements.Radius)
            {
                case 0:
                    return 0;

                case 1:
                    return 1;

                case 2:
                case 3:
                    return 2;

                case 4:
                case 5:
                    return 3;

                case 6:
                case 7:
                    return 4;

                case 8:
                case 9:
                case 10:
                    return 5;
            }

            return 0;
        }

        private void UpdateLayout()
        {
            LayoutEngine.Layout(this, new LayoutEventArgs(this, "DisplayRectangle"));
        }

        #endregion Methods

        #region Delegates + Events

        /// <summary>
        /// Occurs when the value of the AutoSize property changes.
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        new public event EventHandler AutoSizeChanged
        {
            add
            {
                base.AutoSizeChanged += value;
            }
            remove
            {
                base.AutoSizeChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the user clicks the GroupBox control.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event EventHandler Click
        {
            add
            {
                base.Click += value;
            }
            remove
            {
                base.Click -= value;
            }
        }

        /// <summary>
        /// Occurs when the user double-clicks the GroupBox control.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event EventHandler DoubleClick
        {
            add
            {
                base.DoubleClick += value;
            }
            remove
            {
                base.DoubleClick -= value;
            }
        }

        /// <summary>
        /// Occurs when the user presses a key while the GroupBox control has focus.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event KeyEventHandler KeyDown
        {
            add
            {
                base.KeyDown += value;
            }
            remove
            {
                base.KeyDown -= value;
            }
        }

        /// <summary>
        /// Occurs when the user presses a key while the GroupBox control has focus.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event KeyPressEventHandler KeyPress
        {
            add
            {
                base.KeyPress += value;
            }
            remove
            {
                base.KeyPress -= value;
            }
        }

        /// <summary>
        ///  Occurs when the user releases a key while the GroupBox control has focus.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event KeyEventHandler KeyUp
        {
            add
            {
                base.KeyUp += value;
            }
            remove
            {
                base.KeyUp -= value;
            }
        }

        /// <summary>
        /// Occurs when the user clicks the GroupBox control with the mouse.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event MouseEventHandler MouseClick
        {
            add
            {
                base.MouseClick += value;
            }
            remove
            {
                base.MouseClick -= value;
            }
        }

        /// <summary>
        /// Occurs when the user double-clicks the GroupBox control with the mouse.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event MouseEventHandler MouseDoubleClick
        {
            add
            {
                base.MouseDoubleClick += value;
            }
            remove
            {
                base.MouseDoubleClick -= value;
            }
        }

        /// <summary>
        /// Occurs when the user presses a mouse button while the mouse pointer is over the control.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event MouseEventHandler MouseDown
        {
            add
            {
                base.MouseDown += value;
            }
            remove
            {
                base.MouseDown -= value;
            }
        }

        /// <summary>
        /// Occurs when the mouse pointer enters the control.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event EventHandler MouseEnter
        {
            add
            {
                base.MouseEnter += value;
            }
            remove
            {
                base.MouseEnter -= value;
            }
        }

        /// <summary>
        /// Occurs when the mouse pointer leaves the control.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event EventHandler MouseLeave
        {
            add
            {
                base.MouseLeave += value;
            }
            remove
            {
                base.MouseLeave -= value;
            }
        }

        /// <summary>
        /// Occurs when the user moves the mouse pointer over the control.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event MouseEventHandler MouseMove
        {
            add
            {
                base.MouseMove += value;
            }
            remove
            {
                base.MouseMove -= value;
            }
        }

        /// <summary>
        /// Occurs when the user releases a mouse button while the mouse pointer is over the control.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event MouseEventHandler MouseUp
        {
            add
            {
                base.MouseUp += value;
            }
            remove
            {
                base.MouseUp -= value;
            }
        }

        /// <summary>
        /// Occurs when the value of the TabStop property changes.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event EventHandler TabStopChanged
        {
            add
            {
                base.TabStopChanged += value;
            }
            remove
            {
                base.TabStopChanged -= value;
            }
        }

        #endregion Delegates + Events

        #region Structs + Classes + Enums

        [System.Runtime.InteropServices.ComVisible(true)]
        internal class EnhanceGroupBoxAccessibleObject : ControlAccessibleObject
        {

            #region Constructors

            internal EnhanceGroupBoxAccessibleObject(EnhanceGroupBox owner) : base(owner)
            {
            }

            #endregion Constructors

            #region Properties

            public override AccessibleRole Role
            {
                get
                {
                    AccessibleRole role = Owner.AccessibleRole;
                    if (role != AccessibleRole.Default)
                    {
                        return role;
                    }
                    return AccessibleRole.Grouping;
                }
            }

            #endregion Properties

        }

        private enum TextSide
        {
            Top,
            Bottom
        }

        #endregion Structs + Classes + Enums

    }

    /// <summary>
    /// Property changed event with PropertyChangeTypes indicator.
    /// </summary>
    internal class PropertyChanged : EventArgs
    {
        private PropertyChangeTypes _message;

        private PropertyChanged()
        {
            _message = 0;
        }
        /// <summary>
        /// Property value changed on the control.
        /// </summary>
        /// <param name="msg">Integer value indication what type of change.</param>
        public PropertyChanged(PropertyChangeTypes msg) : base()
        {
            _message = msg;
        }
    }

    /// <summary>
    /// Enumeration of different property change types.
    /// </summary>
    [Flags]
    public enum PropertyChangeTypes : int
    {
        /// <summary>
        /// Default value.
        /// </summary>
        None = 0,
        /// <summary>
        /// Graphic element has changed
        /// </summary>
        GraphicElements = 1,
        /// <summary>
        /// Control layout needed
        /// </summary>
        LayoutNeeded = 2
    }
}