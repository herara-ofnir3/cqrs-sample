using CqrsSample.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace CqrsSample.Domain.Posting
{
	public interface IPostRepsitory : IRepository<Post, int> { }

	public class PostRepository : NHRepository<Post, int>, IPostRepsitory
	{
		public PostRepository(ISession nhSession) : base(nhSession)
		{
		}
	}
}
