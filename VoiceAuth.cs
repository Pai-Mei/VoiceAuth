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
		private static readonly string SettingsFilePath = Environment.CurrentDirectory + "\\AudioSettings.xml";

		public int BufferMilliseconds { get; set; }
		public int DeviceNumber { get; set; }
		public WaveFormat WaveFormat { get; set; }
		public Double Duration { get; set; }
		public AudioSettings()
		{
			BufferMilliseconds = 100;
			DeviceNumber = 0;
			WaveFormat = new WaveFormat(16000, 16, 1);
			Duration = 10;
		}

		public static AudioSettings Load()
		{
			return Xml.Xml.Load(SettingsFilePath, typeof(AudioSettings)) as AudioSettings;
		}

		public void Save()
		{
			Xml.Xml.Save(SettingsFilePath, this, typeof(AudioSettings));
		}
	}

	internal class AudioRecorder
	{
		private byte[] m_buffer;
		private byte[] tmpBuffer;
		private WaveIn m_Input;
		private AudioSettings m_Settings;
		private VoiceAnalys m_Analisator;

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

		public void CreateAudioID()
		{
			var data = Analis(m_buffer, 1 << 14);
			var mels = MelFiltering(data, 20);
		}

		public Double[] Analis(byte[] buffer, int frameSize)
		{
			var lenght = buffer.Length / 2;
			var fftBuffer = new AForge.Math.Complex[lenght];
			for (int i = 0; i < buffer.Length / 2; i++)
				fftBuffer[i].Re = (Double)BitConverter.ToInt16(buffer, i * 2) / Int16.MaxValue;

			var offset = 0;
			var windowWidth = frameSize;
			var result = new Double[windowWidth/2];
			var localBuffer = new AForge.Math.Complex[windowWidth];
			while (offset + windowWidth < lenght)
			{
				for (int i = 0; i < windowWidth; i++)
					localBuffer[i].Re = fftBuffer[i + offset].Re*(0.54 - 0.46*Math.Cos(2*Math.PI*i/windowWidth));
				AForge.Math.FourierTransform.FFT(localBuffer, AForge.Math.FourierTransform.Direction.Forward);
				for (int i = 0; i < windowWidth/2; i++)
					result[i] += localBuffer[i].Magnitude;
				offset += windowWidth/2;
			}
			for (int i = 0; i < windowWidth / 2; i++)
				result[i] /= windowWidth;
			var norm = 1 / result.Max();
			for (int i = 0; i < result.Length; i++)
				result[i] *= norm;
			return result;
		}

		public Double[] MelFiltering(Double[] spectr, int numberFilters)
		{
			var adding = 200 * 1024 / 16000;
			var result = new Double[numberFilters];
			var indexes = new Int32[numberFilters];
			var Base = Math.Exp(Math.Log(spectr.Length) / (numberFilters + adding));

			for (int i = 0; i < numberFilters; i++)
			{
				indexes[i] = (int)Math.Pow(Base, i+adding);
			}

			var index = 1; 
			for (int i = indexes[0]; i < spectr.Length; i++)
			{
				if (index > indexes.Length - 1)
					break;
				result[index] += spectr[i] * (i - indexes[index - 1]) / (indexes[index] - indexes[index - 1]);
				result[index-1] += spectr[i] * (indexes[index] - i) / (indexes[index] - indexes[index - 1]);
				if (i > indexes[index]) index++;
			}
			var norm = 1/result.Max();
			for (int i = 0; i < result.Length; i++)
				result[i] *= norm;  
			return result;
		}

		private void waveIn_RecordingStopped(object sender, EventArgs e)
		{
			CreateAudioID();
		}

		private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
		{
			var offset = m_buffer.Length;
			tmpBuffer = e.Buffer;
			Array.Resize(ref m_buffer, m_buffer.Length + e.Buffer.Length);
			for (int index = 0; index < e.Buffer.Length; index++)
				m_buffer[index+offset] = e.Buffer[index];
		}
		
		internal Bitmap Visulization(System.Drawing.Size size)
		{
			var result = new Bitmap(size.Width, size.Height);
			var canvas = Graphics.FromImage(result);
			canvas.FillRectangle(new SolidBrush(Color.Black), 0, 0, size.Width, size.Height);
			if (tmpBuffer != null)
			{
				var data = Analis(tmpBuffer, 1024);
				var startIndex = 110 * 1024 / 16000;
				var maxValue = data.Max();
				var length = data.Length - startIndex;
				var points = new PointF[length];
				for (int i = 0; i < length; i++)
				{
					points[i].X = i * size.Width / length;
					points[i].Y = size.Height - (float)(data[startIndex + i] * size.Height / maxValue);
				}
				canvas.DrawLines(new Pen(Color.White), points);
			}
			return result;
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
