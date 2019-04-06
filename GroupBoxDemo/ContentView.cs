using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GroupBoxDemo
{
    public partial class ContentView : UserControl
    {
        #region Constructors

        public ContentView()
        {
            InitializeComponent();
            ShowScrollBar(flowLayoutPanel1.Handle, (int)ScrollBarDirection.SB_VERT, true);
        }

        #endregion Constructors

        #region Properties

        public FlowLayoutPanel FPanel
        {
            get { return flowLayoutPanel1; }
        }

        public PropertyGrid Grid
        {
            get { return propertyGrid1; }
        }

        #endregion Properties

        #region Methods

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        #endregion Methods

        #region Structs + Classes + Enums

        private enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        #endregion Structs + Classes + Enums
    }
}
