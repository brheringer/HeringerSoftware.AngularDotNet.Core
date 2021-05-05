using HeringerSoftware.AngularDotNet.Core.Model;
using HeringerSoftware.AngularDotNet.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence
{
	public class EFRepository<T> : Repository<T>
		where T : Entity
	{
		protected DbSet<T> Entities { get; private set; }

		public int SearchLimit { get; set; }
		public int SearchPage { get; set; }

		public EFRepository(DbSet<T> dbSet)
		{
			this.Entities = dbSet;
		}

		public virtual T CreateProxy()
		{
			return this.Entities.CreateProxy();
		}

		public virtual void Delete(T entity)
		{
			this.Entities.Remove(entity);
		}

		public virtual T Load(int id)
		{
			var e = this.Entities.Find(id);
			if (e == null)
				throw new InstanceNotFoundException(typeof(T), id);
			return e;
		}

		public virtual T LoadWithCompositions(int id)
		{
			throw new NotImplementedException();
		}

		public virtual IList<T> LoadAll()
		{
			return this.Entities.ToList();
		}

		public virtual IList<Entity> SmartSearch(string smartEntry, string contextFilter, int max)
		{
			throw new NotImplementedException();
		}

		public virtual T Update(T entity)
		{
			if (entity.Id == 0)
				this.Entities.Add(entity);
			else
				this.Entities.Update(entity);
			return entity;
		}
	}
}
