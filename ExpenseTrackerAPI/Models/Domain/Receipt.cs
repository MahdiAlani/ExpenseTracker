namespace ExpenseTrackerAPI.Models.Domain
{
    public class Receipt
    {
        public Receipt(Guid id, string? from, string? card, double totalCost, double taxPercentage, List<Item>? items)
        {
            Id = id;
            From = from;
            Card = card;
            TotalCost = totalCost;
            TaxPercentage = taxPercentage;
            Items = items;
        }

        public Guid Id { get; set; }

        public string? From { get; set; }

        public string? Card { get; set; }

        public double TotalCost { get; set; }

        public double TaxPercentage { get; set; }

        public List<Item>? Items { get; set; }

    }
}
