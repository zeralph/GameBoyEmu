namespace GameBoyTest.Screen
{
    partial class GBScreenForm
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
                m_bgBitmap.Dispose();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GBScreenForm));
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.inputsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveStateF2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadStateF3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stepOneFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.emulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stretchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.originalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.speedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSpeed50 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSpeed100 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSpeed150 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSpeed200 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSpeed500 = new System.Windows.Forms.ToolStripMenuItem();
			this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nearestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bicubicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bilinearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			this.sound01ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sound02ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sound03ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sound04ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelRomName = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelEmuStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelSpeed = new System.Windows.Forms.ToolStripStatusLabel();
			this.unlimitedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point(0, 24);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(320, 288);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			this.pictureBox1.Resize += new System.EventHandler(this.pictureBox1_Resize);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.runToolStripMenuItem,
            this.emulationToolStripMenuItem,
            this.toolStripMenuItem1});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(320, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fichierToolStripMenuItem
			// 
			this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRomToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.toolStripSeparator1});
			this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
			this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.fichierToolStripMenuItem.Text = "Fichier";
			// 
			// loadRomToolStripMenuItem
			// 
			this.loadRomToolStripMenuItem.Name = "loadRomToolStripMenuItem";
			this.loadRomToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+O";
			this.loadRomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.loadRomToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.loadRomToolStripMenuItem.Text = "Load rom";
			this.loadRomToolStripMenuItem.Click += new System.EventHandler(this.loadRomToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Q";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.inputsToolStripMenuItem});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
			this.optionsToolStripMenuItem.Text = "Window";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.CheckOnClick = true;
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.ShortcutKeyDisplayString = "F12";
			this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.refreshToolStripMenuItem.Text = "Debugger";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
			// 
			// inputsToolStripMenuItem
			// 
			this.inputsToolStripMenuItem.Name = "inputsToolStripMenuItem";
			this.inputsToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.inputsToolStripMenuItem.Text = "Inputs";
			this.inputsToolStripMenuItem.Click += new System.EventHandler(this.inputsToolStripMenuItem_Click);
			// 
			// runToolStripMenuItem
			// 
			this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem1,
            this.stopToolStripMenuItem,
            this.saveStateF2ToolStripMenuItem,
            this.loadStateF3ToolStripMenuItem,
            this.resetToolStripMenuItem,
            this.stepOneFrameToolStripMenuItem});
			this.runToolStripMenuItem.Name = "runToolStripMenuItem";
			this.runToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.runToolStripMenuItem.Text = "Run";
			// 
			// runToolStripMenuItem1
			// 
			this.runToolStripMenuItem1.Name = "runToolStripMenuItem1";
			this.runToolStripMenuItem1.ShortcutKeyDisplayString = "F5";
			this.runToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.runToolStripMenuItem1.Size = new System.Drawing.Size(173, 22);
			this.runToolStripMenuItem1.Text = "Run";
			this.runToolStripMenuItem1.Click += new System.EventHandler(this.runToolStripMenuItem1_Click);
			// 
			// stopToolStripMenuItem
			// 
			this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
			this.stopToolStripMenuItem.ShortcutKeyDisplayString = "F6";
			this.stopToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
			this.stopToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.stopToolStripMenuItem.Text = "Stop";
			this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
			// 
			// saveStateF2ToolStripMenuItem
			// 
			this.saveStateF2ToolStripMenuItem.Name = "saveStateF2ToolStripMenuItem";
			this.saveStateF2ToolStripMenuItem.ShortcutKeyDisplayString = "";
			this.saveStateF2ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
			this.saveStateF2ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.saveStateF2ToolStripMenuItem.Text = "Save State";
			this.saveStateF2ToolStripMenuItem.Click += new System.EventHandler(this.saveStateF2ToolStripMenuItem_Click);
			// 
			// loadStateF3ToolStripMenuItem
			// 
			this.loadStateF3ToolStripMenuItem.Name = "loadStateF3ToolStripMenuItem";
			this.loadStateF3ToolStripMenuItem.ShortcutKeyDisplayString = "";
			this.loadStateF3ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
			this.loadStateF3ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.loadStateF3ToolStripMenuItem.Text = "Load State";
			this.loadStateF3ToolStripMenuItem.Click += new System.EventHandler(this.loadStateF3ToolStripMenuItem_Click);
			// 
			// resetToolStripMenuItem
			// 
			this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
			this.resetToolStripMenuItem.ShortcutKeyDisplayString = "F10";
			this.resetToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
			this.resetToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.resetToolStripMenuItem.Text = "Reset";
			this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
			// 
			// stepOneFrameToolStripMenuItem
			// 
			this.stepOneFrameToolStripMenuItem.Name = "stepOneFrameToolStripMenuItem";
			this.stepOneFrameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.stepOneFrameToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.stepOneFrameToolStripMenuItem.Text = "Step one frame";
			this.stepOneFrameToolStripMenuItem.Click += new System.EventHandler(this.stepOneFrameToolStripMenuItem_Click);
			// 
			// emulationToolStripMenuItem
			// 
			this.emulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeToolStripMenuItem,
            this.speedToolStripMenuItem,
            this.imageToolStripMenuItem});
			this.emulationToolStripMenuItem.Name = "emulationToolStripMenuItem";
			this.emulationToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
			this.emulationToolStripMenuItem.Text = "Emulation";
			// 
			// sizeToolStripMenuItem
			// 
			this.sizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stretchToolStripMenuItem,
            this.originalToolStripMenuItem});
			this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
			this.sizeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.sizeToolStripMenuItem.Text = "Size";
			// 
			// stretchToolStripMenuItem
			// 
			this.stretchToolStripMenuItem.Checked = true;
			this.stretchToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.stretchToolStripMenuItem.Name = "stretchToolStripMenuItem";
			this.stretchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.stretchToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.stretchToolStripMenuItem.Text = "Stretch";
			this.stretchToolStripMenuItem.Click += new System.EventHandler(this.stretchToolStripMenuItem_Click);
			// 
			// originalToolStripMenuItem
			// 
			this.originalToolStripMenuItem.Name = "originalToolStripMenuItem";
			this.originalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
			this.originalToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.originalToolStripMenuItem.Text = "Original";
			this.originalToolStripMenuItem.Click += new System.EventHandler(this.originalToolStripMenuItem_Click);
			// 
			// speedToolStripMenuItem
			// 
			this.speedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSpeed50,
            this.toolStripMenuItemSpeed100,
            this.toolStripMenuItemSpeed150,
            this.toolStripMenuItemSpeed200,
            this.toolStripMenuItemSpeed500,
            this.unlimitedToolStripMenuItem});
			this.speedToolStripMenuItem.Name = "speedToolStripMenuItem";
			this.speedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.speedToolStripMenuItem.Text = "Speed";
			// 
			// toolStripMenuItemSpeed50
			// 
			this.toolStripMenuItemSpeed50.Name = "toolStripMenuItemSpeed50";
			this.toolStripMenuItemSpeed50.Size = new System.Drawing.Size(180, 22);
			this.toolStripMenuItemSpeed50.Text = "50%";
			this.toolStripMenuItemSpeed50.Click += new System.EventHandler(this.toolStripMenuItemSpeed50_Click);
			// 
			// toolStripMenuItemSpeed100
			// 
			this.toolStripMenuItemSpeed100.Name = "toolStripMenuItemSpeed100";
			this.toolStripMenuItemSpeed100.Size = new System.Drawing.Size(180, 22);
			this.toolStripMenuItemSpeed100.Text = "100%";
			this.toolStripMenuItemSpeed100.Click += new System.EventHandler(this.toolStripMenuItemSpeed100_Click);
			// 
			// toolStripMenuItemSpeed150
			// 
			this.toolStripMenuItemSpeed150.Name = "toolStripMenuItemSpeed150";
			this.toolStripMenuItemSpeed150.Size = new System.Drawing.Size(180, 22);
			this.toolStripMenuItemSpeed150.Text = "150%";
			this.toolStripMenuItemSpeed150.Click += new System.EventHandler(this.toolStripMenuItemSpeed150_Click);
			// 
			// toolStripMenuItemSpeed200
			// 
			this.toolStripMenuItemSpeed200.Name = "toolStripMenuItemSpeed200";
			this.toolStripMenuItemSpeed200.Size = new System.Drawing.Size(180, 22);
			this.toolStripMenuItemSpeed200.Text = "200%";
			this.toolStripMenuItemSpeed200.Click += new System.EventHandler(this.toolStripMenuItemSpeed200_Click);
			// 
			// toolStripMenuItemSpeed500
			// 
			this.toolStripMenuItemSpeed500.Name = "toolStripMenuItemSpeed500";
			this.toolStripMenuItemSpeed500.Size = new System.Drawing.Size(180, 22);
			this.toolStripMenuItemSpeed500.Text = "500%";
			this.toolStripMenuItemSpeed500.Click += new System.EventHandler(this.toolStripMenuItemSpeed500_Click);
			// 
			// imageToolStripMenuItem
			// 
			this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nearestToolStripMenuItem,
            this.bicubicToolStripMenuItem,
            this.bilinearToolStripMenuItem});
			this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
			this.imageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.imageToolStripMenuItem.Text = "Image";
			// 
			// nearestToolStripMenuItem
			// 
			this.nearestToolStripMenuItem.Name = "nearestToolStripMenuItem";
			this.nearestToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.nearestToolStripMenuItem.Text = "Nearest";
			this.nearestToolStripMenuItem.Click += new System.EventHandler(this.nearestToolStripMenuItem_Click);
			// 
			// bicubicToolStripMenuItem
			// 
			this.bicubicToolStripMenuItem.Name = "bicubicToolStripMenuItem";
			this.bicubicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.bicubicToolStripMenuItem.Text = "Bicubic";
			this.bicubicToolStripMenuItem.Click += new System.EventHandler(this.bicubicToolStripMenuItem_Click);
			// 
			// bilinearToolStripMenuItem
			// 
			this.bilinearToolStripMenuItem.Name = "bilinearToolStripMenuItem";
			this.bilinearToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.bilinearToolStripMenuItem.Text = "Bilinear";
			this.bilinearToolStripMenuItem.Click += new System.EventHandler(this.bilinearToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(54, 20);
			this.toolStripMenuItem1.Text = "Debug";
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sound01ToolStripMenuItem,
            this.sound02ToolStripMenuItem,
            this.sound03ToolStripMenuItem,
            this.sound04ToolStripMenuItem});
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(108, 22);
			this.toolStripMenuItem5.Text = "Sound";
			// 
			// sound01ToolStripMenuItem
			// 
			this.sound01ToolStripMenuItem.Name = "sound01ToolStripMenuItem";
			this.sound01ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.sound01ToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.sound01ToolStripMenuItem.Text = "Sound 01 ";
			this.sound01ToolStripMenuItem.Click += new System.EventHandler(this.sound01ToolStripMenuItem_Click);
			// 
			// sound02ToolStripMenuItem
			// 
			this.sound02ToolStripMenuItem.Name = "sound02ToolStripMenuItem";
			this.sound02ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.sound02ToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.sound02ToolStripMenuItem.Text = "Sound 02";
			this.sound02ToolStripMenuItem.Click += new System.EventHandler(this.sound02ToolStripMenuItem_Click);
			// 
			// sound03ToolStripMenuItem
			// 
			this.sound03ToolStripMenuItem.Name = "sound03ToolStripMenuItem";
			this.sound03ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.sound03ToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.sound03ToolStripMenuItem.Text = "Sound 03";
			this.sound03ToolStripMenuItem.Click += new System.EventHandler(this.sound03ToolStripMenuItem_Click);
			// 
			// sound04ToolStripMenuItem
			// 
			this.sound04ToolStripMenuItem.Name = "sound04ToolStripMenuItem";
			this.sound04ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
			this.sound04ToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.sound04ToolStripMenuItem.Text = "Sound 04";
			this.sound04ToolStripMenuItem.Click += new System.EventHandler(this.sound04ToolStripMenuItem_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelRomName,
            this.toolStripStatusLabelEmuStatus,
            this.toolStripStatusLabelSpeed});
			this.statusStrip1.Location = new System.Drawing.Point(0, 312);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(320, 25);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabelRomName
			// 
			this.toolStripStatusLabelRomName.AutoSize = false;
			this.toolStripStatusLabelRomName.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabelRomName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.toolStripStatusLabelRomName.Name = "toolStripStatusLabelRomName";
			this.toolStripStatusLabelRomName.Size = new System.Drawing.Size(100, 20);
			this.toolStripStatusLabelRomName.Text = "No rom loaded";
			// 
			// toolStripStatusLabelEmuStatus
			// 
			this.toolStripStatusLabelEmuStatus.AutoSize = false;
			this.toolStripStatusLabelEmuStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabelEmuStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.toolStripStatusLabelEmuStatus.Name = "toolStripStatusLabelEmuStatus";
			this.toolStripStatusLabelEmuStatus.Size = new System.Drawing.Size(100, 20);
			// 
			// toolStripStatusLabelSpeed
			// 
			this.toolStripStatusLabelSpeed.AutoSize = false;
			this.toolStripStatusLabelSpeed.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabelSpeed.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.toolStripStatusLabelSpeed.Name = "toolStripStatusLabelSpeed";
			this.toolStripStatusLabelSpeed.Size = new System.Drawing.Size(100, 20);
			this.toolStripStatusLabelSpeed.Click += new System.EventHandler(this.toolStripStatusLabelSpeed_Click);
			this.toolStripStatusLabelSpeed.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripStatusLabelSpeed_Paint);
			// 
			// unlimitedToolStripMenuItem
			// 
			this.unlimitedToolStripMenuItem.Name = "unlimitedToolStripMenuItem";
			this.unlimitedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.unlimitedToolStripMenuItem.Text = "unlimited";
			// 
			// GBScreenForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(320, 337);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GBScreenForm";
			this.Text = "GBScreenForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GBScreenForm_FormClosing);
			this.Load += new System.EventHandler(this.GBScreenForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveStateF2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadStateF3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRomName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelEmuStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSpeed;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem emulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stretchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem originalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSpeed50;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSpeed100;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSpeed150;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSpeed200;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSpeed500;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nearestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bicubicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bilinearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sound01ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sound02ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sound03ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sound04ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepOneFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inputsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem unlimitedToolStripMenuItem;
	}
}