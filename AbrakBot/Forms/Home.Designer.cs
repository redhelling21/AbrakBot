﻿namespace AbrakBot.Forms
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pdvBar = new System.Windows.Forms.ToolStripProgressBar();
            this.pdvLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.xpBar = new System.Windows.Forms.ToolStripProgressBar();
            this.xpLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.podsBar = new System.Windows.Forms.ToolStripProgressBar();
            this.podsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.enerBar = new System.Windows.Forms.ToolStripProgressBar();
            this.enerLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.charNameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lvlLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainTabPanel = new System.Windows.Forms.TabControl();
            this.mainTab = new System.Windows.Forms.TabPage();
            this.mainBox = new System.Windows.Forms.RichTextBox();
            this.invtab = new System.Windows.Forms.TabPage();
            this.invTable = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.debugTab = new System.Windows.Forms.TabPage();
            this.debugBox = new System.Windows.Forms.RichTextBox();
            this.tabImages = new System.Windows.Forms.ImageList(this.components);
            this.kamasLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.connectButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.mainTabPanel.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.invtab.SuspendLayout();
            this.invTable.SuspendLayout();
            this.debugTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(973, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel5,
            this.kamasLabel,
            this.toolStripStatusLabel1,
            this.pdvBar,
            this.pdvLabel,
            this.toolStripStatusLabel2,
            this.lvlLabel,
            this.xpBar,
            this.xpLabel,
            this.toolStripStatusLabel3,
            this.podsBar,
            this.podsLabel,
            this.toolStripStatusLabel4,
            this.enerBar,
            this.enerLabel,
            this.charNameLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 404);
            this.statusStrip1.MaximumSize = new System.Drawing.Size(0, 25);
            this.statusStrip1.MinimumSize = new System.Drawing.Size(0, 25);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(973, 25);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // pdvBar
            // 
            this.pdvBar.Name = "pdvBar";
            this.pdvBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pdvBar.Size = new System.Drawing.Size(100, 19);
            this.pdvBar.Step = 1;
            this.pdvBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // pdvLabel
            // 
            this.pdvLabel.Name = "pdvLabel";
            this.pdvLabel.Size = new System.Drawing.Size(23, 20);
            this.pdvLabel.Text = "0%";
            // 
            // xpBar
            // 
            this.xpBar.Name = "xpBar";
            this.xpBar.Size = new System.Drawing.Size(100, 19);
            this.xpBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // xpLabel
            // 
            this.xpLabel.Name = "xpLabel";
            this.xpLabel.Size = new System.Drawing.Size(23, 20);
            this.xpLabel.Text = "0%";
            // 
            // podsBar
            // 
            this.podsBar.Name = "podsBar";
            this.podsBar.Size = new System.Drawing.Size(100, 19);
            this.podsBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // podsLabel
            // 
            this.podsLabel.Name = "podsLabel";
            this.podsLabel.Size = new System.Drawing.Size(23, 20);
            this.podsLabel.Text = "0%";
            // 
            // enerBar
            // 
            this.enerBar.Name = "enerBar";
            this.enerBar.Size = new System.Drawing.Size(100, 19);
            // 
            // enerLabel
            // 
            this.enerLabel.Name = "enerLabel";
            this.enerLabel.Size = new System.Drawing.Size(23, 20);
            this.enerLabel.Text = "0%";
            // 
            // charNameLabel
            // 
            this.charNameLabel.Margin = new System.Windows.Forms.Padding(50, 3, 0, 2);
            this.charNameLabel.Name = "charNameLabel";
            this.charNameLabel.Size = new System.Drawing.Size(0, 20);
            // 
            // lvlLabel
            // 
            this.lvlLabel.Name = "lvlLabel";
            this.lvlLabel.Size = new System.Drawing.Size(27, 20);
            this.lvlLabel.Text = "Lv.0";
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
            this.mainTabPanel.Size = new System.Drawing.Size(949, 361);
            this.mainTabPanel.TabIndex = 2;
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.mainBox);
            this.mainTab.ImageIndex = 0;
            this.mainTab.Location = new System.Drawing.Point(4, 23);
            this.mainTab.Name = "mainTab";
            this.mainTab.Padding = new System.Windows.Forms.Padding(3);
            this.mainTab.Size = new System.Drawing.Size(941, 334);
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
            this.mainBox.Size = new System.Drawing.Size(932, 325);
            this.mainBox.TabIndex = 0;
            this.mainBox.Text = "";
            // 
            // invtab
            // 
            this.invtab.Controls.Add(this.invTable);
            this.invtab.ImageIndex = 1;
            this.invtab.Location = new System.Drawing.Point(4, 23);
            this.invtab.Name = "invtab";
            this.invtab.Padding = new System.Windows.Forms.Padding(3);
            this.invtab.Size = new System.Drawing.Size(941, 334);
            this.invtab.TabIndex = 1;
            this.invtab.Text = "Inventaire";
            this.invtab.UseVisualStyleBackColor = true;
            // 
            // invTable
            // 
            this.invTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.invTable.AutoScroll = true;
            this.invTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.invTable.ColumnCount = 3;
            this.invTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.invTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.invTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.invTable.Controls.Add(this.label3, 2, 0);
            this.invTable.Controls.Add(this.label1, 0, 0);
            this.invTable.Controls.Add(this.label2, 1, 0);
            this.invTable.Location = new System.Drawing.Point(25, 26);
            this.invTable.Name = "invTable";
            this.invTable.RowCount = 2;
            this.invTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.invTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.invTable.Size = new System.Drawing.Size(890, 305);
            this.invTable.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(802, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "Qte";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "Id";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(93, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nom";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // debugTab
            // 
            this.debugTab.Controls.Add(this.debugBox);
            this.debugTab.ImageIndex = 2;
            this.debugTab.Location = new System.Drawing.Point(4, 23);
            this.debugTab.Name = "debugTab";
            this.debugTab.Padding = new System.Windows.Forms.Padding(3);
            this.debugTab.Size = new System.Drawing.Size(941, 334);
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
            this.debugBox.Size = new System.Drawing.Size(932, 325);
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
            // kamasLabel
            // 
            this.kamasLabel.Name = "kamasLabel";
            this.kamasLabel.Size = new System.Drawing.Size(13, 20);
            this.kamasLabel.Text = "0";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Image = global::AbrakBot.Properties.Resources.coins;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(16, 20);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel1.Image = global::AbrakBot.Properties.Resources.hearts;
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(15, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 20);
            this.toolStripStatusLabel1.Text = "Vie";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel2.Image = global::AbrakBot.Properties.Resources.favorite;
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(15, 3, 0, 2);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(16, 20);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Image = global::AbrakBot.Properties.Resources.balance;
            this.toolStripStatusLabel3.Margin = new System.Windows.Forms.Padding(15, 3, 0, 2);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(16, 20);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Image = global::AbrakBot.Properties.Resources.bolt;
            this.toolStripStatusLabel4.Margin = new System.Windows.Forms.Padding(15, 3, 0, 2);
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(16, 20);
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
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 429);
            this.Controls.Add(this.mainTabPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Home";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Home_FormClosed);
            this.Load += new System.EventHandler(this.Home_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.mainTabPanel.ResumeLayout(false);
            this.mainTab.ResumeLayout(false);
            this.invtab.ResumeLayout(false);
            this.invTable.ResumeLayout(false);
            this.invTable.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel invTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripProgressBar pdvBar;
        public System.Windows.Forms.ToolStripStatusLabel pdvLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        public System.Windows.Forms.ToolStripProgressBar xpBar;
        public System.Windows.Forms.ToolStripStatusLabel xpLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        public System.Windows.Forms.ToolStripProgressBar podsBar;
        public System.Windows.Forms.ToolStripStatusLabel podsLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        public System.Windows.Forms.ToolStripStatusLabel charNameLabel;
        public System.Windows.Forms.ToolStripStatusLabel lvlLabel;
        public System.Windows.Forms.ToolStripStatusLabel enerLabel;
        public System.Windows.Forms.ToolStripProgressBar enerBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        public System.Windows.Forms.ToolStripStatusLabel kamasLabel;
    }
}