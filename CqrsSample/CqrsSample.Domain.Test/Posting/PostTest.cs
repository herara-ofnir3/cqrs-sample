using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CqrsSample.Domain.Posting;
using CqrsSample.Core;

namespace CqrsSample.Domain.Test.Posting
{
	[TestClass]
	public class PostTest
	{
		[TestMethod]
		public void Edit()
		{
			// Arrange
			var post = new Post();
			post.Title = "old post title.";
			post.Body = "old body title.";

			var now = new DateTime(2016, 1, 1, 10, 23, 59);

			var newTitle = "new post title.";
			var newBody = "new post body";

			// Act
			using (Clock.NowIs(now))
			{
				post.Edit(newTitle, newBody);
			}

			// Assert
			Assert.AreEqual(newTitle, post.Title);
			Assert.AreEqual(newBody, post.Body);
			Assert.AreEqual(now, post.UpdatedAt);
		}

		[TestMethod]
		public void Public()
		{
			// Arrange
			var post = new Post();
			post.Status = PostStatus.Draft;

			var now = new DateTime(2016, 1, 1, 10, 23, 59);

			// Act
			using (Clock.NowIs(now))
			{
				post.Public();
			}

			// Assert
			Assert.AreEqual(PostStatus.Public, post.Status);
			Assert.AreEqual(now, post.UpdatedAt);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Public_InvalidStatus()
		{
			var post = new Post();
			post.Status = PostStatus.Public;
			post.Public();
		}

		[TestMethod]
		public void RevertToDraft()
		{
			// Arrange
			var post = new Post();
			post.Public();

			var now = new DateTime(2016, 1, 1, 10, 23, 59);

			// Act
			using (Clock.NowIs(now))
			{
				post.RevertToDraft();
			}

			// Assert
			Assert.AreEqual(PostStatus.Draft, post.Status);
			Assert.AreEqual(now, post.UpdatedAt);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void RevertToDraft_InvalidStatus()
		{
			var post = new Post();
			post.Status = PostStatus.Draft;
			post.RevertToDraft();
		}

		[TestMethod]
		public void PostComment()
		{
			var post = new Post();
			Assert.AreEqual(0, post.Comments.Count);

			post.PostComment("name", "comment body.");
			Assert.AreEqual(1, post.Comments.Count);

			var posted = post.Comments[0];
			Assert.AreEqual("name", posted.Name);
			Assert.AreEqual("comment body.", posted.Body);
		}
	}
}
