using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileKoisk.Api.Data;
using MobileKoisk.Api.Models;

namespace MobileKoisk.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BasketController(ApplicationDbContext context) { 
            _context = context;
        }

        [HttpGet("userId")]
        public async Task<ActionResult<Basket>> GetBasket(int userId)
        {
            var basket = await _context.Baskets
            .FirstOrDefaultAsync(b => b.UserId == userId);

            if (basket == null)
            {
                return NotFound();
            }

            return basket;
        }


        [HttpPost]
        public async Task<ActionResult<Basket>> CreateBasket([FromBody] Basket basket)
        {
            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBasket), new { userId = basket.UserId }, basket);
        }


        [HttpPost("{basketId}/items")]
        public async Task<ActionResult> AddItemToBasket(int basketId, [FromBody] BasketItem item)
        {
            var basket = await _context.Baskets.FindAsync(basketId);
            if (basket == null)
            {
                return NotFound();
            }

            _context.BasketItems.Add(item);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
