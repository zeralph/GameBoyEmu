using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NAudio.Wave;
using GameBoyTest.Memory;

namespace GameBoyTest.Audio
{
    public class QuadrangularWaveProvider32 : WaveProvider32
    {
        private int m_wavePatternDuty = 0;

        private bool m_bAttenuate = true;
        private int m_initialVol = 15;
        private int m_numberVolumeSweep = 0;
        private long m_enveloppeClockCnt = 0;
        private int m_curVol = 0;

        private int m_sweepDirection = 0;
        private int m_sweepShift = 0;
        private int m_sweepTime = 0;
        private float m_volLeft = 0.0f;
        private float m_volRight = 0.0f;

        
        //WaveFileWriter m_wr;
        private bool m_bUseSweepShift = false;
        private string m_fileName;

        private int m_frequencySweepClockCnt = 0;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public QuadrangularWaveProvider32(SoundManager.eSoundChannel channel, ushort nrx0Adr,  ushort nrx1Adr, ushort nrx2Adr, ushort nrx3Adr, ushort nrx4Adr, bool bUseSweepShift, string fileName):base()
        {
            m_channel = channel;
            m_nrx0Adr = nrx0Adr;
            m_nrx1Adr = nrx1Adr;
            m_nrx2Adr = nrx2Adr;
            m_nrx3Adr = nrx3Adr;
            m_nrx4Adr = nrx4Adr;
            m_fileName = fileName;
            m_bUseSweepShift = bUseSweepShift;
            int l = 8820 * 4; // 8820 = 100ms
            m_buffer = new float[l]; 
            for (int i = 0; i < m_buffer.Length; i++)
            {
                m_buffer[i] = 0;
            }

            //WaveFormat wf = new WaveFormat(44100, 16, 2);
            //m_wr = new WaveFileWriter(fileName, wf);

            //MappedMemory.RamHasChanged += new OnRamChange(ReInit);
            m_bufferReadingPos = m_buffer.Length/2;
            m_bufferWritingPos = 0;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        //static double a = 0;
        private float GetPhaseDuty(double time, double frequency)
        {
            double t = 1.0 / (double)(frequency);
            double a = time / t;
            double b = (int)(time / t);
            double p = a - b;
            if (m_wavePatternDuty == 0)
            {
                if (p < 0.125) //if (p >= 0.875)
                {
                    return -1.0f;
                }
            }
            else if (m_wavePatternDuty == 1)
            {
                if (p < 0.25)// if (p <= 0.125 || p>=0.875)
                {
                    return -1.0f;
                }
            }
            else if (m_wavePatternDuty == 2)
            {
                if (p < 0.5)// if (p <= 0.125 || p>=0.625)
                {
                    return -1.0f;
                }
            }
            else //3
            {
                if (p < 0.75)// if (p>=0.125 && p <= 0.875)
                {
                    return -1.0f;
                }
            }
            return 1.0f;
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
                else
                {
                    m_curVol++;
                }
            }
            float volOut = Math.Min(15, Math.Max(m_curVol, 0));
            volOut /= 15.0f;
            volOut *= 7.0f;
            volOut /= masterVol;
            return volOut;
        }

        private float UpdateFrequency()
        {
            float f = ReadFrequency();
            f = (131072) / (2048 - f);
            if (m_sweepTime != 0 && m_frequencySweepClockCnt % m_sweepTime == 0)
            {
                double s = Math.Pow((double)2, (double)m_sweepShift) * (double)m_sweepDirection;
                f = f + (float)(f / s);
            }
            float nf = 2048 - 131072 / f;
            WriteFrequency(nf);
            return f;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            int nbSamples = Math.Min(sampleCount, Math.Abs( m_bufferReadingPos-m_bufferWritingPos) );
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

        private float ReadFrequency()
        {
            byte nr13 = GameBoy.Ram.ReadByteAt(m_nrx3Adr);
            byte nr14 = GameBoy.Ram.ReadByteAt(m_nrx4Adr);
            float f = nr13 | ((nr14 & 0x7) << 8);
            return f;
        }

        private void WriteFrequency( float f )
        {
            byte nr13 = GameBoy.Ram.ReadByteAt(m_nrx3Adr);
            byte nr14 = GameBoy.Ram.ReadByteAt(m_nrx4Adr);
            int fi = (int)f;
            byte f1 = (byte)(fi & 0xFF);
            byte f2 = (byte)((fi & 0xF00) >> 8);
            f2 = (byte)(nr14 & 0xF8 | f2);
            GameBoy.Ram.WriteByte(m_nrx3Adr, f1);
            GameBoy.Ram.WriteByte(m_nrx4Adr, f2);
        }

        public void Update()
        {
            // called @ 512Hz
            m_tick ++;
            byte b = 0;
            float frequency=0.0f;
            double curTime = m_curTime;
            byte nr10 = GameBoy.Ram.ReadByteAt(m_nrx0Adr);
            byte nr11 = GameBoy.Ram.ReadByteAt(m_nrx1Adr);
            byte nr12 = GameBoy.Ram.ReadByteAt(m_nrx2Adr);
            byte nr13 = GameBoy.Ram.ReadByteAt(m_nrx3Adr);
            byte nr14 = GameBoy.Ram.ReadByteAt(m_nrx4Adr);

            b = GameBoy.Ram.ReadByteAt(m_nrx4Adr);
            if ((b & 0x80) != 0)
            {
                m_isConsecutive = (nr14 & 0x40) == 0;
                m_curTime = 0;
                m_frequencySweepClockCnt = 0;
                m_enveloppeClockCnt = 0;
                m_initialVol = (nr12 & 0xF0) >> 4;
                m_curVol = m_initialVol;
                

                UpdateNR52Flag(true);
                b &= 0x7F;
                GameBoy.Ram.WriteByte(m_nrx4Adr, b);
            }

            //length is update at 256Hz (2 cycles)
            if( m_tick % 2 == 0)
            {
                m_lengthSec = nr11 & 0x3F;
                m_lengthSec = (64.0 - m_lengthSec) * (1.0 / 256.0);
                m_remainingTime = m_lengthSec - m_curTime;
                m_wavePatternDuty = (nr11 & 0xC0) >> 6;
            }
            //sweep is update at 128Hz (4 cycles)
            if (m_tick % 4 == 0)
            {
                if ( m_bUseSweepShift )
                {
                    m_sweepDirection = ((nr10 & 0x08) == 0) ? 1 : -1;
                    m_sweepShift =  (nr10 & 0x07);
                    m_sweepTime = (nr10 & 0x70) >> 4;
                    m_frequencySweepClockCnt++;
                }
                else
                {
                    m_sweepDirection = 0;
                    m_sweepShift = 0;
                    m_sweepTime = 0;
                }
            }
            //enveloppe is updated at 64Hz (8 cycles)
            if( m_tick % 8 == 0)
            {
                m_enveloppeClockCnt++;
                m_bAttenuate = (nr12 & 0x8) == 0;
                m_numberVolumeSweep = (nr12 & 0x7);
                m_volLeft = (float)GetCurrentVol(curTime, eChannel.eChannel_Left);
                m_volRight = (float)GetCurrentVol(curTime, eChannel.eChannel_Right);
            }
            frequency = UpdateFrequency(); 
            double duration = 1.0 / 512.0;
            int nbSamplesToFill = (int)(duration * WaveFormat.SampleRate *2);
            double dt = duration / (nbSamplesToFill/2);
            int bufferPos = m_bufferWritingPos;
            float f = 0.0f;
            for (int n = 0; n < nbSamplesToFill; n += 2)
            {
                if (m_isConsecutive || curTime < m_lengthSec)
                {
                    f = GetPhaseDuty(curTime, frequency);
                }
                else
                {
                    f = 0.0f;
                    UpdateNR52Flag( false );

                }
                m_buffer[bufferPos] = m_volLeft  * f * MAX_AMPLITUDE;
                //m_wr.WriteSample(m_volLeft * frequency);
                bufferPos++;
                if (bufferPos >= m_buffer.Length)
                {
                    bufferPos = 0;
                }
                m_buffer[bufferPos] = m_volRight * f * MAX_AMPLITUDE;
                //m_wr.WriteSample(m_volRight * frequency);
                bufferPos ++;
                if( bufferPos >= m_buffer.Length)
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
