namespace ProjetoIntegrador2.API.Entities;

public class Movimento
{
    public Guid Id { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public string NumNt { get; set; } = string.Empty;
    public string Serie { get; set; } = string.Empty;
    public string Chave { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Qtde { get; set; }
    public decimal PrecoUnit { get; set; }
    public decimal Total { get; set; }
}
