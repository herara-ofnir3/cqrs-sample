using System;

namespace CqrsSample.Core.UnitOfWork
{
	public class NHTransaction : ITransaction
	{
		public NHTransaction(NHibernate.ITransaction nhTransaction)
		{
			_nhTransaction = nhTransaction;
		}

		private NHibernate.ITransaction _nhTransaction;

		public void Commit()
		{
			_nhTransaction.Commit();
		}

		public void Dispose()
		{
			if (_nhTransaction != null)
			{
				_nhTransaction.Dispose();
			}
		}
	}
}
