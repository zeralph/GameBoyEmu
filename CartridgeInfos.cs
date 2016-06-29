using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBoyTest
{
    public class CartridgeInfos
    {
        //---------------------------------------------------------------------------------------------//
        readonly static string[] e_cartridge_type_infos = new string[]
        {
            "00",   //0
            "01",   //1
            "02",   //2
            "03",   //3
            "",     //4
            "05",   //5
            "06",   //6
            "",     //7
            "08",   //8
            "09",   //9
            "",     //A
            "",     //B
            "12",   //C
            "",     //D
            "",   //E
            "",   //F
            "",   //10
            "",   //11
            "",   //12
            "19",   //13
            "0B",   //14
            "0C",   //15
            "0D",   //16
            "0F",   //17
            "10",   //18
            "11",   //19
            "12",   //1A
            "13",   //1B
            "15",   //1C
            "16",   //1D
            "17",   //1E
            "19",   //1F
        };

        readonly static string[] e_cartridge_type_infos_name =
        {
            "ROM ONLY",                 
            "MBC1",                 
            "MBC1+RAM",               
            "MBC1+RAM+BATTERY",      
            "MBC2",    
            "MBC2+BATTERY",  
            "ROM+RAM",
            "ROM+RAM+BATTERY",
            "MMM01",
            "MMM01+RAM",
            "MMM01+RAM+BATTERY",
            "MBC3+TIMER+BATTERY",
            "MBC3+TIMER+RAM+BATTERY",
            "MBC3",
            "MBC3+RAM",
            "MBC3+RAM+BATTERY",
            "MBC4",
            "MBC4+RAM",
            "MBC4+RAM+BATTERY",
            "MBC5",
            "MBC5+RAM",
            "MBC5+RAM+BATTERY",
            "MBC5+RUMBLE",
            "MBC5+RUMBLE+RAM",
            "MBC5+RUMBLE+RAM+BATTERY",
            "POCKET CAMERA",
            "BANDAI TAMA5",
            "HuC3",
            "HuC1+RAM+BATTERY",
        };
/*
        public static string GetCartridgeInfos(byte b)
        {
            return e_cartridge_type_infos_name[b];
        }

  */
        public static string GetCartridgeInfos(String hexValue)
        {
            int i = 0;
            while( i < e_cartridge_type_infos.Length && hexValue != e_cartridge_type_infos[i] )
            {
                i++;
            }
            if (i < e_cartridge_type_infos.Length)
            {
                return e_cartridge_type_infos_name[i];
            }
            return "UNDEF";
        }


        public static string GetCartridgeInfos(byte b)
        {
            switch (b)
            {
                case 0x0: { return "ROM ONLY           "; }
                case 0x1: { return "ROM+MBC1           "; }
                case 0x2: { return "ROM+MBC1+RAM       "; }
                case 0x3: { return "ROM+MBC1+RAM+BATT  "; }
                case 0x5: { return "ROM+MBC2           "; }
                case 0x6: { return "ROM+MBC2+BATTERY   "; }
                case 0x8: { return "ROM+RAM            "; }
                case 0x9: { return "ROM+RAM+BATTERY    "; }
                case 0xB: { return "ROM+MMM01          "; }
                case 0xC: { return "ROM+MMM01+SRAM     "; }
                case 0xD: { return "ROM+MMM01+SRAM+BATT"; }
                case 0x12: { return "ROM+MBC3+RAM"; }
                case 0x13: { return "ROM+MBC3+RAM+BATT"; }
                case 0x19: { return "ROM+MBC5"; }
                case 0x1A: { return "ROM+MBC5+RAM"; }
                case 0x1B: { return "ROM+MBC5+RAM+BATT"; }
                case 0x1C: { return "ROM+MBC5+RUMBLE"; }
                case 0x1D: { return "ROM+MBC5+RUMBLE+SRAM"; }
                case 0x1E: { return "ROM+MBC5+RUMBLE+SRAM+BATT"; }
                case 0x1F: { return "Pocket Camer a"; }
                case 0xFD: { return "Bandai TAMA5"; }
                case 0xFE: { return "Hudson HuC-3"; }
                default: { return "UKN"; }
            }
        }
    }
}
