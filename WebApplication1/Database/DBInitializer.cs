using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Database
{
    public static class DBInitializer
    {
        public static void Initialize(DbContextTest context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User{Password = "Hola1234*", UserName = "lazocar"},
                new User{Password = "Hola4321*", UserName = "lvaamonode"},
            };
            var authors = new Author[]
            {
                new Author
                {
                    Name = "Stephen King"
                },
                new Author
                {
                    Name = "Gabriel Garcia Marquez"
                }
            };
            var books = new Book[]
            {
                new Book
                {
                    AuthorId = 1,
                    Country = "USA",
                    Editorial = "NPI",
                    Name = "El internado",
                    PagesNumber = 300,
                    PublishDate = DateTime.Now.AddYears(-5)
                },
                new Book
                {
                    AuthorId = 1,
                    Country = "USA",
                    Editorial = "NPI",
                    Name = "IT",
                    PagesNumber = 300,
                    PublishDate = DateTime.Now.AddYears(-3)
                },
                new Book
                {
                    AuthorId = 2,
                    Country = "Colombia",
                    Editorial = "NPI",
                    Name = "100 años de soledad",
                    PagesNumber = 150,
                    PublishDate = DateTime.Now.AddYears(-10)
                }
            };
            foreach (User s in users)
            {
                context.Users.Add(s);
            }
            foreach (Author s in authors)
            {
                context.Authors.Add(s);
            }
            foreach (Book s in books)
            {
                context.Books.Add(s);
            }
            context.SaveChanges();
        }
    }
}
