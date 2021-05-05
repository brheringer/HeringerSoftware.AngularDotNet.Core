using HeringerSoftware.AngularDotNet.Core.Model;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence
{
	public abstract class EFDBContext : DbContext
	{
		private UserResolver UserResolverInstance { get; set; }

		protected EFDBContext(DbContextOptions options, UserResolver userResolver) : base(options)
		{
			this.UserResolverInstance = userResolver;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			//https://docs.microsoft.com/en-us/ef/core/querying/related-data
			//The simplest way to use lazy-loading is by installing the Microsoft.EntityFrameworkCore.Proxies package and enabling it with a call to UseLazyLoadingProxies.
			//EF Core will then enable lazy loading for any navigation property that can be overridden--that is, it must be virtual and on a class that can be inherited from. 
			optionsBuilder.UseLazyLoadingProxies();
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			OnBeforeSaving();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			OnBeforeSaving();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		private void OnBeforeSaving()
		{
			if (this.UserResolverInstance != null)
			{
				var entries = ChangeTracker.Entries();
				foreach (var entry in entries)
				{
					var e = entry.Entity as Entity;
					if (e != null)
					{
						var user = this.UserResolverInstance.GetUserName();
						var now = DateTime.Now;

						switch (entry.State)
						{
							case EntityState.Modified:
								e.LastUpdateUser = user;
								e.LastUpdateDateTime = now;
								break;

							case EntityState.Added:
								e.CreationDateTime = now;
								e.CreationUser = user;
								e.LastUpdateDateTime = now;
								e.LastUpdateUser = user;
								break;
						}
					}
				}
			}
		}
	}
}
