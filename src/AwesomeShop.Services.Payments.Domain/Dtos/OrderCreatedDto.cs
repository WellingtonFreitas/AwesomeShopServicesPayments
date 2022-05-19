using System;

namespace AwesomeShop.Services.Payments.Domain.Dtos
{
    public class OrderCreatedDto
    {
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentInfoDto PaymentInfo { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
