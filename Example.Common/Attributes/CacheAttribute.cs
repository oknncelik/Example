#region

using System;
using System.Linq;
using Castle.DynamicProxy;
using Example.Common.Cachings.Abstract;
using Example.Common.Constants;
using Example.Common.Enums;
using Example.Common.Helpers;
using Example.Common.Intercepters;
using Example.Common.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

#endregion

namespace Example.Common.Attributes
{
    public class CacheAttribute : MethodInterception
    {
        private readonly ICacheManager _cacheManager;
        private readonly int _duration = 60;
        private readonly string _keyOrPattern;
        private readonly Cache _operation;

        /// <summary>
        ///     Cach oluşturmak için kullanılır.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="duration"></param>
        public CacheAttribute(Cache operation, int duration = 60)
        {
            if (operation != Cache.AddOrGet) throw new Exception(Messages.CacheAddErrorMessage);
            _duration = duration;
            _operation = operation;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        /// <summary>
        ///     Cach temizlemek için kullanılır.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="keyOrPattern"></param>
        public CacheAttribute(Cache operation, string keyOrPattern)
        {
            if (operation != Cache.Remove) throw new Exception(Messages.CacheClearErrorMessage);
            _operation = operation;
            _keyOrPattern = keyOrPattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = $"{invocation.Method.ReflectedType?.FullName}.{invocation.Method.Name}";
            var args = invocation.Arguments.ToList();
            var key =
                $"{methodName}({string.Join(",", args.Select(arg => JsonConvert.SerializeObject(arg ?? "<NULL>")))})";

            if (_operation == Cache.Remove)
            {
                _cacheManager.RemoveByPattern(_keyOrPattern.IsNotEmpty() ? _keyOrPattern : methodName);
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