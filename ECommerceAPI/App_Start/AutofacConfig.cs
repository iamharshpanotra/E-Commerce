using Autofac;
using Autofac.Integration.WebApi;
using ECommerceAPI.Core.Interfaces;
using ECommerceAPI.Core.Interfaces.Service;
using ECommerceAPI.Infrastructure.Data;
using ECommerceAPI.Infrastructure.Repositories;
using System.Reflection;
using System.Web.Http;

public static class AutofacConfig
{
    public static void RegisterDependencies()
    {
        var builder = new ContainerBuilder();

        // Register Web API controllers
        builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

        // Register DbContext
        builder.RegisterType<ApplicationDbContext>()
               .InstancePerRequest();

        // Register Repository & Service
        builder.RegisterType<ProductRepository>()
               .As<IProductRepository>()
               .InstancePerRequest();

        builder.RegisterType<ProductService>()
               .As<IProductService>()
               .InstancePerRequest();

        // Build container
        var container = builder.Build();
        var resolver = new AutofacWebApiDependencyResolver(container);
        GlobalConfiguration.Configuration.DependencyResolver = resolver;
    }
}
