using System;
using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;
using Microsoft.EntityFrameworkCore;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	public class MockDbContext : EFDBContext
	{
		public DbSet<MockEntity> MockEntityDbSet { get; private set; }
		public DbSet<MockAnotherEntity> MockAnotherEntityDbSet { get; private set; }

		public MockDbContext(DbContextOptions options, UserResolver userResolver) : base(options, userResolver)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
			modelBuilder.ApplyConfiguration(new MockEntityMapping());
			modelBuilder.ApplyConfiguration(new MockAnotherEntityMapping());
			modelBuilder.ApplyConfiguration(new MockCompositionMapping());
		}
	}
}
