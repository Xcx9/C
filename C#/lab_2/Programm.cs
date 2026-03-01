using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static int Main(string[] args) {
            List<string> organisms = new List<string>();
            int decision;
            bool isValid;
            do
            {
                Console.Write("1)Создать новый организм\n" +
                "2)Выбрать организм\n" +
                "3)Показать все организмы\n" +
                "Выберите действие: ");
                string input = Console.ReadLine();
                isValid = int.TryParse(input, out decision) && decision >= 1 && decision <= 3;
                if (!isValid)
                {
                    Console.WriteLine($"Ошибка! Такого варианта нет.");
                }
            } while (!isValid);
            switch (decision)
                {
                case 1: 
                    {
                        Console.WriteLine("");
                        break; 
                    }
                case 2: 
                    {
                        Console.WriteLine("");

                        break; }
                case 3: 
                    {

                        break; 
                    }
                default: {Console.WriteLine("Такого действия нет."); break; }
            }
            return 0;
        }
    }
}
