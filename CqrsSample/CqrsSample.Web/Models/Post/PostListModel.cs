using CqrsSample.Core.Collections;
using CqrsSample.Query.Posting;

namespace CqrsSample.Web.Models.Post
{
	public class PostListModel
	{
		public Page Page { get; set; }

		public IPaged<PostInfo> Posts { get; set; }
	}
}