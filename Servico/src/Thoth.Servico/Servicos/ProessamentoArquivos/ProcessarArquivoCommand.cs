
using Mediator;
using Thoth.Servico.Dto.ProcessamentoDados;

namespace Thoth.Servico.Servicos.ProessamentoArquivos;

public record ProcessarArquivoCommand : IRequest<bool>
{
    public ProcessarArquivoCommand(string arquivo, string tipoArquivo, string origem)
    {
        Arquivo = arquivo;
        TipoArquivo = tipoArquivo;
        Origem = origem;
    }

    public string Arquivo { get; }
    public string TipoArquivo { get; }
    public string Origem { get; }
}

public static class ProcessarArquivoCommandExtensions
{
    public static Arquivo ToArquivo(this ProcessarArquivoCommand command)
    {
        return new Arquivo(command.Arquivo, command.TipoArquivo);
    }
}
