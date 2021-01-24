#region

using Example.Common.Cachings;
using Example.Common.Cachings.Abstract;
using Example.Common.Ioc.Abstract;
using Example.Common.Logs.Abstract;
using Example.Common.Logs.Concreate;
using Example.Dal.Abstract.Repositories;
using Example.Dal.Concreate.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Example.Common.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILogRepository, LogRepository>();
            services.AddSingleton<ILogManager, LogManager>();
        }
    }
}