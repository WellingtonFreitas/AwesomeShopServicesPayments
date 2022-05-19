using AwesomeShop.Services.Payments.Domain.Entities;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Payments.Domain.Interfaces.Repositories
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);
    }
}