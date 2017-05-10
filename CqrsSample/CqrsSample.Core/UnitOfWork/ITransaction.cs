using System;

namespace CqrsSample.Core.UnitOfWork
{
	public interface ITransaction : IDisposable
	{
		void Commit();
	}
}
