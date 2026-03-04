using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Animal : Organism, ILogInfo
    {
        private string _name;
        private int _age;
        private int _hunger;
        private int _energy;
        public readonly string organism_type = "Animal";

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Имя животного не может быть пустым.");
                _name = value;
            }
        }

        public Animal(string name, int age, int hunger = 0) : base("Животное", name, age)
        {
            _name = name;
            _age = age;
            _hunger = hunger;
            _energy = 10 - hunger;
        }

        
        public void Hunt()
        {
            if (_energy == 0) { Console.WriteLine("К сожалению у животного не осталось сил охотиться."); }
            else { 
            Random rnd = new Random();
            int hunt_luck = rnd.Next(-_hunger, 4);
            _hunger += hunt_luck;
            _energy -= hunt_luck;
            if (hunt_luck > 0)
            {
                Console.WriteLine("Вы нашли добычу, но не поймали её, голод увеличился ):");
                Console.WriteLine($"Уровень голода у {_name}:{_hunger}\nУровень энергии: {checkEnergy()}");
            }
            else if (hunt_luck == 0)
            {
                Console.WriteLine("Вы никого не нашли, ");
                Console.WriteLine($"Уровень голода у {_name}:{_hunger}");
            }
            else 
            {
                Console.WriteLine("Вы поймали добычу!");
                Console.WriteLine($"Уровень голода у {_name}:{_hunger}");
            }
            }
        }

        public void Chill()
        {
            Console.WriteLine("Вы отдохнули, голод увеличился");
            _hunger += 1;
            Console.WriteLine($"Уровень голода у {_name}:{_hunger}");
            _energy -= 1;
        }

        // Чем больше голод, тем меньше энергия
        public override int checkEnergy()
        {
            return _energy;
        }
        public override string ToString()
        {
            return $"Животное: {_name}, голод: {_hunger}";
        }

        public string LogInfo()
        {
            return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Статус: {GetStatus()}";
        }
    }
}
