using System;

namespace Lab1_Variant3
{
    public class Person
    {
        private string _firstName;
        private string _lastName;
        private DateTime _birthDate;

        public Person(string firstName, string lastName, DateTime birthDate)
        {
            _firstName = firstName;
            _lastName = lastName;
            _birthDate = birthDate;
        }

        public Person() : this("John", "Doe", new DateTime(2000, 1, 1)) { }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public int BirthYear
        {
            get { return _birthDate.Year; }
            set
            {
                _birthDate = new DateTime(value, _birthDate.Month, _birthDate.Day);
            }
        }

        public override string ToString()
        {
            return $"Person: {_firstName} {_lastName}, Born: {_birthDate:yyyy-MM-dd}";
        }

        public virtual string ToShortString()
        {
            return $"{_firstName} {_lastName}";
        }
    }
}