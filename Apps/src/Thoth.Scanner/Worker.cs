using Mediator;
using Microsoft.Extensions.Options;
using Thoth.Scanner.Options;
using Thoth.Servico.Servicos.ProessamentoArquivos;

namespace Thoth.Scanner;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMediator _mediator;
    private readonly ScannerOptions _options;

    public Worker(ILogger<Worker> logger, 
        IOptions<ScannerOptions> options,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
        _options = options.Value;

        
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogWarning("ExecuteAsync");

        return Task.Run(async () =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Vamos verificar o diretório: {dir}", _options.Diretorio);

                string[] arquivos = Directory.GetFiles(_options.Diretorio
                    , $"*.{_options.Extensao}");

                if (arquivos.Any())
                {
                    foreach (var arquivo in arquivos)
                    {
                        if (ArquivoEmUso(arquivo))
                        {
                            _logger.LogInformation("Arquivo em uso: {arquivo}", arquivo);
                            continue;
                        }

                        _logger.LogInformation("Processar arquivo: {arquivo}", arquivo);
                        var command = new ProcessarArquivoCommand(arquivo, _options.TipoArquivo, "Thoth.Scanner");
                        var sucesso = await _mediator.Send(command, stoppingToken).ConfigureAwait(false);

                        if (sucesso)
                        {
                            _logger.LogInformation("{arquivo} Processado com sucesso!", arquivo);
                            File.Delete(arquivo);
                        }
                        else _logger.LogInformation("{arquivo} Não foi processado!", arquivo);
                    }
                }
                else
                {
                    _logger.LogInformation("Nenhum arquivo encontrado no diretório: {dir}", _options.Diretorio);
                }

                await Task.Delay(1000, stoppingToken);
            }
        });
        
    }

    private bool ArquivoEmUso(string arquivo)
    {
        try
        {
            using (var fileStream = new FileStream(arquivo, FileMode.Open, FileAccess.Read))
            {
                return false;
            }
        }
        catch (IOException)
        {
            return true;
        }
    }
}
