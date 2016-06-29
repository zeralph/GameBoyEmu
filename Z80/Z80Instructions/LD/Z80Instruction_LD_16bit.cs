//![DO NOT UPDATE]
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBoyTest.Debug;

namespace GameBoyTest.Z80.Z80Instructions.LD
{
    class Z80Instruction_LD_16bit : Z80Instruction
    {
        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public Z80Instruction_LD_16bit()
        {
            m_Name = "LD16";
            m_Summary = "";
            m_Flags = "- - - -";
            m_OpCode = new byte[] { 0x01, 0x11, 0x21, 0x31, //LD nn into BC, DE, HM, SP
                                    0xF9,                   //LD HL into SP
                                    0xF8,                   //put (SP+n) into HL
                                    0x08,                   //put SP at adress nn    
                                    0xF5, 0xC5, 0xD5, 0xE5, //PUSH nn, SP-2
                                    0xF1, 0xC1, 0xD1, 0xE1  //POP
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
                case 0x01:
                case 0x11:
                case 0x21:
                case 0x31:
                case 0xF8:
                case 0xF1:
                case 0xC1:
                case 0xD1:
                case 0xE1:
                    {
                        return 12;
                    }
                
                case 0xF9:
                    {
                        return 8;
                    }
                case 0x08:
                    {
                        return 20;
                    }
                case 0xF5:
                case 0xC5:
                case 0xD5:
                case 0xE5:
                    {
                        return 16;
                    }
                default:
                    {
                        throw new Exception("wrong instuction length");
                    }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override byte GetLenght( ushort instructionAdress )
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0x01:
                case 0x11:
                case 0x21:
                case 0x31:
                case 0x08:
                    {
                        return 0x03;
                    }
                case 0xF9:
                case 0xF8:
                case 0xF5:
                case 0xC5:
                case 0xD5:
                case 0xE5:
                case 0xF1:
                case 0xC1:
                case 0xD1:
                case 0xE1:
                    {
                        return 0x01;
                    }
                default:
                    {
                        return 0x01;
                    }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override ushort Exec( ushort instructionAdress )
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0x01:
                    {
                        instructionAdress += 0x01;
                        ushort val = GameBoy.Ram.ReadUshortAt(instructionAdress);
                        GameBoy.Cpu.rBC = val;
                        instructionAdress += 0x02;
                        return instructionAdress;
                    }
                case 0x11:
                    {
                        instructionAdress += 0x01;
                        ushort val = GameBoy.Ram.ReadUshortAt(instructionAdress);
                        GameBoy.Cpu.rDE = val;
                        instructionAdress += 0x02;
                        return instructionAdress;
                    }
                case 0x21:
                    {
                        instructionAdress += 0x01;
                        ushort val = GameBoy.Ram.ReadUshortAt(instructionAdress);
                        GameBoy.Cpu.rHL = val;
                        instructionAdress += 0x02;
                        return instructionAdress;
                    }
                case 0x31:
                    {
                        instructionAdress += 0x01;
                        ushort val = GameBoy.Ram.ReadUshortAt(instructionAdress);
                        GameBoy.Cpu.SP = val;
                        instructionAdress += 0x02;
                        return instructionAdress;
                    }
                case 0xF9:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.SP = GameBoy.Cpu.rHL;
                        return instructionAdress;
                    }
                case 0xF8:
                    {
                        instructionAdress += 0x01;
                        sbyte sb = GameBoy.Ram.ReadSignedByteAt(instructionAdress);
                        instructionAdress += 0x01;
                        ushort a = GameBoyTest.Z80.Z80Instructions.ALU.Z80Instruction_ALU_16bit.UpdateFlagsSP(GameBoy.Cpu.SP, sb);
                        GameBoy.Cpu.rHL = a;// GameBoy.Ram.ReadByteAt(a); <-- NON !
                        return instructionAdress;
                    }
                case 0x08:
                    {
                        instructionAdress += 0x01;
                        ushort adr = GameBoy.Ram.ReadUshortAt(instructionAdress);
                        GameBoy.Ram.WriteUshortAt(adr, GameBoy.Cpu.SP);
                        instructionAdress += 0x02;
                        return instructionAdress;
                    }
                case 0xF5:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.SP -= 0x02;
                        GameBoy.Ram.WriteUshortAt( GameBoy.Cpu.SP, GameBoy.Cpu.rAF );
                        return instructionAdress;
                    }
                case 0xC5:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.SP -= 0x02;
                        GameBoy.Ram.WriteUshortAt(GameBoy.Cpu.SP, GameBoy.Cpu.rBC);
                        return instructionAdress;
                    }
                case 0xD5:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.SP -= 0x02;
                        GameBoy.Ram.WriteUshortAt(GameBoy.Cpu.SP, GameBoy.Cpu.rDE);
                        return instructionAdress;
                    }
                case 0xE5:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.SP -= 0x02;
                        GameBoy.Ram.WriteUshortAt(GameBoy.Cpu.SP, GameBoy.Cpu.rHL);
                        return instructionAdress;
                    }
                case 0xF1:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.rAF = GameBoy.Ram.ReadUshortAt(GameBoy.Cpu.SP);
                        GameBoy.Cpu.SP += 0x02;
                        return instructionAdress;
                    }
                case 0xC1:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.rBC = GameBoy.Ram.ReadUshortAt(GameBoy.Cpu.SP);
                        GameBoy.Cpu.SP += 0x02;
                        return instructionAdress;
                    }
                case 0xD1:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.rDE = GameBoy.Ram.ReadUshortAt(GameBoy.Cpu.SP);
                        GameBoy.Cpu.SP += 0x02;
                        return instructionAdress;
                    }
                case 0xE1:
                    {
                        instructionAdress += 0x01;
                        GameBoy.Cpu.rHL = GameBoy.Ram.ReadUshortAt(GameBoy.Cpu.SP);
                        GameBoy.Cpu.SP += 0x02; 
                        return instructionAdress;
                    }
                default:
                    {
                        return (ushort)(instructionAdress + 0x01);
                    }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override string ToString( ushort instructionAdress )
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            String outS = "";
            switch (opcode)
            {
                case 0x01:
                    {
                        ushort i=instructionAdress; 
                        i++;
                        ushort val = GameBoy.Ram.ReadUshortAt(i);
                        return outS + "ld BC, " + String.Format("{0:x4}", val);
                    }
                case 0x11:
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort val = GameBoy.Ram.ReadUshortAt(i);
                        return outS + "ld DE, " + String.Format("{0:x4}", val);
                    }
                case 0x21:
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort val = GameBoy.Ram.ReadUshortAt(i);
                        return outS + "ld HL, " + String.Format("{0:x4}", val);
                    }
                case 0x31:
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort val = GameBoy.Ram.ReadUshortAt(i);
                        return outS + "ld SP, " + String.Format("{0:x4}", val);
                    }
                case 0xF9:
                    {
                        return outS + "ld SP, HL";
                    }
                case 0xF8:
                    {
                        return outS + "ld HL, SP";
                    }
                case 0x08:
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort adr = GameBoy.Ram.ReadUshortAt(i);
                        return outS + "ld " + String.Format("{0:x4}", adr) + ", SP";
                    }
                case 0xF5:
                    {
                        return outS + "push AF";
                    }
                case 0xC5:
                    {
                        return outS + "push BC";
                    }
                case 0xD5:
                    {
                        return outS + "push DE";
                    }
                case 0xE5:
                    {
                        return outS + "push HL";
                    }
                case 0xF1:
                    {
                        return outS + "pop AF";
                    }
                case 0xC1:
                    {
                        return outS + "pop BC";
                    }
                case 0xD1:
                    {
                        return outS + "pop DE";
                    }
                case 0xE1:
                    {
                        return outS + "pop HL";
                    }
            }
            return "";
        }

    }
}