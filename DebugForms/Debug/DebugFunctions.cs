using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameBoyTest.Debug.Visual;
using GameBoyTest.Forms.Debug.Visual;
using GameBoyTest.Debug;
using System.Drawing;

namespace GameBoyTest.Debug
{
    public class DebugFunctions :IDisposable
    {

        private static Z80ViewForm m_Z80ViewForm;
        private static DebuggerForm m_mainForm;
        private static RamViewForm m_ramForm;
        //private static CodeViewForm m_codeForm;
        private static CodeView3Form m_codeForm;
        private static InstructionForm m_instForm;
        private static SpeedForm m_speedForm;
        private static BgTileMapForm m_bgTileMapForm;
        private static SerialForm m_serialForm;
        private static CallstackForm m_callstackForm;
        private static InterruptsForm m_interruptsForm;
        private static bool m_bDoRefresh;
        private static bool m_IsInit = false;

        public static bool IsReady()
        {
            return m_IsInit;
        }

        public BgTileMapForm GetBGTileMap()
        {
            return m_bgTileMapForm;
        }

        public static void ResetDebug()
        {
            if (m_callstackForm != null)
                m_callstackForm.Reset();
            if (m_serialForm != null)
                m_serialForm.Reset();
            if (m_interruptsForm != null)
                m_interruptsForm.Reset();
        }   

        public DebugFunctions( DebuggerForm main)
        {
            m_mainForm = main;
            
            m_Z80ViewForm = new Z80ViewForm( GameBoy.Cpu );
//			m_Z80ViewForm.Parent = m_mainForm;

			m_Z80ViewForm.MdiParent = m_mainForm;

            m_ramForm = new RamViewForm(GameBoy.Ram);
            m_ramForm.MdiParent = m_mainForm;

            m_instForm = new InstructionForm(GameBoy.Cpu, GameBoy.Ram );
            m_instForm.MdiParent = m_mainForm;

            m_codeForm = new CodeView3Form();
            m_codeForm.MdiParent = m_mainForm;

            m_speedForm = new SpeedForm();
            m_speedForm.MdiParent = m_mainForm;

            m_bgTileMapForm = new BgTileMapForm(GameBoy.Ram);
            m_bgTileMapForm.MdiParent = m_mainForm;

            m_serialForm = new SerialForm();
            m_serialForm.MdiParent = m_mainForm;

            m_callstackForm = new CallstackForm();
            m_callstackForm.MdiParent = m_mainForm;

            m_interruptsForm = new InterruptsForm();
            m_interruptsForm.MdiParent = m_mainForm;
			Init();
			m_bDoRefresh = true;
            m_IsInit = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            m_Z80ViewForm.Dispose();
            m_mainForm.Dispose();
            m_ramForm.Dispose();
            m_codeForm.Dispose();
            m_codeForm.Dispose();
            m_instForm.Dispose();
            m_speedForm.Dispose();
            m_bgTileMapForm.Dispose();
            m_serialForm.Dispose();
            m_callstackForm.Dispose();
            m_interruptsForm.Dispose();
        }

        public void debugFormThread()
        {
        }

        public void Init()
        {
            m_instForm.Init();
            m_Z80ViewForm.Init();
            m_ramForm.Init();
            if( m_codeForm != null )
                m_codeForm.Init();
            m_speedForm.Init();
            m_bgTileMapForm.Init();
            m_serialForm.Init();
            m_callstackForm.Init();
            m_interruptsForm.Init();

            m_mainForm.Invoke((MethodInvoker)delegate {
                 m_mainForm.WindowState = FormWindowState.Maximized;
             });

            m_Z80ViewForm.Invoke((MethodInvoker)delegate {
                 m_Z80ViewForm.WindowState = FormWindowState.Normal;
                 m_Z80ViewForm.StartPosition = FormStartPosition.Manual;
                 m_Z80ViewForm.Location = new Point(0, 0);
                 m_Z80ViewForm.Width = 343;
                 m_Z80ViewForm.Height = 601;
             });

            m_instForm.Invoke((MethodInvoker)delegate {
                 m_instForm.WindowState = FormWindowState.Normal;
                 m_instForm.StartPosition = FormStartPosition.Manual;
                 m_instForm.Location = new Point(0, m_Z80ViewForm.Height);
                 m_instForm.Width = m_Z80ViewForm.Width;
             });

            m_speedForm.Invoke((MethodInvoker)delegate {
                 m_speedForm.WindowState = FormWindowState.Normal;
                 m_speedForm.StartPosition = FormStartPosition.Manual;
                 m_speedForm.Location = new Point(0, m_Z80ViewForm.Height + m_instForm.Height);
                 m_speedForm.Width = m_Z80ViewForm.Width;
                 m_speedForm.Height = 150;
             });

            m_serialForm.Invoke((MethodInvoker)delegate {

                 m_serialForm.WindowState = FormWindowState.Normal;
                 m_serialForm.StartPosition = FormStartPosition.Manual;
                 m_serialForm.Location = new Point(0, m_Z80ViewForm.Height + m_instForm.Height + m_speedForm.Height);
                 m_serialForm.Width = m_Z80ViewForm.Width;
             });
            //----------------------
            m_codeForm.Invoke((MethodInvoker)delegate {
                if (m_codeForm != null)
                {
                    m_codeForm.WindowState = FormWindowState.Normal;
                    m_codeForm.StartPosition = FormStartPosition.Manual;
                    m_codeForm.Location = new Point(m_Z80ViewForm.Width, 0);
                    m_codeForm.Width = 850;
                }
            });

            m_callstackForm.Invoke((MethodInvoker)delegate {
                m_callstackForm.WindowState = FormWindowState.Normal;
                m_callstackForm.StartPosition = FormStartPosition.Manual;
                m_callstackForm.Location = new Point(m_Z80ViewForm.Width, m_codeForm.Height);
                m_callstackForm.Width = m_codeForm.Width;
                m_callstackForm.Height = 450;
            });

            m_ramForm.Invoke((MethodInvoker)delegate {
                 m_ramForm.WindowState = FormWindowState.Normal;
                 m_ramForm.StartPosition = FormStartPosition.Manual;
                 m_ramForm.Location = new Point(m_Z80ViewForm.Width + m_callstackForm.Width, 0);
             });

            m_bgTileMapForm.Invoke((MethodInvoker)delegate {
                 m_bgTileMapForm.WindowState = FormWindowState.Normal;
                 m_bgTileMapForm.StartPosition = FormStartPosition.Manual;
                 m_bgTileMapForm.Location = new Point(m_Z80ViewForm.Width + m_callstackForm.Width, m_ramForm.Height);
             });

            m_interruptsForm.Invoke((MethodInvoker)delegate {
                 m_interruptsForm.WindowState = FormWindowState.Normal;
                 m_interruptsForm.StartPosition = FormStartPosition.Manual;
                 m_interruptsForm.Location = new Point(m_Z80ViewForm.Width + m_callstackForm.Width + m_bgTileMapForm.Width, m_ramForm.Height);
             });

            m_callstackForm.Visible = true;
            m_instForm.Visible = true;
            m_Z80ViewForm.Visible = true;
            m_ramForm.Visible = true;
            m_mainForm.Visible = true;
            if( m_codeForm != null )
                m_codeForm.Visible = true;
            m_speedForm.Visible = true;
            m_bgTileMapForm.Visible = true;
            m_serialForm.Visible = true;
            m_interruptsForm.Visible = true;
        }

        public void UpdateForm()
        {
            m_speedForm.UpdateForm();
            m_mainForm.UpdateForm();
            m_interruptsForm.UpdateForm();
            m_serialForm.UpdateForm();
            m_Z80ViewForm.UpdateForm();
            //m_bgTileMapForm.UpdateForm();
            if (m_Z80ViewForm.IsAutoStep())
                return;
           
            m_ramForm.UpdateForm();
            m_instForm.UpdateForm();
            m_callstackForm.UpdateForm();
            if ( m_codeForm != null )
            {
                m_codeForm.UpdateForm();
            }
            
        }

        public static Z80ViewForm Z80Form()
        {
            return m_Z80ViewForm;
        }

        public static CodeView3Form CodeViewForm()
        {
            return m_codeForm;
        }

        public static CallstackForm CallStackForm()
        {
            return m_callstackForm;
        }

        public static SpeedForm GetSpeedForm()
        {
            return m_speedForm;
        }

        public static void DoRefresh(bool b)
        {
            m_bDoRefresh = b;
        }

        public static void ASSERT(bool b, String s)
        {
            if (!b)
            {
                MessageBox.Show(s);
                Console.WriteLine(s);
            }
        }

        public static CodeView3Form codeViewForm
        {
            get{ return m_codeForm;}
        }

        public static void WARNING(string s)
        {
            Console.WriteLine("[WARNING] " + s);
            System.Diagnostics.Debug.WriteLine(s);
        }

        public static void LOG(string s)
        {
            Console.WriteLine("[LOG] " + s);
            System.Diagnostics.Debug.WriteLine(s);
        }

        public static void READAT( byte value, ushort adress)
        {
            string sval = String.Format("{0:x2}", value);
            string sadd = String.Format("{0:x2}", adress);
            Console.WriteLine("[LOG] Read " + sval + " @ " + sadd );
        }

        public static void READAT(ushort value, ushort adress)
        {
            string sval = String.Format("{0:x2}", value);
            string sadd = String.Format("{0:x2}", adress);
            Console.WriteLine("[LOG] Read " + sval + " @ " + sadd);
        }

        public static void WRITEAT(byte value, ushort adress)
        {
            string sval = String.Format("{0:x2}", value);
            string sadd = String.Format("{0:x2}", adress);
            Console.WriteLine("[LOG] Write " + sval + " @ " + sadd);
        }

        public static void WRITEAT(ushort value, ushort adress)
        {
            string sval = String.Format("{0:x2}", value);
            string sadd = String.Format("{0:x2}", adress);
            Console.WriteLine("[LOG] Write " + sval + " @ " + sadd);
        }
    }
}
