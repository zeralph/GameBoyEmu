namespace GameBoyTest.Debug.Visual
{
    partial class BgTileMapForm
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
                m_bitmap.Dispose();
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
            this.TileMapPicture2 = new System.Windows.Forms.PictureBox();
            this.LCD = new System.Windows.Forms.GroupBox();
            this.textLcdCHex = new System.Windows.Forms.TextBox();
            this.textLcdStatHex = new System.Windows.Forms.TextBox();
            this.textLcdC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textLcdStat = new System.Windows.Forms.TextBox();
            this.LCDStat = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Tile = new System.Windows.Forms.Label();
            this.tileNumber = new System.Windows.Forms.TextBox();
            this.tileAdr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TileZoom = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.TileMapPicture2)).BeginInit();
            this.LCD.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TileZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // TileMapPicture2
            // 
            this.TileMapPicture2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TileMapPicture2.Cursor = System.Windows.Forms.Cursors.Cross;
            this.TileMapPicture2.Location = new System.Drawing.Point(1, 0);
            this.TileMapPicture2.Name = "TileMapPicture2";
            this.TileMapPicture2.Size = new System.Drawing.Size(256, 256);
            this.TileMapPicture2.TabIndex = 0;
            this.TileMapPicture2.TabStop = false;
            this.TileMapPicture2.Paint += new System.Windows.Forms.PaintEventHandler(this.TileMapPicture2_Paint);
            this.TileMapPicture2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TileMapPicture2_MouseMove);
            // 
            // LCD
            // 
            this.LCD.Controls.Add(this.textLcdCHex);
            this.LCD.Controls.Add(this.textLcdStatHex);
            this.LCD.Controls.Add(this.textLcdC);
            this.LCD.Controls.Add(this.label1);
            this.LCD.Controls.Add(this.textLcdStat);
            this.LCD.Controls.Add(this.LCDStat);
            this.LCD.Location = new System.Drawing.Point(43, 518);
            this.LCD.Name = "LCD";
            this.LCD.Size = new System.Drawing.Size(244, 100);
            this.LCD.TabIndex = 1;
            this.LCD.TabStop = false;
            this.LCD.Text = "LCD";
            // 
            // textLcdCHex
            // 
            this.textLcdCHex.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLcdCHex.Location = new System.Drawing.Point(180, 56);
            this.textLcdCHex.Name = "textLcdCHex";
            this.textLcdCHex.Size = new System.Drawing.Size(46, 18);
            this.textLcdCHex.TabIndex = 5;
            // 
            // textLcdStatHex
            // 
            this.textLcdStatHex.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLcdStatHex.Location = new System.Drawing.Point(180, 19);
            this.textLcdStatHex.Name = "textLcdStatHex";
            this.textLcdStatHex.Size = new System.Drawing.Size(46, 18);
            this.textLcdStatHex.TabIndex = 4;
            // 
            // textLcdC
            // 
            this.textLcdC.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLcdC.Location = new System.Drawing.Point(94, 56);
            this.textLcdC.Name = "textLcdC";
            this.textLcdC.Size = new System.Drawing.Size(80, 18);
            this.textLcdC.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "LCDC (FF40)";
            // 
            // textLcdStat
            // 
            this.textLcdStat.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLcdStat.Location = new System.Drawing.Point(94, 20);
            this.textLcdStat.Name = "textLcdStat";
            this.textLcdStat.Size = new System.Drawing.Size(80, 18);
            this.textLcdStat.TabIndex = 1;
            // 
            // LCDStat
            // 
            this.LCDStat.AutoSize = true;
            this.LCDStat.Location = new System.Drawing.Point(8, 23);
            this.LCDStat.Name = "LCDStat";
            this.LCDStat.Size = new System.Drawing.Size(80, 13);
            this.LCDStat.TabIndex = 0;
            this.LCDStat.Text = "LCDStat (FF41)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TileZoom);
            this.groupBox1.Controls.Add(this.tileAdr);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tileNumber);
            this.groupBox1.Controls.Add(this.Tile);
            this.groupBox1.Location = new System.Drawing.Point(307, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 227);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // Tile
            // 
            this.Tile.AutoSize = true;
            this.Tile.Location = new System.Drawing.Point(12, 35);
            this.Tile.Name = "Tile";
            this.Tile.Size = new System.Drawing.Size(37, 13);
            this.Tile.TabIndex = 0;
            this.Tile.Text = "Tile n°";
            // 
            // tileNumber
            // 
            this.tileNumber.Location = new System.Drawing.Point(71, 32);
            this.tileNumber.Name = "tileNumber";
            this.tileNumber.Size = new System.Drawing.Size(84, 20);
            this.tileNumber.TabIndex = 1;
            // 
            // tileAdr
            // 
            this.tileAdr.Location = new System.Drawing.Point(71, 64);
            this.tileAdr.Name = "tileAdr";
            this.tileAdr.Size = new System.Drawing.Size(84, 20);
            this.tileAdr.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tile adr";
            // 
            // TileZoom
            // 
            this.TileZoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TileZoom.Location = new System.Drawing.Point(91, 90);
            this.TileZoom.Name = "TileZoom";
            this.TileZoom.Size = new System.Drawing.Size(64, 64);
            this.TileZoom.TabIndex = 4;
            this.TileZoom.TabStop = false;
            this.TileZoom.Click += new System.EventHandler(this.pictureBox1_Click);
            this.TileZoom.Paint += new System.Windows.Forms.PaintEventHandler(this.TileZoom_Paint);
            // 
            // BgTileMapForm
            // 
            this.ClientSize = new System.Drawing.Size(559, 627);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LCD);
            this.Controls.Add(this.TileMapPicture2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BgTileMapForm";
            this.Text = "BgTileMap";
            ((System.ComponentModel.ISupportInitialize)(this.TileMapPicture2)).EndInit();
            this.LCD.ResumeLayout(false);
            this.LCD.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TileZoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox TileMapPicture2;
        private System.Windows.Forms.GroupBox LCD;
        private System.Windows.Forms.TextBox textLcdC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textLcdStat;
        private System.Windows.Forms.Label LCDStat;
        private System.Windows.Forms.TextBox textLcdCHex;
        private System.Windows.Forms.TextBox textLcdStatHex;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox TileZoom;
        private System.Windows.Forms.TextBox tileAdr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tileNumber;
        private System.Windows.Forms.Label Tile;
    }
}