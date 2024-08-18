using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;

namespace ExpenseTrackerAPI.Models.Domain
{
    public class Receipt
    {

        public Receipt() { }

        public Receipt(Guid id, Guid userId, string? merchant, DateTime date, string? category, string? paymentMethod, double subtotal, double taxPercentage, double total)
        {
            Id = id;
            UserId = userId;
            Merchant = merchant;
            Date = date;
            Category = category;
            PaymentMethod = paymentMethod;
            SubTotal = subtotal;
            TaxPercentage = taxPercentage;
            Total = total;
        }

        public Guid Id { get; set; }

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
