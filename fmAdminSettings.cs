using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Mixer;
using NAudio.Wave;
using System.IO;

namespace VoiceAuth
{
	public partial class fmAdminSettings : Form
	{
		private Settings m_Settings;

		public fmAdminSettings()
		{
			InitializeComponent();
			m_Settings = Settings.Load();
		}

		private void LoadList()
		{
			var users = VoiceAuth.AuthClient.GetUsers();
			listBox1.Items.Clear();
			foreach (var user in users)
			{
				listBox1.Items.Add(user);
			}
		}

		private void LoadAudioSettings()
		{
			comboBoxDevice.Items.Clear();
			int waveInDevices = WaveIn.DeviceCount;
			for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
			{
				WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
				comboBoxDevice.Items.Add(deviceInfo.ProductName);
			} 
			if(m_Settings.DeviceNumber  < comboBoxDevice.Items.Count)
				comboBoxDevice.SelectedIndex = m_Settings.DeviceNumber;
			else
				comboBoxDevice.SelectedIndex = 0;
			numericUpDownBufferSize.Value = m_Settings.BufferMilliseconds;
			numericMels.Value = m_Settings.MelsNumber;
			numericRecords.Value = m_Settings.RecCount;
			comboBoxFrameSize.Text = m_Settings.FrameSize.ToString();
			numericUpDownError.Value = Convert.ToDecimal(m_Settings.LearningError);
			numericMaxCount.Value = m_Settings.MaxLearningCount;
		}

		private void SaveAudioSettings()
		{
			m_Settings.BufferMilliseconds = Convert.ToInt32(numericUpDownBufferSize.Value);
			m_Settings.DeviceNumber = comboBoxDevice.SelectedIndex;
			m_Settings.FrameSize = int.Parse(comboBoxFrameSize.Text);
			m_Settings.MelsNumber = Convert.ToInt32(numericMels.Value);
			m_Settings.RecCount = Convert.ToInt32(numericRecords.Value);
			m_Settings.MaxLearningCount = Convert.ToInt32(numericMaxCount.Value);
			m_Settings.LearningError = Convert.ToDouble(numericUpDownError.Value);
			m_Settings.Save();
		}

		private void fmAdminSettings_Load(object sender, EventArgs e)
		{
			LoadList();
			LoadAudioSettings();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (listBox1.SelectedIndex > -1)
			{
				var login = (string)listBox1.SelectedItem;
				if (new fmUserDataDialog(login).ShowDialog() == System.Windows.Forms.DialogResult.OK)
					LoadList();
			}
			else
			{
				if (new fmUserDataDialog().ShowDialog() == System.Windows.Forms.DialogResult.OK)
					LoadList();
			}
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if(listBox1.SelectedIndex > 0)
			{
				var login = (string)listBox1.SelectedItem;
				VoiceAuth.AuthClient.RemoveUser(login);
			}
			LoadList();
		}

		private void btnChange_Click(object sender, EventArgs e)
		{
			if (listBox1.SelectedIndex > -1)
			{
				var login = (string)listBox1.SelectedItem;
				if (new fmUserDataDialog(login).ShowDialog() == System.Windows.Forms.DialogResult.OK)
					LoadList();
			}
			
		}

		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void comboBoxDevice_SelectedIndexChanged(object sender, EventArgs e)
		{
			
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			if (listBox1.SelectedIndex > -1)
			{
				SaveAudioSettings();
				var numberRecords = (Int32)numericRecords.Value;
				var mels = new List<Double[]>();
				for (int i = 0; i < numberRecords; i++)
				{
					fmRecordForm rec = new fmRecordForm();
					rec.Login = (string)listBox1.Items[listBox1.SelectedIndex];
					rec.ShowDialog();
					mels.Add(rec.Mels);
				}
				var analiser = new VoiceAnalys();
				
			}
		}

		private void label4_Click(object sender, EventArgs e)
		{

		}

		private void fmAdminSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveAudioSettings();
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			var login = (string)listBox1.SelectedItem;
			var filePath = Environment.CurrentDirectory + "\\" + login + ".img";
			if (File.Exists(filePath))
				pictureBoxSpectr.Image = new Bitmap(filePath);
			pictureBoxSpectr.SizeMode = PictureBoxSizeMode.StretchImage;
		}
	}
}
