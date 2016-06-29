using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameBoyTest.Debug.Visual
{
    public partial class BgTileMapForm : Form
    {
        
        struct sTile
        {
            public ushort startAdr;
        }

        Memory.MappedMemory m_ram;
        sTile[] m_tiles;
        Bitmap m_bitmap;

        private Color c0;
        private Color c1;
        private Color c2;
        private Color c3;

        private ushort m_startAdr = 0x0;
        private ushort m_endAdr = 0x0;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public BgTileMapForm(Memory.MappedMemory ram)
        {
            InitializeComponent();
            m_ram = ram;
            m_bitmap = new Bitmap(256, 256);
            for( int i=0; i<256; i++ )
            {
                for( int j=0; j<256; j++ )
                {
                    m_bitmap.SetPixel(i, j, Color.White);
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Init()
        {
            m_startAdr = 0x8000;
            m_endAdr = 0x8FFF;
            CreateTilesData();
            ColorConverter cv = new ColorConverter();
            c0 = (Color)cv.ConvertFromString("#E0F8D0");
            c1 = (Color)cv.ConvertFromString("#88C070");
            c2 = (Color)cv.ConvertFromString("#346856");
            c3 = (Color)cv.ConvertFromString("#081820");
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void CreateTilesData()
        {
            //create 1st tile data
            m_tiles = new sTile[256];
            for (int i = 0; i < 256; i++) //a tile is 16 bytes
            {
                ushort adr = (ushort)(0x8800 + (i * 0x10));//a tile is 16 bytes
                m_tiles[i].startAdr = adr;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void UpdateForm()
        {
            Graphics g = Graphics.FromImage(m_bitmap);
            g.Clear(Color.White);
            DrawTiles( g );
            DrawGrid( g );
            TileMapPicture2.Image = m_bitmap;
            //LCDC
            byte b= GameBoy.Ram.ReadByteAt( 0xFF40 );
            textLcdC.Text = ToBinaryStr(b);
            textLcdCHex.Text = String.Format("{0:x2}", b);
            //LCDStat
            b = GameBoy.Ram.ReadByteAt(0xFF41);
            textLcdStat.Text = ToBinaryStr(b);
            textLcdStatHex.Text = String.Format("{0:x2}", b);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void DrawGrid( Graphics g )
        {
            Pen pen = new Pen(Color.Gray);
            Point p1 = new Point(0,0);
            Point p2 = new Point(0,0);

            p1.X = 0;
            p2.X = 256;
            for (int i = 0; i < 256; i += 8)
            {
                p1.Y = i;
                p2.Y = i;
                if (i % 64 == 0)
                {
                    pen.Width = 2;
                }
                else
                {
                    pen.Width = 1;
                }
                g.DrawLine(pen, p1, p2);
            }
            p1.Y = 0;
            p2.Y = 256;
            for (int j = 0; j < 256; j += 8)
            {
                if (j % 128 == 0)
                {
                    pen.Width = 2;
                }
                else
                {
                    pen.Width = 1;
                }
                p1.X = j;
                p2.X = j;
                g.DrawLine(pen, p1, p2);
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void DrawTiles( Graphics g )
        {
            if (m_ram == null)
                return;
            int nbTiles = 256;// 32 * 16;// 32 * 32;
            int x;
            int y;
            sTile tile;
            Color[] halfLine = new Color[8];
            ColorConverter cv = new ColorConverter();
            Pen p = new Pen(Color.Black);
            for (int i = 0; i < nbTiles; i++)
            {
                tile = m_tiles[i];
                y = (i / 16 ) * 8;
                x = (i % 16 ) * 8;
                ushort adr1 = 0;
                ushort adr2 = 0;
                for (int j = 0; j < 16; j+=2)
                {
                    adr1 = (ushort)(tile.startAdr + j);
                    adr2 = (ushort)(tile.startAdr + j + 1); 
                    byte b1 = m_ram.ReadByteAt(adr1);   // a line of 8 px is 2 bytes.
                    byte b2 = m_ram.ReadByteAt(adr2);   // a line of 8 px is 2 bytes.
                    Read8PixelsFrom2Byte(b1, b2, ref halfLine);
                    for (int k = 0; k < 8; k++ )
                    {
                        //m_bitmap.SetPixel(x + k, y, halfLine[k  ]);
                        p.Color = halfLine[k];
                        g.DrawRectangle(p, x + k, y, 1, 1);
                    }
                    y++;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void Read8PixelsFrom2Byte( byte b1, byte b2, ref Color[] outColor )
        {
            for(int i=0; i<8; i++)
            {
                byte bb1 = (byte)(b1 & (0x01 << i));
                bb1 = (byte)(bb1 >> i);
                byte bb2 = (byte)(b2 & (0x01 << i));
                bb2 = (byte)(bb2 >> i);
                if( bb1 == 0x0 )
                {
                    if( bb2 == 0x0 )
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

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private String ToBinaryStr(byte b)
        {
            return Convert.ToString(b, 2).PadLeft(8, '0');
        }
    }
}
