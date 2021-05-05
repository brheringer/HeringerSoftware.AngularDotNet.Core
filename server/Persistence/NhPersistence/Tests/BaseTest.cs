using System;
using MetalSoft.Core.Persistence.EFPersistence;
using MetalSoft.MonitoramentoCarvao.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetalSoft.MonitoramentoCarvao.Persistence.EFPersistence.Tests
{ 
	public class BaseTest
	{
		protected EFMonitoramentoCarvaoDaoFactory DAOFactory { get; private set; }
		protected MonitoramentoCarvaoDbContext DbContext { get; private set; }

		[TestInitialize]
		public void SetUp()
		{
			var builder = new DbContextOptionsBuilder<MonitoramentoCarvaoDbContext>();
			builder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=MonitoramentoCarvao-EFPersistence-Tests;Trusted_Connection=True;");
			var dbContext = new MonitoramentoCarvaoDbContext(builder.Options, new MockUserResolver());
			dbContext.Database.EnsureDeleted();
			dbContext.Database.EnsureCreated();
			this.DbContext = dbContext;
			this.DAOFactory = new EFMonitoramentoCarvaoDaoFactory(dbContext);
		}
	}
}
