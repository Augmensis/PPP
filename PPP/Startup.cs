using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PPP.Startup))]
namespace PPP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
