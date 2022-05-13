using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(xtbdEcommerceAdminPanel.Startup))]
namespace xtbdEcommerceAdminPanel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
