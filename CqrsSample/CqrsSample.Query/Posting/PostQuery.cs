using System;
using CqrsSample.Core.Collections;
using CqrsSample.Core.Data;
using Dapper;
using System.Linq;

namespace CqrsSample.Query.Posting
{
	/// <summary>
	/// 投稿された記事を参照する機能を提供します。
	/// </summary>
	public interface IPostQuery
	{
		PostDetails GetBy(int id);

		/// <summary>
		/// 記事の概要を取得します。ページ処理を行って結果を返します。
		/// </summary>
		/// <param name="page"></param>
		/// <returns></returns>
		IPaged<PostSummary> GetSummaries(Page page);

		IPaged<PostInfo> GetPosts(Page page);
	}

	public class PostQuery : DbQuery, IPostQuery
	{
		public PostQuery(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
		{
		}

		public PostDetails GetBy(int id)
		{
			using (var connection = CreateConnection())
			{
				var sql = @"select * from Posts where Id = @id";
				var post = connection.Query<PostDetails>(sql, new { id }).FirstOrDefault();

				if (post == null)
				{
					return null;
				}

				var commentsSql = @"select * from Comments where PostId = @postId order by PostedAt asc";
				var comments = connection.Query<CommentDto>(commentsSql, new { postId = id });
				post.Comments = comments.ToList();

				return post;
			}
		}

		public IPaged<PostSummary> GetSummaries(Page page)
		{
			using (var connection = CreateConnection())
			{
				var sql = @"
					select 
						p.Id,
						p.Title,
						substring(p.Body, 1, 500) Body,
						p.PostedAt,
						count(c.Id) CommentCount

					from 
						Posts p
						left join Comments c on
							p.Id = c.PostId

					where
						[Status] = 1

					group by
						p.Id,
						p.Title,
						p.Body,
						p.PostedAt

					order by
						p.Id desc

						offset @offset rows
						fetch next @perPage rows only";

				var countSql = @"
					select 
						count(*) PostCount

					from 
						Posts p

					where
						[Status] = 1";

				var posts = connection.Query<PostSummary>(sql, new { offset = page.Offset, perPage = page.PerPage });
				var totalCount = connection.ExecuteScalar<int>(countSql);

				return new Paged<PostSummary>(posts, totalCount);
			}
		}


		public IPaged<PostInfo> GetPosts(Page page)
		{
			using (var connection = CreateConnection())
			{
				var sql = @"
					select 
						p.Id,
						p.Title,
						p.PostedAt,
						p.UpdatedAt,
						count(c.Id) CommentCount,
						p.[Status]

					from 
						Posts p
						left join Comments c on
							p.Id = c.PostId

					group by
						p.Id,
						p.Title,
						p.Body,
						p.PostedAt,
						p.UpdatedAt,
						p.[Status]

					order by
						p.Id desc

						offset @offset rows
						fetch next @perPage rows only";

				var countSql = @"
					select 
						count(*) PostCount

					from 
						Posts p";

				var posts = connection.Query<PostInfo>(sql, new { offset = page.Offset, perPage = page.PerPage });
				var totalCount = connection.ExecuteScalar<int>(countSql);

				return new Paged<PostInfo>(posts, totalCount);
			}
		}

	}
}
