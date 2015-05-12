using AForge.Neuro;
using AForge.Neuro.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceAuth
{
	internal class VoiceAnalys
	{
		public class LearninEventArgs : EventArgs
		{
			public Double Error { get; set; }
			public Int32 Epoch { get; set; }
		}

		private ActivationNetwork m_Network;

		public void CreateNN(int numberMels, double error, int maxCount, List<Double[]> melsData)
		{
			m_Network = new ActivationNetwork(new SigmoidFunction(), numberMels, 1);
			double[][] input = new double[melsData.Count][];
			double[][] output = new double[melsData.Count][];
			for (int i = 0; i < melsData.Count; i++)
			{
				input[i] = melsData[i];
				output[i] = new Double[] { 1.0 };
			}
			var teacher = new PerceptronLearning(m_Network);
			teacher.LearningRate = 0.1;
			double stepError = 1;
			int index = 0;
			while (error > stepError || index < maxCount)
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
			new BinSerializer().SerializeObject(filePath, m_Network);
		}

		public void SaveNetwork(String filePath)
		{
			m_Network = new BinSerializer().DeserializeObject<ActivationNetwork>(filePath);
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
