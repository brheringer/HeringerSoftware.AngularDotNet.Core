using HeringerSoftware.AngularDotNet.Core.DataTransferObjects;
using HeringerSoftware.AngularDotNet.Core.Persistence;
using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;
using HeringerSoftware.AngularDotNet.Core.Model;
using HeringerSoftware.AngularDotNet.Core.WebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Controllers
{
    public abstract class SmartSearchController : TransactionalApiContoller
    {
        public SmartSearchController(IOptions<AppSettings> appSettings, DbContext dbContext, ILogger<TransactionalApiContoller> logger)
            : base(appSettings, dbContext, logger)
        {
        }

        [HttpGet]
		[Route("{entity}/{max}/{entry}/{contextFilter?}")]
		public virtual IActionResult Get(string entity, int max, string entry, string contextFilter = "")
        {
            return Ok(InvokeCommandWithoutTransaction(() => Get2(entity, max, entry, contextFilter)));
		}

        private EntitiesReferencesDto Get2(string entity, int max, string entry, string contextFilter)
		{
			var dao = GetRepository(entity);
			var entities = dao.SmartSearch(entry, contextFilter, max);
			return Wrap(entities);
		}

		private AgnosticRepository GetRepository(string entity)
		{
			EFRepositoryFactory factory = GetRepositoryFactory();
			return factory.GetRepositoryFor(entity);
		}

		protected abstract EFRepositoryFactory GetRepositoryFactory();

		private EntitiesReferencesDto Wrap(IList<Entity> entities)
		{
			return new EntityWrapper(this.AppDbContext).WrapToReferences<Entity>(entities);
		}
	}
}
