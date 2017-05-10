using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CqrsSample.Domain.Posting;
using Moq;

namespace CqrsSample.Domain.Test
{
	[TestClass]
	public class PostCommandHandlerTest
	{
		[TestMethod]
		public void Handle_NewPost()
		{
			// Arrange
			var command = new NewPostCommand();
			command.Title = "new post title.";
			command.Body = "new post body.";
			command.IsDraft = true;

			var component = new Component();
			component.PostRepo
				.Setup(m => m.Save(It.IsAny<Post>()));

			var handler = component.Build();

			// Act
			handler.Handle(command);

			// Assert
			component.PostRepo.Verify(m => m.Save(It.Is<Post>(p =>
				p.Title == command.Title &&
				p.Body == command.Body &&
				p.Status == PostStatus.Draft)), Times.Once);
		}

		[TestMethod]
		public void Handle_Edit()
		{
			// Arrange
			var command = new PostEditCommand();
			command.PostId = 134;
			command.Title = "new post title.";
			command.Body = "new post body.";

			var post = new Mock<Post>();
			post.Setup(m => m.Edit(command.Title, command.Body));

			var component = new Component();
			component.PostRepo
				.Setup(m => m.FindBy(command.PostId))
				.Returns(post.Object);

			component.PostRepo
				.Setup(m => m.Save(post.Object));

			var handler = component.Build();

			// Act
			handler.Handle(command);

			// Assert
			component.PostRepo.VerifyAll();
		}

		[TestMethod]
		public void Handle_Public()
		{
			// Arrange
			var command = new PostPublicCommand();
			command.PostId = 134;

			var post = new Mock<Post>();
			post.Setup(m => m.Public());

			var component = new Component();
			component.PostRepo
				.Setup(m => m.FindBy(command.PostId))
				.Returns(post.Object);

			component.PostRepo
				.Setup(m => m.Save(post.Object));

			var handler = component.Build();

			// Act
			handler.Handle(command);

			// Assert
			component.PostRepo.VerifyAll();
		}

		[TestMethod]
		public void Handle_RevertToDraft()
		{
			// Arrange
			var command = new PostRevertToDraftCommand();
			command.PostId = 134;

			var post = new Mock<Post>();
			post.Setup(m => m.RevertToDraft());

			var component = new Component();
			component.PostRepo
				.Setup(m => m.FindBy(command.PostId))
				.Returns(post.Object);

			component.PostRepo
				.Setup(m => m.Save(post.Object));

			var handler = component.Build();

			// Act
			handler.Handle(command);

			// Assert
			component.PostRepo.VerifyAll();
		}

		[TestMethod]
		public void Handle_Delete()
		{
			// Arrange
			var command = new PostDeleteCommand();
			command.PostId = 134;

			var post = new Post { Id = 134 };

			var component = new Component();
			component.PostRepo
				.Setup(m => m.FindBy(command.PostId))
				.Returns(post);

			component.PostRepo
				.Setup(m => m.Delete(post));

			var handler = component.Build();

			// Act
			handler.Handle(command);

			// Assert
			component.PostRepo.VerifyAll();
		}

		[TestMethod]
		public void Handle_CommentPost()
		{
			// Arrange
			var command = new CommentPostCommand();
			command.PostId = 134;
			command.Name = "user name.";
			command.Body = "comment body.";

			var post = new Mock<Post>();
			post.Setup(m => m.PostComment(command.Name, command.Body));

			var component = new Component();
			component.PostRepo
				.Setup(m => m.FindBy(command.PostId))
				.Returns(post.Object);

			component.PostRepo
				.Setup(m => m.Save(post.Object));

			var handler = component.Build();

			// Act
			handler.Handle(command);

			// Assert
			post.VerifyAll();
			component.PostRepo.VerifyAll();
		}

		private class Component
		{
			public Mock<IPostRepsitory> PostRepo = new Mock<IPostRepsitory>();

			public PostCommandHandler Build()
			{
				return new PostCommandHandler(PostRepo.Object);
			}
		}
	}
}
