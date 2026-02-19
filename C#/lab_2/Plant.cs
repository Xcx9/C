using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Plant:Organism
    {
        private string _name;
        private double _illumination;

        public Plant() { _name = "Unknown"; _illumination = 1.0; }
        public Plant(string name, int illumination = 0) { }


        private void checkIllumination()
        {

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
