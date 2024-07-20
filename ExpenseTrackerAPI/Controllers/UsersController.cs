using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Services;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Azure.Core;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        
        public AuthService authService { get; set; }

        public UsersController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                LoginResponseDto response = authService.GetUser(loginRequest);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                LoginResponseDto response = authService.SaveUser(registerRequest);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, RegisterRequest registerRequest)
        {

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            

            return Ok();
        }

        [HttpGet("AuthTest")]
        public IActionResult AuthTest()
        {
            return Ok();
        }

        [HttpGet("NoAuthTest")]
        [AllowAnonymous]
        public IActionResult NoAuthTest()
        {
            return Ok();
        }
    }
}