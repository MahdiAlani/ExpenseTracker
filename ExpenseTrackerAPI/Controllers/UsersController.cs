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
                User user = authService.GetUser(loginRequest);
                string refreshToken = authService.CreateRefreshToken(user);
                string accessToken = authService.CreateAccessToken(refreshToken, user);

                HttpContext.Response.Cookies.Append("RefreshToken", refreshToken,
                    new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddDays(14),
                        HttpOnly = true,
                        Secure = true,
                        IsEssential = true,
                        SameSite = SameSiteMode.None
                    });

                HttpContext.Response.Cookies.Append("AccessToken", accessToken,
                    new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        HttpOnly = true,
                        Secure = true,
                        IsEssential = true,
                        SameSite = SameSiteMode.None
                    });

                return Ok(AuthService.ConvertToDto(user));
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
                User user = authService.SaveUser(registerRequest);
                string refreshToken = authService.CreateRefreshToken(user);
                string accessToken = authService.CreateAccessToken(refreshToken ,user);

                HttpContext.Response.Cookies.Append("RefreshToken", refreshToken,
                    new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddDays(14),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    });

                HttpContext.Response.Cookies.Append("AccessToken", accessToken,
                    new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    });

                return Ok(AuthService.ConvertToDto(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["RefreshToken"];
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return Unauthorized("No refresh token provided.");
                }

                var user = authService.GetUserByRefreshToken(refreshToken);
                if (user == null)
                {
                    return Unauthorized("Invalid refresh token.");
                }

                var newAccessToken = authService.CreateAccessToken(refreshToken, user);

                HttpContext.Response.Cookies.Append("AccessToken", newAccessToken,
                    new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    });

                return Ok();
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

        [AllowAnonymous]
        [HttpGet("auth")]
        public IActionResult AuthCheck()
        {
            var token = Request.Cookies["AccessToken"];
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
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