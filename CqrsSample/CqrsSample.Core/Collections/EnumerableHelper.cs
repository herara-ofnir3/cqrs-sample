using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsSample.Core.Collections
{
	public static class EnumerableHelper
	{
		public static IPaged<T> ToPaged<T>(this IEnumerable<T> collection, int totalCount)
		{
			return new Paged<T>(collection, totalCount);
		}
	}
}
