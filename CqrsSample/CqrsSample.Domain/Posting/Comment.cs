using CqrsSample.Core;
using CqrsSample.Core.Domain;
using System;

namespace CqrsSample.Domain.Posting
{
	/// <summary>
	/// 記事へのコメント
	/// </summary>
	public class Comment : Entity<int>
	{
		public Comment()
		{
			PostedAt = Clock.Now;
		}

		/// <summary>
		/// コメント先の記事
		/// </summary>
		public virtual Post Post { get; set; }

		/// <summary>
		/// コメントしたユーザの名前
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// コメント本文
		/// </summary>
		public virtual string Body { get; set; }

		/// <summary>
		/// コメントの投稿日時
		/// </summary>
		public virtual DateTime PostedAt { get; set; }

		/// <summary>
		/// コメントの編集日時
		/// </summary>
		public virtual DateTime? UpdatedAt { get; set; }
	}
}
