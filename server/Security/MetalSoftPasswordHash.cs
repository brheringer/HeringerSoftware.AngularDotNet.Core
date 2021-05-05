using System;

namespace MetalSoft.Core.Security
{
	/// <summary>
	/// Essa classe foi criada para se acoplar à classe PasswordHash sem modificá-la,
	/// para não violar os termos da licença.
	/// </summary>
	public class MetalSoftPasswordHash
    {
		private const char SPLIT_CHAR = ':';

		public string FullHashedPassword { get; private set; }
		
		public string Salt { get; private set; }
		
		public string HashedPassword { get; private set; }

		private MetalSoftPasswordHash()
		{
		}

		public bool Verify(string password)
		{
			return PasswordHash.ValidatePassword(password, this.FullHashedPassword);
		}

		public void CreateHash(string password)
		{
			this.FullHashedPassword = PasswordHash.CreateHash(password);
			Split();
		}

		private void Split()
		{
			if (FullHashedPassword == null)
				throw new InvalidOperationException();
			
			string[] tokens = FullHashedPassword.Split(SPLIT_CHAR);

			if (tokens.Length != 3)
				throw new ArgumentException(FullHashedPassword);

			//this.Iterations = tokens[0];
			this.Salt = tokens[1];
			this.HashedPassword = tokens[2];
		}

		public static MetalSoftPasswordHash CreateForVerification(string salt, string hashedPassword)
		{
            MetalSoftPasswordHash h = new MetalSoftPasswordHash();
			h.Salt = salt;
			h.HashedPassword = hashedPassword;
			h.FullHashedPassword = string.Join(SPLIT_CHAR.ToString(), PasswordHash.PBKDF2_ITERATIONS, h.Salt, h.HashedPassword);
			return h;
		}

		public static MetalSoftPasswordHash CreateForHashing()
		{
            MetalSoftPasswordHash h = new MetalSoftPasswordHash();
			return h;
		}

	}
}
