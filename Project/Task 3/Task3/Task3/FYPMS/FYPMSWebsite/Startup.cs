using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FYPMSWebsite.Startup))]
namespace FYPMSWebsite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
