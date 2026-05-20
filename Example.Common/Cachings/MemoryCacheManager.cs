#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Example.Common.Cachings.Abstract;
using Example.Common.Ioc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Example.Common.Cachings
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly List<string> _keys = new();

        public MemoryCacheManager()
        {
            Cache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public IMemoryCache Cache { get; }

        public T Get<T>(string key)
        {
            return Cache.Get<T>(key);
        }

        public object Get(string key)
        {
            return Cache.Get(key);
        }

        public void Add(string key, object data, int duration)
        {
            if (!_keys.Contains(key)) _keys.Add(key);
            Cache.Set(key, data, TimeSpan.FromMinutes(duration));
        }

        public bool IsAdded(string key)
        {
            return Cache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _keys.Remove(key);
            Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var keysToRemove = _keys.Where(k => k.Contains(pattern)).ToList();
            foreach (var key in keysToRemove)
            {
                Remove(key);
            }
        }
    }
}