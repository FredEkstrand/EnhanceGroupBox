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
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        public ContentView()
        {
            InitializeComponent();
            ShowScrollBar(flowLayoutPanel1.Handle, (int)ScrollBarDirection.SB_VERT, true);
        }

        private enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        public FlowLayoutPanel FPanel
        {
            get { return flowLayoutPanel1; }
        }

        public PropertyGrid Grid
        {
            get { return propertyGrid1; }
        }
    }
}
