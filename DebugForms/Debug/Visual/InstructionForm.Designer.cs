namespace GameBoyTest.Debug.Visual
{
    partial class InstructionForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.text_name = new System.Windows.Forms.TextBox();
            this.text_opcode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.text_lenght = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.text_cycles = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.text_data = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // text_name
            // 
            this.text_name.Location = new System.Drawing.Point(42, 6);
            this.text_name.Name = "text_name";
            this.text_name.Size = new System.Drawing.Size(81, 20);
            this.text_name.TabIndex = 1;
            // 
            // text_opcode
            // 
            this.text_opcode.Location = new System.Drawing.Point(154, 6);
            this.text_opcode.Name = "text_opcode";
            this.text_opcode.Size = new System.Drawing.Size(62, 20);
            this.text_opcode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Op";
            // 
            // text_lenght
            // 
            this.text_lenght.Location = new System.Drawing.Point(260, 6);
            this.text_lenght.Name = "text_lenght";
            this.text_lenght.Size = new System.Drawing.Size(62, 20);
            this.text_lenght.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(219, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Lenght";
            // 
            // text_cycles
            // 
            this.text_cycles.Location = new System.Drawing.Point(42, 36);
            this.text_cycles.Name = "text_cycles";
            this.text_cycles.Size = new System.Drawing.Size(62, 20);
            this.text_cycles.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Cycles";
            // 
            // text_data
            // 
            this.text_data.Location = new System.Drawing.Point(12, 63);
            this.text_data.Name = "text_data";
            this.text_data.Size = new System.Drawing.Size(310, 20);
            this.text_data.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(110, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Data";
            // 
            // InstructionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 95);
            this.ControlBox = false;
            this.Controls.Add(this.text_data);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.text_cycles);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.text_lenght);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.text_opcode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.text_name);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InstructionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "InstructionFrom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_name;
        private System.Windows.Forms.TextBox text_opcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_lenght;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox text_cycles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox text_data;
        private System.Windows.Forms.Label label5;
    }
}