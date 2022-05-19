using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Payments.Domain.Dtos
{
    public class PaymentInfoDto
    {
        public string CardNumber { get; set; }
        public string FullName { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }
    }
}
