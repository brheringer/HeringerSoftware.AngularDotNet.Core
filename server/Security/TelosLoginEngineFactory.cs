using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MetalSoft.Core.Model;
using Microsoft.IdentityModel.Tokens;

namespace MetalSoft.Core.Security
{
	public class TelosLoginEngineFactory : LoginEngineFactory
	{
		public string Secret { get; set; }
		public Dictionary<string, string> Parameters { get; set; }

		public LoginEngine Create()
		{
			int expiration = 0;
			if(this.Parameters != null && this.Parameters.ContainsKey("SessionExpirationHours"))
				int.TryParse(this.Parameters["SessionExpirationHours"], out expiration);

			return new TelosLoginEngine()
			{
				Secret = this.Secret,
				SessionExpirationHours = expiration
			};
		}
	}
}
