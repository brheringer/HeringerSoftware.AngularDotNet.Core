using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace MetalSoft.Core.Persistence.NhPersistence
{
	public class NHTransactionManager
		: TransactionManagerBase
	{
		private readonly ISession SessionInstance;

		public NHTransactionManager(ISession session, DAOFactory daoFactory)
        {
            this.SessionInstance = session;
			this.Factory = daoFactory; // new NHibernateDAOFactory(session);
        }

		public override void BeginTransaction()
		{
			base.BeginTransaction();
			if (SessionInstance.Transaction.IsActive) return; //TODO nao deveria deixar dar pau?
			SessionInstance.BeginTransaction();
		}

		public override void Commit()
		{
			base.Commit();
			if (!SessionInstance.Transaction.IsActive) return; //TODO nao deveria deixar dar pau?
			SessionInstance.Transaction.Commit();
		}

		public override void Rollback()
		{
			base.Rollback();
			if (!SessionInstance.Transaction.IsActive) return;
			SessionInstance.Transaction.Rollback();
			CloseSession(); //https://forum.hibernate.org/viewtopic.php?f=25&t=977833
		}

		protected override void Dispose(bool disposing)
		{
			if (!disposing) return;
			// free managed resources
			if (!this.IsDisposed && this.TransactionIsOpen)
			{
				Rollback();
			}
			CloseSession();
			this.Factory = null;
			this.IsDisposed = true;
		}

		public void CloseSession()
		{
			if (SessionInstance == null) return;
			if (!SessionInstance.IsOpen) return;
			SessionInstance.Close();
		}

		public void ClearSession()
		{
			SessionInstance.Clear();
		}
	}
}
