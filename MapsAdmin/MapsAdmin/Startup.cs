using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(MapsAdmin.Startup))]
namespace MapsAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        
    }
}
