using System;

namespace AwesomeShop.Services.Payments.Domain.Events
{
    public class PaymentAcceptedEvent
    {
        public PaymentAcceptedEvent(Guid id, string fullName, string email)
        {
            this.Id = id;
            this.FullName = fullName;
            this.Email = email;

        }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}