using IdentityServer4.AccessTokenValidation;
using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Web;
using System;
using System.Reflection;
using System.Collections.Generic;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI
{
    public class Startup
    {
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

		/// <summary>
		/// Dependency injection.
		/// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/startup
		/// "This method gets called by the runtime. Use this method to add services to the container."
		/// </summary>
		/// <param name="services"></param>
		public virtual void ConfigureServices(IServiceCollection services)
		{
			AppSettings appSettings = ReadAppSettings(services);

			services.AddOptions();

			services.AddCors(setup => setup.AddPolicy("CorePolicy", builder => builder
				.WithOrigins(appSettings.CorsAllowedOrigins)
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials()));

			//services.AddLogging();

			//https://stackoverflow.com/questions/59648290/manage-logging-configuration-with-nlog-in-net-core-3
			services.AddLogging(loggingBuilder =>
			{
				loggingBuilder.ClearProviders();
				//loggingBuilder.AddConfiguration(config.GetSection("Logging"));
				//loggingBuilder.AddNLog(config);
			});

			var mvcBuilder = services.AddMvcCore();
			foreach (var assembly in GetApplicationParts())
				mvcBuilder.AddApplicationPart(assembly);
			mvcBuilder
				.AddAuthorization();
			//.AddJsonFormatters() //TODO mudou na 3.x
			//.AddJsonOptions(options => //TODO mudou na 3.x
			//{
			//	options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
			//	options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			//});

			ConfigureUserResolver(services);
			ConfigureAuthenticationServices(services, appSettings);
			ConfigureDbContext(services, appSettings);
			ConfigureDaoFactory(services, appSettings);
		}

		protected AppSettings ReadAppSettings(IServiceCollection services)
		{
			var appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);
			var appSettings = appSettingsSection.Get<AppSettings>();
			appSettings.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
			return appSettings;
		}

		protected virtual List<Assembly> GetApplicationParts()
		{
			return new List<Assembly>();
			//return new List<Assembly>() { this.GetType().Assembly };
		}

		protected virtual void ConfigureUserResolver(IServiceCollection services)
		{
			services.AddHttpContextAccessor();
			services.AddScoped<UserResolver, UserResolverForIdentity>();
		}

		protected virtual void ConfigureAuthenticationServices(IServiceCollection services, AppSettings appSettings)
		{
			services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
				.AddIdentityServerAuthentication(o =>
				{
					o.Authority = appSettings.IdentityAuthority;
					o.RequireHttpsMetadata = false; //!CurrentEnvironment.IsDevelopment(),
				});
		}

		protected virtual void ConfigureDbContext(IServiceCollection services, AppSettings appSettings)
		{
			//services.AddDbContext<EFDBContext>(options => options.UseSqlServer(appSettings.ConnectionString)); //TODO mysql, oracle...
		}

		protected virtual void ConfigureDaoFactory(IServiceCollection services, AppSettings appSettings)
		{
			//services.AddScoped<EFDAOFactory>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public virtual void Configure(IApplicationBuilder app/*, ILoggingBuilder loggingBuilder*/)
        {
			//app.UseStaticFiles();
			app.UseRouting();
			app.UseCors("CorePolicy");
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});

			//NLog.Web.NLogBuilder.ConfigureNLog("nlog.config");
			//loggingBuilder.AddNLog();
			//app.UseNLog();
		}
	}
}
