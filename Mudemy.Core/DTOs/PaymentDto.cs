namespace Mudemy.Core.DTOs
{
    public class PaymentDto
    {
        public string CardHolderName { get; set; } = default!;
        public string CardNumber { get; set; } = default!; 
        public string ExpirationDate { get; set; } = default!; 
        public string CVV { get; set; } = default!; 
    }
}
