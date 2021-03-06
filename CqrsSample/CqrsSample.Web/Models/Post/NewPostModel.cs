﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CqrsSample.Web.Models.Post
{
	public class NewPostModel
	{
		[DisplayName("タイトル")]
		[Required]
		[MaxLength(255)]
		public string Title { get; set; }

		[DisplayName("内容")]
		[Required]
		public string Body { get; set; }

		[DisplayName("下書きで保存")]
		public bool IsDraft { get; set; }
	}
}