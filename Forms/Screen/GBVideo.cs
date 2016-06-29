using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest.Forms.Screen
{
    class GBVideo
    {
        private bool m_started = false;

        public void Init()
        {

        }

        public void Start()
        {
            m_started = true;
        }

        public void Update()
        {
            byte currentLine = 0;
            GameBoy.Ram.WriteByte(0xFF44, currentLine);
        }

    }
}
