using System.Collections.Generic;

namespace CqrsSample.Domain.Posting
{
	/// <summary>
	/// 記事の状態
	/// </summary>
	public enum PostStatus
	{
		/// <summary>
		/// 下書き
		/// </summary>
		Draft = 0,

		/// <summary>
		/// 公開
		/// </summary>
		Public = 1
	}

	public static class PostStatusHelper
	{
		public static string ToDisplay(this PostStatus value)
		{
			return _displayNames[value];
		}

		private static readonly IDictionary<PostStatus, string> _displayNames = new Dictionary<PostStatus, string>
		{
			{ PostStatus.Draft, "下書き" },
			{ PostStatus.Public, "公開" },
		};
	}
}
