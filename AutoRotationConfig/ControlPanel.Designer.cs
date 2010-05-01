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
            this.pnlHelp = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlApplications = new System.Windows.Forms.Panel();
            this.appList = new Tenor.Mobile.UI.KListControl();
            this.label1 = new System.Windows.Forms.Label();
            this.titleStrip = new Tenor.Mobile.UI.HeaderStrip();
            this.pnlHelp.SuspendLayout();
            this.pnlApplications.SuspendLayout();
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
            this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
            // 
            // pnlHelp
            // 
            this.pnlHelp.Controls.Add(this.label3);
            this.pnlHelp.Location = new System.Drawing.Point(3, 13);
            this.pnlHelp.Name = "pnlHelp";
            this.pnlHelp.Size = new System.Drawing.Size(218, 226);
            this.pnlHelp.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(3, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 193);
            this.label3.Text = "Auto-Rotate Configuration\r\nfor Samsung Pocket PC's\r\n\r\nMarcos Almeida Jr. <junalmeida@gmail." +
                "com>\r\n\r\nThis program is provided as freeware and is distributed as-is without wa" +
                "rranty.\t";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlApplications
            // 
            this.pnlApplications.Controls.Add(this.appList);
            this.pnlApplications.Controls.Add(this.label1);
            this.pnlApplications.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlApplications.Location = new System.Drawing.Point(0, 38);
            this.pnlApplications.Name = "pnlApplications";
            this.pnlApplications.Size = new System.Drawing.Size(227, 238);
            this.pnlApplications.Visible = false;
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
            this.appList.Size = new System.Drawing.Size(227, 201);
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
            this.label1.Location = new System.Drawing.Point(3, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 34);
            this.label1.Text = "All applications on this list will be automatic rotated using device\'s g-sensor.";
            // 
            // titleStrip
            // 
            this.titleStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleStrip.Location = new System.Drawing.Point(0, 0);
            this.titleStrip.Name = "titleStrip";
            this.titleStrip.Size = new System.Drawing.Size(227, 38);
            this.titleStrip.TabIndex = 3;
            this.titleStrip.Text = "headerStrip1";
            this.titleStrip.SelectedTabChanged += new System.EventHandler(this.titleStrip_SelectedTabChanged);
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pnlHelp);
            this.Controls.Add(this.pnlApplications);
            this.Controls.Add(this.titleStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "ControlPanel";
            this.Text = "Auto-Rotate";
            this.Load += new System.EventHandler(this.ControlPanel_Load);
            this.Activated += new System.EventHandler(this.ControlPanel_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ControlPanel_Closing);
            this.pnlHelp.ResumeLayout(false);
            this.pnlApplications.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuAdd;
        private System.Windows.Forms.MenuItem mnuRemove;
        private System.Windows.Forms.Panel pnlHelp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlApplications;
        private Tenor.Mobile.UI.KListControl appList;
        private System.Windows.Forms.Label label1;
        private Tenor.Mobile.UI.HeaderStrip titleStrip;
    }
}