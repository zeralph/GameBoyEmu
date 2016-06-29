using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameBoyTest.Z80;

namespace GameBoyTest.Debug.Visual
{
    public partial class Z80ViewForm : Form
    {
        Z80Cpu m_cpu;
        bool m_Step;
        bool m_autoStep = false;
        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public Z80ViewForm( Z80Cpu z80 )
        {
            InitializeComponent();
            m_cpu = z80;
        }

        public void Init()
        {  
            m_Step = false;
            m_autoStep = false;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public bool CanStep()
        {
            return m_Step || m_autoStep;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void DoStart()
        {
            m_autoStep = true;
            if (!GameBoy.Cpu.running)
            {
                GameBoy.Cpu.Start();
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void DoStepInto()
        {
            m_Step = true;
            GameBoy.Cpu.RunOnce();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void DoStepOver()
        {
            m_Step = true;
            GameBoy.Cpu.RunToStepOver();
        }
        
        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void DoStop()
        {
            m_autoStep = false;
            GameBoy.Cpu.Stop();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void ForceStep()
        {
            m_Step = true;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void ResetStep()
        {
            m_Step = false;
        }

        public bool IsAutoStep()
        {
            return m_autoStep;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void UpdateForm()
        {
            if (m_cpu==null)
                return;
            m_autoStep &= GameBoy.Cpu.running;
            //flags 
            text_f_Z.Text = m_cpu.ZValue ? "1" : "0";
            text_f_N.Text = m_cpu.NValue ? "1" : "0";
            text_f_H.Text = m_cpu.HValue ? "1" : "0";
            text_f_C.Text = m_cpu.CValue ? "1" : "0";
            //registers
            String s;
            s = String.Format("{0:x2}", m_cpu.rA);
            text_r_A.Text = s;
            s = String.Format("{0:x2}", m_cpu.rB);
            text_r_B.Text = s;
            s = String.Format("{0:x2}", m_cpu.rC);
            text_r_C.Text = s;
            s = String.Format("{0:x2}", m_cpu.rD);
            text_r_D.Text = s;
            s = String.Format("{0:x2}", m_cpu.rE);
            text_r_E.Text = s;
            s = String.Format("{0:x2}", m_cpu.rF);
            text_r_F.Text = s;
            s = String.Format("{0:x2}", m_cpu.rH);
            text_r_H.Text = s;
            s = String.Format("{0:x2}", m_cpu.rL);
            text_r_L.Text = s;
            s = String.Format("{0:x4}", m_cpu.rBC);
            text_r_BC.Text = s;
            s = String.Format("{0:x4}", m_cpu.rDE);
            text_r_DE.Text = s;
            s = String.Format("{0:x4}", m_cpu.rHL);
            text_r_HL.Text = s;
            s = String.Format("{0:x4}", m_cpu.rAF);
            text_r_AF.Text = s;
            //pointers
            s = String.Format("{0:x4}", m_cpu.PC);
            text_PC.Text = s;
            s = String.Format("{0:x4}", m_cpu.SP);
            text_SP.Text = s;
            //inst
            s = m_cpu.GetInstructionNumber().ToString();
            text_inst_nb.Text = s;

            byte b = GameBoy.Ram.ReadByteAt(0xFFFF);
            s = String.Format("{0:x2}", b);
            ei_txt.Text = s;
            b = GameBoy.Ram.ReadByteAt(0xFF0F);
            s = String.Format("{0:x2}", b);
            if_txt.Text = s;

            b = GameBoy.Ram.ReadByteAt(0xFF40);
            s = String.Format("{0:x2}", b);
            Sc_line.Text = s;
            
            
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void btn_StepInto_Click(object sender, EventArgs e)
        {
            DoStepInto();
            DebugFunctions.CodeViewForm().GotoPC();
        }

        private void text_Execution_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            m_cpu.Init();
            DebugFunctions.ResetDebug();
        }

        private void btn_Run_Click(object sender, EventArgs e)
        {
            DoStart();
        }

        private void btn_StepOver_Click(object sender, EventArgs e)
        {
            m_autoStep = true;  
            DoStepOver();
            DebugFunctions.CodeViewForm().GotoPC();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            DoStop();
        }

        private void text_inst_nb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
