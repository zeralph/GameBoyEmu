﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.MISC
{
    class Z80Instruction_SCF : Z80Instruction
    {
        public Z80Instruction_SCF()
        {
            m_Name = "SCF";
            m_Summary = "";
            m_Flags = "- 0 0 1";
            m_OpCode = new byte[] { 0x37 };

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
            GameBoy.Cpu.CValue = true;
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = false;
            return ++instructionAdress;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override string ToString(ushort instructionAdress)
        {
            return "scf";
        }
    }
}
