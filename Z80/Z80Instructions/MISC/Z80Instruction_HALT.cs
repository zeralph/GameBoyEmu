using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.MISC
{
    class Z80Instruction_HALT : Z80Instruction
    {
        public Z80Instruction_HALT()
        {
            m_Name = "HALT";
            m_Summary = "";
            m_Flags = "- - - -";
            m_OpCode = new byte[] { 0x76 };

            m_NbCycles = 4;
            m_NbCyclesMax = 4;
            m_Lenght = 1;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override byte GetCurNbCycles(ushort instructionAdress)
        {
            return 4;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override byte GetLenght(ushort instructionAdress)
        {
            return 0x01;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override ushort Exec(ushort instructionAdress)
        {
            //disable interrupts
            //GameBoy.Ram.WriteByte(0xFFFF, 0x00);
            //GameBoy.Cpu.Stop();
            GameBoy.Cpu.Halt();
            return ++instructionAdress;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override string ToString(ushort instructionAdress)
        {
            return "HALT";
        }
    }
}
