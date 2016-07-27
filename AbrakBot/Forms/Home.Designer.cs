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
            this.testButton = new System.Windows.Forms.ToolStripButton();
            this.trajetsList = new System.Windows.Forms.ToolStripComboBox();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.remoteControlButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.kamasLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pdvBar = new System.Windows.Forms.ToolStripProgressBar();
            this.pdvLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lvlLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.xpBar = new System.Windows.Forms.ToolStripProgressBar();
            this.xpLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.podsBar = new System.Windows.Forms.ToolStripProgressBar();
            this.podsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.enerBar = new System.Windows.Forms.ToolStripProgressBar();
            this.enerLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.charNameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mapCoordLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainTabPanel = new System.Windows.Forms.TabControl();
            this.mainTab = new System.Windows.Forms.TabPage();
            this.sendMessageBox = new System.Windows.Forms.TextBox();
            this.mainBox = new System.Windows.Forms.RichTextBox();
            this.resourcesTab = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.resourceTable = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.metierBar1 = new System.Windows.Forms.ProgressBar();
            this.metierBar2 = new System.Windows.Forms.ProgressBar();
            this.metierBar3 = new System.Windows.Forms.ProgressBar();
            this.metierLabel1 = new System.Windows.Forms.Label();
            this.metierLabel2 = new System.Windows.Forms.Label();
            this.metierLabel3 = new System.Windows.Forms.Label();
            this.metierLabelLvl1 = new System.Windows.Forms.Label();
            this.metierLabelLvl2 = new System.Windows.Forms.Label();
            this.metierLabelLvl3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.invtab = new System.Windows.Forms.TabPage();
            this.invTable = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.reportTab = new System.Windows.Forms.TabPage();
            this.debugTab = new System.Windows.Forms.TabPage();
            this.debugBox = new System.Windows.Forms.RichTextBox();
            this.tabImages = new System.Windows.Forms.ImageList(this.components);
            this.explTrajets = new System.Windows.Forms.OpenFileDialog();
            this.resourcesCheckBox = new System.Windows.Forms.CheckedListBox();
            this.toolStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.mainTabPanel.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.resourcesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.invtab.SuspendLayout();
            this.invTable.SuspendLayout();
            this.debugTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectButton,
            this.testButton,
            this.trajetsList,
            this.startButton,
            this.remoteControlButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(975, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // connectButton
            // 
            this.connectButton.Image = global::AbrakBot.Properties.Resources.link;
            this.connectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(84, 22);
            this.connectButton.Text = "Connexion";
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // testButton
            // 
            this.testButton.Image = global::AbrakBot.Properties.Resources.flask;
            this.testButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(176, 22);
            this.testButton.Text = "Bouton pour tester des trucs";
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // trajetsList
            // 
            this.trajetsList.Name = "trajetsList";
            this.trajetsList.Size = new System.Drawing.Size(121, 25);
            this.trajetsList.SelectedIndexChanged += new System.EventHandler(this.trajetsList_SelectedIndexChanged);
            // 
            // startButton
            // 
            this.startButton.Image = global::AbrakBot.Properties.Resources.rocket;
            this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(62, 22);
            this.startButton.Text = "Lancer";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // remoteControlButton
            // 
            this.remoteControlButton.Image = global::AbrakBot.Properties.Resources.console;
            this.remoteControlButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.remoteControlButton.Name = "remoteControlButton";
            this.remoteControlButton.Size = new System.Drawing.Size(109, 22);
            this.remoteControlButton.Text = "Télécommande";
            this.remoteControlButton.Click += new System.EventHandler(this.remoteControlButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.charNameLabel,
            this.mapCoordLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 423);
            this.statusStrip.MaximumSize = new System.Drawing.Size(0, 25);
            this.statusStrip.MinimumSize = new System.Drawing.Size(0, 25);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(975, 25);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            this.statusStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Image = global::AbrakBot.Properties.Resources.coins;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(16, 20);
            // 
            // kamasLabel
            // 
            this.kamasLabel.Name = "kamasLabel";
            this.kamasLabel.Size = new System.Drawing.Size(13, 20);
            this.kamasLabel.Text = "0";
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
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel2.Image = global::AbrakBot.Properties.Resources.favorite;
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(15, 3, 0, 2);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(16, 20);
            // 
            // lvlLabel
            // 
            this.lvlLabel.Name = "lvlLabel";
            this.lvlLabel.Size = new System.Drawing.Size(27, 20);
            this.lvlLabel.Text = "Lv.0";
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
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Image = global::AbrakBot.Properties.Resources.balance;
            this.toolStripStatusLabel3.Margin = new System.Windows.Forms.Padding(15, 3, 0, 2);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(16, 20);
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
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Image = global::AbrakBot.Properties.Resources.bolt;
            this.toolStripStatusLabel4.Margin = new System.Windows.Forms.Padding(15, 3, 0, 2);
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(16, 20);
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
            // mapCoordLabel
            // 
            this.mapCoordLabel.Name = "mapCoordLabel";
            this.mapCoordLabel.Size = new System.Drawing.Size(33, 20);
            this.mapCoordLabel.Text = "[0, 0]";
            // 
            // mainTabPanel
            // 
            this.mainTabPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabPanel.Controls.Add(this.mainTab);
            this.mainTabPanel.Controls.Add(this.resourcesTab);
            this.mainTabPanel.Controls.Add(this.invtab);
            this.mainTabPanel.Controls.Add(this.reportTab);
            this.mainTabPanel.Controls.Add(this.debugTab);
            this.mainTabPanel.ImageList = this.tabImages;
            this.mainTabPanel.Location = new System.Drawing.Point(12, 28);
            this.mainTabPanel.Multiline = true;
            this.mainTabPanel.Name = "mainTabPanel";
            this.mainTabPanel.SelectedIndex = 0;
            this.mainTabPanel.Size = new System.Drawing.Size(951, 380);
            this.mainTabPanel.TabIndex = 2;
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.sendMessageBox);
            this.mainTab.Controls.Add(this.mainBox);
            this.mainTab.ImageIndex = 0;
            this.mainTab.Location = new System.Drawing.Point(4, 23);
            this.mainTab.Name = "mainTab";
            this.mainTab.Padding = new System.Windows.Forms.Padding(3);
            this.mainTab.Size = new System.Drawing.Size(943, 353);
            this.mainTab.TabIndex = 0;
            this.mainTab.Text = "Principal";
            this.mainTab.UseVisualStyleBackColor = true;
            // 
            // sendMessageBox
            // 
            this.sendMessageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendMessageBox.Location = new System.Drawing.Point(3, 327);
            this.sendMessageBox.Name = "sendMessageBox";
            this.sendMessageBox.Size = new System.Drawing.Size(934, 20);
            this.sendMessageBox.TabIndex = 2;
            this.sendMessageBox.Text = "Envoyer un message...";
            this.sendMessageBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sendMessageBox_KeyPress);
            // 
            // mainBox
            // 
            this.mainBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainBox.Location = new System.Drawing.Point(3, 3);
            this.mainBox.Name = "mainBox";
            this.mainBox.Size = new System.Drawing.Size(934, 323);
            this.mainBox.TabIndex = 0;
            this.mainBox.Text = "";
            // 
            // resourcesTab
            // 
            this.resourcesTab.Controls.Add(this.splitContainer1);
            this.resourcesTab.ImageIndex = 4;
            this.resourcesTab.Location = new System.Drawing.Point(4, 23);
            this.resourcesTab.Name = "resourcesTab";
            this.resourcesTab.Padding = new System.Windows.Forms.Padding(3);
            this.resourcesTab.Size = new System.Drawing.Size(943, 353);
            this.resourcesTab.TabIndex = 3;
            this.resourcesTab.Text = "Ressources";
            this.resourcesTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(0, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.resourceTable);
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.resourcesCheckBox);
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Size = new System.Drawing.Size(943, 350);
            this.splitContainer1.SplitterDistance = 530;
            this.splitContainer1.TabIndex = 2;
            // 
            // resourceTable
            // 
            this.resourceTable.BackColor = System.Drawing.Color.Transparent;
            this.resourceTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.resourceTable.ColumnCount = 4;
            this.resourceTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.resourceTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.resourceTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.resourceTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.resourceTable.Location = new System.Drawing.Point(6, 41);
            this.resourceTable.MaximumSize = new System.Drawing.Size(800, 390);
            this.resourceTable.MinimumSize = new System.Drawing.Size(381, 0);
            this.resourceTable.Name = "resourceTable";
            this.resourceTable.RowCount = 1;
            this.resourceTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.resourceTable.Size = new System.Drawing.Size(434, 35);
            this.resourceTable.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.DimGray;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(434, 32);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(306, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 32);
            this.label7.TabIndex = 4;
            this.label7.Text = "Cell";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(371, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 32);
            this.label6.TabIndex = 3;
            this.label6.Text = "Etat";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 32);
            this.label5.TabIndex = 2;
            this.label5.Text = "Id";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(68, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 32);
            this.label4.TabIndex = 1;
            this.label4.Text = "Nom";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.Controls.Add(this.metierBar1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.metierBar2, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.metierBar3, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.metierLabel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.metierLabel2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.metierLabel3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.metierLabelLvl1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.metierLabelLvl2, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.metierLabelLvl3, 2, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(33, 41);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(329, 100);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // metierBar1
            // 
            this.metierBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metierBar1.Location = new System.Drawing.Point(68, 3);
            this.metierBar1.Name = "metierBar1";
            this.metierBar1.Size = new System.Drawing.Size(207, 27);
            this.metierBar1.TabIndex = 0;
            // 
            // metierBar2
            // 
            this.metierBar2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metierBar2.Location = new System.Drawing.Point(68, 36);
            this.metierBar2.Name = "metierBar2";
            this.metierBar2.Size = new System.Drawing.Size(207, 27);
            this.metierBar2.TabIndex = 1;
            // 
            // metierBar3
            // 
            this.metierBar3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metierBar3.Location = new System.Drawing.Point(68, 69);
            this.metierBar3.Name = "metierBar3";
            this.metierBar3.Size = new System.Drawing.Size(207, 28);
            this.metierBar3.TabIndex = 2;
            // 
            // metierLabel1
            // 
            this.metierLabel1.AutoSize = true;
            this.metierLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metierLabel1.Location = new System.Drawing.Point(3, 0);
            this.metierLabel1.Name = "metierLabel1";
            this.metierLabel1.Size = new System.Drawing.Size(59, 33);
            this.metierLabel1.TabIndex = 3;
            this.metierLabel1.Text = "?";
            this.metierLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metierLabel2
            // 
            this.metierLabel2.AutoSize = true;
            this.metierLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metierLabel2.Location = new System.Drawing.Point(3, 33);
            this.metierLabel2.Name = "metierLabel2";
            this.metierLabel2.Size = new System.Drawing.Size(59, 33);
            this.metierLabel2.TabIndex = 4;
            this.metierLabel2.Text = "?";
            this.metierLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metierLabel3
            // 
            this.metierLabel3.AutoSize = true;
            this.metierLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metierLabel3.Location = new System.Drawing.Point(3, 66);
            this.metierLabel3.Name = "metierLabel3";
            this.metierLabel3.Size = new System.Drawing.Size(59, 34);
            this.metierLabel3.TabIndex = 5;
            this.metierLabel3.Text = "?";
            this.metierLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metierLabelLvl1
            // 
            this.metierLabelLvl1.AutoSize = true;
            this.metierLabelLvl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metierLabelLvl1.Location = new System.Drawing.Point(281, 0);
            this.metierLabelLvl1.Name = "metierLabelLvl1";
            this.metierLabelLvl1.Size = new System.Drawing.Size(45, 33);
            this.metierLabelLvl1.TabIndex = 6;
            this.metierLabelLvl1.Text = "Lvl. 0";
            this.metierLabelLvl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metierLabelLvl2
            // 
            this.metierLabelLvl2.AutoSize = true;
            this.metierLabelLvl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metierLabelLvl2.Location = new System.Drawing.Point(281, 33);
            this.metierLabelLvl2.Name = "metierLabelLvl2";
            this.metierLabelLvl2.Size = new System.Drawing.Size(45, 33);
            this.metierLabelLvl2.TabIndex = 7;
            this.metierLabelLvl2.Text = "Lvl. 0";
            this.metierLabelLvl2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metierLabelLvl3
            // 
            this.metierLabelLvl3.AutoSize = true;
            this.metierLabelLvl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metierLabelLvl3.Location = new System.Drawing.Point(281, 66);
            this.metierLabelLvl3.Name = "metierLabelLvl3";
            this.metierLabelLvl3.Size = new System.Drawing.Size(45, 34);
            this.metierLabelLvl3.TabIndex = 8;
            this.metierLabelLvl3.Text = "Lvl. 0";
            this.metierLabelLvl3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Gray;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(12, 167);
            this.label9.MinimumSize = new System.Drawing.Size(370, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(370, 20);
            this.label9.TabIndex = 1;
            this.label9.Text = "Récolter";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Gray;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(12, 9);
            this.label8.MinimumSize = new System.Drawing.Size(370, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(370, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Métiers";
            // 
            // invtab
            // 
            this.invtab.Controls.Add(this.invTable);
            this.invtab.ImageIndex = 1;
            this.invtab.Location = new System.Drawing.Point(4, 23);
            this.invtab.Name = "invtab";
            this.invtab.Padding = new System.Windows.Forms.Padding(3);
            this.invtab.Size = new System.Drawing.Size(943, 353);
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
            this.invTable.MaximumSize = new System.Drawing.Size(890, 390);
            this.invTable.MinimumSize = new System.Drawing.Size(890, 65);
            this.invTable.Name = "invTable";
            this.invTable.RowCount = 2;
            this.invTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.invTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.invTable.Size = new System.Drawing.Size(890, 65);
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
            // reportTab
            // 
            this.reportTab.ImageIndex = 3;
            this.reportTab.Location = new System.Drawing.Point(4, 23);
            this.reportTab.Name = "reportTab";
            this.reportTab.Size = new System.Drawing.Size(943, 353);
            this.reportTab.TabIndex = 0;
            this.reportTab.Text = "Rapport";
            this.reportTab.UseVisualStyleBackColor = true;
            // 
            // debugTab
            // 
            this.debugTab.Controls.Add(this.debugBox);
            this.debugTab.ImageIndex = 2;
            this.debugTab.Location = new System.Drawing.Point(4, 23);
            this.debugTab.Name = "debugTab";
            this.debugTab.Padding = new System.Windows.Forms.Padding(3);
            this.debugTab.Size = new System.Drawing.Size(943, 353);
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
            this.tabImages.Images.SetKeyName(3, "graph.png");
            this.tabImages.Images.SetKeyName(4, "wheat (1).png");
            // 
            // explTrajets
            // 
            this.explTrajets.FileName = "openFileDialog1";
            this.explTrajets.InitialDirectory = "Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)";
            // 
            // resourcesCheckBox
            // 
            this.resourcesCheckBox.FormattingEnabled = true;
            this.resourcesCheckBox.Location = new System.Drawing.Point(16, 192);
            this.resourcesCheckBox.Name = "resourcesCheckBox";
            this.resourcesCheckBox.Size = new System.Drawing.Size(343, 154);
            this.resourcesCheckBox.TabIndex = 3;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 448);
            this.Controls.Add(this.mainTabPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Home";
            this.Text = "AbrakBot v0.1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Home_FormClosed);
            this.Load += new System.EventHandler(this.Home_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.mainTabPanel.ResumeLayout(false);
            this.mainTab.ResumeLayout(false);
            this.mainTab.PerformLayout();
            this.resourcesTab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.invtab.ResumeLayout(false);
            this.invTable.ResumeLayout(false);
            this.invTable.PerformLayout();
            this.debugTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
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
        public System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TextBox sendMessageBox;
        public System.Windows.Forms.ToolStripStatusLabel mapCoordLabel;
        private System.Windows.Forms.ToolStripButton testButton;
        private System.Windows.Forms.TabPage reportTab;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.OpenFileDialog explTrajets;
        public System.Windows.Forms.ToolStripComboBox trajetsList;
        private System.Windows.Forms.ToolStripButton remoteControlButton;
        public System.Windows.Forms.ToolStripButton connectButton;
        private System.Windows.Forms.TabPage resourcesTab;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.TableLayoutPanel resourceTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.CheckedListBox resourcesCheckBox;
        public System.Windows.Forms.ProgressBar metierBar1;
        public System.Windows.Forms.ProgressBar metierBar2;
        public System.Windows.Forms.ProgressBar metierBar3;
        public System.Windows.Forms.Label metierLabel1;
        public System.Windows.Forms.Label metierLabel2;
        public System.Windows.Forms.Label metierLabel3;
        public System.Windows.Forms.Label metierLabelLvl1;
        public System.Windows.Forms.Label metierLabelLvl2;
        public System.Windows.Forms.Label metierLabelLvl3;
    }
}