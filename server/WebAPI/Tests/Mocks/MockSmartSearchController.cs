using HeringerSoftware.AngularDotNet.Core.Persistence;
using HeringerSoftware.AngularDotNet.Core.WebAPI.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using HeringerSoftware.AngularDotNet.Core.WebAPI.Wrappers;
using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	[Route("api/[controller]")]
	public class MockSmartSearchController : SmartSearchController
	{
		private MockRepositoryFactory DaoFactory;

		public MockSmartSearchController(IOptions<AppSettings> appSettings, MockDbContext dbContext, ILogger<TransactionalApiContoller> logger)
			: base(appSettings, dbContext, logger)
		{
			this.DaoFactory = new MockRepositoryFactory(dbContext);
		}

		protected override EFRepositoryFactory GetRepositoryFactory()
		{
			return this.DaoFactory;
		}


	}
}
