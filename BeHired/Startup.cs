using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeHired.Startup))]
namespace BeHired
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
