using CqrsSample.Domain.Posting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace CqrsSample.Query.Posting
{
	public class PostDetails
	{
		public int Id { get; set; }

		[DisplayName("タイトル")]
		public string Title { get; set; }

		[DisplayName("内容")]
		public string Body { get; set; }

		public PostStatus Status { get; set; }

		public DateTime PostedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }

		public IList<CommentDto> Comments { get; set; }
	}
}
