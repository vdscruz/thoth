namespace Thoth.Abstracoes.Mensageria
{
    /// <summary>
    /// Interface que define um consumidor de mensagens.
    /// </summary>
    public interface IConsumer
    {
        /// <summary>
        /// Consome mensagens de um tópico específico.
        /// </summary>
        /// <typeparam name="T">O tipo de dados das mensagens a serem consumidas.</typeparam>
        /// <param name="topic">O nome do tópico a partir do qual as mensagens serão consumidas.</param>
        /// <param name="converter">O conversor JSON a ser usado para desserializar as mensagens.</param>
        /// <param name="cancellationToken">Um token de cancelamento para interromper a operação de consumo.</param>
        /// <returns>Um enumerável de tuplas, cada uma contendo os dados da mensagem e uma ação de confirmação de commit.</returns>        
        IEnumerable<(T data, Action commit)> Consume<T>(string topic, JsonConverter converter, CancellationToken cancellationToken);
    }
}