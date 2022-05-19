using System;

namespace AwesomeShop.Services.Payments.Domain.Bases
{
    public class EntityBase<TIdentifier> 
    {
        public TIdentifier Id { get; set; }
    }
}