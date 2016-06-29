using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.ROTATE_SHIFT
{
    class Z80Instruction_ROTATE : Z80Instruction
    {
        public Z80Instruction_ROTATE()
        {
            m_Name = "RL n";
            m_Summary = "";
            m_Flags = "Z 0 0 C";
            m_OpCode = new byte[] { 0x07,                                               //RLCA
                                    0x17,                                               //RLA
                                    0x0F,                                               //RRCA
                                    0x1F,                                               //RRA
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
            return 4;
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
                case 0x17:
                case 0x0F:
                case 0x1F:
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
                        GameBoy.Cpu.ZValue = false;
                        return ++instructionAdress;
                    }
                case 0x17:
                    {
                        GameBoy.Cpu.rA = RotateOperations.RotateLeftThroughCarry(GameBoy.Cpu.rA);
                        GameBoy.Cpu.ZValue = false;
                        return ++instructionAdress;
                    }
                case 0x0F:
                    {
                        GameBoy.Cpu.rA = RotateOperations.RotateRight(GameBoy.Cpu.rA);
                        GameBoy.Cpu.ZValue = false;
                        return ++instructionAdress;
                    }
                case 0x1F:
                    {
                        GameBoy.Cpu.rA = RotateOperations.RotateRightThroughCarry(GameBoy.Cpu.rA);
                        GameBoy.Cpu.ZValue = false;
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
        public override String ToString(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0x07:
                    {
                        return "rlca";
                    }
                case 0x17:
                    {
                        return "rla";
                    }
                case 0x0F:
                    {
                        return "rrca";
                    }
                case 0x1F:
                    {
                        return "rra";
                    }
                default:
                    {
                        return "rotate error";
                    }
            }
        }
    }
}
