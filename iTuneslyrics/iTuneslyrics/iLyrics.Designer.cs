namespace iTuneslyrics
{
    partial class iLyrics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(iLyrics));
            this.btnAlbums = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.chkAuto = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbService = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLeosSettings = new System.Windows.Forms.Panel();
            this.txtLeosAuthId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlLeosSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAlbums
            // 
            this.btnAlbums.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlbums.Location = new System.Drawing.Point(250, 3);
            this.btnAlbums.Name = "btnAlbums";
            this.btnAlbums.Size = new System.Drawing.Size(85, 34);
            this.btnAlbums.TabIndex = 1;
            this.btnAlbums.Text = "Get Lyrics";
            this.btnAlbums.UseVisualStyleBackColor = true;
            this.btnAlbums.Click += new System.EventHandler(this.btnAlbums_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkOverwrite);
            this.panel1.Controls.Add(this.chkAuto);
            this.panel1.Controls.Add(this.btnAlbums);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 45);
            this.panel1.TabIndex = 3;
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.Checked = true;
            this.chkOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOverwrite.Location = new System.Drawing.Point(144, 12);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(71, 17);
            this.chkOverwrite.TabIndex = 3;
            this.chkOverwrite.Text = "Overwrite";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // chkAuto
            // 
            this.chkAuto.AutoSize = true;
            this.chkAuto.Checked = true;
            this.chkAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAuto.Location = new System.Drawing.Point(12, 12);
            this.chkAuto.Name = "chkAuto";
            this.chkAuto.Size = new System.Drawing.Size(126, 17);
            this.chkAuto.TabIndex = 2;
            this.chkAuto.Text = "Update Automatically";
            this.chkAuto.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbService);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(348, 37);
            this.panel2.TabIndex = 4;
            // 
            // cbService
            // 
            this.cbService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbService.FormattingEnabled = true;
            this.cbService.Location = new System.Drawing.Point(86, 8);
            this.cbService.Name = "cbService";
            this.cbService.Size = new System.Drawing.Size(249, 21);
            this.cbService.TabIndex = 1;
            this.cbService.SelectedIndexChanged += new System.EventHandler(this.cbService_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lyric Service:";
            // 
            // pnlLeosSettings
            // 
            this.pnlLeosSettings.AutoSize = true;
            this.pnlLeosSettings.Controls.Add(this.txtLeosAuthId);
            this.pnlLeosSettings.Controls.Add(this.label2);
            this.pnlLeosSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeosSettings.Location = new System.Drawing.Point(0, 82);
            this.pnlLeosSettings.Name = "pnlLeosSettings";
            this.pnlLeosSettings.Size = new System.Drawing.Size(348, 35);
            this.pnlLeosSettings.TabIndex = 5;
            this.pnlLeosSettings.Visible = false;
            // 
            // txtLeosAuthId
            // 
            this.txtLeosAuthId.Location = new System.Drawing.Point(86, 6);
            this.txtLeosAuthId.Name = "txtLeosAuthId";
            this.txtLeosAuthId.Size = new System.Drawing.Size(249, 20);
            this.txtLeosAuthId.TabIndex = 1;
            this.txtLeosAuthId.Text = "cricket";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Author ID:";
            // 
            // iLyrics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(348, 117);
            this.Controls.Add(this.pnlLeosSettings);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "iLyrics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iTunes Lyrics Importer";
            this.Load += new System.EventHandler(this.iLyrics_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlLeosSettings.ResumeLayout(false);
            this.pnlLeosSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAlbums;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkAuto;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbService;
        private System.Windows.Forms.Panel pnlLeosSettings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLeosAuthId;

    }
}

