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
    [Serializable]
    [ComVisible(false),
    ClassInterface(ClassInterfaceType.AutoDispatch),
    DefaultEvent("Enter"),
    DefaultProperty("Text"),
    Description("GroupBox")]
    [ToolboxItem(true)]
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class GroupBox : Control
    {
        #region Fields

        private const int STATE2_USEPREFERREDSIZECACHE = 0x00000800;
        private BorderElements _borderElements;
        private Rectangle _clientRectangle;
        private GroupBoxStyles _groupBoxStyles = GroupBoxStyles.Standard;
        private HeaderElements _headerElements;
        private InsideBorderElements _insideBoarderElements;
        private TextFormatFlags _textFlags = TextFormatFlags.Default | TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak | TextFormatFlags.PreserveGraphicsTranslateTransform | TextFormatFlags.PreserveGraphicsClipping;

        #endregion Fields

        #region Constructors

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

        [NotifyParentProperty(true)]
        [CategoryAttribute("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BorderElements BorderItems
        {
            get { return _borderElements; }
            private set { _borderElements = value; }
        }

        private Color _disabledTextColor = SystemColors.GrayText;

        [NotifyParentProperty(true)]
        [CategoryAttribute("Appearance")]
        [DefaultValue("SystemColors.GrayText")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color DisabledTextColor
        {
            get { return _disabledTextColor; }
            set { _disabledTextColor = value; }
        }

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

        [NotifyParentProperty(true)]
        [CategoryAttribute("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public HeaderElements Header
        {
            get { return _headerElements; }
            set { _headerElements = value;}

        }

        [NotifyParentProperty(true)]
        [CategoryAttribute("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public InsideBorderElements InsideBorder
        {
            get { return _insideBoarderElements; }
            private set {  _insideBoarderElements = value; }
        }

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

        protected override Padding DefaultPadding
        {
            get
            {
                return new Padding(3);
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(200, 100);
            }
        }

        #endregion Properties

        #region Methods

        public override string ToString()
        {

            string s = base.ToString();
            return s + ", Text: " + Text;
        }

        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new EnhanceGroupBoxAccessibleObject(this);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            Invalidate();
        }

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
            controlEdge.AddPath(e.Graphics.GenerateRoundedRectangle(newRectangle, BorderItems.Radius, BorderItems.BorderCorners), true);

            Region = new Region(controlEdge);
            _clientRectangle = Rectangle.Inflate(ClientRectangle, -4, -4);


        }

        #endregion Methods

        #region Delegates + Events

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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        new public event EventHandler TabStopChanged
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