using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BidCraft.web.Startup))]
namespace BidCraft.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
