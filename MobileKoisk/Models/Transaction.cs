using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MobileKoisk.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
        public string Description { get; set; }
        public int? ScannedItemId { get; set; }
        public int? PaymentMethodId { get; set; }
    }

    public enum TransactionType
    {
        Purchase,
        Deposit,
        Withdrawal,
        Transfer
    }

    public enum TransactionStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }
}
