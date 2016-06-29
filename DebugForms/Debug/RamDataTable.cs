using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using GameBoyTest.Memory;
using System.Threading;
using System.Runtime.Serialization;

namespace GameBoyTest.Debug
{

    public delegate void OnTableChange(object sender, RamDataTable.TableEventArgs e);
    [Serializable]
    public class RamDataTable : DataTable, ISerializable
    {
        public enum eRamTableType
        {
            eRamTableType_data = 0,
            eRamTableType_code,
            eRamTableType_io,
            eRamTableType_ir,
        }

        public enum eRamTableColumn
        {
            eColumn_breakPoint = 0,
            eColumn_zone,
            eColumn_adress,
            eColumn_value,
            eColumn_instruction,
            eColumn_length,
            eColumn_valid,
            eColumn_adressINT,
            eColumn_count,
        }

        public static event OnTableChange m_tableChanged;
        private eRamTableType m_type;
        private ushort m_start;
        private ushort m_cur;
        private long m_end;
        private ushort m_displayLenght;
        private string m_name;
        private bool m_isUpdating;
        private bool m_autorefresh = true;

        DataView m_view = null;
        BindingSource m_bindingSource = null;


        public RamDataTable(eRamTableType type, String name, ushort start, ushort end, byte displayLenght)
        {
            m_type = type;
            m_start = start;
            m_end = end;
            m_displayLenght = displayLenght;
            m_name = name;
            m_isUpdating = false;

            MappedMemory.RamHasChanged += new OnRamChange(OnRamChanged);

            //create columns
            DataColumn dc0 = new DataColumn("break", System.Type.GetType("System.Boolean"));
            DataColumn dc1 = new DataColumn("zone", System.Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("adress", System.Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("value", System.Type.GetType("System.String"));
            DataColumn dc4 = new DataColumn("instruction", System.Type.GetType("System.String"));
            DataColumn dc5 = new DataColumn("lenht", System.Type.GetType("System.Int32"));
            DataColumn dc6 = new DataColumn("valid", System.Type.GetType("System.Boolean"));
            DataColumn dc7 = new DataColumn("adressINT", System.Type.GetType("System.Int32"));

            this.Columns.Add(dc0);
            this.Columns.Add(dc1);
            this.Columns.Add(dc2);
            this.Columns.Add(dc3);
            this.Columns.Add(dc4);
            this.Columns.Add(dc5);
            this.Columns.Add(dc6);
            this.Columns.Add(dc7);

            DataColumn[] keys = new DataColumn[1];
            keys[0] = dc7;
            this.PrimaryKey = keys;

            UpdateData(false);

            m_view = new DataView(this);
            m_bindingSource = new BindingSource();
            m_bindingSource.DataSource = m_view;
            m_bindingSource.Filter = "valid = 'true'";
            m_bindingSource.RaiseListChangedEvents = true;
            m_bindingSource.ResetBindings(false);

        }

        public DataView GetDataView()
        {
            return m_view;
        }

        public bool AutoRefresh
        {
            get { return m_autorefresh; }
            set { m_autorefresh = value; }
        }

        public static OnTableChange TableHasChanged
        {
            get { return m_tableChanged; }
            set { m_tableChanged = value; }
        }

        public void OnRamChanged(object sender, MappedMemory.RamEventArgs e)
        {
            ushort adr = e.adress;
            if (m_autorefresh && adr >= m_start && adr < m_end)
            {
                UpdateAdress(adr);
            }
        }

        private void UpdateAdress(int adress)
        {
            if( adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_Bank0 && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_BankN       ||
                adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_BankN && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_VideoRam    ||
                adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_CpuRam0 && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_CpuRam1   ||
                adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_CpuRam1 && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_Unused0   ||
                adress >= (int)MappedMemory.eMemoryMap.eMemoryMap_HighRam && adress < (int)MappedMemory.eMemoryMap.eMemoryMap_InterruptRegister
                )
            {
                UpdateAdressCode(adress);
                //UpdateDataCode((int)MappedMemory.eMemoryMap.eMemoryMap_CpuRam0, (int)MappedMemory.eMemoryMap.eMemoryMap_CpuRam1);
                //UpdateDataCode(adress, adress + 100);
            }
            else
            {
                UpdateAdressData(adress);
            }
        }

        private void UpdateAdressData(int adress)
        {

        }

        private void UpdateAdressCode(int adress)
        {
            ushort adr = (ushort)adress;
            byte opcode = GameBoy.Ram.ReadByteAt( adr );
            bool isCB = false;
            if (opcode == 0xCB)
            {
                isCB = true;
                ++adr;
            }
            Z80.Z80Instruction inst = GameBoy.Cpu.decoder.GetInstructionAt(adr, isCB);
            this.BeginLoadData();
            if (inst != null)
            {
                byte length = inst.GetLenght(adr);
                String bytecode = "";
                for (byte j = 0; j < length; j++)
                {
                    byte b = GameBoy.Ram.ReadByteAt((ushort)(adr + j));
                    bytecode += String.Format("{0:x2} ", b);
                }
                object[] row = new object[(int)eRamTableColumn.eColumn_count];
                row[(int)eRamTableColumn.eColumn_breakPoint] = false;
                row[(int)eRamTableColumn.eColumn_zone] = m_name;
                row[(int)eRamTableColumn.eColumn_adress] = String.Format("{0:x4}", adr);
                row[(int)eRamTableColumn.eColumn_value] = bytecode;
                row[(int)eRamTableColumn.eColumn_instruction] = inst.ToString(adr);
                row[(int)eRamTableColumn.eColumn_length] = length;
                row[(int)eRamTableColumn.eColumn_valid] = true;
                row[(int)eRamTableColumn.eColumn_adressINT] = adr;

                this.LoadDataRow(row, true);
                ushort l = (ushort)(adr + length);
                ++adr;
                while (adr < l)
                {
                    object[] rowh = new object[(int)eRamTableColumn.eColumn_count];
                    rowh[(int)eRamTableColumn.eColumn_breakPoint] = false;
                    rowh[(int)eRamTableColumn.eColumn_zone] = m_name;
                    rowh[(int)eRamTableColumn.eColumn_adress] = String.Format("{0:x4}", adr);
                    rowh[(int)eRamTableColumn.eColumn_value] = "**";
                    rowh[(int)eRamTableColumn.eColumn_instruction] = "db";
                    rowh[(int)eRamTableColumn.eColumn_length] = length;
                    rowh[(int)eRamTableColumn.eColumn_valid] = false;
                    rowh[(int)eRamTableColumn.eColumn_adressINT] = adr;
                    this.LoadDataRow(rowh, true);
                    ++adr;
                }
                this.EndLoadData();
            }
        }

        public float UpdatePercentage()
        {
            return ((float)(m_cur - m_start) / (float)(m_end - m_start)) * 100.0f;
        }

        public bool IsUpdating()
        {
            return m_isUpdating;
        }

        public void UpdateData(bool sendEvent)
        {
            m_isUpdating = true;
            switch (m_type)
            {
                case eRamTableType.eRamTableType_code:
                    {
                        UpdateDataCode(m_start, (int)m_end);
                        break;
                    }
                default:
                case eRamTableType.eRamTableType_data:
                    {
                        UpdateDataData( sendEvent );
                        break;
                    }
            }
            m_isUpdating = false;
        }

        private void UpdateDataCode( int start, int end )
        {
            this.BeginLoadData();
            ushort i = (ushort)start;
            while (i < end)
            {
                m_cur = i;
                byte opcode = GameBoy.Ram.ReadByteAt(i);
                bool isCB = false;
                if (opcode == 0xCB)
                {
                    isCB = true;
                    i += 0x01;
                }
                Z80.Z80Instruction inst = GameBoy.Cpu.decoder.GetInstructionAt(i, isCB);
                if (inst != null)
                {
                    byte length = inst.GetLenght(i);
                    String bytecode = "";
                    for (byte j = 0; j < length; j++)
                    {
                        byte b = GameBoy.Ram.ReadByteAt((ushort)(i + j));
                        bytecode += String.Format("{0:x2} ", b);
                    }
                    object[] row = new object[(int)eRamTableColumn.eColumn_count];
                    row[(int)eRamTableColumn.eColumn_breakPoint] = false;
                    row[(int)eRamTableColumn.eColumn_zone] = m_name;
                    row[(int)eRamTableColumn.eColumn_adress] = String.Format("{0:x4}", i);
                    row[(int)eRamTableColumn.eColumn_value] = bytecode;
                    row[(int)eRamTableColumn.eColumn_instruction] = inst.ToString(i);
                    row[(int)eRamTableColumn.eColumn_length] = length;
                    row[(int)eRamTableColumn.eColumn_valid] = true;
                    row[(int)eRamTableColumn.eColumn_adressINT] = i;

                    this.LoadDataRow(row, true);
                    ushort l = (ushort)(i + length);
                    ++i;
                    while (i < l)
                    {
                        object[] rowh = new object[(int)eRamTableColumn.eColumn_count];
                        rowh[(int)eRamTableColumn.eColumn_breakPoint] = false;
                        rowh[(int)eRamTableColumn.eColumn_zone] = m_name;
                        rowh[(int)eRamTableColumn.eColumn_adress] = String.Format("{0:x4}", i);
                        rowh[(int)eRamTableColumn.eColumn_value] = "**";
                        rowh[(int)eRamTableColumn.eColumn_instruction] = "db";
                        rowh[(int)eRamTableColumn.eColumn_length] = length;
                        rowh[(int)eRamTableColumn.eColumn_valid] = false;
                        rowh[(int)eRamTableColumn.eColumn_adressINT] = i;
                        this.LoadDataRow(rowh, true);
                        ++i;
                    }
                }
                else
                {
                    ++i;
                }
            }
            this.EndLoadData();

        }

        private void UpdateDataData(bool sendEvent)
        {
            ushort i = m_start;
            while (i < m_end)
            {
                String data = "";
                for (byte j = 0; j < m_displayLenght; j++)
                {
                    byte b = GameBoy.Ram.ReadByteAt((ushort)(i + j));
                    data += String.Format("{0:x2} ", b);
                }
                object[] row =  {   false, 
                                    m_name,
                                    String.Format("{0:x4}", i),
                                    String.Format("{0:x2}", GameBoy.Ram.ReadByteAt(i) ),
                                    data,
                                    m_displayLenght,
                                    true,
                                    i
                                };
                this.LoadDataRow(row, false);
                i += m_displayLenght;
            }
            this.AcceptChanges();
            if (sendEvent)
            {
                TableEventArgs e = new TableEventArgs();
                if (m_tableChanged != null)
                    m_tableChanged(this, e);
            }
        }

        // FireEventArgs: a custom event inherited from EventArgs.
        public class TableEventArgs : EventArgs
        {
            public TableEventArgs()
            {
            }
            public ushort adress;

        }
    }
}
