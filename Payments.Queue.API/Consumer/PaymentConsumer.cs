using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Text.Json;
using Payments.Queue.API.Models;

namespace Payments.Queue.API.Consumer
{
    public class PaymentConsumer : BackgroundService
    {
        private const string QUEUE = "Payments";
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public PaymentConsumer()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: QUEUE,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var byteArray = eventArgs.Body.ToArray();
                var paymentInfoJson = Encoding.UTF8.GetString(byteArray);

                var paymentInfo = JsonSerializer.Deserialize<PaymentInfoModel>(paymentInfoJson);

                //Do something here...
                Console.WriteLine($"Pagamento Processado! PaymentId: {paymentInfo.Id}");

                Task.Delay(2000);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(QUEUE, false, consumer);

            return Task.CompletedTask;
        }
    }
}
