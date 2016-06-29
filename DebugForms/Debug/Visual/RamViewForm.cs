using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameBoyTest.Z80;
using GameBoyTest.Memory;
using Be.Windows.Forms;

namespace GameBoyTest.Debug.Visual
{
    public partial class RamViewForm : Form
    {
        Memory.MappedMemory m_ram;

        ushort m_lastPC_Position;
        ushort m_lastSP_Position;

        public RamViewForm(Memory.MappedMemory ram)
        {
            InitializeComponent();
            m_ram = ram;
        }

        public void Init()
        {
            hexRam.ByteProvider = null;// (Be.Windows.Forms.IByteProvider)m_ram;
            //hexRam.Model.ByteProvider = m_ram;
            hexRam.Update();

            //radioButton
            this.radio_PC.Select();
            this.checkBox_autoFollow.Checked = true;

            m_lastPC_Position = 0;
            m_lastSP_Position = 0;
        }

        public void UpdateForm()
        {
            if (checkBox_autoFollow.Checked)
            {
                if (radio_PC.Checked)
                {
                    if (GameBoy.Cpu.PC != m_lastPC_Position)
                    {
                        m_lastPC_Position = GameBoy.Cpu.PC;
                        hexRam.Select(m_lastPC_Position, 1);
                    }
                }
                else if (radio_SP.Checked)
                {
                    m_lastSP_Position = GameBoy.Cpu.SP;
                    hexRam.Select(m_lastSP_Position, 1);
                }
                else
                {
                    m_lastPC_Position = GameBoy.Cpu.PC;
                    Z80Instruction inst = GameBoy.Cpu.currentInstruction;
                    long l = 1;
                    if (inst!=null)
                    {
                        l = inst.GetLenght(m_lastPC_Position);
                    }
                    hexRam.Select(m_lastPC_Position, l);
                }
            }
        }

        public void Select( long start, long length)
        {
            hexRam.Select(start, length);
        }

    }
}
