
namespace Thoth.Scanner.Options;

public record ScannerOptions
{
    public string Diretorio { get; set; } = null!;
    public string Extensao { get; set;} = null!;
    public string TipoArquivo { get; set; } = null!;
}
