using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GST.Startup))]
namespace GST
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
