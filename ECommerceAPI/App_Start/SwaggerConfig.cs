using ECommerceAPI;
using Swashbuckle.Application;
using System.Web;
using System.Web.Http;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
namespace ECommerceAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c => c.SingleApiVersion("v1", "ECommerceAPI"))
                .EnableSwaggerUi();
        }
    }
}
