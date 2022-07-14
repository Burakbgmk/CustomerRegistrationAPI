using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedLibrary.Configuration;
using SharedLibrary.Events;
using SharedLibrary.Services;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedLibrary.BackgroundServices
{
    public class WatermarkImageBackgroundService : BackgroundService
    {
        private readonly RabbitMqClientService _rabbitMqClientService;
        private readonly ILogger<WatermarkImageBackgroundService> _logger;
        private IModel _channel;
        private readonly ImageRootFile _rootOptions;


        public WatermarkImageBackgroundService(RabbitMqClientService rabbitMqClientService, ILogger<WatermarkImageBackgroundService> logger, IOptions<ImageRootFile> options)
        {
            _rabbitMqClientService = rabbitMqClientService;
            _logger = logger;
            _rootOptions = options.Value;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMqClientService.Connect();

            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var consumer = new AsyncEventingBasicConsumer(_channel);


            _channel.BasicConsume(RabbitMqClientService.QueueName,false,consumer);
            consumer.Received += Consumer_Received;

            return Task.CompletedTask;
        }

        private Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {

            Task.Delay(1000).Wait();
            try
            {
                var imageCreatedEvent = JsonSerializer.Deserialize<ImageCreatedEvent>(Encoding.UTF8.GetString(@event.Body.ToArray()));

                var path = Path.Combine(Directory.GetCurrentDirectory(), _rootOptions.Original, imageCreatedEvent.ImageName);

                var siteName = "wwww.mysite.com";

                using var img = Image.FromFile(path);

                using var graphic = Graphics.FromImage(img);

                var font = new Font(FontFamily.GenericMonospace, 40, FontStyle.Bold, GraphicsUnit.Pixel);

                var textSize = graphic.MeasureString(siteName, font);

                var color = Color.FromArgb(128, 255, 255, 255);
                var brush = new SolidBrush(color);

                var position = new Point(img.Width - ((int)textSize.Width + 30), img.Height - ((int)textSize.Height + 30));


                graphic.DrawString(siteName, font, brush, position);


                img.Save(_rootOptions.Watermarked + imageCreatedEvent.ImageName);


                img.Dispose();
                graphic.Dispose();

                _channel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }


            return Task.CompletedTask;

        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
