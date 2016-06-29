#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBoyTest.Debug;
using Be.Windows.Forms;
using GameBoyTest;

#if DEBUG
using System.Windows.Forms;
using GameBoyTest.Debug.Visual;
#endif

namespace GameBoyTest.Memory
{

    public delegate void OnRamChange(object sender, MappedMemory.RamEventArgs e);

// #if DEBUG
//     public class MappedMemory : Be.Windows.Forms.IByteProvider
// #else
//     public class MappedMemory 
// #endif
    public class MappedMemory 
    {
        public static event OnRamChange m_ramChanged;

        public enum eMemoryMap
        {   
            eMemoryMap_Bank0            = 0x0000,
            eMemoryMap_Bank0_code       = 0x0200,
            eMemoryMap_BankN            = 0x4000,
            eMemoryMap_VideoRam         = 0x8000,
            eMemoryMap_ExternalRam      = 0xA000,
            eMemoryMap_CpuRam0          = 0xC000,
            eMemoryMap_CpuRam1          = 0xD000,
            eMemoryMap_Unused0          = 0xE000,
            eMemoryMap_SpriteTable      = 0xFE00,
            eMemoryMap_Unused1          = 0xFEA0,
            eMemoryMap_IO               = 0xFF00,
            eMemoryMap_HighRam          = 0xFF80,
            eMemoryMap_InterruptRegister= 0xFFFF,
            eMemoryMap_MemorySize       = 0x10001,
        };

        byte[] m_MemoryMap;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public MappedMemory()
        {
            m_MemoryMap = new byte[(int)eMemoryMap.eMemoryMap_MemorySize];
            for (long i = 0; i < (int)eMemoryMap.eMemoryMap_MemorySize; i++)
            {
                m_MemoryMap[i] = 0x00;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public byte[] Serialize()
        {
            return m_MemoryMap;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public int Unserialize(ref byte[] buffer, int startAdr)
        {
            Array.Copy(buffer, startAdr, m_MemoryMap, 0, (int)eMemoryMap.eMemoryMap_MemorySize);
            return startAdr + (int)eMemoryMap.eMemoryMap_MemorySize;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public byte ReadByteAt(ushort address)
        {
            return m_MemoryMap[address];
        }

        public sbyte ReadSignedByteAt(ushort address)
        {
            return (sbyte)m_MemoryMap[address];
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public ushort ReadUshortAt(ushort address)
        {
            ushort ret;
            ret =(ushort)(m_MemoryMap[address+1] << 8 | m_MemoryMap[address]);
            return ret;
        }

        public void Clear()
        {
            OnRamChange( 0x00 );
            for (long i = 0; i < (int)eMemoryMap.eMemoryMap_MemorySize; i++)
            {
                m_MemoryMap[i] = 0x00;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void WriteAt(ushort address, byte data, bool bWriteInRom = false )
        {
            if( address < 0x8000 && ! bWriteInRom )
            {
                SwitchMBC(address, data);
            }
            else
            {
                OnRamChange(address);
                m_MemoryMap[address] = data;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void WriteUshortAt(ushort address, ushort data)
        {
            OnRamChange(address);
            byte b0 = (byte)(data & 0xFF);
            byte b1 = (byte)((data >> 0x08) & 0xFF);
            m_MemoryMap[address] = b0;
            m_MemoryMap[address + 1] = b1;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public static OnRamChange RamHasChanged
        {
            get{ return m_ramChanged;}
            set{ m_ramChanged=value;}
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void WriteChunkAt(ushort destAdress, ref byte[] source, long sourceAdress, int lenght)
        {
            if (lenght > 0)
            {
                Array.Copy(source, sourceAdress, m_MemoryMap, destAdress, lenght);
            }
        }

        public byte[] SwapRam(ref byte[] newChunck, int startAdr)
        {
            byte[] bout = new byte[0x1000];
            Array.Copy(m_MemoryMap, 0xA000, bout, startAdr, 0x1000);
            Array.Copy(newChunck, 0, m_MemoryMap, 0xA000, 0x1000);
            return bout;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void TransfertOAM(ushort startAdress)
        {
            Array.Copy(m_MemoryMap, startAdress, m_MemoryMap, 0xFE00, 0xA0);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void ReadAt(ushort address, ushort lenght, ref byte[] data)
        {
            DebugFunctions.ASSERT((address + lenght < (int)eMemoryMap.eMemoryMap_MemorySize), "ReadAt outside of memory !");
            DebugFunctions.ASSERT((data.Length == lenght), "ReadAt wrong ref array size");
            for (ushort i = 0; i < lenght; i++)
            {
                data[i] = m_MemoryMap[address + i];
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void ReadAt(ushort address, ushort lenght, ref char[] data)
        {
            DebugFunctions.ASSERT((address + lenght < (int)eMemoryMap.eMemoryMap_MemorySize), "ReadAt outside of memory !");
            DebugFunctions.ASSERT((data.Length == lenght), "ReadAt wrong ref array size");
            for (ushort i = 0; i < lenght; i++)
            {
                data[i] = (char)m_MemoryMap[address + i];
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public byte[] ReadAt(ushort address, ushort lenght)
        {
            DebugFunctions.ASSERT((address + lenght < (int)eMemoryMap.eMemoryMap_MemorySize), "ReadAt outside of memory !");
            byte[] bOut = new byte[lenght];
            for (ushort i = 0; i < lenght; i++)
            {
                bOut[i] = m_MemoryMap[address + i];
            }
            return bOut;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public String GetDataToString(long start, long size)
        {
            String s="";
            for (long i = start; i < start + size; i++)
            {
                s += " " + String.Format("{0:x2}", m_MemoryMap[i]);
            }
            return s;
        }

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnRamChange( ushort adress )
        {
            RamEventArgs e = new RamEventArgs(adress);
            if (m_ramChanged != null)
                m_ramChanged(this, e);
        }

#if DEBUG

        public long Length { get { return m_MemoryMap.Length; } }

        //public event EventHandler Changed;

        //public event EventHandler LengthChanged;

        public void ApplyChanges()
        {
        }

        public void DeleteBytes(long index, long length)
        {
        }

        public bool HasChanges()
        {
            return false;
        }

        public void InsertBytes(long index, byte[] bs)
        {
        }

        public void IncDivider()
        {
            byte b;
            b = m_MemoryMap[0xFF04];
            b++;
            m_MemoryMap[0xFF04] = b;
        }

        public byte ReadByte(long index)
        {
            return m_MemoryMap[index];
        }

        public bool SupportsDeleteBytes()
        {
            return false;
        }

        public bool SupportsInsertBytes()
        {
            return false;
        }

        public bool SupportsWriteByte()
        {
            return false;
        }

        public byte[] GetmemoryMap()
        {
            return m_MemoryMap;
        }

        //Interface implementation
        public void WriteByte(long index, byte value)
        {
            m_MemoryMap[index] = value;
            OnRamChange((ushort)index);
        }
#endif

        // FireEventArgs: a custom event inherited from EventArgs.

        public class RamEventArgs : EventArgs
        {
            public RamEventArgs(ushort adress)
            {
                this.adress = adress;
            }
            public ushort adress;
        }	


        //
        private void SwitchMBC( ushort adress, byte b)
        {
            GameBoy.Cartridge.LoadBank(adress, b);
        }


    }
}

