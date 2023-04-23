﻿using RabbitMQ.Client;

namespace ItemService.RabbitMqClient
{
    public class RabbitMqSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _nomeDaFila;

        public RabbitMqSubscriber(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new ConnectionFactory() { HostName = "localhost", Port = 8002 }.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _nomeDaFila = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _nomeDaFila, exchange: "trigger", routingKey: "");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
