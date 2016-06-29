using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBoyTest.Memory;
using System.Threading;

namespace GameBoyTest.Debug
{
    class SB_Debug
    {
        private String output;

        public SB_Debug()
        {
            MappedMemory.RamHasChanged += new OnRamChange(OnRamChanged);
            output = "";
        }

        public void Init()
        {
            output = "";
        }

        private void OnRamChanged(object sender, MappedMemory.RamEventArgs e)
        {
            ushort adr = e.adress;
            if ( adr ==  0xFF02 )
            {
                if (GameBoy.Ram.ReadByteAt(0xFF02) == 0x81)
                {
                    byte b = GameBoy.Ram.ReadByteAt(0xFF01);
                    output += (char)(b);
                    //GameBoy.Cpu.Stop();
                }
            }
            if ( adr ==  0xFF10 )
            {
                int y = 0; 
                y++;
            }
        }
    }
}
