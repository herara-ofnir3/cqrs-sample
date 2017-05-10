using CqrsSample.Core.Config;

namespace CqrsSample.Web
{
	public class WebAppConfig : IAppConfig
	{
		public string DefaultConnection
		{
			get { return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; }
		}
	}
}