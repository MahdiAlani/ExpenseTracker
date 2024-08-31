using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

            var receipts = query.Where(r => r.UserId == id).ToList();
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

        // Gets the total amount spent in the current month, year, week in a specific category, method, or neither.
        [HttpGet("totalSpendings/{id}")]
        public async Task<IActionResult> GetCurrentSpendings(
            Guid id,
            [FromQuery] string term,        // Term: monthly, yearly, weekly
            [FromQuery] string filter = null     // Filter: category, paymentMethod, or null
        )
        {
            Console.WriteLine($"API called with: ID={id}, Term={term}, Filter={filter}");

            // Base query for fetching receipts for the given user
            var query = dbContext.Receipts
                .Where(r => r.UserId == id);

            // Initialize total to zero
            double total = 0;

            // Get the current date
            var currentDate = DateTime.Now;

            switch (term)
            {
                // Filter to this month
                case "month":
                    query = query.Where(r => r.Date.Month == currentDate.Month && r.Date.Year == currentDate.Year);
                    break;

                // Filter to this week
                case "week":
                    var startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek); // Start of the current week (Sunday)
                    var endOfWeek = startOfWeek.AddDays(7); // End of the current week (next Sunday)
                    query = query.Where(r => r.Date >= startOfWeek && r.Date < endOfWeek);
                    break;

                // Filter to this year
                case "year":
                    query = query.Where(r => r.Date.Year == currentDate.Year);
                    break;
                default:
                    return BadRequest("Invalid term option");
            }

            // Filter by a specific category/payment method if applicable
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(r => r.PaymentMethod == filter || r.Category == filter);
            }

            // Calculate the total amount spent
            total = await query.SumAsync(r => r.Total);

            return Ok(total);
        }

        // Get a list of total spendings in a specific time period, possibly by category, method or neither
        [HttpGet("spendingsList/{id}")]
        public async Task<IActionResult> GetTotalReceiptsByDateRange(
             Guid id,
             [FromQuery] DateTime startDate,
             [FromQuery] DateTime endDate,
             [FromQuery] string frequency = "", // Frequency: monthly, yearly, weekly or null
             [FromQuery] string filter = ""     // Filter: category, paymentMethod, or null
             )
            {
            Console.WriteLine($"API called with: ID={id}, StartDate={startDate}, EndDate={endDate}, Frequency={frequency}, Filter={filter}");

            // Ensure that the endDate is after startDate
            if (endDate < startDate)
            {
                return BadRequest("End date must be after start date.");
            }

            // Base query for fetching receipts for the given user within the given date
            var query = dbContext.Receipts
                .Where(r => r.UserId == id && r.Date >= startDate && r.Date <= endDate);

            IQueryable<object> result;

            switch (frequency) // Use frequency with default value of an empty string
            {
                // Filter to monthly spendings
                case "monthly":
                    result = query
                        .GroupBy(r => new { r.Date.Month, r.Date.Year })
                        .Select(g => new
                        {
                            Month = $"{g.Key.Month}",
                            Year = $"{g.Key.Year}",
                            TotalSpent = g.Sum(r => r.Total)
                        });
                    break;

                // Filter to weekly spendings
                case "weekly":
                    result = query
                        .GroupBy(r => new { Week = EF.Functions.DateDiffWeek(startDate, r.Date), r.Date.Month, r.Date.Year })
                        .Select(g => new
                        {
                            Week = $"{g.Key.Week}",
                            Month = $"{g.Key.Month}",
                            Year = $"{g.Key.Year}",
                            TotalSpent = g.Sum(r => r.Total)
                        });
                    break;

                // Filter to yearly spendings
                case "yearly":
                    result = query
                        .GroupBy(r => r.Date.Year)
                        .Select(g => new
                        {
                            Year = g.Key.ToString(),
                            TotalSpent = g.Sum(r => r.Total)
                        });
                    break;

                default:
                    // Initialize result with the base query to allow further grouping by filter
                    result = query;
                    break;
            }

            // Apply additional filter by category or payment method if specified
            if (!string.IsNullOrEmpty(filter))
            {
                if (filter == "category")
                {
                     result = query
                        .GroupBy(r => new { Period = new { r.Date.Month, r.Date.Year }, r.Category })
                        .Select(g => new
                        {
                            Month = $"{g.Key.Period.Month}",
                            Year = $"{g.Key.Period.Year}",
                            Type = g.Key.Category,
                            TotalSpent = g.Sum(r => r.Total)
                        });
                }
                else if (filter.ToLower() == "paymentmethod")
                {
                    result = query
                        .GroupBy(r => new
                        {
                            Month = new { r.Date.Month },
                            Year = new { r.Date.Year },
                            r.PaymentMethod
                        })
                        .Select(g => new
                        {
                            Month = $"{g.Key.Month}",
                            Year = $"{g.Key.Year}",
                            Type = g.Key.PaymentMethod,
                            TotalSpent = g.Sum(r => r.Total)
                        });
                }
                else
                {
                    return BadRequest("Invalid filter value. Please specify 'category' or 'paymentMethod'.");
                }
            }

            // Execute the grouped query and return the results
            return Ok(await result.ToListAsync());
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