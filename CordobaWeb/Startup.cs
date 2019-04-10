using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CordobaWeb.Startup))]
namespace CordobaWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
