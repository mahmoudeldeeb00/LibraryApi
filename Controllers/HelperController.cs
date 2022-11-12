using Api_Project.DAL.DataBase;
using Api_Project.DAL.Entities;
using Api_Project.Helpers;
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
    public class HelperController : ControllerBase
    {
        private readonly DbContainer _db;
        public HelperController(DbContainer db )
        {
            _db = db; 
        }
        [HttpGet("Cities")]
        public IActionResult GetCities() =>Ok( _db.Cities.ToList());
        [HttpGet("BookTypes")]
        public IActionResult GetBookTypes() => Ok(_db.BookTypes.ToList());

  
        [HttpPost("SaveFile")]
        public IActionResult FileSaver( IFormCollection formdata)
        {
            var files = HttpContext.Request.Form.Files;
            foreach (var file in files)
            {
               
                if (file.Length > 0)
                {
                    return Ok(SaveFileHelper.SaveAuthorPic(file));
                }
            }
            return BadRequest("no file saved");
        }

    }
}
