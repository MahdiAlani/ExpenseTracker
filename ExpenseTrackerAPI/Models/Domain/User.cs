namespace ExpenseTrackerAPI.Models.Domain
{
    public class User
    {
        public User() { }

        public User(Guid id, string? email, string? password)
        {
            Id = id;
            Email = email;
            Password = password;
        }

        public Guid Id { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

    }
}
