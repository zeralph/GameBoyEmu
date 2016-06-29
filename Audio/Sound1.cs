using GameBoyTest.Memory;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoyTest.Audio
{
    class Sound1 : IDisposable
    {
        private QuadrangularWaveProvider32 m_waveProvider;
        private WaveOutEvent m_waveOut;
        private bool m_started = false;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public Sound1()
        {
            m_waveProvider = new QuadrangularWaveProvider32(SoundManager.eSoundChannel.e_soundChannel_1, 0xFF10, 0xFF11, 0xFF12, 0xFF13, 0xFF14, true, "s01.wav");
            m_waveProvider.SetWaveFormat(44100, 2);
            m_waveOut = new WaveOutEvent();
            m_waveOut.DesiredLatency = 100;
            m_waveOut.Init(m_waveProvider);
            m_waveOut.Play();
        }

        public void Init()
        {
            m_waveProvider.Reset();
        }

        public void Start()
        {
            m_started = true;
            m_waveOut.Play();
            //m_waveOut.Stop();
            //m_waveOut.Init(m_waveProvider);
            //m_waveOut.Play();
        }

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
        // vol is between 0 and 7
        //
        //////////////////////////////////////////////////////////////////////
        public void Update(bool bEnable, long tickCounter, bool bOnLeft, bool bOnRight, int S01Vol, int S02Vol)
        {
            if (m_started )
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
