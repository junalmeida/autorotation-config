namespace AutoRotationConfig
{
    partial class Help
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 193);
            this.label3.Text = "\r\nAuto-Rotate Configuration\r\nfor Windows Phone\r\n\r\nRising Mobility\r\n\r\nThis program" +
                " is provided as freeware and is distributed as-is without warranty.\t";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.label3);
            this.Name = "Help";
            this.Size = new System.Drawing.Size(189, 193);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
    }
}
