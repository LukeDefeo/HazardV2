using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalRHost.Startup))]

namespace SignalRHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();    
        }
    }
}
