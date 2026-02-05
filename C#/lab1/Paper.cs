using System;

namespace Lab1_Variant3
{
    public class Paper
    {
        public string Title { get; set; }
        public Person Author { get; set; }
        public DateTime PublicationDate { get; set; }

        public Paper(string title, Person author, DateTime publicationDate)
        {
            Title = title;
            Author = author;
            PublicationDate = publicationDate;
        }

        public Paper() : this("Untitled", new Person(), new DateTime(2023, 1, 1)) { }

        public override string ToString()
        {
            return $"Paper: \"{Title}\" by {Author.ToShortString()}, Published: {PublicationDate:yyyy-MM-dd}";
        }
    }
}