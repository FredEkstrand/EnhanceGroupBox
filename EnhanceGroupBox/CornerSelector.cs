using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ekstrand;

namespace Ekstrand.Windows.Forms
{
    //[Flags]
    //public enum BorderCorners
    //{

    //    None = 0x01,
    //    TopLeft = 0x02,
    //    TopRight = 0x04,
    //    BottomLeft = 0x08,
    //    BottomRight = 0x10,
    //    All = TopLeft | TopRight | BottomLeft | BottomRight
    //}

    internal partial class CornerSelector : UserControl
    {

        #region Fields

        private BorderCorners _borderCorners;
        private Color _normalState = SystemColors.ControlLight;
        private Color _selectedState = Color.PaleGreen;

        #endregion Fields

        #region Constructors

        public CornerSelector()
        {
            InitializeComponent();
            InitButtonTag();
            _borderCorners = BorderCorners.None;
            btnNone.BackColor = _selectedState;
            btnNone.Focus();
            btnAll.BackColor = btnBL.BackColor = btnBR.BackColor = 
                btnTL.BackColor = btnTR.BackColor = _normalState;
        }

        #endregion Constructors

        #region Properties

        public BorderCorners BorderCorners
        {
            get { return _borderCorners; }
            set
            {
                ClearFlags();
                SetFlag(value, true);   
                SetBorderCorners();
            }
        }

        public string BorderCornersText
        {
            get { return DisplaySelection().Trim(); }
        }

        #endregion Properties

        #region Methods

        public string DisplaySelection()
        {
            StringBuilder sb = new StringBuilder();
            List<BorderCorners> items = EnumUtil.EnumList<BorderCorners>();

            for (int i = 0; i < items.Count; i++)
            {
                if (_borderCorners.HasFlag(items[i]))
                {
                    sb.Append(Enum.GetName(typeof(BorderCorners), items[i]).ToString() + " ");
                }

            }

            return sb.ToString();
        }

        private void AllRule(bool select)
        {
            SetFlag(BorderCorners.All, select);
        }

        private void AllRule()
        {
            if (GetFlag(BorderCorners.All))
            {
                btnAll.BackColor = btnBL.BackColor = btnBR.BackColor = btnTL.BackColor = btnTR.BackColor = _selectedState;
            }
            else
            {
                btnAll.BackColor = _normalState;
            }

            if (GetFlag(BorderCorners.None))
            {
                btnNone.BackColor = _normalState;
                SetFlag(BorderCorners.None, false);
            }
        }

        private void BottomLeftRule(bool select)
        {
            SetFlag(BorderCorners.BottomLeft, select);
        }

        private void BottomRightRule(bool select)
        {
            SetFlag(BorderCorners.BottomRight, select);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b == null)
            {
                throw new NullReferenceException("Button is Null.");
            }

            b.BackColor = IsSelected(b.BackColor) ? _normalState : _selectedState;
            SelectionRules((BorderCorners)b.Tag, IsSelected(b.BackColor));
        }

        private void ClearFlags()
        {
            SetFlag(BorderCorners.All|BorderCorners.None, false);
            btnAll.BackColor = btnBL.BackColor = btnBR.BackColor = btnTL.BackColor = btnTR.BackColor = btnNone.BackColor = _normalState;
        }

        private Button GetButton(BorderCorners bc)
        {
            switch (bc)
            {
                case BorderCorners.TopLeft:
                return btnTL;
                case BorderCorners.TopRight:
                return btnTR;
                case BorderCorners.BottomLeft:
                return btnBL;
                case BorderCorners.BottomRight:
                return btnBR;
                case BorderCorners.All:
                return btnAll;
                case BorderCorners.None:
                return btnNone;
            }

            return null;
        }

        private bool GetFlag(BorderCorners flag)
        {
            return ((_borderCorners & flag) == flag);
        }

        private void InitButtonTag()
        {
            btnTL.Tag = BorderCorners.TopLeft;
            btnTR.Tag = BorderCorners.TopRight;
            btnBL.Tag = BorderCorners.BottomLeft;
            btnBR.Tag = BorderCorners.BottomRight;
            btnAll.Tag = BorderCorners.All;
            btnNone.Tag = BorderCorners.None;
        }

        private bool IsSelected(Color c)
        {
            return !c.Equals(_normalState);
        }

        private void NoneRule(bool select)
        {
            SetFlag(BorderCorners.None, select);
            if (GetFlag(BorderCorners.None))
            {
                btnAll.BackColor = btnBL.BackColor = btnBR.BackColor = btnTL.BackColor = btnTR.BackColor = _normalState;
                SetFlag(BorderCorners.All, false);
            }
        }

        private void NoneRule()
        {
            if ((int)_borderCorners == 0)
            {
                SetFlag(BorderCorners.None, true);
                btnNone.BackColor = _selectedState;
            }
        }

        private void SelectionRules(BorderCorners bc, bool select)
        {
            switch (bc)
            {
                case BorderCorners.All:
                AllRule(select);
                break;
                case BorderCorners.BottomLeft:
                BottomLeftRule(select);
                break;
                case BorderCorners.BottomRight:
                BottomRightRule(select);
                break;
                case BorderCorners.TopLeft:
                TopLeftRule(select);
                break;
                case BorderCorners.TopRight:
                TopRightRule(select);
                break;
                case BorderCorners.None:
                NoneRule(select);
                break;

            }

            AllRule();
            NoneRule();
            Invalidate();
        }

        private void SetBorderCorners()
        {
            List<BorderCorners> items = EnumUtil.EnumList<BorderCorners>();
            for(int i = 0; i < items.Count; i++)
            {
                if(GetFlag(items[i]))
                {
                    Button b = GetButton(items[i]);
                    b.BackColor = IsSelected(b.BackColor) ? _normalState : _selectedState;
                }
            }
        }

        private void SetFlag(BorderCorners flag, bool value)
        {
            if (value)
            {
                _borderCorners |= flag;
            }
            else
            {
                _borderCorners &= ~flag;
            }

        }

        private void TopLeftRule(bool select)
        {
            SetFlag(BorderCorners.TopLeft, select);
        }

        private void TopRightRule(bool select)
        {
            SetFlag(BorderCorners.TopRight,select);
        }

        #endregion Methods

    }
}
