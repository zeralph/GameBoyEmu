using GameBoyTest.Inputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing.Drawing2D;
using Microsoft.Win32;
using GameBoyTest.Forms;
using System.IO;

namespace GameBoyTest.Screen
{
    public partial class GBScreenForm : Form, IDisposable
    {
        public class myToolStripMenuItem : ToolStripMenuItem
        {
            private string m_path = "";
            private string m_name ="";

            public myToolStripMenuItem() : base()
            { 
            }

            public string romName
            {
                get{ return m_name;}
                set { m_name = value; }
            }

            public string romPath
            {
                get { return m_path; }
                set { m_path = value; }
            }
        }

        int[] m_cpuUsage;
        int m_cpuUsageIndex = 0;
        Rectangle m_wnRect;
        Rectangle m_srcRect;
        Rectangle m_spRect;

        GraphicsUnit m_units;

        Bitmap m_bgBitmap;

        private String m_romName = "";
        private int m_actualSpeedPercentage = 0;
        private String m_status= "";
        private Rectangle m_rect;

        private bool m_stretch = true;
        private int m_speed = 100;
        private InterpolationMode m_interpolation = InterpolationMode.NearestNeighbor;
        private myToolStripMenuItem[] m_toolStripRecent;

        private bool m_isClosing = false;

        public GBScreenForm(Memory.MappedMemory ram)
        {
            InitializeComponent();
            m_cpuUsage = new int[toolStripStatusLabelSpeed.Width];
            for (int i = 0; i < m_cpuUsage.Length; i++ )
            {
                m_cpuUsage[i] = 0;
            }
            this.IsMdiContainer = true;
            this.Visible = true;
            m_srcRect = new Rectangle(0, 0, 256, 256);
            m_wnRect = new Rectangle(0, 0, 160, 144);
            m_spRect = new Rectangle(0, 0, 160, 144);
            m_units = GraphicsUnit.Pixel;
            m_rect = new Rectangle();
            m_bgBitmap = new Bitmap(160, 144, PixelFormat.Format32bppArgb);

            m_toolStripRecent = new myToolStripMenuItem[5];
            for (int i = 0; i < 5; i++)
            {
                m_toolStripRecent[i] = new myToolStripMenuItem();
                m_toolStripRecent[i].Text = "";
                m_toolStripRecent[i].Click += new System.EventHandler(OnToolStripRecentClick);
                fichierToolStripMenuItem.DropDownItems.Add(m_toolStripRecent[i]);
                m_toolStripRecent[i].Visible = false;
            }

            ReadIniFile();
            RefreshUI();
            this.Show();
        }

        public void Init()
        {
            RefreshUI();
            this.Visible = true;
        }

        private void OnToolStripRecentClick(object sender, EventArgs e)
        {
            myToolStripMenuItem t = (myToolStripMenuItem)sender;
            String name = t.romName;
            string path = t.romPath;
            GameBoy.LoadCartridge(path+"\\"+name, name, false);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (!m_isClosing)
            {
                e.Graphics.InterpolationMode = m_interpolation;
                Draw(e.Graphics);
            }
            else
            {
                //base.OnFormClosing();
                GameBoy.OnClose();
            }
            toolStripStatusLabelSpeed.Invalidate();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public Bitmap GetBitmapCopy()
        {
            Bitmap b = (Bitmap)m_bgBitmap.Clone();
            return b;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public void Refresh(Bitmap bg)
        {
            m_bgBitmap = Clone32BPPBitmap(ref bg);
            //this.toolStripStatusLabelSpeed.Text = m_actualSpeedPercentage.ToString();
            this.toolStripStatusLabelRomName.Text = m_romName;
            this.toolStripStatusLabelEmuStatus.Text = m_status;
            this.pictureBox1.Refresh();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void Draw(Graphics g)
        {
            m_rect.X = 0;
            m_rect.Y = 0;
            m_rect.Width = pictureBox1.Width;
            m_rect.Height = pictureBox1.Height;
            if (!m_stretch)
            {
                int x = (pictureBox1.Width - 160) / 2;
                int y = (pictureBox1.Height - 144) / 2;
                x = Math.Max(0, x);
                y = Math.Max(0, y);
                m_rect.X = x;
                m_rect.Y = y;
                m_rect.Width = 160;
                m_rect.Height = 144;
            }
            //lock (m_bgBitmap)
            {
                g.DrawImage(m_bgBitmap, m_rect, m_spRect, m_units);
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Console.WriteLine("[LOG] ");
            //GameBoy.InputsMgr.RecordInput(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private unsafe Bitmap Clone32BPPBitmap(ref Bitmap srcBitmap)
        {
            Bitmap result = new Bitmap(srcBitmap.Width, srcBitmap.Height, PixelFormat.Format32bppArgb);

            Rectangle bmpBounds = new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height);
            BitmapData srcData = srcBitmap.LockBits(bmpBounds, ImageLockMode.ReadOnly, srcBitmap.PixelFormat);
            BitmapData resData = result.LockBits(bmpBounds, ImageLockMode.WriteOnly, result.PixelFormat);

            int* srcScan0 = (int*)srcData.Scan0;
            int* resScan0 = (int*)resData.Scan0;
            int numPixels = srcData.Stride / 4 * srcData.Height;
            try
            {
                for (int p = 0; p < numPixels; p++)
                {
                    resScan0[p] = srcScan0[p];
                }
            }
            finally
            {
                srcBitmap.UnlockBits(srcData);
                result.UnlockBits(resData);
            }

            return result;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public bool IsClosing()
        {
            return m_isClosing;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameBoy.Cpu.Stop();
            GameBoy.Cpu.Init();
            GameBoy.Screen.Init();
            GameBoy.Cpu.Start();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem it = (ToolStripMenuItem)sender;
            GameBoy.EnableDebugger();
        }

        private void GBScreenForm_Load(object sender, EventArgs e)
        {
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //e.Cancel = true;
            m_isClosing = true;
            GameBoy.OnClose();
        } 

        private void GBScreenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameBoy.OnClose();
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            //Graphics g = e.Graphics;
            //System.Diagnostics.Debug.WriteLine( "toto"+ pictureBox1.Width);
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //pictureBox1.Width = 
        }

        #region Window stuff

        public void SetRomTitle( String t)
        {
            m_romName = t;
        }
        
        public void SetEmuStatus( String t )
        {
            m_status = t;
        }
        public void SetSpeedValuePercentage(int t )
        {
            m_cpuUsageIndex++;
            m_cpuUsageIndex = m_cpuUsageIndex % m_cpuUsage.Length;
            m_cpuUsage[m_cpuUsageIndex] = m_actualSpeedPercentage;
            m_actualSpeedPercentage = t;
        }

        private void loadRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread_bug = new Thread(new ThreadStart(
            delegate
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                //m_cartridge.Init("..//rom//ld.gb");
                System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                openFileDialog1.InitialDirectory = ".";
                openFileDialog1.Filter = "GB files(*.gb,*.gbc)|*.gb;*.gbc|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 0;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    GameBoy.LoadCartridge(openFileDialog1.FileName, openFileDialog1.SafeFileName, true);
                }
            }));
            thread_bug.SetApartmentState(ApartmentState.STA);  /*<=*/
            thread_bug.Start();
        }

        private void runToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //if (!GameBoy.Cpu.running)
            {
                GameBoy.Cpu.Start();
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameBoy.Cpu.Stop();
        }

        private void saveStateF2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            State.StateSystem.saveState();
        }

        private void loadStateF3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(GameBoy.Cartridge.HasGame() )
            {
                LoadStatesForm f = new LoadStatesForm();
                f.Show();
            }
            else
            {
                string message = "Load a rom before loading states";
                string caption = "No rom loaded";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
            }
        }

        private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_stretch = true;
            RefreshUI();
        }

        private void originalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_stretch = false;
            RefreshUI();
        }

        private void SetSpeed(int speed)
        {
            if (GameBoy.Cpu != null)
            {
                m_speed = speed;
                GameBoy.Cpu.SetSpeed(m_speed);
                RefreshUI();
            }
        }

        private void SetInterpolation(InterpolationMode i)
        {
            m_interpolation = i;
            RefreshUI();
        }

        private void ReadIniFile()
        {
            //stretch
            m_stretch = GameBoy.Parameters.Strech;
            //speed
            m_speed = GameBoy.Parameters.Speed;
            SetSpeed(m_speed);
            //interpolation
            m_interpolation = (InterpolationMode)GameBoy.Parameters.InterpolationMode;
            if(m_interpolation == InterpolationMode.Default)
            {
                m_interpolation = InterpolationMode.NearestNeighbor;
            }
            //recent
            for (int i = 0; i < GameBoy.Parameters.NbRecentFile; i++)
            {
                m_toolStripRecent[i].Visible = true;
                m_toolStripRecent[i].Text = GameBoy.Parameters.Recent[i].path+"\\"+GameBoy.Parameters.Recent[i].name;
                m_toolStripRecent[i].romName = GameBoy.Parameters.Recent[i].name;
                m_toolStripRecent[i].romPath = GameBoy.Parameters.Recent[i].path;
                m_toolStripRecent[i].Invalidate();
            }
        }

        private void RefreshUI()
        {
            //stretch
            GameBoy.Parameters.Strech = m_stretch;
            stretchToolStripMenuItem.Checked = m_stretch;
            originalToolStripMenuItem.Checked = !m_stretch;
            //speed
            toolStripMenuItemSpeed50.Checked = (m_speed == 50);
            toolStripMenuItemSpeed100.Checked = (m_speed == 100);
            toolStripMenuItemSpeed150.Checked = (m_speed == 150);
            toolStripMenuItemSpeed200.Checked = (m_speed == 200);
            toolStripMenuItemSpeed500.Checked = (m_speed == 500);
            GameBoy.Parameters.Speed = m_speed;
            //recent
            for (int i = 0; i < GameBoy.Parameters.NbRecentFile; i++)
            {
                m_toolStripRecent[i].Visible = true;
                m_toolStripRecent[i].Text = GameBoy.Parameters.Recent[i].path + "\\" + GameBoy.Parameters.Recent[i].name;
                m_toolStripRecent[i].Invalidate();
            }
            //interpolation
            GameBoy.Parameters.InterpolationMode = (int)m_interpolation;
            nearestToolStripMenuItem.Checked = m_interpolation == InterpolationMode.NearestNeighbor;
            bicubicToolStripMenuItem.Checked = m_interpolation == InterpolationMode.HighQualityBicubic;
            bilinearToolStripMenuItem.Checked = m_interpolation == InterpolationMode.HighQualityBilinear;
        }

        private void toolStripMenuItemSpeed50_Click(object sender, EventArgs e)
        {
            SetSpeed(50);
        }

        private void toolStripMenuItemSpeed100_Click(object sender, EventArgs e)
        {
            SetSpeed(100);
        }

        private void toolStripMenuItemSpeed150_Click(object sender, EventArgs e)
        {
            SetSpeed(150);
        }

        private void toolStripMenuItemSpeed200_Click(object sender, EventArgs e)
        {
            SetSpeed(200);
        }

        private void toolStripMenuItemSpeed500_Click(object sender, EventArgs e)
        {
            SetSpeed(500);
        }

        private void nearestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetInterpolation(InterpolationMode.NearestNeighbor);
        }

        private void bicubicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetInterpolation(InterpolationMode.HighQualityBicubic);
        }

        private void bilinearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetInterpolation(InterpolationMode.HighQualityBilinear);
        }

        private void sound01ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameBoy.Sound.EnablSwitchChannelEnable(0);
        }

        private void sound02ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameBoy.Sound.EnablSwitchChannelEnable(1);
        }

        private void sound03ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameBoy.Sound.EnablSwitchChannelEnable(2);
        }

        private void sound04ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameBoy.Sound.EnablSwitchChannelEnable(3);
        }

        private void toolStripStatusLabelSpeed_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabelSpeed_Paint(object sender, PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Green, 1.0f);
            Point p1 = new Point(0,0);
            Point p2 = new Point(toolStripStatusLabelSpeed.Width, toolStripStatusLabelSpeed.Height);
            int k = 0;
            int usage = 0;
            SolidBrush brush = new SolidBrush(Color.Black);
            for (int i = 0; i < m_cpuUsage.Length; i++)
            {
                usage = (int)((m_cpuUsage[k] / 100.0f) * toolStripStatusLabelSpeed.Height);
                p.Color = (m_cpuUsage[k] < 50) ? Color.Green : (m_cpuUsage[k] < 80 ? Color.Orange : Color.Red);
                k = ( m_cpuUsageIndex + i )% m_cpuUsage.Length;
                p1.X = i;
                p1.Y = toolStripStatusLabelSpeed.Height;
                p2.X = i;
                p2.Y = toolStripStatusLabelSpeed.Height - usage;
                g.DrawLine(p, p1, p2);
            }
            g.DrawString(m_actualSpeedPercentage.ToString()+"%", toolStripStatusLabelSpeed.Font, brush, toolStripStatusLabelSpeed.Width / 2, 0);
            //toolStripStatusLabelSpeed.Text = m_actualSpeedPercentage.ToString();
        }

        private void stepOneFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameBoy.Cpu.StopAndRunOneFrame();
        }

        private void inputsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputManagerForm f = new InputManagerForm();
            f.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameBoy.OnClose();
        }
    }

#endregion

}
