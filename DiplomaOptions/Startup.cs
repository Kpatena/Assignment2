using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DiplomaOptions.Startup))]
namespace DiplomaOptions
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
