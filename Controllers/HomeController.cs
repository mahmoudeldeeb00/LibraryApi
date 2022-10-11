using Api_Project.BL.IRep;
using Api_Project.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IBookRep _book;

        
        public HomeController(IBookRep booksrep)
        {
            this._book = booksrep;
        }

        //[Authorize(Roles="Admin")]
        //[HttpGet("View Books")]
        //public List<Book> Books() => booksrep.GetAllBooksInDatabase();

        //[HttpGet("View Book Details{id}")]
        //public Book GetBook(int id ) => booksrep.GetBookById(id);

        //[HttpPost("AddBook")]
        //public void AddBook( string NewBook)
        //{
        //    booksrep.CreateNewBook(NewBook);

        //}
      

    }
}
