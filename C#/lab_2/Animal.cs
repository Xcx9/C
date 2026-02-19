using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Animal:Organism
    {
        private string _name;
        private int _hunger;
        public readonly string type;
        



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

        public Animal() 
        {
            _name = "Unknown";
            _hunger = 0;
        }

        public Animal(string name, int hunger = 0) 
        { 
            _name = name;
            _hunger = hunger;
        }


        private void Hunt()
        {

        }

        private void Chill()
        {
            _hunger -= 1;
        }

        public override int checkEnergy() 
        { 
            switch (_hunger)
            {
                case 0: return 10;
                case 1: return 9;
                case 2: return 8;
                case 3: return 7;
                case 4: return 6;
                case 5: return 5;
                case 6: return 4;
                case 7: return 3;
                case 8: return 2;
                case 9: return 1;
                case 10: return 0;
                default: return -1;

            }

            return 0; 
        }
    }
}
