using MetalSoft.Core.Model;
using System;
using System.Security.Cryptography;
using System.Text;

namespace MetalSoft.Core.Security
{
	public class TelosLoginEngine
		: JwtLoginEngine
	{
		private const string privateKey = @"TxhFsrIcmcAboxZtiT50jkK4y2ZWe6Ei";
		private const string publicKey = @"Iw7e9APmgow=";
		private static TripleDESCryptoServiceProvider desCrypto = new TripleDESCryptoServiceProvider();

		public TelosLoginEngine()
		{
			this.SessionExpirationHours = 1;
		}

		protected override void HashPasswordBySideEffect(User user, string newPassword)
		{
			user.HashedPassword = Encode(newPassword, privateKey, publicKey);
			user.Salt = "não se aplica mas é obrigatório";
		}

		protected override bool VerifyPassword(User user, string password)
		{
			string incomingHashedPassword = Encode(password, privateKey, publicKey);
			return user.HashedPassword != null
				&& incomingHashedPassword.Length >= publicKey.Length
				&& user.HashedPassword.StartsWith(incomingHashedPassword);
		}

		private string Encode(string encValue, string privateKey, string publicKey)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(encValue);
			desCrypto.Key = Convert.FromBase64String(privateKey);
			desCrypto.IV = Convert.FromBase64String(publicKey);
			encValue = Convert.ToBase64String(desCrypto.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
			return encValue;
		}

		private string Decode(string decValue, string privateKey, string publicKey)
		{
			byte[] inputBuffer = Convert.FromBase64String(decValue);
			desCrypto.Key = Convert.FromBase64String(privateKey);
			desCrypto.IV = Convert.FromBase64String(publicKey);
			decValue = Encoding.ASCII.GetString(desCrypto.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
			return decValue;
		}
	}
}
