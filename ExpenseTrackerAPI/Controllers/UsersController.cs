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
using Microsoft.IdentityModel.Tokens;

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

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                // Expire all cookies
                HttpContext.Response.Cookies.Append("AccessToken", string.Empty,
                    new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddDays(-1),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    });
                HttpContext.Response.Cookies.Append("RefreshToken", string.Empty,
                    new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddDays(-1),
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

        [HttpGet("auth")]
        [AllowAnonymous]
        public IActionResult AuthCheck()
        {

            // Find the access Token
            var accessToken = Request.Cookies["AccessToken"];

            // No access token
            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized("No access token found");
            }

            // Validate the access token
            try
            {
                authService.ValidateToken(accessToken);
            }
            // Expired access token
            catch (SecurityTokenExpiredException ex)
            {
                // Find the refresh token
                var refreshToken = Request.Cookies["RefreshToken"];

                // No refresh token
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return Unauthorized("No refresh token found");
                }

                try
                {
                    // Validate refresh token
                    authService.ValidateToken(refreshToken);

                    // Valid refresh token, create new access token
                    User user = authService.GetUserByRefreshToken(refreshToken);

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
                // Invalid refresh token
                catch 
                {
                    return Unauthorized("Refresh Token invalid");
                }
            }
            // Invalid access token, return unauthorized
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

            // Valid access token, return ok
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