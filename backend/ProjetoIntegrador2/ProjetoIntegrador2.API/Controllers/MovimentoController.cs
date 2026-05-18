using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador2.API.Data;
using ProjetoIntegrador2.API.DTOs;
using ProjetoIntegrador2.API.Entities;

namespace ProjetoIntegrador2.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentoController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public MovimentoController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovimentoDto>>> GetAll()
    {
        var movimentos = await _context.Movimentos
            .AsNoTracking()
            .OrderByDescending(m => m.Data)
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<MovimentoDto>>(movimentos));
    }

    [HttpPost]
    public async Task<ActionResult<MovimentoDto>> Create([FromBody] CreateMovimentoDto dto)
    {
        var produto = await _context.Produtos.FindAsync(dto.ProdutoId);
        if (produto == null)
            return NotFound("Produto não encontrado.");

        var tipo = dto.Tipo.Trim();

        // Não permite saída se não tiver estoque suficiente
        if (tipo.Equals("Saida", StringComparison.OrdinalIgnoreCase) && produto.Estoque < dto.Qtde)
            return BadRequest("Estoque insuficiente para realizar a saída.");

        var movimento = new Movimento
        {
            Id        = Guid.NewGuid(),
            Tipo      = tipo,
            Data      = DateTime.Now,
            NumNt     = string.Empty,
            Serie     = string.Empty,
            Chave     = string.Empty,
            Codigo    = produto.Codigo,
            Descricao = produto.Descricao,
            Qtde      = dto.Qtde,
            PrecoUnit = dto.PrecoUnit,
            Total     = dto.Qtde * dto.PrecoUnit
        };

        // Atualiza o estoque do produto conforme o tipo de movimento
        if (tipo.Equals("Entrada", StringComparison.OrdinalIgnoreCase))
            produto.Estoque += dto.Qtde;
        else
            produto.Estoque -= dto.Qtde;

        _context.Movimentos.Add(movimento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = movimento.Id }, _mapper.Map<MovimentoDto>(movimento));
    }
}
