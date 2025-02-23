using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileKoisk.Api.Data;
using MobileKoisk.Api.Models;

namespace MobileKoisk.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController :ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ReceiptController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Receipt>>> GetUserReceipts(int userId)
        {
            return await _context.Receipts
                .Where(r => r.UserId == userId)
                .Include(r => r.PurchasedItems)
                .ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Receipt>> GetReceipt(int id)
        {
            var receipt = await _context.Receipts
                .Include(r => r.PurchasedItems)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receipt == null)
            {
                return NotFound();
            }

            return receipt;
        }


        [HttpPost]
        public async Task<ActionResult<Receipt>> CreateReceipt(Receipt receipt)
        {
            _context.Receipts.Add(receipt);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReceipt), new { id = receipt.Id }, receipt);
        }
    }
}
