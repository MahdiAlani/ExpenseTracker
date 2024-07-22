namespace ExpenseTrackerAPI.Models
{
    public class UserDto
    {
        public string? Email { get; set; }

        public UserDto(string? email)
        {
            Email = email;
        }
    }
}
