using CqrsSample.Core.Collections;
using CqrsSample.Query.Posting;

namespace CqrsSample.Web.Models.Post
{
	public class PostIndexModel
	{
		public Page Page { get; set; }

		public IPaged<PostSummary> Posts { get; set; }
	}
}