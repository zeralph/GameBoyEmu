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
        sTile[] m_tiles_8000_8FFF;
        sTile[] m_tiles_8800_97FF;
        Bitmap m_bitmap;
        Bitmap m_bitmapTileZoom;
        private BitmapData m_bitmapData;
        private BitmapData m_bitmapDataTileZoom;
        private Color[] m_bgPalette;
        private Color[] m_mainPalette;
        private Color c0;
        private Color c1;
        private Color c2;
        private Color c3;

        private int m_currentTileNumber = -1;

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
            m_bitmapTileZoom = new Bitmap(8, 8);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    m_bitmap.SetPixel(i, j, Color.White);
                }
            }
            ColorConverter cv = new ColorConverter();
            c0 = (Color)cv.ConvertFromString("#E0F8D0");    //lightest      -00
            c1 = (Color)cv.ConvertFromString("#88C070");    //light         -01
            c2 = (Color)cv.ConvertFromString("#346856");    //dark          -10
            c3 = (Color)cv.ConvertFromString("#081820");    //darkest       -11
            m_bgPalette = new Color[4];
            m_bgPalette[0] = c0;
            m_bgPalette[1] = c1;
            m_bgPalette[2] = c2;
            m_bgPalette[3] = c3;
            m_mainPalette = new Color[4];
            m_mainPalette[0] = c0;
            m_mainPalette[1] = c1;
            m_mainPalette[2] = c2;
            m_mainPalette[3] = c3;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Init()
        {
            CreateTilesData(ref m_tiles_8800_97FF, 0x8800);
            CreateTilesData(ref m_tiles_8000_8FFF, 0x8000);
            ColorConverter cv = new ColorConverter();
            c0 = (Color)cv.ConvertFromString("#E0F8D0");
            c1 = (Color)cv.ConvertFromString("#88C070");
            c2 = (Color)cv.ConvertFromString("#346856");
            c3 = (Color)cv.ConvertFromString("#081820");
        }


        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void CreateTilesData(ref sTile[] t, ushort startAdr)
        {
            //create 1st tile data
            t = new sTile[256];
            for (int i = 0; i < 256; i++) //a tile is 16 bytes
            {
                ushort adr = (ushort)(startAdr + (i * 0x10));//a tile is 16 bytes
                t[i].startAdr = adr;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void UpdateForm()
        {
            TileMapPicture2.Invalidate();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void TileMapPicture2_Paint(object sender, PaintEventArgs e)
        {
            RefreshPaletteData();
            m_bitmapData = m_bitmap.LockBits(new Rectangle(0, 0, m_bitmap.Width, m_bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            DrawTiles(ref m_bitmapData);
            DrawGrid(ref m_bitmapData);
            m_bitmap.UnlockBits(m_bitmapData);
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.DrawImage(
               m_bitmap,
                new Rectangle(0, 0, TileMapPicture2.Width, TileMapPicture2.Height),
                // destination rectangle 
                0,
                0,           // upper-left corner of source rectangle
                m_bitmap.Width,       // width of source rectangle
                m_bitmap.Height,      // height of source rectangle
                GraphicsUnit.Pixel);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private unsafe void DrawGrid( ref BitmapData b )
        {
            int stride = b.Stride;
            byte* ptr = (byte*)b.Scan0;
            Color c = Color.Gray;
            //horiz
            for (int px = 0; px < 256; px += 8)
            {
                for (int y = 0; y < 256; y++)
                {
                    ptr[(px * 3) + y * stride] = c.B;
                    ptr[(px * 3) + y * stride + 1] = c.G;
                    ptr[(px * 3) + y * stride + 2] = c.R;
                }
            }
            //vert
            for (int y = 0; y < 256; y += 8)
            {
                for (int px = 0; px < 256; px++)
                {
                    ptr[(px * 3) + y * stride] = c.B;
                    ptr[(px * 3) + y * stride + 1] = c.G;
                    ptr[(px * 3) + y * stride + 2] = c.R;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void DrawTiles( ref BitmapData b )
        {
            if (m_ram == null)
                return;

            int nbTiles = 512;// 32 * 16;// 32 * 32;
            for (int i = 0; i < nbTiles; i++)
            {
                int x = (i / 16) * 8;
                int y = (i % 16) * 8;
                DrawATile(i, ref b, x, y);
            }
        }

        private unsafe void DrawATile(int tileNumber, ref BitmapData b, int bitmapXPos, int bitmapYPos)
        {
            int stride = b.Stride;
            byte* ptr = (byte*)b.Scan0;
            int tileAdr = 0x8000 + tileNumber * 0x10;
            int y = bitmapXPos;
            int x = bitmapYPos;
            ushort adr1 = 0;
            ushort adr2 = 0;
            Color[] halfLine = new Color[8];
            for (int j = 0; j < 16; j += 2)
            {
                adr1 = (ushort)(tileAdr + j);
                adr2 = (ushort)(tileAdr + j + 1);
                byte b1 = m_ram.ReadByteAt(adr1);   // a line of 8 px is 2 bytes.
                byte b2 = m_ram.ReadByteAt(adr2);   // a line of 8 px is 2 bytes.
                Read8PixelsFrom2Byte(b1, b2, ref halfLine, ref m_bgPalette);
                for (int m = 0; m < 8; m++)
                {
                    int px = x + m;
                    //if (y >= 0 && y <= h && px <= w && px >= 0)
                    {
                        Color c = halfLine[m];
                        ptr[(px * 3) + y * stride] = c.B;
                        ptr[(px * 3) + y * stride + 1] = c.G;
                        ptr[(px * 3) + y * stride + 2] = c.R;
                    }
                }
                y++;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void Read8PixelsFrom2Byte(byte b1, byte b2, ref Color[] outColor, ref Color[] inColor)
        {
            int k;
            byte bb1, bb2;
            for (int i = 0; i < 8; i++)
            {
                k = 7 - i;
                bb1 = (byte)(b1 & (0x01 << k));
                bb1 = (byte)(bb1 >> k);
                bb2 = (byte)(b2 & (0x01 << k));
                bb2 = (byte)(bb2 >> k);
                bb2 = (byte)(bb2 << 0x01);
                outColor[i] = inColor[bb1 + bb2];
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private String ToBinaryStr(byte b)
        {
            return Convert.ToString(b, 2).PadLeft(8, '0');
        }
        
        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void RefreshPaletteData()
        {
            byte b = 0;
            byte c = 0;
            //bg palette data
            b = GameBoy.Ram.ReadByteAt(0xFF47);
            c = (byte)((b & 0xC0) >> 6);
            m_bgPalette[3] = m_mainPalette[c];
            c = (byte)((b & 0x30) >> 4);
            m_bgPalette[2] = m_mainPalette[c];
            c = (byte)((b & 0x0C) >> 2);
            m_bgPalette[1] = m_mainPalette[c];
            c = (byte)((b & 0x03) >> 0);
            m_bgPalette[0] = m_mainPalette[c];
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void TileMapPicture2_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            int i = x / 8;
            int j = y / 8;
            int number = j * 16 + i;
            int adr = 0x8000 + number * 0x10;
            tileNumber.Text = ""+number;
            tileAdr.Text = String.Format("{0:x2}", adr);
            if (m_currentTileNumber != number)
            {
                m_currentTileNumber = number;
                if (m_currentTileNumber == 0)
                {
                    int h = 0;
                    h++;
                }
                m_bitmapDataTileZoom = m_bitmapTileZoom.LockBits(new Rectangle(0, 0, m_bitmapTileZoom.Width, m_bitmapTileZoom.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                DrawATile(m_currentTileNumber, ref m_bitmapDataTileZoom, 0, 0);
                m_bitmapTileZoom.UnlockBits(m_bitmapDataTileZoom);
                TileZoom.Invalidate();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void TileZoom_Paint(object sender, PaintEventArgs e)
        {
            if (m_currentTileNumber >= 0)
            {
                //RefreshPaletteData();
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                e.Graphics.DrawImage(
                   m_bitmapTileZoom,
                    new Rectangle(0, 0, TileZoom.Width, TileZoom.Height),
                    // destination rectangle 
                    0,
                    0,           // upper-left corner of source rectangle
                    m_bitmapTileZoom.Width,       // width of source rectangle
                    m_bitmapTileZoom.Height,      // height of source rectangle
                    GraphicsUnit.Pixel);
            }
        }
    }
}
