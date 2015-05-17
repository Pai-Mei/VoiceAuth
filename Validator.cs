using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceAuth
{
	public class Validator
	{
		public List<List<Double>> Mels { get; set; }

		public Validator()
		{
			Mels = new List<List<double>>();
		}

		public void AddMel(List<Double> mels)
		{
			Mels.Add(mels);
		}

		public Double Validate(List<Double> mels)
		{
			var result = 0.0;
			for (int i = 0; i < Mels.Count; i++)
			{
				var d = 0.0;
				for (int j = 1; j < Mels[i].Count; j++)
					d += Math.Abs(Mels[i][j] - mels[j]);
				if (i == 0)
					result = d;
				else
					result = (result + d) / 2;
			}

			return result;
		}

		public void Save(string filePath)
		{
			Xml.Xml.Save(filePath, this, typeof(Validator));
		}

		public static Validator Load(string filePath)
		{
			return Xml.Xml.Load(filePath, typeof(Validator)) as Validator;
		}
	}
}
