using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCacheSystem.Cache;

/// <summary>
/// Глобальный менеджер кэша (паттерн Singleton).
/// Обеспечивает потокобезопасное хранение результатов вычислений.
/// </summary>
public sealed class CacheManager : ICacheManager
{
    private static readonly Lazy<CacheManager> _instance = new(() => new CacheManager());
    private readonly ConcurrentDictionary<string, (object Value, DateTime Expires)> _cache = new();

    /// <summary>
    /// Единственный экземпляр менеджера кэша.
    /// </summary>
    public static CacheManager Instance => _instance.Value;

    private CacheManager() { }

    /// <summary>
    /// Сохраняет значение в кэш с опциональным временем жизни.
    /// </summary>
    /// <param name="key">Уникальный ключ для доступа к значению</param>
    /// <param name="value">Значение для сохранения в кэш</param>
    /// <param name="minutes">Время жизни значения в минутах (по умолчанию 15)</param>
    public void Set(string key, object value, int minutes = 15)
    {
        _cache[key] = (value, DateTime.UtcNow.AddMinutes(minutes));
    }

    /// <summary>
    /// Получает значение из кэша по ключу.
    /// </summary>
    /// <param name="key">Ключ значения</param>
    /// <param name="value">Извлечённое значение (если найдено)</param>
    /// <returns>True, если значение найдено и не истекло</returns>
    public bool TryGet(string key, out object value)
    {
        if (_cache.TryGetValue(key, out var cached) && cached.Expires > DateTime.UtcNow)
        {
            value = cached.Value;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Очищает все записи в кэше.
    /// </summary>
    public void Clear()
    {
        _cache.Clear();
    }
}

/// <summary>
/// Интерфейс менеджера кэша.
/// </summary>
public interface ICacheManager
{
    /// <summary>
    /// Сохраняет значение в кэш.
    /// </summary>
    void Set(string key, object value, int minutes = 15);

    /// <summary>
    /// Получает значение из кэша.
    /// </summary>
    bool TryGet(string key, out object value);

    /// <summary>
    /// Очищает кэш.
    /// </summary>
    void Clear();
}