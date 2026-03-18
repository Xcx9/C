using System;
using System.Collections.Generic;
using System.Linq;

var station = new ServiceStation();

for (int i = 1; i <= 10; i++)
{
    new Manager(station, i);
}

Console.WriteLine("\nВызов GC.Collect()...");
GC.Collect();
GC.WaitForPendingFinalizers();
Console.WriteLine("GC завершил сборку.\n");

station.CompleteService("A123BC");

Console.WriteLine("\nНажмите любую клавишу для продолжения...");
Console.ReadKey();

Console.WriteLine("\n=== ЧАСТЬ 2: Отписка через IDisposable ===\n");

{
    using (var manager11 = new Manager(station, 11))
    {
        station.CompleteService("B456DE");
    }
    Console.WriteLine("Блок using закончен, manager11 отписан.");
}

Console.WriteLine("\nВызов GC.Collect() после отписки...");
GC.Collect();
GC.WaitForPendingFinalizers();
Console.WriteLine("GC завершил сборку.\n");

station.CompleteService("C789FG");

Console.WriteLine("\n=== ЧАСТЬ 3: Обработка коллекции запчастей с помощью Func ===\n");

var parts = new List<Part>
{
    new Part("Тормозные колодки", 1500m, true),
    new Part("Масляный фильтр", 300m, false),
    new Part("Ремень ГРМ", 2200m, true),
    new Part("Свеча зажигания", 400m, false),
};

static IEnumerable<decimal> CalculatePrices(IEnumerable<Part> parts, Func<Part, decimal> calculator)
{
    return parts.Select(calculator);
}

Console.WriteLine("Лямбда 1: наценка на оригинал (+30%), скидка на аналог (-10%):");
var prices1 = CalculatePrices(parts, p => p.IsOriginal ? p.BasePrice * 1.3m : p.BasePrice * 0.9m);
foreach (var (part, price) in parts.Zip(prices1))
{
    Console.WriteLine($"{part.Name,-20} ({(part.IsOriginal ? "оригинал" : "аналог")}) база: {part.BasePrice,6:C} -> итог: {price,6:C}");
}

Console.WriteLine("\nЛямбда 2: фиксированная наценка +500 руб:");
var prices2 = CalculatePrices(parts, p => p.BasePrice + 500m);
foreach (var (part, price) in parts.Zip(prices2))
{
    Console.WriteLine($"{part.Name,-20} ({(part.IsOriginal ? "оригинал" : "аналог")}) база: {part.BasePrice,6:C} -> итог: {price,6:C}");
}

decimal discount = 0.15m;
Console.WriteLine($"\nЛямбда 3 (с замыканием): применяем скидку {discount:P0} ко всем запчастям:");
var prices3 = CalculatePrices(parts, p => p.BasePrice * (1 - discount));
foreach (var (part, price) in parts.Zip(prices3))
{
    Console.WriteLine($"{part.Name,-20} ({(part.IsOriginal ? "оригинал" : "аналог")}) база: {part.BasePrice,6:C} -> итог: {price,6:C}");
}

Console.WriteLine("\nПрограмма завершена.");
