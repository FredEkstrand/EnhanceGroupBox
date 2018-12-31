using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ekstrand.Windows.Forms;

namespace GroupBoxDemo
{
    internal class ExcitativeStyle
    {
        private Ekstrand.Windows.Forms.GroupBox[] _groupBoxes;

        public ExcitativeStyle()
        {
            InitGroupBoxes();
        }

        private void InitGroupBoxes()
        {
            _groupBoxes = new GroupBox[4];

            for (int i = 0; i < 4; i++)
            {
                _groupBoxes[i] = new GroupBox();
            }

            
            _groupBoxes[0].Text = "Excitative";
            _groupBoxes[0].GroupBoxStyles = GroupBoxStyles.Excitative;

            // top left text with header text area with rounded corners
            _groupBoxes[1].Text = "Excitative";
            _groupBoxes[1].GroupBoxStyles = GroupBoxStyles.Excitative;
            _groupBoxes[1].Header.TextAlignment = BorderTextAlignment.TopCenter;
            _groupBoxes[1].BorderItems.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[1].Header.Width = 0;


            
            _groupBoxes[2].Text = "Excitative";
            _groupBoxes[2].GroupBoxStyles = GroupBoxStyles.Excitative;
            _groupBoxes[2].BorderItems.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[2].Header.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[2].Header.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[2].Header.Radius = 5;
            _groupBoxes[2].Header.BorderCorners = BorderCorners.All;
            _groupBoxes[2].Header.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            
            _groupBoxes[3].Text = "Excitative";
            _groupBoxes[3].GroupBoxStyles = GroupBoxStyles.Excitative;
            _groupBoxes[3].Header.GradientMode = EnhanceGroupBoxGradientMode.Horizontal;
            _groupBoxes[3].Header.GradientStartColor = System.Drawing.Color.Tomato;
            _groupBoxes[3].Header.GradientEndColor = System.Drawing.SystemColors.Control;
            _groupBoxes[3].Header.Radius = 5;
            _groupBoxes[3].Header.BorderCorners = BorderCorners.None;
            _groupBoxes[3].Header.TextAlignment = BorderTextAlignment.TopCenter;
            _groupBoxes[3].Header.Width = 0;
            _groupBoxes[3].BorderItems.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[3].BorderItems.BackColor = System.Drawing.Color.Transparent;
            _groupBoxes[3].InsideBorder.GradientStartColor = System.Drawing.Color.Khaki;
            _groupBoxes[3].InsideBorder.GradientEndColor = System.Drawing.SystemColors.Control;
            _groupBoxes[3].InsideBorder.GradientMode = EnhanceGroupBoxGradientMode.Vertical;




        }

        public Ekstrand.Windows.Forms.GroupBox[] GroupBoxes
        {
            get { return _groupBoxes; }
        }
    }
}
