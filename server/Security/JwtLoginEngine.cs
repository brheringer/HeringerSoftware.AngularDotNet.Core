using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MetalSoft.Core.Model;
using Microsoft.IdentityModel.Tokens;

namespace MetalSoft.Core.Security
{
	public class JwtLoginEngine
		: LoginEngine
	{
		public string Secret { get; set; }
		public int SessionExpirationHours { get; set; }

		public JwtLoginEngine()
		{
			this.SessionExpirationHours = 1;
		}

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
			if (user != null && user.Banished)
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

		protected virtual void HashPasswordBySideEffect(User user, string newPassword)
		{
			JwtPasswordHash hasher = JwtPasswordHash.CreateForHashing();
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

		protected virtual bool VerifyPassword(User user, string password)
		{
			if (user == null)
				return false;

			return JwtPasswordHash
                .CreateForVerification(user.Salt, user.HashedPassword)
				.Verify(password);
		}

		private UserSession CreateUserSession(User user)
		{
			UserSession s = new UserSession();
			s.SessionId = GenerateSessionId(user.UserName);
			s.UserLoggedIn = user;
			return s;
		}

		private string GenerateSessionId(string username)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = System.Text.Encoding.ASCII.GetBytes(this.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, username)
					//new Claim(ClaimTypes.Name, user.Id.ToString())
				}),
				Expires = DateTime.UtcNow.AddHours(this.SessionExpirationHours),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			string tokenString = tokenHandler.WriteToken(token);
			return tokenString;
		}
	}
}
