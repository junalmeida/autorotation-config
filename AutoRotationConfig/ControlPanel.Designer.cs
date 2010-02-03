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
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabApps = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.appList = new AutoRotationConfig.KListControl();
            this.tabs.SuspendLayout();
            this.tabApps.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuAdd);
            this.mainMenu1.MenuItems.Add(this.mnuRemove);
            // 
            // mnuAdd
            // 
            this.mnuAdd.Text = "&Add Application";
            // 
            // mnuRemove
            // 
            this.mnuRemove.Text = "&Remove";
            this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabApps);
            this.tabs.Controls.Add(this.tabAbout);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 24);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(240, 244);
            this.tabs.TabIndex = 0;
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
            // 
            // tabApps
            // 
            this.tabApps.Controls.Add(this.appList);
            this.tabApps.Controls.Add(this.label1);
            this.tabApps.Location = new System.Drawing.Point(0, 0);
            this.tabApps.Name = "tabApps";
            this.tabApps.Size = new System.Drawing.Size(240, 221);
            this.tabApps.Text = "Applications";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(7, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 34);
            this.label1.Text = "All applications on this list will be automatic rotated using device\'s g-sensor.";
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.pictureBox1);
            this.tabAbout.Controls.Add(this.label3);
            this.tabAbout.Location = new System.Drawing.Point(0, 0);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(240, 221);
            this.tabAbout.Text = "About";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(96, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(41, 38);
            this.pictureBox1.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(2, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(237, 122);
            this.label3.Text = "Auto-Rotate Configuration\r\nfor Pocket PC\r\n\r\nMarcos Almeida Jr. <junalmeida@gmail." +
                "com>\r\n\r\nThis program is provided as freeware and is distributed as-is without wa" +
                "rranty.\t";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 24);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 14);
            this.label2.Text = "Auto-Rotate";
            // 
            // appList
            // 
            this.appList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.appList.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.appList.ItemHeight = 28;
            this.appList.Location = new System.Drawing.Point(0, 0);
            this.appList.Name = "appList";
            this.appList.Size = new System.Drawing.Size(240, 185);
            this.appList.TabIndex = 1;
            this.appList.SelectedItemChanged += new System.EventHandler(this.appList_SelectedItemChanged);
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.panel1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "ControlPanel";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.ControlPanel_Load);
            this.Activated += new System.EventHandler(this.ControlPanel_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ControlPanel_Closing);
            this.tabs.ResumeLayout(false);
            this.tabApps.ResumeLayout(false);
            this.tabAbout.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabApps;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuItem mnuAdd;
        private System.Windows.Forms.MenuItem mnuRemove;
        private System.Windows.Forms.Label label3;
        private KListControl appList;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}