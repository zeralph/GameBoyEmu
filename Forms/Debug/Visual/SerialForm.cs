using GameBoyTest.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameBoyTest.Forms.Debug.Visual
{
    public partial class SerialForm : Form
    {
        private String m_output;
        private bool m_update;

        public SerialForm()
        {
            InitializeComponent();
            m_update = false;
            MappedMemory.RamHasChanged += new OnRamChange(OnRamChanged);
        }

        public void Init()
        {
            m_output = "";
            serialTextBox.Text = "";
            m_update = false;
        }

        public void Reset()
        {
            Init();
            serialTextBox.Text = m_output;
        }

        public void UpdateForm()
        {
            if(m_update)
            {
                String str = m_output;
                str = str.Replace("\n", Environment.NewLine);
                serialTextBox.Text = str;
                m_update = false;
            }
        }

        private void OnRamChanged(object sender, MappedMemory.RamEventArgs e)
        {
            ushort adr = e.adress;
            if (adr == 0xFF02)
            {
                if (GameBoy.Ram.ReadByteAt(0xFF02) == 0x81)
                {
                    m_update = true;
                    byte b = GameBoy.Ram.ReadByteAt(0xFF01);
                    m_output += (char)(b);
                }
            }
        }
    }
}
