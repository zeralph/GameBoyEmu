using GameBoyTest.Memory;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoyTest.Audio
{
    class Sound2 : IDisposable
    {
        private QuadrangularWaveProvider32 m_waveProvider;
        private WaveOutEvent m_waveOut;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public Sound2()
        {
            m_waveProvider = new QuadrangularWaveProvider32( SoundManager.eSoundChannel.e_soundChannel_2, 0, 0XFF16, 0xFF17, 0xFF18, 0xFF19, false, "s02.wav");
            m_waveOut = new WaveOutEvent();
            m_waveOut.DesiredLatency = 100;
            m_waveOut.Init(m_waveProvider);
            m_waveOut.Play();
        }


        public void Init()
        {
            m_waveProvider.Reset();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Start()
        {
            m_waveOut.Play();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Stop()
        {
            m_waveOut.Stop();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Dispose()
        {
            Stop();
            m_waveOut.Dispose();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public WaveProvider32 GetWaveProvider()
        {
            return null;
            //return m_waveProvider;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Update(bool bEnable, long tickCounter, bool bOnLeft, bool bOnRight, int S01Vol, int S02Vol)
        {
            m_waveProvider.Update();
            if (m_waveOut.PlaybackState != PlaybackState.Playing)
            {
                m_waveOut.Play();
            }
        }
    }
}
