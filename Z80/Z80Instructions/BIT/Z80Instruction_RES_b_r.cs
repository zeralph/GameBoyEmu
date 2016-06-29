using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.BIT
{
    class Z80Instruction_RES_b_r : Z80Instruction
    {
        public Z80Instruction_RES_b_r()
        {
            m_IsBCInstruction = true;
            m_Name = "RES b,r";
            m_Summary = "";
            m_Flags = "- - - -";
            m_OpCode = new byte[] { 0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87,   // bit 0,x
                                    0x88, 0x89, 0x8A, 0x8B, 0x8C, 0x8D, 0x8E, 0x8F,   // bit 1,x
                                    0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97,   // bit 2,x
                                    0x98, 0x99, 0x9A, 0x9B, 0x9C, 0x9D, 0x9E, 0x9F,   // bit 3,x
                                    0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7,   // bit 4,x
                                    0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF,   // bit 5,x
                                    0xB0, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7,   // bit 6,x
                                    0xB8, 0xB9, 0xBA, 0xBB, 0xBC, 0xBD, 0xBE, 0xBF,   // bit B,x
            };

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
                case 0x86:
                case 0x8E:
                case 0x96:
                case 0x9E:
                case 0xA6:
                case 0xAE:
                case 0xB6:
                case 0xBE:
                    {
                        return 16;
                    }
                default:
                    {
                        return 8;
                    }
            }
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
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            byte value = BitGetIndex(opcode);
            byte mask = (byte)(~ (byte)(0x01 << value));
            BitSetRegister(opcode, mask);
            return ++instructionAdress;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override String ToString(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            String register = BitGetRegisterStr(opcode);

            byte value = BitGetIndex(opcode);
            return "res " + value + "," + register;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private byte BitGetIndex(byte opcode)
        {
            byte b = (byte)((opcode & 0x38) >> 3);
            switch (b)
            {
                case 0x00: return 0;
                case 0x01: return 1;
                case 0x02: return 2;
                case 0x03: return 3;
                case 0x04: return 4;
                case 0x05: return 5;
                case 0x06: return 6;
                case 0x07: return 7;
            }
            return 0;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private byte BitSetRegister(byte opcode, byte value)
        {
            byte b = (byte)(opcode & 0x07);
            switch (b)
            {
                case 0x00: {GameBoy.Cpu.rB = (byte)(GameBoy.Cpu.rB & value ); break; }
                case 0x01: {GameBoy.Cpu.rC = (byte)(GameBoy.Cpu.rC & value ); break; }
                case 0x02: {GameBoy.Cpu.rD = (byte)(GameBoy.Cpu.rD & value ); break; }
                case 0x03: {GameBoy.Cpu.rE = (byte)(GameBoy.Cpu.rE & value ); break; }
                case 0x04: {GameBoy.Cpu.rH = (byte)(GameBoy.Cpu.rH & value ); break; }
                case 0x05: {GameBoy.Cpu.rL = (byte)(GameBoy.Cpu.rL & value ); break; }
                case 0x06:
                    {
                        byte bout = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        bout &= value;
                        GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, bout);
                        break;
                    }
                case 0x07: { GameBoy.Cpu.rA = (byte)(GameBoy.Cpu.rA & value); break; }
            }
            return 0;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private String BitGetRegisterStr(byte opcode)
        {
            byte b = (byte)(opcode & 0x07);
            switch (b)
            {
                case 0x00: return "b";
                case 0x01: return "c";
                case 0x02: return "d";
                case 0x03: return "e";
                case 0x04: return "h";
                case 0x05: return "l";
                case 0x06: return "(hl)";
                case 0x07: return "a";
            }
            return "err";
        }
    }
}
