namespace CqrsSample.Core.Domain
{
	public interface IRepository<TEntity, TId> where TEntity : Entity<TId>
	{
		TEntity FindBy(TId id);
		void Save(TEntity entity);
		void Delete(TEntity entity);
	}
}
