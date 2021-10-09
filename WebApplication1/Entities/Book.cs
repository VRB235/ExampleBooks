using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Editorial { get; set; }
        public int PagesNumber { get; set; }
        public DateTime PublishDate { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
    }
}
