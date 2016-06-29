using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBoyTest.Debug;

namespace GameBoyTest.Z80
{
    /*
        FF0F - IF - Interrupt Flag (R/W)
        Bit 0: V-Blank Interrupt Request (INT 40h) (1=Request) Bit 1: LCD STAT Interrupt Request
        (INT 48h) (1=Request) Bit 2: Timer Interrupt Request (INT 50h) (1=Request) Bit 3: Serial
        Interrupt Request (INT 58h) (1=Request) Bit 4: Joypad Interrupt Request (INT 60h)
        (1=Request)
        When an interrupt signal changes from low to high, then the corresponding bit in the IF
        register becomes set. Fo 
     */
    
        /*
         * FFFF - IE - Interrupt Enable (R/W)
    Bit 0: V-Blank Interrupt Enable (INT 40h) (1=Enable) Bit 1: LCD STAT Interrupt Enable
    (INT 48h) (1=Enable) Bit 2: Timer Interrupt Enable (INT 50h) (1=Enable) Bit 3: Serial
    Interrupt Enable (INT 58h) (1=Enable) Bit 4: Joypad Interrupt Enable (INT 60h)
    (1=Enable)
         * */

    class InterruptsMgr
    {
        enum e_interrupt
        {
            e_VBLANK = 0,
            e_LCD_STAT,
            e_Timer,
            e_Serial,
            e_Joypad,
            __max_interrupts__,
        }

        private const int INTERRUPT_NB_CYCLES = 12;

        private byte[] e_interrupts_bytes;
        private byte[] e_interrupts_startAdr; 

        private ushort m_IME_adress = 0xFFFF;
        private ushort m_IF_adress = 0xFF0F;

        private bool m_interruptStarted = false;
        private int m_interruptCurCycle = 0;


        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public InterruptsMgr()
        {
            e_interrupts_bytes = new byte[(int)e_interrupt.__max_interrupts__];
            e_interrupts_bytes[(int)e_interrupt.e_VBLANK]   = 0;
            e_interrupts_bytes[(int)e_interrupt.e_LCD_STAT] = 1;
            e_interrupts_bytes[(int)e_interrupt.e_Timer]    = 2;
            e_interrupts_bytes[(int)e_interrupt.e_Serial]   = 3;
            e_interrupts_bytes[(int)e_interrupt.e_Joypad]   = 4;

            e_interrupts_startAdr = new byte[(int)e_interrupt.__max_interrupts__];
            e_interrupts_startAdr[(int)e_interrupt.e_VBLANK]    = 0x0040;
            e_interrupts_startAdr[(int)e_interrupt.e_LCD_STAT]  = 0x0048;
            e_interrupts_startAdr[(int)e_interrupt.e_Timer]     = 0x0050;
            e_interrupts_startAdr[(int)e_interrupt.e_Serial]    = 0x0058;
            e_interrupts_startAdr[(int)e_interrupt.e_Joypad]    = 0x0060;
            m_interruptStarted = false;
            m_interruptCurCycle = 0;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private bool IsEnabled( e_interrupt inter )
        {
            //return true;
            byte b = GameBoy.Ram.ReadByteAt(m_IME_adress);
            return (b & (0x01 << e_interrupts_bytes[(int)inter])) != 0;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private bool IsRequested(e_interrupt inter)
        {
            byte b = GameBoy.Ram.ReadByteAt(m_IF_adress);
            return (b & (0x01 << e_interrupts_bytes[(int)inter])) != 0;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void Reset_IME()
        {
            GameBoy.Cpu.DisableInterrupts();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void Reset_IF( e_interrupt inter )
        {
            byte b = GameBoy.Ram.ReadByteAt(m_IF_adress);
            byte c = (byte)(~( 0x01 << e_interrupts_bytes[(int)inter] ));
            b = (byte)( b & c );
            GameBoy.Ram.WriteAt(m_IF_adress, b);
            //GameBoy.Ram.WriteAt(m_IF_adress, 0);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public bool CheckInterruptOccured()
        {
            for (int i = 0; i < (int)e_interrupt.__max_interrupts__; i++)
            {
                if (IsEnabled((e_interrupt)i) && IsRequested((e_interrupt)i))
                {
                    return true;
                }
            }
            return false;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public bool Update()
        {
            if (!m_interruptStarted)
            {
                if ( GameBoy.Cpu.IMEStatus() )
                {
                    for (int i = 0; i < (int)e_interrupt.__max_interrupts__; i++ )
                    {
                        if (IsEnabled((e_interrupt)i) && IsRequested((e_interrupt)i))
                        {
                            Reset_IF((e_interrupt)i);
                            Reset_IME();
                            //save current PC adress
                            GameBoy.Cpu.SP -= 0x02;
                            GameBoy.Ram.WriteUshortAt(GameBoy.Cpu.SP, GameBoy.Cpu.PC);
                    
                            //jump to interrupt adress
                            GameBoy.Cpu.PC = e_interrupts_startAdr[i];
                            if (DebugFunctions.CallStackForm() != null)
                            {
                                DebugFunctions.CallStackForm().AddInterruptToCallstack(i);
                            }
                            if (GameBoy.Cpu.IsHalted())
                            {
                                GameBoy.Cpu.ResumeFromHalt();
                            }
                            m_interruptStarted = true;
                            m_interruptCurCycle = 4;
                            return true;
                        }
                    }
                }
            }
            else
            {
                m_interruptCurCycle += 4;
                if (m_interruptCurCycle >= INTERRUPT_NB_CYCLES)
                {
                    m_interruptStarted = false;
                    m_interruptCurCycle = 0;
                    return true;
                }
            }
            return false;
        }
    }
}
