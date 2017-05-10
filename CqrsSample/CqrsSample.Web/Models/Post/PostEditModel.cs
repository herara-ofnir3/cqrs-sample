using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CqrsSample.Web.Models.Post
{
	public class PostEditModel
	{
		public int Id { get; set; }

		[DisplayName("タイトル")]
		[Required]
		[MaxLength(255)]
		public string Title { get; set; }

		[DisplayName("内容")]
		[Required]
		public string Body { get; set; }
	}
}