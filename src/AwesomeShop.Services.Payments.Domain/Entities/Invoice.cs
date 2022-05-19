using AwesomeShop.Services.Payments.Domain.Bases;
using System;

namespace AwesomeShop.Services.Payments.Domain.Entities
{
    public class Invoice : EntityBase<Guid>
    {
        public Invoice(decimal totalPrice, Guid orderId, string cardNumber)
        {
            this.TotalPrice = totalPrice;
            this.OrderId = orderId;
            this.CardNumber = "**-" + cardNumber.Substring(cardNumber.Length - 4);
            this.PaidAt = DateTime.Now;
        }

        public decimal TotalPrice { get; private set; }
        public Guid OrderId { get; private set; }
        public string CardNumber { get; private set; }
        public DateTime PaidAt { get; private set; }
    }
}