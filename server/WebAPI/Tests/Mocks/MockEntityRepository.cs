using System;
using System.Collections.Generic;
using System.Linq;
using HeringerSoftware.AngularDotNet.Core.Model;
using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;
using Microsoft.EntityFrameworkCore;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	public class MockEntityRepository : EFRepository<MockEntity>
	{
		public MockEntityRepository(DbSet<MockEntity> dbSet) : base(dbSet)
		{
		}

		public override IList<Entity> SmartSearch(string smartEntry, string companyFilter, int max)
		{
			return this.Entities
				.Where(e => e.Company == companyFilter && EF.Functions.Like(e.TheString, "%"+smartEntry+"%"))
				.OrderBy(e => e.TheString)
				.Take(max)
				.AsNoTracking()
				.ToList<Entity>();
		}

		public override MockEntity LoadWithCompositions(int id)
		{
			var e = this.Entities.Where(e => e.Id == id)
				.Include(e => e.TheComposition)
				.FirstOrDefault();
			return e;
		}
	}
}
