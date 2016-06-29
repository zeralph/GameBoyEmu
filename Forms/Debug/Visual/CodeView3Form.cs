using GameBoyTest.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace GameBoyTest.Forms.Debug.Visual
{
    public partial class CodeView3Form : Form
    {
        StringBuilder m_strBuilder;
        bool[] m_breakPoints;
        int[] m_lineToAdress;
        int[] m_adressToLine;
        int m_breakAtAdress;
        bool m_isBreak;
        int m_nbTotLines = 0;
        ushort m_lastPC = 0;
        int m_curSelectedLineNumber = 0;

        public CodeView3Form()
        {
            InitializeComponent();
            m_strBuilder = new StringBuilder(0xFFFF*50);
            m_breakPoints = new bool[0x10000];
            m_lineToAdress = new int[0x10000];
            m_adressToLine = new int[0x10000];
            MappedMemory.RamHasChanged += new OnRamChange(OnRamChanged);
        }

        public void Init()
        {
            m_breakAtAdress = -1;
            m_isBreak = false;
            m_nbTotLines = 0;
            for (int i = 0; i < 0xFFFF; i++)
            {
                m_breakPoints[i] = false;
            }
            m_curSelectedLineNumber = 0;
            FillTextCode();
        }

        public void UpdateForm()
        {
            ushort pc = GameBoy.Cpu.PC;
            if (pc != m_lastPC)
            {
                int line = m_adressToLine[pc];
                SwitchCursorToLine( line, true);
                line = m_adressToLine[m_lastPC];
                SwitchCursorToLine( line, false);
                m_lastPC = pc;
            }
        }

        private void SwitchCursorToLine( int line, bool enable )
        {
            int firstCharPosition = codeBox.GetFirstCharIndexFromLine(line);
            if (line >= 0 && line < codeBox.Lines.Length)
            {
                String s = codeBox.Lines[line];
                String[] pouet = codeBox.Lines;
                if (enable)
                {
                    s = s.Remove(0, 2).Insert(0, "->");
                }
                else
                {
                    s = s.Remove(0, 2).Insert(0, "  ");
                }
                pouet[line] = s;
                codeBox.Lines = pouet;
            }
        }

        public void GotoAdress( ushort adr, bool highlightLine )
        {
            int line = m_adressToLine[adr];
            int index = codeBox.GetFirstCharIndexFromLine(line-1);
            codeBox.Select(index, 0);
            codeBox.ScrollToCaret();
            if (highlightLine)
            {
                HighlightCurrentLine();
            }
        }

        public bool HasBreakPointAtLine( int line )
        {
            int adress = m_lineToAdress[line];
            return m_breakPoints[adress];
        }

        public bool DoBreakIfNeeded(ushort adress)
        {
            if (!m_isBreak)
            {
                if (m_breakPoints[adress])
                {
                    m_breakAtAdress = adress;
                    m_isBreak = true;
                    return true;
                }
                return false;
            }
            else
            {
                if (m_breakPoints[adress] && m_breakAtAdress != adress)
                {
                    m_breakAtAdress = adress;
                    return true;
                }
                else
                {
                    m_breakAtAdress = -1;
                    m_isBreak = false;
                    return false;
                }
            }
        }

        public int DecodeData( int adress, ref StringBuilder output)
        {
            int outAdr = 0;
            String bp = m_breakPoints[adress] ? "X " : "  ";
            byte data = GameBoy.Ram.ReadByteAt((ushort)adress);
            string s = "";
            s += String.Format("  \t{0,-6}{1,-6}{2,-20}", GetLocation(adress), String.Format("{0:x4} ", adress).ToUpper(), String.Format("{0:x2} ", data).ToUpper());
            output.AppendLine(s);
            outAdr++;
            return outAdr;
        }

        public int DecodeInstruction( int adress, ref StringBuilder output)
        {
            const String myFormat = "  \t{0,-6}{1,-6}{2,-20}{3,-20}{4,-4}";
            String bytecode = "";
            ushort i= (ushort)adress;
            int outAdr = 0;
            String bp = m_breakPoints[adress] ? "X" : "";
            byte opcode = GameBoy.Ram.ReadByteAt(i);
            bool isCB = false;
            if (opcode == 0xCB)
            {
                isCB = true;
            }
            if (isCB)
            {
                i += 0x01;
                outAdr++;
            }
            Z80.Z80Instruction inst = GameBoy.Cpu.decoder.GetInstructionAt(i, isCB);
            if (inst != null)
            {
                byte length = inst.GetLenght((ushort)i);
                for (byte j = 0; j < length; j++)
                {
                    byte b = GameBoy.Ram.ReadByteAt((ushort)(i + j));
                    if( isCB )
                    {
                        bytecode += "CB ";
                    }
                    bytecode += String.Format("{0:x2} ", b);
                }
                outAdr += length;
                string s = bp;
                s += String.Format(myFormat, GetLocation(adress), String.Format("{0:x4} ", (adress)).ToUpper(), bytecode.ToUpper(), inst.ToString(i), ";" + length);
                output.AppendLine(s);
            }
            else
            {
                byte b = GameBoy.Ram.ReadByteAt((ushort)(i));
                bytecode += String.Format("{0:x2} ", b);
                output.AppendLine(String.Format(myFormat, GetLocation(adress), String.Format("{0:x4} ", (adress)).ToUpper(), bytecode.ToUpper(), "-", ";1"));
                outAdr++;
            }
            return outAdr;
        }

        private String GetLocation( int adress )
        {
            if (adress < (int)MappedMemory.eMemoryMap.eMemoryMap_BankN)
            {
                return "ROM0";
            }
            else if (adress < (int)MappedMemory.eMemoryMap.eMemoryMap_VideoRam)
            {
                return "ROM1";
            }
            else if (adress < (int)MappedMemory.eMemoryMap.eMemoryMap_CpuRam0)
            {
                return "VRAM";
            }
            else if (adress < (int)MappedMemory.eMemoryMap.eMemoryMap_CpuRam1)
            {
                return "RAM0";
            }
            else if (adress < (int)MappedMemory.eMemoryMap.eMemoryMap_Unused0)
            {
                return "RAM1";
            }
            else if (adress < (int)MappedMemory.eMemoryMap.eMemoryMap_HighRam)
            {
                return "I/O";
            }
            else if (adress < (int)MappedMemory.eMemoryMap.eMemoryMap_InterruptRegister)
            {
                return "HRAM";
            }
            else
            {
                return "I/O";
            }
        }

        private void FillTextCode()
        {
            m_strBuilder.Clear();
            int adress = 0;
            int line = 0;
            while (adress < 65535) //for (int adress = 0; adress < 65535; adress++)
            {
                m_lineToAdress[line] = adress;
                line++;
                if (adress == 0x01FD)
                {
                    int yy = 0;
                    yy++;
                }
                if (adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_Bank0 && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_BankN ||
                    adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_BankN && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_VideoRam ||
                    adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_CpuRam0 && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_CpuRam1 ||
                    adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_CpuRam1 && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_Unused0 ||
                    adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_HighRam && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_InterruptRegister
                    )
                {
                    //adress++;
                    adress += DecodeInstruction(adress, ref m_strBuilder);
                }
                else
                {
                    adress += DecodeData(adress, ref m_strBuilder);
                }
            }
            //
            this.codeBox.Text = m_strBuilder.ToString();

            for (int i = 0; i < 0xFFFF; i++)
            {
                int adr = m_lineToAdress[i];
                m_adressToLine[adr] = i;
            }
            m_nbTotLines = line;
        }

        private void textCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void textCode_Click(object sender, EventArgs e)
        {
        }

        private void textCode_DoubleClick(object sender, EventArgs e)
        {
        }

        private void OnRamChanged(object sender, MappedMemory.RamEventArgs e)
        {
            int adress = e.adress;
            if (adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_Bank0_code && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_BankN ||
                adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_BankN && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_VideoRam ||
                adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_CpuRam0 && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_CpuRam1 ||
                adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_CpuRam1 && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_Unused0 ||
                adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_HighRam && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_InterruptRegister
                )
            {
                
            }
        }

        private void textCode_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.MouseEventArgs ee = (System.Windows.Forms.MouseEventArgs)e;
            if (ee.Button == MouseButtons.Left)
            {
                textCode_Click(sender, e);
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            FillTextCode();
        }

        private void gotoCursor_Click(object sender, EventArgs e)
        {
        }

        private void textAdress_TextChanged(object sender, EventArgs e)
        {

        }

        public void GotoPC()
        {
            GotoAdress( GameBoy.Cpu.PC, true);
        }

        private void textAdress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (textAdress.Text.Length > 0)
                {
                    try
                    {
                        int adr = int.Parse(textAdress.Text, NumberStyles.HexNumber);
                        if (adr < 0xFFFF)
                        {
                            GotoAdress( (ushort)adr, true);
                        }
                    }
                    catch( Exception ex )
                    {
                        textAdress.Text = ""+ex;
                    }
                }
            }   
        }

        private void codeBox1_Load(object sender, EventArgs e)
        {

        }

        private void codeBox_Click(object sender, EventArgs e)
        {
            HighlightCurrentLine();
        }

        private int GetClickedLine()
        {
            int cursorPosition = codeBox.SelectionStart;
            int lineIndex = codeBox.GetLineFromCharIndex(cursorPosition);
            return lineIndex;
        }

        public void SwitchBreakPointAtLine(int line)
        {
            int adr = m_lineToAdress[line];
            m_breakPoints[adr] = !m_breakPoints[adr];
            if (m_breakPoints[adr])
            {
                colorLine(line, Color.Red, Color.White);
            }
            else
            {
                colorLine(line, SystemColors.Window, Color.Black);
            }
        }

        public void colorLine(int line, Color back, Color front)
        {
            // Save current selection
            int selectionStart = codeBox.SelectionStart;
            int selectionLength = codeBox.SelectionLength;

            // Get character positions for the current line
            int firstCharPosition = codeBox.GetFirstCharIndexFromLine( line );
            int lineNumber = codeBox.GetLineFromCharIndex(firstCharPosition);
            int lastCharPosition = codeBox.GetFirstCharIndexFromLine(lineNumber + 1);
            if (lastCharPosition == -1)
                lastCharPosition = codeBox.TextLength;

            // Set new color
            codeBox.SelectionStart = firstCharPosition;
            codeBox.SelectionLength = lastCharPosition - firstCharPosition;
            if (codeBox.SelectionLength > 0)
                codeBox.SelectionBackColor = back;

            // Reset selection
            codeBox.SelectionStart = selectionStart;
            codeBox.SelectionLength = selectionLength;
        }

        private int GetAdressFromLine(int line)
        {
            return m_lineToAdress[line];
        }

        private void codeBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void codeBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int l = GetClickedLine();
            SwitchBreakPointAtLine(l);
            string s = "";
            breakPointsBox.Items.Clear();
            int size = m_breakPoints.Length;
            for (int i = 0; i < size; i++)
            {
                if (m_breakPoints[i])
                {
                    s = String.Format("{0:x2}\n", i).ToUpper();
                    breakPointsBox.Items.Add(s);
                }
            }
        }

        private void HighlightCurrentLine()
        {
            // Save current selection
            int selectionStart = codeBox.SelectionStart;
            int selectionLength = codeBox.SelectionLength;

            // Get character positions for the current line
            int firstCharPosition = codeBox.GetFirstCharIndexOfCurrentLine();
            int lineNumber = codeBox.GetLineFromCharIndex(firstCharPosition);
            int lastCharPosition = codeBox.GetFirstCharIndexFromLine(lineNumber + 1);
            if (lastCharPosition == -1)
                lastCharPosition = codeBox.TextLength;

            // Clear any previous color
            if (lineNumber != m_curSelectedLineNumber)
            {
                int previousFirstCharPosition = codeBox.GetFirstCharIndexFromLine(m_curSelectedLineNumber);
                int previousLastCharPosition = codeBox.GetFirstCharIndexFromLine(m_curSelectedLineNumber + 1);
                if (previousLastCharPosition == -1)
                    previousLastCharPosition = codeBox.TextLength;

                codeBox.SelectionStart = previousFirstCharPosition;
                codeBox.SelectionLength = previousLastCharPosition - previousFirstCharPosition;
                if (HasBreakPointAtLine(m_curSelectedLineNumber))
                {
                    codeBox.SelectionBackColor = Color.Red;
                }
                else
                {
                    codeBox.SelectionBackColor = SystemColors.Window;
                }     
                m_curSelectedLineNumber = lineNumber;
            }

            // Set new color
            codeBox.SelectionStart = firstCharPosition;
            codeBox.SelectionLength = lastCharPosition - firstCharPosition;
            if (codeBox.SelectionLength > 0)
                codeBox.SelectionBackColor = Color.PaleTurquoise;

            // Reset selection
            codeBox.SelectionStart = selectionStart;
            codeBox.SelectionLength = selectionLength;
        }

        private void breakPointsBox_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void breakPointsBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String s = (String)breakPointsBox.Items[breakPointsBox.SelectedIndex];
            int i = int.Parse(s, System.Globalization.NumberStyles.HexNumber);
            //MessageBox.Show("click on item " + i);
            GotoAdress((ushort)i, false);
        }
    }
}


