//![DO NOT UPDATE]
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBoyTest.Debug;

namespace GameBoyTest.Z80.Z80Instructions.LD
{
    class Z80Instruction_LD_8bit : Z80Instruction
    {
        const byte LD_NN_N = 0x06;

        public Z80Instruction_LD_8bit()
        {
            m_Name = "LD8";
            m_Summary = "";
            m_Flags = "- - - -";
            m_OpCode = new byte[] { 0x06, 0x0E, 0x16, 0x1E, 0x26, 0x2E, //ld nn,n
                                    0x7F, 0x78, 0x79, 0x7A, 0x7B, 0x7C, //ld r1,r2
                                    0x7E, 0x40, 0x41, 0x42, 0x43, 0x44, 
                                    0x45, 0x46, 0x48, 0x49, 0x4A, 0x4B, 
                                    0x4C, 0x4D, 0x4E, 0x50, 0x51, 
                                    
                                    0x52, 0x53, 0x54, 0x55, 0x56, 0x58, 
                                    0x59, 0x5A, 0x5B, 0x5C, 0x5D, 0x5E, 
                                    0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 
                                    0x66, 0x68, 0x69, 0x6A, 0x6B, 0x6C, 
                                    0x6D, 0x6E, 0x70, 0x71, 0x72, 0x73, 
                                    0x74, 0x75, 0x36, 
                                    
                                    /*0x7F, 0x78, 0x79, 0x7A,
                                    0x7B, 0x7C,*/ 0x7D, 0x0A, 0x1A, /*0x7E,*/ 0xFA, 0x3E, /*0x7F,*/ 0x47, 0x4F,
                                    0x57, 0x5F, 0x67, 0x6F, 0x02, 0x12, 0x77, 0xEA, 0xF2, 0xE2, 0x3A,
                                    0x32, 0x2A, 0x22, 0xE0, 0xF0
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
                case 0x7F:  //ld A,A
                case 0x78:  //ld A,B
                case 0x79:  //ld A,C
                case 0x7A:  //ld A,D
                case 0x7B:  //ld A,E
                case 0x7C:  //ld A,H
                case 0x7D:  //ld A,L
                case 0x40:  //ld B,B
                case 0x41:  //ld B,C
                case 0x42:  //ld B,D
                case 0x43:  //ld B,E
                case 0x44:  //ld B,H
                case 0x45:  //ld B,L
                case 0x48:  //ld C,B
                case 0x49:  //ld C,C
                case 0x4A:  //ld C,D
                case 0x4B:  //ld C,E
                case 0x4C:  //ld C,H
                case 0x4D:  //ld C,L
                case 0x50:  //ld D,B
                case 0x51:  //ld D,C
                case 0x52:  //ld D,D
                case 0x53:  //ld D,E
                case 0x54:  //ld D,H
                case 0x55:  //ld D,L
                case 0x58:  //ld E,B
                case 0x59:  //ld E,C
                case 0x5A:  //ld E,D
                case 0x5B:  //ld E,E
                case 0x5C:  //ld E,H
                case 0x5D:  //ld E,L
                case 0x60:  //ld H,B
                case 0x61:  //ld H,C
                case 0x62:  //ld H,D
                case 0x63:  //ld H,E
                case 0x64:  //ld H,H
                case 0x65:  //ld H,L
                case 0x68:  //ld L,B
                case 0x69:  //ld L,C
                case 0x6A:  //ld L,D
                case 0x6B:  //ld L,E
                case 0x6C:  //ld L,H
                case 0x6D:  //ld L,L
                case 0x47:  //ld B,A
                case 0x4F:  //ld C,A
                case 0x57:  //ld D,A
                case 0x5F:  //ld E,A
                case 0x67:  //ld H,A
                case 0x6F:  //ld L,A
                    {
                        return 4;
                    }
                case 0x06:  //ld B,n
                case 0x0E:  //ld C,n
                case 0x16:  //ld D,n
                case 0x1E:  //ld E,n
                case 0x26:  //ld H,n
                case 0x2E:  //ld N,n
                case 0x7E:  //ld A,(HL)
                case 0x46:  //ld B,(HL)
                case 0x4E:  //ld C,(HL)
                case 0x56:  //ld D,(HL)
                case 0x5E:  //ld E,(HL)
                case 0x66:  //ld H,(HL)
                case 0x6E:  //ld L,(HL)
                case 0x70:  //ld (HL),B
                case 0x71:  //ld (HL),C
                case 0x72:  //ld (HL),D
                case 0x73:  //ld (HL),E
                case 0x74:  //ld (HL),H
                case 0x75:  //ld (HL),L
                case 0x0A:  //ld A,(BC)
                case 0x1A:  //ld A,(DE)
                case 0x3E:  //ld A,#
                case 0x02:  //ld (BC),A
                case 0x12:  //ld (DE),A
                case 0x77:  //ld (HL),A 
                case 0xF2:  //ld A,(C)
                case 0xE2:  //ld (C),A
                case 0x3A:  //ld A,(HLD)
                case 0x32:  //ld (HLD), A
                case 0x2A:  //ld A,(HLI)
                case 0x22:  //ld (HLI), A
                    {
                        return 8;
                    }
                case 0xE0:  //ld(n), A
                case 0xF0:  //ld A,(n)
                case 0x36:  //ld (HL),n
                    {
                        return 12;
                    }
                case 0xEA:  //ld (nn),A
                case 0xFA:  //ld A,(nn)
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
                case 0x06:  //ld B,n
                case 0x0E:  //ld C,n
                case 0x16:  //ld D,n
                case 0x1E:  //ld E,n
                case 0x26:  //ld H,n
                case 0x2E:  //ld N,n
                    {
                        return 0x02;
                    }
                case 0x7F:  //ld A,A
                case 0x78:  //ld A,B
                case 0x79:  //ld A,C
                case 0x7A:  //ld A,D
                case 0x7B:  //ld A,E
                case 0x7C:  //ld A,H
                case 0x7D:  //ld A,L
                case 0x7E:  //ld A,(HL)
                case 0x40:  //ld B,B
                case 0x41:  //ld B,C
                case 0x42:  //ld B,D
                case 0x43:  //ld B,E
                case 0x44:  //ld B,H
                case 0x45:  //ld B,L
                case 0x46:  //ld B,(HL)
                case 0x48:  //ld C,B
                case 0x49:  //ld C,C
                case 0x4A:  //ld C,D
                case 0x4B:  //ld C,E
                case 0x4C:  //ld C,H
                case 0x4D:  //ld C,L
                case 0x4E:  //ld C,(HL)
                case 0x50:  //ld D,B
                case 0x51:  //ld D,C
                case 0x52:  //ld D,D
                case 0x53:  //ld D,E
                case 0x54:  //ld D,H
                case 0x55:  //ld D,L
                case 0x56:  //ld D,(HL)
                case 0x58:  //ld E,B
                case 0x59:  //ld E,C
                case 0x5A:  //ld E,D
                case 0x5B:  //ld E,E
                case 0x5C:  //ld E,H
                case 0x5D:  //ld E,L
                case 0x5E:  //ld E,(HL)
                case 0x60:  //ld H,B
                case 0x61:  //ld H,C
                case 0x62:  //ld H,D
                case 0x63:  //ld H,E
                case 0x64:  //ld H,H
                case 0x65:  //ld H,L
                case 0x66:  //ld H,(HL)
                case 0x68:  //ld L,B
                case 0x69:  //ld L,C
                case 0x6A:  //ld L,D
                case 0x6B:  //ld L,E
                case 0x6C:  //ld L,H
                case 0x6D:  //ld L,L
                case 0x6E:  //ld L,(HL)
                case 0x70:  //ld (HL),B
                case 0x71:  //ld (HL),C
                case 0x72:  //ld (HL),D
                case 0x73:  //ld (HL),E
                case 0x74:  //ld (HL),H
                case 0x75:  //ld (HL),L
                    {
                        return 0x01;
                    }
                case 0xFA:  //ld A,(nn)
                    {
                        return 0x03;
                    }
                case 0x0A:  //ld A,(BC)
                case 0x1A:  //ld A,(DE)
                    {
                        return 0x01;
                    }
                case 0x3E:  //ld A,#
                case 0x36:  //ld (HL),n
                    {
                        return 0x02;
                    }
                case 0x47:  //ld B,A
                case 0x4F:  //ld C,A
                case 0x57:  //ld D,A
                case 0x5F:  //ld E,A
                case 0x67:  //ld H,A
                case 0x6F:  //ld L,A
                    {
                        return 0x01;
                    }
                case 0x02:  //ld (BC),A
                case 0x12:  //ld (DE),A
                case 0x77:  //ld (HL),A 
                    {
                        return 0x01;
                    }
                case 0xF2:  //ld A,(C)
                case 0xE2:  //ld (C),A
                case 0x3A:  //ld A,(HLD)
                case 0x32:  //ld (HLD), A
                case 0x2A:  //ld A,(HLI)
                case 0x22:  //ld (HLI), A
                    {
                        return 0x01;
                    }
                case 0xE0:  //ld(n), A
                case 0xF0:  //ld A,(n)
                default:
                    {
                        return 0x02;
                    }
                case 0xEA:  //ld (nn),A
                    {
                        return 0x03;
                    }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override ushort Exec( ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0x06:  //ld B,n
                    {
                        ++instructionAdress;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        GameBoy.Cpu.rB= val;
                        return ++instructionAdress;
                    }
                case 0x0E:  //ld C,n
                    {
                        ++instructionAdress;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        GameBoy.Cpu.rC= val;
                        return ++instructionAdress;
                    }
                case 0x16:  //ld D,n
                    {
                        ++instructionAdress;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        GameBoy.Cpu.rD= val;
                        return ++instructionAdress;
                    }
                case 0x1E:  //ld E,n
                    {
                        ++instructionAdress;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        GameBoy.Cpu.rE= val;
                        return ++instructionAdress;
                    }
                case 0x26:  //ld H,n
                    {
                        ++instructionAdress;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        GameBoy.Cpu.rH= val;
                        return ++instructionAdress;
                    }
                case 0x2E:  //ld L,n
                    {
                        ++instructionAdress;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        GameBoy.Cpu.rL= val;
                        return ++instructionAdress;
                    }
                case 0x7F:  //ld A,A
                    {
                        GameBoy.Cpu.rA = GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x78:  //ld A,B
                    {
                        GameBoy.Cpu.rA = GameBoy.Cpu.rB;
                        return ++instructionAdress;
                    }
                case 0x79:  //ld A,C
                    {
                        GameBoy.Cpu.rA = GameBoy.Cpu.rC;
                        return ++instructionAdress;
                    }
                case 0x7A:  //ld A,D
                    {
                        GameBoy.Cpu.rA = GameBoy.Cpu.rD;
                        return ++instructionAdress;
                    }
                case 0x7B:  //ld A,E
                    {
                        GameBoy.Cpu.rA = GameBoy.Cpu.rE;
                        return ++instructionAdress;
                    }
                case 0x7C:  //ld A,H
                    {
                        GameBoy.Cpu.rA = GameBoy.Cpu.rH;
                        return ++instructionAdress;
                    }
                case 0x7D:  //ld A,L
                    {
                        GameBoy.Cpu.rA =  GameBoy.Cpu.rL;
                        return ++instructionAdress;
                    }
                case 0x7E:  //ld A,(HL)
                    {
                        GameBoy.Cpu.rA = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        return ++instructionAdress;
                    }
                case 0x40:  //ld B,B
                    {
                        GameBoy.Cpu.rB= GameBoy.Cpu.rB;
                        return ++instructionAdress;
                    }
                case 0x41:  //ld B,C
                    {
                        GameBoy.Cpu.rB= GameBoy.Cpu.rC;
                        return ++instructionAdress;
                    }
                case 0x42:  //ld B,D
                    {
                        GameBoy.Cpu.rB= GameBoy.Cpu.rD;
                        return ++instructionAdress;
                    }
                case 0x43:  //ld B,E
                    {
                        GameBoy.Cpu.rB= GameBoy.Cpu.rE;
                        return ++instructionAdress;
                    }
                case 0x44:  //ld B,H
                    {
                        GameBoy.Cpu.rB= GameBoy.Cpu.rH;
                        return ++instructionAdress;
                    }
                case 0x45:  //ld B,L
                    {
                        GameBoy.Cpu.rB= GameBoy.Cpu.rL;
                        return ++instructionAdress;
                    }
                case 0x46:  //ld B,(HL)
                    {
                        GameBoy.Cpu.rB = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        return ++instructionAdress;
                    }
                case 0x48:  //ld C,B
                    {
                        GameBoy.Cpu.rC= GameBoy.Cpu.rB;
                        return ++instructionAdress;
                    }
                case 0x49:  //ld C,C
                    {
                        GameBoy.Cpu.rC= GameBoy.Cpu.rC;
                        return ++instructionAdress;
                    }
                case 0x4A:  //ld C,D
                    {
                        GameBoy.Cpu.rC= GameBoy.Cpu.rD;
                        return ++instructionAdress;
                    }
                case 0x4B:  //ld C,E
                    {
                        GameBoy.Cpu.rC= GameBoy.Cpu.rE;
                        return ++instructionAdress;
                    }
                case 0x4C:  //ld C,H
                    {
                        GameBoy.Cpu.rC= GameBoy.Cpu.rH;
                        return ++instructionAdress;
                    }
                case 0x4D:  //ld C,L
                    {
                        GameBoy.Cpu.rC= GameBoy.Cpu.rL;
                        return ++instructionAdress;
                    }
                case 0x4E:  //ld C,(HL)
                    {
                        GameBoy.Cpu.rC = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        return ++instructionAdress;
                    }
                case 0x50:  //ld D,B
                    {
                        GameBoy.Cpu.rD= GameBoy.Cpu.rB;
                        return ++instructionAdress;
                    }
                case 0x51:  //ld D,C
                    {
                        GameBoy.Cpu.rD= GameBoy.Cpu.rC;
                        return ++instructionAdress;
                    }
                case 0x52:  //ld D,D
                    {
                        GameBoy.Cpu.rD= GameBoy.Cpu.rD;
                        return ++instructionAdress;
                    }
                case 0x53:  //ld D,E
                    {
                        GameBoy.Cpu.rD= GameBoy.Cpu.rE;
                        return ++instructionAdress;
                    }
                case 0x54:  //ld D,H
                    {
                        GameBoy.Cpu.rD= GameBoy.Cpu.rH;
                        return ++instructionAdress;
                    }
                case 0x55:  //ld D,L
                    {
                        GameBoy.Cpu.rD= GameBoy.Cpu.rL;
                        return ++instructionAdress;
                    }
                case 0x56:  //ld D,(HL)
                    {
                        GameBoy.Cpu.rD = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        return ++instructionAdress;
                    }
                case 0x58:  //ld E,B
                    {
                        GameBoy.Cpu.rE= GameBoy.Cpu.rB;
                        return ++instructionAdress;
                    }
                case 0x59:  //ld E,C
                    {
                        GameBoy.Cpu.rE= GameBoy.Cpu.rC;
                        return ++instructionAdress;
                    }
                case 0x5A:  //ld E,D
                    {
                        GameBoy.Cpu.rE= GameBoy.Cpu.rD;
                        return ++instructionAdress;
                    }
                case 0x5B:  //ld E,E
                    {
                        GameBoy.Cpu.rE= GameBoy.Cpu.rE;
                        return ++instructionAdress;
                    }
                case 0x5C:  //ld E,H
                    {
                        GameBoy.Cpu.rE= GameBoy.Cpu.rH;
                        return ++instructionAdress;
                    }
                case 0x5D:  //ld E,L
                    {
                        GameBoy.Cpu.rE= GameBoy.Cpu.rL;
                        return ++instructionAdress;
                    }
                case 0x5E:  //ld E,(HL)
                    {
                        GameBoy.Cpu.rE = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        return ++instructionAdress;
                    }
                case 0x60:  //ld H,B
                    {
                        GameBoy.Cpu.rH= GameBoy.Cpu.rB;
                        return ++instructionAdress;
                    }
                case 0x61:  //ld H,C
                    {
                        GameBoy.Cpu.rH= GameBoy.Cpu.rC;
                        return ++instructionAdress;
                    }
                case 0x62:  //ld H,D
                    {
                        GameBoy.Cpu.rH= GameBoy.Cpu.rD;
                        return ++instructionAdress;
                    }
                case 0x63:  //ld H,E
                    {
                        GameBoy.Cpu.rH= GameBoy.Cpu.rE;
                        return ++instructionAdress;
                    }
                case 0x64:  //ld H,H
                    {
                        GameBoy.Cpu.rH= GameBoy.Cpu.rH;
                        return ++instructionAdress;
                    }
                case 0x65:  //ld H,L
                    {
                        GameBoy.Cpu.rH= GameBoy.Cpu.rL;
                        return ++instructionAdress;
                    }
                case 0x66:  //ld H,(HL)
                    {
                        GameBoy.Cpu.rH = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        return ++instructionAdress;
                    }
                case 0x68:  //ld L,B
                    {
                        GameBoy.Cpu.rL= GameBoy.Cpu.rB;
                        return ++instructionAdress;
                    }
                case 0x69:  //ld L,C
                    {
                        GameBoy.Cpu.rL= GameBoy.Cpu.rC;
                        return ++instructionAdress;
                    }
                case 0x6A:  //ld L,D
                    {
                        GameBoy.Cpu.rL= GameBoy.Cpu.rD;
                        return ++instructionAdress;
                    }
                case 0x6B:  //ld L,E
                    {
                        GameBoy.Cpu.rL= GameBoy.Cpu.rE;
                        return ++instructionAdress;
                    }
                case 0x6C:  //ld L,H
                    {
                        GameBoy.Cpu.rL= GameBoy.Cpu.rH;
                        return ++instructionAdress;
                    }
                case 0x6D:  //ld L,L
                    {
                        GameBoy.Cpu.rL=GameBoy.Cpu.rL;
                        return ++instructionAdress;
                    }
                case 0x6E:  //ld L,(HL)
                    {
                        GameBoy.Cpu.rL = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        return ++instructionAdress;
                    }
                case 0x70:  //ld (HL),B
                    {
                        GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, GameBoy.Cpu.rB);
                        return ++instructionAdress;
                    }
                case 0x71:  //ld (HL),C
                    {
                        GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, GameBoy.Cpu.rC);
                        return ++instructionAdress;
                    }
                case 0x72:  //ld (HL),D
                    {
                        GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, GameBoy.Cpu.rD);
                        return ++instructionAdress;
                    }
                case 0x73:  //ld (HL),E
                    {
                        GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, GameBoy.Cpu.rE);
                        return ++instructionAdress;
                    }
                case 0x74:  //ld (HL),H
                    {
                        GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, GameBoy.Cpu.rH);
                        return ++instructionAdress;
                    }
                case 0x75:  //ld (HL),L
                    {
                        GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, GameBoy.Cpu.rL);
                        return ++instructionAdress;
                    }
                case 0x36:  //ld (HL),n
                    {
                        ++instructionAdress;
                        byte b = GameBoy.Ram.ReadByteAt(instructionAdress);
                        GameBoy.Ram.WriteAt( GameBoy.Cpu.rHL, b);
                        return ++instructionAdress;
                    }
                case 0x0A:  //ld A,(BC)
                    {
                        GameBoy.Cpu.rA = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rBC);
                        return ++instructionAdress;
                    }
                case 0x1A:  //ld A,(DE)
                    {
                        GameBoy.Cpu.rA = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rDE);
                        return ++instructionAdress;;
                    }
                case 0xFA:  //ld A,(nn)
                    {
                        ++instructionAdress;
                        ushort adr = GameBoy.Ram.ReadUshortAt(instructionAdress);
                        GameBoy.Cpu.rA = GameBoy.Ram.ReadByteAt(adr);
                        return (ushort)(instructionAdress+2);
                    }
                case 0x3E:  //ld A,#
                    {
                        ++instructionAdress;
                        byte b = GameBoy.Ram.ReadByteAt( instructionAdress );
                        GameBoy.Cpu.rA= b;
                        return ++instructionAdress;
                    }
                case 0x47:  //ld B,A
                    {
                        GameBoy.Cpu.rB= GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x4F:  //ld C,A
                    {
                        GameBoy.Cpu.rC= GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x57:  //ld D,A
                    {
                        GameBoy.Cpu.rD= GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x5F:  //ld E,A
                    {
                        GameBoy.Cpu.rE= GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x67:  //ld H,A
                    {
                        GameBoy.Cpu.rH= GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x6F:  //ld L,A
                    {
                        GameBoy.Cpu.rL= GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x02:  //ld (BC),A
                    {
                        GameBoy.Ram.WriteAt(GameBoy.Cpu.rBC, GameBoy.Cpu.rA);
                        return ++instructionAdress;
                    }
                case 0x12:  //ld (DE),A
                    {
                        GameBoy.Ram.WriteAt(GameBoy.Cpu.rDE, GameBoy.Cpu.rA);
                        return ++instructionAdress;
                    }
                case 0x77:  //ld (HL),A 
                    {
                        GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, GameBoy.Cpu.rA);
                        return ++instructionAdress;
                    }
                case 0xEA:  //ld (nn),A
                    {
                        ushort s;
                        instructionAdress++;
                        s = GameBoy.Ram.ReadUshortAt(instructionAdress);
                        if( s == 0x2100 )
                        {
                            int y = 0;
                            y++;
                        }
                        GameBoy.Ram.WriteAt(s, GameBoy.Cpu.rA);
                        return instructionAdress+=2;
                    }
                case 0xF2:  //ld A,(C)
                    {
                        GameBoy.Cpu.rA = GameBoy.Ram.ReadByteAt((ushort)(GameBoy.Cpu.rC + 0xFF00));
                        return ++instructionAdress;
                    }
                case 0xE2:  //ld (C),A
                    {
                        ushort adr = (ushort)(0xFF00 + GameBoy.Cpu.rC);
                        GameBoy.Ram.WriteAt(adr, GameBoy.Cpu.rA);
                        return ++instructionAdress;
                    }
                case 0x3A:  //ld A,(HLD)
                    {
                        GameBoy.Cpu.rA = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        GameBoy.Cpu.DecHL();
                        return ++instructionAdress;
                    }
                case 0x32:  //ld (HLD), A
                    {
                        GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, GameBoy.Cpu.rA);
                        GameBoy.Cpu.DecHL();
                        return ++instructionAdress;
                    }
                case 0x2A:  //ld A,(HLI)
                    {
                        GameBoy.Cpu.rA = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        GameBoy.Cpu.IncHL();
                        return ++instructionAdress;
                    }
                case 0x22:  //ld (HLI), A
                    {
                        GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, GameBoy.Cpu.rA);
                        GameBoy.Cpu.IncHL();
                        return ++instructionAdress;
                    }
                case 0xE0:  //ld(n), A : Put A into memory address $FF00+n.
                    {
                        ++instructionAdress;
                        byte b = GameBoy.Ram.ReadByteAt(instructionAdress);
                        ushort adr = (ushort)(b + 0xFF00);
                        GameBoy.Ram.WriteAt(adr, GameBoy.Cpu.rA);
                        return ++instructionAdress;
                    }
                case 0xF0:  //ld A,(n) : Put memory address $FF00+n into A.
                    {
                        ++instructionAdress;
                        byte b = GameBoy.Ram.ReadByteAt(instructionAdress);
                        GameBoy.Cpu.rA = GameBoy.Ram.ReadByteAt((ushort)(b + 0xFF00));
                        return ++instructionAdress;
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
        public override string ToString(ushort instructionAdress)
        {
            string outS = "";
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0x06:  //ld B,n
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte val = GameBoy.Ram.ReadByteAt(i);
                       outS += LD_register_value("b", val);
                        return outS;
                    }
                case 0x0E:  //ld C,n
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte val = GameBoy.Ram.ReadByteAt(i);
                       outS += LD_register_value("c", val);
                        return outS;
                    }
                case 0x16:  //ld D,n
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte val = GameBoy.Ram.ReadByteAt(i);
                       outS += LD_register_value("d", val);
                        return outS;
                    }
                case 0x1E:  //ld E,n
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte val = GameBoy.Ram.ReadByteAt(i);
                       outS += LD_register_value("e", val);
                        return outS;
                    }
                case 0x26:  //ld H,n
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte val = GameBoy.Ram.ReadByteAt(i);
                       outS += LD_register_value("h", val);
                        return outS;
                    }
                case 0x2E:  //ld L,n
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte val = GameBoy.Ram.ReadByteAt(i);
                       outS += LD_register_value("l", val);
                        return outS;
                    }
                case 0x7F:  //ld A,A
                    {
                       outS += LD_register_register("a", "a");
                        return outS;
                    }
                case 0x78:  //ld A,B
                    {
                       outS += LD_register_register("a", "b");
                        return outS;
                    }
                case 0x79:  //ld A,C
                    {
                       outS += LD_register_register("a", "c");
                        return outS;
                    }
                case 0x7A:  //ld A,D
                    {
                       outS += LD_register_register("a", "d");
                        return outS;
                    }
                case 0x7B:  //ld A,E
                    {
                       outS += LD_register_register("a", "e");
                        return outS;
                    }
                case 0x7C:  //ld A,H
                    {
                       outS += LD_register_register("a", "h");
                        return outS;
                    }
                case 0x7D:  //ld A,L
                    {
                       outS += LD_register_register("a", "l");
                        return outS;
                    }
                case 0x7E:  //ld A,(HL)
                    {
                       outS += LD_register_adress("a", "hl");
                        return outS;
                    }
                case 0x40:  //ld B,B
                    {
                       outS += LD_register_register("b", "b");
                        return outS;
                    }
                case 0x41:  //ld B,C
                    {
                       outS += LD_register_register("b", "c");
                        return outS;
                    }
                case 0x42:  //ld B,D
                    {
                       outS += LD_register_register("b", "d");
                        return outS;
                    }
                case 0x43:  //ld B,E
                    {
                       outS += LD_register_register("b", "e");
                        return outS;
                    }
                case 0x44:  //ld B,H
                    {
                       outS += LD_register_register("b", "h");
                        return outS;
                    }
                case 0x45:  //ld B,L
                    {
                       outS += LD_register_register("b", "l");
                        return outS;
                    }
                case 0x46:  //ld B,(HL)
                    {
                       outS += LD_register_adress("a", "hl");
                        return outS;
                    }
                case 0x48:  //ld C,B
                    {
                       outS += LD_register_register("c", "b");
                        return outS;
                    }
                case 0x49:  //ld C,C
                    {
                       outS += LD_register_register("c", "c");
                        return outS;
                    }
                case 0x4A:  //ld C,D
                    {
                       outS += LD_register_register("c", "c");
                        return outS;
                    }
                case 0x4B:  //ld C,E
                    {
                       outS += LD_register_register("c", "e");
                        return outS;
                    }
                case 0x4C:  //ld C,H
                    {
                       outS += LD_register_register("c", "h");
                        return outS;
                    }
                case 0x4D:  //ld C,L
                    {
                       outS += LD_register_register("c", "l");
                        return outS;
                    }
                case 0x4E:  //ld C,(HL)
                    {
                       outS += LD_register_adress("c", "hl");
                        return outS;
                    }
                case 0x50:  //ld D,B
                    {
                       outS += LD_register_register("d", "b");
                        return outS;
                    }
                case 0x51:  //ld D,C
                    {
                       outS += LD_register_register("d", "c");
                        return outS;
                    }
                case 0x52:  //ld D,D
                    {
                       outS += LD_register_register("d", "d");
                        return outS;
                    }
                case 0x53:  //ld D,E
                    {
                       outS += LD_register_register("d", "e");
                        return outS;
                    }
                case 0x54:  //ld D,H
                    {
                       outS += LD_register_register("d", "h");
                        return outS;
                    }
                case 0x55:  //ld D,L
                    {
                       outS += LD_register_register("d", "l");
                        return outS;
                    }
                case 0x56:  //ld D,(HL)
                    {
                       outS += LD_register_adress("d", "hl");
                        return outS;
                    }
                case 0x58:  //ld E,B
                    {
                       outS += LD_register_register("e", "b");
                        return outS;
                    }
                case 0x59:  //ld E,C
                    {
                       outS += LD_register_register("e", "c");
                        return outS;
                    }
                case 0x5A:  //ld E,D
                    {
                       outS += LD_register_register("e", "d");
                        return outS;
                    }
                case 0x5B:  //ld E,E
                    {
                       outS += LD_register_register("e", "e");
                        return outS;
                    }
                case 0x5C:  //ld E,H
                    {
                       outS += LD_register_register("e", "h");
                        return outS;
                    }
                case 0x5D:  //ld E,L
                    {
                       outS += LD_register_register("e", "l");
                        return outS;
                    }
                case 0x5E:  //ld E,(HL)
                    {
                       outS += LD_register_adress("e", "hl");
                        return outS;
                    }
                case 0x60:  //ld H,B
                    {
                       outS += LD_register_register("h", "b");
                        return outS;
                    }
                case 0x61:  //ld H,C
                    {
                       outS += LD_register_register("h", "c");
                        return outS;
                    }
                case 0x62:  //ld H,D
                    {
                       outS += LD_register_register("h", "d");
                        return outS;
                    }
                case 0x63:  //ld H,E
                    {
                       outS += LD_register_register("h", "e");
                        return outS;
                    }
                case 0x64:  //ld H,H
                    {
                       outS += LD_register_register("h", "h");
                        return outS;
                    }
                case 0x65:  //ld H,L
                    {
                       outS += LD_register_register("h", "l");
                        return outS;
                    }
                case 0x66:  //ld H,(HL)
                    {
                       outS += LD_register_adress("h", "hl");
                        return outS;
                    }
                case 0x68:  //ld L,B
                    {
                       outS += LD_register_register("l", "b");
                        return outS;
                    }
                case 0x69:  //ld L,C
                    {
                       outS += LD_register_register("l", "c");
                        return outS;
                    }
                case 0x6A:  //ld L,D
                    {
                       outS += LD_register_register("l", "d");
                        return outS;
                    }
                case 0x6B:  //ld L,E
                    {
                       outS += LD_register_register("l", "e");
                        return outS;
                    }
                case 0x6C:  //ld L,H
                    {
                       outS += LD_register_register("l", "h");
                        return outS;
                    }
                case 0x6D:  //ld L,L
                    {
                       outS += LD_register_register("l", "l");
                        return outS;
                    }
                case 0x6E:  //ld L,(HL)
                    {
                       outS += LD_register_adress("l", "hl");
                        return outS;
                    }
                case 0x70:  //ld (HL),B
                    {
                       outS += LD_adress_register("hl", "b");
                        return outS;
                    }
                case 0x71:  //ld (HL),C
                    {
                       outS += LD_adress_register("hl", "c");
                        return outS;
                    }
                case 0x72:  //ld (HL),D
                    {
                       outS += LD_adress_register("hl", "d");
                        return outS;
                    }
                case 0x73:  //ld (HL),E
                    {
                       outS += LD_adress_register("hl", "e");
                        return outS;
                    }
                case 0x74:  //ld (HL),H
                    {
                       outS += LD_adress_register("hl", "h");
                        return outS;
                    }
                case 0x75:  //ld (HL),L
                    {
                       outS += LD_adress_register("hl", "l");
                        return outS;
                    }
                case 0x36:  //ld (HL),n
                    {
                        return "ld (HL),n";
                    }
                // ******************************************************************************************
                case 0x0A:  //ld A,(BC)
                    {
                       outS += LD_register_adress("a", "bc");
                        return outS;
                    }
                case 0x1A:  //ld A,(DE)
                    {
                       outS += LD_register_adress("a", "de");
                        return outS; ;
                    }
                case 0xFA:  //ld A,(nn)
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort adr = GameBoy.Ram.ReadUshortAt(i);
                        outS += LD_register_adress("a", adr);
                        return outS;
                    }
                case 0x3E:  //ld A,#
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte b = GameBoy.Ram.ReadByteAt( i );
                       outS += LD_register_value("a", b);
                        return outS;

                    }
                case 0x47:  //ld B,A
                    {
                       outS += LD_register_register("b", "a");
                        return outS;
                    }
                case 0x4F:  //ld C,A
                    {
                       outS += LD_register_register("c", "a");
                        return outS;
                    }
                case 0x57:  //ld D,A
                    {
                       outS += LD_register_register("d", "a");
                        return outS;
                    }
                case 0x5F:  //ld E,A
                    {
                       outS += LD_register_register("e", "a");
                        return outS;
                    }
                case 0x67:  //ld H,A
                    {
                       outS += LD_register_register("h", "a");
                        return outS;
                    }
                case 0x6F:  //ld L,A
                    {
                       outS += LD_register_register("l", "a");
                        return outS;
                    }
                case 0x02:  //ld (BC),A
                    {
                       outS += LD_adress_register("bc", "a");
                        return outS;
                    }
                case 0x12:  //ld (DE),A
                    {
                       outS += LD_adress_register("de", "a");
                        return outS;
                    }
                case 0x77:  //ld (HL),A 
                    {
                       outS += LD_adress_register("hl", "a");
                        return outS;
                    }
                case 0xEA:  //ld (nn),A
                    {
                        ushort i = instructionAdress;
                        i++;
                        ushort s = GameBoy.Ram.ReadUshortAt(i);
                        outS += LD_adress_register(s, "a");
                        return outS;
                    }
                case 0xF2:  //ld A,(C)
                    {
                       outS += LD_register_offset("a", "c");
                        return outS;
                    }
                case 0xE2:  //ld (C),A
                    {
                       outS += LD_offset_register("c", "a");
                        return outS;
                    }
                case 0x3A:  //ld A,(HLD)
                    {
                       outS += LD_register_adress("a", "hlD");
                        return outS;
                    }
                case 0x32:  //ld (HLD), A
                    {
                       outS += LD_adress_register("hl", "a");                      
                        return outS;
                    }
                case 0x2A:  //ld A,(HLI)
                    {
                        outS += LD_register_adress("a", "hlI");
                        return outS;
                    }
                case 0x22:  //ld (HLI), A
                    {
                        outS += LDI_adress_register("hl", "a");
                        return outS;
                    }
                case 0xE0:  //ld(n), A
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte b = GameBoy.Ram.ReadByteAt(i);
                        outS += LD_offset_register(b, "a");
                        return outS;
                    }
                case 0xF0:  //ld A,(n)
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte b = GameBoy.Ram.ReadByteAt(i);
                        outS += LD_register_offset("a", "b");
                        return outS;
                    }
                default:
                    {
                        return "ld error";
                    }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private String LD_register_value(String r, byte val)
        {
            return "ld " + r + "," + String.Format("{0:x2}", val);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private String LD_register_register(String r1, String r2)
        {
            return "ld "+r2+","+r1;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private String LD_register_adress(String r, String adr)
        {
            return "ld " + r + ",(" + adr + ")";
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private String LD_register_adress(String r, ushort adr)
        {
            return "ld " + r + ",(" + String.Format("{0:x4}",adr) + ")";
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private String LD_adress_register(String adr, String r)
        {
            return "ld " + "(" + adr + "),"+r ;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private String LDI_adress_register(String adr, String r)
        {
            return "ldi " + "(" + adr + ")," + r;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private String LD_adress_register(ushort adr, String r)
        {
            return "ld " + "(" + String.Format("{0:x4}", adr) + ")," + r;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private String LD_register_offset(String r, String offset)
        {
            return "ld " + r + ",(0xFF00+" + String.Format("{0:x2}", offset) + ")";
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private String LD_offset_register(String offset, String r)
        {
            return "ld " + "(0xFF00+" + offset + ")," + r;
        }
        
        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private String LD_offset_register(byte offset, String r)
        {
            return "ld " + "(0xFF00+" + String.Format("{0:x2}", offset) + ")," + r;
        }
    }
}