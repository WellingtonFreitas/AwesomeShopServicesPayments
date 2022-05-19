using AwesomeShop.Services.Payments.Domain.Dtos;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Payments.Domain.Interfaces.Services
{
    public interface IPaymentGatewayService
    {
        Task<bool> Process(CreditCardInfoDto info);
    }
}