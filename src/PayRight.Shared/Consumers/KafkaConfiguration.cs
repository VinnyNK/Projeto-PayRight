using Confluent.Kafka;

namespace PayRight.Shared.Consumers;

public class KafkaConfiguration
{
    public string GroupId { get; set; }

    public string BootstratpServers { get; set; }

    public AutoOffsetReset AutoOffsetReset  { get; set; }

    public string Topics { get; set; }
}