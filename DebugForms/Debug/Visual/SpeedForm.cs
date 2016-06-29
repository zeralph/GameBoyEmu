using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GameBoyTest.Debug;
using GameBoyTest.Z80;


namespace GameBoyTest.Debug.Visual
{
    public partial class SpeedForm : Form
    {
        private bool m_tooSlow = false;
        private double m_realSpeed = 0;

        public SpeedForm()
        {
            InitializeComponent();
        }
        public void SetTooSlow( bool b )
        {
             m_tooSlow = b;
        }

        public void SetRealSpeed( double speedSec )
        {
            m_realSpeed = speedSec;// / 0.0000010;
            //labelRealSpeed.Text = String.Format("{0} Mhz", m_realSpeed);
        }

        private double GetTrackBarSpeedValue()
        {
            return (double)(trackBar_speed.Value);
        }

        public void Init()
        {
            label_curSpeed.Text = GetTrackBarSpeedValue() + " Mhz";
        }

        public void UpdateForm()
        {
            if( m_tooSlow )
            {
                label_curSpeed.BackColor = Color.Red;
            }
            else
            {
                label_curSpeed.BackColor = Color.Green;
            }
            labelRealSpeed.Text = String.Format( "{0:F4} Mhz", m_realSpeed );
        }

        private void button_normalSpeed_Click(object sender, EventArgs e)
        {

        }

        private void trackBar_speed_Scroll(object sender, EventArgs e)
        {
        }

        private void button_lowSpeed_Click(object sender, EventArgs e)
        {
            trackBar_speed.Value = 1;
        }

        private void label_curSpeed_Click(object sender, EventArgs e)
        {
            trackBar_speed.Value = 4000;
        }
    }
}
