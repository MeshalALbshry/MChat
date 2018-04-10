using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mohdthat.Startup))]
namespace Mohdthat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
