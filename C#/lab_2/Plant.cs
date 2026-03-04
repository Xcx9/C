using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp2
{
    internal class Plant : Organism, ILogInfo
    {
        private string _name;
        private int _age;
        private int energy;
        private double _illumination;
        public readonly string organism_type = "Plant";

        public Plant(string name, int age, int illumination = 0) : base("Растение", name, age)
        { _name = name; 
            age = 0; 
            _illumination = illumination / 10; energy = illumination;
        }

        public void checkIllumination()
        {
            Console.WriteLine($"Уровень освещения у {_name}: {_illumination}");
        }

        public void changeIllumination() 
        {
            int value;
            bool isValid;
            do
            {
                Console.Write($"Введите целое число от {1} до {10}: ");
                string input = Console.ReadLine();
                isValid = int.TryParse(input, out value) && value >= 1 && value <= 10;
                if (!isValid)
                {
                    Console.WriteLine($"Ошибка! Введите число от {1} до {10}.");
                }
            } while (!isValid);
            energy = value;
            _illumination = value / 10;
            Console.WriteLine($"Теперь уровень освещения {_name}: {_illumination}");
        } 

        public override int checkEnergy()
        {
                return energy;
        }

        public override string ToString()
        {
            return $"Растение: {_name}, Уровень освещения: {_illumination}";
        }

        public string LogInfo()
        {
            return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Статус: {GetStatus()}";
        }
    }
}
