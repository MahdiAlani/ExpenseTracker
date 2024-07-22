using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthCore.Helpers;
using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Migrations;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;

namespace ExpenseTrackerAPI.Services
{

    public class AuthService
    {
        private readonly IPasswordHasher<User> passwordHasher;

        private readonly AppDbContext appDbContext;

        private readonly JwtSecurityTokenHandler jwtHandler;

        /*
         * MAKE SURE TO CHANGE AFTER
         */
        private readonly byte[] _secretKey = Encoding.ASCII.GetBytes(AuthSettings.PrivateKey);

        public AuthService(IPasswordHasher<User> passwordHasher, AppDbContext appDbContext,
            JwtSecurityTokenHandler jwtHandler)
        {
            this.passwordHasher = passwordHasher;
            this.appDbContext = appDbContext;
            this.jwtHandler = jwtHandler;
        }

        public LoginResponseDto GetUser(LoginRequest loginRequest)
        {

            User? user = appDbContext.Users.SingleOrDefault(u => u.Email == loginRequest.Email);

            // User does not exist
            if (user == null)
            {
                throw new KeyNotFoundException("User was not found");
            }

            // Allow the user to login
            string refreshToken = CreateRefreshToken(user);
            string accessToken = CreateAccessToken(refreshToken, user);

            return new LoginResponseDto(ConvertToDto(user), accessToken, refreshToken);

        }

        public LoginResponseDto SaveUser(RegisterRequest registerRequest)
        {

            // User already exists
            if (appDbContext.Users.Any(u => u.Email == registerRequest.Email))
            {
                throw new InvalidOperationException("User already exists.");
            }

            // Create a new user
            User user = new User();
            user.Email = registerRequest.Email;
            user.Password = passwordHasher.HashPassword(user, registerRequest.Password);

            user.Roles = new List<string> { "USER" };

            appDbContext.Users.Add(user);
            appDbContext.SaveChanges();

            string refreshToken = CreateRefreshToken(user);
            string accessToken = CreateAccessToken(refreshToken, user);

            return new LoginResponseDto(ConvertToDto(user), accessToken, refreshToken);
        }

        private string CreateRefreshToken(User user)
        {
            // Adding all claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(168),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = jwtHandler.CreateToken(tokenDescriptor);
            string tokenString = jwtHandler.WriteToken(token);

            return tokenString;
        }

        private string CreateAccessToken(string refreshToken, User user)
        {
            // Validates the refresh token
            // Throws an Error if token is not valid
            ValidateToken(refreshToken);

            // Adding all claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Creating Token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = jwtHandler.CreateToken(tokenDescriptor);
            string tokenString = jwtHandler.WriteToken(token);

            return tokenString;


        }

        private void ValidateToken(string token)
        {
            jwtHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_secretKey),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);
        }

        private static UserDto ConvertToDto(User user)
        {
            return new UserDto(user.Email);
        }
    }
}
