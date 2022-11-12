using Api_Project.BL.IRep;
using Api_Project.DAL.Entities;
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
    [Authorize(Roles="User")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRep _author;

        public AuthorController(IAuthorRep Author)
        {
            _author = Author;
        }
        [HttpGet("View Authors")]
        public IActionResult ViewAuthors()
        {
            var AuthorsList = _author.GetAllAuthors();
            return Ok(AuthorsList);
           
        }

       [Authorize(Roles="Admin")]
       
        [HttpPost("Add Author")]
        public IActionResult AddAuthor(AuthorModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           var result =  _author.AddAuthor(model);
            return result=="Good"? Ok("Author added Succesfully") :BadRequest(result);

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete Author")]
        public IActionResult DeleteAuthor(int AuthorId)
        {
           var result =  _author.DeleteAuthor(AuthorId);
            return result == "Good"? Ok("Author Deleted Succefuly "):BadRequest(result);

        }


        [Authorize(Roles = "Admin")]
        [HttpPut("Edit Model")]
        public IActionResult EditAuthor(int AuthorId , [FromForm]AuthorModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result =  _author.EditAuthor(AuthorId, model);
            return result=="Good" ? Ok("Book Edited Succefully"):BadRequest(result);
        }


        [HttpGet("Get One Author")]
        public IActionResult GetAuthorById(int Id) => Ok(_author.GetAuthorById(Id));
        [HttpGet("Get Author Books")]
        public IActionResult GetAuthorBooks(int Id) => Ok(_author.GetBooksToAuthor(Id));

        [HttpGet("Cities")]
        public IActionResult GetCities() => Ok(_author.cities());


    }
}
