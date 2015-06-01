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
		public Double LevelValidation { get; set; }
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
			LevelValidation = 0.5;
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
		private const int freq = 16000;

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
			var mels = MelFiltering(data, melsNumbers, frameSize);
			return mels;
		}

		private Double[] Analis(byte[] buffer, int frameSize)
		{
			var lenght = buffer.Length / 2;
			var fftBuffer = new AForge.Math.Complex[lenght];
			var maxValue = Double.MinValue;
			for (int i = 0; i < buffer.Length / 2; i++)
			{
				fftBuffer[i].Re = (Double)BitConverter.ToInt16(buffer, i * 2) / Int16.MaxValue;
				if (fftBuffer[i].Re > maxValue)
					maxValue = fftBuffer[i].Re;
			}

			for (int i = 0; i < fftBuffer.Length; i++)
				fftBuffer[i].Re /= maxValue;



			var offset = 0;
			var windowWidth = frameSize;
			
			var localBuffer = new AForge.Math.Complex[windowWidth];
			var width = windowWidth / 2;
			var result = new Double[width];
			while (offset + windowWidth < lenght)
			{
				for (int i = 0; i < windowWidth; i++)
					localBuffer[i].Re = fftBuffer[i + offset].Re *(0.54 - 0.46 * Math.Cos(2 * Math.PI * i / windowWidth));
				AForge.Math.FourierTransform.FFT(localBuffer, AForge.Math.FourierTransform.Direction.Forward);
				for (int i = 0; i < width; i++)
					result[i] += localBuffer[i].Magnitude / windowWidth;
				offset += windowWidth / 2;
			}			
			//var norm = 1 / result.Max();
			//for (int i = 0; i < result.Length; i++)
			//	result[i] *= norm;
			return result;
		}

		private Double ToMel(Double Hz)
		{
			return 1127 * Math.Log(1 + Hz/700);
		}
		private Double ToHz(Double Mel)
		{
			return 700 * (Math.Exp(Mel / 1125) - 1);
		}

		private Double[] MelFiltering(Double[] spectr, int numberFilters, int frameSize)
		{

			var result = new Double[numberFilters];
			var indexes = new Int32[numberFilters];
			
			var stepMel = ToMel(spectr.Length) / (numberFilters+1);
			var curMel = stepMel;
			var counter = 0;
			for (int i = 0; i < numberFilters; i++)
			{
				indexes[counter++] = (int)ToHz(curMel);
				curMel += stepMel;
			}

			var index = 1;

			for (int i = 0; i < indexes.Length; i++)
			{
				result[i] = 0;
				var startIndex = i > 0 ? indexes[i-1] : 0;
				var endIndex = i < indexes.Length - 1 ? indexes[i+1] : spectr.Length - 1;
				for (int j = startIndex; j < indexes[i]; j++)
				{
					result[i] += spectr[j] * spectr[j] * (j - startIndex) / (indexes[i] - startIndex);
				}
				for (int j = indexes[i]; j < endIndex; j++)
				{
					result[i] += spectr[j] * spectr[j] * (endIndex - j) / (endIndex - indexes[i]);
				}
 				result[i] = Math.Log(result[i]);
			}
			var M = result.Length;
			var C = new Double[M];
			for (int i = 0; i < M; i++)
			{
				for (int j = 0; j < M; j++)
				{
					C[i] += result[j] * Math.Cos(Math.PI * i * (j + 0.5) / M);
				}
			}
			
			return C;
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
				var data = Analis(tmpBuffer, 4096);
				var f500Index = 500 * 4096 / freq;
				var f1kIndex = 1000 * 4096 / freq;
				var f2kIndex = 2000 * 4096 / freq;
				var f4kIndex = 4000 * 4096 / freq;
				var startIndex = 0;
				var maxValue = data.Max();
				var length = data.Length - startIndex;
				var points = new PointF[length];
				for (int i = 1; i < length; i++)
				{
					points[i].X = (int)(Math.Log10(i) * size.Width / Math.Log10(length));
					points[i].Y = size.Height - (float)(data[startIndex + i] * size.Height / maxValue);
				}
				canvas.DrawLines(new Pen(Color.White), points);
			}
			return result;
		}
	}
}
