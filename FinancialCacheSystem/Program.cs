using FinancialCacheSystem.Cache;
using FinancialCacheSystem.Reports;

Console.WriteLine("=== Финансовая система с кэшированием ===\n");

// 1. Singleton - получаем единственный экземпляр кэша
var cache = CacheManager.Instance;

Console.WriteLine(" Первый расчёт (медленно, без кэша)...");
var report1 = CalculateReport("Q1 2026");
cache.Set("report_q1", report1, 15);
Console.WriteLine($" {report1.Title} | Прибыль: {report1.Data["Profit"]}");

Console.WriteLine("\n Второй расчёт (быстро, из кэша)...");
if (cache.TryGet("report_q1", out var cached))
{
    var report2 = (FinancialReport)cached;
    Console.WriteLine($" {report2.Title} | Прибыль: {report2.Data["Profit"]}");
}

Console.WriteLine("\n Prototype - клонирование шаблона:");
var template = new FinancialReport();
template.Title = "Шаблон";
template.AddData("Base", 1000m);

var clone = (FinancialReport)template.Clone();
clone.Title = "Копия";
clone.AddData("New", 999m);

Console.WriteLine($"Оригинал: {template.Title} (данных: {template.Data.Count})");
Console.WriteLine($"Копия: {clone.Title} (данных: {clone.Data.Count})");

Console.WriteLine("\n Готово! Нажмите любую клавишу...");
Console.ReadKey();

// Метод расчёта отчёта
static FinancialReport CalculateReport(string quarter)
{
    System.Threading.Thread.Sleep(500); // имитация вычислений
    var report = new FinancialReport();
    report.Title = $"Отчёт за {quarter}";
    report.AddData("Revenue", 1500000m);
    report.AddData("Profit", 320000m);
    return report;
}