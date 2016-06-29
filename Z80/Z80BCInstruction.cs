using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Z80
{
    public class Z80BCInstruction:Z80Instruction
    {
        public Z80BCInstruction():base()
        {
        }

        public override bool IsBC()
        {
            return true;
        }
    }
}
