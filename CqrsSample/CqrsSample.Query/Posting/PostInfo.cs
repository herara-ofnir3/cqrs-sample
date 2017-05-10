using CqrsSample.Domain.Posting;
using System;

namespace CqrsSample.Query.Posting
{
	public class PostInfo	
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public DateTime PostedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }

		public int CommentCount { get; set; }

		public PostStatus Status { get; set; }
	}
}
