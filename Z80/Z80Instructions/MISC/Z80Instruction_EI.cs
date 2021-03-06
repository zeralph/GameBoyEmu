﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.MISC
{
    class Z80Instruction_EI : Z80Instruction
    {
        public Z80Instruction_EI()
        {
            m_Name = "EI";
            m_Summary = "";
            m_Flags = "- - - -";
            m_OpCode = new byte[] { 0xFB };

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

        public override ushort Exec(ushort instructionAdress)
        {
            GameBoy.Cpu.EnableInterrupts();
            return ++instructionAdress;
        }

        public override string ToString(ushort instructionAdress)
        {
            return "EI";
        }
    }
}
