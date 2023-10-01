using Thoth.Core.Enums;

namespace Thoth.Core.Entidades
{
    public class ItemExtrato
    {
        public DateTime Data { get; set; }

        public Operacao Operacao { get; set; }

        public string Codigo { get; set; } = null!;

        public TipoMovimento TipoMovimento { get; set; }

        public string NomeProduto { get; set; } = null!;

        public string Instituicao { get; set; } = null!;

        public double Quantidade { get; set; }

        public double PrecoUnitario { get; set; }

        public Moeda Moeda { get; set; }

        public double ValorDaMoedaNoDia { get; set; }

        public double ValorOperacao { get; set; }
    }
}
