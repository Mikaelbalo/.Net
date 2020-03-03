using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Swedating.Startup))]
namespace Swedating
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        
    }
}
