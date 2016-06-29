using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.MISC
{
    class Z80Instruction_STOP : Z80Instruction
    {
        public Z80Instruction_STOP()
        {
            m_Name = "STOP";
            m_Summary = "";
            m_Flags = "- - - -";
            m_OpCode = new byte[] { 0x10 };

            m_NbCycles = 4;
            m_NbCyclesMax = 4;
            m_Lenght = 2;
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
            return 0x02;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override ushort Exec(ushort instructionAdress)
        {
            //GameBoy.Cpu.Stop();
            return (ushort)(instructionAdress+2);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override string ToString(ushort instructionAdress)
        {
            return "STOP";
        }
    }
}
