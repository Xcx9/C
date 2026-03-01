namespace ConsoleApp2
{
    abstract class Organism
    {
        public readonly string type;
        public int energy;
        public int age
        {
            get => age;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Возраст не может быть отрицательным.");
                age = value;
            }
        }


        public abstract int checkEnergy();

        public virtual string GetStatus() { return $"Тип: {type}, Возраст: {age}"; }

        interface IComparable<T> 
        {
            void compaire_Age(List<T> list)
            {

            }
        }

        /*
        public abstract void getName(string name);
        public abstract void setName(string name);
        */
        protected Organism() 
        {
            type = "Unknown";
            energy = 10;
            age = 0;
        }

        protected Organism(string type = "Unknown", int energy = 10, int age = 0)
        {
            this.type = type;
            this.energy = energy;
            this.age = age;
        }
    }
}
