using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ekstrand.Windows.Forms;

namespace GroupBoxDemo
{
    internal class HeaderStyle
    {
        private Ekstrand.Windows.Forms.GroupBox[] _groupBoxes;

        public HeaderStyle()
        {
            InitGroupBoxes();
        }

        private void InitGroupBoxes()
        {
            _groupBoxes = new GroupBox[5];

            for (int i = 0; i < 5; i++)
            {
                _groupBoxes[i] = new GroupBox();
            }

            
            _groupBoxes[0].Text = "Header";
            _groupBoxes[0].GroupBoxStyles = GroupBoxStyles.Header;
            _groupBoxes[0].Header.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[0].Header.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[0].BorderItems.Width = 0;
            

            
            _groupBoxes[1].Text = "Header";
            _groupBoxes[1].GroupBoxStyles = GroupBoxStyles.Header;
            _groupBoxes[1].Header.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[1].Header.BorderColor = System.Drawing.Color.Transparent;


            
            _groupBoxes[2].Text = "Header";
            _groupBoxes[2].GroupBoxStyles = GroupBoxStyles.Header;
            _groupBoxes[2].Header.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[2].Header.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[2].BorderItems.Radius = 10;
            _groupBoxes[2].BorderItems.BorderCorners = BorderCorners.All;
            _groupBoxes[2].BackColor = System.Drawing.Color.White;

            
            _groupBoxes[3].Text = "Header";
            _groupBoxes[3].GroupBoxStyles = GroupBoxStyles.Header;
            _groupBoxes[3].Header.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[3].Header.TextAlignment = BorderTextAlignment.BottomCenter;
            _groupBoxes[3].InsideBorder.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[3].Header.Width = 0;
            _groupBoxes[3].BorderItems.Radius = 10;
            _groupBoxes[3].BorderItems.BorderCorners = BorderCorners.All;
            _groupBoxes[3].BackColor = System.Drawing.Color.White;

           
            _groupBoxes[4].Text = "Header";
            _groupBoxes[4].GroupBoxStyles = GroupBoxStyles.Header;
            _groupBoxes[4].Header.GradientStartColor = System.Drawing.Color.Tomato;
            _groupBoxes[4].Header.GradientEndColor = System.Drawing.SystemColors.Control;
            _groupBoxes[4].Header.GradientMode = EnhanceGroupBoxGradientMode.Horizontal;
            _groupBoxes[4].Header.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            _groupBoxes[4].Header.TextAlignment = BorderTextAlignment.TopCenter;
            _groupBoxes[4].InsideBorder.GradientStartColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[4].InsideBorder.GradientEndColor = System.Drawing.SystemColors.Control;
            _groupBoxes[4].InsideBorder.GradientMode = EnhanceGroupBoxGradientMode.Vertical;
            _groupBoxes[4].Header.Width = 0;



        }

        public Ekstrand.Windows.Forms.GroupBox[] GroupBoxes
        {
            get { return _groupBoxes; }
        }
    }
}
