using Microsoft.Extensions.DependencyInjection;

namespace Example.Common.Ioc.Abstract
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}