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
        private const int ArraySize = 5;
        public HeaderStyle()
        {
            InitGroupBoxes();
        }

        private void InitGroupBoxes()
        {
            _groupBoxes = new GroupBox[ArraySize];

            for (int i = 0; i < ArraySize; i++)
            {
                _groupBoxes[i] = new GroupBox();
            }

            
            _groupBoxes[0].Text = "Header";
            _groupBoxes[0].GroupBoxStyle = GroupBoxStyle.Header;
            _groupBoxes[0].BackColor = System.Drawing.Color.Snow;
            _groupBoxes[0].HeaderElements.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[0].HeaderElements.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[0].BorderElements.Width = 0;
            

            
            _groupBoxes[1].Text = "Header";
            _groupBoxes[1].GroupBoxStyle = GroupBoxStyle.Header;
            _groupBoxes[1].BackColor = System.Drawing.Color.Snow;
            _groupBoxes[1].HeaderElements.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[1].HeaderElements.BorderColor = System.Drawing.Color.Transparent;


            
            _groupBoxes[2].Text = "Header";
            _groupBoxes[2].GroupBoxStyle = GroupBoxStyle.Header;
            _groupBoxes[2].HeaderElements.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[2].HeaderElements.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[2].BorderElements.Radius = 10;
            _groupBoxes[2].BorderElements.BorderCorners = BorderCorners.All;
            _groupBoxes[2].BackColor = System.Drawing.Color.White;

            
            _groupBoxes[3].Text = "Header";
            _groupBoxes[3].GroupBoxStyle = GroupBoxStyle.Header;
            _groupBoxes[3].HeaderElements.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[3].HeaderElements.TextAlignment = BorderTextAlignment.BottomCenter;
            _groupBoxes[3].InsideBorderElements.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[3].HeaderElements.Width = 0;
            _groupBoxes[3].BorderElements.Radius = 10;
            _groupBoxes[3].BorderElements.BorderCorners = BorderCorners.All;
            _groupBoxes[3].BackColor = System.Drawing.SystemColors.ControlDark;

           
            _groupBoxes[4].Text = "Header";
            _groupBoxes[4].GroupBoxStyle = GroupBoxStyle.Header;
            _groupBoxes[4].BackColor = System.Drawing.Color.Tomato;
            _groupBoxes[4].HeaderElements.GradientStartColor = System.Drawing.Color.Tomato;
            _groupBoxes[4].HeaderElements.GradientEndColor = System.Drawing.SystemColors.Control;
            _groupBoxes[4].HeaderElements.GradientMode = EnhanceGroupBoxGradientMode.Horizontal;
            _groupBoxes[4].HeaderElements.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            _groupBoxes[4].HeaderElements.TextAlignment = BorderTextAlignment.TopCenter;
            _groupBoxes[4].InsideBorderElements.GradientStartColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[4].InsideBorderElements.GradientEndColor = System.Drawing.SystemColors.Control;
            _groupBoxes[4].InsideBorderElements.GradientMode = EnhanceGroupBoxGradientMode.Vertical;
            _groupBoxes[4].HeaderElements.Width = 0;



        }

        public Ekstrand.Windows.Forms.GroupBox[] GroupBoxes
        {
            get { return _groupBoxes; }
        }
    }
}
