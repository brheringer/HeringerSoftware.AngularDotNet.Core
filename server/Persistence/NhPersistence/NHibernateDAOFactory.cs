using NHibernate;
using System;

namespace MetalSoft.Core.Persistence.NhPersistence
{
	public class NHibernateDAOFactory
		: DAOFactory
	{
		protected readonly ISession SessionInstance;

		public NHibernateDAOFactory(ISession session)
		{
			this.SessionInstance = session;
		}
	}
}
