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
        private const int ArraySize = 4;
        public ExcitativeStyle()
        {
            InitGroupBoxes();
        }

        private void InitGroupBoxes()
        {
            _groupBoxes = new Ekstrand.Windows.Forms.GroupBox[ArraySize];

            for (int i = 0; i < ArraySize; i++)
            {
                _groupBoxes[i] = new Ekstrand.Windows.Forms.GroupBox();
            }

            
            _groupBoxes[0].Text = "Excitative";
            _groupBoxes[0].GroupBoxStyle = GroupBoxStyle.Excitative;
            _groupBoxes[0].HeaderElements.Width = 0;

            // top left text with header text area with rounded corners
            _groupBoxes[1].Text = "Excitative";
            _groupBoxes[1].GroupBoxStyle = GroupBoxStyle.Excitative;
            _groupBoxes[1].HeaderElements.TextAlignment = BorderTextAlignment.TopLeft;
            _groupBoxes[1].HeaderElements.BackColor = System.Drawing.Color.Tomato;
            _groupBoxes[1].HeaderElements.Width = 0;
           
            _groupBoxes[2].Text = "Excitative";
            _groupBoxes[2].GroupBoxStyle = GroupBoxStyle.Excitative;
            _groupBoxes[2].BorderElements.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[2].HeaderElements.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[2].HeaderElements.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[2].HeaderElements.Radius = 5;
            _groupBoxes[2].HeaderElements.BorderCorners = BorderCorners.All;
            _groupBoxes[2].HeaderElements.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;


            _groupBoxes[3].Text = "Excitative";
            _groupBoxes[3].GroupBoxStyle = GroupBoxStyle.Excitative;
            _groupBoxes[3].BackColor = System.Drawing.SystemColors.ControlLight;
            _groupBoxes[3].HeaderElements.GradientMode = EnhanceGroupBoxGradientMode.Horizontal;
            _groupBoxes[3].HeaderElements.GradientStartColor = System.Drawing.Color.Red;
            _groupBoxes[3].HeaderElements.GradientEndColor = System.Drawing.SystemColors.Control;
            _groupBoxes[3].HeaderElements.BorderCorners = BorderCorners.None;
            _groupBoxes[3].HeaderElements.Radius = 0;            
            _groupBoxes[3].HeaderElements.TextAlignment = BorderTextAlignment.BottomCenter;
            _groupBoxes[3].HeaderElements.Width = 0;
            _groupBoxes[3].BorderElements.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[3].BorderElements.BackColor = System.Drawing.Color.Transparent;
            _groupBoxes[3].InsideBorderElements.GradientStartColor = System.Drawing.Color.Khaki;
            _groupBoxes[3].InsideBorderElements.GradientEndColor = System.Drawing.SystemColors.Control;
            _groupBoxes[3].InsideBorderElements.GradientMode = EnhanceGroupBoxGradientMode.Vertical;




        }

        public Ekstrand.Windows.Forms.GroupBox[] GroupBoxes
        {
            get { return _groupBoxes; }
        }
    }
}
