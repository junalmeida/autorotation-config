namespace AutoRotationConfig
{
    partial class ApplicationList
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
            this.appList = new Tenor.Mobile.UI.KListControl();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // appList
            // 
            this.appList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.appList.DefaultItemHeight = 38;
            this.appList.DefaultItemWidth = 80;
            this.appList.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.appList.Layout = Tenor.Mobile.UI.KListLayout.Vertical;
            this.appList.Location = new System.Drawing.Point(0, 0);
            this.appList.Name = "appList";
            this.appList.SeparatorColor = System.Drawing.SystemColors.InactiveBorder;
            this.appList.Size = new System.Drawing.Size(406, 330);
            this.appList.TabIndex = 3;
            this.appList.SelectedItemChanged += new System.EventHandler(this.appList_SelectedItemChanged);
            this.appList.DrawItem += new Tenor.Mobile.UI.DrawItemEventHandler(this.appList_DrawItem);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(0, 333);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(406, 34);
            this.label1.Text = "All applications on this list will be automatic rotated using device\'s g-sensor.";
            // 
            // ApplicationList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.appList);
            this.Name = "ApplicationList";
            this.Size = new System.Drawing.Size(406, 367);
            this.ResumeLayout(false);

        }

        #endregion

        private Tenor.Mobile.UI.KListControl appList;
        private System.Windows.Forms.Label label1;
    }
}
