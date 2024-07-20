namespace ExpenseTrackerAPI.Models.Domain
{
    public class User
    {
        public User() { }

        public User(Guid id, string? email, string? password, List<string> roles)
        {
            Id = id;
            Email = email;
            Password = password;
            Roles = roles;
        }

        public Guid Id { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public List<string> Roles { get; set; }

    }
}
