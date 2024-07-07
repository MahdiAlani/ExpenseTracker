using ExpenseTrackerAPI.Models.Domain;

namespace ExpenseTrackerAPI.Models
{
    public class ReceiptDto
    {
        public string? From { get; set; }

        public string? Card { get; set; }

        public double TotalCost { get; set; }

        public double TaxPercentage { get; set; }

        public List<Item>? Items { get; set; }
    }
}
