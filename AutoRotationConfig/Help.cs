using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AutoRotationConfig
{
    public partial class Help : UserControl
    {
        public Help()
        {
            InitializeComponent();

            Tenor.Mobile.UI.Skin.Current.ApplyColorsToControl(label3);
            label3.ForeColor = Tenor.Mobile.UI.Skin.Current.TextForeColor;

        }

        
    }
}
