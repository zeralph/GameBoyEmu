using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.JUMP
{
    class Z80Instruction_JUMP : Z80Instruction
    {
        private bool m_branchTaken = false;

        public Z80Instruction_JUMP()
        {
            m_Name = "JUMP";
            m_Summary = "";
            m_Flags = "- - - -";
            m_OpCode = new byte[] { 0xC3,                   //direct jump
                                    0xC2, 0xCA, 0xD2, 0xDA, //conditional direct jump
                                    0xE9,                   //jump to (HL)
                                    0x18,                   //relative jump
                                    0x20, 0x28, 0x30, 0x38};//conditionnal relative jump

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
                case 0xC3: //direct jump
                    {
                        return 16;
                    }
                case 0xC2: //conditional jump Z
                case 0xCA: //conditional jump !Z
                case 0xD2: //conditional jump C
                case 0xDA: //conditional jump !C
                    {
                        if( m_branchTaken )
                        {
                            return 16;
                        }
                        else
                        {
                            return 12;
                        }
                    }
                case 0x18: //add n to current adress and jump to it
                    {
                        return 12;
                    }
                case 0x20: //relative conditional jump !Z
                case 0x28: //relative conditional jump Z
                case 0x30: //relative conditional jump !C
                case 0x38: //relative conditional jump C
                    {
                        if (m_branchTaken)
                        {
                            return 12;
                        }
                        else
                        {
                            return 8;
                        }
                    }
                case 0xE9: //jump to address contained in HL
                    {
                        return 4;
                    }
                default:
                    {
                        return 1;
                    }
            }
        }

        public override byte GetLenght(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0xC3: //direct jump
                case 0xC2: //conditional jump Z
                case 0xCA: //conditional jump !Z
                case 0xD2: //conditional jump C
                case 0xDA: //conditional jump !C
                    {
                        return 0x03;
                    }
                case 0x18: //add n to current adress and jump to it
                    {
                        return 0x02;
                    }
                case 0x20: //relative conditional jump !Z
                case 0x28: //relative conditional jump Z
                case 0x30: //relative conditional jump !C
                case 0x38: //relative conditional jump C
                    {
                        return 0x02;
                    }
                case 0xE9: //jump to address contained in HL
                default:
                    {
                        return 0x01;
                    }
            }
        }

        public override ushort Exec( ushort instructionAdress )
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0xC3: //direct jump
                    {
                        instructionAdress += 0x01;
                        ushort adr = GameBoy.Ram.ReadUshortAt( instructionAdress );
                        return adr;
                    }
                case 0xC2: //conditional jump !Z
                    {
                        instructionAdress += 0x01;
                        if (!GameBoy.Cpu.ZValue)
                        {
                            m_branchTaken = true;
                            ushort adr = GameBoy.Ram.ReadUshortAt(instructionAdress);
                            return adr;
                        }
                        else
                        {
                            return (ushort)(instructionAdress + 0x02);
                        }
                    }
                case 0xCA: //conditional jump Z
                    {
                        instructionAdress += 0x01;
                        if (GameBoy.Cpu.ZValue)
                        {
                            m_branchTaken = true;
                            ushort adr = GameBoy.Ram.ReadUshortAt(instructionAdress);
                            return adr;
                        }
                        else
                        {
                            m_branchTaken = false;
                            return (ushort)(instructionAdress + 0x02);
                        }
                    }
                case 0xD2: //conditional jump !C
                    {
                        instructionAdress += 0x01;
                        if (!GameBoy.Cpu.CValue)
                        {
                            m_branchTaken = true;
                            ushort adr = GameBoy.Ram.ReadUshortAt(instructionAdress);
                            return adr;
                        }
                        else
                        {
                            m_branchTaken = false;
                            return (ushort)(instructionAdress + 0x02);
                        }
                    }
                case 0xDA: //conditional jump C
                    {
                        instructionAdress += 0x01;
                        if (GameBoy.Cpu.CValue)
                        {
                            m_branchTaken = true;
                            ushort adr = GameBoy.Ram.ReadUshortAt(instructionAdress);
                            return adr;
                        }
                        else
                        {
                            m_branchTaken = false;
                            return (ushort)(instructionAdress + 0x02);
                        }
                    }
                case 0xE9: //jump to address contained in HL
                        {
                            ushort adr = /*GameBoy.Ram.ReadUshortAt*/(GameBoy.Cpu.rHL);
                            return adr;
                        }
                case 0x18: //add n to current adress and jump to it
                        {
                            instructionAdress += 0x01;
                            sbyte sb = GameBoy.Ram.ReadSignedByteAt(instructionAdress);
                            instructionAdress = DoRelativeJump(instructionAdress, sb);
                            return instructionAdress;
                        }
                case 0x20: //relative conditional jump !Z
                    {
                        instructionAdress += 0x01;
                        if (!GameBoy.Cpu.ZValue)
                        {
                            m_branchTaken = true;
                            sbyte sb = GameBoy.Ram.ReadSignedByteAt(instructionAdress);
                            instructionAdress = DoRelativeJump(instructionAdress, sb);
                            return instructionAdress;
                        }
                        else
                        {
                            m_branchTaken = false;
                            return (ushort)(instructionAdress + 0x01);
                        }
                    }
                case 0x28: //relative conditional jump Z
                    {
                        instructionAdress += 0x01;
                        if (GameBoy.Cpu.ZValue)
                        {
                            m_branchTaken = true;
                            sbyte sb = GameBoy.Ram.ReadSignedByteAt(instructionAdress);
                            instructionAdress = DoRelativeJump(instructionAdress, sb);
                            return instructionAdress;
                        }
                        else
                        {
                            m_branchTaken = false;
                            return (ushort)(instructionAdress + 0x01);
                        }
                    }
                case 0x30: //relative conditional jump !C
                    {
                        instructionAdress += 0x01;
                        if (!GameBoy.Cpu.CValue)
                        {
                            m_branchTaken = true;
                            sbyte sb = GameBoy.Ram.ReadSignedByteAt(instructionAdress);
                            instructionAdress = DoRelativeJump(instructionAdress, sb);
                            return instructionAdress;
                        }
                        else
                        {
                            return (ushort)(instructionAdress + 0x01);
                        }
                    }
                case 0x38: //relative conditional jump C
                    {
                        instructionAdress += 0x01;
                        if (GameBoy.Cpu.CValue)
                        {
                            m_branchTaken = true;
                            sbyte sb = GameBoy.Ram.ReadSignedByteAt(instructionAdress);
                            instructionAdress = DoRelativeJump(instructionAdress, sb);
                            return instructionAdress;
                        }
                        else
                        {
                            m_branchTaken = false;
                            return (ushort)(instructionAdress + 0x01);
                        }
                    }
                default:
                    {
                        m_branchTaken = false;
                        return (ushort)(instructionAdress + 0x01);
                    }
            }
        }

        public override string ToString(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0xC3: //direct jump
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort adr = GameBoy.Ram.ReadUshortAt(i);
                        return "jp " + String.Format("{0:x4}", adr);
                    }
                case 0xC2: //conditional jump Z
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort adr = GameBoy.Ram.ReadUshortAt(i);
                        return "jpnz " + String.Format("{0:x4}", adr);
                    }
                case 0xCA: //conditional jump !Z
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort adr = GameBoy.Ram.ReadUshortAt(i);
                        return "jpz " + String.Format("{0:x4}", adr);
                    }
                case 0xD2: //conditional jump C
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort adr = GameBoy.Ram.ReadUshortAt(i);
                        return "jpc " + String.Format("{0:x4}", adr);
                    }
                case 0xDA: //conditional jump !C
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort adr = GameBoy.Ram.ReadUshortAt(i);
                        return "jpnc " + String.Format("{0:x4}", adr);
                    }
                case 0xE9: //jump to address contained in HL
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort adr = GameBoy.Ram.ReadByteAt(i);
                        return "jp HL";
                    }
                case 0x18: //add n to current adress and jump to it
                    {
                        ushort i = instructionAdress;
                        i++;
                        sbyte sb = GameBoy.Ram.ReadSignedByteAt(i);
                        return "jr " + String.Format("{0:x2}", DoRelativeJump(instructionAdress, sb));
                    }
                case 0x20: //relative conditional jump !Z
                    {
                        ushort i = instructionAdress;
                        i++;
                        sbyte sb = GameBoy.Ram.ReadSignedByteAt(i);
                        return "jr nz," + String.Format("{0:x2}", DoRelativeJump(i, sb));
                    }
                case 0x28: //relative conditional jump Z
                    {
                        ushort i = instructionAdress;
                        i++;
                        sbyte sb = GameBoy.Ram.ReadSignedByteAt(i);
                        return "jr z," + String.Format("{0:x2}", DoRelativeJump(i, sb));
                    }
                case 0x30: //relative conditional jump !C
                    {
                        ushort i = instructionAdress;
                        i++;
                        sbyte sb = GameBoy.Ram.ReadSignedByteAt(i);
                        return "jr nc," + String.Format("{0:x2}", DoRelativeJump(i, sb));
                    }
                case 0x38: //relative conditional jump C
                    {
                        ushort i = instructionAdress;
                        i++;
                        sbyte sb = GameBoy.Ram.ReadSignedByteAt(i);
                        return "jr c," + String.Format("{0:x2}", DoRelativeJump(i, sb));
                    }
                default:
                    {
                        return "jp";
                    }
            }
        }

        private ushort DoRelativeJump(ushort instructionAdress, sbyte offset)
        {
            return (ushort)(instructionAdress + 0x01 + (ushort)offset);
//             /*
//             ushort soffset = (ushort)Math.Abs(offset);
//             if (offset > 0)
//             {
//                 return (ushort)(instructionAdress /*+ 0x01 */+ soffset);
//             }
//             else
//             {
//                 return (ushort)(instructionAdress /*+ 0x01*/ - soffset);
//             }
//             */
        }
    }
}
