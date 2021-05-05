using System;
using System.Collections.Generic;
using HeringerSoftware.AngularDotNet.Core.Model;
using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;
using Microsoft.EntityFrameworkCore;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	public class MockRepositoryFactory
		: EFRepositoryFactory
	{
		public MockRepositoryFactory(MockDbContext dbContext) : base(dbContext)
		{
		}

		private MockEntityRepository _mockEntityRepository = null;
		public MockEntityRepository MockEntityRepository
		{
			get
			{
				if (_mockEntityRepository == null)
					_mockEntityRepository = new MockEntityRepository(((MockDbContext)this.DbContext).MockEntityDbSet);
				return _mockEntityRepository;
			}
		}

		private MockAnotherEntityRepository _mockAnotherEntityRepository = null;
		public MockAnotherEntityRepository MockAnotherEntityRepository
		{
			get
			{
				if (_mockAnotherEntityRepository == null)
					_mockAnotherEntityRepository = new MockAnotherEntityRepository(((MockDbContext)this.DbContext).MockAnotherEntityDbSet);
				return _mockAnotherEntityRepository;
			}
		}
	}
}
