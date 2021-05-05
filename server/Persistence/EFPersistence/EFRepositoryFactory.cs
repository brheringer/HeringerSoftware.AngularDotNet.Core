using HeringerSoftware.AngularDotNet.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence
{
	public abstract class EFRepositoryFactory
	{
		private readonly EFDBContext _dbContext;

		public EFRepositoryFactory(EFDBContext dbContext)
		{
			this._dbContext = dbContext;
		}

		protected EFDBContext DbContext
		{
			get
			{
				return this._dbContext;
			}
		}

		public virtual AgnosticRepository GetRepositoryFor(string entity)
		{
			PropertyInfo pInfo = this.GetType().GetProperty(InferRepositoryGetterName(entity));
			if (pInfo != null)
			{
				object repository = pInfo.GetGetMethod().Invoke(this, null);
				return (AgnosticRepository)repository;
			}
			return null;
		}

		protected virtual string InferRepositoryGetterName(string entity)
		{
			return entity + "Repository";
		}
	}
}
