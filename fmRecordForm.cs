using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceAuth
{
	public partial class fmRecordForm : Form
	{
		private AudioRecorder m_Rec;
		private TimeSpan m_EllapsedTime;
		private TimeSpan m_RecTime;

		public fmRecordForm()
		{
			m_Rec = new AudioRecorder(VoiceAuth.AudioSettings);
			InitializeComponent();
			timer1.Interval = 100;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			m_EllapsedTime += new TimeSpan(0, 0, 0, 0, timer1.Interval);
			if (m_EllapsedTime > m_RecTime)
			{
				timer1.Stop();
				m_Rec.StopRecord();
			}
			else
			{
				progressBar1.Value = (int)m_EllapsedTime.TotalMilliseconds;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			m_Rec.StartRecord();
			m_EllapsedTime = new TimeSpan();
			m_RecTime = new TimeSpan(0, 0, 10);
			progressBar1.Maximum = (int)m_RecTime.TotalMilliseconds;
			timer1.Start();
			button1.Enabled = false;
		}
	}
}
