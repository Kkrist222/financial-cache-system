using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCacheSystem.Reports;

// Prototype - шаблон отчёта с клонированием
public class FinancialReport : ICloneable
{
    public string Title { get; set; } = "";

    public Dictionary<string, decimal> Data { get; } = new();

    // Метод для добавления данных
    public void AddData(string key, decimal value)
    {
        Data[key] = value;
    }

    // Глубокое клонирование (Prototype)
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