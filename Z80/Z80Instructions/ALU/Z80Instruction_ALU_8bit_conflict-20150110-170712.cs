using GameBoyTest.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.ALU
{
    class Z80Instruction_ALU_8bit : Z80Instruction
    {

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public Z80Instruction_ALU_8bit()
        {
            m_Name = "ALU8";
            m_Summary = "";
            m_Flags = "- - - -";
            m_OpCode = new byte[] { 0x87, 0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0xC6,
                                    0x8F, 0x88, 0x89, 0x8A, 0x8B, 0x8C, 0x8D, 0x8E, 0xCE,
                                    0x97, 0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0xD6,
                                    0x9F, 0x98, 0x99, 0x9A, 0x9B, 0x9C, 0x9D, 0x9E, 0xDE,
                                    0xA7, 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xE6,
                                    0xB7, 0xB0, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xF6,
                                    0xAF, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xEE,
                                    0xBF, 0xB8, 0xB9, 0xBA, 0xBB, 0xBC, 0xBD, 0xBE, 0xFE, 0x3C,
                                    0x04, 0x0C, 0x14, 0x1C, 0x24, 0x2C, 0x34, 0x3D, 0x05,
                                    0x0D, 0x15, 0x1D, 0x25, 0x2D, 0x35};

            m_NbCycles = 4;
            m_NbCyclesMax = 4;
            m_Lenght = 1;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override byte GetLenght(ushort instructionAdress)
        {
            byte opcode = GameBoy.Ram.ReadByteAt(instructionAdress);
            switch (opcode)
            {
                case 0xC6:
                case 0xCE:
                case 0xD6:
                case 0xE6:
                case 0xF6:
                case 0xEE:
                case 0xFE:
                case 0xDE:
                    {
                        return 0x02;
                    }
                case 0x87: //ADD A,A
                case 0x80: //ADD A,B
                case 0x81: //ADD A,C
                case 0x82: //ADD A,D
                case 0x83: //ADD A,E
                case 0x84: //ADD A,H
                case 0x85: //ADD A,L
                case 0x86: //ADD A,(HL)
                case 0x8F: //ADC A,A
                case 0x88: //ADC A,B
                case 0x89: //ADC A,C
                case 0x8A: //ADC A,D
                case 0x8B: //ADC A,E
                case 0x8C: //ADC A,H
                case 0x8D: //ADC A,L
                case 0x8E: //ADC A,(HL)
                case 0x97: //SUB A,A
                case 0x90: //SUB A,B
                case 0x91: //SUB A,C
                case 0x92: //SUB A,D
                case 0x93: //SUB A,E
                case 0x94: //SUB A,F
                case 0x95: //SUB A,L
                case 0x96: //SUB A,(HL)
                case 0x9F: //SBC A,A
                case 0x98: //SBC A,B
                case 0x99: //SBC A,C
                case 0x9A: //SBC A,D
                case 0x9B: //SBC A,E
                case 0x9C: //SBC A,H
                case 0x9D: //SBC A,L
                case 0x9E: //SBC A,(HL)
                case 0xA7: //AND A,A
                case 0xA0: //AND A,B
                case 0xA1: //AND A,C
                case 0xA2: //AND A,D
                case 0xA3: //AND A,E
                case 0xA4: //AND A,H
                case 0xA5: //AND A,L
                case 0xA6: //AND A,(HL)
                case 0xB7: //OR A,A
                case 0xB0: //OR A,B
                case 0xB1: //OR A,C
                case 0xB2: //OR A,D
                case 0xB3: //OR A,E
                case 0xB4: //OR A,H
                case 0xB5: //OR A,L
                case 0xB6: //OR A,(HL)
                case 0xAF: //XOR A,A
                case 0xA8: //XOR A,B
                case 0xA9: //XOR A,C
                case 0xAA: //XOR A,D
                case 0xAB: //XOR A,E
                case 0xAC: //XOR A,H
                case 0xAD: //XOR A,L
                case 0xAE: //XOR A,(HL)
                case 0xBF: //CP A,A
                case 0xB8: //CP A,B
                case 0xB9: //CP A,C
                case 0xBA: //CP A,D
                case 0xBB: //CP A,E
                case 0xBC: //CP A,H
                case 0xBD: //CP A,L
                case 0xBE: //CP A,(HL)
                case 0x3C: //INC A
                case 0x04: //INC B
                case 0x0C: //INC C
                case 0x14: //INC D
                case 0x1C: //INC E
                case 0x24: //INC H
                case 0x2C: //INC L
                case 0x34: //INC (HL)
                case 0x3D: //DEC A
                case 0x05: //DEC B
                case 0x0D: //DEC C
                case 0x15: //DEC D
                case 0x1D: //DEC E
                case 0x25: //DEC H
                case 0x2D: //DEC L
                case 0x35: //DEC (HL)
                    {
                        return 0x01;
                    }
                default:
                    {
                        return 0x00;
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
                case 0x87: //ADD A,A
                    {
                        UpdateFlagsADD(GameBoy.Cpu.rA, GameBoy.Cpu.rA);
                        GameBoy.Cpu.rA += GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x80: //ADD A,B
                    {
                        UpdateFlagsADD(GameBoy.Cpu.rA, GameBoy.Cpu.rB);
                        GameBoy.Cpu.rA += GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x81: //ADD A,C
                    {
                        UpdateFlagsADD(GameBoy.Cpu.rA, GameBoy.Cpu.rC);
                        GameBoy.Cpu.rA += GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x82: //ADD A,D
                    {
                        UpdateFlagsADD(GameBoy.Cpu.rA, GameBoy.Cpu.rD);
                        GameBoy.Cpu.rA += GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x83: //ADD A,E
                    {
                        UpdateFlagsADD(GameBoy.Cpu.rA, GameBoy.Cpu.rE);
                        GameBoy.Cpu.rA += GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x84: //ADD A,H
                    {
                        UpdateFlagsADD(GameBoy.Cpu.rA, GameBoy.Cpu.rH);
                        GameBoy.Cpu.rA += GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x85: //ADD A,L
                    {
                        UpdateFlagsADD(GameBoy.Cpu.rA, GameBoy.Cpu.rL);
                        GameBoy.Cpu.rA += GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x86: //ADD A,(HL)
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt( GameBoy.Cpu.rHL );
                        UpdateFlagsADD(GameBoy.Cpu.rA, val);
                        GameBoy.Cpu.rA += val;
                        return ++instructionAdress;
                    }
                case 0xC6: //ADD A,#
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        UpdateFlagsADD(GameBoy.Cpu.rA, val);
                        GameBoy.Cpu.rA += val;
                        return ++instructionAdress;
                    }
                case 0x8F: //ADC A,A
                    {
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        UpdateFlagsADC(GameBoy.Cpu.rA, GameBoy.Cpu.rA);
                        
                        GameBoy.Cpu.rA += (byte)( GameBoy.Cpu.rA + carry );
                        return ++instructionAdress;
                    }
                case 0x88: //ADC A,B
                    {
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        UpdateFlagsADC(GameBoy.Cpu.rA, GameBoy.Cpu.rB);
                        
                        GameBoy.Cpu.rA += (byte)(GameBoy.Cpu.rB + carry);
                        return ++instructionAdress;
                    }
                case 0x89: //ADC A,C
                    {
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        UpdateFlagsADC(GameBoy.Cpu.rA, GameBoy.Cpu.rC);
                        
                        GameBoy.Cpu.rA += (byte)(GameBoy.Cpu.rC + carry);
                        return ++instructionAdress;
                    }
                case 0x8A: //ADC A,D
                    {
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        UpdateFlagsADC(GameBoy.Cpu.rA, GameBoy.Cpu.rD);
                        
                        GameBoy.Cpu.rA += (byte)(GameBoy.Cpu.rD + carry);
                        return ++instructionAdress;
                    }
                case 0x8B: //ADC A,E
                    {
                         byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        UpdateFlagsADC(GameBoy.Cpu.rA, GameBoy.Cpu.rE);
                        GameBoy.Cpu.rA += (byte)(GameBoy.Cpu.rE + carry);
                        return ++instructionAdress;
                    }
                case 0x8C: //ADC A,H
                    {
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        UpdateFlagsADC(GameBoy.Cpu.rA, GameBoy.Cpu.rH);
                        GameBoy.Cpu.rA += (byte)(GameBoy.Cpu.rH + carry);
                        return ++instructionAdress;
                    }
                case 0x8D: //ADC A,L
                    {
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        UpdateFlagsADC(GameBoy.Cpu.rA, GameBoy.Cpu.rL);
                        GameBoy.Cpu.rA += (byte)(GameBoy.Cpu.rL + carry);
                        return ++instructionAdress;
                    }
                case 0x8E: //ADC A,(HL)
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        UpdateFlagsADC(GameBoy.Cpu.rA, val);
                        GameBoy.Cpu.rA += (byte)(val + carry);
                        return ++instructionAdress;
                    }
                case 0xCE: //ADC A,#
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        UpdateFlagsADC(GameBoy.Cpu.rA, val);
                        GameBoy.Cpu.rA += (byte)(val + carry);
                        return ++instructionAdress;
                    }
                case 0x97: //SUB A,A
                    {
                        UpdateFlagsSUB(GameBoy.Cpu.rA, GameBoy.Cpu.rA);
                        GameBoy.Cpu.rA -= GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0x90: //SUB A,B
                    {
                        UpdateFlagsSUB(GameBoy.Cpu.rA, GameBoy.Cpu.rB);
                        GameBoy.Cpu.rA -= GameBoy.Cpu.rB;
                        return ++instructionAdress;
                    }
                case 0x91: //SUB A,C
                    {
                        UpdateFlagsSUB(GameBoy.Cpu.rA, GameBoy.Cpu.rC);
                        GameBoy.Cpu.rA -= GameBoy.Cpu.rC;
                        return ++instructionAdress;
                    }
                case 0x92: //SUB A,D
                    {
                        UpdateFlagsSUB(GameBoy.Cpu.rA, GameBoy.Cpu.rD);
                        GameBoy.Cpu.rA -= GameBoy.Cpu.rD;
                        return ++instructionAdress;
                    }
                case 0x93: //SUB A,E
                    {
                        UpdateFlagsSUB(GameBoy.Cpu.rA, GameBoy.Cpu.rE);
                        GameBoy.Cpu.rA -= GameBoy.Cpu.rE;
                        return ++instructionAdress;
                    }
                case 0x94: //SUB A,L
                    {
                        UpdateFlagsSUB(GameBoy.Cpu.rA, GameBoy.Cpu.rL);
                        GameBoy.Cpu.rA -= GameBoy.Cpu.rL;
                        return ++instructionAdress;
                    }
                case 0x95: //SUB A,H
                    {
                        UpdateFlagsSUB(GameBoy.Cpu.rA, GameBoy.Cpu.rH);
                        GameBoy.Cpu.rA -= GameBoy.Cpu.rH;
                        return ++instructionAdress;
                    }
                case 0x96: //SUB A,(HL)
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        UpdateFlagsSUB(GameBoy.Cpu.rA, val);
                        GameBoy.Cpu.rA -= val;
                        return ++instructionAdress;
                    }
                case 0xD6: //SUB A,#
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        UpdateFlagsSUB(GameBoy.Cpu.rA, val);
                        GameBoy.Cpu.rA -= val;
                        return ++instructionAdress;
                    }
                case 0x9F: //SBC A,A
                    {
                        UpdateFlagsSBC(GameBoy.Cpu.rA, GameBoy.Cpu.rA);
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        GameBoy.Cpu.rA -= (byte)(GameBoy.Cpu.rA + carry);
                        return ++instructionAdress;
                    }
                case 0x98: //SBC A,B
                    {
                        UpdateFlagsSBC(GameBoy.Cpu.rA, GameBoy.Cpu.rB);
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        GameBoy.Cpu.rA -= (byte)(GameBoy.Cpu.rB + carry);
                        return ++instructionAdress;
                    }
                case 0x99: //SBC A,C
                    {
                        UpdateFlagsSBC(GameBoy.Cpu.rA, GameBoy.Cpu.rC);
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        GameBoy.Cpu.rA -= (byte)(GameBoy.Cpu.rC + carry);
                        return ++instructionAdress;
                    }
                case 0x9A: //SBC A,D
                    {
                        UpdateFlagsSBC(GameBoy.Cpu.rA, GameBoy.Cpu.rD);
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        GameBoy.Cpu.rA -= (byte)(GameBoy.Cpu.rD + carry);
                        return ++instructionAdress;
                    }
                case 0x9B: //SBC A,E
                    {
                        UpdateFlagsSBC(GameBoy.Cpu.rA, GameBoy.Cpu.rE);
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        GameBoy.Cpu.rA -= (byte)(GameBoy.Cpu.rE + carry);
                        return ++instructionAdress;
                    }
                case 0x9C: //SBC A,H
                    {
                        UpdateFlagsSBC(GameBoy.Cpu.rA, GameBoy.Cpu.rH);
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        GameBoy.Cpu.rA -= (byte)(GameBoy.Cpu.rH + carry);
                        return ++instructionAdress;
                    }
                case 0x9D: //SBC A,L
                    {
                        UpdateFlagsSBC(GameBoy.Cpu.rA, GameBoy.Cpu.rL);
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        GameBoy.Cpu.rA -= (byte)(GameBoy.Cpu.rL + carry);
                        return ++instructionAdress;
                    }
                case 0x9E: //SBC A,(HL)
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        UpdateFlagsSBC(GameBoy.Cpu.rA, val);
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        GameBoy.Cpu.rA -= (byte)(val + carry);
                        return ++instructionAdress;
                    }
                case 0xDE: //SBC A,# 
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        UpdateFlagsSBC(GameBoy.Cpu.rA, val);
                        byte carry = GameBoy.Cpu.CValue ? (byte)0x01 : (byte)0x00;
                        GameBoy.Cpu.rA -= (byte)(val + carry);
                        return ++instructionAdress;

                    }
                case 0xA7: //AND A,A
                    {
                        UpdateFlagsAND(GameBoy.Cpu.rA, GameBoy.Cpu.rA);
                        GameBoy.Cpu.rA &= GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0xA0: //AND A,B
                    {
                        UpdateFlagsAND(GameBoy.Cpu.rA, GameBoy.Cpu.rB);
                        GameBoy.Cpu.rA &= GameBoy.Cpu.rB;
                        return ++instructionAdress;
                    }
                case 0xA1: //AND A,C
                    {
                        UpdateFlagsAND(GameBoy.Cpu.rA, GameBoy.Cpu.rC);
                        GameBoy.Cpu.rA &= GameBoy.Cpu.rC;
                        return ++instructionAdress;
                    }
                case 0xA2: //AND A,D
                    {
                        UpdateFlagsAND(GameBoy.Cpu.rA, GameBoy.Cpu.rD);
                        GameBoy.Cpu.rA &= GameBoy.Cpu.rD;
                        return ++instructionAdress;
                    }
                case 0xA3: //AND A,E
                    {
                        UpdateFlagsAND(GameBoy.Cpu.rA, GameBoy.Cpu.rE);
                        GameBoy.Cpu.rA &= GameBoy.Cpu.rE;
                        return ++instructionAdress;
                    }
                case 0xA4: //AND A,H
                    {
                        UpdateFlagsAND(GameBoy.Cpu.rA, GameBoy.Cpu.rH);
                        GameBoy.Cpu.rA &= GameBoy.Cpu.rH;
                        return ++instructionAdress;
                    }
                case 0xA5: //AND A,L
                    {
                        UpdateFlagsAND(GameBoy.Cpu.rA, GameBoy.Cpu.rL);
                        GameBoy.Cpu.rA &= GameBoy.Cpu.rL;
                        return ++instructionAdress;
                    }
                case 0xA6: //AND A,(HL)
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        UpdateFlagsAND(GameBoy.Cpu.rA, val);
                        GameBoy.Cpu.rA &= val;
                        return ++instructionAdress;
                    }
                case 0xE6: //AND A,#
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        UpdateFlagsAND(GameBoy.Cpu.rA, val);
                        GameBoy.Cpu.rA &= val;
                        return ++instructionAdress;
                    }
                case 0xB7: //OR A,A
                    {
                        UpdateFlagsOR(GameBoy.Cpu.rA, GameBoy.Cpu.rA);
                        GameBoy.Cpu.rA |= GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0xB0: //OR A,B
                    {
                        UpdateFlagsOR(GameBoy.Cpu.rA, GameBoy.Cpu.rB);
                        GameBoy.Cpu.rA |= GameBoy.Cpu.rB;
                        return ++instructionAdress;
                    }
                case 0xB1: //OR A,C
                    {
                        UpdateFlagsOR(GameBoy.Cpu.rA, GameBoy.Cpu.rC);
                        GameBoy.Cpu.rA |= GameBoy.Cpu.rC;
                        return ++instructionAdress;
                    }
                case 0xB2: //OR A,D
                    {
                        UpdateFlagsOR(GameBoy.Cpu.rA, GameBoy.Cpu.rD);
                        GameBoy.Cpu.rA |= GameBoy.Cpu.rD;
                        return ++instructionAdress;
                    }
                case 0xB3: //OR A,E
                    {
                        UpdateFlagsOR(GameBoy.Cpu.rA, GameBoy.Cpu.rE);
                        GameBoy.Cpu.rA |= GameBoy.Cpu.rE;
                        return ++instructionAdress;
                    }
                case 0xB4: //OR A,H
                    {
                        UpdateFlagsOR(GameBoy.Cpu.rA, GameBoy.Cpu.rH);
                        GameBoy.Cpu.rA |= GameBoy.Cpu.rH;
                        return ++instructionAdress;
                    }
                case 0xB5: //OR A,L
                    {
                        UpdateFlagsOR(GameBoy.Cpu.rA, GameBoy.Cpu.rL);
                        GameBoy.Cpu.rA |= GameBoy.Cpu.rL;
                        return ++instructionAdress;
                    }
                case 0xB6: //OR A,(HL)
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        UpdateFlagsOR(GameBoy.Cpu.rA, val);
                        GameBoy.Cpu.rA |= val;
                        return ++instructionAdress;
                    }
                case 0xF6: //OR A,#
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        UpdateFlagsOR(GameBoy.Cpu.rA, val);
                        GameBoy.Cpu.rA |= val;
                        return ++instructionAdress;
                    }
                case 0xAF: //XOR A,A
                    {
                        UpdateFlagsXOR(GameBoy.Cpu.rA, GameBoy.Cpu.rA);
                        GameBoy.Cpu.rA ^= GameBoy.Cpu.rA;
                        return ++instructionAdress;
                    }
                case 0xA8: //XOR A,B
                    {
                        UpdateFlagsXOR(GameBoy.Cpu.rA, GameBoy.Cpu.rB);
                        GameBoy.Cpu.rA ^= GameBoy.Cpu.rB;
                        return ++instructionAdress;
                    }
                case 0xA9: //XOR A,C
                    {
                        UpdateFlagsXOR(GameBoy.Cpu.rA, GameBoy.Cpu.rC);
                        GameBoy.Cpu.rA ^= GameBoy.Cpu.rC;
                        return ++instructionAdress;
                    }
                case 0xAA: //XOR A,D
                    {
                        UpdateFlagsXOR(GameBoy.Cpu.rA, GameBoy.Cpu.rD);
                        GameBoy.Cpu.rA ^= GameBoy.Cpu.rD;
                        return ++instructionAdress;
                    }
                case 0xAB: //XOR A,E
                    {
                        UpdateFlagsXOR(GameBoy.Cpu.rA, GameBoy.Cpu.rE);
                        GameBoy.Cpu.rA ^= GameBoy.Cpu.rE;
                        return ++instructionAdress;
                    }
                case 0xAC: //XOR A,H
                    {
                        UpdateFlagsXOR(GameBoy.Cpu.rA, GameBoy.Cpu.rH);
                        GameBoy.Cpu.rA ^= GameBoy.Cpu.rH;
                        return ++instructionAdress;
                    }
                case 0xAD: //XOR A,L
                    {
                        UpdateFlagsXOR(GameBoy.Cpu.rA, GameBoy.Cpu.rL);
                        GameBoy.Cpu.rA ^= GameBoy.Cpu.rL;
                        return ++instructionAdress;
                    }
                case 0xAE: //XOR A,(HL)
                    {
                        //instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        UpdateFlagsXOR(GameBoy.Cpu.rA, val);
                        GameBoy.Cpu.rA ^= val;
                        return ++instructionAdress;
                    }
                case 0xEE: //XOR A,#
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        UpdateFlagsXOR(GameBoy.Cpu.rA, val);
                        GameBoy.Cpu.rA ^= val;
                        return ++instructionAdress;
                    }
                case 0xBF: //CP A,A
                    {
                        UpdateFlagsCP(GameBoy.Cpu.rA, GameBoy.Cpu.rA);
                        return ++instructionAdress;
                    }
                case 0xB8: //CP A,B
                    {
                        UpdateFlagsCP(GameBoy.Cpu.rA, GameBoy.Cpu.rB);
                        return ++instructionAdress;
                    }
                case 0xB9: //CP A,C
                    {
                        UpdateFlagsCP(GameBoy.Cpu.rA, GameBoy.Cpu.rC);
                        return ++instructionAdress;
                    }
                case 0xBA: //CP A,D
                    {
                        UpdateFlagsCP(GameBoy.Cpu.rA, GameBoy.Cpu.rD);
                        return ++instructionAdress;
                    }
                case 0xBB: //CP A,E
                    {
                        UpdateFlagsCP(GameBoy.Cpu.rA, GameBoy.Cpu.rE);
                        return ++instructionAdress;
                    }
                case 0xBC: //CP A,H
                    {
                        UpdateFlagsCP(GameBoy.Cpu.rA, GameBoy.Cpu.rH);
                        return ++instructionAdress;
                    }
                case 0xBD: //CP A,L
                    {
                        UpdateFlagsCP(GameBoy.Cpu.rA, GameBoy.Cpu.rL);
                        return ++instructionAdress;
                    }
                case 0xBE: //CP A,(HL)
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        UpdateFlagsCP(GameBoy.Cpu.rA, val);
                        return ++instructionAdress;
                    }
                case 0xFE: //CP A,#
                    {
                        instructionAdress++;
                        byte val = GameBoy.Ram.ReadByteAt(instructionAdress);
                        UpdateFlagsCP(GameBoy.Cpu.rA, val);
                        return ++instructionAdress;
                    }
                case 0x3C: //INC A
                    {
                        UpdateFlagsINC(GameBoy.Cpu.rA);
                        GameBoy.Cpu.rA += 0x01;
                        return ++instructionAdress;
                    }
                case 0x04: //INC B
                    {
                        UpdateFlagsINC(GameBoy.Cpu.rB);
                        GameBoy.Cpu.rB += 0x01;
                        return ++instructionAdress;
                    }
                case 0x0C: //INC C
                    {
                        UpdateFlagsINC(GameBoy.Cpu.rC);
                        GameBoy.Cpu.rC += 0x01;
                        return ++instructionAdress;
                    }
                case 0x14: //INC D
                    {
                        UpdateFlagsINC(GameBoy.Cpu.rD);
                        GameBoy.Cpu.rD += 0x01;
                        return ++instructionAdress;
                    }
                case 0x1C: //INC E
                    {
                        UpdateFlagsINC(GameBoy.Cpu.rE);
                        GameBoy.Cpu.rE += 0x01;
                        return ++instructionAdress;
                    }
                case 0x24: //INC H
                    {
                        UpdateFlagsINC(GameBoy.Cpu.rH);
                        GameBoy.Cpu.rH += 0x01;
                        return ++instructionAdress;
                    }
                case 0x2C: //INC L
                    {
                        UpdateFlagsINC(GameBoy.Cpu.rL);
                        GameBoy.Cpu.rL += 0x01;
                        return ++instructionAdress;
                    }
                case 0x34: //INC (HL)
                    {
                        byte val = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        UpdateFlagsINC(val);
                        val += 0x01;
                        GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, val);
                        return ++instructionAdress;
                    }
                case 0x3D: //DEC A
                    {
                        UpdateFlagsDEC(GameBoy.Cpu.rA);
                        GameBoy.Cpu.rA -= 0x01;
                        return ++instructionAdress;
                    }
                case 0x05: //DEC B
                    {
                        UpdateFlagsDEC(GameBoy.Cpu.rB);
                        GameBoy.Cpu.rB -= 0x01;
                        return ++instructionAdress;
                    }
                case 0x0D: //DEC C
                    {
                        UpdateFlagsDEC(GameBoy.Cpu.rC);
                        GameBoy.Cpu.rC -= 0x01;
                        return ++instructionAdress;
                    }
                case 0x15: //DEC D
                    {
                        UpdateFlagsDEC(GameBoy.Cpu.rD);
                        GameBoy.Cpu.rD -= 0x01;
                        return ++instructionAdress;
                    }
                case 0x1D: //DEC E
                    {
                        UpdateFlagsDEC(GameBoy.Cpu.rE);
                        GameBoy.Cpu.rE -= 0x01;
                        return ++instructionAdress;
                    }
                case 0x25: //DEC H
                    {
                        UpdateFlagsDEC(GameBoy.Cpu.rH);
                        GameBoy.Cpu.rH -= 0x01;
                        return ++instructionAdress;
                    }
                case 0x2D: //DEC L
                    {
                        UpdateFlagsDEC(GameBoy.Cpu.rL);
                        GameBoy.Cpu.rL -= 0x01;
                        return ++instructionAdress;
                    }
                case 0x35: //DEC (HL)
                    {
                        byte val = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.rHL);
                        UpdateFlagsDEC(val);
                        val -= 0x01;
                        GameBoy.Ram.WriteAt(GameBoy.Cpu.rHL, val);
                        return ++instructionAdress;
                    }
                default:
                    {
                        return 0x00;
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
                case 0x87: //ADD A,A
                    {
                        outS = "add a,a";
                    }
                    break;
                case 0x80: //ADD A,B
                    {
                        outS = "add a,b";
                    }
                    break;
                case 0x81: //ADD A,C
                    {
                        outS = "add a,c";
                    }
                    break;
                case 0x82: //ADD A,D
                    {
                        outS = "add a,d";
                    }
                    break;
                case 0x83: //ADD A,E
                    {
                        outS = "add a,e";
                    }
                    break;
                case 0x84: //ADD A,H
                    {
                        outS = "add a,h";
                    }
                    break;
                case 0x85: //ADD A,L
                    {
                        outS = "add a,l";
                    }
                    break;
                case 0x86: //ADD A,(HL)
                    {
                        outS = "add a,hl";
                    }
                    break;
                case 0xC6: //ADD A,#
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte val = GameBoy.Ram.ReadByteAt(i);
                        outS = "add a," + string.Format("{0:x2}", val);
                    }
                    break;
                case 0x8F: //ADC A,A
                    {
                        outS = "adc a,a";
                    }
                    break;
                case 0x88: //ADC A,B
                    {
                        outS = "adc a,b";
                    }
                    break;
                case 0x89: //ADC A,C
                    {
                        outS = "adc a,c";
                    }
                    break;
                case 0x8A: //ADC A,D
                    {
                        outS = "adc a,d";
                    }
                    break;
                case 0x8B: //ADC A,E
                    {
                        outS = "adc a,e";
                    }
                    break;
                case 0x8C: //ADC A,H
                    {
                        outS = "adc a,h";
                    }
                    break;
                case 0x8D: //ADC A,L
                    {
                        outS = "adc a,l";
                    }
                    break;
                case 0x8E: //ADC A,(HL)
                    {
                        outS = "adc a,(hl)";
                    }
                    break;
                case 0xCE: //ADC A,#
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte val = GameBoy.Ram.ReadByteAt(i);
                        outS = "adc a," + string.Format("{0:x2}", val);
                    }
                    break;
                case 0x97: //SUB A,A
                    {
                        outS = "sub a,a";
                    }
                    break;
                case 0x90: //SUB A,B
                    {
                        outS = "sub a,b";
                    }
                    break;
                case 0x91: //SUB A,C
                    {
                        outS = "sub a,c";
                    }
                    break;
                case 0x92: //SUB A,D
                    {
                        outS = "sub a,d";
                    }
                    break;
                case 0x93: //SUB A,E
                    {
                        outS = "sub a,e";
                    }
                    break;
                case 0x94: //SUB A,F
                    {
                        outS = "sub a,f";
                    }
                    break;
                case 0x95: //SUB A,L
                    {
                        outS = "sub a,l";
                    }
                    break;
                case 0x96: //SUB A,(HL)
                    {
                        outS = "sub a,(hl)";
                    }
                    break;
                case 0xD6: //SUB A,#
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte val = GameBoy.Ram.ReadByteAt(i);
                        outS = "sub a," + string.Format("{0:x2}", val);
                    }
                    break;
                case 0x9F: //SBC A,A
                    {
                        outS = "sbc a,a";
                    }
                    break;
                case 0x98: //SBC A,B
                    {
                        outS = "sbc a,b";
                    }
                    break;
                case 0x99: //SBC A,C
                    {
                        outS = "sbc a,c";
                    }
                    break;
                case 0x9A: //SBC A,D
                    {
                        outS = "sbc a,d";
                    }
                    break;
                case 0x9B: //SBC A,E
                    {
                        outS = "sbc a,e";
                    }
                    break;
                case 0x9C: //SBC A,H
                    {
                        outS = "sbc a,h";
                    }
                    break;
                case 0x9D: //SBC A,L
                    {
                        outS = "sbc a,l";
                    }
                    break;
                case 0x9E: //SBC A,(HL)
                    {
                        outS = "sbc a,(hl)";
                    }
                    break;
                //SBC A,#   ??
                case 0xA7: //AND A,A
                    {
                        outS = "and a,a";
                    }
                    break;
                case 0xA0: //AND A,B
                    {
                        outS = "and a,b";
                    }
                    break;
                case 0xA1: //AND A,C
                    {
                        outS = "and a,c";
                    }
                    break;
                case 0xA2: //AND A,D
                    {
                        outS = "and a,d";
                    }
                    break;
                case 0xA3: //AND A,E
                    {
                        outS = "and a,e";
                    }
                    break;
                case 0xA4: //AND A,H
                    {
                        outS = "and a,h";
                    }
                    break;
                case 0xA5: //AND A,L
                    {
                        outS = "and a,l";
                    }
                    break;
                case 0xA6: //AND A,(HL)
                    {
                        outS = "and a,(hl)";
                    }
                    break;
                case 0xE6: //AND A,#
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte val = GameBoy.Ram.ReadByteAt(i);
                        outS = "and a," + string.Format("{0:x2}", val);
                    }
                    break;
                case 0xB7: //OR A,A
                    {
                        outS = "or a,a";
                    }
                    break;
                case 0xB0: //OR A,B
                    {
                        outS = "or a,b";
                    }
                    break;
                case 0xB1: //OR A,C
                    {
                        outS = "or a,c";
                    }
                    break;
                case 0xB2: //OR A,D
                    {
                        outS = "or a,d";
                    }
                    break;
                case 0xB3: //OR A,E
                    {
                        outS = "or a,e";
                    }
                    break;
                case 0xB4: //OR A,H
                    {
                        outS = "or a,h";
                    }
                    break;
                case 0xB5: //OR A,L
                    {
                        outS = "or a,l";
                    }
                    break;
                case 0xB6: //OR A,(HL)
                    {
                        outS = "or a,(hl)";
                    }
                    break;
                case 0xF6: //OR A,#
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte val = GameBoy.Ram.ReadByteAt(i);
                        outS = "or a," + string.Format("{0:x2}", val);
                    }
                    break;
                case 0xAF: //XOR A,A
                    {
                        outS = "xor a,a";
                    }
                    break;
                case 0xA8: //XOR A,B
                    {
                        outS = "xor a,b";
                    }
                    break;
                case 0xA9: //XOR A,C
                    {
                        outS = "xor a,c";
                    }
                    break;
                case 0xAA: //XOR A,D
                    {
                        outS = "xor a,d";
                    }
                    break;
                case 0xAB: //XOR A,E
                    {
                        outS = "xor a,e";
                    }
                    break;
                case 0xAC: //XOR A,H
                    {
                        outS = "xor a,h";
                    }
                    break;
                case 0xAD: //XOR A,L
                    {
                        outS = "xor a,l";
                    }
                    break;
                case 0xAE: //XOR A,(HL)
                    {
                        outS = "xor a,(hl)";
                    }
                    break;
                case 0xEE: //XOR A,#
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte val = GameBoy.Ram.ReadByteAt(i);
                        outS = "xor a," + string.Format("{0:x2}", val);
                    }
                    break;
                case 0xBF: //CP A,A
                    {
                        outS = "cp a,a";
                    }
                    break;
                case 0xB8: //CP A,B
                    {
                        outS = "cp a,b";
                    }
                    break;
                case 0xB9: //CP A,C
                    {
                        outS = "cp a,c";
                    }
                    break;
                case 0xBA: //CP A,D
                    {
                        outS = "cp a,d";
                    }
                    break;
                case 0xBB: //CP A,E
                    {
                        outS = "cp a,e";
                    }
                    break;
                case 0xBC: //CP A,H
                    {
                        outS = "cp a,h";
                    }
                    break;
                case 0xBD: //CP A,L
                    {
                        outS = "cp a,l";
                    }
                    break;
                case 0xBE: //CP A,(HL)
                    {
                        outS = "cp a,(hl)";
                    }
                    break;
                case 0xFE: //CP A,#
                    {
                        ushort i = instructionAdress;
                        i++;
                        byte val = GameBoy.Ram.ReadByteAt(i);
                        outS = "cp a," + string.Format("{0:x2}", val);
                    }
                    break;
                case 0x3C: //INC A
                    {
                        outS = "inc a";
                    }
                    break;
                case 0x04: //INC B
                    {
                        outS = "inc b";
                    }
                    break;
                case 0x0C: //INC C
                    {
                        outS = "inc c";
                    }
                    break;
                case 0x14: //INC D
                    {
                        outS = "inc d";
                    }
                    break;
                case 0x1C: //INC E
                    {
                        outS = "inc e";
                    }
                    break;
                case 0x24: //INC H
                    {
                        outS = "inc f";
                    }
                    break;
                case 0x2C: //INC L
                    {
                        outS = "inc l";
                    }
                    break;
                case 0x34: //INC (HL)
                    {
                        outS = "inc (hl)";
                    }
                    break;
                case 0x3D: //DEC A
                    {
                        outS = "dec a";
                    }
                    break;
                case 0x05: //DEC B
                    {
                        outS = "dec b";
                    }
                    break;
                case 0x0D: //DEC C
                    {
                        outS = "dec c";
                    }
                    break;
                case 0x15: //DEC D
                    {
                        outS = "dec d";
                    }
                    break;
                case 0x1D: //DEC E
                    {
                        outS = "dec e";
                    }
                    break;
                case 0x25: //DEC H
                    {
                        outS = "dec h";
                    }
                    break;
                case 0x2D: //DEC L
                    {
                        outS = "dec l";
                    }
                    break;
                case 0x35: //DEC (HL)
                    {
                        outS = "dec (hl)";
                    }
                    break;
                default:
                    {
                        outS = "add error";
                    }
                    break;
            }
            return outS;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void UpdateFlagsADD( byte r1, byte r2)
        {
            ulong res = (ulong)((ushort)r1 + (ushort)r2);
            GameBoy.Cpu.ZValue = ((byte)res == 0x00);
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = GameBoy.Cpu.ComputeHCarry( r1, (ushort)res, false);
            GameBoy.Cpu.CValue = res > 0xFF;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void UpdateFlagsADC(byte r1, byte r2)
        {
            ulong res = (ulong)((ushort)r1 + (ushort)r2);
            GameBoy.Cpu.ZValue = ((byte)res == 0x00);
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = GameBoy.Cpu.ComputeHCarry(r1, (ushort)res, false);
            GameBoy.Cpu.CValue = res > 0xFF;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void UpdateFlagsSUB(byte r1, byte r2)
        {
            long res = (long)((ushort)r1 - (ushort)r2);
            GameBoy.Cpu.ZValue = ((byte)res == 0x00);
            GameBoy.Cpu.NValue = true;
            GameBoy.Cpu.HValue = GameBoy.Cpu.ComputeHCarry(r1, (ushort)res, true);
            GameBoy.Cpu.CValue = r1 < r2;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void UpdateFlagsSBC(byte r1, byte r2)
        {
            long res = (long)((ushort)r1 - (ushort)r2);
            GameBoy.Cpu.ZValue = ((byte)res == 0x00);
            GameBoy.Cpu.NValue = true;
            GameBoy.Cpu.HValue = GameBoy.Cpu.ComputeHCarry(r1, (ushort)res, true);
            GameBoy.Cpu.CValue = r1 < r2;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void UpdateFlagsAND(byte r1, byte r2)
        {
            GameBoy.Cpu.ZValue = ((r1 & r2) == 0);
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = true;
            GameBoy.Cpu.CValue = false;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void UpdateFlagsOR(byte r1, byte r2)
        {
            GameBoy.Cpu.ZValue = (r1 | r2) ==0;
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = false;
            GameBoy.Cpu.CValue = false;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void UpdateFlagsXOR(byte r1, byte r2)
        {
            GameBoy.Cpu.ZValue = (r1 ^ r2) == 0;
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = false;
            GameBoy.Cpu.CValue = false;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void UpdateFlagsCP(byte r1, byte r2)
        {
            long res = (long)((ushort)r1 - (ushort)r2);
            GameBoy.Cpu.ZValue = ((byte)res == 0x00);
            GameBoy.Cpu.NValue = true;
            GameBoy.Cpu.HValue = GameBoy.Cpu.ComputeHCarry(r1, (ushort)res, true);
            GameBoy.Cpu.CValue = r1 < r2;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void UpdateFlagsINC(byte r)
        {
            ulong ival = (ulong)(r);
            ulong val = (ulong)(r);
            val += 0x01;
            GameBoy.Cpu.ZValue = ((byte)val == 0x00);
            GameBoy.Cpu.NValue = false;
            ushort val2 = (ushort)(val & 0x0F);
            GameBoy.Cpu.HValue = GameBoy.Cpu.ComputeHCarry((ushort)ival, (ushort)val, false); // (val2 == 0x00 && ival >= 0x0F);
            if (GameBoy.Cpu.ZValue)
            {
                int u = 0;
                u = 1;
                //GameBoy.Cpu.Stop();
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void UpdateFlagsDEC(byte r)
        {
            ulong ival = (ulong)(r);
            ulong val = ival - 0x01;
            GameBoy.Cpu.ZValue = ((byte)val == 0x00);
            GameBoy.Cpu.NValue = (val <= 0x0F);
            GameBoy.Cpu.HValue = GameBoy.Cpu.ComputeHCarry((ushort)ival, (ushort)val, true);
        }
    }
}
