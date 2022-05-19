using MongoDB.Driver;

namespace AwesomeShop.Services.Payments.Domain.Interfaces.Repositories
{
    public interface IMongoDBContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string collection);
    }
}
