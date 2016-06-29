using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NAudio.Wave;

namespace GameBoyTest.Audio
{
    public class PatternWaveProvider32 : WaveProvider32
    {

        private int m_outputLevel = 0;
        private float[] m_waveData;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public PatternWaveProvider32(SoundManager.eSoundChannel channel, ushort nrx0Adr, ushort nrx1Adr, ushort nrx2Adr, ushort nrx3Adr, ushort nrx4Adr, bool bUseSweepShift, string fileName)
        {
            m_channel = channel;
            m_nrx0Adr = nrx0Adr;
            m_nrx1Adr = nrx1Adr;
            m_nrx2Adr = nrx2Adr;
            m_nrx3Adr = nrx3Adr;
            m_nrx4Adr = nrx4Adr;

            m_frequency = 400.0f;
            int l = 8820 * 4; // 8820 = 100ms
            m_buffer = new float[l];
            for (int i = 0; i < m_buffer.Length; i++)
            {
                m_buffer[i] = 0;
            }
            m_waveData = new float[WAVE_DATA_LENGHT * 2];
            for (int i = 0; i < m_waveData.Length; i++)
            {
                m_waveData[i] = 0.0f;
            }
            //WaveFormat wf = new WaveFormat(44100, 16, 2);
            //m_wr = new WaveFileWriter(fileName, wf);
            m_bufferReadingPos = m_buffer.Length / 2;
            m_bufferWritingPos = 0;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            int nbSamples = Math.Min(sampleCount, Math.Abs(m_bufferReadingPos - m_bufferWritingPos));
            int j = m_bufferReadingPos;
            for (int i = 0; i < nbSamples; i++)
            {
                buffer[i] = m_buffer[j];
                //m_buffer[j] = 0;
                j++;
                if (j >= m_buffer.Length)
                {
                    j = 0;
                }
            }
            m_bufferReadingPos = j % m_buffer.Length;
            return nbSamples;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private float GetCurrentVolume(WaveProvider32.eChannel channel)
        {
            int masterVol = 0;
            if (channel == eChannel.eChannel_Left)
            {
                if (!m_leftChannel)
                {
                    return 0.0f;
                }
                else
                {
                    masterVol = m_leftVolume;
                }
            }
            else
            {
                if (!m_rightChannel)
                {
                    return 0.0f;
                }
                else
                {
                    masterVol = m_rightVolume;
                }
            }
            float outVol = 0;
            switch (m_outputLevel)
            {
                case 0:
                    outVol = 0.0f;
                    break;
                case 1:
                    outVol = 1.0f;
                    break;
                case 2:
                    outVol = 0.5f;
                    break;
                case 3:
                    outVol = 0.25f;
                    break;
                default:
                    outVol = 0.0f;
                    break;
            }
            return (outVol * masterVol ) / 7.0f;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void SetupWaveData()
        {
            byte[] waveData = new byte[WAVE_DATA_LENGHT];
            GameBoy.Ram.ReadAt(0xFF30, WAVE_DATA_LENGHT, ref waveData);       
            int j = 0;
            string s="";
            for (int i = 0; i < waveData.Length; i++)
            {
                byte b1 = (byte)(waveData[i] & 0x0F);
                byte b2 = (byte)((waveData[i] & 0xF0) >> 4);
                m_waveData[j] = -((float)-b2 / 7.5f + 1.0f);
                m_waveData[j + 1] = -((float)-b1 / 7.5f + 1.0f);
                s += m_waveData[j] + ";" + m_waveData[j + 1] + ";";
                j += 2;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        static double toto = 1.0;
        private float GetCurrentSample(double time)
        {
            double t = 1.0 / (double)(m_frequency * toto);
            double a = time / t;
            double b = (int)(time / t);
            double p = a - b;
            p *= m_waveData.Length;
            //p = a / m_waveData.Length;
            int curSampleIndex = (int)p;
            return m_waveData[curSampleIndex];
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Update()
        {
            // called @ 512Hz
            m_tick++;
            byte b = 0;
            float frequency = 0.0f;
            double curTime = m_curTime;
            byte nr30 = GameBoy.Ram.ReadByteAt(m_nrx0Adr);
            byte nr31 = GameBoy.Ram.ReadByteAt(m_nrx1Adr);
            byte nr32 = GameBoy.Ram.ReadByteAt(m_nrx2Adr);
            byte nr33 = GameBoy.Ram.ReadByteAt(m_nrx3Adr);
            byte nr34 = GameBoy.Ram.ReadByteAt(m_nrx4Adr);

            b = GameBoy.Ram.ReadByteAt(m_nrx4Adr);
            if ((b & 0x80) != 0)
            {
                
                m_isConsecutive = (nr34 & 0x40) == 0;
                m_curTime = 0;
                SetupWaveData();
                UpdateNR52Flag(true);
                b &= 0x7F;
                GameBoy.Ram.WriteByte(m_nrx4Adr, b);
            }
            
            //length is update at 256Hz (2 cycles)
            if (m_tick % 2 == 0)
            {
                m_bSoundIsOff = (nr30 & 0x80) == 0;
                m_lengthSec = nr31;
                m_lengthSec = (256.0 - m_lengthSec) * (1.0 / 2.0);
                m_remainingTime = m_lengthSec - m_curTime;
                float f = nr33 | ((nr34 & 0x7) << 8);
                f = (65536) / (2048 - f);
                m_frequency = f;
            }

            //enveloppe is updated at 64Hz (8 cycles)
            if (m_tick % 8 == 0)
            {
                m_outputLevel = (nr32 & 0x60) >> 5;
                
            }
            double duration = 1.0 / 512.0;
            int nbSamplesToFill = (int)(duration * WaveFormat.SampleRate * 2);
            double dt = duration / (nbSamplesToFill / 2);
            int bufferPos = m_bufferWritingPos;

            for (int n = 0; n < nbSamplesToFill; n += 2)
            {
                if(m_bSoundIsOff )
                { 
                    frequency = 0.0f;
                }
                else
                {
                    if (m_isConsecutive || curTime < m_lengthSec)
                    {
                        frequency = GetCurrentSample(curTime);
                    }
                    else
                    {
                        frequency = 0.0f;
                        UpdateNR52Flag(false);

                    }
                }
                float volLeft = GetCurrentVolume( eChannel.eChannel_Left );
                float volRight = GetCurrentVolume( eChannel.eChannel_Right );
                m_buffer[bufferPos] = (float)(volLeft * frequency * MAX_AMPLITUDE);
                bufferPos++;
                if (bufferPos >= m_buffer.Length)
                {
                    bufferPos = 0;
                }
                m_buffer[bufferPos] = (float)(volRight * frequency * MAX_AMPLITUDE);
                bufferPos++;
                if (bufferPos >= m_buffer.Length)
                {
                    bufferPos = 0;
                }
                curTime += dt;
            }
            m_bufferWritingPos += nbSamplesToFill;
            m_bufferWritingPos %= m_buffer.Length;
            m_curTime += duration;
            //m_wr.Flush();
        }
    }
}
