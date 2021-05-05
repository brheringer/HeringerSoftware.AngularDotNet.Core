using IdentityModel.AspNetCore.OAuth2Introspection;
using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;
using HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests
{
    public class MockStartup : Startup
    {
		public MockStartup(IConfiguration configuration) : base(configuration)
        {
        }

		protected override void ConfigureAuthenticationServices(IServiceCollection services, AppSettings appSettings)
		{
			//BaseControllerTest mocks the authentication. Furthermore, it must avoid the superclass implementation. So, it is empty.
		}

		protected override List<Assembly> GetApplicationParts()
		{
			return new List<Assembly>() { typeof(HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks.MockController).Assembly };
		}

		protected override void ConfigureUserResolver(IServiceCollection services)
		{
			services.AddScoped<UserResolver, MockUserResolver>();
		}

		protected override void ConfigureDbContext(IServiceCollection services, AppSettings appSettings)
		{
			services.AddDbContext<MockDbContext>(options => options.UseSqlServer(appSettings.ConnectionString)); //TODO mysql, oracle...
		}

		protected override void ConfigureDaoFactory(IServiceCollection services, AppSettings appSettings)
		{
			services.AddSingleton<MockRepositoryFactory>();
		}

	}
}
