using Example.Common.Cachings;
using Example.Common.Cachings.Abstract;
using Example.Common.Ioc.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Common.DependencyResolvers
{
    public class CoreModule :ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}