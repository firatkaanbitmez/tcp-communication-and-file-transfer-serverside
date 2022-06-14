using System;

namespace TCP_SERVER
{
    partial class frmServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServer));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.listView1 = new MaterialSkin.Controls.MaterialListView();
            this.ClientIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Computer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ConnectionID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClientName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PingTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtboxComunication = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.startServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverConfigurationPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.porToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialSwitch1 = new MaterialSkin.Controls.MaterialSwitch();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label1 = new MaterialSkin.Controls.MaterialLabel();
            this.labelstatusinfo = new MaterialSkin.Controls.MaterialLabel();
            this.label2 = new MaterialSkin.Controls.MaterialLabel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.searchbox = new MaterialSkin.Controls.MaterialMaskedTextBox();
            this.serverManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(24, 92);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 83;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.BackColor = System.Drawing.Color.White;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(6, 332);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(175, 19);
            this.materialLabel1.TabIndex = 87;
            this.materialLabel1.Text = "SERVER EVENT SCREEN";
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listView1.AutoSizeTable = false;
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ClientIP,
            this.Computer,
            this.Version,
            this.ConnectionID,
            this.ClientName,
            this.PingTime});
            this.listView1.Depth = 0;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 126);
            this.listView1.MinimumSize = new System.Drawing.Size(200, 100);
            this.listView1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.listView1.MouseState = MaterialSkin.MouseState.OUT;
            this.listView1.Name = "listView1";
            this.listView1.OwnerDraw = true;
            this.listView1.Size = new System.Drawing.Size(936, 203);
            this.listView1.TabIndex = 92;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // ClientIP
            // 
            this.ClientIP.Text = "Client IP";
            this.ClientIP.Width = 160;
            // 
            // Computer
            // 
            this.Computer.Text = "Computer";
            this.Computer.Width = 160;
            // 
            // Version
            // 
            this.Version.Text = "ID";
            this.Version.Width = 150;
            // 
            // ConnectionID
            // 
            this.ConnectionID.Text = "Name Surname";
            this.ConnectionID.Width = 200;
            // 
            // ClientName
            // 
            this.ClientName.Text = "Client Number";
            this.ClientName.Width = 125;
            // 
            // PingTime
            // 
            this.PingTime.Text = "PingTime";
            this.PingTime.Width = 140;
            // 
            // txtboxComunication
            // 
            this.txtboxComunication.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtboxComunication.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtboxComunication.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtboxComunication.Depth = 0;
            this.txtboxComunication.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtboxComunication.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtboxComunication.HideSelection = false;
            this.txtboxComunication.Location = new System.Drawing.Point(3, 354);
            this.txtboxComunication.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtboxComunication.Name = "txtboxComunication";
            this.txtboxComunication.ReadOnly = true;
            this.txtboxComunication.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtboxComunication.ShowSelectionMargin = true;
            this.txtboxComunication.Size = new System.Drawing.Size(936, 228);
            this.txtboxComunication.TabIndex = 93;
            this.txtboxComunication.TabStop = false;
            this.txtboxComunication.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServerToolStripMenuItem,
            this.stopServerToolStripMenuItem,
            this.serverManagementToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(3, 64);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(937, 25);
            this.menuStrip1.TabIndex = 95;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // startServerToolStripMenuItem
            // 
            this.startServerToolStripMenuItem.Name = "startServerToolStripMenuItem";
            this.startServerToolStripMenuItem.Size = new System.Drawing.Size(88, 21);
            this.startServerToolStripMenuItem.Text = "Start Server";
            this.startServerToolStripMenuItem.Click += new System.EventHandler(this.StartServerToolStripMenuItem_Click);
            // 
            // stopServerToolStripMenuItem
            // 
            this.stopServerToolStripMenuItem.Name = "stopServerToolStripMenuItem";
            this.stopServerToolStripMenuItem.Size = new System.Drawing.Size(88, 21);
            this.stopServerToolStripMenuItem.Text = "Stop Server";
            this.stopServerToolStripMenuItem.Click += new System.EventHandler(this.StopServerToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverConfigurationPanelToolStripMenuItem,
            this.exportListToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(66, 21);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // serverConfigurationPanelToolStripMenuItem
            // 
            this.serverConfigurationPanelToolStripMenuItem.Name = "serverConfigurationPanelToolStripMenuItem";
            this.serverConfigurationPanelToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.serverConfigurationPanelToolStripMenuItem.Text = "Server Configuration Panel";
            this.serverConfigurationPanelToolStripMenuItem.Click += new System.EventHandler(this.ServerConfigurationPanelToolStripMenuItem_Click);
            // 
            // exportListToolStripMenuItem
            // 
            this.exportListToolStripMenuItem.Name = "exportListToolStripMenuItem";
            this.exportListToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.exportListToolStripMenuItem.Text = "Export List";
            this.exportListToolStripMenuItem.Click += new System.EventHandler(this.ExportListToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userManualToolStripMenuItem,
            this.porToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // userManualToolStripMenuItem
            // 
            this.userManualToolStripMenuItem.Name = "userManualToolStripMenuItem";
            this.userManualToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.userManualToolStripMenuItem.Text = "User Manual";
            this.userManualToolStripMenuItem.Click += new System.EventHandler(this.UserManualToolStripMenuItem_Click);
            // 
            // porToolStripMenuItem
            // 
            this.porToolStripMenuItem.Name = "porToolStripMenuItem";
            this.porToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.porToolStripMenuItem.Text = "Port Problem Solver";
            this.porToolStripMenuItem.Click += new System.EventHandler(this.porToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.UpdateToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // materialSwitch1
            // 
            this.materialSwitch1.Depth = 0;
            this.materialSwitch1.Location = new System.Drawing.Point(888, 64);
            this.materialSwitch1.Margin = new System.Windows.Forms.Padding(0);
            this.materialSwitch1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialSwitch1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSwitch1.Name = "materialSwitch1";
            this.materialSwitch1.Ripple = true;
            this.materialSwitch1.Size = new System.Drawing.Size(51, 27);
            this.materialSwitch1.TabIndex = 96;
            this.materialSwitch1.UseVisualStyleBackColor = true;
            this.materialSwitch1.CheckedChanged += new System.EventHandler(this.MaterialSwitch1_CheckedChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::TCP_SERVER.Properties.Resources.ip_address__1_;
            this.pictureBox3.Location = new System.Drawing.Point(326, 92);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 28);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 99;
            this.pictureBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Depth = 0;
            this.label1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label1.Location = new System.Drawing.Point(364, 95);
            this.label1.MouseState = MaterialSkin.MouseState.HOVER;
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 19);
            this.label1.TabIndex = 100;
            this.label1.Text = "...";
            // 
            // labelstatusinfo
            // 
            this.labelstatusinfo.AutoSize = true;
            this.labelstatusinfo.Depth = 0;
            this.labelstatusinfo.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelstatusinfo.Location = new System.Drawing.Point(74, 95);
            this.labelstatusinfo.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelstatusinfo.Name = "labelstatusinfo";
            this.labelstatusinfo.Size = new System.Drawing.Size(177, 19);
            this.labelstatusinfo.TabIndex = 101;
            this.labelstatusinfo.Text = "Click \'Start Server\' button";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Depth = 0;
            this.label2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label2.Location = new System.Drawing.Point(570, 95);
            this.label2.MouseState = MaterialSkin.MouseState.HOVER;
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 19);
            this.label2.TabIndex = 102;
            this.label2.Text = "...";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::TCP_SERVER.Properties.Resources.ethernet;
            this.pictureBox4.Location = new System.Drawing.Point(528, 90);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(36, 30);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 99;
            this.pictureBox4.TabStop = false;
            // 
            // searchbox
            // 
            this.searchbox.AllowPromptAsInput = true;
            this.searchbox.AnimateReadOnly = false;
            this.searchbox.AsciiOnly = false;
            this.searchbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.searchbox.BeepOnError = false;
            this.searchbox.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.searchbox.Depth = 0;
            this.searchbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.searchbox.HidePromptOnLeave = false;
            this.searchbox.HideSelection = true;
            this.searchbox.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.searchbox.LeadingIcon = null;
            this.searchbox.Location = new System.Drawing.Point(784, 90);
            this.searchbox.Mask = "";
            this.searchbox.MaxLength = 32767;
            this.searchbox.MouseState = MaterialSkin.MouseState.OUT;
            this.searchbox.Name = "searchbox";
            this.searchbox.PasswordChar = '\0';
            this.searchbox.PrefixSuffixText = null;
            this.searchbox.PromptChar = '_';
            this.searchbox.ReadOnly = false;
            this.searchbox.RejectInputOnFirstFailure = false;
            this.searchbox.ResetOnPrompt = true;
            this.searchbox.ResetOnSpace = true;
            this.searchbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.searchbox.SelectedText = "";
            this.searchbox.SelectionLength = 0;
            this.searchbox.SelectionStart = 0;
            this.searchbox.ShortcutsEnabled = true;
            this.searchbox.Size = new System.Drawing.Size(156, 36);
            this.searchbox.SkipLiterals = true;
            this.searchbox.TabIndex = 103;
            this.searchbox.TabStop = false;
            this.searchbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.searchbox.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.searchbox.TrailingIcon = global::TCP_SERVER.Properties.Resources.search__1_;
            this.searchbox.UseSystemPasswordChar = false;
            this.searchbox.UseTallSize = false;
            this.searchbox.ValidatingType = null;
            // 
            // serverManagementToolStripMenuItem
            // 
            this.serverManagementToolStripMenuItem.Name = "serverManagementToolStripMenuItem";
            this.serverManagementToolStripMenuItem.Size = new System.Drawing.Size(138, 21);
            this.serverManagementToolStripMenuItem.Text = "Server Management";
            this.serverManagementToolStripMenuItem.Click += new System.EventHandler(this.serverManagementToolStripMenuItem_Click);
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(943, 588);
            this.Controls.Add(this.searchbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelstatusinfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.materialSwitch1);
            this.Controls.Add(this.txtboxComunication);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.DrawerAutoShow = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SERVER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmServer_FormClosing);
            this.Load += new System.EventHandler(this.frmServer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private System.Windows.Forms.ColumnHeader ClientIP;
        private System.Windows.Forms.ColumnHeader Computer;
        private System.Windows.Forms.ColumnHeader Version;
        private System.Windows.Forms.ColumnHeader ConnectionID;
        private System.Windows.Forms.ColumnHeader ClientName;
        private System.Windows.Forms.ColumnHeader PingTime;
        private MaterialSkin.Controls.MaterialMultiLineTextBox txtboxComunication;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem startServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private MaterialSkin.Controls.MaterialSwitch materialSwitch1;
        private System.Windows.Forms.ToolStripMenuItem serverConfigurationPanelToolStripMenuItem;
        public MaterialSkin.Controls.MaterialListView listView1;
        private System.Windows.Forms.ToolStripMenuItem porToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox3;
        private MaterialSkin.Controls.MaterialLabel label1;
        private MaterialSkin.Controls.MaterialLabel labelstatusinfo;
        private MaterialSkin.Controls.MaterialLabel label2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private MaterialSkin.Controls.MaterialMaskedTextBox searchbox;
        private System.Windows.Forms.ToolStripMenuItem serverManagementToolStripMenuItem;
    }
}

