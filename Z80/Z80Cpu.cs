
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBoyTest.Debug;
using GameBoyTest.MicroTimer;
using System.Diagnostics;
using System.Windows.Forms;
using System.Timers;

namespace GameBoyTest.Z80
{
    public class Z80Cpu:IDisposable
    {
        // on clock sur le refresh screen a 59.73Hz, soit 16742µs. (16)ms
        // a chaque tick on fait effectuer un cycle de refresh complet, soit 17557 ticks CPU
        private const double GAMEBOY_DEFAULT_SCREEN_REFRESH_HZ= 59.73;
        private const double GAMEBOY_DEFAULT_SOUND_REFRESH_HZ = 512;//4096.0;
        private const double GAMEBOY_DEFAULT_CLOCK_SPEED_MS = 1.0 / GAMEBOY_DEFAULT_SCREEN_REFRESH_HZ * 1000D;
        private const double GAMEBOY_DEFAULT_CPU_SPEED_HZ = 1000D * 1000D * 4.194304;
        //cpu cycles is divided by 4
        private const int GAMEBOY_NB_CYCLES_PER_SCREEN_REFRESH = (int)((GAMEBOY_DEFAULT_CPU_SPEED_HZ / GAMEBOY_DEFAULT_SCREEN_REFRESH_HZ ) / 4.0);
        //sound is updated at 256Hz
        private const int GAMEBOY_NB_CYCLES_PER_SOUND_REFRESH = (int)((GAMEBOY_DEFAULT_CPU_SPEED_HZ / GAMEBOY_DEFAULT_SOUND_REFRESH_HZ ));
        private const int cpuArrayLength = 13;
       
        bool m_started = false;
        bool m_halted = false;
        bool m_runOnce = false;
        bool m_runToStepOver = false;
        bool m_runFrameByFrame = false;
        bool m_runAFrame = false;
        //decoder
        Z80InstructionDecoder m_decoder;

        //interrupts
        InterruptsMgr m_interruptsMgr;
        bool    m_ImeFlag;

        //Timer
        CpuTimer    m_cpuTimer;

        //registers
        byte m_rA;
        byte m_rB;
        byte m_rC;
        byte m_rD;
        byte m_rE;
        byte m_rF;
        byte m_rH;
        byte m_rL;

        //pointers
        ushort m_SP;
        ushort m_PC;

        //instructions
        long m_instNumber;
        Z80Instruction m_curInstruction;
        private int m_curInstructionNbCycles;
        private int m_curInstructionCurCycle;

        //debugger
        private bool m_IsDebbugerReady = false;
        private long m_clockCnt = 0;
        private System.Timers.Timer m_controlTimer;
        private double m_lastTickElapsedMS = 0;

        private long m_CpuTick = 0;

        //DMA
        private bool m_DmaTransfertStarted = false;
        private long m_DmaTransfertStartTime = 0;
        private ushort m_DmaTransfertStartAdress = 0;
//        private int m_DmaTransfertByteIndex = 0;
        private int m_curNbCyclesPerScreenRefresh = GAMEBOY_NB_CYCLES_PER_SCREEN_REFRESH;
        private int m_curNbCyclesPerSoundRefresh = GAMEBOY_NB_CYCLES_PER_SOUND_REFRESH;
        //timer
        private System.Diagnostics.Stopwatch m_stopWatch;
        //private System.Timers.Timer m_timer;
        private GameBoyTest.MicroTimer.MicroTimer m_timer;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public Z80Cpu( GameBoyTest.MicroTimer.MicroTimer timer)
        {
            m_timer = timer; //new GameBoyTest.MicroTimer.MicroTimer();// new System.Timers.Timer();
            m_timer.Interval = (long)(GAMEBOY_DEFAULT_CLOCK_SPEED_MS * 1000);
            //m_timer.AutoReset = false;
            //m_timer.Elapsed += new ElapsedEventHandler(OnCpuTick);
            m_timer.MicroTimerElapsed += new GameBoyTest.MicroTimer.MicroTimer.MicroTimerElapsedEventHandler(OnCpuTick);

            m_decoder = new Z80InstructionDecoder();
            m_interruptsMgr = new InterruptsMgr();
            m_cpuTimer = new CpuTimer();

            m_curInstructionNbCycles = 0;
            m_curInstructionCurCycle = 0;
            m_curInstruction = null;

            m_clockCnt = 0;
            m_CpuTick = 0;
            m_controlTimer = new System.Timers.Timer();
            m_controlTimer.Interval = 100;
            m_controlTimer.Elapsed += new ElapsedEventHandler(ControlTimerElapsed);
            m_controlTimer.Enabled = true; // Enable it
            m_ImeFlag = false;
            m_stopWatch = new Stopwatch();
            m_timer.Start();
        }

        public void Dispose()
        {
            Dispose(true);
            m_timer.Stop();
        }

        protected virtual void Dispose(bool disposing)
        {
            //m_timer.Dispose();
            m_controlTimer.Dispose();
        }

        public byte[] Serialize()
        {
            byte[] cpuArray = new byte[cpuArrayLength];
            cpuArray[0] = GameBoy.Cpu.rA;
            cpuArray[1] = GameBoy.Cpu.rB;
            cpuArray[2] = GameBoy.Cpu.rC;
            cpuArray[3] = GameBoy.Cpu.rD;
            cpuArray[4] = GameBoy.Cpu.rE;
            cpuArray[5] = GameBoy.Cpu.rF;
            cpuArray[6] = GameBoy.Cpu.rH;
            cpuArray[7] = GameBoy.Cpu.rL;
            cpuArray[8] = (byte)((GameBoy.Cpu.PC & 0xFF00) >> 8);
            cpuArray[9] = (byte)((GameBoy.Cpu.PC & 0x00FF) >> 0);
            cpuArray[10] = (byte)((GameBoy.Cpu.SP & 0xFF00) >> 8);
            cpuArray[11] = (byte)((GameBoy.Cpu.SP & 0x00FF) >> 0);
            cpuArray[12] = GameBoy.Cpu.IMEStatus() ? (byte)1 : (byte)0;
            return cpuArray;
        }

        public int Unserialize(ref byte[] buffer, int startAdr)
        {
            GameBoy.Cpu.rA = buffer[startAdr+0];
            GameBoy.Cpu.rB = buffer[startAdr+1];
            GameBoy.Cpu.rC = buffer[startAdr+2];
            GameBoy.Cpu.rD = buffer[startAdr+3];
            GameBoy.Cpu.rE = buffer[startAdr+4];
            GameBoy.Cpu.rF = buffer[startAdr+5];
            GameBoy.Cpu.rH = buffer[startAdr+6];
            GameBoy.Cpu.rL = buffer[startAdr+7];
            GameBoy.Cpu.PC = (ushort)(buffer[startAdr+8] << 8 | buffer[startAdr+9]);
            GameBoy.Cpu.SP = (ushort)(buffer[startAdr+10] << 8 | buffer[startAdr+11]);
            if (buffer[startAdr + 12] != 0)
            {
                GameBoy.Cpu.EnableInterrupts();
            }
            return startAdr + cpuArrayLength;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void ControlTimerElapsed(object sender, ElapsedEventArgs e)
        {
            
            double t = (double)m_clockCnt / (1000D * 1000D);
            m_clockCnt = 0;
            double available = (m_lastTickElapsedMS / GAMEBOY_DEFAULT_CLOCK_SPEED_MS) * 100.0;
            //t = m_debug_value_speed;
            String s = String.Format(""+ (int)available);
            if (GameBoy.Screen != null)
            {
                GameBoy.Screen.SetSpeedValuePercentage((int)available);
            }
        }

        #region accessors

        //Zero Flag
        public bool ZValue
        {
            get
            {
                return (m_rF & 0x80) != 0x00;
            }
            set
            {
                if (value)
                {
                    m_rF |= 0x80;
                }
                else
                {
                    m_rF &= 0x7F;
                }
            }
        }

        //Substract flag
        public bool NValue
        {
            get
            {
                return (m_rF & 0x40) != 0x00;
            }
            set
            {
                if (value)
                {
                    m_rF |= 0x40;
                }
                else
                {
                    m_rF &= 0xBF;
                }
            }
        }

        //HalfCarry flag
        public bool HValue
        {
            get
            {
                return (m_rF & 0x20) != 0x00;
            }
            set
            {
                if (value)
                {
                    m_rF |= 0x20;
                }
                else
                {
                    m_rF &= 0xDF;
                }
            }
        }

        //Carry flag
        public bool CValue
        {
            get
            {
                return (m_rF & 0x10) != 0x00;
            }
            set
            {
                if (value)
                {
                    m_rF |= 0x10;
                }
                else
                {
                    m_rF &= 0xEF;
                }
            }
        }

        //rA
        public byte rA
        {
            get { return m_rA; }
            set { m_rA = value; }
        }

        //rB
        public byte rB
        {
            get { return m_rB; }
            set { m_rB = value; }
        }

        //rC
        public byte rC
        {
            get { return m_rC; }
            set { m_rC = value; }
        }

        //rD
        public byte rD
        {
            get { return m_rD; }
            set { m_rD = value; }
        }

        //rE
        public byte rE
        {
            get { return m_rE; }
            set { m_rE = value; }
        }

        //rF
        public byte rF
        {
            get { return m_rF; }
            set
            {
                byte b = (byte)(value & 0xF0);
                m_rF = b;
            }
        }

        //rH
        public byte rH
        {
            get { return m_rH; }
            set { m_rH = value; }
        }

        //rL
        public byte rL
        {
            get { return m_rL; }
            set { m_rL = value; }
        }

        //rAF
        public ushort rAF
        {
            get { return (ushort)(m_rA << 8 | m_rF); }
            set { m_rA = (byte)(value >> 8); rF = (byte)(value & 0x00FF); }
        }
        //rBC
        public ushort rBC
        {
            get { return (ushort)(m_rB << 8 | m_rC); }
            set { m_rB = (byte)(value >> 8); m_rC = (byte)(value & 0x00FF); }
        }
        //rDE
        public ushort rDE
        {
            get { return (ushort)(m_rD << 8 | m_rE); }
            set { m_rD = (byte)(value >> 8); m_rE = (byte)(value & 0x00FF); }
        }
        //rHL
        public ushort rHL
        {
            get { return (ushort)(m_rH << 8 | m_rL); }
            set { m_rH = (byte)(value >> 8); m_rL = (byte)(value & 0x00FF); }
        }

        public void IncHL()
        {
            long HL = (long)(m_rH << 8 | m_rL);
            HL += 0x01;
            HL = (ushort)(HL & 0xFFFF);
            m_rH = (byte)(HL >> 8);
            m_rL = (byte)(HL & 0x00FF);

        }

        public void DecHL()
        {
            long HL = (long)(m_rH << 8 | m_rL);
            HL -= 0x01;
            HL = (ushort)(HL & 0xFFFF);
            m_rH = (byte)(HL >> 8);
            m_rL = (byte)(HL & 0x00FF);
        }

        //PC-FP commands
        public ushort SP
        {
            get { return m_SP; }
            set { m_SP = value; }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public bool ComputeHCarry(ushort val1, ushort val2, bool bIsSub, bool bUseCarry)
        {
            if (!bIsSub)
            {
                byte c = 0x00;
                if (bUseCarry)
                {
                    c = 0x01;
                }
                ushort bc = (ushort)(val2 + c);
                ushort a = (ushort)(val1 & 0x0F);
                ushort b = (ushort)(bc & 0x0F);
                bool bout = a + b > 0x0F;
                return bout;

            }
            else
            {
                ushort a = (ushort)(val1 & 0x0f);// >> 4);
                ushort b = (ushort)(val2 & 0x0f);// >> 4);
                bool bout = b > a;
                return bout;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public ushort PC
        {
            get { return m_PC; }
            set { m_PC = value; }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public Z80Instruction currentInstruction
        {
            get { return m_curInstruction; }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public long GetInstructionNumber()
        {
            return m_instNumber;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void IncInstructionNumber()
        {
            m_instNumber++;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public Z80InstructionDecoder decoder
        {
            get { return m_decoder; }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public bool running
        {
            get { return m_started; }
        }

        #endregion accessors

        #region execution

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void SetSpeed(int percentage)
        {
            m_curNbCyclesPerScreenRefresh = (int)((double)GAMEBOY_NB_CYCLES_PER_SCREEN_REFRESH * (double)percentage / 100D);
            m_curNbCyclesPerSoundRefresh = (int)((double)GAMEBOY_NB_CYCLES_PER_SOUND_REFRESH * (double)percentage / 100D); 
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Stop()
        {
            m_started = false;
            GameBoy.Sound.Stop();
            GameBoy.Screen.SetEmuStatus("Stopped");
            //m_timer.Stop();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void EnableInterrupts()
        {
            m_ImeFlag = true;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void DisableInterrupts()
        {
            m_ImeFlag = false;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Start()
        {
            m_runFrameByFrame = false;
            m_runAFrame = false;
            m_halted = false;
            m_started = true;
            GameBoy.Sound.Start();
            GameBoy.Screen.SetEmuStatus("Running");
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Halt()
        {
            m_halted = true;
        }

        public bool IsHalted()
        {
            return m_halted;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void ResumeFromHalt()
        {
            m_halted = false;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void RunOnce()
        {
            m_runOnce = true;
            //m_timer.Start();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void RunToStepOver()
        {
            m_runToStepOver = true;
            Start();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void StopAndRunOneFrame()
        {
            m_runFrameByFrame = true;
            m_runAFrame = true;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public bool IMEStatus()
        {
            return m_ImeFlag;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Init()
        {
            m_rA = 0x11;    //check with SGB, can be different
            m_rB = 0x00;
            m_rC = 0x00;
            m_rD = 0xFF;
            m_rE = 0x56;
            m_rF = 0x80;
            m_rH = 0x00;
            m_rL = 0x0D;
            m_PC = 0x100;
            m_SP = 0xFFFE;


            m_instNumber = 0;

            ZValue = true;
            NValue = false;
            HValue = false;
            CValue = false;

            GameBoy.Ram.WriteAt(0xFF05, 0x00);
            GameBoy.Ram.WriteAt(0xFF06, 0x00);
            GameBoy.Ram.WriteAt(0xFF07, 0x00);
            GameBoy.Ram.WriteAt(0xFF10, 0x80);
            GameBoy.Ram.WriteAt(0xFF11, 0xBF);
            GameBoy.Ram.WriteAt(0xFF12, 0xF3);
            GameBoy.Ram.WriteAt(0xFF14, 0xBF);
            GameBoy.Ram.WriteAt(0xFF16, 0x3F);
            GameBoy.Ram.WriteAt(0xFF17, 0x00);
            GameBoy.Ram.WriteAt(0xFF19, 0xBF);
            GameBoy.Ram.WriteAt(0xFF1A, 0x7F);
            GameBoy.Ram.WriteAt(0xFF1B, 0xFF);
            GameBoy.Ram.WriteAt(0xFF1C, 0x9F);
            GameBoy.Ram.WriteAt(0xFF1E, 0xBF);
            GameBoy.Ram.WriteAt(0xFF20, 0xFF);
            GameBoy.Ram.WriteAt(0xFF21, 0x00);
            GameBoy.Ram.WriteAt(0xFF22, 0x00);
            GameBoy.Ram.WriteAt(0xFF23, 0xBF);
            GameBoy.Ram.WriteAt(0xFF24, 0x77);
            GameBoy.Ram.WriteAt(0xFF25, 0xF3);
            GameBoy.Ram.WriteAt(0xFF26, 0xF1);    //for GB, F0 fot SGB ;  NR52
            GameBoy.Ram.WriteAt(0xFF40, 0x91);
            GameBoy.Ram.WriteAt(0xFF42, 0x00);
            GameBoy.Ram.WriteAt(0xFF43, 0x00);
            GameBoy.Ram.WriteAt(0xFF45, 0x00);
            GameBoy.Ram.WriteAt(0xFF47, 0xFC);
            GameBoy.Ram.WriteAt(0xFF48, 0xFF);
            GameBoy.Ram.WriteAt(0xFF49, 0xFF);
            GameBoy.Ram.WriteAt(0xFF4A, 0x00);
            GameBoy.Ram.WriteAt(0xFF4B, 0x00);
            GameBoy.Ram.WriteAt(0xFFFF, 0x00);

            m_ImeFlag = false;

            GameBoy.Screen.SetEmuStatus("Stopped");
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void OnCpuTick(object sender, MicroTimerEventArgs timerEventArgs)
        {
            RunOneFrame();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void RunOneFrame()
        {
            if( m_runFrameByFrame )
            {
                if( !m_runAFrame )
                {
                    return;
                }
                else
                {
                    m_runAFrame = false;
                }
            }

            m_stopWatch.Start();
            if (m_started)
            {
                GameBoy.Video.ResetTick();
                for (int i = 0; i < m_curNbCyclesPerScreenRefresh; i++)
                {
                    m_cpuTimer.Update();
                    GameBoy.Cartridge.Update();
                    GameBoy.InputsMgr.Update();
                    UpdateCpu();
                    if (m_CpuTick % m_curNbCyclesPerSoundRefresh == 0)
                    {
                        GameBoy.Sound.Update();
                    }
                    GameBoy.Video.Update();
                    m_CpuTick += 4;
                    m_clockCnt++;
                }
            }
            m_lastTickElapsedMS = m_stopWatch.ElapsedMilliseconds;
            m_stopWatch.Stop();
            m_stopWatch.Reset();
            double d = GAMEBOY_DEFAULT_CLOCK_SPEED_MS - m_lastTickElapsedMS;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void UpdateCpu()
        {
            if (m_halted)
            {
                //m_interruptsMgr.Update();
                //return;
                byte b = 0;
                byte b1 = GameBoy.Ram.ReadByteAt(0xFF0F);
                byte b2 = GameBoy.Ram.ReadByteAt(0xFFFF);
                b = (byte)(b1 & b2);
                if (b != 0x00)
                {
                    /*
                     * The interrupt-enable flag and its corresponding interrupt request flag are set
                     *  IME = 0 (Interrupt Master Enable flag disabled)
                     *      Starting address: address following that of the HALT instruction
                     *  IME = 1 (Interrupt Master Enable flag enabled)
                     *      Starting address: each interrupt starting address
                     */
                    if (m_ImeFlag)
                    {
                        m_interruptsMgr.Update();
                    }
                    m_halted = false;
                }
                else
                {
                    return;
                }
            }
            if (!m_DmaTransfertStarted)
            {
                if (GameBoy.IsDebuggerEnabled())
                {
                    if (!m_IsDebbugerReady)
                    {
                        m_IsDebbugerReady = DebugFunctions.IsReady();
                    }
                    if (m_IsDebbugerReady && DebugFunctions.CodeViewForm().DoBreakIfNeeded(GameBoy.Cpu.PC))
                    {
                        GameBoy.Cpu.Stop();
                    }
                    if (m_IsDebbugerReady && RunOneInstruction(true))
                    {
                        ReInitDebug();
                    }
                }
                else
                {
                    RunOneInstruction(false);
                }
            }
            CheckDMA();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void CheckDMA()
        {
            if (!m_DmaTransfertStarted)
            {
                byte b = GameBoy.Ram.ReadByteAt(0xFF46);
                if (b != 0)
                {
                    m_DmaTransfertStartAdress = (ushort)((ushort)b * (ushort)0x100);
//                    m_DmaTransfertByteIndex = 0;
                    m_DmaTransfertStartTime = m_CpuTick;
                    m_DmaTransfertStarted = true;
                    GameBoy.Ram.WriteAt(0xFF46, 0);
                    GameBoy.Ram.TransfertOAM(m_DmaTransfertStartAdress);
                }
            }
            else
            {
                if (m_CpuTick > m_DmaTransfertStartTime + 160)
                {
                    m_DmaTransfertStarted = false;
                    m_DmaTransfertStartTime = 0;
                }
                else
                {
                    //GameBoy.Ram.TransfertOAM(m_DmaTransfertStartAdress, m_DmaTransfertByteIndex);
                    //m_DmaTransfertByteIndex++;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //@return true / if instruction was NOP
        //
        //////////////////////////////////////////////////////////////////////
        public bool RunOneInstruction( bool bDebugEnabled )
        {
            if (m_started || m_runOnce || m_runToStepOver)
            {
                bool bCanRun = bDebugEnabled ? DebugFunctions.Z80Form().CanStep() : true;
                if (bCanRun && (m_curInstruction == null || m_curInstructionCurCycle >= m_curInstructionNbCycles))
                {
                    if (m_interruptsMgr.Update())
                    {
                            return true;
                    }
                    else
                    {
                        byte opcode = 0;
                        opcode = GameBoy.Ram.ReadByteAt(m_PC);
                        m_curInstruction = m_decoder.GetNextInstruction();
                        if (m_curInstruction == null)
                        {
                            ThrowNullInstructionException(opcode, m_PC, bDebugEnabled);
                            return true;
                        }
                        if (m_runOnce)
                        {
                            m_runOnce = false;
                            m_curInstructionCurCycle = m_curInstructionNbCycles;
                        }
                        if (bDebugEnabled)
                        {
                            DebugFunctions.CallStackForm().AddToCallstack();
                        }
                        ushort oldPc = m_PC;
                        m_PC = m_curInstruction.Exec(m_PC);
                        m_curInstructionNbCycles = m_curInstruction.GetCurNbCycles(oldPc);
                        m_curInstructionCurCycle = 4;
                        GameBoy.Cpu.IncInstructionNumber();
                        if (m_runToStepOver && opcode == 0xC9)
                        {
                            m_runToStepOver = false;
                            Stop();
                        }
                    }
                }
                else
                {
                    if (!m_halted)
                    {
                        //on avance 4 cycles par 4 cycles car le timer est a 1Mhz, donc x4 pour les 4Mhz de la GB
                        m_curInstructionCurCycle += 4;
                    }
                }
                return true;
            }
            return false;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void ThrowNullInstructionException(byte opcode, ushort pc, bool bDebugEnabled )
        {
            GameBoy.Cpu.Stop();
            if (bDebugEnabled)
            {
                DebugFunctions.CallStackForm().AddToCallstack();
            }
            string message = String.Format(" Wrong instruction, cannot be decoded (0x{0:x2}), PC = 0x{1:x4}", opcode, pc);
            string caption = "Error";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public double GetSpeedPercentage()
        {
            return m_lastTickElapsedMS;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void ReInitDebug()
        {
#if DEBUG
            DebugFunctions.Z80Form().ResetStep();
#endif
        }
    }
#endregion execution
}
