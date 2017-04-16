using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NMCT.Startup))]
namespace NMCT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
