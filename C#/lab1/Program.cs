using System;
using System.Diagnostics;

namespace Lab1_Variant3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Лабораторная работа 1. Вариант 3 ===");
            Console.WriteLine();

            // 1. Создание объекта ResearchTeam и вывод ToShortString()
            Console.WriteLine("1. ToShortString():");
            ResearchTeam team = new ResearchTeam(
                "Исследование ИИ в медицине",
                "НИИ Кибернетики",
                772831,
                TimeFrame.TwoYears
            );
            Console.WriteLine(team.ToShortString());
            Console.WriteLine();

            // 2. Вывод значений индексатора
            Console.WriteLine("2. Значения индексатора:");
            Console.WriteLine($"TimeFrame.Year: {team[TimeFrame.Year]}");
            Console.WriteLine($"TimeFrame.TwoYears: {team[TimeFrame.TwoYears]}");
            Console.WriteLine($"TimeFrame.Long: {team[TimeFrame.Long]}");
            Console.WriteLine();

            // 3. Присвоение значений свойствам и вывод ToString()
            Console.WriteLine("3. После присвоения свойств:");
            team.ResearchTopic = "Нейросети для диагностики";
            team.OrganizationName = "Лаборатория AI Health";
            team.RegistrationNumber = 880055;
            team.ResearchDuration = TimeFrame.Long;
            team.Publications = new Paper[]
            {
                new Paper("Диагностика COVID по КТ",
                         new Person("Анна", "Смирнова", new DateTime(1985, 7, 20)),
                         new DateTime(2022, 5, 10)),
                new Paper("Генетические алгоритмы в онкологии",
                         new Person("Иван", "Петров", new DateTime(1979, 3, 15)),
                         new DateTime(2023, 11, 30))
            };
            Console.WriteLine(team.ToString());
            Console.WriteLine();

            // 4. Добавление публикаций через AddPapers()
            Console.WriteLine("4. После добавления публикаций:");
            team.AddPapers(
                new Paper("Этика ИИ в медицине",
                         new Person("Мария", "Иванова", new DateTime(1990, 12, 5)),
                         new DateTime(2024, 1, 25)),
                new Paper("Квантовые вычисления для биоинформатики",
                         new Person("Алексей", "Сидоров", new DateTime(1982, 9, 8)),
                         new DateTime(2023, 6, 17))
            );
            Console.WriteLine(team.ToString());
            Console.WriteLine();

            // 5. Вывод самой поздней публикации
            Console.WriteLine("5. Самая поздняя публикация:");
            Paper latest = team.LatestPublication;
            Console.WriteLine(latest != null ? latest.ToString() : "Публикаций нет");
            Console.WriteLine();

            // 6. Сравнение времени выполнения операций с массивами
            Console.WriteLine("\n6. Сравнение времени работы массивов:");

            Console.Write("Введите количество строк и столбцов (разделители: пробел, запятая, точка с запятой): ");
            string input = Console.ReadLine();

            // разбитие строк
            char[] separators = { ' ', ',', ';' };
            string[] parts = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            int nrow = 0, ncolumn = 0;

            if (parts.Length >= 2)
            {
                nrow = int.Parse(parts[0]);
                ncolumn = int.Parse(parts[1]);
            }
            else if (parts.Length == 1)
            {
                nrow = ncolumn = int.Parse(parts[0]);
            }
            else
            {
                Console.WriteLine("Использую значения по умолчанию: 100 x 100");
                nrow = ncolumn = 100;
            }

            int totalElements = nrow * ncolumn;
            Console.WriteLine($"\nРазмеры: {nrow} строк × {ncolumn} столбцов = {totalElements} элементов");

            Paper[] oneDimArray = new Paper[totalElements];
            Paper[,] rectArray = new Paper[nrow, ncolumn];
            Paper[][] jaggedArray = new Paper[nrow][];

            for (int i = 0; i < nrow; i++)
            {
                jaggedArray[i] = new Paper[ncolumn];
            }

            for (int i = 0; i < totalElements; i++)
            {
                oneDimArray[i] = new Paper();
            }

            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncolumn; j++)
                {
                    rectArray[i, j] = new Paper();
                    jaggedArray[i][j] = new Paper();
                }
            }

            // одномерный массив
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            for (int i = 0; i < totalElements; i++)
            {
                oneDimArray[i].Title = "Updated Title";
            }
            sw1.Stop();

            // прямоугольный массив
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncolumn; j++)
                {
                    rectArray[i, j].Title = "Updated Title";
                }
            }
            sw2.Stop();

            Stopwatch sw3 = new Stopwatch();
            sw3.Start();
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncolumn; j++)
                {
                    jaggedArray[i][j].Title = "Updated Title";
                }
            }
            sw3.Stop();

            Console.WriteLine($"\nРезультаты замеров времени:");
            Console.WriteLine($"Одномерный массив [{totalElements}]: {sw1.ElapsedTicks} тиков");
            Console.WriteLine($"Прямоугольный массив [{nrow}x{ncolumn}]: {sw2.ElapsedTicks} тиков");
            Console.WriteLine($"Ступенчатый массив [{nrow}][] по {ncolumn}]: {sw3.ElapsedTicks} тиков");

            Console.WriteLine($"\nПроверка инициализации:");
            Console.WriteLine($"Элемент [0]: {oneDimArray[0].Title}, Автор: {oneDimArray[0].Author.ToShortString()}");
        }
    }
}