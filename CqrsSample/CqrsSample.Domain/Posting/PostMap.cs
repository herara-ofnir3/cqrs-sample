using FluentNHibernate.Mapping;

namespace CqrsSample.Domain.Posting
{
	public class PostMap : ClassMap<Post>
	{
		public PostMap()
		{
			Table("Posts");
			Id(x => x.Id).GeneratedBy.Identity();
			Version(x => x.Version).Generated.Always().CustomSqlType("BinaryBlob");
			Map(x => x.Title);
			Map(x => x.Body);
			Map(x => x.PostedAt);
			Map(x => x.UpdatedAt);
			Map(x => x.Status).CustomType<PostStatus>();
			HasMany(x => x.Comments).Inverse().Cascade.AllDeleteOrphan();
		}
	}

	public class CommentMap : ClassMap<Comment>
	{
		public CommentMap()
		{
			Table("Comments");
			Id(x => x.Id).GeneratedBy.Identity();
			Map(x => x.Name);
			Map(x => x.Body);
			Map(x => x.PostedAt);
			Map(x => x.UpdatedAt);
			References(x => x.Post).Column("PostId");
		}
	}
}
