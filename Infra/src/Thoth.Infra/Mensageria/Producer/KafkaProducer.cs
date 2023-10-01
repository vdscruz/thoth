using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Thoth.Abstracoes.Mensageria;

namespace Thoth.Infra.Mensageria.Producer;

public class KafkaProducer : IProducer
{
    private readonly ProducerConfig _config;
    private readonly ILogger<KafkaProducer> _logger;
    private IProducer<string, string>? _producer;

    public KafkaProducer(ILogger<KafkaProducer> logger, IOptions<ProducerConfig> producerConfig)
    {
        _config = producerConfig.Value;
        _logger = logger;
    }

    public Task ProduceAsync<T>(string topic, T data) where T : class
    {
        _producer = _producer ?? new ProducerBuilder<string, string>(_config)
            .SetKeySerializer(Serializers.Utf8)
            .SetValueSerializer(Serializers.Utf8)
            .Build();

        var eventMessage = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(data, data.GetType())
        };

        return _producer.ProduceAsync(topic, eventMessage)
            .ContinueWith((task) =>
            {
                DeliveryResult<string, string> deliveryResult = task.Result;
                if (deliveryResult.Status == PersistenceStatus.NotPersisted)
                {
                    throw new Exception($"Could not produce {data.GetType().Name} message to topic - {topic} due to the following reason: {task.Result.Message}.");
                }
            });

        
    }
}