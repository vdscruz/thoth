using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;
using Thoth.Abstracoes.Mensageria;

namespace Thoth.Infra.Mensageria.Producer;

public class SQSProducer : IProducer
{
    private readonly string _iamAccessKey;
    private readonly string _iamSecretKey;
    private readonly string _region;


    public SQSProducer()
    {
        _iamAccessKey = Environment.GetEnvironmentVariable("AWS_IAM_ACCESS_KEY") ??
                throw new ArgumentNullException("Não encontramos a variável de ambiente AWS_IAM_ACCESS_KEY");

        _iamSecretKey = Environment.GetEnvironmentVariable("AWS_IAM_SECRET_KEY") ??
            throw new ArgumentNullException("Não encontramos a variável de ambiente AWS_IAM_SECRET_KEY");

        _region = Environment.GetEnvironmentVariable("AWS_REGION") ??
            throw new ArgumentNullException("Não encontramos a variável de ambiente AWS_REGION");
    }

    public async Task ProduceAsync<T>(string topic, T data) where T : class
    {
        var config = new AmazonSQSConfig
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(_region),
            ServiceURL = $"https://sqs.{_region}.amazonaws.com"

        };

        IAmazonSQS sqs = new AmazonSQSClient(_iamAccessKey, _iamSecretKey, config);
        var queue = await sqs.GetQueueUrlAsync(topic);
        var request = new SendMessageRequest
        {
            QueueUrl = queue.QueueUrl,
            MessageBody = JsonSerializer.Serialize(data, data.GetType())
        };

        await sqs.SendMessageAsync(request);
    }
}