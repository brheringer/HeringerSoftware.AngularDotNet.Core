using System;
using System.Collections.Generic;
using MetalSoft.Core.Model;
using NHibernate;
using NHibernate.Criterion;

namespace MetalSoft.Core.Persistence.NhPersistence
{
	public abstract class NHibernateDAO<EntityType>
		: DAO<EntityType>
		where EntityType : Entity
	{
		public int SearchLimit { get; set; }
		public int SearchPage { get; set; }

		public readonly ISession SessionInstance;

		protected NHibernateDAO(ISession session)
		{
			this.SessionInstance = session;
		}

		public virtual void Delete(EntityType entity)
		{
			this.SessionInstance.Delete(entity);
		}

		public virtual EntityType Load(int id)
		{
			EntityType entidade = this.SessionInstance.Get<EntityType>(id);
			if (entidade == null)
			{
				throw new InstanceNotFoundException(typeof(EntityType), id);
			}
			return entidade;
		}

		public virtual IList<EntityType> LoadAll()
		{
			//TODO limitar searchLimit?
			return this.SessionInstance.QueryOver<EntityType>().List();
		}

		public virtual EntityType Update(EntityType entity)
		{
			if (entity == null)
				throw new ArgumentNullException();

			if (!entity.IsPersistent)
			{
				entity.CreationDateTime = DateTime.Now;
				//o que vier preenchido entity.CreationUser;
				entity.LastUpdateDateTime = entity.CreationDateTime;
				entity.LastUpdateUser = entity.CreationUser;
			}
			else
			{
				entity.LastUpdateDateTime = DateTime.Now;
				//o que vier preenchido entity.LastUpdateUser = entity.CreationUser;
			}

			this.SessionInstance.SaveOrUpdate(entity);
			return entity;
		}

		protected EntityType LoadByHelper(IQueryOver<EntityType> query, string entryKey)
		{
			var list = query.List();
			if (list.Count == 1)
			{
				return list[0];
			}
			else if (list.Count < 1)
			{
				throw new InstanceNotFoundException(typeof(EntityType), entryKey);
			}
			else
			{
				throw new TooManyInstancesFoundException(typeof(EntityType), entryKey);
			}
		}

		protected EntityType LoadByIfExistsHelper(IQueryOver<EntityType> query, string entryKey)
		{
			var list = query.List();
			if (list.Count == 1)
			{
				return list[0];
			}
			else if (list.Count < 1)
			{
				return null;
			}
			else
			{
				throw new TooManyInstancesFoundException(typeof(EntityType), entryKey);
			}
		}

		public virtual IList<EntityType> SmartSearch(string smartEntry)
		{
			throw new NotImplementedException();
		}
	}
}
