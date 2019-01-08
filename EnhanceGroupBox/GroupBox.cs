using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Security.Permissions;
using System.Diagnostics.CodeAnalysis;
using Ekstrand.Drawing;
using System.Web.UI.Design;

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
    ToolboxItem(true),
    ToolboxBitmap(typeof(GroupBox), "Resources.GroupBox_16x"),
    Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]  
    public class GroupBox : Control
    {

        #region Fields

        private const int STATE2_USEPREFERREDSIZECACHE = 0x00000800;
        private BorderElements _borderElements;
        private Rectangle _clientRectangle;
        private Color _disabledTextColor = SystemColors.GrayText;
        private GroupBoxStyles _groupBoxStyles = GroupBoxStyles.Standard;
        private HeaderElements _headerElements;
        private InsideBorderElements _insideBoarderElements;
        private TextFormatFlags _textFlags = TextFormatFlags.Default | TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak | TextFormatFlags.PreserveGraphicsTranslateTransform | TextFormatFlags.PreserveGraphicsClipping;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Represents a control that creates a container that has a border and a header for user interface (UI) content.
        /// </summary>
        public GroupBox() : base()
        {
            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            SetStyle(ControlStyles.Selectable, false);
            TabStop = false;

            _headerElements = new HeaderElements(this);
            _insideBoarderElements = new InsideBorderElements(this);
            _borderElements = new BorderElements(this);

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
        [Description("Properties for BorderItems apperance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public BorderElements BorderItems
        {
            get { return _borderElements; }
            private set { _borderElements = value; }
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
            set { _disabledTextColor = value; }
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
                Invalidate();
            }
        }

        /// <summary>
        /// Get or sets GroupBox Style used for rendering
        /// </summary>
        [Browsable(true)]
        [DefaultValue(GroupBoxStyles.Standard)]
        [NotifyParentProperty(true)]
        [Category("Appearance")]
        [Description("Specifies the rendering style to use")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public GroupBoxStyles GroupBoxStyles
        {
            get { return _groupBoxStyles; }
            set
            {
                if (value != _groupBoxStyles)
                {
                    _groupBoxStyles = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets header properties
        /// </summary>
        [NotifyParentProperty(true)]
        [CategoryAttribute("Appearance")]
        [Description("Properties for header area apperance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public HeaderElements Header
        {
            get { return _headerElements; }
            set { _headerElements = value;}

        }

        /// <summary>
        /// Gets or sets inside border properties
        /// </summary>
        [NotifyParentProperty(true)]
        [CategoryAttribute("Appearance")]
        [Description("Properties for InsideBorder area apperance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public InsideBorderElements InsideBorder
        {
            get { return _insideBoarderElements; }
            private set {  _insideBoarderElements = value; }
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
                base.Text = value;
                Invalidate();
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
        /// <returns>Return the text repersentation of the control</returns>
        public override string ToString()
        {

            string s = base.ToString();
            return s + ", Text: " + Text;
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
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            ControlShape(e);

            EnhanceGroupBoxState state = Enabled ? EnhanceGroupBoxState.Normal : EnhanceGroupBoxState.Disabled;
            GroupBoxRenderer.DrawEnhanceGroupBox(this, e.Graphics, ClientRectangle, Text, Font, ForeColor, _textFlags, state);

            e.Graphics.SmoothingMode = cachSmothingMode;

            // pass to base last
            base.OnPaint(e); // raise paint event
        }

        /// <summary>
        /// Processes a mnemonic character.
        /// </summary>
        /// <param name="charCode">The character to process.</param>
        /// <returns></returns>
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

        private void ControlShape(PaintEventArgs e)
        {
            GraphicsPath controlEdge = new GraphicsPath();
            Rectangle newRectangle = ClientRectangle;
            controlEdge.AddPath(e.Graphics.RoundedRectanglePath(newRectangle, BorderItems.Radius, BorderItems.BorderCorners), true);

            Region = new Region(controlEdge);
            _clientRectangle = Rectangle.Inflate(ClientRectangle, -4, -4);


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

            internal EnhanceGroupBoxAccessibleObject(GroupBox owner) : base(owner)
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

        #endregion Structs + Classes + Enums

    }
}