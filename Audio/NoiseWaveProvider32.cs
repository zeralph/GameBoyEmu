using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NAudio.Wave;
using GameBoyTest.Memory;
using GameBoyTest.LSFR;

namespace GameBoyTest.Audio
{
    public class NoiseWaveProvider32 : WaveProvider32
    {

        private bool m_bAttenuate = true;
        private int m_initialVol = 15;
        private int m_numberVolumeSweep = 0;
        private long m_enveloppeClockCnt = 0;
        private int m_curVol = 0;
        private float m_volLeft = 0.0f;
        private float m_volRight = 0.0f;

        private int m_polynomialShiftClockFrequency = 0;
        private int m_polynomialCounterStep = 0;
        private int m_polynomialDividerRatio= 0;


        private Lfsr m_Lfsr;
        //WaveFileWriter m_wr;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public NoiseWaveProvider32(SoundManager.eSoundChannel channel, ushort nrx0Adr, ushort nrx1Adr, ushort nrx2Adr, ushort nrx3Adr, ushort nrx4Adr, bool bUseSweepShift, string fileName)
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
            //WaveFormat wf = new WaveFormat(44100, 16, 2);
            //m_wr = new WaveFileWriter(fileName, wf);
            m_bufferReadingPos = m_buffer.Length / 2;
            m_bufferWritingPos = 0;
            m_Lfsr = new Lfsr(15);
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
        //vol is between 0 and 15
        //
        //////////////////////////////////////////////////////////////////////
        private float GetCurrentVol(double time, WaveProvider32.eChannel channel)
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
            if (m_numberVolumeSweep != 0 && m_enveloppeClockCnt % m_numberVolumeSweep == 0)
            {
                if (m_bAttenuate)
                {
                    m_curVol--;
                }
            }
            float volOut = Math.Min(15, Math.Max(m_curVol, 0));
            volOut /= 15.0f;
            volOut *= 7.0f;
            volOut /= masterVol;
            return volOut;
        }

        public float GetLfsrValue()
        {
            bool b = m_Lfsr.GetBit(0);
            return b ? 0.0f : 1.0f;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Update()
        {
            // called @ 512Hz
            m_tick++;
            byte b = 0;
            double curTime = m_curTime;
            byte nr41 = GameBoy.Ram.ReadByteAt(m_nrx1Adr);
            byte nr42 = GameBoy.Ram.ReadByteAt(m_nrx2Adr);
            byte nr43 = GameBoy.Ram.ReadByteAt(m_nrx3Adr);
            byte nr44 = GameBoy.Ram.ReadByteAt(m_nrx4Adr);

            b = GameBoy.Ram.ReadByteAt(m_nrx4Adr);
            if ((b & 0x80) != 0)
            {
                m_isConsecutive = (nr44 & 0x40) == 0;
                m_curTime = 0;
                m_enveloppeClockCnt = 0;
                m_initialVol = (nr42 & 0xF0) >> 4;
                m_curVol = m_initialVol;


                UpdateNR52Flag(true);
                b &= 0x7F;
                GameBoy.Ram.WriteByte(m_nrx4Adr, b);
            }
            //length is update at 256Hz (2 cycles)
            if (m_tick % 2 == 0)
            {
                m_lengthSec = nr41 & 0x3F;
                m_lengthSec = (64.0 - m_lengthSec) * (1.0 / 256.0);
                m_remainingTime = m_lengthSec - m_curTime;

                m_polynomialShiftClockFrequency = (nr43 & 0xF0) >> 4;
                m_polynomialCounterStep = (nr43 & 0x08) >> 3;
                m_polynomialDividerRatio = (nr43 & 0x03);
            }
            //enveloppe is updated at 64Hz (8 cycles)
            if (m_tick % 8 == 0)
            {
                m_enveloppeClockCnt++;
                m_bAttenuate = (nr42 & 0x8) == 0;
                m_numberVolumeSweep = (nr42 & 0x7);
                m_volLeft = (float)GetCurrentVol(curTime, eChannel.eChannel_Left);
                m_volRight = (float)GetCurrentVol(curTime, eChannel.eChannel_Right);
            }
            int divider = 0;
            switch( m_polynomialDividerRatio)
            {
                case 0:     { divider = 8/8; break; }
                case 1:     { divider = 16/8; break; }
                case 2:     { divider = 32/8; break; }
                case 3:     { divider = 48/8; break; }
                case 4:     { divider = 64/8; break; }
                case 5:     { divider = 80/8; break; }
                case 6:     { divider = 96/8; break; }
                case 7:     { divider = 112/8; break; }
                default:    { divider = 0; break; }

            }
            if (m_tick % divider == 0 )
            {
                m_Lfsr.Shift();
            }
            
            double duration = 1.0 / 512.0;
            int nbSamplesToFill = (int)(duration * WaveFormat.SampleRate * 2);
            double dt = duration / (nbSamplesToFill / 2);
            int bufferPos = m_bufferWritingPos;
            float f = 0.0f;
            for (int n = 0; n < nbSamplesToFill; n += 2)
            {
                if (m_isConsecutive || curTime < m_lengthSec)
                {
                    f = GetLfsrValue();
                }
                else
                {
                    f = 0.0f;
                    UpdateNR52Flag(false);

                }
                m_buffer[bufferPos] = m_volLeft * f * MAX_AMPLITUDE;
                //m_wr.WriteSample(m_volLeft * frequency);
                bufferPos++;
                if (bufferPos >= m_buffer.Length)
                {
                    bufferPos = 0;
                }
                m_buffer[bufferPos] = m_volRight * f * MAX_AMPLITUDE;
                //m_wr.WriteSample(m_volRight * frequency);
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