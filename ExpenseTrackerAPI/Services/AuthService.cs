using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthCore.Helpers;
using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;

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

        public User GetUser(LoginRequest loginRequest)
        {
            // Find the user by email
            User? user = appDbContext.Users.SingleOrDefault(u => u.Email == loginRequest.Email);

            // Check if user exists
            if (user == null)
            {
                throw new KeyNotFoundException("User was not found");
            }

            // Verify the password
            if (passwordHasher.VerifyHashedPassword(user, user.Password, loginRequest.Password) == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return user;
        }

        public User SaveUser(RegisterRequest registerRequest)
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

            return user;
        }

        public string CreateRefreshToken(User user)
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
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = jwtHandler.CreateToken(tokenDescriptor);
            string tokenString = jwtHandler.WriteToken(token);

            return tokenString;
        }

        public string CreateAccessToken(string refreshToken, User user)
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
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = jwtHandler.CreateToken(tokenDescriptor);
            string tokenString = jwtHandler.WriteToken(token);

            return tokenString;


        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            return jwtHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_secretKey),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);
        }

        public static UserDto ConvertToDto(User user)
        {
            return new UserDto(user.Email, user.Id);
        }

        public User GetUserByRefreshToken(string refreshToken)
        {
            ClaimsPrincipal claimsPrincipal = this.ValidateToken(refreshToken);
            var userId = Guid.Parse(claimsPrincipal.FindFirst("sub")?.Value);

            return appDbContext.Users.FirstOrDefault(u => u.Id == (userId));

        }
    }
}
