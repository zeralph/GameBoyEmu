

#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBoyTest.Z80;
using GameBoyTest.Memory;
using GameBoyTest.Debug;
using System.Windows.Forms;
using GameBoyTest.Debug.Visual;
using GameBoyTest.Screen;
using GameBoyTest.Forms.Screen;
using GameBoyTest.Inputs;
using GameBoyTest.Audio;
using GameBoyTest.Parameters;
using GameBoyTest.MicroTimer;
using System.Threading;
using System.IO;
using System.Xml.Serialization;

namespace GameBoyTest
{
    class GameBoy
    {
        private const string PARAMETERS_FILE = "./prefs.xml";
        private static Z80Cpu m_cpu;
        private static MappedMemory m_memory;
        private static Cartridge m_cartridge;
//        private static DebugFunctions m_debugFunctions;
        private static GBScreenForm m_BGScreen;
        private static GBVideo m_video;
        private static DebuggerForm m_debuggerForm;
        private static SB_Debug m_sbDebug;
        private static InputsMgr m_inputsMgr;
        private static SoundManager m_soundManager;
        private static CParameters m_parameters;
        private static GameBoyTest.MicroTimer.MicroTimer m_timer;

        private static System.Threading.Thread m_DebugThread;
        private static bool m_bDebuggerEnabled = false;
//        private static bool m_debugContextInitalized = false;

        public GameBoy()
        {
            m_parameters = new CParameters();
            LoadParameters();
            m_DebugThread = new System.Threading.Thread(DebuggerThread);
            m_DebugThread.Start();
//          m_debugContextInitalized = false;

            m_timer = new GameBoyTest.MicroTimer.MicroTimer();

            m_BGScreen = new GBScreenForm(GameBoy.Ram);
            m_memory = new MappedMemory();
            m_cartridge = new Cartridge();
            m_sbDebug = new SB_Debug();
            m_inputsMgr = new InputsMgr();
            m_video = new GBVideo( m_BGScreen );
            m_soundManager = new SoundManager();
            m_cpu = new Z80Cpu( m_timer );

            m_bDebuggerEnabled = false;

            m_cpu.Init();
            m_video.Init();
            m_sbDebug.Init();
            //sDebugReady = false;

            //m_cpu.Start();
            m_video.Start();
            Application.Run();
            System.Windows.Forms.Application.Exit();
        }

        public static void Exit()
        {
            m_soundManager.Stop();
            GameBoy.Cpu.Stop();
            m_soundManager.Stop();
            GameBoy.Cartridge.Exit();
            FlushParameters();
            if( m_debuggerForm != null )
            {
                m_debuggerForm.Dispose();
            }
            m_video.Dispose();
            m_BGScreen.Dispose();
            m_soundManager.Dispose();
            m_cpu.Dispose();
            m_inputsMgr.Dispose();
            m_DebugThread.Abort();
            Application.Exit();
        }

        public static void OnClose()
        {
            Exit();
            
            //System.Environment.Exit(0);
            //System.Windows.Forms.Application.Exit();
        }

        public static void LoadCartridge(String fullpath, String name, bool bSaveToIni)
        {
            m_soundManager.Stop();
            m_cpu.Stop();
            GameBoy.Ram.Clear();
            if (m_cartridge.Init(fullpath, name, bSaveToIni))
            {
                m_cpu.Init();
                if (m_debuggerForm != null)
                {
                    m_debuggerForm.Init();
                }
//                if (m_debugFunctions != null)
//                {
//                    m_debugFunctions.Init();
//                }
                m_soundManager.Init();
                m_BGScreen.Init();
                m_BGScreen.SetRomTitle(m_cartridge.GetTitle());
                m_soundManager.Start();
                m_cpu.Start();
                m_video.Start();
            }
        }

        public static void EnableDebugger()
        {
            EnableDebugger(!m_bDebuggerEnabled);
        }

        public static void EnableDebugger( bool bEnable )
        {
			if (m_debuggerForm == null)
			{
				CreateDebugContext();
			}
			/*
            //m_bDebuggerEnabled = bEnable;
            Action act = delegate ()
            {
                CreateDebugContext();
                while(true)
                {
                    Thread.Sleep(16);
                    m_debugFunctions.UpdateForm();
                }
                DestroyDebugContext();
            };
            act.BeginInvoke(OnDebugFormClosed, null);
            */
		}

        public static void CreateDebugContext()
        {
            m_debuggerForm = new DebuggerForm();
            //m_debugFunctions = new DebugFunctions(m_debuggerForm);
            m_debuggerForm.Init();
			m_debuggerForm.Show();
			//m_debugFunctions.Init();
			//          m_debugContextInitalized = true;
		}

        public static void DestroyDebugContext()
        {
            //m_debugFunctions.Dispose();
            //m_debugFunctions = null;
            m_debuggerForm.Close();
            m_debuggerForm.Dispose();
            m_debuggerForm = null;
//            m_debugContextInitalized = false;
        }

        public static void DebuggerThread()
        {
            /*
            Action act = delegate ()
            {
                if (m_bDebuggerEnabled && !m_debugContextInitalized)
                {
                    CreateDebugContext();
                }
            };
            act.BeginInvoke(OnDebugFormClosed, null);
            */
            /*
            while (true)
            {
                if (m_bDebuggerEnabled && !m_debugContextInitalized)
                {
                    CreateDebugContext();
                }
                if (!m_bDebuggerEnabled && m_debugContextInitalized)
                {
                    DestroyDebugContext();
                }
                if (m_bDebuggerEnabled && m_debugContextInitalized)
                {
                    m_debugFunctions.UpdateForm();
                }
                Application.DoEvents();
                Thread.Sleep(16);
            }
            */
        }

        private static void OnDebugFormClosed(IAsyncResult result)
        {

        }

        public static void FlushParameters()
        {

            XmlSerializer mySerializer = new XmlSerializer(typeof(CParameters));
            StreamWriter myWriter = new StreamWriter(PARAMETERS_FILE);
            mySerializer.Serialize(myWriter, GameBoy.Parameters);
            myWriter.Close();
        }

        public void LoadParameters()
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(CParameters));
            if( File.Exists(PARAMETERS_FILE))
            {
                FileStream myFileStream = new FileStream(PARAMETERS_FILE, FileMode.Open);
                GameBoy.Parameters = (CParameters)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
            }
        }

        public static bool IsDebuggerEnabled()
        {
            return m_bDebuggerEnabled;
        }

        public static DebugFunctions Debugger
        {
            get { return m_debuggerForm.GetDebugger(); }
        }

        public static InputsMgr InputsMgr
        {
            get { return m_inputsMgr; }
        }

        public static Z80Cpu Cpu
        {
            get { return m_cpu; }
        }

        public static MappedMemory Ram
        {
            get { return m_memory; }
        }

        public static GBVideo Video
        {
            get { return m_video; }
        }

        public static GBScreenForm Screen
        {
            get { return m_BGScreen; }
        }

        public static Cartridge Cartridge
        {
            get { return m_cartridge; }
        }

        public static SoundManager Sound
        {
            get { return m_soundManager; }
        }

        public static CParameters Parameters
        {
            get { return m_parameters; }
            set { m_parameters = value;  }
        }  
    }
}
