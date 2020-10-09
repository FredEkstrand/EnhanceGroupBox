using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ekstrand.Drawing;

namespace GroupBoxDemo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = ClientRectangle;
            r.Inflate(-10,-10);
            int width = 1;
            int.TryParse(textBox1.Text,out width);
            Pen p = new Pen(Color.Black, width);
            p.Alignment = PenAlignment.Inset;
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }

}
