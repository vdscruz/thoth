using Thoth.Core.Entidades;

namespace Thoth.Core.Eventos
{
    public class ArquivoProcessadoEvent: EventoBase
    {        
        public IEnumerable<ItemExtrato> ItensExtrato { get; private set; }

        public ArquivoProcessadoEvent(string origem, IEnumerable<ItemExtrato> items) : base(origem, nameof(ArquivoProcessadoEvent))
        {
            ItensExtrato = items;
        }
    }
}
