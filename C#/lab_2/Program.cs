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
            List<Organism> organisms = new List<Organism>();

            organisms.Add(new Animal("Волк", 5, 1));
            organisms.Add(new Animal("Лиса", 3, 0));
            organisms.Add(new Plant("Дуб", 10, 8));
            organisms.Add(new Plant("Кактус", 2, 3));

            Console.WriteLine("До сортировки:");
            foreach (var org in organisms)
                Console.WriteLine(org.GetStatus());

            organisms.Sort();
            Console.WriteLine("\nПосле сортировки по возрасту:");
            foreach (var org in organisms)
            {
                Console.WriteLine(org.GetStatus());

                if (org is ILogInfo loggable)
                    Console.WriteLine(loggable.LogInfo());

                Console.WriteLine();
            }

            int decision;
            int menu_decision;
            string name;
            int age;
            bool isValid;
            string input;
            do {
                do
                {
                    Console.Write("1)Создать новый организм\n" +
                    "2)Выбрать организм\n" +
                    "3)Показать все организмы\n" +
                    "4)Вывести информацию об объектах\n" +
                    "5)Закончить работу\n"+
                    "Выберите действие: ");
                    input = Console.ReadLine();
                    isValid = check_diapazon(input, 1, 5, out menu_decision);
                } while (!isValid);
                //decision = int.Parse(input);
                switch (menu_decision)
                {
                    case 1:
                        {
                            do
                            {
                                Console.WriteLine("Какой организм создать:\n1)Животное\n2)Растение");
                                input = Console.ReadLine();
                                isValid = check_diapazon(input, 1, 2, out decision);
                            } while (!isValid);
                            if (decision == 1)
                            {
                                int hunger;
                                Console.Write("Введите имя животного:");
                                name = Console.ReadLine();
                                Console.Write("Введите возраст животного:");
                                do {
                                    input = Console.ReadLine();
                                    isValid = check_diapazon(input, 0, 75, out age);
                                } while (!isValid);
                                Console.Write("Введите уровень голода животного от 1 до 10:");
                                hunger = int.Parse(Console.ReadLine());
                                organisms.Add(new Animal(name, hunger));
                            }

                            else
                            {
                                int illumination;
                                Console.Write("Введите имя растения:");
                                name = Console.ReadLine();
                                Console.Write("Введите уровень освещения от 1 до 10:");
                                illumination = int.Parse(Console.ReadLine());
                                Console.WriteLine("");
                                organisms.Add(new Plant(name, illumination));
                            }
                            break;
                        }
                    case 2:
                        {
                            InteractWithOrganism(organisms);
                            break; }
                    case 3:
                        {
                            foreach (Organism org in organisms)
                            {
                                if (org is ILogInfo loggable)
                                    Console.WriteLine(loggable.LogInfo());
                                else
                                    Console.WriteLine(org);
                            }
                            break;
                        }
                    case 4:
                        {
                            foreach (Organism organism in organisms)
                            {
                                Console.WriteLine(organism);
                            }
                            break;
                        }
                    default: { Console.WriteLine("Такого действия нет."); break; }
                }
            } while (menu_decision != 5);
            return 0;
        }

        static void show_organisms(List<Organism> organisms) 
        {
            for (int i = 0; i < organisms.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {organisms[i]}");
            }
        }

        static void InteractWithOrganism(List<Organism> organisms)
        {
            if (organisms.Count == 0)
            {
                Console.WriteLine("Список организмов пуст.");
                return;
            }

            show_organisms(organisms);
            Console.Write("Выберите номер организма: ");
            int index = int.Parse(Console.ReadLine());
            Organism selected = organisms[index - 1];

            if (selected is Animal animal)
                AnimalMenu(animal);
            else if (selected is Plant plant)
                PlantMenu(plant);
        }

        static void AnimalMenu(Animal animal)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine($"\nВы выбрали животное: {animal.Name}");
                Console.WriteLine("1) Охота\n2) Отдых\n3) Вернуться");
                int act = int.Parse(Console.ReadLine());
                switch (act)
                {
                    case 1: animal.Hunt(); break;
                    case 2: animal.Chill(); break;
                    case 3: exit = true; break;
                }
            }
        }

        static void PlantMenu(Plant plant)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine($"\nВы выбрали растение: {plant.Name}");
                Console.WriteLine("1) Проверить освещение\n2) Изменить освещение\n3) Вернуться");
                int act = int.Parse(Console.ReadLine());
                switch (act)
                {
                    case 1: plant.checkIllumination(); break;
                    case 2: plant.changeIllumination(); break;
                    case 3: exit = true; break;
                }
            }
        }

        static bool check_diapazon(string input, int min, int max, out int decision)
        {
            if (int.TryParse(input, out decision) && decision >= min && decision <= max)
            {
                return true;
            }
            Console.WriteLine($"Ошибка! Введите число от {min} до {max}.");
            return false;
        }
    }
    
}
