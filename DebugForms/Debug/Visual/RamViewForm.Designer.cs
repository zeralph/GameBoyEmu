namespace GameBoyTest.Debug.Visual
{
    partial class RamViewForm
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
            this.hexRam = new Be.Windows.Forms.HexBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radio_inst = new System.Windows.Forms.RadioButton();
            this.checkBox_autoFollow = new System.Windows.Forms.CheckBox();
            this.radio_PC = new System.Windows.Forms.RadioButton();
            this.radio_SP = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hexRam
            // 
            this.hexRam.BoldFont = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexRam.ColumnInfoVisible = true;
            this.hexRam.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexRam.GroupSeparatorVisible = true;
            this.hexRam.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexRam.LineInfoVisible = true;
            this.hexRam.Location = new System.Drawing.Point(12, 12);
            this.hexRam.Name = "hexRam";
            this.hexRam.ReadOnly = true;
            this.hexRam.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexRam.Size = new System.Drawing.Size(669, 446);
            this.hexRam.StringViewVisible = true;
            this.hexRam.TabIndex = 0;
            this.hexRam.UseFixedBytesPerLine = true;
            this.hexRam.VScrollBarVisible = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radio_inst);
            this.groupBox1.Controls.Add(this.checkBox_autoFollow);
            this.groupBox1.Controls.Add(this.radio_PC);
            this.groupBox1.Controls.Add(this.radio_SP);
            this.groupBox1.Location = new System.Drawing.Point(13, 473);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 65);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Watch";
            // 
            // radio_inst
            // 
            this.radio_inst.AutoSize = true;
            this.radio_inst.Location = new System.Drawing.Point(110, 42);
            this.radio_inst.Name = "radio_inst";
            this.radio_inst.Size = new System.Drawing.Size(74, 17);
            this.radio_inst.TabIndex = 3;
            this.radio_inst.TabStop = true;
            this.radio_inst.Text = "Instruction";
            this.radio_inst.UseVisualStyleBackColor = true;
            // 
            // checkBox_autoFollow
            // 
            this.checkBox_autoFollow.AutoSize = true;
            this.checkBox_autoFollow.Location = new System.Drawing.Point(11, 19);
            this.checkBox_autoFollow.Name = "checkBox_autoFollow";
            this.checkBox_autoFollow.Size = new System.Drawing.Size(78, 17);
            this.checkBox_autoFollow.TabIndex = 2;
            this.checkBox_autoFollow.Text = "AutoFollow";
            this.checkBox_autoFollow.UseVisualStyleBackColor = true;
            // 
            // radio_PC
            // 
            this.radio_PC.AutoSize = true;
            this.radio_PC.Location = new System.Drawing.Point(60, 42);
            this.radio_PC.Name = "radio_PC";
            this.radio_PC.Size = new System.Drawing.Size(39, 17);
            this.radio_PC.TabIndex = 1;
            this.radio_PC.TabStop = true;
            this.radio_PC.Text = "PC";
            this.radio_PC.UseVisualStyleBackColor = true;
            // 
            // radio_SP
            // 
            this.radio_SP.AutoSize = true;
            this.radio_SP.Location = new System.Drawing.Point(6, 42);
            this.radio_SP.Name = "radio_SP";
            this.radio_SP.Size = new System.Drawing.Size(39, 17);
            this.radio_SP.TabIndex = 0;
            this.radio_SP.TabStop = true;
            this.radio_SP.Text = "SP";
            this.radio_SP.UseVisualStyleBackColor = true;
            // 
            // RamViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 541);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.hexRam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RamViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "RamViewForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Be.Windows.Forms.HexBox hexRam;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radio_PC;
        private System.Windows.Forms.RadioButton radio_SP;
        private System.Windows.Forms.CheckBox checkBox_autoFollow;
        private System.Windows.Forms.RadioButton radio_inst;

    }
}