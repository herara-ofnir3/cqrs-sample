using CqrsSample.Query.Posting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CqrsSample.Web.Models.Post
{
	public class PostDetailsModel
	{
		public PostDetails Post { get; set; }

		public CommentPostModel CommentPostModel { get; set; }
	}
}