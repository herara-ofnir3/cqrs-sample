using CqrsSample.Core.Collections;
using CqrsSample.Core.Domain;
using CqrsSample.Domain.Posting;
using CqrsSample.Query.Posting;
using CqrsSample.Web.Controllers;
using CqrsSample.Web.Models.Post;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CqrsSample.Web.Test.Controllers
{
	[TestClass]
	public class PostControllerTest
	{
		[TestMethod]
		public void Index()
		{
			// Arrange
			var page = 3;
			var posts = new List<PostSummary> { new PostSummary(), new PostSummary() } .ToPaged(33);

			var component = new Component();
			component.PostQuery
				.Setup(m => m.GetSummaries(new Page(3, 5)))
				.Returns(posts);

			var controller = component.Build();

			// Act
			var viewResult = controller.Index(page);

			// Assert
			Assert.IsInstanceOfType(viewResult.Model, typeof(PostIndexModel));
			var model = (PostIndexModel)viewResult.Model;
			
			Assert.AreEqual(posts, model.Posts);
		}

		[TestMethod]
		public void PostNew()
		{
			// Arrange
			var posting = new NewPostModel();
			posting.Title = "post title.";
			posting.Body = "post body.";
			posting.IsDraft = true;

			var component = new Component();
			component.Bus.Setup(x => x.Send(It.IsAny<NewPostCommand>()));

			var controller = component.Build();

			// Act
			var actionResult = controller.New(posting);

			// Assert
			Assert.IsInstanceOfType(actionResult, typeof(RedirectToRouteResult));

			var redirectResult = (RedirectToRouteResult)actionResult;
			Assert.AreEqual("Index", redirectResult.RouteValues["action"]);

			component.Bus.Verify(m => m.Send(It.Is<NewPostCommand>(x =>
				x.Title == posting.Title &&
				x.Body == posting.Body &&
				x.IsDraft == posting.IsDraft)), Times.Once);
		}

		[TestMethod]
		public void PostNew_Invalid()
		{
			// Arrange
			var posting = new NewPostModel();
			posting.Title = "post title.";
			posting.Body = "post body.";
			posting.IsDraft = true;

			var component = new Component();
			var controller = component.Build();
			controller.ModelState.AddModelError("dummy error.", "");

			// Act
			var actionResult = controller.New(posting);

			// Assert
			Assert.IsInstanceOfType(actionResult, typeof(ViewResult));

			var viewResult = (ViewResult)actionResult;
			Assert.AreEqual(posting, viewResult.Model);
			component.Bus.Verify(m => m.Send(It.IsAny<NewPostCommand>()), Times.Never);
		}

		[TestMethod]
		public void Details()
		{
			// Arrange
			var postId = 2109;
			var postDetails = new PostDetails { };

			var component = new Component();
			component.PostQuery
				.Setup(m => m.GetBy(postId))
				.Returns(postDetails);

			var controller = component.Build();

			// Act
			var viewResult = controller.Details(postId);

			// Assert
			Assert.IsInstanceOfType(viewResult.Model, typeof(PostDetailsModel));
			var model = (PostDetailsModel)viewResult.Model;
			Assert.AreEqual(postDetails, model.Post);
			Assert.AreEqual(postId, model.CommentPostModel.Id);
		}

		[TestMethod]
		public void GetEdit()
		{
			// Arrange
			var postId = 2109;
			var postDetails = new PostDetails { };

			var component = new Component();
			component.PostQuery
				.Setup(m => m.GetBy(postId))
				.Returns(postDetails);

			var controller = component.Build();

			// Act
			var viewResult = controller.Edit(postId);

			// Assert
			Assert.AreEqual(postDetails, viewResult.Model);
		}

		[TestMethod]
		public void PostEdit()
		{
			// Arrange
			var postId = 2109;
			var model = new PostEditModel
			{
				Title = "new title.",
				Body = "new body.",
			};

			var component = new Component();
			component.Bus
				.Setup(m => m.Send(It.IsAny<PostEditCommand>()));

			var controller = component.Build();

			// Act
			var actionResult = controller.Edit(postId, model);

			// Assert
			Assert.IsInstanceOfType(actionResult, typeof(RedirectToRouteResult));
			var redirectResult = (RedirectToRouteResult)actionResult;
			Assert.AreEqual("Index", redirectResult.RouteValues["action"]);

			component.Bus
				.Verify(m => m.Send(It.Is<PostEditCommand>(x =>
					x.PostId == postId &&
					x.Title == model.Title &&
					x.Body == model.Body)));
		}

		[TestMethod]
		public void PostEdit_Invalid()
		{
			// Arrange
			var postId = 2109;
			var model = new PostEditModel
			{
				Title = "new title.",
				Body = "new body.",
			};
			var postDetails = new PostDetails { };

			var component = new Component();
			component.PostQuery
				.Setup(m => m.GetBy(postId))
				.Returns(postDetails);

			var controller = component.Build();
			controller.ModelState.AddModelError("dummy error", "");

			// Act
			var actionResult = controller.Edit(postId, model);

			// Assert
			Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
			var viewResult = (ViewResult)actionResult;
			Assert.AreEqual(postDetails, viewResult.Model);
			Assert.AreEqual(model.Title, postDetails.Title);
			Assert.AreEqual(model.Body, postDetails.Body);

			component.Bus.Verify(m => m.Send(It.IsAny<PostEditCommand>()), Times.Never);
			component.PostQuery.Verify(m => m.GetBy(postId));
		}

		[TestMethod]
		public void GetDelete()
		{
			// Arrange
			var postId = 2109;
			var postDetails = new PostDetails { };

			var component = new Component();
			component.PostQuery
				.Setup(m => m.GetBy(postId))
				.Returns(postDetails);

			var controller = component.Build();

			// Act
			var viewResult = controller.Delete(postId);

			// Assert
			Assert.AreEqual(postDetails, viewResult.Model);
		}

		[TestMethod]
		public void PostDelete()
		{
			// Arrange
			var postId = 2109;
			var model = new PostDeleteModel { };

			var component = new Component();
			component.Bus
				.Setup(m => m.Send(It.IsAny<PostDeleteCommand>()));

			var controller = component.Build();

			// Act
			var actionResult = controller.Delete(postId, model);

			// Assert
			Assert.IsInstanceOfType(actionResult, typeof(RedirectToRouteResult));
			var redirectResult = (RedirectToRouteResult)actionResult;
			Assert.AreEqual("Index", redirectResult.RouteValues["action"]);

			component.Bus
				.Verify(m => m.Send(It.Is<PostDeleteCommand>(x =>
					x.PostId == postId)));
		}

		[TestMethod]
		public void PostDelete_Invalid()
		{
			// Arrange
			var postId = 2109;
			var model = new PostDeleteModel { };
			var postDetails = new PostDetails { };

			var component = new Component();
			component.PostQuery
				.Setup(m => m.GetBy(postId))
				.Returns(postDetails);

			var controller = component.Build();
			controller.ModelState.AddModelError("dummy error", "");

			// Act
			var actionResult = controller.Delete(postId, model);

			// Assert
			Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
			var viewResult = (ViewResult)actionResult;
			Assert.AreEqual(postDetails, viewResult.Model);

			component.Bus.Verify(m => m.Send(It.IsAny<PostDeleteCommand>()), Times.Never);
			component.PostQuery.Verify(m => m.GetBy(postId));
		}

		[TestMethod]
		public void PostComment()
		{
			// Arrange
			var postId = 2109;
			var model = new CommentPostModel { };

			var component = new Component();
			component.Bus
				.Setup(m => m.Send(It.IsAny<CommentPostCommand>()));

			var controller = component.Build();

			// Act
			var actionResult = controller.PostComment(postId, model);

			// Assert
			Assert.IsInstanceOfType(actionResult, typeof(RedirectToRouteResult));
			var redirectResult = (RedirectToRouteResult)actionResult;
			Assert.AreEqual("Details", redirectResult.RouteValues["action"]);
			Assert.AreEqual(postId, redirectResult.RouteValues["id"]);

			component.Bus.Verify(m => m.Send(It.IsAny<CommentPostCommand>()), Times.Once);
		}

		[TestMethod]
		public void PostComment_Invalid()
		{
			// Arrange
			var postId = 2109;
			var model = new CommentPostModel { };
			var postDetails = new PostDetails { };

			var component = new Component();
			component.PostQuery
				.Setup(m => m.GetBy(postId))
				.Returns(postDetails);

			var controller = component.Build();
			controller.ModelState.AddModelError("dummy error.", "");

			// Act
			var actionResult = controller.PostComment(postId, model);

			// Assert
			Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
			var viewResult = (ViewResult)actionResult;
			Assert.AreEqual("Details", viewResult.ViewName);

			var viewModel = (PostDetailsModel)viewResult.Model;
			Assert.AreEqual(model, viewModel.CommentPostModel);
			Assert.AreEqual(postDetails, viewModel.Post);

			component.Bus.Verify(m => m.Send(It.IsAny<CommentPostCommand>()), Times.Never);
		}

		private class Component
		{
			public Mock<ICommandBus> Bus = new Mock<ICommandBus>();
			public Mock<IPostQuery> PostQuery = new Mock<IPostQuery>();

			public PostController Build()
			{
				return new PostController(
					Bus.Object,
					PostQuery.Object);
			}
		}
	}
}
