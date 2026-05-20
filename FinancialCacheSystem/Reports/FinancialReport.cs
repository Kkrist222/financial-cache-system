using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCacheSystem.Reports;

/// <summary>
/// Финансовый отчёт с поддержкой клонирования (паттерн Prototype).
/// </summary>
public class FinancialReport : ICloneable
{
    /// <summary>
    /// Заголовок отчёта.
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    /// Данные отчёта (ключ-значение).
    /// </summary>
    public Dictionary<string, decimal> Data { get; } = new();

    /// <summary>
    /// Добавляет данные в отчёт.
    /// </summary>
    /// <param name="key">Ключ данных</param>
    /// <param name="value">Значение данных</param>
    public void AddData(string key, decimal value)
    {
        Data[key] = value;
    }

    /// <summary>
    /// Создаёт глубокую копию отчёта (клонирование).
    /// </summary>
    /// <returns>Копия отчёта</returns>
    public object Clone()
    {
        var clone = new FinancialReport();
        clone.Title = this.Title;

        // Копируем все данные
        foreach (var item in this.Data)
        {
            clone.Data[item.Key] = item.Value;
        }

        return clone;
    }
}