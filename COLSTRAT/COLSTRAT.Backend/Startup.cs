using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(COLSTRAT.Backend.Startup))]
namespace COLSTRAT.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
