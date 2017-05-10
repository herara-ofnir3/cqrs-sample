using System;

namespace CqrsSample.Query.Posting
{
	public class PostSummary
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Body { get; set; }

		public DateTime PostedAt { get; set; }

		public int CommentCount { get; set; }
	}
}
