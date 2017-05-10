namespace CqrsSample.Core.Domain
{
	public abstract class NHRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : Entity<TId>
	{
		protected NHRepository(NHibernate.ISession nhSession)
		{
			NHSession = nhSession;
		}

		protected NHibernate.ISession NHSession { get; private set; }

		public TEntity FindBy(TId id)
		{
			return NHSession.Get<TEntity>(id);
		}

		public void Save(TEntity entity)
		{
			NHSession.SaveOrUpdate(entity);
		}

		public void Delete(TEntity entity)
		{
			NHSession.Delete(entity);
		}
	}
}
