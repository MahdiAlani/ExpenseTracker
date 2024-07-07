namespace ExpenseTrackerAPI.Models.Domain
{
    public class Item
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public double Cost { get; set; }

        public string? Category { get; set; }

    }

}
