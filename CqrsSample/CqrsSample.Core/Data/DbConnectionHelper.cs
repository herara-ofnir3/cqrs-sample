using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsSample.Core.Data
{
	public static class DbConnectionHelper
	{
		public static void OpenIfClosed(this IDbConnection connection)
		{
			if (connection.State == ConnectionState.Closed)
			{
				connection.Open();
			}
		}
	}
}
