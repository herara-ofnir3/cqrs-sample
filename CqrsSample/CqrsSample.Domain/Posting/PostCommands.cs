using CqrsSample.Core.Domain;

namespace CqrsSample.Domain.Posting
{
	public class NewPostCommand : Command
	{
		public string Title { get; set; }

		public string Body { get; set; }

		public bool IsDraft { get; set; }
	}

	public class PostEditCommand : Command
	{
		public int PostId { get; set; }

		public string Title { get; set; }

		public string Body { get; set; }
	}

	public class PostDeleteCommand : Command
	{
		public int PostId { get; set; }
	}

	public class PostPublicCommand : Command
	{
		public int PostId { get; set; }
	}

	public class PostRevertToDraftCommand : Command
	{
		public int PostId { get; set; }
	}

	public class CommentPostCommand : Command
	{
		public int PostId { get; set; }

		public string Name { get; set; }

		public string Body { get; set; }
	}
}
