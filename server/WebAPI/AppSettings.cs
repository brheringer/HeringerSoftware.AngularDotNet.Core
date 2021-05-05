using System;
using System.Collections.Generic;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI
{
    public class AppSettings
    {
		public int MaxAutoSearch { get; set; }
		public int MaxManualSearch { get; set; }
		public int SessionExpirationsHours { get; set; }
		public bool ShowFullException { get; set; } //TODO talvez tirar isso e diferenciar se eh debug ou producao
		public string ConnectionString { get; set; }
		public string IdentityAuthority { get; set; }
		public string ApiName { get; set; }
		public string[] CorsAllowedOrigins { get; set; } = new string[] { "*" };

	}
}
