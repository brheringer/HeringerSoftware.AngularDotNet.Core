using System;
using MetalSoft.Core.Model;

namespace MetalSoft.Core.Security
{
	public class MetalSoftLoginEngine
		: LoginEngine
	{
		public UserSession DoLogoff()
		{
			return new UserSession();
		}

		public UserSession DoLogin(User user, string password)
		{
			UserBanishedSieve(user);
			PasswordSieve(user, password);
			UserSession s = CreateUserSession(user);
			return s;
		}

		private void UserBanishedSieve(User user)
		{
			if (user == null)
				throw new ArgumentNullException();

			if (user.Banished)
			{
				throw new MetalSoftSecurityException("User banished: " + user);
			}
		}

		public void SetPasswordBySideEffect(User user, string newPassword)
		{
			HashPasswordBySideEffect(user, newPassword);
		}

		public void ChangePasswordBySideEffect(User user, string oldPassword, string newPassword)
		{
			PasswordSieve(user, oldPassword);
			HashPasswordBySideEffect(user, newPassword);
		}

		private void HashPasswordBySideEffect(User user, string newPassword)
		{
            MetalSoftPasswordHash hasher = MetalSoftPasswordHash.CreateForHashing();
			hasher.CreateHash(newPassword);
			user.Salt = hasher.Salt;
			user.HashedPassword = hasher.HashedPassword;
		}

		private void PasswordSieve(User user, string password)
		{
			if (!VerifyPassword(user, password))
			{
				throw new MetalSoftSecurityException("Invalid password or user.");
			}
		}

		private bool VerifyPassword(User user, string password)
		{
			if (user == null)
				throw new ArgumentNullException();

			return MetalSoftPasswordHash
                .CreateForVerification(user.Salt, user.HashedPassword)
				.Verify(password);
		}

		private UserSession CreateUserSession(User user)
		{
			UserSession s = new UserSession();
			s.SessionId = GenerateSessionId();
			s.UserLoggedIn = user;
			return s;
		}

		private string GenerateSessionId()
		{
			return Guid.NewGuid().ToString();
			//note: NewGuid is thread-safe
		}
	}
}
