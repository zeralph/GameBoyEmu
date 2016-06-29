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
    public partial class InterruptsForm : Form
    {
        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public InterruptsForm()
        {
            InitializeComponent();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Init()
        {
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Reset()
        {
            Init();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void UpdateForm()
        {
            byte b;
            //
            b = GameBoy.Ram.ReadByteAt( 0xFF01 );
            this.textFF01.Text = String.Format("{0:x2}", b);
            //
            b = GameBoy.Ram.ReadByteAt(0xFF02);
            this.textFF02.Text = String.Format("{0:x2}", b);
            //
            b = GameBoy.Ram.ReadByteAt(0xFF04);
            this.textFF04.Text = String.Format("{0:x2}", b);
            //
            b = GameBoy.Ram.ReadByteAt(0xFF05);
            this.textFF05.Text = String.Format("{0:x2}", b);
            //
            b = GameBoy.Ram.ReadByteAt(0xFF06);
            this.textFF06.Text = String.Format("{0:x2}", b);
            //
            b = GameBoy.Ram.ReadByteAt(0xFF07);
            this.textFF07.Text = String.Format("{0:x2}", b);
            //
            b = GameBoy.Ram.ReadByteAt(0xFF0F);
            this.textFF0F.Text = ToBinaryStr(b);
            //
            b = GameBoy.Ram.ReadByteAt(0xFFFF);
            this.textFFFF.Text = ToBinaryStr(b);// String.Format("{0:x2}", b);
            //
            this.textIME.Text = GameBoy.Cpu.IMEStatus()?"ON":"OFF";
            
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private String ToBinaryStr(byte b)
        {
            return Convert.ToString(b, 2).PadLeft(8, '0');
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
