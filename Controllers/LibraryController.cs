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
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryRep _library;

        public LibraryController(ILibraryRep library)
        {
            this._library = library;
        }
        [HttpGet("View libraries")]
        public IActionResult ViewLibraries(int Page=1) => Ok(_library.GetAllLibraries(Page));

        [Authorize(Roles ="Admin")]
        [HttpPost("Add Library")]
        public IActionResult AddLibrary([FromBody]LibraryModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);     
            var result = _library.AddLibrary(model);
            return result == "Good"?Ok("book Added Successfully"):BadRequest(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("Delete Library")]
        public IActionResult DeleteLibrary(int Id)
        {
            var result = _library.DeleteLibrary(Id);
            return result != "Good" ? BadRequest(result) : Ok("Library Deleted Succefully ");              
        }
       
        [Authorize(Roles = "Admin")]
        [HttpPut("Edit Library")]
        public IActionResult EditLibrary(int Id, [FromForm] LibraryModel model)
        {
            var result = _library.EditLibrary(Id, model);
            return result == "Good" ? Ok("Library Editted Succefully ") : BadRequest(result)  ;
        }
      
        [HttpGet("View All Books to one library")]
        public IActionResult ViewLibraryBooks(int LibraryId ,int Page = 1) => Ok(_library.GetBooksInLibrary(LibraryId, Page));

        [HttpGet("View Checked  Books In Library")]
        public IActionResult ViewCheckedLibraryBooks(int LibraryId) => Ok(_library.GetBooksCheckedInLibrary(LibraryId));



    }

}
