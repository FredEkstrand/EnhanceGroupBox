using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ekstrand.Windows.Forms;

namespace GroupBoxDemo
{
    internal class EnhanceStyle
    {
        private Ekstrand.Windows.Forms.GroupBox[] _groupBoxes;

        public EnhanceStyle()
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

            // top left text with text background color SkyBlue
            _groupBoxes[0].Text = "Enhance";
            _groupBoxes[0].GroupBoxStyles = GroupBoxStyles.Enhance;
            _groupBoxes[0].Header.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[0].BorderItems.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[0].Header.BorderColor = System.Drawing.Color.Black;

            // top left text with header text area with rounded corners
            _groupBoxes[1].Text = "Enhance";
            _groupBoxes[1].GroupBoxStyles = GroupBoxStyles.Enhance;
            _groupBoxes[1].BorderItems.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[1].Header.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[1].Header.Radius = 0;
            _groupBoxes[1].Header.Width = 0;
            

            // top right text with dash border color tomato red and width
            _groupBoxes[2].Text = "Enhance";
            _groupBoxes[2].GroupBoxStyles = GroupBoxStyles.Enhance;
            _groupBoxes[2].BorderItems.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[2].Header.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[2].Header.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[2].Header.Radius = 5;
            _groupBoxes[2].Header.BorderCorners = BorderCorners.All;
            _groupBoxes[2].Header.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            // bottom left text with width to normal
            _groupBoxes[3].Text = "Enhance";
            _groupBoxes[3].GroupBoxStyles = GroupBoxStyles.Enhance;
            _groupBoxes[3].Header.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[3].Header.Radius = 5;
            _groupBoxes[3].Header.BorderCorners = BorderCorners.TopLeft | BorderCorners.TopRight;
            _groupBoxes[3].Header.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            _groupBoxes[3].Header.TextAlignment = BorderTextAlignment.TopCenter;
            _groupBoxes[3].InsideBorder.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[3].Header.Width = 0;

            // bottom left text with width to normal
            _groupBoxes[4].Text = "Enhance";
            _groupBoxes[4].GroupBoxStyles = GroupBoxStyles.Enhance;
            _groupBoxes[4].Header.GradientStartColor = System.Drawing.Color.Tomato;
            _groupBoxes[4].Header.GradientEndColor = System.Drawing.SystemColors.Control;
            _groupBoxes[4].Header.GradientMode = EnhanceGroupBoxGradientMode.Horizontal;            
            _groupBoxes[4].Header.Radius = 5;
            _groupBoxes[4].Header.BorderCorners = BorderCorners.None;
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
