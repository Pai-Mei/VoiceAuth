using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VoiceAuth
{
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

		public Authentification()
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
}
