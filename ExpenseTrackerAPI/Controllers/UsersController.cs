using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        public UserDbContext dbContext { get; set; }

        public UsersController(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            var User = dbContext.Users.SingleOrDefault(u => u.Id == id);
            return Ok(User);
        }

        [HttpPost]
        public IActionResult AddUser(UserDto userDto)
        {
            // Creates a new User Model
            var user = new User(
                Guid.NewGuid(),
                userDto.email,
                userDto.password);

            // Adds the User to the Database
            dbContext.Add(user);
            dbContext.SaveChanges();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, UserDto userDto)
        {
            var existingUser = dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Email = userDto.email;
            existingUser.Password = userDto.password;

            dbContext.Users.Update(existingUser);
            dbContext.SaveChanges();

            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();

            return Ok();
        }
    }

}