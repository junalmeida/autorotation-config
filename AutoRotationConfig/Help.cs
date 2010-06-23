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

            Tenor.Mobile.UI.Skin.Current.ApplyColorsToControl(lblHelpText);
            lblHelpText.ForeColor = Tenor.Mobile.UI.Skin.Current.TextForeColor;
            lblHelpText.Text = string.Format(lblHelpText.Text, this.GetType().Assembly.GetName().Version);
        }
        
    }
}
