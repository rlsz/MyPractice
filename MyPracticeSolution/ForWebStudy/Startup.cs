using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ForWebStudy.Startup))]
namespace ForWebStudy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
