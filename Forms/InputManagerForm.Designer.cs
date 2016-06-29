namespace GameBoyTest.Forms
{
    partial class InputManagerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.UpInput = new System.Windows.Forms.TextBox();
            this.DownInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LeftInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RightInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AInput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BInput = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.StartInput = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SelectInput = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboInputSelection = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 22);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Up";
            // 
            // UpInput
            // 
            this.UpInput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.UpInput.Location = new System.Drawing.Point(59, 19);
            this.UpInput.Name = "UpInput";
            this.UpInput.ReadOnly = true;
            this.UpInput.Size = new System.Drawing.Size(100, 20);
            this.UpInput.TabIndex = 1;
            this.UpInput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UpInput_MouseClick);
            // 
            // DownInput
            // 
            this.DownInput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DownInput.Location = new System.Drawing.Point(59, 48);
            this.DownInput.Name = "DownInput";
            this.DownInput.ReadOnly = true;
            this.DownInput.Size = new System.Drawing.Size(100, 20);
            this.DownInput.TabIndex = 3;
            this.DownInput.Click += new System.EventHandler(this.DownInput_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 51);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Down";
            // 
            // LeftInput
            // 
            this.LeftInput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LeftInput.Location = new System.Drawing.Point(59, 78);
            this.LeftInput.Name = "LeftInput";
            this.LeftInput.ReadOnly = true;
            this.LeftInput.Size = new System.Drawing.Size(100, 20);
            this.LeftInput.TabIndex = 5;
            this.LeftInput.Click += new System.EventHandler(this.LeftInput_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 78);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Left";
            // 
            // RightInput
            // 
            this.RightInput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RightInput.Location = new System.Drawing.Point(59, 107);
            this.RightInput.Name = "RightInput";
            this.RightInput.ReadOnly = true;
            this.RightInput.Size = new System.Drawing.Size(100, 20);
            this.RightInput.TabIndex = 7;
            this.RightInput.Click += new System.EventHandler(this.RightInput_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 110);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Right";
            // 
            // AInput
            // 
            this.AInput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AInput.Location = new System.Drawing.Point(212, 19);
            this.AInput.Name = "AInput";
            this.AInput.ReadOnly = true;
            this.AInput.Size = new System.Drawing.Size(100, 20);
            this.AInput.TabIndex = 9;
            this.AInput.Click += new System.EventHandler(this.AInput_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(192, 22);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "A";
            // 
            // BInput
            // 
            this.BInput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BInput.Location = new System.Drawing.Point(212, 48);
            this.BInput.Name = "BInput";
            this.BInput.ReadOnly = true;
            this.BInput.Size = new System.Drawing.Size(100, 20);
            this.BInput.TabIndex = 11;
            this.BInput.Click += new System.EventHandler(this.BInput_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(192, 51);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "B";
            // 
            // StartInput
            // 
            this.StartInput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.StartInput.Location = new System.Drawing.Point(212, 78);
            this.StartInput.Name = "StartInput";
            this.StartInput.ReadOnly = true;
            this.StartInput.Size = new System.Drawing.Size(100, 20);
            this.StartInput.TabIndex = 13;
            this.StartInput.Click += new System.EventHandler(this.StartInput_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(177, 81);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Start";
            // 
            // SelectInput
            // 
            this.SelectInput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SelectInput.Location = new System.Drawing.Point(212, 107);
            this.SelectInput.Name = "SelectInput";
            this.SelectInput.ReadOnly = true;
            this.SelectInput.Size = new System.Drawing.Size(100, 20);
            this.SelectInput.TabIndex = 15;
            this.SelectInput.Click += new System.EventHandler(this.SelectInput_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(169, 110);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Select";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.SelectInput);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.UpInput);
            this.groupBox1.Controls.Add(this.StartInput);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.DownInput);
            this.groupBox1.Controls.Add(this.BInput);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.LeftInput);
            this.groupBox1.Controls.Add(this.AInput);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.RightInput);
            this.groupBox1.Location = new System.Drawing.Point(12, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 148);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // comboInputSelection
            // 
            this.comboInputSelection.FormattingEnabled = true;
            this.comboInputSelection.Location = new System.Drawing.Point(50, 10);
            this.comboInputSelection.Name = "comboInputSelection";
            this.comboInputSelection.Size = new System.Drawing.Size(290, 21);
            this.comboInputSelection.TabIndex = 17;
            this.comboInputSelection.SelectedIndexChanged += new System.EventHandler(this.comboInputSelection_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Input";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(71, 201);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 19;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(224, 201);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // InputManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 238);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboInputSelection);
            this.Controls.Add(this.groupBox1);
            this.Name = "InputManagerForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "InputManagerForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputManagerForm_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UpInput;
        private System.Windows.Forms.TextBox DownInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LeftInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox RightInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox AInput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox BInput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox StartInput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox SelectInput;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboInputSelection;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
    }
}