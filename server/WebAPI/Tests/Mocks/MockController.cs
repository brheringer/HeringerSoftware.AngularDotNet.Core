using HeringerSoftware.AngularDotNet.Core.Persistence;
using HeringerSoftware.AngularDotNet.Core.WebAPI.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using HeringerSoftware.AngularDotNet.Core.WebAPI.Wrappers;
using System.Linq;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	[Route("api/[controller]")]
	public class MockController : GenericController<MockDto, MockEntity>
	{
		private MockRepositoryFactory DaoFactory;

		public MockController(IOptions<AppSettings> appSettings, MockDbContext dbContext, ILogger<TransactionalApiContoller> logger)
			: base(appSettings, dbContext, logger)
		{
			this.DaoFactory = new MockRepositoryFactory(dbContext);
		}

		protected override Repository<MockEntity> GetDAO()
		{
			return this.DaoFactory.MockEntityRepository;
		}

		[HttpGet]
		[Route("hello")]
		[AllowAnonymous]
		public virtual IActionResult GetHello()
		{
			return Ok(new MockDto() { TheString = "Hello, MockController!" });
		}
	}
}
