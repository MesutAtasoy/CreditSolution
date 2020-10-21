using System;
using System.Threading;
using System.Threading.Tasks;
using Framework.EventBusRabbitMQ;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Sms.Api.Events;
using ConnectionFactory = RabbitMQ.Client.ConnectionFactory;

namespace Sms.Api.Consumers
{
    public class CreatedCreditUserRequestConsumer : ConsumerBase, IHostedService
    {
        protected override string QueueName => "CUSTOM_HOST.credit.message";

        public CreatedCreditUserRequestConsumer(
            IMediator mediator,
            ConnectionFactory connectionFactory,
            ILogger<CreatedCreditUserRequestConsumer> logConsumerLogger,
            ILogger<ConsumerBase> consumerLogger,
            ILogger<RabbitMqClientBase> logger) :
            base(mediator, connectionFactory, consumerLogger, logger)
        {
            try
            {
                var consumer = new AsyncEventingBasicConsumer(Channel);
                consumer.Received += OnEventReceived<CreatedCreditUserRequestEvent>;
                Channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                logConsumerLogger.LogCritical(ex, "Error while consuming message");
            }
        }

        public virtual Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }
    }
}