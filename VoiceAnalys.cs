using AForge.Neuro;
using AForge.Neuro.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceAuth
{
	public class VoiceAnalys
	{
		public class LearninEventArgs : EventArgs
		{
			public Double Error { get; set; }
			public Int32 Epoch { get; set; }
		}

		private ActivationNetwork m_Network;

		public void CreateNN(int numberMels, double error, int maxCount, List<Double[]> melsData)
		{
			m_Network = new ActivationNetwork(new SigmoidFunction(2), melsData.First().Length, melsData.First().Length, 1);
			double[][] input = new double[melsData.Count*2][]; 
			double[][] output = new double[melsData.Count*2][];
			for (int i = 0; i < melsData.Count; i++)
			{
				input[i] = melsData[i];
				output[i] = new Double[] { 1.0 };
			}
			Random rnd = new Random();
			for (int i = melsData.Count; i < melsData.Count * 2; i++)
			{
				input[i] = new Double[melsData.First().Length];
				for (int j = 0; j < melsData.First().Length; j++)
					input[i][j] = rnd.NextDouble();
				output[i] = new Double[] { -1.0 };
			}
			var teacher = new BackPropagationLearning(m_Network);
			teacher.LearningRate = 1e-5;
			double stepError = double.MaxValue;
			int index = 0;
			while (error < stepError && index < maxCount)
			{
				stepError = teacher.RunEpoch(input, output);
				Learning(new LearninEventArgs() {  Error = stepError, Epoch = index});
				index++;
			}
		}

		public Double Validate(Double[] melsData)
		{
			var result = m_Network.Compute(melsData);
			return result[0];
		}

		public void LoadNetwork(String filePath)
		{
			m_Network = new BinSerializer().DeserializeObject<ActivationNetwork>(filePath);
		}

		public void SaveNetwork(String filePath)
		{
			new BinSerializer().SerializeObject(filePath, m_Network);
		}
		
		#region events

		event EventHandler<LearninEventArgs> OnLearning;

		protected virtual void Learning(LearninEventArgs args)
		{
			if (OnLearning != null)
				OnLearning(this, args);
		}

		#endregion
	}  
}
