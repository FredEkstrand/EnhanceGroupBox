using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ekstrand.Windows.Forms;

namespace GroupBoxDemo
{
    public class SelectedEventArgs : EventArgs
    {

        private Ekstrand.Windows.Forms.EnhanceGroupBox _groupBox;
        public SelectedEventArgs(Ekstrand.Windows.Forms.EnhanceGroupBox groupBox)
        {
            _groupBox = groupBox;
        }

        public Ekstrand.Windows.Forms.EnhanceGroupBox GroupBox
        {
            get { return _groupBox;}
            set { _groupBox = value;}
        }
    }
}
