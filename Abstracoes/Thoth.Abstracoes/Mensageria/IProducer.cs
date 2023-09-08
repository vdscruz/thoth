namespace Thoth.Abstracoes.Mensageria
{
    public interface IProducer
    {
        Task ProduceAsync<T>(string topic, T data) where T : class;
    }
}