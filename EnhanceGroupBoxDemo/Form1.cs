using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GroupBoxDemo
{
    public partial class Form1 : Form
    {
        private StandardStyle _standardStyles = new StandardStyle();
        private EnhanceStyle _enhanceStyle = new EnhanceStyle();
        private ExcitativeStyle _excitativeStyle = new ExcitativeStyle();
        private HeaderStyle  _headerStyle = new HeaderStyle();

        public Form1()
        {
            InitializeComponent();          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < _standardStyles.GroupBoxes.Length; i++)
            {
                SelectablePanel sp = new SelectablePanel();
                sp.GroupBox = _standardStyles.GroupBoxes[i];
                sp.Selected += Sp_Selected;
                contentView1.FPanel.Controls.Add(sp);                
            }

            for (int i = 0; i < _enhanceStyle.GroupBoxes.Length; i++)
            {
                SelectablePanel sp = new SelectablePanel();
                sp.GroupBox = _enhanceStyle.GroupBoxes[i];
                sp.Selected += Sp_Selected;
                contentView1.FPanel.Controls.Add(sp);
            }

            for(int i = 0; i < _excitativeStyle.GroupBoxes.Length; i++)
            {
                SelectablePanel sp = new SelectablePanel();
                sp.GroupBox = _excitativeStyle.GroupBoxes[i];
                sp.Selected += Sp_Selected;
                contentView1.FPanel.Controls.Add(sp);
            }
            
            for (int i = 0; i < _headerStyle.GroupBoxes.Length; i++)
            {
                SelectablePanel sp = new SelectablePanel();
                sp.GroupBox = _headerStyle.GroupBoxes[i];
                sp.Selected += Sp_Selected;
                contentView1.FPanel.Controls.Add(sp);
            }
        }

        private SelectablePanel _prev = null;
        private void Sp_Selected(object sender, SelectedEventArgs e)
        {
            if(_prev != null)
            {
                _prev.HaveFocus = false;
            }
            contentView1.Grid.SelectedObject = e.GroupBox;
            _prev = (SelectablePanel)sender;
            _prev.HaveFocus = true;
        }
    }
}
