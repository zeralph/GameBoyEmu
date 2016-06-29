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
            this.LCDStat = new System.Windows.Forms.Label();
            this.textLcdStat = new System.Windows.Forms.TextBox();
            this.textLcdC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textLcdStatHex = new System.Windows.Forms.TextBox();
            this.textLcdCHex = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.TileMapPicture2)).BeginInit();
            this.LCD.SuspendLayout();
            this.SuspendLayout();
            // 
            // TileMapPicture2
            // 
            this.TileMapPicture2.Location = new System.Drawing.Point(0, 0);
            this.TileMapPicture2.Name = "TileMapPicture2";
            this.TileMapPicture2.Size = new System.Drawing.Size(256, 256);
            this.TileMapPicture2.TabIndex = 0;
            this.TileMapPicture2.TabStop = false;
            // 
            // LCD
            // 
            this.LCD.Controls.Add(this.textLcdCHex);
            this.LCD.Controls.Add(this.textLcdStatHex);
            this.LCD.Controls.Add(this.textLcdC);
            this.LCD.Controls.Add(this.label1);
            this.LCD.Controls.Add(this.textLcdStat);
            this.LCD.Controls.Add(this.LCDStat);
            this.LCD.Location = new System.Drawing.Point(12, 269);
            this.LCD.Name = "LCD";
            this.LCD.Size = new System.Drawing.Size(244, 100);
            this.LCD.TabIndex = 1;
            this.LCD.TabStop = false;
            this.LCD.Text = "LCD";
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
            // textLcdStat
            // 
            this.textLcdStat.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLcdStat.Location = new System.Drawing.Point(94, 20);
            this.textLcdStat.Name = "textLcdStat";
            this.textLcdStat.Size = new System.Drawing.Size(80, 18);
            this.textLcdStat.TabIndex = 1;
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
            // textLcdStatHex
            // 
            this.textLcdStatHex.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLcdStatHex.Location = new System.Drawing.Point(180, 19);
            this.textLcdStatHex.Name = "textLcdStatHex";
            this.textLcdStatHex.Size = new System.Drawing.Size(46, 18);
            this.textLcdStatHex.TabIndex = 4;
            // 
            // textLcdCHex
            // 
            this.textLcdCHex.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLcdCHex.Location = new System.Drawing.Point(180, 56);
            this.textLcdCHex.Name = "textLcdCHex";
            this.textLcdCHex.Size = new System.Drawing.Size(46, 18);
            this.textLcdCHex.TabIndex = 5;
            // 
            // BgTileMapForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 381);
            this.ControlBox = false;
            this.Controls.Add(this.LCD);
            this.Controls.Add(this.TileMapPicture2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BgTileMapForm";
            this.Text = "BgTileMap";
            ((System.ComponentModel.ISupportInitialize)(this.TileMapPicture2)).EndInit();
            this.LCD.ResumeLayout(false);
            this.LCD.PerformLayout();
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
    }
}