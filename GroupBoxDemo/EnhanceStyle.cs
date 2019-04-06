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
        private const int ArraySize = 4;
        public EnhanceStyle()
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
            
            _groupBoxes[0].Text = "Enhance";
            _groupBoxes[0].GroupBoxStyle = GroupBoxStyle.Enhance;
            _groupBoxes[0].HeaderElements.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[0].HeaderElements.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[0].HeaderElements.TextAlignment = BorderTextAlignment.TopRight;
            _groupBoxes[0].BorderElements.BorderColor = System.Drawing.Color.Black;         
                        
            _groupBoxes[1].Text = "Enhance";
            _groupBoxes[1].GroupBoxStyle = GroupBoxStyle.Enhance;
            _groupBoxes[1].BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[1].BorderElements.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[1].BorderElements.Radius = 5;
            _groupBoxes[1].BorderElements.BorderCorners = BorderCorners.All;
            _groupBoxes[1].HeaderElements.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[1].HeaderElements.Radius = 0;
            _groupBoxes[1].HeaderElements.Width = 1;
            _groupBoxes[1].HeaderElements.BorderColor = System.Drawing.Color.DarkBlue;
            _groupBoxes[1].HeaderElements.TextAlignment = BorderTextAlignment.TopCenter;

            _groupBoxes[2].Text = "Enhance";
            _groupBoxes[2].GroupBoxStyle = GroupBoxStyle.Enhance;
            _groupBoxes[2].BackColor = System.Drawing.Color.LightGray;
            _groupBoxes[2].HeaderElements.TextAlignment = BorderTextAlignment.TopRight;
            _groupBoxes[2].BorderElements.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[2].HeaderElements.BorderColor = System.Drawing.Color.Black;
            _groupBoxes[2].HeaderElements.BackColor = System.Drawing.Color.SkyBlue;
            _groupBoxes[2].HeaderElements.Radius = 5;
            _groupBoxes[2].HeaderElements.BorderCorners = BorderCorners.All;
            _groupBoxes[2].HeaderElements.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            _groupBoxes[2].InsideBorderElements.GradientStartColor = System.Drawing.Color.LightGray;
            _groupBoxes[2].InsideBorderElements.GradientEndColor = System.Drawing.SystemColors.Control;
            _groupBoxes[2].InsideBorderElements.GradientMode = EnhanceGroupBoxGradientMode.Vertical;
            
            _groupBoxes[3].Text = "Enhance";
            _groupBoxes[3].GroupBoxStyle = GroupBoxStyle.Enhance;
            _groupBoxes[3].HeaderElements.GradientStartColor = System.Drawing.Color.Red;
            _groupBoxes[3].HeaderElements.GradientEndColor = System.Drawing.Color.White;
            _groupBoxes[3].HeaderElements.GradientMode = EnhanceGroupBoxGradientMode.Horizontal;
            _groupBoxes[3].HeaderElements.TextAlignment = BorderTextAlignment.TopCenter;
            _groupBoxes[3].HeaderElements.Radius = 5;
            _groupBoxes[3].HeaderElements.BorderCorners = BorderCorners.TopLeft | BorderCorners.TopRight;
            _groupBoxes[3].HeaderElements.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            _groupBoxes[3].HeaderElements.Width = 1;
            _groupBoxes[3].InsideBorderElements.GradientMode = EnhanceGroupBoxGradientMode.Vertical;
            _groupBoxes[3].InsideBorderElements.GradientStartColor = System.Drawing.Color.WhiteSmoke;
            _groupBoxes[3].InsideBorderElements.GradientEndColor = System.Drawing.SystemColors.Control;

        }

        public Ekstrand.Windows.Forms.GroupBox[] GroupBoxes
        {
            get { return _groupBoxes; }
        }
    }
}
