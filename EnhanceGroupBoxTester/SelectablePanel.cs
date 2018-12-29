using System;
using System.Drawing;
using System.Windows.Forms;

// taken from: https://stackoverflow.com/questions/3562235/panel-not-getting-focus
namespace GroupBoxDemo
{
    class SelectablePanel : Panel
    {
        private Ekstrand.Windows.Forms.GroupBox _groupBox;
        private bool _haveFocus = false;

        public SelectablePanel()
        {
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
            this.GotFocus += SelectablePanel_GotFocus;
            this.Size = new Size(225, 100);
           
        }

        public Ekstrand.Windows.Forms.GroupBox GroupBox
        {
            get { return _groupBox; }
            set { _groupBox = value; InitGroupBox();}
        }

        public bool HaveFocus
        {
            get { return _haveFocus; }
            set {  _haveFocus = value; Invalidate();}
        }

        private void InitGroupBox()
        {
            this.Controls.Add(_groupBox);
            this._groupBox.Width = this.Width-5;
            this._groupBox.Height = this.Height-5;
            this._groupBox.Left = (this.Width - this._groupBox.Width) / 2;
            this._groupBox.Top = (this.Height - this._groupBox.Height) / 2;
            this._groupBox.Click+= SelectablePanel_GotFocus;
            
            Invalidate();
        }

        private void SelectablePanel_GotFocus(object sender, EventArgs e)
        {
            Focus();
            HaveFocus = true;
            Invalidate();
            RaiseSelected(this.Controls[0] as Ekstrand.Windows.Forms.GroupBox);
            
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(ClientRectangle.Contains(e.Location))
            {
                Focus();
                HaveFocus = true;
                Invalidate();
            }
            
            base.OnMouseDown(e);
        }
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down) return true;
            if (keyData == Keys.Left || keyData == Keys.Right) return true;
            return base.IsInputKey(keyData);
        }
        protected override void OnEnter(EventArgs e)
        {
            this.Invalidate();
            base.OnEnter(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            this.Invalidate();
            base.OnLeave(e);
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (HaveFocus)
            {
                var rc = this.ClientRectangle;
                rc.Inflate(-1, -1);
                pe.Graphics.DrawRectangle(SystemPens.Highlight,rc);
            }
        }

        public event EventHandler<SelectedEventArgs> Selected;

        private void RaiseSelected(Ekstrand.Windows.Forms.GroupBox groupBox)
        {
            EventHandler<SelectedEventArgs> handler = Selected;
            if(handler != null)
            {
                handler(this,new SelectedEventArgs(groupBox));
            }
        }



        
    }
}
