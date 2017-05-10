using System;

namespace CqrsSample.Query.Posting
{
	public class CommentDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Body { get; set; }

		public DateTime PostedAt { get; set; }
	}
}
