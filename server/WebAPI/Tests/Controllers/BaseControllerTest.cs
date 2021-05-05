using IdentityServer4.AccessTokenValidation;
using HeringerSoftware.AngularDotNet.Core.DataTransferObjects;
using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;
using HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using HeringerSoftware.AngularDotNet.Core.Model;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Controllers
{
	[TestClass]
	public class BaseControllerTest
	{
		private readonly TestServer ApiServer;
		private readonly HttpClient ApiClient;
		private readonly TestServer IdentityTestServer;
		private readonly HttpClient IdentityClient;
		private MockDbContext DbContext;

		public BaseControllerTest()
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(System.IO.Path.GetFullPath(@"../../../../Tests/"))
				.AddJsonFile("appsettings.json", optional: false)
				.Build();

			string authorityUrl = configuration.GetValue<string>("AppSettings:IdentityAuthority");

			//MOCK IDENTITY SERVER
			//https://stackoverflow.com/questions/39390339/integration-testing-with-in-memory-identityserver
			var idBuilder = new WebHostBuilder();
			idBuilder.UseStartup<MockIdentityStartup>(); //TODO startup do identity server in-memory
			IdentityTestServer = new TestServer(idBuilder);
			IdentityTestServer.BaseAddress = new Uri(authorityUrl);
			IdentityClient = IdentityTestServer.CreateClient();
			IdentityClient.BaseAddress = new Uri(authorityUrl);

			//BUILD API SERVER
			ApiServer = new TestServer(new WebHostBuilder()
				.ConfigureServices(c => c.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
				.AddIdentityServerAuthentication(o =>
				{
					o.Authority = authorityUrl;
					o.RequireHttpsMetadata = false; //!CurrentEnvironment.IsDevelopment(),
					//TODO o.ApiName = appSettings.ApiName;
					o.JwtBackChannelHandler = IdentityTestServer.CreateHandler();
					//https://github.com/IdentityServer/IdentityServer4/issues/3666
					//o.IntrospectionDiscoveryHandler = IdentityTestServer.CreateHandler();
					//o.IntrospectionBackChannelHandler = IdentityTestServer.CreateHandler();
				}))
				.UseStartup<MockStartup>()
				.UseConfiguration(configuration));

			ApiClient = ApiServer.CreateClient();

			PrepareDatabase(configuration);
		}

		private void PrepareDatabase(IConfigurationRoot configuration)
		{
			var builder = new DbContextOptionsBuilder<MockDbContext>();
			builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			this.DbContext = new MockDbContext(builder.Options, new MockUserResolver());
			DbContext.Database.EnsureDeleted();
			DbContext.Database.EnsureCreated();
		}

		protected void AddEntities(params Entity[] entities)
		{
			foreach (Entity entity in entities)
				this.DbContext.Add(entity);
			this.DbContext.SaveChanges();
		}

		protected void SetBearerTokenForDefaultTestUser()
		{
			SetBearerTokenFor("bob", "bob");
		}

		protected void SetBearerTokenFor(string username, string password)
		{
			var token = GetIdentityTokenFor(username, password);
			ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}

		protected string GetIdentityTokenFor(string username, string password)
		{
			FormUrlEncodedContent content = new FormUrlEncodedContent(new Dictionary<string, string>() {
				{ "grant_type", "password" },
				{ "username", username },
				{ "password", password },
				{ "client_id", "unit-test" },  //TODO ler de algum lugar
				{ "client_secret", "unit-test-as-a-client-secret" } //TODO ler de algum lugar
			});

			var task = IdentityClient.PostAsync($"connect/token", content);
			//var task = IdentityClient.PostAsync($"{authorityUrl}/connect/token", content);
			task.Wait();
			var task2 = task.Result.Content.ReadAsStringAsync();
			task2.Wait();
			var responseString = task2.Result;
			dynamic json = JsonConvert.DeserializeObject(responseString);
			return json.access_token;
		}

		protected void SetInvalidToken()
		{
			//era assim ApiClient.SetBearerToken("invalidtoken"); mudou para
			ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "invalidtoken");
		}

		protected TResponseDto Post<TResponseDto>(string requestUri, TResponseDto requestDto)
		{
			return Post<TResponseDto>(requestUri, (object)requestDto);
		}

		protected TResponseDto Post<TResponseDto>(string requestUri, object requestDto)
		{
			var content = JsonConvert.SerializeObject(requestDto);
			return Post<TResponseDto>(requestUri, content);
		}

		protected TResponseDto Post<TResponseDto>(string requestUri, string content)
		{
			var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
			var task = ApiClient.PostAsync(requestUri, stringContent);
			task.Wait();
			return Unwrap<TResponseDto>(task.Result);
		}

		protected TResponseDto Get<TResponseDto>(string requestUri)
		{
			var task = ApiClient.GetAsync(requestUri);
			task.Wait();
			return Unwrap<TResponseDto>(task.Result);
		}

		protected TResponseDto Delete<TResponseDto>(string requestUri)
		{
			var task = ApiClient.DeleteAsync(requestUri);
			task.Wait();
			return Unwrap<TResponseDto>(task.Result);
		}

		private TResponseDto Unwrap<TResponseDto>(HttpResponseMessage response)
		{
			response.EnsureSuccessStatusCode();
			var task2 = response.Content.ReadAsStringAsync();
			task2.Wait();
			var responseString = task2.Result;
			var responseDto = JsonConvert.DeserializeObject<TResponseDto>(responseString);
			return responseDto;
		}

		protected EntityReferenceDto Wrap(EntityDto dto)
		{
			if (dto == null)
				return null;
			return new EntityReferenceDto() { Id = dto.Id };
		}

		//private class MockUserResolver : UserResolver { public string GetUserName() => "testUser"; }
	}
}

