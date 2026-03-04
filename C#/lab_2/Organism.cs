using System.Xml.Linq;

namespace ConsoleApp2
{
    abstract class Organism : IComparable<Organism>
    {
        public readonly string organism_type;
        private int _energy;
        private string _name;
        private int _age;

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

        protected int Age
        {
            get => _age;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Возраст не может быть отрицательным.");
                _age = value;
            }
        }

        public abstract int checkEnergy();

        public virtual string GetStatus() { return $"Тип: {organism_type}\n Имя: {_name}, Возраст: {_age}"; }

        public int CompareTo(Organism other)
        {
            if (other == null) return 1;

            return Age.CompareTo(other.Age);
        }


        protected Organism(string org_type, string name, int age)
        {
            organism_type = org_type;
            Name = name;
            Age = age;
        }
    }
}
