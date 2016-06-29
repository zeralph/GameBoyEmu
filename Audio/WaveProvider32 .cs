using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoyTest.Audio
{
    public abstract class WaveProvider32 : IWaveProvider
    {
        public enum eChannel
        {
            eChannel_None = 0,
            eChannel_Left,
            eChannel_Right,
        }

        protected const float MAX_AMPLITUDE = 0.5f;
        protected const int WAVE_DATA_LENGHT = 0x10;
        protected SoundManager.eSoundChannel m_channel = SoundManager.eSoundChannel.e_soundChannel_none;
        protected ushort m_nrx0Adr;
        protected ushort m_nrx1Adr;
        protected ushort m_nrx2Adr;
        protected ushort m_nrx3Adr;
        protected ushort m_nrx4Adr;
        protected long m_tick = 0;
        protected bool m_IsNR52Reset = true;
        protected float[] m_buffer;
        protected int m_bufferReadingPos = 0;
        protected int m_bufferWritingPos = 0;
        protected bool m_bSoundIsOff = true;
        protected float m_frequency = 400.0f;
        protected double m_lengthSec = 0;
        protected double m_curTime = 0;
        protected double m_remainingTime = 0;
        protected bool m_isConsecutive = false;
        protected bool m_leftChannel = true;
        protected bool m_rightChannel = true;
        protected int m_leftVolume = 7;
        protected int m_rightVolume = 7;
        protected WaveFormat waveFormat;


        //////////////////////////////////////////////////////////////////////
        //
        //  vol is between 0 and 7
        //
        //////////////////////////////////////////////////////////////////////
        public WaveProvider32() : this(44100, 2)
        {
        }

        //////////////////////////////////////////////////////////////////////
        //
        //  vol is between 0 and 7
        //
        //////////////////////////////////////////////////////////////////////
        public WaveProvider32(int sampleRate, int channels)
        {
            SetWaveFormat(sampleRate, channels);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //  vol is between 0 and 7
        //
        //////////////////////////////////////////////////////////////////////
        public void SetWaveFormat(int sampleRate, int channels)
        {
            this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //  vol is between 0 and 7
        //
        //////////////////////////////////////////////////////////////////////
        public void Reset()
        {
            for (int i = 0; i < m_buffer.Length; i++)
            {
                m_buffer[i] = 0;
            }
            m_bufferReadingPos = m_buffer.Length / 2;
            m_bufferWritingPos = 0;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //
        //////////////////////////////////////////////////////////////////////
        public bool IsPlaying()
        {
            return m_remainingTime > 0;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //  vol is between 0 and 7
        //
        //////////////////////////////////////////////////////////////////////
        public void SetupChannel(bool bOnLeft, bool bOnRight, int S01Vol, int S02Vol)
        {
            m_leftChannel = bOnLeft;
            m_rightChannel = bOnRight;
            m_leftVolume = S01Vol;
            m_rightVolume = S02Vol;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //  vol is between 0 and 7
        //
        //////////////////////////////////////////////////////////////////////
        protected void UpdateNR52Flag(bool enable)
        {
            if (enable != m_IsNR52Reset)
            {
                m_IsNR52Reset = enable;
                byte b = GameBoy.Ram.ReadByteAt(0xFF26);
                if (enable)
                {
                    if (m_channel == SoundManager.eSoundChannel.e_soundChannel_1)
                    {
                        b |= 0x01;
                    }
                    else if (m_channel == SoundManager.eSoundChannel.e_soundChannel_2)
                    {
                        b |= 0x02;
                    }
                    else if (m_channel == SoundManager.eSoundChannel.e_soundChannel_3)
                    {
                        b |= 0x04;
                    }
                    else if (m_channel == SoundManager.eSoundChannel.e_soundChannel_4)
                    {
                        b |= 0x08;
                    }
                }
                else
                {
                    if (m_channel == SoundManager.eSoundChannel.e_soundChannel_1)
                    {
                        b &= 0xFE;
                    }
                    else if (m_channel == SoundManager.eSoundChannel.e_soundChannel_2)
                    {
                        b &= 0xFD;
                    }
                    else if (m_channel == SoundManager.eSoundChannel.e_soundChannel_3)
                    {
                        b &= 0xFB;
                    }
                    else if (m_channel == SoundManager.eSoundChannel.e_soundChannel_4)
                    {
                        b &= 0xF7;
                    }
                }
                GameBoy.Ram.WriteAt(0xFF26, b);
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //  vol is between 0 and 7
        //
        //////////////////////////////////////////////////////////////////////
        public int Read(byte[] buffer, int offset, int count)
        {
            WaveBuffer waveBuffer = new WaveBuffer(buffer);
            int samplesRequired = count / 4;
            int samplesRead = Read(waveBuffer.FloatBuffer, offset / 4, samplesRequired);
            return samplesRead * 4;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //  vol is between 0 and 7
        //
        //////////////////////////////////////////////////////////////////////
        public abstract int Read(float[] buffer, int offset, int sampleCount);

        //////////////////////////////////////////////////////////////////////
        //
        //  vol is between 0 and 7
        //
        //////////////////////////////////////////////////////////////////////
        public WaveFormat WaveFormat
        {
            get { return waveFormat; }
        }
    }
}
