using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using GameBoyTest.Screen;
using GameBoyTest.Z80;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace GameBoyTest.Forms.Screen
{

    class GBVideo:IDisposable
    {
        struct sTile
        {
            public ushort startAdr;
        }

        enum LCDC_mode
        {
            HBLANK = 0,
            VBLANK = 1,
            OAM_USE=2,
            OAM_DISPLAYRAM_USE = 3,
        }

        //public static Object m_lock;

        sTile[] m_tiles_8800_97FF;
        sTile[] m_tiles_8000_8FFF;

        public const int LCD_STAT = 0xFF41;

        public const int _REFRESH_MICROSECOND = 65;  //59.7Hz * 256 lines

        public static float screen_frequency = 59.7f;
        public static int screen_max_line = 153;
        public static int screen_VBLANK_start_line = 145;
        public static double screen_HBLANK_period_µs = 1/9198000 * 1000D * 1000D;

        private bool m_VBlankInterrupted = false;

        private int m_refreshLine = -1;

        private ushort m_LCDC_adr = 0xFF40;
        private long m_clockTick = 0;

        //LCDC stuff
        private int m_LCLD_flag_LcdEnabled = 0;
        private int m_LCLD_flag_TileMapSelect = 0;
        private int m_LCLD_flag_WindowDisplay = 0;
        private int m_LCLD_flag_WindowTileDataSelect = 0;
        private int m_LCLD_flag_TileMapDisplaySelect = 0;
        private int m_LCLD_flag_SpriteSize = 0;
        private int m_LCLD_flag_SpriteDisplay = 0;
        private int m_LCLD_flag_BGWindowDisplay = 0;
        private byte m_LCDC_value = 0;

        private Bitmap m_bgBitmap;
        private BitmapData m_dataBG;
        private bool m_bitmapLocked = false;
        private bool m_isDisposing = false;
        private bool m_isDisposed = false;
        private bool m_scanLIneInterruptHappened = false;

        //color
        private Color c0;
        private Color c1;
        private Color c2;
        private Color c3;
        private Color[] m_mainPalette;
        private Color[] m_halfLine;
        private Color[] m_bgPalette;
        private Color[] m_spPaletteOBJ0;
        private Color[] m_spPaletteOBJ1;
        private Color m_bitmapTransparency;

        private GBScreenForm m_screen;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public GBVideo( GBScreenForm screenForm )
        {
            m_bgBitmap = new Bitmap(160, 144);
            //m_spBitmap = new Bitmap(160, 144);

            ColorConverter cv = new ColorConverter();
            c0 = (Color)cv.ConvertFromString("#E0F8D0");    //lightest      -00
            c1 = (Color)cv.ConvertFromString("#88C070");    //light         -01
            c2 = (Color)cv.ConvertFromString("#346856");    //dark          -10
            c3 = (Color)cv.ConvertFromString("#081820");    //darkest       -11

            m_mainPalette = new Color[4];
            m_mainPalette[0] = c0;
            m_mainPalette[1] = c1;
            m_mainPalette[2] = c2;
            m_mainPalette[3] = c3;

            m_bgPalette = new Color[4];
            m_bgPalette[0] = c0;
            m_bgPalette[1] = c1;
            m_bgPalette[2] = c2;
            m_bgPalette[3] = c3;
            m_spPaletteOBJ0 = new Color[4];
            m_spPaletteOBJ0[0] = c0;
            m_spPaletteOBJ0[1] = c1;
            m_spPaletteOBJ0[2] = c2;
            m_spPaletteOBJ0[3] = c3;
            m_spPaletteOBJ1 = new Color[4];
            m_spPaletteOBJ1[0] = c0;
            m_spPaletteOBJ1[1] = c1;
            m_spPaletteOBJ1[2] = c2;
            m_spPaletteOBJ1[3] = c3;
            m_bitmapTransparency = Color.Red;


            m_screen = screenForm;

            m_clockTick = 0;

            m_halfLine = new Color[8];
            m_VBlankInterrupted = false;
            m_refreshLine = -1;
            m_bitmapLocked = false;
            m_isDisposed = false;
            m_isDisposing = false;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Dispose()
        {
            m_isDisposing = true;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Init()
        {
            m_refreshLine = 0;
            CreateTilesData( ref m_tiles_8800_97FF, 0x8800 );
            CreateTilesData( ref m_tiles_8000_8FFF, 0x8000);
            m_bitmapLocked = false;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Start()
        {
            //m_started = true;

            GameBoy.Ram.WriteByte(m_LCDC_adr, (byte)0x091);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public byte GetLCDCValue()
        {
            return m_LCDC_value;
        }

        public void ResetTick()
        {
            m_clockTick = 0;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Update( )
        {
            UpdateLCDCFlags();
            if (m_LCLD_flag_LcdEnabled != 0 && !m_isDisposed)
            {
                UpdateLCD_STAT(m_clockTick);
                //if (GameBoy.Cpu.running)
                {
                    m_clockTick++;
                    if (m_clockTick > 17555)
                    {
                        m_clockTick = 0;
                    }
                }
            }
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

            //OBJ0 palette data
            b = GameBoy.Ram.ReadByteAt(0xFF48);
            c = (byte)((b & 0xC0) >> 6);
            m_spPaletteOBJ0[3] = m_mainPalette[c];
            c = (byte)((b & 0x30) >> 4);
            m_spPaletteOBJ0[2] = m_mainPalette[c];
            c = (byte)((b & 0x0C) >> 2);
            m_spPaletteOBJ0[1] = m_mainPalette[c];
            m_spPaletteOBJ0[0] = m_bitmapTransparency;

            //OBJ1 palette data
            b = GameBoy.Ram.ReadByteAt(0xFF49);
            c = (byte)((b & 0xC0) >> 6);
            m_spPaletteOBJ1[3] = m_mainPalette[c];
            c = (byte)((b & 0x30) >> 4);
            m_spPaletteOBJ1[2] = m_mainPalette[c];
            c = (byte)((b & 0x0C) >> 2);
            m_spPaletteOBJ1[1] = m_mainPalette[c];
            m_spPaletteOBJ1[0] = m_bitmapTransparency;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void WriteLCDVBlankInterrupt( bool bInterrupt )
        {
            byte b = GameBoy.Ram.ReadByteAt(0xFF0F);
            if (bInterrupt)
                b |= 0x01;
            else
                b &= 0xFE;
            GameBoy.Ram.WriteByte(0xFF0F, b);
        }
        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void WriteLCDControllerInterrupt(bool bInterrupt)
        {
            byte b = GameBoy.Ram.ReadByteAt(0xFF0F);
            if (bInterrupt)
                b |= 0x02;
            else
                b &= 0xFD;
            GameBoy.Ram.WriteByte(0xFF0F, b);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void Draw( int line)
        {
            if( line < 100 )
            {
                //return;
            }
            if (m_LCLD_flag_LcdEnabled != 0)
            {
                if(line == 0)
                {
                    int yyy = 0;
                    yyy++;
                }
                if (line < 144)
                {
                    if (m_LCLD_flag_BGWindowDisplay != 0)
                    {
                        DisplayBG(ref m_dataBG, line);
                    }
                    if (m_LCLD_flag_WindowDisplay != 0 )
                    {
                       DisplayWindow(ref m_dataBG, line);
                    }
                    if (m_LCLD_flag_SpriteDisplay != 0 )
                    {
                        DisplaySprites(ref m_dataBG, line);
                    }
                }
            }
            else
            {
                if (line == 1)
                {
                    ClearBitmap(ref m_dataBG, ref c0);
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void UpdateLCD_STAT( long clockTick)
        {
            if (m_isDisposing)
            {
                m_bgBitmap.Dispose();
                m_isDisposed = true;
                m_isDisposing = false;
                return;
            }
            //complete step is 456clks --> 114 ticks
            int hBlank_time = 51;
            int oam_time = 20;
            byte b;
            LCDC_mode curMode = LCDC_mode.HBLANK;
            if (clockTick == 0)
            {
                m_VBlankInterrupted = false;
                if (!m_bitmapLocked)
                {
                    m_dataBG = m_bgBitmap.LockBits(new Rectangle(0, 0, m_bgBitmap.Width, m_bgBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                    m_bitmapLocked = true;
                }
                m_refreshLine = -1;
            }

            long curLineTick = clockTick % 114;
            long line = clockTick / 114 ;

            if( line != m_refreshLine )
            {
                m_scanLIneInterruptHappened = false;
                m_refreshLine = (int)line;
                GameBoy.Ram.WriteByte(0xFF44, (byte)m_refreshLine);
            }
            if (clockTick >= 16416)  //V-Blank
            {
                if ( !m_VBlankInterrupted )
                {
                    WriteLCDVBlankInterrupt( true );
                    m_VBlankInterrupted = true;
                    if (m_bitmapLocked)
                    {
                        m_bgBitmap.UnlockBits(m_dataBG);
                        m_bitmapLocked = false;
                    }
                    //try
                    //{

                        GameBoy.Screen.Invoke((MethodInvoker)delegate()
                        {
                            GameBoy.Screen.Refresh(m_bgBitmap);
                        });
                        if (GameBoy.Debugger != null)
                        {
                            GameBoy.Debugger.GetBGTileMap().Invoke((MethodInvoker)delegate()
                            {
                                GameBoy.Debugger.GetBGTileMap().UpdateForm();
                            });
                        }
                    //}
                    //catch(Exception e)
                    //{
                        //do nothing
                    //}
                }
                curMode = LCDC_mode.VBLANK;
            }
            else
            {
                WriteLCDVBlankInterrupt(false);
                if (curLineTick < hBlank_time)  //H-Blank
                {
                    curMode = LCDC_mode.HBLANK;
                }
                else if (curLineTick < (hBlank_time + oam_time) ) //OAM in use
                {
                    curMode = LCDC_mode.OAM_USE;
                }
                else                      //OAM and display Ram in use
                {
                    curMode = LCDC_mode.OAM_DISPLAYRAM_USE;
                }
            }

            //clear mode and LYC 
            byte lcdStat = GameBoy.Ram.ReadByteAt(LCD_STAT);
            //reset Mode Flag
            lcdStat &= 0xFC;
            switch( curMode )
            {
                case LCDC_mode.HBLANK:
                    lcdStat |= 0x00;
                    break;
               case LCDC_mode.VBLANK:
                    lcdStat |= 0x01;
                    break;
                case LCDC_mode.OAM_USE:
                    lcdStat |= 0x02;
                    break;
                case LCDC_mode.OAM_DISPLAYRAM_USE:
                    lcdStat |= 0x03;
                    break;

            }
            //Bit2    Scanline coincidence flag
            byte scanLine = (byte)(GameBoy.Ram.ReadByteAt(0xFF45));
            if (curLineTick == 0 && scanLine == m_refreshLine)
            {
                lcdStat |= 0x04;
            }
            else
            {
                lcdStat &= 0xFB;
            }

            //lcdStat |= 0x80; //byte 8 is never used
            GameBoy.Ram.WriteAt(LCD_STAT, lcdStat);

            if (curLineTick == 113)
            {
                RefreshPaletteData();
                Draw(m_refreshLine);
            }


            if (!m_scanLIneInterruptHappened && scanLine == m_refreshLine && ((lcdStat & 0x40) != 0))   //Bit6    Interrupt on scanline coincidence 
            {
                m_scanLIneInterruptHappened = true;
                WriteLCDControllerInterrupt( true );
            }
            if (curMode == LCDC_mode.OAM_USE && ((lcdStat & 0x20) != 0))     //Bit5    Interrupt on controller mode 10 
            {
                WriteLCDControllerInterrupt( true );
            }
            if (curMode == LCDC_mode.VBLANK && ((lcdStat & 0x10) != 0))     //Bit4    Interrupt on controller mode 01
            {
                WriteLCDControllerInterrupt( true );
            }
            if (curMode == LCDC_mode.HBLANK && ((lcdStat & 0x08) != 0))     //Bit3    Interrupt on controller mode 00 
            {
                WriteLCDControllerInterrupt( true );
            }


            
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void DisplayWindow(ref BitmapData b, int line)
        {
            ushort tileDataSelectAdr = 0x9800;
            if (m_LCLD_flag_TileMapSelect != 0)
            {
                tileDataSelectAdr = 0x9C00;
            }
            sTile[] t = m_tiles_8800_97FF;
            bool bIsSByte = true;
            if (m_LCLD_flag_WindowTileDataSelect != 0)  //unsigned numbers
            {

                t = m_tiles_8000_8FFF;
                bIsSByte = false;
            }

            int dy = (int)GameBoy.Ram.ReadByteAt(0xFF4A);
            int dx = (int)GameBoy.Ram.ReadByteAt(0xFF4B);
            dx -= 7;
            DrawWindowTiles(line, dx, dy, 160, 144, ref b, tileDataSelectAdr, ref t, bIsSByte);
        }

        public int getRefreshLine()
        {
            return m_refreshLine;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void DisplayBG(ref BitmapData b, int line )
        {
            ushort tileDataSelectAdr = 0x9800;
            if (m_LCLD_flag_TileMapDisplaySelect != 0)
            {
                tileDataSelectAdr = 0x9C00;
            }
            sTile[] t = m_tiles_8800_97FF;
            bool bIsSByte = true;
            if (m_LCLD_flag_WindowTileDataSelect != 0)  //unsigned numbers
            {

                t = m_tiles_8000_8FFF;
                bIsSByte = false;
            }
            byte sx = GameBoy.Ram.ReadByteAt(0xFF43);
            byte sy = GameBoy.Ram.ReadByteAt(0xFF42);
            int dx = sx;
            int dy = -sy;
            //dx = 0;
            //dy = 0;
            DrawBGTiles(line, dx, -dy, 160, 144, ref b, tileDataSelectAdr, ref t, bIsSByte);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void DisplaySprites(ref BitmapData b, int line)
        {
            byte[] oam = new byte[0xA0];
            GameBoy.Ram.ReadAt(0xFE00, 0xA0, ref oam);
            bool spriteSize8x16 = m_LCLD_flag_SpriteSize != 0;
            for (int i = (0xA0-4); i >= 0 ; i -= 4)
            //for (int i = 0; i < 0xA0-4; i++ )
            {
                int posY = oam[i];
                int posX = oam[i + 1];
                int idx = oam[i + 2];
                int flags = oam[i + 3];
                bool isAboveBg = (flags & 0x80) == 0;
                bool HFlip = (flags & 0x40) != 0;
                bool VFlip = (flags & 0x20) != 0;
                bool useOBJ1 = (flags & 0x10) != 0;
                Color[] p = m_spPaletteOBJ0;
                if (useOBJ1)
                {
                    p = m_spPaletteOBJ1;
                }
                int l = line % 8;
                posX = posX - 8;
                posY = posY - 16 + l;
                int curSpriteLineMin = posY;
                int curSpriteLineMax = posY + 8;
                if (spriteSize8x16 )
                {
                    curSpriteLineMax += 8;
                }
                if (curSpriteLineMax >= line && curSpriteLineMin <= line)
                {
                    if (posX != 0 && posY != 0)
                    {
                        if (!spriteSize8x16)
                        {
                            sTile t = m_tiles_8000_8FFF[idx];
                            DrawTile(t, posX, posY, 160, 144, HFlip, VFlip, ref b, ref p, isAboveBg, l);
                        }
                        else
                        {
                            int k0 = idx & 0xFE;
                            int k1 = idx | 0x01;
                            if (!HFlip)
                            {
                                sTile t = m_tiles_8000_8FFF[k0];
                                DrawTile(t, posX, posY, 160, 144, HFlip, VFlip, ref b, ref p, isAboveBg, l);
                                t = m_tiles_8000_8FFF[k1];
                                DrawTile(t, posX, posY + 8, 160, 144, HFlip, VFlip, ref b, ref p, isAboveBg, l);
                            }
                            else
                            {
                                sTile t = m_tiles_8000_8FFF[k0];
                                DrawTile(t, posX, posY + 8, 160, 144, HFlip, VFlip, ref b, ref p, isAboveBg, l);
                                t = m_tiles_8000_8FFF[k1];
                                DrawTile(t, posX, posY, 160, 144, HFlip, VFlip, ref b, ref p, isAboveBg, l);
                            }
                        }
                    }
                }
            }
        }

        
        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private unsafe void ClearBitmap( ref BitmapData b, ref Color c)
        {
            int stride = b.Stride;
            //Color c = m_bitmapTransparency;
            byte* ptr = (byte*)b.Scan0;
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    ptr[(i * 3) + j * stride] = c.B;
                    ptr[(i * 3) + j * stride + 1] = c.G;
                    ptr[(i * 3) + j * stride + 2] = c.R;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private unsafe void DrawTile(sTile t, int posX, int posY, int w, int h, bool hFlip, bool vFlip, ref BitmapData b, ref Color[] palette, bool isAboveBg, int tileLine)
        {
            int y = posY;
            int x = posX;
            int stride = b.Stride;
            byte* ptr = (byte*)b.Scan0;
            int l = tileLine;// % 8;
            int maxBitmapIndex = 160 * 3 + 144 * stride;
            ushort adr1 = (ushort)(t.startAdr + l*2);
            ushort adr2 = (ushort)(t.startAdr + 1 + l*2);
            if (hFlip)
            {
                adr2 = (ushort)(t.startAdr + 15 -l*2);
                adr1 = (ushort)(t.startAdr + 15 -1 -l*2);
            }
            byte b1 = GameBoy.Ram.ReadByteAt(adr1);   // a line of 8 px is 2 bytes.
            byte b2 = GameBoy.Ram.ReadByteAt(adr2);   // a line of 8 px is 2 bytes.
            Read8PixelsFrom2Byte(b1, b2, ref m_halfLine, ref palette);
            for (int m = 0; m < 8; m++)
            {
                int px = x + m;
                if (y>=0 && y <= h && px <= w && px>=0)
                {
                    Color c = m_halfLine[m];
                    if (vFlip)
                    {
                        c = m_halfLine[7 - m];
                    }
                    if( c != m_bitmapTransparency )
                    {
                        if ( !isAboveBg )
                        {
                            Color bc = Color.FromArgb(255, ptr[(px * 3) + y * stride + 2], ptr[(px * 3) + y * stride + 1], ptr[(px * 3) + y * stride]);
                            if (bc == m_bgPalette[0])
                            {
                                ptr[(px * 3) + y * stride] = c.B;
                                ptr[(px * 3) + y * stride + 1] = c.G;
                                ptr[(px * 3) + y * stride + 2] = c.R;
                            }
                        }
                        else
                        {
                            ptr[(px * 3) + y * stride] = c.B;
                            ptr[(px * 3) + y * stride + 1] = c.G;
                            ptr[(px * 3) + y * stride + 2] = c.R;
                        }
                    }
                }
            }
        }


        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void DrawBGTiles(int line, int dx, int dy, int w, int h, ref BitmapData b, ushort tileDataSelectAdr, ref sTile[] tilesArray, bool bIsSByte)
        {
            int x = -dx %8;
            int y = 0;
            ushort tileAdr;
            sTile t;
            byte tileIdx = 0;
            y = line;
            line += dy;
            
            if( line < 0)
            { 
                line += 256;
            }
            if( line > 255 )
            { 
                line -= 256;
            }
            
            int s = line / 8;
            int tileLine = line % 8;
            int u = dx / 8;
            for (int k = 0; k < 32; k++)
            {
                u = u % 32;
                tileAdr = (ushort)(tileDataSelectAdr + u + (s * 32));
                if (bIsSByte)
                {
                    sbyte sb = GameBoy.Ram.ReadSignedByteAt(tileAdr);
                    tileAdr = (ushort)(sb + 0x80);
                    t = tilesArray[tileAdr];
                }
                else
                {
                    tileIdx = GameBoy.Ram.ReadByteAt(tileAdr);
                    t = tilesArray[tileIdx];
                }
                DrawTile(t, x, y, w, h, false, false, ref b, ref m_bgPalette, true, tileLine);
                x += 8;
                x = x % 256;
                u++;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void DrawWindowTiles( int line, int dx, int dy, int w, int h, ref BitmapData b, ushort tileDataSelectAdr, ref sTile[] tilesArray, bool bIsSByte)
        {
            if( line < dy )
            {
                return;
            }
            int x=dx;
            int y=0;
            ushort tileAdr;
            sTile t;
            byte tileIdx = 0;
            y = line;
            line -= dy;
            
            int s = line / 8;
            int tileLine = line % 8;
            //s = 0;
            for (int k=0; k < 32; k++)
            {
                tileAdr = (ushort)(tileDataSelectAdr + k + (s*32) );
                if( bIsSByte )
                {
                    sbyte sb = GameBoy.Ram.ReadSignedByteAt(tileAdr);
                    tileAdr = (ushort)(sb + 0x80);
                    t = tilesArray[tileAdr];
                }
                else 
                {
                    tileIdx = GameBoy.Ram.ReadByteAt(tileAdr);
                    t = tilesArray[tileIdx];
                }
                DrawTile(t, x, y, w, h, false, false, ref b, ref m_bgPalette, true, tileLine);
                x += 8;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void CreateTilesData( ref sTile[] t, ushort startAdr )
        {
            //create 1st tile data
            t = new sTile[256];
            for (int i = 0; i < 256; i ++) //a tile is 16 bytes
            {
                ushort adr = (ushort)(startAdr + (i * 0x10 ) );//a tile is 16 bytes
                t[i].startAdr = adr;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void UpdateLCDCFlags()
        {

            byte b = GameBoy.Ram.ReadByteAt(m_LCDC_adr);
            m_LCLD_flag_LcdEnabled = ((b & (0x01 << 7)) != 0) ? 1 : 0;
            m_LCLD_flag_TileMapSelect = ((b & (0x01 << 6)) != 0) ? 1 : 0;
            m_LCLD_flag_WindowDisplay = ((b & (0x01 << 5)) != 0) ? 1 : 0;
            m_LCLD_flag_WindowTileDataSelect = ((b & (0x01 << 4)) != 0) ? 1 : 0;
            m_LCLD_flag_TileMapDisplaySelect = ((b & (0x08)) != 0) ? 1 : 0;
            m_LCLD_flag_SpriteSize = ((b & (0x01 << 2)) != 0) ? 1 : 0;
            m_LCLD_flag_SpriteDisplay = ((b & (0x01 << 1)) != 0) ? 1 : 0;
            m_LCLD_flag_BGWindowDisplay = ((b & (0x01 << 0)) != 0) ? 1 : 0;

            m_LCDC_value = (byte)( m_LCLD_flag_LcdEnabled >> 7 &
                            m_LCLD_flag_TileMapSelect >> 6 &
                            m_LCLD_flag_WindowDisplay >> 5 &
                            m_LCLD_flag_WindowTileDataSelect >> 4 &
                            m_LCLD_flag_TileMapDisplaySelect >> 3 &
                            m_LCLD_flag_SpriteSize >> 2 &
                            m_LCLD_flag_SpriteDisplay >> 1 &
                            m_LCLD_flag_BGWindowDisplay);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void Read8PixelsFrom2Byte(byte b1, byte b2, ref Color[] outColor, ref Color[] inColor )
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
                outColor[i] = inColor[bb1+bb2];
            }
        }
    }
}