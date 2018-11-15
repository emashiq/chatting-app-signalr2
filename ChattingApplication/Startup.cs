using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChattingApplication.Startup))]
namespace ChattingApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
