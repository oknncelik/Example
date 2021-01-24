#region

using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Example.Common.Ioc.Abstract
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}