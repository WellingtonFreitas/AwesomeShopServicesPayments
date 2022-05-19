using AwesomeShop.Services.Payments.Domain.Dtos;
using AwesomeShop.Services.Payments.Domain.Entities;
using AwesomeShop.Services.Payments.Domain.Events;
using AwesomeShop.Services.Payments.Domain.Interfaces.Repositories;
using AwesomeShop.Services.Payments.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Payments.Services.Subscribers
{
    public class OrderCreatedSubscriber : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string Queue = "payment-service/order-created";
        private const string Exchange = "payment-service";
        private const string RoutingKey = "order-created";
        public OrderCreatedSubscriber(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = connectionFactory.CreateConnection("payment-service-order-created-consumer");

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: Exchange, type: "topic", durable: true);
            _channel.QueueDeclare(queue: Queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(Queue, Exchange, Queue);

            _channel.QueueBind(queue: Queue, "order-service", routingKey: RoutingKey);

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var message = JsonConvert.DeserializeObject<OrderCreatedDto>(contentString);

                Console.WriteLine($"Message OrderCreated received with Id {message.Id}");

                var result = await ProcessPayment(message);

                if (result)
                {
                    _channel.BasicAck(eventArgs.DeliveryTag, false);

                    var paymentAccepted = new PaymentAcceptedEvent(message.Id, message.FullName, message.Email);
                    var payload = JsonConvert.SerializeObject(paymentAccepted);
                    var byteArray = Encoding.UTF8.GetBytes(payload);

                    Console.WriteLine("PaymentAccepted Published");

                    _channel.BasicPublish(Exchange, "payment-accepted", null, byteArray);
                }
            };

            _channel.BasicConsume(Queue, false, consumer);

            return Task.CompletedTask;
        }

        private async Task<bool> ProcessPayment(OrderCreatedDto orderCreated)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var paymentService = scope.ServiceProvider.GetService<IPaymentGatewayService>();

                var result = await paymentService
                    .Process(new CreditCardInfoDto(
                        orderCreated.PaymentInfo.CardNumber,
                        orderCreated.PaymentInfo.FullName,
                        orderCreated.PaymentInfo.ExpirationDate,
                        orderCreated.PaymentInfo.Cvv));

                var invoiceRepository = scope.ServiceProvider.GetService<IInvoiceRepository>();

                await invoiceRepository.AddAsync(new Invoice(orderCreated.TotalPrice, orderCreated.Id, orderCreated.PaymentInfo.CardNumber));

                return result;
            }
        }
    }
}