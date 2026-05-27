namespace ProjetoIntegrador2.API.DTOs;

public class CreateProdutoDto
{
    public string Codigo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public string Unid { get; set; } = string.Empty;
    public decimal Custo { get; set; }
    public decimal Preco { get; set; }
    public decimal Estoque { get; set; }
}
