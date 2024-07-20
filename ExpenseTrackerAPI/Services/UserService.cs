using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTrackerAPI.Services
{
    public class UserService
    {
        public AppDbContext dbContext { get; set; }

        public UserService() { }
    }
}
