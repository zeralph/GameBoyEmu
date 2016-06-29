using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBoyTest.Debug;

using GameBoyTest.Z80.Z80Instructions.ALU;
using GameBoyTest.Z80.Z80Instructions.LD;
using GameBoyTest.Z80.Z80Instructions.MISC;
using GameBoyTest.Z80.Z80Instructions.ROTATE_SHIFT;
using GameBoyTest.Z80.Z80Instructions.BIT;
using GameBoyTest.Z80.Z80Instructions.JUMP;
using GameBoyTest.Z80.Z80Instructions.CALL;
using GameBoyTest.Z80.Z80Instructions.RESTART;
using GameBoyTest.Z80.Z80Instructions.RETURN;

namespace GameBoyTest.Z80
{
    public class Z80InstructionDecoder
    {
        Z80Instruction[]    m_instructions;
        Z80Instruction[]    m_BCInstruction;

        public Z80InstructionDecoder()
        {
            m_instructions = new Z80Instruction[0x100];
            m_BCInstruction = new Z80Instruction[0x100];
            for (byte i = 0; i < 0xFF; i++)
            {
                m_instructions[i] = null;
                m_BCInstruction[i] = null;
            }
            //LD8
            Z80Instruction_LD_8bit ld8 = new Z80Instruction_LD_8bit();
            RegisterInstruction(ld8);
            Z80Instruction_LD_16bit ld16 = new Z80Instruction_LD_16bit();
            RegisterInstruction(ld16);
            //ALU
            Z80Instruction_ALU_8bit alu8 = new Z80Instruction_ALU_8bit();
            RegisterInstruction(alu8);
            Z80Instruction_ALU_16bit alu16 = new Z80Instruction_ALU_16bit();
            RegisterInstruction(alu16);
            //MISC
            Z80Instruction_CCF ccf = new Z80Instruction_CCF();
            RegisterInstruction(ccf);
            Z80Instruction_CPL cpl = new Z80Instruction_CPL();
            RegisterInstruction(cpl);
            Z80Instruction_DAA daa = new Z80Instruction_DAA();
            RegisterInstruction(daa);
            Z80Instruction_DI di = new Z80Instruction_DI();
            RegisterInstruction(di);
            Z80Instruction_EI ei = new Z80Instruction_EI();
            RegisterInstruction(ei);
            Z80Instruction_HALT halt = new Z80Instruction_HALT();
            RegisterInstruction(halt);
            Z80Instruction_NOP nop = new Z80Instruction_NOP();
            RegisterInstruction(nop);
            Z80Instruction_SCF scf = new Z80Instruction_SCF();
            RegisterInstruction(scf);
            Z80Instruction_STOP stop = new Z80Instruction_STOP();
            RegisterInstruction(stop);
            Z80Instruction_SWAP swap = new Z80Instruction_SWAP();
            RegisterInstruction(swap);
            //ROTATE/SHIFT
            Z80Instruction_ROTATE rot = new Z80Instruction_ROTATE();
            RegisterInstruction(rot);
            Z80Instruction_ROTATE_CB rot_cb = new Z80Instruction_ROTATE_CB();
            RegisterInstruction(rot_cb);
            //BIT
            Z80Instruction_BIT_b_r bit_b_r = new Z80Instruction_BIT_b_r();
            RegisterInstruction(bit_b_r);
            Z80Instruction_RES_b_r res_b_r = new Z80Instruction_RES_b_r();
            RegisterInstruction(res_b_r);
            Z80Instruction_SET_b_r set_b_r = new Z80Instruction_SET_b_r();
            RegisterInstruction(set_b_r);
            //JUMP
            Z80Instruction_JUMP jump = new Z80Instruction_JUMP();
            RegisterInstruction(jump);
            //CALL
            Z80Instruction_CALL call = new Z80Instruction_CALL();
            RegisterInstruction(call);
            //RESTART
            Z80Instruction_RST rst = new Z80Instruction_RST();
            RegisterInstruction(rst);
            //RETURN
            Z80Instruction_RET ret = new Z80Instruction_RET();
            RegisterInstruction(ret);

        }

        public Z80Instruction GetInstructionAt( ushort adr, bool bIsCB )
        {
            ushort myPC = adr;
            if (bIsCB ) 
            {
                //myPC++;
                byte opcode = GameBoy.Ram.ReadByteAt( myPC );
                Z80Instruction inst = m_BCInstruction[opcode];
                if(inst == null)
                { 
                    int u = 0;
                    u++;
                }
                return inst;
            }
            else
            {
                byte opcode = GameBoy.Ram.ReadByteAt( myPC );
                Z80Instruction inst = m_instructions[opcode];
                if (inst == null)
                {
                    int u = 0;
                    u++;
                }
                return inst;
            }
        }

        public Z80Instruction GetNextInstruction()
        {
            if (GameBoy.Ram.ReadByteAt(GameBoy.Cpu.PC) == 0xCB) 
            {
                GameBoy.Cpu.PC++;
                byte opcode = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.PC);
                Z80Instruction inst = m_BCInstruction[opcode];
                return inst;
            }
            else
            {
                byte opcode = GameBoy.Ram.ReadByteAt(GameBoy.Cpu.PC);
                Z80Instruction inst = m_instructions[opcode];
                return inst;
            }
        }

        private void RegisterInstruction(Z80Instruction inst)
        {
            byte[] opcodes = inst.opCodes;
            if (!inst.isBC)
            {
                for (byte i = 0; i < opcodes.Length; i++)
                {
                    DebugFunctions.ASSERT(m_instructions[opcodes[i]] == null, "Overwriting instruction " + String.Format("{0:x2}",opcodes[i])  + " by " + inst.ToString());
                    m_instructions[opcodes[i]] = inst;
                }
            }
            else
            {
                for (byte i = 0; i < opcodes.Length; i++)
                {
                    DebugFunctions.ASSERT(m_BCInstruction[opcodes[i]] == null, "Overwriting instruction !");
                    m_BCInstruction[opcodes[i]] = inst;
                }
            }
        }
    }
}
