using System;
using System.Collections.Generic;
using System.Linq;
using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	public class MockAnotherEntityRepository : EFRepository<MockAnotherEntity>
	{
		public MockAnotherEntityRepository(DbSet<MockAnotherEntity> dbSet) : base(dbSet)
		{
		}
	}
}
