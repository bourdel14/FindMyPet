using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FindMyPet.Startup))]
namespace FindMyPet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
