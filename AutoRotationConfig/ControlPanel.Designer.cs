namespace AutoRotationConfig
{
    partial class ControlPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlPanel));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mnuAdd = new System.Windows.Forms.MenuItem();
            this.mnuRemove = new System.Windows.Forms.MenuItem();
            this.titleStrip = new Tenor.Mobile.UI.HeaderStrip();
            this.ucApplications = new AutoRotationConfig.ApplicationList();
            this.ucHelp = new AutoRotationConfig.Help();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuAdd);
            this.mainMenu1.MenuItems.Add(this.mnuRemove);
            // 
            // mnuAdd
            // 
            this.mnuAdd.Text = "&Add";
            // 
            // mnuRemove
            // 
            this.mnuRemove.Text = "&Remove";
            // 
            // titleStrip
            // 
            this.titleStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleStrip.Location = new System.Drawing.Point(0, 0);
            this.titleStrip.Name = "titleStrip";
            this.titleStrip.Size = new System.Drawing.Size(240, 49);
            this.titleStrip.TabIndex = 3;
            this.titleStrip.Text = "headerStrip1";
            this.titleStrip.SelectedTabChanged += new System.EventHandler(this.titleStrip_SelectedTabChanged);
            // 
            // ucApplications
            // 
            this.ucApplications.Location = new System.Drawing.Point(15, 78);
            this.ucApplications.Name = "ucApplications";
            this.ucApplications.Size = new System.Drawing.Size(150, 150);
            this.ucApplications.TabIndex = 4;
            this.ucApplications.Visible = false;
            // 
            // ucHelp
            // 
            this.ucHelp.Location = new System.Drawing.Point(58, 55);
            this.ucHelp.Name = "ucHelp";
            this.ucHelp.Size = new System.Drawing.Size(150, 150);
            this.ucHelp.TabIndex = 5;
            this.ucHelp.Visible = false;
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.ucHelp);
            this.Controls.Add(this.ucApplications);
            this.Controls.Add(this.titleStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "ControlPanel";
            this.Text = "Auto-Rotate";
            this.Load += new System.EventHandler(this.ControlPanel_Load);
            this.Activated += new System.EventHandler(this.ControlPanel_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ControlPanel_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuAdd;
        private System.Windows.Forms.MenuItem mnuRemove;
        private Tenor.Mobile.UI.HeaderStrip titleStrip;
        private ApplicationList ucApplications;
        private Help ucHelp;
    }
}