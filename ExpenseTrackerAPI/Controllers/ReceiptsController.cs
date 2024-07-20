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

        [HttpGet]
        public IActionResult GetAllReceipts() 
        {
            var Receipts = dbContext.Receipts.ToList();
            return Ok(Receipts);
        }

        /*
        * Gets receipts within a specific id
        */
        [HttpGet("byId")]
        public IActionResult GetReceiptsByDate(Guid id)
        {
            var receipts = dbContext.Receipts.SingleOrDefault(r => r.Id == id);
            return Ok(receipts);
        }

        /*
         * Gets receipts within a specific date
         */
        [HttpGet("bydate")]
        public IActionResult GetReceiptsByDate(DateTime date)
        {
            var receipts = dbContext.Receipts.Where(r => r.Date.Date == date.Date).ToList();
            return Ok(receipts);
        }

        /*
         * Gets receipts within a specific category
         */
        [HttpGet("bycategory")]
        public IActionResult GetReceiptsByCategory(string category)
        {
            var receipts = dbContext.Receipts.Where(r => r.Category == category).ToList();
            return Ok(receipts);
        }

        /*
         * Gets receipts from a specific payment method
         */
        [HttpGet("bypaymentmethod")]
        public IActionResult GetReceiptsByPaymentMethod(string paymentMethod)
        {
            var receipts = dbContext.Receipts.Where(r => r.PaymentMethod == paymentMethod).ToList();
            return Ok(receipts);
        }

        [HttpPost]
        public IActionResult AddReceipt(ReceiptDto receiptDto)
        {
            // Creates a new Receipt Model
            var receipt = new Receipt(
                Guid.NewGuid(),
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