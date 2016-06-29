namespace GameBoyTest.Forms.Debug.Visual
{
    partial class CodeView3Form
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.codeBox = new System.Windows.Forms.RichTextBox();
            this.breakPointsBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textAdress = new System.Windows.Forms.TextBox();
            this.gotoCursor = new System.Windows.Forms.Button();
            this.refresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.codeBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.breakPointsBox);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.gotoCursor);
            this.splitContainer1.Panel2.Controls.Add(this.refresh);
            this.splitContainer1.Size = new System.Drawing.Size(834, 488);
            this.splitContainer1.SplitterDistance = 688;
            this.splitContainer1.TabIndex = 2;
            // 
            // codeBox
            // 
            this.codeBox.BackColor = System.Drawing.SystemColors.Window;
            this.codeBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.codeBox.DetectUrls = false;
            this.codeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.codeBox.Location = new System.Drawing.Point(0, 0);
            this.codeBox.Name = "codeBox";
            this.codeBox.ReadOnly = true;
            this.codeBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.codeBox.ShortcutsEnabled = false;
            this.codeBox.Size = new System.Drawing.Size(688, 488);
            this.codeBox.TabIndex = 0;
            this.codeBox.Text = "";
            this.codeBox.Click += new System.EventHandler(this.codeBox_Click);
            this.codeBox.TextChanged += new System.EventHandler(this.codeBox_TextChanged);
            this.codeBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.codeBox_MouseDoubleClick);
            // 
            // breakPointsBox
            // 
            this.breakPointsBox.FormattingEnabled = true;
            this.breakPointsBox.Location = new System.Drawing.Point(19, 157);
            this.breakPointsBox.Name = "breakPointsBox";
            this.breakPointsBox.Size = new System.Drawing.Size(111, 329);
            this.breakPointsBox.TabIndex = 6;
            this.breakPointsBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.breakPointsBox_MouseClick);
            this.breakPointsBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.breakPointsBox_MouseDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textAdress);
            this.groupBox1.Location = new System.Drawing.Point(19, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(111, 53);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Adress";
            // 
            // textAdress
            // 
            this.textAdress.Location = new System.Drawing.Point(7, 20);
            this.textAdress.Name = "textAdress";
            this.textAdress.Size = new System.Drawing.Size(98, 20);
            this.textAdress.TabIndex = 0;
            this.textAdress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textAdress_KeyPress);
            // 
            // gotoCursor
            // 
            this.gotoCursor.Location = new System.Drawing.Point(19, 55);
            this.gotoCursor.Name = "gotoCursor";
            this.gotoCursor.Size = new System.Drawing.Size(111, 37);
            this.gotoCursor.TabIndex = 3;
            this.gotoCursor.Text = "Goto cursor";
            this.gotoCursor.UseVisualStyleBackColor = true;
            this.gotoCursor.Click += new System.EventHandler(this.gotoCursor_Click);
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(19, 12);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(111, 37);
            this.refresh.TabIndex = 2;
            this.refresh.Text = "Refresh";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // CodeView3Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 488);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CodeView3Form";
            this.Text = "CodeView2Form";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button gotoCursor;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textAdress;
        private System.Windows.Forms.RichTextBox codeBox;
        private System.Windows.Forms.ListBox breakPointsBox;
    }
}