using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	public class MockIdentityStartup
	{
		public MockIdentityStartup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddDefaultIdentity<IdentityServerUser>()
			//	.AddDefaultTokenProviders();
			IdentityModelEventSource.ShowPII = true;

			services.AddMvc();

			services.AddIdentityServer()
				.AddDeveloperSigningCredential() //which is for signing the tokens
				.AddInMemoryApiScopes(GetApiResources()) //covers which APIs are allowed to use this Auth server
				.AddInMemoryClients(GetClients()) //covers which clients are allowed to use this Auth server
				.AddTestUsers(GetTestUsers());
		}

		private static IEnumerable<Client> GetClients()
		{
			return new List<Client>
			{
				new Client
				{
					ClientId = "core-api",
					AllowedGrantTypes = GrantTypes.ClientCredentials,
					ClientSecrets = { new Secret("core-api-as-a-client-secret".Sha256()) },
					AllowedScopes = {
						"core-api"
					}
				},
				new Client
				{
					ClientId = "unit-test",
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
					ClientSecrets = { new Secret("unit-test-as-a-client-secret".Sha256()) },
					AllowedScopes = {
						"core-api"
					}
				}
			};
		}

		private static IEnumerable<ApiScope> GetApiResources()
		{
			return new List<ApiScope>
			{
				new ApiScope("core-api", "Core API")
			};
		}

		private static List<TestUser> GetTestUsers()
		{
			return new List<TestUser>
			{
				new TestUser() { SubjectId = "bob", Username = "bob", Password = "bob" },
				new TestUser() { SubjectId = "john", Username = "john", Password = "john" }
			};
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseIdentityServer(); //UseIdentityServer allows IdentityServer to start intercepting routes and handle requests.
		}
	}
}
