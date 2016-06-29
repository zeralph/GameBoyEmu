using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameBoyTest.Screen
{
    public partial class GBScreenForm : Form
    {
        Memory.MappedMemory m_ram;
        ushort m_OAMStartAdr = 0xFE00;
        ushort m_OAMEndAdr = 0xFE9F;
        ushort m_BGTilesStartAdr = 0x9800;
        Bitmap m_bitmap;
        private Color c0;
        private Color c1;
        private Color c2;
        private Color c3;


        public GBScreenForm(Memory.MappedMemory ram)
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.Visible = true;
            m_ram = ram;
            m_bitmap = new Bitmap(166, 144);
            ColorConverter cv = new ColorConverter();
            c0 = (Color)cv.ConvertFromString("#000000");
            c1 = (Color)cv.ConvertFromString("#550000");
            c2 = (Color)cv.ConvertFromString("#AAAAAA");
            c3 = (Color)cv.ConvertFromString("#FFFFFF");
        }

        public void Init()
        {
            this.Visible = true;
        }

        public void UpdateForm()
        {
            for( ushort i= m_OAMStartAdr; i< m_OAMEndAdr; i+=4 )
            {
                UpdateSprite(i);
            }
            // update bg tiles 
            for (int i = 0; i < 32; i++ )
            {
                for (int j = 0; j < 32; j++)
                {
                    ushort adr = (ushort)(m_BGTilesStartAdr + i + j * 32);
                    int tileNumber = m_ram.ReadByteAt( adr );
                    // todo : tile start adr switch
                    ushort tileAdr = (ushort)(0x8000 + tileNumber * 16);
                    UpdateBackgroundTile(tileAdr, i, j);
                }
            }
        }

        private void UpdateBackgroundTile( ushort tileAdr, int i, int j )
        {
            Color[] halfLine = new Color[8];
            ushort adr1 = 0;
            ushort adr2 = 0;
            int x = i * 8;
            int y = j * 8;
            for (int k = 0; k < 16; k += 2)
            {
                adr1 = (ushort)(tileAdr + k);
                adr2 = (ushort)(tileAdr + k + 1);
                byte bt1 = m_ram.ReadByteAt(adr1);   // a line of 8 px is 2 bytes.
                byte bt2 = m_ram.ReadByteAt(adr2);   // a line of 8 px is 2 bytes.
                Read8PixelsFrom2Byte(bt1, bt2, ref halfLine);
                for (int l = 0; l < 8; l++)
                {
                    if (x >= 0 && y >= 0 && y<144 && x<160)
                    {
                        m_bitmap.SetPixel(x + l, y, halfLine[l]);
                    }
                }
                y++;
            }
        }

        private void UpdateSprite( ushort OAMAdr )
        {
            byte b0 = m_ram.ReadByteAt( (ushort)(OAMAdr));          //X
            byte b1 = m_ram.ReadByteAt( (ushort)(OAMAdr + 1));      //Y
            byte b2 = m_ram.ReadByteAt( (ushort)(OAMAdr + 2));      // tile #
            byte b3 = m_ram.ReadByteAt( (ushort)(OAMAdr + 3));      // attributes

            Color[] halfLine = new Color[8];
            ushort tileAdr = (ushort)(0x8000 + 16 * b2); //tile starts at 0x8000, tile is 16 bytes length
            ushort adr1 = 0;
            ushort adr2 = 0;
            int x = b0 - 8;
            int y = b1 - 16;
            for (int j = 0; j < 16; j += 2)
            {
                adr1 = (ushort)(tileAdr + j);
                adr2 = (ushort)(tileAdr + j + 1);
                byte bt1 = m_ram.ReadByteAt(adr1);   // a line of 8 px is 2 bytes.
                byte bt2 = m_ram.ReadByteAt(adr2);   // a line of 8 px is 2 bytes.
                Read8PixelsFrom2Byte(bt1, bt2, ref halfLine);
                for (int k = 0; k < 8; k++)
                {
                    if( x>=0 && y>=0 )
                    {
                        m_bitmap.SetPixel(x + k, y, halfLine[k]);
                    }  
                }
                y++;
            }
        }

        private void Read8PixelsFrom2Byte(byte b1, byte b2, ref Color[] outColor)
        {
            for (int i = 0; i < 8; i++)
            {
                byte bb1 = (byte)(b1 & (0x01 << i));
                bb1 = (byte)(bb1 >> i);
                byte bb2 = (byte)(b2 & (0x01 << i));
                bb2 = (byte)(bb2 >> i);
                if (bb1 == 0x0)
                {
                    if (bb2 == 0x0)
                    {
                        outColor[7 - i] = c0;
                    }
                    else
                    {
                        outColor[7 - i] = c1;
                    }
                }
                else
                {
                    if (bb2 == 0x0)
                    {
                        outColor[7 - i] = c2;
                    }
                    else
                    {
                        outColor[7 - i] = c3;
                    }
                }
            }
        }
    }
}
