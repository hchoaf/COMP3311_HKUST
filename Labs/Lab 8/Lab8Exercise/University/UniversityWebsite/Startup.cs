using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UniversityWebsite.Startup))]
namespace UniversityWebsite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
