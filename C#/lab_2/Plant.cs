using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Plant:Organism
    {
        private int illumination;

        public Plant() { }
        public Plant(string name) { }


        public override int checkEnergy()
        {
            switch (illumination)
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
        }
    }
}
