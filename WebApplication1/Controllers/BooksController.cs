using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Database;
using WebApplication1.Models.Requests;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DbContextTest _DbContextTest;
        public string user { get; set; }
        public string userId { get; set; }
        public BooksController(DbContextTest dbContextTest)
        {
            _DbContextTest = dbContextTest;
        }
        [HttpPost]
        public async Task<IActionResult> RegiterAuthor(Book book)
        {
            var author = _DbContextTest.Authors.Add(new Entities.Author
            {
                Name = $"{book.Author.Person.FirstName} {book.Author.Person.SecondName} {book.Author.Person.FirstSurName} {book.Author.Person.SecondSurName}"
            });
            _DbContextTest.SaveChanges();
            _DbContextTest.Books.Add(new Entities.Book
            {
                Country = book.Country,
                Editorial = book.Editorial,
                Name = book.Name,
                PagesNumber = book.PagesNumber,
                PublishDate = book.PublishDate,
                AuthorId = author.Entity.Id
            });
            _DbContextTest.SaveChanges();
            return Ok();
        }
        [HttpGet("get-all-books")]
        public async Task<IActionResult> GetBooks()
        {
            var books = _DbContextTest.Books.ToList();
            List<Book> listBooks = new List<Book>();
            foreach(var book in books)
            {
                book.Author = _DbContextTest.Authors.Find(book.AuthorId);
                string[] fullname = book.Author.Name.Split(" ");
                listBooks.Add(new Book
                {
                    Name = book.Name,
                    Country = book.Country,
                    Editorial = book.Editorial,
                    PagesNumber = book.PagesNumber,
                    PublishDate = book.PublishDate,
                    Author = new Author
                    {
                        DateBIrth = "12-12-1234",
                        Person = new Person
                        {
                            FirstName = fullname.Length > 0 ? fullname[0] : "",
                            SecondName = fullname.Length > 1 ? fullname[1] : "",
                            FirstSurName = fullname.Length > 2 ? fullname[2] : "",
                            SecondSurName = fullname.Length > 3 ? fullname[3] : ""
                        }
                    }
                });
            }
            return Ok(listBooks);

            
        }
    }
}
