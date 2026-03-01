using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Plant : Organism
    {
        private string _name;
        private double _illumination;

        public Plant() { _name = "Unknown"; _illumination = 1.0; }
        public Plant(string name, int illumination = 0) { _name = name; _illumination = illumination; }


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
            _illumination = value / 10;
            Console.WriteLine($"Теперь уровень освещения {_name}: {_illumination}");
        } 

        public override int checkEnergy()
        {
            switch (_illumination)
            {
                case 1: return 10;
                case 0.9: return 9;
                case 0.8: return 8;
                case 0.7: return 7;
                case 0.6: return 6;
                case 0.5: return 5;
                case 0.4: return 4;
                case 0.3: return 3;
                case 0.2: return 2;
                case 0.1: return 1;
                case 0: return 0;
                default: return -1;
            }
        }
    }
}
