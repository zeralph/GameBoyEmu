using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace GameBoyTest.Debug.Visual
{
    public partial class DebuggerForm : Form
    {
        public DebuggerForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.Width = 1600;
            this.Height = 1200;
            this.Visible = true;
            this.KeyPreview = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            /*
            if( keyData == Keys.F2)
            {
                State.StateSystem.saveState("save.state");
            }
            if (keyData == Keys.F3)
            {
                State.StateSystem.loadState("save.state");
            }
            if (keyData == Keys.F11)
            {
                DebugFunctions.Z80Form().DoStepInto();
                DebugFunctions.CodeViewForm().GotoPC();
                return true;    // indicate that you handled this keystroke
            }
            if (keyData == Keys.F10)
            {
                DebugFunctions.Z80Form().DoStepOver();
                DebugFunctions.CodeViewForm().GotoPC();
                return true;    // indicate that you handled this keystroke
            }
            if (keyData == Keys.F5)
            {
                DebugFunctions.Z80Form().DoStart();
                return true;    // indicate that you handled this keystroke
            }
            if (keyData == Keys.F6)
            {
                DebugFunctions.Z80Form().DoStop();
                return true;    // indicate that you handled this keystroke
            }
            */ 
            // Call the base class 
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void Init()
        {

        }

        public void SetTitle( string s )
        {
            this.Text = s;
        }

        public void UpdateForm()
        {
            //toolStripStatus_curInst.Text = GameBoy.Cpu.GetInstructionNumber().ToString();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebugFunctions.DoRefresh(refreshToolStripMenuItem.Checked);
        }

        private void DebugForm_Load(object sender, EventArgs e)
        {

        }

        private void DebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameBoy.EnableDebugger(false);
        }

        private void loadRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread_bug = new Thread(new ThreadStart(
            delegate
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                //m_cartridge.Init("..//rom//ld.gb");
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = ".";
                openFileDialog1.Filter = "rom files(*.gb)|*.gb|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    String s = openFileDialog1.FileName;
                    GameBoy.LoadCartridge(s);    
                }
            }));
            thread_bug.SetApartmentState(ApartmentState.STA);  /*<=*/
            thread_bug.Start();
        }
    }
}
