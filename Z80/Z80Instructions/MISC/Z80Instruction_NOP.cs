//![DO NOT UPDATE]

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.MISC
{
    class Z80Instruction_NOP:Z80Instruction
    {
        public Z80Instruction_NOP()
        {
            m_Name = "NOP";
            m_Summary = "";
			m_Flags = "- - - -";
            m_OpCode = new byte[] {0x00};
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

        public override byte GetLenght(ushort instructionAdress)
        {
            return 0x01;
        }

        public override bool IsNOP()
        {
            return true;
        }

        public override void Init()
        {
            base.Init();
        }

        public override ushort Exec(ushort instructionAdress)
        {
            return (ushort)(instructionAdress + 0x01);
        }

        public override string ToString( ushort InstructionAdress)
        {
            return "nop";
        }
    }
}
