using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceAuth
{
	public partial class fmAuthForm : Form
	{
		private bool Fake = false;

		public fmAuthForm()
		{
			InitializeComponent();
		}

		private void btnVoice_Click(object sender, EventArgs e)
		{
			if (comboBoxLogin.Text == "")
			{
				MessageBox.Show("Введите логин!","Ошибка!",MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			var filePAth = Environment.CurrentDirectory + "\\" + comboBoxLogin.Text + ".lvd";
			if(!File.Exists(filePAth))
			{
				MessageBox.Show("Отсутствуют голосовые данные!","Ошибка!",MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			var dialog = new fmRecordForm();
			dialog.Login = comboBoxLogin.Text;
			dialog.ShowDialog();
			var V = Validator.Load(filePAth);
			Settings Settings = Settings.Load();
			var result = V.Validate(dialog.Mels.ToList());
			if (result < Settings.LevelValidation*100)
				this.DialogResult = System.Windows.Forms.DialogResult.OK;
			else
				this.DialogResult = System.Windows.Forms.DialogResult.No;		
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (VoiceAuth.AuthClient.ValidateUser(comboBoxLogin.Text, maskedTextBoxPassword.Text))
			{
				if (comboBoxLogin.Text == "admin")
					this.DialogResult = System.Windows.Forms.DialogResult.Retry;
				else
					this.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
			else
			{
				this.DialogResult = System.Windows.Forms.DialogResult.No;
			}
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void fmAuthForm_Load(object sender, EventArgs e)
		{
			var users = VoiceAuth.AuthClient.GetUsers();
			foreach (var user in users)
			{
				comboBoxLogin.Items.Add(user);
			}
		}

		private void label1_Click(object sender, EventArgs e)
		{
			Fake = true;
		}
	}
}
