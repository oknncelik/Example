using System.Linq;
using Castle.DynamicProxy;
using Example.Common.Cachings.Abstract;
using Example.Common.Intercepters;
using Example.Common.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Common.Attributes
{
    public class CacheAttribute : MethodInterception
    {
        private readonly ICacheManager _cacheManager;
        private readonly int _duration;

        public CacheAttribute(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            if (invocation.Method.ReflectedType is { })
            {
                var methodName = $"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}";
                var args = invocation.Arguments.ToList();
                var key = $"{methodName}({string.Join(",", args.Select(x => x?.ToString() ?? "<NULL>"))})";
                if (_cacheManager.IsAdded(key))
                {
                    invocation.ReturnValue = _cacheManager.Get(key);
                }
                else
                {
                    invocation.Proceed();
                    _cacheManager.Add(key, invocation.ReturnValue, _duration);
                }
            }
        }
    }
}