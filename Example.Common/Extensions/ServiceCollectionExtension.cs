using Example.Common.Ioc;
using Example.Common.Ioc.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Common.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, ICoreModule[] modules)
        {
            foreach (var module in modules)
                module.Load(services);
            return ServiceTool.Create(services);
        }
    }
}