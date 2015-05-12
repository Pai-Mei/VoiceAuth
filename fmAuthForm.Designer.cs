namespace VoiceAuth
{
	partial class fmAuthForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.maskedTextBoxPassword = new System.Windows.Forms.MaskedTextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnVoice = new System.Windows.Forms.Button();
			this.comboBoxLogin = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Логин:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Пароль:";
			// 
			// maskedTextBoxPassword
			// 
			this.maskedTextBoxPassword.Location = new System.Drawing.Point(66, 32);
			this.maskedTextBoxPassword.Name = "maskedTextBoxPassword";
			this.maskedTextBoxPassword.PasswordChar = '*';
			this.maskedTextBoxPassword.Size = new System.Drawing.Size(178, 20);
			this.maskedTextBoxPassword.TabIndex = 3;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(168, 58);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(87, 58);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "Вход";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnVoice
			// 
			this.btnVoice.Location = new System.Drawing.Point(6, 58);
			this.btnVoice.Name = "btnVoice";
			this.btnVoice.Size = new System.Drawing.Size(75, 23);
			this.btnVoice.TabIndex = 6;
			this.btnVoice.Text = "Голос";
			this.btnVoice.UseVisualStyleBackColor = true;
			this.btnVoice.Click += new System.EventHandler(this.btnVoice_Click);
			// 
			// comboBoxLogin
			// 
			this.comboBoxLogin.FormattingEnabled = true;
			this.comboBoxLogin.Location = new System.Drawing.Point(66, 5);
			this.comboBoxLogin.Name = "comboBoxLogin";
			this.comboBoxLogin.Size = new System.Drawing.Size(177, 21);
			this.comboBoxLogin.TabIndex = 7;
			// 
			// fmAuthForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(255, 89);
			this.Controls.Add(this.comboBoxLogin);
			this.Controls.Add(this.btnVoice);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.maskedTextBoxPassword);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "fmAuthForm";
			this.Text = "Авторизация";
			this.Load += new System.EventHandler(this.fmAuthForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.MaskedTextBox maskedTextBoxPassword;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnVoice;
		private System.Windows.Forms.ComboBox comboBoxLogin;
	}
}