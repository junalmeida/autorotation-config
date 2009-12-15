using System;

using System.Collections.Generic;
using System.Text;
using Fluid.Controls;
using System.Drawing;

namespace AutoRotationConfig
{
    public class WelcomeScreen : FluidPanel
    {
        protected override void InitControl()
        {
            base.InitControl();

            Bounds = new Rectangle(0, 0, 240, 300);

            BackColor = Color.Black;

            header = new Header();
            header.Text = "Welcome";
            Controls.Add(header);

            string infoText = "This is the first time you started AutoRotation Config for {0} devices. \r\n\r\n" +
                "Thank you for using my application. If you find any bugs, please, provide me a feedback. \r\n" +
                "Tap 'Start' to proceed.";

#if SAMSUNG
            infoText = string.Format(infoText, "Samsung");
#else
            infoText = string.Format(infoText, "HTC");
#endif

            FluidLabel infoLabel = new FluidLabel(infoText, 3, 40, Bounds.Width - 6, 220);
            infoLabel.ForeColor = Color.White;
            infoLabel.Font = new Font(FontFamily.GenericSansSerif, 8f, FontStyle.Regular);
            infoLabel.Alignment = StringAlignment.Near;
            infoLabel.LineAlignment = StringAlignment.Near;
            infoLabel.Format.FormatFlags = 0;
            infoLabel.Anchor = AnchorTLR;
            Controls.Add(infoLabel);

            btnStart = new FluidButton("Start", 70, Bounds.Height - 70, 100, 32);
            btnStart.BackColor = Color.FromArgb(131, 135, 136);
            btnStart.Click += new EventHandler(btnStart_Click);

            btnStart.ForeColor = Color.White;
            btnStart.ShadowColor = Color.Wheat;
            btnStart.Anchor = AnchorBLR;
            Controls.Add(btnStart);
        }

        void btnStart_Click(object sender, EventArgs e)
        {
            OnContinue(e);
        }

        Header header;
        FluidButton btnStart;

        public event EventHandler Continue;
        protected void OnContinue(EventArgs e)
        {
            if (Continue != null)
                Continue(this, e);
        }

    }
}
