using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PayRight.Shared.Consumers;

public abstract class Consumer : BackgroundService
{
    protected readonly KafkaConfiguration _kafkaConfiguration;

    protected readonly ILogger _logger;

    public ConsumerConfig Config { get; }

    protected Consumer(KafkaConfiguration kafkaConfiguration, ILogger logger)
    {
        _kafkaConfiguration = kafkaConfiguration;
        _logger = logger;

        Config = new ConsumerConfig()
        {
            BootstrapServers = _kafkaConfiguration.BootstratpServers,
            GroupId = _kafkaConfiguration.GroupId,
            AutoOffsetReset = _kafkaConfiguration.AutoOffsetReset
        };
    }
}