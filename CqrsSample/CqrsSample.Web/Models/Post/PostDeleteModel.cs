using CqrsSample.Domain.Posting;
using CqrsSample.Query.Posting;

namespace CqrsSample.Web.Models.Post
{
	public class PostDeleteModel
	{
		public int Id { get; set; }

		public PostDetails Post { get; set; }
	}
}