

namespace Thoth.Servico.Dto.ProcessamentoDados
{
    public abstract class Arquivo
    {
        public string Caminho { get; private set; }
        public string TipoArquivo { get; private set; }

        public Arquivo(string caminhoDoArquivo, string tipo)
        {
            Caminho = caminhoDoArquivo;
            TipoArquivo = tipo;
        }
    }
}
