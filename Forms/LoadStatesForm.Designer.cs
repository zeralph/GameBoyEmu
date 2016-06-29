namespace GameBoyTest.Forms
{
    partial class LoadStatesForm
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
            this.StateFilsList = new System.Windows.Forms.ListView();
            this.image = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // StateFilsList
            // 
            this.StateFilsList.AutoArrange = false;
            this.StateFilsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.image,
            this.columnHeader3});
            this.StateFilsList.Location = new System.Drawing.Point(12, 12);
            this.StateFilsList.MultiSelect = false;
            this.StateFilsList.Name = "StateFilsList";
            this.StateFilsList.Size = new System.Drawing.Size(456, 237);
            this.StateFilsList.TabIndex = 0;
            this.StateFilsList.UseCompatibleStateImageBehavior = false;
            this.StateFilsList.DoubleClick += new System.EventHandler(this.StateFilsList_DoubleClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 200;
            // 
            // LoadStatesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 261);
            this.Controls.Add(this.StateFilsList);
            this.Name = "LoadStatesForm";
            this.Text = "LoadStatesForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView StateFilsList;
        private System.Windows.Forms.ColumnHeader image;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}