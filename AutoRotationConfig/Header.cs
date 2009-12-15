using System;

using System.Collections.Generic;
using System.Text;
using Fluid.Controls;
using System.Drawing;

namespace AutoRotationConfig
{
    public class Header : FluidPanel
    {

        protected override void InitControl()
        {
            base.InitControl();

            Bounds = new Rectangle(0, 0, 240, 35);

            BackColor = Color.FromArgb(34, 38, 41);
            this.GradientFill = true;
            GradientFillOffset = 35;
            ForeColor = Color.FromArgb(89, 103, 114);

            h1 = new FluidLabel(string.Empty, 0, 0, Bounds.Width, Bounds.Height);

            h1.ShadowColor = Color.Black;
            h1.ForeColor = Color.White;
            h1.Alignment = StringAlignment.Center;
            h1.LineAlignment = StringAlignment.Center;
            h1.Font = new Font(FontFamily.GenericSansSerif, 12f, FontStyle.Bold);
            h1.Anchor = AnchorTLR;
            Controls.Add(h1);

        }

        FluidLabel h1;


        public string Text
        {
            get
            {
                return h1.Text;
            }
            set
            {
                h1.Text = value;
            }
        }



    }
}
