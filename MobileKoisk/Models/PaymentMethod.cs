using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileKoisk.Models
{
    // Payment-related models
    public class PaymentMethod
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public PaymentMethodType Type { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CVV { get; set; }
        public bool IsPrimary { get; set; }
    }


    public enum PaymentMethodType
    {
        CreditCard,
        DebitCard,
        BankTransfer,
        PayPal,
        ApplePay,
        GooglePay
    }
}
