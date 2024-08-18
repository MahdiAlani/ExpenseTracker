namespace ExpenseTrackerAPI.Models
{
    public class UserDto
    {
        public string? Email { get; set; }
        public Guid Id { get; set; }

        public UserDto(string? email, Guid id)
        {
            Email = email;
            Id = id;
        }
    }
}
