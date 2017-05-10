using CqrsSample.Core.Collections;
using System;
using System.Linq;

namespace CqrsSample.Web.Models
{
	public class PagerModel
	{
		public string QueryName { get; set; } = "p";

		public Page Page { get; set; }

		public IPaged Items { get; set; }

		public int TotalCount { get { return Items.TotalCount; } }

		public int TotalPages { get { return (int)Math.Ceiling(TotalCount / (double)Page.PerPage); } }

		public bool IsFirst
		{
			get { return Page.Number == 1; }
		}

		public bool IsLast
		{
			get { return Page.Number == Last; }
		}

		public int First { get { return 1; } }

		public int Prev {
			get
			{
				if (IsFirst) return First;
				return Page.Number - 1;
			}
		}

		public int Next
		{
			get
			{
				if (IsLast) return Last;
				return Page.Number + 1;
			}
		}

		public int Last { get { return TotalPages; } }

		public int StartIndex { get { return Page.Offset; } }

		public int LastIndex
		{
			get
			{
				if (IsLast)
				{
					return TotalCount - 1;
				}

				return StartIndex + Page.PerPage - 1;
			}
		}
	}

	public static class PagedHelper
	{
		public static PagerModel ToPagerModel(this IPaged paged, Page page)
		{
			return new PagerModel { Items = paged, Page = page };
		}
	}
}