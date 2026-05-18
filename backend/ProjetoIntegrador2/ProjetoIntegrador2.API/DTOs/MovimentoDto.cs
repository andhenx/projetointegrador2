namespace ProjetoIntegrador2.API.DTOs;

public class MovimentoDto
{
    public Guid Id { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Qtde { get; set; }
    public decimal PrecoUnit { get; set; }
    public decimal Total { get; set; }
}
