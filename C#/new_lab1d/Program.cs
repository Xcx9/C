using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Демонстрация присваивания (товары) ===");

        ProductClass classA = new ProductClass("Телефон", 10, ProductCategory.Electronics);
        ProductClass classB = classA;          
        classB.Quantity = 100;                 
        Console.WriteLine("classA: " + classA);
        Console.WriteLine("classB: " + classB); 

        ProductStruct structA = new ProductStruct("Книга", 5, ProductCategory.Books);
        ProductStruct structB = structA;       
        structB = structB with { Quantity = 50 };
        Console.WriteLine("structA: " + structA);
        Console.WriteLine("structB: " + structB); 

        Console.WriteLine("\n=== Измерение производительности ===");
        Console.Write("Введите общее количество элементов (минимум 1 000 000): ");
        int totalElements = int.Parse(Console.ReadLine());
        if (totalElements < 1_000_000) totalElements = 1_000_000;

        TestOneDimensionalArray(totalElements);

        int rows = 1000;
        int cols = totalElements / rows; 
        int rectTotal = rows * cols;
        Console.WriteLine($"\nПрямоугольный массив: {rows} x {cols} = {rectTotal} элементов");
        TestRectangularArray(rows, cols);

        Console.WriteLine("\nГотово.");
    }

    static void TestOneDimensionalArray(int size)
    {
        Console.WriteLine($"\n--- Одномерный массив ({size} элементов) ---");

        var classArray = new ProductClass[size];
        var structArray = new ProductStruct[size];

        for (int i = 0; i < size; i++)
        {
            classArray[i] = new ProductClass($"Товар{i}", i % 100, ProductCategory.Other);
            structArray[i] = new ProductStruct($"Товар{i}", i % 100, ProductCategory.Other);
        }

        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < size; i++)
        {
            classArray[i].Quantity++;
        }
        sw.Stop();
        Console.WriteLine($"Классы: {sw.ElapsedMilliseconds} мс");

        sw.Restart();
        for (int i = 0; i < size; i++)
        {
            var p = structArray[i];
            p.Quantity++;
            structArray[i] = p;  
        }
        sw.Stop();
        Console.WriteLine($"Структуры: {sw.ElapsedMilliseconds} мс");
    }

    static void TestRectangularArray(int rows, int cols)
    {
        var classRect = new ProductClass[rows, cols];
        var structRect = new ProductStruct[rows, cols];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
            {
                classRect[i, j] = new ProductClass($"Товар{i}-{j}", (i + j) % 100, ProductCategory.Other);
                structRect[i, j] = new ProductStruct($"Товар{i}-{j}", (i + j) % 100, ProductCategory.Other);
            }

        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
            {
                classRect[i, j].Quantity++;
            }
        sw.Stop();
        Console.WriteLine($"Классы (прямоугольный): {sw.ElapsedMilliseconds} мс");

        sw.Restart();
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
            {
                var p = structRect[i, j];
                p.Quantity++;
                structRect[i, j] = p;
            }
        sw.Stop();
        Console.WriteLine($"Структуры (прямоугольный): {sw.ElapsedMilliseconds} мс");
    }
}
