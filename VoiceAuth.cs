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
		static internal AudioSettings m_AudioSettings;

		static internal Authentification AuthClient;

		static internal AudioSettings AudioSettings
		{
			get 
			{
				if (m_AudioSettings == null)
					m_AudioSettings = new AudioSettings();
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

	internal class AudioSettings
	{
		public int[] SampleRates = new int[] { 8000, 16000, 21000, 44100, 48000, 96000, 128000, 320000 };

		public int BufferMilliseconds { get; set; }
		public int DeviceNumber { get; set; }
		public WaveFormat WaveFormat { get; set; }
		public Double Duration { get; set; }
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

		public AudioRecorder (AudioSettings settings)
		{
			m_Settings = settings;
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

	internal class Authentification : IDisposable
	{
		private const string filePath = "\\auth.xml";

		public class LoginData
		{
			public String Login { get; set; }
			public String Hash { get; set; }

			public LoginData()
			{

			}

			public LoginData(string login, string hash)
			{
				this.Login = login;
				this.Hash = hash;
			}
		}

		private Dictionary<string, string> data;

		public Authentification ()
		{
			Load();
		}

		private void Load()
		{
			var listdata = Xml.Xml.Load(Environment.CurrentDirectory + filePath, typeof(List<LoginData>)) as List<LoginData>;
			if (listdata == null || listdata.Count == 0)
			{
				listdata = new List<LoginData>();
				string hash = "";
				using (MD5 md5Hash = MD5.Create())
					hash = GetMd5Hash(md5Hash, "admin");
				listdata.Add(new LoginData("admin", hash));
			}
			data = new Dictionary<string, string>(listdata.Count);
			foreach (var item in listdata)
				data.Add(item.Login, item.Hash);
			Save();
		}

		public void Save()
		{
			var listData = new List<LoginData>();
			foreach (var item in data)
				listData.Add(new LoginData(item.Key, item.Value));
			Xml.Xml.Save(Environment.CurrentDirectory + filePath, listData, typeof(List<LoginData>));
		}

		public List<string> GetUsers()
		{
			return data.Keys.ToList<string>();
		}

		public void AddUser(string login, string password)
		{
			string hash = "";
			using (MD5 md5Hash = MD5.Create())
				hash = GetMd5Hash(md5Hash, password);
			if (!data.ContainsKey(login))
				data.Add(login, hash);
			else
				data[login] = hash;
			Save();
		}

		public void RemoveUser(string login)
		{
			data.Remove(login);
			Save();
		}

		public bool ValidateUser(string login, string password)
		{
			if (!data.ContainsKey(login))
				return false;
			string hash = "";
			using (MD5 md5Hash = MD5.Create())
				hash = GetMd5Hash(md5Hash, password);
			var trueHash = data[login];
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
			Save();
		}
	}

	internal class VoiceAnalys
	{

	}
}
