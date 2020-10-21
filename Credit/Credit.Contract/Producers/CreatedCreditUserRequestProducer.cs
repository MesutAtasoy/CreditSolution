using Credit.Contract.IntegrationEvents;
using Framework.EventBusRabbitMQ;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Credit.Contract.Producers
{
    public class CreatedCreditUserRequestProducer: ProducerBase<CreatedCreditUserRequestEvent>
    {
        public CreatedCreditUserRequestProducer(ConnectionFactory connectionFactory,
            ILogger<RabbitMqClientBase> logger,
            ILogger<ProducerBase<CreatedCreditUserRequestEvent>> producerBaseLogger) :
            base(connectionFactory, logger, producerBaseLogger)
        {
        }
        
        protected readonly string CreditExchange = $"{VirtualHost}.CreditExchange";
        protected readonly string CreditQueue = $"{VirtualHost}.credit.message";
        protected const string CreditQueueAndExchangeRoutingKey = "credit.message";

        protected override string ExchangeName => "CreditExchange";
        protected override string RoutingKeyName => "credit.message";
        protected override string AppId => "Credit";
    }
}