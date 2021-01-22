using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Example.Common.Cachings.Abstract;
using Example.Common.Ioc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Common.Cachings
{
    public class MemoryCacheManager : ICacheManager
    {
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
            Cache.Set(key, data, TimeSpan.FromMinutes(duration));
        }

        public bool IsAdded(string key)
        {
            return Cache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var definition = typeof(MemoryCache).GetProperty("EntriesCollection",
                BindingFlags.NonPublic | BindingFlags.Instance);

            if (definition is null) return;
            var items = definition.GetValue(Cache) as dynamic;
            var values = new List<ICacheEntry>();

            foreach (var cacheItem in items)
                values.Add(cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null));

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = values.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key)
                .ToList();
        }
    }
}