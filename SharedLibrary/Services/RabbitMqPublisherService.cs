using RabbitMQ.Client;
using SharedLibrary.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedLibrary.Services
{
    public class RabbitMqPublisherService
    {
        private readonly RabbitMqClientService _rabbitMqClientService;

        public RabbitMqPublisherService(RabbitMqClientService rabbitMqClientService)
        {
            _rabbitMqClientService = rabbitMqClientService;
        }

        public void Publish(ImageCreatedEvent imageCreatedEvent)
        {
            var channel = _rabbitMqClientService.Connect();

            var bodyString = JsonSerializer.Serialize(imageCreatedEvent);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();

            properties.Persistent = true;

            channel.BasicPublish(exchange: RabbitMqClientService.ExchangeName, routingKey:  RabbitMqClientService.RoutingWatermark, basicProperties: properties, body: bodyByte);

        }
    }
}
