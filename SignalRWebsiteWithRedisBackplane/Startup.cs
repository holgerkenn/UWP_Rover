using Microsoft.Owin;
using Owin;
using StackExchange.Redis;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Redis;
using SignalRWebsiteWithRedisBackplane;

namespace SignalRWebsiteWithRedisBackplane
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            var configuration = new RedisScaleoutConfiguration(/*<connection string goes here*/"", "AppName");
            GlobalHost.DependencyResolver.UseRedis(configuration);
            app.MapSignalR();
        }
    }
}

