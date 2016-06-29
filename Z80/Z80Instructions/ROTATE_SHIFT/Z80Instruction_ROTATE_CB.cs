using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.ROTATE_SHIFT
{
    class Z80Instruction_ROTATE_CB : Z80Instruction
    {
        public Z80Instruction_ROTATE_CB()
        {
            m_IsBCInstruction = true;
            m_Name = "RL n";
            m_Summary = "";
            m_Flags = "Z 0 0 C";
            m_OpCode = new byte[] { 0x07, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06,     //RLC
                                    0x17, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16,     //RL
                                    0x0F, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E,     //RRC
                                    0x1F, 0x18, 0x19, 0x1A, 0x1B, 0x1C,0x1D, 0x1E,      //RR
                                    0x27, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26,     //SLA
                                    0x2F, 0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E,     //SRA
                                    0x3F, 0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E,     //SRL
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
                case 0x07:
                case 0x00:
                case 0x01:
                case 0x02:
                case 0x03:
                case 0x04:
                case 0x05:
                case 0x17:
                case 0x10:
                case 0x11:
                case 0x12:
                case 0x13:
                case 0x14:
                case 0x15:
                case 0x0F:
                case 0x08:
                case 0x09:
                case 0x0A:
                case 0x0B:
                case 0x0C:
                case 0x0D:
                case 0x1F:
                case 0x18:
                case 0x19:
                case 0x1A:
                case 0x1B:
                case 0x1C:
                case 0x1D:
                case 0x27:
                case 0x20:
                case 0x21:
                case 0x22:
                case 0x23:
                case 0x24:
                case 0x25:
                case 0x2F:
                case 0x28:
                case 0x29:
                case 0x2A:
                case 0x2B:
                case 0x2C:
                case 0x2D:
                case 0x3F:
                case 0x38:
                case 0x39:
                case 0x3A:
                case 0x3B:
                case 0x3C:
                case 0x3D:
                
                    {
                        return 8;
                    }
                case 0x06:
                case 0x16:
                case 0x26:
                case 0x0E:
                case 0x2E:
                case 0x1E:
                case 0x3E:
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
                case 0x07:
                case 0x00:
                case 0x01:
                case 0x02:
                case 0x03:
                case 0x04:
                case 0x05:
                case 0x06:
                case 0x17:
                case 0x10:
                case 0x11:
                case 0x12:
                case 0x13:
                case 0x14:
                case 0x15:
                case 0x16:
                case 0x0F:
                case 0x08:
                case 0x09:
                case 0x0A:
                case 0x0B:
                case 0x0C:
                case 0x0D:
                case 0x0E:
                case 0x1F:
                case 0x18:
                case 0x19:
                case 0x1A:
                case 0x1B:
                case 0x1C:
                case 0x1D:
                case 0x1E:
                case 0x27:
                case 0x20:
                case 0x21:
                case 0x22:
                case 0x23:
                case 0x24:
                case 0x25:
                case 0x26:
                case 0x2F:
                case 0x28:
                case 0x29:
                case 0x2A:
                case 0x2B:
                case 0x2C:
                case 0x2D:
                case 0x2E:
                case 0x3F:
                case 0x38:
                case 0x39:
                case 0x3A:
                case 0x3B:
                case 0x3C:
                case 0x3D:
                case 0x3E:
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
                case 0x07:
                {
                    GameBoy.Cpu.rA = RotateOperations.RotateLeft(GameBoy.Cpu.rA);
                    return ++instructionAdress;
                }
                 case 0x00:
                {
                    GameBoy.Cpu.rB = RotateOperations.RotateLeft(GameBoy.Cpu.rB);
                    return ++instructionAdress;
                }
                 case 0x01:
                {
                    GameBoy.Cpu.rC = RotateOperations.RotateLeft(GameBoy.Cpu.rC);
                    return ++instructionAdress;
                }
                 case 0x02:
                {
                    GameBoy.Cpu.rD = RotateOperations.RotateLeft(GameBoy.Cpu.rD);
                    return ++instructionAdress;
                }
                 case 0x03:
                {
                    GameBoy.Cpu.rE = RotateOperations.RotateLeft(GameBoy.Cpu.rE);
                    return ++instructionAdress;
                }
                 case 0x04:
                {
                    GameBoy.Cpu.rH = RotateOperations.RotateLeft(GameBoy.Cpu.rH);
                    return ++instructionAdress;
                }
                 case 0x05:
                {
                    GameBoy.Cpu.rL = RotateOperations.RotateLeft(GameBoy.Cpu.rL);
                    return ++instructionAdress;
                }
                 case 0x06:
                {
                    byte b = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                    b = RotateOperations.RotateLeft(b);
                    GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, b);
                    return ++instructionAdress;
                }
//                                                                                                                      
                case 0x17:
                {
                    GameBoy.Cpu.rA = RotateOperations.RotateLeftThroughCarry(GameBoy.Cpu.rA);
                    return ++instructionAdress;
                }
                 case 0x10:
                {
                    GameBoy.Cpu.rB = RotateOperations.RotateLeftThroughCarry(GameBoy.Cpu.rB);
                    return ++instructionAdress;
                }
                 case 0x11:
                {
                    GameBoy.Cpu.rC = RotateOperations.RotateLeftThroughCarry(GameBoy.Cpu.rC);
                    return ++instructionAdress;
                }
                 case 0x12:
                {
                    GameBoy.Cpu.rD = RotateOperations.RotateLeftThroughCarry(GameBoy.Cpu.rD);
                    return ++instructionAdress;
                }
                 case 0x13:
                {
                    GameBoy.Cpu.rE = RotateOperations.RotateLeftThroughCarry(GameBoy.Cpu.rE);
                    return ++instructionAdress;
                }
                 case 0x14:
                {
                    GameBoy.Cpu.rH = RotateOperations.RotateLeftThroughCarry(GameBoy.Cpu.rH);
                    return ++instructionAdress;
                }
                 case 0x15:
                {
                    GameBoy.Cpu.rL = RotateOperations.RotateLeftThroughCarry(GameBoy.Cpu.rL);
                    return ++instructionAdress;
                }
                 case 0x16:
                {
                    byte b = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                    b = RotateOperations.RotateLeftThroughCarry(b);
                    GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, b);
                    return ++instructionAdress;
                }
//                                                                                                                      
                case 0x0F:
                {
                    GameBoy.Cpu.rA = RotateOperations.RotateRight(GameBoy.Cpu.rA);
                    return ++instructionAdress;
                }
                 case 0x08:
                {
                    GameBoy.Cpu.rB = RotateOperations.RotateRight(GameBoy.Cpu.rB);
                    return ++instructionAdress;
                }
                 case 0x09:
                {
                    GameBoy.Cpu.rC = RotateOperations.RotateRight(GameBoy.Cpu.rC);
                    return ++instructionAdress;
                }
                 case 0x0A:
                {
                    GameBoy.Cpu.rD = RotateOperations.RotateRight(GameBoy.Cpu.rD);
                    return ++instructionAdress;
                }
                 case 0x0B:
                {
                    GameBoy.Cpu.rE = RotateOperations.RotateRight(GameBoy.Cpu.rE);
                    return ++instructionAdress;
                }
                 case 0x0C:
                {
                    GameBoy.Cpu.rH = RotateOperations.RotateRight(GameBoy.Cpu.rH);
                    return ++instructionAdress;
                }
                 case 0x0D:
                {
                    GameBoy.Cpu.rL = RotateOperations.RotateRight(GameBoy.Cpu.rL);
                    return ++instructionAdress;
                }
                 case 0x0E:
                {
                    byte b = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                    b = RotateOperations.RotateRight(b);
                    GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, b);
                    return ++instructionAdress;
                }
//                                                                                                                      
                 case 0x1F:
                {
                    GameBoy.Cpu.rA = RotateOperations.RotateRightThroughCarry(GameBoy.Cpu.rA);
                    return ++instructionAdress;
                }
                 case 0x18:
                {
                    GameBoy.Cpu.rB = RotateOperations.RotateRightThroughCarry(GameBoy.Cpu.rB);
                    return ++instructionAdress;
                }
                 case 0x19:
                {
                    GameBoy.Cpu.rC = RotateOperations.RotateRightThroughCarry(GameBoy.Cpu.rC);
                    return ++instructionAdress;
                }
                 case 0x1A:
                {
                    GameBoy.Cpu.rD = RotateOperations.RotateRightThroughCarry(GameBoy.Cpu.rD);
                    return ++instructionAdress;
                }
                 case 0x1B:
                {
                    GameBoy.Cpu.rE = RotateOperations.RotateRightThroughCarry(GameBoy.Cpu.rE);
                    return ++instructionAdress;
                }
                 case 0x1C:
                {
                    GameBoy.Cpu.rH = RotateOperations.RotateRightThroughCarry(GameBoy.Cpu.rH);
                    return ++instructionAdress;
                }
                 case 0x1D:
                {
                    GameBoy.Cpu.rL = RotateOperations.RotateRightThroughCarry(GameBoy.Cpu.rL);
                    return ++instructionAdress;
                }
                 case 0x1E:
                {
                    byte b = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                    b = RotateOperations.RotateRightThroughCarry(b);
                    GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, b);
                    return ++instructionAdress;
                }
//                                                                                                                      
                 case 0x27:
                {
                    GameBoy.Cpu.rA = RotateOperations.ShiftLeft(GameBoy.Cpu.rA);
                    return ++instructionAdress;
                }
                 case 0x20:
                {
                    GameBoy.Cpu.rB = RotateOperations.ShiftLeft(GameBoy.Cpu.rB);
                    return ++instructionAdress;
                }
                 case 0x21:
                {
                    GameBoy.Cpu.rC = RotateOperations.ShiftLeft(GameBoy.Cpu.rC);
                    return ++instructionAdress;
                }
                 case 0x22:
                {
                    GameBoy.Cpu.rD = RotateOperations.ShiftLeft(GameBoy.Cpu.rD);
                    return ++instructionAdress;
                }
                 case 0x23:
                {
                    GameBoy.Cpu.rE = RotateOperations.ShiftLeft(GameBoy.Cpu.rE);
                    return ++instructionAdress;
                }
                 case 0x24:
                {
                    GameBoy.Cpu.rH = RotateOperations.ShiftLeft(GameBoy.Cpu.rH);
                    return ++instructionAdress;
                }
                 case 0x25:
                {
                    GameBoy.Cpu.rL = RotateOperations.ShiftLeft(GameBoy.Cpu.rL);
                    return ++instructionAdress;
                }
                 case 0x26:
                {
                    byte b = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                    b = RotateOperations.ShiftLeft(b);
                    GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, b);
                    return ++instructionAdress;
                }
//                                                                                                                      
                case 0x2F:
                {
                    GameBoy.Cpu.rA = RotateOperations.ShiftRightSetMSB(GameBoy.Cpu.rA);
                    return ++instructionAdress;
                }
                 case 0x28:
                {
                    GameBoy.Cpu.rB = RotateOperations.ShiftRightSetMSB(GameBoy.Cpu.rB);
                    return ++instructionAdress;
                }
                 case 0x29:
                {
                    GameBoy.Cpu.rC = RotateOperations.ShiftRightSetMSB(GameBoy.Cpu.rC);
                    return ++instructionAdress;
                }
                 case 0x2A:
                {
                    GameBoy.Cpu.rD = RotateOperations.ShiftRightSetMSB(GameBoy.Cpu.rD);
                    return ++instructionAdress;
                }
                 case 0x2B:
                {
                    GameBoy.Cpu.rE = RotateOperations.ShiftRightSetMSB(GameBoy.Cpu.rE);
                    return ++instructionAdress;
                }
                 case 0x2C:
                {
                    GameBoy.Cpu.rH = RotateOperations.ShiftRightSetMSB(GameBoy.Cpu.rH);
                    return ++instructionAdress;
                }
                 case 0x2D:
                {
                    GameBoy.Cpu.rL = RotateOperations.ShiftRightSetMSB(GameBoy.Cpu.rL);
                    return ++instructionAdress;
                }
                 case 0x2E:
                {
                    byte b = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                    b = RotateOperations.ShiftRightSetMSB(b);
                    GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, b);
                    return ++instructionAdress;
                }
//                                                                                                                      
                 case 0x3F:
                {
                    GameBoy.Cpu.rA = RotateOperations.ShiftRight(GameBoy.Cpu.rA);
                    return ++instructionAdress;
                }
                 case 0x38:
                {
                    GameBoy.Cpu.rB = RotateOperations.ShiftRight(GameBoy.Cpu.rB);
                    return ++instructionAdress;
                }
                 case 0x39:
                {
                    GameBoy.Cpu.rC = RotateOperations.ShiftRight(GameBoy.Cpu.rC);
                    return ++instructionAdress;
                }
                 case 0x3A:
                {
                    GameBoy.Cpu.rD = RotateOperations.ShiftRight(GameBoy.Cpu.rD);
                    return ++instructionAdress;
                }
                 case 0x3B:
                {
                    GameBoy.Cpu.rE = RotateOperations.ShiftRight(GameBoy.Cpu.rE);
                    return ++instructionAdress;
                }
                 case 0x3C:
                {
                    GameBoy.Cpu.rH = RotateOperations.ShiftRight(GameBoy.Cpu.rH);
                    return ++instructionAdress;
                }
                 case 0x3D:
                {
                    GameBoy.Cpu.rL = RotateOperations.ShiftRight(GameBoy.Cpu.rL);
                    return ++instructionAdress;
                }
                 case 0x3E:
                {
                    byte b = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                    b = RotateOperations.ShiftRight(b);
                    GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, b);
                    return ++instructionAdress;
                }
//                                                                                                                      
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
                case 0x07:
                    {
                        return "rlc a";
                    }
                case 0x00:
                    {
                        return "rlc b";
                    }
                case 0x01:
                    {
                        return "rlc c";
                    }
                case 0x02:
                    {
                        return "rlc d";
                    }
                case 0x03:
                    {
                        return "rlc e";
                    }
                case 0x04:
                    {
                        return "rlc h";
                    }
                case 0x05:
                    {
                        return "rlc l";
                    }
                case 0x06:
                    {
                        return "rlc (hl)";
                    }
                case 0x17:
                    {
                        return "rl a";
                    }
                case 0x10:
                    {
                        return "rl b";
                    }
                case 0x11:
                    {
                        return "rl c";
                    }
                case 0x12:
                    {
                        return "rld ";
                    }
                case 0x13:
                    {
                        return "rl e";
                    }
                case 0x14:
                    {
                        return "rl h";
                    }
                case 0x15:
                    {
                        return "rl l";
                    }
                case 0x16:
                    {
                        return "rl (hl)";
                    }
                case 0x0F:
                    {
                        return "rrc a";
                    }
                case 0x08:
                    {
                        return "rrc b";
                    }
                case 0x09:
                    {
                        return "rrc c";
                    }
                case 0x0A:
                    {
                        return "rrc d";
                    }
                case 0x0B:
                    {
                        return "rrc e";
                    }
                case 0x0C:
                    {
                        return "rrc h";
                    }
                case 0x0D:
                    {
                        return "rrc l";
                    }
                case 0x0E:
                    {
                        return "rrc (hl)";
                    }
                case 0x1F:
                    {
                        return "rr a";
                    }
                case 0x18:
                    {
                        return "rr b";
                    }
                case 0x19:
                    {
                        return "rr c";
                    }
                case 0x1A:
                    {
                        return "rr d";
                    }
                case 0x1B:
                    {
                        return "rr e";
                    }
                case 0x1C:
                    {
                        return "rr h";
                    }
                case 0x1D:
                    {
                        return "rr l";
                    }
                case 0x1E:
                    {
                        return "rr (hl)";
                    }
                case 0x27:
                    {
                        return "sla a";
                    }
                case 0x20:
                    {
                        return "sla b";
                    }
                case 0x21:
                    {
                        return "sla c";
                    }
                case 0x22:
                    {
                        return "sla d";
                    }
                case 0x23:
                    {
                        return "sla e";
                    }
                case 0x24:
                    {
                        return "sla h";
                    }
                case 0x25:
                    {
                        return "sla l";
                    }
                case 0x26:
                    {
                        return "sla (hl)";
                    }
                case 0x2F:
                    {
                        return "sra a";
                    }
                case 0x28:
                    {
                        return "sra b";
                    }
                case 0x29:
                    {
                        return "sra c";
                    }
                case 0x2A:
                    {
                        return "sra d";
                    }
                case 0x2B:
                    {
                        return "sra e";
                    }
                case 0x2C:
                    {
                        return "sra h";
                    }
                case 0x2D:
                    {
                        return "sra l";
                    }
                case 0x2E:
                    {
                        return "sra (hl)";
                    }
                case 0x3F:
                    {
                        return "srl a";
                    }
                case 0x38:
                    {
                        return "srl b";
                    }
                case 0x39:
                    {
                        return "srl c";
                    }
                case 0x3A:
                    {
                        return "srl d";
                    }
                case 0x3B:
                    {
                        return "srl e";
                    }
                case 0x3C:
                    {
                        return "srl h";
                    }
                case 0x3D:
                    {
                        return "srl l";
                    }
                case 0x3E:
                    {
                        return "srl (hl)";
                    }
                default:
                    {
                        return "rotate CB error";
                    }
            }
        }
    }
}
