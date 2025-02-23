using MobileKoisk.Api.Models;
namespace MobileKoisk.Api.DTOs
{
    public class CheckoutRequest
    {
        public int UserId { get; set; }
        public List<BasketItem> Items { get; set; }
        public string PaymentMethod { get; set; }
    }
}
