using GameBoyTest.Z80;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoyTest.Audio
{
    public class SoundManager:IDisposable
    {
        public enum eSoundChannel
        {
            e_soundChannel_none = 0,
            e_soundChannel_1,
            e_soundChannel_2,
            e_soundChannel_3,
            e_soundChannel_4,
        }

        //sound01
        private Sound1 m_sound1;
        private Sound2 m_sound2;
        private Sound3 m_sound3;
        private Sound4 m_sound4;
        private long m_tickCounter=0;

        private bool m_sound01Enable = true;
        private bool m_sound02Enable = true;
        private bool m_sound03Enable = true;
        private bool m_sound04Enable = true;

        public SoundManager()
        {
            m_sound1 = new Sound1();
            m_sound2 = new Sound2();
            m_sound3 = new Sound3();
            m_sound4 = new Sound4();
        }

        public void Dispose()
        {
            m_sound1.Dispose();
            m_sound2.Dispose();
            m_sound3.Dispose();
            m_sound4.Dispose();
        }

        public void Start()
        {
            m_sound1.Start();
            m_sound2.Start();
            m_sound3.Start();
            m_sound4.Start();
        }

        public void Init()
        {
            m_sound1.Init();
            m_sound2.Init();
            m_sound3.Init();
            m_sound4.Init();
        }

        public void Stop()
        {
            m_sound1.Stop();
            m_sound2.Stop();
            m_sound3.Stop();
            m_sound4.Stop();
        }

        public void EnablSwitchChannelEnable(int channel)
        {
            switch (channel)
            {
                case 0:
                    {
                        m_sound01Enable = !m_sound01Enable;
                        break;
                    }
                case 1:
                    {
                        m_sound02Enable = !m_sound02Enable;
                        break;
                    }
                case 2:
                    {
                        m_sound03Enable = !m_sound03Enable;
                        break;
                    }
              case 3:
                    {
                        m_sound04Enable = !m_sound04Enable;
                        break;
                    }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Update()
        {
            m_tickCounter++;
            byte b = GameBoy.Ram.ReadByteAt(0xFF26);
            bool bIsRunning = GameBoy.Cpu.running;
            bool bMasterOn = (b & 0x80) != 0;

            byte nr52 = GameBoy.Ram.ReadByteAt(0xFF26);
            bool bIsSoundOn = (nr52 & 0x80) != 0;

            if( bIsSoundOn )
            {
                m_sound1.Start();
                m_sound2.Start();
                m_sound3.Start();
                m_sound4.Start();

                byte nr50 = GameBoy.Ram.ReadByteAt(0xFF24);
                byte nr51 = GameBoy.Ram.ReadByteAt(0xFF25);

                bool bS02On = (nr50 & 0x80) != 0;
                bool bS01On = (nr50 & 0x04) != 0;
                int S02Vol = (nr50 & 0x70) >> 4;
                int S01Vol = (nr50 & 0x07);

                bool b_sound4_S02 = (nr51 & 0x80) != 0;
                bool b_sound3_S02 = (nr51 & 0x40) != 0;
                bool b_sound2_S02 = (nr51 & 0x20) != 0;
                bool b_sound1_S02 = (nr51 & 0x10) != 0;
                bool b_sound4_S01 = (nr51 & 0x08) != 0;
                bool b_sound3_S01 = (nr51 & 0x04) != 0;
                bool b_sound2_S01 = (nr51 & 0x02) != 0;
                bool b_sound1_S01 = (nr51 & 0x01) != 0;

                m_sound1.Update(m_sound01Enable && bIsRunning, m_tickCounter, b_sound1_S01, b_sound1_S02, S01Vol, S02Vol);
                m_sound2.Update(m_sound02Enable && bIsRunning, m_tickCounter, b_sound3_S01, b_sound3_S02, S01Vol, S02Vol);
                m_sound3.Update(m_sound03Enable && bIsRunning, m_tickCounter, b_sound3_S01, b_sound3_S02, S01Vol, S02Vol);
                m_sound4.Update(m_sound04Enable && bIsRunning, m_tickCounter, b_sound4_S01, b_sound4_S02, S01Vol, S02Vol);
            }
            else
            {
                m_sound1.Stop();
                m_sound2.Stop();
                m_sound3.Stop();
                m_sound4.Stop();
            }
        }
    }
}
