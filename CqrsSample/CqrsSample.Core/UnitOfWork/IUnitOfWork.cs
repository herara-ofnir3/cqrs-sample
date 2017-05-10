namespace CqrsSample.Core.UnitOfWork
{
	public interface IUnitOfWork
	{
		ITransaction BeginTransaction();
	}
}
