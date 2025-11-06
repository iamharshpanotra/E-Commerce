using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ECommerceAPI.Startup))]
namespace ECommerceAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // OWIN pipeline configuration can go here
            // Example: app.UseCors(CorsOptions.AllowAll);
        }
    }
}
