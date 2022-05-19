using AwesomeShop.Services.Payments.Domain.Entities;
using AwesomeShop.Services.Payments.Domain.Interfaces.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Payments.Data.Persistence.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IMongoCollection<Invoice> _collection;
        public InvoiceRepository(IMongoDBContext mongoDBContext)
        {
            _collection = mongoDBContext.GetCollection<Invoice>("invoices");
        }

        public async Task AddAsync(Invoice invoice)
        {
            await _collection.InsertOneAsync(invoice);
        }
    }
}