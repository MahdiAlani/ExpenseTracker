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

        public ReceiptDbContext DbContext { get; set; }

        public ReceiptsController(ReceiptDbContext DbContext) 
        {
            this.DbContext = DbContext;   
        }

        [HttpGet]
        public IActionResult GetAllReceipts() 
        {
            var Receipts = DbContext.Receipts.ToList();

            return Ok(Receipts);
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
            DbContext.Add(receipt);
            DbContext.SaveChanges();

            return Ok(receipt);
        }

        [HttpPut]
        public IActionResult DeleteReceipt(Guid id)
        {

            DbContext.Remove(id); // Removes the Receipt based on its id

            return Ok();
        }

        [HttpDelete]
        public IActionResult UpdateReceipt(Guid id, ReceiptDto receiptDto)
        {
            // Deletes the old Receipt
            this.DeleteReceipt(id);

            // Adds the new receipt to the database
            return this.AddReceipt(receiptDto);

        }

    }
}
