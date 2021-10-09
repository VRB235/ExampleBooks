using BackendBanplusprSAC.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Database;
using WebApplication1.Models.Requests;
using WebApplication1.Models.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DbContextTest _DbContextTest;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string user { get; set; }
        public string userId { get; set; }
        public AuthController(DbContextTest dbContextTest, IHttpContextAccessor httpContextAccessor)
        {
            _DbContextTest = dbContextTest;
            _httpContextAccessor = httpContextAccessor;
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity.Claims.ToList();
            user = claims.Count > 0 ? claims[0].Value : null;
            userId = claims.Count > 0 ? claims[1].Value : null;
        }

        // POST api/<AuthController>
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var user = _DbContextTest.Users.Where(u => u.UserName == login.User).FirstOrDefault();

            if(user != null)
            {
                if (user.Password == login.Password)
                {
                    return Ok(new LoginResponse
                    {
                        Token = Cryptography.GenerateToken(login.User, user.Id)
                    });
                }
            }
            return Conflict();
        }

        [HttpGet("{username}/{password}")]
        public async Task<IActionResult> LoginGet(string username, string password)
        {
            var user = _DbContextTest.Users.Where(u => u.UserName == username).FirstOrDefault();

            if (user != null)
            {
                if (user.Password == password)
                {
                    return Ok(new LoginResponse
                    {
                        Token = Cryptography.GenerateToken(username, user.Id)
                    });
                }
            }

            return Conflict();
        }
        [Authorize]
        [HttpGet("get-books-by-country")]
        public async Task<IActionResult> GetBooksByCountry(string country)
        {
            if(user.StartsWith("l"))
            {
                var books = _DbContextTest.Books.Where(b => b.Country == country).Select(b => new {
                    name = b.Name,
                    editorial = b.Editorial,
                    publishDate = b.PublishDate
                });

                //var bookAux = books.Select(b => new {
                //    name = b.Name,
                //    editorial = b.Editorial,
                //    publishDate = b.PublishDate
                //});

                //List < Book> booksResponse = new List<Book>();
                //foreach(var book in books)
                //{
                //    booksResponse.Add(new Book
                //    {
                //        Name = book.Name,
                //        Edidiorial = book.Editorial,
                //        PublishDate = book.PublishDate.ToString("D")
                //    });
                //}
                return Ok(books);
            }
            return Conflict();
        }
    }
}
