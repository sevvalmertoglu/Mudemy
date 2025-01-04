using System;

namespace Mudemy.Core.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = default!;

        public string CardHolderName { get; set; } = default!; 
        public string CardNumber { get; set; } = default!; 
        public string ExpirationDate { get; set; } = default!; 
        public string CVV { get; set; } = default!; 
        public string TransactionId { get; set; } = default!;

        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
    }
}
