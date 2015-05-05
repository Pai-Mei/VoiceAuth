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

namespace VoiceAuth
{
	public static class VoiceAuth
    {
		static public Authentification AuthClient;
		
		static public Boolean Login()
		{
			try {
				AuthClient = new Authentification();
				var fmAuth = new fmAuthForm();
				return fmAuth.ShowDialog() == System.Windows.Forms.DialogResult.OK;				 
			} 
			catch(Exception e)
			{
				MessageBox.Show(e.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}
	}

	public class AudioSettings
	{
		public int BufferMilliseconds { get; set; }
		public int DeviceNumber { get; set; }
		public WaveFormat WaveFormat { get; set; }

		public AudioSettings()
		{
			BufferMilliseconds = 100;
			DeviceNumber = 0;
			WaveFormat = new WaveFormat(16000, 1);
		}
	}

	internal class AudioRecorder
	{
		private byte[] m_buffer;
		private WaveIn m_Input;
		private AudioSettings m_Settings;

		public AudioRecorder ()
		{
			m_Settings = new AudioSettings();
			m_buffer = new byte[0];
		}

		public void StartRecord()
		{
			m_Input = new WaveIn();
            //Дефолтное устройство для записи (если оно имеется)
            //встроенный микрофон ноутбука имеет номер 0
            m_Input.DeviceNumber = m_Settings.DeviceNumber;
			m_Input.BufferMilliseconds = m_Settings.BufferMilliseconds;
           //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
            m_Input.DataAvailable += waveIn_DataAvailable;
            //Прикрепляем обработчик завершения записи
            m_Input.RecordingStopped += waveIn_RecordingStopped;
            //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
            m_Input.WaveFormat = m_Settings.WaveFormat;
            //Начало записи
            m_Input.StartRecording();
		}

		public void StopRecord()
		{
			m_Input.StopRecording();
		}

		private void Visualization(byte[] buffer)
		{

		}

		private void waveIn_RecordingStopped(object sender, EventArgs e)
		{
 			
		}

		private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
		{
 			m_buffer.Concat(e.Buffer);
			Visualization(e.Buffer);
		}

		

		
	}

	public class Authentification : IDisposable
	{
		private RegistryKey key;

		public Authentification ()
		{
			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			RegistryAccessRule accessRule = new RegistryAccessRule(	identity.User, 
																	RegistryRights.FullControl, 
																	AccessControlType.Allow);
			RegistrySecurity regSecurity = new RegistrySecurity();
			regSecurity.SetAccessRule(accessRule);

			key = Registry.CurrentUser.OpenSubKey("Software");
			key.SetAccessControl(regSecurity);
			key.CreateSubKey("Authentificator");
			if(key.GetValue("admin", null) == null)
			{
				string hash = "";
				using (MD5 md5Hash = MD5.Create())
					hash = GetMd5Hash(md5Hash, "admin");
				key.SetValue("admin", hash);
			}
		}
		
		public List<string> GetUsers()
		{
			return key.GetValueNames().ToList();
		}

		public void AddUser(string login, string password)
		{
			string hash = "";
			using (MD5 md5Hash = MD5.Create())
				hash = GetMd5Hash(md5Hash, password);
			key.SetValue(login, hash, RegistryValueKind.String);
		}

		public void RemoveUser(string login)
		{
			key.DeleteValue(login);
		}

		public bool ValidateUser(string login, string password)
		{
			string hash = "";
			using (MD5 md5Hash = MD5.Create())
				hash = GetMd5Hash(md5Hash, password);
			var trueHash = key.GetValue(login, "");
			return (trueHash == hash);			
		}

		private string GetMd5Hash(MD5 md5Hash, string input)
		{

			// Convert the input string to a byte array and compute the hash.
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}

		public void Dispose()
		{
			key.Dispose();
		}
	}
}
