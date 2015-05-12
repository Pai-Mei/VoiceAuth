using System;
using System.Security.Permissions;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio;
using NAudio.Wave;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.Security.Principal;
using AForge;
using System.Drawing;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace VoiceAuth
{
	public static class VoiceAuth
    {
		static internal Settings m_AudioSettings;

		static internal Authentification AuthClient;

		static internal Settings AudioSettings
		{
			get 
			{
				if (m_AudioSettings == null)
					m_AudioSettings = new Settings();
				return m_AudioSettings;
			}
		}

		static public Boolean Login()
		{
			
			try {
				AuthClient = new Authentification();
				var fmAuth = new fmAuthForm();
				var result = fmAuth.ShowDialog();
				if (result == DialogResult.Retry)
				{
					fmAdminSettings adminForm = new fmAdminSettings();
					adminForm.ShowDialog();
					return true;
				}
				return result == DialogResult.OK;
			} 
			catch(Exception e)
			{
				MessageBox.Show(e.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			} 
		}
	}

	

	

	   
}
