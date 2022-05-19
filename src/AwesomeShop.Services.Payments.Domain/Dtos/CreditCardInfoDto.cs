using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Payments.Domain.Dtos
{
    public class CreditCardInfoDto
    {
        public CreditCardInfoDto(string cardNumber, string fullName, string expirationDate, string cvv)
        {
            this.CardNumber = cardNumber;
            this.FullName = fullName;
            this.ExpirationDate = expirationDate;
            this.Cvv = cvv;

        }
        public string CardNumber { get; set; }
        public string FullName { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }
    }
}
