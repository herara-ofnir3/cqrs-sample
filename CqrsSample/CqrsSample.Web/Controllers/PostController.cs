using CqrsSample.Core.Collections;
using CqrsSample.Core.Domain;
using CqrsSample.Domain.Posting;
using CqrsSample.Query.Posting;
using CqrsSample.Web.Models.Post;
using System.Web.Mvc;

namespace CqrsSample.Web.Controllers
{
	public class PostController : Controller
    {
		public PostController(ICommandBus bus, IPostQuery postQuery)
		{
			_bus = bus;
			_postQuery = postQuery;
		}

		private readonly ICommandBus _bus;
		private readonly IPostQuery _postQuery;

        // GET: Post
        public ViewResult Index(int? p)
        {
			var page = new Page(p ?? 1, 5);
			var model = new PostIndexModel
			{
				Page = page,
				Posts = _postQuery.GetSummaries(page)
			};
			
            return View(model);
        }

		[HttpGet]
		public ViewResult List(int? p)
		{
			var page = new Page(p ?? 1, 10);
			var model = new PostListModel
			{
				Page = page,
				Posts = _postQuery.GetPosts(page)
			};

			return View(model);
		}

		[HttpGet]
		public ViewResult New()
		{
			return View();
		}

		[HttpPost]
		public ActionResult New(NewPostModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var newPost = new NewPostCommand
			{
				Title = model.Title,
				Body = model.Body,
				IsDraft = model.IsDraft,
			};

			_bus.Send(newPost);
			return RedirectToAction("Index");
		}

		public ViewResult Details(int id)
		{
			var post = _postQuery.GetBy(id);
			var model = new PostDetailsModel
			{
				Post = post,
				CommentPostModel = new CommentPostModel { Id = id },
			};

			return View(model);
		}

		[HttpGet]
		public ViewResult Edit(int id)
		{
			var post = _postQuery.GetBy(id);
			return View(post);
		}

		[HttpPost]
		public ActionResult Edit(int id, PostEditModel model)
		{
			if (!ModelState.IsValid)
			{
				var post = _postQuery.GetBy(id);
				post.Title = model.Title;
				post.Body = model.Body;
				return View(post);
			}

			var command = new PostEditCommand
			{
				PostId = id,
				Title = model.Title,
				Body = model.Body
			};

			_bus.Send(command);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ViewResult Delete(int id)
		{
			var post = _postQuery.GetBy(id);
			var model = new PostDeleteModel
			{
				Post = post,
			};

			return View(model);
		}

		[HttpPost]
		public ActionResult Delete(int id, PostDeleteModel model)
		{
			if (!ModelState.IsValid)
			{
				var post = _postQuery.GetBy(id);
				var vm = new PostDeleteModel
				{
					Post = post,
				};
				return View(vm);
			}

			var command = new PostDeleteCommand { PostId = id };
			_bus.Send(command);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult PostComment(int id, CommentPostModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Id = id;
				var detailsModel = new PostDetailsModel
				{
					CommentPostModel = model,
					Post = _postQuery.GetBy(id),
				};

				return View("Details", detailsModel);
			}

			var command = new CommentPostCommand
			{
				PostId = id,
				Name = model.Name,
				Body = model.Body,
			};

			_bus.Send(command);
			return RedirectToAction("Details", new { id });
		}
	}
}