using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thoth.Core.Eventos
{
    public class EventoBase
    {
        public DateTime DataHora { get; private set; }
        public string Origem { get; private set; }
        public string Tipo { get; private set; }
        protected EventoBase(string origemEvento, string tipoEvento)
        {
            DataHora = DateTime.Now;
            Origem = origemEvento;
            Tipo = tipoEvento;
        }
    }
}
