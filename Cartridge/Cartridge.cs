using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.AccessControl;
using GameBoyTest.Z80;

namespace GameBoyTest
{
    public class Cartridge
    {
        public const int MBC_1 = 0x03;
        public const int CARTRIDGE_RAM_SIZE = 0x4000 ;

        private byte[] m_cartridgeData;
        private byte[] m_RamCartridgeData; 
        private byte[] m_bank0;
        private byte[] m_bankN;

        private String m_file = "";
        private String m_path = "";

        private byte m_mbc1RamMode = 0;
        private byte m_currentRomBank = 0;
        private byte m_currentRamBank = 0;
        private byte m_mbcMode = 0;
        private bool m_RamBankWriteEnabled = false;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public Cartridge()
        {
            m_bank0 = new byte[0x4000];
            m_bankN = new byte[0x4000];
            for (ushort i = 0; i < 0x4000; i++)
            {
                m_bank0[i] = 0x00;
                m_bankN[i] = 0x00;
            }
            m_RamCartridgeData = new byte[CARTRIDGE_RAM_SIZE];
            m_file = "";
            m_mbc1RamMode = 0;
            m_currentRomBank = 0;
            m_currentRamBank = 0;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public bool HasGame()
        {
            return m_file.Length > 0;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public byte[] Serialize()
        {
            int l = CARTRIDGE_RAM_SIZE + 4;
            byte[] ba = new byte[l];
            Array.Copy(m_RamCartridgeData, ba, CARTRIDGE_RAM_SIZE);
            ba[CARTRIDGE_RAM_SIZE+0] = m_mbc1RamMode;
            ba[CARTRIDGE_RAM_SIZE+1] = m_currentRomBank;
            ba[CARTRIDGE_RAM_SIZE + 2] = m_currentRamBank;
            ba[CARTRIDGE_RAM_SIZE + 3] = (byte)(m_RamBankWriteEnabled?1:0);
            return ba;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public int Unserialize( ref byte[] buffer, int startAdr)
        {
            Array.Copy(buffer, startAdr, m_RamCartridgeData, 0, CARTRIDGE_RAM_SIZE);
            startAdr += CARTRIDGE_RAM_SIZE;
            m_mbc1RamMode = buffer[startAdr];
            startAdr += 1;
            m_currentRomBank = buffer[startAdr];
            startAdr += 1;
            m_currentRamBank = buffer[startAdr];
            startAdr += 1;
            m_RamBankWriteEnabled = (buffer[startAdr] == 1);
            startAdr += 1;
            LoadRomBankIndex(m_currentRomBank);
            LoadRamBankIndex(m_currentRamBank);
            return startAdr;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public bool Init( String fullPath, String name, bool bSaveToIni )
        {
            try
            {
                m_file = name;
                m_path = Path.GetDirectoryName(fullPath);
                FileStream fileStream = File.Open(fullPath, FileMode.Open, FileAccess.Read);
                if (bSaveToIni)
                {
                    SavePathToIniFile(m_path, m_file);
                }
                //copy filestream to buffer
                if (fileStream != null)
                {
                    int lenght = (int)fileStream.Length;
                    m_cartridgeData = new byte[lenght];
                    fileStream.Position = 0;
                    fileStream.Read(m_cartridgeData, 0x00, lenght);
                }
                fileStream.Close();
                //copy first two banks
                CopyRomDataToBank(0x00, 0x00);
                CopyRomDataToBank(0x4000, 0x4000);
                m_mbc1RamMode = 0;
                m_mbcMode = GameBoy.Ram.ReadByteAt(0x147);
                if (m_mbcMode == MBC_1)
                {
                    LoadMBC1Save();
                }
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        
        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void CopyRomDataToBank( long dataStartAdress, ushort bankStartAdress)
        {
            int l = (int)(m_cartridgeData.Length - dataStartAdress);
            l = Math.Max( l, 0);
            ushort length = (ushort)Math.Min(l, 0x4000);
            GameBoy.Ram.WriteChunkAt(bankStartAdress, ref m_cartridgeData, dataStartAdress, length);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Update()
        {
        }

        public void ShowMBCError( int mbc )
        {
            String s = CartridgeInfos.GetCartridgeInfos( (byte)mbc);
            string message = String.Format("Cartridge has MBC switching, will not work yet ! (" + s + ")");
            string caption = "Error";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public bool LoadBank( ushort adress, byte b )
        {
            switch (m_mbcMode)
            {
                case 00:
                    {
                        return true;
                    }
                case 01:
                case 02:
                case MBC_1:
                    {
                        LoadBankMBC1( adress, b );
                        return true;
                    }
                case 5:
                case 6:
                    {
                        LoadBankMBC2(adress, b);
                        return true;
                    }
                default:
                    {
                        ShowMBCError(m_mbcMode);
                        GameBoy.Cpu.Stop();
                        return false;
                    }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void LoadMBC1Save()
        {
            try
            {
                if (m_mbcMode == MBC_1)
                {
                    String fullPath = m_path + "\\" + m_file;
                    String find = ".gb";
                    String replace = ".sav";
                    int Place = fullPath.LastIndexOf(find);
                    string result = fullPath.Remove(Place, find.Length).Insert(Place, replace);
                    FileStream fs = File.Open(result, FileMode.Open, FileAccess.Read);
                    //copy filestream to buffer
                    if (fs != null)
                    {
                        int lenght = m_RamCartridgeData.Length;
                        fs.Position = 0;
                        fs.Read(m_RamCartridgeData, 0x00, lenght);
                        //force bank0 into memory
                        GameBoy.Ram.SwapRam(ref m_RamCartridgeData, 0);
                    }
                    fs.Close();
                }
            }
            catch(Exception e)
            {
                e.Message.ToString();
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void SaveMBC1Save()
        {          
            String fullPath = m_path + "\\" + m_file;
            String find = ".gb";
            String replace = ".sav";
            int Place = fullPath.LastIndexOf(find);
            string result = fullPath.Remove(Place, find.Length).Insert(Place, replace);
            FileStream fs = File.Open(result, FileMode.OpenOrCreate, FileAccess.Write);
            fs.SetLength(0);
            StreamWriter sw = new StreamWriter(fs);
            char[] c = new char[CARTRIDGE_RAM_SIZE];
            for (int i = 0; i < CARTRIDGE_RAM_SIZE; i++)
            {
                c[i] = (char)m_RamCartridgeData[i];
            }
            sw.Write(c, 0, CARTRIDGE_RAM_SIZE);
            sw.Close();
            //fs.Close();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Exit()
        {
            if (m_mbcMode == MBC_1)
            {
                SaveMBC1Save();
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void LoadBankMBC1( ushort adress, byte b )
        {
            if( adress >= 0x6000 && adress <= 0x7FFF )   // switch MBC mode
            {
                b &= 0x01;
                if( b != 0)
                {
                    m_mbc1RamMode = 1;
                }
                else
                { 
                    m_mbc1RamMode = 0; 
                }
            }
            else if( adress >= 0x2000 && adress <= 0x3FFF)   //
            {
                LoadRomBankIndex(b & 0x1F );
            }
            else if( adress >= 0x4000 && adress <= 0x5FFF )
            {
                if( m_mbc1RamMode == 1)
                {
                    LoadRamBankIndex(b & 0x03);
                }
                else
                {
                    LoadRamBankIndex(0);
                }
            }

        }
        
        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void LoadBankMBC2(ushort adress, byte b)
        {
            if( adress >= 0x2000 && adress <= 0x3FFF)   //
            {
                LoadRomBankIndex(b & 0x0F);
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void LoadRomBankIndex( int index )
        {
            if( index == 0x0 )
            { 
                index = 0x1; 
            }
            else if( index == 0x20 )
            {
                index = 0x21;
            }
            else if (index == 0x40)
            {
                index = 0x41;
            }
            else if (index == 0x60)
            {
                index = 0x61;
            }
            int startAdr = index * 0x4000;
            if (m_cartridgeData.Length > startAdr)
            {
                m_currentRomBank = (byte)index;
                long startAdress = (long)index * 0x4000;
                CopyRomDataToBank(startAdress, 0x4000);
            }
            else
            {
                //throw new Exception("zob!");
            }
        }
        
        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void LoadRamBankIndex(int index)
        {
            if (index != m_currentRamBank)
            {
                System.Diagnostics.Debug.WriteLine("LoadRamBankIndex " + m_currentRamBank +" --> " + index);
                int lastRamBank = m_currentRamBank;
                int startAdr = 0x1000 * index;
                //copy back current ram bank
                byte[] bin = GameBoy.Ram.SwapRam( ref m_RamCartridgeData, startAdr );
                m_currentRamBank = (byte)index;
                Array.Copy(bin, 0, m_RamCartridgeData, lastRamBank * 0x1000, 0x1000);
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public String GetTitle()
        {
            ushort i = 0x0134;
            byte b = 0;
            String s = "";
            do
            {
                b = GameBoy.Ram.ReadByteAt(i);
                s += (char)b;
                i++;
            }
            while (b != 0x00 && i<0x0143);
            s = s.Replace("\0", string.Empty);
            return s;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void SavePathToIniFile(string path, string name)
        {
            int index = GameBoy.Parameters.RecentFileIdx;
            index++;
            if (index >= 5)
            {
                index = 0;
            }
            GameBoy.Parameters.Recent[index].path = path;
            GameBoy.Parameters.Recent[index].name = name;
            GameBoy.Parameters.RecentFileIdx = index;
            int n = GameBoy.Parameters.NbRecentFile;
            if( n<5)
            {
                n++;
            }
            GameBoy.Parameters.NbRecentFile = n;
        }
    }
}
