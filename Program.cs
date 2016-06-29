
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GameBoyTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            GameBoy bg = new GameBoy();
        }

        // Decimal to Hexadecimal
        public static string DecToHex(int decValue)
        {
            return string.Format("{0:x}", decValue);
        }

        // Hexadecimal to Decimal
        public static int HexToDec(string hexValue)
        {
            return Int32.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
        }


        public static string Read(byte[] ba, string fromHexa, string toHexa)
        {
            return Read(ba, HexToDec(fromHexa), HexToDec(toHexa));
        }

        public static string Read(byte[] ba, int from, int to)
        {
            StringBuilder sb = new StringBuilder( (to-from) * 2);
            int i = from;
            while( i<to && i<ba.Length)
            {
                sb.Append( (char)ba[i] );
                i++;
            }
            return sb.ToString();
        }

        public static string ReadAt(byte[] ba, string adressHexa)
        {
            return ReadAt( ba, HexToDec(adressHexa) );
        }

        public static string ReadAt(byte[] ba, int adress)
        {
            StringBuilder sb = new StringBuilder(2);
            if (adress < ba.Length)
            {
                sb.Append((char)ba[adress]);
            }
            return sb.ToString();
        }

        public static string test(byte[] ba)
        {
            StringBuilder sb = new StringBuilder(ba.Length * 2);
            int i = 0;
            foreach (byte b in ba)
            {
                i++;
                sb.AppendFormat("{0:x2}", b);
                if (i % 16 == 0)
                {
                    sb.Append("\n");
                }            
            }
            return sb.ToString();
        }
    }
}
