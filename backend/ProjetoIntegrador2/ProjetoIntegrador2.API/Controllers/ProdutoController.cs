using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador2.API.Data;
using ProjetoIntegrador2.API.DTOs;
using ProjetoIntegrador2.API.Entities;

namespace ProjetoIntegrador2.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProdutoController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAll()
    {
        var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
        return Ok(_mapper.Map<IEnumerable<ProdutoDto>>(produtos));
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDto>> Create([FromBody] CreateProdutoDto dto)
    {
        var produto = _mapper.Map<Produto>(dto);
        produto.Id = Guid.NewGuid();
        produto.Ativo = true;

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = produto.Id }, _mapper.Map<ProdutoDto>(produto));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProdutoDto>> Update(Guid id, [FromBody] UpdateProdutoDto dto)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        _mapper.Map(dto, produto);
        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<ProdutoDto>(produto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
