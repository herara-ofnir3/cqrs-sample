using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CqrsSample.Web.Startup))]
namespace CqrsSample.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
