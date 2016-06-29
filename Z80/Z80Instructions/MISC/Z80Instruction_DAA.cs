using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.MISC
{
    class Z80Instruction_DAA : Z80Instruction
    {
        public Z80Instruction_DAA()
        {
            m_Name = "DAA";
            m_Summary = "";
            m_Flags = "Z - 0 C";
            m_OpCode = new byte[] { 0x27 };

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
            DAA();
            return ++instructionAdress;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override string ToString(ushort instructionAdress)
        {
            return "DAA";
        }

        private void DAA()
        {
            if (!GameBoy.Cpu.NValue)
            {
                DAA_Add();
            }
            else
            {
                DAA_Sub();
            }
            if (GameBoy.Cpu.rA == 0x00)
            {
                GameBoy.Cpu.ZValue = true;
            }
            else
            {
                GameBoy.Cpu.ZValue = false;
            }
            GameBoy.Cpu.HValue = false;
        }

        private void DAA_Add()
        {
            ushort AValue = GameBoy.Cpu.rA;
            byte nl = (byte)(GameBoy.Cpu.rA & 0x0f);
            bool cFinalvalue = false;
            bool c = GameBoy.Cpu.CValue;
            bool h = GameBoy.Cpu.HValue;

            if (h || nl > 0x9)
            {
                AValue += 0x06;
            }

            ushort nh = (ushort)(AValue & 0xfff0);

            if (c || nh > 0x90)
            {
                AValue += 0x60;
                cFinalvalue = true;
            }

            GameBoy.Cpu.rA = (byte)(AValue & 0xff);
            GameBoy.Cpu.CValue = cFinalvalue;
        }

        private void DAA_Sub()
        {
            bool cFinalvalue = false;
            bool c = GameBoy.Cpu.CValue;
            bool h = GameBoy.Cpu.HValue;

            if (h)
            {
                GameBoy.Cpu.rA -= 0x06;
            }

            if (c)
            {
                GameBoy.Cpu.rA -= 0x60;
                cFinalvalue = true;
            }

            GameBoy.Cpu.CValue = cFinalvalue;
        }
    }
}
