using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.ALU
{
    class Z80Instruction_ALU_16bit : Z80Instruction
    {
        public Z80Instruction_ALU_16bit()
        {
            m_Name = "ALU16";
            m_Summary = "";
            m_Flags = "- - - -";
            m_OpCode = new byte[] { 0x09, 0x19, 0x29, 0x39,     //add hl, n
                                    0xE8,                       //add SP, n
                                    0x03, 0x13, 0x23, 0x33,     //INC nn
                                    0x0B, 0x1B, 0x2B, 0x3B,     //DEC nn
            };

            m_NbCycles = 4;
            m_NbCyclesMax = 4;
            m_Lenght = 1;
        }

        public override void Init()
        {
            base.Init();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override byte GetCurNbCycles(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0x09:
                case 0x19:
                case 0x29:
                case 0x39:
                case 0x03:
                case 0x13:
                case 0x23:
                case 0x33:
                case 0x0B:
                case 0x1B:
                case 0x2B:
                case 0x3B:
                    {
                        return 8;
                    }
                case 0xE8:
                    {
                        return 16;
                    }
                default:
                    {
                        throw new Exception("Wrong instruction timing");
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
                case 0x09:
                case 0x19:
                case 0x29:
                case 0x39:
                    {
                        return 0x01;
                    }
                case 0xE8:
                    {
                        return 0x02;
                    }
                case 0x03:
                case 0x13:
                case 0x23:
                case 0x33:
                case 0x0B:
                case 0x1B:
                case 0x2B:
                case 0x3B:
                    {
                        return 0x01;
                    }
                default:
                    {
                        throw new Exception("Wrong instruction timing");
                    }
            }
        }

        public override ushort Exec(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0x09:
                    {
                        ++instructionAdress;
                        GameBoy.Cpu.rHL = UpdateFlagsADD(GameBoy.Cpu.rHL, GameBoy.Cpu.rBC);
                        return instructionAdress;
                    }
                case 0x19:
                    {
                        ++instructionAdress;
                        GameBoy.Cpu.rHL = UpdateFlagsADD(GameBoy.Cpu.rHL, GameBoy.Cpu.rDE);
                        return instructionAdress;
                    }
                case 0x29:
                    {
                        ++instructionAdress;
                        GameBoy.Cpu.rHL = UpdateFlagsADD(GameBoy.Cpu.rHL, GameBoy.Cpu.rHL);
                        return instructionAdress;
                    }
                case 0x39:
                    {
                        ++instructionAdress;
                        GameBoy.Cpu.rHL = UpdateFlagsADD(GameBoy.Cpu.rHL, GameBoy.Cpu.SP);
                        return instructionAdress;
                    }
                case 0xE8:  //add SP,n
                    {
                        ++instructionAdress;
                        sbyte b = GameBoy.Ram.ReadSignedByteAt(instructionAdress);
                        ++instructionAdress;
                        GameBoy.Cpu.SP = UpdateFlagsSP(GameBoy.Cpu.SP, b);
                        return instructionAdress;
                    }
                case 0x03: //INC
                    {
                        ++instructionAdress;
                        GameBoy.Cpu.rBC += 0x01;
                        return instructionAdress;
                    }
                case 0x13:
                    {
                        ++instructionAdress;
                        GameBoy.Cpu.rDE += 0x01;
                        return instructionAdress;
                    }
                case 0x23:
                    {
                        ++instructionAdress;
                        GameBoy.Cpu.rHL += 0x01;
                        return instructionAdress;
                    }
                case 0x33:
                    {
                        ++instructionAdress;
                        GameBoy.Cpu.SP += 0x01;
                        return instructionAdress;
                    }
                case 0x0B:  //DEC
                    {
                        ++instructionAdress;
                        GameBoy.Cpu.rBC -= 0x01;
                        return instructionAdress;
                    }
                case 0x1B:
                    {
                        ++instructionAdress;
                        GameBoy.Cpu.rDE -= 0x01;
                        return instructionAdress;
                    }
                case 0x2B:
                    {
                        ++instructionAdress;
                        GameBoy.Cpu.rHL -= 0x01;
                        return instructionAdress;
                    }
                case 0x3B:
                    {
                        ++instructionAdress;
                        GameBoy.Cpu.SP -= 0x01;
                        return instructionAdress;
                    }
                default:
                    {
                        return ++instructionAdress;
                    }
            }
        }

        public override string ToString(ushort InstructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(InstructionAdress);
            switch (opcode)
            {
                case 0x09:
                    {
                        return "add, HL,BC";
                    }
                case 0x19:
                    {
                        return "add, HL,DE";
                    }
                case 0x29:
                    {
                        return "add, HL,HL";
                    }
                case 0x39:
                    {
                        return "add, HL,SP";
                    }
                case 0xE8:
                    {
                        ushort i = InstructionAdress;
                        i++;
                        sbyte val = GameBoy.Ram.ReadSignedByteAt(i);
                        return "add, SP, " + String.Format("{0:x2}", val);
                    }
                case 0x03:
                    {
                        return "inc, BC";
                    }
                case 0x13:
                    {
                        return "inc, DE";
                    }
                case 0x23:
                    {
                        return "inc, HL";
                    }
                case 0x33:
                    {
                        return "inc, SP";
                    }
                case 0x0B:
                    {
                        return "dec, BC";
                    }
                case 0x1B:
                    {
                        return "dec, DE";
                    }
                case 0x2B:
                    {
                        return "dec, HL";
                    }
                case 0x3B:
                    {
                        return "dec, SP";
                    }
                default:
                    {
                        return "add error";
                    }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public static ushort UpdateFlagsSP(ushort sp, sbyte b)
        {
            bool isBNeg = (b & 0x80) != 0;

            byte bValue = (byte)(b);

            // 8bit op from ALU_8bit UpdateFlagsADD
            byte r1 = (byte)(sp & 0xFF);
            byte r2 = (byte)bValue;
            ushort res = (ushort)(r1 + r2);
            byte halfRes = (byte)((byte)(r1 & 0xF) + (byte)(r2 & 0xF));

            GameBoy.Cpu.HValue = (halfRes > 0xF);
            GameBoy.Cpu.CValue = (res > 0xFF);
            GameBoy.Cpu.ZValue = false;
            GameBoy.Cpu.NValue = false;
            if (b < 0)
            {
                //GameBoy.Cpu.HValue = !GameBoy.Cpu.HValue;
                //GameBoy.Cpu.CValue = !GameBoy.Cpu.CValue;
            }
            // actual computation
            if (isBNeg)
            {
                // manipulating two's complement here
                bValue = (byte)(0x7F & (~b));
                bValue += 1;
                sp -= bValue;
            }
            else
            {
                sp += bValue;
            }

            return sp;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private ushort UpdateFlagsADD(ushort r1, ushort r2)
        {
            long r = (long)r1 + (long)r2;
            ushort hResult = (ushort) ((r1 & 0xFFF) + (r2 & 0xFFF));
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = (hResult > 0xFFF);
            GameBoy.Cpu.CValue = r>0xFFFF;

            return (ushort)(r & 0xFFFF);
        }
    }
}
