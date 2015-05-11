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
	public partial class fmAuthForm : Form
	{
		public fmAuthForm()
		{
			InitializeComponent();
		}

		private void btnVoice_Click(object sender, EventArgs e)
		{
			var dialog = new fmRecordForm();
			this.DialogResult = dialog.ShowDialog();
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
