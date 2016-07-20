namespace AbrakBot.Forms
{
    partial class Home
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.connectButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mainTabPanel = new System.Windows.Forms.TabControl();
            this.mainTab = new System.Windows.Forms.TabPage();
            this.mainBox = new System.Windows.Forms.RichTextBox();
            this.invtab = new System.Windows.Forms.TabPage();
            this.debugTab = new System.Windows.Forms.TabPage();
            this.debugBox = new System.Windows.Forms.RichTextBox();
            this.tabImages = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.mainTabPanel.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.debugTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(790, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // connectButton
            // 
            this.connectButton.Image = ((System.Drawing.Image)(resources.GetObject("connectButton.Image")));
            this.connectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(84, 22);
            this.connectButton.Text = "Connexion";
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 388);
            this.statusStrip1.MaximumSize = new System.Drawing.Size(0, 30);
            this.statusStrip1.MinimumSize = new System.Drawing.Size(0, 30);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(790, 30);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mainTabPanel
            // 
            this.mainTabPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabPanel.Controls.Add(this.mainTab);
            this.mainTabPanel.Controls.Add(this.invtab);
            this.mainTabPanel.Controls.Add(this.debugTab);
            this.mainTabPanel.ImageList = this.tabImages;
            this.mainTabPanel.Location = new System.Drawing.Point(12, 28);
            this.mainTabPanel.Multiline = true;
            this.mainTabPanel.Name = "mainTabPanel";
            this.mainTabPanel.SelectedIndex = 0;
            this.mainTabPanel.Size = new System.Drawing.Size(766, 354);
            this.mainTabPanel.TabIndex = 2;
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.mainBox);
            this.mainTab.ImageIndex = 0;
            this.mainTab.Location = new System.Drawing.Point(4, 23);
            this.mainTab.Name = "mainTab";
            this.mainTab.Padding = new System.Windows.Forms.Padding(3);
            this.mainTab.Size = new System.Drawing.Size(758, 327);
            this.mainTab.TabIndex = 0;
            this.mainTab.Text = "Principal";
            this.mainTab.UseVisualStyleBackColor = true;
            // 
            // mainBox
            // 
            this.mainBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainBox.Location = new System.Drawing.Point(3, 3);
            this.mainBox.Name = "mainBox";
            this.mainBox.Size = new System.Drawing.Size(752, 321);
            this.mainBox.TabIndex = 0;
            this.mainBox.Text = "";
            // 
            // invtab
            // 
            this.invtab.ImageIndex = 1;
            this.invtab.Location = new System.Drawing.Point(4, 23);
            this.invtab.Name = "invtab";
            this.invtab.Padding = new System.Windows.Forms.Padding(3);
            this.invtab.Size = new System.Drawing.Size(758, 327);
            this.invtab.TabIndex = 1;
            this.invtab.Text = "Inventaire";
            this.invtab.UseVisualStyleBackColor = true;
            // 
            // debugTab
            // 
            this.debugTab.Controls.Add(this.debugBox);
            this.debugTab.ImageIndex = 2;
            this.debugTab.Location = new System.Drawing.Point(4, 23);
            this.debugTab.Name = "debugTab";
            this.debugTab.Padding = new System.Windows.Forms.Padding(3);
            this.debugTab.Size = new System.Drawing.Size(758, 327);
            this.debugTab.TabIndex = 2;
            this.debugTab.Text = "Debug";
            this.debugTab.UseVisualStyleBackColor = true;
            this.debugTab.UseWaitCursor = true;
            // 
            // debugBox
            // 
            this.debugBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debugBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.debugBox.Location = new System.Drawing.Point(3, 3);
            this.debugBox.Name = "debugBox";
            this.debugBox.Size = new System.Drawing.Size(752, 321);
            this.debugBox.TabIndex = 0;
            this.debugBox.Text = "";
            this.debugBox.UseWaitCursor = true;
            // 
            // tabImages
            // 
            this.tabImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabImages.ImageStream")));
            this.tabImages.TransparentColor = System.Drawing.Color.Transparent;
            this.tabImages.Images.SetKeyName(0, "favicon.ico");
            this.tabImages.Images.SetKeyName(1, "backpack_icon.ico");
            this.tabImages.Images.SetKeyName(2, "debug-bug-icon.png");
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 418);
            this.Controls.Add(this.mainTabPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Home";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Home_FormClosed);
            this.Load += new System.EventHandler(this.Home_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mainTabPanel.ResumeLayout(false);
            this.mainTab.ResumeLayout(false);
            this.debugTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton connectButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabPage mainTab;
        private System.Windows.Forms.TabPage invtab;
        private System.Windows.Forms.TabControl mainTabPanel;
        private System.Windows.Forms.ImageList tabImages;
        private System.Windows.Forms.TabPage debugTab;
        public System.Windows.Forms.RichTextBox mainBox;
        public System.Windows.Forms.RichTextBox debugBox;
    }
}