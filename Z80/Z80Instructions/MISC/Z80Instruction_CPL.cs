﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.MISC
{
    class Z80Instruction_CPL : Z80Instruction
    {
        public Z80Instruction_CPL()
        {
            m_Name = "CPL";
            m_Summary = "";
            m_Flags = "- 1 1 -";
            m_OpCode = new byte[] { 0x2F };

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
            GameBoy.Cpu.rA = (byte)~GameBoy.Cpu.rA;
            GameBoy.Cpu.NValue = true;
            GameBoy.Cpu.HValue = true;
            return ++instructionAdress;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override string ToString(ushort instructionAdress)
        {
            return "cpl";
        }
    }
}
