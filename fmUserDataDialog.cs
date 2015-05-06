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
	public partial class fmUserDataDialog : Form
	{
		public fmUserDataDialog(string login):this()
		{
			textBoxLogin.Text = login;
		}

		public fmUserDataDialog()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (maskedTextBoxPassword.Text.Length > 3)
				VoiceAuth.AuthClient.AddUser(textBoxLogin.Text, maskedTextBoxPassword.Text);
			else
			{
				MessageBox.Show("Пароль должен быть не менее 3 символов.");
				DialogResult = System.Windows.Forms.DialogResult.None;
			}
		}
	}
}
