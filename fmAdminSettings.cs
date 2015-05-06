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

namespace VoiceAuth
{
	public partial class fmAdminSettings : Form
	{
		public fmAdminSettings()
		{
			InitializeComponent();
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
			textBoxDuration.Text = VoiceAuth.AudioSettings.Duration.ToString("0.00");
			comboBoxDevice.Items.Clear();
			int waveInDevices = WaveIn.DeviceCount;
			for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
			{
				WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
				comboBoxDevice.Items.Add(deviceInfo.ProductName);
			} 
			comboBoxDevice.Select(0, 1);
		}

		private void SaveAudioSettings()
		{

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
	}
}
