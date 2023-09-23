using Thoth.Core.Entidades;
using Thoth.Servico.Dto.ProcessamentoDados;

namespace Thoth.Servico.Interfaces.ProcessamentoArquivos
{
    public interface IProcessaArquivo
    {
        IEnumerable<ItemExtrato> ObterItensExtrato(Arquivo arq);
    }
}
