using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBoyTest.Debug;
using System.Drawing;
using System.Drawing.Imaging;

namespace GameBoyTest.State
{
    class StateSystem
    {

        public static bool saveState()
        {
            string path = "..//..//states//";
            DateTime d = DateTime.UtcNow;
            string fileName = path+GameBoy.Cartridge.GetTitle() + "_" + d.Year + d.Month + d.Day + d.Hour + d.Minute + d.Second+".sta";
            FileStream fs = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            Bitmap b = GameBoy.Screen.GetBitmapCopy();
            GameBoy.Cpu.Stop();
            //IMAGE
            MemoryStream ms = new MemoryStream();
            b.Save(ms, ImageFormat.Png);
            byte[] bitmapData = ms.ToArray();
            long l = (long)bitmapData.Length;
            byte[] lb = BitConverter.GetBytes((long)l);
            fs.Write(lb, 0, lb.Length);
            fs.Write(bitmapData, 0, bitmapData.Length);
            //CARTRIDGE
            byte[] cartdridge = GameBoy.Cartridge.Serialize();
            fs.Write(cartdridge, 0, cartdridge.Length);
            //MEM
            byte[] mem = GameBoy.Ram.GetmemoryMap();
            fs.Write(mem, 0, mem.Length);
            //CPU
            byte[] cpuArray = GameBoy.Cpu.Serialize();
            fs.Write(cpuArray, 0, cpuArray.Length);
            fs.Close();
            GameBoy.Cpu.Start();
            return true;
        }

        public static Bitmap GetStateImage(string filename)
        {
            FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read);
            int l = (int)fs.Length;
            byte[] ba = new byte[l];
            fs.Read(ba, 0, l);
            fs.Close();
            long imageSize = BitConverter.ToInt64(ba, 0);
            MemoryStream ms = new MemoryStream(ba, 8, (int)imageSize);
            Bitmap b = new Bitmap(ms);
            return b;
        }

        public static bool loadState(string filename)
        {
            try
            {                
                GameBoy.Cpu.Init();
                DebugFunctions.ResetDebug();
                FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read);
                int l = (int)fs.Length;
                byte[] ba = new byte[l];
                fs.Read(ba, 0, l);
                fs.Close();
                int startAdr = 0;
                long imageSize = BitConverter.ToInt64(ba, 0);
                startAdr += 8;
                startAdr += (int)imageSize;
                //CARTRIDGE
                startAdr = GameBoy.Cartridge.Unserialize(ref ba, startAdr);
                //MEM
                startAdr = GameBoy.Ram.Unserialize(ref ba, startAdr);
                //CPU
                startAdr = GameBoy.Cpu.Unserialize(ref ba, startAdr);
                GameBoy.Cpu.Start();
                return true;
            }
            catch(Exception e)
            {
                Console.Write(e);
                return false;
            }
        }
    }
}
