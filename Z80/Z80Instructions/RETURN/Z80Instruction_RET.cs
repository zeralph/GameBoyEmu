using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.RETURN
{
    class Z80Instruction_RET : Z80Instruction
    {
        private bool m_branchTaken = false;

        public Z80Instruction_RET()
        {
            m_Name = "RET";
            m_Summary = "";
            m_Flags = "- - - -";
            m_OpCode = new byte[] { 0xC9, 
                                    0xC0, 0xC8, 0xD0, 0xD8,
                                    0xD9};

            m_NbCycles = 4;
            m_NbCyclesMax = 4;
            m_Lenght = 1;
            m_branchTaken = false;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override byte GetCurNbCycles(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0xC9:
                case 0xD9:
                default:
                    {
                        return 16;
                    }
                case 0xC8:
                case 0xD8:
                case 0xC0:
                case 0xD0:
                    {
                        if( m_branchTaken )
                        {
                            return 20;
                        }
                        else
                        {
                            return 8;
                        }
                    }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override bool IsRET()
        {
            return true;
        }

        public override byte GetLenght(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0xC9:
                case 0xC0:
                case 0xD0:
                case 0xC8:
                case 0xD8:
                case 0xD9:
                default:
                    {
                        return 0x01;
                    }
            }
        }

        public override ushort Exec(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0xC9:
                    {
                        instructionAdress += 0x01;
                        ushort adr = GameBoy.Ram.ReadUshortAt(GameBoy.Cpu.SP);
                        GameBoy.Cpu.SP += 0x02;
                        return adr;
                    }
                case 0xC0:
                    {
                        instructionAdress += 0x01;
                        ushort adr = GameBoy.Ram.ReadUshortAt(GameBoy.Cpu.SP);
                        if (!GameBoy.Cpu.ZValue)
                        {
                            m_branchTaken = true;
                            GameBoy.Cpu.SP += 0x02;
                            return adr;
                        }
                        else
                        {
                            m_branchTaken = false;
                            return instructionAdress;
                        }
                    }
                case 0xC8:
                    {
                        instructionAdress += 0x01;
                        ushort adr = GameBoy.Ram.ReadUshortAt(GameBoy.Cpu.SP);
                        if (GameBoy.Cpu.ZValue)
                        {
                            m_branchTaken = true;
                            GameBoy.Cpu.SP += 0x02;
                            return adr;
                        }
                        else
                        {
                            m_branchTaken = false;
                            return instructionAdress;
                        }
                    }
                case 0xD0:
                    {
                        instructionAdress += 0x01;
                        ushort adr = GameBoy.Ram.ReadUshortAt(GameBoy.Cpu.SP);
                        if (!GameBoy.Cpu.CValue)
                        {
                            m_branchTaken = true;
                            GameBoy.Cpu.SP += 0x02;
                            return adr;
                        }
                        else
                        {
                            m_branchTaken = false;
                            return instructionAdress;
                        }
                    }
                case 0xD8:
                    {
                        instructionAdress += 0x01;
                        ushort adr = GameBoy.Ram.ReadUshortAt(GameBoy.Cpu.SP);
                        if (GameBoy.Cpu.CValue)
                        {
                            m_branchTaken = true;
                            GameBoy.Cpu.SP += 0x02;
                            return adr;
                        }
                        else
                        {
                            m_branchTaken = false;
                            return instructionAdress;
                        }
                    }
                case 0xD9:
                    {
                        instructionAdress += 0x01;
                        ushort adr = GameBoy.Ram.ReadUshortAt(GameBoy.Cpu.SP);
                        GameBoy.Cpu.SP += 0x02;
                        //Enable all interrupts;
                        GameBoy.Cpu.EnableInterrupts();
                        return adr;
                    }
                default:
                    {
                        return 0x01;
                    }
            }
        }

        public override string ToString(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0xC9:
                    {
                        return "ret";
                    }
                case 0xC0:
                    {
                        return "ret nz";
                    }
                case 0xC8:
                    {
                        return "ret z";
                    }
                case 0xD0:
                    {
                        return "ret nc";
                    }
                case 0xD8:
                    {
                        return "ret z";
                    }
                case 0xD9:
                    {
                        return "reti";
                    }
                default:
                    {
                        return "ret error";
                    }
            }
        }
    }
}
