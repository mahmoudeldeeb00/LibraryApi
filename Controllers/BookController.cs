using Api_Project.BL.IRep;
using Api_Project.Models;
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
    [Authorize(Roles ="User")]
    public class BookController : ControllerBase
    {
        private readonly IBookRep _book;
        public BookController(IBookRep booksrep)
        {
            this._book = booksrep;
        }
        [Authorize(Roles = "Admin")]

        [HttpPost("Add New Book")]
        public IActionResult AddNewBook([FromBody] BookModel book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = _book.AddNewBook(book);
            return result == "Good" ? Ok("Book Added Sucessfully") : BadRequest(result);
               
        }

        [HttpGet("View Books ")]
        public IActionResult ViewBooks(int page = 1 )
        {
            var booklist = _book.GetAllBooks(page);
            return Ok(booklist);
        }

        [HttpPost("Check Book")]
        public IActionResult CheckBook(string UserId, int BookId, int LibraryId)
        {
           var result = _book.CheckBook(UserId, BookId, LibraryId);
            return result != 0 ? Ok("book Checked Succesfully and his number is " + result.ToString() ) : BadRequest("User or library or Book are not found ");

        }
        
        [HttpPost("Finish The Check")]
        public IActionResult FinishTheCheck(int CheckId)
        {
            var result = _book.FinishTheCheck(CheckId);
            return result=="Good"? Ok("Check Finished Succesfully"):BadRequest(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete Book ")]
        public IActionResult DeleteBook(int BookId)
        {
          var result = _book.DeleteBook(BookId);
            return result=="Good"? Ok("Book Deleted Succefully"):BadRequest(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Edit Book")]
        public IActionResult EditBook (int BookId , BookModel model)
        {
            var result = _book.EditBook(BookId, model);           
            return result == "Good" ? Ok("Book edited Succefully") : BadRequest(result);
        }
        [HttpGet("View One Book By Id ")]
        public IActionResult ViewOneBookById(int Id) => Ok(_book.GetBookById(Id));
       
        [HttpGet("Books Checked To User")]
        public IActionResult BooksCheckedToUser(string UserId) => Ok(_book.GetBooksCheckedToUser(UserId));

        [HttpGet("Search In Books")]
        public IActionResult SearchBooks(string word) => Ok(_book.SearchInBooks(word));
       
    }
}
