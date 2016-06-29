using GameBoyTest.Z80;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GameBoyTest.Forms.Debug.Visual
{
    public partial class CallstackForm : Form
    {

        private struct sStackData
        {
            public long _cnt;
            public ushort _adr;
            public bool _isCB;
            public int _opcode;
            public string _str;
            public ushort _raf;
            public ushort _rbc;
            public ushort _rde;
            public ushort _rhl;
            public ushort _pc;
            public ushort _sp;
            public bool _n;
            public bool _z;
            public bool _h;
            public bool _c;
            public bool _written;
            public bool _isInt;

            public void Reset()
            {
                _cnt = 0;
                _adr = 0;
                _isCB = false;
                _opcode = 0;
                _str = "";
                _raf = 0;
                _rbc = 0;
                _rde = 0;
                _rhl = 0;
                _pc = 0;
                _sp = 0;
                _n = false;
                _z = false;
                _h = false;
                _c = false;
                _written = true;
                _isInt = false;
            }
        };

        private static int m_callstackSize = 1000;

        private sStackData[] m_contentArray = new sStackData[m_callstackSize];

        //private String m_content;
        private int m_cnt;
        private bool m_bNeedUpdate = false;
        private const String myFormatInst = "{0,-10}{1,-8}{2,-20}{3,-8}{4,-8}{5,-8}{6,-8}{7,-8}{8}";
        private const String myFormatInt = "--INTERRUPT-- {0,-10}{1,-8}";
        FileStream m_saveFile;
        StreamWriter m_writer;
        string m_fileSavePath = "../../log/callstack.txt";
        private ushort m_instToStop = 0;
        private bool m_bSaveCallStack = true;
        private int m_curIdx = 0;

        public CallstackForm()
        {
            InitializeComponent();
            for (int i = 0; i < m_callstackSize; i++ )
            {
                m_contentArray[i].Reset();
            }
            m_cnt = 0;
            textLabels.Text = String.Format(myFormatInst, "#", "PC", "inst", "af", "bc", "de", "hl", "SP", "flags");
            m_bSaveCallStack = this.checkSaveCallstack.Checked;
            if ( m_bSaveCallStack )
            {
                m_saveFile = File.Open(m_fileSavePath, FileMode.OpenOrCreate, FileAccess.Write);
                m_saveFile.SetLength(0);
                m_writer = new StreamWriter(m_saveFile);
                m_writer.AutoFlush = true;
            }
        }

        public void Reset()
        {
            for (int i = 0; i < m_callstackSize; i++)
            {
                m_contentArray[i].Reset();
            }
            m_cnt = 0;
            m_bNeedUpdate = true;
            m_curIdx = 0;
        }

        public void AddInterruptToCallstack( int i )
        {
            int idx = m_cnt % m_callstackSize;
            m_cnt++;
            m_curIdx = idx;
            m_bNeedUpdate = true;
            m_contentArray[idx]._isInt = true;
            m_contentArray[idx]._written = false;
            m_contentArray[idx]._opcode = i;
            m_contentArray[idx]._pc = GameBoy.Cpu.PC;
            if (m_bSaveCallStack)
            {
                UpdateContentArray();
            }
        }

        public void AddToCallstack()
        {
            int idx = m_cnt % m_callstackSize;
            m_cnt++;
            m_curIdx = idx;
            m_bNeedUpdate = true;
            ushort adr = GameBoy.Cpu.PC;
            bool bIsCB = false;
            ushort opcode = GameBoy.Ram.ReadByteAt(adr);
            if (opcode == 0xCB)
            {
                bIsCB = true;
                adr += 0x01;
                opcode = GameBoy.Ram.ReadByteAt(adr);
            }
            m_contentArray[idx]._written = false;
            m_contentArray[idx]._isInt = false;
            m_contentArray[idx]._adr = adr;
            m_contentArray[idx]._isCB = bIsCB;
            m_contentArray[idx]._opcode = opcode;
            m_contentArray[idx]._raf = GameBoy.Cpu.rAF;
            m_contentArray[idx]._rbc = GameBoy.Cpu.rBC;
            m_contentArray[idx]._rde = GameBoy.Cpu.rDE;
            m_contentArray[idx]._rhl = GameBoy.Cpu.rHL;
            m_contentArray[idx]._pc = GameBoy.Cpu.PC;
            m_contentArray[idx]._sp = GameBoy.Cpu.SP;
            m_contentArray[idx]._n = GameBoy.Cpu.NValue;
            m_contentArray[idx]._z = GameBoy.Cpu.ZValue;
            m_contentArray[idx]._h = GameBoy.Cpu.HValue;
            m_contentArray[idx]._c = GameBoy.Cpu.CValue;
            m_contentArray[idx]._cnt = m_cnt;
            if (m_bSaveCallStack)
            {
                UpdateContentArray();
            }
        }

        public void EnableLogging( bool bEnable)
        {
            if (bEnable)
            {
                Reset();
                m_bSaveCallStack = true;
            }
            else
            {
                m_bSaveCallStack = false;
            }
        }

        private void UpdateContentArray()
        {
            for (int i = 0; i < m_callstackSize; i++)
            {
                if (!m_contentArray[i]._written)
                {
                    if (m_contentArray[i]._isInt)
                    {
                        String intStr = "";
                        switch (m_contentArray[i]._opcode)
                        {
                            case 0   : { intStr = "vblank"; break; }
                            case 1 : { intStr = "lcdstat"; break; }
                            case 2    : { intStr = "timer"; break; }
                            case 3   : { intStr = "serial"; break; }
                            case 4   : { intStr = "joypad"; break; }
                            default: { intStr = "error"; break; }
                        }
                        String opcodeStr = String.Format("{0:x2}", m_contentArray[i]._opcode);
                        String pc = String.Format("{0:x4}", m_contentArray[i]._pc);
                        m_contentArray[i]._str = String.Format(myFormatInt, intStr, m_contentArray[i]._pc);
                    }
                    else
                    {
                        Z80Instruction inst = GameBoy.Cpu.decoder.GetInstructionAt(m_contentArray[i]._adr, m_contentArray[i]._isCB);
                        String opcodeStr = String.Format("{0:x2}", m_contentArray[i]._opcode);
                        String name = "????";
                        if (inst!=null)
                        {
                            name = opcodeStr +" - " +inst.ToString(m_contentArray[i]._adr);
                        }
                        String af = String.Format("{0:x4}", m_contentArray[i]._raf);
                        String de = String.Format("{0:x4}", m_contentArray[i]._rde);
                        String bc = String.Format("{0:x4}", m_contentArray[i]._rbc);
                        String hl = String.Format("{0:x4}", m_contentArray[i]._rhl);
                        String pc = String.Format("{0:x4}", m_contentArray[i]._pc);
                        String sp = String.Format("{0:x4}", m_contentArray[i]._sp);
                        String fl = "";
                        if (GameBoy.Cpu.ZValue) { fl += "Z"; } else { fl += "-"; }
                        if (GameBoy.Cpu.NValue) { fl += "N"; } else { fl += "-"; }
                        if (GameBoy.Cpu.HValue) { fl += "H"; } else { fl += "-"; }
                        if (GameBoy.Cpu.CValue) { fl += "C"; } else { fl += "-"; }
                        m_contentArray[i]._str = String.Format(myFormatInst, m_contentArray[i]._cnt, pc, name, af, bc, de, hl, sp, fl);
                    }

                    if (m_bSaveCallStack)
                    {
                        if (m_saveFile == null)
                        {
                            m_saveFile = File.Open(m_fileSavePath, FileMode.OpenOrCreate, FileAccess.Write);
                            m_writer = new StreamWriter(m_saveFile);
                            m_writer.AutoFlush = true;
                        }
                        m_writer.WriteLine(m_contentArray[i]._str);
                    }
                    m_contentArray[i]._written = true;
                }
            }
        }

        public void Init()
        {
            for (int i = 0; i < m_callstackSize; i++)
            {
                m_contentArray[i].Reset();
            }

        }

        public void UpdateForm()
        {
            if (m_bNeedUpdate)
            {

                UpdateContentArray();
                String content = "";
                int c = m_curIdx;
                while (c > -1)
                {
                    content += m_contentArray[c]._str + "\n";
                    c--;
                }
                c = m_callstackSize -1;
                while (c > m_curIdx)
                {
                    content += m_contentArray[c]._str + "\n";
                    c--;
                }
                this.callstackBox.Text = content;
                m_bNeedUpdate = false;
                if (m_writer != null)
                {
                    m_writer.Flush();
                    m_writer.Close();
                    m_writer = null;
                }
                if (m_saveFile != null)
                {
                    m_saveFile.Close();
                    m_saveFile = null;
                }
            }
        }

        private void textInstNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int instNumber = int.Parse(textInstNumber.Text);
                m_instToStop = (ushort)instNumber;
            }
            catch (Exception ex)
            {
                textInstNumber.Text = ""+ex;
                m_instToStop = 0;
            }
        }

        private void callstackBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            EnableLogging(  this.checkSaveCallstack.Checked );

        }

        private void callstackBox_MouseEnter(object sender, EventArgs e)
        {

        }

        private void callstackBox_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.MouseEventArgs ee = (System.Windows.Forms.MouseEventArgs)e;
            if (ee.Button == MouseButtons.Left)
            {
                callstackBox_Click(sender, e);
            }
        }

        private void callstackBox_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void callstackBox_Click(object sender, EventArgs e)
        {
        }

        private void callstackBox_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point p = callstackBox.Location;
            p.X = me.X - p.X;
            p.Y = me.Y - p.Y;
            int l = p.Y / (callstackBox.Font.Height - 1);
            if (l >= 0 && l < m_callstackSize)
            {
                sStackData d = m_contentArray[l];
                GameBoyTest.Debug.DebugFunctions.CodeViewForm().GotoAdress(d._pc, true);
            }
        }
    }
}
