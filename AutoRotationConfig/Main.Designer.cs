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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mnuAdd = new System.Windows.Forms.MenuItem();
            this.mnuRemove = new System.Windows.Forms.MenuItem();
            this.fluidHost = new Fluid.Controls.FluidHost();
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
            // fluidHost
            // 
            this.fluidHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fluidHost.Location = new System.Drawing.Point(0, 0);
            this.fluidHost.Name = "fluidHost";
            this.fluidHost.Size = new System.Drawing.Size(240, 268);
            this.fluidHost.TabIndex = 2;
            this.fluidHost.Text = "fluidHost1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.fluidHost);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "Main";
            this.Text = "Rotation Config";
            this.Deactivate += new System.EventHandler(this.Main_Deactivate);
            this.Activated += new System.EventHandler(this.Main_Activated);
            this.GotFocus += new System.EventHandler(this.Main_GotFocus);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Main_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuAdd;
        private System.Windows.Forms.MenuItem mnuRemove;
        private Fluid.Controls.FluidHost fluidHost;
    }
}

