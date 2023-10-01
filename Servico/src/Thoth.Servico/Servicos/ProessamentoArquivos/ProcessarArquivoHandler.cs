using Mediator;
using Microsoft.Extensions.Logging;
using Thoth.Abstracoes.Mensageria;
using Thoth.Core.Eventos;
using Thoth.Servico.Interfaces.ProcessamentoArquivos;

namespace Thoth.Servico.Servicos.ProessamentoArquivos;

public class ProcessarArquivoHandler : IRequestHandler<ProcessarArquivoCommand, bool>
{
    private readonly ILogger<ProcessarArquivoHandler> _logger;
    private readonly IEnumerable<IProcessaArquivo> _processadores;
    private readonly IProducer _producer;

    public ProcessarArquivoHandler(ILogger<ProcessarArquivoHandler> logger, IEnumerable<IProcessaArquivo> processadores, IProducer producer)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _processadores = processadores ?? throw new ArgumentNullException(nameof(processadores));
        _producer = producer ?? throw new ArgumentNullException(nameof(producer));
    }

    public async ValueTask<bool> Handle(ProcessarArquivoCommand command, CancellationToken cancellationToken)
    {
        var processador = _processadores.FirstOrDefault(p=> string.Equals(p.TipoArquivo, command.TipoArquivo, StringComparison.OrdinalIgnoreCase));
        if (processador != null)
        {
            var items = processador.ObterItensExtrato(command.ToArquivo());
            if (items != null)
            {
                var evento = new ArquivoProcessadoEvent(command.Origem, items);
                await _producer.ProduceAsync("thoth_evento_arquivo_processado", evento);
                return true;
            }
        }
        else
        {
            _logger.LogDebug("Processador não encontrado...");
        }

        return false;
    }
}
