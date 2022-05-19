using AwesomeShop.Services.Payments.Domain.Dtos;
using AwesomeShop.Services.Payments.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Payments.Services.PaymentGateway
{
    public class PaymentGatewayService : IPaymentGatewayService
    {
        public Task<bool> Process(CreditCardInfoDto info)
        {
            return Task.FromResult(true);
        }
    }
}