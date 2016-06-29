using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameBoyTest.Inputs;
using SlimDX.XInput;
using System.Threading;

namespace GameBoyTest.Forms
{
    public partial class InputManagerForm : Form
    {
        public delegate void OnInputsChanged(object sender, EventArgs e);

        public class ComboboxItem
        {
            public ComboboxItem(string text, InputsMgr.eInputName value, bool bIsGamepad)
            {
                Text = text;
                Value = value;
                IsGamepad = bIsGamepad;
            }
            public string Text { get; set; }
            public InputsMgr.eInputName Value { get; set; }
            public bool IsGamepad { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private int[] m_inputs = new int[(int)InputsMgr.e_buttons.__btn_max__];
        private bool m_isWaitingForInput = false;
        private InputsMgr.e_buttons m_currentWaitingButton = InputsMgr.e_buttons.btn_none;
        public static event OnInputsChanged m_inputsChanged;

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        public InputManagerForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            for (int i = 0; i < (int)InputsMgr.e_buttons.__btn_max__; i++)
            {
                m_inputs[i] = (int)Keys.None;
            }
            ComboboxItem item;
            string name = "";
            item = new ComboboxItem("Keyboard", InputsMgr.eInputName.e_inputName_keyboard, false);
            comboInputSelection.Items.Add(item);
            if (IsGamepadPresent(SlimDX.XInput.UserIndex.One, ref name))
            {
                item = new ComboboxItem(name + "_1", InputsMgr.eInputName.e_inputName_gamepad_1, true);
                comboInputSelection.Items.Add(item);
            }
            if (IsGamepadPresent(SlimDX.XInput.UserIndex.Two, ref name))
            {
                item = new ComboboxItem(name + "_2", InputsMgr.eInputName.e_inputName_gamepad_2, true);
                comboInputSelection.Items.Add(item);
            }
            if (IsGamepadPresent(SlimDX.XInput.UserIndex.Three, ref name))
            {
                item = new ComboboxItem(name + "_3", InputsMgr.eInputName.e_inputName_gamepad_3, true);
                comboInputSelection.Items.Add(item);
            }
            if (IsGamepadPresent(SlimDX.XInput.UserIndex.Four, ref name))
            {
                item = new ComboboxItem(name + "_4", InputsMgr.eInputName.e_inputName_gamepad_4, true);
                comboInputSelection.Items.Add(item);
            }
            comboInputSelection.SelectedItem = comboInputSelection.Items[0];

            InitUI();
            RefreshUI();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private int getGamepadInput(Xbox360Controller.Gamepad360 g)
        {
            SlimDX.XInput.Gamepad gamepadState = g.Controller.GetState().Gamepad;
            // Shoulders
            if ((gamepadState.Buttons & GamepadButtonFlags.LeftShoulder) != 0)
            {
                return (int)GamepadButtonFlags.LeftShoulder;
            }
            if ((gamepadState.Buttons & GamepadButtonFlags.RightShoulder) != 0)
            {
                return (int)GamepadButtonFlags.LeftShoulder;
            }
            // Buttons
            if ((gamepadState.Buttons & GamepadButtonFlags.Start) != 0)
            {
                return (int)GamepadButtonFlags.Start;
            }
            if ((gamepadState.Buttons & GamepadButtonFlags.Back) != 0)
            {
                return (int)GamepadButtonFlags.Back;
            }
            if ((gamepadState.Buttons & GamepadButtonFlags.A) != 0)
            {
                return (int)GamepadButtonFlags.A;
            }
            if ((gamepadState.Buttons & GamepadButtonFlags.B) != 0)
            {
                return (int)GamepadButtonFlags.B;
            }
            if ((gamepadState.Buttons & GamepadButtonFlags.X) != 0)
            {
                return (int)GamepadButtonFlags.X;
            }
            if ((gamepadState.Buttons & GamepadButtonFlags.Y) != 0)
            {
                return (int)GamepadButtonFlags.Y;
            }
            // D-Pad
            if ((gamepadState.Buttons & GamepadButtonFlags.DPadUp) != 0)
            {
                return (int)GamepadButtonFlags.DPadUp;
            }
            if ((gamepadState.Buttons & GamepadButtonFlags.DPadDown) != 0)
            {
                return (int)GamepadButtonFlags.DPadDown;
            }
            if ((gamepadState.Buttons & GamepadButtonFlags.DPadLeft) != 0)
            {
                return (int)GamepadButtonFlags.DPadLeft;
            }
            if ((gamepadState.Buttons & GamepadButtonFlags.DPadRight) != 0)
            {
                return (int)GamepadButtonFlags.DPadRight;
            }
            return 0;


            /*
            // Thumbsticks
            LeftStick = new ThumbstickState(
                Normalize(gamepadState.LeftThumbX, gamepadState.LeftThumbY, Gamepad.GamepadLeftThumbDeadZone),
                (gamepadState.Buttons & GamepadButtonFlags.LeftThumb) != 0);
            RightStick = new ThumbstickState(
                Normalize(gamepadState.RightThumbX, gamepadState.RightThumbY, Gamepad.GamepadRightThumbDeadZone),
                (gamepadState.Buttons & GamepadButtonFlags.RightThumb) != 0);
            // Triggers
            if (gamepadState.LeftTrigger / (float)byte.MaxValue != 0.0 )
            {
                return (int)-1;
            }
            if (gamepadState.RightTrigger / (float)byte.MaxValue != 0.0 )
            {
                return (int)-1;
            }
             * */
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private int getKeyboardInput()
        {
            return 0;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void RecordInput(InputsMgr.e_buttons button)
        {
            ComboboxItem item = (ComboboxItem)comboInputSelection.SelectedItem;
            if (item.Value == InputsMgr.eInputName.e_inputName_keyboard)
            {
                m_isWaitingForInput = true;
                m_currentWaitingButton = button;
            }
            else
            {
                m_isWaitingForInput = true;
                m_currentWaitingButton = button;
                Thread t = new Thread( new ParameterizedThreadStart(GamePadCheckInput) );
                t.Start( item );
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private bool IsGamepadPresent(SlimDX.XInput.UserIndex userIndex, ref string name)
        {
            Xbox360Controller.Gamepad360 g;
            g = new Xbox360Controller.Gamepad360(userIndex);
            name = g.ToString();
            return g.Connected;
        }

        private void InitUI()
        {
            InputsMgr.eInputName inputName = (InputsMgr.eInputName)GameBoy.Parameters.inputName;

            if (inputName == InputsMgr.eInputName.e_inputName_keyboard)
            {
                m_inputs[(int)InputsMgr.e_buttons.btn_A] = GameBoy.Parameters.inputA;
                m_inputs[(int)InputsMgr.e_buttons.btn_B] = GameBoy.Parameters.inputB;
                m_inputs[(int)InputsMgr.e_buttons.btn_Start] = GameBoy.Parameters.inputStart;
                m_inputs[(int)InputsMgr.e_buttons.btn_Select] = GameBoy.Parameters.inputSelect;
                m_inputs[(int)InputsMgr.e_buttons.btn_up] = GameBoy.Parameters.inputUp;
                m_inputs[(int)InputsMgr.e_buttons.btn_down] = GameBoy.Parameters.inputDown;
                m_inputs[(int)InputsMgr.e_buttons.btn_left] = GameBoy.Parameters.inputLeft;
                m_inputs[(int)InputsMgr.e_buttons.btn_right] = GameBoy.Parameters.inputRight;
            }
            else
            {
                AInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_A]);
                BInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_B]);
                StartInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_Start]);
                SelectInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_Select]);
                UpInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_up]);
                DownInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_down]);
                LeftInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_left]);
                RightInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_right]);
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void RefreshUI()
        {
            ComboboxItem item = (ComboboxItem)comboInputSelection.SelectedItem;
            if (item.IsGamepad)
            {
                AInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_A]);
                BInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_B]);
                StartInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_Start]);
                SelectInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_Select]);
                UpInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_up]);
                DownInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_down]);
                LeftInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_left]);
                RightInput.Text = GamepadButtonFlagToString(m_inputs[(int)InputsMgr.e_buttons.btn_right]);

            }
            else
            {
                AInput.Text = ((Keys)m_inputs[(int)InputsMgr.e_buttons.btn_A]).ToString();
                BInput.Text = ((Keys)m_inputs[(int)InputsMgr.e_buttons.btn_B]).ToString();
                StartInput.Text = ((Keys)m_inputs[(int)InputsMgr.e_buttons.btn_Start]).ToString();
                SelectInput.Text = ((Keys)m_inputs[(int)InputsMgr.e_buttons.btn_Select]).ToString();
                UpInput.Text = ((Keys)m_inputs[(int)InputsMgr.e_buttons.btn_up]).ToString();
                DownInput.Text = ((Keys)m_inputs[(int)InputsMgr.e_buttons.btn_down]).ToString();
                LeftInput.Text = ((Keys)m_inputs[(int)InputsMgr.e_buttons.btn_left]).ToString();
                RightInput.Text = ((Keys)m_inputs[(int)InputsMgr.e_buttons.btn_right]).ToString();
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////-
        private void UpInput_MouseClick(object sender, MouseEventArgs e)
        {
            RecordInput(InputsMgr.e_buttons.btn_up);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void DownInput_Click(object sender, EventArgs e)
        {
            RecordInput(InputsMgr.e_buttons.btn_down);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void LeftInput_Click(object sender, EventArgs e)
        {
            RecordInput(InputsMgr.e_buttons.btn_left);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void RightInput_Click(object sender, EventArgs e)
        {
            RecordInput(InputsMgr.e_buttons.btn_right);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void AInput_Click(object sender, EventArgs e)
        {
            RecordInput(InputsMgr.e_buttons.btn_A);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void BInput_Click(object sender, EventArgs e)
        {
            RecordInput(InputsMgr.e_buttons.btn_B);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void StartInput_Click(object sender, EventArgs e)
        {
            RecordInput(InputsMgr.e_buttons.btn_Start);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void SelectInput_Click(object sender, EventArgs e)
        {
            RecordInput(InputsMgr.e_buttons.btn_Select);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void InputManagerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_isWaitingForInput)
            {
                Keys k = e.KeyCode;
                m_inputs[(int)m_currentWaitingButton] = (int)k;
                m_isWaitingForInput = false;
                m_currentWaitingButton = (int)Keys.None;
                RefreshUI();
            }
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void comboInputSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_isWaitingForInput = false;
            m_currentWaitingButton = (int)Keys.None;
            for (int i = 0; i < (int)InputsMgr.e_buttons.__btn_max__; i++)
            {
                m_inputs[i] = (int)Keys.None;
            }
            RefreshUI();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveInputs();
            this.Close();
            m_inputsChanged.Invoke(this,e);
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private string GamepadButtonFlagToString( int f)
        {
            string s = "";
            switch (f)
            {
                case (int)GamepadButtonFlags.Y: s = "Y"; break;
                case (int)GamepadButtonFlags.None: s = "None"; break;
                case (int)GamepadButtonFlags.DPadUp: s = "DPadUp"; break;
                case (int)GamepadButtonFlags.DPadDown: s = "DPadDown"; break;
                case (int)GamepadButtonFlags.DPadLeft: s = "DPadLeft"; break;
                case (int)GamepadButtonFlags.DPadRight: s = "DPadRight"; break;
                case (int)GamepadButtonFlags.Start: s = "Start"; break;
                case (int)GamepadButtonFlags.Back: s = "Back"; break;
                case (int)GamepadButtonFlags.LeftThumb: s = "LeftThumb"; break;
                case (int)GamepadButtonFlags.RightThumb: s = "RightThumb"; break;
                case (int)GamepadButtonFlags.LeftShoulder: s = "LeftShoulder"; break;
                case (int)GamepadButtonFlags.RightShoulder: s = "RightShoulder"; break;
                case (int)GamepadButtonFlags.A: s = "A"; break;
                case (int)GamepadButtonFlags.B: s = "B"; break;
                case (int)GamepadButtonFlags.X: s = "X"; break;
            }
            return s;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void SaveInputs()
        {
            GameBoy.Parameters.inputA =m_inputs[(int)InputsMgr.e_buttons.btn_A];     
            GameBoy.Parameters.inputB =m_inputs[(int)InputsMgr.e_buttons.btn_B];      
            GameBoy.Parameters.inputStart = m_inputs[(int)InputsMgr.e_buttons.btn_Start];  
            GameBoy.Parameters.inputSelect = m_inputs[(int)InputsMgr.e_buttons.btn_Select]; 
            GameBoy.Parameters.inputUp = m_inputs[(int)InputsMgr.e_buttons.btn_up];     
            GameBoy.Parameters.inputDown = m_inputs[(int)InputsMgr.e_buttons.btn_down];   
            GameBoy.Parameters.inputLeft = m_inputs[(int)InputsMgr.e_buttons.btn_left];
            GameBoy.Parameters.inputRight = m_inputs[(int)InputsMgr.e_buttons.btn_right];
            GameBoy.Parameters.inputName = (int)GetInputName();
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private InputsMgr.eInputName GetInputName()
        {
            ComboboxItem item = (ComboboxItem)comboInputSelection.SelectedItem;
            return item.Value;
        }

        //////////////////////////////////////////////////////////////////////
        //
        //////////////////////////////////////////////////////////////////////
        private void GamePadCheckInput(object comboBoxItemObj )
        {
            ComboboxItem item = (ComboboxItem)comboBoxItemObj;
            UserIndex i = UserIndex.Any;
            switch (item.Value)
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
            if (i != UserIndex.Any)
            {
                Xbox360Controller.Gamepad360 g = new Xbox360Controller.Gamepad360(i);
                int input = 0;
                do
                {
                    input = getGamepadInput(g);
                    Thread.Sleep(100);
                } while (input == 0);
                m_inputs[(int)m_currentWaitingButton] = input;
                m_isWaitingForInput = false;
                m_currentWaitingButton = (int)Keys.None;

                this.Invoke((MethodInvoker)delegate()
                {
                    this.RefreshUI();
                });
            }
        }
    }
}
