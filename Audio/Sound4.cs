using GameBoyTest.Memory;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoyTest.Audio
{
    class Sound4 : IDisposable
    {
        private NoiseWaveProvider32 m_waveProvider;
        private WaveOutEvent m_waveOut;
        private bool m_started = false;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public Sound4()
        {
            m_waveProvider = new NoiseWaveProvider32(SoundManager.eSoundChannel.e_soundChannel_3, 0, 0xFF20, 0xFF21, 0xFF22, 0xFF23, true, "s04.wav");
            m_waveOut = new WaveOutEvent();
            m_waveOut.DesiredLatency = 100;
            m_waveOut.Init(m_waveProvider);
            m_waveOut.Play();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Start()
        {
            m_started = true;
            m_waveOut.Play();
        }

        public void Init()
        {
            m_waveProvider.Reset();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Stop()
        {
            m_started = false;
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
            return m_waveProvider;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Update(bool bEnable, long tickCounter, bool bOnLeft, bool bOnRight, int S01Vol, int S02Vol)
        {
            if (m_started)
            {
                m_waveProvider.Update();
                if (m_waveOut.PlaybackState != PlaybackState.Playing)
                {
                    m_waveOut.Play();
                }
            }
        }
    }
}
