using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {

        public AppDbContext dbContext { get; set; }

        public ReceiptsController(AppDbContext dbContext) 
        {
            this.dbContext = dbContext;   
        }

        /*
         * Gets all database receipts
         */
        [HttpGet]
        public IActionResult GetAllReceipts() 
        {
            var Receipts = dbContext.Receipts.ToList();
            return Ok(Receipts);
        }

        /*
        * Gets receipts from a specific user. Data can be sorted by the user based on parameters
        */
        [HttpGet("{id}")]
        public IActionResult GetReceipts(Guid id, string sortBy = "date", string sortDirection = "desc")
        {
            var query = dbContext.Receipts.AsQueryable();

            switch (sortBy.ToLower())
            {
                case "merchant":
                    query = sortDirection.ToLower() == "desc" ? query.OrderByDescending(r => r.Merchant) : query.OrderBy(r => r.Merchant);
                    break;
                case "total":
                    query = sortDirection.ToLower() == "desc" ? query.OrderByDescending(r => r.Total) : query.OrderBy(r => r.Total);
                    break;
                case "date":
                default:
                    query = sortDirection.ToLower() == "desc" ? query.OrderByDescending(r => r.Date) : query.OrderBy(r => r.Date);
                    break;
            }

            var receipts = query.ToList();
            return Ok(receipts);
        }

        /*
         * Gets receipts within a specific date
         */
        [HttpGet("bydate")]
        public IActionResult GetReceiptsByDate(Guid id, DateTime date)
        {
            var receipts = dbContext.Receipts.Where(r => (r.UserId == id && r.Date.Date == date.Date)).ToList();
            return Ok(receipts);
        }

        /*
         * Gets receipts within a specific category
         */
        [HttpGet("bycategory")]
        public IActionResult GetReceiptsByCategory(Guid id, string category)
        {
            var receipts = dbContext.Receipts.Where(r => (r.UserId == id && r.Category == category)).ToList();
            return Ok(receipts);
        }

        /*
         * Gets receipts from a specific payment method
         */
        [HttpGet("bypaymentmethod")]
        public IActionResult GetReceiptsByPaymentMethod(Guid id, string paymentMethod)
        {
            var receipts = dbContext.Receipts.Where(r => (r.UserId == id && r.PaymentMethod == paymentMethod)).ToList();
            return Ok(receipts);
        }

        [HttpPost]
        public IActionResult AddReceipt(ReceiptDto receiptDto)
        {
            // Creates a new Receipt Model
            var receipt = new Receipt(
                Guid.NewGuid(),
                receiptDto.UserId,
                receiptDto.Merchant,
                receiptDto.Date,
                receiptDto.Category,
                receiptDto.PaymentMethod,
                receiptDto.SubTotal,
                receiptDto.TaxPercentage,
                receiptDto.Total);

            // Adds the Receipt to the Database
            dbContext.Add(receipt);
            dbContext.SaveChanges();
            Console.WriteLine("Receipt Created");

            return Ok(receipt);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReceipt(Guid id, ReceiptDto receiptDto)
        {
            var existingReceipt = dbContext.Receipts.FirstOrDefault(r => r.Id == id);
            if (existingReceipt == null)
            {
                return NotFound();
            }

            existingReceipt.Merchant = receiptDto.Merchant;
            existingReceipt.Date = receiptDto.Date;
            existingReceipt.Category = receiptDto.Category;
            existingReceipt.PaymentMethod = receiptDto.PaymentMethod;
            existingReceipt.SubTotal = receiptDto.SubTotal;
            existingReceipt.TaxPercentage = receiptDto.TaxPercentage;
            existingReceipt.Total = receiptDto.Total;

            dbContext.Receipts.Update(existingReceipt);
            dbContext.SaveChanges();

            return Ok(existingReceipt);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReceipt(Guid id)
        {
            var receipt = dbContext.Receipts.FirstOrDefault(r => r.Id == id);
            if (receipt == null)
            {
                return NotFound();
            }

            dbContext.Receipts.Remove(receipt);
            dbContext.SaveChanges();

            return Ok();
        }
    }

}