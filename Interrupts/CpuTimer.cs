using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80
{
    class CpuTimer
    {

        /// <summary>
        /// ///////////////////////////////////////////////////////////////
        /// </summary>
        /// 

        public const int _DIV       = 0xFF04;   //Timer counter
        public const int _TIMA      = 0xFF05;   //Timer counter
        public const int _TMA       = 0xFF06;   //Timer Modulo
        public const int _TAC       = 0xFF07;   //Timer control

        public const int _DIVIDER_SPEED_HZ = 16384;
        private bool m_timerWorking = false;

        private const int _4096Hz_to_microsecond = 256;     // 244.140625 µs(p) 
        private const int _262144Hz_to_microsecond = 4;     // 3.814697265625 µs(p) 
        private const int _65536Hz_to_microsecond = 16;     // 15.2587890625 µs(p) 
        private const int _16384Hz_to_microsecond = 64;     // 61.03515625 µs(p) 

        private long m_timerSpeed = 0;
        private long m_timerOffset = 0;
        private long m_tick = 0;
		private byte m_previousb = 0;
        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public CpuTimer()
        {
            m_timerWorking = false;
            m_timerSpeed = 0;
            m_tick = 0;
        }

        static bool bstartedOnce = false;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Update()
        {
            m_tick++;
            if (m_tick % _16384Hz_to_microsecond == 0)
            {
                IncDivider();
            }
            bool bTimerWorking = m_timerWorking;
            ReadTimerSetup();
            //timer just started
            if (m_timerWorking && bTimerWorking != m_timerWorking)
            {
                bstartedOnce = true;
                m_timerOffset = m_tick;
            }
            long t = (m_tick - m_timerOffset);
            t += 1;
            if (m_timerWorking && t % m_timerSpeed == 0)
            {
                IncTimer();
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void IncDivider()
        {
            GameBoy.Ram.IncDivider();
        }

        private void IncTimer()
        {
            //reload timer setup
            //ReadTimerSetup();
            //update clock speed and timer start
            byte b;
            b = GameBoy.Ram.ReadByteAt(_TIMA);
            b = (byte)(b + 0x01);
			GameBoy.Ram.WriteAt(_TIMA, b);
			if (b == 0)
			{
				EnableTimerInterrupt();
				ReloadTMA();
			}
			m_previousb = b;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void ReadTimerSetup()
        {
            byte b = GameBoy.Ram.ReadByteAt(_TAC);
            bool started = (b & 0x04) != 0x0;
            int speedCase = (b & 0x03);
            switch (speedCase)
            {
                case 0x00:
                    {
                        m_timerSpeed = _4096Hz_to_microsecond;
                        break;
                    }
                case 0x01:
                    {
                        m_timerSpeed = _262144Hz_to_microsecond;
                        break;
                    }
                case 0x02:
                    {
                        m_timerSpeed = _65536Hz_to_microsecond;
                        break;
                    }
                case 0x03:
                    {
                        m_timerSpeed = _16384Hz_to_microsecond;
                        break;
                    }
                default:
                    {
                        m_timerSpeed = 0;
                        break;
                    }
            }
            if (bstartedOnce && started == false)
            {
                byte by = GameBoy.Cpu.rA;
                int y = 0;
                y++;
            }
            m_timerWorking = started;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void ReloadTMA()
        {
            byte modulo = GameBoy.Ram.ReadByteAt(_TMA);
			byte b = GameBoy.Ram.ReadByteAt(_TIMA);

			b += modulo;
            GameBoy.Ram.WriteAt(_TIMA, b);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void EnableTimerInterrupt()
        {
            byte b = GameBoy.Ram.ReadByteAt(0xFF0F);
            b |= 0x04;
            GameBoy.Ram.WriteByte(0xFF0F, b);
        }
    }
}
