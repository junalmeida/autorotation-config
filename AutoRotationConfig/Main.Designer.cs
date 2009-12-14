namespace AutoRotationConfig
{
    partial class Main
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mnuAdd = new System.Windows.Forms.MenuItem();
            this.mnuRemove = new System.Windows.Forms.MenuItem();
            this.listBox = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer();
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
            this.mnuRemove.Enabled = false;
            this.mnuRemove.Text = "&Remove";
            this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
            // 
            // listBox
            // 
            this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox.Location = new System.Drawing.Point(8, 58);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(224, 198);
            this.listBox.TabIndex = 0;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.listBox);
            this.Menu = this.mainMenu1;
            this.Name = "Main";
            this.Text = "Rotation Config";
            this.Activated += new System.EventHandler(this.Main_Activated);
            this.GotFocus += new System.EventHandler(this.Main_GotFocus);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.MenuItem mnuAdd;
        private System.Windows.Forms.MenuItem mnuRemove;
        private System.Windows.Forms.Timer timer1;
    }
}

