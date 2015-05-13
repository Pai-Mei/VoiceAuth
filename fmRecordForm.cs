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
		private bool m_StartRec = false;

		public String Login { get; set; }

		public Double[] Mels { get; private set; }

		public fmRecordForm()
		{
			m_Rec = new AudioRecorder(VoiceAuth.AudioSettings);
			InitializeComponent();
			timer1.Interval = 100;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			pictureBox1.Image = m_Rec.Visulization(pictureBox1.Size);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (!m_StartRec)
			{
				m_Rec.StartRecord();
				timer1.Start();
				button1.Text = "Остановить";
			}
			else
			{
				timer1.Stop();
				m_Rec.StopRecord();
				button1.Text = "Запись";
				Mels = m_Rec.GetMels();
				try
				{
					pictureBox1.Image.Save(Environment.CurrentDirectory + "\\" + Login + ".img");
				}
				catch { }
				this.Close();
			}
			m_StartRec = !m_StartRec;
		}
	}
}
