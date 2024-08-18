using ExpenseTrackerAPI.Models.Domain;

namespace ExpenseTrackerAPI.Models
{
    public class ReceiptDto
    {
        public Guid UserId { get; set; }
        public string? Merchant { get; set; }

        public DateTime Date { get; set; }

        public string? Category { get; set; }

        public string? PaymentMethod { get; set; }

        public double SubTotal { get; set; }

        public double TaxPercentage { get; set; }

        public double Total { get; set; }
    }
}
