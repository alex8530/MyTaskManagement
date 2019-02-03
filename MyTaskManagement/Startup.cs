using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyTaskManagement.Startup))]
namespace MyTaskManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
