using System;
using NHibernate;

namespace MetalSoft.Core.Persistence.NhPersistence
{
	public class NHTransactionManagerFactory
		: TransactionManagerFactory
	{
		private ISessionFactory SessionFactoryInstance;

		protected ISessionFactory SessionFactory
		{
			get
			{
				if (SessionFactoryInstance != null)
					return SessionFactoryInstance;
				SessionFactoryInstance = InitializeSessionFactory();
				return SessionFactoryInstance;
			}
		}

		public string ConfigurationFileName { get; set; }

		private ISessionFactory InitializeSessionFactory()
		{
			NHBootStrapper nhConfiguration = new NHBootStrapper();
			nhConfiguration.ConfigurationFileName = this.ConfigurationFileName;
			return nhConfiguration.NhConfiguration.BuildSessionFactory();
		}

		public virtual TransactionManager Create()
		{
			ISession session = SessionFactory.OpenSession();
			session.FlushMode = FlushMode.Commit;
			return new NHTransactionManager(session, new NHibernateDAOFactory(session));
		}
	}
}
