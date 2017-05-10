using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CqrsSample.Web.Models.Post
{
	public class CommentPostModel
	{
		public int Id { get; set; }

		[DisplayName("名前")]
		[Required]
		[StringLength(50)]
		public string Name { get; set; } = "名無しさん";

		[DisplayName("コメント")]
		[Required]
		[StringLength(1024)]
		public string Body { get; set; }
	}
}