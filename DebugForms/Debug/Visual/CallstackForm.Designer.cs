namespace GameBoyTest.Forms.Debug.Visual
{
    partial class CallstackForm
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
                m_writer.Dispose();
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
            this.textLabels = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.callstackBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkSaveCallstack = new System.Windows.Forms.CheckBox();
            this.textInstNumber = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textLabels
            // 
            this.textLabels.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textLabels.Location = new System.Drawing.Point(16, 13);
            this.textLabels.Name = "textLabels";
            this.textLabels.ReadOnly = true;
            this.textLabels.Size = new System.Drawing.Size(907, 15);
            this.textLabels.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.callstackBox);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(824, 438);
            this.splitContainer1.SplitterDistance = 342;
            this.splitContainer1.TabIndex = 3;
            // 
            // callstackBox
            // 
            this.callstackBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.callstackBox.BackColor = System.Drawing.SystemColors.Window;
            this.callstackBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.callstackBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.callstackBox.Location = new System.Drawing.Point(4, 50);
            this.callstackBox.Margin = new System.Windows.Forms.Padding(4);
            this.callstackBox.Name = "callstackBox";
            this.callstackBox.ReadOnly = true;
            this.callstackBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.callstackBox.Size = new System.Drawing.Size(816, 292);
            this.callstackBox.TabIndex = 1;
            this.callstackBox.Text = "";
            this.callstackBox.Click += new System.EventHandler(this.callstackBox_Click);
            this.callstackBox.TextChanged += new System.EventHandler(this.callstackBox_TextChanged);
            this.callstackBox.DoubleClick += new System.EventHandler(this.callstackBox_DoubleClick);
            this.callstackBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.callstackBox_MouseDown);
            this.callstackBox.MouseEnter += new System.EventHandler(this.callstackBox_MouseEnter);
            this.callstackBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.callstackBox_MouseMove);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.checkSaveCallstack);
            this.groupBox1.Controls.Add(this.textInstNumber);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(824, 74);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Callstack";
            // 
            // checkSaveCallstack
            // 
            this.checkSaveCallstack.AutoSize = true;
            this.checkSaveCallstack.Location = new System.Drawing.Point(244, 33);
            this.checkSaveCallstack.Name = "checkSaveCallstack";
            this.checkSaveCallstack.Size = new System.Drawing.Size(139, 20);
            this.checkSaveCallstack.TabIndex = 1;
            this.checkSaveCallstack.Text = "Save callstack";
            this.checkSaveCallstack.UseVisualStyleBackColor = true;
            this.checkSaveCallstack.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textInstNumber
            // 
            this.textInstNumber.Location = new System.Drawing.Point(90, 31);
            this.textInstNumber.Name = "textInstNumber";
            this.textInstNumber.Size = new System.Drawing.Size(100, 22);
            this.textInstNumber.TabIndex = 0;
            // 
            // CallstackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 451);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.textLabels);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CallstackForm";
            this.Text = "Callstack";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textLabels;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox callstackBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textInstNumber;
        private System.Windows.Forms.CheckBox checkSaveCallstack;
    }
}