using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.BIT
{
    class Z80Instruction_BIT_b_r : Z80Instruction
    {
        public Z80Instruction_BIT_b_r()
        {
            m_IsBCInstruction = true;
            m_Name = "BIT b,r";
            m_Summary = "";
            m_Flags = "Z 0 1 -";
            m_OpCode = new byte[] { 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47,   // bit 0,x
                                    0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F,   // bit 1,x
                                    0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57,   // bit 2,x
                                    0x58, 0x59, 0x5A, 0x5B, 0x5C, 0x5D, 0x5E, 0x5F,   // bit 3,x
                                    0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67,   // bit 4,x
                                    0x68, 0x69, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E, 0x6F,   // bit 5,x
                                    0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77,   // bit 6,x
                                    0x78, 0x79, 0x7A, 0x7B, 0x7C, 0x7D, 0x7E, 0x7F,   // bit 7,x
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
            switch(opcode)
            {
                case 0x46 :
                case 0x4E:
                case 0x56:
                case 0x5E:
                case 0x66:
                case 0x6E:
                case 0x76:
                case 0x7E:
                    {
                        return 12;
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

            byte register = BitGetRegister(opcode);
            byte value = BitGetIndex(opcode);

            byte mask = (byte)(0x01 << value);
            GameBoy.Cpu.ZValue = (register & mask) == 0x00;
            if (GameBoy.Cpu.ZValue )
            {
                //GameBoy.Cpu.Stop();
            }
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = true;
            //if(opcode == 0x41)
              //  GameBoy.Cpu.Stop();
            return ++instructionAdress ;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override String ToString(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            String register = BitGetRegisterStr(opcode);

            byte value = BitGetIndex(opcode);
            return "bit " + value + "," + register;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private byte BitGetIndex(byte opcode)
        {
            byte b = (byte)( ( opcode & 0x38 ) >> 3);
            switch( b )
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
        private byte BitGetRegister(byte opcode)
        {
            byte b = (byte)( opcode & 0x07 );
            switch (b)
            {
                case 0x00: return GameBoy.Cpu.rB;
                case 0x01: return GameBoy.Cpu.rC;
                case 0x02: return GameBoy.Cpu.rD;
                case 0x03: return GameBoy.Cpu.rE;
                case 0x04: return GameBoy.Cpu.rH;
                case 0x05: return GameBoy.Cpu.rL;
                case 0x06: return GameBoy.Ram.ReadByteAt( GameBoy.Cpu.rHL );
                case 0x07: return GameBoy.Cpu.rA;
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
