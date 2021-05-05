using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI
{
	public class UserResolverForIdentity : UserResolver
	{
		private readonly IHttpContextAccessor _context;

		public UserResolverForIdentity(IHttpContextAccessor context)
		{
			_context = context;
		}

		public string GetUserName()
		{
			Claim subClaim = null;
			var identity = _context.HttpContext?.User?.Identity;
			if (identity != null)
			{
				subClaim = ((ClaimsIdentity)identity).Claims.FirstOrDefault(c => c.Type == "sub");
			}
			return subClaim != null
				? subClaim.Value
				: string.Empty;
		}
	}
}
