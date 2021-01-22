using System.Linq;
using Castle.DynamicProxy;
using Example.Common.Cachings.Abstract;
using Example.Common.Enums;
using Example.Common.Helpers;
using Example.Common.Intercepters;
using Example.Common.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Example.Common.Attributes
{
    public class CacheAttribute : MethodInterception
    {
        private readonly ICacheManager _cacheManager;
        private readonly int _duration = 60;
        private readonly Cache _operation;
        private readonly string _keyOrPattern;
        
        public CacheAttribute(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        
        public CacheAttribute(Cache operation, string keyOrPattern = "") 
        {
            _operation = operation;
            _keyOrPattern = keyOrPattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = $"{invocation.Method.ReflectedType?.FullName}.{invocation.Method.Name}";
            var args = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", args.Select(x => JsonConvert.SerializeObject(x ?? "<NULL>")))})";
            
            if (_operation == Cache.Remove)
            {
                _cacheManager.RemoveByPattern(_keyOrPattern.IsNotEmpity() ? _keyOrPattern : methodName);
                invocation.Proceed();
            }
            else
            {
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