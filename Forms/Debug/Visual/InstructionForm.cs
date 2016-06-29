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
    public partial class InstructionForm : Form
    {
        Z80Cpu m_cpu;
        //Z80Instruction m_curInst;
        Memory.MappedMemory m_ram;
        public InstructionForm( Z80Cpu cpu, Memory.MappedMemory ram )
        {
            m_cpu = cpu;
            m_ram = ram;
            //m_curInst = null;
            InitializeComponent();
        }

        public void Init()
        {
        }

        public void UpdateForm()
        {
            ushort adr = GameBoy.Cpu.PC;
            bool bIsCB = false;
            ushort opcode = GameBoy.Ram.ReadByteAt(adr);
            if (opcode == 0xCB)
            {
                bIsCB = true;
                adr += 0x01;
                opcode = GameBoy.Ram.ReadByteAt(adr);
            }
            Z80Instruction inst = GameBoy.Cpu.decoder.GetInstructionAt(adr, bIsCB);
            if (inst != null)
            {
                this.text_name.Text = inst.name;
                String opcodeStr = String.Format("{0:x2}", opcode);
                this.text_name.Text = inst.ToString(adr);
                this.text_opcode.Text = opcodeStr;
                //this.text_lenght.Text = m_curInst.GetLenght().ToString();
                this.text_cycles.Text = inst.nbCycles.ToString() + "/" + inst.nbCyclesMax.ToString();
                //String data = m_ram.GetDataToString(m_cpu.PC+1, m_curInst.lenght-1);
                //this.text_data.Text = m_curInst.toString(); ;
            }
        }
    }
}
