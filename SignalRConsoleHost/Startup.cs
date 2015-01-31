using Microsoft.Owin.Cors;
using Owin;

namespace SignalRConsoleHost
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}