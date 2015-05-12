using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceAuth
{
	public class Settings
	{
		private static readonly string SettingsFilePath = Environment.CurrentDirectory + "\\AudioSettings.xml";

		public int BufferMilliseconds { get; set; }
		public int DeviceNumber { get; set; }
		public WaveFormat WaveFormat { get; set; }
		public int FrameSize { get; set; }
		public int MelsNumber { get; set; }
		public int RecCount { get; set;}
		public int MaxLearningCount { get; set; }
		public Double LearningError { get; set; }
		public Settings()
		{
			BufferMilliseconds = 100;
			DeviceNumber = 0;
			WaveFormat = new WaveFormat(16000, 16, 1);
			FrameSize = 1 << 14;
			MelsNumber = 20;
			RecCount = 3;
			MaxLearningCount = 100;
			LearningError = 0.1;
		}

		public static Settings Load()
		{
			Settings sets = null;
			try{
				sets = Xml.Xml.Load(SettingsFilePath, typeof(Settings)) as Settings;
			} 
			finally
			{
				if(sets == null)
					sets = new Settings();
			}
			return sets;

		}

		public void Save()
		{
			Xml.Xml.Save(SettingsFilePath, this, typeof(Settings));
		}
	}

	internal class AudioRecorder
	{
		private byte[] m_buffer;
		private byte[] tmpBuffer;
		private WaveIn m_Input;
		private Settings m_Settings;
		private VoiceAnalys m_Analisator;

		public AudioRecorder(Settings settings)
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

		public Double[] GetMels()
		{
			return GetMels(m_buffer, m_Settings.FrameSize, m_Settings.MelsNumber);
		}

		private Double[] GetMels(byte[] buffer, int frameSize, int melsNumbers)
		{
			var data = Analis(buffer, frameSize);
			var mels = MelFiltering(data, melsNumbers);
			return mels;
		}

		private Double[] Analis(byte[] buffer, int frameSize)
		{
			var lenght = buffer.Length / 2;
			var fftBuffer = new AForge.Math.Complex[lenght];
			for (int i = 0; i < buffer.Length / 2; i++)
				fftBuffer[i].Re = (Double)BitConverter.ToInt16(buffer, i * 2) / Int16.MaxValue;

			var offset = 0;
			var windowWidth = frameSize;
			var result = new Double[windowWidth / 2];
			var localBuffer = new AForge.Math.Complex[windowWidth];
			while (offset + windowWidth < lenght)
			{
				for (int i = 0; i < windowWidth; i++)
					localBuffer[i].Re = fftBuffer[i + offset].Re * (0.54 - 0.46 * Math.Cos(2 * Math.PI * i / windowWidth));
				AForge.Math.FourierTransform.FFT(localBuffer, AForge.Math.FourierTransform.Direction.Forward);
				for (int i = 0; i < windowWidth / 2; i++)
					result[i] += localBuffer[i].Magnitude;
				offset += windowWidth / 2;
			}
			for (int i = 0; i < windowWidth / 2; i++)
				result[i] /= windowWidth;
			var norm = 1 / result.Max();
			for (int i = 0; i < result.Length; i++)
				result[i] *= norm;
			return result;
		}

		private Double[] MelFiltering(Double[] spectr, int numberFilters)
		{
			var adding = 200 * 1024 / 16000;
			var result = new Double[numberFilters];
			var indexes = new Int32[numberFilters];
			var Base = Math.Exp(Math.Log(spectr.Length) / (numberFilters + adding));

			for (int i = 0; i < numberFilters; i++)
			{
				indexes[i] = (int)Math.Pow(Base, i + adding);
			}

			var index = 1;
			for (int i = indexes[0]; i < spectr.Length; i++)
			{
				if (index > indexes.Length - 1)
					break;
				result[index] += spectr[i] * (i - indexes[index - 1]) / (indexes[index] - indexes[index - 1]);
				result[index - 1] += spectr[i] * (indexes[index] - i) / (indexes[index] - indexes[index - 1]);
				if (i > indexes[index]) index++;
			}
			var norm = 1 / result.Max();
			for (int i = 0; i < result.Length; i++)
				result[i] *= norm;
			return result;
		}

		private void waveIn_RecordingStopped(object sender, EventArgs e)
		{

		}

		private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
		{
			var offset = m_buffer.Length;
			tmpBuffer = e.Buffer;
			Array.Resize(ref m_buffer, m_buffer.Length + e.Buffer.Length);
			for (int index = 0; index < e.Buffer.Length; index++)
				m_buffer[index + offset] = e.Buffer[index];
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

		internal bool Auth(string Login)
		{
			throw new NotImplementedException();
		}
	}
}
