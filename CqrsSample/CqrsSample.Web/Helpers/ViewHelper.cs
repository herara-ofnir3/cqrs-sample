using CqrsSample.Core;
using CqrsSample.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CqrsSample.Web.Helpers
{
	public static class ViewHelper
	{
		public static MvcHtmlString ReplaceBreak(this HtmlHelper html, string s)
		{
			return MvcHtmlString.Create(html.Encode(s).Replace("\n", "<br />"));
		}

		public static string PageUrl(this WebViewPage<PagerModel> pagerView, int page)
		{
			var routeValues = new RouteValueDictionary(pagerView.Url.RequestContext.RouteData.Values);
			var q = pagerView.Request.QueryString;
			foreach (string k in q.AllKeys)
			{
				routeValues.Add(k, q[k]);
			}

			var model = pagerView.Model;
			if (!routeValues.Keys.Contains(model.QueryName))
			{
				routeValues.Add(model.QueryName, page.ToString());
			}
			else
			{
				routeValues[model.QueryName] = page.ToString();
			}

			return pagerView.Url.RouteUrl(routeValues);
		}

		public static string TimeAgo(this DateTime value)
		{
			var threshold = new Dictionary<TimeSpan, Func<TimeSpan, string>>
			{
				{ TimeSpan.FromMinutes(1), t => string.Format("{0}秒前", (int)t.TotalSeconds) },
				{ TimeSpan.FromHours(1), t => string.Format("{0}分前", (int)t.TotalMinutes) },
				{ TimeSpan.FromDays(1), t => string.Format("{0}時間前", (int)t.TotalHours) },
				{ TimeSpan.FromDays(31), t => string.Format("{0}日前", (int)t.TotalDays) },
				{ TimeSpan.FromDays(365), t => string.Format("{0}ヶ月前", (int)(t.TotalDays / 31)) },
			};

			var diff = Clock.Now - value;
			foreach (var pair in threshold)
			{
				if (diff < pair.Key)
				{
					return pair.Value(diff);
				}
			}

			return string.Format("{0}年前", (int)(diff.TotalDays / 365));
		}
	}
}