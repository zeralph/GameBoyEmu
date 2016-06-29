using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using GameBoyTest.Debug;
using GameBoyTest.Z80;
using System.Runtime.InteropServices;
using System.Threading;
using GameBoyTest.Forms;
using SlimDX.XInput;
//using SlimDX.

namespace GameBoyTest.Inputs
{
    public class InputsMgr : IDisposable
    {
        public enum e_buttons
        {
            btn_none=-1,
            btn_right=0,
            btn_left,
            btn_up,
            btn_down,
            btn_A,
            btn_B,
            btn_Select,
            btn_Start,
            __btn_max__,
        };

        public enum eInputName
        {
            e_inputName_none = 0,
            e_inputName_keyboard,
            e_inputName_gamepad_1,
            e_inputName_gamepad_2,
            e_inputName_gamepad_3,
            e_inputName_gamepad_4,
        }

        const double GAMEPAD_THRESHOLD = 0.5;

        Xbox360Controller.Gamepad360 m_gamepad;

        static byte p10 = 0x01;
        static byte p11 = 0x02;
        static byte p12 = 0x04;
        static byte p13 = 0x08;
        static byte p14 = 0x10;
        static byte p15 = 0x20;


        private byte[] m_btnRow = new byte[(int)e_buttons.__btn_max__];
        private byte[] m_btnCol = new byte[(int)e_buttons.__btn_max__];
        private bool[] m_btnStat = new bool[(int)e_buttons.__btn_max__];
        private InputsMgr.eInputName m_currentInput = eInputName.e_inputName_none;
        private long m_tickCounter = 0;
        private System.Threading.Thread m_InputsThread;
        private Key m_k_up = Key.None;
        private Key m_k_down = Key.None;
        private Key m_k_left = Key.None;
        private Key m_k_right = Key.None;
        private Key m_k_A = Key.None;
        private Key m_k_B = Key.None;
        private Key m_k_start = Key.None;
        private Key m_k_select = Key.None;

        private GamepadButtonFlags m_p_up = GamepadButtonFlags.None;
        private GamepadButtonFlags m_p_down = GamepadButtonFlags.None;
        private GamepadButtonFlags m_p_left = GamepadButtonFlags.None;
        private GamepadButtonFlags m_p_right = GamepadButtonFlags.None;
        private GamepadButtonFlags m_p_A = GamepadButtonFlags.None;
        private GamepadButtonFlags m_p_B = GamepadButtonFlags.None;
        private GamepadButtonFlags m_p_start = GamepadButtonFlags.None;
        private GamepadButtonFlags m_p_select = GamepadButtonFlags.None;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public InputsMgr()
        {

            m_btnRow[(int)e_buttons.btn_right]      = p10;
            m_btnRow[(int)e_buttons.btn_left]       = p11;
            m_btnRow[(int)e_buttons.btn_up]         = p12;
            m_btnRow[(int)e_buttons.btn_down]       = p13;
            m_btnRow[(int)e_buttons.btn_A]          = p10;
            m_btnRow[(int)e_buttons.btn_B]          = p11;
            m_btnRow[(int)e_buttons.btn_Select]     = p12;
            m_btnRow[(int)e_buttons.btn_Start]      = p13;

            m_btnCol[(int)e_buttons.btn_right]      = p14;
            m_btnCol[(int)e_buttons.btn_left]       = p14;
            m_btnCol[(int)e_buttons.btn_up]         = p14;
            m_btnCol[(int)e_buttons.btn_down]       = p14;
            m_btnCol[(int)e_buttons.btn_A]          = p15;
            m_btnCol[(int)e_buttons.btn_B]          = p15;
            m_btnCol[(int)e_buttons.btn_Select]     = p15;
            m_btnCol[(int)e_buttons.btn_Start]      = p15;

            m_btnStat[(int)e_buttons.btn_right]     = false;
            m_btnStat[(int)e_buttons.btn_left] = false;
            m_btnStat[(int)e_buttons.btn_up] = false;
            m_btnStat[(int)e_buttons.btn_down] = false;
            m_btnStat[(int)e_buttons.btn_A] = false;
            m_btnStat[(int)e_buttons.btn_B] = false;
            m_btnStat[(int)e_buttons.btn_Select] = false;
            m_btnStat[(int)e_buttons.btn_Start] = false;

            InputManagerForm.m_inputsChanged += new InputManagerForm.OnInputsChanged(OnInputChanged);
            //load config
            OnInputChanged(this, null);

            m_tickCounter = 0;
            m_InputsThread = new System.Threading.Thread(UpdateInputsThread);
            m_InputsThread.SetApartmentState(ApartmentState.STA);
            m_InputsThread.Start();
        }

        private void OnInputChanged(object sender, EventArgs e)
        {
            m_currentInput = (InputsMgr.eInputName)GameBoy.Parameters.inputName;
            if (m_currentInput != eInputName.e_inputName_none)
            {
                if (m_currentInput == eInputName.e_inputName_keyboard)
                {
                    m_k_up = (Key)KeyInterop.KeyFromVirtualKey((int)GameBoy.Parameters.inputUp);
                    m_k_down = (Key)KeyInterop.KeyFromVirtualKey((int)GameBoy.Parameters.inputDown);
                    m_k_left = (Key)KeyInterop.KeyFromVirtualKey((int)GameBoy.Parameters.inputLeft);
                    m_k_right = (Key)KeyInterop.KeyFromVirtualKey((int)GameBoy.Parameters.inputRight);
                    m_k_A = (Key)KeyInterop.KeyFromVirtualKey((int)GameBoy.Parameters.inputA);
                    m_k_B = (Key)KeyInterop.KeyFromVirtualKey((int)GameBoy.Parameters.inputB);
                    m_k_start = (Key)KeyInterop.KeyFromVirtualKey((int)GameBoy.Parameters.inputStart);
                    m_k_select = (Key)KeyInterop.KeyFromVirtualKey((int)GameBoy.Parameters.inputSelect);
                }
                else
                {
                    UserIndex i = UserIndex.Any;
                    switch (m_currentInput)
                    {
                        case InputsMgr.eInputName.e_inputName_gamepad_1:
                            {
                                i = UserIndex.One;
                                break;
                            }
                        case InputsMgr.eInputName.e_inputName_gamepad_2:
                            {
                                i = UserIndex.Two;
                                break;
                            }
                        case InputsMgr.eInputName.e_inputName_gamepad_3:
                            {
                                i = UserIndex.Three;
                                break;
                            }
                        case InputsMgr.eInputName.e_inputName_gamepad_4:
                            {
                                i = UserIndex.Four;
                                break;
                            }
                    }
                    m_gamepad = new Xbox360Controller.Gamepad360(i); 
                    m_p_up = (GamepadButtonFlags)GameBoy.Parameters.inputUp;
                    m_p_down = (GamepadButtonFlags)GameBoy.Parameters.inputDown;
                    m_p_left = (GamepadButtonFlags)GameBoy.Parameters.inputLeft;
                    m_p_right = (GamepadButtonFlags)GameBoy.Parameters.inputRight;
                    m_p_A = (GamepadButtonFlags)GameBoy.Parameters.inputA;
                    m_p_B = (GamepadButtonFlags)GameBoy.Parameters.inputB;
                    m_p_start = (GamepadButtonFlags)GameBoy.Parameters.inputStart;
                    m_p_select = (GamepadButtonFlags)GameBoy.Parameters.inputSelect;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        //this function is update each microsecond (1Mhz)
        //////////////////////////////////////////////////////////////////////
        public void Update()
        {
           //UpdateInputs();
           UpdateFlags();
           m_tickCounter++;
           
        }

        public void Dispose()
        {
            m_InputsThread.Abort();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private byte UpdateBtnStatus(e_buttons btn)
        {
            byte old = GameBoy.Ram.ReadByteAt(0xFF00);
            byte col = m_btnCol[(int)btn];
            if ((col & old) == 0)
            {
                if( m_btnStat[(int)btn] )
                {
                    //m_btnStat[(int)btn] = false;
                    return (byte)(0xFF & ~m_btnRow[(int)btn]);
                }
            }
            return 0xFF;
        }

        private void UpdateInputsThread()
        {
            
            while (true)
            {
                UpdateInputs();
                Thread.Sleep(30);
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void UpdateFlags()
        {
            byte old = GameBoy.Ram.ReadByteAt(0xFF00);
            byte b = 0xFF;
            for (int i = 0; i < (int)e_buttons.__btn_max__; i++)
            {
                b &= UpdateBtnStatus((e_buttons)i);
            }
            if ((old & 0x20) != 0)
            {
                b |= 0x20;
            }
            else
            {
                b &= 0xDF;
            }
            if ((old & 0x10) != 0)
            {
                b |= 0x10;
            }
            else
            {
                b &= 0xEF;
            }
            GameBoy.Ram.WriteByte(0xFF00, b);
            b &= 0x0F;
            if (b != 0x0F)
            {
                WriteJoypadInterrupt();
            }
        }

        private void UpdateInputs()
        {
            //if( this.active)
            m_btnStat[(int)e_buttons.btn_right] = false;
            m_btnStat[(int)e_buttons.btn_left] = false;
            m_btnStat[(int)e_buttons.btn_up] = false;
            m_btnStat[(int)e_buttons.btn_down] = false;
            m_btnStat[(int)e_buttons.btn_A] = false;
            m_btnStat[(int)e_buttons.btn_B] = false;
            m_btnStat[(int)e_buttons.btn_Select] = false;
            m_btnStat[(int)e_buttons.btn_Start] = false;

            if (m_currentInput == eInputName.e_inputName_keyboard)
            {
                UpdateKeyboardInput();
            }
            else
            {
                m_gamepad.Update();
                UpdateGamepadInput();
            }
        }

        private void UpdateKeyboardInput()
        {
            //START
            if (Keyboard.IsKeyDown(m_k_start))
            {
                m_btnStat[(int)e_buttons.btn_Start] |= true;
            }
            //A
            if (Keyboard.IsKeyDown(m_k_A))
            {
                m_btnStat[(int)e_buttons.btn_A] |= true;
            }
            //B
            if (Keyboard.IsKeyDown(m_k_B))
            {
                m_btnStat[(int)e_buttons.btn_B] |= true;
            }
            //UP
            if (Keyboard.IsKeyDown(m_k_up))
            {
                m_btnStat[(int)e_buttons.btn_up] |= true;
            }
            //DOWN
            if (Keyboard.IsKeyDown(m_k_down))
            {
                m_btnStat[(int)e_buttons.btn_down] |= true;
            }
            //LEFT
            if (Keyboard.IsKeyDown(m_k_left))
            {
                m_btnStat[(int)e_buttons.btn_left] |= true;
            }
            //RIGHT
            if (Keyboard.IsKeyDown(m_k_right))
            {
                m_btnStat[(int)e_buttons.btn_right] |= true;
            }
            //SELECT
            if (Keyboard.IsKeyDown(m_k_select))
            {
                m_btnStat[(int)e_buttons.btn_Select] |= true;
            }
        }

        private void UpdateGamepadInput()
        {
            //START
            if (m_gamepad.Start)
            {
                m_btnStat[(int)e_buttons.btn_Start] |= true;
            }
            //A
            if (m_gamepad.A)
            {
                m_btnStat[(int)e_buttons.btn_A] |= true;
            }
            //B
            if (m_gamepad.B)
            {
                m_btnStat[(int)e_buttons.btn_B] |= true;
            }
            //UP
            if (m_gamepad.LeftStick.Position.Y > GAMEPAD_THRESHOLD  || m_gamepad.DPad.Up )
            {
                m_btnStat[(int)e_buttons.btn_up] |= true;
            }
            //DOWN
            if (m_gamepad.LeftStick.Position.Y < -GAMEPAD_THRESHOLD  || m_gamepad.DPad.Down )
            {
                m_btnStat[(int)e_buttons.btn_down] |= true;
            }
            //LEFT
            if (m_gamepad.LeftStick.Position.X < -GAMEPAD_THRESHOLD  || m_gamepad.DPad.Left)
            {
                m_btnStat[(int)e_buttons.btn_left] |= true;
            }
            //RIGHT
            if (m_gamepad.LeftStick.Position.X > GAMEPAD_THRESHOLD   || m_gamepad.DPad.Right )
            {
                m_btnStat[(int)e_buttons.btn_right] |= true;
            }
            //SELECT
            if (m_gamepad.Back)
            {
                m_btnStat[(int)e_buttons.btn_Select] |= true;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void WriteJoypadInterrupt()
        {
            byte b = GameBoy.Ram.ReadByteAt(0xFF0F);
            b |= 0x10;
            GameBoy.Ram.WriteByte(0xFF0F, b);
        }
    }
}
