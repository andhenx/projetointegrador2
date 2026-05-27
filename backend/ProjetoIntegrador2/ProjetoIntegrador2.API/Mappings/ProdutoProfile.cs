using AutoMapper;
using ProjetoIntegrador2.API.DTOs;
using ProjetoIntegrador2.API.Entities;

namespace ProjetoIntegrador2.API.Mappings;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<Produto, ProdutoDto>();
        CreateMap<CreateProdutoDto, Produto>();
        CreateMap<UpdateProdutoDto, Produto>();
        CreateMap<Movimento, MovimentoDto>();
    }
}
