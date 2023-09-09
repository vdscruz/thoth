using Thoth.Abstracoes.Mensageria;

namespace Thoth.Infra.Mensageria.Producer;

public class KafkaProducer : IProducer
{
    public Task ProduceAsync<T>(string topic, T data) where T : class
    {
        throw new NotImplementedException();
    }
}