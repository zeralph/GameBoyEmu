namespace GameBoyTest.Debug.Visual
{
    partial class SpeedForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.trackBar_speed = new System.Windows.Forms.TrackBar();
            this.label_curSpeed = new System.Windows.Forms.Label();
            this.button_normalSpeed = new System.Windows.Forms.Button();
            this.button_lowSpeed = new System.Windows.Forms.Button();
            this.labelRealSpeed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_speed)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar_speed
            // 
            this.trackBar_speed.Location = new System.Drawing.Point(12, 12);
            this.trackBar_speed.Maximum = 20;
            this.trackBar_speed.Minimum = 1;
            this.trackBar_speed.Name = "trackBar_speed";
            this.trackBar_speed.Size = new System.Drawing.Size(267, 45);
            this.trackBar_speed.SmallChange = 0;
            this.trackBar_speed.TabIndex = 0;
            this.trackBar_speed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_speed.Value = 4;
            this.trackBar_speed.Scroll += new System.EventHandler(this.trackBar_speed_Scroll);
            // 
            // label_curSpeed
            // 
            this.label_curSpeed.AutoSize = true;
            this.label_curSpeed.Location = new System.Drawing.Point(96, 47);
            this.label_curSpeed.Name = "label_curSpeed";
            this.label_curSpeed.Size = new System.Drawing.Size(81, 13);
            this.label_curSpeed.TabIndex = 1;
            this.label_curSpeed.Text = "label_curSpeed";
            // 
            // button_normalSpeed
            // 
            this.button_normalSpeed.Location = new System.Drawing.Point(12, 66);
            this.button_normalSpeed.Name = "button_normalSpeed";
            this.button_normalSpeed.Size = new System.Drawing.Size(75, 23);
            this.button_normalSpeed.TabIndex = 2;
            this.button_normalSpeed.Text = "4Mhz";
            this.button_normalSpeed.UseVisualStyleBackColor = true;
            this.button_normalSpeed.Click += new System.EventHandler(this.button_normalSpeed_Click);
            // 
            // button_lowSpeed
            // 
            this.button_lowSpeed.Location = new System.Drawing.Point(197, 66);
            this.button_lowSpeed.Name = "button_lowSpeed";
            this.button_lowSpeed.Size = new System.Drawing.Size(75, 23);
            this.button_lowSpeed.TabIndex = 3;
            this.button_lowSpeed.Text = "1Hz";
            this.button_lowSpeed.UseVisualStyleBackColor = true;
            this.button_lowSpeed.Click += new System.EventHandler(this.button_lowSpeed_Click);
            // 
            // labelRealSpeed
            // 
            this.labelRealSpeed.AutoSize = true;
            this.labelRealSpeed.Location = new System.Drawing.Point(99, 66);
            this.labelRealSpeed.Name = "labelRealSpeed";
            this.labelRealSpeed.Size = new System.Drawing.Size(35, 13);
            this.labelRealSpeed.TabIndex = 4;
            this.labelRealSpeed.Text = "label1";
            // 
            // SpeedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.ControlBox = false;
            this.Controls.Add(this.labelRealSpeed);
            this.Controls.Add(this.button_lowSpeed);
            this.Controls.Add(this.button_normalSpeed);
            this.Controls.Add(this.label_curSpeed);
            this.Controls.Add(this.trackBar_speed);
            this.Name = "SpeedForm";
            this.Text = "Speed";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_speed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar_speed;
        private System.Windows.Forms.Label label_curSpeed;
        private System.Windows.Forms.Button button_normalSpeed;
        private System.Windows.Forms.Button button_lowSpeed;
        private System.Windows.Forms.Label labelRealSpeed;
    }
}