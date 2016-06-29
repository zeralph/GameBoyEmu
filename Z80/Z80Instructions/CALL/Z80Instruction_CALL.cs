using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.CALL
{
    class Z80Instruction_CALL : Z80Instruction
    {
        private bool m_branchTaken = false;

        public Z80Instruction_CALL()
        {
            m_Name = "CALL";
            m_Summary = "";
            m_Flags = "- - - -";
            m_OpCode = new byte[] { 0xCD, 
                                    0xC4, 0xCC, 0xD4, 0xDC};

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
                case 0xC4:
                case 0xCC:
                case 0xD4:
                case 0xDC:
                    {
                        if (m_branchTaken)
                        {
                            return 24;
                        }
                        else
                        {
                            return 12;
                        }
                    }
                case 0xCD:
                default:
                    {
                        return 24;
                    }
            }
        }

        public override byte GetLenght(ushort instructionAdress)
        {
            return 0x03;
        }

        public override ushort Exec(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0xCD:
                    {
                        return DoCall(instructionAdress);
                    }
                case 0xC4:
                    {
                        if (!GameBoy.Cpu.ZValue)
                        {
                            m_branchTaken = true;
                            return DoCall(instructionAdress);
                        }
                        else
                        {
                            m_branchTaken = false;
                            return (ushort)(instructionAdress + 0x03);
                        }
                    }
                case 0xCC:
                    {
                        if (GameBoy.Cpu.ZValue)
                        {
                            m_branchTaken = true;
                            return DoCall(instructionAdress);
                        }
                        else
                        {
                            m_branchTaken = false;
                            return (ushort)(instructionAdress + 0x03);
                        }
                    }
                case 0xD4:
                    {
                        if (!GameBoy.Cpu.CValue)
                        {
                            m_branchTaken = true;
                            return DoCall(instructionAdress);
                        }
                        else
                        {
                            m_branchTaken = false;
                            return (ushort)(instructionAdress + 0x03);
                        }
                    }
                case 0xDC:
                    {
                        if (GameBoy.Cpu.CValue)
                        {
                            m_branchTaken = true;
                            return DoCall(instructionAdress);
                        }
                        else
                        {
                            m_branchTaken = false;
                            return (ushort)(instructionAdress + 0x03);
                        }
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
                case 0xCD:
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort val = GameBoy.Ram.ReadUshortAt(i);
                        return "call " + String.Format("{0:x4}", val);
                    }
                case 0xC4:
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort val = GameBoy.Ram.ReadUshortAt(i); 
                        return "call nz" + String.Format("{0:x4}", val);
                    }
                case 0xCC:
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort val = GameBoy.Ram.ReadUshortAt(i); 
                        return "call n," + String.Format("{0:x4}", val);
                    }
                case 0xD4:
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort val = GameBoy.Ram.ReadUshortAt(i); 
                        return "call nc" + String.Format("{0:x4}", val);
                    }
                case 0xDC:
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort val = GameBoy.Ram.ReadUshortAt(i); 
                        return "call c" + String.Format("{0:x4}", val);
                    }
                default:
                    {
                        return "call error";
                    }
            }
        }

        private ushort DoCall(ushort instructionAdress)
        {
            instructionAdress += 0x01;
            ushort adr = GameBoy.Ram.ReadUshortAt(instructionAdress);
            instructionAdress += 0x02;
            GameBoy.Cpu.SP -= 0x02;
            GameBoy.Ram.WriteUshortAt(GameBoy.Cpu.SP, instructionAdress);
            return adr;
        }
    }
}
