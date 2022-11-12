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
    public class AuthiController : ControllerBase
    {
        private readonly IAuthiService _authiService;
        public AuthiController(IAuthiService aservice)
        {
            _authiService = aservice;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync( RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authiService.RegisterAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }
        [HttpPost("Token")]
        public async Task<IActionResult> GetTokenAsync( TokenModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authiService.GetTokenAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole( AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authiService.addRoleAsync(model);
            return Ok(result);
        }

        

    }
}
