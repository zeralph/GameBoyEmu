using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.MISC
{
    class Z80Instruction_SWAP : Z80Instruction
    {
        public Z80Instruction_SWAP()
        {
            m_IsBCInstruction = true;
            m_Name = "SWAP";
            m_Summary = "";
            m_Flags = "- - - -";
            m_OpCode = new byte[] { 0x37, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36 };

            m_NbCycles = 4;
            m_NbCyclesMax = 4;
            m_Lenght = 1;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override byte GetCurNbCycles(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0x37:
                case 0x30:
                case 0x31:
                case 0x32:
                case 0x33:
                case 0x34:
                case 0x35:
                    {
                        return 8;
                    }
                case 0x36:
                    {
                        return 16;
                    }
                default:
                    {
                        return 1;
                    }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override byte GetLenght(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0x37:
                case 0x30:
                case 0x31:
                case 0x32:
                case 0x33:
                case 0x34:
                case 0x35:
                case 0x36:
                default:
                    {
                        return 0x01;
                    }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override ushort Exec(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0x37:
                    {
                        GameBoy.Cpu.rA = Swap(GameBoy.Cpu.rA);
                        return ++instructionAdress;
                    }
                case 0x30:
                    {
                        GameBoy.Cpu.rB = Swap(GameBoy.Cpu.rB);
                        return ++instructionAdress;
                    }
                case 0x31:
                    {
                        GameBoy.Cpu.rC = Swap(GameBoy.Cpu.rC);
                        return ++instructionAdress;
                    }
                case 0x32:
                    {
                        GameBoy.Cpu.rD = Swap(GameBoy.Cpu.rD);
                        return ++instructionAdress;
                    }
                case 0x33:
                    {
                        GameBoy.Cpu.rE = Swap(GameBoy.Cpu.rE);
                        return ++instructionAdress;
                    }
                case 0x34:
                    {
                        GameBoy.Cpu.rH = Swap(GameBoy.Cpu.rH);
                        return ++instructionAdress;
                    }
                case 0x35:
                    {
                        GameBoy.Cpu.rL = Swap(GameBoy.Cpu.rL);
                        return ++instructionAdress;
                    }
                case 0x36:
                    {
                        byte val = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        val = Swap( val );
                        GameBoy.Ram.WriteByte(GameBoy.Cpu.rHL, val);
                        return ++instructionAdress;
                    }
                default:
                    {
                        return ++instructionAdress;
                    }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override string ToString(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0x37:
                    {
                        return "swap a";
                    }
                case 0x30:
                    {
                        return "swap b";
                    }
                case 0x31:
                    {
                        return "swap c";
                    }
                case 0x32:
                    {
                        return "swap d";
                    }
                case 0x33:
                    {
                        return "swap e";
                    }
                case 0x34:
                    {
                        return "swap h";
                    }
                case 0x35:
                    {
                        return "swap l";
                    }
                case 0x36:
                    {
                        return "swap hl";
                    }
                default:
                {
                    return "swap error";
                }
        }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private byte Swap(byte val)
        {
            byte result = (byte)(((val >> 4) & 0x0f) | ((val << 4) & 0xf0));
            GameBoy.Cpu.ZValue = (result == 0);
            GameBoy.Cpu.CValue = false;
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = false;

            return result;
        }
    }
}
