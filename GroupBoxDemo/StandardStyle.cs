using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ekstrand.Windows.Forms;
namespace GroupBoxDemo
{
    internal class StandardStyle
    {
        private Ekstrand.Windows.Forms.GroupBox[] _groupBoxes;
        private const int ArraySize = 3;
        public StandardStyle()
        {
            InitGroupBoxes();
        }

        private void InitGroupBoxes()
        {
            _groupBoxes = new GroupBox[ArraySize];

            for(int i = 0; i < ArraySize; i++)
            {
                _groupBoxes[i] = new GroupBox();
            }
            
            _groupBoxes[0].Text = "Standard";            
            
            _groupBoxes[1].Text = "Standard";
            _groupBoxes[1].BorderElements.BorderColor = System.Drawing.Color.Orange;
            _groupBoxes[1].BorderElements.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            _groupBoxes[1].BorderElements.BorderCorners = BorderCorners.All;
            _groupBoxes[1].BorderElements.Radius = 5;
            _groupBoxes[1].HeaderElements.TextAlignment = BorderTextAlignment.TopCenter;      
            
            _groupBoxes[2].Text = "Standard";
            _groupBoxes[2].BorderElements.BorderColor = System.Drawing.Color.Tomato;
            _groupBoxes[1].BorderElements.BorderCorners = BorderCorners.BottomLeft | BorderCorners.TopRight;
            _groupBoxes[1].BorderElements.Radius = 5;
            _groupBoxes[2].BorderElements.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            _groupBoxes[2].HeaderElements.TextAlignment = BorderTextAlignment.TopRight;

        }

        public Ekstrand.Windows.Forms.GroupBox[] GroupBoxes
        {
            get { return _groupBoxes; }
        }
    }
}
