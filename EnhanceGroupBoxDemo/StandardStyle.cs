﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ekstrand.Windows.Forms;
namespace GroupBoxDemo
{
    internal class StandardStyle
    {
        private Ekstrand.Windows.Forms.GroupBox[] _groupBoxes;

        public StandardStyle()
        {
            InitGroupBoxes();
        }

        private void InitGroupBoxes()
        {
            _groupBoxes = new GroupBox[6];

            for(int i = 0; i < 6; i++)
            {
                _groupBoxes[i] = new GroupBox();
            }

            // top left text with square border colored control dark (default)
            _groupBoxes[0].Text = "Standard";
            
            // top center text with dash border color tomato red
            _groupBoxes[1].Text = "Standard";
            _groupBoxes[1].BorderItems.BorderColor = System.Drawing.Color.Tomato;
            _groupBoxes[1].BorderItems.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            _groupBoxes[1].Header.TextAlignment = BorderTextAlignment.TopCenter;

            // top right text with dash border color tomato red and width
            _groupBoxes[2].Text = "Standard";
            _groupBoxes[2].BorderItems.BorderColor = System.Drawing.Color.Tomato;
            _groupBoxes[2].BorderItems.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            _groupBoxes[2].BorderItems.Width = 2;
            _groupBoxes[2].Header.TextAlignment = BorderTextAlignment.TopRight;

            // bottom left text with width to normal
            _groupBoxes[3].Text = "Standard";
            _groupBoxes[3].BorderItems.BorderColor = System.Drawing.Color.Tomato;
            _groupBoxes[3].BorderItems.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            _groupBoxes[3].BorderItems.Width = 1;
            _groupBoxes[3].Header.TextAlignment = BorderTextAlignment.BottomLeft;

            // bottom center text with all corners having a radius value
            _groupBoxes[4].Text = "Standard";
            _groupBoxes[4].BorderItems.BorderColor = System.Drawing.Color.Tomato;
            _groupBoxes[4].BorderItems.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            _groupBoxes[4].BorderItems.Radius = 10;
            _groupBoxes[4].BorderItems.BorderCorners = BorderCorners.All;
            _groupBoxes[4].BorderItems.Width = 1;
            _groupBoxes[4].Header.TextAlignment = BorderTextAlignment.BottomCenter;
            _groupBoxes[4].ForeColor = System.Drawing.Color.DarkOrange;


            // bottom right text with top two corners having a radius value
            _groupBoxes[5].Text = "Standard";
            _groupBoxes[5].BorderItems.BorderColor = System.Drawing.Color.Tomato;
            _groupBoxes[5].BorderItems.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            _groupBoxes[5].BorderItems.Width = 1;
            _groupBoxes[5].BorderItems.Radius = 5;
            _groupBoxes[5].BorderItems.BorderCorners = BorderCorners.TopLeft | BorderCorners.TopRight;
            _groupBoxes[5].Header.TextAlignment = BorderTextAlignment.BottomRight;

        }

        public Ekstrand.Windows.Forms.GroupBox[] GroupBoxes
        {
            get { return _groupBoxes; }
        }
    }
}