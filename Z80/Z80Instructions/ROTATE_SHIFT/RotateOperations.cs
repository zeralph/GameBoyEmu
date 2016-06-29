using System;
using System.Collections.Generic;
using System.Text;

namespace GameBoyTest.Z80.Z80Instructions.ROTATE_SHIFT
{
    class RotateOperations
    {
        //  --------------v
        //  |             |
        //  <-C<-76543210<-
        public static byte RotateLeftThroughCarry(byte b)
        {
            bool c = (b & 0x80) != 0;
            byte outByte = (byte)(b << 1);
            if( GameBoy.Cpu.CValue)
            {
                outByte |= 0x01;
            }
            GameBoy.Cpu.ZValue = (outByte == 0);
            GameBoy.Cpu.CValue = c;
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = false;

            return outByte;
        }

        //      >---------v
        //      |         |
        //  C<---76543210<-
        public static byte RotateLeft(byte b)
        {
            bool c = (b & 0x80) != 0;
            byte outByte = (byte)(b << 1);
            if (c)
            {
                outByte |= 0x01;
            }

            GameBoy.Cpu.ZValue = (outByte == 0);
            GameBoy.Cpu.CValue = c;
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = false;
            return outByte;
        }

        //  v-------------<
        //  |             |
        //  >-C->76543210-^
        public static byte RotateRightThroughCarry(byte b)
        {
            bool c = (b & 0x01) != 0;
            byte outByte = (byte)(b >> 1);
            if (GameBoy.Cpu.CValue)
            {
                outByte |= 0x80;
            }

            GameBoy.Cpu.ZValue = (outByte == 0);
            GameBoy.Cpu.CValue = c;
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = false;
            return outByte;
        }

        //      v---------<
        //      |         |
        //      >76543210-^-->C
        public static byte RotateRight(byte b)
        {
            bool c = (b & 0x01) != 0;
            byte outByte = (byte)(b >> 1);
            if (c)
            {
                outByte |= 0x80;
            }

            GameBoy.Cpu.ZValue = (outByte == 0);
            GameBoy.Cpu.CValue = c;
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = false;
            return outByte;
        }

        public static byte ShiftLeft(byte b)
        {
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = false;
            byte outByte = (byte)(b << 1);
            GameBoy.Cpu.ZValue = outByte == 0;
            GameBoy.Cpu.CValue = (b & 0x80) != 0;
            return outByte;
        }

        public static byte ShiftRight(byte b)
        {
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = false;
            byte outByte = (byte)(b >> 1);
            GameBoy.Cpu.ZValue = outByte == 0;
            GameBoy.Cpu.CValue = (b & 0x01) != 0;
            return outByte;
        }

        //in a right arithmetic shift, the sign bit (the MSB in two's complement) is shifted in on the left, thus preserving the sign of the operand.
        public static byte ShiftRightSetMSB(byte b)
        {
            byte msb = (byte) (b & 0x80);
            byte outByte = (byte)(b >> 1);
            outByte |= msb;
            GameBoy.Cpu.ZValue = (outByte == 0);
            GameBoy.Cpu.CValue = (b & 0x01) == 1;
            GameBoy.Cpu.NValue = false;
            GameBoy.Cpu.HValue = false;
            
            return outByte;
        }
    }
}
