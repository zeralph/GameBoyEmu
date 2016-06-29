using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.RESTART
{
    class Z80Instruction_RST : Z80Instruction
    {
        public Z80Instruction_RST()
        {
            m_Name = "RST";
            m_Summary = "";
            m_Flags = "- - - -";
            m_OpCode = new byte[] { 0xC7, 0xCF, 0xD7, 0xDF, 0xE7, 0xEF, 0XF7, 0xFF};

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
                case 0xC7:
                case 0xCF:
                case 0xD7:
                case 0xDF:
                case 0xE7:
                case 0xEF:
                case 0xF7:
                case 0xFF:
                default:
                    {
                        return 16;
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
                case 0xC7:
                case 0xCF:
                case 0xD7:
                case 0xDF:
                case 0xE7:
                case 0xEF:
                case 0xF7:
                case 0xFF:
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
                case 0xC7:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.SP -= 0x02;
                        GameBoy.Ram.WriteUshortAt( GameBoy.Cpu.SP, instructionAdress);
                        instructionAdress = 0x00;
                        return instructionAdress;
                    }
                case 0xCF:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.SP -= 0x02;
                        GameBoy.Ram.WriteUshortAt(GameBoy.Cpu.SP, instructionAdress);
                        instructionAdress = 0x08;
                        return instructionAdress;
                    }
                case 0xD7:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.SP -= 0x02;
                        GameBoy.Ram.WriteUshortAt(GameBoy.Cpu.SP, instructionAdress);
                        instructionAdress = 0x10;
                        return instructionAdress;
                    }
                case 0xDF:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.SP -= 0x02;
                        GameBoy.Ram.WriteUshortAt(GameBoy.Cpu.SP, instructionAdress);
                        instructionAdress = 0x18;
                        return instructionAdress;
                    }
                case 0xE7:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.SP -= 0x02;
                        GameBoy.Ram.WriteUshortAt(GameBoy.Cpu.SP, instructionAdress);
                        instructionAdress = 0x20;
                        return instructionAdress;
                    }
                case 0xEF:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.SP -= 0x02;
                        GameBoy.Ram.WriteUshortAt(GameBoy.Cpu.SP, instructionAdress);
                        instructionAdress = 0x28;
                        return instructionAdress;
                    }
                case 0xF7:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.SP -= 0x02;
                        GameBoy.Ram.WriteUshortAt(GameBoy.Cpu.SP, instructionAdress);
                        instructionAdress = 0x30;
                        return instructionAdress;
                    }
                case 0xFF:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.SP -= 0x02;
                        GameBoy.Ram.WriteUshortAt(GameBoy.Cpu.SP, instructionAdress);
                        instructionAdress = 0x38;
                        return instructionAdress;
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
        public override String ToString(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0xC7:
                    {
                        return "rst 00";
                    }
                case 0xCF:
                    {
                        return "rst 08";
                    }
                case 0xD7:
                    {
                        return "rst 10";
                    }
                case 0xDF:
                    {
                        return "rst 18";
                    }
                case 0xE7:
                    {
                        return "rst 20";
                    }
                case 0xEF:
                    {
                        return "rst 28";
                    }
                case 0xF7:
                    {
                        return "rst 30";
                    }
                case 0xFF:
                    {
                        return "rst 38";
                    }
                default:
                    {
                        return "rst error";
                    }
            }
        }
    }
}
