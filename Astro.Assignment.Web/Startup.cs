using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Astro.Assignment.Web.Startup))]
namespace Astro.Assignment.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
