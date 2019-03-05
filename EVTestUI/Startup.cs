using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute("EVTestUIConfig",typeof(EVTestUI.Startup))]
namespace EVTestUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
