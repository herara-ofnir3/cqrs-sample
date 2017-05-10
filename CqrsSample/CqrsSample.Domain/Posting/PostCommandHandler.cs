using System;
using CqrsSample.Core.Domain;

namespace CqrsSample.Domain.Posting
{
	public class PostCommandHandler :
		ICommandHandler<NewPostCommand>,
		ICommandHandler<PostEditCommand>,
		ICommandHandler<PostDeleteCommand>,
		ICommandHandler<PostPublicCommand>,
		ICommandHandler<PostRevertToDraftCommand>,
		ICommandHandler<CommentPostCommand>
	{
		public PostCommandHandler(IPostRepsitory postRepo)
		{
			_postRepo = postRepo;
		}

		private IPostRepsitory _postRepo;

		public void Handle(NewPostCommand command)
		{
			var newPost = new Post
			{
				Title = command.Title,
				Body = command.Body,
				Status = command.IsDraft ? PostStatus.Draft : PostStatus.Public,
			};

			_postRepo.Save(newPost);
		}

		public void Handle(PostEditCommand command)
		{
			var post = _postRepo.FindBy(command.PostId);
			post.Edit(command.Title, command.Body);
			_postRepo.Save(post);
		}

		public void Handle(PostDeleteCommand command)
		{
			var post = _postRepo.FindBy(command.PostId);
			_postRepo.Delete(post);
		}

		public void Handle(PostPublicCommand command)
		{
			var post = _postRepo.FindBy(command.PostId);
			post.Public();
			_postRepo.Save(post);
		}

		public void Handle(PostRevertToDraftCommand command)
		{
			var post = _postRepo.FindBy(command.PostId);
			post.RevertToDraft();
			_postRepo.Save(post);
		}

		public void Handle(CommentPostCommand command)
		{
			var post = _postRepo.FindBy(command.PostId);
			post.PostComment(command.Name, command.Body);
			_postRepo.Save(post);
		}
	}
}
