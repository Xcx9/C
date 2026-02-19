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

        interface IComparable { }
        
        /*
        public abstract void getName(string name);
        public abstract void setName(string name);
        */
        protected Organism() { }
    }
}
