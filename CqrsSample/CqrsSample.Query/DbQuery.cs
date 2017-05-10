using CqrsSample.Core.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsSample.Query
{
	public abstract class DbQuery
	{
		protected DbQuery(IDbConnectionFactory dbConnectionFactory)
		{
			_dbConnectionFactory = dbConnectionFactory;
		}

		private readonly IDbConnectionFactory _dbConnectionFactory;

		protected IDbConnection CreateConnection()
		{
			return _dbConnectionFactory.CreateConnection();
		}
	}
}
