using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NSEBACKCOPY.Startup))]
namespace NSEBACKCOPY
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
