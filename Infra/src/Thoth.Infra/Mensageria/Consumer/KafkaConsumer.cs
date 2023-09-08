using System.Text.Json.Serialization;
using Thoth.Abstracoes.Mensageria;

namespace Thoth.Infra.Mensageria.Consumer;

public class KafkaConsumer : IConsumer
{
    public IEnumerable<(T data, Action commit)> Consume<T>(string topic, JsonConverter converter, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}