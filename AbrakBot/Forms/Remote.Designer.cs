namespace AbrakBot.Forms
{
    partial class Remote
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
            this.upArrowButton = new System.Windows.Forms.Button();
            this.leftArrowButton = new System.Windows.Forms.Button();
            this.downArrowButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.specificCellBox = new System.Windows.Forms.TextBox();
            this.specificCellButton = new System.Windows.Forms.Button();
            this.rightArrowButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // upArrowButton
            // 
            this.upArrowButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.upArrowButton.Image = global::AbrakBot.Properties.Resources.up;
            this.upArrowButton.Location = new System.Drawing.Point(102, 12);
            this.upArrowButton.Name = "upArrowButton";
            this.upArrowButton.Size = new System.Drawing.Size(64, 64);
            this.upArrowButton.TabIndex = 0;
            this.upArrowButton.UseVisualStyleBackColor = true;
            this.upArrowButton.Click += new System.EventHandler(this.upArrowButton_Click);
            // 
            // leftArrowButton
            // 
            this.leftArrowButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.leftArrowButton.Image = global::AbrakBot.Properties.Resources.left;
            this.leftArrowButton.Location = new System.Drawing.Point(41, 73);
            this.leftArrowButton.Name = "leftArrowButton";
            this.leftArrowButton.Size = new System.Drawing.Size(64, 64);
            this.leftArrowButton.TabIndex = 1;
            this.leftArrowButton.UseVisualStyleBackColor = true;
            this.leftArrowButton.Click += new System.EventHandler(this.leftArrowButton_Click);
            // 
            // downArrowButton
            // 
            this.downArrowButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.downArrowButton.Image = global::AbrakBot.Properties.Resources.down;
            this.downArrowButton.Location = new System.Drawing.Point(102, 136);
            this.downArrowButton.Name = "downArrowButton";
            this.downArrowButton.Size = new System.Drawing.Size(64, 64);
            this.downArrowButton.TabIndex = 3;
            this.downArrowButton.UseVisualStyleBackColor = true;
            this.downArrowButton.Click += new System.EventHandler(this.downArrowButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Aller sur une case spécifique :";
            // 
            // specificCellBox
            // 
            this.specificCellBox.Location = new System.Drawing.Point(87, 221);
            this.specificCellBox.Name = "specificCellBox";
            this.specificCellBox.Size = new System.Drawing.Size(100, 20);
            this.specificCellBox.TabIndex = 5;
            // 
            // specificCellButton
            // 
            this.specificCellButton.Location = new System.Drawing.Point(102, 247);
            this.specificCellButton.Name = "specificCellButton";
            this.specificCellButton.Size = new System.Drawing.Size(75, 23);
            this.specificCellButton.TabIndex = 6;
            this.specificCellButton.Text = "Go";
            this.specificCellButton.UseVisualStyleBackColor = true;
            this.specificCellButton.Click += new System.EventHandler(this.specificCellButton_Click);
            // 
            // rightArrowButton
            // 
            this.rightArrowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rightArrowButton.Image = global::AbrakBot.Properties.Resources.right;
            this.rightArrowButton.Location = new System.Drawing.Point(163, 73);
            this.rightArrowButton.Name = "rightArrowButton";
            this.rightArrowButton.Size = new System.Drawing.Size(64, 64);
            this.rightArrowButton.TabIndex = 2;
            this.rightArrowButton.UseVisualStyleBackColor = true;
            this.rightArrowButton.Click += new System.EventHandler(this.rightArrowButton_Click);
            // 
            // Remote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 277);
            this.Controls.Add(this.specificCellButton);
            this.Controls.Add(this.specificCellBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.downArrowButton);
            this.Controls.Add(this.rightArrowButton);
            this.Controls.Add(this.leftArrowButton);
            this.Controls.Add(this.upArrowButton);
            this.Name = "Remote";
            this.Text = "Remote";
            this.Load += new System.EventHandler(this.Remote_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button upArrowButton;
        private System.Windows.Forms.Button leftArrowButton;
        private System.Windows.Forms.Button rightArrowButton;
        private System.Windows.Forms.Button downArrowButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox specificCellBox;
        private System.Windows.Forms.Button specificCellButton;
    }
}