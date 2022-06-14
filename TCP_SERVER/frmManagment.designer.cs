namespace TCP_SERVER
{
    partial class frmManagment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManagment));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.clientcheckbox = new MaterialSkin.Controls.MaterialCheckbox();
            this.label8 = new MaterialSkin.Controls.MaterialLabel();
            this.labelStatusInfo = new MaterialSkin.Controls.MaterialLabel();
            this.materialDrawer1 = new MaterialSkin.Controls.MaterialDrawer();
            this.panel1 = new MaterialSkin.Controls.MaterialCard();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.buttonSendToClients = new MaterialSkin.Controls.MaterialButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxRcv = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.btnFileSend = new MaterialSkin.Controls.MaterialButton();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(208, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            
            // 
            // textBoxText
            // 
            this.textBoxText.Location = new System.Drawing.Point(181, 131);
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxText.Size = new System.Drawing.Size(622, 166);
            this.textBoxText.TabIndex = 12;
            this.textBoxText.TabStop = false;
            this.textBoxText.Text = resources.GetString("textBoxText.Text");
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(6, 131);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 359);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Clients";
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(3, 18);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(158, 338);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // clientcheckbox
            // 
            this.clientcheckbox.AutoSize = true;
            this.clientcheckbox.Depth = 0;
            this.clientcheckbox.Location = new System.Drawing.Point(9, 91);
            this.clientcheckbox.Margin = new System.Windows.Forms.Padding(0);
            this.clientcheckbox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.clientcheckbox.MouseState = MaterialSkin.MouseState.HOVER;
            this.clientcheckbox.Name = "clientcheckbox";
            this.clientcheckbox.ReadOnly = false;
            this.clientcheckbox.Ripple = true;
            this.clientcheckbox.Size = new System.Drawing.Size(144, 37);
            this.clientcheckbox.TabIndex = 91;
            this.clientcheckbox.Text = "Select All Client";
            this.clientcheckbox.UseVisualStyleBackColor = true;
            this.clientcheckbox.CheckedChanged += new System.EventHandler(this.clientcheckbox_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Depth = 0;
            this.label8.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label8.Location = new System.Drawing.Point(205, 109);
            this.label8.MouseState = MaterialSkin.MouseState.HOVER;
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(467, 19);
            this.label8.TabIndex = 92;
            this.label8.Text = "Please Choose Client/Clients then use Send Message or Send File.";
            // 
            // labelStatusInfo
            // 
            this.labelStatusInfo.AutoSize = true;
            this.labelStatusInfo.Depth = 0;
            this.labelStatusInfo.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelStatusInfo.Location = new System.Drawing.Point(250, 71);
            this.labelStatusInfo.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelStatusInfo.Name = "labelStatusInfo";
            this.labelStatusInfo.Size = new System.Drawing.Size(301, 19);
            this.labelStatusInfo.TabIndex = 93;
            this.labelStatusInfo.Text = "Click the \'Connect\' button if Not Connected";
            // 
            // materialDrawer1
            // 
            this.materialDrawer1.AllowDrop = true;
            this.materialDrawer1.AutoHide = false;
            this.materialDrawer1.AutoShow = false;
            this.materialDrawer1.BackgroundWithAccent = false;
            this.materialDrawer1.BaseTabControl = null;
            this.materialDrawer1.Depth = 0;
            this.materialDrawer1.HighlightWithAccent = true;
            this.materialDrawer1.IndicatorWidth = 0;
            this.materialDrawer1.IsOpen = false;
            this.materialDrawer1.Location = new System.Drawing.Point(-250, 265);
            this.materialDrawer1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDrawer1.Name = "materialDrawer1";
            this.materialDrawer1.ShowIconsWhenHidden = false;
            this.materialDrawer1.Size = new System.Drawing.Size(250, 120);
            this.materialDrawer1.TabIndex = 97;
            this.materialDrawer1.Text = "materialDrawer1";
            this.materialDrawer1.UseColors = true;
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.Depth = 0;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel1.Location = new System.Drawing.Point(559, 529);
            this.panel1.Margin = new System.Windows.Forms.Padding(14);
            this.panel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(14);
            this.panel1.Size = new System.Drawing.Size(244, 95);
            this.panel1.TabIndex = 98;
            this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelFileDropArea_DragDrop);
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelFileDropArea_DragEnter);            
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(652, 503);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(64, 19);
            this.materialLabel1.TabIndex = 99;
            this.materialLabel1.Text = "File Drop";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(205, 309);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(102, 19);
            this.materialLabel2.TabIndex = 99;
            this.materialLabel2.Text = "Incoming Text";
            // 
            // buttonSendToClients
            // 
            this.buttonSendToClients.AutoSize = false;
            this.buttonSendToClients.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSendToClients.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.buttonSendToClients.Depth = 0;
            this.buttonSendToClients.HighEmphasis = true;
            this.buttonSendToClients.Icon = null;
            this.buttonSendToClients.Location = new System.Drawing.Point(431, 303);
            this.buttonSendToClients.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.buttonSendToClients.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonSendToClients.Name = "buttonSendToClients";
            this.buttonSendToClients.NoAccentTextColor = System.Drawing.Color.Empty;
            this.buttonSendToClients.Size = new System.Drawing.Size(120, 25);
            this.buttonSendToClients.TabIndex = 100;
            this.buttonSendToClients.Text = "Send";
            this.buttonSendToClients.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.buttonSendToClients.UseAccentColor = false;
            this.buttonSendToClients.UseVisualStyleBackColor = true;
            this.buttonSendToClients.Click += new System.EventHandler(this.buttonSendToClients_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(9, 505);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(316, 122);
            this.listView1.TabIndex = 101;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Client ID";
            this.columnHeader1.Width = 110;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Send";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Receive";
            this.columnHeader3.Width = 100;
            // 
            // textBoxRcv
            // 
            this.textBoxRcv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBoxRcv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxRcv.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBoxRcv.Depth = 0;
            this.textBoxRcv.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.textBoxRcv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.textBoxRcv.HideSelection = false;
            this.textBoxRcv.Location = new System.Drawing.Point(181, 331);
            this.textBoxRcv.MouseState = MaterialSkin.MouseState.HOVER;
            this.textBoxRcv.Name = "textBoxRcv";
            this.textBoxRcv.ReadOnly = true;
            this.textBoxRcv.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.textBoxRcv.ShowSelectionMargin = true;
            this.textBoxRcv.Size = new System.Drawing.Size(622, 159);
            this.textBoxRcv.TabIndex = 104;
            this.textBoxRcv.Text = "";
            // 
            // btnFileSend
            // 
            this.btnFileSend.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFileSend.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnFileSend.Depth = 0;
            this.btnFileSend.HighEmphasis = true;
            this.btnFileSend.Icon = null;
            this.btnFileSend.Location = new System.Drawing.Point(387, 588);
            this.btnFileSend.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnFileSend.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnFileSend.Name = "btnFileSend";
            this.btnFileSend.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnFileSend.Size = new System.Drawing.Size(91, 36);
            this.btnFileSend.TabIndex = 105;
            this.btnFileSend.Text = "Fıle Send";
            this.btnFileSend.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnFileSend.UseAccentColor = false;
            this.btnFileSend.UseVisualStyleBackColor = true;
            this.btnFileSend.Click += new System.EventHandler(this.btnFileSend_Click);
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(352, 511);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(177, 19);
            this.materialLabel3.TabIndex = 99;
            this.materialLabel3.Text = "Please Select the file you";
            this.materialLabel3.Click += new System.EventHandler(this.materialLabel3_Click);
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel4.Location = new System.Drawing.Point(339, 536);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(190, 19);
            this.materialLabel4.TabIndex = 99;
            this.materialLabel4.Text = " want to send or Drag Drop";
            this.materialLabel4.Click += new System.EventHandler(this.materialLabel4_Click);
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel5.Location = new System.Drawing.Point(390, 563);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(88, 19);
            this.materialLabel5.TabIndex = 99;
            this.materialLabel5.Text = "to \'File Drop\'";
            this.materialLabel5.Click += new System.EventHandler(this.materialLabel5_Click);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(820, 2);
            // 
            // frmManagment
            // 
            this.AcceptButton = this.buttonSendToClients;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(811, 641);
            this.Controls.Add(this.btnFileSend);
            this.Controls.Add(this.textBoxRcv);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.buttonSendToClients);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel5);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.materialDrawer1);
            this.Controls.Add(this.labelStatusInfo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.clientcheckbox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.pictureBox1);
            this.DrawerAutoShow = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmManagment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SERVER MANAGEMENT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmManagment_FormClosing);
            this.Load += new System.EventHandler(this.frmManagment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private MaterialSkin.Controls.MaterialCheckbox clientcheckbox;
        private MaterialSkin.Controls.MaterialLabel label8;
        private MaterialSkin.Controls.MaterialLabel labelStatusInfo;
        private MaterialSkin.Controls.MaterialDrawer materialDrawer1;
        public MaterialSkin.Controls.MaterialCard panel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialButton buttonSendToClients;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private MaterialSkin.Controls.MaterialMultiLineTextBox textBoxRcv;
        private MaterialSkin.Controls.MaterialButton btnFileSend;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
    }
}

