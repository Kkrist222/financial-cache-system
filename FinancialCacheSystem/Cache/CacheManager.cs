using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCacheSystem.Cache;

// Singleton - глобальный менеджер кэша
public sealed class CacheManager
{
    private static readonly Lazy<CacheManager> _instance =
        new(() => new CacheManager());

    public static CacheManager Instance => _instance.Value;

    private readonly ConcurrentDictionary<string, (object Value, DateTime Expires)> _cache = new();

    private CacheManager() { }

    // Сохранить в кэш
    public void Set(string key, object value, int minutes = 15)
    {
        var expires = DateTime.UtcNow.AddMinutes(minutes);
        _cache[key] = (value, expires);
    }

    // Получить из кэша
    public bool TryGet(string key, out object? value)
    {
        if (_cache.TryGetValue(key, out var entry))
        {
            if (entry.Expires > DateTime.UtcNow)
            {
                value = entry.Value;
                return true;
            }
            _cache.TryRemove(key, out _);
        }
        value = null;
        return false;
    }

    // Очистить кэш
    public void Clear() => _cache.Clear();
}