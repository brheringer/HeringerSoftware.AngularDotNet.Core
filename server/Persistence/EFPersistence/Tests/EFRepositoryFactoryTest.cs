using System;
using System.Collections.Generic;
using HeringerSoftware.AngularDotNet.Core.Model;
using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence.Tests
{
	[TestClass]
	public class EFRepositoryFactoryTest
	{
		[TestMethod]
		public void GetRepositoryForTest()
		{
			MockEFRepositoryFactory factory = new MockEFRepositoryFactory();
			AgnosticRepository repo = factory.GetRepositoryFor("MockEntity");
			Assert.IsNotNull(repo);
		}

		private class MockEntity : Entity
		{
			public override void Validate()
			{
			}
		}

		private class MockEntityRepository : EFRepository<Entity>
		{
			public MockEntityRepository() : base(null)
			{
			}
		}

		private class MockEFRepositoryFactory : EFRepositoryFactory
		{
			public MockEFRepositoryFactory() : base(null)
			{
			}

			public MockEntityRepository MockEntityRepository
			{
				get
				{
					return new MockEntityRepository();
				}
			}
		}
	}
}
