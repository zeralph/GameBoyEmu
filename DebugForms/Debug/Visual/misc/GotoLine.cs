using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameBoyTest.Forms.Debug.Visual;

namespace GameBoyTest.Debug.Visual.Misc
{
    public partial class GotoLine : Form
    {
        private Form m_parentForm;

        public GotoLine(Form parent)
        {
            InitializeComponent();
            m_parentForm = parent;
        }

        private void button_go_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                Search();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Search()
        {
            String s = textBox_gotoLine.Text;
            this.Close();
            if (m_parentForm is CodeView3Form)
            {
                CodeView3Form form = m_parentForm as CodeView3Form;
                try
                {
                    //ushort line = (ushort)Convert.ToInt32(s, 16);
                    //form.HighligthLine( s );
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Search error " + ex);  
                }
            }
            //this.Dispose();
        }
    }
}
