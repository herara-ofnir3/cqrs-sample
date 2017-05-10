namespace CqrsSample.Core.UnitOfWork
{
	public class NHUnitOfWork : IUnitOfWork
	{
		public NHUnitOfWork(NHibernate.ISession nhSession)
		{
			_nhSession = nhSession;
		}

		private NHibernate.ISession _nhSession;

		public NHibernate.ISession NHSession
		{
			get { return _nhSession; }
		}

		public ITransaction BeginTransaction()
		{
			return new NHTransaction(_nhSession.BeginTransaction());
		}
	}
}
