using CqrsSample.Core;
using CqrsSample.Core.Domain;
using System;
using System.Collections.Generic;

namespace CqrsSample.Domain.Posting
{
	/// <summary>
	/// 投稿された記事
	/// </summary>
	public class Post : Entity<int>
	{
		public Post()
		{
			PostedAt = Clock.Now;
			Comments = new List<Comment>();
		}

		public virtual byte[] Version { get; set; }

		/// <summary>
		/// 記事のタイトル
		/// </summary>
		public virtual string Title { get; set; }

		/// <summary>
		/// 記事の本文
		/// </summary>
		public virtual string Body { get; set; }

		/// <summary>
		/// 投稿日時
		/// </summary>
		public virtual DateTime PostedAt { get; set; }

		/// <summary>
		/// 更新日時
		/// </summary>
		public virtual DateTime? UpdatedAt { get; set; }

		/// <summary>
		/// 記事の状態
		/// </summary>
		public virtual PostStatus Status { get; set; }

		/// <summary>
		/// 記事についたコメント
		/// </summary>
		public virtual IList<Comment> Comments { get; set; }

		public virtual void Edit(string title, string body)
		{
			Title = title;
			Body = body;
			UpdatedAt = Clock.Now;
		}

		public virtual void Public()
		{
			if (Status != PostStatus.Draft)
			{
				throw new InvalidOperationException("既に公開されている記事を公開しようとしました。");
			}

			Status = PostStatus.Public;
			UpdatedAt = Clock.Now;
		}

		public virtual void RevertToDraft()
		{
			if (Status != PostStatus.Public)
			{
				throw new InvalidOperationException("まだ公開されていない記事を下書きに戻そうとしました。");
			}

			Status = PostStatus.Draft;
			UpdatedAt = Clock.Now;
		}

		public virtual void PostComment(string name, string body)
		{
			Comments.Add(new Comment
			{
				Post = this,
				Name = name,
				Body = body,
			});
		}
	}
}
