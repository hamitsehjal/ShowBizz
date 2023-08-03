using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HS2231A5.Startup))]

namespace HS2231A5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
