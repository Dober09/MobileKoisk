using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileKoisk.Api.Data;
using MobileKoisk.Api.Models;
using MobileKoisk.Api.DTOs;

namespace MobileKoisk.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost("checkout")]
        public async Task<ActionResult<Receipt>> Checkout(CheckoutRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Calculate total
                double total = request.Items.Sum(item => item.Price * item.Quantity);

                // Create receipt
                var receipt = new Receipt
                {
                    UserId = request.UserId,
                    PurchasedItems = request.Items,
                    TotalAmount = total,
                    PurchaseDate = DateTime.UtcNow,
                    ReceiptNumber = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(),
                    
                };

                _context.Receipts.Add(receipt);

                // Create notification
                var notification = new Notification
                {
                    UserId = request.UserId,
                    Title = "Purchase Successful",
                    Message = $"Your purchase of ${total:F2} has been completed.",
                    Type = NotificationType.PaymentConfirmation,
                    Timestamp = DateTime.UtcNow,
                    IsRead = false
                };

                _context.Notifications.Add(notification);

                // Clear user's basket
                var basket = await _context.Baskets
                    .FirstOrDefaultAsync(b => b.UserId == request.UserId);
                if (basket != null)
                {
                    _context.Baskets.Remove(basket);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return CreatedAtAction(nameof(ReceiptController.GetReceipt),
                    "Receipt", new { id = receipt.Id }, receipt);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
