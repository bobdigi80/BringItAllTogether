using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BringingItAllTogether.Startup))]
namespace BringingItAllTogether
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
