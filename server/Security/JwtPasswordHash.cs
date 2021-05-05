using System;

namespace MetalSoft.Core.Security
{
	public class JwtPasswordHash
	{
		public string Salt { get; private set; }
		
		public string HashedPassword { get; private set; }

		private JwtPasswordHash()
		{
		}

		public static JwtPasswordHash CreateForVerification(string salt, string hashedPassword)
		{
			JwtPasswordHash h = new JwtPasswordHash();
			h.Salt = salt;
			h.HashedPassword = hashedPassword;
			return h;
		}

		public static JwtPasswordHash CreateForHashing()
		{
			JwtPasswordHash h = new JwtPasswordHash();
			return h;
		}

		public bool Verify(string password)
		{
			if (password == null)
				throw new ArgumentNullException("password");
			if (string.IsNullOrWhiteSpace(password))
				throw new ArgumentException("password");

			byte[] saltBytes = Convert.FromBase64String(this.Salt);
			byte[] hashBytes = Convert.FromBase64String(this.HashedPassword);

			using (var hmac = new System.Security.Cryptography.HMACSHA512(saltBytes))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < computedHash.Length; i++)
				{
					if (computedHash[i] != hashBytes[i])
						return false;
				}
			}

			return true;
		}

		public void CreateHash(string password)
		{
			if (password == null)
				throw new ArgumentNullException("password");
			if (string.IsNullOrWhiteSpace(password))
				throw new ArgumentException("password");

			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				this.Salt = Convert.ToBase64String(hmac.Key);
				this.HashedPassword = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
			}
		}
	}
}
