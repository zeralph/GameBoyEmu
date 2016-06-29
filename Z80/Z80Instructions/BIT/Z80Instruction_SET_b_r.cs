using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.BIT
{
    class Z80Instruction_SET_b_r : Z80Instruction
    {
        public Z80Instruction_SET_b_r()
        {
            m_IsBCInstruction = true;
            m_Name = "SET b,r";
            m_Summary = "";
            m_Flags = "- - - -";

            m_OpCode = new byte[] { 0xC0, 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7,   // bit 0,x
                                    0xC8, 0xC9, 0xCA, 0xCB, 0xCC, 0xCD, 0xCE, 0xCF,   // bit 1,x
                                    0xD0, 0xD1, 0xD2, 0xD3, 0xD4, 0xD5, 0xD6, 0xD7,   // bit 2,x
                                    0xD8, 0xD9, 0xDA, 0xDB, 0xDC, 0xDD, 0xDE, 0xDF,   // bit 3,x
                                    0xE0, 0xE1, 0xE2, 0xE3, 0xE4, 0xE5, 0xE6, 0xE7,   // bit 4,x
                                    0xE8, 0xE9, 0xEA, 0xEB, 0xEC, 0xED, 0xEE, 0xEF,   // bit 5,x
                                    0xF0, 0xF1, 0xF2, 0xF3, 0xF4, 0xF5, 0xF6, 0xF7,   // bit 6,x
                                    0xF8, 0xF9, 0xFA, 0xFB, 0xFC, 0xFD, 0xFE, 0xFF,   // bit 7,x
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
                case 0xC6:
                case 0xCE:
                case 0xD6:
                case 0xDE:
                case 0xE6:
                case 0xEE:
                case 0xF6:
                case 0xFE:
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
            byte mask = (byte)(0x01 << value);
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
            return "set " + value + "," + register;
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
        private void BitSetRegister(byte opcode, byte value)
        {
            byte b = (byte)(opcode & 0x07);
            switch (b)
            {
                case 0x00: 
                    {
                        byte t = GameBoy.Cpu.rB;
                        GameBoy.Cpu.rB = (byte)(GameBoy.Cpu.rB | value);
                        //toto += String.Format("{0:x2} | {0:x2} = {0:x2} oOo ", t, value, GameBoy.Cpu.rB);    
                        break; 
                    }
                case 0x01: { GameBoy.Cpu.rC = (byte)(GameBoy.Cpu.rC | value); break; }
                case 0x02: { GameBoy.Cpu.rD = (byte)(GameBoy.Cpu.rD | value); break; }
                case 0x03: { GameBoy.Cpu.rE = (byte)(GameBoy.Cpu.rE | value); break; }
                case 0x04: { GameBoy.Cpu.rH = (byte)(GameBoy.Cpu.rH | value); break; }
                case 0x05: { GameBoy.Cpu.rL = (byte)(GameBoy.Cpu.rL | value); break; }
                case 0x06: 
                {
                    byte bout = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                    bout |= value;
                    GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, bout);
                    break;
                }
                case 0x07: 
                    {
                        byte t = GameBoy.Cpu.rA;
                        GameBoy.Cpu.rA = (byte)(GameBoy.Cpu.rA | value);
                        //toto = toto + String.Format("{0:x2}", t) +" | " + String.Format("{0:x2}", value) +" = "+ String.Format("{0:x2}"+GameBoy.Cpu.rA) + " oOo";   
                        break; 
                    }
            }
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
