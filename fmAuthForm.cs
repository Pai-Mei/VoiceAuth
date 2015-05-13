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
			var filePAth = Environment.CurrentDirectory + "\\" + comboBoxLogin.Text + ".nnd";
			if(!File.Exists(filePAth))
			{
				MessageBox.Show("Отсутствуют голосовые данные!","Ошибка!",MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			var dialog = new fmRecordForm();
			dialog.Login = comboBoxLogin.Text;
			dialog.ShowDialog();
			var VA = new VoiceAnalys();
			VA.LoadNetwork(filePAth);
			var result = VA.Validate(dialog.Mels);
			if (result > 0.8)
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
	}
}
