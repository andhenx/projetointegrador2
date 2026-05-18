namespace ProjetoIntegrador2.API.DTOs;

public class CreateMovimentoDto
{
    public Guid ProdutoId { get; set; }
    public string Tipo { get; set; } = string.Empty; // "Entrada" ou "Saida"
    public decimal Qtde { get; set; }
    public decimal PrecoUnit { get; set; }
}
