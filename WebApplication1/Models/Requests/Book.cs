using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Requests
{
    public class Book
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Editorial { get; set; }
        public int PagesNumber { get; set; }
        public DateTime PublishDate { get; set; }
        public Author Author { get; set; }
    }
    public class Author
    {
        public Person Person { get; set; }
        public string DateBIrth { get; set; }
    }
    public class Person
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstSurName { get; set; }
        public string SecondSurName { get; set; }
    }
}
