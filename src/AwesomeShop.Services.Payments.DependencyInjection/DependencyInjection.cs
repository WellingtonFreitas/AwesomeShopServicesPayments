
using AwesomeShop.Services.Payments.Data.Persistence;
using AwesomeShop.Services.Payments.Data.Persistence.Repositories;
using AwesomeShop.Services.Payments.Domain.Interfaces.Repositories;
using AwesomeShop.Services.Payments.Domain.Interfaces.Services;
using AwesomeShop.Services.Payments.Services.PaymentGateway;
using AwesomeShop.Services.Payments.Services.Subscribers;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeShop.Services.Payments.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddServicesDependenciesInjection(this IServiceCollection services)
        {
            services.AddTransient<IPaymentGatewayService, PaymentGatewayService>();
        }

        public static void AddRepositoriesDependenciesInjection(this IServiceCollection services)
        {
            services.AddSingleton<IMongoDBContext, MongoDbContext>();

            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        }

        public static void AddMessageBus(this IServiceCollection services)
        {
        }

        public static void AddSubscribers(this IServiceCollection services)
        {
            services.AddHostedService<OrderCreatedSubscriber>();

        }
    }
}