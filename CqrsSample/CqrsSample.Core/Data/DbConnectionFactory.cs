using CqrsSample.Core.Config;
using System.Data;
using System.Data.SqlClient;

namespace CqrsSample.Core.Data
{
	public interface IDbConnectionFactory
	{
		IDbConnection CreateConnection();
	}

	public class SqlConnectionFactory : IDbConnectionFactory
	{
		public IDbConnection CreateConnection()
		{
			return new SqlConnection(AppConfigProvider.Config.DefaultConnection);
		}
	}
}
