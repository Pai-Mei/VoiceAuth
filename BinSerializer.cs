using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace VoiceAuth
{
	public class BinSerializer
	{
		public BinSerializer()
		{

		}

		public void SerializeObject<T>(string fileName, T objToSerialize)
		{
			try
			{
				using (FileStream fstream = File.Open(fileName, FileMode.Create))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(fstream, objToSerialize);
				}
			}
			catch
			{
			}
		}

		public T DeserializeObject<T>(string fileName)
		{
			try
			{
				T objToSerialize = default(T);
				using (FileStream fstream = File.Open(fileName, FileMode.Open))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					var obj = (T)binaryFormatter.Deserialize(fstream);
					if (obj is T)
						objToSerialize = (T)obj;
				}
				return objToSerialize;
			}
			catch
			{
				return default(T);
			}
		}

	}
}
